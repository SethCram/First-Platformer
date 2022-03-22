using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBall : MonoBehaviour
{
    public Vector3 startPosition;
    Rigidbody rigidbodyComponent;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();

        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        rigidbodyComponent.velocity = new Vector3(0, 0, 0);

        gameObject.transform.position = startPosition;

    }
}
