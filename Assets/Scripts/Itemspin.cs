using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemspin : MonoBehaviour
{
    public bool rotatex;
    

    void Update()
    {
        if (rotatex)
        {
            transform.Rotate(0f, 0f , 100f * Time.deltaTime, Space.Self);
        }
        else
        transform.Rotate(0f, 100f * Time.deltaTime, 0f, Space.Self);   
    }
}
