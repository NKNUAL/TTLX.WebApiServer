using Api.DAL.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Api.DAL
{
    public class DbContextFactory
    {

        public static DbMonitorSystemContext GetDbMonitorSystemContext()
        {
            DbMonitorSystemContext dbContext = (DbMonitorSystemContext)CallContext.GetData("DbMonitorSystem");
            if (dbContext == null)
            {
                dbContext = new DbMonitorSystemContext();
                CallContext.SetData("DbMonitorSystem", dbContext);
            }
            return dbContext;
        }

        public static DbServer0905Context GetDbServer0905Context()
        {
            DbServer0905Context dbContext = (DbServer0905Context)CallContext.GetData("DbServer0905");
            if (dbContext == null)
            {
                dbContext = new DbServer0905Context();
                CallContext.SetData("DbServer0905", dbContext);
            }
            return dbContext;
        }

        public static DbUserAdminContext GetDbUserAdmin()
        {
            DbUserAdminContext dbContext = (DbUserAdminContext)CallContext.GetData("DbUserAdmin");
            if (dbContext == null)
            {
                dbContext = new DbUserAdminContext();
                CallContext.SetData("DbUserAdmin", dbContext);
            }
            return dbContext;
        }

        public static DbUsersContext GetDbUsers()
        {
            DbUsersContext dbContext = (DbUsersContext)CallContext.GetData("DbUsers");
            if (dbContext == null)
            {
                dbContext = new DbUsersContext();
                CallContext.SetData("DbUsers", dbContext);
            }
            return dbContext;
        }

        public static DbShareContext GetDbShare()
        {
            DbShareContext dbContext = (DbShareContext)CallContext.GetData("DbShare");
            if (dbContext == null)
            {
                dbContext = new DbShareContext();
                CallContext.SetData("DbShare", dbContext);
            }
            return dbContext;
        }

        public static DbSystem3Context GetDbSystem3()
        {
            DbSystem3Context dbContext = (DbSystem3Context)CallContext.GetData("DbSystem3");
            if (dbContext == null)
            {
                dbContext = new DbSystem3Context();
                CallContext.SetData("DbSystem3", dbContext);
            }
            return dbContext;
        }
    }
}
