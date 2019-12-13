using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

enum TileType
{
    Background,
    Floor,
    Wall
}

enum TextureType
{
    None,
    Grass,
    Water,
    Mine
}

enum TileObject
{
    Tile,
    GrassTile,
    WallTile,
    TreeTile,
}

class Tile : SpriteGameObject
{

    protected TileType type;
    protected TextureType texturetype;
    protected TileObject tileobject;
    protected Point grid;

    public Tile(Point grid, string assetname = "", TileType tp = TileType.Background, TextureType tt = TextureType.None, int layer = 0, string id = "")
        : base(assetname, layer, id, 0)
    {
        tileobject = TileObject.Tile;
        this.grid = grid;
        texturetype = tt;
        type = tp;
    }

    public override void Reset()
    {
        InitializeTile();
    }

    public virtual void ChangeTile(TileType tp, TextureType tt, string assetName = "")
    {
        sprite = null;
        sprite = new SpriteSheet(assetName);
        texturetype = tt;
        type = tp;

        LevelGrid levelGrid = GameWorld.GetObject("levelgrid") as LevelGrid;
        for (int x = grid.X -1; x <= grid.X + 1; x++)
        {
            for (int y = grid.Y - 1; y <= grid.Y + 1; y++)
            {
                Tile tile = levelGrid.Get(x, y) as Tile;
                if (tile != null)
                {
                    tile.InitializeTile();
                }
            }
        }
    }

    public TileType TileType
    {
        get { return type; }
    }

    public TextureType TextureType
    {
        get { return texturetype; }
    }

    public TileObject TileObject
    {
        get { return tileobject; }
    }

    public virtual void InitializeTile()
    {
        if (type == TileType.Background)
        {
            return;
        }

        origin = new Vector2(Width/2, sprite.Height / 2);

        SetSprite();
    }

    public virtual void SetSprite()
    {
        int r = CalculateSurroundingStraightTiles();
        int s = CalculateSurroundingSideTiles();
        if (r != 0)
        {
            sprite.SheetIndex = r % 16;
        }
        else
        {
            sprite.SheetIndex = s % 16 + 16;
        }
    }
    public virtual int CalculateSurroundingStraightTiles()
    {

        LevelGrid levelGrid = GameWorld.GetObject("levelgrid") as LevelGrid;
        //regt
        int r = 0;
        if (levelGrid.GetTextureType(grid.X, grid.Y - 1) == texturetype)
        {
            r += 1;
        }
        if (levelGrid.GetTextureType(grid.X + 1, grid.Y) == texturetype)
        {
            r += 2;
        }
        if (levelGrid.GetTextureType(grid.X, grid.Y + 1) == texturetype)
        {
            r += 4;
        }
        if (levelGrid.GetTextureType(grid.X - 1, grid.Y) == texturetype)
        {
            r += 8;
        }
        return r;
    }

    public virtual int CalculateSurroundingSideTiles()
    {
        LevelGrid levelGrid = GameWorld.GetObject("levelgrid") as LevelGrid;
        //schuin
        int s = 0;
        if (levelGrid.GetTextureType(grid.X + 1, grid.Y - 1) == texturetype)
        {
            s += 1;
        }
        if (levelGrid.GetTextureType(grid.X + 1, grid.Y + 1) == texturetype)
        {
            s += 2;
        }
        if (levelGrid.GetTextureType(grid.X - 1, grid.Y + 1) == texturetype)
        {
            s += 4;
        }
        if (levelGrid.GetTextureType(grid.X - 1, grid.Y - 1) == texturetype)
        {
            s += 8;
        }
        return s;
    }

}

