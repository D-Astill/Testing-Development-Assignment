using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Test : MonoBehaviour {

    public float checkDelay = 0f;
    private float checkTimer = 0f;

    protected virtual void Simulate() { }
    protected virtual void Debug() { }
    protected virtual void Check() { }

	// Update is called once per frame
	void Update ()
    {
        Simulate();
        checkTimer += Time.deltaTime;
        if (checkTimer >= checkDelay)
        {
            Check();
        }
        //Preform debugging 
        Debug();	
	}
}
