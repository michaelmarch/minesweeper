#if TOOLS
using System;
using DiscordRPC;
using DiscordRPC.Logging;
using Godot;

namespace DiscordPlugin;
public class DiscordRichPresence
{
    private DiscordRpcClient? client;
    private readonly RichPresence richPresence = new()
    {
        Details = $"Project - {ProjectSettings.GetSetting("application/config/name").AsString()}",
        Assets = new Assets()
        {
            LargeImageKey = "icon512x512",
            LargeImageText = $"Godot Engine {Engine.GetVersionInfo()["string"].AsString()}",
        },
        Timestamps = new Timestamps()
        {
            Start = DateTime.UtcNow
        }
    };

    public DiscordRichPresence(string clientId)
    {
        client = new DiscordRpcClient(clientId, -1, new ConsoleLogger() { Level = LogLevel.None }, false);
    }

    public bool Init()
    {
        if (client is null)
        {
            return false;
        }

        client.Initialize();

        client.SetPresence(richPresence);
        return true;
    }

    public bool SetScriptPresence(string fileName)
    {
        if (client is null)
        {
            return false;
        }

        var extension = fileName.GetExtension();

        if (extension == "gd")
        {
            richPresence.Assets.SmallImageText = "GDScript";
            richPresence.Assets.SmallImageKey = "gd_script";
        }
        else if (extension == "cs")
        {
            richPresence.Assets.SmallImageText = "C# Script";
            richPresence.Assets.SmallImageKey = "c_sharp_script";
        }

        richPresence.State = $"Editing {fileName}";
        richPresence.Timestamps.Start = DateTime.UtcNow;
        client.SetPresence(richPresence);

        return true;
    }

    public bool SetScenePresence(string fileName)
    {
        if (client is null)
        {
            return false;
        }
        richPresence.State = $"Editing {fileName}";
        richPresence.Assets.SmallImageText = "Scene";
        richPresence.Assets.SmallImageKey = "packed_scene";
        richPresence.Timestamps.Start = DateTime.UtcNow;
        client.SetPresence(richPresence);

        return true;
    }

    public void Dispose()
    {
        client?.Dispose();
    }
}
#endif