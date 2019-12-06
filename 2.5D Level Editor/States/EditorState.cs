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
    }

    public void NewLevel(int x, int y)
    {
        LevelEditer = new LevelEditer(x, y);
    }

    public void LoadLevel(string path)
    {
        LevelEditer = new LevelEditer(0, 0, true, path);
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
