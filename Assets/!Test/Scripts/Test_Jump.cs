using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Jump : Test {

    [Header("Test parameters")]
    public float minHeight = 1f;

    private PlayerController player;
    private float originalY;
    private float JumpApex;


    // Use this for initialization
    void Start()
    {
        player = GetComponent<PlayerController>();
        originalY = transform.position.y;
    }

    protected override void Simulate()
    {
        player.Jump();
    }

    protected override void Check()
    {
        //grabs the players current y pos
        float playerY = transform.position.y;
        float height = playerY - originalY;
        //check if the height is over the apex
        if (height > JumpApex)
            JumpApex = height;
        if (JumpApex > minHeight)
        {
            IntegrationTest.Pass(gameObject);
        }
    }

	
}
