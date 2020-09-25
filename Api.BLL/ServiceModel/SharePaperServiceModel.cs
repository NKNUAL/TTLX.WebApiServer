using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL.ServiceModel
{
    public class SharePaperTotalServiceModel
    {
        public int TotalCount { get; set; }
        public List<SharePaperServiceModel> PaperData { get; set; }
    }


    public class SharePaperServiceModel
    {
        public string PaperID { get; set; }
        public string PaperName { get; set; }//试卷名称
        public string PaperDesc { get; set; }//试卷描述
        public string PaperCreateDate { get; set; }//试卷创建时间
        public string CreateUserName { get; set; }//出题人
        public int PaperQueCount { get; set; }//全部试题数
        public double PaperPrice { get; set; }//试卷价格
        public int? PurchaseNumber { get; set; }//采购人数
        public double? CommentLevel { get; set; }//好评度
        public bool IsBought { get; set; }//是否购买
        public int CheckStatu { get; set; }//审核状态
        public int PaperStatu { get; set; }//是否上架
        public string PaperVersion { get; set; }//试卷版本
    }
}
