using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartonWeapons
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Karton Inc./New Gun")]
    public class GunData : ScriptableObject
    {
        public string menuName;
        public Sprite menuPic;

        public GameObject prefab;

        public int shotCount;
        public int magazine;
        public int maxBullets;

        public float fireRate;


    }
}