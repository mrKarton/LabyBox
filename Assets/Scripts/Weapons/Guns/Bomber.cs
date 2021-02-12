using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartonWeapons;
public class Bomber : Gun
{
    public override void Shoot()
    {
        if (magazine > 0)
        {
            Debug.Log(transform.parent.parent.rotation);
            GameObject bullet = Instantiate(bulletPref.gameObject, transform.parent.parent.position + transform.parent.parent.forward * 2f, transform.parent.parent.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletPref.speed, ForceMode.Impulse);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.up * bulletPref.speed / 2, ForceMode.Impulse);
            bullet.GetComponent<Bullet>().Init();
            magazine--;
        }
    }
}
