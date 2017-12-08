using Assets.Scripts.XMLToGameObjectParser;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XMlParser;
using System.Linq;
public class MasterScript : MonoBehaviour
{

    public static List<GameObject> Puzzles = new List<GameObject>();
    public static List<Vector3> placements = new List<Vector3>();
    public static List<AudioClip> AudioItems = new List<AudioClip>();
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
        XMLToGameObjectParser.XMLToGameObjects(scene, ref Puzzles, ref placements, ref AudioItems);
    }
    void setRootScene()
    {
        rootScene = XMLToGameObjectParser.getRootScene();
    }

    void initializeGameObjects()
    {
        CurrentPuzzle = Puzzles.ElementAt(0);
        GameObject firstPuzzle = Instantiate(nextPuzzle(), new Vector3(0,0,0), CurrentPuzzle.transform.rotation);
        placements.Add(firstPuzzle.transform.position);
        firstPuzzle.SetActive(true);
        GameObject back = Instantiate(nextPuzzle(), firstPuzzle.transform.position, firstPuzzle.transform.rotation);
        back.transform.Rotate(0, 180, 0);
        back.transform.Translate(0, 0, puzzleSize);
        placements.Add(back.transform.position);
        back.SetActive(true);

    }
    public static GameObject nextPuzzle()
    {
        //TODO: zwroc losowy puzel z listy
        System.Random rnd = new System.Random();
        int index = rnd.Next(Puzzles.Count);
        CurrentPuzzle = Puzzles.ElementAt(index);
        return CurrentPuzzle;
    }
    public GameObject tunnel;
    // Use this for initialization
    void Start()
    {
        loadXml();
        parseToGameObjects();
        setRootScene();
        puzzleSize = scene.PuzzleSize;
        initializeGameObjects();
        CurrentPuzzle = nextPuzzle();
        Dropdown dropdown = GameObject.FindGameObjectWithTag("drop").GetComponent<Dropdown>();
        dropdown.ClearOptions();
        List<string> options = new List<string>();
        if (scene.GroupCount > 20)
            scene.GroupCount = 20;
        for (int i = 0; i <= scene.GroupCount; i++)
            options.Add(i.ToString());
        dropdown.AddOptions(options);
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
