using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunScript : MonoBehaviour
{
    public GameObject box;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Transform body = GameObject.FindObjectOfType<Move>().transform;

            Instantiate(box, body.position + body.forward * 3, Quaternion.identity);
        }
    }
}
