using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartonWeapons;

public class Physgun : Gun
{
    public GameObject goal;
    public bool goaled;
    public float sensitivity;
    public LineRenderer renderer;

    private Transform head;
    private Quaternion originRotation;
    private Transform originParent;
    private bool originGravity;
    private RigidbodyConstraints originConstraints;

    private Move mover;
    private void Start()
    {
        head = transform.parent.parent;
        renderer.SetPosition(0, transform.position);

        renderer.SetPosition(99, transform.position);

        mover = GameObject.FindObjectOfType<Move>();
    }

    public override void Shoot()
    {
        if(!goaled)
        {
            renderer.SetPosition(0, transform.position);
            renderer.SetPosition(99, transform.position + head.forward * 10 );
            
            RaycastHit hit = new RaycastHit();
            Ray ray = new Ray(head.position, head.forward);
            if (Physics.Raycast(ray, out hit, 10f))
            {
                if (hit.collider.gameObject.GetComponent<HealthManager>())
                {
                    goal = hit.collider.gameObject;
                    originParent = goal.transform.parent;
                    goal.transform.parent = head;
                    goaled = true;
                    originRotation = goal.transform.rotation;

                    if(goal.GetComponent<Rigidbody>())
                    {
                        originGravity = goal.GetComponent<Rigidbody>().useGravity;
                        goal.GetComponent<Rigidbody>().useGravity = false;
                        goal.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                    }

                    head.gameObject.GetComponent<GunInventoryController>().canChange = false;
                }
            }
        }
        else
        {
            
            renderer.SetPosition(0, transform.position);
            //renderer.SetPosition(1, Vector3.Lerp(renderer.GetPosition(1), goal.transform.position, Time.deltaTime * 2));
            renderer.SetPosition(99, goal.transform.position);
            if(Input.GetKey(KeyCode.Q))
            {
                if(goal.GetComponent<Rigidbody>())
                {
                    //goal.GetComponent<Rigidbody>().AddForce(goal.transform.up * Input.GetAxis("Mouse Y") * sensitivity);
                    goal.transform.position += transform.up * Input.GetAxis("Mouse Y") * sensitivity;
                }
            }
            goal.transform.position = Vector3.MoveTowards(goal.transform.position, head.position,
                Time.deltaTime * 100 * Input.GetAxis("Mouse ScrollWheel"));

            if(Input.GetKeyDown(KeyCode.R))
            {
                mover.canLook = false;
            }
            if(Input.GetKeyUp(KeyCode.R))
            {
                mover.canLook = true;
            }
            if(Input.GetKey(KeyCode.E))
            {
                float X, Y = 0;
                X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
                Y += Input.GetAxis("Mouse Y") * sensitivity * 4;
                Y = Mathf.Clamp(Y, -90, 90);
                goal.transform.eulerAngles =  new Vector3(-Y, X, 0);
            }

            goal.transform.rotation = originRotation;
        }
    }

    public override void StopShooting()
    {
        if (goaled)
        {
            renderer.SetPosition(0, transform.position);
            renderer.SetPosition(99, transform.position);

            Debug.Log("Stopped Shooting");
            goal.transform.parent = originParent;
            goal.GetComponent<Rigidbody>().useGravity = originGravity;
            goal.GetComponent<Rigidbody>().constraints = originConstraints;
            goal = null;
            goaled = false;
            head.gameObject.GetComponent<GunInventoryController>().canChange = true;
        }
    }

    private void FixedUpdate()
    {
        if(! goaled)
        {
            renderer.SetPosition(0, transform.position);
            renderer.SetPosition(99, transform.position);
            for (int i = 98; i > 0; i--)
            {
                renderer.SetPosition(i, transform.position);
            }
        }
        if (goaled)
        {
            for (int i = 98; i > 0; i--)
            {
                renderer.SetPosition(i, Vector3.Lerp(renderer.GetPosition(i), renderer.GetPosition(i + 1), Time.deltaTime * 40));
            }
        }
    }
}
