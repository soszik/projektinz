using Assets.Scripts.XMLToGameObjectParser;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMlParser;
using System.Linq;
public class MasterScript : MonoBehaviour
{

    public static List<GameObject> Puzzles = new List<GameObject>();
    public static List<Vector3> placements = new List<Vector3>();
    public static float puzzleSize;
    private Scene scene;
    private GameObject rootScene;
    public float size;
    public static GameObject CurrentPuzzle;
    public GameObject NextPuzlle;
    enum Mode
    {
        Tunnel,
        OpenSpace
    }
    //TODO w xml jako atr sceny
    private Mode environment = Mode.Tunnel;
    void loadXml()
    {
        scene = XmlLoader.LoadGameObjectsFromFile("sample2.xml");
    }
    void parseToGameObjects()
    {
        XMLToGameObjectParser.XMLToGameObjects(scene, ref Puzzles, ref placements);
    }
    void setRootScene()
    {
        rootScene = XMLToGameObjectParser.getRootScene();
    }

    void initializeGameObjects()
    {
        CurrentPuzzle = Puzzles.ElementAt(0);
        CurrentPuzzle.SetActive(true);


    }
    public static GameObject nextPuzzle()
    {
        //TODO: zwroc losowy puzel z listy
        System.Random rnd = new System.Random();
        int index = rnd.Next(Puzzles.Count);
        CurrentPuzzle.SetActive(false);
        CurrentPuzzle = Puzzles.ElementAt(index);
        CurrentPuzzle.SetActive(true);
        return CurrentPuzzle;
    }
    public GameObject tunnel;
    // Use this for initialization
    void Start()
    {
        loadXml();
        parseToGameObjects();
        setRootScene();
        initializeGameObjects();
        Debug.Log(scene.PuzzleSize);
        size = scene.PuzzleSize * 3;
        CurrentPuzzle = nextPuzzle();

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
    void Update()
    {

    }
}
