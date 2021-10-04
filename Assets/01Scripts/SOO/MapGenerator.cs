using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class MapGenerator
{
    public static int MapTileWidth { get; private set; } = -1;
    public static int MapTileHeight { get; private set; } = -1;
    public static int MapWidthTile { get; private set; } = -1;
    public static int MapHeightTile { get; private set; } = -1;

    public MapComponent[] Map { get; private set; }

    public static int RoomsInStage { get; private set; }
    public static int Stage { get; private set; }

    public MapGenerator(int stageNum, int roomNum)
    {
        RoomsInStage = roomNum;
        Stage = stageNum;
        Map = new MapComponent[stageNum * roomNum];
        int stage;
        int room;
        for (int i = 0; i < Map.Length; i++)
        {
            stage = i / roomNum + 1;
            room = i % roomNum;

            if (room++ < roomNum - 1)
               Map[i] = XmlLoadComplete(SOO.Util.StringBuilder(stage.ToString(), "_", room.ToString()));
            else
               Map[i] = XmlLoadComplete(SOO.Util.StringBuilder(stage.ToString(), "_End"));
        }
    }

    private MapComponent XmlLoadComplete(string _fileName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(SOO.Util.StringBuilder("Maps/", _fileName));
        if (textAsset == null)
            return default;
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(textAsset.text);
        MapComponent map = new MapComponent();

        XmlNode node = xml.SelectSingleNode("map");
        if (MapHeightTile == -1)
        {
            MapTileWidth = int.Parse(node.Attributes.GetNamedItem("tilewidth").Value);
            MapTileHeight = int.Parse(node.Attributes.GetNamedItem("tileheight").Value);
            MapWidthTile = int.Parse(node.Attributes.GetNamedItem("width").Value);
            MapHeightTile = int.Parse(node.Attributes.GetNamedItem("height").Value);
        }

        map.tileWidth = MapTileWidth;
        map.tileHeight = MapTileHeight;
        map.widthTile = MapWidthTile;
        map.heightTile = MapHeightTile;

        map.tiles = new int[map.heightTile, map.widthTile];
        int count = 0;
        foreach (string str in node.InnerText.Split(','))
        {
            map.tiles[count / map.widthTile, count++ % map.widthTile]
                = int.Parse(str);
        }

        XmlNodeList nodeList = xml.SelectNodes("map/objectgroup");
        XmlNode objectGroupNode;
        XmlNodeList objectNodeList;
        XmlAttributeCollection attributeCollection;
        XmlNode objectNode;
        for (int i = 0; i < nodeList.Count; i++)
        {
            objectGroupNode = nodeList.Item(i);
            if (objectGroupNode.Attributes.GetNamedItem("name").Value
                == nameof(map.collision))
            {
                objectNodeList = objectGroupNode.SelectNodes("object");
                map.collision = new Rect[objectNodeList.Count];
                for(int j = 0; j < objectNodeList.Count; j++)
                {
                    objectNode = objectNodeList.Item(j);
                    attributeCollection = objectNode.Attributes;
                    map.collision[j] = new Rect(
                        float.Parse(attributeCollection.GetNamedItem("x").Value),
                        float.Parse(attributeCollection.GetNamedItem("y").Value),
                        float.Parse(attributeCollection.GetNamedItem("width").Value),
                        float.Parse(attributeCollection.GetNamedItem("height").Value)
                        );
                }

            }
            else if (objectGroupNode.Attributes.GetNamedItem("name").Value
                == nameof(map.MonsterSpawner))
            {
                objectNodeList = objectGroupNode.SelectNodes("object");
                map.MonsterSpawner = new Vector2[objectNodeList.Count];
                for (int j = 0; j < objectNodeList.Count; j++)
                {
                    objectNode = objectNodeList.Item(j);
                    attributeCollection = objectNode.Attributes;
                    map.MonsterSpawner[j] = new Vector2(
                        float.Parse(attributeCollection.GetNamedItem("x").Value),
                        float.Parse(attributeCollection.GetNamedItem("y").Value));
                }
            }
            else
                continue;
        }
        //이름을 이용해서 변수를 불러오는 기능, 사용하기 까다롭고 적용시키기 애매해서 일단 주석 처리
        //map.GetType().GetProperty(attri.Value).SetValue();
        return map;
    }
}


public struct MapComponent
{
    public int tileHeight;
    public int tileWidth;
    public int heightTile;
    public int widthTile;
    public int[,] tiles;

    public Rect[] collision;
    public Vector2[] MonsterSpawner { get; set; }
}