using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

class EditorState : IGameLoopObject
{
    protected ContentManager content;
    protected LevelEditer LevelEditer;

    public EditorState(ContentManager content)
    {
        this.content = content;
        LevelEditer = new LevelEditer();
    }

    public virtual void HandleInput(InputHelper inputHelper)
    {
        LevelEditer.HandleInput(inputHelper);
    }

    public virtual void Update (GameTime gameTime)
    {
        LevelEditer.Update(gameTime);
    }

    public  virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        LevelEditer.Draw(gameTime, spriteBatch);
    }

    public virtual void Reset()
    {
        LevelEditer.Reset();
    }
}
