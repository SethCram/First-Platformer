//declaring namespace:
using System;
using System.Collections;
using System.Collections.Generic; //for UI stuff
using UnityEngine; //telling unity what namespace to use, needed for 'MonoBehaviour'

public class TutorialPlayer : MonoBehaviour //MonoBheavior = coll of alota stuff we can do in our script, our class inherits its variables and methods (parent class)
                                    // could type 'UnityEngine.MonoBehaviour', but using the unity namespace = easier
{
    //public Transform groundCheckTransform; //public fields are shown in unity under inspector > script 
    [SerializeField] Transform groundCheckTransform; //does same thing as public, but doesn't expose to other classes, this is preferred way (exposes stuff in inspector)
    [SerializeField] LayerMask playerMask;
    [SerializeField] float killZoneY;

    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidBodyComponent; //to maximize code effeciency
    private int superJumpsRemaining = 0; //automatically zero at start in C#, but specify anyways
    Vector3 startPosition;
    private int deathCount;

    //old code: private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();

        startPosition = rigidBodyComponent.position; //grabs+saves starting position

        deathCount = 0;
    
    }

    //Update is called once per frame
    // always keep 'key pressing' in update method, but physics here is bad
    void Update()
    {
        //if (sprite = null)
        //  return;

        //check if space key pressed down
        if (Input.GetKeyDown(KeyCode.Space) == true) //KeyDown so it updates every frame, just GetKey would only do once
        { //need to be in update bc checking over and over again
          //old code: Debug.Log("Space Key pressed down"); //log sends this string to consolve

            //old code: GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.VelocityChange); //get comp to get any comp this script attached to    
            // get rigidbody, which is a method
            // in 3d so have to used 3D vector
            jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal"); //getaxis returns a float
                                                       // 'Horizontal' bc that's the name of the horizontal axis
                                                       // increases or decreases w/ go left or right w/ a,d,or left,right arrows bc those r defaults in 'input manager' under horizontal axis

        if ( gameObject.transform.position.y <= killZoneY )
        {
            gameObject.transform.position = startPosition; //reset sprite to start
            superJumpsRemaining = 0; //removes super jumps
            deathCount++;
        }
    }
    //Called once every 'physics update' (unity physics engine updates 100 times every sec) (can change that number in preferences)
    // these physics don't depend on framerate of computer, work correctly always
    // always put physics related stuff in here
    private void FixedUpdate()
    {
        rigidBodyComponent.velocity = new Vector3(horizontalInput * 2, rigidBodyComponent.velocity.y, 0); //to preserve above applied y-speed, more horizontal input = faster character

        //old code: if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length == 1) //returns array of colliders colliding with
        // 'f' signifies number as a float
        // colliding with zero colliders(in air) (doesn't account for collision with player)
        // so, expect to collide with 1 collider (parent)

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) //could use 'checkSphere' instead since returns bool, common to access which colliders colliding with tho
        {
            return;
        }


        /*old code
        if( isGrounded == false )
        {
            return; //returns before allowing more jumps to change velocity and side-to-side moves
        }
        */
        if(jumpKeyWasPressed == true) //don't need to include true, compiler implies it
        {

            //old code: GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.VelocityChange);

            float jumpPower = 5f; //only need 'f' w/ decimal #'s, but doesnt hurt
            if( superJumpsRemaining > 0 ) //if have any super jumps
            {
                jumpPower *= 2; //doubles jump power
                superJumpsRemaining--; //removes a super jump
            }

                rigidBodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);

                jumpKeyWasPressed = false;
        }   

        //old code: GetComponent<Rigidbody>().velocity = new Vector3(horizontalInput, 0, 0); // constantly resets above applied force to zero
        
        //old code: GetComponent<Rigidbody>().velocity = new Vector3(horizontalInput, GetComponent<Rigidbody>().velocity.y, 0); //to preserve above applied y-speed

       

    }

    private void OnTriggerEnter(Collider other) //'other' collider is what's 'triggering' collision w/ player
    {
        if( other.gameObject.layer == 9 ) //9 is the coin layer
        {
            Destroy( other.gameObject );
            superJumpsRemaining++; //incr by 1
        }
    }

    /* old code
    private void OnCollisionEnter(Collision collision) // collision obj contains info regarding collision, we don't need it rn
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision) //when no longer collision
    {
        isGrounded = false;   
    }
    */
}
