﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;

class EntityOverlay : GameObjectList
{
    public EntityOverlay(GameObjectLibrary gameworld, string path, int layer = 101, string id = "")
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

        if (type.Length != 3)
        {
            return;
        }

        string asset = type[0];
        int boundingy = int.Parse(type[1]);
        EntityType et = EntityType.None;

        switch (type[2])
        {
            case "none":
                et = EntityType.None;
                break;
            case "item":
                et = EntityType.Item;
                break;
            case "player":
                et = EntityType.Player;
                break;
            case "enemy":
                et = EntityType.Enemy;
                break;
        }

        EntityButton button = new EntityButton(asset, boundingy, et);
        button.Position = new Vector2(x, 10);
        Add(button);
    }

    public override bool Visible
    {
        get { return visible; }
        set
        {
            visible = value;
            foreach (string id in children)
            {
                GameWorld.GetObject(id).Visible = visible;
            }
        }
    }
}