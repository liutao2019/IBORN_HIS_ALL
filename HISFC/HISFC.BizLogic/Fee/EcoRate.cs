/*----------------------------------------------------------------
            // Copyright (C) ������������ɷ����޹�˾
            // ��Ȩ���С� 
            //
            // �ļ�����			EcoRate.cs
            // �ļ�����������	�Żݱ��ʷ�����
            //
            // 
            // ������ʶ��		2006-6-16
            //
            // �޸ı�ʶ��
            // �޸�������
            //
            // �޸ı�ʶ��
            // �޸�������
//----------------------------------------------------------------*/

using System;
using System.Collections;

namespace FS.HISFC.BizLogic.Fee
{
    /// <summary>
    /// �Żݱ��ʷ�����
    /// </summary>
    public class EcoRate : FS.FrameWork.Management.Database
    {
        public EcoRate()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }


        #region ��������
        /// <summary>
        /// ����ֵ
        /// </summary>
        int intReturn = 0;
        /// <summary>
        /// Select���
        /// </summary>
        string SELECT = "";
        /// <summary>
        /// Where���
        /// </summary>
        string WHERE = "";
        /// <summary>
        /// SQL���
        /// </summary>
        string SQL = "";
        /// <summary>
        /// �ֶ����ö��
        /// </summary>
        enum Field
        {
            /// <summary>
            /// ҽԺ��������
            /// </summary>
            ParentCode = 0,
            /// <summary>
            /// ҽԺ��������
            /// </summary>
            CurrentCode = 1,
            /// <summary>
            /// ����������
            /// </summary>
            EcoCode = 2,
            /// <summary>
            /// ��Ŀ���ͱ���
            /// </summary>
            TypeCode = 3,
            /// <summary>
            /// ��Ŀ����
            /// </summary>
            ItemCode = 4,
            /// <summary>
            /// ���ѱ���
            /// </summary>
            PubRate = 5,
            /// <summary>
            /// �Էѱ���
            /// </summary>
            OwnRate = 6,
            /// <summary>
            /// �Ը�����
            /// </summary>
            PayRate = 7,
            /// <summary>
            /// �Żݱ���
            /// </summary>
            EcoRate = 8,
            /// <summary>
            /// �������
            /// </summary>
            ArrRate = 9,
            /// <summary>
            /// ����Ա����
            /// </summary>
            OperatorCode = 10,
            /// <summary>
            /// ��������
            /// </summary>
            OperateDate = 11,
            /// <summary>
            /// ��Ŀ����
            /// </summary>
            ItemName = 12
        }
        /// <summary>
        /// ��������
        /// </summary>
        string[] parameters = new string[12];
        #endregion

        #region	�������
        /// <summary>
        /// �������
        /// </summary>
        private void InitParameters()
        {
            for (int i = 0; i < this.parameters.Length; i++)
            {
                this.parameters[i] = "";
            }
        }
        #endregion

        #region ת��Reader��Object
        /// <summary>
        /// ת��Reader��Object
        /// [����: FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate - �Żݱ�����]
        /// </summary>
        /// <param name="ecoRate">�Żݱ�����</param>
        private void ChangeReaderToObject(FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate)
        {
            // ��������
            ecoRate.Hospital.ID = this.Reader[(int)Field.ParentCode].ToString();
            // ��������
            ecoRate.Hospital.Name = this.Reader[(int)Field.CurrentCode].ToString();
            // ����������
            ecoRate.RateType.ID = this.Reader[(int)Field.EcoCode].ToString();
            // ��Ŀ������
            ecoRate.ItemType.ID = this.Reader[(int)Field.TypeCode].ToString();
            switch (ecoRate.ItemType.ID)
            {
                case "0":
                    ecoRate.ItemType.Name = "����";
                    break;
                case "1":
                    ecoRate.ItemType.Name = "��С����";
                    break;
                case "2":
                    ecoRate.ItemType.Name = "�շ���Ŀ";
                    break;
            }
            // ��Ŀ����
            ecoRate.Item.ID = this.Reader[(int)Field.ItemCode].ToString();
            // ��Ŀ����
            ecoRate.Item.Name = this.Reader[(int)Field.ItemName].ToString();
            // ���ѱ���
            try
            {
                ecoRate.Rate.PubRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[(int)Field.PubRate].ToString());
            }
            catch
            {
                ecoRate.Rate.PubRate = 1m;
            }
            // �Էѱ���
            try
            {
                ecoRate.Rate.OwnRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[(int)Field.OwnRate].ToString());
            }
            catch
            {
                ecoRate.Rate.OwnRate = 1m;
            }
            // �Ը�����
            try
            {
                ecoRate.Rate.PayRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[(int)Field.PayRate].ToString());
            }
            catch
            {
                ecoRate.Rate.PayRate = 1m;
            }
            // �Żݱ���
            try
            {
                ecoRate.Rate.RebateRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[(int)Field.EcoRate].ToString());
            }
            catch
            {
                ecoRate.Rate.RebateRate = 1m;
            }
            // �������
            try
            {
                ecoRate.Rate.DerateRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[(int)Field.ArrRate].ToString());
            }
            catch
            {
                ecoRate.Rate.DerateRate = 1m;
            }
            // ����Ա
            ecoRate.CurrentOperator.ID = this.Reader[(int)Field.OperatorCode].ToString();
            // ����ʱ��
            try
            {
                ecoRate.OperateDateTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[(int)Field.OperateDate].ToString());
            }
            catch { };
        }
        #endregion
        #region ת��Object��Parameters����
        /// <summary>
        /// ת��Object��Parameters����
        /// [����: FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate - �Żݱ�����]
        /// </summary>
        /// <param name="ecoRate">�Żݱ�����</param>
        private void ChangeObjectToParameters(FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate)
        {
            // �������
            this.InitParameters();
            // ��������
            this.parameters[(int)Field.ParentCode] = ecoRate.Hospital.ID;
            // ��������
            this.parameters[(int)Field.CurrentCode] = ecoRate.Hospital.Name;
            // ����������
            this.parameters[(int)Field.EcoCode] = ecoRate.RateType.ID;
            // ��Ŀ������
            this.parameters[(int)Field.TypeCode] = ecoRate.ItemType.ID;
            // ��Ŀ����
            this.parameters[(int)Field.ItemCode] = ecoRate.Item.ID;
            // ���ѱ���
            this.parameters[(int)Field.PubRate] = ecoRate.Rate.PubRate.ToString();
            // �Էѱ���
            this.parameters[(int)Field.OwnRate] = ecoRate.Rate.OwnRate.ToString();
            // �Ը�����
            this.parameters[(int)Field.PayRate] = ecoRate.Rate.PayRate.ToString();
            // �Żݱ���
            this.parameters[(int)Field.EcoRate] = ecoRate.Rate.RebateRate.ToString();
            // �������
            this.parameters[(int)Field.ArrRate] = ecoRate.Rate.DerateRate.ToString();
            // ����Ա����
            this.parameters[(int)Field.OperatorCode] = ecoRate.CurrentOperator.ID;
            // ����ʱ��
            this.parameters[(int)Field.OperateDate] = ecoRate.OperateDateTime.ToString();
        }
        #endregion
        #region ת��Reader��ArrayList
        /// <summary>
        /// ת��Reader��ArrayList
        /// [����: ArrayList alEcoRate - ��������]
        /// </summary>
        /// <param name="alEcoRate">��������</param>
        private void ChangeReaderToList(ArrayList alEcoRate)
        {
            // ѭ���ƽ��α�
            while (this.Reader.Read())
            {
                // ������
                FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate = new Models.Fee.Outpatient.EcoRate();

                // ת��Reader�ɶ���
                this.ChangeReaderToObject(ecoRate);

                // ��ӽ�����
                alEcoRate.Add(ecoRate);
            }
        }
        #endregion

        #region ��֤����Ƿ�Ϸ�
        /// <summary>
        /// ��֤����Ƿ�Ϸ�
        /// [����1: FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate - ���صĸ��ֱ���]
        /// [����2: bool boolForce - �Ƿ�ǿ��ʹ����Ŀ���Լ��,true - ʹ����Ŀ���Լ��,false - ��ʹ����Ŀ���Լ��]
        /// [����: bool,true-�д��󲻺Ϸ�,false-û�д���]
        /// </summary>
        /// <param name="ecoRate">���صĸ��ֱ���</param>
        /// <param name="boolForce">�Ƿ�ǿ��ʹ����Ŀ���Լ��,true - ʹ����Ŀ���Լ��,false - ��ʹ����Ŀ���Լ��</param>
        /// <returns>true-�д��󲻺Ϸ�,false-û�д���</returns>
        private bool ValidParameter(FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate, bool boolForce)
        {
            //
            // �жϱ����������Ƿ�Ϸ�
            //
            if (ecoRate.RateType.ID.Equals("") || ecoRate.RateType.ID == null)
            {
                this.Err = "����������ecoRate.EcoCode������Ϊ��";
                return true;
            }
            //
            // �ж���Ŀ�����Ƿ�Ϸ�
            //
            if (ecoRate.Item.ID.Equals("") || ecoRate.Item.ID == null)
            {
                this.Err = "��Ŀ����ecoRate.ItemCode������Ϊ��";
                return true;
            }
            //
            // �������Ŀ���Լ��,��ô��Ŀ�������Ϊ��
            //
            if (boolForce)
            {
                if (ecoRate.ItemType.Equals("") || ecoRate.ItemType == null)
                {
                    this.Err = "��Ŀ���ecoRate.ItemType������Ϊ��";
                    return true;
                }
            }
            //
            // �Ϸ�
            //
            return false;
        }
        #endregion

        #region �����Żݱ������������Ŀ�����ȡ���ֱ���
        /// <summary>
        /// �����Żݱ������������Ŀ�����ȡ���ֱ��ʡ�
        /// (1)ʹ����Ŀ���Լ��:�����ȡ��Ŀ������,��ôֱ�ӷ��ء�
        /// (2)��ʹ����Ŀ���Լ��:��������ecoRate.ItemType,
        /// �������Ȱ���Ŀ�����ȡ,
        /// ���������,����С���ñ����ȡ,
        /// ����ٲ����ڣ�����������ȡ�� 
        /// [����: FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate - ���صĸ��ֱ���]
        /// [����: bool boolForce - �Ƿ�ǿ��ʹ����Ŀ���Լ��,true - ʹ����Ŀ���Լ��,false - ��ʹ����Ŀ���Լ��]
        /// [���: ecoRate.EcoCode - ����������]
        /// [���: ecoRate.ItemCode - ��Ŀ���롢��С���ñ����������]
        /// [���: ecoRate.ItemType]
        /// [���� : int,1-�ɹ�,-1-ʧ��]
        /// </summary>
        /// <param name="ecoRate">���صĸ��ֱ���</param>
        /// <returns>1-�ɹ�,-1-ʧ��</returns>
        public int GetRate(FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate, bool boolForce)
        {
            //
            // ��֤�Ϸ���
            //
            if (this.ValidParameter(ecoRate, boolForce))
            {
                return -1;
            }
            //
            // ���ǿ�����Լ��
            //
            if (boolForce)
            {
                switch (ecoRate.ItemType.ID)
                {
                    case "0":
                        // ����������ȡ
                        this.intReturn = this.GetRateByClass(ecoRate);
                        break;
                    case "1":
                        // ����С���ñ����ȡ
                        this.intReturn = this.GetRateByMinFee(ecoRate);
                        break;
                    case "2":
                        // ����Ŀ�����ȡ
                        this.intReturn = this.GetRateByItem(ecoRate);
                        break;
                }
                if (this.intReturn == -1)
                {
                    this.Err = "��ȡ��Ŀ����ʧ��!" + "\n" + this.Err;
                    return -1;
                }
                //
                // �����Ŀ������,��ô������Ŀ����Ϊ100%
                //
                if (this.intReturn == 0)
                {
                    this.FullRate(ecoRate);
                }
                //
                // �ɹ�����1
                //
                return 1;
            }
            //
            // ���û�����Լ��,������Ŀ�����ȡ��Ŀ����
            //
            this.intReturn = this.GetRateByItem(ecoRate);
            if (this.intReturn == -1)
            {
                this.Err = "��ȡ��Ŀ����ʧ��!" + "\n" + this.Err;
                return -1;
            }
            else
                //
                // �����ȡ��Ŀ������,��ô�����ϼ���С���ô����ȡ����
                //
                if (this.intReturn == 0)
                {
                    this.intReturn = this.GetRateByMinFee(ecoRate);
                    if (this.intReturn == -1)
                    {
                        this.Err = "��ȡ��Ŀ����ʧ��!" + "\n" + this.Err;
                    }
                }
            //
            // �����ȡ��Ŀ������,��ô���ݴ�������ȡ����
            //
            if (this.intReturn == 0)
            {
                this.intReturn = this.GetRateByClass(ecoRate);
                if (this.intReturn == -1)
                {
                    this.Err = "��ȡ��Ŀ����ʧ��!" + "\n" + this.Err;
                }
            }
            //
            // �����ȡ��Ŀ������,��ô���ø��ַ��ñ���Ϊ100%
            //
            if (this.intReturn == 0)
            {
                this.FullRate(ecoRate);
            }
            //
            // �ɹ�����1
            //
            return 1;
        }
        #endregion
        #region �����Żݱ������������Ŀ����(�Ǵ������ͷ���С���ñ���)��ȡ���ֱ���
        /// <summary>
        /// �����Żݱ������������Ŀ����(�Ǵ������ͷ���С���ñ���)��ȡ���ֱ���
        /// [����: FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate - ���صĸ��ֱ���]
        /// [���� : int,1-�ɹ�,0-������,-1-ʧ��]
        /// </summary>
        /// <param name="ecoRate">���صĸ��ֱ���</param>
        /// <returns>1-�ɹ�,0-������,-1-ʧ��</returns>
        public int GetRateByItem(FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate)
        {
            //
            // ��ȡSQL���
            //
            this.intReturn = this.Sql.GetCommonSql("FS.HISFC.BizLogic.Fee.EcoRate.GetRate.Select", ref this.SELECT);
            if (this.intReturn == -1)
            {
                this.Err = "��ȡSQL���ʧ��!";
                return -1;
            }
            this.intReturn = this.Sql.GetCommonSql("FS.HISFC.BizLogic.Fee.EcoRate.GetRateByItem.Where", ref this.WHERE);
            if (this.intReturn == -1)
            {
                this.Err = "��ȡSQL���ʧ��!";
                return -1;
            }
            this.SQL = this.SELECT + " " + this.WHERE;
            //
            // ��ʽ��SQL���
            //
            try
            {
                this.SQL = string.Format(this.SQL, ecoRate.RateType.ID, ecoRate.Item.ID);
            }
            catch (Exception e)
            {
                this.Err = "��ʽ��SQL���ʧ��!" + "\n" + e.Message;
                return -1;
            }
            //
            // ִ��SQL���
            //
            this.intReturn = this.ExecQuery(this.SQL);
            if (this.intReturn == -1)
            {
                this.Err = "ִ��SQL���ʧ��!" + "\n" + this.Err;
                return -1;
            }
            //
            // ��ȡ���
            //
            if (this.Reader.Read())
            {
                this.ChangeReaderToObject(ecoRate);
                this.Reader.Close();
            }
            else
            {
                this.Reader.Close();
                return 0;
            }
            //
            // �ɹ�����1
            //
            return 1;
        }
        #endregion
        #region �����Żݱ������������С���ñ���(�Ǵ������ͷ���Ŀ����)��ȡ���ֱ���
        /// <summary>
        /// �����Żݱ������������С���ñ���(�Ǵ������ͷ���Ŀ����)��ȡ���ֱ���
        /// [����: FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate - ���صĸ��ֱ���]
        /// [���� : int,1-�ɹ�,0-������,-1-ʧ��]
        /// </summary>
        /// <param name="ecoRate">���صĸ��ֱ���</param>
        /// <returns>1-�ɹ�,0-������,-1-ʧ��</returns>
        public int GetRateByMinFee(FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate)
        {
            //
            // ��ȡSQL���
            //
            this.intReturn = this.Sql.GetCommonSql("FS.HISFC.BizLogic.Fee.EcoRate.GetRate.Select", ref this.SELECT);
            if (this.intReturn == -1)
            {
                this.Err = "��ȡSQL���ʧ��!";
                return -1;
            }
            this.intReturn = this.Sql.GetCommonSql("FS.HISFC.BizLogic.Fee.EcoRate.GetRateByMinFee.Where", ref this.WHERE);
            if (this.intReturn == -1)
            {
                this.Err = "��ȡSQL���ʧ��!";
                return -1;
            }
            this.SQL = this.SELECT + " " + this.WHERE;
            //
            // ��ʽ��SQL���
            //
            try
            {
                this.SQL = string.Format(this.SQL, ecoRate.RateType.ID, ecoRate.Item.ID);
            }
            catch (Exception e)
            {
                this.Err = "��ʽ��SQL���ʧ��!" + "\n" + e.Message;
                return -1;
            }
            //
            // ִ��SQL���
            //
            this.intReturn = this.ExecQuery(this.SQL);
            if (this.intReturn == -1)
            {
                this.Err = "ִ��SQL���ʧ��!" + "\n" + this.Err;
                return -1;
            }
            //
            // ��ȡ���
            //
            if (this.Reader.Read())
            {
                this.ChangeReaderToObject(ecoRate);
                this.Reader.Close();
            }
            else
            {
                this.Reader.Close();
                return 0;
            }
            //
            // �ɹ�����1
            //
            return 1;
        }
        #endregion
        #region �����Żݱ���������ʹ������(����С���ñ���ͷ���Ŀ����)��ȡ���ֱ���
        /// <summary>
        /// �����Żݱ���������ʹ������(����С���ñ���ͷ���Ŀ����)��ȡ���ֱ���
        /// [����: FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate - ���صĸ��ֱ���]
        /// [���� : int,1-�ɹ�,0-������,-1-ʧ��]
        /// </summary>
        /// <param name="ecoRate">���صĸ��ֱ���</param>
        /// <returns>1-�ɹ�,0-������,-1-ʧ��</returns>
        public int GetRateByClass(FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate)
        {
            //
            // ��ȡSQL���
            //
            this.intReturn = this.Sql.GetCommonSql("FS.HISFC.BizLogic.Fee.EcoRate.GetRate.Select", ref this.SELECT);
            if (this.intReturn == -1)
            {
                this.Err = "��ȡSQL���ʧ��!";
                return -1;
            }
            this.intReturn = this.Sql.GetCommonSql("FS.HISFC.BizLogic.Fee.EcoRate.GetRateByClass.Where", ref this.WHERE);
            if (this.intReturn == -1)
            {
                this.Err = "��ȡSQL���ʧ��!";
                return -1;
            }
            this.SQL = this.SELECT + " " + this.WHERE;
            //
            // ��ʽ��SQL���
            //
            try
            {
                this.SQL = string.Format(this.SQL, ecoRate.RateType.ID, ecoRate.Item.ID);
            }
            catch (Exception e)
            {
                this.Err = "��ʽ��SQL���ʧ��!" + "\n" + e.Message;
                return -1;
            }
            //
            // ִ��SQL���
            //
            this.intReturn = this.ExecQuery(this.SQL);
            if (this.intReturn == -1)
            {
                this.Err = "ִ��SQL���ʧ��!" + "\n" + this.Err;
                return -1;
            }
            //
            // ��ȡ���
            //
            if (this.Reader.Read())
            {
                this.ChangeReaderToObject(ecoRate);
                this.Reader.Close();
            }
            else
            {
                this.Reader.Close();
                return 0;
            }
            //
            // �ɹ�����1
            //
            return 1;
        }
        #endregion

        #region ���ø��ֱ���Ϊ100%
        /// <summary>
        /// ���ø��ֱ���Ϊ100%
        /// [����: FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate - ���صĸ��ֱ���]
        /// </summary>
        /// <param name="ecoRate">���صĸ��ֱ���</param>
        private void FullRate(FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate)
        {
            ecoRate.Rate.ArrearageRate = 1m;
            ecoRate.Rate.DerateRate = 1m;
            ecoRate.Rate.OwnRate = 1m;
            ecoRate.Rate.PayRate = 1m;
            ecoRate.Rate.PubRate = 1m;
            ecoRate.Rate.RebateRate = 1m;
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// [����: FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate - �Żݱ�����]
        /// [����: int,1-�ɹ�,-1-ʧ��]
        /// </summary>
        /// <param name="ecoRate">�Żݱ�����</param>
        /// <returns>1-�ɹ�,-1-ʧ��</returns>
        public int CreateEcoRate(FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate)
        {
            //
            // ��ȡSQL���
            //
            this.intReturn = this.Sql.GetCommonSql("FS.HISFC.BizLogic.Fee.EcoRate.CreateEcoRate", ref this.SQL);
            if (this.intReturn == -1)
            {
                this.Err = "��ȡSQL���FS.HISFC.BizLogic.Fee.EcoRate.CreateEcoRateʧ��!";
                return -1;
            }
            //
            // ת������Ϊ�ַ�������
            //
            this.ChangeObjectToParameters(ecoRate);
            //
            // ��ʽ��SQL���
            //
            try
            {
                this.SQL = string.Format(this.SQL,
                                        ecoRate.RateType.ID,			// ����������
                                        ecoRate.ItemType.ID,			// ��Ŀ���
                                        ecoRate.Item.ID,			// ��Ŀ����
                                        ecoRate.Rate.PubRate,		// ���ѱ���
                                        ecoRate.Rate.OwnRate,		// �Էѱ���
                                        ecoRate.Rate.PayRate,		// �Ը�����
                                        ecoRate.Rate.RebateRate,	// �Żݱ���
                                        ecoRate.Rate.DerateRate,	// �������
                                        ecoRate.CurrentOperator.ID,	// ����Ա����
                                        ecoRate.OperateDateTime		// ����ʱ��
                                        );
            }
            catch (Exception e)
            {
                this.Err = "��ʽ��SQL���ʧ��!" + "\n" + e.Message;
                return -1;
            }
            //
            // ִ��SQL���
            //
            this.intReturn = this.ExecNoQuery(this.SQL);
            if (this.intReturn < 1)
            {
                this.Err = "����������Ŀʧ��!" + "\n" + this.Err;
                return -1;
            }
            //
            // �ɹ�����1
            //
            return 1;
        }
        #endregion
        #region ɾ������
        /// <summary>
        /// ɾ������
        /// [����: FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate - �Żݱ�����]
        /// [����: int,Ӱ�������]
        /// </summary>
        /// <param name="ecoRate">�Żݱ�����</param>
        /// <returns>Ӱ�������</returns>
        public int DeleteEcoRate(FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate)
        {
            //
            // ��ȡSQL���
            //
            this.intReturn = this.Sql.GetCommonSql("FS.HISFC.BizLogic.Fee.EcoRate.DeleteEcoRate.Delete", ref this.SELECT);
            if (this.intReturn == -1)
            {
                this.Err = "��ȡSQL���FS.HISFC.BizLogic.Fee.EcoRate.DeleteEcoRate.Deleteʧ��!";
            }
            this.intReturn = this.Sql.GetCommonSql("FS.HISFC.BizLogic.Fee.EcoRate.DeleteEcoRate.Where", ref this.WHERE);
            if (this.intReturn == -1)
            {
                this.Err = "��ȡSQL���FS.HISFC.BizLogic.Fee.EcoRate.DeleteEcoRate.Whereʧ��!";
            }
            this.SQL = this.SELECT + " " + this.WHERE;
            //
            // ת������Ϊ�ַ�������
            //
            this.ChangeObjectToParameters(ecoRate);
            //
            // ��ʽ��SQL���
            //
            try
            {
                this.SQL = string.Format(this.SQL,
                                        this.parameters[(int)Field.EcoCode],		// �������
                                        this.parameters[(int)Field.TypeCode],		// ��Ŀ���
                                        this.parameters[(int)Field.ItemCode]		// ��Ŀ����
                                        );
            }
            catch (Exception e)
            {
                this.Err = "��ʽ��SQL���ʧ��!" + "\n" + e.Message;
                return -1;
            }
            //
            // ִ��SQL���
            //
            return this.ExecNoQuery(this.SQL);
        }
        #endregion
        #region ���±���
        /// <summary>
        /// ���±���
        /// [����: FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate - �Żݱ�����]
        /// [����: int,-1-ʧ��,����-Ӱ�������]
        /// </summary>
        /// <param name="ecoRate">�Żݱ�����</param>
        /// <returns>int,-1-ʧ��,����-Ӱ�������</returns>
        public int UpdateEcoRate(FS.HISFC.Models.Fee.Outpatient.EcoRate ecoRate)
        {
            //
            // ��ȡSQL���
            //
            this.intReturn = this.Sql.GetCommonSql("FS.HISFC.BizLogic.Fee.EcoRate.UpdateEcoRate.Update", ref this.SELECT);
            if (this.intReturn == -1)
            {
                this.Err = "��ȡSQL���FS.HISFC.BizLogic.Fee.EcoRate.UpdateEcoRate.Updateʧ��!";
                return -1;
            }
            this.intReturn = this.Sql.GetCommonSql("FS.HISFC.BizLogic.Fee.EcoRate.DeleteEcoRate.Where", ref this.WHERE);
            if (this.intReturn == -1)
            {
                this.Err = "��ȡSQL���FS.HISFC.BizLogic.Fee.EcoRate.DeleteEcoRate.Whereʧ��!";
                return -1;
            }
            this.SQL = this.SELECT + " " + this.WHERE;
            //
            // ת������Ϊ�ַ�������
            //
            this.ChangeObjectToParameters(ecoRate);
            //
            // ��ʽ��SQL���
            //
            try
            {
                this.SQL = string.Format(this.SQL,
                                        ecoRate.RateType.ID,			// ����������
                                        ecoRate.ItemType.ID,			// ��Ŀ���
                                        ecoRate.Item.ID,			// ��Ŀ����
                                        ecoRate.Rate.PubRate,		// ���ѱ���
                                        ecoRate.Rate.OwnRate,		// �Էѱ���
                                        ecoRate.Rate.PayRate,		// �Ը�����
                                        ecoRate.Rate.RebateRate,	// �Żݱ���
                                        ecoRate.Rate.DerateRate,	// �������
                                        ecoRate.CurrentOperator.ID,	// ����Ա����
                                        ecoRate.OperateDateTime		// ����ʱ��
                                        );
            }
            catch (Exception e)
            {
                this.Err = "��ʽ��SQL���ʧ��!" + "\n" + e.Message;
                return -1;
            }
            //
            // ִ��SQL���
            //
            this.intReturn = this.ExecNoQuery(this.SQL);
            if (this.intReturn == -1)
            {
                this.Err = "����ִ��SQL���ʧ��!" + "\n" + this.Err;
                return -1;
            }
            //
            // ����Ӱ�������
            //
            return this.intReturn;
        }
        #endregion

        #region ���ݱ������������Ŀ���,��ȡ��Ӧ����ϸ
        /// <summary>
        /// ���ݱ������������Ŀ���,��ȡ��Ӧ����ϸ
        /// [����1: string ecoCode - ����������]
        /// [����2: string typeCode - ��Ŀ������]
        /// [����3: ArrayList alEcoRate - ���ص����д�����ϸ]
        /// [����: int,1-�ɹ�,-1-ʧ��]
        /// </summary>
        /// <param name="ecoCode">����������</param>
        /// <param name="typeCode">��Ŀ������</param>
        /// <param name="alEcoRate">���ص����д�����ϸ</param>
        /// <returns>1-�ɹ�,-1-ʧ��</returns>
        public int GetAllEcoRate(string ecoCode, string typeCode, ArrayList alEcoRate)
        {
            //
            // ��ȡSQL���
            //
            this.intReturn = this.Sql.GetCommonSql("FS.HISFC.BizLogic.Fee.EcoRate.GetRate.Select", ref this.SELECT);
            if (this.intReturn == -1)
            {
                this.Err = "��ȡSQL���ʧ��!";
                return -1;
            }
            //
            // �����ȡȫ����Ŀ
            //
            if (typeCode.Equals("999"))
            {
                this.intReturn = this.Sql.GetCommonSql("FS.HISFC.BizLogic.Fee.EcoRate.GetAll.Where1", ref this.WHERE);
            }
            else
            {
                this.intReturn = this.Sql.GetCommonSql("FS.HISFC.BizLogic.Fee.EcoRate.GetAll.Where", ref this.WHERE);
            }
            if (this.intReturn == -1)
            {
                this.Err = "��ȡSQL���ʧ��!";
                return -1;
            }
            this.SQL = this.SELECT + " " + this.WHERE;
            //
            // ��ʽ��SQL���
            //
            try
            {
                if (typeCode.Equals("999"))
                {
                    // ��ȡȫ����ϸ
                    this.SQL = string.Format(this.SQL, ecoCode);
                }
                else
                {
                    this.SQL = string.Format(this.SQL, ecoCode, typeCode);
                }
            }
            catch (Exception e)
            {
                this.Err = "��ʽ��SQL���ʧ��!" + "\n" + e.Message;
                return -1;
            }
            //
            // ִ��SQL���
            //
            this.intReturn = this.ExecQuery(this.SQL);
            if (this.intReturn == -1)
            {
                this.Err = "ִ��SQL���ʧ��!" + "\n" + this.Err;
                return -1;
            }
            //
            // ת��Reader������
            //
            this.ChangeReaderToList(alEcoRate);
            //
            // �ɹ�����1
            //
            return 1;
        }
        #endregion
        #region ���ݱ��ʱ����ȡ���д�����ϸ
        /// <summary>
        /// ���ݱ��ʱ����ȡ���д�����ϸ
        /// [����1: string ecoCode - ����������]
        /// [����2: ArrayList alEcoRate - ���ص���ϸ����]
        /// [����: int,1-�ɹ�,-1-ʧ��]
        /// </summary>
        /// <param name="ecoCode">����������</param>
        /// <param name="alEcoRate">��ϸ����</param>
        /// <returns>1-�ɹ�,-1-ʧ��</returns>
        public int GetAllClassEcoRate(string ecoCode, ArrayList alEcoRate)
        {
            return this.GetAllEcoRate(ecoCode, "0", alEcoRate);
        }
        #endregion
        #region ���ݱ��ʱ����ȡ������С������ϸ
        /// <summary>
        /// ���ݱ��ʱ����ȡ������С������ϸ
        /// [����1: string ecoCode - ����������]
        /// [����2: ArrayList alEcoRate - ���ص���ϸ����]
        /// [����: int,1-�ɹ�,-1-ʧ��]
        /// </summary>
        /// <param name="ecoCode">����������</param>
        /// <param name="alEcoRate">��ϸ����</param>
        /// <returns>1-�ɹ�,-1-ʧ��</returns>
        public int GetAllMinFeeEcoRate(string ecoCode, ArrayList alEcoRate)
        {
            return this.GetAllEcoRate(ecoCode, "1", alEcoRate);
        }
        #endregion
        #region ���ݱ��ʱ����ȡ������Ŀ��ϸ
        /// <summary>
        /// ���ݱ��ʱ����ȡ������Ŀ��ϸ
        /// [����1: string ecoCode - ����������]
        /// [����2: ArrayList alEcoRate - ���ص���ϸ����]
        /// [����: int,1-�ɹ�,-1-ʧ��]
        /// </summary>
        /// <param name="ecoCode">����������</param>
        /// <param name="alEcoRate">��ϸ����</param>
        /// <returns>1-�ɹ�,-1-ʧ��</returns>
        public int GetAllItemEcoRate(string ecoCode, ArrayList alEcoRate)
        {
            return this.GetAllEcoRate(ecoCode, "2", alEcoRate);
        }
        #endregion
        #region ��ȡ������ϸ
        /// <summary>
        /// ��ȡ������ϸ
        /// [����: ArrayList alEcoRate - ���ʶ�������]
        /// [����: int,1-�ɹ�,-1-ʧ��]
        /// </summary>
        /// <param name="alEcoRate">���ʶ�������</param>
        /// <returns></returns>
        public int GetAll(string ecoCode, ArrayList alEcoRate)
        {
            return this.GetAllEcoRate(ecoCode, "999", alEcoRate);
        }
        #endregion

        #region ��ȡ���Կ�������Ŀ
        /// <summary>
        /// ��ȡ���Կ�������Ŀ
        /// [����: System.Data.DataSet dataSet - ���ص���Ŀ���ݼ�]
        /// [����: int,1-�ɹ�,-1-ʧ��]
        /// </summary>
        /// <param name="dataSet">���ص���Ŀ���ݼ�</param>
        /// <returns>1-�ɹ�,-1-ʧ��</returns>
        public int GetPermitItems(System.Data.DataSet dataSet)
        {
            //
            // ��ȡSQL���
            //
            this.intReturn = this.Sql.GetCommonSql("FS.HISFC.BizLogic.Fee.GetPermitItems", ref this.SELECT);
            if (this.intReturn == -1)
            {
                this.Err = "��ȡSQL���FS.HISFC.BizLogic.Fee.GetPermitItemsʧ��!";
                return -1;
            }
            //
            // ִ��SQL���
            //
            this.intReturn = this.ExecQuery(this.SELECT, ref dataSet);
            if (this.intReturn == -1)
            {
                this.Err = "ִ��SQL���ʧ��!" + "\n" + this.Err;
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// ��ȡ���Կ�������Ŀ
        /// [����: System.Data.DataSet dataSet - ���ص���Ŀ���ݼ�]
        /// [����: int,1-�ɹ�,-1-ʧ��]
        /// </summary>
        /// <param name="dataSet">���ص���Ŀ���ݼ�</param>
        /// <returns>1-�ɹ�,-1-ʧ��</returns>
        public int GetPermitItemsWorkload(System.Data.DataSet dataSet)
        {
            //
            // ��ȡSQL���
            //
            this.intReturn = this.Sql.GetCommonSql("FS.HISFC.BizLogic.Fee.GetPermitItems.Workload", ref this.SELECT);
            if (this.intReturn == -1)
            {
                this.Err = "��ȡSQL���FS.HISFC.BizLogic.Fee.GetPermitItemsʧ��!";
                return -1;
            }
            //
            // ִ��SQL���
            //
            this.intReturn = this.ExecQuery(this.SELECT, ref dataSet);
            if (this.intReturn == -1)
            {
                this.Err = "ִ��SQL���ʧ��!" + "\n" + this.Err;
                return -1;
            }
            return 1;
        }
        #endregion
    }
}
