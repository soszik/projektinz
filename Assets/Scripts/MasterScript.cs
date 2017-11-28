using Assets.Scripts.XMLToGameObjectParser;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMlParser;
using System.Linq;
public class MasterScript : MonoBehaviour
{

    public static List<GameObject> Puzzles = new List<GameObject>();
    public static List<Vector3> PuzzlesPlacements = new List<Vector3>();
    private Scene scene;
    private GameObject rootScene;
    public float size;
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
        XMLToGameObjectParser.XMLToGameObjects(scene, ref Puzzles, ref PuzzlesPlacements);
    }
    void setRootScene()
    {
        rootScene = XMLToGameObjectParser.getRootScene();
    }

    void initializeGameObjects()
    {
        /*foreach (var gameObj in Puzzles)
        {
            GameObject instance = Instantiate(gameObj, rootScene.transform) as GameObject;
        }


        foreach (var scene in scenes)
        {
            foreach (var puzzle in scene.Puzzles)
            {
                foreach (var smallObject in puzzle.SmallObjects)
                {
                    var sourceFile = puzzle.Files.Find(f => f.Type == smallObject.Type);
                    var newGameObj = Instantiate(Resources.Load(sourceFile.Path)) as GameObject;
                    newGameObj.name = smallObject.Id;
                    newGameObj.transform.position = new Vector3((float)smallObject.bezierPoints.ElementAt(0)[0],
                        (float)smallObject.bezierPoints.ElementAt(0)[1], (float)smallObject.bezierPoints.ElementAt(0)[2]);
                }
            }
        }*/


    }
    public static GameObject nextPuzzle()
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
        loadXml();
        parseToGameObjects();
        setRootScene();
        initializeGameObjects();


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
