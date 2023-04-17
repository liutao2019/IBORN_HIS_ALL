using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Integrate
{
    public abstract class IntegrateBase
    {
        public void SetDB(FS.FrameWork.Management.Database db)
        {
            this.db = db;
        }
        private FS.FrameWork.Management.Database db = null;
        protected System.Data.IDbTransaction trans = null;

        /// <summary>
        /// �����������һ��manager�Ͳ���Ҫ����д
        /// ����漰���manager��Ϊ�˽����񴫵�������override
        /// Ȼ����������
        /// </summary>
        /// <param name="trans"></param>
        public virtual void SetTrans(System.Data.IDbTransaction trans)
        {
            if (db != null) this.db.SetTrans(trans);
            this.trans = trans;
        }
        private string err = string.Empty;
        public string Err
        {
            get
            {
                if (db == null) return err;
                return this.db.Err;
            }
            set
            {
                if (db == null)
                    err = value;
                else
                    this.db.Err = value;

            }
        }
        public string ErrCode
        {
            get
            {
                if (db == null) return "";
                return this.db.ErrCode;
            }
            set
            {
                this.db.ErrCode = value;
            }
        }
        public int DBErrCode
        {
            get
            {
                if (db == null) return 0;
                return this.db.DBErrCode;
            }
            set
            {
                this.db.DBErrCode = value;
            }
        }

        /// <summary>
        /// ���ϵͳʱ��
        /// </summary>
        /// <returns></returns>
        public DateTime GetDateTimeFromSysDateTime()
        {
            return db.GetDateTimeFromSysDateTime();
        }

        /// <summary>
        /// ��ǰ����Ա
        /// </summary>
        public FS.FrameWork.Models.NeuObject Operator
        {
            get
            {
                return db.Operator;
            }
        }
    }
}
