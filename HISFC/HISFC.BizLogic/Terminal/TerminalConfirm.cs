using System;
using System.Collections.Generic;
using System.ComponentModel; 
using System.Data;
using System.Text; 
using System.Collections;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Terminal;
using FS.HISFC.Models.Registration;

namespace FS.HISFC.BizLogic.Terminal
{
    /// <summary>
    /// TerminalConfirm <br></br>
    /// [��������: ҽ���ն�ȷ��ҵ�����]<br></br>
    /// [�� �� ��: ��һ��]<br></br>
    /// [����ʱ��: 2007-3-1]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class TerminalConfirm : FS.FrameWork.Management.Database
    {
        #region ��ͨ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public TerminalConfirm()
        {
            // ��ʼ���ֲ�����
            this.intReturn = 0;
            this.SQL = "";
        }
        #endregion
        #region ���캯����������������
        /// <summary>
        /// ���캯����������������
        /// </summary>
        /// <param name="transaction">�������</param>
        public TerminalConfirm(FS.FrameWork.Management.Transaction transaction)
        {
            // ��ʼ���ֲ�����
            this.intReturn = 0;
            this.SQL = "";
            // �����������
            this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
        }
        #endregion

        #region ����

        /// <summary>
        /// �洢���ֵ��õķ���ֵ
        /// </summary>
        private int intReturn;

        /// <summary>
        /// ִ�в�����ʹ�õ�SQL���
        /// </summary>
        private string SQL;

        /// <summary>
        /// SQL����WHERE����
        /// </summary>
        private string WHERE;

        /// <summary>
        /// SQL����ORDER���
        /// </summary>
        private string ORDER;

        /// <summary>
        /// ���Ƶ�����
        /// </summary>
        string stringLimitedDate = "";

        /// <summary>
        /// �Ƿ���ʱ������
        /// </summary>
        
        bool boolLimited = false;

        /// <summary>
        /// ��������
        /// </summary>
        string[] parms = new string[39];

        /// <summary>
        /// ҵ���ӱ��������
        /// </summary>
        string[] detailParms = new string[21];

        #endregion

        #region ˽�к���

        #region ���ô�����Ϣ
        /// <summary>
        /// ���ô�����Ϣ
        /// </summary>
        /// <param name="errorCode">������뷢������</param>
        /// <param name="errorText">������Ϣ</param>
        private void SetError(string errorCode, string errorText)
        {
            this.ErrCode = errorCode;
            this.Err = errorText + "[" + this.Err + "]"; // + "��TerminalConfirm.cs�ĵ�" + argErrorCode + "�д���";
            this.WriteErr();
        }
        #endregion
        #region ��ʼ������
        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void Clear()
        {
            this.intReturn = 0;
            this.SQL = "";
            this.WHERE = "";
            this.stringLimitedDate = "";
            this.boolLimited = false;
            this.ORDER = "";
        }
        #endregion

        #region ��ȡ��SQL���
        /// <summary>
        /// ��ȡ��SQL
        /// </summary>
        /// <returns></returns>
        private string GetTerminalSql()
        {
            // SQL���
            string strSql = "";
            // ��ȡSQL���
            if (this.Sql.GetSql("Terminal.TerminalValidate.GetApplyListByCardNO", ref strSql) == -1)
            {
                this.Err = "��ȡSQL���Terminal.TerminalValidate.GetApplyListByCardNO ʧ��";
                return null;
            }
            return strSql;
        }
        #endregion

        #region  ˽�� ��ȡ�ն�ȷ����Ϣ
        private ArrayList GetTerminalList(string strSql)
        {
            ArrayList applyList = new ArrayList();
            //ִ�в�ѯ���
            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "��ȡ�ն�ȷ����Ϣʱ��ִ��SQL������" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                // ������ض���
                FS.HISFC.Models.Terminal.TerminalApply terminalApply;

                while (this.Reader.Read()) // ѭ����ȡ�̳е�Reader����
                {
                    #region ��ȡ����
                    terminalApply = new TerminalApply();
                    // ���뵥��ˮ��[3]
                    terminalApply.ID = this.Reader[0].ToString();

                    // סԺ��ˮ�Ż������[04]
                    terminalApply.Patient.PID.ID = this.Reader[1].ToString();
                    terminalApply.Patient.ID = terminalApply.Patient.PID.ID;
                    // ����[05]
                    terminalApply.Patient.Name = this.Reader[2].ToString();
                    // ��ͬ��λ[06]
                    terminalApply.Patient.Pact.ID = this.Reader[3].ToString();
                    // ���벿�ű��루���һ��߲�����[07]
                    terminalApply.Item.Order.DoctorDept.ID = this.Reader[4].ToString();
                    // �ն˿��ұ���[08]
                    terminalApply.Item.ExecOper.Dept.ID = this.Reader[5].ToString();
                    // �����ǹҺſ��ҡ�סԺ����Ժ����[09]
                    terminalApply.Patient.DoctorInfo.Templet.Dept.ID = this.Reader[6].ToString();
                    // ��ҩ���ű���[10]
                    terminalApply.Item.Order.DoctorDept.ID = this.Reader[7].ToString();

                    // ���¿�����ˮ��(����)[11]
                    terminalApply.UpdateStoreSequence = Convert.ToInt32(this.Reader[8]);

                    // ����ҽʦ����[12]
                    //terminalApply.Item.DoctInfo.ID = this.Reader[11].ToString();
                    terminalApply.Item.Order.ReciptDoctor.ID = this.Reader[9].ToString();

                    // ������[13]
                    //terminalApply.Item.RecipeNo = this.Reader[12].ToString();
                    terminalApply.Item.RecipeNO = this.Reader[10].ToString();

                    // ��������Ŀ��ˮ��[14]
                    //terminalApply.Item.SeqNo = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[13].ToString());
                    terminalApply.Item.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[11].ToString());

                    // ��Ŀ����[15]
                    terminalApply.Item.Item.ID = this.Reader[12].ToString();

                    // ��Ŀ����[16]
                    terminalApply.Item.Item.Name = this.Reader[13].ToString();

                    // ����[17]
                    //terminalApply.Item.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[16].ToString());
                    terminalApply.Item.Item.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[14].ToString());

                    // ����[18]
                    //terminalApply.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[17].ToString());
                    terminalApply.Item.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[15].ToString());

                    //��ǰ��λ[19]
                    //terminalApply.Item.PriceUnit = this.Reader[18].ToString();
                    terminalApply.Item.Item.PriceUnit = this.Reader[16].ToString();

                    //���״���[20]
                    //terminalApply.Item.Package.ID = this.Reader[19].ToString();
                    terminalApply.Item.UndrugComb.ID = this.Reader[17].ToString();

                    // ��������[21]
                    //terminalApply.Item.Package.Name = this.Reader[20].ToString();
                    terminalApply.Item.UndrugComb.Name = this.Reader[18].ToString();

                    // ���ý��[22]
                    //terminalApply.Item.Cost.Tot_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[21].ToString());
                    terminalApply.Item.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[19].ToString());

                    // ��Ŀ״̬��0 ����  1 ���� 2 ִ�У�ҩƷ���ţ���[23]
                    terminalApply.ItemStatus = this.Reader[20].ToString();

                    // ��ȷ����[24]
                    terminalApply.AlreadyConfirmCount = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[21].ToString());

                    // �豸��[25]
                    terminalApply.Machine.ID = this.Reader[22].ToString();

                    // �����豸��Ż�ȡ�豸����
                    //
                    if (this.Reader[23].ToString() == "0") // �շѱ�־��0δ�շѣ�1���շ�[26]
                    {
                        terminalApply.Item.PayType = PayTypes.Charged;
                    }
                    else
                    {
                        terminalApply.Item.PayType = PayTypes.Balanced;
                    } // �շѱ�־��0δ�շѣ�1���շ�[26]

                    // �¾���Ŀ��ʶ�� 0 �� 1 ��[27]
                    terminalApply.NewOrOld = this.Reader[24].ToString();

                    // ��չ��־1[28]
                    terminalApply.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[25].ToString());

                    // ��չ��־2(�շѷ�ʽ0סԺ��ֱ���շ�,1��ʿվҽ���շ�,2ȷ���շ�,3��ݱ��,4��������)[29]
                    terminalApply.User02 = this.Reader[26].ToString();

                    // ��ע[30]
                    terminalApply.User03 = this.Reader[27].ToString();

                    // ҽ����ˮ��[31]
                    terminalApply.Order.ID = this.Reader[28].ToString();

                    // ҽ��ִ�е���ˮ��[32]
                    terminalApply.OrderExeSequence = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[29].ToString());

                    // ����Ա���������뵥��[33]
                    //terminalApply.InsertOperator.ID = this.Reader[32].ToString();
                    terminalApply.InsertOperEnvironment.ID = this.Reader[30].ToString();

                    // ����ʱ�䣨�������뵥��[34]
                    //terminalApply.InsertDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[33].ToString());
                    terminalApply.InsertOperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[31].ToString());

                    // ������𣺡�1�� ����|��2�� סԺ|��3�� ����|��4�� ���[35]
                    terminalApply.PatientType = this.Reader[32].ToString();
                    terminalApply.Patient.Memo = this.Reader[32].ToString();

                    // �Ա�[36]
                    //terminalApply.Patient.SexID = this.Reader[35].ToString();
                    terminalApply.Patient.Sex.ID = this.Reader[33].ToString();

                    // ҩƷ����ʱ��[37]
                    terminalApply.SendDrugDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[34].ToString());

                    // �ն�ִ���˱��[38]
                    //terminalApply.ConfirmOperator.ID = this.Reader[37].ToString();
                    terminalApply.ConfirmOperEnvironment.ID = this.Reader[35].ToString();

                    // �ն�ִ��ʱ��[39]
                    //terminalApply.ConfirmDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[38].ToString());
                    terminalApply.ConfirmOperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[36].ToString());

                    // �Ƿ�ҩƷ��1����/0����[40]
                    //terminalApply.Item.IsPhamarcy = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[39].ToString());
                    //terminalApply.Item.Item.IsPharmacy = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[37].ToString());
                    terminalApply.Item.Item.ItemType = (EnumItemType)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[37].ToString());

                    // ������
                    //terminalApply.Patient.PID.CardNo = this.Reader[40].ToString();
                    terminalApply.Patient.PID.CardNO = this.Reader[38].ToString();

                    applyList.Add(terminalApply);
                    #endregion

                }
            }//�׳�����
            catch (Exception ex)
            {
                this.Err = "��ȡ�ն�ȷ����Ϣ����" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            this.Reader.Close();
            return applyList;
        }
        #endregion
        #region ��Reader���ص����뵥ȫ����ϸArrayList
        /// <summary>
        /// ��Reader���ص����뵥ȫ����ϸArrayList
        /// </summary>
        /// <param name="applyList">���ص����뵥ȫ���ֶ���ϸ</param>
        private void FillApply(ref ArrayList applyList)
        {
            // ������ض���
            FS.HISFC.Models.Terminal.TerminalApply terminalApply;

            while (this.Reader.Read()) // ѭ����ȡ�̳е�Reader����
            {
                terminalApply = new TerminalApply();
                // ���뵥��ˮ��[3]
                terminalApply.ID = this.Reader[0].ToString();

                // סԺ��ˮ�Ż������[04]
                terminalApply.Patient.PID.ID = this.Reader[1].ToString();
                terminalApply.Patient.ID = terminalApply.Patient.PID.ID;
                // ����[05]
                terminalApply.Patient.Name = this.Reader[2].ToString();
                // ��ͬ��λ[06]
                terminalApply.Patient.Pact.ID = this.Reader[3].ToString();
                // ���벿�ű��루���һ��߲�����[07]
                terminalApply.Item.Order.DoctorDept.ID = this.Reader[4].ToString();
                // �ն˿��ұ���[08]
                terminalApply.Item.ExecOper.Dept.ID = this.Reader[5].ToString();
                // �����ǹҺſ��ҡ�סԺ����Ժ����[09]
                terminalApply.Patient.DoctorInfo.Templet.Dept.ID = this.Reader[6].ToString();
                // ��ҩ���ű���[10]
                terminalApply.Item.Order.DoctorDept.ID = this.Reader[7].ToString();

                // ���¿�����ˮ��(����)[11]
                terminalApply.UpdateStoreSequence = Convert.ToInt32(this.Reader[8]);

                // ����ҽʦ����[12]
                //terminalApply.Item.DoctInfo.ID = this.Reader[11].ToString();
                terminalApply.Item.Order.ReciptDoctor.ID = this.Reader[9].ToString();

                // ������[13]
                //terminalApply.Item.RecipeNo = this.Reader[12].ToString();
                terminalApply.Item.RecipeNO = this.Reader[10].ToString();

                // ��������Ŀ��ˮ��[14]
                //terminalApply.Item.SeqNo = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[13].ToString());
                terminalApply.Item.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[11].ToString());

                // ��Ŀ����[15]
                terminalApply.Item.Item.ID = this.Reader[12].ToString();

                // ��Ŀ����[16]
                terminalApply.Item.Item.Name = this.Reader[13].ToString();

                // ����[17]
                //terminalApply.Item.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[16].ToString());
                terminalApply.Item.Item.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[14].ToString());

                // ����[18]
                //terminalApply.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[17].ToString());
                terminalApply.Item.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[15].ToString());

                //��ǰ��λ[19]
                //terminalApply.Item.PriceUnit = this.Reader[18].ToString();
                terminalApply.Item.Item.PriceUnit = this.Reader[16].ToString();

                //���״���[20]
                //terminalApply.Item.Package.ID = this.Reader[19].ToString();
                terminalApply.Item.UndrugComb.ID = this.Reader[17].ToString();

                // ��������[21]
                //terminalApply.Item.Package.Name = this.Reader[20].ToString();
                terminalApply.Item.UndrugComb.Name = this.Reader[18].ToString();

                // ���ý��[22]
                //terminalApply.Item.Cost.Tot_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[21].ToString());
                terminalApply.Item.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[19].ToString());

                // ��Ŀ״̬��0 ����  1 ���� 2 ִ�У�ҩƷ���ţ���[23]
                terminalApply.ItemStatus = this.Reader[20].ToString();

                // ��ȷ����[24]
                terminalApply.AlreadyConfirmCount = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[21].ToString());

                // �豸��[25]
                terminalApply.Machine.ID = this.Reader[22].ToString();

                // �����豸��Ż�ȡ�豸����
                //
                if (this.Reader[23].ToString() == "0") // �շѱ�־��0δ�շѣ�1���շ�[26]
                {
                    terminalApply.Item.PayType = PayTypes.Charged;
                }
                else
                {
                    terminalApply.Item.PayType = PayTypes.Balanced;
                } // �շѱ�־��0δ�շѣ�1���շ�[26]

                // �¾���Ŀ��ʶ�� 0 �� 1 ��[27]
                terminalApply.NewOrOld = this.Reader[24].ToString();

                // ��Ч��־1[28]
                terminalApply.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[25].ToString());

                // ��չ��־2(�շѷ�ʽ0סԺ��ֱ���շ�,1��ʿվҽ���շ�,2ȷ���շ�,3��ݱ��,4��������)[29]
                terminalApply.User02 = this.Reader[26].ToString();

                // ��ע[30]
                terminalApply.User03 = this.Reader[27].ToString();

                // ҽ����ˮ��[31]
                terminalApply.Order.ID = this.Reader[28].ToString();

                // ҽ��ִ�е���ˮ��[32]
                terminalApply.OrderExeSequence = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[29].ToString());

                // ����Ա���������뵥��[33]
                //terminalApply.InsertOperator.ID = this.Reader[32].ToString();
                //terminalApply.InsertOperEnvironment.ID = this.Reader[30].ToString(); //{D95D9641-54AF-4a47-9879-942E26618258}
                terminalApply.ConfirmOperEnvironment.ID = this.Reader[30].ToString();
                // ����ʱ�䣨�������뵥��[34]
                //terminalApply.InsertDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[33].ToString());
                terminalApply.InsertOperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[31].ToString());

                // ������𣺡�1�� ����|��2�� סԺ|��3�� ����|��4�� ���[35]
                terminalApply.PatientType = this.Reader[32].ToString();
                terminalApply.Patient.Memo = this.Reader[32].ToString();

                // �Ա�[36]
                //terminalApply.Patient.SexID = this.Reader[35].ToString();
                terminalApply.Patient.Sex.ID = this.Reader[33].ToString();

                // ҩƷ����ʱ��[37]
                terminalApply.SendDrugDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[34].ToString());

                // �ն�ִ���˱��[38]
                //terminalApply.ConfirmOperator.ID = this.Reader[37].ToString();
                terminalApply.InsertOperEnvironment.ID = this.Reader[35].ToString();

                // �ն�ִ��ʱ��[39]
                //terminalApply.ConfirmDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[38].ToString());
                terminalApply.ConfirmOperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[36].ToString());

                // �Ƿ�ҩƷ��1����/0����[40]
                //terminalApply.Item.IsPhamarcy = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[39].ToString());
                terminalApply.Item.Item.ItemType = (EnumItemType)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[37].ToString());

                // ������
                //terminalApply.Patient.PID.CardNo = this.Reader[40].ToString();
                terminalApply.Patient.PID.CardNO = this.Reader[38].ToString();

                applyList.Add(terminalApply);

            } // ѭ����ȡ�̳е�Reader����
        }
        #endregion
        #region ת��ʵ�嵽��������
        /// <summary>
        /// ת��ʵ�嵽��������
        /// </summary>
        /// <param name="terminalApply">�����ʵ������</param>
        /// <param name="newApply">�Ƿ��������뵥</param>
        /// <returns>1���ɹ�/-1ʧ��</returns>
        private int FillParms(FS.HISFC.Models.Terminal.TerminalApply terminalApply, bool newApply)
        {
            //
            // ����Ŀ����ˮ��
            //
            string sequence = "";

            //
            // ת����ֵ
            //
            // ���뵥��ˮ��
            if (newApply)
            {
                intReturn = this.GetNextSequence(ref sequence);
                if (intReturn == -1)
                {
                    this.SetError("", "��ȡSequenceʧ��");
                    return -1;
                }
                parms[0] = sequence;
                terminalApply.ID = sequence;
                if (terminalApply.Item.PayType == PayTypes.Balanced && (terminalApply.ItemStatus == "" || terminalApply.ItemStatus == null || terminalApply.ItemStatus == string.Empty || terminalApply.ItemStatus == "0"))
                {
                    terminalApply.ItemStatus = "1";
                }

            }
            else
            {
                parms[0] = terminalApply.ID;
            }
            // סԺ��ˮ�Ż������
            if (terminalApply.Patient.PID.ID == "")
            {
                terminalApply.Patient.PID.ID = terminalApply.Patient.ID;
            }
            parms[1] = terminalApply.Patient.PID.ID;
            // ����
            parms[2] = terminalApply.Patient.Name;
            // ��ͬ��λ
            parms[3] = terminalApply.Patient.Pact.ID;
            // ���벿�ű���
            parms[4] = terminalApply.Item.Order.DoctorDept.ID;
            if (parms[4] == "")
            {
                // ������벿��Ϊnull,��ô�ҺŲ���
                parms[4] = terminalApply.Patient.DoctorInfo.Templet.Dept.ID;
            }
            // �ն˿��ұ���
            parms[5] = terminalApply.Item.ExecOper.Dept.ID;
            // �����ǹҺſ��ҡ�סԺ����Ժ����
            parms[6] = terminalApply.Patient.DoctorInfo.Templet.Dept.ID;
            // ��ҩ���ű���
            parms[7] = terminalApply.Item.ExecOper.Dept.ID;
            // ���¿�����ˮ��(����)
            parms[8] = terminalApply.UpdateStoreSequence.ToString();
            // ����ҽʦ����
            //parms[9] = terminalApply.Item.Order.Doctor.ID;
            parms[9] = terminalApply.Item.RecipeOper.ID;
            // ������
            parms[10] = terminalApply.Item.RecipeNO;
            // ��������Ŀ��ˮ��
            parms[11] = terminalApply.Item.SequenceNO.ToString();
            // ��Ŀ����
            parms[12] = terminalApply.Item.Item.ID;
            // ��Ŀ����
            parms[13] = terminalApply.Item.Item.Name;
            // ����
            parms[14] = terminalApply.Item.Item.Price.ToString();
            // ����
            parms[15] = terminalApply.Item.Item.Qty.ToString();
            // ��ǰ��λ
            parms[16] = terminalApply.Item.Item.PriceUnit;
            // ���״���
            parms[17] = terminalApply.Item.UndrugComb.ID;
            // ��������
            parms[18] = terminalApply.Item.UndrugComb.Name;
            // ���ý��
            parms[19] = terminalApply.Item.FT.TotCost.ToString();
            // ��Ŀ״̬��0 ����  1 ���� 2 ִ�У�ҩƷ���ţ���
            parms[20] = terminalApply.ItemStatus;
            // ��ȷ����
            parms[21] = terminalApply.AlreadyConfirmCount.ToString();
            // �豸��
            parms[22] = "";  // ************* �����շѲ���û�и���Ϣ������û�и�ֵ���������ػ������뵥��ϸʱ��̬����
            // �շѱ�־��0δ�շѣ�1���շ�
            if (terminalApply.Item.PayType == PayTypes.Charged)
            {
                parms[23] = "0";
            }
            else
            {
                parms[23] = "1";
            }
            // �¾���Ŀ��ʶ�� 0 �� 1 ��
            parms[24] = "0";
            // �Ƿ���Ч1
            parms[25] = FS.FrameWork.Function.NConvert.ToInt32(terminalApply.IsValid).ToString();
            // ��չ��־2(�շѷ�ʽ0סԺ��ֱ���շ�,1��ʿվҽ���շ�,2ȷ���շ�,3��ݱ��,4��������)��
            parms[26] = terminalApply.User02;
            // ��ע
            parms[27] = terminalApply.User03;
            // ҽ����ˮ��
            parms[28] = terminalApply.Order.ID;
            if (parms[28] == "" || parms[28] == null)
            {
                //parms[28] = terminalApply.Item.MoOrder;
                parms[28] = terminalApply.Item.Order.ID;
            }
            if (parms[28] == "" || parms[28] == null)
            {
                this.SetError("", "û��ҽ����ˮ��");
                return -1;
            }
            // ҽ��ִ�е���ˮ�ţ�����=ҽ����ˮ��
            parms[29] = terminalApply.OrderExeSequence.ToString();
            // ����Ա���������뵥��
            parms[30] = terminalApply.InsertOperEnvironment.ID;
            // ����ʱ�䣨�������뵥��
            parms[31] = terminalApply.InsertOperEnvironment.OperTime.ToString();
            // ������𣺡�1�� ����|��2�� סԺ|��3�� ����|��4�� ���
            parms[32] = terminalApply.PatientType;
            if (parms[32] == "" || parms[32] == null)
            {
                this.SetError("", "�����������Ϊ��:��1�� ����|��2�� סԺ|��3�� ����|��4�� ���");
                return -1;
            }
            // �Ա�
            parms[33] = terminalApply.Patient.Sex.ID.ToString();
            // ҩƷ����ʱ��
            parms[34] = DateTime.MinValue.ToString();
            // �ն�ִ���˱��
            parms[35] = terminalApply.ConfirmOperEnvironment.ID;
            // �ն�ִ��ʱ��
            parms[36] = DateTime.MinValue.ToString();
            // �Ƿ�ҩƷ
            //if (terminalApply.Item.Item.IsPharmacy)
            if(terminalApply.Item.Item.ItemType == EnumItemType.Drug)
            {
                parms[37] = "1";
            }
            else
            {
                parms[37] = "0";
            }
            // ���߲�����
            parms[38] = terminalApply.Patient.PID.CardNO;

            return 1;
        }
        #endregion

        #region ��ȡʱ������(1����ȡ�ɹ���-1����ȡʧ��)
        /// <summary>
        /// ��ȡʱ������(1����ȡ�ɹ���-1����ȡʧ��)
        /// </summary>
        /// <returns>1����ȡ�ɹ���-1����ȡʧ��</returns>
        private int GetLimited()
        {
            //
            // �ж��Ƿ���ʱ������
            //
            this.intReturn = this.JudgeDaysLimited(ref this.stringLimitedDate);
            // ���ʧ�ܷ���-1
            if (this.intReturn == -1)
            {
                return -1;
            }

            //
            // �жϳɹ����������ã�0����û�����ƣ�1����������
            // 
            if (this.intReturn == 0)
            {
                boolLimited = false;
            }
            if (this.intReturn == 1)
            {
                boolLimited = true;
            }

            return 1;
        }
        #endregion

        #region �ϲ�SQL��䣨�ǲ�ѯ�����������ϲ���һ��
        /// <summary>
        /// �ϲ�SQL��䣨�ǲ�ѯ�����������ϲ���һ��
        /// </summary>
        private void CreateSql()
        {
            this.SQL = this.SQL + " " + this.WHERE;// +" " + this.ORDER;
        }
        #endregion

        #region ��ȡ�µ�Sequence
        /// <summary>
        /// ��ȡ�µ�Sequence(1���ɹ�/-1��ʧ��)
        /// </summary>
        /// <param name="sequence">��ȡ���µ�Sequence</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int GetNextSequence(ref string sequence)
        {
            //
            // ִ��SQL���
            //
            sequence = this.GetSequence("FS.HISFC.Management.TerminalValidate.GetNextSequence");
            //
            // �������NULL�����ȡʧ��
            //
            if (sequence == null)
            {
                this.SetError("", "��ȡSequenceʧ��");
                return -1;
            }
            //
            // �ɹ�����
            //
            return 1;
        }
        #endregion

        #region ת��סԺ����ʵ���סԺ���߷���Ϊ�ն�ȷ��ʵ��
        /// <summary>
        /// ת��סԺ����ʵ���סԺ���߷���Ϊ�ն�ȷ��ʵ��
        /// </summary>
        /// <param name="inpatientFee">סԺ���߷���</param>
        /// <param name="terminalApply">ҽ���ն�ʵ��</param>
        /// <returns>1���ɹ�����1��ʧ��</returns>
        public int ConvertToTerminalApply(FS.HISFC.Models.Fee.Inpatient.FeeItemList inpatientFee,
                                          ref FS.HISFC.Models.Terminal.TerminalApply terminalApply,
                                          FS.HISFC.Models.Terminal.InpatientChargeType chargeType)
        {
            try
            {
                // סԺ��
                terminalApply.Patient.PID.ID = inpatientFee.Patient.PID.ID;
                // ����[05]
                terminalApply.Patient.Name = inpatientFee.Patient.Name;
                // ��ͬ��λ[06]
                terminalApply.Patient.Pact.ID = inpatientFee.Patient.Pact.ID;
                // ���벿�ű��루���һ��߲�����[07]
                terminalApply.Item.Order.DoctorDept.ID = inpatientFee.Order.DoctorDept.ID;
                // �ն˿��ұ���[08]
                terminalApply.Item.ExecOper.Dept.ID = inpatientFee.Order.ExecOper.Dept.ID;
                // �����ǹҺſ��ҡ�סԺ����Ժ����[09]
                terminalApply.Patient.DoctorInfo.Templet.Dept.ID = inpatientFee.Order.DoctorDept.ID;
                // ��ҩ���ű���[10]
                terminalApply.Item.Order.DoctorDept.ID = inpatientFee.Order.StockDept.ID;
                // ���¿�����ˮ��(����)[11]
                terminalApply.UpdateStoreSequence = 0;
                // ����ҽʦ����[12]
                terminalApply.Item.Order.ReciptDoctor.ID = inpatientFee.Order.ReciptDoctor.ID;
                // ������[13]
                terminalApply.Item.RecipeNO = inpatientFee.RecipeNO;
                // ��������Ŀ��ˮ��[14]
                terminalApply.Item.SequenceNO = inpatientFee.SequenceNO;
                // ��Ŀ����[15]
                terminalApply.Item.Item.ID = inpatientFee.Item.ID;
                // ��Ŀ����[16]
                terminalApply.Item.Item.Name = inpatientFee.Item.Name;
                // ����[17]
                terminalApply.Item.Item.Price = inpatientFee.Item.Price;
                // ����[18]
                terminalApply.Item.Item.Qty = inpatientFee.Item.Qty;
                //��ǰ��λ[19]
                terminalApply.Item.Item.PriceUnit = inpatientFee.Item.PriceUnit;
                //���״���[20]
                terminalApply.Item.UndrugComb.ID = inpatientFee.UndrugComb.ID;
                // ��������[21]
                terminalApply.Item.UndrugComb.Name = inpatientFee.UndrugComb.Name;
                // ���ý��[22]
                terminalApply.Item.FT.TotCost = inpatientFee.FT.TotCost;
                // ��Ŀ״̬��0 ����  1 ���� 2 ִ�У�ҩƷ���ţ���[23]
                if (inpatientFee.PayType == PayTypes.Charged)
                {
                    terminalApply.ItemStatus = "0";
                }
                else
                {
                    terminalApply.ItemStatus = "1";
                }
                // ��ȷ����[24]
                terminalApply.AlreadyConfirmCount = 0;
                // �豸��[25]
                terminalApply.Machine.ID = inpatientFee.MachineNO;
                // �¾���Ŀ��ʶ�� 0 �� 1 ��[27]
                terminalApply.NewOrOld = "0";
                // ��չ��־1[28]
                terminalApply.IsValid = true;
                // ��չ��־2(�շѷ�ʽ0סԺ��ֱ���շ�,1��ʿվҽ���շ�,2ȷ���շ�,3��ݱ��,4��������)[29]
                terminalApply.User02 = ((int)chargeType).ToString();
                // ��ע[30]
                terminalApply.User03 = inpatientFee.Item.Memo;
                // ҽ����ˮ��[31]
                terminalApply.Order.ID = inpatientFee.Order.ID;
                // ҽ��ִ�е���ˮ��[32]
                terminalApply.OrderExeSequence = FS.FrameWork.Function.NConvert.ToInt32(inpatientFee.ExecOrder.ID);
                // ����Ա���������뵥��[33]
                terminalApply.InsertOperEnvironment.ID = inpatientFee.ChargeOper.ID;
                // ����ʱ�䣨�������뵥��[34]
                terminalApply.InsertOperEnvironment.OperTime = inpatientFee.ChargeOper.OperTime;
                // ������𣺡�1�� ����|��2�� סԺ|��3�� ����|��4�� ���[35]
                terminalApply.PatientType = "2";
                // �Ա�[36]
                terminalApply.Patient.Sex.ID = inpatientFee.Patient.Sex.ID;
                // ҩƷ����ʱ��[37]
                terminalApply.SendDrugDate = DateTime.MinValue;
                // �ն�ִ���˱��[38]
                terminalApply.ConfirmOperEnvironment.ID = "";
                // �ն�ִ��ʱ��[39]
                terminalApply.ConfirmOperEnvironment.OperTime = DateTime.MinValue;
                // �Ƿ�ҩƷ��1����/0����[40]
                //terminalApply.Item.Item.IsPharmacy = terminalApply.Order.Item.IsPharmacy;
                terminalApply.Item.Item.ItemType = terminalApply.Order.Item.ItemType;
                // ������
                terminalApply.Patient.PID.CardNO = inpatientFee.Patient.PID.CardNO;
            }
            catch (Exception e)
            {
                this.Err += e.Message;
                return -1;
            }
            // �ɹ�����
            return 1;
        }

        #endregion

        //
        // ҵ������
        //
        #region �������뵥
        /// <summary>
        /// �������뵥
        /// </summary>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        private int Insert()
        {
            //
            // ��ȡSQL���
            //
            if (this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.Terminal.Insert", ref SQL) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }

            //
            // ��ʽ��SQL���
            //
            try
            {
                SQL = string.Format(SQL, parms);
            }
            catch (Exception e)
            {
                this.SetError("", "��ʽ��SQL���ʧ��!" + e.Message);
                return -1;
            }

            //
            // ִ��SQL���
            //
            intReturn = this.ExecNoQuery(SQL);
            if (intReturn <= 0)
            {
                this.SetError("", "ִ��SQL���ʧ��");
                return -1;
            }

            return 1;
        }
        #endregion
        #region �������뵥
        /// <summary>
        /// �������뵥
        /// </summary>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        private int Update()
        {
            //
            // ��ȡSQL���
            //
            if (this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.Terminal.ReUpdate.Update", ref SQL) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            if (this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.Terminal.ReUpdate.Where", ref WHERE) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }

            this.CreateSql();

            //
            // ��ʽ��SQL���
            //
            try
            {
                SQL = string.Format(SQL, parms, parms[28]);
            }
            catch (Exception e)
            {
                this.SetError("", "��ʽ��SQL���ʧ��!" + e.Message);
                return -1;
            }

            //
            // ִ��SQL���
            //
            intReturn = this.ExecNoQuery(SQL);
            if (intReturn <= 0)
            {
                this.SetError("", "ִ��SQL���ʧ��");
                return -1;
            }

            return 1;
        }
        #endregion
        #region ����ҽ����ˮ��ɾ�����뵥(1���ɹ�/-1��ʧ��)
        /// <summary>
        /// ����ҽ����ˮ��ɾ�����뵥(1���ɹ�/-1��ʧ��)
        /// </summary>
        /// <param name="stringOrderNo">ҽ����ˮ��</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int DeleteByOrder(string stringOrderNo)
        {
            // �ж����뵥�Ƿ����
            string stringTemp = "";
            this.intReturn = JudgeIfConfirm(stringOrderNo, ref stringTemp);
            if (intReturn == -1)
            {
                return -1;
            }
            else if (intReturn != 1)
            {
                //�ն�ȷ�ϱ��в����� ����Ҫɾ��
                return 1;
            }
            //
            // ��ʼ������
            //
            this.Clear();

            //
            // ��ȡSQL���
            //
            if (this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.Terminal.DeleteByOrder.Delete", ref SQL) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            if (this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.Terminal.DeleteByOrder.Where", ref WHERE) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            this.CreateSql();

            //
            // ��ʽ��SQL���
            //
            try
            {
                SQL = string.Format(SQL, stringOrderNo);
            }
            catch (Exception e)
            {
                this.SetError("", "��ʽ��SQL���ʧ��!" + e.Message);
                return -1;
            }

            //
            // ִ��SQL���
            //
            intReturn = this.ExecNoQuery(SQL);
            if (intReturn <= 0)
            {
                this.SetError("", "ִ��SQL���ʧ��");
                return -1;
            }

            return 1;
        }
        #endregion
        //
        // ҵ����ϸ�ӱ�
        #region �������
        /// <summary>
        /// �������
        /// [����: FS.HISFC.Models.Terminal.TerminalConfirmDetail detail - ҵ���ӱ�ʵ��]
        /// </summary>
        /// <param name="detail">ҵ���ӱ�ʵ��</param>
        private void FillDetailParm(FS.HISFC.Models.Terminal.TerminalConfirmDetail detail)
        {
            //// ��������
            //this.detailParms[0] = detail.Hospital.ID;
            //// ��������
            //this.detailParms[1] = detail.Hospital.Name;
            // '��ˮ��';
            //this.detailParms[0] = detail.Sequence;
            //  '���뵥��ˮ��';
            this.detailParms[0] = detail.Apply.ID;
            // '�������';
            this.detailParms[1] = detail.Apply.PatientType;
            // '����';
            this.detailParms[2] = detail.Apply.Patient.PID.CardNO;
            //if (detail.Apply.Patient.PID.CardNO == null)
            //{
            //    this.detailParms[5] = detail.Apply.Patient.CardNo;
            //}
            // '�����';
            this.detailParms[3] = detail.Apply.Patient.ID;
            //  '��Ŀ����';
            this.detailParms[4] = detail.Apply.Item.Item.ID;
            //  '��Ŀ����';
            this.detailParms[5] = detail.Apply.Item.Item.Name;
            //  '��������';
            this.detailParms[6] = detail.Apply.Item.Item.Qty.ToString();
            //  '����ȷ������';
            this.detailParms[7] = detail.Apply.Item.ConfirmedQty.ToString();
            //  'ʣ������';
            this.detailParms[8] = detail.FreeCount.ToString();
            //  'ȷ��ʱ��';
            this.detailParms[9] = detail.Apply.ConfirmOperEnvironment.OperTime.ToString();
            //  'ȷ�Ͽ���';
            this.detailParms[10] = detail.Apply.Item.ExecOper.Dept.ID;
            //  'ȷ����';
            this.detailParms[11] = detail.Apply.ConfirmOperEnvironment.ID;
            //  '���뵥״̬';
            this.detailParms[12] = detail.Status.ID;
            //  '��չ�ֶ�1';
            this.detailParms[13] = detail.Apply.Order.ID;
            //  '��չ�ֶ�2';
            this.detailParms[14] = detail.Apply.SpecalFlag;;
            //  '��չ�ֶ�3';
            this.detailParms[15] = detail.User03;
            this.detailParms[16] = detail.OperInfo.ID;//��̨Ա {D95D9641-54AF-4a47-9879-942E26618258}
            this.detailParms[17] = detail.OperInfo.OperTime.ToString();//����ʱ��
            this.detailParms[18] = detail.ExecDevice;//ִ���豸
            this.detailParms[19] = detail.OperInfo.ID;//��̨Ա {D95D9641-54AF-4a47-9879-942E26618258}
        }
        #endregion
        #region ת��ReaderΪʵ��
        /// <summary>
        /// ת��ReaderΪʵ��
        /// [����: FS.HISFC.Models.Terminal.TerminalConfirmDetail detail - ת�����ʵ��]
        /// [����: int,1-�ɹ�,-1-ʧ��]
        /// </summary>
        /// <param name="detail">ת�����ʵ��</param>
        /// <returns>1-�ɹ�,-1-ʧ��</returns>
        private int DetailReaderToObject(FS.HISFC.Models.Terminal.TerminalConfirmDetail detail)
        {
            // ��������
            //detail.Hospital.ID = this.Reader[0].ToString();
            //// ��������
            //detail.Hospital.Name = this.Reader[1].ToString();
            // '��ˮ��';
            try
            {
                detail.Sequence = this.Reader[0].ToString();
            }
            catch
            {
                //
                // ת��ʧ��
                //
                this.SetError("", "ת����ʵ�����ʧ��!");
                return -1;
            }
            //  '���뵥��ˮ��';
            detail.Apply.ID = this.Reader[1].ToString();
            // '�������';
            detail.Apply.PatientType = this.Reader[2].ToString();
            // '����';
            detail.Apply.Patient.PID.CardNO = this.Reader[3].ToString();
            // '�����';
            detail.Apply.Patient.PID.ID = this.Reader[4].ToString();
            //  '��Ŀ����';
            detail.Apply.Item.Item.ID = this.Reader[5].ToString();
            //  '��Ŀ����';
            detail.Apply.Item.Item.Name = this.Reader[6].ToString();
            //  '��������';
            try
            {
                detail.Apply.Item.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7].ToString());
            }
            catch
            {
                this.SetError("", "ת����ʵ�����ʧ��!");
                return -1;
            }
            //  '����ȷ������';
            try
            {
                detail.Apply.Item.ConfirmedQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[8].ToString());
            }
            catch
            {
                this.SetError("", "ת����ʵ�����ʧ��!");
                return -1;
            }
            //  'ʣ������';
            try
            {
                detail.FreeCount = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9].ToString());
            }
            catch
            {
                this.SetError("", "ת����ʵ�����ʧ��!");
                return -1;
            }
            //  'ȷ��ʱ��';
            try
            {
                detail.Apply.ConfirmOperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[10].ToString());
            }
            catch
            {
                this.SetError("", "ת����ʵ�����ʧ��!");
                return -1;
            }
            //  'ȷ�Ͽ���';
            detail.Apply.Item.ConfirmOper.Dept.ID = this.Reader[11].ToString();
            //  'ȷ����';
            detail.Apply.ConfirmOperEnvironment.ID = this.Reader[12].ToString();
            //  '���뵥״̬';
            detail.Status.ID = this.Reader[13].ToString();
            //  '��չ�ֶ�';
            detail.Apply.Order.ID = this.Reader[14].ToString();
            //  '��չ�ֶ�';
            detail.Apply.SpecalFlag = this.Reader[15].ToString();
            //  '��չ�ֶ�';
            detail.User03 = this.Reader[16].ToString();
            detail.OperInfo.ID = this.Reader[17].ToString(); //����Ա
            detail.OperInfo.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[18].ToString()); //����ʱ��
            detail.CancelInfo.ID = this.Reader[19].ToString();//������
            detail.CancelInfo.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[20].ToString()); //����ʱ��
            //
            // �ɹ�����
            //
            return 1;
        }
        #endregion

        #endregion

        #region ���к���

        #region �����ն�ȷ������
        /// <summary>
        /// �����ն�ȷ������
        /// </summary>
        /// <param name="MoOrder"></param>
        /// <param name="ItemCode"></param>
        /// <returns></returns>
        public int UpdateConfirm(string MoOrder,string ItemCode)
        {
            // SQL���
            string sql = "";
            //
            // ��ȡSQL���
            //
            if (this.Sql.GetSql("TerminalConfirm.UpdateConfirm.1", ref sql) == -1)
            {
                this.Err = "��ȡSQL���TerminalConfirm.UpdateConfirm.1ʧ��";
                return -1;
            }
            // ƥ��ִ��
            try
            {
                //sql = string.Format(sql, GetParam(medTechItem));
                return this.ExecNoQuery(sql, MoOrder, ItemCode);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
        }
        #endregion
        #region �����ն�ȷ�ϱ��ִ�б�־
        /// <summary>
        /// �����ն�ȷ�ϱ��ִ�б�־
        /// </summary>
        /// <param name="MoOrder"></param>
        /// <param name="ItemCode"></param>
        /// <returns></returns>
        public int UpdateConfirmSendFlag(string MoOrder, string ItemCode,string SendFlag)
        {
            // SQL���
            string sql = "";
            //
            // ��ȡSQL���
            //
            if (this.Sql.GetSql("TerminalConfirm.UpdateConfirmSendFlag", ref sql) == -1)
            {
                this.Err = "��ȡSQL���TerminalConfirm.UpdateConfirmSendFlagʧ��";
                return -1;
            }
            // ƥ��ִ��
            try
            {
                return this.ExecNoQuery(sql, MoOrder, ItemCode, SendFlag);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
        }
        #endregion 
        #region ���·��ñ����ȷ������ ��ȷ�ϱ�� ,ȡ��ȷ�ϵ���Ŀʱ����
        /// <summary>
        /// ���·��ñ����ȷ������ ��ȷ�ϱ�� ,ȡ��ȷ�ϵ���Ŀʱ����
        /// </summary>
        /// <param name="MoOrder"></param>
        /// <param name="ConfirmFlag"></param>
        /// <param name="ConfirmQty"></param>
        /// <returns></returns>
        public int UpdateFeeConfirmQty(string MoOrder, string ConfirmFlag, int ConfirmQty)
        {
            // SQL���
            string sql = "";
            //
            // ��ȡSQL���
            //
            if (this.Sql.GetSql("TerminalConfirm.UpdateFeeConfirmQty", ref sql) == -1)
            {
                this.Err = "��ȡSQL���TerminalConfirm.UpdateFeeConfirmQty.1ʧ��";
                return -1;
            }
            // ƥ��ִ��
            try
            {
                //sql = string.Format(sql, GetParam(medTechItem));
                return this.ExecNoQuery(sql, MoOrder, ConfirmQty.ToString(), ConfirmFlag);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
        }
               #endregion 
        //
        // Insert��Update��Delete
        //
        #region ҽ���ն�ȷ�ϲ���(1���ɹ�/-1��ʧ��)
        /// <summary>
        /// ҽ���ն�ȷ�ϲ���(1���ɹ�/-1��ʧ��)
        /// </summary>
        /// <param name="terminalApply">���뵥��</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int Insert(FS.HISFC.Models.Terminal.TerminalApply terminalApply)
        {
            // ҽ����
            string confirmFlag = "";
            // ���뵥��
            string applyNo = "";
            //
            // ��ʼ������
            //
            this.Clear();
            //
            // �ж�ҽ���ţ��ж����뵥�Ƿ���ڣ��ж����뵥�Ƿ��Ѿ�ȷ��
            //
            if (terminalApply.Order.ID == "")
            {
                terminalApply.Order.ID = terminalApply.Item.Order.ID;
            }
            #region ���������ͬ��ҽ����ˮ������������֮ǰ������,�ٲ����µ�����
            if (UpdateConfirm(terminalApply.Order.ID, terminalApply.Item.Item.ID) == -1)
            {
                return -1;
            }
            #endregion 
            this.intReturn = this.JudgeIfConfirmByID(terminalApply.ID, ref confirmFlag);
            if (intReturn == -1)
            {
                // �жϷ�������
                this.SetError("", "�ж����뵥�Ƿ����ʧ�ܣ�" + this.Err);
                return -1;
            }
            else if (intReturn == 1)
            {
                // ���ڣ��ж��Ƿ��Ѿ�ȷ��
                if (confirmFlag == "2")
                {
                    // �Ѿ�ȷ�ϣ�����ִ�и��ģ�����-1��
                    this.SetError("", "���뵥�Ѿ�ȷ�ϣ����ܸ���");
                    return -1;
                }
                else
                {
                    //
                    // δȷ�ϣ�����ִ�и���
                    //
                    // ����ҽ���Ż�ȡ���뵥��
                    if (terminalApply.Order.ID == "")
                    {
                        terminalApply.Order.ID = terminalApply.Item.Order.ID;
                    }
                    intReturn = this.GetApplyNoByOrderNo(terminalApply.Order.ID, ref applyNo);
                    if (intReturn == -1)
                    {
                        this.SetError("", "����ִ�е���ȡ���뵥��ʧ�ܣ�" + this.Err);
                        return -1;
                    }
                    // ������
                    if (this.FillParms(terminalApply, false) == -1)
                    {
                        return -1;
                    }
                    // ִ�и���
                    intReturn = this.Update();
                    {
                        if (intReturn == -1)
                        {
                            // ����ʧ�ܣ�����-1��
                            this.SetError("", "����ʧ�ܣ�" + this.Err);
                            return -1;
                        }
                        else
                        {
                            // ���³ɹ�������1��
                            return 1;
                        }
                    }
                }
            }
            else
            {
                // ������ֱ�Ӳ���
                // ������
                if (this.FillParms(terminalApply, true) == -1)
                {
                    return -1;
                }

                // ����
                intReturn = this.Insert();
                if (intReturn == -1)
                {
                    this.SetError("", "�������뵥ʧ�ܣ�" + this.Err);
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
        }
        #endregion
        #region ���ݴ����źʹ�������Ŀ��ˮ��ɾ�����뵥(Ӱ���������-1��ʧ��)
        /// <summary>
        /// ���ݴ����źʹ�������Ŀ��ˮ��ɾ�����뵥(Ӱ���������-1��ʧ��)
        /// </summary>
        /// <param name="recipeCode">������</param>
        /// <param name="sequanceInRecipe">��������Ŀ��ˮ��</param>
        /// <returns>Ӱ���������-1��ʧ��</returns>
        public int Delete(string recipeCode, string sequanceInRecipe)
        {
            //
            // ��ʼ������
            //
            this.Clear();
            //
            // ��ȡSQL
            // 
            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.Outpatient.DeleteValidate", ref SQL);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.Outpatient.DeleteValidate.Where", ref WHERE);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            // �ϲ�SQL���
            SQL = SQL + WHERE;
            //
            // ��ʽ�����
            //
            try
            {
                SQL = string.Format(SQL, recipeCode, sequanceInRecipe);
            }
            catch (Exception e)
            {
                this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                return -1;
            }
            //
            // ִ��SQL���
            //
            intReturn = this.ExecNoQuery(SQL);
            if (intReturn <= 0)
            {
                this.SetError("", "ִ��SQL���ʧ��");
                return -1;
            }

            return intReturn;
        }
        #endregion
        #region ����Ŀ����ȷ�ϱ�־(Ӱ���������-1��ʧ��)
        /// <summary>
        /// ����Ŀ����ȷ�ϱ�־(Ӱ���������-1��ʧ��)
        /// </summary>
        /// <param name="terminalApply">���뵥��</param>
        /// <returns>Ӱ���������-1��ʧ��</returns>
        public int Update(FS.HISFC.Models.Terminal.TerminalApply terminalApply)
        {
            // ��ʽ��SQL����������
            string[] parmsUpdate = new string[7];
            //
            // ��ʼ������
            //
            this.Clear();
            //
            // ��ȡSQL���
            //
            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.UpdateApply", ref SQL);
            if (intReturn == -1)
            {
                this.SetError("", "��ʽ��SQL���ʧ��");
                return -1;
            }
            //
            // ���ø�ʽ��������
            //
            parmsUpdate[0] = terminalApply.Item.ConfirmedQty.ToString(); // �Ѿ�ȷ������
            parmsUpdate[1] = terminalApply.ConfirmOperEnvironment.ID; // ȷ����
            parmsUpdate[2] = terminalApply.ConfirmOperEnvironment.OperTime.ToString(); // �ն�ִ��ʱ��
            parmsUpdate[4] = terminalApply.ID; // ��ˮ��
            //
            // ��ʽ��SQL
            //
            try
            {
                SQL = string.Format(SQL, parmsUpdate);
            }
            catch (Exception e)
            {
                this.SetError("", "��ʽ��SQLʧ��" + e.Message);
                return -1;
            }
            //
            // ִ��SQL
            //
            intReturn = this.ExecNoQuery(SQL);
            if (intReturn < 0)
            {
                this.SetError("", "ִ��SQLʧ��" + this.Err);
                return -1;
            }
            //
            // Ӱ�������
            //
            return intReturn;
        }
        #endregion
        #region �������뵥��ˮ��ɾ�����뵥(Ӱ�������/-1��ʧ��)
        /// <summary>
        /// �������뵥��ˮ��ɾ�����뵥(Ӱ�������/-1��ʧ��)
        /// </summary>
        /// <param name="sequence">���뵥��ˮ��</param>
        /// <returns>Ӱ�������/-1��ʧ��</returns>
        public int Delete(string sequence)
        {
            //
            // ��ʼ������
            //
            this.Clear();
            //
            // ��ȡSQL���
            //
            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.DeleteApplyBySequence", ref SQL);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.DeleteApplyBySequence.Where", ref WHERE);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            // �ϲ�SQL���
            SQL = SQL + WHERE;
            //
            // ��ʽ��SQL���
            //
            try
            {
                SQL = string.Format(SQL, sequence);
            }
            catch (Exception e)
            {
                this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                return -1;
            }
            //
            // ִ��SQL���
            //
            intReturn = this.ExecNoQuery(SQL);
            if (intReturn < 0)
            {
                this.SetError("", "ִ��SQL���ʧ��");
                return -1;
            }
            //
            // Ӱ�������
            //
            return intReturn;
        }
        /// <summary>
        ///����ҽ���� �����ն�ȷ�ϱ������,��ȷ������,�۸�
        /// </summary>
        /// <param name="MoOrder"></param>
        /// <returns></returns>
        public int UpdateTerminalConfirmByMoOrder(string MoOrder, int BackQty, decimal Cost)
        {
            string strSql = "";
            if (this.Sql.GetSql("TerminalConfirm.UpdateTerminalByMoOrder", ref strSql) == -1)
            {
                this.Err = "��ȡTerminalConfirm.UpdateTerminalByMoOrder ʧ��";
                return -1;
            }
            strSql = string.Format(strSql, MoOrder, BackQty, Cost);
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        ///����ҽ���� �����ն�ȷ�ϱ������
        /// </summary>
        /// <param name="MoOrder"></param>
        /// <returns></returns>
        public int UpdateTerminalConfirmByMoOrder(string MoOrder, int BackQty)
        {
            string strSql = "";
            if (this.Sql.GetSql("TerminalConfirm.UpdateTerminalByMoOrderNotCost", ref strSql) == -1)
            {
                this.Err = "��ȡTerminalConfirm.UpdateTerminalByMoOrderNotCost ʧ��";
                return -1;
            }
            strSql = string.Format(strSql, MoOrder, BackQty);
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        ///����ҽ���� ���·�����ϸ��Ŀ�����������ȷ������
        /// </summary>
        /// <param name="MoOrder"></param>
        /// <returns></returns>
        public int UpdateNobackNum(string moOrder, string itemCode, decimal cancelNum)
        {
            string strSql = "";
            if (this.Sql.GetSql("Terminal.TerminalConfirm.UpdateFeeDetailNobackNum", ref strSql) == -1)
            {
                this.Err = "��ȡ Terminal.TerminalConfirm.UpdateFeeDetailNobackNum ʧ��";

                return -1;
            }
            strSql = string.Format(strSql, moOrder, itemCode, cancelNum);

            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        ///����ҽ���� ���·�����ϸ��Ŀ�����������ȷ������
        /// </summary>
        /// <param name="MoOrder"></param>
        /// <returns></returns>
        public int UpdateNobackNumCancel(string moOrder, string itemCode, decimal cancelNum)
        {
            string strSql = "";
            if (this.Sql.GetSql("Terminal.TerminalConfirm.UpdateFeeDetailNobackNumCancel", ref strSql) == -1)
            {
                this.Err = "��ȡ Terminal.TerminalConfirm.UpdateFeeDetailNobackNumCancel ʧ��";

                return -1;
            }
            strSql = string.Format(strSql, moOrder, itemCode, cancelNum);

            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        ///����ȷ����ˮ�� ���������ն�ȷ����ϸ�����ȷ��������extend_field3=�ɵ���ȷ������
        /// </summary>
        /// <param name="MoOrder"></param>
        /// <returns></returns>
        public int UpdateConfirmedQty(string applyID, decimal newConfirmedQty, string oldConfirmedQtyString)
        {
            string strSql = "";
            if (this.Sql.GetSql("Terminal.TerminalConfirm.UpdateTaDetailConfirmedQty", ref strSql) == -1)
            {
                this.Err = "��ȡ Terminal.TerminalConfirm.UpdateTaDetailConfirmedQty ʧ��";

                return -1;
            }
            strSql = string.Format(strSql, applyID, newConfirmedQty, oldConfirmedQtyString);

            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        ///����ȷ����ˮ�� ���������ն�ȷ����ϸ�����ȷ��������extend_field3=�ɵ���ȷ������
        /// </summary>
        /// <param name="MoOrder"></param>
        /// <returns></returns>
        public int UpdateConfirmedQtyCancel(string applyID, decimal newConfirmedQty, string oldConfirmedQtyString)
        {
            string strSql = "";
            if (this.Sql.GetSql("Terminal.TerminalConfirm.UpdateTaDetailConfirmedQtyCancel", ref strSql) == -1)
            {
                this.Err = "��ȡ Terminal.TerminalConfirm.UpdateTaDetailConfirmedQtyCancel ʧ��";

                return -1;
            }
            strSql = string.Format(strSql, applyID, newConfirmedQty, oldConfirmedQtyString);

            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        ///����ȷ����ˮ�� ���������ն�ȷ����ϸ�����ȷ��������extend_field3=�ɵ���ȷ������
        /// </summary>
        /// <param name="MoOrder"></param>
        /// <returns></returns>
        public int UpdateConfirmedFlag(string applyID,string operCode,string operDate)
        {
            string strSql = "";
            if (this.Sql.GetSql("Terminal.TerminalConfirm.UpdateTaDetailConfirmedFlag", ref strSql) == -1)
            {
                this.Err = "��ȡ Terminal.TerminalConfirm.UpdateTaDetailConfirmedFlag ʧ��";

                return -1;
            }
            strSql = string.Format(strSql, applyID, operCode, operDate);

            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        ///����ȷ����ˮ�� ���������ն�ȷ����ϸ�����ȷ��������extend_field3=�ɵ���ȷ������
        /// </summary>
        /// <param name="MoOrder"></param>
        /// <returns></returns>
        public int UpdateConfirmedFlagCancel(string applyID, string operCode, string operDate)
        {
            string strSql = "";
            if (this.Sql.GetSql("Terminal.TerminalConfirm.UpdateTaDetailConfirmedFlagCancel", ref strSql) == -1)
            {
                this.Err = "��ȡ Terminal.TerminalConfirm.UpdateTaDetailConfirmedFlagCancel ʧ��";

                return -1;
            }
            strSql = string.Format(strSql, applyID, operCode, operDate);

            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        ///����ȷ����ˮ�� ���������ն�ȷ��������ȷ������
        /// </summary>
        /// <param name="MoOrder"></param>
        /// <returns></returns>
        public int UpdateApplyConfirmQty(string applyCode,decimal cancelQty)
        {
            string strSql = "";
            if (this.Sql.GetSql("Terminal.TerminalConfirm.UpdateTerminalApplyConfirmQty", ref strSql) == -1)
            {
                this.Err = "��ȡ Terminal.TerminalConfirm.UpdateTerminalApplyConfirmQty ʧ��";

                return -1;
            }
            strSql = string.Format(strSql, applyCode, cancelQty);

            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        ///����ȷ����ˮ�� ���������ն�ȷ��������ȷ������
        /// </summary>
        /// <param name="MoOrder"></param>
        /// <returns></returns>
        public int UpdateApplyConfirmQtyCancel(string applyCode, decimal cancelQty)
        {
            string strSql = "";
            if (this.Sql.GetSql("Terminal.TerminalConfirm.UpdateTerminalApplyConfirmQtyCancel", ref strSql) == -1)
            {
                this.Err = "��ȡ Terminal.TerminalConfirm.UpdateTerminalApplyConfirmQtyCancel ʧ��";

                return -1;
            }
            strSql = string.Format(strSql, applyCode, cancelQty);

            return this.ExecNoQuery(strSql);
        }
        #endregion

        /// <summary>
        /// סԺ���ò����ն����뵥
        /// </summary>
        /// <param name="inpatientFee">סԺ����</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int Insert(FS.HISFC.Models.Fee.Inpatient.FeeItemList inpatientFee,
                          FS.HISFC.Models.Terminal.InpatientChargeType chargeType)
        {
            // �ն�ȷ��ʵ��
            FS.HISFC.Models.Terminal.TerminalApply terminalApply = new TerminalApply();

            // ת��סԺ����ʵ��Ϊ���ﻼ��ʵ��
            this.intReturn = this.ConvertToTerminalApply(inpatientFee, ref terminalApply, chargeType);
            if (this.intReturn == -1)
            {
                return -1;
            }

            // ִ�в���
            return this.Insert(terminalApply);
        }

        #region ���ӻ�������ά�����򻯷��� �������Ʋ���������HosCode�������������Ϻ���

        //
        // ά����������
        //

        //#region ɾ�������������뵥��������(1���ɹ���-1��ʧ��)
        ///// <summary>
        ///// ɾ�������������뵥��������(1���ɹ���-1��ʧ��)
        ///// </summary>
        ///// <returns>1���ɹ���-1��ʧ��</returns>
        //public int RemoveDaysLimited()
        //{
        //    //
        //    // ��ʼ���ֲ�����
        //    //
        //    this.Clear();
        //    //
        //    // ��ȡɾ��SQL���
        //    //
        //    if (this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.DeleteMaxPermitDays", ref this.SQL) == -1)
        //    {
        //        this.SetError("", "��ȡSQL���ʧ��");
        //        return -1;
        //    }
        //    if (this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.DeleteMaxPermitDays.Where", ref this.WHERE) == -1)
        //    {
        //        this.SetError("", "��ȡSQL���ʧ��");
        //        return -1;
        //    }
        //    //
        //    // �ϲ����
        //    //
        //    this.CreateSql();
        //    //
        //    // ִ��ɾ��SQL���
        //    //
        //    this.intReturn = this.ExecNoQuery(this.SQL);
        //    if (this.intReturn <= 0)
        //    {
        //        this.SetError("", "ִ��SQL���ʧ��");
        //        return -1;
        //    }
        //    //
        //    // ��ȡ����SQL���
        //    //
        //    if (this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.NoDaysLimited", ref this.SQL) == -1)
        //    {
        //        this.SetError("", "��ȡSQL���ʧ��");
        //        return -1;
        //    }
        //    //
        //    // ִ��SQL���
        //    //
        //    this.intReturn = this.ExecNoQuery(this.SQL);
        //    if (this.intReturn <= 0)
        //    {
        //        this.SetError("", "ִ��SQL���ʧ��");
        //        return -1;
        //    }
        //    //
        //    // �ɹ�����
        //    //
        //    return 1;
        //}
        //#endregion

        //#region ���������������뵥��������(1���ɹ���-1��ʧ��)
        ///// <summary>
        ///// ���������������뵥��������(1���ɹ���-1��ʧ��)
        ///// </summary>
        ///// <param name="days">��������</param>
        ///// <returns>1���ɹ���-1��ʧ��</returns>
        //public int UpdateDaysLimited(int days)
        //{
        //    //
        //    // ��ʼ������
        //    //
        //    this.Clear();
        //    //
        //    // ��ȡɾ��SQL���
        //    //
        //    if (this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.DeleteMaxPermitDays", ref this.SQL) == -1)
        //    {
        //        this.SetError("", "��ȡSQL���ʧ��");
        //        return -1;
        //    }
        //    //
        //    // ִ��SQL���
        //    //
        //    this.intReturn = this.ExecNoQuery(this.SQL);
        //    if (this.intReturn <= 0)
        //    {
        //        this.SetError("", "ִ��SQL���ʧ��");
        //        return -1;
        //    }
        //    //
        //    // ��ȡ����SQL���
        //    //
        //    if (this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.UpdateMaxPermitDays", ref this.SQL) == -1)
        //    {
        //        this.SetError("", "��ȡSQL���ʧ��");
        //        return -1;
        //    }
        //    //
        //    // ��ʽ��SQL���
        //    //
        //    try
        //    {
        //        this.SQL = string.Format(this.SQL, days);
        //    }
        //    catch (Exception e)
        //    {
        //        this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
        //        return -1;
        //    }
        //    //
        //    // ִ��SQL���
        //    //
        //    this.intReturn = this.ExecNoQuery(this.SQL);
        //    if (this.intReturn <= 0)
        //    {
        //        this.SetError("", "003ִ��SQL���ʧ��");
        //        return -1;
        //    }
        //    //
        //    // �ɹ�����
        //    //
        //    return 1;
        //}
        //#endregion

        #endregion

        //
        // ��ѯ��������
        //
        #region ��ȡϵͳ�����������뵥������������(����0����������������0��û���������ƣ�С��0����ȡʧ��)
        /// <summary>
        /// ��ȡϵͳ�����������뵥������������(����0����������������0��û���������ƣ�С��0����ȡʧ��)
        /// </summary>
        /// <returns>����0����������������0��û���������ƣ�С��0����ȡʧ��</returns>
        public int GetMaxPermitDays()
        {
            FS.FrameWork.Management.ControlParam controlerFunction = new FS.FrameWork.Management.ControlParam();
            int intDays = 0;

            if (this.Trans != null)
            {
                controlerFunction.SetTrans(this.Trans);
            }
            intDays = FS.FrameWork.Function.NConvert.ToInt32(controlerFunction.QueryControlerInfo("TC001"));
            if (intDays >= 0)
            {
                return intDays;
            }
            ////
            //// �ַ����͵�����
            ////
            //string stringDays = "";
            ////
            //// ��ʼ��SQL���
            ////
            //this.Clear();
            ////
            //// ��ȡSQL���
            ////
            //if (this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetMaxPermitDays", ref SQL) == -1)
            //{
            //    this.SetError("", "��ȡSQL���ʧ��");
            //    return -1;
            //}
            ////
            //// ִ��SQL���
            ////
            //this.intReturn = this.ExecQuery(this.SQL);
            //if (this.intReturn == -1)
            //{
            //    this.SetError("", "ִ��SQL���ʧ��");
            //    return -1;
            //}
            ////
            //// ������������
            ////
            //if (this.Reader.Read())
            //{
            //    stringDays = this.Reader[0].ToString();
            //    return FS.FrameWork.Function.NConvert.ToInt32(stringDays);
            //}
            //
            // ���û�ж�ȡ�ɹ�����ʧ��
            //
            this.SetError("", "û��������������");
            return -1;
        }
        #endregion
        #region �Զ���ȡ����������ƣ�������Ӧ������(1��ת���ɹ�/-1��ʧ��/0��û����������)
        /// <summary>
        /// �Զ��������ƣ�������Ӧ������(1��ת���ɹ�/-1��ʧ��/0��û����������)
        /// </summary>
        /// <param name="limitedDay">���ص�����</param>
        /// <returns>1��ת���ɹ�/-1��ʧ��/0��û����������</returns>
        public int JudgeDaysLimited(ref string limitedDay)
        {
            //
            //��������
            //
            // ���ڵ�ʱ��
            DateTime datetimeNow;
            // ���Ƶ�ʱ��
            DateTime dtLimited;
            // ��������
            System.TimeSpan timeSpan;
            // ��
            int year = 0;
            // ��
            int month = 0;
            // ��
            int day = 0;
            //
            // ��ȡ��������
            //
            this.intReturn = this.GetMaxPermitDays();
            //
            // �ж�����
            //
            // û����������
            if (intReturn == 0)
            {
                return 0;
            }
            // ��ȡʧ�ܣ�
            if (intReturn < 0)
            {
                return -1;
            }
            // 
            // ����ת�����ڣ����ؽ��
            //
            // ��ȡϵͳʱ��
            datetimeNow = this.GetDateTimeFromSysDateTime();
            // ����
            timeSpan = TimeSpan.FromDays(intReturn);
            year = (datetimeNow - timeSpan).Year;
            month = (datetimeNow - timeSpan).Month;
            day = (datetimeNow - timeSpan).Day;
            dtLimited = new DateTime(year, month, day, 0, 0, 0);
            limitedDay = dtLimited.ToString();
            //
            // �ɹ�����
            //
            return 1;
        }
        #endregion
        #region ����������ȡ����
        /// <summary>
        /// ����������ȡ����
        /// </summary>
        /// <param name="dayLimited">����</param>
        /// <returns>����</returns>
        public System.DateTime GetDateByDays(int dayLimited)
        {
            // ���ڵ�ʱ��
            DateTime datetimeNow;
            // ʱ����
            System.TimeSpan timeSpan;
            // ��
            int year = 0;
            // ��
            int month = 0;
            // ��
            int day = 0;
            //
            // ��ȡϵͳʱ��
            //
            datetimeNow = this.GetDateTimeFromSysDateTime();
            // ����
            timeSpan = TimeSpan.FromDays(dayLimited);
            year = (datetimeNow - timeSpan).Year;
            month = (datetimeNow - timeSpan).Month;
            day = (datetimeNow - timeSpan).Day;

            // ����ʱ��
            return new DateTime(year, month, day, 0, 0, 0);
        }
        #endregion
        //
        // ��ȡ����
        //
        #region ���ݻ��߲����Ż�ȡ���߻�����Ϣ��1���ɹ�/-1��ʧ�ܣ�
        /// <summary>
        /// ���ݻ��߲����Ż�ȡ���߻�����Ϣ��1���ɹ�/-1��ʧ�ܣ�
        /// </summary>
        /// <param name="cardNO">������/����</param>
        /// <param name="register">���صĻ�����Ϣ</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int GetPatientsByCardNO(string cardNO, string departmentID, ref ArrayList register)
        {
            //
            // ��ʼ������
            //
            this.Clear();
            //
            // ��ȡʱ������
            //
            if (this.GetLimited() == -1)
            {
                return -1;
            }
            //
            // ��ȡSQL���
            //
            this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetPatientByCardNO", ref SQL);
            if (SQL == null)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            // ��������������ҵĻ�����Ϣ
            if (departmentID.Equals(""))
            {
                intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetPatientByCardNO.Where.Other", ref WHERE);
            }
            else
            {
                intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetPatientByCardNO.Where", ref WHERE);
            }
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            this.CreateSql();
            // �����ʱ�����ƣ�����ʱ������WHERE����
            if (boolLimited)
            {
                // ��������������ҵĻ�����Ϣ
                if (departmentID.Equals(""))
                {
                    intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetPatientByCardNO.Where.Date.Other", ref WHERE);
                }
                else
                {
                    intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetPatientByCardNO.Where.Date", ref WHERE);
                }
                if (intReturn == -1)
                {
                    this.SetError("", "��ȡSQL���ʧ��");
                    return -1;
                }
                this.CreateSql();
            }
            //
            // SQL����ʽ��
            // 
            if (boolLimited) // �����ʱ������
            {
                try
                {
                    // ��������������ҵĻ�����Ϣ
                    if (departmentID.Equals(""))
                    {
                        SQL = string.Format(SQL, cardNO, this.stringLimitedDate);
                    }
                    else
                    {
                        SQL = string.Format(SQL, cardNO, departmentID, this.stringLimitedDate);
                    }
                }
                catch (Exception e)
                {
                    this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                    return -1;
                }
            } // �����ʱ������
            else
            {
                try
                {
                    if (departmentID.Equals(""))
                    {
                        SQL = string.Format(SQL, cardNO);
                    }
                    else
                    {
                        SQL = string.Format(SQL, cardNO, departmentID);
                    }
                }
                catch (Exception e)
                {
                    this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                    return -1;
                }
            }
            //
            // ִ��SQL���
            //
            intReturn = this.ExecQuery(SQL);
            if (intReturn == -1)
            {
                this.SetError("", "ִ��SQL���ʧ��" + this.Err);
                return -1;
            }
            // �������Ϊ�գ��򷵻�0
            if (this.Reader == null)
            {
                return 0;
            }
            //
            // �γɽ����
            //
            if (this.Reader.Read())
            {
                FS.HISFC.Models.Registration.Register patient = new Register();

                // ���߱��
                patient.ID = this.Reader[0].ToString();
                // ��������
                patient.Name = this.Reader[1].ToString();
                // ��ͬ��λ����
                patient.Pact.ID = this.Reader[2].ToString();
                // �Һſ���
                patient.DoctorInfo.Templet.Dept.ID = this.Reader[3].ToString();
                // �������ͣ�1-���2-סԺ��3-���4-���
                patient.Memo = this.Reader[4].ToString();
                // �Ա����
                patient.Sex.ID = this.Reader[5].ToString();
                //����
                patient.PID.CardNO = this.Reader[6].ToString();

                register.Add(patient);
                //
                // �ɹ�����
                //
                return 1;
            }
            return 1;
        }
        #endregion
        #region ���ݻ��߱�źͻ������ͻ�ȡ���߻�����Ϣ��1���ɹ�/-1��ʧ�ܣ�
        /// <summary>
        /// ���ݻ��߱�źͻ������ͻ�ȡ���߻�����Ϣ��1���ɹ�/-1��ʧ�ܣ�
        /// </summary>
        /// <param name="clinicCode">������/����</param>
        /// <param name="departmentID">���ұ���</param>
        /// <param name="patientType">�������</param>
        /// <param name="register">���صĹҺ���Ϣ</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int GetPatientsByClinicCode(string clinicCode, string patientType, string departmentID, ref Register register)
        {
            //
            // ��ʼ������
            //
            this.Clear();
            //
            // ��ȡʱ������
            //
            if (this.GetLimited() == -1)
            {
                return -1;
            }
            //
            // ��ȡSQL���
            //
            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetPatientsByClinicCode", ref SQL);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            if (departmentID.Equals(""))
            {
                intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetPatientsByClinicCode.Where.Other", ref WHERE);
            }
            else
            {
                intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetPatientsByClinicCode.Where", ref WHERE);
            }
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            this.CreateSql();
            // �����ʱ�����ƣ�����ʱ������WHERE����
            if (boolLimited)
            {
                if (departmentID.Equals(""))
                {
                    intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetPatientsByClinicCode.Where.Date.Other", ref WHERE);
                }
                else
                {
                    intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetPatientsByClinicCode.Where.Date", ref WHERE);
                }
                if (intReturn == -1)
                {
                    this.SetError("", "��ȡSQL���ʧ��");
                    return -1;
                }
                this.CreateSql();
            }
            //
            // SQL����ʽ��
            // 
            if (boolLimited) // �����ʱ������
            {
                try
                {
                    if (departmentID.Equals(""))
                    {
                        SQL = string.Format(SQL, clinicCode, patientType, this.stringLimitedDate);
                    }
                    else
                    {
                        SQL = string.Format(SQL, clinicCode, patientType, departmentID, this.stringLimitedDate);
                    }
                }
                catch (Exception e)
                {
                    this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                    return -1;
                }
            } // �����ʱ������
            else
            {
                try
                {
                    if (departmentID.Equals(""))
                    {
                        SQL = string.Format(SQL, clinicCode, patientType);
                    }
                    else
                    {
                        SQL = string.Format(SQL, clinicCode, patientType, departmentID);
                    }
                }
                catch (Exception e)
                {
                    this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                    return -1;
                }
            }
            //
            // ִ��SQL���
            //
            intReturn = this.ExecQuery(SQL);
            if (intReturn == -1)
            {
                this.SetError("", "ִ��SQL���ʧ��" + this.Err);
                return -1;
            }
            // �������Ϊ�գ��򷵻�0
            if (this.Reader == null)
            {
                return 0;
            }
            //
            // �γɽ����
            //
            if (this.Reader.Read())
            {
                // ���߱��
                register.ID = this.Reader[0].ToString();
                // ��������
                register.Name = this.Reader[1].ToString();
                // ��ͬ��λ����
                register.Pact.ID = this.Reader[2].ToString();
                // �Һſ���
                register.DoctorInfo.Templet.Dept.ID = this.Reader[3].ToString();
                // �������ͣ�1-���2-סԺ��3-���4-���
                register.Memo = this.Reader[4].ToString();
                // �Ա����
                register.Sex.ID = this.Reader[5].ToString();
                //����
                register.PID.CardNO = this.Reader[6].ToString();
                //
                // �ɹ�����
                //
                return 1;
            }
            //
            // ���󷵻�
            //
            return -1;
        }
        #endregion
        #region ���ݻ�������Ż�ȡ���߹Һ�����(1���ɹ�/-1��ʧ��)
        /// <summary>
        /// ���ݻ�������Ż�ȡ���߹Һ�����(1���ɹ�/-1��ʧ��)
        /// </summary>
        /// <param name="clinicNO">�����</param>
        /// <param name="regDate">�Һ�����</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int GetRegDate(string clinicNO, ref DateTime regDate)
        {
            //
            // ��ʼ������
            //
            this.Clear();
            //
            // ��ȡSQL���
            //
            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetRegDate.Select", ref SQL);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }

            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetRegDate.Where", ref WHERE);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                ;
                return -1;
            }
            // �ϲ�û��ʱ�����Ƶ�SQL���
            SQL = SQL + " " + WHERE;

            // ��ʽ��SQL���
            try
            {
                SQL = string.Format(SQL, clinicNO);
            }
            catch (Exception e)
            {
                this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                return -1;
            }
            // ִ��SQL���
            intReturn = this.ExecQuery(SQL);
            if (intReturn == -1)
            {
                this.SetError("", "SQL���ִ��ʧ��");
                return -1;
            }
            // �γɽ��
            this.Reader.Read();
            regDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[0].ToString());
            //
            // �ɹ�����
            //
            return 1;
        }
        #endregion
        #region ���ݿ��ұ��룬�������еĻ��߻�����Ϣ(1���ɹ���-1��ʧ�ܣ�0�����ؿ�)
        /// <summary>
        /// ���ݿ��ұ��룬�������еĻ��߻�����Ϣ(1���ɹ���-1��ʧ�ܣ�0�����ؿ�)
        /// </summary>
        /// <param name="patientList">TerminalApplyʵ����</param>
        /// <param name="deptCode">ִ�п��ұ���</param>
        /// <param name="dayLimited">�����б���������</param>
        /// <returns>1���ɹ���-1��ʧ�ܣ�0�����ؿ�</returns>
        public int QueryPatients(ref ArrayList patientList, string deptCode, int dayLimited)
        {
            //
            // ��ʼ������
            //
            String stringDayLimited = "";
            string sqlGroup = "";

            this.Clear();
            //
            // ��ȡʱ������
            //
            if (this.GetLimited() == -1)
            {
                return -1;
            }
            //
            // ��ȡSQL���
            //
            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetPatientsByDepartmentCode", ref SQL);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            // ��ȡORDER���
            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetPatientsByDepartmentCode.Order", ref ORDER);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            // ������������ƣ�����ʱ������WHERE����
            if (dayLimited > 0)
            {
                intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetPatientsByDepartmentCode.Where", ref WHERE);
                if (intReturn == -1)
                {
                    this.SetError("", "��ȡSQL���ʧ��");
                    return -1;
                }
            }
            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetPatientsByDepartmentCode.Group", ref sqlGroup);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }

            this.SQL = this.SQL + " " + this.WHERE + " " + sqlGroup + " " + this.ORDER;
            //
            // SQL����ʽ��
            // 
            if (dayLimited > 0) // �����ʱ������
            {
                try
                {
                    stringDayLimited = this.GetDateByDays(dayLimited).ToString();
                    SQL = string.Format(SQL, deptCode, stringDayLimited);
                }
                catch (Exception e)
                {
                    this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                    return -1;
                }
            }
            else
            {
                try
                {
                    SQL = string.Format(SQL, deptCode);
                }
                catch (Exception e)
                {
                    this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                    return -1;
                }
            }
            //
            // ִ��SQL���
            //
            intReturn = this.ExecQuery(SQL);
            if (intReturn == -1)
            {
                this.SetError("", "ִ��SQL���ʧ��" + this.Err);
                return -1;
            }
            // �������Ϊ�գ��򷵻�0
            if (this.Reader == null)
            {
                return 0;
            }
            //
            // �γɽ����
            //
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Registration.Register patient = new Register();
                //// ���߱��
                patient.ID = this.Reader[0].ToString();
                // ��������
                patient.Name = this.Reader[1].ToString();
                // ��ͬ��λ����
                patient.Pact.ID = this.Reader[2].ToString();
                // �Һſ���
                patient.DoctorInfo.Templet.Dept.ID = this.Reader[3].ToString();
                // �������ͣ�1-���2-סԺ��3-���4-���
                patient.Memo = this.Reader[4].ToString();
                // �Ա����
                patient.Sex.ID = this.Reader[5].ToString();
                //����
                patient.PID.CardNO = this.Reader[6].ToString();

                patientList.Add(patient);
            }
            return 1;
        }
        #endregion
        /// <summary>
        /// ����ִ�п��Ҳ�ѯ��Ҫȷ����Ŀ�Ļ��ߵ����ڿ���
        /// </summary>
        /// <param name="applyList"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public int QueryPatientDeptByConfirmDeptID(ref ArrayList applyList, string deptCode)
        {
            string strSQL = "";

            if (this.Sql.GetSql("Terminal.TerminalConfirm.QueryPatientDept.NeedConfirm.1", ref strSQL) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, deptCode);
            }
            catch
            {
                this.Err = "����������ԣ�Terminal.TerminalConfirm.QueryPatientDept.NeedConfirm.1";
                return -1;
            }

            if (this.ExecQuery(strSQL) == -1)
            {
                return -1;
            }

            try
            {
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

                    obj.ID = this.Reader[0].ToString();

                    applyList.Add(obj);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ѯ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return -1;
            }
            finally
            {
                this.Reader.Close();
            }

            return 1;
        }

        /// <summary>
        /// ����סԺ��ˮ�ź�ִ�п���  ������Ҫȷ�ϵ���Ŀ��Ϣ
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public ArrayList QueryItemListNeedConfirmByDeptCode(string inpatientNO, string deptCode)
        {
            ArrayList alItemList = new ArrayList();
            string strSQL = "";

            if (this.Sql.GetSql("Terminal.TerminalConfirm.QueryItemList.NeedConfirm.1", ref strSQL) == -1)
            {
                this.Err = this.Sql.Err;

                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, inpatientNO, deptCode);
            }
            catch
            {
                this.Err = "����������ԣ�Terminal.TerminalConfirm.QueryItemList.NeedConfirm.1";

                return null;
            }

            if (this.ExecQuery(strSQL) == -1)
            {
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();

                    feeItemList.Item.ID = this.Reader[0].ToString();
                    feeItemList.Item.Name = this.Reader[1].ToString();
                    feeItemList.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2].ToString());
                    feeItemList.ConfirmedQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3].ToString());
                    feeItemList.NoBackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4].ToString());
                    feeItemList.Item.PriceUnit = this.Reader[5].ToString();
                    feeItemList.Item.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6].ToString());
                    feeItemList.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7].ToString());
                    feeItemList.Order.ID = this.Reader[8].ToString();
                    feeItemList.ExecOrder.ID = this.Reader[9].ToString();
                    feeItemList.RecipeNO = this.Reader[10].ToString();
                    feeItemList.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[11].ToString());
                    feeItemList.TransType = (TransTypes)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[12].ToString());

                    alItemList.Add(feeItemList);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ѯ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();

                return null;
            }
            finally 
            {
                this.Reader.Close(); 
            }

            return alItemList;
        }

        /// <summary>
        /// ����ִ�п��ҡ��������ڿ��Ҳ�ѯ��Ҫȷ����Ŀ�Ļ���
        /// </summary>
        /// <param name="confirmDept">ִ�п���</param>
        /// <param name="patientDept">�������ڿ���</param>
        /// <returns></returns>
        public ArrayList QueryPatientByConfirmDeptAndPatDept(string confirmDept, string patientDept)
        {
            ArrayList alPatient = new ArrayList();
            string strSQL = "";

            if (this.Sql.GetSql("Terminal.TerminalConfirm.QueryInpatientNO.NeedConfirm.1", ref strSQL) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, confirmDept, patientDept);
            }
            catch
            {
                this.Err = "����������ԣ�Terminal.TerminalConfirm.QueryInpatientNO.NeedConfirm.1";
                return null;
            }

            if (this.ExecQuery(strSQL) == -1)
            {
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

                    obj.ID = this.Reader[0].ToString();

                    alPatient.Add(obj);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ѯ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return alPatient;
        }
        //
        // ��ȡ���뵥
        //
        #region ���ݻ��߱�Ż�ȡ�ȴ�ȷ�ϵ����뵥��ϸ��Ϣ(1���ɹ���-1��ʧ�ܣ�0����)
        /// <summary>
        /// ���ݻ��߱�Ż�ȡ�ȴ�ȷ�ϵ����뵥��ϸ��Ϣ(1���ɹ���-1��ʧ�ܣ�0����)
        /// </summary>
        /// <param name="queryCode">���߱��</param>
        /// <param name="applyList">���ص����뵥��ϸ</param>
        /// <param name="executeDepartment">ִ�п��ұ���</param>
        /// <returns>1���ɹ���-1��ʧ�ܣ�0����</returns>
        public int QueryTerminalApplyList(string queryCode, ref ArrayList applyList, string executeDepartment)
        {
            //
            // ��ʼ������
            //
            this.Clear();
            //
            // ��ȡʱ������
            //
            if (this.GetLimited() == -1) // ��ȡʱ������ʧ��
            {
                return -1;
            }
            //
            // ��ȡSQL���
            //
            string strSql = this.GetTerminalSql();
            if (strSql == null)
            {
                return -1;
            }
            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetTerminalApplyList", ref SQL);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            SQL = strSql + SQL;
            // �����ʱ�����ƣ�����ʱ������WHERE����
            if (boolLimited)
            {
                intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetTerminalApplyList.Where", ref WHERE);
                if (intReturn == -1)
                {
                    this.SetError("", "��ȡSQL���ʧ��");
                    return -1;
                }
                this.CreateSql();
            }
            //
            // ��ʽ��SQL���
            //
            if (boolLimited) // �����ʱ�����ƣ�����ʱ���ʽ������
            {
                try
                {
                    SQL = string.Format(SQL, executeDepartment, queryCode, this.stringLimitedDate);
                }
                catch (Exception e)
                {
                    this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                    return -1;
                }
            } // �����ʱ�����ƣ�����ʱ���ʽ������
            else
            {
                try
                {
                    SQL = string.Format(SQL, executeDepartment, queryCode);
                }
                catch (Exception e)
                {
                    this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                    return -1;
                }
            }
            //
            // ִ��SQL���
            //
            intReturn = this.ExecQuery(SQL);
            if (intReturn == -1)
            {
                this.SetError("", "SQL���ִ��ʧ��");
                return -1;
            }
            //
            // �洢����ֵ
            //
            if (this.Reader == null)
            {
                return 0;
            }
            this.FillApply(ref applyList);

            return 1;
        }
        #endregion
        #region �������ﻼ�߱�źͻ�������ȡ�ȴ�ȷ�ϵ����뵥��ϸ��Ϣ(1���ɹ���-1��ʧ�ܣ�0����)
        /// <summary>
        /// �������ﻼ�߱�źͻ�������ȡ�ȴ�ȷ�ϵ����뵥��ϸ��Ϣ(1���ɹ���-1��ʧ�ܣ�0����)
        /// </summary>
        /// <param name="queryCode">���߱���</param>
        /// <param name="patientType">�������</param>
        /// <param name="applyList">���ص����뵥��ϸ</param>
        /// <param name="executeDepartment">ִ�п��ұ���</param>
        /// <returns>1���ɹ���-1��ʧ�ܣ�0����</returns>
        public int QueryTerminalApplyList(string queryCode, ref ArrayList applyList, string executeDepartment, string patientType)
        {
            //
            // ��ʼ������
            //
            this.Clear();
            //
            // ��ȡʱ������
            //
            if (this.GetLimited() == -1)
            {
                return -1;
            }
            //
            // ��ȡSQL���
            //
            string strSql = this.GetTerminalSql();
            if (strSql == null)
            {
                return -1;
            }
            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetOutpatientTerminalApplyList", ref SQL);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            SQL = strSql + SQL;
            // �����ʱ�����ƣ�����ʱ������WHERE���
            if (boolLimited)
            {
                intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetTerminalApplyList.Where", ref WHERE);
                if (intReturn == -1)
                {
                    this.SetError("", "��ȡSQL���ʧ��");
                    ;
                    return -1;
                }
                SQL = SQL + " " + WHERE;
            }

            //
            // ��ʽ��SQL���
            //
            if (boolLimited) // �����ʱ�����ƣ�����ʱ�����
            {
                try
                {
                    SQL = string.Format(SQL, executeDepartment, queryCode, patientType, this.stringLimitedDate);
                }
                catch (Exception e)
                {
                    this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                    return -1;
                }
            } // �����ʱ�����ƣ�����ʱ�����
            else
            {
                try
                {
                    SQL = string.Format(SQL, executeDepartment, queryCode, patientType);
                }
                catch (Exception e)
                {
                    this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                    return -1;
                }
            }
            //
            // ִ��SQL���
            //
            intReturn = this.ExecQuery(SQL);
            if (intReturn == -1)
            {
                this.SetError("", "SQL���ִ��ʧ��");
                return -1;
            }
            //
            // �γɽ��
            //
            if (this.Reader == null)
            {
                return 0;
            }
            this.FillApply(ref applyList);
            //
            // �ɹ�����
            //
            return 1;
        }
        #endregion
        #region �������뵥��ˮ�Ż�ȡ�Ѿ�ȷ������(1���ɹ�/-1��ʧ��/0����)
        /// <summary>
        /// �������뵥��ˮ�Ż�ȡ�Ѿ�ȷ������(1���ɹ�/-1��ʧ��/0����)
        /// </summary>
        /// <param name="applyNumber">���뵥��ˮ��</param>
        /// <param name="alreadyCount">���ص��Ѿ�ȷ������</param>
        /// <returns>1���ɹ�/-1��ʧ��/0����</returns>
        public int GetAlreadyCount(string applyNumber, ref decimal alreadyCount)
        {
            //
            // ��ʼ������
            //
            this.Clear();
            //
            // ��ȡSQL���
            //
            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetAlreadyCount", ref SQL);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetAlreadyCount", ref WHERE);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            // �ϲ�SQL��WHERE
            SQL = SQL + WHERE;
            //
            // ��ʽ��SQL���
            // 
            try
            {
                SQL = string.Format(SQL, applyNumber);
            }
            catch (Exception e)
            {
                this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                return -1;
            }
            //
            // ִ��SQL���
            //
            intReturn = this.ExecQuery(SQL);
            if (intReturn == -1)
            {
                this.SetError("", "ִ��SQL���ʧ��");
                return -1;
            }
            //
            // ���ؽ��
            //
            if (this.Reader.Read())
            {
                alreadyCount = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);
            }
            else
            {
                return 0;
            }
            //
            // �ɹ�����
            //
            return 1;
        }
        #endregion
        #region ���ݲ����Ż�ȡ���뵥����
        /// <summary>
        /// ���ݲ����Ż�ȡ���뵥����
        /// [����1: string cardNO - ������]
        /// [����2: ArrayList applyList - ���뵥����]
        /// [����: int,1-�ɹ�,-1-ʧ��]
        /// </summary>
        /// <param name="cardNO">������</param>
        /// <param name="applyList">���ص���ϸ</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int QueryApplyListByCardNO(string cardNO, ref ArrayList applyList)
        {
            //
            // ��ʼ������
            //
            this.Clear();
            //
            // ��ȡSQL���
            //
            SQL = this.GetTerminalSql();
            if (SQL == null)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            intReturn = this.Sql.GetSql("Terminal.TerminalValidate.GetApplyListByCardNO.Where.Nodepartment", ref WHERE);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            // �ϲ�û��ʱ�����Ƶ�SQL���
            SQL = SQL + " " + WHERE;
            //
            // ��ʽ��SQL���
            //
            try
            {
                SQL = string.Format(SQL, cardNO);
            }
            catch (Exception e)
            {
                this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                return -1;
            }
            //
            // ִ��SQL���
            //
            intReturn = this.ExecQuery(SQL);
            if (intReturn == -1)
            {
                this.SetError("", "SQL���ִ��ʧ��");
                return -1;
            }
            //
            // �γɽ��
            //
            if (this.Reader == null)
            {
                return 0;
            }
            this.FillApply(ref applyList);
            //
            // �ɹ�����
            //
            return 1;
        }
        #endregion
        /// <summary>
        /// ����סԺ�Ų����ն�ȷ����ϸ
        /// </summary>
        /// <param name="inpatientNO">סԺ��</param>
        /// <param name="operDept">����Ա���ڿ���</param>
        /// <param name="applyList">���ص���ϸ</param>
        /// <returns></returns>
        public int QueryTerminalConfirmList(string inpatientNO,string  operDept, ref ArrayList applyList)
        {
            // SQL���
            string sql = "";
            //
            // ��ȡSQL���
            //
            if (this.Sql.GetSql("Terminal.TerminalConfirm.QueryConfirmInfoByInpatientNO", ref sql) == -1)
            {
                this.Err = "��ȡSQL���Terminal.TerminalConfirm.QueryConfirmInfoByInpatientNOʧ��";
                return -1;
            }
            // ƥ��ִ��SQL���
            try
            {
                //sql = string.Format(sql, GetParam(medTechItem));

                this.ExecQuery(sql, inpatientNO, operDept);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }

            this.FillInpatientConfirmDetail(applyList);

            return 1;
        }
        /// <summary>
        /// ����סԺ�Ų����ն�ȷ����ϸ
        /// </summary>
        /// <param name="inpatientNO">סԺ��</param>
        /// <param name="operDept">����Ա���ڿ���</param>
        /// <param name="applyList">���ص���ϸ</param>
        /// <returns></returns>
        public int QueryTerminalConfirmListCancel1(string inpatientNO, string operDept, ref ArrayList applyList)
        {
            // SQL���
            string sql = "";
            //
            // ��ȡSQL���
            //
            if (this.Sql.GetSql("Terminal.TerminalConfirm.QueryConfirmInfoByInpatientNOCancel1", ref sql) == -1)
            {
                this.Err = "��ȡSQL���Terminal.TerminalConfirm.QueryConfirmInfoByInpatientNOCancel1ʧ��";
                return -1;
            }
            // ƥ��ִ��SQL���
            try
            {
                //sql = string.Format(sql, GetParam(medTechItem));

                this.ExecQuery(sql, inpatientNO, operDept);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }

            this.FillInpatientConfirmDetail(applyList);

            return 1;
        }
        /// <summary>
        /// ����סԺ�Ų����ն�ȷ����ϸ
        /// </summary>
        /// <param name="inpatientNO">סԺ��</param>
        /// <param name="operDept">����Ա���ڿ���</param>
        /// <param name="applyList">���ص���ϸ</param>
        /// <returns></returns>
        public int QueryTerminalConfirmListCancel(string inpatientNO, string operDept, ref ArrayList applyList)
        {
            // SQL���
            string sql = "";
            //
            // ��ȡSQL���
            //
            if (this.Sql.GetSql("Terminal.TerminalConfirm.QueryConfirmInfoByInpatientNOCancel", ref sql) == -1)
            {
                this.Err = "��ȡSQL���Terminal.TerminalConfirm.QueryConfirmInfoByInpatientNOCancelʧ��";
                return -1;
            }
            // ƥ��ִ��SQL���
            try
            {
                //sql = string.Format(sql, GetParam(medTechItem));

                this.ExecQuery(sql, inpatientNO, operDept);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }

            this.FillInpatientConfirmDetail(applyList);

            return 1;
        }
        /// <summary>
        /// ���ݲ�ѯ�����arraylist��ֵ
        /// </summary>
        /// <param name="applyList"></param>
        /// <returns></returns>
        private void FillInpatientConfirmDetail(ArrayList applyList)
        {
            // ������ض���
            FS.HISFC.Models.Terminal.TerminalConfirmDetail confirmDetail;

            while (this.Reader.Read()) // ѭ����ȡ�̳е�Reader����
            {
                confirmDetail = new TerminalConfirmDetail();
                confirmDetail.MoOrder = this.Reader[0].ToString();
                confirmDetail.ExecMoOrder = this.Reader[1].ToString();
                confirmDetail.Sequence = this.Reader[2].ToString();
                confirmDetail.Apply.Item.ID = this.Reader[3].ToString();
                confirmDetail.Apply.Item.Name = this.Reader[4].ToString();
                confirmDetail.Apply.Item.ConfirmedQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5].ToString());
                confirmDetail.Apply.Item.ConfirmOper.ID = this.Reader[6].ToString();
                confirmDetail.Apply.ConfirmOperEnvironment.Dept.ID = this.Reader[7].ToString();
                confirmDetail.Apply.ConfirmOperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8].ToString());
                confirmDetail.Apply.Item.RecipeNO = this.Reader[9].ToString();
                confirmDetail.Apply.Item.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[10].ToString());
                confirmDetail.ExecDevice = this.Reader[11].ToString();
                confirmDetail.Oper.ID = this.Reader[12].ToString();
                if (this.Reader.FieldCount > 13)
                {
                    confirmDetail.Status.ID = this.Reader[13].ToString();
                }
                //confirmDetail.CancelInfo.ID  = this.Reader[6].ToString();
                //confirmDetail.CancelInfo.ID = this.Reader[13].ToString();
                //confirmDetail.CancelInfo.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[14].ToString());
           
                applyList.Add(confirmDetail);
            }
        }


        #region ���ݲ����Ż�ȡ���ߵ����뵥��ϸ��1���ɹ�/-1��ʧ�ܣ�
        /// <summary>
        /// ���ݲ����Ż�ȡ���ߵ����뵥��ϸ��1���ɹ�/-1��ʧ�ܣ�
        /// </summary>
        /// <param name="cardNO">������</param>
        /// <param name="applyList">���ص���ϸ</param>
        /// <param name="currentDepartment">��ǰ���ұ���</param>
        /// <param name="IsExam">�ǲ��Ǽ������</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int QueryApplyListByCardNO(string cardNO, ref ArrayList applyList, string currentDepartment, bool IsExam)
        {
            //
            // ��ʼ������
            //
            this.Clear();
            //
            // ��ȡʱ������
            //
            if (this.GetLimited() == -1)
            {
                return -1;
            }
            //
            // ��ȡSQL���
            //
            SQL = this.GetTerminalSql();
            if (SQL == null)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            if (currentDepartment.Equals(""))
            {
                if (boolLimited)
                {
                    intReturn = this.Sql.GetSql("Terminal.TerminalConfirm.GetApplyListByCardNO.Where.1.Other1", ref WHERE);
                }
                else
                {
                    intReturn = this.Sql.GetSql("Terminal.TerminalConfirm.GetApplyListByCardNO.Where.1.Other", ref WHERE);
                }               
            }
            else
            {
                if (boolLimited)
                {
                    intReturn = this.Sql.GetSql("Terminal.TerminalConfirm.GetApplyListByCardNO.Where.11", ref WHERE);
                }
                else
                {
                    intReturn = this.Sql.GetSql("Terminal.TerminalConfirm.GetApplyListByCardNO.Where.1", ref WHERE);
                }
            }       
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            // �ϲ�û��ʱ�����Ƶ�SQL���
            SQL = SQL + " " + WHERE;

            #region ���޸�com_sql��Ҫ���������´��롣 ��by lichao2007-4-28
            /*
            string IsFeeWhere = "";
            if (!IsExam)
            {
                intReturn = this.Sql.GetSql("TerminalValidate.GetApplyListByCardNO.Where.IsFeeWhere", ref IsFeeWhere);
            }            
           
            // �����ʱ�����ƣ�����ʱ������WHERE�������
            if (boolLimited)
            {
                intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetApplyListByCardNO.Where.2", ref WHERE);
                if (intReturn == -1)
                {
                    this.SetError("", "��ȡSQL���ʧ��");
                    return -1;
                }
                SQL = SQL + " " + WHERE;
            }
            */
            #endregion
            //
            // ��ʽ��SQL���
            //
            if (boolLimited) // �����ʱ�����ƣ�����ʱ�����
            {
                try
                {
                    SQL = string.Format(SQL, cardNO, currentDepartment, this.stringLimitedDate);
                }
                catch (Exception e)
                {
                    this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                    return -1;
                }
            }
            else
            {
                try
                {
                    SQL = string.Format(SQL, cardNO, currentDepartment);
                }
                catch (Exception e)
                {
                    this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                    return -1;
                }
            }
            //
            // ִ��SQL���
            //
            intReturn = this.ExecQuery(SQL);
            if (intReturn == -1)
            {
                this.SetError("", "SQL���ִ��ʧ��");
                return -1;
            }
            //
            // �γɽ��
            //
            if (this.Reader == null)
            {
                return 0;
            }
            this.FillApply(ref applyList);
            //
            // �ɹ�����
            //
            return 1;
        }
        #endregion
        #region ����ҽ�����ж����뵥�Ƿ���ڣ������Ѿ�ȷ�ϱ�־(1������/0��������/-1��ʧ��)
        /// <summary>
        /// ����ҽ�����ж����뵥�Ƿ���ڣ������Ѿ�ȷ�ϱ�־(1������/0��������/-1��ʧ��)
        /// </summary>
        /// <param name="orderCode">ҽ����ˮ��</param>
        /// <param name="sendFlag">ҽ��ִ��״̬��'2'�Ѿ�ִ��</param>
        /// <returns>1������/0��������/-1��ʧ��</returns>
        public int JudgeIfConfirm(string orderCode, ref string sendFlag)
        {
            //
            // ��ʼ������
            //
            this.Clear();
            //
            // ��ȡSQL���
            //
            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.JudgeIfConfirm.Select", ref SQL);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }

            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.JudgeIfConfirm.Where", ref WHERE);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            // �ϲ�û��ʱ�����Ƶ�SQL���
            this.CreateSql();
            // ��ʽ��SQL���
            try
            {
                SQL = string.Format(SQL, orderCode);
            }
            catch (Exception e)
            {
                this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                return -1;
            }

            // ִ��SQL���
            intReturn = this.ExecQuery(SQL);
            if (intReturn == -1)
            {
                this.SetError("", "ִ��SQL���ʧ��");
                return -1;
            }
            // �жϻ�ȡ���
            if (this.Reader == null)
            {
                return 0;
            }
            else if (this.Reader.Read())
            {
                // ����м�¼
                sendFlag = this.Reader[0].ToString();
                return 1;
            }
            else
            {
                // ������
                return 0;
            }
        }

        /// <summary>
        /// ����ҽ�����ж����뵥�Ƿ���ڣ������Ѿ�ȷ�ϱ�־(1������/0��������/-1��ʧ��)
        /// </summary>
        /// <param name="applyID">���뵥����</param>
        /// <param name="sendFlag">ҽ��ִ��״̬��'2'�Ѿ�ִ��</param>
        /// <returns>1������/0��������/-1��ʧ��</returns>
        public int JudgeIfConfirmByID(string applyID, ref string sendFlag)
        {
            //
            // ��ʼ������
            //
            this.Clear();
            //
            // ��ȡSQL���
            //
            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.JudgeIfConfirm.Select", ref SQL);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }

            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.JudgeIfConfirm.Where.ID", ref WHERE);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            // �ϲ�û��ʱ�����Ƶ�SQL���
            this.CreateSql();
            // ��ʽ��SQL���
            try
            {
                if (applyID == "" || applyID == null)
                {
                    applyID = "0";
                }
                SQL = string.Format(SQL, applyID);
            }
            catch (Exception e)
            {
                this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                return -1;
            }

            // ִ��SQL���
            intReturn = this.ExecQuery(SQL);
            if (intReturn == -1)
            {
                this.SetError("", "ִ��SQL���ʧ��");
                return -1;
            }
            // �жϻ�ȡ���
            if (this.Reader == null)
            {
                return 0;
            }
            else if (this.Reader.Read())
            {
                // ����м�¼
                sendFlag = this.Reader[0].ToString();
                return 1;
            }
            else
            {
                // ������
                return 0;
            }
        }
        #endregion
        #region ����ҽ���Ż�ȡ���뵥��
        /// <summary>
        /// ����ҽ���Ż�ȡ���뵥��
        /// </summary>
        /// <param name="orderNO">ҽ����</param>
        /// <param name="applyNO">���뵥��</param>
        /// <returns>1���ɹ�����1��ʧ��</returns>
        public int GetApplyNoByOrderNo(string orderNO, ref string applyNO)
        {
            //
            // ��ʼ������
            //
            this.Clear();
            //
            // ��ȡSQL���
            //
            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetApplyNoByOrderNo.Select", ref SQL);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }

            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetApplyNoByOrderNo.Where", ref WHERE);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            // �ϲ�û��ʱ�����Ƶ�SQL���
            this.CreateSql();
            // ��ʽ��SQL���
            try
            {
                SQL = string.Format(SQL, orderNO);
            }
            catch (Exception e)
            {
                this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                return -1;
            }
            // ִ��SQL���
            intReturn = this.ExecQuery(SQL);
            if (intReturn == -1)
            {
                this.SetError("", "ִ��SQL���ʧ��");
                return -1;
            }
            // �жϻ�ȡ���
            if (this.Reader == null)
            {
                return -1;
            }
            else if (this.Reader.Read())
            {
                // ����м�¼
                applyNO = this.Reader[0].ToString();
                return 1;
            }
            else
            {
                // ʧ��
                return -1;
            }
        }
        #endregion
        #region ����ҽ�����ж��Ƿ����ԤԼ��¼
        /// <summary>
        /// ����ҽ���Ż�ȡ���뵥��
        /// </summary>
        /// <param name="orderNO">ҽ����</param>
        /// <param name="applyNO">���뵥��</param>
        /// <returns>1���ɹ�����1��ʧ��</returns>
        public int GetTerminalApply(string moOrder)
        {
            string applyNO = string.Empty;
            //
            // ��ȡSQL���
            //
            intReturn = this.Sql.GetSql("Terminal.TerminalConfirm.GetTerminalApply", ref SQL);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }

            try
            {
                SQL = string.Format(SQL, moOrder);
            }
            catch (Exception e)
            {
                this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                return -1;
            }
            // ִ��SQL���
            intReturn = this.ExecQuery(SQL);
            if (intReturn == -1)
            {
                this.SetError("", "ִ��SQL���ʧ��");
                return -1;
            }
            // �жϻ�ȡ���
            if (this.Reader == null)
            {
                return -1;
            }
            else if (this.Reader.Read())
            {
                // ����м�¼
                applyNO = this.Reader[0].ToString();
                return -1;
            }
            else
            {
                // �޼�¼
                return 1;
            }
        }
        #endregion

        /// <summary>
        /// ����ҽ���Ż�ȡ�ն�ȷ��������Ϣ
        /// </summary>
        /// <param name="SequenceNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Terminal.TerminalApply GetItemListBySequence(string MoOrder, string ApplyNum)
        {
            string strSQL = "";
            string strSql2 = GetTerminalSql();
            //ȡSELECT���
            if (this.Sql.GetSql("FS.HISFC.Object.Terminal.TerminalApply", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�FS.HISFC.Object.Terminal.TerminalApply�ֶ�!";
                return null;
            }
            strSQL = string.Format(strSQL, MoOrder, ApplyNum);
            strSQL = strSql2 + strSQL;
            ArrayList list = GetTerminalList(strSQL); //�����Ŀ��Ϣʵ��
            if (list == null)
            {
                return null;
            }
            if (list.Count == 0)
            {
                return new FS.HISFC.Models.Terminal.TerminalApply();
            }
            return (FS.HISFC.Models.Terminal.TerminalApply)list[0];
        }

      


        //
        // ����
        //
        #region ����ִ�п��ұ��롢ʱ�䷶Χ��ȡ������ϸ(1���ɹ�/-1��ʧ��)
        /// <summary>
        /// ����ִ�п��ұ��롢ʱ�䷶Χ��ȡ������ϸ(1���ɹ�/-1��ʧ��)
        /// </summary>
        /// <param name="departmentCode">����Ŀ��ұ���</param>
        /// <param name="operatorCode">����Ĳ���Ա����</param>
        /// <param name="dateFrom">�������ʼʱ��</param>
        /// <param name="dateTo">����Ľ�ֹʱ��</param>
        /// <param name="dsResult">���صĲ�ѯ���</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int QueryFeeItemList(string departmentCode, string operatorCode, string dateFrom, string dateTo, ref System.Data.DataSet dsResult)
        {
            //
            // ��ʼ������
            //
            this.Clear();
            //
            // ��ȡSQL���
            //
            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetFeeItemList.Select", ref SQL);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }

            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetFeeItemList.Where", ref WHERE);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            // �ϲ�û��ʱ�����Ƶ�SQL���
            SQL = SQL + " " + WHERE;

            // ��ʽ��SQL���
            try
            {
                SQL = string.Format(SQL, departmentCode, operatorCode, dateFrom, dateTo);
            }
            catch (Exception e)
            {
                this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                return -1;
            }
            // ִ��SQL���
            intReturn = this.ExecQuery(SQL, ref dsResult);
            if (intReturn == -1)
            {
                this.SetError("", "SQL���ִ��ʧ��");
                return -1;
            }
            //
            // �ɹ�����
            //
            return 1;
        }
        #endregion
        #region ����ҽ���Ż�ȡ��Ʊ��ϸ��Ϣ
        /// <summary>
        /// ����ҽ���Ż�ȡ��Ʊ��ϸ��Ϣ
        /// [����1: string orderCode - ҽ����]
        /// [����2: System.Data.DataSet dsResult - ��ѯ���]
        /// [����: int,1-�ɹ�,-1-ʧ��]
        /// </summary>
        /// <param name="orderCode">ҽ����</param>
        /// <param name="dsResult">��ѯ���</param>
        /// <returns>1-�ɹ�,-1-ʧ��</returns>
        public int QueryInvoiceDetailByOrderCode(string orderCode, ref System.Data.DataSet dsResult)
        {
            //
            // ��ʼ������
            //
            this.Clear();
            //
            // ��ȡSQL���
            //
            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetInvoiceDetailByOrderCode.Select", ref this.SQL);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            intReturn = this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.GetInvoiceDetailByOrderCode.Where", ref this.WHERE);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            this.CreateSql();
            //
            // ��ʽ��SQL���
            //
            try
            {
                SQL = string.Format(SQL, orderCode);
            }
            catch (Exception e)
            {
                this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                return -1;
            }
            //
            // ִ��SQL���
            //
            this.intReturn = this.ExecQuery(this.SQL, ref dsResult);
            if (this.intReturn == -1)
            {
                this.SetError("", "����ҽ���Ż�ȡ��Ʊ��ϸ��Ϣ");
                return -1;
            }
            //
            // �ɹ�����
            //
            return 1;
        }
        #endregion

        /// <summary>
        /// סԺ���߻�ȡ��Ŀ��ִ������
        /// </summary>
        /// <param name="patient">����ʵ��</param>
        /// <param name="fee">����ʵ��</param>
        /// <param name="confirmedCount">�Ѿ�ȷ��ִ������</param>
        /// <returns>1���ɹ�����1��ʧ�ܣ�0��������</returns>
        public int GetInpatientConfirmInformation(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList fee, ref int confirmedCount)
        {
            //
            // ��ʼ������
            //
            this.Clear();
            //
            // ��ȡSQL���
            //
            intReturn = this.Sql.GetSql("FS.HISFC.BizLogic.Terminal.Confirm.GetConfirmInformation", ref this.SQL);
            if (intReturn == -1)
            {
                this.SetError("", "��ȡSQL���FS.HISFC.BizLogic.Terminal.Confirm.GetConfirmInformationʧ��");
                return -1;
            }
            //
            // ��ʽ��SQL���
            //
            try
            {
                SQL = string.Format(SQL, "2", fee.RecipeNO, fee.SequenceNO);
            }
            catch (Exception e)
            {
                this.SetError("", "��ʽ��SQL���ʧ��" + e.Message);
                return -1;
            }
            //
            // ִ��SQL���
            //
            this.intReturn = this.ExecQuery(this.SQL);
            if (this.intReturn == -1)
            {
                this.SetError("", "����ҽ���Ż�ȡ��Ʊ��ϸ��Ϣ");
                return -1;
            }
            if (this.Reader.Read())
            {
                confirmedCount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0].ToString());
            }
            else
            {
                return 0;
            }
            // �ɹ�����
            return 1;
        }

        //
        // ҵ���ӱ�
        //
        #region ȡ���ն�ȷ����ϸ�� ��ȷ�Ϲ�����ϸ
        public int UpdateConfirmDetail(string ApplyNum, string ValidFlag)
        {
            // SQL���
            string sql = "";
            //
            // ��ȡSQL���
            //
            if (this.Sql.GetSql("TerminalConfirm.UpdateConfirmDetail", ref sql) == -1)
            {
                this.Err = "��ȡSQL���TerminalConfirm.UpdateConfirmDetailʧ��";
                return -1;
            }
            // ƥ��ִ��
            try
            {
                //sql = string.Format(sql, GetParam(medTechItem));
                return this.ExecNoQuery(sql, ApplyNum, ValidFlag,this.Operator.ID);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
        }

        #endregion
        #region ������ϸ
        /// <summary>
        /// ������ϸ
        /// [����: FS.HISFC.Models.Terminal.TerminalConfirmDetail detail - ��ϸʵ����]
        /// [����: int,1-�ɹ�,-1-ʧ��]
        /// </summary>
        /// <param name="detail">��ϸʵ����</param>
        /// <returns>1-�ɹ�,-1-ʧ��</returns>
        public int Insert(FS.HISFC.Models.Terminal.TerminalConfirmDetail detail)
        {
            //
            // ��ȡSQL���
            //
            this.intReturn = this.Sql.GetSql("FS.HISFC.Management.MedTech.TerminalConfirm.CreateDetail", ref this.SQL);
            if (this.intReturn == -1)
            {
                this.SetError("", "����ҵ����ϸ��ȡSQL���ʧ��!" + "\n" + this.Err);
                return -1;
            }
            //
            // �������
            //
            this.FillDetailParm(detail);
            //
            // ��ʽ��SQL���
            //
            try
            {
                this.SQL = string.Format(this.SQL, detailParms);
            }
            catch
            {
                this.SetError("", "����ҵ����ϸ��ʽ��SQL���ʧ��!" + "\n" + this.Err);
                return -1;
            }
            //
            // ִ��SQL���
            //
            this.intReturn = this.ExecNoQuery(this.SQL);
            if (this.intReturn == -1)
            {
                this.SetError("", "����ҵ����ϸִ��SQL���ʧ��!" + "\n" + this.Err);
                return -1;
            }
            //
            // �ɹ�����
            //
            return 1;
        }
        #endregion
        #region �������뵥��ˮ�Ż�ȡҵ����ϸ
        /// <summary>
        /// �������뵥��ˮ�Ż�ȡҵ����ϸ
        /// [����1: FS.HISFC.Models.Terminal.TerminalApply apply - ���뵥]
        /// [����2: ArrayList alDetails - ҵ����ϸ]
        /// [����: int,1-�ɹ�,-1-ʧ��]
        /// </summary>
        /// <param name="apply">���뵥</param>
        /// <param name="alDetails">ҵ����ϸ</param>
        /// <returns></returns>
        public int QueryDetailsByApply(FS.HISFC.Models.Terminal.TerminalApply apply, ref ArrayList alDetails)
        {
            //
            // ��ȡSQL���
            //
            this.intReturn = this.Sql.GetSql("FS.HISFC.Management.MedTech.GetDetails", ref this.SQL);
            if (this.intReturn == -1)
            {
                this.SetError("", "��ȡҵ����ϸʧ��!" + "\n" + this.Err);
                return -1;
            }
            this.intReturn = this.Sql.GetSql("FS.HISFC.Management.MedTech.GetDetails.ByApplyCode", ref this.WHERE);
            if (this.intReturn == -1)
            {
                this.SetError("", "��ȡҵ����ϸʧ��!" + "\n" + this.Err);
                return -1;
            }
            // ����SQL���
            this.CreateSql();
            //
            // ��ʽ��SQL���
            //
            try
            {
                this.SQL = string.Format(this.SQL, apply.Order.ID, apply.ID);
            }
            catch
            {
                this.SetError("", "��ȡҵ����ϸʧ��!" + "\n" + "��ʽ��SQL���ʧ��");
            }
            //
            // ִ��SQL���
            //
            this.intReturn = this.ExecQuery(this.SQL);
            if (this.intReturn == -1)
            {
                this.SetError("", "��ȡҵ����ϸʧ��!" + "\n" + this.Err);
                return -1;
            }
            //
            // ת�����������
            //
            while (this.Reader.Read())
            {
                // ҵ����ϸ��
                FS.HISFC.Models.Terminal.TerminalConfirmDetail detail = new TerminalConfirmDetail();
                // ת��Reader�ɶ���
                this.intReturn = this.DetailReaderToObject(detail);
                // ���������
                if (this.intReturn == -1)
                {
                    this.SetError("", "��ȡҵ����ϸʧ��!" + "\n" + this.Err);
                    return -1;
                }
                // ��Ӷ��������
                alDetails.Add(detail);
            }
            //
            // �ɹ�����
            //
            return 1;
        }
        #endregion

        #endregion 

        #region סԺ�ն�ȷ����ϸ��
        /// <summary>
        /// ��ȡ��SQL
        /// </summary>
        /// <returns></returns>
        private string GetInpatientConfirmDetailSql()
        {
            // SQL���
            string strSql = "";
            // ��ȡSQL���
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.GetInpatientDetailSql", ref strSql) == -1)
            {
                this.Err = "��ȡSQL��� Management.Terminal.TerminalConfirm.GetInpatientDetailSql ʧ��";
                return null;
            }
            return strSql;
        }

        /// <summary>
        /// ����ҽ����ˮ�Ż�ȡȷ����ϸ
        /// </summary>
        /// <param name="MoOrder"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Terminal.TerminalConfirmDetail>  QueryTerminalConfirmDetailByMoOrder(string MoOrder)
        {
            string strSql = GetInpatientConfirmDetailSql();
            if (strSql == null)
            {
                return null;
            }
            string strSql2 = "";
            // ��ȡSQL���
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.QueryTerminalConfirmDetailByMoOrder", ref strSql2) == -1)
            {
                this.Err = "��ȡSQL��� Management.Terminal.TerminalConfirm.QueryTerminalConfirmDetailByMoOrder ʧ��";
                return null;
            }

            strSql = strSql + strSql2;
            strSql = string.Format(strSql, MoOrder);
            List<FS.HISFC.Models.Terminal.TerminalConfirmDetail> list = new List<TerminalConfirmDetail>();
            list = QuertMyTerminalConfirmDetail(strSql);
            return list;
        }
        /// <summary>
        /// ������ˮ�Ż�ȡȷ����ϸ
        /// </summary>
        /// <param name="MoOrder"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Terminal.TerminalConfirmDetail> QueryTerminalConfirmDetailBySequence(string Sequence)
        {
            string strSql = GetInpatientConfirmDetailSql();
            if (strSql == null)
            {
                return null;
            }
            string strSql2 = "";
            // ��ȡSQL���
            if (this.Sql.GetSql("Terminal.TerminalConfirm.QueryTerminalConfirmDetailBySequence", ref strSql2) == -1)
            {
                this.Err = "��ȡSQL��� Terminal.TerminalConfirm.QueryTerminalConfirmDetailBySequence ʧ��";
                return null;
            }

            strSql = strSql + strSql2;
            strSql = string.Format(strSql, Sequence);
            List<FS.HISFC.Models.Terminal.TerminalConfirmDetail> list = new List<TerminalConfirmDetail>();
            list = QuertMyTerminalConfirmDetail(strSql);
            return list;
        }
        
        /// <summary>
        /// ��������Ż�ȡ�ն�ȷ����ϸ
        /// </summary>
        /// <param name="MoOrder"></param>
        /// <returns></returns>
        public int QueryConfirmInfoByClinicNo(string clinicNo,string deptID, ref List<FS.HISFC.Models.Terminal.TerminalApply> apply)
        {
            string strSql = "";
            // ��ȡSQL���
            if (this.Sql.GetSql("Terminal.TerminalConfirm.GetOutpaientConfirmInfoByCardno", ref strSql) == -1)
            {
                this.Err = "��ȡSQL��� Terminal.TerminalConfirm.GetOutpaientConfirmInfoByCardno ʧ��";
                return -1;
            }

            strSql = string.Format(strSql, clinicNo, deptID);
            apply = QueryOutpatientConfirmDetail(strSql);
            return 1;
        }
        /// <summary>
        /// ��������Ż�ȡ�ն�ȷ����ϸ
        /// </summary>
        /// <param name="MoOrder"></param>
        /// <returns></returns>
        public int QueryConfirmInfoByClinicNoCancel(string clinicNo, string deptID, ref List<FS.HISFC.Models.Terminal.TerminalApply> apply)
        {
            string strSql = "";
            // ��ȡSQL���
            if (this.Sql.GetSql("Terminal.TerminalConfirm.GetOutpaientConfirmInfoByCardnoCancel", ref strSql) == -1)
            {
                this.Err = "��ȡSQL��� Terminal.TerminalConfirm.GetOutpaientConfirmInfoByCardnoCancel ʧ��";
                return -1;
            }

            strSql = string.Format(strSql, clinicNo, deptID);
            apply = QueryOutpatientConfirmDetail(strSql);
            return 1;
        }
        
        /// <summary>
        /// ����ҽ����ˮ�ź�ҽ��ִ�е��Ų�ѯ
        /// </summary>
        /// <param name="MoOrder"></param>
        /// <param name="ExecNO"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Terminal.TerminalConfirmDetail> QueryTerminalConfirmDetailByMoOrderAndExeMoOrder(string MoOrder,string  ExecNO)
        {
            string strSql = GetInpatientConfirmDetailSql();
            if (strSql == null)
            {
                return null;
            }
            string strSql2 = "";
            // ��ȡSQL���
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.QueryTerminalConfirmDetailByMoOrderAndExeMoOrder", ref strSql2) == -1)
            {
                this.Err = "��ȡSQL��� Management.Terminal.TerminalConfirm.QueryTerminalConfirmDetailByMoOrderAndExeMoOrder ʧ��";
                return null;
            }

            strSql = strSql + strSql2;
            List<FS.HISFC.Models.Terminal.TerminalConfirmDetail> list = new List<TerminalConfirmDetail>();
            strSql = string.Format(strSql, MoOrder, ExecNO);
            list = QuertMyTerminalConfirmDetail(strSql);
            return list;
        }

        /// <summary>
        ///�����˷Ѽ�¼�����ŵȲ�ѯ//�޸�ҽ���˷Ѹ���ȡ��ȷ������bug {47531763-0983-44dc-BC92-59A5588AF2F8} 2014-12-11 by lixuelong
        /// </summary>
        /// <param name="FeeItemList"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Terminal.TerminalConfirmDetail> QueryTerminalConfirmDetailByQuitFee(FS.HISFC.Models.Fee.ReturnApply QuitFee)
        {
            string strSql = GetInpatientConfirmDetailSql();
            if (strSql == null)
            {
                return null;
            }
            string strSql2 = "";
            // ��ȡSQL���
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.QueryTerminalConfirmDetailByQuitFee", ref strSql2) == -1)
            {
                this.Err = "��ȡSQL��� Management.Terminal.TerminalConfirm.QueryTerminalConfirmDetailByQuitFee ʧ��";
                return null;
            }

            strSql = strSql + strSql2;
            List<FS.HISFC.Models.Terminal.TerminalConfirmDetail> list = new List<TerminalConfirmDetail>();
            strSql = string.Format(strSql, QuitFee.Order.ID, QuitFee.ExecOrder.ID ,QuitFee.RecipeNO ,QuitFee.SequenceNO);
            list = QuertMyTerminalConfirmDetail(strSql);
            return list;
        }

        /// <summary>
        ///����ȷ����ϸ������Ϊ��Ч�շѼ�¼�Ĵ����� {47531763-0983-44dc-BC92-59A5588AF2F8} 2014-12-11 by lixuelong
        /// </summary>
        /// <param name="CancelRecipeNO"></param>
        /// <param name="RecipeNO"></param>
        /// <returns></returns>
        public int UpdateTerminalDetailRecipe(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            string strSql = "";
            if (this.Sql.GetSql("Terminal.TerminalConfirm.UpdateTerminalDetailRecipe", ref strSql) == -1)
            {
                this.Err = "��ȡTerminal.TerminalConfirm.UpdateTerminalDetailRecipe ʧ��";
                return -1;
            }
            strSql = string.Format(strSql, feeItemList.RecipeNO, feeItemList.CancelRecipeNO, feeItemList.SequenceNO, feeItemList.Order.ID, feeItemList.ExecOrder.ID);
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ��ѯ�����ն�ȷ����ϸ
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        private List<TerminalApply> QueryOutpatientConfirmDetail(string strSql)
        {
            List<FS.HISFC.Models.Terminal.TerminalApply> list = new List<TerminalApply>();
            /*
            select t.item_code,--��Ŀ����0
             t.item_name, --��Ŀ��1
            t.confirm_number,--��ȷ������2
            t.confirm_employee,--ȷ����3
            t.confirm_department,--ȷ�Ͽ���4
            t.extend_field1,--ҽ����ˮ��5
            t.exec_device,--ִ���豸6
            t.exec_oper,--ִ����7
            t.confirm_date,--ȷ��ʱ��8
             t.current_sequence,--��ˮ��
             t.apply_code
             from met_tec_ta_detail t 
             where t.card_no = '{0}' and t.status = '0'
             
             order by t.oper_date desc
             */
            try
            {

                intReturn = this.ExecQuery(strSql);
                if (intReturn < 0)
                {
                    this.Err = "��ѯ�ն�ȷ����ϸʧ��";
                    return null;
                }
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Terminal.TerminalApply obj = new TerminalApply();
                    obj.Item.ID = this.Reader[0].ToString();
                    obj.Item.Name = this.Reader[1].ToString();
                    obj.Item.ConfirmedQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2].ToString());
                    obj.ConfirmOperEnvironment.ID = this.Reader[3].ToString();
                    obj.ConfirmOperEnvironment.Dept.ID = this.Reader[4].ToString();
                    obj.Order.ID = this.Reader[5].ToString();
                    obj.Machine.ID = this.Reader[6].ToString();
                    obj.ExecOper.ID = this.Reader[7].ToString();
                    obj.ConfirmOperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8].ToString());
                    obj.ID = this.Reader[9].ToString();
                    obj.User02 = this.Reader[10].ToString();
                    list.Add(obj);
                }
            }
            catch (Exception ex)
            {
            }
            finally 
            {
                this.Reader.Close();
            }
            return list;
        }
        /// <summary>
        /// ����ȷ����ϸ��ˮ�Ų�ѯ
        /// </summary>
        /// <param name="ApplyNum"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Terminal.TerminalConfirmDetail> QueryTerminalConfirmDetailByApplyNum(string ApplyNum)
        {
            string strSql = GetInpatientConfirmDetailSql();
            if (strSql == null)
            {
                return null;
            }
            string strSql2 = "";
            // ��ȡSQL���
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.QueryTerminalConfirmDetailByApplyNum", ref strSql2) == -1)
            {
                this.Err = "��ȡSQL��� Management.Terminal.TerminalConfirm.QueryTerminalConfirmDetailByApplyNum ʧ��";
                return null;
            }

            strSql = strSql + strSql2;
            List<FS.HISFC.Models.Terminal.TerminalConfirmDetail> list = new List<TerminalConfirmDetail>();
            strSql = string.Format(strSql, ApplyNum);
            list = QuertMyTerminalConfirmDetail(strSql);
            return list;
        }

        private List<FS.HISFC.Models.Terminal.TerminalConfirmDetail> QuertMyTerminalConfirmDetail(string strSql)
        {
            List<FS.HISFC.Models.Terminal.TerminalConfirmDetail> list = new List<TerminalConfirmDetail>();
            intReturn = this.ExecQuery(strSql);
            if (intReturn < 0)
            {
                this.Err = "��ѯ�ն�ȷ����ϸʧ��";
                return null;
            } 
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Terminal.TerminalConfirmDetail obj = new TerminalConfirmDetail();
                obj.MoOrder = this.Reader[0].ToString();
                obj.ExecMoOrder = this.Reader[1].ToString();
                obj.Sequence = this.Reader[2].ToString();
                obj.Apply.Item.ID = this.Reader[3].ToString();
                obj.Apply.Item.Name = this.Reader[4].ToString();
                obj.Apply.Item.ConfirmedQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5].ToString());
                obj.Apply.ConfirmOperEnvironment.ID = this.Reader[6].ToString();
                obj.Apply.ConfirmOperEnvironment.Dept.ID = this.Reader[7].ToString();
                obj.Apply.ConfirmOperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8].ToString());
                obj.Status.ID = this.Reader[9].ToString();
                obj.CancelInfo.ID = this.Reader[10].ToString();
                obj.CancelInfo.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[11].ToString());
                obj.Apply.Patient.ID = this.Reader[12].ToString();
                obj.Apply.Item.RecipeNO = this.Reader[13].ToString();
                obj.Apply.Item.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[14].ToString());
                obj.ExecDevice = this.Reader[15].ToString();
                obj.Oper.ID = this.Reader[16].ToString();
                //����ʣ��ȷ��������ȷ������-ȡ�������� {47531763-0983-44dc-BC92-59A5588AF2F8} 2014-12-11 by lixuelong
                obj.FreeCount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[17].ToString());
                list.Add(obj);
            }
            return list;
        }

        /// <summary>
        /// �������뵥
        /// </summary>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int InsertInpatientConfirmDetail(FS.HISFC.Models.Terminal.TerminalConfirmDetail detail)
        {
            string strSql = "";
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.InsertInpatientConfirmDetail", ref strSql) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            string[] parms = new string[]{  detail.MoOrder,//0
                                            detail.ExecMoOrder,//1
                                            detail.Sequence,//2
                                            detail.Apply.Item.ID,//3
                                            detail.Apply.Item.Name,//4
                                            detail.Apply.Item.ConfirmedQty.ToString(),//5
                                            detail.Apply.ConfirmOperEnvironment.ID,//6
                                            detail.Apply.ConfirmOperEnvironment.Dept.ID,//7
                                            detail.Apply.ConfirmOperEnvironment.OperTime.ToString(),//8
                                            detail.Status.ID,//9
                                            detail.CancelInfo.ID,//10
                                            detail.CancelInfo.OperTime.ToString(),//11
                                            detail.Apply.Patient.ID,
                                            detail.Apply.Item.RecipeNO,
                                            detail.Apply.Item.SequenceNO.ToString(),
                                            detail.ExecDevice,//ִ���豸
                                            detail.Oper.ID  //ִ�м�ʦ
                                        };
            strSql = string.Format(strSql, parms);
            intReturn = this.ExecNoQuery(strSql);
            if (intReturn <= 0)
            {
                this.Err = "�����ն�ȷ����ϸʧ��";
                return -1;
            } 
            return 1;
        }
        /// <summary>
        /// �������뵥
        /// </summary>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int CancelInpatientConfirmDetail(FS.HISFC.Models.Terminal.TerminalConfirmDetail detail)
        {
            string strSql = "";
            string status = "";
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.CancelInpatientConfirmDetail", ref strSql) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            FS.HISFC.Models.Base.Employee obj = (FS.HISFC.Models.Base.Employee)this.Operator;
            //if (detail.Apply.Item.ConfirmedQty > detail.FreeCount)
            //{
            //    status = "0";
            //}
            //else
            //{
            //    status = "1";
            //}
            strSql = string.Format(strSql, detail.Sequence, detail.Status.ID, obj.ID, detail.FreeCount, detail.Apply.Item.ConfirmedQty - detail.FreeCount);
            intReturn = this.ExecNoQuery(strSql);
            if (intReturn <= 0)
            {
                this.Err = "�����ն�ȷ����ϸʧ��";
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ����ȷ����ϸ {47531763-0983-44dc-BC92-59A5588AF2F8} 2014-12-11 by lixuelong
        /// </summary>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int CancelInpatientConfirmDetail2(FS.HISFC.Models.Terminal.TerminalConfirmDetail detail)
        {
            string strSql = "";
            string status = "";
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.CancelInpatientConfirmDetail", ref strSql) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            FS.HISFC.Models.Base.Employee obj = (FS.HISFC.Models.Base.Employee)this.Operator;
            //cancelTot Ϊ�ܼ�ȡ����
            decimal cancelTot = detail.Apply.Item.ConfirmedQty - detail.FreeCount;
            strSql = string.Format(strSql, detail.Sequence, detail.Status.ID, obj.ID, cancelTot);
            intReturn = this.ExecNoQuery(strSql);
            if (intReturn <= 0)
            {
                this.Err = "�����ն�ȷ����ϸʧ��";
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// �������뵥
        /// </summary>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int CancelInpatientConfirmDetailCancel1(FS.HISFC.Models.Terminal.TerminalConfirmDetail detail)
        {
            string strSql = "";
            string status = "";
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.CancelInpatientConfirmDetailCancel1", ref strSql) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            FS.HISFC.Models.Base.Employee obj = (FS.HISFC.Models.Base.Employee)this.Operator;
            //if (detail.Apply.Item.ConfirmedQty > detail.FreeCount)
            //{
            //    status = "0";
            //}
            //else
            //{
            //    status = "1";
            //}
            strSql = string.Format(strSql, detail.Sequence, "0", obj.ID, detail.FreeCount, detail.Apply.Item.ConfirmedQty - detail.FreeCount);
            intReturn = this.ExecNoQuery(strSql);
            if (intReturn <= 0)
            {
                this.Err = "�����ն�ȷ����ϸʧ��";
                return -1;
            }

            return 1;
        }
        /// <summary>
        /// �������뵥
        /// </summary>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int CancelInpatientConfirmDetailCancal(FS.HISFC.Models.Terminal.TerminalConfirmDetail detail)
        {
            string strSql = "";
            string status = "";
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.CancelInpatientConfirmDetailCancel", ref strSql) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            FS.HISFC.Models.Base.Employee obj = (FS.HISFC.Models.Base.Employee)this.Operator;
            //if (detail.Apply.Item.ConfirmedQty > detail.FreeCount)
            //{
            //    status = "0";
            //}
            //else
            //{
            //    status = "1";
            //}
            strSql = string.Format(strSql, detail.Sequence, "1", obj.ID, detail.FreeCount, detail.Apply.Item.ConfirmedQty - detail.FreeCount);
            intReturn = this.ExecNoQuery(strSql);
            if (intReturn <= 0)
            {
                this.Err = "�����ն�ȷ����ϸʧ��";
                return -1;
            }

            return 1;
        }
        /// <summary>
        /// ����ҽ����
        /// </summary>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int CancelInpatientConfirmMoOrder(FS.HISFC.Models.Terminal.TerminalConfirmDetail detail)
        {
            string strSql = "";
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.CancelInpatientConfirmMoOrder", ref strSql) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            strSql = string.Format(strSql, detail.MoOrder,detail.ExecMoOrder);
            intReturn = this.ExecNoQuery(strSql);
            if (intReturn <= 0)
            {
                this.Err = "����ҽ��ִ�е�ʧ��";
                return -1;
            }

            return 1;
        }
        /// <summary>
        /// ����ҽ����
        /// </summary>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int CancelInpatientConfirmMoOrderCancel1(FS.HISFC.Models.Terminal.TerminalConfirmDetail detail)
        {
            string strSql = "";
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.CancelInpatientConfirmMoOrderCancel1", ref strSql) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            strSql = string.Format(strSql, detail.MoOrder, detail.ExecMoOrder);
            intReturn = this.ExecNoQuery(strSql);
            if (intReturn <= 0)
            {
                this.Err = "����ҽ��ִ�е�ʧ��";
                return -1;
            }

            return 1;
        }
        /// <summary>
        /// ����ҽ����
        /// </summary>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int CancelInpatientConfirmMoOrderCancel(FS.HISFC.Models.Terminal.TerminalConfirmDetail detail)
        {
            string strSql = "";
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.CancelInpatientConfirmMoOrderCancel", ref strSql) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            strSql = string.Format(strSql, detail.MoOrder, detail.ExecMoOrder);
            intReturn = this.ExecNoQuery(strSql);
            if (intReturn <= 0)
            {
                this.Err = "����ҽ��ִ�е�ʧ��";
                return -1;
            }

            return 1;
        }
        /// <summary>
        /// �����շѱ�
        /// </summary>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int CancelInpatientItemList(FS.HISFC.Models.Terminal.TerminalConfirmDetail detail)
        {
            string strSql = "";
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.CancelInpatientItemList", ref strSql) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            strSql = string.Format(strSql, detail.Apply.Item.RecipeNO,detail.Apply.Item.SequenceNO,detail.FreeCount);//׼����
            intReturn = this.ExecNoQuery(strSql);
            if (intReturn <= 0)
            {
                this.Err = "����ҽ��ִ�е�ʧ��";
                return -1;
            }

            return 1;
        }
        /// <summary>
        /// �����շѱ�
        /// </summary>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int CancelInpatientItemListCancel1(FS.HISFC.Models.Terminal.TerminalConfirmDetail detail)
        {
            string strSql = "";
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.CancelInpatientItemListCancel1", ref strSql) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            strSql = string.Format(strSql, detail.Apply.Item.RecipeNO, detail.Apply.Item.SequenceNO, detail.FreeCount);//׼����
            intReturn = this.ExecNoQuery(strSql);
            if (intReturn <= 0)
            {
                this.Err = "����ҽ��ִ�е�ʧ��";
                return -1;
            }

            return 1;
        }
        /// <summary>
        /// �����շѱ�
        /// </summary>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int CancelInpatientItemListCancel(FS.HISFC.Models.Terminal.TerminalConfirmDetail detail)
        {
            string strSql = "";
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.CancelInpatientItemListCancel", ref strSql) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            strSql = string.Format(strSql, detail.Apply.Item.RecipeNO, detail.Apply.Item.SequenceNO, detail.FreeCount);//׼����
            intReturn = this.ExecNoQuery(strSql);
            if (intReturn <= 0)
            {
                this.Err = "����ҽ��ִ�е�ʧ��";
                return -1;
            }

            return 1;
        }
        /// <summary>
        /// �����շѱ�{2CE2BB1D-9DEB-4afa-90CF-15E3E44285E1}
        /// </summary>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int CancelInpatientItemListWithSeq(FS.HISFC.Models.Terminal.TerminalConfirmDetail detail)
        {
            string strSql = "";
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.CancelInpatientItemListWithSeq", ref strSql) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            //strSql = string.Format(strSql, detail.Apply.Item.RecipeNO,detail.Apply.Item.SequenceNO,detail.Apply.Item.ConfirmedQty);
            #region {CB8F8F87-3389-4757-AC01-242CAF44A492}
            strSql = string.Format(strSql,
                detail.MoOrder, detail.ExecMoOrder, detail.Apply.Item.ConfirmedQty, detail.Apply.Item.RecipeNO
                , detail.Apply.Item.SequenceNO);
            #endregion
            intReturn = this.ExecNoQuery(strSql);
            if (intReturn <= 0)
            {
                this.Err = "����ҽ��ִ�е�ʧ��";
                return -1;
            }

            return 1;
        }
        /// <summary>
        /// ��ѯҽ�����µ���ȷ������
        /// </summary>
        /// <param name="MoOrder"></param>
        /// <returns></returns>
        public decimal GetAlreadConfirmNum(string MoOrder)
        {
            string strSql = "";
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.GetAlreadConfirmNum", ref strSql) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            strSql = string.Format(strSql, MoOrder);
            this.ExecQuery(strSql);
            decimal confirmQty = 0;
            while(this.Reader.Read())
            {
                confirmQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);
            }
            return confirmQty;
        }
        /// <summary>
        /// ��ѯҽ�����µ���ȷ������
        /// </summary>
        /// <param name="MoOrder"></param>
        /// <returns></returns>
        public decimal GetAlreadConfirmNum(string MoOrder,string ExeNO)
        {
            if (ExeNO == null || ExeNO == "")
            {
                return GetAlreadConfirmNum(MoOrder);
            }
            string strSql = "";
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.GetAlreadConfirmNumMoOrder", ref strSql) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            strSql = string.Format(strSql, MoOrder, ExeNO);
            this.ExecQuery(strSql);
            decimal confirmQty = 0;
            while (this.Reader.Read())
            {
                confirmQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);
            }
            return confirmQty;
        }


        /// <summary>
        /// ��ѯҽ�����µ���ȷ������
        /// </summary>
        /// <param name="MoOrder"></param>
        /// <returns></returns>
        public decimal GetAlreadConfirmNum(string MoOrder, string ExeNO,string ItemCode)
        {
            if (ExeNO == null || ExeNO == "")
            {
                return GetAlreadConfirmNum(MoOrder);
            }
            string strSql = "";
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.GetAlreadConfirmNumMoOrderAndItemCode", ref strSql) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            strSql = string.Format(strSql, MoOrder, ExeNO,ItemCode);
            this.ExecQuery(strSql);
            decimal confirmQty = 0;
            while (this.Reader.Read())
            {
                confirmQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);
            }
            return confirmQty;
        }

        /// <summary>
        /// ��ѯ������Ŀ�İ�װ����{47531763-0983-44dc-BC92-59A5588AF2F8} 2014-12-12 by lixuelong
        /// </summary>
        /// <param name="MoOrder"></param>
        /// <returns></returns>
        public decimal GetPackageItemPackQty(string MoOrder, string ExeNO)
        {
            string strSql = "";
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.GetPackageItemPackQty", ref strSql) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            strSql = string.Format(strSql, MoOrder, ExeNO);
            this.ExecQuery(strSql);
            decimal packQty = 0;
            while (this.Reader.Read())
            {
                packQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);
            }
            return packQty;
        }

        /// <summary>
        /// ��ѯҽ��ִ�е�����
        /// </summary>
        /// <param name="MoOrder"></param>
        /// <returns></returns>
        public decimal GetExecOrderQty(string execOrder)
        {
            string strSql = "";
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.GetExecOrderQty", ref strSql) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            strSql = string.Format(strSql, execOrder);
            this.ExecQuery(strSql);
            decimal execOrderQty = 0;
            while (this.Reader.Read())
            {
                execOrderQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0]);
            }
            return execOrderQty;
        } 

        /// <summary>
        /// ����ִ�е�
        /// </summary>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int CancelExecOrder(string execOrder)
        {
            string strSql = "";
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.CancelExecOrder", ref strSql) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            strSql = string.Format(strSql, execOrder, this.Operator.ID, this.GetSysDateTime());
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ����ִ�е�
        /// </summary>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int CancelExecOrderCancel1(string execOrder)
        {
            string strSql = "";
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.CancelExecOrderCancel1", ref strSql) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            strSql = string.Format(strSql, execOrder, this.Operator.ID, this.GetSysDateTime());
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ����ִ�е�
        /// </summary>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int CancelExecOrderCancel(string execOrder)
        {
            string strSql = "";
            if (this.Sql.GetSql("Management.Terminal.TerminalConfirm.CancelExecOrderCancel", ref strSql) == -1)
            {
                this.SetError("", "��ȡSQL���ʧ��");
                return -1;
            }
            strSql = string.Format(strSql, execOrder, this.Operator.ID, this.GetSysDateTime());
            return this.ExecNoQuery(strSql);
        }
        #endregion 

        //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        #region �˻�����
        /// <summary>
        /// ɾ����Ч����������
        /// </summary>
        /// <param name="recipeNO">�������</param>
        /// <param name="recipeSequenceNO">��������Ŀ��ˮ��</param>
        /// <returns></returns>
        public int DelTecApply(string recipeNO, string recipeSequenceNO)
        {
            string sql = string.Empty;
            string wheresql = string.Empty;
            if (this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.Outpatient.DeleteValidate", ref sql) == -1)
            {
                this.Err = "��ѯ����ΪFS.HISFC.Management.TerminalValidate.Outpatient.DeleteValidate��SQLʧ�ܣ�";
                return -1;
            }
            if (this.Sql.GetSql("FS.HISFC.Management.TerminalValidate.Outpatient.DeleteValidate.Where1", ref wheresql) == -1)
            {
                this.Err = "��ѯ����ΪFS.HISFC.Management.TerminalValidate.Outpatient.DeleteValidate.Where1��SQLʧ�ܣ�";
                return -1;
            }
            sql += wheresql;
            sql = string.Format(sql, recipeNO, recipeSequenceNO);
            return this.ExecNoQuery(sql);
        }

        #endregion
    }
}
