using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace GZSI.ApiManager
{
    /// <summary>
    /// API����HIS��
    /// </summary>
    public class LocalManager : FS.FrameWork.Management.Database 
    {
        /// <summary>
        /// ҽԺ����
        /// </summary>
        public string HosCode = string.Empty;

        /// <summary>
        /// ��ȡ��ͬ��λ��Ӧ��ҵ������ 
        /// �������ͣ� biz_type  
        /// code:��ͬ��λ���� 
        /// name:��ͬ��λ����
        /// mark:ҵ�����ʹ���
        /// </summary>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public string GetApiBizType(string pactCode)
        {
            string strSql = "";
            string biz_type = "";
            if (this.Sql.GetSql("gzsi.GetApiBizType", ref strSql) == -1)
            {
                strSql = @"select d.mark from com_dictionary d where d.type='biz_type' and d.code='{0}'";
            }
            try
            {
                strSql = string.Format(strSql, pactCode);
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    biz_type = Reader[0].ToString();
                }
                Reader.Close();
                return biz_type;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
 
        }

        /// <summary>
        /// ��ȡAPI�еĴ������� 
        /// ������treatment_type 
        /// code:�������ʹ��� 
        /// name:������������ 
        /// mark:ҵ�����ʹ���
        /// </summary>
        /// <param name="biz_type"></param>
        /// <returns></returns>
        public ArrayList GetApiTreatmenttype(string biz_type)
        {

            string strSql = "";
            ArrayList alTreatmenttype = new ArrayList();
            if (this.Sql.GetSql("gzsi.GetApiTreatmenttype", ref strSql) == -1)
            {
                strSql = @"select d.code,d.name,d.mark from com_dictionary d where d.type='treatment_type' and d.mark='{0}'";
            }
            try
            {
                strSql = string.Format(strSql, biz_type);
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new  FS.FrameWork.Models.NeuObject();
                    obj.ID = Reader[0].ToString();
                    obj.Name = Reader[1].ToString();
                    obj.Memo = Reader[2].ToString();
                    alTreatmenttype.Add(obj);
                }
                Reader.Close();
                return alTreatmenttype;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// ���´�������
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <param name="regNo"></param>
        /// <param name="treamType"></param>
        /// <returns></returns>
        public int UpdateApiTreamType(string clinicNo, string regNo,string treamType)
        {
            string strSql = "";
            if (this.Sql.GetSql("gzsi.UpdateApiTreamType", ref strSql) == -1)
            {
                strSql = @"update fin_ipr_siinmaininfo si set si.applytypeid='{2}' where si.inpatient_no='{0}' and si.reg_no='{1}'";
            }
            try
            {
                strSql = string.Format(strSql, clinicNo, regNo, treamType);
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// ���¹��˾�ҽƾ֤��
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <param name="regNo"></param>
        /// <param name="treamType"></param>
        /// <returns></returns>
        public int UpdateApiIDNO_GS(string clinicNo, string regNo, string idNoGS)
        {
            string strSql = "";
            if (this.Sql.GetSql("gzsi.UpdateApiidNoGS", ref strSql) == -1)
            {
                strSql = @"update fin_ipr_siinmaininfo si set si.IDNO_GS='{2}' where si.inpatient_no='{0}' and si.reg_no='{1}'";
            }
            try
            {
                strSql = string.Format(strSql, clinicNo, regNo, idNoGS);
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <param name="regNo"></param>
        /// <returns></returns>
        public string GetHisApiTreamType(string clinicNo, string regNo)
        {
            string strSql = "",treamType="";
            if (this.Sql.GetSql("gzsi.UpdateApiTreamType", ref strSql) == -1)
            {
                strSql = @"select si.applytypeid from  fin_ipr_siinmaininfo si  where si.inpatient_no='{0}' and si.reg_no='{1}' and si.valid_flag='1' order by si.balance_no";
            }
            try
            {
                strSql = string.Format(strSql, clinicNo, regNo);
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    treamType = Reader[0].ToString();
                }
                Reader.Close();
                return treamType;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
        }

        /// <summary>
        /// ��ȡҽ���ǼǺ�
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <returns></returns>
        public string GetSiRegNo(string clinicNo)
        {
            string balNo = this.GetBalNo(clinicNo);
            if (balNo == "")
                return null;
            string strSql = "", regNo = "";
            if (this.Sql.GetSql("gzsi.GetSiRegNo", ref strSql) == -1)
            {
                strSql = @"select si.reg_no from  fin_ipr_siinmaininfo si where si.inpatient_no='{0}' and si.balance_no='{1}' and si.valid_flag='1'";
            }
            try
            {
                strSql = string.Format(strSql, clinicNo, balNo);
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    regNo = Reader[0].ToString();
                }
                Reader.Close();
                return regNo;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
 
        }

        /// <summary>
        /// ������ˮ�źͷ�Ʊ��
        /// ��ȡ�ȼ���,�������ͺͷ���
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetSiRegNo(string clinicNo, string invoiceNo)
        {
            FS.FrameWork.Models.NeuObject obj = null;

            string strSql = @"select si.reg_no,si.APPLYTYPEID,si.tot_cost from  fin_ipr_siinmaininfo si  where si.inpatient_no='{0}' and si.INVOICE_NO='{1}' and si.valid_flag='1'";

            try
            {
                strSql = string.Format(strSql, clinicNo, invoiceNo);
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = Reader[0].ToString();
                    obj.Name = Reader[1].ToString();
                    obj.Memo = Reader[2].ToString();
                }
                Reader.Close();
                return obj;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

        }

        /// <summary>
        /// ���ݿ��Ż�ȡ���֤����
        /// </summary>
        /// <param name="card_no"></param>
        /// <returns></returns>
        public string GetIDCardByCardNo(string card_no)
        {
            string idcard = "";
            string strSql = @"SELECT  IDENNO FROM COM_PATIENTINFO WHERE CARD_NO='{0}'";
            try
            {
                strSql = string.Format(strSql, card_no);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
            this.ExecQuery(strSql);
            try
            {
                while (Reader.Read())
                {
                    idcard = Reader[0].ToString();
                }
                Reader.Close();
                return idcard;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
        }

        /// <summary>
        /// �õ��������
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public string GetBalNo(string inpatientNo)
        {
            string strSql = "";
            string balNo = "";
            if (this.Sql.GetCommonSql("Fee.Interface.GetBalNo.1", ref strSql) == -1)
                return "";
            try
            {
                strSql = string.Format(strSql, inpatientNo);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
            this.ExecQuery(strSql);
            try
            {
                while (Reader.Read())
                {
                    balNo = Reader[0].ToString();
                }
                Reader.Close();
                return balNo;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
        }

        /// <summary>
        /// ��ȡҽ������ֵ�
        /// </summary>
        /// <returns></returns>
        public ArrayList QuerySiDisease()
        {
            string strSql = "";
            ArrayList alDisease = new ArrayList();
            if (this.Sql.GetSql("gzsi.QuerySiDisease", ref strSql) == -1)
            {   
                //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
                //strSql = @"SELECT * FROM MET_COM_SIDIAGNOSE";
                strSql = @"SELECT * FROM MET_COM_ICD10";
            }
            try
            {
                this.ExecQuery(strSql);
                FS.HISFC.Models.HealthRecord.ICD icd = null;
                while (Reader.Read())
                {
                    icd = new FS.HISFC.Models.HealthRecord.ICD();
                    //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
                    //icd.ID = Reader["ICD"].ToString();
                    //icd.Name = Reader["DISEASE"].ToString();
                    //icd.SpellCode = Reader["SPELL_CODE"].ToString();
                    //icd.WBCode = Reader["WB_CODE"].ToString();
                    //icd.Memo = Reader["MEMO"].ToString();

                    icd.ID = Reader["ICD_CODE"].ToString();
                    icd.Name = Reader["ICD_NAME"].ToString();
                    icd.SpellCode = Reader["SPELL_CODE"].ToString();
                    icd.WBCode = Reader["WB_CODE"].ToString();

                    alDisease.Add(icd); //�������
                    icd = null;
                }
                Reader.Close();
                return alDisease;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// ����ҽ����Ż�ȡ������ҩ��ʶ 
        /// </summary>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public string GetLimitByCard_No(string recipe_no, string sequence_no)
        {
            string sql = @"select recipe_memo from fin_opb_feedetail where recipe_no='{0}' and sequence_no='{1}'";
            sql = string.Format(sql, recipe_no, sequence_no);

            //FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            string num = this.ExecSqlReturnOne(sql);
            if (num == null || num.Equals("-1"))
            {
                return "";
            }
            else
            {
                return num;
            }
        }

        #region ����ҽ�������

        /// <summary>
        /// �������������Ϣ
        /// </summary>
        /// <param name="reg"></param>
        /// <param name="jsid"></param>
        /// <returns></returns>
        public int InsertMZJSData(FS.HISFC.Models.Registration.Register reg, ref string jsid)
        {

            try
            {
                string getIDSql = "select SEQ_GZSI_HIS_MZJS.Nextval from dual";
                this.ExecQuery(getIDSql);
                if (Reader.Read())
                {
                    jsid = Reader[0].ToString();
                }
                Reader.Close();
                if (string.IsNullOrEmpty(jsid))
                {
                    this.Err = "��ȡ����ID(SEQ_GZSI_HIS_MZJS)ʧ��";
                    return -1;
                }

                #region �������
                string insertSql = @"INSERT INTO GZSI_HIS_MZJS(JYDJH
                                                             , FYPC
                                                             , YYBH
                                                             , GMSFHM
                                                             , ZYH
                                                             , RYRQ
                                                             , JSRQ
                                                             , ZYZJE
                                                             , SBZFJE
                                                             , ZHZFJE
                                                             , BFXMZFJE
                                                             , QFJE
                                                             , GRZFJE1
                                                             , GRZFJE2
                                                             , GRZFJE3
                                                             , CXZFJE
                                                             , ZFYY
                                                             , YYFDJE
                                                             , BZ1
                                                             , BZ2
                                                             , BZ3
                                                             , DRBZ
                                                             , CLINIC_CODE
                                                             , CARD_NO
                                                             , INVOICE_NO
                                                             , OPER_CODE
                                                             , OPER_DATE
                                                             ,VALID_FLAG
                                                             ,JSID)
                                                        VALUES
                                                            ('{0}'      --��ҽ�ǼǺ�
                                                            ,'{1}'     --��������
                                                            ,'{2}'     --ҽԺ���
                                                            ,'{3}'     --���֤��
                                                            ,'{4}'     --�����/סԺ��
                                                            ,to_date('{5}','yyyy-mm-dd hh24:mi:ss')     --��������
                                                            ,to_date('{6}','yyyy-mm-dd hh24:mi:ss')     --��������
                                                            ,'{7}'     --�ܽ��
                                                            ,'{8}'     --�籣֧�����
                                                            ,'{9}'     --�˻�֧�����
                                                            ,'{10}'    --������Ŀ�Ը����
                                                            ,'{11}'    --�����𸶽��
                                                            ,'{12}'    --�����Է���Ŀ���
                                                            ,'{13}'    --�����Ը����
                                                            ,'{14}'    --�����Ը����
                                                            ,'{15}'    --��ͳ��֧���޶�����Ը����
                                                            ,'{16}'    --�Է�ԭ��
                                                            ,'{17}'    --ҽҩ�����ֵ����
                                                            ,'{18}'    --��ע1,��¼����ʱ��
                                                            ,'{19}'    --��ע2
                                                            ,'{20}'    --��ע3
                                                            ,'{21}'    --�����־
                                                            ,'{22}'    --������ˮ��
                                                            ,'{23}'    --�����
                                                            ,'{24}'    --��Ʊ��
                                                            ,'{25}'    --����Ա
                                                            ,sysdate    --����ʱ��
                                                            ,'1'
                                                            ,'{26}'    --����ID
                                                            )";
                #endregion

                //д��
                insertSql = string.Format(insertSql, reg.SIMainInfo.RegNo, reg.SIMainInfo.FeeTimes, reg.SIMainInfo.HosNo, reg.IDCard, reg.PID.CardNO,
                                                     reg.SeeDoct.OperTime, reg.SIMainInfo.BalanceDate, reg.SIMainInfo.TotCost, reg.SIMainInfo.PubCost, reg.SIMainInfo.PayCost,
                                                     reg.SIMainInfo.ItemYLCost, reg.SIMainInfo.BaseCost, reg.SIMainInfo.ItemPayCost, reg.SIMainInfo.PubOwnCost, reg.SIMainInfo.OwnCost,
                                                     reg.SIMainInfo.OverTakeOwnCost, reg.SIMainInfo.Memo, reg.SIMainInfo.HosCost, reg.SIMainInfo.User01, reg.SIMainInfo.User02,
                                                     reg.SIMainInfo.User03, reg.SIMainInfo.ReadFlag, reg.ID, reg.PID.CardNO, reg.SIMainInfo.InvoiceNo,
                                                     reg.SIMainInfo.OperInfo.ID,/*����ʱ��,��Ч���,*/ jsid);
                return this.ExecNoQuery(insertSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// ����ҽ��������ϢΪ��Ч
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public int DisableMZJSData(string clinicCode, string invoiceNo)
        {
            if (string.IsNullOrEmpty(clinicCode) || string.IsNullOrEmpty(invoiceNo))
            {
                this.Err = "DisableMZJSData������������";
                return -1;
            }
            string strSql = string.Format("update GZSI_HIS_MZJS t set t.valid_flag='0' where t.clinic_code='{0}' and t.invoice_no='{1}'", clinicCode, invoiceNo);
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����ҽ��������ϢΪ��Ч
        /// </summary>
        /// <param name="jsid"></param>
        /// <returns></returns>
        public int DisableMZJSData(string regNO)
        {
            if (string.IsNullOrEmpty(regNO))
            {
                this.Err = "DisableMZJSData������������";
                return -1;
            }
            string strSql = string.Format("update GZSI_HIS_MZJS t set t.valid_flag='0' where t.jydjh='{0}' and t.valid_flag='1'", regNO);
            return this.ExecNoQuery(strSql);
        }

        #endregion

        #region ����ҽ��������ϸ��

        /// <summary>
        /// �������������ϸ
        /// </summary>
        /// <param name="r">�Һ���Ϣ������ҽ�����߻�����Ϣ</param>
        /// <param name="feeDetails">������ϸ</param>
        /// <param name="operDate">����ʱ��</param>
        /// <returns>-1���� 0 û�в��� 1��ȷ</returns>
        public int InsertMZXMData(FS.HISFC.Models.Registration.Register r, ArrayList feeDetails, DateTime operDate, string jsid)
        {
            int iReturn = 0;
            int uploadRows = 0;
            try
            {
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
                {
                    iReturn = DeleteMZXMData(r, f);
                    if (iReturn < 0)
                    {
                        this.Err += "ɾ����ʷ������ϸʧ��!";
                        return -1;
                    }

                    iReturn = InsertMZXMData(r, f, operDate, jsid);
                    if (iReturn <= 0)
                    {
                        this.Err += "������ϸʧ��!";
                        return -1;
                    }

                    uploadRows += iReturn;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return uploadRows;
        }

        /// <summary>
        /// ���뵥��������ϸ
        /// </summary>
        /// <param name="r">�Һ���Ϣ������ҽ�����߻�����Ϣ</param>
        /// <param name="f">������ϸ</param>
        /// <param name="operDate">����ʱ��</param>
        /// <returns>-1���� 0 û�в��� 1��ȷ</returns>
        public int InsertMZXMData(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f, DateTime operDate, string jsid)
        {
            if (string.IsNullOrEmpty(r.SIMainInfo.HosNo))
            {
                r.SIMainInfo.HosNo = string.IsNullOrEmpty(HosCode) ? r.SIMainInfo.RegNo.Substring(0, 6) : HosCode;
            }

            #region �������

            string strSQL = @"INSERT INTO GZSI_HIS_MZXM     --����ҽ��������ϸ��Ϣ��
                                      ( JYDJH,   --��ҽ�ǼǺ�
                                        YYBH,   --ҽԺ���
                                        GMSFHM,   --�������֤��
                                        ZYH,   --סԺ��/�����
                                        RYRQ,   --�Һ�/��Ժʱ��
                                        FYRQ,   --�շ�ʱ��
                                        XMXH,   --��Ŀ���
                                        XMBH,   --ҽԺ����Ŀ���
                                        XMMC,   --ҽԺ����Ŀ����
                                        FLDM,   --�������
                                        YPGG,   --���
                                        YPJX,   --����
                                        JG,   --����
                                        MCYL,   --����
                                        JE,   --���
                                        BZ1,   --��ע1����ż�¼����ʱ��
                                        BZ2,   --��ע2
                                        BZ3,   --��ע3,��ŷ�����ϸ����ʱ��Ч�Լ��Ĵ���������
                                        DRBZ,   --�����־
                                        YPLY,   --1-���� 2-���� 3-����
                                        CLINIC_CODE,   --���������ˮ��
                                        CARD_NO,   --�����
                                        OPER_CODE,   --����Ա
                                        OPER_DATE,   --����ʱ��
                                        INVOICE_NO,   --��Ʊ��
                                        FYPC,   --��������
                                        fee_code,
                                        jsid
                                        ) 
                                        VALUES
                                        (
                                        '{0}',   --��ҽ�ǼǺ�
                                        '{1}',   --ҽԺ���
                                        '{2}',   --�������֤��
                                        '{3}',   --סԺ��/�����
                                        TO_DATE('{4}','YYYY-MM-DD HH24:MI:SS'),   --�Һ�/��Ժʱ��
                                        TO_DATE('{5}','YYYY-MM-DD HH24:MI:SS'),   --�շ�ʱ��
                                        '{6}',   --��Ŀ���
                                        '{7}',   --ҽԺ����Ŀ���
                                        '{8}',   --ҽԺ����Ŀ����
                                        '{9}',   --�������
                                        '{10}',   --���
                                        '{11}',   --����
                                        '{12}',   --����
                                        '{13}',   --����
                                        '{14}',   --���
                                        '{15}',   --��ע1����ż�¼����ʱ��
                                        '{16}',   --��ע2
                                        '{17}',   --��ע3,��ŷ�����ϸ����ʱ��Ч�Լ��Ĵ���������
                                        '{18}',   --�����־
                                        '{19}',   --1-����   2-����3-����
                                        '{20}',   --���������ˮ��
                                        '{21}',   --�����
                                        '{22}',   --����Ա
                                        sysdate,   --����ʱ��
                                        '{23}',   --��Ʊ��
                                        '{24}',   --��������
                                        '{25}',  --HIS�������
                                        '{26}') ";
            #endregion 

            string name = "";
            name = f.Item.Name;
            if (name.Length > 20)
            {
                name = name.Substring(0, 20);
            }
            try
            {
                decimal price = f.SIPrice; //this.GetPrice(f.Item);
                decimal totCost = f.SIft.OwnCost + f.SIft.PubCost + f.SIft.PayCost;

                strSQL = string.Format(strSQL,
                    r.SIMainInfo.RegNo,
                    r.SIMainInfo.HosNo,
                    r.IDCard,
                    r.PID.CardNO,
                    r.DoctorInfo.SeeDate.ToString(),
                    operDate.ToString(),
                    f.RecipeNO+f.SequenceNO,//f.Order.SequenceNO.ToString(),
                    f.Item.UserCode,
                    name,
                    "0",
                    f.Item.Specs,
                    r.MainDiagnose, //���
                    FS.FrameWork.Public.String.FormatNumber(price / f.Item.PackQty, 4),
                    f.Item.Qty,                 //����Amount
                    totCost,//(f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost).ToString(),
                    operDate.ToString(),
                    "",
                    "",
                    "0",
                    "",
                    r.ID,
                    r.PID.CardNO,
                    this.Operator.ID,
                    r.SIMainInfo.InvoiceNo,
                    r.SIMainInfo.FeeTimes.ToString(),
                    f.Item.MinFee.ID,
                    jsid
                    );
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }


        /// <summary>
        /// ɾ������������ϸ
        /// </summary>
        /// <param name="r">�Һ���Ϣ������ҽ�����߻�����Ϣ</param>
        /// <param name="f">������ϸ</param>
        /// <param name="operDate">����ʱ��</param>
        /// <returns>-1���� 0 û�в��� 1��ȷ</returns>
        public int DeleteMZXMData(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            if (string.IsNullOrEmpty(r.SIMainInfo.HosNo))
            {
                r.SIMainInfo.HosNo = string.IsNullOrEmpty(HosCode) ? r.SIMainInfo.RegNo.Substring(0, 6) : HosCode;
            }

            string strSQL = "delete from gzsi_his_mzxm t where t.clinic_code='{0}' and t.jydjh='{1}' and t.xmbh='{2}' and t.xmxh='{3}' ";
            try
            {
                strSQL = string.Format(strSQL,
                    r.ID,
                    r.SIMainInfo.RegNo,
                    f.Item.UserCode,
                    f.RecipeNO + f.SequenceNO
                    );
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        #endregion 

        #region ����۸�

        /// <summary>
        /// ����ϴ��ļ۸�
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private decimal GetPrice(FS.HISFC.Models.Base.Item item)
        {
            decimal price = item.Price;
            if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
            {
                FS.HISFC.Models.Fee.Item.Undrug undrug = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(item.ID);
                //if (undrug.SpecialPrice > 0)
                //{
                price = undrug.SpecialPrice;
                //}
            }
            else if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(item.ID);
                if (phaItem.SpecialPrice > 0)
                {
                    price = phaItem.SpecialPrice;
                }
            }
            if (price == 0)
            {
                price = item.Price;
            }
            return price;
        }

        #endregion
    }
}
