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
        public static List <Scene> LoadGameObjectsFromFile(String path)
        {
            //List<Scene> gameObjects = new List<Scene>();
            XDocument doc = XDocument.Load(path);
            List<Scene> gameObjects = parseFromFile(doc);

            return gameObjects;
        }

        private static List<Scene> parseFromFile(XDocument doc)
        {
            IEnumerable<Scene> result = from s in doc.Descendants("scene")
                                        select new Scene()
                                        {
                                            Type = (string)s.Attribute("type"),
                                            Name = (string)s.Attribute("name"),
                                            X = (int)s.Attribute("x"),
                                            Y = (int)s.Attribute("y"),
                                            Z = (int)s.Attribute("z"),
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
                                                }).ToList(),
                                                SmallObjects = p.Descendants("smallObject").Select(smallObject => new SmallObject()
                                                {
                                                    Id = (string)smallObject.Attribute("id"),
                                                    Type = (string)smallObject.Attribute("type"),
                                                    rotate = (string)smallObject.Attribute("rotate"),
                                                    pulsation = (string)smallObject.Attribute("pulsation"),
                                                    vibrating = (string)smallObject.Attribute("vibrating"),
                                                    pulsationFrequency = xmlStringToDoubleArray((string)smallObject.Attribute("pulsationFrequency"), 3),
                                                    pulsationAmplitudeMax = xmlStringToDoubleArray((string)smallObject.Attribute("pulsationFrequency"), 3),
                                                    pulsationAmplitudeMin = xmlStringToDoubleArray((string)smallObject.Attribute("pulsationFrequency"), 3),
                                                    rotationSpeed = xmlStringToDoubleArray((string)smallObject.Attribute("pulsationFrequency"), 3),
                                                    rotationMax = xmlStringToDoubleArray((string)smallObject.Attribute("pulsationFrequency"), 3),
                                                    rotationMin = xmlStringToDoubleArray((string)smallObject.Attribute("pulsationFrequency"), 3),
                                                    bezierPoints = smallObject.Descendants("bezierPoint").Select(bezpierPoint => 
                                                    new double[] { Double.Parse((string)bezpierPoint.Attribute("x")),
                                                                    Double.Parse((string)bezpierPoint.Attribute("y")),
                                                                    Double.Parse((string)bezpierPoint.Attribute("z"))}
                                                     
                                                    ).ToList(),
                                                }).ToList(),
                                            }).ToList()
                                            
                                        };
            return result.ToList<Scene>();
        }
        //later parsed to Vertex3 in XMLToGameObjectParser
        private static double[] xmlStringToDoubleArray(string toParse, int length)
        {
            double[] array = new double[length];

            array = Array.ConvertAll(toParse.Split(','), Double.Parse);

            return array;
        }
    }
}
