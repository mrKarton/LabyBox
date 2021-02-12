using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartonWeapons
{
    public class Gun : MonoBehaviour
    {
        public GunData data;
        public Bullet bulletPref;
        public int magazine;
        public int bulletsCount;

        public virtual void Shoot()
        {
            if (magazine > 0)
            {
                GameObject bullet = Instantiate(bulletPref.gameObject, transform.parent.parent.position + transform.parent.parent.forward * 2f, transform.parent.parent.rotation);
                bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * bulletPref.speed, ForceMode.Impulse);
                bullet.GetComponent<Bullet>().Init();
                magazine--;
            }
        }

        public virtual void StopShooting()
        {

        }

        public virtual void Reload()
        {
            if(bulletsCount - data.magazine >= 0)
            {
                bulletsCount -= data.magazine;
                magazine = data.magazine;
            }
            else
            {
                magazine = bulletsCount;
                bulletsCount = 0;
            }
        }
    }
}
