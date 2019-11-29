using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

partial class LevelEditer : GameObjectLibrary
{
    public void NewLevel(int width, int height)
    {
        LevelGrid levelGrid = new LevelGrid(width, height, 0, "levelgrid");
        RootList.Add(levelGrid);
        levelGrid.CellWidth = 108;
        levelGrid.CellHeight = 54;
        levelGrid.SetupGrid();
    }

    private void LoadOverlay()
    {
        RootList.Add(new GameMouse());

        OverlayStatus overlay = new OverlayStatus(this);
        RootList.Add(overlay);

        overlay.AddStatus("Floor", new Overlay(this, "Content/Tiles/Floor.txt"));
        overlay.AddStatus("Wall", new Overlay(this, "Content/Tiles/Wall.txt"));
        overlay.AddStatus("Tree", new Overlay(this, "Content/Tiles/Tree.txt"));
        overlay.AddStatus("Items", new Overlay(this, "Content/Tiles/Wall.txt"));

        LevelGrid levelGrid = GetObject("levelgrid") as LevelGrid;
        Camera camera = new Camera();
        camera.SetupCamera = levelGrid.AnchorPosition(5, 5) - GameEnvironment.Screen.ToVector2() / 2;
        RootList.Add(camera);
    }

    private void LoadTiles(List<string> textlines, int width, Dictionary<char, string> tiletypechar)
    {
        LevelGrid levelGrid = new LevelGrid(width, textlines.Count, 0, "levelgrid");
        RootList.Add(levelGrid);
        levelGrid.CellWidth = 108;
        levelGrid.CellHeight = 54;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < textlines.Count; y++)
            {
                Tile t = LoadTile(x, y, tiletypechar[textlines[y][x]]);
                levelGrid.Add(t, x, y);
            }
        }
    }

    private Tile LoadTile(int x, int y, string tiletype)
    {
        string[] type = tiletype.Split(',');
        string asset = type[0];
        TileType tp = TileType.Background;
        TextureType tt = TextureType.None;

        switch (type[1])
        {
            case "Floor":
                tp = TileType.Floor;
                break;
            case "Background":
                tp = TileType.Background;
                break;
            case "Wall":
                tp = TileType.Wall;
                break;
        }

        switch (type[2])
        {
            case "None":
                tt = TextureType.None;
                break;
            case "Grass":
                tt = TextureType.Grass;
                break;
            case "Water":
                tt = TextureType.Water;
                break;
        }

        switch (type[3])
        {
            case "Tile":
                return new Tile(new Point(x, y), asset, tp, tt);
            case "WallTile":
                return new WallTile(new Point(x, y), asset, tp, tt);
            case "TreeTile":
                return new TreeTile(new Point(x, y), asset, tp, tt);
        }

        return new Tile(new Point(x, y));
    }
}