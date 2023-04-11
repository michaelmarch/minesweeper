using Godot;

using LootLocker;

public partial class Test : Node
{
    private static readonly ConfigFile config = new();
#if DEBUG
    private static readonly LootLockerAPI Api = new("dev_5d7395cd89e0466f8e7a310e52e643fa");
#else
    private static readonly LootLockerAPI Api = new("prod_d92753f8a7764844a14b011e9d257a68");
#endif
    public async override void _Ready()
    {
        if (!Godot.FileAccess.FileExists("user://lootlocker.cfg"))
        {
            Godot.FileAccess.Open("user://lootlocker.cfg", Godot.FileAccess.ModeFlags.Write).Close();
        }

        var error = config.Load("user://lootlocker.cfg");
        var playerIdentifier = config.GetValue("keys", "player_indentifier", "").As<string>();
        var result = await Api.GuestAuthenticate(playerIdentifier);
    }

    public override void _ExitTree()
    {
        var error = config.Save("user://lootlocker.cfg");
        //GD.Print(error.ToString());
    }
}
