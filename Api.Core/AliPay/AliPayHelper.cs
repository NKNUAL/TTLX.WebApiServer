using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using Aop.Api.Response;
using Api.Core.Enum;
using Api.Core.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.AliPay
{
    public class AliPayHelper
    {
        #region 单例
        private AliPayHelper() { }
        private static AliPayHelper _instance = null;
        private static readonly object objlock = new object();

        public static AliPayHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (objlock)
                    {
                        if (_instance == null)
                            _instance = new AliPayHelper();
                    }
                }
                return _instance;
            }
        }
        #endregion

        readonly string privateKey = Properties.Resources.private_key;
        readonly string publicKey = Properties.Resources.public_key;
        readonly string serverUrl = "https://openapi.alipay.com/gateway.do";
        readonly string appId = "2019070265724452";

        /// <summary>
        /// 创建交易
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="total_amount"></param>
        /// <param name="subject"></param>
        /// <param name="paperName"></param>
        /// <param name="paperId"></param>
        /// <returns></returns>
        public string CreateOrder(string orderNo, double total_amount, string subject, string goods_name, string goods_id)
        {
            IAopClient client =
                new DefaultAopClient(serverUrl, appId, privateKey, "json", "1.0", "RSA2", publicKey, "GBK", false);
            AlipayTradePrecreateRequest request = new AlipayTradePrecreateRequest();
            request.BizContent = "{" +
            "\"out_trade_no\":\"" + orderNo + "\"," +
            "\"seller_id\":\"\"," +
            "\"total_amount\":" + total_amount + "," +
            "\"discountable_amount\":0," +
            "\"subject\":\"" + subject + "\"," +
            "      \"goods_detail\":[{" +
            "        \"goods_id\":\"" + goods_id + "\"," +
            "\"goods_name\":\"" + goods_name + "\"," +
            "\"quantity\":1," +
            "\"price\":" + total_amount + "," +
            "\"goods_category\":\"\"," +
            "\"categories_tree\":\"\"," +
            "\"body\":\"" + subject + "\"," +
            "\"show_url\":\"\"" +
            "        }]," +
            "\"body\":\"" + subject + "\"," +
            "\"product_code\":\"FACE_TO_FACE_PAYMENT\"," +
            "\"operator_id\":\"\"," +
            "\"store_id\":\"\"," +
            "\"disable_pay_channels\":\"\"," +
            "\"enable_pay_channels\":\"\"," +
            "\"terminal_id\":\"\"," +
            "\"extend_params\":{" +
            "\"sys_service_provider_id\":\"\"," +
            "\"card_type\":\"\"" +
            "    }," +
            "\"timeout_express\":\"30m\"," +
            "\"settle_info\":{" +
            "        \"settle_detail_infos\":[{" +
            "          \"trans_in_type\":\"loginName\"," +
            "\"trans_in\":\"442500321@qq.com\"," +
            "\"summary_dimension\":\"\"," +
            "\"settle_entity_id\":\"\"," +
            "\"settle_entity_type\":\"\"," +
            "\"amount\":" + total_amount + "" +
            "          }]," +
            "\"settle_period_time\":\"7d\"" +
            "    }," +
            "\"merchant_order_no\":\"\"," +
            "\"business_params\":{" +
            "\"campus_card\":\"\"," +
            "\"card_type\":\"\"," +
            "\"actual_order_time\":\"\"" +
            "    }," +
            "\"qr_code_timeout_express\":\"30m\"" +
            "  }";
            AlipayTradePrecreateResponse response = client.Execute(request);

            if (!response.IsError)
            {
                return response.QrCode;
            }
            else
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = "alipay",
                    LogMessage = $"创建支付宝交易失败：【{Newtonsoft.Json.JsonConvert.SerializeObject(response)}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);

                return null;
            }
        }

        /// <summary>
        /// 关闭交易
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public bool CloseOrder(string orderNo)
        {
            IAopClient client =
                new DefaultAopClient(serverUrl, appId, privateKey, "json", "1.0", "RSA2", publicKey, "GBK", false);
            AlipayTradeCloseRequest request = new AlipayTradeCloseRequest();
            request.BizContent = "{" +
            "\"trade_no\":\"\"," +
            "\"out_trade_no\":\"" + orderNo + "\"," +
            "\"operator_id\":\"\"" +
            "  }";
            AlipayTradeCloseResponse response = client.Execute(request);
            if (!response.IsError)
            {
                return true;
            }
            else
            {
                LogContent.Instance.WriteLog(new AppOpLog
                {
                    MemberID = "alipay",
                    LogMessage = $"关闭支付宝交易失败：【{Newtonsoft.Json.JsonConvert.SerializeObject(response)}】",
                    MethodName = $"[{MethodBase.GetCurrentMethod().DeclaringType.Name}:{MethodBase.GetCurrentMethod().Name}]"
                }, Log4NetLevel.Error);
                return false;
            }
        }

        /// <summary>
        /// 交易查询
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public AliPayStatu QueryOrder(string orderNo)
        {
            IAopClient client =
                new DefaultAopClient(serverUrl, appId, privateKey, "json", "1.0", "RSA2", publicKey, "GBK", false);
            AlipayTradeQueryRequest request = new AlipayTradeQueryRequest();
            request.BizContent = "{" +
            "\"out_trade_no\":\"" + orderNo + "\"," +
            "\"trade_no\":\"\"," +
            "\"org_pid\":\"\"," +
            "      \"query_options\":[" +
            "        \"trade_settle_info\"" +
            "      ]" +
            "  }";
            AlipayTradeQueryResponse response = client.Execute(request);

            if (!response.IsError)
            {
                if (response.TradeStatus == null)
                    return AliPayStatu.Nono;
                if (response.TradeStatus.ToUpper().Equals("TRADE_SUCCESS"))
                    return AliPayStatu.Paid;
                if (response.TradeStatus.ToUpper().Equals("TRADE_CLOSED"))
                    return AliPayStatu.Closed;
                if (response.TradeStatus.ToUpper().Equals("WAIT_BUYER_PAY"))
                    return AliPayStatu.WaitingPay;
            }
            return AliPayStatu.Nono;

        }

    }

}
