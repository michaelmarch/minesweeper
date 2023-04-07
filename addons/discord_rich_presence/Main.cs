#if TOOLS
using Godot;

namespace DiscordPlugin;
[Tool]
public partial class Main : EditorPlugin
{
    private readonly DiscordRichPresence drp = new("844130645081063435");
    private ItemList? MembersOverview;
    public override void _EnterTree()
    {
        MembersOverview = FindMembersOverview();
        if (MembersOverview == null)
        {
            return;
        }
        MembersOverview.ItemSelected += ItemSelected;

        SceneChanged += SceneChangedCallback;
        drp.Init();
    }

    private void ItemSelected(long index)
    {
        var tooltip = MembersOverview!.GetItemTooltip((int)index);

        if (tooltip.StartsWith("res://"))
        {
            drp.SetScriptPresence(tooltip.GetFile());
        }
    }

    private void SceneChangedCallback(Node root)
    {
        drp.SetScenePresence(GetEditorInterface().GetEditedSceneRoot().SceneFilePath.GetFile());
    }

    private ItemList? FindMembersOverview()
    {
        var editorControl = GetEditorInterface().GetBaseControl();

        var membersOverview = editorControl.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild<ItemList>(1);

        return membersOverview;
    }

    public override void _ExitTree()
    {
        drp.Dispose();
    }
}
#endif