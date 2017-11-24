using System;
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
                                                }).ToList(),
                                            }).ToList()
                                            
                                        };
            return result.ToList<Scene>();
        }
    }
}
