using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingScript : MonoBehaviour {
    public enum direction
    {
        Up,
        Down,
        Left,
        Right
    }
    public bool right = true;
    public float speed = 10;
    public bool CreateNext = false;
    public direction dir;
    public Vector3 placement;
    public int group = 0;

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

    void createNextPuzzle()
    {

    }
    void OnCollisionEnter(Collision col)
    {
        if (CreateNext)
            if (col.gameObject.name == "Player")
            {
                createNextPuzzle();
            }
    }
}
