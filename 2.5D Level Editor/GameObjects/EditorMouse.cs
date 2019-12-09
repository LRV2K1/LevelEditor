using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


class GameMouse : GameObject
{
    Vector2 mousePos;
    protected TextureType texturetype;
    protected TileType tiletype;
    protected TileObject tileobject;
    protected EntityType entitytype;
    protected EnemyType enemytype;
    protected bool item, tile;
    string asset;
    int entityBoundingBox;

    public GameMouse()
        : base (200, "mouse")
    {
        item = false;
        tile = false;
        asset = "";
        entityBoundingBox = 0;
        texturetype = TextureType.Grass;
        tiletype = TileType.Floor;
        tileobject = TileObject.Tile;
        entitytype = EntityType.None;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        Camera camera = GameWorld.GetObject("camera") as Camera;
        mousePos = inputHelper.MousePosition + camera.CameraPosition;
        position = inputHelper.MousePosition;

        if (inputHelper.MouseLeftButtonPressed() && position.X > 200 && position.Y < 830)
        {
            if (tile)
            {
                SwitchTile();
            }
            else if (item)
            {
                SwitchItem();
            }
        }

        if(inputHelper.MouseLeftButtonDown() && position.X > 200 && position.Y < 830 && tileobject == TileObject.Tile)
        {
            if (tile)
            {
                SwitchTile();
            }
        }
    }

    private void SwitchTile()
    {
        LevelGrid level = GameWorld.GetObject("levelgrid") as LevelGrid;
        level.SwitchTile(mousePos, tiletype, texturetype, tileobject, asset);
    }

    private void SwitchItem()
    {
        ItemGrid itemGrid = GameWorld.GetObject("itemgrid") as ItemGrid;
        itemGrid.SwitchItem(mousePos, entitytype, asset, entityBoundingBox);
    }

    public Vector2 MousePos
    {
        get { return mousePos; }
    }

    public string Asset
    {
        get { return asset; }
        set { asset = value; }
    }

    public TileType TileType
    {
        get { return tiletype; }
        set { tiletype = value; }
    }

    public TextureType TextureType
    {
        get { return texturetype; }
        set { texturetype = value; }
    }

    public TileObject TileObject
    {
        get { return tileobject; }
        set { tileobject = value; }
    }

    public EntityType EntityType
    {
        get { return entitytype; }
        set { entitytype = value; }
    }

    public EnemyType EnemyType
    {
        get { return enemytype; }
        set { enemytype = value; }
    }

    public int EntityBoundingy
    {
        get { return entityBoundingBox; }
        set { entityBoundingBox = value; }
    }

    public bool Tile
    {
        get { return tile; }
        set { tile = value;
            item = !tile;
        }
    }

    public bool Item
    {
        get { return item; }
        set { item = value;
            tile = !item;
        }
    }
}

