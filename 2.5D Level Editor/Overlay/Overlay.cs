using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;

class Overlay : GameObjectList
{
    public Overlay(GameObjectLibrary gameworld, string path ,int layer = 101, string id = "")
        : base(layer, id)
    {
        GameWorld = gameworld;
        ReadFile(path);
    }

    private void ReadFile(string path)
    {
        StreamReader streamReader = new StreamReader(path);
        string line = streamReader.ReadLine();
        int x = 20;
        while (line != "" && line != null)
        {
            MakeButton(x, line);
            x += 130;
            line = streamReader.ReadLine();
        }
        streamReader.Close();
    }

    private void MakeButton(int x, string line)
    {
        string[] type = line.Split(',');

        if (type.Length != 4)
        {
            return;
        }

        string asset = type[0];
        TileType tp = TileType.Background;
        TextureType tt = TextureType.None;
        TileObject to = TileObject.Tile;

        switch (type[1])
        {
            case "floor":
                tp = TileType.Floor;
                break;
            case "background":
                tp = TileType.Background;
                break;
            case "wall":
                tp = TileType.Wall;
                break;
        }

        switch (type[2])
        {
            case "none":
                tt = TextureType.None;
                break;
            case "grass":
                tt = TextureType.Grass;
                break;
            case "water":
                tt = TextureType.Water;
                break;
        }

        switch (type[3])
        {
            case "tile":
                to = TileObject.Tile;
                break;
            case "walltile":
                to = TileObject.WallTile;
                break;
            case "treetile":
                to = TileObject.TreeTile;
                break;
        }

        TileButton button = new TileButton(asset, tp, tt, to);
        button.Position = new Vector2(x, 10);
        Add(button);
    }

    public override bool Visible
    {
        get { return visible; }
        set { 
            visible = value; 
            foreach (string id in children)
            {
                GameWorld.GetObject(id).Visible = visible;
            }
        }
    }
}