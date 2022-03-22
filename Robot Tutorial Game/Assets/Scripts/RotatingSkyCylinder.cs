using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSkyCylinder : MonoBehaviour
{
    [SerializeField] float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //rotate the x coord:
        gameObject.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        
    }
}
