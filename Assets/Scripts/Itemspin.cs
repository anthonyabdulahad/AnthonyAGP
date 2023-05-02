using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemspin : MonoBehaviour
{

    

    void Update()
    {
        transform.Rotate(0f, 100 * Time.deltaTime, 0f, Space.Self);   
    }
}
