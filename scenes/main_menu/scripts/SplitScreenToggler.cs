using Godot;

namespace Minesweeper;

// That's a really hacky way to make toggleable panels.
// Might replace this with a TabContainer

public partial class SplitScreenToggler : Control
{
    [Export]
    private Control? OptionsScreen;

    [Export]
    private Control? CreditsScreen;

    [Export]
    private Button? OptionsButton;

    [Export]
    private Button? CreditsButton;

    [Export]
    private Control? ScreensHolder;

    public override void _Ready()
    {
        ScreensHolder!.Show();
        CreditsScreen!.Hide();
        OptionsScreen!.Show();

        GetTree().Root.MinSize = new Vector2I(720, 720);
    }

    private void OnOptionsButtonToggled(bool buttonPressed)
    {
        OptionsButton!.Disabled = buttonPressed;

        if (buttonPressed)
        {
            ScreensHolder!.Show();
            CreditsScreen!.Hide();
            OptionsScreen!.Show();
        }
    }

    private void OnCreditsButtonToggled(bool buttonPressed)
    {
        CreditsButton!.Disabled = buttonPressed;

        if (buttonPressed)
        {
            ScreensHolder!.Show();
            CreditsScreen!.Show();
            OptionsScreen!.Hide();
        }
    }

    private void OnExitButtonPressed()
    {
        CreditsButton!.ButtonPressed = false;
        OptionsButton!.ButtonPressed = false;

        ScreensHolder!.Hide();
    }
}
