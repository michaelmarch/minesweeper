using Godot;

namespace Minesweeper;

public partial class MainMenu : Node
{

    [Export]
    private OptionButton? DifficultyOption;
    private void OnPlayButtonPressed()
    {
        SaveSettings();
        GetTree().ChangeSceneToFile("res://scenes/game/game.tscn");
    }

    private void SaveSettings()
    {
        var index = DifficultyOption?.GetSelectedId();
        switch (index)
        {
            case 0:
                {
                    Settings.Width = 10;
                    Settings.Height = 10;
                    Settings.MineCount = 10;
                    break;
                }
            case 1:
                {
                    Settings.Width = 12;
                    Settings.Height = 12;
                    Settings.MineCount = 30;
                    break;
                }
            case 2:
                {
                    Settings.Width = 16;
                    Settings.Height = 16;
                    Settings.MineCount = 99;
                    break;
                }
            case 3:
                {
                    Settings.Width = 20;
                    Settings.Height = 20;
                    Settings.MineCount = 120;
                    break;
                }
            case 4:
                {
                    Settings.Width = 30;
                    Settings.Height = 20;
                    Settings.MineCount = 200;
                    break;
                }
            default:
                {
                    // Error ?
                    break;
                }
        }

        Settings.TournamentMode = false;
    }

    private void OnQuitButtonPressed()
    {
        GetTree().Quit();
    }
}
