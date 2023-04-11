using System.Text.Json.Serialization;

namespace LootLocker.Result;

public struct ScoreListResult : IResult
{
    [JsonPropertyName("pagination")]
    public PaginationInfo Pagination { set; get; }

    [JsonPropertyName("items")]
    public Item[] Items { set; get; }

    public struct PaginationInfo
    {
        [JsonPropertyName("total")]
        public long Total { set; get; }

        [JsonPropertyName("next_cursor")]
        public long? NextCursor { set; get; }
        [JsonPropertyName("previous_cursor")]
        public long? PreviousCursor { set; get; }
    }

    public struct Item
    {
        [JsonPropertyName("member_id")]
        public string MemberId;
        [JsonPropertyName("rank")]
        public long Rank;
        [JsonPropertyName("score")]
        public long Score;
        [JsonPropertyName("player")]
        public Player Player;
        [JsonPropertyName("metadata")]
        public string Metadata;

    }

    public struct Player
    {
        [JsonPropertyName("id")]
        public long Id;
        [JsonPropertyName("player_uid")]
        public string Uid;
        [JsonPropertyName("name")]
        public string Name;
    }
}