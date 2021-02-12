using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartonWeapons
{
    public class GunInventoryController : MonoBehaviour
    {
        ShootingController controller;
        public bool canChange = true;

        private void Start()
        {
            controller = GetComponent<ShootingController>();
        }


        void Update()
        {
            if (canChange)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0)
                {
                    if (controller.activeIndex + 1 > controller.inventory.Count - 1)
                    {
                        controller.SetActiveGun(0);
                    }
                    else
                    {
                        controller.SetActiveGun(controller.activeIndex + 1);
                    }
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    if (controller.activeIndex - 1 < 0)
                    {
                        controller.SetActiveGun(controller.inventory.Count - 1);
                    }
                    else
                    {
                        controller.SetActiveGun(controller.activeIndex - 1);
                    }
                }
            }
        }
    }
}