using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingLavaBlocks : MonoBehaviour
{
    private Rigidbody rigidbodyComponent;
    public Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();

        startPosition = rigidbodyComponent.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        MyPlayer player = collision.gameObject.GetComponent<MyPlayer>();

        //fall if overhead collision with player
        if (player != null && collision.contacts[0].normal.y < -0.5)
        {
            StartCoroutine(Fall());

        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds( 2 );

        rigidbodyComponent.isKinematic = false;

        rigidbodyComponent.useGravity = true;
    }
}
