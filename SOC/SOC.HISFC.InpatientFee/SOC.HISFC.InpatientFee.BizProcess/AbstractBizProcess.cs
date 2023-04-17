using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neusoft.SOC.HISFC.InpatientFee.BizProcess
{
    /// <summary>
    /// [功能描述: 基类逻辑业务类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public abstract class AbstractBizProcess
    {
        private System.Data.IDbTransaction outTrans = null;
        private Neusoft.FrameWork.Management.Database database = null;

        protected virtual System.Data.IDbTransaction Trans
        {
            get
            {
                return this.outTrans ?? Neusoft.FrameWork.Management.PublicTrans.Trans;
            }
        }

        /// <summary>
        /// 设置事务，如果一个manager就不需要重新写
        /// 如果涉及多个manager，为了将事务传到，必须override
        /// 然后设置事务。
        /// </summary>
        /// <param name="trans"></param>
        public virtual void SetTrans(System.Data.IDbTransaction trans)
        {
            this.outTrans = trans;
        }

        protected virtual void SetDB(Neusoft.FrameWork.Management.Database database)
        {
            this.database = database;
            database.SetTrans(this.outTrans ?? Neusoft.FrameWork.Management.PublicTrans.Trans);
        }

        protected virtual void BeginTransaction()
        {
            if (this.outTrans == null)
            {
                Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            }
        }

        protected virtual void Commit()
        {
            if (this.outTrans == null)
            {
                Neusoft.FrameWork.Management.PublicTrans.Commit();
            }
        }

        protected virtual void RollBack()
        {
            if (this.outTrans == null)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
            }
        }

        protected string err = string.Empty;

        public string Err
        {
            get
            {
                if (err.Equals(string.Empty))
                {
                    return database == null ? err : database.Err;
                }
                return err;
            }
        }

    }
}
