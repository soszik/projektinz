using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XMlParser;


namespace Assets.Scripts.XMLToGameObjectParser
{
    public static class XMLToGameObjectParser
    {
        private static Scene scn;

        public static void XMLToGameObjects(Scene scene, ref List<GameObject> gameObjectsFromScene,
           ref List<Vector3> puzzlesPlacements, ref List<AudioClip> AudioItems)
        {
            scn = scene;

            parseScene(scene, ref gameObjectsFromScene, ref puzzlesPlacements, ref AudioItems);
        }

        public static GameObject getRootScene()
        {
            var scene = new GameObject(scn.Name);
            scene.transform.position = new Vector3(scn.X, scn.Y, scn.Z);

            return scene;
        }

        private static void parseScene(Scene scene, ref List<GameObject> gameObjectsFromScene,
           ref List<Vector3> puzzlesPlacements, ref List<AudioClip> AudioItems)
        {
            foreach (var audio in scene.AudioItems)
            {
                AudioClip audioItem = UnityEngine.Object.Instantiate(
                    Resources.Load(audio.Path) as AudioClip);
                audioItem.name = audio.Id.ToString();
                AudioItems.Add(audioItem);
            }

            foreach (var puzzle in scene.Puzzles)
            {
                parsePuzzle(puzzle, ref gameObjectsFromScene, ref puzzlesPlacements);
            }
        }



        private static void parsePuzzle(Puzzle puzzle, ref List<GameObject> gameObjectsFromPuzzle,
            ref List<Vector3> puzzlesPlacements)
        {

            foreach (var part in puzzle.Parts)
            {

                var sourceFile = puzzle.Files.Find(f => f.Type == part.Type);
                var newGameObj = UnityEngine.Object.Instantiate(Resources.Load(sourceFile.Path) as GameObject);
                newGameObj.name = part.Id;
                Vector3 puzzlePlacement = new Vector3(part.X, -5, part.Z);
                //newGameObj.SetActive(false);
                newGameObj.transform.position = puzzlePlacement;
                puzzlesPlacements.Add(puzzlePlacement);
                gameObjectsFromPuzzle.Add(newGameObj);


                foreach (var smallObject in puzzle.SmallObjects)
                {
                    var sourceFileForSmall = puzzle.Files.Find(f => f.Type == smallObject.Type);
                    var newGameObjSmall = UnityEngine.Object.Instantiate(Resources.Load(sourceFileForSmall.Path) as GameObject
                        , newGameObj.transform);
                    newGameObjSmall.name = smallObject.Id;
                    newGameObjSmall.transform.position = new Vector3(smallObject.bezierPoints.ElementAt(0)[0],
                        (float)smallObject.bezierPoints.ElementAt(0)[1], (float)smallObject.bezierPoints.ElementAt(0)[2]);
                    var comp = newGameObjSmall.AddComponent<FlyingObjectScript>() as FlyingObjectScript;
                    setFlyingScriptProperties(ref comp, smallObject);
                }

                foreach (var ring in puzzle.Rings)
                {
                    var sourceFileForRing= puzzle.Files.Find(f => f.Type == "ring");
                    var newGameRing= UnityEngine.Object.Instantiate(Resources.Load(sourceFileForRing.Path) as GameObject
                        , newGameObj.transform);
                    newGameRing.name = ring.Id;
                    newGameRing.transform.position = new Vector3(ring.Placement[0], ring.Placement[1],
                        ring.Placement[2]);
                    var comp = newGameRing.AddComponent<RingScript>() as RingScript;
                    setRingScriptProperties(ref comp, ring);
                }

            }
        }

        private static void setRingScriptProperties(ref RingScript comp, Ring ring)
        {
            comp.CreateNext = ring.CreateNext;
            comp.dir = (RingScript.direction)ring.Dir;
            comp.group = ring.Group;
            comp.right = bool.Parse(ring.Right);
            comp.speed = ring.Speed;
        }

        private static void setFlyingScriptProperties(ref FlyingObjectScript comp, SmallObject smallObject)
        {
            comp.bezierPoints = getBezierPoints(smallObject.bezierPoints);
            comp.bezierSpeed = smallObject.bezierSpeed;
            comp.pulsation = Boolean.Parse(smallObject.pulsation);
            if (comp.pulsation)
            {
                comp.pulsationAmplitudeMax = floatArrayToVector(smallObject.pulsationAmplitudeMax);
                comp.pulsationAmplitudeMin = floatArrayToVector(smallObject.pulsationAmplitudeMin);
                comp.pulsationFrequency = floatArrayToVector(smallObject.pulsationFrequency);
            }
            comp.rotate = Boolean.Parse(smallObject.rotate);
            if (comp.rotate)
            {
                comp.rotationDir = floatArrayToVector(smallObject.rotationDir);
                comp.rotationMax = floatArrayToVector(smallObject.rotationMax);
                comp.rotationMin = floatArrayToVector(smallObject.rotationMin);
                comp.rotationSpeed = floatArrayToVector(smallObject.rotationSpeed);
            }

            comp.vibrating = Boolean.Parse(smallObject.vibrating);
            if (comp.vibrating)
            {
                comp.vibrationAmplitude = floatArrayToVector(smallObject.vibrationAmplitude);
                comp.vibrationFrequency = floatArrayToVector(smallObject.vibrationFrequency);
            }
        }

        private static Vector3 floatArrayToVector(float[] toPrase)
        {
            Vector3 vector = new Vector3(toPrase[0], toPrase[1], toPrase[2]);
            
            return vector;
        }

        private static List<Vector3> getBezierPoints(List<float[]> bezierPointsToParse)
        {
            List<Vector3> bezierPoints = new List<Vector3>();

            foreach (var points in bezierPointsToParse)
            {
                bezierPoints.Add(new Vector3(points[0], points[1], points[2]));
            }

            return bezierPoints;
        }




    }
}
