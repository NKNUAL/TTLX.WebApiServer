using Api.Queue.Impl;
using Api.Queue.QueueModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Queue
{
    public class GlabolDataExe
    {

        #region single
        private static GlabolDataExe _instance = null;
        private static readonly object objlock = new object();
        private GlabolDataExe()
        {

        }
        public static GlabolDataExe Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (objlock)
                    {
                        if (_instance == null)
                        {
                            try
                            {
                                _instance = new GlabolDataExe();
                            }
                            catch { }
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        /// <summary>
        /// 全局的队列数据
        /// </summary>
        private Dictionary<QueueDataType, IQueueData> Datas = new Dictionary<QueueDataType, IQueueData>();

        /// <summary>
        /// 处理队列数据
        /// </summary>
        public void Process()
        {
            foreach (var data in Datas)
            {
                data.Value.ToDb();
            }
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="t"></param>
        public void AddData<T>(QueueDataType key, T t) where T : class
        {
            if (!Datas.ContainsKey(key))
            {
                Datas.Add(key, GetImpl(key));
            }
            Datas[key].Enqueue(t);
        }

        private IQueueData GetImpl(QueueDataType type)
        {
            switch (type)
            {
                case QueueDataType.ErrorQuestions:
                    return new ErrorQuestionQueueData();
                case QueueDataType.Upload:
                    return new PhoneUserQueueData();
                case QueueDataType.SharePaperUpload:
                    return new PaperQuestionQueueData();
                case QueueDataType.MockTestPaper:
                    return new MockTestPaperQueueData();
                default:
                    return null;
            }
        }
    }
}
