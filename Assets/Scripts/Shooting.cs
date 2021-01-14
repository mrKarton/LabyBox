using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float cooldown;
    public GameObject bullet;
    public float bulletSpeed;

    private float _cooldown;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject sawned = Instantiate(bullet, transform.position + transform.forward * 1.5f, transform.rotation);
            sawned.GetComponent<Rigidbody>().AddForce(sawned.transform.forward * Time.deltaTime * bulletSpeed, ForceMode.Impulse);
            Destroy(sawned, 10f);
        }
    }
}
