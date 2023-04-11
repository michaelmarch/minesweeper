namespace LootLocker.Result;

using System.Text.Json.Serialization;

public struct GuestAuthResponse : IResult
{
    [JsonPropertyName("success")]
    public bool Success { set; get; }

    [JsonPropertyName("session_token")]
    public string SessionToken { set; get; }

    [JsonPropertyName("player_id")]
    public long PlayerId { set; get; }

    [JsonPropertyName("public_uid")]
    public string PublicUId { set; get; }

    [JsonPropertyName("player_identifier")]
    public string PlayerIdentifier { set; get; }

    [JsonPropertyName("player_created_at")]
    public string PlayerCreatedAt { set; get; }

    [JsonPropertyName("check_grant_notifications")]
    public bool CheckGrantNotifications { set; get; }

    [JsonPropertyName("check_deactivation_notifications")]
    public bool CheckDeactivationNotifications { set; get; }

    [JsonPropertyName("seen_before")]
    public bool SeenBefore { set; get; }

    public override string ToString()
    {
        return this.PrettyFormat();
    }
}