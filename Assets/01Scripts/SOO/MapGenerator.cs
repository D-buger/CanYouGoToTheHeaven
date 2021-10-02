using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class MapGenerator : MonoBehaviour
{
    System.Xml.XmlDocument Document = new System.Xml.XmlDocument();
    MapComponent map;

    public static int MapWidthTile { get; private set; }
    public static int MapHeightTile { get; private set; }

    public void Awake()
    {
        XmlLoadComplete("1_1");
    }

    private void XmlLoadComplete(string _fileName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(SOO.Util.StringBuilder("Maps/", _fileName));
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(textAsset.text);
        map = new MapComponent();

        XmlNode node = xml.SelectSingleNode("map");
        map.tileWidth = int.Parse(node.Attributes.GetNamedItem("tilewidth").Value);
        map.tileHeight = int.Parse(node.Attributes.GetNamedItem("tileheight").Value);
        map.widthTile = int.Parse(node.Attributes.GetNamedItem("width").Value);
        map.heightTile = int.Parse(node.Attributes.GetNamedItem("height").Value);

        map.tiles = new int[map.heightTile, map.widthTile];
        int count = 0;
        foreach (string str in node.InnerText.Split(','))
        {
            map.tiles[count++ / map.widthTile, count % map.widthTile]
                = int.Parse(str);
        }

        XmlNodeList nodeList = xml.SelectNodes("map/objectgroup");

        XmlNode rectNode = nodeList.Item(0);
        XmlNodeList ObjectNodeList = rectNode.SelectNodes("Object");

        XmlNode spawnerNode = nodeList.Item(1);


        foreach (XmlNode nodeInList in nodeList)
        {
            XmlAttribute attri = nodeInList.Attributes[name];
            XmlNodeList objectNodeList = nodeInList.SelectNodes("object");
            map.GetType().GetProperty(attri.Value).SetValue( ,new Rect[objectNodeList.Count]);
            map.Collision = new Rect[objectNodeList.Count];
            foreach (XmlNode objectNode in objectNodeList)
            {
                XmlAttribute objectAttribute = objectNode.Attributes["x"];
                Debug.Log(objectAttribute.Value);
            }

            //map.GetType().GetProperty(attri.Value).SetValue();
        }
    }
}

public struct MapComponent
{
    public int tileHeight;
    public int tileWidth;
    public int heightTile;
    public int widthTile;
    public int[,] tiles;
    
    public Rect[] Collision { get; set; }
    public Vector2[] MonsterSpawner { get; set; }
}