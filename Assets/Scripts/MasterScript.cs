using Assets.Scripts.XMLToGameObjectParser;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMlParser;
public class MasterScript : MonoBehaviour {

    private List<GameObject> Puzzles;
    private List<Scene> scenes;
    enum Mode
    {
        Tunnel,
        OpenSpace
    }
    private Mode environment = Mode.Tunnel;
    void loadXml()
    {
         scenes = XmlLoader.LoadGameObjectsFromFile("sample2.xml");
        //GameObject instance = Instantiate(Resources.Load("Czesc1Pref", typeof(GameObject))) as GameObject;
    }
    void parseToGameObjects()
    {
       Puzzles = XMLToGameObjectParser.XMLToGameObjects(scenes);
   
    }
    public GameObject nextPuzzle()
    {
        //TODO: zwroc losowy puzel z listy
        return new GameObject(); //placeholder
    }
    public GameObject tunnel;
	// Use this for initialization
	void Start () {
        loadXml();
        parseToGameObjects();
        foreach(var gameObj in Puzzles)
        {
            GameObject instance = Instantiate(gameObj) as GameObject;
        }
        //TODO: zmienic ponizsze na wstawienie pierwszego puzzla
        /* for (int i =0; i < 10; i++)
         {
             GameObject nowytunel = Instantiate(tunnel);
             nowytunel.transform.Translate(i, 0, 0);
             nowytunel.transform.rotation = Quaternion.Euler(i*33,0,0);
             if (i % 2 == 1)
             {
                 nowytunel.GetComponent<RingScript>().right = false;
                 nowytunel.GetComponent<RingScript>().speed = i*10;
             }
         */
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
