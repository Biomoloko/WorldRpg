using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private Camera cam;
    private GameObject placeHolder;
    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        
    }

    public void ButtonTyper(GameObject buttonType)
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //if (buttonType.name == "SquareBackBig")
        //{
        //    mousePos.x = Mathf.Round(mousePos.x) - 2;
        //    mousePos.y = Mathf.Round(mousePos.y) - 2;
        //    mousePos.z = 0;
        //}
        //else if (buttonType.name == "SquareBack")
        //{
        //    mousePos.x = Mathf.Round(mousePos.x) - 0.5f;
        //    mousePos.y = Mathf.Round(mousePos.y) - 0.5f;
        //    mousePos.z = 0;
        //}


        if (placeHolder != null)
        {
            Destroy(placeHolder);
        }
        placeHolder = Instantiate(buttonType, mousePos, Quaternion.identity);
    }
  
}
