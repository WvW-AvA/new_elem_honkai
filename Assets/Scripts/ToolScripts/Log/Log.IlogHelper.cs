using System.Collections.Generic;

public static partial class Log
{
    /// <summary>
    /// 游戏框架日志辅助器接口。
    /// </summary>
    public interface ILogHelper
    {
        void Init();
        void Release();

        /// <summary>
        /// 记录日志。
        /// </summary>
        /// <param name="level">游戏框架日志等级。</param>
        /// <param name="message">日志内容。</param>
        void Log(LogLevel level, object message);

        void UILogDataUpdate(string key, object value);
        void UILogDataRemove(string key);
        void UILog(string value);
        void ConsoleLog(LogLevel level, object message);

        void ConsoleWriteLine(string value);
    }
}
