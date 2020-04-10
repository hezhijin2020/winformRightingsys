using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RightingSys.WinForm.BLL
{
    public class LoginLog
    {
        DAL.LoginLog dal = new DAL.LoginLog();
        public System.Data.DataTable Query()
        {
            return dal.Query("");
        }
        public System.Data.DataTable Query(string where)
        {
            return dal.Query(where);
        }
        public bool Delete(DateTime LastTime)
        {
            string where = "where OpTime<'" + LastTime + "'";
            int i= dal.Delete(where);
            if (i > 0)
                return true;
            else
                return false;
        }
    }
}
