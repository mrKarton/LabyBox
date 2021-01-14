using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public Vector3 barOffest;
    public bool barEnabled;

    public SpriteRenderer bar;

    private float barMaxWidth;

    private void Start()
    {
        barMaxWidth = bar.gameObject.transform.localScale.x;
        bar.gameObject.SetActive(false);
    }

    public void Damage(float count)
    {
        health -= count;

        if(barEnabled)
        {
            bar.transform.localScale = new Vector3((health / maxHealth) * barMaxWidth, bar.transform.localScale.y, 0);
        }
    }

    public void Damage(float min, float max)
    {
        Damage(Random.Range(min, max));
    }    
}
