using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterScript : MonoBehaviour {

    public GameObject tunnel;
	// Use this for initialization
	void Start () {
		for (int i =0; i < 10; i++)
        {
            GameObject nowytunel = Instantiate(tunnel);
            nowytunel.transform.Translate(i, 0, 0);
            nowytunel.transform.rotation = Quaternion.Euler(i*33,0,0);
            if (i % 2 == 1)
            {
                nowytunel.GetComponent<RingScript>().right = false;
                nowytunel.GetComponent<RingScript>().speed = i*10;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
