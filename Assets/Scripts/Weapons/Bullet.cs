using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace KartonWeapons
{
    public class Bullet : MonoBehaviour
    {
        public float damage;
        public float speed;

        public float lifeTime;

        public virtual void EndLife()
        {
            Destroy(this.gameObject, 0.1f);
        }

        public virtual void EntLifeByTime()
        {
            EndLife();
        }

        public virtual void Hit(Collision collision)
        {
            if(collision.gameObject.GetComponent<HealthManager>())
            {
                collision.gameObject.GetComponent<HealthManager>().Damage(damage);
            }
            EndLife();
        }

        public virtual void Init()
        {

            Debug.Log("Init");

            StartCoroutine(LifeTimer());
            
            GetComponent<Rigidbody>().AddForce(transform.forward * Time.deltaTime * speed, ForceMode.Impulse);
        }

        public IEnumerator LifeTimer()
        {
            yield return new WaitForSeconds(lifeTime);

            EntLifeByTime();
        }

        private void OnCollisionEnter(Collision collision)
        {
            Hit(collision);
        }
    }
}