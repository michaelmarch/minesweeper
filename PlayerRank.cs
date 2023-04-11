
using Godot;

public partial class PlayerRank : HBoxContainer
{

    private Label RankLabel;
    private Label ScoreLabel;

    [Export]
    private long Rank
    {
        get
        {
            if (RankLabel is not null)
            {
                return long.Parse(RankLabel.Text);
            }

            return 0;
        }
        set
        {
            if (RankLabel is not null)
            {
                RankLabel.Text = value.ToString();
            }
        }
    }

    [Export]
    private long Score
    {
        get
        {
            if (ScoreLabel is not null)
            {
                return long.Parse(ScoreLabel.Text);
            }

            return 0;
        }
        set
        {
            if (ScoreLabel is not null)
            {
                ScoreLabel.Text = value.ToString();
            }
        }
    }

    public override void _Ready()
    {
        RankLabel = GetNode<Label>("Rank");
        ScoreLabel = GetNode<Label>("Score");
    }
}
