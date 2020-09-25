using Api.Core.AliPay;
using Api.Core.Enum;
using Api.DAL;
using Api.DAL.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL.Helper
{
    public class OrderHelper
    {
        #region 单例
        private OrderHelper() { }
        private static OrderHelper _instance = null;
        private static readonly object objlock = new object();

        public static OrderHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (objlock)
                    {
                        if (_instance == null)
                            _instance = new OrderHelper();
                    }
                }
                return _instance;
            }
        }
        #endregion

        public List<string> orderNos = new List<string>();

        public void Process()
        {
            if (orderNos.Count > 0)
            {
                DbShareContext dbShare = DbContextFactory.GetDbShare();
                for (int i = orderNos.Count - 1; i >= 0; i--)
                {
                    string orderNo = orderNos.ElementAt(i);
                    var statu = AliPayHelper.Instance.QueryOrder(orderNo);
                    if (statu == AliPayStatu.Paid || statu == AliPayStatu.Closed)
                    {
                        var order = dbShare.OrderRecord.FirstOrDefault(o => o.OrderNo == orderNo);
                        if (order != null)
                        {
                            order.OrderStatu = statu == AliPayStatu.Paid ?
                                (int)OrderStatuDictionary.Finished : (int)OrderStatuDictionary.Closed;
                        }
                        dbShare.SaveChanges();
                        orderNos.RemoveAt(i);
                    }
                }
            }
        }
    }
}
