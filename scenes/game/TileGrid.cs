using System;
using System.Collections.Generic;

using Godot;

namespace Minesweeper;

public partial class TileGrid : GridContainer
{
    [Export]
    private GameOverDialog? Dialog;

    private List<Tile> tiles = new();
    private int collapsedTilesCount;
    private int mouseClicks;

    public override void _Ready()
    {
        Columns = Settings.Width;
#if DEBUG
        Clear();
#endif
        Fill();
        PlaceMines();
        Adjust();

        var tile = GetChild<Tile>(0);
        HashSet<Tile> a = new();
        a.Add(tile);
        a.Add(tile);

        GD.Print(a.Count);
    }

    public override void _EnterTree()
    {
        GetTree().Root.SizeChanged += OnResized;
    }

    private void OnResized()
    {
        Adjust();
    }

    private void Adjust()
    {
        if (tiles.Count == 0)
        {
            return;
        }

        // FIXME: dehardcode values
        // FINDME: if UI is broken, this is probably the cause
        // 67 - height of the top panel
        // 100 - margin top (50) + margin bottom (50) of the main board
        // 14 - PanelContainer texture margins (7 + 7)
        // 4 - Grid seperation constant
        var boardMaxSize = DisplayServer.WindowGetSize().Y - 67 - 100 - 14;
        var h = Settings.Height;
        var tileMinSize = (boardMaxSize - (h - 1) * 4) / h;
        var boardMinSize = tileMinSize * h + (h - 1) * 4 + 14;
        GetParent<Control>().CustomMinimumSize = new Vector2I(boardMinSize, boardMinSize);

        foreach (var tile in tiles)
        {
            tile.CustomMinimumSize = new Vector2I(tileMinSize, tileMinSize);
        }
    }
#if DEBUG
    private void Clear()
    {
        foreach (var child in GetChildren())
        {
            if (child is not Control)
            {
                continue;
            }
            RemoveChild(child);
            child.QueueFree();
        }
    }
#endif
    private void Fill()
    {
        var tileScene = ResourceLoader.Load<PackedScene>("res://scenes/game/tile/tile.tscn");

        for (var y = 0; y < Settings.Height; y++)
        {
            for (var x = 0; x < Settings.Width; x++)
            {
                var tile = CreateTile(x, y, tileScene);
                AddChild(tile);
                tiles.Add(tile);
            }
        }
    }

    private void PlaceMines()
    {
        var randomGenerator = new Random();

        var minesLeft = Settings.MineCount;

        while (minesLeft > 0)
        {
            var randomIndex = randomGenerator.Next() % tiles.Count;
            var tile = tiles[randomIndex];

            if (tile.TileType == TileType.Info)
            {
                tile.TileType = TileType.Mine;
                minesLeft--;
            }
        }
    }

    private Tile GetRandomInfoTile(Random random)
    {
        Tile tile;
        do
        {
            tile = tiles[random.Next() % tiles.Count];
        }
        while (tile.TileType == TileType.Mine);

        return tile;
    }

    private Tile CreateTile(int x, int y, PackedScene tileScene)
    {
        var tile = tileScene.Instantiate<Tile>();
#if DEBUG
        tile.Name = $"{y * Settings.Width + x}";
        tile.Owner = GetTree().EditedSceneRoot;
#endif
        tile.TilePosition = new Vector2I(x, y);
        tile.Pressed += () => TilePressed(tile);

        return tile;
    }

    private void CollapseNeighbors(Tile tile)
    {
        if (tile.Disabled)
        {
            return;
        }

        tile.Disabled = true;
        collapsedTilesCount++;

        var mineCount = CountNeighborMines(tile);

        if (mineCount > 0)
        {
            tile.SetValue(mineCount);
            return;
        }

        foreach (var neighborTile in GetNeighbors(tile.TilePosition))
        {
            CollapseNeighbors(neighborTile);
        }
    }

    private int CountNeighborMines(Tile tile)
    {
        var mineCount = 0;
        foreach (var neighborTile in GetNeighbors(tile.TilePosition))
        {
            if (neighborTile.TileType == TileType.Mine)
            {
                mineCount++;
            }
        }
        return mineCount;
    }

    private List<Tile> GetNeighbors(Vector2I pos)
    {
        var offsets = new Vector2I[]
        {
            new Vector2I(-1, -1), new Vector2I(0, -1), new Vector2I(1, -1),
            new Vector2I(-1, 0),                       new Vector2I(1, 0),
            new Vector2I(-1, 1),  new Vector2I(0, 1),  new Vector2I(1, 1)
        };

        List<Tile> neighbors = new();

        foreach (var offset in offsets)
        {
            var pos_offset = pos + offset;
            if (pos_offset.X >= 0 && pos_offset.X < Settings.Width && pos_offset.Y >= 0 && pos_offset.Y < Settings.Height)
            {
                neighbors.Add(tiles[To1dIndex(pos_offset)]);
            }
        }

        return neighbors;
    }

    private int To1dIndex(Vector2I pos)
    {
        return pos.Y * Settings.Width + pos.X;
    }

    private void TilePressed(Tile tile)
    {
        mouseClicks++;

        if (tile.TileType == TileType.Mine)
        {
            if (collapsedTilesCount != 0)
            {
                Dialog!.ShowCentered(new Vector2I(400, 400));
                GetTree().Paused = true;
                return;
            }

            // Make sure that the first clicked tile is never a mine.
            tile.TileType = TileType.Info;
            var infoTiles = tiles.FindAll((Tile tile) => tile.TileType == TileType.Info);

            var randomGenerator = new Random();
            var randomIndex = randomGenerator.Next() % infoTiles.Count;
            infoTiles[randomIndex].TileType = TileType.Mine;
        }

        CollapseNeighbors(tile);
    }

    private void OnWindowConfirmed()
    {
        GetTree().Paused = false;
        GetTree().ReloadCurrentScene();
    }

    private void OnWindowCanceled()
    {
        GetTree().Paused = false;
        GetTree().ChangeSceneToFile("res://scenes/main_menu/main_menu.tscn");
    }
}
