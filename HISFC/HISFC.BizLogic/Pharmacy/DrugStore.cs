using System;
using System.Collections;
using FS.HISFC.Models.Pharmacy;
using FS.HISFC.Models;
using FS.FrameWork.Function;

namespace FS.HISFC.BizLogic.Pharmacy 
{
    /// <summary>
    /// [��������: ҩƷ��ҩ������]<br></br>
    /// [�� �� ��: Cuip]<br></br>
    /// [����ʱ��: 2005-01]<br></br>
    /// <�޸ļ�¼
    ///		�޸���='������'
    ///		�޸�ʱ��='2006-09-28'
    ///		�޸�Ŀ��='ϵͳ�ع�'
    ///		�޸�����='�������ƹ淶 ��ʽ�淶'
    ///  />
    /// <�޸ļ�¼>
    ///    1.ҩ����ҩ������� by Sunjh 2010-9-14 {6DB2E467-CFBE-4cf1-B5E9-C48BAEFDC487} 
    /// </�޸ļ�¼>
    /// </summary>
    public class DrugStore : DataBase
    {
        public DrugStore()
        {

        }

        #region ��̬����

        /// <summary>
        /// ���ҵ�ַ (���ط�ҩ��Ϣ  ǰ���ַ�)
        /// </summary>
        public static System.Collections.Hashtable hsDeptAddress = null;

        /// <summary>
        /// �÷�����
        /// </summary>
        internal static System.Collections.Hashtable hsUsageContrast = null;

        /// <summary>
        /// ���Ͷ���
        /// </summary>
        internal static System.Collections.Hashtable hsDosageContrast = null;
        #endregion

        #region ��ҩ������

        #region ��������ɾ���Ĳ���

        /// <summary>
        /// ���update����insert��ҩ�������Ĵ����������
        /// </summary>
        /// <param name="DrugBillClass">���������</param>
        /// <returns>�ɹ������ַ������� ʧ�ܷ���null</returns>
        private string[] myGetParmDrugBillClass(DrugBillClass DrugBillClass)
        {
            #region "�ӿ�˵��"
            //1����ҩ�������
            //2����ҩ��������
            //3����ӡ����1����2��ϸ3��ҩ4�󴦷�
            //4����ҩ����
            //5��ͣ�ñ��1-ͣ��0����Ч
            //6������Ա
            //7������ʱ��
            //8����ע

            #endregion
            string[] strParm ={   DrugBillClass.ID,
								 DrugBillClass.Name,
								 DrugBillClass.DrugAttribute.ID.ToString(),
								 DrugBillClass.PrintType.ID.ToString(),
				NConvert.ToInt32(DrugBillClass.IsValid).ToString(),
								 DrugBillClass.Memo,
								 this.Operator.ID
							 };

            return strParm;
        }

        /// <summary>
        /// ȡ��ҩ��������Ϣ�б�������һ�����߶���ҩƷ��¼
        /// ˽�з����������������е���
        /// </summary>
        /// <param name="SQLString">SQL���</param>
        /// <returns>��ҩ����������</returns>
        private ArrayList myGetDrugBillClass(string SQLString)
        {
            ArrayList al = new ArrayList();  //���ڷ���ҩƷ��Ϣ������
            DrugBillClass info;            //���������еİ�ҩ��������Ϣ

            this.ExecQuery(SQLString);
            try
            {
                while (this.Reader.Read())
                {
                    info = new DrugBillClass();
                    try
                    {
                        info.ID = this.Reader[0].ToString();                          //��ҩ���������
                        info.Name = this.Reader[1].ToString();                        //��ҩ����������
                        info.PrintType.ID = this.Reader[2].ToString();                //��ӡ����
                        info.IsValid = NConvert.ToBoolean(this.Reader[3].ToString()); //�Ƿ���Ч
                        info.Memo = this.Reader[4].ToString();                        //��ע
                        info.Oper.ID = this.Reader[5].ToString();                //����Ա����
                        try { info.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6].ToString()); }
                        catch { }//����ʱ��
                    }
                    catch (Exception ex)
                    {
                        this.Err = "��ð�ҩ��������Ϣ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    al.Add(info);
                }
            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "��ð�ҩ��������Ϣʱ��ִ��SQL������myGetDrugBillClass" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return al;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// ����һ����ҩ�������¼
        /// </summary>
        /// <param name="info">��ҩ������ʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int InsertDrugBillClass(DrugBillClass info)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.InsertDrugBillClass", ref strSql) == -1) return -1;
            try
            {
                //ȡ��ҩ��������ˮ��
                string ID = "";
                ID = this.GetSysDateTime("yyMMddHHmmss");
                if (ID == "-1") return -1;

                //��ֵ��info.ID���Ա���ô˷����Ķ���ʹ�ô˰�ҩ��������ˮ��
                if (info.ID != "P" && info.ID != "R")
                {
                    info.ID = ID;
                }

                string[] strParm = myGetParmDrugBillClass(info);  //ȡ�����б�
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����һ����ҩ�������¼
        /// </summary>
        /// <param name="info">��ҩ������ʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int UpdateDrugBillClass(DrugBillClass info)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugBillClass", ref strSql) == -1) return -1;

            try
            {
                string[] strParm = myGetParmDrugBillClass(info);  //ȡ�����б�
                strSql = string.Format(strSql, strParm);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���ݰ�ҩ���������,ɾ��һ����¼
        /// </summary>
        /// <param name="ID">��ҩ���������</param>
        /// <returns>�ɹ�����ɾ������ ʧ�ܷ���null</returns>
        public int DeleteDrugBillClass(string ID)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.DeleteDrugBillClass", ref strSql) == -1) return -1;

            try
            {
                strSql = string.Format(strSql, ID);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "�����������ȷ��Pharmacy.Item.DrugBillClass";
                return -1;
            }
            int parm = this.ExecNoQuery(strSql);
            if (parm == -1) return -1;

            //ɾ����ҩ�����ͬʱ��ɾ����ҩ������ϸ����
            return this.DeleteDrugBillList(ID);
        }

        /// <summary>
        /// �����ҩ�����࣭����ִ�и��²��������û���ҵ����Ը��µ����ݣ������һ���¼�¼
        /// </summary>
        /// <param name="info">��ҩ������ʵ��</param>
        /// <returns>0δ���£�����1�ɹ���-1ʧ��</returns>
        public int SetDrugBillClass(DrugBillClass info)
        {
            int parm;
            #region �ȸ��º����
            //			//ִ�и��²���
            //			parm = UpdateDrugBillClass(info);
            //
            //			//���û���ҵ����Ը��µ����ݣ������һ���¼�¼
            //			if (parm == 0 )
            //			{
            //				parm = InsertDrugBillClass(info);
            //			}
            #endregion

            //����������ӵ����������һ���¼�¼��������´˼�¼
            if (info.ID == "")
                parm = InsertDrugBillClass(info);
            else
                parm = UpdateDrugBillClass(info);

            return parm;
        }

        #endregion

        #region �ڲ�ʹ��

        /// <summary>
        /// ���ݰ�ҩ�����������ĳһ��ҩ��������Ϣ
        /// </summary>
        /// <param name="ID">��ҩ���������</param>
        /// <returns>�ɹ����ذ�ҩ��������Ϣ ʧ�ܷ���null</returns>
        public DrugBillClass GetDrugBillClass(string ID)
        {
            string strSelect = "";  //��ð�ҩ��������Ϣ��SELECT���
            string strSQL = "";  //���ݰ�ҩ�����������ĳһ��ҩ��������Ϣ��WHERE�������

            //ȡSELECT���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugBillClass", ref strSelect) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugBillClass�ֶ�!";
                return null;
            }

            //ȡWHERE�������
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugBillClass.Where", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugBillClass.Where�ֶ�!";
                return null;
            }

            try
            {
                strSQL = string.Format(strSelect + " " + strSQL, ID);
            }
            catch
            {
                return null;
            }

            //����SQL���ȡ��ҩ���������鲢���������е�������¼
            try
            {
                ArrayList al = this.myGetDrugBillClass(strSQL);
                //���û��ȡ�����ݣ���ʾ����
                if (al.Count == 0)
                {
                    this.Err = "û���ҵ���Ӧ�İ�ҩ���� ���룺" + ID;
                    this.WriteErr();
                    return null;
                }
                return (DrugBillClass)al[0];
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// ���ݰ�ҩ��������ϸ��ҽ�����ͣ��÷���ҩƷ���ͣ�ҩƷ���ʣ�ҩƷ���ͣ����ĳһ��ҩ��������Ϣ
        /// </summary>
        /// <param name="orderType">ҽ������</param>
        /// <param name="usageCode">�÷�</param>
        /// <param name="drugType">ҩƷ����</param>
        /// <param name="drugQuality">ҩƷ����</param>
        /// <param name="dosageFormCode">ҩƷ����</param>
        /// <returns>���ҳɹ�����ʵ�� ʧ�ܷ���null δ�ҵ�����ErrCode -1</returns>
        public DrugBillClass GetDrugBillClass(string orderType, string usageCode, string drugType, string drugQuality, string dosageFormCode)
        {
            string strSQL = "";  //��ð�ҩ��������Ϣ��SQL���

            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugBillClass.ByList", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugBillClass.ByList�ֶ�!";
                return null;
            }

            try
            {
                string[] parm = {
									orderType,
									usageCode,
									drugType,
									drugQuality,
									dosageFormCode
								};
                strSQL = string.Format(strSQL, parm);
            }
            catch
            {
                return null;
            }

            //����SQL���ȡ��ҩ���������鲢���������е�������¼
            try
            {
                ArrayList al = this.myGetDrugBillClass(strSQL);
                //���û��ȡ�����ݣ���ʾ����
                if (al.Count == 0)
                {
                    this.Err = "û���ҵ���Ӧ�İ�ҩ���������Ƿ��ڰ�ҩ����ά�������ݡ�\nҽ������:" + orderType
                        + " \nҩƷ����:" + drugType + " \n�÷�:" + usageCode + " \nҩƷ����:" + drugQuality + " \nҩƷ����:" + dosageFormCode;
                    this.ErrCode = "-1";
                    this.WriteErr();
                    return null;
                }
                return (DrugBillClass)al[0];
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// ���ݰ�ҩ��������ϸ��ҽ�����ͣ��÷���ҩƷ���ͣ�ҩƷ���ʣ�ҩƷ���ͣ����ĳһ��ҩ��������Ϣ
        /// </summary>
        /// <param name="isUsageDosageClass">��ҩ���Ƿ�ʹ���÷�/���ʹ���</param>
        /// <param name="orderType">ҽ������</param>
        /// <param name="usageCode">�÷�</param>
        /// <param name="drugType">ҩƷ����</param>
        /// <param name="drugQuality">ҩƷ����</param>
        /// <param name="dosageFormCode">ҩƷ����</param>
        /// <returns>���ҳɹ�����ʵ�� ʧ�ܷ���null δ�ҵ�����ErrCode -1</returns>
        public DrugBillClass GetDrugBillClass(bool isUsageDosageClass, string orderType, string usageCode, string drugType, string drugQuality, string dosageFormCode)
        {
            if (!isUsageDosageClass)
            {
                return GetDrugBillClass(orderType, usageCode, drugType, drugQuality, dosageFormCode);
            }
            else
            {
                //��ȡ����������÷�/���Ͷ�Ӧ�Ĵ�����ϸ
                if (DrugStore.hsUsageContrast == null || DrugStore.hsDosageContrast == null)      //��ȡ�÷��������
                {
                    FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
                    if (this.Trans != null)
                    {
                        consManager.SetTrans(this.Trans);
                    }
                    //��ȡ�÷����������Ϣ
                    ArrayList alUsageContrast = consManager.GetList("USAGECONTRAST");
                    if (alUsageContrast == null || alUsageContrast.Count == 0)
                    {
                        DrugStore.hsUsageContrast = new Hashtable();
                    }
                    else
                    {
                        foreach (FS.HISFC.Models.Base.Const usageContrast in alUsageContrast)
                        {
                            DrugStore.hsUsageContrast.Add(usageContrast.ID, usageContrast.Name);
                        }
                    }
                    //��ȡ���ʹ��������Ϣ
                    ArrayList alDosageContrast = consManager.GetList("DOSAGECONTRAST");
                    if (alDosageContrast == null || alDosageContrast.Count == 0)
                    {
                        DrugStore.hsDosageContrast = new Hashtable();
                    }
                    else
                    {
                        foreach (FS.HISFC.Models.Base.Const dosageContrast in alDosageContrast)
                        {
                            DrugStore.hsDosageContrast.Add(dosageContrast.ID, dosageContrast.Name);
                        }
                    }
                }
                //��ȡ���մ���
                if (DrugStore.hsDosageContrast.ContainsKey(dosageFormCode))
                {
                    dosageFormCode = DrugStore.hsDosageContrast[dosageFormCode] as string;
                }
                //��ȡ���մ���
                if (DrugStore.hsUsageContrast.ContainsKey(usageCode))
                {
                    usageCode = DrugStore.hsUsageContrast[usageCode] as string;
                }

                return GetDrugBillClass(orderType, usageCode, drugType, drugQuality, dosageFormCode);
            }
        }

        /// <summary>
        /// ��ð�ҩ��������Ϣ�б�
        /// </summary>
        /// <returns>��ҩ����������</returns>
        public ArrayList QueryDrugBillClassList()
        {
            string strSelect = "";  //���ȫ����ҩ��������Ϣ��SELECT���
            string strOrder = "";  //���ȫ����ҩ��������Ϣ��ORDER���

            //ȡSELECT���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugBillClass", ref strSelect) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugBillClass�ֶ�!";
                return null;
            }

            //ȡORDER�������
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugBillClass.Order", ref strOrder) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugBillClass.Order�ֶ�!";
                return null;
            }

            //����SQL���ȡ��ҩ���������鲢��������
            return this.myGetDrugBillClass(strSelect + strOrder);
        }

        #endregion

        #endregion ��ҩ������

        #region ��ҩ��������ϸ

        /// <summary>
        /// ���ݰ�ҩ����������ð�ҩ��������ϸ
        /// </summary>
        /// <param name="drugBillClassCode">�������</param>
        /// <param name="column">ָ��ȥ���ظ���������</param>
        /// <returns>�ɹ����ذ�ҩ��������ϸ ʧ�ܷ���null</returns>
        public ArrayList QueryDrugBillList(string drugBillClassCode, string column)
        {
            ArrayList al = new ArrayList();
            string strSelect = "";  //���ȫ����ҩ��������Ϣ��SELECT���

            //��ʱʹ�ù̶���sql��䣬�Ժ���б仯
            strSelect = "SELECT DISTINCT " + column + " FROM PHA_STO_BILLLIST WHERE BILLCLASS_CODE = '" + drugBillClassCode + "'";
            DrugBillList info;            //���������еİ�ҩ��������Ϣ

            if (this.ExecQuery(strSelect) == -1)
            {
                this.Err = "ȡ��ҩ��������ϸʱ����" + this.Err;
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    info = new DrugBillList();
                    try
                    {
                        info.ID = this.Reader[0].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Err = "��ð�ҩ��������ϸ��Ϣ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    al.Add(info);
                }

                return al;
            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "��ð�ҩ��������Ϣʱ��ִ��SQL������myGetDrugBillClass" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        #region ��������ɾ���Ĳ���

        /// <summary>
        /// �����ҩ��������ϸ��¼
        /// </summary>
        /// <param name="info">��ҩ��������ϸʵ��</param>
        /// <returns>1�ɹ���-1ʧ��</returns>
        public int InsertDrugBillList(DrugBillList info)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.InsertDrugBillList", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = myGetParmDrugBillList(info);  //ȡ�����б�
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���ݰ�ҩ���������,ɾ����ҩ��������ϸ
        /// </summary>
        /// <param name="ID">��ҩ���������</param>
        /// <returns>0û�и��µ����ݣ�1�ɹ���-1ʧ��</returns>
        public int DeleteDrugBillList(string ID)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.DeleteDrugBillList", ref strSql) == -1) return -1;

            try
            {
                strSql = string.Format(strSql, ID);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "�����������ȷ��Pharmacy.Item.DeleteDrugBillList";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���update����insert��ҩ��������ϸ��Ĵ����������
        /// </summary>
        /// <param name="DrugBillList">���������</param>
        /// <returns>�ɹ������ַ����������� ʧ�ܷ���null</returns>
        private string[] myGetParmDrugBillList(DrugBillList DrugBillList)
        {
            #region "�ӿ�˵��"
            //��ҩ�������
            //ҽ�����ͱ���
            //ҩƷ�÷�����
            //ҩƷ���ͱ���
            //ҩƷ���ʱ���
            //���ͱ���

            #endregion
            string[] strParm ={
								 DrugBillList.DrugBillClass.ID,
								 DrugBillList.OrderType.ID,
								 DrugBillList.Usage.ID,
								 DrugBillList.DrugType.ID,
								 DrugBillList.DrugQuality.ID,
								 DrugBillList.DosageForm.ID
							 };

            return strParm;
        }

        #endregion

        #region ���²�����ʱû��

        /// <summary>
        /// ���°�ҩ��������ϸ��¼
        /// </summary>
        /// <param name="info">��ҩ��������ϸʵ��</param>
        /// <returns>0û�и��µ����ݣ�1�ɹ���-1ʧ��</returns>
        public int UpdateDrugBillList(DrugBillList info)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugBillList", ref strSql) == -1) return -1;

            try
            {
                string[] strParm = myGetParmDrugBillList(info);  //ȡ�����б�
                strSql = string.Format(strSql, strParm);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���ð�ҩ��������ϸʵ��
        /// ��ɾ���������
        /// </summary>
        /// <param name="info">��ҩ��������ϸʵ��</param>
        /// <returns></returns>
        public int SetDrugBillList(DrugBillList info)
        {
            int parm;
            parm = this.DeleteDrugBillList(info.ID);
            if (parm == -1) return parm;

            return this.InsertDrugBillList(info);

        }

        #endregion

        #endregion ��ҩ��������ϸ

        #region ��ҩ̨

        #region �ڲ�ʹ��

        /// <summary>
        /// ȡ��ҩ̨��ˮ��
        /// </summary>
        /// <returns>"-1"����oterhs �ɹ�</returns>
        public string GetDrugControlNO()
        {
            #region //��ʽ��ʱ����� {35DE4ACA-F66C-47fd-845C-5AFF253731F7} wbo 2010-08-23
            //return this.GetSysDateTime("YYMMDDHH24MISS");
            string conNO = "-1";
            try
            {
                conNO = this.GetDateTimeFromSysDateTime().ToString("yyMMddHHmmss");
            }
            catch (Exception e)
            {
                conNO = "-1";
            }
            return conNO;
            #endregion
        }

        /// <summary>
        /// ���ݿ��ұ���Ͱ�ҩ���ʣ�ȡ��ҩ̨��Ϣ
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="drugAtr">��ҩ̨����</param>
        /// <returns>�ɹ����ذ�ҩ̨���� ʧ�ܷ���null</returns>
        public FS.HISFC.Models.Pharmacy.DrugControl GetDrugControl(string deptCode, FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute drugAtr)
        {
            string strSQL = "";  //���ĳһ����ȫ����ҩ̨��Ϣ��SQL���

            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugControl", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugControl�ֶ�!";
                return null;
            }

            //����SQL���ȡ��ҩ̨���鲢���������е�������¼
            try
            {
                string[] parm = { deptCode, drugAtr.ToString() };
                strSQL = string.Format(strSQL, parm);
                //ȡ��ҩ̨����
                ArrayList al = this.myGetDrugControl(strSQL);
                //���û��ȡ�����ݣ��򷵻���ʵ��
                if (al.Count == 0)
                {
                    DrugControl info = new DrugControl();
                    info.Dept.ID = deptCode;
                    info.DrugAttribute.ID = drugAtr;
                    return info;
                }
                //���������е�������¼
                return al[0] as DrugControl;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ���ݿ��ұ��룬ȡ�����ҵ�ȫ����ҩ̨�б�
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <returns>��ҩ̨����</returns>
        public ArrayList QueryDrugControlList(string deptCode)
        {
            ArrayList al = new ArrayList();
            string strSQL = "";  //���ĳһ����ȫ����ҩ̨��Ϣ��SELECT���

            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugControlList", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugControlList�ֶ�!";
                return null;
            }

            strSQL = string.Format(strSQL, deptCode);
            //ȡ��ҩ̨�����б�			
            return this.myGetDrugControl(strSQL);
        }

        /// <summary>
        /// ���ݰ�ҩ̨���룬ȡ�˰�ҩ̨�е�ȫ����ϸ
        /// </summary>
        /// <param name="drugControlCode">��ҩ̨����</param>
        /// <returns>��ҩ����������</returns>
        public ArrayList QueryDrugControlDetailList(string drugControlCode)
        {
            ArrayList al = new ArrayList();
            string strSelect = "";

            //ȡSELECT���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugControlDetailList", ref strSelect) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugControlDetailList�ֶ�!";
                return null;
            }

            DrugBillClass info;   //��ҩ������ʵ��			

            strSelect = string.Format(strSelect, drugControlCode);
            if (this.ExecQuery(strSelect) == -1)
            {
                this.Err = "ȡ��ҩ̨��ϸ�б�ʱ����" + this.Err;
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    info = new DrugBillClass();
                    try
                    {
                        info.ID = this.Reader[0].ToString();                          //��ҩ���������
                        info.Name = this.Reader[1].ToString();                        //��ҩ����������
                        info.PrintType.ID = this.Reader[2].ToString();                //��ӡ����
                        info.IsValid = NConvert.ToBoolean(this.Reader[3].ToString()); //�Ƿ���Ч
                        info.Memo = this.Reader[4].ToString();                        //��ע
                    }
                    catch (Exception ex)
                    {
                        this.Err = "��ð�ҩ̨��ϸ�б�ʱ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    al.Add(info);
                }
                return al; ;
            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "��ð�ҩ̨��ϸ�б�ʱ��ִ��SQL������myGetDrugBillClass" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        #endregion

        #region ��������ɾ���Ĳ���

        /// <summary>
        /// ���ҩ̨���в���һ����¼
        /// </summary>
        /// <param name="info">��ҩ̨ʵ��</param>
        /// <returns>1�ɹ���-1ʧ��</returns>
        public int InsertDrugControl(DrugControl info)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.InsertDrugControl", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = myGetParmDrugControl(info);  //ȡ�����б�
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ɾ��һ����ҩ̨�����ݿ����Ƕ�����¼��
        /// </summary>
        /// <param name="ID">��ҩ̨����</param>
        /// <returns>0û�и��µ����ݣ�1�ɹ���-1ʧ��</returns>
        public int DeleteDrugControl(string ID)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.DeleteDrugControl", ref strSql) == -1) return -1;

            try
            {
                strSql = string.Format(strSql, ID);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "�����������ȷ��Pharmacy.Item.DrugControl";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���update����insert��ҩ��������ϸ��Ĵ����������
        /// </summary>
        /// <param name="drugControl">���������</param>
        /// <returns>�ɹ������ַ������� ʧ�ܷ���null</returns>
        private string[] myGetParmDrugControl(DrugControl drugControl)
        {
            #region "�ӿ�˵��"
            //��ҩ̨����
            //��ҩ̨����
            //��ҩ���������
            //��ҩ����������
            //��ҩ����
            //���ұ���

            #endregion
            string[] strParm ={
								 drugControl.ID,                         //��ҩ̨����
								 drugControl.Name,                       //��ҩ̨����
								 drugControl.DrugBillClass.ID,           //��ҩ���������
								 drugControl.DrugBillClass.Name,         //��ҩ����������
								 drugControl.DrugAttribute.ID.ToString(),//��ҩ����
								 drugControl.SendType.ToString(),        //ҽ����������	
								 drugControl.Dept.ID,			         //���ұ���					 
								 this.Operator.ID,                       //����Ա
								 drugControl.Memo,                       //��ע
								 drugControl.ShowLevel.ToString(),        //��ʾ�ȼ���0��ʾ���һ��ܣ�1��ʾ������ϸ��2��ʾ������ϸ
                                 NConvert.ToInt32(drugControl.IsAutoPrint).ToString(), //�Ƿ��Զ���ӡ
                                 NConvert.ToInt32(drugControl.IsPrintLabel).ToString(),//��Ժ��ҩ�Ƿ��ӡ�����ǩ
                                 NConvert.ToInt32(drugControl.IsBillPreview).ToString(),//��ҩ���Ƿ���ҪԤ��
                                 drugControl.ExtendFlag,
                                 drugControl.ExtendFlag1
							 };

            return strParm;
        }

        /// <summary>
        /// ȡ��ҩ̨��Ϣ
        /// </summary>
        /// <param name="strSQL">ȡ��ҩ̨��SQL���</param>
        /// <returns>�ɹ����ذ�ҩ̨���� ʧ�ܷ���null</returns>
        private ArrayList myGetDrugControl(string strSQL)
        {
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "ȡ��ҩ̨ʱ����" + this.Err;
                return null;
            }
            ArrayList al = new ArrayList();
            try
            {
                DrugControl info;   //��ҩ̨ʵ��	
                while (this.Reader.Read())
                {
                    info = new DrugControl();
                    info.ID = this.Reader[0].ToString();                 //��ҩ̨����
                    info.Name = this.Reader[1].ToString();               //��ҩ̨����
                    info.DrugAttribute.ID = this.Reader[2].ToString();   //��ҩ����
                    info.SendType = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString());//ҽ����������
                    info.Dept.ID = this.Reader[4].ToString();            //���ұ���
                    info.Memo = this.Reader[5].ToString();               //��ע
                    info.ShowLevel = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[6].ToString());//��ʾ�ȼ���0��ʾ���һ��ܣ�1��ʾ������ϸ��2��ʾ������ϸ
                    info.IsAutoPrint = NConvert.ToBoolean(this.Reader[7].ToString());
                    info.IsPrintLabel = NConvert.ToBoolean(this.Reader[8].ToString());
                    info.IsBillPreview = NConvert.ToBoolean(this.Reader[9].ToString());
                    info.ExtendFlag = this.Reader[10].ToString();
                    info.ExtendFlag1 = this.Reader[11].ToString();

                    al.Add(info);
                }
            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "��ð�ҩ̨ʱ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        #endregion

        #endregion ��ҩ̨

        #region ��ҩ֪ͨ

        #region �ڲ�ʹ��

        /// <summary>
        /// ���ĳһ������ҵ�δ��ҩ֪ͨ�б�
        /// </summary>
        /// <param name="sendDeptCode">������ұ���</param>
        /// <returns>�ɹ����ذ�ҩ֪ͨ��Ϣ ʧ�ܷ���null</returns>
        public ArrayList QueryDrugMessageList(string sendDeptCode)
        {
            string strSQL = "";    //���ĳһ������ҵ�ȫ����ҩ֪ͨ�б��SELECT���

            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugMessageList.BySendDept", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugMessageList.BySendDept�ֶ�!";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, sendDeptCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "|Pharmacy.DrugStore.GetDrugMessageList.BySendDept";
                return null;
            }
            return myGetDrugMessage(strSQL);
        }

        /// <summary>
        /// ���ĳһ������ҵ�ȫ����ҩ֪ͨ�б�
        /// </summary>
        /// <param name="sendDeptCode">������ұ���</param>
        /// <returns>�ɹ����ذ�ҩ֪ͨ�б� ʧ�ܷ���null</returns>
        public ArrayList QueryAllDrugMessageList(string sendDeptCode)
        {
            string strSQL = "";    //���ĳһ������ҵ�ȫ����ҩ֪ͨ�б��SELECT���

            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.GetAllDrugMessageList", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetAllDrugMessageList�ֶ�!";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, sendDeptCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "|Pharmacy.DrugStore.GetAllDrugMessageList";
                return null;
            }

            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "ȡ��ҩ֪ͨ�б�ʱ����" + this.Err;
                return null;
            }
            try
            {
                DrugMessage info;   //��ҩ֪ͨʵ��		
                while (this.Reader.Read())
                {
                    info = new DrugMessage();
                    try
                    {
                        info.StockDept.ID = this.Reader[0].ToString();          //���Ϳ��ұ���
                        info.StockDept.Name = this.Reader[1].ToString();          //���Ϳ�������
                        info.DrugBillClass.ID = this.Reader[2].ToString();          //��ҩ���������
                        info.DrugBillClass.Name = this.Reader[3].ToString();          //��ҩ����������
                        info.SendType = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[4].ToString());      //��ҩ����1-���а�ҩ2-��ʱ��ҩ
                    }
                    catch (Exception ex)
                    {
                        this.Err = "��ð�ҩ֪ͨ��Ϣ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    al.Add(info);
                }
                return al;
            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "��ð�ҩ֪ͨ��Ϣʱ��ִ��SQL������myGetDrugBillClass" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// ���ĳһ��ҩ̨��ȫ����ҩ֪ͨ�б�
        /// SendType=1���У�2��ʱ
        /// ��SendType��0ʱ����ʾȫ�����͵İ�ҩ֪ͨ��
        /// </summary>
        /// <param name="drugControl">��ҩ̨</param>
        /// <returns>�ɹ����ذ�ҩ֪ͨ���� ʧ�ܷ���null</returns>
        public ArrayList QueryDrugMessageList(DrugControl drugControl)
        {
            //���û��ָ�����Ϳ��ң���ȡȫ�����Ϳ��ҵ�֪ͨ
            string strSQL = "";    //���ĳһ��ҩ̨����ҩ̨���п�����Ϣ����ȫ����ҩ֪ͨ�б��SELECT���

            #region ȡ�����Ұ�ҩ��
            //ȡSQL���
            //			if (drugControl.ID =="P") {
            //				if (this.GetSQL("Pharmacy.DrugStore.GetDrugMessageList.ByOPR",ref strSQL) == -1) {
            //					this.Err="û���ҵ�Pharmacy.DrugStore.GetDrugMessageList.ByOPR�ֶ�!";
            //					return null;
            //				}
            //				try {
            //					string[] strParm={drugControl.Dept.ID};
            //					strSQL = string.Format(strSQL, strParm);
            //				}
            //				catch(Exception ex) {
            //					this.ErrCode=ex.Message;
            //					this.Err=ex.Message + "|Pharmacy.DrugStore.GetDrugMessageList.ByOPR";
            //					return null;
            //				}
            //			}
            #endregion

            if (this.GetSQL("Pharmacy.DrugStore.GetDrugMessageList.ByDrugControl", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugMessageList.ByDrugControl�ֶ�!";
                return null;
            }
            try
            {
                string[] strParm = { drugControl.ID };
                strSQL = string.Format(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "|Pharmacy.DrugStore.GetDrugMessageList.ByDrugControl";
                return null;
            }
            return myGetDrugMessage(strSQL);
        }

        /// <summary>
        /// ���ĳһ��ҩ֪ͨ����ϸ�б�;
        /// </summary>
        /// <param name="drugMessage">��ҩ֪ͨ</param>
        /// <returns>�ɹ����ذ�ҩ֪ͨ��Ϣ ʧ�ܷ���null</returns>
        public ArrayList QueryDrugMessageList(DrugMessage drugMessage)
        {

            string strSQL = "";    //���ĳһ��ҩ֪ͨ����ϸ�б��SQL���

            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugMessageList.ByDrugMessage", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugMessageList.ByDrugMessage�ֶ�!";
                return null;
            }
            try
            {
                string[] strParm ={
									 drugMessage.StockDept.ID, 
									 drugMessage.DrugBillClass.ID, 
									 drugMessage.SendType.ToString()
								 };
                strSQL = string.Format(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "|Pharmacy.DrugStore.GetDrugMessageList.ByDrugMessage";
                return null;
            }
            return myGetDrugMessage(strSQL);
        }

        /// <summary>
        /// �ɹ����ذ�ҩ֪ͨ��Ϣ
        /// </summary>
        /// <param name="drugControlID">��ҩ̨����</param>
        /// <param name="drugMessage">��ҩ֪ͨ</param>
        /// <returns>�ɹ����ذ�ҩ֪ͨ��Ϣ ʧ�ܷ���null</returns>
        public ArrayList QueryDrugBillList(string drugControlID, DrugMessage drugMessage)
        {
            string strSQL = "";
            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugBillList.ByDept", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugBillList.ByDept�ֶ�!";
                return null;
            }
            try
            {
                string[] strParm ={
									 drugControlID,
									 drugMessage.ApplyDept.ID, 
									 drugMessage.StockDept.ID, 
									 drugMessage.SendType.ToString()
								 };
                strSQL = string.Format(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "|Pharmacy.DrugStore.GetDrugBillList.ByDept";
                return null;
            }
            return myGetDrugMessage(strSQL);
        }

        /// <summary>
        /// ���°�ҩ֪ͨ״̬
        /// </summary>
        /// <param name="drugDeptCode">��ҩҩ��</param>
        /// <param name="deptCode">�������</param>
        /// <param name="billClassCode">��ҩ�����</param>
        /// <param name="sendType">�������� 1 ���� 2 ��ʱ</param>
        /// <param name="state">֪ͨ״̬ 0 ֪ͨ 1 �Ѱ�</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int UpdateDrugMessage(string drugDeptCode, string deptCode, string billClassCode, int sendType, string state)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugMessageState", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, drugDeptCode, deptCode, billClassCode, sendType.ToString(), state);       //
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ��" + "Pharmacy.DrugStore.UpdateDrugMessageState"; ;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        #endregion

        #region ��������ɾ���Ĳ���

        /// <summary>
        /// ���ҩ̨���в���һ����¼
        /// </summary>
        /// <param name="drugMessage">��ҩ̨ʵ��</param>
        /// <returns>1�ɹ���-1ʧ��</returns>
        public int InsertDrugMessage(DrugMessage drugMessage)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.InsertDrugMessage", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = myGetParmDrugMessage(drugMessage);  //ȡ�����б�
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + "Pharmacy.DrugStore.InsertDrugMessage";
                this.ErrCode = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���ҩ֪ͨ���в���һ����¼
        /// </summary>
        /// <param name="drugMessage">��ҩ֪ͨʵ��</param>
        /// <returns>1�ɹ���-1ʧ��</returns>
        public int UpdateDrugMessage(DrugMessage drugMessage)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugMessage", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = myGetParmDrugMessage(drugMessage);  //ȡ�����б�
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ��" + "Pharmacy.DrugStore.UpdateDrugMessage"; ;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ɾ��һ����ҩ֪ͨ
        /// </summary>
        /// <param name="ID">��ҩ֪ͨ��ˮ��</param>
        /// <returns>0û�и��µ����ݣ�1�ɹ���-1ʧ��</returns>
        public int DeleteDrugMessage(string ID)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.DeleteDrugMessage", ref strSql) == -1) return -1;

            try
            {
                strSql = string.Format(strSql, ID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "�����������ȷ��Pharmacy.Item.DeleteDrugMessage";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���ð�ҩ֪ͨ
        /// ��ִ�и��²�����������ݿ���û�м�¼��ִ�в������
        /// </summary>
        /// <param name="drugMessage">��ҩ֪ͨ</param>
        /// <returns></returns>
        public int SetDrugMessage(DrugMessage drugMessage)
        {
            //��ִ�и��²���
            int parm = UpdateDrugMessage(drugMessage);
            if (parm == 0)
            {
                //������ݿ���û�м�¼��ִ�в������
                parm = InsertDrugMessage(drugMessage);
            }
            return parm;
        }

        /// <summary>
        /// ���update����insert��ҩ֪ͨ�����������
        /// </summary>
        /// <param name="drugMessage">��ҩ֪ͨ</param>
        /// <returns>�ɹ������ַ�����������  ʧ�ܷ���null</returns>
        private string[] myGetParmDrugMessage(DrugMessage drugMessage)
        {
            string[] strParm ={
								 drugMessage.ApplyDept.ID,         //���һ��߲�������
								 drugMessage.ApplyDept.Name,       //���һ��߲�������
								 drugMessage.DrugBillClass.ID,    //��ҩ���������
								 drugMessage.DrugBillClass.Name,  //��ҩ����������
								 drugMessage.SendType.ToString(), //��������0ȫ��,1-����,2-��ʱ
								 drugMessage.SendFlag.ToString(), //״̬0-֪ͨ,1-�Ѱ�
								 drugMessage.StockDept.ID,		  //���ұ���					 
								 this.Operator.ID,                //����Ա����				 
								 this.Operator.Name,              //����Ա����
			};

            return strParm;
        }

        /// <summary>
        /// ����SQL��䣬ȡ��ҩ֪ͨ����
        /// </summary>
        /// <param name="strSQL">��ѯSQL���</param>
        /// <returns>�ɹ����ذ�ҩ֪ͨ���� ʧ�ܷ���null</returns>
        private ArrayList myGetDrugMessage(string strSQL)
        {
            ArrayList al = new ArrayList();

            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "ȡ��ҩ֪ͨ�б�ʱ����" + this.Err;
                return null;
            }
            try
            {
                DrugMessage info;   //��ҩ֪ͨʵ��		
                while (this.Reader.Read())
                {
                    info = new DrugMessage();
                    try
                    {
                        info.ApplyDept.ID = this.Reader[0].ToString();          //���Ϳ��ұ���
                        info.ApplyDept.Name = this.Reader[1].ToString();          //���Ϳ�������
                        info.DrugBillClass.ID = this.Reader[2].ToString();          //��ҩ���������
                        info.DrugBillClass.Name = this.Reader[3].ToString();          //��ҩ����������
                        info.SendType = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[4].ToString());      //��ҩ����1-���а�ҩ2-��ʱ��ҩ
                        info.SendTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[5].ToString()); //֪ͨʱ��
                        info.SendFlag = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[6].ToString());      //����״̬0��֪ͨ��1���Ѱ�ҩ
                        info.StockDept.ID = this.Reader[7].ToString();                 //��ҩ���ұ���				 
                        info.ID = this.Reader[8].ToString();                 //����Ա����				 
                        info.Name = this.Reader[9].ToString();                 //����Ա����
                    }
                    catch (Exception ex)
                    {
                        this.Err = "��ð�ҩ֪ͨ��Ϣ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    al.Add(info);
                }
                return al;

            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "��ð�ҩ֪ͨ��Ϣʱ��ִ��SQL������myGetDrugBillClass" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        #endregion

        #endregion ��ҩ֪ͨ

        #region ��ҩ��������

        /// <summary>
        /// ȡĳһ�������룬ĳһĿ�걾����δ��׼��ĳһ��ҩ�������е������б�
        /// ���磬ĳһҩ���鿴ĳһ���ң���������ĳһ�Ű�ҩ�������еĴ���ҩ��Ϣ	
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="objectDeptCode">�������</param>
        /// <param name="drugBillClass">��ҩ���������</param>
        /// <returns>�ɹ����ذ�ҩ�������� ʧ�ܷ���null</returns>
        public ArrayList QueryDrugList(string deptCode, string objectDeptCode, string drugBillClass)
        {
            string strSelect = "";  //��ð�ҩ��Ϣ��SELECT���
            string strWhere = "";  //���ĳһ���Ұ�ҩ��Ϣ��Ϣ��WHERE�������

            //ȡSELECT���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugListByClass", ref strSelect) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugListByClass�ֶ�!";
                return null;
            }

            //ȡWHERE�������
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugListByClass.Where", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugListByClass.Where�ֶ�!";
                return null;
            }

            //����SQL���ȡҩƷ�����鲢��������
            return this.myGetDrugList(strSelect + " " + strWhere);
        }

        /// <summary>
        /// ȡҩƷ������Ϣ�б�������һ�����߶���ҩƷ��¼
        /// ˽�з����������������е���
        /// </summary>
        /// <param name="SQLString">SQL���</param>
        /// <returns>��ҩ����������</returns>
        private ArrayList myGetDrugList(string SQLString)
        {
            ArrayList al = new ArrayList();            //���ڷ���ҩƷ��Ϣ������
            FS.HISFC.Models.Pharmacy.DrugControl DrugList;   //���������еİ�ҩ��Ϣ�࣬ÿ����ѭ���д���ʵ����

            this.ExecQuery(SQLString);
            try
            {
                while (this.Reader.Read())
                {
                    DrugList = new FS.HISFC.Models.Pharmacy.DrugControl();
                    #region "�ӿ�˵��"

                    #endregion

                    try
                    {
                        DrugList.ID = this.Reader[0].ToString();


                    }
                    catch (Exception ex)
                    {
                        this.Err = "��ð�ҩ��Ϣ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    al.Add(DrugList);
                }

                return al;
            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "���ҩƷ������Ϣʱ��ִ��SQL������" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return al;
            }
            finally
            {
                this.Reader.Close();
            }

        }

        #endregion

        #region ���﷢ҩ������ҩ̨ά��

        #region ��������ɾ���Ĳ���

        /// <summary>
        /// ���Update��Insert�����ն�ά���Ĵ����������
        /// </summary>
        /// <param name="drugTerminal">�����ն�ʵ��</param>
        /// <returns>�ɹ������ַ������顢ʧ�ܷ���null</returns>
        protected string[] myGetParmDrugTerminal(DrugTerminal drugTerminal)
        {
            //����ʱ����sql��ͨ��sysdateȡ��
            string[] strParm = {
								   drugTerminal.ID,							              //0 �ն˱��
								   drugTerminal.Name,							          //1 �ն�����
								   drugTerminal.Dept.ID,								  //2 �����ⷿ���
								   drugTerminal.TerminalType.GetHashCode().ToString(),	  //3 �ն���� 0 ��ҩ���� 1 ��ҩ̨
								   drugTerminal.ReplaceTerminal.ID,						  //4 ����ն˺�
								   NConvert.ToInt32(drugTerminal.IsClose).ToString(),	  //5 �Ƿ�ر� 0 ���� 1 �ر�
								   NConvert.ToInt32(drugTerminal.IsAutoPrint).ToString(), //6 �Ƿ��Զ���ӡ 0 �� 1 �Զ���ӡ
								   drugTerminal.RefreshInterval1.ToString(),			  //7 ����ˢ��ʱ����
								   drugTerminal.RefreshInterval2.ToString(),			  //8 ��ӡ/��ʾ ʱ����
								   drugTerminal.TerminalProperty.GetHashCode().ToString(),						  //9 �ն����� 0 ��ͨ 1 ר�� 2 ����
								   drugTerminal.AlertQty.ToString(),					  //10 ������
								   drugTerminal.ShowQty.ToString(),						  //11 ��ʾ����
								   drugTerminal.SendWindow.ID,								  //12 ��ҩ���ڱ��
								   this.Operator.ID,									  //13 ����Ա
								   drugTerminal.Memo,									  //14 ��ע
                                   ((int)drugTerminal.TerimalPrintType).ToString()
							   };
            return strParm;
        }

        /// <summary>
        /// ��ȡ�����ն���Ϣ
        /// </summary>
        /// <param name="StrSQL">��ѯ��sql���</param>
        /// <returns>�ɹ����������ն�ʵ�����顢ʧ�ܷ���null</returns>
        protected ArrayList myGetDrugTerminal(string StrSQL)
        {
            ArrayList al = new ArrayList();
            if (this.ExecQuery(StrSQL) == -1)
            {
                this.Err = "��ȡ�����ն���Ϣʱ����" + this.Err;
                return null;
            }
            try
            {
                DrugTerminal info;
                while (this.Reader.Read())
                {
                    info = new DrugTerminal();

                    info.ID = this.Reader[0].ToString();							//0 �ն˱���
                    info.Name = this.Reader[1].ToString();							//1 �ն�����
                    info.Dept.ID = this.Reader[2].ToString();								//2 �����ⷿ����
                    info.TerminalType = (EnumTerminalType)NConvert.ToInt32(this.Reader[3].ToString());							//3 �ն����
                    info.TerminalProperty = (EnumTerminalProperty)NConvert.ToInt32(this.Reader[4].ToString());						//4 �ն�����
                    info.ReplaceTerminal.ID = this.Reader[5].ToString();							//5 ����ն˺�
                    info.IsClose = NConvert.ToBoolean(this.Reader[6].ToString());			//6 �Ƿ�ر�
                    info.IsAutoPrint = NConvert.ToBoolean(this.Reader[7].ToString());		//7 �Ƿ��Զ���ӡ
                    info.RefreshInterval1 = NConvert.ToDecimal(this.Reader[8].ToString());	//8 ����ˢ��ʱ����
                    info.RefreshInterval2 = NConvert.ToDecimal(this.Reader[9].ToString());	//9 ��ӡ/��ʾ ˢ��ʱ����
                    info.AlertQty = NConvert.ToInt32(this.Reader[10].ToString());			//10 ������
                    info.ShowQty = NConvert.ToInt32(this.Reader[11].ToString());			//11 ��ʾ����
                    info.SendWindow.ID = this.Reader[12].ToString();							//12 ��ҩ���ڱ���
                    info.Oper.ID = this.Reader[13].ToString();								//13 ����Ա
                    info.Oper.OperTime = NConvert.ToDateTime(this.Reader[14].ToString());		//14 ����ʱ��
                    info.Memo = this.Reader[15].ToString();									//15 ��ע
                    info.SendQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[16].ToString());
                    info.DrugQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[17].ToString());
                    info.Average = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[18].ToString());
                    if (this.Reader.FieldCount > 18)
                    {
                        info.TerimalPrintType = (EnumClinicPrintType)NConvert.ToInt32(this.Reader[19]);
                    }

                    al.Add(info);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��������ն���Ϣʱ��ִ��SQL������" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }

        /// <summary>
        /// �������ն˱��ڲ�������
        /// </summary>
        /// <param name="drugTerminal">�����ն�ʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int InsertDrugTerminal(DrugTerminal drugTerminal)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.InsertDrugTerminal", ref strSql) == -1) return -1;
            try
            {
                //���������ն˺�
                drugTerminal.ID = this.GetSequence("Pharmacy.Constant.GetNewCompanyID");
                if (drugTerminal.SendWindow == null || drugTerminal.SendWindow.ID == "")
                    drugTerminal.SendWindow.ID = drugTerminal.ID;
                string[] strParm = this.myGetParmDrugTerminal(drugTerminal);  //ȡ�����б�
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + "Pharmacy.DrugStore.InsertDrugTerminal";
                this.ErrCode = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���������ն�ʵ��
        /// </summary>
        /// <param name="drugTerminal">�����ն�ʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1 �޸��·���0</returns>
        public int UpdateDrugTerminal(DrugTerminal drugTerminal)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugTerminal", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = this.myGetParmDrugTerminal(drugTerminal);  //ȡ�����б�
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ��" + "Pharmacy.DrugStore.UpdateDrugTerminal"; ;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ɾ��һ�������ն�����
        /// </summary>
        /// <param name="terminalCode">�ն˱��</param>
        /// <returns>�޸��·���0 �ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int DeleteDrugTerminal(string terminalCode)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.DeleteDrugTerminal", ref strSql) == -1) return -1;

            try
            {
                strSql = string.Format(strSql, terminalCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "�����������ȷ��Pharmacy.Item.DeleteDrugTerminal";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���������ն�ʵ����Ϣ���������������
        /// </summary>
        /// <param name="drugTerminal">�����ն�ʵ��</param>
        /// <returns>�ɹ�����1��ʧ�ܷ��أ�1</returns>
        public int SetDrugTerminal(DrugTerminal drugTerminal)
        {
            int parm;
            parm = this.UpdateDrugTerminal(drugTerminal);
            if (parm == 0)
                parm = this.InsertDrugTerminal(drugTerminal);
            return parm;
        }

        #endregion

        #region �ڲ�ʹ��

        /// <summary>
        /// ���ĳ����ĳ���������ն��б�
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="terminalType">�ն����� 0 ��ҩ�� 1 ��ҩ̨</param>
        /// <returns>�ɹ�����DrugTerminal��ArrayList���飬ʧ�ܷ���null</returns>
        public ArrayList QueryDrugTerminalByDeptCode(string deptCode, string terminalType)
        {
            string strSQL = "", strWhere = "";

            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminal", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugTerminal�ֶ�!";
                return null;
            }
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminal.ByDeptCode", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugTerminal.ByDeptCode�ֶ�!";
                return null;
            }
            try
            {
                strSQL = strSQL + strWhere;
                strSQL = string.Format(strSQL, deptCode, terminalType);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.GetDrugTerminal.ByDeptCode";
                return null;
            }
            return this.myGetDrugTerminal(strSQL);
        }

        /// <summary>
        /// ���ĳ����ĳ���������ն��б� �������ⷿ����
        /// </summary>
        /// <param name="terminalType">�ն����� 0 ��ҩ�� 1 ��ҩ̨</param>
        /// <returns>�ɹ�����DrugTerminal��ArrayList���飬ʧ�ܷ���null</returns>
        public ArrayList QueryDrugTerminalByTerminalType(string terminalType)
        {
            string strSQL = "", strWhere = "";

            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminal", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugTerminal�ֶ�!";
                return null;
            }
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminal.ByTerminalType", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugTerminal.ByTerminalType�ֶ�!";
                return null;
            }
            try
            {
                strSQL = strSQL + strWhere;
                strSQL = string.Format(strSQL, terminalType);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.GetDrugTerminal.ByTerminalType";
                return null;
            }
            return this.myGetDrugTerminal(strSQL);
        }

        /// <summary>
        /// �����ն˱�š����ұ����������ն���Ϣ
        /// </summary>
        /// <param name="terminalCode">�ն˱��</param>
        /// <returns>DrugTerminal��ʧ�ܻ�û�ҵ�����null</returns>
        public DrugTerminal GetDrugTerminalById(string terminalCode)
        {
            string strSQL = "", strWhere = "";

            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminal", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugTerminal�ֶ�!";
                return null;
            }
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminal.ById", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugTerminal.ById�ֶ�!";
                return null;
            }
            try
            {
                strSQL = strSQL + strWhere;
                strSQL = string.Format(strSQL, terminalCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.GetDrugTerminal.ById";
                return null;
            }
            ArrayList al = this.myGetDrugTerminal(strSQL);
            if (al == null) return null;

            if (al.Count == 0) return new DrugTerminal();

            return al[0] as DrugTerminal;
        }

        /// <summary>
        /// ���ݷ�ҩ���ڱ����ȡ���õ�һ����ҩ̨����
        /// </summary>
        /// <param name="sendWindow">��ҩ���ڱ���</param>
        /// <returns>�ɹ����ض�Ӧ����ҩ̨��Ϣ ʧ�ܷ���null</returns>
        public DrugTerminal GetDrugTerminalBySendWindow(string sendWindow)
        {
            string strSQL = "", strWhere = "";

            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminal", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugTerminal�ֶ�!";
                return null;
            }
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminal.BySendWindow", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugTerminal.BySendWindow�ֶ�!";
                return null;
            }
            try
            {
                strSQL = strSQL + strWhere;
                strSQL = string.Format(strSQL, sendWindow);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.GetDrugTerminal.BySendWindow";
                return null;
            }
            ArrayList al = this.myGetDrugTerminal(strSQL);
            if (al == null) return null;

            if (al.Count == 0) return new DrugTerminal();

            return al[0] as DrugTerminal;
        }

        /// <summary>
        /// �����ն˱�����ҩ̨��Ϣ �������ҩ̨�ر� ��ѭ����������ն���Ϣ
        /// </summary>
        /// <param name="terminalCode">��ҩ�ն˱���</param>
        /// <returns>�ɹ�����δ�رյ���ҩ�ն� ʧ�ܷ���null ��������������ҩ�ն˷��ؿ�ʵ��</returns>
        public DrugTerminal GetDrugTerminal(string terminalCode)
        {
            FS.HISFC.Models.Pharmacy.DrugTerminal info = null;
            info = this.GetDrugTerminalById(terminalCode);
            if (info == null)
                return null;
            if (info.ID != "")
            {
                while (info.IsClose)
                {
                    if (info.ReplaceTerminal.ID == null || info.ReplaceTerminal.ID == "")
                    {
                        info = new DrugTerminal();
                        break;
                    }
                    info = this.GetDrugTerminalById(info.ReplaceTerminal.ID);
                    if (info == null || info.ID == "")
                        break;
                    //��ֹѭ������
                    if (info.ID == terminalCode)
                    {
                        if (info.IsClose)
                            info = new DrugTerminal();
                        break;
                    }
                }
            }
            return info;
        }

        /// <summary>
        /// �жϸ��ն��Ƿ�Ϊ�����ն˵�����ն�
        /// </summary>
        /// <param name="terminalCode">�ն˱���</param>
        /// <returns>��Ϊ����ն˷���1 ���򷵻�0 ������-1</returns>
        public int IsReplaceFlag(string terminalCode)
        {
            string strSQL = "", strWhere = "";

            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminal", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugTerminal�ֶ�!";
                return -1;
            }
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminal.IsReplaceFlag", ref strWhere) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugTerminal.IsReplaceFlag�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = strSQL + strWhere;
                strSQL = string.Format(strSQL, terminalCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.IsReplaceFlag";
                return -1;
            }
            ArrayList al = this.myGetDrugTerminal(strSQL);

            if (al == null) return -1;

            if (al.Count == 0)		//���������ն˵�����ն�
                return 0;
            else					//Ϊ�����ն˵�����ն�
                return 1;
        }

        /// <summary>
        /// �Դ��������� �����ѷ��͡�����ҩ�����ִ�����Ϣ
        /// </summary>
        /// <param name="terminalCode">�ն˱���</param>
        /// <param name="sendNum">�����ѷ��ʹ���Ʒ����</param>
        /// <param name="drugNum">���մ���ҩ����Ʒ����</param>
        /// <param name="averageNum">���վ��ִ���</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int UpdateTerminalAdjustInfo(string terminalCode, decimal sendNum, decimal drugNum, decimal averageNum)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateTerminalAdjustInfo", ref strSql) == -1)
                return -1;
            try
            {
                strSql = string.Format(strSql, terminalCode, sendNum.ToString(), drugNum.ToString(), averageNum.ToString());
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���ݴ����� �����ѷ��͡�����ҩ��Ϣ ���ϵ�����Ϣʱ����
        /// </summary>
        /// <param name="recipeNo">������</param>
        /// <param name="sendNum">�����ѷ��ʹ���Ʒ����</param>
        /// <param name="drugNum">��Ȼ����ҩ����Ʒ����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int UpdateTerminalAdjustInfo(string recipeNo, decimal sendNum, decimal drugNum)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateTerminalAdjustInfo.1", ref strSql) == -1)
                return -1;
            try
            {
                strSql = string.Format(strSql, recipeNo, sendNum.ToString(), drugNum.ToString());
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����һ���ն� �Ƿ�ر� ״̬
        /// </summary>
        /// <param name="terminalType">�ն���� 0 ��ҩ���� 1 ��ҩ̨</param>
        /// <param name="closeFlag">�ر�״̬ 0 ���� 1 �ر�</param>
        /// <returns>�ɹ�������Ӱ������ ʧ�ܷ���null</returns>
        public int UpdateTerminalCloseFlag(string terminalType, string closeFlag)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateTerminalCloseFlag", ref strSql) == -1)
                return -1;
            try
            {
                strSql = string.Format(strSql, terminalType, closeFlag);
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����һ���ն� �Ƿ�ر� ״̬
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="terminalType">�ն����</param>
        /// <param name="closeFlag">�ر�״̬</param>
        /// <returns>�ɹ�������Ӱ������ ʧ�ܷ���null</returns>
        public int UpdateTerminalCloseFlag(string deptCode, string terminalType, string closeFlag)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDeptTerminalCloseFlag", ref strSql) == -1)
                return -1;
            try
            {
                strSql = string.Format(strSql, deptCode, terminalType, closeFlag);
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ������� Ѱ��������������ҩ�ն�
        /// </summary>
        /// <param name="deptCode">ҩ������</param>
        /// <param name="type">��� 1 �ѷ��͵Ĵ���Ʒ�������ٵ���ҩ̨ 2 ����ҩ�Ĵ���Ʒ�������ٵ���ҩ̨</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public FS.HISFC.Models.Pharmacy.DrugTerminal TerminalStatInfo(string deptCode, string type)
        {
            string strSQL = "", strWhere = "";

            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminal", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugTerminal�ֶ�!";
                return null;
            }

            if (this.GetSQL("Pharmacy.DrugStore.TerminalStatInfo" + "." + type, ref strWhere) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.TerminalStatInfo" + "." + type + "�ֶ�!";
                return null;
            }
            try
            {
                strSQL = strSQL + strWhere;
                strSQL = string.Format(strSQL, deptCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.TerminalStatInfo";
                return null;
            }
            ArrayList al = this.myGetDrugTerminal(strSQL);
            if (al == null) return null;

            if (al.Count == 0) return new DrugTerminal();

            return al[0] as DrugTerminal;
        }

        /// <summary>
        /// �����ն��Ƿ��Դ���δִ�е�����
        /// </summary>
        /// <param name="terminalNO">�ն˱���</param>
        /// <returns></returns>
        public bool IsHaveRecipe(string terminalNO)
        {
            string strSQL = "", strWhere = "";

            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.IsHaveRecipe", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.IsHaveRecipe�ֶ�!";
                return false;
            }
            try
            {
                strSQL = string.Format(strSQL, terminalNO);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.IsHaveRecipe";
                return false;
            }

            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "�����ն��Ƿ��Դ���δִ�е�����" + this.Err;
                return false;
            }

            if (this.Reader.Read())
            {
                this.Reader.Close();

                return true;
            }

            this.Reader.Close();

            return false;
        }

        #endregion

        #endregion

        #region ����������ҩ̨ά��

        #region ��������ɾ���Ĳ���

        /// <summary>
        /// ���update��insert����Ĳ�������
        /// </summary>
        /// <param name="drugSPETerminal">����������ҩ̨��Ϣʵ��</param>
        /// <returns>�ɹ������ַ������顢ʧ�ܷ���null</returns>
        protected string[] myGetParmDrugSPETerminal(DrugSPETerminal drugSPETerminal)
        {
            //����ʱ��ͨ��sql��sysdateȡ��
            string[] strParm = {
								   drugSPETerminal.Terminal.ID,		//0 �ն˱��(��ҩ̨���)
								   drugSPETerminal.ItemType,					//1 ��Ŀ��� 1 ҩƷ 2 ר�� 3 ������� 4 �ض��շѴ���
								   drugSPETerminal.Item.ID,					//2 ��Ŀ����
								   drugSPETerminal.Item.Name,					//3 ��Ŀ����
								   this.Operator.ID,							//4 ����Ա
								   drugSPETerminal.Memo,						//5 ��ע
			};
            return strParm;
        }

        /// <summary>
        /// �������������ҩ̨��Ϣ
        /// </summary>
        /// <param name="StrSQL">ִ�е�SQL���</param>
        /// <returns>�ɹ��������顢ʧ�ܷ���null</returns>
        protected ArrayList myGetDrugSPETerminal(string StrSQL)
        {
            ArrayList al = new ArrayList();
            if (this.ExecQuery(StrSQL) == -1)
            {
                this.Err = "��ȡ����������ҩ̨��Ϣʱ����" + this.Err;
                return null;
            }
            try
            {
                DrugSPETerminal info;
                while (this.Reader.Read())
                {
                    info = new DrugSPETerminal();

                    info.Terminal.ID = this.Reader[0].ToString();
                    info.ItemType = this.Reader[1].ToString();									//1 ��Ŀ���
                    info.Item.ID = this.Reader[2].ToString();									//2 ��Ŀ����
                    info.Item.Name = this.Reader[3].ToString();									//3 ��Ŀ����
                    info.Oper.ID = this.Reader[4].ToString();									//4 ����Ա
                    info.Oper.OperTime = NConvert.ToDateTime(this.Reader[5].ToString());				//5 ����ʱ��
                    info.Memo = this.Reader[6].ToString();										//6 ��ע

                    al.Add(info);
                }
            }
            catch (Exception ex)
            {
                this.Err = "�������������ҩ̨��Ϣʱ��ִ��SQL������" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// ������������ҩ̨��������
        /// </summary>
        /// <param name="drugSPETerminal">����������ҩ̨ʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int InsertDrugSPETerminal(DrugSPETerminal drugSPETerminal)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.InsertDrugSPETerminal", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = this.myGetParmDrugSPETerminal(drugSPETerminal);  //ȡ�����б�
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + "Pharmacy.DrugStore.InsertDrugSPETerminal";
                this.ErrCode = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ��������������ҩ̨����
        /// </summary>
        /// <param name="drugSPETerminal">����������ҩ̨ʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1 �޸��·���0</returns>
        public int UpdateDrugSPETerminal(DrugSPETerminal drugSPETerminal)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugSPETerminal", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = this.myGetParmDrugSPETerminal(drugSPETerminal);  //ȡ�����б�
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ��" + "Pharmacy.DrugStore.UpdateDrugSPETerminal"; ;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ɾ��һ������������ҩ̨��Ϣ
        /// </summary>
        /// <param name="speInfo">������ҩ̨ʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int DeleteDrugSPETerminal(FS.HISFC.Models.Pharmacy.DrugSPETerminal speInfo)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.DeleteDrugSPETerminal", ref strSql) == -1) return -1;

            try
            {
                strSql = string.Format(strSql, speInfo.Terminal.ID, speInfo.ItemType, speInfo.Item.ID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "�����������ȷ��Pharmacy.Item.DeleteDrugSPETerminal";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        #endregion

        #region �ڲ�ʹ��

        /// <summary>
        /// ɾ��ָ��ҩ����ָ�����͵�������ҩ�ն���Ϣ
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="itemType">�ն�����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int DeleteDrugSPETerminal(string deptCode, string itemType)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.DeleteDrugSPETerminal.DeptItemType", ref strSql) == -1) return -1;

            try
            {
                strSql = string.Format(strSql, itemType, deptCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "�����������ȷ��Pharmacy.Item.DeleteDrugSPETerminal.DeptItemType";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ɾ��ĳ���������ҩ̨��Ϣ
        /// </summary>
        /// <param name="itemType">��Ŀ��� 1 ҩƷ��� 2 ר����� 3 ������� 4 �շѴ���</param>
        /// <returns>�ɹ�����ɾ��������Ŀ ʧ�ܷ��أ�1 �޲�������0</returns>
        public int DeleteDrugSPETerminal(string itemType)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.DeleteDrugSPETerminal.ItemType", ref strSql) == -1)
                return -1;
            try
            {
                strSql = string.Format(strSql, itemType);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL��䴫���������!" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ĳ��������������ҩ̨�б�
        /// </summary>
        /// <param name="itemType">��� 1 ҩƷ 2 ר�� 3 ������� 4 �ض��շѴ��� "A"���� </param>
        /// <returns>�ɹ�����DrugSPETerminal��ArrayList���飬ʧ�ܷ���null</returns>
        public ArrayList QueryDrugSPETerminalByType(string itemType)
        {
            string strSQL = "";

            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugSPETerminal.ByType", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugSPETerminal.ByType�ֶ�!";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, itemType);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.GetDrugSPETerminal.ByType";
                return null;
            }
            return this.myGetDrugSPETerminal(strSQL);
        }

        /// <summary>
        /// ĳ���ҡ�ĳ��������������ҩ̨�б� ����Ϊ"A"�����������
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="itemType">���  1 ҩƷ 2 ר�� 3 ������� 4 �ض��շѴ��� "A"����</param>
        /// <returns>�ɹ�����DrugSPETerminal��ArrayList���顢ʧ�ܷ���null</returns>
        public ArrayList QueryDrugSPETerminalByDeptCode(string deptCode, string itemType)
        {
            ArrayList al = this.QueryDrugSPETerminalByType(itemType);
            ArrayList myAl = new ArrayList();
            DrugSPETerminal info;
            for (int i = 0; i < al.Count; i++)
            {
                info = al[i] as DrugSPETerminal;
                info.Terminal = this.GetDrugTerminalById(info.Terminal.ID);
                if (info.Terminal == null) return null;

                if (info.Terminal.Dept.ID == deptCode)
                    myAl.Add(info);
            }
            return myAl;
        }

        /// <summary>
        /// �����ն˱�Ż������������ҩ̨��Ϣ
        /// </summary>
        /// <param name="terminalCode">�ն˱��</param>
        /// <returns>DrugSPETerminal��ʧ�ܷ���null</returns>
        public DrugSPETerminal GetDrugSPETerminalById(string terminalCode)
        {
            string strSQL = "";

            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugSPETerminal.ById", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugSPETerminal.ById�ֶ�!";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, terminalCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.GetDrugSPETerminal.ById";
                return null;
            }
            ArrayList al = this.myGetDrugSPETerminal(strSQL);
            if (al == null) return null;

            if (al.Count == 0) return null;

            return al[0] as DrugSPETerminal;
        }

        /// <summary>
        /// �������������е���
        /// ����������Ŀ�����ȡ������ҩ�ն���Ϣ �������ȼ�����ߵ���ҩ�ն�
        /// sql���ʹ��in�������
        /// </summary>
        /// <param name="adjustType">������ʽ 0 ƽ�� 1 ����</param>
        /// <param name="itemCode">������Ŀ����</param>
        /// <returns>�ɹ�����������Ŀʵ�� ʧ�ܷ���null �޼�¼���ؿ�ʵ��</returns>
        public FS.HISFC.Models.Pharmacy.DrugSPETerminal GetDrugSPETerminalByItemCode(string adjustType, string deptCode, params string[] itemCode)
        {
            string strSQL = "";
            //SQL�����ͨ��Inʵ��
            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugSPETerminal.ByItemCode" + "." + adjustType, ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugSPETerminal.ByItemCode." + adjustType + "�ֶ�!";
                return null;
            }
            try
            {
                string strParm = "";
                foreach (string str in itemCode)
                {
                    if (strParm == "")
                        strParm = "'" + str + "'";
                    else
                        strParm = strParm + "," + "'" + str + "'";
                }
                strSQL = string.Format(strSQL, deptCode, strParm);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.GetDrugSPETerminal.ByItemCode";
                return null;
            }
            ArrayList al = this.myGetDrugSPETerminal(strSQL);
            if (al == null) return null;
            if (al.Count == 0) return new FS.HISFC.Models.Pharmacy.DrugSPETerminal();
            return al[0] as DrugSPETerminal;
        }

        /// <summary>
        /// ��������������ҩ̨ʵ����Ϣ���������������
        /// </summary>
        /// <param name="drugSPETerminal">����������ҩ̨ʵ��</param>
        /// <returns>�ɹ�����1��ʧ�ܷ��أ�1</returns>
        public int SetDrugSPETerminal(DrugSPETerminal drugSPETerminal)
        {
            int parm;
            parm = this.UpdateDrugSPETerminal(drugSPETerminal);
            if (parm == 0)
                parm = this.InsertDrugSPETerminal(drugSPETerminal);
            return parm;
        }

        #endregion

        #endregion

        #region ������ҩ̨(��ҩ����)ģ��ά��

        #region ��������ɾ���Ĳ���

        /// <summary>
        /// ���Update��Insert�����������
        /// </summary>
        /// <param name="obj">ģ��neuObjectʵ��</param>
        /// <returns>�ɹ������ַ������� ʧ�ܷ���null</returns>
        protected string[] myGetParmDrugOpenTerminal(FS.FrameWork.Models.NeuObject obj)
        {
            string[] strParm = {
								   obj.ID,						//ģ�����
								   obj.Name,					//ģ������
								   obj.User01,					//��ҩ̨����
								   obj.User02,					//�Ƿ�ر� 0 ���� 1 �ر�
								   obj.User03,					//�����ⷿ����
								   obj.Memo					//��ע
							   };
            return strParm;
        }

        /// <summary>
        /// �������ģ����Ϣ
        /// </summary>
        /// <param name="StrSQL">��ѯsql�ַ���</param>
        /// <returns>�ɹ�����neuobject���� ʧ�ܷ���null</returns>
        protected ArrayList myGetDrugOpenTerminal(string StrSQL)
        {
            ArrayList al = new ArrayList();
            if (this.ExecQuery(StrSQL) == -1)
            {
                this.Err = "��ȡ����ģ����Ϣʱ����" + this.Err;
                return null;
            }
            try
            {
                FS.FrameWork.Models.NeuObject info;
                while (this.Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();

                    info.ID = this.Reader[1].ToString();		//ģ�����
                    info.Name = this.Reader[2].ToString();		//ģ������
                    info.User01 = this.Reader[3].ToString();	//��ҩ̨����
                    info.User02 = this.Reader[4].ToString();	//�Ƿ�ر� 0 ���� 1 �ر�
                    info.User03 = this.Reader[0].ToString();	//�����ⷿ����
                    info.Memo = this.Reader[5].ToString();		//��ע

                    al.Add(info);
                }
            }
            catch (Exception ex)
            {
                this.Err = "�������ģ����Ϣʱ��ִ��SQL������" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }

        /// <summary>
        /// ����һ�����ݽ�������ģ��
        /// </summary>
        /// <param name="info">neuobjectʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int InsertDrugOpenTerminal(FS.FrameWork.Models.NeuObject info)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.InsertDrugOpenTerminal", ref strSql) == -1) return -1;
            try
            {
                //				if (info.ID == null || info.ID == "")				//��ȡģ����
                //                    info.ID = this.GetSequence("Pharmacy.Constant.GetNewCompanyID");
                string[] strParm = this.myGetParmDrugOpenTerminal(info);  //ȡ�����б�
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + "Pharmacy.DrugStore.InsertDrugOpenTerminal";
                this.ErrCode = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���ݿⷿ���� ģ���š��ն˱�� ����һ��������ҩ̨����ҩ����ģ������
        /// </summary>
        /// <param name="info">neuobjectʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int UpdateDrugOpenTerminal(FS.FrameWork.Models.NeuObject info)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugOpenTerminal", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = this.myGetParmDrugOpenTerminal(info);  //ȡ�����б�
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ��" + "Pharmacy.DrugStore.UpdateDrugOpenTerminal"; ;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����ģ���š��ն�����ɾ��ģ����Ϣ
        /// </summary>
        /// <param name="templateCode">ģ����</param>
        /// <param name="terminalType">�ն����� (0 ��ҩ���� 1 ��ҩ̨) "A"��������</param>
        /// <returns>�ɹ�����ɾ������ ʧ�ܷ��أ�1</returns>
        public int DeleteDrugOpenTerminalByType(string templateCode, string terminalType)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.DeleteDrugOpenTerminalByType", ref strSql) == -1) return -1;

            try
            {
                strSql = string.Format(strSql, templateCode, terminalType);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "�����������ȷ��Pharmacy.DrugStore.DeleteDrugOpenTerminalByType";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����ģ����ɾ�����и�ģ������
        /// </summary>
        /// <param name="templateCode">ģ����</param>
        /// <returns>�ɹ�����ɾ������ ʧ�ܷ��أ�1</returns>
        public int DeleteDrugOpenTerminalByTemplateCode(string templateCode)
        {
            return this.DeleteDrugOpenTerminalByType(templateCode, "A");
        }

        /// <summary>
        /// ����ģ���š��ն˱��ɾ��һ��ģ����Ϣ
        /// </summary>
        /// <param name="templateCode">ģ����</param>
        /// <param name="terminalCode">�ն˱��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int DeleteDrugOpenTerminalById(string templateCode, string terminalCode)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.DeleteDrugOpenTerminalById", ref strSql) == -1) return -1;

            try
            {
                strSql = string.Format(strSql, templateCode, terminalCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "�����������ȷ��Pharmacy.DrugStore.DeleteDrugOpenTerminalById";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// �����ն˱��ɾ����ģ�����������Ϣ
        /// </summary>
        /// <param name="terminalCode">�ն˱��</param>
        /// <returns>�ɹ�����ɾ������ʧ�ܷ���null</returns>
        public int DeleteDrugOpenTerminalById(string terminalCode)
        {
            return this.DeleteDrugOpenTerminalById("AAAA", terminalCode);
        }

        #endregion

        #region �ڲ�ʹ��

        /// <summary>
        /// ���ݿ��ұ�Ż�ȡ�ÿ���ģ���б�
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <returns>�ɹ�����neuobject����(ID ģ���� Name ģ������)��ʧ�ܷ���null</returns>
        public ArrayList QueryDrugOpenTerminalByDeptCode(string deptCode)
        {
            string strSQL = "";

            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugOpenTerminalByDeptCode", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugOpenTerminalByDeptCode�ֶ�!";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, deptCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.GetDrugOpenTerminalByDeptCode";
                return null;
            }
            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "��ȡ����ģ����Ϣʱ����" + this.Err;
                return null;
            }
            try
            {
                FS.FrameWork.Models.NeuObject info;
                while (this.Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();

                    info.ID = this.Reader[1].ToString();		//ģ�����
                    info.Name = this.Reader[2].ToString();		//ģ������
                    info.User03 = this.Reader[0].ToString();	//�����ⷿ����

                    al.Add(info);
                }
            }
            catch (Exception ex)
            {
                this.Err = "�������ģ����Ϣʱ��ִ��SQL������" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }

        /// <summary>
        /// ����ģ���Ż�ȡģ����ϸ��Ϣ
        /// </summary>
        /// <param name="templateCode">ģ����</param>
        /// <returns>�ɹ��������� ʧ�ܷ���null</returns>
        public ArrayList QueryDrugOpenTerminalById(string templateCode)
        {
            string strSQL = "";

            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugOpenTerminalById", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugOpenTerminal.ById�ֶ�!";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, templateCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.GetDrugOpenTerminalById";
                return null;
            }
            return this.myGetDrugOpenTerminal(strSQL);
        }

        /// <summary>
        /// ��������ģ����Ϣ���������������һ��������
        /// </summary>
        /// <param name="info">neuobjectʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int SetDrugOpenTerminal(FS.FrameWork.Models.NeuObject info)
        {
            int parm;
            parm = this.UpdateDrugOpenTerminal(info);
            if (parm == 0)
                parm = this.InsertDrugOpenTerminal(info);
            return parm;
        }

        /// <summary>
        /// ִ��ѡ��ģ��
        /// </summary>
        /// <param name="templateCode">ģ����</param>
        /// <returns>�ɹ����ظ������� ʧ�ܷ���-1</returns>
        public int ExecOpenTerminal(string deptCode, string templateCode)
        {
            if (this.UpdateTerminalCloseFlag(deptCode, "1", "1") == -1)
            {
                this.Err = "ִ�йر�ȫ����ҩ̨ʧ��" + this.Err;
                return -1;
            }

            string strSQL = "";

            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.ExecOpenTerminal", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.ExecOpenTerminal�ֶ�!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, templateCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.ExecOpenTerminal";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        #endregion

        #endregion

        #region ����ԱĬ����ҩ̨����ҩ����ά��

        #region ��������ɾ���Ĳ���

        /// <summary>
        /// ���Update��Insert��������
        /// </summary>
        /// <param name="info">neuobjectʵ��</param>
        /// <returns>�ɹ�����string�������顢ʧ�ܷ���null</returns>
        protected string[] myGetParmDrugTerminalOper(FS.FrameWork.Models.NeuObject info)
        {
            string[] strParm = {
								   info.ID,				//Ա������
								   info.Name,				//Ա������
								   info.User01,			//�ն�(��ҩ̨����ҩ����)����
								   this.Operator.ID		//����Ա
							   };
            return strParm;
        }

        /// <summary>
        /// ��������ն�Ĭ�ϲ���Ա��Ϣ
        /// </summary>
        /// <param name="strSQL">��ѯ��SQl���</param>
        /// <returns>�ɹ�����neuobject��̬���� ʧ�ܷ���null</returns>
        protected ArrayList myGetDrugTerminalOper(string strSQL)
        {
            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "��ȡ�����ն�Ĭ�ϲ���Ա��Ϣ" + this.Err;
                return null;
            }
            try
            {
                FS.FrameWork.Models.NeuObject info;
                while (this.Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();

                    info.ID = this.Reader[0].ToString();		//Ա������
                    info.Name = this.Reader[1].ToString();		//Ա������
                    info.Memo = this.Reader[2].ToString();		//��/��ҩ�ն����� 0 ��ҩ���� 1 ��ҩ̨
                    info.User01 = this.Reader[3].ToString();	//�ն˱���
                    info.User02 = this.Reader[4].ToString();	//�ն�����					
                    al.Add(info);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ȡ�����ն�Ĭ�ϲ���Ա��Ϣ��ִ��SQL������" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }

        /// <summary>
        /// ���������ԱĬ����ҩ̨�����һ������
        /// </summary>
        /// <param name="info">neuobjectʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���null</returns>
        public int InsertDrugTerminalOper(FS.FrameWork.Models.NeuObject info)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.InsertDrugTerminalOper", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = this.myGetParmDrugTerminalOper(info);  //ȡ�����б�
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + "Pharmacy.DrugStore.InsertDrugTerminalOper";
                this.ErrCode = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���������ԱĬ����ҩ̨�����һ������
        /// </summary>
        /// <param name="info">neuobjectʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���null</returns>
        public int UpdateDrugTerminalOper(FS.FrameWork.Models.NeuObject info)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugTerminalOper", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = this.myGetParmDrugTerminalOper(info);  //ȡ�����б�
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + "Pharmacy.DrugStore.UpdateDrugTerminalOper";
                this.ErrCode = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���������ԱĬ����ҩ̨��ɾ��һ������
        /// </summary>
        /// <param name="emplCode">����ԱԱ����</param>
        /// <param name="terminalCode">�ն˱���</param>
        ///<returns>�ɹ�����ɾ��������ʧ�ܷ��أ�1</returns>
        public int DeleteDrugTerminalOper(string emplCode, string terminalCode)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.DeleteDrugTerminalOper", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, emplCode, terminalCode);       //
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + "Pharmacy.DrugStore.DeleteDrugTerminalOper";
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ɾ��ĳ����Ա����Ĭ�ϲ�����Ϣ
        /// </summary>
        /// <param name="emplCode">����Ա����</param>
        /// <returns>�ɹ�����ɾ������ ʧ�ܷ��أ�1</returns>
        public int DelDrugTerminalOperByEmplId(string emplCode)
        {
            return this.DeleteDrugTerminalOper(emplCode, "AAAA");
        }

        /// <summary>
        /// ɾ��ĳָ���ն����в�����Ϣ
        /// </summary>
        /// <param name="terminalCode">�ն˱���</param>
        /// <returns>�ɹ�����ɾ������ ʧ�ܷ���-1</returns>
        public int DelDrugTerminalOperByTerminalId(string terminalCode)
        {
            return this.DeleteDrugTerminalOper("AAAA", terminalCode);
        }

        /// <summary>
        ///  �Ƚ�������ɾ���ڽ��в������
        /// </summary>
        /// <param name="info">neuobjectʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int SetDrugTerminalOper(FS.FrameWork.Models.NeuObject info)
        {
            int parm;
            parm = this.DeleteDrugTerminalOper(info.ID, info.User01);
            if (parm == -1) return parm;

            parm = this.InsertDrugTerminalOper(info);

            return parm;
        }

        #endregion

        #region �ڲ�ʹ��

        /// <summary>
        /// ���������Ҳ���ԱĬ�ϲ����ն��б���Ϣ
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <returns>�ɹ�����neuobject���� ʧ�ܷ���null</returns>
        public ArrayList QueryDrugTerminalOperList(string deptCode)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminalOperList", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, deptCode);       //
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + "Pharmacy.DrugStore.GetDrugTerminalOperList";
                this.ErrCode = ex.Message;
                return null;
            }
            return this.myGetDrugTerminalOper(strSql);
        }

        /// <summary>
        /// ��ȡָ������ԱĬ�ϲ�����Ϣ
        /// </summary>
        /// <param name="emplCode">����Ա����</param>
        /// <returns>�ɹ����������ն�ʵ������ ʧ�ܷ���null</returns>
        public ArrayList QueryDrugTerminalOperByEmplId(string emplCode)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.GerDrugTerminalOperByEmplId", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, emplCode);       //
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + "Pharmacy.DrugStore.GerDrugTerminalOperByEmplId";
                this.ErrCode = ex.Message;
                return null;
            }
            return this.myGetDrugTerminal(strSql);
        }

        /// <summary>
        /// ��ȡָ���ն˵�Ĭ�ϲ���Ա��Ϣ
        /// </summary>
        /// <param name="terminalCode">�ն˱���</param>
        /// <returns>�ɹ�����neuobject����(Id ��Ա���� Name ��Ա����) ʧ�ܷ���null</returns>
        public ArrayList QueryDrugTerminalOperByTerminalId(string terminalCode)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminalOperByTerminalId", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, terminalCode);       //
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + "Pharmacy.DrugStore.GetDrugTerminalOperByTerminalId";
                this.ErrCode = ex.Message;
                return null;
            }
            return this.myGetDrugTerminalOper(strSql);
        }

        #endregion

        #endregion

        #region �����ҩ����(��������)

        #region ��������ɾ���Ĳ���

        /// <summary>
        /// ��ȡUpdate��Insert���鴫��������� 
        /// </summary>
        /// <param name="info">�����ҩ����ʵ��</param>
        /// <returns>�ɹ�����string�������� ʧ�ܷ���null</returns>
        protected string[] myGetParmDrugRecipe(FS.HISFC.Models.Pharmacy.DrugRecipe info)
        {
            string[] strParm = {
								   info.StockDept.ID,							//ҩ������(��ҩҩ��)
								   info.RecipeNO,								//������
								   info.SystemType,								//�����������
								   info.TransType,								//�������� 1 ������ 2 ������								
								   info.RecipeState,							//����״̬
								   info.ClinicNO,								//�����
								   info.CardNO,									//������
								   info.PatientName,							//��������
								   info.Sex.ID.ToString(),						//�Ա�
								   info.Age.ToString(),							//����
								   info.PayKind.ID,								//�������
								   info.PatientDept.ID,							//���߿���
								   info.RegTime.ToString(),						//�Һ�����
								   info.Doct.ID,								//����ҽ��
								   info.DoctDept.ID,							//����ҽ������
								   info.DrugTerminal.ID,						//��ҩ�ն�
								   info.SendTerminal.ID,						//��ҩ�ն�
								   info.FeeOper.ID,								//�շ���
								   info.FeeOper.OperTime.ToString(),			//�շ�ʱ��
								   info.InvoiceNO,								//Ʊ�ݺ�
								   info.Cost.ToString(),						//�������
								   info.RecipeQty.ToString(),					//������ҩƷƷ����
								   info.DrugedQty.ToString(),					//����ҩҩƷƷ����
								   info.DrugedOper.ID,							//��ҩ��
								   info.StockDept.ID,							//��ҩ����
								   info.DrugedOper.OperTime.ToString(),			//��ҩ����
								   info.SendOper.ID,							//��ҩ��
								   info.SendOper.OperTime.ToString(),			//��ҩ����
								   info.StockDept.ID,							//��ҩ����

								   ((int)info.ValidState).ToString(),		    //��Ч״̬ 1 ��Ч 0 ��Ч 2 ��ҩ���˷�
								   NConvert.ToInt32(info.IsModify).ToString(),	//��/��ҩ״̬ 0 �� 1 ��

								   info.BackOper.ID,							//��ҩ��
								   info.BackOper.OperTime.ToString(),			//��ҩʱ��
								   info.CancelOper.ID,							//ȡ����
								   info.CancelOper.OperTime.ToString(),			//ȡ��ʱ��
								   info.Memo,									//��ע
                                   info.SumDays.ToString()						//������ҩƷ�����ϼ�
							   };
            return strParm;
        }

        /// <summary>
        /// ��������ҩ����(��������)��Ϣ
        /// </summary>
        /// <param name="strSQL">��ѯ��SQl���</param>
        /// <returns>�ɹ��������� ʧ�ܷ���null</returns>
        protected ArrayList myGetDrugRecipeInfo(string strSQL)
        {
            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "��ȡ���ﴦ��������Ϣ����" + this.Err;
                return null;
            }
            try
            {
                FS.HISFC.Models.Pharmacy.DrugRecipe info;
                while (this.Reader.Read())
                {
                    #region �ɽ�����ڶ�ȡ����
                    info = new DrugRecipe();

                    info.StockDept.ID = this.Reader[0].ToString();						//ҩ������
                    info.RecipeNO = this.Reader[1].ToString();							//������
                    info.SystemType = this.Reader[2].ToString();						//�����������
                    info.TransType = this.Reader[3].ToString();							//��������,1�����ף�2������
                    info.RecipeState = this.Reader[4].ToString();						//����״̬: 0����,1��ӡ,2��ҩ,3��ҩ,4��ҩ(����δ����ҩƷ���ػ���)
                    info.ClinicNO = this.Reader[5].ToString();						//�����
                    info.CardNO = this.Reader[6].ToString();							//��������
                    info.PatientName = this.Reader[7].ToString();						//��������
                    info.Sex.ID = this.Reader[8].ToString();							//�Ա�
                    info.Age = NConvert.ToDateTime(this.Reader[9].ToString());			//����
                    info.PayKind.ID = this.Reader[10].ToString();						//����������
                    info.PatientDept.ID = this.Reader[11].ToString();					//���߿��ұ���
                    info.RegTime = NConvert.ToDateTime(this.Reader[12].ToString());		//�Һ�����
                    info.Doct.ID = this.Reader[13].ToString();							//����ҽʦ
                    info.DoctDept.ID = this.Reader[14].ToString();						//����ҽʦ���ڿ���
                    info.DrugTerminal.ID = this.Reader[15].ToString();					//��ҩ�նˣ���ӡ̨��
                    info.SendTerminal.ID = this.Reader[16].ToString();					//��ҩ�նˣ���ҩ���ڣ�
                    info.FeeOper.ID = this.Reader[17].ToString();							//�շ��˱���(�����˱���)
                    info.FeeOper.OperTime = NConvert.ToDateTime(this.Reader[18].ToString());		//�շ�ʱ��(����ʱ��)
                    info.InvoiceNO = this.Reader[19].ToString();						//Ʊ�ݺ�
                    info.Cost = NConvert.ToDecimal(this.Reader[20].ToString());			//���������۽�
                    info.RecipeQty = NConvert.ToDecimal(this.Reader[21].ToString());	//������ҩƷ����(��ɽһ��Ʒ����)
                    info.DrugedQty = NConvert.ToDecimal(this.Reader[22].ToString());	//����ҩ��ҩƷ����(��ɽһ��Ʒ����)
                    info.DrugedOper.ID = this.Reader[23].ToString();						//��ҩ��
                    info.StockDept.ID = this.Reader[24].ToString();					    //��ҩ����
                    info.DrugedOper.OperTime = NConvert.ToDateTime(this.Reader[25].ToString());	//��ҩ����
                    info.SendOper.ID = this.Reader[26].ToString();							//��ҩ��
                    info.SendOper.OperTime = NConvert.ToDateTime(this.Reader[27].ToString());	//��ҩʱ��
                    info.StockDept.ID = this.Reader[28].ToString();						//��ҩ����

                    info.ValidState = (FS.HISFC.Models.Base.EnumValidState)(NConvert.ToInt32(this.Reader[29]));					//��Ч״̬��0��Ч��1��Ч 2 ��ҩ���˷�
                    info.IsModify = NConvert.ToBoolean(this.Reader[30].ToString());						//��ҩ��ҩ0��1��

                    info.BackOper.ID = this.Reader[31].ToString();							//-��ҩ��
                    info.BackOper.OperTime = NConvert.ToDateTime(this.Reader[32].ToString());	//��ҩʱ��
                    info.CancelOper.ID = this.Reader[33].ToString();						//ȡ������Ա
                    info.CancelOper.OperTime = NConvert.ToDateTime(this.Reader[34].ToString());	//ȡ������
                    info.Memo = this.Reader[35].ToString();								//��ע
                    info.SumDays = NConvert.ToDecimal(this.Reader[36].ToString());

                    al.Add(info);

                    #endregion
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ȡ���ﴦ��������Ϣ����ִ��SQL������" + ex.Message;
                this.ErrCode = ex.ToString();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }

        /// <summary>
        /// �������ҩ����(��������)�ڼ���һ������
        /// </summary>
        /// <param name="info">�����ҩ����(��������)ʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int InsertDrugRecipeInfo(FS.HISFC.Models.Pharmacy.DrugRecipe info)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.InsertDrugRecipeInfo", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = this.myGetParmDrugRecipe(info);  //ȡ�����б�
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + "Pharmacy.DrugStore.InsertDrugRecipeInfo";
                this.ErrCode = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// �� �����ҩ����(��������) ��������
        /// </summary>
        /// <param name="info">�����ҩ����(��������)ʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1 �޸��·���0</returns>
        public int UpdateDrugRecipeInfo(FS.HISFC.Models.Pharmacy.DrugRecipe info)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugRecipeInfo", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = this.myGetParmDrugRecipe(info);  //ȡ�����б�
                strSql = string.Format(strSql, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + "Pharmacy.DrugStore.UpdateDrugRecipeInfo";
                this.ErrCode = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        ///  �Ƚ�������ɾ���ڽ��в������
        /// </summary>
        /// <param name="info">�����ҩ����(��������)ʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int SetDrugTerminalOper(FS.HISFC.Models.Pharmacy.DrugRecipe info)
        {
            int parm;
            parm = this.UpdateDrugRecipeInfo(info);
            if (parm == 0)
                parm = this.InsertDrugRecipeInfo(info);
            return parm;
        }

        #endregion

        #region �ڲ�ʹ��

        /// <summary>
        /// ���¼�¼��Ч/��Ч״̬
        /// </summary>
        /// <param name="recipeNo">������</param>
        /// <param name="class3MenaingCode">����������� M1/M2/AA</param>
        /// <param name="validState">״̬ 0 ��Ч 1 ��Ч</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1 �޲�������0</returns>
        public int UpdateDrugRecipeValidState(string recipeNo, string class3MenaingCode, FS.HISFC.Models.Base.EnumValidState validState)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugRecipeValidState", ref strSql) == -1)
                return -1;
            try
            {
                strSql = string.Format(strSql, recipeNo, class3MenaingCode, ((int)validState).ToString(), this.Operator.ID);
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���¼�¼״̬
        /// </summary>
        /// <param name="deptCode">ҩ������</param>
        /// <param name="recipeNo">������</param>
        /// <param name="class3MeaningCode">����������� M1/M2</param>
        /// <param name="oldState">ԭ״̬</param>
        /// <param name="newState">��״̬</param>		
        /// <returns>�ɹ�����1 ʧ�ܷ���-1 �޲�������0</returns>
        public int UpdateDrugRecipeState(string deptCode, string recipeNo, string class3MeaningCode, string oldState, string newState)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugRecipeState", ref strSql) == -1)
                return -1;
            try
            {
                strSql = string.Format(strSql, deptCode, recipeNo, class3MeaningCode, oldState, newState);
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ������ҩ��Ϣ ���ݱ�������ҩ�����ı䴦��״̬ ��������ҩ�ն� 
        /// </summary>
        /// <param name="drugDept">�ⷿ����</param>
        /// <param name="recipeNo">������</param>
        /// <param name="class3MeaningCode">�������</param>
        /// <param name="drugOper">��ҩ��</param>
        /// <param name="drugedDept">��ҩ����</param>
        /// <param name="drugedNum">��������ҩ����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int UpdateDrugRecipeDrugedInfo(string drugDept, string recipeNo, string class3MeaningCode, string drugOper, string drugedDept, decimal drugedNum)
        {
            #region ԭSql���
            /*
			 UPDATE PHA_STO_RECIPE T
				   SET T.Druged_Oper = '{3}',
					   T.DRUGED_DEPT = '{4}',
					   T.DRUGED_DATE = sysdate,
					   T.DRUGED_QTY = {5},
					   T.RECIPE_STATE = DECODE(T.RECIPE_QTY - T.DRUGED_QTY - 1,0,'2','1')
				WHERE  T.PARENT_CODE = '000010'
				  AND  T.CURRENT_CODE = '004004'
				  AND  T.RECIPE_STATE = '1'
				  AND  T.CLASS3_MEANING_CODE = '{2}'
				  AND  T.DRUG_DEPT_CODE = '{0}'
				  AND  T.RECIPE_NO = '{1}'
			�ָ���Ϊ �ݲ�������ҩ����ȷ�� ��Ϊ����δ֪���� ���� ״̬���²���
			  UPDATE PHA_STO_RECIPE T
				   SET T.Druged_Oper = '{3}',
					   T.DRUGED_DEPT = '{4}',
					   T.DRUGED_DATE = sysdate,
					   T.DRUGED_QTY = {5},
					   T.RECIPE_STATE = '2'
				WHERE  T.PARENT_CODE = '000010'
				  AND  T.CURRENT_CODE = '004004'
				  AND  T.RECIPE_STATE = '1'
				  AND  T.CLASS3_MEANING_CODE = '{2}'
				  AND  T.DRUG_DEPT_CODE = '{0}'
				  AND  T.RECIPE_NO = '{1}'
			*/
            #endregion
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugRecipeInfo.Druged", ref strSql) == -1)
                return -1;
            try
            {
                string[] strParm = {	
									   drugDept,							//ҩ��
									   recipeNo,							//������
									   class3MeaningCode,					//�������
									   drugOper,							//��ҩ��
									   drugedDept,							//��ҩ����
									   drugedNum.ToString(),				//��������ҩ����
				};
                strSql = string.Format(strSql, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ������ҩ��Ϣ ���ݱ�������ҩ�����ı䴦��״̬ ������ҩ�ն�
        /// </summary>
        /// <param name="drugDept">�ⷿ����</param>
        /// <param name="recipeNo">������</param>
        /// <param name="class3MeaningCode">�������</param>
        /// <param name="drugOper">��ҩ��</param>
        /// <param name="drugedDept">��ҩ����</param>
        /// <param name="drugedNum">��������ҩ����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int UpdateDrugRecipeDrugedInfo(string drugDept, string recipeNo, string class3MeaningCode, string drugOper, string drugedDept, string drugedTerminal, decimal drugedNum)
        {
            #region ԭSql���
            /*
			 UPDATE PHA_STO_RECIPE T
				   SET T.Druged_Oper = '{3}',
					   T.DRUGED_DEPT = '{4}',
					   T.DRUGED_DATE = sysdate,
					   T.DRUGED_QTY = {5},
					   T.RECIPE_STATE = DECODE(T.RECIPE_QTY - T.DRUGED_QTY - 1,0,'2','1')
				WHERE  T.PARENT_CODE = '000010'
				  AND  T.CURRENT_CODE = '004004'
				  AND  T.RECIPE_STATE = '1'
				  AND  T.CLASS3_MEANING_CODE = '{2}'
				  AND  T.DRUG_DEPT_CODE = '{0}'
				  AND  T.RECIPE_NO = '{1}'
			�ָ���Ϊ �ݲ�������ҩ����ȷ�� ��Ϊ����δ֪���� ���� ״̬���²���
			  UPDATE PHA_STO_RECIPE T
				   SET T.Druged_Oper = '{3}',
					   T.DRUGED_DEPT = '{4}',
					   T.DRUGED_DATE = sysdate,
                       T.DRUGED_TERMINAL = '{5}',
					   T.DRUGED_QTY = {6},
					   T.RECIPE_STATE = '2',
                       T.EXT_FLAG = T.DRUGED_TERMINAL
				WHERE  T.PARENT_CODE = '000010'
				  AND  T.CURRENT_CODE = '004004'
				  AND  T.RECIPE_STATE = '1'
				  AND  T.CLASS3_MEANING_CODE = '{2}'
				  AND  T.DRUG_DEPT_CODE = '{0}'
				  AND  T.RECIPE_NO = '{1}'
			*/
            #endregion
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugRecipeInfo.Druged.Other", ref strSql) == -1)
                return -1;
            try
            {
                string[] strParm = {	
									   drugDept,							//ҩ��
									   recipeNo,							//������
									   class3MeaningCode,					//�������
									   drugOper,							//��ҩ��
									   drugedDept,							//��ҩ����
                                       drugedTerminal,                      //��ҩ�ն�
									   drugedNum.ToString(),				//��������ҩ����
				};
                strSql = string.Format(strSql, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }


        /// <summary>
        /// ���·�ҩ��Ϣ
        /// </summary>
        /// <param name="drugDept">�ⷿ����</param>
        /// <param name="recipeNo">������</param>
        /// <param name="class3MeaningCode">�������</param>
        /// <param name="type">1 ��ͨ���� 2 ���﷢ҩ</param>
        /// <param name="sendOper">��ҩ��</param>
        /// <param name="sendDept">��ҩ����</param>
        /// <param name="sendTerminal">��ҩ�ն�</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int UpdateDrugRecipeSendInfo(string drugDept, string recipeNo, string class3MeaningCode, string type, string sendOper, string sendDept, string sendTerminal)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugRecipeInfo.Send", ref strSql) == -1)
                return -1;
            try
            {
                if (type == "1")			//��ͨ����
                {
                    string[] strParm = {
										   drugDept,					//ҩ������
										   recipeNo,					//������
										   class3MeaningCode,			//�������
										   "2",
										   sendOper,					//��ҩ��
										   sendDept,					//��ҩ����
										   sendTerminal,				//��ҩ�ն�
									   };
                    strSql = string.Format(strSql, strParm);
                }
                else if (type == "2")	   //���﷢ҩ
                {
                    string[] strParm = {
										   drugDept,					//ҩ������
										   recipeNo,					//������
										   class3MeaningCode,			//�������
										   "A",
										   sendOper,					//��ҩ��
										   sendDept,					//��ҩ����
										   sendTerminal
									   };
                    strSql = string.Format(strSql, strParm);
                }

            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ��ҩȷ��
        /// </summary>
        /// <param name="drugDept">�ⷿ����</param>
        /// <param name="recipeNo">������</param>
        /// <param name="class3MeaningCode">�������</param>
        /// <param name="drugOper">��ҩ��</param>
        /// <param name="oldState">�����жϲ��� ָ������ԭ״̬ �����ж� ��Ϊ"A"</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int UpdateDrugRecipeBackInfo(string drugDept, string recipeNo, string class3MeaningCode, string drugOper, string oldState)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugRecipeInfo.Back", ref strSql) == -1)
                return -1;
            try
            {
                string[] strParm = {	
									   drugDept,							//ҩ��
									   recipeNo,							//������
									   class3MeaningCode,					//�������
									   drugOper,							//��ҩ��
									   oldState,							//ָ������ԭ״̬
				};
                strSql = string.Format(strSql, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���˸�ҩ���´���״̬���շ�ʱ�䡢�˸�ҩ���
        /// </summary>
        /// <param name="drugDept">��ҩҩ��</param>
        /// <param name="recipeNo">������</param>
        /// <param name="class3MeaningCode">Ȩ����</param>
        /// <param name="newState">�´���״̬</param>
        /// <param name="feeDate">�շ�ʱ��</param>
        /// <param name="isModify">�Ƿ��˸�ҩ</param>
        /// <returns>�ɹ����·���1 �޼�¼����0 ������0</returns>
        public int UpdateDrugRecipeModifyInfo(string drugDept, string recipeNo, string class3MeaningCode, string newState, DateTime feeDate, bool isModify)
        {
            string strSql = "";
            /*
             *
                    UPDATE PHA_STO_RECIPE T
                       SET T.Recipe_State = '{3}',
                           T.FEE_DATE = TO_DATE('{4}','YYYY-MM-DD HH24:MI:SS'),
                 T.MODIFY_FLAG = '{5}'
                    WHERE  T.PARENT_CODE = '000010'
                      AND  T.CURRENT_CODE = '004004'
                      AND  T.DRUG_DEPT_CODE = '{0}'
                      AND  T.RECIPE_NO = '{1}'
            AND  T.CLASS3_MEANING_CODE = '{2}' 
            */
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugRecipeInfo.Modify", ref strSql) == -1)
                return -1;
            try
            {
                string[] strParm;
                if (isModify)
                {
                    strParm = new string[]{	
											  drugDept,							//ҩ��
											  recipeNo,							//������
											  class3MeaningCode,					//�������
											  newState,							//�´���״̬
											  feeDate.ToString(),					//�շ�ʱ��
											  "1"
										  };
                }
                else
                {
                    strParm = new string[]{	
											  drugDept,							//ҩ��
											  recipeNo,							//������
											  class3MeaningCode,					//�������
											  newState,							//�´���״̬
											  feeDate.ToString(),					//�շ�ʱ��
											  "0"
										  };
                }
                strSql = string.Format(strSql, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���˸�ҩ���´���״̬��ҩƷ�������������շ�ʱ�䡢�˸�ҩ��ǡ������š���Ч��״̬��
        /// </summary>
        /// <param name="modifyRecipeInfo">���Ĵ�����Ϣ</param>
        /// <returns>�ɹ����·���1 �޼�¼����0 ������-1</returns>
        public int UpdateDrugRecipeModifyInfo(FS.HISFC.Models.Pharmacy.DrugRecipe modifyRecipeInfo)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugRecipeInfo.Modify.Recipe", ref strSql) == -1)
                return -1;
            try
            {
                string[] strParm;
                strParm = new string[]{	
										  modifyRecipeInfo.StockDept.ID,							//ҩ��
										  modifyRecipeInfo.RecipeNO,							//������
										  modifyRecipeInfo.SystemType,							//�������
										  modifyRecipeInfo.RecipeState,							//�´���״̬
										  modifyRecipeInfo.RecipeQty.ToString(),				//������Ʒ������
										  modifyRecipeInfo.Cost.ToString(),						//�������
										  modifyRecipeInfo.FeeOper.OperTime.ToString(),					//�շ�ʱ��
										  modifyRecipeInfo.InvoiceNO,							//��Ʊ��
										  ((int)modifyRecipeInfo.ValidState).ToString(),							//��Ч��
										  NConvert.ToInt32(modifyRecipeInfo.IsModify).ToString()//�˸�ҩ 0 �� 1 ��
									  };
                strSql = string.Format(strSql, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���ݾɷ�Ʊ�Ÿ����·�Ʊ��
        /// </summary>
        /// <param name="oldInvoiceNo">�ɷ�Ʊ��</param>
        /// <param name="newInvoiceNo">�·�Ʊ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1 �޼�¼����0</returns>
        public int UpdateDrugRecipeInvoiceN0(string oldInvoiceNo, string newInvoiceNo)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugRecipeInfo.UpdateInvoiceNo", ref strSql) == -1)
                return -1;
            try
            {
                string[] strParm;
                strParm = new string[]{	
										 oldInvoiceNo,
										 newInvoiceNo
									  };
                strSql = string.Format(strSql, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ��ȡ���͵�ָ��ҩ����ָ���ն˵Ĵ����б�
        /// </summary>
        /// <param name="deptCode">ҩ������</param>
        /// <param name="terminalCode">�ն˱���</param>
        /// <param name="type">�ն���� 0��ҩ����/1��ҩ̨</param>
        /// <param name="state">����״̬</param>
        /// <returns>�ɹ����������ҩʵ������ ʧ�ܷ���null</returns>
        public ArrayList QueryList(string deptCode, string terminalCode, string type, string state)
        {
            string strSqlSelect = "", strSqlWhere = "";
            if (this.GetSQL("Pharmacy.DrugStore.GetList.Select", ref strSqlSelect) == -1)
            {
                return null;
            }
            if (this.GetSQL("Pharmacy.DrugStore.GetList.Where1", ref strSqlWhere) == -1)
            {
                return null;
            }
            try
            {
                strSqlSelect = strSqlSelect + strSqlWhere;
                strSqlSelect = string.Format(strSqlSelect, deptCode, terminalCode, type, state);
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + ex.Message;
                return null;
            }
            ArrayList al = new ArrayList();
            al = this.myGetDrugRecipeInfo(strSqlSelect);
            return al;
        }

        /// <summary>
        /// ��ȡָ���շ�ʱ����͵�ָ��ҩ����ָ���ն˵Ĵ����б�
        /// </summary>
        /// <param name="deptCode">ҩ������</param>
        /// <param name="terminalCode">�ն˱���</param>
        /// <param name="type">�ն����  0��ҩ����/1��ҩ̨/2��ҩ/3ֱ�ӷ�ҩ</param>
        /// <param name="state">����״̬</param>
        /// <param name="queryDate">�շ�ʱ��</param>
        /// <returns>�ɹ����������ҩʵ������ ʧ�ܷ���null</returns>
        public ArrayList QueryList(string deptCode, string terminalCode, string type, string state, DateTime queryDate)
        {
            string strSqlSelect = "", strSqlWhere = "";
            if (this.GetSQL("Pharmacy.DrugStore.GetList.Select", ref strSqlSelect) == -1)
            {
                return null;
            }
            if (type == "1")		//��ҩ
            {
                if (this.GetSQL("Pharmacy.DrugStore.GetList.Druged", ref strSqlWhere) == -1)
                {
                    return null;
                }
            }
            else if (type == "0")   //��ҩ
            {
                if (this.GetSQL("Pharmacy.DrugStore.GetList.Send", ref strSqlWhere) == -1)
                {
                    return null;
                }
            }
            else if (type == "3")   //ֱ�ӷ�ҩ
            {
                if (this.GetSQL("Pharmacy.DrugStore.GetList.DirectSend", ref strSqlWhere) == -1)
                {
                    return null;
                }
            }

            try
            {
                strSqlSelect = strSqlSelect + strSqlWhere;
                strSqlSelect = string.Format(strSqlSelect, deptCode, terminalCode, state, queryDate.ToString());
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + ex.Message;
                return null;
            }
            ArrayList al = new ArrayList();
            al = this.myGetDrugRecipeInfo(strSqlSelect);
            return al;
        }

        /// <summary>
        /// ��ȡĳ��������δ��ҩ�����б�
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <returns>�ɹ����ػ����б� ʧ�ܷ���null</returns>
        public ArrayList QueryUnSendList(string deptCode)
        {
            string strSqlSelect = "", strSqlWhere = "";
            if (this.GetSQL("Pharmacy.DrugStore.GetList.Select", ref strSqlSelect) == -1)
            {
                return null;
            }
            if (this.GetSQL("Pharmacy.DrugStore.GetList.UnSend", ref strSqlWhere) == -1)
            {
                return null;
            }
            try
            {
                strSqlSelect = strSqlSelect + strSqlWhere;
                strSqlSelect = string.Format(strSqlSelect, deptCode);
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + ex.Message;
                return null;
            }

            ArrayList al = this.myGetDrugRecipeInfo(strSqlSelect);

            return al;
        }

        /// <summary>
        /// ���ݴ����Ż�ȡ����������Ϣ
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="class3MeaningCode">�������</param>
        /// <param name="recipeNo">������</param>
        /// <param name="state">����״̬</param>
        /// <returns>�ɹ�����DrugRecipeʵ�� ʧ�ܷ���null δ�ҵ����ؿ�ʵ��</returns>
        public FS.HISFC.Models.Pharmacy.DrugRecipe GetDrugRecipe(string deptCode, string class3MeaningCode, string recipeNo, string state)
        {
            string strSqlSelect = "", strSqlWhere = "";
            if (this.GetSQL("Pharmacy.DrugStore.GetList.Select", ref strSqlSelect) == -1)
            {
                return null;
            }
            if (this.GetSQL("Pharmacy.DrugStore.GetList.Where3", ref strSqlWhere) == -1)
            {
                return null;
            }
            try
            {
                strSqlSelect = strSqlSelect + strSqlWhere;
                strSqlSelect = string.Format(strSqlSelect, deptCode, class3MeaningCode, recipeNo, state);
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + ex.Message;
                return null;
            }
            ArrayList al = new ArrayList();
            al = this.myGetDrugRecipeInfo(strSqlSelect);
            if (al == null)
                return null;
            if (al.Count == 0)
                return new FS.HISFC.Models.Pharmacy.DrugRecipe();
            return al[0] as FS.HISFC.Models.Pharmacy.DrugRecipe;
        }

        /// <summary>
        /// ���ݴ����Ż�ȡ����������Ϣ
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="recipeNO">������</param>
        /// <returns>�ɹ�����DrugRecipeʵ��,ʧ�ܷ���null δ�ҵ����ؿ�ʵ��</returns>
        public FS.HISFC.Models.Pharmacy.DrugRecipe GetDrugRecipe(string deptCode, string recipeNO)
        {
            string strSqlSelect = "", strSqlWhere = "";
            if (this.GetSQL("Pharmacy.DrugStore.GetList.Select", ref strSqlSelect) == -1)
            {
                return null;
            }
            if (this.GetSQL("Pharmacy.DrugStore.GetList.Where.Recipe", ref strSqlWhere) == -1)
            {
                return null;
            }
            try
            {
                strSqlSelect = strSqlSelect + strSqlWhere;
                strSqlSelect = string.Format(strSqlSelect, deptCode, recipeNO);
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + ex.Message;
                return null;
            }
            ArrayList al = new ArrayList();
            al = this.myGetDrugRecipeInfo(strSqlSelect);
            if (al == null)
                return null;
            if (al.Count == 0)
                return new FS.HISFC.Models.Pharmacy.DrugRecipe();
            return al[0] as FS.HISFC.Models.Pharmacy.DrugRecipe;
        }

        /// <summary>
        /// ���ݵ��ݺ� ��ȡ ����������Ϣ
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="class3MeaningCode">������� M1 ������� M2  �����˿�</param>
        /// <param name="recipeState">����״̬</param>
        /// <param name="billType">�������� 0 ������ 1 ��Ʊ�� 2 ��������</param>
        /// <param name="billNo">���ݺ�</param>
        /// <returns>�ɹ�����DrugRecipe���� ʧ�ܷ���null δ�ҵ����ؿ�����</returns>
        public ArrayList QueryDrugRecipe(string deptCode, string class3MeaningCode, string recipeState, int billType, string billNo)
        {
            string strSqlSelect = "", strSqlWhere = "";
            string strWhereIndex = "";				//SQL���Where���� ����
            if (this.GetSQL("Pharmacy.DrugStore.GetList.Select", ref strSqlSelect) == -1)
            {
                return null;
            }
            switch (billType)
            {
                case 0:			//������
                    strWhereIndex = "Pharmacy.DrugStore.GetList.Where3";
                    break;
                case 1:			//��Ʊ��
                    strWhereIndex = "Pharmacy.DrugStore.GetList.Where4";
                    break;
                default:		//��������
                    strWhereIndex = "Pharmacy.DrugStore.GetList.Where5";
                    break;
            }
            if (this.GetSQL(strWhereIndex, ref strSqlWhere) == -1)
            {
                return null;
            }
            try
            {
                strSqlSelect = strSqlSelect + strSqlWhere;
                strSqlSelect = string.Format(strSqlSelect, deptCode, class3MeaningCode, billNo, recipeState);
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + ex.Message;
                return null;
            }
            ArrayList al = new ArrayList();
            al = this.myGetDrugRecipeInfo(strSqlSelect);
            if (al == null)
                return null;
            return al;
        }

        /// <summary>
        /// �жϻ����Ƿ����δȡҩ�Ĵ��� ����� �򷵻���һ�Ŵ����ķ�ҩ���ں�
        /// �粻����δȡҩ�Ĵ��� �򷵻ط�ҩ���ں�Ϊ��
        /// ����һ�Ŵ����ķ�ҩ�����ѹر� �򷵻ؿ�
        /// </summary>
        /// <param name="deptCode">ȡҩҩ��</param>
        /// <param name="clinicNo">������ˮ��</param>
        /// <param name="sendWindow">��ҩ���ں� Ϊ�ձ�ʾ������δȡҩ����</param>
        /// <returns>1 ���سɹ� ��1 ���� </returns>
        public int JudegPatientRecipe(string deptCode, string clinicNo, out string sendWindow)
        {
            sendWindow = "";

            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.JudegPatientRecipe", ref strSql) == -1)
            {
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, deptCode, clinicNo);
            }
            catch (Exception ex)
            {
                this.Err = "��������ȷ" + ex.Message;
                return -1;
            }
            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "��ȡδȡҩ������Ϣ����" + this.Err;
                return -1;
            }
            try
            {
                while (this.Reader.Read())
                {
                    sendWindow = this.Reader[0].ToString();
                }
            }
            catch (Exception ex)
            {
                this.Err = "" + ex.Message;
                return -1;
            }
            finally
            {
                this.Reader.Close();
            }
            return 1;
        }

        #endregion

        #endregion

        #region ��������

        /// <summary>
        /// ��ȡ������ʽ 
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <returns>�ɹ����ش���������ʽ 0 ƽ�� 1 ����</returns>
        public string GetAdjustType(string deptCode)
        {
            FS.FrameWork.Management.ExtendParam extManager = new FS.FrameWork.Management.ExtendParam();
            extManager.SetTrans(this.Trans);

            string adjustType = "0";

            try
            {
                FS.HISFC.Models.Base.ExtendInfo deptExt = extManager.GetComExtInfo(FS.HISFC.Models.Base.EnumExtendClass.DEPT, "TerminalAdjust", deptCode);
                if (deptExt == null)
                {
                    this.Err = "��ȡ������չ��������ҩ��������ʧ�ܣ�";

                    adjustType = "0";
                }

                if (deptExt.StringProperty == "1")		//����
                {
                    adjustType = "1";
                }
                else									//ƽ��
                {
                    adjustType = "0";
                }
            }
            catch { }

            return adjustType;
        }

        /// <summary>
        /// �շѹ����е��� ���봦��������
        /// ���ش���������Ϣ ��ҩҩ��+��ҩ����
        /// </summary>
        /// <param name="patient">������Ϣʵ��</param>
        /// <param name="feeAl">������Ϣ����</param>
        /// <param name="feeWindow">�շѴ��ں�</param>
        /// <param name="drugSendInfo">����������Ϣ ��ҩҩ��+��ҩ����</param>        
        /// <returns>�ɹ�����1 ʧ�ܷ���-1 </returns>
        public int DrugRecipe(FS.HISFC.Models.Registration.Register patient, ArrayList feeAl, string feeWindow, out string drugSendInfo)
        {
            return DrugRecipe(patient, feeAl, feeWindow, null, out drugSendInfo);
        }

        /// <summary>
        /// �շѹ����е��� ���봦��������
        /// ���ش���������Ϣ ��ҩҩ��+��ҩ����
        /// </summary>
        /// <param name="patient">������Ϣʵ��</param>
        /// <param name="feeAl">������Ϣ����</param>
        /// <param name="feeWindow">�շѴ��ں�</param>
        /// <param name="hsDeptAddress">ҩ��λ����Ϣ</param>
        /// <param name="drugSendInfo">����������Ϣ ��ҩҩ��+��ҩ����</param>        
        /// <returns>�ɹ�����1 ʧ�ܷ���-1 </returns>
        public int DrugRecipe(FS.HISFC.Models.Registration.Register patient, ArrayList feeAl, string feeWindow, System.Collections.Hashtable hsDeptAddress, out string drugSendInfo)
        {
            if (hsDeptAddress == null)
            {
                hsDeptAddress = new Hashtable();

                #region ���´����Ժ�Ų�����ҵ���

                //FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();
                ////consMgr.SetTrans(this.Transaction);
                //ArrayList alDeptAddress = consMgr.GetList("DeptAddress");
                //if (alDeptAddress != null)
                //{
                //    DrugStore.hsDeptAddress = new Hashtable();
                //    foreach (FS.HISFC.Models.Base.Const consInfo in alDeptAddress)
                //    {
                //        hsDeptAddress.Add(consInfo.ID, consInfo.Name);
                //    }
                //}

                #endregion
            }

            string adjustType = "0";            //0 ƽ������ 1 ��������

            drugSendInfo = "";

            #region �Է�����Ϣ���鰴�շ�ҩҩ�����з���
            ArrayList feeTempAl = new ArrayList();			//��ά���� �洢�����ķ�����Ϣ
            ArrayList feeNowTemp = new ArrayList(); 		//��ά���� �洢��һ�εķ���

            FS.HISFC.Models.Fee.Outpatient.FeeItemList feeTemp;
            string privDrugDept = "";
            try
            {
                FeeSort feeSortInterface = new FeeSort();
                feeAl.Sort(feeSortInterface);
            }
            catch (Exception ex)
            {
                this.Err = "�����߷�����Ϣ����ʱ��������" + ex.Message;
                return -1;
            }
            for (int i = 0; i < feeAl.Count; i++)
            {
                feeTemp = feeAl[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                if (feeTemp == null) continue;

                string stockDept = feeTemp.StockOper.Dept.ID;
                if (string.IsNullOrEmpty(stockDept))
                {
                    stockDept = feeTemp.ExecOper.Dept.ID;
                }

                if (stockDept == privDrugDept)
                {
                    feeNowTemp.Add(feeTemp);
                }
                else
                {
                    feeNowTemp = new ArrayList();
                    feeNowTemp.Add(feeTemp);
                    feeTempAl.Add(feeNowTemp);
                    privDrugDept = stockDept;
                }
            }
            #endregion

            FS.HISFC.Models.Pharmacy.DrugRecipe info;		//����������Ϣʵ��
            FS.HISFC.Models.Fee.Outpatient.FeeItemList feeInfo = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();
            foreach (ArrayList temp in feeTempAl)
            {
                if (temp.Count == 0) continue;
                info = new DrugRecipe();
                info.Cost = 0;
                info.SumDays = 0;

                string recipeNo = "";
                ArrayList alTemp = new ArrayList();
                Hashtable comboHs = new Hashtable();

                try
                {
                    RecipeSort feeRecipeSort = new RecipeSort();
                    temp.Sort(feeRecipeSort);
                }
                catch (Exception ex)
                {
                    this.Err = "�����߷�����Ϣ��������ʱ��������" + ex.Message;
                    return -1;
                }

                //������ʱ��������ַ����� ��������������ҩƷ������£��������ҩƷ���ڵڶ������� ��ô�����
                //һ��ҽ�������ֵ��˲�ͬ����ҩ̨��ͬʱ���ڴ������������ĸ���Ҳ�����ֻ������һ�Ŵ�������
                DrugTerminal drugTerminalTemp = new DrugTerminal();
                DrugTerminal sendTerminalTemp = new DrugTerminal();
                bool isArrangeDrugTerminal = false;

                for (int i = 0; i < temp.Count; i++)
                {
                    feeInfo = temp[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                    if (feeInfo == null)
                    {
                        this.Err = "���ݴ���ķ���ʵ������ ��ȡ����ʵ��ʵ��ʱ��������ת������";
                        return -1;
                    }
                    if (recipeNo != "" && recipeNo != feeInfo.RecipeNO)
                    {
                        if (alTemp.Count > 0)
                        {
                            //{24CF1B4D-1422-45da-B6E9-7075978ECF5A}  ͬһ����ÿ��ܴ��ڲ�ͬ�ķ�Ʊ��
                            FS.HISFC.Models.Fee.Outpatient.FeeItemList tempFeeInfo = alTemp[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;

                            #region ���ݷ�����Ϣʵ��Դ���������Ϣʵ����и�ֵ
                            info.StockDept.ID = feeInfo.StockOper.Dept.ID;						//ҩ������(��ҩҩ��)
                            if (string.IsNullOrEmpty(feeInfo.StockOper.Dept.ID))
                            {
                                info.StockDept.ID = feeInfo.ExecOper.Dept.ID;						//ҩ������(��ҩҩ��)    
                            }
                            info.RecipeNO = recipeNo;									        //������
                            info.SystemType = "M1";												//�����������
                            info.TransType = "1";												//�������� 1 ������ 2 ������								
                            info.RecipeState = "0";												//����״̬
                            info.ClinicNO = feeInfo.Patient.ID;								            //�����
                            info.CardNO = feeInfo.Patient.PID.CardNO;							//������
                            info.PatientName = patient.Name;									//��������
                            info.Sex = patient.Sex;												//�Ա�
                            info.Age = patient.Birthday;										//����
                            info.PayKind.ID = patient.Pact.PayKind.ID;								//�������
                            //���߿��� �� �Һſ���
                            info.PatientDept = ((FS.HISFC.Models.Registration.Register)feeInfo.Patient).DoctorInfo.Templet.Dept;								//���߿���
                            info.RegTime = ((FS.HISFC.Models.Registration.Register)feeInfo.Patient).DoctorInfo.SeeDate;										//�Һ�����
                            info.Doct = ((FS.HISFC.Models.Registration.Register)feeInfo.Patient).DoctorInfo.Templet.Doct;									//����ҽ��
                            info.DoctDept = feeInfo.RecipeOper.Dept;							//����ҽ������

                            //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                            if (patient.IsAccount)
                            {
                                info.FeeOper.OperTime = feeInfo.ChargeOper.OperTime;
                                info.FeeOper.ID = feeInfo.ChargeOper.ID;
                            }
                            else
                            {
                                info.FeeOper.OperTime = feeInfo.FeeOper.OperTime;					//�շ�ʱ��
                                info.FeeOper.ID = feeInfo.FeeOper.ID;
                            }

                            //{24CF1B4D-1422-45da-B6E9-7075978ECF5A}  ͬһ����ÿ��ܴ��ڲ�ͬ�ķ�Ʊ��
                            info.InvoiceNO = tempFeeInfo.Invoice.ID;									//Ʊ�ݺ�
                            info.RecipeQty = alTemp.Count;										//������ҩƷƷ����
                            info.DrugedQty = 0;													//����ҩҩƷƷ����
                            info.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;	//��Ч״̬ 1 ��Ч 0 ��Ч 2 ��ҩ���˷�
                            info.IsModify = false;												//��/��ҩ״̬ 0 �� 1 ��
                            //info.Memo = feeInfo.Memo;											//��ע
                            #endregion

                            #region ��ȡ����������ʽ

                            adjustType = this.GetAdjustType(info.StockDept.ID);

                            #endregion

                            #region ���ݴ������������ȡ��ҩ̨����ҩ���ڱ���

                            //DrugTerminal drugTerminalTemp = new DrugTerminal(),sendTerminalTemp = new DrugTerminal();
                            if (isArrangeDrugTerminal == false)     //�Ա������һ�ν��е���
                            {
                                if (this.RecipeAdjust(patient, alTemp, feeWindow, adjustType, out drugTerminalTemp, out sendTerminalTemp) == -1)
                                    return -1;
                                isArrangeDrugTerminal = true;
                            }
                            else
                            {
                                int averageNum = 0;
                                if (adjustType == "1")
                                {
                                    averageNum = 1;
                                }
                                //���µ������� �ڵ�����������±�����ֶ����
                                if (this.UpdateTerminalAdjustInfo(drugTerminalTemp.ID, alTemp.Count, alTemp.Count, averageNum) == -1)
                                {
                                    this.Err = "������ҩ̨�ѷ��͡�����ҩ����ʱ����" + this.Err;
                                    return -1;
                                }
                            }

                            info.DrugTerminal.ID = drugTerminalTemp.ID;
                            info.SendTerminal.ID = sendTerminalTemp.ID;
                            if (drugTerminalTemp.Memo != null)
                            {
                                info.Memo = drugTerminalTemp.Memo;
                            }

                            if (info.DrugTerminal.ID == "" || info.SendTerminal.ID == "")
                            {
                                this.Err = "��������ִ�д��� δ��ȡ��ȷ����ҩ̨/��ҩ���ڱ���";
                                return -1;
                            }
                            if (drugSendInfo == "")
                            {
                                if (feeInfo.UndrugComb.User03 == null || feeInfo.UndrugComb.User03 == "")
                                {
                                    string dept = feeInfo.StockOper.Dept.ID;
                                    string deptName = feeInfo.StockOper.Dept.Name;
                                    if (string.IsNullOrEmpty(dept))
                                    {
                                        dept = feeInfo.ExecOper.Dept.ID;
                                        deptName = feeInfo.ExecOper.Dept.Name;
                                    }
                                    if (hsDeptAddress.ContainsKey(dept))
                                        drugSendInfo = drugSendInfo + hsDeptAddress[dept].ToString() + deptName + sendTerminalTemp.Name;
                                    else
                                        drugSendInfo = drugSendInfo + deptName + sendTerminalTemp.Name;	//ȡҩҩ�� + ��ҩ����
                                }
                            }
                            else
                            {
                                if (feeInfo.UndrugComb.User03 == null || feeInfo.UndrugComb.User03 == "")
                                {
                                    string dept = feeInfo.StockOper.Dept.ID;
                                    string deptName = feeInfo.StockOper.Dept.Name;
                                    if (string.IsNullOrEmpty(dept))
                                    {
                                        dept = feeInfo.ExecOper.Dept.ID;
                                        deptName = feeInfo.ExecOper.Dept.Name;
                                    }
                                    if (hsDeptAddress.ContainsKey(dept))
                                    {
                                        string sendInfo = hsDeptAddress[dept].ToString() + deptName + sendTerminalTemp.Name;
                                        if (!drugSendInfo.Contains(sendInfo))
                                        {
                                            drugSendInfo = drugSendInfo + "|" + hsDeptAddress[dept].ToString() + deptName + sendTerminalTemp.Name;
                                        }
                                    }
                                    else
                                    {
                                        string sendInfo = deptName + sendTerminalTemp.Name;
                                        if (!drugSendInfo.Contains(sendInfo))
                                        {
                                            drugSendInfo = drugSendInfo + "|" + deptName + sendTerminalTemp.Name;	//ȡҩҩ�� + ��ҩ����
                                        }
                                    }
                                }
                            }
                            #endregion

                            if (this.InsertDrugRecipeInfo(info) == -1)
                            {
                                if (this.DBErrCode != 1)
                                {
                                    return -1;
                                }
                                else
                                {
                                    #region ����/��ҩ��� �Դ�������ͷ�����״̬����
                                    int parm = this.UpdateDrugRecipeModifyInfo(info);
                                    if (parm == -1)
                                    {
                                        return parm;
                                    }
                                    else if (parm == 0)
                                    {
                                        this.Err = "δ��ȷ�ҵ��˸�ҩ��Ҫ���µĴ�������ͷ������ ���������ѷ����仯 ";
                                        return -1;
                                    }
                                    #endregion
                                }
                            }
                        }

                        recipeNo = feeInfo.RecipeNO;

                        alTemp = new ArrayList();
                        comboHs = new Hashtable();
                        alTemp.Add(feeInfo);
                        comboHs.Add(feeInfo.Order.Combo, feeInfo.Days);
                        info.Cost = 0;
                        info.Cost = info.Cost + feeInfo.FT.TotCost;
                        info.SumDays = 0;
                        info.SumDays = info.SumDays + feeInfo.Days;
                    }
                    else
                    {
                        recipeNo = feeInfo.RecipeNO;
                        alTemp.Add(feeInfo);

                        if (!comboHs.ContainsKey(feeInfo.Order.Combo))
                        {
                            comboHs.Add(feeInfo.Order.Combo, feeInfo.Days);
                            info.SumDays = info.SumDays + feeInfo.Days;
                        }

                        info.Cost = info.Cost + feeInfo.FT.TotCost;
                    }

                }
                #region �������һ��
                if (alTemp != null && alTemp.Count > 0)
                {
                    //{24CF1B4D-1422-45da-B6E9-7075978ECF5A}  ͬһ����ÿ��ܴ��ڲ�ͬ�ķ�Ʊ��
                    FS.HISFC.Models.Fee.Outpatient.FeeItemList tempFeeInfo = alTemp[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;

                    #region ���ݷ�����Ϣʵ��Դ���������Ϣʵ����и�ֵ
                    info.StockDept.ID = feeInfo.StockOper.Dept.ID;							//ҩ������(��ҩҩ��)
                    if (string.IsNullOrEmpty(info.StockDept.ID))
                    {
                        info.StockDept.ID = feeInfo.ExecOper.Dept.ID;							//ҩ������(��ҩҩ��)
                    }
                   
                    info.RecipeNO = recipeNo;									            //������
                    info.SystemType = "M1";												    //�����������
                    info.TransType = "1";												    //�������� 1 ������ 2 ������								
                    info.RecipeState = "0";												    //����״̬
                    info.ClinicNO = feeInfo.Patient.ID;								        //�����
                    info.CardNO = feeInfo.Patient.PID.CardNO;								//������
                    info.PatientName = patient.Name;									    //��������
                    info.Sex = patient.Sex;												    //�Ա�
                    info.Age = patient.Birthday;										    //����
                    info.PayKind.ID = patient.Pact.PayKind.ID;								//�������
                    //���߿��� �� �Һſ���
                    info.PatientDept = ((FS.HISFC.Models.Registration.Register)feeInfo.Patient).DoctorInfo.Templet.Dept;								//���߿���
                    info.RegTime = ((FS.HISFC.Models.Registration.Register)feeInfo.Patient).DoctorInfo.SeeDate;										//�Һ�����
                    info.Doct = ((FS.HISFC.Models.Registration.Register)feeInfo.Patient).DoctorInfo.Templet.Doct;										//����ҽ��
                    info.DoctDept = feeInfo.RecipeOper.Dept;								//����ҽ������
                    //info.FeeOper.ID = feeInfo.FeeOper.ID;								    //�շ���
                    //info.FeeOper.OperTime = feeInfo.FeeOper.OperTime;						//�շ�ʱ��

                    //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                    if (patient.IsAccount)
                    {
                        info.FeeOper.OperTime = feeInfo.ChargeOper.OperTime;
                        info.FeeOper.ID = feeInfo.ChargeOper.ID;
                    }
                    else
                    {
                        info.FeeOper.OperTime = feeInfo.FeeOper.OperTime;					//�շ�ʱ��
                        info.FeeOper.ID = feeInfo.FeeOper.ID;
                    }

                    //{24CF1B4D-1422-45da-B6E9-7075978ECF5A}  ͬһ����ÿ��ܴ��ڲ�ͬ�ķ�Ʊ��
                    info.InvoiceNO = tempFeeInfo.Invoice.ID;									//Ʊ�ݺ�
                    info.RecipeQty = alTemp.Count;										    //������ҩƷƷ����
                    info.DrugedQty = 0;													    //����ҩҩƷƷ����

                    info.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;

                    info.IsModify = false;												    //��/��ҩ״̬ 0 �� 1 ��
                    //info.Memo = feeInfo.Memo;											    //��ע
                    #endregion

                    #region ��ȡ����������ʽ

                    adjustType = this.GetAdjustType(info.StockDept.ID);

                    #endregion

                    #region ���ݴ������������ȡ��ҩ̨����ҩ���ڱ���

                    //DrugTerminal drugTerminalTemp = new DrugTerminal(),sendTerminalTemp = new DrugTerminal();
                    if (isArrangeDrugTerminal == false)
                    {
                        if (this.RecipeAdjust(patient, alTemp, feeWindow, adjustType, out drugTerminalTemp, out sendTerminalTemp) == -1)
                            return -1;
                        isArrangeDrugTerminal = true;
                    }
                    else
                    {
                        int averageNum = 0;
                        if (adjustType == "1")
                        {
                            averageNum = 1;
                        }
                        //���µ�������
                        if (this.UpdateTerminalAdjustInfo(drugTerminalTemp.ID, alTemp.Count, alTemp.Count, averageNum) == -1)
                        {
                            this.Err = "������ҩ̨�ѷ��͡�����ҩ����ʱ����" + this.Err;
                            return -1;
                        }
                    }

                    info.DrugTerminal.ID = drugTerminalTemp.ID;
                    info.SendTerminal.ID = sendTerminalTemp.ID;
                    if (drugTerminalTemp.Memo != null)
                    {
                        info.Memo = drugTerminalTemp.Memo;
                    }
                    if (info.DrugTerminal.ID == "" || info.SendTerminal.ID == "")
                    {
                        this.Err = "��������ִ�д��� δ��ȡ��ȷ����ҩ̨/��ҩ���ڱ���";
                        return -1;
                    }
                    if (drugSendInfo == "")
                    {
                        if (feeInfo.UndrugComb.User03 == null || feeInfo.UndrugComb.User03 == "")
                        {
                            string dept = feeInfo.StockOper.Dept.ID;
                            string deptName = feeInfo.StockOper.Dept.Name;
                            if (string.IsNullOrEmpty(dept))
                            {
                                dept = feeInfo.ExecOper.Dept.ID;
                                deptName = feeInfo.ExecOper.Dept.Name;
                            }
                            if (hsDeptAddress.ContainsKey(dept))
                                drugSendInfo = drugSendInfo + hsDeptAddress[dept].ToString() + deptName + sendTerminalTemp.Name;
                            else
                                drugSendInfo = drugSendInfo + deptName + sendTerminalTemp.Name;	//ȡҩҩ�� + ��ҩ����
                        }
                    }
                    else
                    {
                        if (feeInfo.UndrugComb.User03 == null || feeInfo.UndrugComb.User03 == "")
                        {
                            string dept = feeInfo.StockOper.Dept.ID;
                            string deptName = feeInfo.StockOper.Dept.Name;
                            if (string.IsNullOrEmpty(dept))
                            {
                                dept = feeInfo.ExecOper.Dept.ID;
                                deptName = feeInfo.ExecOper.Dept.Name;
                            }
                            if (hsDeptAddress.ContainsKey(deptName))
                                drugSendInfo = drugSendInfo + "|" + hsDeptAddress[deptName].ToString() + deptName + sendTerminalTemp.Name;
                            else
                                drugSendInfo = drugSendInfo + "|" + deptName + sendTerminalTemp.Name;	//ȡҩҩ�� + ��ҩ����
                        }
                    }
                    #endregion

                    if (this.InsertDrugRecipeInfo(info) == -1)
                    {
                        if (this.DBErrCode != 1)
                        {
                            return -1;
                        }
                        else
                        {
                            #region ����/��ҩ��� �Դ�������ͷ�����״̬����
                            int parm = this.UpdateDrugRecipeModifyInfo(info);
                            if (parm == -1)
                            {
                                return parm;
                            }
                            else if (parm == 0)
                            {
                                this.Err = "δ��ȷ�ҵ��˸�ҩ��Ҫ���µĴ�������ͷ������ ���������ѷ����仯 ";
                                return -1;
                            }
                            #endregion
                        }
                    }
                }
                #endregion
            }

            return 1;
        }

        /// <summary>
        /// ����������ķ�����Ϣ���� ���ݵ��������ж�Ӧ���͵���ҩ̨����ҩ����� 
        /// ������ ������ķ�ҩ���ںš���ҩ�ն˺�
        /// </summary>
        /// <param name="patient">������Ϣʵ��</param>
        /// <param name="feeAl">������Ϣ����</param>
        /// <param name="feeWindow">�շѴ��ڱ���</param>
        /// <param name="adjustType">����������� 0 ƽ������ 1 ��������</param>
        /// <param name="drugTerminalObject">��ҩ�ն�ʵ��</param>
        /// <param name="sendTerminalObject">��ҩ�ն�ʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int RecipeAdjust(FS.HISFC.Models.Registration.Register patient, ArrayList feeAl, string feeWindow, string adjustType, out DrugTerminal drugTerminalObject, out DrugTerminal sendTerminalObject)
        {

            drugTerminalObject = new DrugTerminal();				//����������ص���ҩ�ն�
            sendTerminalObject = new DrugTerminal();				//����������صķ�ҩ�ն�
            string drugTerminal = "";								//���������������ҩ̨����
            string sendTerminal = "";								//������������ķ�ҩ������

            string adjustLevel = "a";								//������������ �ַ�Խ�󼶱�Խ��
            int drugKindNum = feeAl.Count;							//������Ʒ����
            int averageNum = 0;										//���ִ���

            if (adjustType != "1")
                adjustType = "0";

            FS.HISFC.Models.Fee.Outpatient.FeeItemList feeTemp = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();
            if (feeAl.Count <= 0)
                return 1;

            for (int i = 0; i < feeAl.Count; i++)
            {
                #region �����������
                feeTemp = feeAl[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                if (feeTemp == null)
                {
                    this.Err = "���������Ϣʱ ��������ת������";
                    return -1;
                }

                #region ����������ҩ̨�ĵ�����������ж� �緵�ز�Ϊ�� �ж��Ƿ�Ϊ�˸ķѵĴ���
                //�ж��Ƿ�����������ҩ̨���� 
                FS.HISFC.Models.Pharmacy.DrugSPETerminal speTerminalTemp = new DrugSPETerminal();
                //�����е�ר�Ƹ���ʲô�ж� ��ǰ�ȸ��ݹҺſ����ж�
                string strDept = ((FS.HISFC.Models.Registration.Register)feeTemp.Patient).DoctorInfo.Templet.Dept.ID;
                //feeItemListʵ��̳���BaseItem feeTemp.ID�洢��Ŀ���� 
                speTerminalTemp = this.GetDrugSPETerminalByItemCode(adjustType, feeTemp.ExecOper.Dept.ID, feeWindow, feeTemp.Item.ID, strDept, patient.Pact.ID,patient.PatientType);
                //���ز�Ϊ�� ˵������������ҩ̨�������� �����ж�
                if (speTerminalTemp != null && speTerminalTemp.Terminal.ID != null && speTerminalTemp.Terminal.ID != "")
                {
                    if (adjustType == "1")
                        averageNum = 1;

                    FS.HISFC.Models.Pharmacy.DrugTerminal tempTerminal = this.GetDrugTerminal(speTerminalTemp.Terminal.ID);
                    if (tempTerminal != null && tempTerminal.ID != "" && !tempTerminal.IsClose)
                    {
                        speTerminalTemp.Terminal = tempTerminal;
                        if (speTerminalTemp.ItemType != null && speTerminalTemp.ItemType.CompareTo(adjustLevel) >= 0) //���ε����������������ҩ̨�ĵ�������ʱ�Ž��и���
                        {
                            drugTerminal = speTerminalTemp.Terminal.ID;					//���ݵ��������õ�����ҩ̨
                            adjustLevel = speTerminalTemp.ItemType;					//�������� 'a'��'z' �ַ����󼶱�Խ��
                            drugTerminalObject = null;
                        }
                        if (speTerminalTemp.ItemType == "z")	//�����շѴ��ڵĵ������� ������������ж� ��ֱ�ӷ���
                        {
                            //�շѴ���Ϊ��߼���ĵ������� �϶�������е���ҩ̨���и���
                            drugTerminal = speTerminalTemp.Terminal.ID;
                            adjustLevel = "z";					//��߼���
                            drugTerminalObject = null;
                            break;
                        }
                        continue;
                    }
                }
                #endregion

                #region �жϸû����Ƿ�δȡҩ��ȡҩ����  �˴�ȡδ�رյ���ҩ̨
                if (adjustLevel.CompareTo("d") < 0)				//ԭ�����������ȼ�С�ڱ�����ʱ�Ž�����һ���ж�
                {
                    //��ҩҩ�� = ִ�п���
                    string dept = feeTemp.StockOper.Dept.ID;
                    if (string.IsNullOrEmpty(dept))
                    {
                        dept = feeTemp.ExecOper.Dept.ID;
                    }
                    this.JudegPatientRecipe(dept, feeTemp.Patient.PID.CardNO, out sendTerminal);
                    //����δȡҩ�Ĵ���
                    if (sendTerminal != "")
                    {
                        FS.HISFC.Models.Pharmacy.DrugTerminal terminalTemp = new DrugTerminal();
                        terminalTemp = this.GetDrugTerminalBySendWindow(sendTerminal);
                        if (terminalTemp != null && terminalTemp.ID != "")
                        {
                            drugTerminal = terminalTemp.ID;			//��ҩ̨����
                            adjustLevel = "d";
                            drugTerminalObject = null;
                            continue;
                        }
                        else
                        {
                            sendTerminal = "";
                        }
                    }
                }
                #endregion

                #region ���������ж� ƽ������/��������  �˴�ȡδ�رյġ������������ͨ��ҩ̨
                if (adjustType != "1")
                {
                    #region ƽ������
                    if (adjustLevel.CompareTo("c") < 0)			//�ϴε�������С�ڱ���ʱ 
                    {
                        string dept = feeTemp.StockOper.Dept.ID;
                        if (string.IsNullOrEmpty(dept))
                        {
                            dept = feeTemp.ExecOper.Dept.ID;
                        }
                        drugTerminalObject = this.TerminalStatInfo(dept, "1");
                        if (drugTerminalObject == null)
                            return -1;
                        if (drugTerminalObject.ID != "")
                        {
                            averageNum = 0;
                            drugTerminal = drugTerminalObject.ID;
                            adjustLevel = "c";
                            continue;
                        }
                        else
                        {
                            this.Err = "��" + dept + "��δ�ҵ�������������Ŀ��ŵ���ҩ̨ ����ҩ��������Ա��ϵ";
                            return -1;
                        }
                    }
                    #endregion
                }
                else
                {
                    #region ��������
                    if (adjustLevel.CompareTo("b") < 0)			//�ϴε�������С�ڱ���ʱ
                    {
                        string dept = feeTemp.StockOper.Dept.ID;
                        if (string.IsNullOrEmpty(dept))
                        {
                            dept = feeTemp.ExecOper.Dept.ID;
                        }
                        drugTerminalObject = this.TerminalStatInfo(dept, "2");
                        if (drugTerminalObject == null)
                            return -1;
                        if (drugTerminalObject.ID != "")
                        {
                            averageNum = 1;
                            drugTerminal = drugTerminalObject.ID;
                            adjustLevel = "b";
                            continue;
                        }
                        else
                        {
                            this.Err = "��" + dept + "��δ�ҵ�������������Ŀ��ŵ���ҩ̨ ����ҩ��������Ա��ϵ";
                            return -1;
                        }
                    }
                    #endregion
                }
                #endregion

                #endregion
            }
            if (drugTerminal != "")
            {
                #region ���ݸ���ҩ̨���� ��ȡ��Ӧ�ķ�ҩ���ڱ��� �����ѷ��ʹ���Ʒ������Ϣ �����ض�Ӧ��ȡҩ��Ϣ�ַ���
                if (drugTerminalObject == null || drugTerminalObject.ID == "")
                {
                    drugTerminalObject = this.GetDrugTerminal(drugTerminal);
                    if (drugTerminalObject == null)
                    {
                        this.Err = "��ȡ���������ҩ�ն���ϸ��Ϣʱ����" + this.Err;
                        return -1;
                    }
                    if (drugTerminalObject.ID == "")
                    {
                        this.Err = "���ݴ����������� �޷��ҵ����������ҿ��ŵ���ҩ̨/��ҩ����";
                        return -1;
                    }
                }
                //��ҩ���ڱ���Ϊ�� ������ҩ̨��ȡ��Ӧ�ķ�ҩ���ڱ���
                if (sendTerminalObject == null || sendTerminalObject.ID == "")
                {
                    if (sendTerminal != null && sendTerminal != "")
                        sendTerminalObject = this.GetDrugTerminalById(sendTerminal);
                    else
                        sendTerminalObject = this.GetDrugTerminalById(drugTerminalObject.SendWindow.ID);
                    if (sendTerminalObject == null)
                    {
                        this.Err = "��ȡ������ķ�ҩ�ն���ϸ��Ϣʱ����" + this.Err;
                        return -1;
                    }
                    if (sendTerminalObject.ID == "")
                    {
                        this.Err = "���ݴ����������� �޷��ҵ����������ҿ��ŵ���ҩ̨/��ҩ����" + this.Err;
                        return -1;
                    }
                }
                //�����ѷ��͡�����ҩ�Ĵ���Ʒ������Ϣ
                if (this.UpdateTerminalAdjustInfo(drugTerminalObject.ID, drugKindNum, drugKindNum, averageNum) == -1)
                {
                    this.Err = "������ҩ̨�ѷ��͡�����ҩ����ʱ����" + this.Err;
                    return -1;
                }

                //��¼����ԭ��
                drugTerminalObject.Memo = adjustLevel;
                return 1;
                #endregion
            }

            this.Err = "���ݴ����������� �޷��ҵ����������ҿ��ŵ���ҩ̨/��ҩ����" + this.Err;
            return -1;
        }

        public class FeeSort : System.Collections.IComparer
        {
            public FeeSort() { }


            #region IComparer ��Ա

            public int Compare(object x, object y)
            {
                // TODO:  ��� FeeSort.Compare ʵ��
                FS.HISFC.Models.Fee.Outpatient.FeeItemList f1 = x as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                FS.HISFC.Models.Fee.Outpatient.FeeItemList f2 = y as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                if (f1 == null || f2 == null)
                {
                    throw new Exception("�����ڱ���ΪOutPatient.FeeItemList����");
                }
                string oX = f1.ExecOper.Dept.ID;          //ִ�п���
                string oY = f2.ExecOper.Dept.ID;          //ִ�п���

                int nComp;

                if (oX == null)
                {
                    nComp = (oY != null) ? -1 : 0;
                }
                else if (oY == null)
                {
                    nComp = 1;
                }
                else
                {
                    nComp = string.Compare(oX.ToString(), oY.ToString());
                }

                return nComp;
            }

            #endregion

        }
        public class RecipeSort : System.Collections.IComparer
        {
            public RecipeSort() { }


            #region IComparer ��Ա

            public int Compare(object x, object y)
            {
                // TODO:  ��� FeeSort.Compare ʵ��
                FS.HISFC.Models.Fee.Outpatient.FeeItemList f1 = x as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                FS.HISFC.Models.Fee.Outpatient.FeeItemList f2 = y as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                if (f1 == null || f2 == null)
                    throw new Exception("�����ڱ���ΪOutPatient.FeeItemList����");
                string oX = f1.RecipeNO;          //������
                string oY = f2.RecipeNO;          //������

                int nComp;

                if (oX == null)
                {
                    nComp = (oY != null) ? -1 : 0;
                }
                else if (oY == null)
                {
                    nComp = 1;
                }
                else
                {
                    nComp = string.Compare(oX.ToString(), oY.ToString());
                }

                return nComp;
            }

            #endregion
        }

        #endregion

        #region ��Ч
        /// <summary>
        /// ��ð�ҩ��������Ϣ�б�
        /// </summary>
        /// <returns>��ҩ����������</returns>
        [System.Obsolete("ϵͳ�ع� ����ΪQueryDrugBillClassList����", true)]
        public ArrayList GetDrugBillClassList()
        {
            return null;
        }
        /// <summary>
        /// ���ݰ�ҩ����������ð�ҩ��������ϸ
        /// </summary>
        /// <param name="drugBillClassCode">�������</param>
        /// <returns></returns>
        [System.Obsolete("ϵͳ�ع� ����ΪQueryDrugBillList���� �ú�����������ע��", true)]
        public ArrayList GetDrugBillList(string drugBillClassCode, string column)
        {
            return null;
        }

        /// <summary>
        /// ȡ��ҩ̨��ˮ��
        /// </summary>
        /// <returns>"-1"����oterhs �ɹ�</returns>
        [System.Obsolete("ϵͳ�ع� ����ΪGetDrugControlNO����", true)]
        public string GetDrugControlID()
        {
            return null;
        }

        /// <summary>
        /// ���ݿ��ұ��룬ȡ�����ҵ�ȫ����ҩ̨�б�
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <returns>��ҩ̨����</returns>
        [System.Obsolete("ϵͳ�ع� ����ΪQueryDrugControlList����", true)]
        public ArrayList GetDrugControlList(string deptCode)
        {
            return null;
        }
        /// <summary>
        /// ���ݰ�ҩ̨���룬ȡ�˰�ҩ̨�е�ȫ����ϸ
        /// </summary>
        /// <param name="drugControlCode">��ҩ̨����</param>
        /// <returns>��ҩ����������</returns>
        [System.Obsolete("ϵͳ�ع� ����ΪQueryDrugControlDetailList����", true)]
        public ArrayList GetDrugControlDetailList(string drugControlCode)
        {
            return null;
        }

        /// <summary>
        /// ���ĳһ������ҵ�δ��ҩ֪ͨ�б�
        /// </summary>
        /// <param name="sendDeptCode">������ұ���</param>
        /// <returns>�ɹ����ذ�ҩ֪ͨ��Ϣ ʧ�ܷ���null</returns>
        [System.Obsolete("ϵͳ�ع� ����ΪQueryDrugMessageList����", true)]
        public ArrayList GetDrugMessageList(string sendDeptCode)
        {
            string strSQL = "";    //���ĳһ������ҵ�ȫ����ҩ֪ͨ�б��SELECT���

            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugMessageList.BySendDept", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugMessageList.BySendDept�ֶ�!";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, sendDeptCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "|Pharmacy.DrugStore.GetDrugMessageList.BySendDept";
                return null;
            }
            return myGetDrugMessage(strSQL);
        }


        /// <summary>
        /// ���ĳһ������ҵ�ȫ����ҩ֪ͨ�б�
        /// </summary>
        /// <param name="sendDeptCode">������ұ���</param>
        /// <returns>�ɹ����ذ�ҩ֪ͨ�б� ʧ�ܷ���null</returns>
        [System.Obsolete("ϵͳ�ع� ����ΪQueryAllDrugMessageList����", true)]
        public ArrayList GetAllDrugMessageList(string sendDeptCode)
        {
            string strSQL = "";    //���ĳһ������ҵ�ȫ����ҩ֪ͨ�б��SELECT���

            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.GetAllDrugMessageList", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetAllDrugMessageList�ֶ�!";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, sendDeptCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "|Pharmacy.DrugStore.GetAllDrugMessageList";
                return null;
            }

            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "ȡ��ҩ֪ͨ�б�ʱ����" + this.Err;
                return null;
            }
            try
            {
                DrugMessage info;   //��ҩ֪ͨʵ��		
                while (this.Reader.Read())
                {
                    info = new DrugMessage();
                    try
                    {
                        info.StockDept.ID = this.Reader[0].ToString();          //���Ϳ��ұ���
                        info.StockDept.Name = this.Reader[1].ToString();          //���Ϳ�������
                        info.DrugBillClass.ID = this.Reader[2].ToString();          //��ҩ���������
                        info.DrugBillClass.Name = this.Reader[3].ToString();          //��ҩ����������
                        info.SendType = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[4].ToString());      //��ҩ����1-���а�ҩ2-��ʱ��ҩ
                    }
                    catch (Exception ex)
                    {
                        this.Err = "��ð�ҩ֪ͨ��Ϣ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    al.Add(info);
                }
                return al;
            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "��ð�ҩ֪ͨ��Ϣʱ��ִ��SQL������myGetDrugBillClass" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// ���ĳһ��ҩ̨��ȫ����ҩ֪ͨ�б�
        /// SendType=1���У�2��ʱ
        /// ��SendType��0ʱ����ʾȫ�����͵İ�ҩ֪ͨ��
        /// </summary>
        /// <param name="drugControl">��ҩ̨</param>
        /// <returns>�ɹ����ذ�ҩ֪ͨ���� ʧ�ܷ���null</returns>
        [System.Obsolete("ϵͳ�ع� ����ΪQueryDrugMessageList����", true)]
        public ArrayList GetDrugMessageList(DrugControl drugControl)
        {
            //���û��ָ�����Ϳ��ң���ȡȫ�����Ϳ��ҵ�֪ͨ
            string strSQL = "";    //���ĳһ��ҩ̨����ҩ̨���п�����Ϣ����ȫ����ҩ֪ͨ�б��SELECT���

            #region ȡ�����Ұ�ҩ��
            //ȡSQL���
            //			if (drugControl.ID =="P") {
            //				if (this.GetSQL("Pharmacy.DrugStore.GetDrugMessageList.ByOPR",ref strSQL) == -1) {
            //					this.Err="û���ҵ�Pharmacy.DrugStore.GetDrugMessageList.ByOPR�ֶ�!";
            //					return null;
            //				}
            //				try {
            //					string[] strParm={drugControl.Dept.ID};
            //					strSQL = string.Format(strSQL, strParm);
            //				}
            //				catch(Exception ex) {
            //					this.ErrCode=ex.Message;
            //					this.Err=ex.Message + "|Pharmacy.DrugStore.GetDrugMessageList.ByOPR";
            //					return null;
            //				}
            //			}
            #endregion

            if (this.GetSQL("Pharmacy.DrugStore.GetDrugMessageList.ByDrugControl", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugMessageList.ByDrugControl�ֶ�!";
                return null;
            }
            try
            {
                string[] strParm = { drugControl.ID };
                strSQL = string.Format(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "|Pharmacy.DrugStore.GetDrugMessageList.ByDrugControl";
                return null;
            }
            return myGetDrugMessage(strSQL);
        }


        /// <summary>
        /// ���ĳһ��ҩ֪ͨ����ϸ�б�;
        /// </summary>
        /// <param name="drugMessage">��ҩ֪ͨ</param>
        /// <returns>�ɹ����ذ�ҩ֪ͨ��Ϣ ʧ�ܷ���null</returns>
        [System.Obsolete("ϵͳ�ع� ����ΪQueryDrugMessageList����", true)]
        public ArrayList GetDrugMessageList(DrugMessage drugMessage)
        {

            string strSQL = "";    //���ĳһ��ҩ֪ͨ����ϸ�б��SQL���

            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugMessageList.ByDrugMessage", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugMessageList.ByDrugMessage�ֶ�!";
                return null;
            }
            try
            {
                string[] strParm ={
									 drugMessage.StockDept.ID, 
									 drugMessage.DrugBillClass.ID, 
									 drugMessage.SendType.ToString()
								 };
                strSQL = string.Format(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "|Pharmacy.DrugStore.GetDrugMessageList.ByDrugMessage";
                return null;
            }
            return myGetDrugMessage(strSQL);
        }
        /// <summary>
        /// �ɹ����ذ�ҩ֪ͨ��Ϣ
        /// </summary>
        /// <param name="drugControlID">��ҩ̨����</param>
        /// <param name="drugMessage">��ҩ֪ͨ</param>
        /// <returns>�ɹ����ذ�ҩ֪ͨ��Ϣ ʧ�ܷ���null</returns>
        [System.Obsolete("ϵͳ�ع� ����ΪQueryDrugBillList����", true)]
        public ArrayList GetDrugBillList(string drugControlID, DrugMessage drugMessage)
        {
            string strSQL = "";
            //ȡSQL���
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugBillList.ByDept", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetDrugBillList.ByDept�ֶ�!";
                return null;
            }
            try
            {
                string[] strParm ={
									 drugControlID,
									 drugMessage.ApplyDept.ID, 
									 drugMessage.StockDept.ID, 
									 drugMessage.SendType.ToString()
								 };
                strSQL = string.Format(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "|Pharmacy.DrugStore.GetDrugBillList.ByDept";
                return null;
            }
            return myGetDrugMessage(strSQL);
        }

        /// <summary>
        /// ���ĳ����ĳ���������ն��б�
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="terminalType">�ն����� 0 ��ҩ�� 1 ��ҩ̨</param>
        /// <returns>�ɹ�����DrugTerminal��ArrayList���飬ʧ�ܷ���null</returns>
        [System.Obsolete("ϵͳ�ع� ����ΪQueryDrugTerminalByDeptCode����", true)]
        public ArrayList GetDrugTerminalByDeptCode(string deptCode, string terminalType)
        {
            return null;
        }

        /// <summary>
        /// ĳ��������������ҩ̨�б�
        /// </summary>
        /// <param name="itemType">��� 1 ҩƷ 2 ר�� 3 ������� 4 �ض��շѴ��� "A"���� </param>
        /// <returns>�ɹ�����DrugSPETerminal��ArrayList���飬ʧ�ܷ���null</returns>
        [System.Obsolete("ϵͳ�ع� ����ΪQueryDrugSPETerminalByType����", true)]
        public ArrayList GetDrugSPETerminalByType(string itemType)
        {
            return null;
        }

        /// <summary>
        /// ĳ���ҡ�ĳ��������������ҩ̨�б� ����Ϊ"A"�����������
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="itemType">���  1 ҩƷ 2 ר�� 3 ������� 4 �ض��շѴ��� "A"����</param>
        /// <returns>�ɹ�����DrugSPETerminal��ArrayList���顢ʧ�ܷ���null</returns>
        [System.Obsolete("ϵͳ�ع� ����ΪQueryDrugSPETerminalByDeptCode����", true)]
        public ArrayList GetDrugSPETerminalByDeptCode(string deptCode, string itemType)
        {
            return null;
        }

        /// <summary>
        /// ���ݿ��ұ�Ż�ȡ�ÿ���ģ���б�
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <returns>�ɹ�����neuobject����(ID ģ���� Name ģ������)��ʧ�ܷ���null</returns>
        [System.Obsolete("ϵͳ�ع� ����ΪQueryDrugOpenTerminalByDeptCode����", true)]
        public ArrayList GetDrugOpenTerminalByDeptCode(string deptCode)
        {
            return null;
        }
        /// <summary>
        /// ����ģ���Ż�ȡģ����ϸ��Ϣ
        /// </summary>
        /// <param name="templateCode">ģ����</param>
        /// <returns>�ɹ��������� ʧ�ܷ���null</returns>
        [System.Obsolete("ϵͳ�ع� ����ΪQueryDrugOpenTerminalByID����", true)]
        public ArrayList GetDrugOpenTerminalById(string templateCode)
        {
            return null;
        }

        /// <summary>
        /// ��ȡ���͵�ָ��ҩ����ָ���ն˵Ĵ����б�
        /// </summary>
        /// <param name="deptCode">ҩ������</param>
        /// <param name="terminalCode">�ն˱���</param>
        /// <param name="type">�ն���� 0��ҩ����/1��ҩ̨</param>
        /// <param name="state">����״̬</param>
        /// <returns>�ɹ����������ҩʵ������ ʧ�ܷ���null</returns>
        [System.Obsolete("ϵͳ�ع� ����ΪQueryList����", true)]
        public ArrayList GetList(string deptCode, string terminalCode, string type, string state)
        {
            return null;
        }

        /// <summary>
        /// ��ȡָ���շ�ʱ����͵�ָ��ҩ����ָ���ն˵Ĵ����б�
        /// </summary>
        /// <param name="deptCode">ҩ������</param>
        /// <param name="terminalCode">�ն˱���</param>
        /// <param name="type">�ն����  0��ҩ����/1��ҩ̨</param>
        /// <param name="state">����״̬</param>
        /// <param name="queryDate">�շ�ʱ��</param>
        /// <returns>�ɹ����������ҩʵ������ ʧ�ܷ���null</returns>
        [System.Obsolete("ϵͳ�ع� ����ΪQueryList����", true)]
        public ArrayList GetList(string deptCode, string terminalCode, string type, string state, DateTime queryDate)
        {
            return null;
        }

        /// <summary>
        /// ���ݵ��ݺ� ��ȡ ����������Ϣ
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="class3MeaningCode">������� M1 ������� M2  �����˿�</param>
        /// <param name="recipeState">����״̬</param>
        /// <param name="billType">�������� 0 ������ 1 ��Ʊ�� 2 ��������</param>
        /// <param name="billNo">���ݺ�</param>
        /// <returns>�ɹ�����DrugRecipe���� ʧ�ܷ���null δ�ҵ����ؿ�����</returns>
        [System.Obsolete("ϵͳ�ع� ����ΪQueryDrugRecipe����", true)]
        public ArrayList GetDrugRecipe(string deptCode, string class3MeaningCode, string recipeState, int billType, string billNo)
        {
            return null;
        }

        #endregion

        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        #region �˻�����
        /// <summary>
        /// ���ݴ�����ִ�п���ɾ������ͷ��
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="execDeptCode">ִ�п���</param>
        /// <returns></returns>
        public int DeleteDrugStoRecipe(string recipeNO, string execDeptCode)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.Item.DeleteStoRecipe", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, recipeNO, execDeptCode);       //
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ���µ���ͷ��Ĵ������ҩƷ����
        /// </summary>
        /// <param name="reciptCost">�������</param>
        /// <param name="drugCount">ҩƷ����</param>
        /// <returns></returns>
        public int UpdateStoRecipe(string recipeNO, string deptCode, decimal reciptCost, int drugCount)
        {
            //���ݴ�����ִ�п��Ҳ�ѯδ�շѵ�ҩƷ������Ϣ
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.UpdateStoRecipe", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�SQL���Pharmacy.Item.UpdateStoRecipe";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, recipeNO, deptCode, reciptCost.ToString(), drugCount.ToString());
            }
            catch
            {
                this.Err = "�����������ȷ��Pharmacy.Item.UpdateStoRecipe";
                return -1;
            }
            return this.ExecNoQuery(strSQL);

        }

        /// <summary>
        /// ���´����������շѲ�����Ϣ
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="deptCode">ִ�п���</param>
        /// <param name="operCode">����Ա</param>
        /// <param name="operDate">����ʱ��</param>
        /// <returns></returns>
        public int UpdateStoRecipeFeeOper(string recipeNO, string deptCode, string operCode)
        {
            //���ݴ�����ִ�п��Ҳ�ѯδ�շѵ�ҩƷ������Ϣ
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.UpdateStoRecipeFeeOper", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�SQL���Pharmacy.Item.UpdateStoRecipeFeeOper";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, recipeNO, deptCode, operCode);
            }
            catch
            {
                this.Err = "�����������ȷ��Pharmacy.Item.UpdateStoRecipeFeeOper";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        #endregion

        #region סԺ��ҩ����ѯ ������ӣ��������գ�

        /// <summary>
        /// ��ѯ��ҩ���б�
        /// </summary>
        /// <param name="deptCode">���Ҵ���</param>
        /// <param name="startDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="useTime">�Ƿ�����ʱ���ѯ 0�� 1��</param>
        /// <param name="drugedType">��ҩ���� 0δ��ҩ 1�Ѱ�ҩ 2ȫ��</param>
        /// <returns></returns>
        public ArrayList QueryBillListByDept(string deptCode, string startDate, string endDate, string useTime, string drugedType)
        {
            string strSql = "";
            ArrayList alBills = new ArrayList();

            if (this.GetSQL("Pharmacy.Drugstore.Inpatient.DrugList.BillList", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Drugstore.Inpatient.DrugList.BillList����";
                return null;
            }
            try
            {
                if (this.ExecQuery(strSql, "0", deptCode, startDate, endDate, useTime, drugedType) == -1)
                {
                    this.Err = "��ð�ҩ���б�ʧ�ܣ�ִ��SQL������" + this.Err;
                    this.ErrCode = "-1";
                    return null;
                }
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject billObj = new FS.FrameWork.Models.NeuObject();
                    billObj.ID = this.Reader[0].ToString();
                    billObj.Name = this.Reader[1].ToString();
                    alBills.Add(billObj);
                }

                return alBills;
            }
            catch (Exception ex)
            {
                this.Err = "ִ��sql��ð�ҩ���б�ʧ�ܣ�" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// ��ѯ��ҩ������
        /// </summary>
        /// <param name="billCode">��ҩ�����</param>
        /// <param name="deptCode">���Ҵ���</param>
        /// <param name="startDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="useTime">�Ƿ�����ʱ���ѯ 0�� 1��</param>
        /// <param name="drugedType">��ҩ���� 0δ��ҩ 1�Ѱ�ҩ 2ȫ��</param>
        /// <returns></returns>
        public System.Data.DataTable QueryDrugTotalByDept(string billCode, string deptCode, string startDate, string endDate, string drugedType)
        {
            string strSql = "";
            System.Data.DataSet dsTotal = new System.Data.DataSet();

            if (this.GetSQL("Pharmacy.Drugstore.Inpatient.DrugList.Total", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Drugstore.Inpatient.DrugList.Total����";
                return null;
            }
            try
            {
                strSql = string.Format(strSql, billCode, deptCode, startDate, endDate, drugedType);

                if (this.ExecQuery(strSql, ref dsTotal) == -1)
                {
                    this.Err = "��ð�ҩ��ҩƷ������Ϣʧ�ܣ�ִ��SQL������" + this.Err;
                    this.ErrCode = "-1";
                    return null;
                }

                return dsTotal.Tables[0];
            }
            catch (Exception ex)
            {
                this.Err = "ִ��sql��ð�ҩ��ҩƷ������Ϣʧ�ܣ�" + ex.Message;
                return null;
            }
        }

        /// <summary>
        /// ��ѯ��ҩ����ϸ
        /// </summary>
        /// <param name="billCode">��ҩ�����</param>
        /// <param name="deptCode">���Ҵ���</param>
        /// <param name="startDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="useTime">�Ƿ�����ʱ���ѯ 0�� 1��</param>
        /// <param name="drugedType">��ҩ���� 0δ��ҩ 1�Ѱ�ҩ 2ȫ��</param>
        /// <returns></returns>
        public System.Data.DataTable QueryDrugDetailByDept(string billCode, string deptCode, string startDate, string endDate, string drugedType)
        {
            string strSql = "";
            System.Data.DataSet dsTotal = new System.Data.DataSet();

            if (this.GetSQL("Pharmacy.Drugstore.Inpatient.DrugList.Detail", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Drugstore.Inpatient.DrugList.Detail����";
                return null;
            }
            try
            {
                strSql = string.Format(strSql, billCode, deptCode, startDate, endDate, drugedType);

                if (this.ExecQuery(strSql, ref dsTotal) == -1)
                {
                    this.Err = "��ð�ҩ��ҩƷ��ϸ��Ϣʧ�ܣ�ִ��SQL������" + this.Err;
                    this.ErrCode = "-1";
                    return null;
                }

                return dsTotal.Tables[0];
            }
            catch (Exception ex)
            {
                this.Err = "ִ��sql��ð�ҩ��ҩƷ��ϸ��Ϣʧ�ܣ�" + ex.Message;
                return null;
            }
        }

        #endregion

        #region ��ҩ���

        /// <summary>
        /// ��ѯҩ����ҩ������� by Sunjh 2010-9-14 {6DB2E467-CFBE-4cf1-B5E9-C48BAEFDC487}
        /// </summary>
        /// <param name="queryType">��ѯ���� M����ZסԺ</param>
        /// <param name="states">���״̬</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <returns></returns>
        public ArrayList QueryDrugAuditing(string queryType, string states, string beginTime, string endTime)
        {
            string strSql = "";
            string sqlIndex = "Pharmacy.Item.Auditing.None";
            ArrayList alList = new ArrayList();

            if (states == "0")
            {
                sqlIndex = "Pharmacy.Item.Auditing.None";
                if (this.GetSQL(sqlIndex, ref strSql) == -1)
                {
                    this.Err = "û���ҵ�" + sqlIndex + "����";
                    return null;
                }
                strSql = string.Format(strSql, beginTime, endTime);
            }
            else
            {
                sqlIndex = "Pharmacy.Item.Auditing.Done";
                if (this.GetSQL(sqlIndex, ref strSql) == -1)
                {
                    this.Err = "û���ҵ�" + sqlIndex + "����";
                    return null;
                }
                strSql = string.Format(strSql, states, beginTime, endTime);
            }

            try
            {
                if (this.ExecQuery(strSql) == -1)
                {
                    this.Err = "��ѯҩ����ҩ�������ʧ�ܣ�ִ��SQL������" + this.Err;
                    this.ErrCode = "-1";
                    return null;
                }
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut appObj = new ApplyOut();
                    appObj.ID = this.Reader[0].ToString();
                    appObj.PatientNO = this.Reader[1].ToString();
                    appObj.Name = this.Reader[2].ToString();
                    appObj.RecipeInfo.Dept.ID = this.Reader[3].ToString();
                    appObj.RecipeInfo.Dept.Name = this.Reader[4].ToString();
                    appObj.Item.ID = this.Reader[5].ToString();
                    appObj.Item.Name = this.Reader[6].ToString();
                    appObj.Item.Specs = this.Reader[7].ToString();
                    appObj.Item.Qty = Convert.ToDecimal(this.Reader[8].ToString());
                    appObj.Item.MinUnit = this.Reader[9].ToString();
                    appObj.Frequency.Name = this.Reader[10].ToString();
                    appObj.Usage.Name = this.Reader[11].ToString();
                    appObj.State = this.Reader[12].ToString();
                    appObj.User01 = this.Reader[13].ToString();
                    appObj.Memo = this.Reader[14].ToString();
                    appObj.Operation.ApproveOper.ID = this.Reader[15].ToString();
                    appObj.Operation.ApproveOper.Name = this.Reader[16].ToString();
                    appObj.Operation.ApproveOper.OperTime = Convert.ToDateTime(this.Reader[17].ToString());

                    alList.Add(appObj);
                }

                return alList;
            }
            catch (Exception ex)
            {
                this.Err = "ִ��sql��ѯҩ����ҩ�������ʧ�ܣ�" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// ������ҩ�������
        /// </summary>
        /// <param name="queryType"></param>
        /// <param name="tempObj"></param>
        /// <returns></returns>
        public int InsertDrugAuditing(string queryType, FS.FrameWork.Models.NeuObject tempObj)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.Item.Auditing.Insert", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Item.Auditing.Insert����";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, tempObj.ID, tempObj.User01, tempObj.Memo, tempObj.Name, tempObj.User02);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ���ַ���ʧ��!" + ex.Message;
                this.ErrCode = "-1";
                return -1;
            }

            return this.ExecQuery(strSql);
        }

        /// <summary>
        /// ɾ���������ҩ����
        /// </summary>
        /// <param name="applyNumber"></param>
        /// <returns></returns>
        public int DeleteDrugAuditing(string applyNumber)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.Item.Auditing.Delete", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Item.Auditing.Delete����";
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, applyNumber);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ���ַ���ʧ��!" + ex.Message;
                this.ErrCode = "-1";
                return -1;
            }

            return this.ExecQuery(strSql);
        }

        #endregion


        #region ���ϰ汾�¼�

        /// <summary>
        /// ��ȡסԺ��ҩ֪ͨ�Ļ����б�
        /// </summary>
        /// <param name="drugControlNO">��ҩ̨</param>
        /// <returns>DrugMessage��ҩ֪ͨʵ��</returns>
        public ArrayList QueryDrugMessageInpatientList(FS.HISFC.Models.Pharmacy.DrugControl drugControl)
        {
            string strSQL = "";
            
            //ȡSQL���ByDrugControlAndMessage
            if (this.GetSQL("Pharmacy.DrugStore.GetInpatientList.ByDrugControl", ref strSQL) == -1)
            {
                strSQL = @"select  u.billclass_code,
                                   u.dept_code,
                                   u.patient_id,
                                   i.bed_no,
                                   i.name
                            from                           
                            (                        
                                  select distinct a.billclass_code,
                                         a.dept_code,
                                         a.patient_id
                                  from   pha_com_applyout a,pha_sto_control c,pha_sto_msg m
                                  where  a.drug_dept_code = m.med_dept_code
                                  and    a.billclass_code = m.billclass_code
                                  and    a.apply_state = '0'
                                  --and    a.send_type = c.send_type      
                                  and    a.valid_state = '1'
                                  and    m.billclass_code = c.billclass_code
                                  and    m.med_dept_code = c.dept_code
                                  and    (m.send_type = c.send_type or c.send_type = '0')
                                  and    m.dept_code = a.dept_code
                                  and    c.control_code = '{0}'
                            ) u,fin_ipr_inmaininfo i
                            where u.patient_id = i.inpatient_no
                            order by i.bed_no";
                this.Err = "û���ҵ�Pharmacy.DrugStore.GetInpatientList.ByDrugControl�ֶ�!";
                //return null;
            }
            try
            {
                string[] strParm = { drugControl.ID };
                strSQL = string.Format(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "|Pharmacy.DrugStore.GetDrugMessageList.ByDrugControl";
                return null;
            }

            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "ȡ��ҩ֪ͨ�б�ʱ����" + this.Err;
                return null;
            }

            ArrayList al = new ArrayList();
            try
            {
                DrugMessage info;   //��ҩ֪ͨʵ��		
                while (this.Reader.Read())
                {
                    info = new DrugMessage();
                    try
                    {
                        info.DrugBillClass.ID = this.Reader[0].ToString();          //��ҩ���������
                        info.ApplyDept.ID = this.Reader[1].ToString();          //���Ϳ��ұ���
                        info.ID = this.Reader[2].ToString();                 //סԺ��ˮ��
                        info.Memo = this.Reader[3].ToString();                 //����
                        info.Name = this.Reader[4].ToString();                 //����
                        info.User01 = info.ID;

                        info.StockDept.ID = drugControl.Dept.ID;
                        info.SendType = drugControl.SendType;
                    }
                    catch (Exception ex)
                    {
                        this.Err = "��ð�ҩ֪ͨ��Ϣ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    al.Add(info);
                }
                return al;

            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "��ð�ҩ֪ͨ��Ϣʱ��ִ��SQL������myGetDrugBillClass" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// ����һ����ҩ�������¼
        /// </summary>
        /// <param name="info">��ҩ������ʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int InsertOneDrugBillClass(DrugBillClass info)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.InsertDrugBillClass", ref strSql) == -1) return -1;
            try
            {
                //ȡ��ҩ��������ˮ��
                if (string.IsNullOrEmpty(info.ID))
                {
                    string ID = "";
                    ID = this.GetSysDateTime("yyMMddHHmmss");
                    if (ID == "-1") return -1;
                    info.ID = ID;
                }

                string[] strParm = myGetParmDrugBillClass(info);  //ȡ�����б�
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        #endregion

    }
}
