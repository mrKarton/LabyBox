using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float rotSpeed;
    public Camera cam;
    public float sensitivity;
    public Transform a;
    public Transform b;
    public float aimRadius;

    public Transform head;
    private void Start()
    {
        rb = transform.parent.gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(0, 0, Input.GetAxis("Vertical")) * Time.deltaTime * speed);
        rb.AddForce(new Vector3(Input.GetAxis("Horizontal"), 0, 0) * Time.deltaTime * speed);

        Vector3 mouseWorldPosition = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        float mouseZ = mouseWorldPosition.y + 0.5f;

        Vector3 position = transform.parent.position + new Vector3(mouseWorldPosition.x, 0, mouseZ) * sensitivity;
        a.position = transform.parent.position + Vector3.ClampMagnitude(position - transform.parent.position, aimRadius);
        head.LookAt(a);

        Vector3 pos = b.position;
        pos += b.forward * Input.GetAxis("Vertical") * Time.deltaTime * speed;
        pos += b.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed;

        b.position = transform.parent.position + Vector3.ClampMagnitude(pos - transform.parent.position, aimRadius);
        transform.LookAt(b);
    }
}
