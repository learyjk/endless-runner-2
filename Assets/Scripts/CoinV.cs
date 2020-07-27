using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinV : MonoBehaviour
{
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = GameManager.GetInstance().GetObjectSpeed();
    }

    // Update is called once per frame
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
