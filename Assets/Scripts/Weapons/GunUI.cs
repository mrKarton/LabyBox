using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore;

namespace KartonWeapons
{
    public class GunUI : MonoBehaviour
    {
        public Image icon;
        public Text counter;

        public ShootingController controller;

        private void Start()
        {
            controller = GameObject.FindObjectOfType<ShootingController>();
        }

        private void FixedUpdate()
        {
            Gun data = controller.activeGun;
            counter.text = data.magazine.ToString() + "/" + data.bulletsCount.ToString();
            icon.sprite = data.data.menuPic;
        }
    }
}