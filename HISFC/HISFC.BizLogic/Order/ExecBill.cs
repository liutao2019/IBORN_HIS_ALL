using System;
using FS.HISFC.Models;
using System.Collections;
using FS.FrameWork.Models;

namespace FS.HISFC.BizLogic.Order
{
    /// <summary>
    /// ExecBill ��ժҪ˵����
    /// ִ�е����ù�����
    /// </summary>
    public class ExecBill : FS.FrameWork.Management.Database
    {
        public ExecBill()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }


        #region ��ɾ��
        /// <summary>
        /// ����ִ�е���Ϣ
        /// ������д��objBill.ID ִ�е���ˮ�ţ�objBill.Name ִ�е���,objBill.Memoִ�е����ͣ�1ҩ/2��ҩ,objBill.user01 ҽ������,
        /// objBill.user02��ҩϵͳ���ҩƷ���,objBill.user03 ҩƷ�÷���
        /// </summary>
        /// <param name="objBill"></param>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        private int InsertExecBill(FS.FrameWork.Models.NeuObject objBill, string nurseCode)
        {
            string strSql = "";

            #region "�ӿ�"
            //���룺0 �������� ��1ִ�е���ˮ�� 2 ִ�е����� 3 ҽ������ 4��ҩϵͳ���ҩƷ��� 5 ҩƷ�÷�������Ա 7��Ŀ���� 8��Ŀ����
            //������0
            #endregion

            if (objBill.Memo == "1") //ҩƷ
            {
                if (this.Sql.GetCommonSql("Order.ExecBill.InsertItem.1", ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return -1;
                }
            }
            else if (objBill.Memo == "2")//��ҩƷ
            {
                if (this.Sql.GetCommonSql("Order.ExecBill.InsertItem.2", ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return -1;
                }
            }

            try
            {
                strSql = string.Format(
                    strSql,
                    nurseCode,//0 ��ʿվ����
                    objBill.ID,//1ִ�е����
                    objBill.Name,//2ҽ�����
                    objBill.User01,//3 ��Ŀ���
                    objBill.User02,//4 ������Ա����
                    objBill.User03,//5 ����ʱ��
                    this.Operator.ID, //6����Ա����/
                    objBill.User03,//7 ��Ŀ����
                    objBill.Name //8��Ŀ����
                    );
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);

        }

        /// <summary>
        /// ����ִ�е���Ϣ
        /// ������д��objBill.ID ִ�е���ˮ�ţ�objBill.Name ִ�е���,objBill.Memoִ�е����ͣ�1ҩ/2��ҩ,objBill.user01 ҽ������,
        /// objBill.user02��ҩϵͳ���ҩƷ���,objBill.user03 ҩƷ�÷���
        /// </summary>
        /// <param name="objBill"></param>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        private int InsertExecBill(FS.HISFC.Models.Base.Spell objBill, string nurseCode)
        {
            string strSql = "";

            #region "�ӿ�"
            //���룺0 �������� ��1ִ�е���ˮ�� 2 ִ�е����� 3 ҽ������ 4��ҩϵͳ���ҩƷ��� 5 ҩƷ�÷�������Ա 7��Ŀ���� 8��Ŀ����
            //������0
            #endregion

            if (objBill.Memo == "1") //ҩƷ
            {
                if (this.Sql.GetCommonSql("Order.ExecBill.InsertItem.1", ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return -1;
                }
            }
            else if (objBill.Memo == "2")//��ҩƷ
            {
                if (this.Sql.GetCommonSql("Order.ExecBill.InsertItem.2", ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return -1;
                }
            }

            try
            {
                strSql = string.Format(
                    strSql,
                    nurseCode,//0 ��ʿվ����
                    objBill.ID,//1ִ�е����
                    objBill.Name,//2ҽ�����
                    objBill.User01,//3 ��Ŀ���
                    objBill.User02,//4 ������Ա����
                    objBill.User03,//5 ����ʱ��
                    this.Operator.ID, //6����Ա����/
                    objBill.SpellCode,//7 ��Ŀ����
                    objBill.WBCode //8��Ŀ����
                    );
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);

        }

        #region addby xuewj 2009-8-26 ִ�е����� ����Ŀά�� {0BB98097-E0BE-4e8c-A619-8B4BCA715001}

        /// <summary>
        /// ���·�ҩƷ��Ŀִ�е���ȫ����
        /// </summary>
        /// <param name="nurseID">��ʿվ</param>
        /// <param name="billNO">ִ�е���</param>
        /// <param name="typeCode">ҽ������</param>
        /// <param name="classCode">ҽ����Ŀ����</param>
        /// <returns></returns>
        public int UpdateExecBillAllItem(string nurseID, string billNO, string typeCode, string classCode)
        {
            if (this.DeleteExecBillAllItem(nurseID, typeCode, classCode) == -1)
            {
                return -1;
            }

            if (this.InsertExecBillAllItem(nurseID, billNO, typeCode, classCode) == -1)
            {
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// ɾ����ҩƷ��Ŀִ�е���ȫ����
        /// </summary>
        /// <param name="nurseID">��ʿվ</param>
        /// <param name="billNO">ִ�е���</param>
        /// <param name="typeCode">ҽ������</param>
        /// <param name="classCode">ҽ����Ŀ����</param>
        /// <returns></returns>
        private int DeleteExecBillAllItem(string nurseID, string typeCode, string classCode)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Order.ExecBill.DeleteAllItem", ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }

            try
            {
                sql = string.Format(sql, nurseID, typeCode, classCode);
            }
            catch (Exception err)
            {
                this.Err = err.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ���ӷ�ҩƷ��Ŀִ�е���ȫ����
        /// </summary>
        /// <param name="nurseID">��ʿվ����</param>
        /// <param name="billNO">ִ�е�����</param>
        /// <param name="typeCode">ҽ������</param>
        /// <param name="classCode">ҽ����Ŀ����</param>
        /// <returns></returns>
        private int InsertExecBillAllItem(string nurseID, string billNO, string typeCode, string classCode)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Order.ExecBill.InsertAllItem", ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }

            try
            {
                sql = string.Format(sql, nurseID, billNO, typeCode, classCode, Operator.ID);
            }
            catch (Exception err)
            {
                this.Err = err.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ���ӷ�ҩƷ��Ŀִ�е���ʣ��ȫ����
        /// </summary>
        /// <param name="nurseID">��ʿվ����</param>
        /// <param name="billNO">ִ�е�����</param>
        /// <param name="typeCode">ҽ������</param>
        /// <param name="classCode">ҽ����Ŀ����</param>
        /// <returns></returns>
        public int InsertExecBillOtherItem(string nurseID, string billNO, string typeCode, string classCode)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Order.ExecBill.InsertOtherItem", ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }

            try
            {
                sql = string.Format(sql, nurseID, billNO, typeCode, classCode, Operator.ID);
            }
            catch (Exception err)
            {
                this.Err = err.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ���·�ҩƷ��Ŀִ�е���������
        /// </summary>
        /// <param name="nurseID">��ʿվ����</param>
        /// <param name="billNO">ִ�е�</param>
        /// <param name="orderType">ҽ������</param>
        /// <param name="sysClass">ҽ����Ŀ����</param>
        /// <param name="itemCode">��Ŀ����</param>
        /// <param name="itemName">��Ŀ����</param>
        /// <returns></returns>
        public int UpdateExecBillItem(string nurseID, string billNO, string orderType, string sysClass, string itemCode, string itemName)
        {
            if (this.DeleteExecBillItem(nurseID, billNO, orderType, sysClass, itemCode) == -1)
            {
                return -1;
            }
            if (this.InsertExecBillItem(nurseID, billNO, orderType, sysClass, itemCode, itemName) == -1)
            {
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// ɾ����ҩƷ��Ŀִ�е���������
        /// </summary>
        /// <param name="nurseID">��ʿվ����</param>
        /// <param name="billNO">ִ�е�����</param>
        /// <param name="orderType">ҽ������</param>
        /// <param name="sysClass">ҽ����Ŀ����</param>
        /// <param name="itemCode">��Ŀ����</param>
        /// <param name="itemName">��Ŀ����</param>
        /// <returns></returns>
        private int DeleteExecBillItem(string nurseID, string billNO, string orderType, string sysClass, string itemCode)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Order.ExecBill.DeleteOneItem", ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }

            try
            {
                sql = string.Format(sql, nurseID, billNO, orderType, sysClass, itemCode);
            }
            catch (Exception err)
            {
                this.Err = err.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// �����ҩƷ��Ŀִ�е���������
        /// </summary>
        /// <param name="nurseID">��ʿվ����</param>
        /// <param name="billNO">ִ�е�</param>
        /// <param name="orderType">ҽ������</param>
        /// <param name="sysClass">ҽ����Ŀ����</param>
        /// <param name="itemCode">��Ŀ����</param>
        /// <param name="itemName">��Ŀ����</param>
        /// <returns></returns>
        private int InsertExecBillItem(string nurseID, string billNO, string orderType, string sysClass, string itemCode, string itemName)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Order.ExecBill.InsertOneItem", ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }

            try
            {
                sql = string.Format(sql, nurseID, billNO, orderType, sysClass, this.Operator.ID, itemCode, itemName);
            }
            catch (Exception err)
            {
                this.Err = err.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ������ִ�е�
        /// ������д��objBill.Name ִ�е���,objBill.Memoִ�е����ͣ�1ҩ/2��ҩ,objBill.user01 ҽ������
        /// objBill.user02��ҩϵͳ���ҩƷ��� objBill.user03 ҩƷ�÷���
        /// </summary>
        /// <param name="al"></param>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        public int SetExecBill(FS.FrameWork.Models.NeuObject objExecBill, string nurseCode)
        {
            string strBillNo = GetNewExecBillID();
            if (strBillNo == "-1" || strBillNo == "")
            {
                return -1;
            }
            objExecBill.ID = strBillNo;

            string strSql = "";

            /*
               INSERT INTO met_ipm_execbill --ҽ��ִ�е���Ϣ��
                   (nurse_cell_code, --����վ����   0
                    bill_no, --ִ�е���ˮ��         1
                    bill_name, --ִ�е�����         2
                    bill_kind, --ִ�е����ͣ�1ҩ/2��ҩ  3
                    oper_code, --������Ա����       4
                    MARK, --��ע                    5
                    item_flag,--                    6
                    oper_date) --����ʱ��
               VALUES
                   ('{0}', --����վ����
                    '{1}',
                    '{2}',
                    '{4}',
                    '{3}',
                    '{5}',
                    '{6}',
                    sysdate) --����ʱ��
             */

            if (this.Sql.GetCommonSql("Order.ExecBill.InsertItem", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, nurseCode, strBillNo, objExecBill.Name, this.Operator.ID, objExecBill.User02, objExecBill.User01, objExecBill.Memo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                this.WriteErr();
                return -1;
            }
            if (this.ExecNoQuery(strSql) < 0) return -1;

            return 0;
        }

        #endregion

        /// <summary>
        /// ����ִ������
        ///������д��objBill.ID ִ�е���ˮ�ţ�objBill.Memoִ�е����ͣ�1ҩ/2��ҩ,objBill.user01 ҽ������,
        /// objBill.user02��ҩϵͳ���ҩƷ���,objBill.user03 ҩƷ�÷���
        /// </summary>
        /// <param name="objBill"></param>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        public int UpdateExecBill(FS.FrameWork.Models.NeuObject objBill, string nurseCode)
        {
            if (DeleteExecBill(objBill) < 0)
            {
                return -1;
            }
            if (InsertExecBill(objBill, nurseCode) <= 0)
            {
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// ɾ��ִ�е�
        /// </summary>
        /// <param name="billNo"></param>
        /// <returns></returns>
        public int DeleteExecBill(string billNo)
        {
            string strSql = "";
            #region ɾ������
            if (this.Sql.GetCommonSql("Order.ExecBill.DeleteItem", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, billNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                this.WriteErr();
                return -1;
            }
            if (this.ExecNoQuery(strSql) <= 0) return -1;
            #endregion
            #region ɾ��ϸ��
            if (this.Sql.GetCommonSql("Order.ExecBill.DeleteItem.Drug", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, billNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                this.WriteErr();
                return -1;
            }
            if (this.ExecNoQuery(strSql) < 0) return -1;
            if (this.Sql.GetCommonSql("Order.ExecBill.DeleteItem.unDrug", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, billNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                this.WriteErr();
                return -1;
            }
            if (this.ExecNoQuery(strSql) < 0) return -1;
            #endregion
            return 0;
        }

        #region {46983F5B-E184-4b8b-B819-AA1C34993F1B}
        /// <summary>
        /// ɾ��ִ�е��������е�һ����Ŀ
        /// ������д��objBill.ID ִ�е���ˮ�ţ�objBill.Memoִ�е����ͣ�1ҩ/2��ҩ,objBill.user01 ҽ������,
        /// objBill.user02��ҩϵͳ���ҩƷ���,objBill.user03 ҩƷ�÷���
        /// </summary>
        /// <param name="objBill"></param>
        /// <returns></returns>
        public int DeleteExecBillOneItem(FS.FrameWork.Models.NeuObject objBill)
        {
            string strSql = "";
            #region "�ӿ�"
            //���룺0 ִ�е���ˮ�� 1ҽ������ 2��ҩϵͳ���ҩƷ��� 3ҩƷ�÷�
            //������0
            #endregion

            if (this.Sql.GetCommonSql("Order.ExecBill.DeleteItem.OneItem", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }

            try
            {
                strSql = string.Format(strSql, objBill.ID, objBill.User01, objBill.User02, objBill.User03);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);

        }
        #endregion

        #endregion

        #region ����
        [Obsolete("��SetExecBill�����˰�", true)]
        public int CreatExecBill(ArrayList al, FS.FrameWork.Models.NeuObject objExecBill, string NurseCode, ref string BillID)
        {

            return 0;
        }

        #endregion

        #region ����
        /// <summary>
        /// ִ�е�����
        /// ������д��objBill.Name ִ�е���,objBill.Memoִ�е����ͣ�1ҩ/2��ҩ,objBill.user01 ҽ������
        /// objBill.user02��ҩϵͳ���ҩƷ��� objBill.user03 ҩƷ�÷���
        /// </summary>
        /// <param name="al"></param>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        public int SetExecBill(ArrayList al, FS.FrameWork.Models.NeuObject objExecBill, string nurseCode, ref string billID)
        {
            string strSql = "";
            string strBillNo = "";
            FS.HISFC.Models.Base.Spell obj = new FS.HISFC.Models.Base.Spell();
            if (al.Count == 0)
            {
                this.Err = FS.FrameWork.Management.Language.Msg("û��ά����ϸ��Ŀ!");
                return -1;
            }
            strBillNo = GetNewExecBillID();

            if (strBillNo == "-1" || strBillNo == "") return -1;
            if (this.Sql.GetCommonSql("Order.ExecBill.InsertItem", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, nurseCode, strBillNo, objExecBill.Name, this.Operator.ID, objExecBill.User02, objExecBill.User01, objExecBill.Memo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                this.WriteErr();
                return -1;
            }
            if (this.ExecNoQuery(strSql) < 0) 
                return -1;

            for (int i = 0; i < al.Count; i++)
            {
                obj = (FS.HISFC.Models.Base.Spell)al[i];
                obj.ID = strBillNo;
                if (obj.ID == "-1" || obj.ID == "")
                {
                    return -1;
                }
                if (InsertExecBill(obj, nurseCode) < 0)
                {
                    this.Err = FS.FrameWork.Management.Language.Msg("ִ�е�����ʧ�ܣ�") + this.Err;
                    return -1;
                }
            }
            billID = strBillNo;

            return 0;
        }

        /// <summary>
        /// ���ִ�е���ˮ��
        /// </summary>
        /// <returns></returns>
        public string GetNewExecBillID()
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Management.ExecBill.GetNewExecBillID", ref sql) == -1) return null;
            string strReturn = this.ExecSqlReturnOne(sql);
            if (strReturn == "-1" || strReturn == "") return null;
            return strReturn;
        }
        /// <summary>
        /// ��com_dirctionary����ȡ����Ŀ��Ϣ
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public ArrayList GetItemInfo(string itemType)
        {
            string strSql = "";
            ArrayList alItem = new ArrayList();
            if (this.Sql.GetCommonSql("Management.ExecBill.GetItemInfo", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            strSql = System.String.Format(strSql, itemType);
            if (this.ExecQuery(strSql) == -1) return null;

            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject obj = new NeuObject();
                obj.ID = this.Reader[0].ToString();//����
                obj.Name = this.Reader[1].ToString();//����
                obj.User01 = this.Reader[2].ToString();//��ע
                obj.User02 = this.Reader[3].ToString();//��Ч��־
                alItem.Add(obj);
            }
            this.Reader.Close();
            return alItem;
        }

        /// <summary>
        /// ����ִ�е�����
        /// </summary>
        /// <param name="billNo"></param>
        /// <param name="billName"></param>
        /// <param name="Memo"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public int UpdateExecBillName(string billNo, string billName, string Memo, string style)
        {
            string strSql = "";
            #region "�ӿ�"
            //���룺0 ִ�е���ˮ�� 1 ִ�е����� 3 ����Ա
            //������0
            #endregion

            /*
				update met_ipm_execbill   --ҽ��ִ�е���Ϣ��
				set bill_name= '{1}',     --ִ�е�����
				    oper_code= '{2}',     --����Ա����
                    mark = '{3}',         --ִ�е���ע
                    bill_kind = '{4}',    --ִ�е�����
				    oper_date=sysdate
				where BILL_NO='{0}'   
			 */


            if (this.Sql.GetCommonSql("Order.ExecBill.UpdateItem", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, billNo, billName, this.Operator.ID, Memo, style);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// �༭ִ�е� by huangchw 2012-09-05
        /// </summary>
        /// <param name="billNo"></param>
        /// <param name="billName"></param>
        /// <param name="Memo"></param>
        /// <param name="isProExeBill"></param>
        /// <returns></returns>
        public int UpdateExecBill(string billNo, string billName, string Memo, string isProExeBill)
        {
            string strSql = "";
            /*
				update met_ipm_execbill   --ҽ��ִ�е���Ϣ��
				set bill_name= '{1}',     --ִ�е�����
				    oper_code= '{2}',     --����Ա����
                    mark = '{3}',         --ִ�е���ע
                    item_flag = '{4}',    --ִ�е�����
				    oper_date=sysdate
				where BILL_NO='{0}'   
			 */

            if (this.Sql.GetCommonSql("Order.ExecBill.UpdateItem.Edit", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, billNo, billName, this.Operator.ID, Memo, isProExeBill);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }


        /// <summary>
        /// ɾ��ִ�е�����
        /// ������д��objBill.ID ִ�е���ˮ�ţ�objBill.Memoִ�е����ͣ�1ҩ/2��ҩ,objBill.user01 ҽ������,
        /// objBill.user02��ҩϵͳ���ҩƷ���,objBill.user03 ҩƷ�÷���
        /// </summary>
        /// <param name="objBill"></param>
        /// <returns></returns>
        public int DeleteExecBill(FS.FrameWork.Models.NeuObject objBill)
        {
            string strSql = "";
            #region "�ӿ�"
            //���룺0 ִ�е���ˮ�� 1ҽ������ 2��ҩϵͳ���ҩƷ��� 3ҩƷ�÷�
            //������0
            #endregion

            if (objBill.Memo == "1") //ҩƷ
            {
                if (this.Sql.GetCommonSql("Order.ExecBill.DeleteItem.1", ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return -1;
                }
            }
            else if (objBill.Memo == "2")//��ҩƷ
            {
                if (!string.IsNullOrEmpty(objBill.User03))
                {
                    if (this.Sql.GetCommonSql("Order.ExecBill.DeleteItem.2", ref strSql) == -1)
                    {
                        this.Err = this.Sql.Err;
                        return -1;
                    }
                }
                else
                {
                    if (this.Sql.GetCommonSql("Order.ExecBill.DeleteItem.3", ref strSql) == -1)
                    {
                        this.Err = this.Sql.Err;
                        return -1;
                    }

                }
            }

            try
            {
                strSql = string.Format(strSql, objBill.ID, objBill.User01, objBill.User02, objBill.User03);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);

        }

        /// <summary>
        /// ��ò���ִ�е�
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        public ArrayList QueryExecBill(string nurseCode)
        {
            ArrayList al = new ArrayList();
            string strSql = "";
            //Order.ExecBill.SelectItem.1
            //���룺����������
            //����:0 ִ�е���ˮ�ţ���ִ�е�����
            if (this.Sql.GetCommonSql("Order.ExecBill.SelectItem.1", ref strSql) == 0)
            {
                try
                {
                    strSql = string.Format(strSql, nurseCode);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.ErrCode = ex.Message;
                    return null;
                }
                if (this.ExecQuery(strSql) == -1) return null;
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new NeuObject();
                    obj.ID = this.Reader[0].ToString();
                    obj.Name = this.Reader[1].ToString();
                    obj.User01 = this.Reader[2].ToString();//ִ�е���ע
                    obj.User02 = this.Reader[3].ToString();//ִ�е�����

                    obj.Memo = this.Reader[4].ToString();//��ϸִ�е����
                    al.Add(obj);
                }
                this.Reader.Close();
            }
            else
            {
                return null;
            }
            return al;
        }

        /// <summary>
        /// ���ִ�е�������Ϣbyִ�е���ˮ��
        /// </summary>
        /// <param name="billNo"></param>
        /// <returns></returns>
        public ArrayList QueryExecBillDetail(string billNo)
        {
            ArrayList al = new ArrayList();
            string strSql = "";
            //Order.ExecBill.SelectItem.��
            //���룺0  ִ�е���ˮ��
            //����:0  ִ�е���ˮ��,1������ҩ����ҩ��
            //2ҽ������id 3 name��4��ҩϵͳ���ҩƷ���5ҩƷ�÷� id 6 name
            if (this.Sql.GetCommonSql("Order.ExecBill.SelectItem.2", ref strSql) == 0)
            {
                try
                {
                    strSql = string.Format(strSql, billNo);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.ErrCode = ex.Message;
                    this.WriteErr();
                    return null;
                }
                if (this.ExecQuery(strSql) == -1)
                {
                    return null;
                }

                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Order.Inpatient.Order obj = new FS.HISFC.Models.Order.Inpatient.Order();
                    obj.ID = this.Reader[0].ToString();
                    obj.Memo = this.Reader[1].ToString();//ҩƷ��ҩƷ
                    obj.OrderType.ID = this.Reader[2].ToString();
                    obj.OrderType.Name = this.Reader[3].ToString();
                    if (obj.Memo == "1")
                    {
                        obj.Item.User01 = this.Reader[4].ToString();
                    }
                    else
                    {
                        obj.Item.SysClass.ID = this.Reader[4].ToString();//ϵͳ���
                    }
                    obj.Usage.ID = this.Reader[5].ToString();
                    obj.Usage.Name = this.Reader[6].ToString();

                    obj.Item.ID = this.Reader[7].ToString();
                    obj.Item.Name = this.Reader[8].ToString();

                    al.Add(obj);
                }
                this.Reader.Close();
            }
            else
            {
                return null;
            }
            return al;
        }


        /// <summary>
        /// ɾ��ĳ������վ�µ�����ִ�е�����ϸ
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        public int DeleteAllExecBill(string nurseCode)
        {
            string strSql = "";

            #region ɾ��ϸ��
            if (this.Sql.GetCommonSql("Order.ExecBill.DeleteAllExecBill.Drug", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, nurseCode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                this.WriteErr();
                return -1;
            }
            if (this.ExecNoQuery(strSql) < 0) return -1;
            if (this.Sql.GetCommonSql("Order.ExecBill.DeleteAllExecBill.unDrug", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, nurseCode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            if (this.ExecNoQuery(strSql) < 0) return -1;
            #endregion

            #region ɾ������
            if (this.Sql.GetCommonSql("Order.ExecBill.DeleteAllExecBill", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, nurseCode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                this.WriteErr();
                return -1;
            }
            if (this.ExecNoQuery(strSql) < 0) return -1;
            #endregion

            return 0;
        }

        /// <summary>
        /// ���ִ�е�������Ϣbyִ�е���ˮ�š�����neuobject ʵ�������б�
        /// </summary>
        /// <param name="billNo"></param>
        /// <returns></returns>
        public ArrayList QueryExecBillDetailByBillNo(string billNo)
        {
            ArrayList al = new ArrayList();
            string strSql = "";
            //Order.ExecBill.SelectItem.��
            //���룺0  ִ�е���ˮ��
            //����:0  ִ�е���ˮ��,1������ҩ����ҩ��
            //2ҽ������id 3 name��4��ҩϵͳ���ҩƷ���5ҩƷ�÷� id 6 name
            if (this.Sql.GetCommonSql("Order.ExecBill.SelectItem.2", ref strSql) == 0)
            {
                try
                {
                    strSql = string.Format(strSql, billNo);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.ErrCode = ex.Message;
                    this.WriteErr();
                    return null;
                }
                if (this.ExecQuery(strSql) == -1)
                {
                    return null;
                }

                while (this.Reader.Read())
                {
                    //FS.HISFC.Models.Order.Inpatient.ExecBillDetail billDetail = new FS.HISFC.Models.Order.Inpatient.ExecBillDetail();
                    //billDetail.ID = this.Reader[0].ToString(); //ִ�е����
                    //billDetail.Memo = this.Reader[1].ToString();//ҩƷ��ҩƷ 0��ҩƷ��1 ҩƷ
                    //billDetail.OrderTypeID = this.Reader[2].ToString(); //ҽ��������
                    ////billDetail. = Reader[3].ToString();//ҽ���������
                    //billDetail.ClassCode = this.Reader[4].ToString();//ϵͳ���
                    //billDetail.UsageID = this.Reader[5].ToString(); //�÷�����
                    ////billDetail. = Reader[5].ToString();//�÷�����
                    //billDetail.Item.ID = Reader[6].ToString();//��Ŀ���루��ҩ����Ŀִ�е���
                    //billDetail.Item.Name = Reader[7].ToString();//��Ŀ���ƣ���ҩ����Ŀִ�е���
                    //al.Add(billDetail);

                    FS.HISFC.Models.Base.Spell obj = new FS.HISFC.Models.Base.Spell();
                    obj.ID = this.Reader[0].ToString(); //ִ�е����
                    obj.Memo = this.Reader[1].ToString();//ҩƷ��ҩƷ 0��ҩƷ��1 ҩƷ
                    obj.User01 = this.Reader[2].ToString(); //ҽ��������
                    //Reader[2].ToString();//ҽ���������
                    obj.User02 = this.Reader[4].ToString();//ϵͳ���
                    obj.User03 = this.Reader[5].ToString(); //�÷�����
                    //Reader[6].ToString();//�÷�����
                    obj.SpellCode = Reader[7].ToString();//��Ŀ���루��ҩ����Ŀִ�е���
                    obj.WBCode = Reader[8].ToString();//��Ŀ���ƣ���ҩ����Ŀִ�е���
                    al.Add(obj);
                }
                this.Reader.Close();
            }
            else
            {
                return null;
            }
            return al;
        }

        #endregion
    }
}
