using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartonWeapons;

public class Bomb : Bullet
{
    public float explosionRadius;
    public float explosionForce;

    public override void Hit(Collision collision)
    {
        if(collision.gameObject.GetComponent<HealthManager>())
        {
            EndLife();
        }
    }

    public override void EndLife()
    {
        HealthManager[] hms = GameObject.FindObjectsOfType<HealthManager>();
        foreach (HealthManager mngr in hms)
        {

            Debug.Log(mngr.gameObject.name + " - " + Vector3.Distance(transform.position, mngr.transform.position));
            if (Vector3.Distance(transform.position, mngr.transform.position) < explosionRadius && mngr != this)
            {
                mngr.Damage(damage * (explosionRadius / Vector3.Distance(transform.position, mngr.transform.position)));
                if (mngr.gameObject.GetComponent<Rigidbody>())
                {
                    Transform _transform = mngr.transform;
                    _transform.LookAt(this.transform);
                    mngr.GetComponent<Rigidbody>().AddForce(_transform.forward * -1 * explosionForce * ((explosionRadius / Vector3.Distance(transform.position, mngr.transform.position))));
                }
            }
        }
        Destroy(this.gameObject);
    }
}
