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
        while (line != null)
        {
            textLines.Add(line);
            line = streamReader.ReadLine();
        }

        LoadTiles(textLines, width, tiletypeschar);
    }
}
