using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Api.Core.Logger
{
    public class LogWriter
    {

        #region single
        private static LogWriter _instance = null;
        private static object _lock = new object();
        private LogWriter() { }
        public static LogWriter Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new LogWriter();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        private string temp = AppDomain.CurrentDomain.BaseDirectory;
        public void AddLog(string value)
        {
            if (Directory.Exists(Path.Combine(temp, @"log\")) == false)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(temp, @"log\"));
                directoryInfo.Create();
            }
            using (StreamWriter sw = File.AppendText(Path.Combine(temp, $"log\\{DateTime.Now.ToString("yyyy-MM-dd")}.log")))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + value);
            }
        }

    }
}