using Godot;

namespace Minesweeper;

public partial class GameOverDialog : VBoxContainer
{

    [Signal]
    public delegate void CancelledEventHandler();

    [Signal]
    public delegate void ConfirmedEventHandler();

    public override void _Ready()
    {
        // Just like popups, hidden by default
        Visible = false;
    }

    public void ShowCentered(Vector2I size)
    {
        if (Visible)
        {
            return;
        }

        Size = size;
        Position = (GetTree().Root.Size - size) / 2;

        Visible = true;
    }


    private void OnExitButtonPressed()
    {
        Visible = false;
        EmitSignal(SignalName.Cancelled);
    }

    private void OnConfirmButtonPressed()
    {
        EmitSignal(SignalName.Confirmed);
    }

    private void OnCancelButtonPressed()
    {
        EmitSignal(SignalName.Cancelled);
    }
}
