using Godot;

namespace Minesweeper;

public partial class Tile : Button
{
    [Signal]
    public delegate void MarkedEventHandler();
    public Vector2I TilePosition { set; get; }
    public TileType TileType { set; get; }

    public bool IsMarked { private set; get; }

    public readonly static Texture2D[] numberIcons = new Texture2D[8];
    public readonly static Color[] numbersColor = {
        new Color(0.36f, 0.69f, 1f),
        new Color(0.7f, 0.58f, 1f),
        new Color(1f, 0.37f, 0.93f),
        new Color(1f, 0.56f, 0.82f),
        new Color(1f, 0.21f, 0.31f),
        new Color(0.97f, 0.46f, 0f),
        new Color(0.57f, 0.94f, 0f),
        new Color(0f, 0.95f, 0.89f),
    };

    public Tile()
    {
        for (var i = 0; i < numberIcons.Length; i++)
        {
            numberIcons[i] = ResourceLoader.Load<Texture2D>($"res://assets/kenney-platformer-redux/hud_{i + 1}.tres");
        }

#if DEBUG
        MouseEntered += () =>
        {
            TooltipText = Size.ToString();
        };
#endif
    }

    public void SetValue(int value)
    {
        if (value > 0 && value < numberIcons.Length)
        {
            Icon = numberIcons[value - 1];
            AddThemeColorOverride("icon_disabled_color", numbersColor[value - 1]);
        }
    }

    public override void _GuiInput(InputEvent @event)
    {
        if (Disabled)
        {
            return;
        }

        if (@event is not InputEventMouseButton mouseButtonEvent)
        {
            return;
        }

        if (!@event.IsPressed())
        {
            return;
        }

        if (mouseButtonEvent.ButtonIndex == MouseButton.Right)
        {
            IsMarked = !IsMarked;
            EmitSignal(SignalName.Marked);
        }
        else if (mouseButtonEvent.ButtonIndex == MouseButton.Left)
        {
            Text = "";
        }
#if DEBUG
        else if (mouseButtonEvent.ButtonIndex == MouseButton.Middle)
        {
            TooltipText = Size.ToString();
        }
#endif
    }
}
