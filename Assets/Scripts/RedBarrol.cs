using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBarrol : MonoBehaviour
{
    public GameObject explosion;
    public float explosionRadius;
    public float explosionDamage;
    public float explosionForce;
    public HealthManager hm;
    public bool exploded = false;

    private void Start()
    {
        hm = GetComponent<HealthManager>();
    }

    private void FixedUpdate()
    {
        if(hm.health <= 0 && !exploded)
        {
            var expl = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(expl, 2.20f);
            exploded = true;
            GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject, 2.20f);

            HealthManager[] hms = GameObject.FindObjectsOfType<HealthManager>();
            foreach(HealthManager mngr in hms)
            {
                Debug.Log(mngr.gameObject.name + " - " + Vector3.Distance(transform.position, mngr.transform.position));
                if(Vector3.Distance(transform.position, mngr.transform.position) < explosionRadius && mngr != this)
                {
                    mngr.Damage(explosionDamage * (explosionRadius / Vector3.Distance(transform.position, mngr.transform.position)));

                    if(mngr.gameObject.GetComponent<Rigidbody>())
                    {
                        Transform _transform = mngr.transform;
                        _transform.LookAt(this.transform);
                        mngr.GetComponent<Rigidbody>().AddForce(_transform.forward * -1 * explosionForce * ((explosionRadius / Vector3.Distance(transform.position, mngr.transform.position))));
                    }
                }
            }
        }
    }
}
