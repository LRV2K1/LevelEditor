using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

enum EntityType
{
    None,
    SpriteItem,
    AnimatedItem,
    Player,
    Enemy
}

enum EnemyType
{
    Close,
    Ranged
}

class Entity : SpriteGameObject
{
    protected EntityType type;
    protected EnemyType enemy;
    protected Point grid;
    protected int boundingy;

    public Entity(Point grid, string assetname = "", int boundingy = 0, EntityType et = EntityType.None, int layer = 0, string id = "")
        : base(assetname, layer, id)
    {
        this.boundingy = boundingy;
        this.grid = grid;
        type = et;
    }

    public  EntityType EntityType
    {
        get { return type; }
        set { type = value; }
    }

    public EnemyType EnemyType
    {
        get { return enemy; }
        set { enemy = value; }
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (type == EntityType.None)
        {
            return;
        }
        base.Draw(gameTime, spriteBatch);
    }

    public override void Reset()
    {
        InitializeTile();
    }

    public virtual void InitializeTile()
    {
        if (type == EntityType.None)
        {
            return;
        }
        ItemGrid itemGrid = GameWorld.GetObject("itemgrid") as ItemGrid;

        origin = new Vector2(Width / 2, sprite.Height - boundingy/2);
    }

    public int Boundingy
    {
        get { return boundingy; }
        set { boundingy = value; }
    }

}