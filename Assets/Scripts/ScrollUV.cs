using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUV : MonoBehaviour
{
    public float speedModifier;


    void Update()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mat = mr.material;

        Vector2 offset = mat.mainTextureOffset;

        offset.x += Time.deltaTime / speedModifier;

        mat.mainTextureOffset = offset;

        if (GameManager.GetInstance().incSpeed)
        {
            speedModifier -= 1f;
        }
    }
}
