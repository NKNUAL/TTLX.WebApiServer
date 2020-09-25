using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.DAL
{
    public class DALBaseHelper
    {

        /// <summary>
        /// 判断字段是否存在
        /// </summary>
        /// <param name="db"></param>
        /// <param name="table_name"></param>
        /// <param name="column_name"></param>
        /// <returns></returns>
        public bool CheckColumnExist(DbContext db, string table_name, string column_name)
        {
            List<string> columns = db.Database.SqlQuery<string>("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME=@table_name", new SqlParameter("@table_name", table_name)).ToList();
            return columns.Contains(column_name);
        }

        /// <summary>
        /// 判断表是否存在
        /// </summary>
        /// <param name="db"></param>
        /// <param name="table_name"></param>
        /// <param name="column_name"></param>
        /// <returns></returns>
        public bool CheckTableExist(DbContext db, string table_name)
        {
            List<string> name = db.Database.SqlQuery<string>("select name from sysObjects where Id=OBJECT_ID(N'@table_name') and xtype='U'", new SqlParameter("@table_name", table_name)).ToList();
            return name.Count > 0;
        }


        /// <summary>
        /// 判断视图是否存在
        /// </summary>
        /// <param name="db"></param>
        /// <param name="table_name"></param>
        /// <param name="column_name"></param>
        /// <returns></returns>
        public bool CheckViewExist(DbContext db, string view_name)
        {
            List<string> name = db.Database.SqlQuery<string>("select table_name from information_schema.views where table_name = @view_name", new SqlParameter("@view_name", view_name)).ToList();
            return name.Count > 0;
        }

    }
}
