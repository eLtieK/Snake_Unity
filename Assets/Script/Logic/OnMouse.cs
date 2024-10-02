using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnMouse : MonoBehaviour
{
    void OnMouseEnter()
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 1);
        Debug.Log("Mouse Enter");
    }
    void OnMouseExit()
    {
        transform.localScale = new Vector2(1, 1);
    }
}
