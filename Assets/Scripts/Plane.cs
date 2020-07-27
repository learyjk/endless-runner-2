using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    private float speed;


    void Start()
    {
        speed = GameManager.GetInstance().GetObjectSpeed();
    }

    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (speed != 0)
        {
            transform.Translate(new Vector3(speed, 0, 0));
        }  
    }
}
