using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Factory
{
    public abstract class FactoryBase
    {
        protected FS.FrameWork.Management.Database db = null;
        protected string err;
        public string Err
        {
            get
            {
                if (db != null)
                    return db.Err;
                else
                    return err;
            }
            set
            {
                err = value;
            }
        }

        protected string errCode = "";
        public string ErrCode 
        {
            get
            {
                if (db != null)
                    return db.ErrCode;
                else
                    return errCode;
            }
            set
            {
                errCode = value;
            }
        }

        protected int dberrCode = 0;
        public int DBErrorCode 
        {
            get
            {
                if (db != null)
                    return db.DBErrCode;
                else
                    return dberrCode;
            }
            set
            {
                dberrCode = value;
            }
        }

        protected virtual void SetDB(FS.FrameWork.Management.Database database)
        {
            db = database;
        }
    }
}
