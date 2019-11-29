using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

partial class LevelEditer : GameObjectLibrary
{
    public void EditorStartUp(int width, int height)
    {
        RootList.Add(new GameMouse());

        LevelGrid levelGrid = new LevelGrid(width, height, 0, "levelgrid");
        RootList.Add(levelGrid);
        levelGrid.CellWidth = 108;
        levelGrid.CellHeight = 54;
        levelGrid.SetupGrid();

        Camera camera = new Camera();
        camera.SetupCamera = levelGrid.AnchorPosition(5, 5) - GameEnvironment.Screen.ToVector2() / 2;
        RootList.Add(camera);

        LoadOverlay();
    }

    private void LoadOverlay()
    {
        OverlayStatus overlay = new OverlayStatus(this);
        RootList.Add(overlay);

        overlay.AddStatus("Floor", new Overlay(this, "Content/Tiles/Floor.txt"));
        overlay.AddStatus("Wall", new Overlay(this, "Content/Tiles/Wall.txt"));
        overlay.AddStatus("Tree", new Overlay(this, "Content/Tiles/Tree.txt"));
        overlay.AddStatus("Items", new Overlay(this, "Content/Tiles/Wall.txt"));
    }
}