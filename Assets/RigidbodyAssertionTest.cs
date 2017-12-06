using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyAssertionTest : Test {


    private Rigidbody rb;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected override void Simulate()
    {
        
    }

    protected override void Check()
    {
        if (rb != null)
        {
            IntegrationTest.Pass(gameObject);
        }
    }
}
