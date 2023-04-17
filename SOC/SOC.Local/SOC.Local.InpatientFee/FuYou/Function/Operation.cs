using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace FS.SOC.Local.InpatientFee.FuYou.Function
{
    class Operation : FS.FrameWork.Management.Database
    {
        public Operation()
        { 
        }

        /// <summary>
        /// 查询患者费用
        /// </summary>
        /// <param name="patientNo"></param>
        /// <param name="execDeptCode"></param>
        /// <returns></returns>
        public ArrayList GetPatientOprationFee(string patientNo, string execDeptCode)
        { 
            ArrayList al = new ArrayList();
            
            string strSql = string.Empty;
            try
            {
                if (this.Sql.GetSql("Fee.GetAllItemsForOperationTotal", ref strSql) == -1)
                {
                    this.Err = "未找到索引为Fee.GetAllItemsForOperationTotal的sql语句";
                    return null;
                }
                strSql = string.Format(strSql, patientNo, execDeptCode);
                if (this.ExecQuery(strSql) == -1)
                {
                    this.Err = "查询患者费用出错";
                    return null;
                }

                FS.FrameWork.Models.NeuObject obj = null;

                while (this.Reader.Read())
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.Reader[0].ToString();
                    obj.Name = this.Reader[1].ToString();
                    obj.Memo = this.Reader[2].ToString();
                    obj.User01 = this.Reader[3].ToString();
                    obj.User02 = this.Reader[4].ToString();
                    al.Add(obj);
                }
            }
            catch(Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return null;
            }
            finally
            {
                if (!this.Reader.IsClosed && this.Reader != null)
                {
                    this.Reader.Close();
                }
            }
            return al;
        }

        /// <summary>
        /// 查询患者费用明细
        /// </summary>
        /// <param name="patientNo"></param>
        /// <param name="execDeptCode"></param>
        /// <param name="feeDate"></param>
        /// <param name="ds"></param>
        public void GetPatientOprationFeeDetail(string patientNo, string execDeptCode, string feeDate, ref DataSet ds)
        {
            string strSql = string.Empty;

            try
            {
                if (this.Sql.GetSql("Fee.GetAllItemsForOperationDetail", ref strSql) == -1)
                {
                    this.Err = "未找到索引为Fee.GetAllItemsForOperationDetail的sql语句";
                    return;
                }

                strSql = string.Format(strSql, patientNo, feeDate, execDeptCode);

                if (this.ExecQuery(strSql, ref ds) == -1)
                {
                    this.Err = "查询患者费用出错";
                    return;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return;
            }
            finally
            {
                if (!this.Reader.IsClosed && this.Reader != null)
                {
                    this.Reader.Close();
                }
            }
        }
    }
}
