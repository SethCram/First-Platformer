using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public MyPlayer player;
    public DownElevator downElevator;
    public UpElevator upElevator;
    public GoldBall yellowBall;

    private Coin[] coins; //= FindObjectOfType(typeof(Coin));
    private FallingLavaBlocks[] lavaBlocks;
    private Rigidbody rigidbodyComponent;

    // Start is called before the first frame update
    void Start()
    {
        coins = FindObjectsOfType<Coin>();

        lavaBlocks = FindObjectsOfType<FallingLavaBlocks>();
  
    }



    // Update is called once per frame
    void Update()
    {
        //resets if player dies:
        if( player.dead == true )
        {
            downElevator.Reset();

            upElevator.Reset();

            yellowBall.Reset();

            print("Elevators and ball reset");

            foreach (var FallingBlock in lavaBlocks)
            {
                rigidbodyComponent = FallingBlock.GetComponent<Rigidbody>();

                rigidbodyComponent.useGravity = false;

                rigidbodyComponent.isKinematic = true;

                FallingBlock.transform.position = FallingBlock.startPosition;

                print("Lava block reset");
            }

            foreach (var coin in coins)
            {
                coin.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

                coin.transform.position = coin.startPosition;

                print("Coin reappeared");
            }

        }
    }

    private void FixedUpdate()
    {

    }
}
