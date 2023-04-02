using Godot;


namespace Minesweeper;

public partial class SizeConroller : MarginContainer
{

    public void Adjust()
    {
        // if (tiles.Count == 0)
        // {
        //     return;
        // }

        // var parent = GetParent<Control>();
        // parent.SizeFlagsHorizontal = Control.SizeFlags.ShrinkCenter;
        // parent.SizeFlagsVertical = Control.SizeFlags.Fill;
        // var windowHeight = (int)DisplayServer.WindowGetSize().Y;

        // var boardMaxSize = windowHeight - 67 - 100 - 14;

        // var h = tiles.Count / Columns;
        // var tileMinSize = (boardMaxSize - (h - 1) * 4) / h;

        // foreach (var tile in tiles)
        // {
        //     tile.CustomMinimumSize = new Vector2I(tileMinSize, tileMinSize);
        // }
        // if (Settings.Width == Settings.Height)
        // {

        // }
        // else if (Settings.Width > Settings.Height)
        // {
        //     parent.SizeFlagsHorizontal = Control.SizeFlags.ShrinkCenter;
        //     parent.SizeFlagsVertical = Control.SizeFlags.Fill;
        // }
        // else
        // {
        //     // TODO:
        // }
    }

}
