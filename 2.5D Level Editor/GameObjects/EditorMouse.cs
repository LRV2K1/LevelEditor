using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


class GameMouse : SpriteGameObject
{
    Vector2 mousePos;
    protected TextureType texturetype;
    protected TileType tiletype;
    protected TileObject tileobject;
    string asset;

    public GameMouse()
        : base ("Sprites/Menu/spr_mouse",200, "mouse")
    {
        asset = "Sprites/Tiles/spr_grass_sheet_0@4x4";
        texturetype = TextureType.Grass;
        tiletype = TileType.Floor;
        tileobject = TileObject.Tile;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        Camera camera = GameWorld.GetObject("camera") as Camera;
        mousePos = inputHelper.MousePosition + camera.CameraPosition;
        position = inputHelper.MousePosition;

        if (inputHelper.MouseLeftButtonPressed() && position.X > 200 && position.Y < 830)
        {
            SwitchTile();
        }

        if(inputHelper.MouseLeftButtonDown() && position.X > 200 && position.Y < 830 && tileobject == TileObject.Tile)
        {
            SwitchTile();
        }
    }

    private void SwitchTile()
    {
        LevelGrid level = GameWorld.GetObject("levelgrid") as LevelGrid;
        level.SwitchTile(mousePos, tiletype, texturetype, tileobject, asset);
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
}

