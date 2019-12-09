using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class EntityButton : Button
{
    protected EntityType entitytype;
    protected EnemyType enemytype;
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
            if (entitytype == EntityType.Enemy)
            {
                mouse.EnemyType = enemytype;
            }
        }
    }

    public EnemyType EnemyType
    {
        set { enemytype = value; }
    }
}