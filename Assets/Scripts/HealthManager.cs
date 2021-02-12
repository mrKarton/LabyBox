using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public Vector3 barOffest;
    public bool barEnabled;
    public bool destroyable = true;
    public SpriteRenderer bar;

    private float barMaxWidth;

    private bool showingBar;
    
    private void Start()
    {
        barMaxWidth = bar.gameObject.transform.localScale.x;
        bar.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        
    }

    public void Damage(float count)
    {
        health -= count;
        if(health <= 0)
        {
            Die();
        }
        //StopCoroutine(ShowBar());
        if (!showingBar)
        {
            StartCoroutine(ShowBar());
            StartCoroutine(HideBar());
        }
    }

    public void FixedUpdate()
    {
        

        if(showingBar)
        {
            bar.transform.localScale = Vector3.Lerp(bar.transform.localScale,
            new Vector3((health / maxHealth) * barMaxWidth, bar.transform.localScale.y, 0), Time.deltaTime * 10);

            bar.transform.position = transform.position + barOffest;
            bar.transform.rotation = Quaternion.identity;
        }
    }

    public virtual void Die()
    {
        showingBar = false;
        StopAllCoroutines();
        bar.gameObject.SetActive(false);
        Destroy(this.gameObject, 10f);

        if(GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().mass = 0;
        }
    }

    public void Damage(float min, float max)
    {
        Damage(Random.Range(min, max));
    }

    public IEnumerator ShowBar()
    {
        SpriteRenderer sprite = bar.gameObject.GetComponent<SpriteRenderer>();
        showingBar = true;
        while (sprite.color.a != 0.5 && showingBar)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, Mathf.Lerp(sprite.color.a, 1, Time.deltaTime));
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator HideBar()
    {
        SpriteRenderer sprite = bar.gameObject.GetComponent<SpriteRenderer>();
        yield return new WaitForSeconds(2f);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
        StopCoroutine(ShowBar());
        showingBar = false;
    }
}
