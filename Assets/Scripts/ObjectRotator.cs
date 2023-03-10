using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = Random.Range(0.3f * rotationSpeed, 1.3f * rotationSpeed);
    }

    void FixedUpdate()
    {
        if(GameManager.instance.inGame)
        {
            transform.Rotate(new Vector3(0f, 0f, rotationSpeed));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
