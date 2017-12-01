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
        Debug.Log("pew?");
       if (dir == direction.Up)
        {
            Debug.Log("Pew");
            bool exists = false;
            Vector3 newPos = transform.parent.transform.position;
            newPos.z += MasterScript.puzzleSize;
            Debug.Log(MasterScript.puzzleSize);
            Debug.Log(newPos.ToString() + " my");
            foreach (var place in MasterScript.placements)
            {
                if (place == newPos)
                    exists = true;
                Debug.Log(place.ToString());
            }
            if (!exists)
            {
                MasterScript.placements.Add(newPos);
                GameObject newPuzzle = GameObject.Instantiate(MasterScript.nextPuzzle(), newPos, transform.parent.transform.parent.transform.rotation);
                Debug.Log("pew!");
            }
        }
       else if (dir == direction.Down)
        {
            bool exists = false;
            Vector3 newPos = transform.parent.transform.position;
            newPos.z -= MasterScript.puzzleSize;
            foreach (var place in MasterScript.placements)
            {
                if (place == newPos)
                    exists = true;
            }
            if (!exists)
            {
                MasterScript.placements.Add(newPos);
                GameObject newPuzzle = GameObject.Instantiate(MasterScript.nextPuzzle(), newPos, transform.parent.transform.parent.transform.rotation);
                newPuzzle.transform.Rotate(0, 180, 0);
            }
        }
       else if (dir == direction.Right)
        {
            bool exists = false;
            Vector3 newPos = transform.parent.transform.position;
            newPos.x += MasterScript.puzzleSize;
            foreach (var place in MasterScript.placements)
            {
                if (place == newPos)
                    exists = true;
            }
            if (!exists)
            {
                MasterScript.placements.Add(newPos);
                GameObject newPuzzle = GameObject.Instantiate(MasterScript.nextPuzzle(), newPos, transform.parent.transform.parent.transform.rotation);
                newPuzzle.transform.Rotate(0, 90, 0);
            }
        }
       else if (dir == direction.Left)
        {
            bool exists = false;
            Vector3 newPos = transform.parent.transform.position;
            newPos.x -= MasterScript.puzzleSize;
            foreach (var place in MasterScript.placements)
            {
                if (place == newPos)
                    exists = true;
            }
            if (!exists)
            {
                MasterScript.placements.Add(newPos);
                GameObject newPuzzle = GameObject.Instantiate(MasterScript.nextPuzzle(), newPos, transform.parent.transform.parent.transform.rotation);
                newPuzzle.transform.Rotate(0, -90, 0);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        createNextPuzzle();
        if (CreateNext)
                createNextPuzzle();
    }
}
