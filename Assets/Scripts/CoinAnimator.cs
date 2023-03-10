using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float rotationSpeed;

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0f, rotationSpeed, 0f));
    }
}
