using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.Fee.BizLogic
{   
    /// <summary>
    /// [功能描述: 非药品组套SOC业务层，从核心版本独立出来，只对SOC层有效]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public class UndrugGroup : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 取包含此项目的组套明细信息
        /// </summary>
        /// <param name="pcode">组套编码</param>
        /// <param name="pname">组套名称</param>
        /// <param name="listzt">结果集</param>
        /// <returns>1,成功; -1,失败</returns>
        public ArrayList QueryUndrugGroupDetail(string packageCode)
        {
            string strsql = FS.SOC.HISFC.Fee.Data.AbstractUndrugGroup.Current.SelectAll + FS.SOC.HISFC.Fee.Data.AbstractUndrugGroup.Current.WhereByID;

            try
            {
                strsql = String.Format(strsql, packageCode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }

            if (this.ExecQuery(strsql) == -1)
            {
                this.WriteErr();
                return null;
            }
            try
            {
                ArrayList al = new ArrayList();
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Fee.Item.UndrugComb zt = new FS.HISFC.Models.Fee.Item.UndrugComb();
                    zt.Package.ID = this.Reader.IsDBNull(0) ? "" : this.Reader.GetString(0);
                    zt.Package.Name = this.Reader.IsDBNull(1) ? "" : this.Reader.GetString(1);
                    zt.ID = this.Reader.IsDBNull(2) ? "" : this.Reader.GetString(2);
                    zt.Name = this.Reader.IsDBNull(3) ? "" : this.Reader.GetString(3);
                    zt.SortID = this.Reader.IsDBNull(4) ? 0 : Convert.ToInt32(this.Reader.GetDecimal(4));
                    zt.Qty = this.Reader.IsDBNull(5) ? 0 : this.Reader.GetDecimal(5);
                    zt.ValidState = this.Reader.IsDBNull(6) ? "" : this.Reader.GetString(6);
                    zt.SpellCode = this.Reader.IsDBNull(7) ? "" : this.Reader.GetString(7);
                    zt.WBCode = this.Reader.IsDBNull(8) ? "" : this.Reader.GetString(8);
                    zt.UserCode = this.Reader.IsDBNull(9) ? "" : this.Reader.GetString(9);
                    zt.Memo = "11";//这是一个标志位,如果为11则,再操作时用update,否则用insert;
                    zt.Price = this.Reader.IsDBNull(10) ? 0 : Convert.ToDecimal(this.Reader.GetDecimal(10));
                    zt.ChildPrice = this.Reader.IsDBNull(11) ? 0 : Convert.ToDecimal(this.Reader.GetDecimal(11));
                    zt.SpecialPrice = this.Reader.IsDBNull(12) ? 0 : Convert.ToDecimal(this.Reader.GetDecimal(12));
                    zt.Oper.Name = this.Reader.IsDBNull(13) ? "" : this.Reader.GetString(13);
                    zt.Oper.OperTime = this.Reader.IsDBNull(14) ? DateTime.MinValue : this.Reader.GetDateTime(14);

                    if (Reader.FieldCount > 15)
                    {
                        zt.ItemRate = Convert.ToDecimal(Reader[15]);
                    }
                    al.Add(zt);
                }

                return al;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                if (this.Reader != null && this.Reader.IsClosed == false)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// 取包含此项目的组套信息
        /// </summary>
        /// <param name="detailCode"></param>
        /// <returns></returns>
        public ArrayList QueryUndrugGroupInfo(string detailCode)
        {
            string strsql = FS.SOC.HISFC.Fee.Data.AbstractUndrugGroup.Current.SelectAll + FS.SOC.HISFC.Fee.Data.AbstractUndrugGroup.Current.WhereByDetailID;

            try
            {
                strsql = String.Format(strsql, detailCode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }

            if (this.ExecQuery(strsql) == -1)
            {
                this.WriteErr();
                return null;
            }
            try
            {
                ArrayList al = new ArrayList();
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Fee.Item.UndrugComb zt = new FS.HISFC.Models.Fee.Item.UndrugComb();
                    zt.Package.ID = this.Reader.IsDBNull(0) ? "" : this.Reader.GetString(0);
                    zt.Package.Name = this.Reader.IsDBNull(1) ? "" : this.Reader.GetString(1);
                    zt.ID = this.Reader.IsDBNull(2) ? "" : this.Reader.GetString(2);
                    zt.Name = this.Reader.IsDBNull(3) ? "" : this.Reader.GetString(3);
                    zt.SortID = this.Reader.IsDBNull(4) ? 0 : Convert.ToInt32(this.Reader.GetDecimal(4));
                    zt.Qty = this.Reader.IsDBNull(5) ? 0 : this.Reader.GetDecimal(5);
                    zt.ValidState = this.Reader.IsDBNull(6) ? "" : this.Reader.GetString(6);
                    zt.SpellCode = this.Reader.IsDBNull(7) ? "" : this.Reader.GetString(7);
                    zt.WBCode = this.Reader.IsDBNull(8) ? "" : this.Reader.GetString(8);
                    zt.UserCode = this.Reader.IsDBNull(9) ? "" : this.Reader.GetString(9);
                    zt.Memo = "11";//这是一个标志位,如果为11则,再操作时用update,否则用insert;
                    zt.Price = this.Reader.IsDBNull(10) ? 0 : Convert.ToDecimal(this.Reader.GetDecimal(10));
                    zt.ChildPrice = this.Reader.IsDBNull(11) ? 0 : Convert.ToDecimal(this.Reader.GetDecimal(11));
                    zt.SpecialPrice = this.Reader.IsDBNull(12) ? 0 : Convert.ToDecimal(this.Reader.GetDecimal(12));
                    zt.Oper.Name = this.Reader.IsDBNull(13) ? "" : this.Reader.GetString(13);
                    zt.Oper.OperTime = this.Reader.IsDBNull(14) ? DateTime.MinValue : this.Reader.GetDateTime(14);
                    al.Add(zt);
                }

                return al;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                if (this.Reader != null && this.Reader.IsClosed == false)
                {
                    this.Reader.Close();
                }
            }
        }
    }
}
