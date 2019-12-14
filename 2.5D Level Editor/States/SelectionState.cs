using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

class SelectionState : IGameLoopObject
{
    protected TextButton Load, New;
    protected ContentManager content;
    public SelectionState(ContentManager content)
    {
        this.content = content;
        Load = new TextButton("Fonts/Hud", "Load");
        Load.Color = Color.White;
        Load.Position = new Vector2(200, 200);
        New = new TextButton("Fonts/Hud", "New");
        New.Color = Color.White;
        New.Position = new Vector2(200, 230);
    }

    public void HandleInput(InputHelper inputHelper)
    {
        Load.HandleInput(inputHelper);
        New.HandleInput(inputHelper);
        //selecting load/new
        if (Load.Pressed)
        {
            EditorState editorState = GameEnvironment.GameStateManager.GetGameState("editorState") as EditorState;
            editorState.LoadLevel("Content/1.txt");
            GameEnvironment.GameStateManager.SwitchTo("editorState");
        }

        if (New.Pressed)
        {
            EditorState editorState = GameEnvironment.GameStateManager.GetGameState("editorState") as EditorState;
            editorState.NewLevel(40, 40);
            GameEnvironment.GameStateManager.SwitchTo("editorState");
        }
    }

    public void Update(GameTime gameTime)
    {
        Load.Update(gameTime);
        New.Update(gameTime);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        Load.Draw(gameTime, spriteBatch);
        New.Draw(gameTime, spriteBatch);
    }
    public  void Reset()
    {
        Load.Reset();
        New.Reset();
    }
}

