using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class EntityButton : Button
{
    //button for entities
    protected EntityType entitytype;
    protected EnemyType enemytype;
    protected ItemType itemtype;
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
        //give information to the mouse
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
            else if (entitytype == EntityType.AnimatedItem || entitytype == EntityType.SpriteItem)
            {
                mouse.ItemType = itemtype;
            }
        }
    }

    public EnemyType EnemyType
    {
        set { enemytype = value; }
    }

    public ItemType ItemType
    {
        set { itemtype = value; }
    }
}