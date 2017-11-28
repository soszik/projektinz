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
           ref List<Vector3> puzzlesPlacements)
        {
            scn = scene;

            parseScene(scene, ref gameObjectsFromScene, ref puzzlesPlacements);
        }

        public static GameObject getRootScene()
        {
            var scene = new GameObject(scn.Name);
            scene.transform.position = new Vector3(scn.X, scn.Y, scn.Z);

            return scene;
        }

        private static void parseScene(Scene scene, ref List<GameObject> gameObjectsFromScene,
           ref List<Vector3> puzzlesPlacements)
        {

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
                Vector3 puzzlePlacement = new Vector3(part.X, part.Y, part.Z);
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

            }
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
