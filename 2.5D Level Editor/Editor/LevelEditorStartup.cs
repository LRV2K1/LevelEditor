using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

partial class LevelEditer : GameObjectLibrary
{
    //make new level
    public void NewLevel(int width, int height)
    {
        LevelGrid levelGrid = new LevelGrid(width, height, 0, "levelgrid");
        RootList.Add(levelGrid);
        levelGrid.CellWidth = 108;
        levelGrid.CellHeight = 54;
        levelGrid.SetupGrid();

        ItemGrid itemGrid = new ItemGrid(width, height, 1, "itemgrid");
        RootList.Add(itemGrid);
        itemGrid.CellWidth = 108;
        itemGrid.CellHeight = 54;
        itemGrid.SetupGrid();
    }

    //load all overlays
    private void LoadOverlay()
    {
        RootList.Add(new GameMouse());

        OverlayStatus overlay = new OverlayStatus(this);
        RootList.Add(overlay);

        overlay.AddStatus("Floor", new TileOverlay(this, "Content/Tiles/Floor.txt"));
        overlay.AddStatus("Wall", new TileOverlay(this, "Content/Tiles/Wall.txt"));
        overlay.AddStatus("Cave", new TileOverlay(this, "Content/Tiles/Cave.txt"));
        overlay.AddStatus("Tree", new TileOverlay(this, "Content/Tiles/Tree.txt"));
        overlay.AddStatus("Items", new EntityOverlay(this, "Content/Entities/Item.txt"));
        overlay.AddStatus("Objects", new EntityOverlay(this, "Content/Entities/Object.txt"));
        overlay.AddStatus("Cave_Objects", new EntityOverlay(this, "Content/Entities/Cave_Object.txt"));
        overlay.AddStatus("Enemies", new EntityOverlay(this, "Content/Entities/Enemy.txt"));
        overlay.AddStatus("Spawn", new EntityOverlay(this, "Content/Entities/Spawn.txt"));

        LevelGrid levelGrid = GetObject("levelgrid") as LevelGrid;
        Camera camera = new Camera();
        camera.SetupCamera = levelGrid.AnchorPosition(5, 5) - GameEnvironment.Screen.ToVector2() / 2;
        RootList.Add(camera);
    }

    //load tiles
    private void LoadTiles(List<string> textlines, int width, Dictionary<char, string> tiletypechar)
    {
        LevelGrid levelGrid = new LevelGrid(width, textlines.Count, 0, "levelgrid");
        RootList.Add(levelGrid);
        levelGrid.CellWidth = 108;
        levelGrid.CellHeight = 54;

        //check all characters
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < textlines.Count; y++)
            {
                Tile t = LoadTile(x, y, tiletypechar[textlines[y][x]]);
                levelGrid.Add(t, x, y);
            }
        }
    }

    //laod tile
    private Tile LoadTile(int x, int y, string tiletype)
    {
        string[] type = tiletype.Split(',');
        string set = type[0];
        TileType tp = (TileType)Enum.Parse(typeof(TileType), type[1]);
        TextureType tt = (TextureType)Enum.Parse(typeof(TextureType), type[2]);

        //make different tiles
        switch (type[3])
        {
            case "Tile":
                return new Tile(new Point(x, y), set, tp, tt);
            case "WallTile":
                return new WallTile(new Point(x, y), set, tp, tt);
            case "TreeTile":
                return new TreeTile(new Point(x, y), set, tp, tt);
            case "GrassTile":
                return new GrassTile(new Point(x, y), set, tp, tt);
        }

        return new Tile(new Point(x, y));
    }

    //load entities
    private void LoadEntities(List<string> textlines, int width, Dictionary<char, string> entitytypechar)
    {
        ItemGrid itemGrid = new ItemGrid(width, textlines.Count, 0, "itemgrid");
        RootList.Add(itemGrid);
        itemGrid.CellWidth = 108;
        itemGrid.CellHeight = 54;

        //check all characters
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < textlines.Count; y++)
            {
                Entity t = LoadEntity(x, y, entitytypechar[textlines[y][x]]);
                itemGrid.Add(t, x, y);
            }
        }
    }

    //load entity
    private Entity LoadEntity(int x, int y, string entitytype)
    {
        string[] type = entitytype.Split(',');
        if (type[0] == "None")
        {
            return new Entity(new Point(x, y));
        }

        string asset = type[0];
        int boundingy = int.Parse(type[1]);
        EntityType et = (EntityType)Enum.Parse(typeof(EntityType), type[2]);
        Entity entity = new Entity(new Point(x, y), asset, boundingy, et);
        //check for additional information
        if (et == EntityType.Enemy)
        {
            entity.EnemyType = (EnemyType)Enum.Parse(typeof(EnemyType), type[3]);
        }
        if (et == EntityType.SpriteItem || et == EntityType.AnimatedItem)
        {
            entity.ItemType = (ItemType)Enum.Parse(typeof(ItemType), type[3]);
        }
        return entity;
    }
}