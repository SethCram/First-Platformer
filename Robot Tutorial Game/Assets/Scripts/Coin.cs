using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //coin dissapears on contact with anything:
    private void OnTriggerEnter(Collider other)
    {
        gameObject.transform.localScale = new Vector3(0, 0, 0);

        gameObject.transform.position = new Vector3(90, 0, 90);

        print("Coin dissapeared");
    }

}
