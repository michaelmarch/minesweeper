namespace LootLocker.Result;

using System.Text.Json.Serialization;

public struct ScoreSubmitResult : IResult
{
    [JsonPropertyName("member_id")]
    public string MemberId { set; get; }

    [JsonPropertyName("rank")]
    public long Rank { set; get; }

    [JsonPropertyName("score")]
    public long Score { set; get; }

    [JsonPropertyName("metadata")]
    public string? Metadata { set; get; }

    public override string ToString()
    {
        return this.PrettyFormat();
    }
}