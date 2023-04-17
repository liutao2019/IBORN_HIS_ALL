using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizLogic.Order
{
    /// <summary>
    /// ������뵥
    /// </summary>
    public class CheckSlip : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public CheckSlip()
        {
        }

        public int InsertCheckSlip(FS.HISFC.Models.Order.CheckSlip checkslip)
        {
            string sql ="";
            if(this.Sql.GetCommonSql("Order.CheckSlip.Insert",ref sql)==-1)
            {
                this.Err=this.Sql.Err;
                return -1;
            }
            try
            {
                checkslip.CheckSlipNo = this.GetSequence("Order.CheckSlip.Seq");
                sql = string.Format(sql, checkslip.CheckSlipNo, checkslip.CardNo, checkslip.InpatientNO, checkslip.Doct_dept, checkslip.ZsInfo,
                    checkslip.YxtzInfo, checkslip.YxsyInfo, checkslip.DiagName, checkslip.ItemNote, checkslip.EmcFlag, checkslip.MoNote, checkslip.ExtFlag1,
                    checkslip.ExtFlag2, checkslip.ExtFlag3, checkslip.ExtFlag4,checkslip.ApplyDate,checkslip.OperDate);
                return this.ExecNoQuery(sql);                
            }
            catch(Exception e)
            {
                this.Err = "���������뵥����![Order.CheckSlip.Insert]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        public int UpdateCheckSlip(FS.HISFC.Models.Order.CheckSlip checkslip)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Order.CheckSlip.Update", ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                sql = string.Format(sql, checkslip.CheckSlipNo, checkslip.Doct_dept, checkslip.ZsInfo, checkslip.YxtzInfo, checkslip.YxsyInfo,
                    checkslip.DiagName, checkslip.ItemNote, checkslip.EmcFlag, checkslip.MoNote, checkslip.ExtFlag1,checkslip.OperDate);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "���¼�����뵥����![Order.CheckSlip.Update]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        //private string[] GetCheckSlip(HISFC.Object.Order.CheckSlip checkslip)
        //{
        //    string[] checkslipObj = new string[]
        //    {
        //        checkslip.CheckSlipNo,
        //        checkslip.checkslipObj,
        //        checkslip.InpatientNO, 
        //        checkslip.Doct_dept, 
        //        checkslip.ZsInfo,
        //        checkslip.YxtzInfo, 
        //        checkslip.YxsyInfo,
        //        checkslip.DiagName, 
        //        checkslip.ItemNote, 
        //        checkslip.EmcFlag,
        //        checkslip.MoNote, 
        //        checkslip.ExtFlag1,
        //        checkslip.ExtFlag2, 
        //        checkslip.ExtFlag3, 
        //        checkslip.ExtFlag4,
        //        checkslip.ApplyDate,
        //        checkslip.OperDate

        //    };
        //    return checkslip;
        //}

        public List<FS.HISFC.Models.Order.CheckSlip> QuerySlip(string checkSlipNo)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Order.CheckSlip.Select", ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            sql = string.Format(sql, checkSlipNo);
            return MyQuderSlip(sql);
        }
        public List<FS.HISFC.Models.Order.CheckSlip> QueryRecientSlip(string InpatientNo)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Order.CheckSlip.QueryRecientSlip", ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            sql = string.Format(sql, InpatientNo);
            return MyQuderSlip(sql);
        }

        public List<FS.HISFC.Models.Order.CheckSlip> MyQuderSlip(string sql)
        {
            List<FS.HISFC.Models.Order.CheckSlip> list = new List<FS.HISFC.Models.Order.CheckSlip>();
            try
            {
                if (this.ExecQuery(sql) == -1) return null;
                while (Reader.Read())
                {
                    FS.HISFC.Models.Order.CheckSlip checkslip = new FS.HISFC.Models.Order.CheckSlip();
                    checkslip.CheckSlipNo = this.Reader[0].ToString();//����
                    checkslip.CardNo = this.Reader[1].ToString();//�����
                    checkslip.InpatientNO = this.Reader[2].ToString();//סԺ��
                    checkslip.Doct_dept = this.Reader[3].ToString();//�������Ҵ���
                    checkslip.ZsInfo = this.Reader[4].ToString();//����
                    checkslip.YxtzInfo = this.Reader[5].ToString();//��������
                    checkslip.YxsyInfo = this.Reader[6].ToString();//����ʵ������
                    checkslip.DiagName = this.Reader[7].ToString();//�����
                    checkslip.ItemNote = this.Reader[8].ToString();//��鲿λ 
                    checkslip.EmcFlag = this.Reader[9].ToString();//�Ƿ�Ӽ�(0��ͨ/1�Ӽ�)
                    checkslip.MoNote = this.Reader[10].ToString();//��ע
                    checkslip.ExtFlag1 = this.Reader[11].ToString();//ҽ����Ŀ
                    checkslip.ExtFlag2 = this.Reader[12].ToString();//���߿���
                    checkslip.ExtFlag3 = this.Reader[13].ToString();//���߲���
                    checkslip.ExtFlag4 = this.Reader[14].ToString();//����
                    checkslip.ApplyDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[15]);
                    checkslip.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[16]);

                    list.Add(checkslip);
                }
            }
            catch (Exception e)
            {
                this.Err = "���뵥��Ϣ����!" + e.Message;
                this.ErrCode = e.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                Reader.Close();
            }
            return list;
        }

        public int QueryByMoOrder(string moorder)
        {
            string sql = "";
            int i = -1;
            if (this.Sql.GetCommonSql("Order.MetIpmOrder.SelectByMoOrder", ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            sql = string.Format(sql, moorder);
            try
            {
                if (ExecQuery(sql) == -1)
                {
                    return -1;
                }
                while (this.Reader.Read())
                {
                    i = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0]);
                }
                Reader.Close();
            }
            catch (Exception e)
            {
                if (Reader.IsClosed == false) Reader.Close();
                this.Err = "��ҽ����ˮ�Ŷ�ȡ���뵥�ų���!" + e.Message;
                this.ErrCode = e.Message;
                this.WriteErr();
                return -1;
            }
            return i;
        }

        public string QueryDiagName(string InpatientNo)
        {
            string sql = "";
            string diagName="";
            if (this.Sql.GetCommonSql("Order.CheckSlip.QueryDiagNanme", ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return "";
            }
            sql = string.Format(sql, InpatientNo);
            try
            {
                if (ExecQuery(sql) == -1)
                {
                    return "";
                }
                while (this.Reader.Read())
                {
                    diagName = this.Reader[0].ToString();
                }
                Reader.Close();
            }
            catch (Exception e)
            {
                if (Reader.IsClosed == false) Reader.Close();
                this.Err = "��ҽ����ˮ�Ŷ�ȡ���뵥�ų���!" + e.Message;
                this.ErrCode = e.Message;
                this.WriteErr();
                return "";
            }
            return diagName;
        }

        public int Delete(string checkSlipNo)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.CheckSlip.Delete", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, checkSlipNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                this.WriteErr();
                return -1;
            }
            if (this.ExecNoQuery(strSql) <= 0) return -1;
            return 1;
        }

        public int UpdateMetIpmOrder(string moorder)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Order.MetIpmOrder.UpdateApplyNo", ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                sql = string.Format(sql, moorder);
                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "���³���![Order.MetIpmOrder.UpdateApplyNo]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        public List<FS.HISFC.Models.Order.CheckSlip> QueryPatineInfo(string InpatientNo)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Order.InmainInfo.SelectByInpatient", ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            sql = string.Format(sql, InpatientNo);
            List<FS.HISFC.Models.Order.CheckSlip> list = new List<FS.HISFC.Models.Order.CheckSlip>();
            try
            {
                if (this.ExecQuery(sql) == -1) return null;
                while (Reader.Read())
                {
                    FS.HISFC.Models.Order.CheckSlip checkslip = new FS.HISFC.Models.Order.CheckSlip();
                    checkslip.ExtFlag2 = this.Reader[0].ToString();
                    checkslip.ExtFlag3 = this.Reader[1].ToString();
                    checkslip.ExtFlag4 = this.Reader[2].ToString();

                    list.Add(checkslip);
                }
                Reader.Close();
            }
            catch (Exception e)
            {
                if (Reader.IsClosed == false) Reader.Close();
                this.Err = "��ȡ���߻�����Ϣ����!" + e.Message;
                this.ErrCode = e.Message;
                this.WriteErr();
                return null;
            }
            return list;
            
        }
    }
        
}
