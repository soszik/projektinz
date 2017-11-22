using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingScript : MonoBehaviour {
    public bool right = true;
    public float speed = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (right)
            transform.Rotate( speed * Time.deltaTime, 0, 0);
        else
            transform.Rotate(-1*speed * Time.deltaTime, 0, 0);
    }
}
