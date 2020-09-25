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
    public class RenewOrderHelper
    {
        #region 单例
        private RenewOrderHelper() { }
        private static RenewOrderHelper _instance = null;
        private static readonly object objlock = new object();

        public static RenewOrderHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (objlock)
                    {
                        if (_instance == null)
                            _instance = new RenewOrderHelper();
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
                DbServer0905Context db0905 = DbContextFactory.GetDbServer0905Context();
                for (int i = orderNos.Count - 1; i >= 0; i--)
                {
                    string orderNo = orderNos[i];
                    var statu = AliPayHelper.Instance.QueryOrder(orderNo);
                    if (statu == AliPayStatu.Paid || statu == AliPayStatu.Closed)
                    {
                        var order = db0905.RenewRecord.FirstOrDefault(o => o.RenewNo == orderNo);
                        if (order != null)
                        {
                            order.RenewStatu = statu == AliPayStatu.Paid ?
                                (int)OrderStatuDictionary.Finished : (int)OrderStatuDictionary.Closed;
                        }
                        db0905.SaveChanges();
                        orderNos.RemoveAt(i);
                    }
                }
            }
        }
    }
}
