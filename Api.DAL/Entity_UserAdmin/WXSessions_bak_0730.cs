namespace Api.DAL.Entity_UserAdmin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WXSessions_bak_0730
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string LexueID { get; set; }

        [StringLength(500)]
        public string WXOpenID { get; set; }

        [StringLength(500)]
        public string WXSession_Key { get; set; }

        [StringLength(500)]
        public string LXSessionID { get; set; }

        [StringLength(100)]
        public string BindTime { get; set; }

        [StringLength(500)]
        public string WXAvatarUrl { get; set; }
    }
}
