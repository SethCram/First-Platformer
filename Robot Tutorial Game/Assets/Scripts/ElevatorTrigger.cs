using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    public bool playerOnElevator = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        playerOnElevator = true;
    }

    private void OnTriggerStay(Collider other)
    {
        //other

        playerOnElevator = true;
    }

    private void OnTriggerExit(Collider other)
    {
        //set elevator to not move player when this happens
        playerOnElevator = false;
        
    }
}
