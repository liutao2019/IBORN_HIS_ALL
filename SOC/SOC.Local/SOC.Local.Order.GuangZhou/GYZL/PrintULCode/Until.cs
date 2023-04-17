using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;

namespace FS.SOC.Local.Order.GuangZhou.Gyzl.PrintULCode
{
    public class Unitl : FS.FrameWork.Management.Database
    {
        #region 查询

        /// <summary>
        /// 根据住院护士站编码查询住院检验
        /// </summary>
        /// <param name="nurseCellCode"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ArrayList QueryPatientWithULItemByNurseCellCodeAndDate(string nurseCellCode, DateTime start, DateTime end)
        {
            string sqlStr = @"select t.card_no,
                                     t.name,
                                     i.recipe_no,
                                     i.item_name,
                                     e.lab_barcode,
                                     i.noback_num
                                from fin_ipr_inmaininfo t, met_ipm_execundrug e, fin_ipb_itemlist i, met_ipm_order o
                               where e.class_code = 'UL'
                                 and e.mo_order = i.mo_order
                                 and i.noback_num = i.qty
                                 and t.inpatient_no = e.inpatient_no
                                 and e.mo_order = o.mo_order
                                 and t.nurse_cell_code = '{0}'
                                 and o.mo_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                 and o.mo_date <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                                 and e.valid_flag = '1'";
            try
            {
                sqlStr = string.Format(sqlStr, nurseCellCode, start, end);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }

            if (this.ExecQuery(sqlStr) == -1)
            {
                return null;
            }

            ArrayList al = null;
            try
            {
                al = new ArrayList();
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                    feeItemList.Patient.PID.CardNO = this.Reader[0].ToString();         //就诊卡号
                    feeItemList.Memo = this.Reader[1].ToString();                       //Memo暂时存姓名
                    feeItemList.RecipeNO = this.Reader[2].ToString();                   //处方号
                    feeItemList.Item.Name = this.Reader[3].ToString();                  //项目名
                    feeItemList.Order.ApplyNo = this.Reader[4].ToString();              //ApplyNo暂时存条码号
                    feeItemList.NoBackQty = decimal.Parse(this.Reader[5].ToString());   //可退数量(是否已经接收,接收为0)
                    al.Add(feeItemList);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
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
            return al;
        }

        /// <summary>
        /// 根据就诊卡号查询住院患者检验条码号
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ArrayList QueryLisBarCodeByCardNoAndDate(string cardNo, DateTime start, DateTime end)
        {
            string sqlStr = @"select e.lab_barcode
                                from fin_ipr_inmaininfo t, met_ipm_execundrug e
                               where t.inpatient_no = e.inpatient_no
                                 and t.card_no = '{0}'
                                 and e.class_code = 'UL'
                                 and e.mo_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                 and e.mo_date <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                                 and e.valid_flag = '1'";
            try
            {
                sqlStr = string.Format(sqlStr, cardNo, start, end);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }

            if (this.ExecQuery(sqlStr) == -1)
            {
                return null;
            }

            ArrayList al = null;
            try
            {
                al = new ArrayList();
                while (this.Reader.Read())
                {
                    string lisBarCode = this.Reader[0].ToString();
                    al.Add(lisBarCode);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
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
            return al;
        }

        #endregion

        #region Lis条码相关

        public void PrintLisBarCode(string cardNo, DateTime start, DateTime end)
        {
            try
            {
                string strPath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "LisInterface\\EasiLab.Client.BarCode.dll";
                Assembly assembly = Assembly.LoadFrom(strPath);
                Type type = assembly.GetType("EasiLab.Client.BarCode.BarCodeHelper");

                ArrayList lisBarCodeList = this.QueryLisBarCodeByCardNoAndDate(cardNo, start, end);
                bool haveEmptyCode = false;
                foreach (string tempCode in lisBarCodeList)
                {
                    if (string.IsNullOrEmpty(tempCode))
                    {
                        haveEmptyCode = true;
                        break;
                    }
                }
                if (haveEmptyCode)
                {
                    //存在无条码的,重新根据就诊卡号打条码
                    MethodInfo method = type.GetMethod("PrintBarcodeByHospNum", new Type[] { typeof(string), typeof(DateTime), typeof(DateTime) });
                    method.Invoke(null, new object[] { cardNo, start, end });
                }
                else
                {
                    //均存在条码,根据条码号重打条码
                    MethodInfo method = type.GetMethod("PrintBarcodeByLabNum", BindingFlags.Static | BindingFlags.Public);
                    foreach (string sampleCode in lisBarCodeList)
                    {
                        method.Invoke(null, new object[] { sampleCode });
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("LIS出错信息:\n" + e.GetBaseException().Message);
            }
        }

        public void CancelLisBarCode(string cardNo, DateTime start, DateTime end, string OperId)
        {
            try
            {
                string strPath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "LisInterface\\EasiLab.Client.BarCode.dll";
                Assembly assembly = Assembly.LoadFrom(strPath);
                Type type = assembly.GetType("EasiLab.Client.BarCode.BarCodeHelper");
                MethodInfo method = type.GetMethod("InvalidSpecimen", BindingFlags.Static | BindingFlags.Public);

                ArrayList lisBarCodeList = this.QueryLisBarCodeByCardNoAndDate(cardNo, start, end);
                foreach (string tempCode in lisBarCodeList)
                {
                    if (!string.IsNullOrEmpty(tempCode))
                    {
                        method.Invoke(null, new object[] { tempCode, "HIS作废", OperId });
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("LIS出错信息:\n" + e.GetBaseException().Message);
            }
        }

        #endregion
    }
}
