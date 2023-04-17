using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Nurse.GuangZhou.Gyzl
{
    public class Common : FS.FrameWork.Management.Database
    {
        #region 查询

        /// <summary>
        /// 根据门诊流水号获取检验样本号
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <returns></returns>
        public ArrayList GetULItemSampleCodeByClinicNO(string clinicNO)
        {
            string sql = @"select distinct nvl(f.sample_id,' ')
                             from fin_opb_feedetail f  
                            where f.pay_flag = '1'
                              and f.cancel_flag = '1'
                              and f.clinic_code = '{0}'
                              and f.class_code = 'UL'";
            if (this.ExecQuery(sql, clinicNO) == -1)
            {
                return null;
            }
            ArrayList sampleList = new ArrayList();
            try
            {
                while (this.Reader.Read())
                {
                    sampleList.Add(this.Reader[0]);
                }
                this.Reader.Close();
                return sampleList;
            }
            catch (Exception e)
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                return null;
            }
        }

        /// <summary>
        /// 根据门诊流水号获取检验项目
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <returns></returns>
        public ArrayList GetULItemListByClinicNo(string clinicNO)
        {
            string sqlStr = @"select f.recipe_no,
                                     f.item_name,
                                     f.sample_id,
                                     f.confirm_inject
                                from fin_opb_feedetail f
                               where f.clinic_code = '{0}'
                                 and f.confirm_inject = '0'
                                 and f.cancel_flag = '1'
                                 and f.class_code = 'UL'";
            try
            {
                sqlStr = String.Format(sqlStr, clinicNO);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
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
                    FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();
                    feeItemList.RecipeNO = this.Reader[0].ToString();
                    feeItemList.Item.Name = this.Reader[1].ToString();
                    feeItemList.Order.Sample.ID = this.Reader[2].ToString();
                    feeItemList.ConfirmedInjectCount = int.Parse(this.Reader[3].ToString());
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
        /// 根据姓名和挂号日期查询有检验项目的患者
        /// </summary>
        /// <param name="name"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ArrayList QueryPatientWithULItemByNameAndDate(string name, DateTime start, DateTime end)
        {
            string sqlStr = @"select distinct r.clinic_code,
                                     r.name,
                                     decode(r.sex_code,'M','男','F','女','未知'),
                                     r.birthday,
                                     r.card_no,
                                     p.home_tel,
                                     p.home,
                                     p.linkman_name,
                                     p.linkman_tel,
                                     decode((select count(distinct d.sample_id)
                                               from fin_opb_feedetail d
                                              where d.clinic_code = f.clinic_code
                                                and d.sample_id is not null),0,'0','1') num
                                from fin_opr_register r, fin_opb_feedetail f, com_patientinfo p
                               where r.clinic_code = f.clinic_code
                                 and r.card_no = p.card_no
                                 and f.class_code = 'UL'
                                 and f.pay_flag = '1'
                                 and f.cancel_flag = '1'
                                 and r.name like '%{0}%'
                                 and f.fee_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                 and f.fee_date <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')";
            try
            {
                sqlStr = String.Format(sqlStr, name, start.ToString(), end.ToString());
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
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
                FS.HISFC.Models.RADT.PatientInfo patientInfo = null;
                while (this.Reader.Read())
                {
                    patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                    patientInfo.ID = this.Reader[0].ToString();
                    patientInfo.Name = this.Reader[1].ToString();
                    patientInfo.Sex.Name = this.Reader[2].ToString();
                    patientInfo.Birthday = Convert.ToDateTime(this.Reader[3].ToString());
                    patientInfo.PID.CardNO = this.Reader[4].ToString();
                    patientInfo.PhoneHome = this.Reader[5].ToString();
                    patientInfo.AddressHome = this.Reader[6].ToString();
                    patientInfo.Kin.Name = this.Reader[7].ToString();
                    patientInfo.Kin.RelationPhone = this.Reader[8].ToString();
                    patientInfo.Memo = this.Reader[9].ToString();
                    al.Add(patientInfo);
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
        /// 根据卡号和日期查询有检验项目的患者
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ArrayList QueryPatientWithULItemByCardNoAndDate(string cardNo, DateTime start, DateTime end)
        {
            string sqlStr = @"select distinct r.clinic_code,
                                     r.name,
                                     decode(r.sex_code,'M','男','F','女','未知'),
                                     r.birthday,
                                     r.card_no,
                                     p.home_tel,
                                     p.home,
                                     p.linkman_name,
                                     p.linkman_tel
                                from fin_opr_register r, fin_opb_feedetail f, com_patientinfo p
                               where r.clinic_code = f.clinic_code
                                 and r.card_no = p.card_no
                                 and f.class_code = 'UL'
                                 and f.pay_flag = '1'
                                 and f.cancel_flag = '1'
                                 and r.card_no = '{0}'
                                 and f.fee_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                 and f.fee_date <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')";
            try
            {
                sqlStr = String.Format(sqlStr, cardNo, start.ToString(), end.ToString());
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
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
                FS.HISFC.Models.RADT.PatientInfo patientInfo = null;
                while (this.Reader.Read())
                {
                    patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                    patientInfo.ID = this.Reader[0].ToString();
                    patientInfo.Name = this.Reader[1].ToString();
                    patientInfo.Sex.Name = this.Reader[2].ToString();
                    patientInfo.Birthday = Convert.ToDateTime(this.Reader[3].ToString());
                    patientInfo.PID.CardNO = this.Reader[4].ToString();
                    patientInfo.PhoneHome = this.Reader[5].ToString();
                    patientInfo.AddressHome = this.Reader[6].ToString();
                    patientInfo.Kin.Name = this.Reader[7].ToString();
                    patientInfo.Kin.RelationPhone = this.Reader[8].ToString();

                    al.Add(patientInfo);
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
        /// 根据姓名和挂号日期查询有注射项目的患者
        /// </summary>
        /// <param name="name"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ArrayList QueryPatientWithInjectItemByNameAndDate(string name, DateTime start, DateTime end)
        {
            string sqlStr = @"select distinct r.clinic_code,
                                     r.name,
                                     decode(r.sex_code,'M','男','F','女','未知'),
                                     r.birthday,
                                     r.card_no,
                                     p.home_tel,
                                     p.home,
                                     p.linkman_name,
                                     p.linkman_tel
                                from fin_opr_register r, fin_opb_feedetail f, com_patientinfo p
                               where r.clinic_code = f.clinic_code
                                 and r.card_no = p.card_no
                                 and f.inject_number <> 0
                                 and f.pay_flag = '1'
                                 and f.cancel_flag = '1'
                                 and r.name like '%{0}%'
                                 and f.fee_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                 and f.fee_date <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')";
            try
            {
                sqlStr = String.Format(sqlStr, name, start.ToString(), end.ToString());
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
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
                FS.HISFC.Models.RADT.PatientInfo patientInfo = null;
                while (this.Reader.Read())
                {
                    patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                    patientInfo.ID = this.Reader[0].ToString();
                    patientInfo.Name = this.Reader[1].ToString();
                    patientInfo.Sex.Name = this.Reader[2].ToString();
                    patientInfo.Birthday = Convert.ToDateTime(this.Reader[3].ToString());
                    patientInfo.PID.CardNO = this.Reader[4].ToString();
                    patientInfo.PhoneHome = this.Reader[5].ToString();
                    patientInfo.AddressHome = this.Reader[6].ToString();
                    patientInfo.Kin.Name = this.Reader[7].ToString();
                    patientInfo.Kin.RelationPhone = this.Reader[8].ToString();

                    al.Add(patientInfo);
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
        /// 返回诊断信息
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <returns></returns>
        public string GetDiagnose(string clinicNo)
        {
            string sqlStr = "select diagnose from met_cas_history where clinic_code = '{0}'";
            try
            {
                sqlStr = String.Format(sqlStr, clinicNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            if (this.ExecQuery(sqlStr) == -1)
            {
                return null;
            }
            try
            {
                string diagnose = string.Empty;
                while (this.Reader.Read())
                {
                    diagnose = this.Reader.IsDBNull(0) ? "" : this.Reader.GetString(0);
                }
                return diagnose;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                return string.Empty;
            }
            finally
            {
                if (this.Reader != null && this.Reader.IsClosed == false)
                {
                    this.Reader.Close();
                }
            }
        }

        #endregion

        #region 公用方法

        /// <summary>
        /// 获取显示的注射天数
        /// 格式：(正常)第X天/共X天
        ///       (补打)第X天/共X天(*)
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="isReprint"></param>
        /// <returns></returns>
        public string GetInjectDay(FS.HISFC.Models.Fee.Outpatient.FeeItemList itemInfo, bool isReprint)
        {
            string result = string.Empty;

            int totalInject = (int)itemInfo.InjectCount;                                    //总注射次数
            int injectCountPerDay = itemInfo.Order.Frequency.Times.Length;                  //每天院注次数
            int confirmedCount = (int)itemInfo.ConfirmedInjectCount;                        //已经确认院注数
            int totalDay = (int)Math.Ceiling((decimal)totalInject / injectCountPerDay);     //总确认天数
            int confirmedDay = (int)(confirmedCount / injectCountPerDay);                   //已经确认天数
            bool isConfirmedAll = true;

            if (isReprint)
            {
                isConfirmedAll = false;
                if (confirmedDay == 0)
                {
                    confirmedDay = 1;
                }
            }
            else if (confirmedDay < totalDay)
            {
                //未注射完的情况
                confirmedDay += 1;
                isConfirmedAll = false;
            }
            result += "第" + confirmedDay.ToString() + "天/共" + totalDay.ToString() + "天";
            if (isConfirmedAll)
            {
                result += "(*)";
            }
            return result;
        }
        
        #endregion
    }
}
