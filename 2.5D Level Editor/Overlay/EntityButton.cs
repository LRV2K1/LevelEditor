﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class EntityButton : Button
{
    protected EntityType entitytype;
    protected string asset;
    protected int boundingy;
    public EntityButton(string assetname, int boundingy, EntityType et)
        : base(assetname)
    {
        sprite.SheetIndex = 0;
        this.boundingy = boundingy;
        asset = assetname;
        entitytype = et;
    }

    public override void HandleInput(InputHelper inputHelper)
    {
        base.HandleInput(inputHelper);
        if (pressed)
        {
            GameMouse mouse = GameWorld.GetObject("mouse") as GameMouse;
            mouse.Asset = asset;
            mouse.EntityBoundingy = boundingy;
            mouse.EntityType = entitytype;
            mouse.Item = true;
        }
    }
}