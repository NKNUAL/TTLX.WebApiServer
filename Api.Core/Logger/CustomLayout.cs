using log4net.Core;
using log4net.Layout;
using log4net.Layout.Pattern;
using System.IO;

namespace Api.Core.Logger
{
    public class CustomLayout : PatternLayout
    {
        public CustomLayout()
        {
            this.AddConverter("MemberID", typeof(MemberIDPatternConverter));
            this.AddConverter("MethodName", typeof(MethodNamePatternConverter));
            this.AddConverter("LogMessage", typeof(LogMessagePatternConverter));
        }
    }
    internal sealed class MemberIDPatternConverter : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            if (loggingEvent.MessageObject is AppOpLog log)
                writer.Write(log.MemberID);
        }
    }

    internal sealed class MethodNamePatternConverter : PatternLayoutConverter
    {

        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            if (loggingEvent.MessageObject is AppOpLog log)
                writer.Write(log.MethodName);
        }
    }

    internal sealed class LogMessagePatternConverter : PatternLayoutConverter
    {

        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            if (loggingEvent.MessageObject is AppOpLog log)
                writer.Write(log.LogMessage);
        }
    }

    public class AppOpLog
    {
        public string MemberID { get; set; }
        public string MethodName { get; set; }
        public string LogMessage { get; set; }
    }
}