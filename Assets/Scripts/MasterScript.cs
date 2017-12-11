using Assets.Scripts.XMLToGameObjectParser;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XMlParser;
using System.Linq;
using LZWPlib;
public class MasterScript : MonoBehaviour
{
    public GameObject UI;
    public static MasterScript master;
    public List<GameObject> Puzzles = new List<GameObject>();
    public List<GameObject> SmallObjects = new List<GameObject>();
    public List<Vector3> placements = new List<Vector3>();
    public List<AudioClip> AudioItems = new List<AudioClip>();
    public float puzzleSize;
    private Scene scene;
    public GameObject horizon;
    public GameObject player;
    public Camera left, right;
    public enum Mode
    {
        Tunnel,
        OpenSpace
    }
    //TODO w xml jako atr sceny
    public Mode environment = Mode.Tunnel;
    
    void loadXml()
    {
        scene = XmlLoader.LoadGameObjectsFromFile("sample2.xml");
    }
    void parseToGameObjects()
    {
        XMLToGameObjectParser.XMLToGameObjects(scene, ref Puzzles, ref placements, ref AudioItems,
                                                ref SmallObjects);
    }

    void initializeGameObjects()
    {
        if (environment == Mode.Tunnel)
        {
            GameObject firstPuzzle = (GameObject)Network.Instantiate(nextPuzzle(), new Vector3(0, 0, 0), new Quaternion(), 1);
            placements.Add(firstPuzzle.transform.position);
            GameObject back = (GameObject)Network.Instantiate(nextPuzzle(), firstPuzzle.transform.position, firstPuzzle.transform.rotation, 1);
            back.transform.Rotate(0, 180, 0);
            back.transform.Translate(0, 0, puzzleSize);
            placements.Add(back.transform.position);
        }
        else
            Network.Instantiate(horizon, new Vector3(0, 0, 0), new Quaternion(), 1);

    }
    public GameObject nextPuzzle()
    {
        //TODO: zwroc losowy puzel z listy
        System.Random rnd = new System.Random();
        int index = rnd.Next(Puzzles.Count);
        return Puzzles.ElementAt(index);
    }
    public GameObject tunnel;
    // Use this for initialization
    void Start()
    {

        if (LzwpManager.Instance.isServer)
        {
            master = this.GetComponent<MasterScript>();
            loadXml();
            parseToGameObjects();
            puzzleSize = scene.PuzzleSize;
            if (scene.Type == "Tunnel")
                environment = Mode.Tunnel;
            else
                environment = Mode.OpenSpace;
            initializeGameObjects();
            
            Dropdown dropdown = GameObject.FindGameObjectWithTag("drop").GetComponent<Dropdown>();
            dropdown.ClearOptions();
            List<string> options = new List<string>();
            if (scene.GroupCount > 20)
                scene.GroupCount = 20;
            for (int i = 0; i <= scene.GroupCount; i++)
                options.Add(i.ToString());

            dropdown.AddOptions(options);
            if (environment == Mode.OpenSpace)
            {
                left.clearFlags = CameraClearFlags.Skybox;
                right.clearFlags = CameraClearFlags.Skybox;
                left.farClipPlane = 50000;
                right.farClipPlane = 50000;
                FPSConrtoller controller = player.GetComponent<FPSConrtoller>();
                controller.m_GravityMultiplier = 0;
                player.transform.Translate(0, 51, 0);
                foreach(GameObject a in GameObject.FindObjectsOfType(typeof(GameObject)))
                {
                    FlyingObjectScript script = a.GetComponent<FlyingObjectScript>();
                    if (script != null)
                    {
                        a.transform.Translate(new Vector3(0, 50, 0), Space.World);
                        controller.flyingObjects.Add(a);
                        for (int i = 0; i < script.bezierPoints.Count; i++)
                        {
                            script.bezierPoints[i] = new Vector3(script.bezierPoints[i].x, script.bezierPoints[i].y+50, script.bezierPoints[i].z);
                        }
                    }
                }
            }
        }
        else
            UI.SetActive(false);      

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
                UI.SetActive(!UI.activeSelf);
    }
}
