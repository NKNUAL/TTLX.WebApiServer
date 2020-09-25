using Api.Core.Logger;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Redis
{
    public class RedisConnection
    {
        /// <summary>
        /// key的前缀
        /// </summary>
        public static readonly string SysCustomKey = ConfigurationManager.AppSettings["redisKeyCustom"] ?? "";
        private static readonly string RedisConnectionString = ConfigurationManager.AppSettings["RedisConnectionString"];

        private static readonly object Locker = new object();
        private static ConnectionMultiplexer _instance;
        private static readonly ConcurrentDictionary<string, ConnectionMultiplexer> ConnectionCache = new ConcurrentDictionary<string, ConnectionMultiplexer>();
        /// <summary>
        /// 单例获取
        /// </summary>
        public static ConnectionMultiplexer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Locker)
                    {
                        if (_instance == null || !_instance.IsConnected)
                        {
                            _instance = GetManager();
                        }
                    }
                }
                return _instance;
            }
        }


        public static ConnectionMultiplexer GetConnectionMultiplexer(string connectionString)
        {
            if (!ConnectionCache.ContainsKey(connectionString))
            {
                ConnectionCache[connectionString] = GetManager(connectionString);
            }
            return ConnectionCache[connectionString];
        }

        private static ConnectionMultiplexer GetManager(string connectionString = null)
        {
            connectionString = connectionString ?? ConfigTools.DESDecrypt(RedisConnectionString);
            var connect = ConnectionMultiplexer.Connect(connectionString);

            connect.ErrorMessage += MuxerErrorMessage;

            return connect;
        }

        /// <summary>
        /// 发生错误时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerErrorMessage(object sender, RedisErrorEventArgs e)
        {
            LogContent.Instance.WriteLog($"redis发生错误：【{e.Message}】", Log4NetLevel.Error);
        }
    }
}
