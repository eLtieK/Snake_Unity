using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float destroyTime = 3f;
    // Update is called once per frame
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
