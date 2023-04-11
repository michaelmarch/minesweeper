using System.Net.Http;
using static Logging.Logger;

namespace Logging.Extensions;

public static class HttpLoggerExtensions
{
    public static void Log(this Logger logger, Level level, HttpResponseMessage responseMessage)
    {
        logger.Log(level, responseMessage.ToString());
    }
}

