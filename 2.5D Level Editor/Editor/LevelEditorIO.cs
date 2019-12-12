using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

partial class LevelEditer : GameObjectLibrary
{
    public void Save()
    {
        Dictionary<string, char> tiletypeschar = new Dictionary<string, char>();
        List<string> tiletypes = new List<string>();

        StreamWriter streamWriter = new StreamWriter("Content/1.txt");
        LevelGrid level = GetObject("levelgrid") as LevelGrid;
        string[] map = new string[level.Rows];
        char type = 'a';

        for (int y = 0; y < level.Rows; y++)
        {
            string line = "";
            for (int x = 0; x < level.Columns; x++)
            {
                char tilechar = ' ';
                Tile tile = GetObject(level.Grid[x, y]) as Tile;
                string tiletype = tile.Sprite.Sprite.ToString() + "," + tile.TileType.ToString() + "," + tile.TextureType.ToString() + "," + tile.TileObject.ToString();

                if (!tiletypeschar.ContainsKey(tiletype))
                {
                    tiletypeschar.Add(tiletype, type);
                    type = (char)((int)type + 1);
                    tiletypes.Add(tiletype);
                }

                tilechar = tiletypeschar[tiletype];
                line += tilechar;
            }
            map[y] = line;
        }

        for (int i = 0; i < tiletypes.Count; i++)
        {
            streamWriter.WriteLine(tiletypeschar[tiletypes[i]] +":"+ tiletypes[i]);
        }
        streamWriter.WriteLine("");
        
        for (int y = 0; y < level.Rows; y++)
        {
            streamWriter.WriteLine(map[y]);
        }
        streamWriter.WriteLine("");

        Dictionary<string, char> entitytypechar = new Dictionary<string, char>();
        List<string> entitytypes = new List<string>();
        ItemGrid itemGrid = GetObject("itemgrid") as ItemGrid;
        string[] items = new string[level.Rows];
        type = 'a';

        for (int y = 0; y < level.Rows; y++)
        {
            string line = "";
            for (int x = 0; x < level.Columns; x++)
            {
                char entitychar = ' ';
                Entity entity = GetObject(itemGrid.Grid[x, y]) as Entity;

                if (entity.EntityType == EntityType.None)
                {
                    if (!entitytypechar.ContainsKey("None"))
                    {
                        entitytypechar.Add("None", type);
                        type = (char)((int)type + 1);
                        entitytypes.Add("None");
                    }
                    entitychar = entitytypechar["None"];
                    line += entitychar;
                    continue;
                }
                string entitytype = entity.Sprite.Sprite.ToString() + "," + entity.Boundingy.ToString() + "," + entity.EntityType.ToString();
                if (entity.EntityType == EntityType.Enemy)
                {
                    entitytype += "," + entity.EnemyType.ToString();
                }
                else if (entity.EntityType == EntityType.AnimatedItem || entity.EntityType == EntityType.SpriteItem)
                {
                    entitytype += "," + entity.ItemType.ToString();
                }
                
                if (!entitytypechar.ContainsKey(entitytype))
                {
                    entitytypechar.Add(entitytype, type);
                    type = (char)((int)type + 1);
                    entitytypes.Add(entitytype);
                }

                entitychar = entitytypechar[entitytype];
                line += entitychar;
            }
            map[y] = line;
        }

        for (int i = 0; i < entitytypes.Count; i++)
        {
            streamWriter.WriteLine(entitytypechar[entitytypes[i]] + ":" + entitytypes[i]);
        }
        streamWriter.WriteLine("");

        for (int y = 0; y < level.Rows; y++)
        {
            streamWriter.WriteLine(map[y]);
        }

        streamWriter.Close();
    }

    public void Load(string path)
    {
        Dictionary<char, string> tiletypeschar = new Dictionary<char, string>();
        List<string> textLines = new List<string>();
        StreamReader streamReader = new StreamReader(path);

        string line = streamReader.ReadLine();
        while (line != "")
        {
            string[] types = line.Split(':');
            char[] a = types[0].ToCharArray();
            tiletypeschar.Add(a[0], types[1]);
            line = streamReader.ReadLine();
        }

        line = streamReader.ReadLine();
        int width = line.Length;
        while (line != "")
        {
            textLines.Add(line);
            line = streamReader.ReadLine();
        }

        LoadTiles(textLines, width, tiletypeschar);

        Dictionary<char, string> entitytypeschar = new Dictionary<char, string>();
        List<string> entityLines = new List<string>();

        line = streamReader.ReadLine();
        while (line != "")
        {
            string[] types = line.Split(':');
            char[] a = types[0].ToCharArray();
            entitytypeschar.Add(a[0], types[1]);
            line = streamReader.ReadLine();
        }
        
        line = streamReader.ReadLine();
        width = line.Length;
        while (line != null)
        {
            entityLines.Add(line);
            line = streamReader.ReadLine();
        }

        LoadEntities(entityLines, width, entitytypeschar);
        
        streamReader.Close();
    }
}
