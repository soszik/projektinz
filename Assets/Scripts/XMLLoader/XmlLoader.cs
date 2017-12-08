﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;


namespace XMlParser
{
    public static class XmlLoader
    {
        //TODO List of gameobjects
        public static Scene LoadGameObjectsFromFile(String path)
        {
            //List<Scene> gameObjects = new List<Scene>();
            XDocument doc = XDocument.Load(path);
            Scene gameObjects = parseFromFile(doc);

            return gameObjects;
        }

        private static Scene parseFromFile(XDocument doc)
        {
            IEnumerable<Scene> result = from s in doc.Descendants("scene")
                                        select new Scene()
                                        {
                                            Type = (string)s.Attribute("type"),
                                            Name = (string)s.Attribute("name"),
                                            X = (int)s.Attribute("x"),
                                            Y = (int)s.Attribute("y"),
                                            Z = (int)s.Attribute("z"),
                                            PuzzleSize = float.Parse((string)s.Attribute("size")),
                                            GroupCount = (int)s.Attribute("groupCount"),
                                            AudioItems = s.Descendants("audio").Select(audio => new Audio()
                                            {
                                                  Id = (string)audio.Attribute("id"),
                                                  Path = (String)audio.Attribute("path"),
                                            }).ToList(),
                                            Puzzles = s.Descendants("puzzle").Select(p => new Puzzle()
                                            {
                                               
                                                Name = (string)p.Attribute("name"),
                                                Files = p.Descendants("file").Select(f => new File()
                                                {
                                                    Path = (string)f.Attribute("path"),
                                                    Type = (string)f.Attribute("type"),
                                                }).ToList(),
                                                Parts = p.Descendants("part").Select(part => new Part()
                                                {
                                                    Id = (string)part.Attribute("id"),
                                                    Type = (string)part.Attribute("type"),
                                                    X = (int)part.Attribute("x"),
                                                    Y = (int)part.Attribute("y"),
                                                    Z = (int)part.Attribute("z"),
                                                    Scale = (int)part.Attribute("scale"),
                                                }).ToList(),
                                                Rings = p.Descendants("ring").Select(ring => new Ring()
                                                {
                                                    Scale = (int)ring.Attribute("scale"),
                                                    Id = (string)ring.Attribute("id"),
                                                    Group = (int)ring.Attribute("group"),
                                                    Placement = xmlStringToFloatArray((string)ring.Attribute("placement"), 3),
                                                    CreateNext = bool.Parse((string)ring.Attribute("createNext")),
                                                    Right = (string)ring.Attribute("right"),
                                                    Speed = float.Parse((string)ring.Attribute("speed")),
                                                    Dir = (Ring.direction)Enum.Parse(typeof(Ring.direction), (string)ring.Attribute("dir")),
                                                }).ToList(),
                                                SmallObjects = p.Descendants("smallObject").Select(smallObject => new SmallObject()
                                                {
                                                    Scale = (int)smallObject.Attribute("scale"),
                                                    Id = (string)smallObject.Attribute("id"),
                                                    Type = (string)smallObject.Attribute("type"),
                                                    rotate = (string)smallObject.Attribute("rotate"),
                                                    pulsation = (string)smallObject.Attribute("pulsation"),
                                                    vibrating = (string)smallObject.Attribute("vibrating"),
                                                    pulsationFrequency = xmlStringToFloatArray((string)smallObject.Attribute("pulsationFrequency"), 3),
                                                    pulsationAmplitudeMax = xmlStringToFloatArray((string)smallObject.Attribute("pulsationAmplitudeMax"), 3),
                                                    pulsationAmplitudeMin = xmlStringToFloatArray((string)smallObject.Attribute("pulsationAmplitudeMin"), 3),
                                                    rotationSpeed = xmlStringToFloatArray((string)smallObject.Attribute("rotationSpeed"), 3),
                                                    rotationMax = xmlStringToFloatArray((string)smallObject.Attribute("rotationMax"), 3),
                                                    rotationMin = xmlStringToFloatArray((string)smallObject.Attribute("rotationMin"), 3),
                                                    bezierPoints = smallObject.Descendants("bezierPoint").Select(bezpierPoint => 
                                                    new float[] { float.Parse((string)bezpierPoint.Attribute("x")),
                                                                    float.Parse((string)bezpierPoint.Attribute("y")),
                                                                    float.Parse((string)bezpierPoint.Attribute("z"))}
                                                     
                                                    ).ToList(),
                                                }).ToList(),
                                                
                                            }).ToList()
                                            
                                        };
            return result.ToList<Scene>().ElementAt(0);
        }
        //later parsed to Vertex3 in XMLToGameObjectParser
        private static float[] xmlStringToFloatArray(string toParse, int length)
        {
            float[] array = new float[length];

            array = Array.ConvertAll(toParse.Split(','), float.Parse);

            return array;
        }
    }
}
