﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XMlParser;

namespace Assets.Scripts.XMLToGameObjectParser
{
    public static class XMLToGameObjectParser
    {
        private static List<Scene> scns;

        public static List<GameObject> XMLToGameObjects(List<Scene> scenes)
        {
            List<GameObject> gameObjects = new List<GameObject>();
            scns = scenes;

            foreach(var scene in scenes)
            {
                gameObjects.AddRange(parseScene(scene));
            }
            return gameObjects;
        }

        public static GameObject getRootScene()
        {
            var scene = new GameObject(scns.ElementAt(0).Name);
            scene.transform.position = new Vector3(scns.ElementAt(0).X, scns.ElementAt(0).Y, 
                scns.ElementAt(0).Z);

            return scene;
        }

        private static List<GameObject> parseScene(Scene scene)
        {
            List<GameObject> gameObjectsFromScene = new List<GameObject>();

            foreach (var puzzle in scene.Puzzles)
            {
                gameObjectsFromScene.AddRange(parsePuzzle(puzzle));
            }

            return gameObjectsFromScene;
        }

        private static List<GameObject> parsePuzzle(Puzzle puzzle)
        {
            List<GameObject> gameObjectsFromPuzzle = new List<GameObject>();

            foreach (var part in puzzle.Parts)
            {
                var sourceFile = puzzle.Files.Find(f => f.Type == part.Type);
                var newGameObj = Resources.Load(sourceFile.Path) as GameObject;
                newGameObj.transform.position = new Vector3(part.X, part.Y, part.Z);
                gameObjectsFromPuzzle.Add(newGameObj);
            }

            return gameObjectsFromPuzzle;
        }


    }
}
