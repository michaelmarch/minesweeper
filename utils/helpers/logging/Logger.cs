using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;

using static Godot.GD;

namespace Logging;

public class Logger
{
    public enum Level
    {
#if DEBUG
        Debug = 1,
#endif
        Info = 1 << 1,
        Warning = 1 << 2,
        Error = 1 << 3,
        None = 1 << 15
    }

#if !DEBUG
    private static Queue<LogData> LoggerQueue = new();
    private ushort MaxBufferSize = 100;
#endif

    private static Dictionary<string, Logger> LoggerMap = new();
    private readonly string ModuleName;


    private Logger(string moduleName)
    {
        ModuleName = moduleName;
    }

    public static Logger Create(string moduleName)
    {
        moduleName = moduleName.Trim();
        moduleName = string.Concat(moduleName[0].ToString().ToUpper(), moduleName.AsSpan(1));

        if (LoggerMap.ContainsKey(moduleName))
        {
            return LoggerMap[moduleName];
        }

        var logger = new Logger(moduleName);
        LoggerMap[moduleName] = logger;

        return logger;
    }

    [Conditional("DEBUG")]
    public void Debug(string message)
    {
        Log(Level.Debug, message);
    }

    public void Info(string message)
    {
        Log(Level.Info, message);
    }

    public void Warning(string message)
    {
        Log(Level.Warning, message);
    }

    public void Error(string message)
    {
        Log(Level.Error, message);
    }

    public void Log(Level level, string message)
    {
        var logData = new LogData(
            time: DateTime.Now,
            level: level,
            moduleName: ModuleName,
            message: message
        );
#if DEBUG
        Print(logData);
#else
        LoggerQueue.Enqueue(logData);

        if (LoggerQueue.Count >= MaxBufferSize)
        {
            Flush();
        }
#endif
    }

    public void Log(Level level, HttpResponseMessage httpResponseMessage)
    {
        var message = httpResponseMessage.PrettyFormat();

        Log(level, message);
    }

    private void Store(LogData logData)
    {
        if (logData.Level <= Level.Info)
        {
            Print(logData);
        }
        else if (logData.Level == Level.Warning)
        {
            PushWarning(logData);
        }
        else if (logData.Level == Level.Error)
        {
            PushError(logData);
        }
    }

#if !DEBUG
    public void Flush()
    {
        while (LoggerQueue.Count > 0)
        {
            var logData = LoggerQueue.Dequeue();
            Store(logData);
        }
    }
#endif

    private readonly struct LogData
    {
        public readonly DateTime Time;
        public readonly Level Level;
        public readonly string ModuleName;
        public readonly string Message;

        public LogData(DateTime time, Level level, string moduleName, string message)
        {
            Time = time;
            Level = level;
            ModuleName = moduleName;
            Message = message;
        }

        public override string ToString()
        {
            var hour = Time.Hour > 9 ? $"{Time.Hour}" : $"0{Time.Hour}";
            var minute = Time.Minute > 9 ? $"{Time.Minute}" : $"0{Time.Minute}";
            var second = Time.Second > 9 ? $"{Time.Second}" : $"0{Time.Second}";
            return $"[{Time.Hour}:{Time.Minute}:{Time.Second}][{Level}][{ModuleName}]: {Message}";
        }
    }
}
