using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScroller : MonoBehaviour
{
    void FixedUpdate()
    {
        if(GameManager.instance.inGame)
        {
            transform.position -= new Vector3(GameManager.instance.worldScrollingSpeed, 0f, 0f);
        }
    }
}
