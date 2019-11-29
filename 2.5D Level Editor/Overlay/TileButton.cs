using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class TileButton : Button
{
    protected TileType tiletype;
    protected TextureType texturetype;
    protected TileObject tileobject;
    protected string asset;
    public TileButton (string assetname, TileType tp, TextureType tt, TileObject to)
        : base(assetname)
    {
        sprite.SheetIndex = 0;
        asset = assetname;
        tiletype = tp;
        texturetype = tt;
        tileobject = to;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (pressed)
        {
            GameMouse mouse = GameWorld.GetObject("mouse") as GameMouse;
            mouse.Asset = asset;
            mouse.TileType = tiletype;
            mouse.TextureType = texturetype;
            mouse.TileObject = tileobject;
        }
    }
}