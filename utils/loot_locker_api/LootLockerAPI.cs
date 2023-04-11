using System.Threading.Tasks;

using Logging;
using LootLocker.Result;

namespace LootLocker;

public class LootLockerAPI : API<LootLockerAPI>
{
    private static readonly Logger Logger = Logger.Create("LootLocker");
    private readonly string GameKey;

    private const string BaseUrl = "https://api.lootlocker.io/game/";
    private const string GameVersion = "0.10.0.0";

    public LootLockerAPI(string gameKey) : base(BaseUrl)
    {
        GameKey = gameKey;
    }

    public async Task<GuestAuthResponse> GuestAuthenticate(string playerIdentifier)
    {
        if (playerIdentifier.Length != 0)
        {
            return await PostAsync<GuestAuthResponse>(
                "v2/session/guest",
                new(),
                new
                {
                    game_key = GameKey,
                    game_version = GameVersion,
                    player_identifier = playerIdentifier,
                }
            );
        }

        return await PostAsync<GuestAuthResponse>(
            "v2/session/guest",
            new(),
            new
            {
                game_key = GameKey,
                game_version = GameVersion,
            }
        );
    }

    public async Task<ScoreSubmitResult> SumbitScore(long score, string leaderboardId, string sessionToken)
    {
        return await PostAsync<ScoreSubmitResult>(
            $"leaderboards/{leaderboardId}/submit",
            new() { { "x-session-token", sessionToken } },
            new
            {
                Score = score
            }
        );
    }

    private async Task<ScoreListResult> GetScoreList(string leaderboardId, string sessionToken, int count, int after)
    {
        return await GetAsync<ScoreListResult>(
            $"leaderboards/{leaderboardId}/list",
            new() { { "x-session-token", sessionToken } },
            new() {
                { "count", count.ToString() },
                { "after", after.ToString() }
            }
        );
    }


}