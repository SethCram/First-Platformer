//declaring namespace:
using System;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine; 

public class MyPlayer : MonoBehaviour 
                                    
{
    [SerializeField] Transform groundCheckTransform; 
    [SerializeField] LayerMask playerMask;
    [SerializeField] float killZoneY;
    [SerializeField] float jumpVelocity;
    [SerializeField] float speed;

    private bool jumpKeyWasPressed = false;
    private bool alreadyDoubleJumped = false;
    private float horizontalInput;
    private Rigidbody rigidBodyComponent; //to maximize code effeciency
    private int doubleJumpsRemaining = 0; 
    private Vector3 startPosition;
    private int deathCount;
    private bool isColliding = false;

    public bool dead = false;


    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();

        startPosition = rigidBodyComponent.position; //grabs+saves starting position

        deathCount = 0;

        //coins = FindObjectsOfType<Coin>();

    }

    //Update is called once per frame (Physics here is bad)
    void Update()
    {
        dead = false;
      
        //check if space key pressed down
        if (Input.GetKeyDown(KeyCode.Space) == true) //use KeyDown so it updates every frame
        { 
            jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal"); //gets general keybinding info on x-axis

        //player dies:
        if ( gameObject.transform.position.y <= killZoneY )
        {
           
            gameObject.transform.position = startPosition; //reset sprite to start

            doubleJumpsRemaining = 0; //removes double jumps
            
            deathCount++;

            rigidBodyComponent.velocity = new Vector3( 0, 0, 0);

            dead = true;

            print(string.Format("death count is {0}", deathCount));
        }
    }

    //Called once every 'physics update' (always put physics related stuff in here)
    private void FixedUpdate()
    {

        rigidBodyComponent.velocity = new Vector3(horizontalInput * speed, rigidBodyComponent.velocity.y, rigidBodyComponent.velocity.z);

        //if player not in contact with anything, return or double jump then return
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) //should only be 0 when in air
        {
            //double jump:
            if( doubleJumpsRemaining > 0 && jumpKeyWasPressed == true && alreadyDoubleJumped == false )
            {
                
                doubleJumpsRemaining--;

                rigidBodyComponent.velocity = Vector3.up * jumpVelocity; //midair jump

                Debug.Log("Double Jumped");

                jumpKeyWasPressed = false;

                alreadyDoubleJumped = true;
            }
            return;
        }



        //Debug.Log("I'm currently in contact with = " + Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length );

        //now, we're touching the ground:
        alreadyDoubleJumped = false;

        if (jumpKeyWasPressed == true)
        {
            Debug.Log("Jumped");
            rigidBodyComponent.AddForce(Vector3.up * jumpVelocity, ForceMode.VelocityChange);

            jumpKeyWasPressed = false;
        }
    }

    private void OnTriggerEnter(Collider other) //'other' collider is what's 'triggering' collision w/ player
    {
        //don't do anything if obj already colliding:
        if( isColliding == true )
        {

            return;
        }

        //collide w/ coin:
        if (other.gameObject.tag == "Coin")
        {
            doubleJumpsRemaining++;

            print("Double jump added");

            isColliding = true;

            StartCoroutine(ResetIsColliding());
        }
    }

    IEnumerator ResetIsColliding()
    {
        yield return new WaitForEndOfFrame();

        isColliding = false;
    }
}
