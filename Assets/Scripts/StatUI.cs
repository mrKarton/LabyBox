using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    [Header("Mouse Statistic")]
    public Text inputMouse;
    public Text mousePosition;
    public Text mouseWorldPos;
    public Text mouseRayPos;
    public bool mouseEnable = true;

    private void FixedUpdate()
    {
        if (mouseEnable)
        {
            inputMouse.text = "INPUT X:" + Input.GetAxis("Mouse X") + " Y:" + Input.GetAxis("Mouse Y");
            mousePosition.text = "POSITION: " + Input.mousePosition;
            mouseWorldPos.text = "WORLD: " + Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseRayPos.text = "RAY: " + Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        }
    }
}
