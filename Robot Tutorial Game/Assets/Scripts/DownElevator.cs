using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownElevator : MonoBehaviour
{
    [SerializeField] float elevatorSpeed;
    [SerializeField] float minElevatorHeight;
    [SerializeField] ElevatorTrigger upElevatorTrigger;

    private Rigidbody rigidBodyComponent; //to maximize code effeciency
    private bool shouldMoveUp = false;
    private bool isMoving;
    private bool shouldMoveDown = false;
    private Vector3 startPosition;
    private GameObject storedCollidedGameObject;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();

        rigidBodyComponent.isKinematic = true; //doesnt allow normal physics

        startPosition = rigidBodyComponent.position; //grabs+saves starting position

        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if gets stuck:
        if( shouldMoveDown == true && shouldMoveUp == true)
        {
            shouldMoveDown = false;
        }

        //stop moving down
        if( gameObject.transform.position.y < minElevatorHeight )
        {
            shouldMoveDown = false;

            isMoving = false;
        }

        //stop moving up
        if(gameObject.transform.position.y > startPosition.y )
        {
            shouldMoveUp = false;

            isMoving = false;
        }

        //move elevator up
        if( shouldMoveUp == true)
        {

            //translate player w/ on elevator:
            if (upElevatorTrigger.playerOnElevator == true)
            {
                storedCollidedGameObject.transform.Translate(Vector3.up * Time.deltaTime * elevatorSpeed);
            }

            //translate elevator
            gameObject.transform.Translate(Vector3.up * Time.deltaTime * elevatorSpeed); //move elevator up
        }

        //move elevator down
        if( shouldMoveDown == true )
        {
            //translate player w/ on elevator:
            if (upElevatorTrigger.playerOnElevator == true)
            {
                storedCollidedGameObject.transform.Translate(Vector3.up * Time.deltaTime * elevatorSpeed * -1);
            }

            //translate elevator
            gameObject.transform.Translate(Vector3.up * Time.deltaTime * elevatorSpeed * -1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //elevator moves when player steps on it:
        if (isMoving == false)
        {
            if (gameObject.transform.position.y < minElevatorHeight)
            {
                StartCoroutine(Ascend());
            }
            else
            {

                StartCoroutine(Descend());
            }
        }

        collidedGameObject(collision.gameObject); //pass game obj collided with
        Debug.Log("elevator collision");
    }

    private void collidedGameObject(GameObject gameObject)
    {
        storedCollidedGameObject = gameObject;
    }

    private IEnumerator Ascend()
    {

        //anything after this happens X seconds later:
        yield return new WaitForSeconds(2);

        shouldMoveUp = true;

        isMoving = true;
    }

    private IEnumerator Descend()
    {
        yield return new WaitForSeconds(2);

        shouldMoveDown = true;

        isMoving = true;
    }

    //reset elevator's position
    public void Reset()
    {
        rigidBodyComponent.position = startPosition;

        shouldMoveDown = false;

        shouldMoveUp = false;

        isMoving = false;
    }
}
