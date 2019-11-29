using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


class Game1 : GameEnvironment
{
    TextGameObject framecounter, physicscounter;

    protected int frames = 0;
    protected int physics = 0;
    protected double time = 0;
    protected double time2 = 0;

    static void Main()
    {
        Game1 game = new Game1();
        game.Run();
    }

    public Game1()
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        screen = new Point(1920, 1080);
        windowSize = new Point(1600, 900);
        FullScreen = false;

        framecounter = new TextGameObject("Fonts/Hud", 101, "framecounter");
        framecounter.Position = new Vector2(10, 10);
        framecounter.Color = Color.Black;
        physicscounter = new TextGameObject("Fonts/Hud", 101, "physicscounter");
        physicscounter.Position = new Vector2(10, 30);
        physicscounter.Color = Color.Black;

        gameStateManager.AddGameState("editorState", new EditorState(Content));
        GameStateManager.AddGameState("selectionState", new SelectionState(Content));
        gameStateManager.SwitchTo("selectionState");
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

            physics++;
        if (time2 <= 0)
        {
            time2 = 1;
            physicscounter.Text = physics.ToString();
            physics = 0;
        }
        else
        {
            time2 -= gameTime.ElapsedGameTime.TotalSeconds;
        }

    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        spriteBatch.Begin();
        physicscounter.Draw(gameTime, spriteBatch);
        framecounter.Draw(gameTime, spriteBatch);
        spriteBatch.End();

            frames++;
        if (time <= 0)
        {
            time = 1;
            framecounter.Text = frames.ToString();
            frames = 0;
        }
        else
        {
            time -= gameTime.ElapsedGameTime.TotalSeconds;
        }

    }
}
