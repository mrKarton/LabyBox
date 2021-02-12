using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartonWeapons
{
    public class ShootingController : MonoBehaviour
    {
        public Gun activeGun;
        public GameObject gunSlot;

        public List<GunData> inventory = new List<GunData>();
        public Dictionary<GunData, GameObject> gunObjs = new Dictionary<GunData, GameObject>();

        bool shooting;
        public bool canShoot = true;

        public int activeIndex;

        private void Start()
        {
            foreach(GunData gd in inventory)
            {
                var obj = Instantiate(gd.prefab, gunSlot.transform.position, gunSlot.transform.rotation);
                obj.transform.parent = gunSlot.transform;
                obj.SetActive(false);
                gunObjs.Add(gd, obj);

                Gun gun = obj.GetComponent<Gun>();
                gun.magazine = gd.magazine;
                gun.bulletsCount = gd.maxBullets;
            }
            SetActiveGun(0);
        }

        private void Update()
        {
            if (Input.GetKeyDown(Settings.shootKey) && !shooting && canShoot)
            {
                StartCoroutine(Shoot());
                shooting = true;
            }
            else if(Input.GetKeyUp(Settings.shootKey))
            {
                activeGun.StopShooting();
                shooting = false;
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                activeGun.Reload();
            }
        }

        public void SetActiveGun(int i)
        {
            foreach(GameObject obj in gunObjs.Values)
            {
                obj.SetActive(false);
            }
            GunData data = inventory[i];

            gunObjs[data].SetActive(true);
            activeGun = gunObjs[data].GetComponent<Gun>();

            activeIndex = i;
        }

        public IEnumerator Shoot()
        {
            while (Input.GetKey(Settings.shootKey))
            {
                Debug.Log("Shooting");
                activeGun.Shoot();

                yield return new WaitForSeconds(activeGun.data.fireRate);
            }
        }

        public void AddGun(GunData gd)
        {
            inventory.Add(gd);
            var obj = Instantiate(gd.prefab, gunSlot.transform.position, gunSlot.transform.rotation);
            obj.transform.parent = gunSlot.transform;
            obj.SetActive(false);
            gunObjs.Add(gd, obj);

            Gun gun = obj.GetComponent<Gun>();
            gun.magazine = gd.magazine;
            gun.bulletsCount = gd.maxBullets;
        }

        public void DropGun(int i)
        {
            GunData gd = inventory[i];
            Destroy(gunObjs[gd]);
            gunObjs.Remove(gd);
            inventory.Remove(gd);

            //Тут должен быть код создания дропнутой пушки на карте, но я пока не прописал дропнутые пушки :p
        }
    }
}