using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using System.Collections;

namespace FS.HISFC.Components.InpatientFee.Fee
{
    /// <summary>
    /// [��������: סԺ���߷��ü���]<br></br>
    /// [�� �� ��: nxy]<br></br>
    /// [����ʱ��: 2008-08]<br></br>
    /// <�޸ļ�¼>
    ///     
    /// </�޸ļ�¼>
    /// </summary>
    public partial class ucInpatientDerateFee : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
       
        public ucInpatientDerateFee()
        {
            InitializeComponent();
        }
        #region ����
        /// <summary>
        /// סԺ������Ϣʵ��
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// ���תintegrate��
        /// </summary>
        FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Fee.Derate derateMgr = new FS.HISFC.BizLogic.Fee.Derate();

        /// <summary>
        /// �Ѿ��������Ϣ
        /// </summary>
        DataTable dtDerated = new DataTable();

        /// <summary>
        /// ����ҵ���integrate
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ��С���ù�ϣ��
        /// </summary>
        Hashtable minFeeHastable = null;

        /// <summary>
        /// ��ɾ����
        /// </summary>
        ArrayList alDelete = new ArrayList();

        /// <summary>
        /// toolBarService
        /// </summary>
        FS.FrameWork.WinForms.Forms.ToolBarService tooBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// סԺ����ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Fee.InPatient feeInpatient = new FS.HISFC.BizLogic.Fee.InPatient();

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ���ʾ�ܷ���Sheetҳ
        /// </summary>
        [Category("����")]
        public bool IsShowTotSheet
        {
            get
            {
                return this.fpFeeInfo_TotFee.Visible;
            }
            set
            {
                this.fpFeeInfo_TotFee.Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ��ϸ����Sheetҳ
        /// </summary>
        [Category("����")]
        public bool IsShowFeeDetailsSheet
        {
            get
            {
                return this.fpFeeInfo_Items.Visible;
            }
            set
            {
                this.fpFeeInfo_Items.Visible = value;
            }
        }

        #endregion

        #region ˽�з���

        /// <summary>
        /// ���ü���fp��ʾ��ʽ
        /// </summary>
        /// <returns></returns>
        private int InitDataSetDerated()
        {
            this.dtDerated.Columns.AddRange(new DataColumn[] 
                    {
                        new DataColumn("ҽ����ˮ��",typeof(string)),  //0
                        new DataColumn("�������",typeof(int)),//1
                        new DataColumn("�����������",typeof(string)),//2
                        new DataColumn("��������",typeof(string)),//3
                        new DataColumn("������������",typeof(string)),//4
                        new DataColumn("��������",typeof(string)),//5
                        new DataColumn("��С���ñ���",typeof(string)),//6
                        new DataColumn("��С��������",typeof(string)),//7
                        new DataColumn("������",typeof(decimal)),//8
                        new DataColumn("����ԭ��",typeof(string)),//9
                        new DataColumn("��׼��Ա������",typeof(string)),//10
                        new DataColumn("��׼������",typeof(string)),//11
                        new DataColumn("���Ҵ���",typeof(string)),//12
                        new DataColumn("��������",typeof(string)),//13
                        new DataColumn("������",typeof(string)),//14
                        new DataColumn("����ʱ��",typeof(DateTime)),//15
                        new DataColumn("����Ա",typeof(string)),//16
                        new DataColumn("��������",typeof(DateTime)),//17
                        new DataColumn("��Ŀ����",typeof(string)),//18
                        new DataColumn("��Ŀ����",typeof(string)),//19
                        new DataColumn("�Ƿ���Ч",typeof(bool))//20
                    });
            this.fpDerateInfo_Sheet1.DataAutoHeadings = false;
            this.fpDerateInfo_Sheet1.DataAutoSizeColumns = false;
            this.fpDerateInfo_Sheet1.DataSource = this.dtDerated;

            //���ò��ɼ�
            for (int i = 0; i < this.fpDerateInfo_Sheet1.Columns.Count; i++)
            {
                this.fpDerateInfo_Sheet1.Columns[i].Locked = true;
            }

            return 1;
        }

        /// <summary>
        /// ��ֵ����dataset
        /// </summary>
        /// <param name="al"></param>
        private void InSertDataSet(ArrayList al)
        {
            foreach (FS.HISFC.Models.Fee.DerateFee derateObj in al)
            {

                this.dtDerated.Rows.Add(new object[]
                        {
                            derateObj.InpatientNO,
                            derateObj.HappenNO,
                            derateObj.DerateKind.ID,
                            derateObj.DerateKind.Name,
                            derateObj.DerateType.ID,
                            derateObj.DerateType.Name,
                            derateObj.FeeCode,
                            derateObj.FeeName,
                            derateObj.DerateCost,
                            derateObj.DerateCause,
                            derateObj.ConfirmOperator.ID,
                            derateObj.ConfirmOperator.Name,
                            derateObj.DerateOper.Dept.ID,
                            derateObj.DerateOper.Dept.Name,
                            derateObj.CancelDerateOper.ID,
                            derateObj.CancelDerateOper.OperTime,
                            derateObj.DerateOper.ID,
                            derateObj.DerateOper.OperTime,
                            derateObj.ItemCode,
                            derateObj.ItemName,
                            derateObj.IsValid

                        }
                        );

            }

        }

        /// <summary>
        /// �Ƚ�ֵ��С
        /// </summary>
        /// <param name="derateCode"></param>
        /// <param name="leftCost"></param>
        /// <returns></returns>
        private bool ValidDerateCost(decimal derateCost, decimal leftCost)
        {
            return derateCost <= leftCost;
        }

        /// <summary>
        /// �������(�ܽ��)
        /// </summary>
        /// <param name="derateFee"></param>
        /// <returns></returns>
        private int SetAtferDerateTotFee(decimal derateFee)
        {
            //ȡ�Ѽ�����
            decimal deratedFee = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_TotFee.Cells[0, 4].Text);

            //ȡ���
            decimal leftFee = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_TotFee.Cells[0, 5].Text);

            //�����Ѽ�����
            this.fpFeeInfo_TotFee.Cells[0, 4].Text = (deratedFee + derateFee).ToString();

            //�������
            this.fpFeeInfo_TotFee.Cells[0, 5].Text = (leftFee - derateFee).ToString();


            return 1;
        }

        /// <summary>
        /// �������(��С����)
        /// </summary>
        /// <param name="row"></param>
        /// <param name="derateFee"></param>
        /// <returns></returns>
        private int SetAfterDerateMinFee(int row, decimal derateFee)
        {
            //ȡ�Ѽ�����
            decimal deratedFee = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_FeeCode.Cells[row, 7].Text);

            //ȡ���
            decimal leftFee = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_FeeCode.Cells[row, 8].Text);

            //�����Ѽ�����
            this.fpFeeInfo_FeeCode.Cells[row, 7].Text = (deratedFee + derateFee).ToString();

            //�������
            this.fpFeeInfo_FeeCode.Cells[row, 8].Text = (leftFee - derateFee).ToString();

            return 1;
        }

        /// <summary>
        /// ������Ŀ����
        /// </summary>
        /// <param name="row"></param>
        /// <param name="derateFee"></param>
        /// <returns></returns>
        private int SetAfterDerateFeeDetail(int row, decimal derateFee)
        {
            //ȡ�Ѽ�����
            decimal deratedFee = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_Items.Cells[row, 6].Text);

            //ȡ���
            decimal leftFee = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_Items.Cells[row, 7].Text);

            //�����Ѽ�����
            this.fpFeeInfo_Items.Cells[row, 6].Text = (deratedFee + derateFee).ToString();

            //�������t
            this.fpFeeInfo_Items.Cells[row, 7].Text = (leftFee - derateFee).ToString();

            return 1;
        }

        /// <summary>
        /// ȡ����С�����к�
        /// </summary>
        /// <param name="feeCode"></param>
        /// <returns></returns>
        private int GetRowIndex(string feeCode)
        {
            int count = this.fpFeeInfo_FeeCode.RowCount;
            string strFeeCode = "";
            if (count == 0)
            {
                //δָ����
                return -1;
            }

            for (int i = 0; i < count; i++)
            {
                strFeeCode = this.fpFeeInfo_FeeCode.Cells[i, 1].Text;
                if (strFeeCode == feeCode)
                {
                    return i;
                    break;
                }
            }

            //δָ����
            return -1;
        }

        /// <summary>
        /// ȡ����Ŀ�к�
        /// </summary>
        /// <param name="feeCode"></param>
        /// <returns></returns>
        private int GetRowItemsIndex(string itemCode)
        {
            int count = this.fpFeeInfo_Items.RowCount;
            string strItemCode = "";
            if (count == 0)
            {
                //δָ����
                return -1;
            }

            for (int i = 0; i < count; i++)
            {
                strItemCode = this.fpFeeInfo_Items.Cells[i, 2].Text;
                if (strItemCode == itemCode)
                {
                    return i;
                    break;
                }
            }

            //δָ����
            return -1;
        }

        /// <summary>
        /// ��������ӹ�ϣ��
        /// </summary>
        private void AddHasTable()
        {
            //this.minFeeHastable.Clear();
            int count = this.fpFeeInfo_FeeCode.Rows.Count;

            if (count <= 0) return;
            this.minFeeHastable = new Hashtable();
            for (int i = 0; i < count; i++)
            {
                this.minFeeHastable.Add(this.fpFeeInfo_FeeCode.Cells[i, 1].Text, this.fpFeeInfo_FeeCode.Cells[i, 8].Text);
            }

            this.rbCost.Checked = true;
            this.ntxtFee.Enabled = true;
            this.ntxtFee.Text = "0.00";
            this.rbRate.Checked = false;
            this.ntxRate.Enabled = false;
            this.ntxRate.Text = "0.00";
        }

        /// <summary>
        /// ���ñ���
        /// </summary>
        private void SetFeeDetailHead()
        {
            this.fpFeeInfo_Items.Columns[0].Label = "���ñ���";
            this.fpFeeInfo_Items.Columns[1].Label = "��������";
            this.fpFeeInfo_Items.Columns[2].Label = "��Ŀ����";
            this.fpFeeInfo_Items.Columns[3].Label = "��Ŀ����";
            this.fpFeeInfo_Items.Columns[4].Label = "�����ܶ�";
            this.fpFeeInfo_Items.Columns[5].Label = "�Էѽ��";
            this.fpFeeInfo_Items.Columns[6].Label = "�Ѽ�����";
            this.fpFeeInfo_Items.Columns[7].Label = "�������";

        }

        //ʵ�帳ֵ
        private ArrayList GetChanges(DataTable dt)
        {
            ArrayList al = new ArrayList();
            foreach (DataRow dr in dt.Rows)
            {
                FS.HISFC.Models.Fee.DerateFee info = new FS.HISFC.Models.Fee.DerateFee();

                info.InpatientNO = dr[0].ToString(); //סԺ��ˮ�� 

                info.HappenNO = this.derateMgr.GetHappenNO(this.patientInfo.ID); //�������
                info.DerateKind.ID = dr[2].ToString(); //��������  

                info.DerateType.ID = dr[4].ToString(); //�������� 
                info.FeeCode = dr[6].ToString();  //��С���� 
                info.FeeName = dr[7].ToString();

                info.DerateCost = Convert.ToDecimal(dr[8]); //������ 

                info.DerateCause = dr[9].ToString(); //����ԭ�� 

                info.ConfirmOperator.ID = dr[10].ToString(); //��׼�˱��� 

                info.ConfirmOperator.Name = dr[11].ToString(); // ��׼�� 

                info.DerateOper.Dept.ID = dr[12].ToString(); //  ����

                info.CancelDerateOper.ID = dr[14].ToString();

                info.CancelDerateOper.OperTime = Convert.ToDateTime(dr[15].ToString());

                info.DerateOper.ID = dr[16].ToString();

                info.DerateOper.OperTime = Convert.ToDateTime(dr[17]);

                info.ItemCode = dr[18].ToString();

                info.ItemName = dr[19].ToString();
                info.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(dr[20]);
                info.BalanceState = "0";
                al.Add(info);

            }
            return al;
        }

        /// <summary>
        /// ���߼������ʹ���ȡ����
        /// </summary>
        /// <param name="derateKindID"></param>
        /// <returns></returns>
        private string GetDerateKindName(string derateKindID)
        {
            string derateName = string.Empty;
            switch (derateKindID)
            {
                case "0":
                    {
                        derateName = "�ܷ��ü���";
                        break;
                    }
                case "1":
                    {
                        derateName = "��С���ü���";
                        break;
                    }
                case "2":
                    {
                        derateName = "��Ŀ����";
                        break;
                    }
                case "3":
                    {
                        derateName = "��С���ü���";
                        break;
                    }
                default:
                    break;
            }
            return derateName;
            //decode(derate_kind,'0','�ܷ��ü���','1','��С���ü���','��Ŀ����','3','��С���ü���')
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        private FS.FrameWork.Models.NeuObject GetDerateType()
        {
            return this.cmbDerateType.SelectedItem as FS.FrameWork.Models.NeuObject;
        }

        #endregion

        #region ���з���

        #region ��ʼ������
        /// <summary>
        /// ��û��߷�����Ϣ
        /// </summary>
        /// <returns></returns>
        protected virtual int GetFeeInfo()
        {
            //ȡ�ܷ���
            int returnValue = this.GetTotCost();
            if (returnValue == -1)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ����Ѿ�����ķ�����Ϣ
        /// </summary>
        /// <returns></returns>
        protected virtual int GetDeratedFeeInfo()
        {
            ArrayList al = this.derateMgr.GetDeratedDetail(this.patientInfo.ID);

            if (al == null)
            {
                MessageBox.Show(Language.Msg(this.derateMgr.Err));
                return -1;
            }

            //����dataset
            this.InSertDataSet(al);

            this.dtDerated.AcceptChanges();
            return 1;
        }

        /// <summary>
        /// ��ȡ�ܷ���
        /// </summary>
        /// <returns></returns>
        protected virtual int GetTotCost()
        {
            this.fpFeeInfo_TotFee.AddRows(0, 1);
            string deratedCost = this.derateMgr.GetTotDerateFeeByClinicNO(this.patientInfo.ID);
            if (deratedCost == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѯ������ʧ��" + this.derateMgr.Err));
                return -1;
            }

            this.fpFeeInfo_TotFee.Cells[0, 0].Text = this.patientInfo.FT.TotCost.ToString();
            this.fpFeeInfo_TotFee.Cells[0, 1].Text = this.patientInfo.FT.OwnCost.ToString();
            this.fpFeeInfo_TotFee.Cells[0, 2].Text = this.patientInfo.FT.PayCost.ToString();
            this.fpFeeInfo_TotFee.Cells[0, 3].Text = this.patientInfo.FT.PubCost.ToString();
            this.fpFeeInfo_TotFee.Cells[0, 4].Text = deratedCost;
            this.fpFeeInfo_TotFee.Cells[0, 5].Text = (this.patientInfo.FT.OwnCost - FS.FrameWork.Function.NConvert.ToDecimal(deratedCost)).ToString();



            return 1;
        }

        /// <summary>
        /// �����С����
        /// </summary>
        /// <returns></returns>
        protected virtual int GetMinfee()
        {
            DataSet ds = new DataSet();
            ds = this.derateMgr.GetMinFeeAndDerateByInpatientNO(this.patientInfo.ID);
            if (ds == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѯ������С���ó���") + this.derateMgr.Err);
                return -1;
            }
            this.fpFeeInfo_FeeCode.DataAutoSizeColumns = false;
            this.fpDerateInfo_Sheet1.DataAutoHeadings = false;
            this.fpFeeInfo_FeeCode.DataSource = ds;
            return 1;
        }

        /// <summary>
        /// ��ȡ
        /// </summary>
        /// <returns></returns>
        protected virtual int GetFeeDetail()
        {
            DataSet ds = new DataSet();
            ds = this.derateMgr.GetFeeDetailByInpatientNO(this.patientInfo.ID);
            if (ds == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѯ���߷�����ϸ����") + this.derateMgr.Err);
                return -1;
            }
            this.fpFeeInfo_Items.DataAutoHeadings = false;
            this.fpFeeInfo_Items.DataAutoSizeColumns = false;

            this.fpFeeInfo_Items.DataSource = ds;
            return 1;
        }


        #endregion

        #region �������
        /// <summary>
        /// �����ܷ���
        /// </summary>
        /// <param name="isRate"></param>
        /// <param name="derateFee"></param>
        /// <param name="totOwnFee"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        protected int DerateTotoalFee(bool isRate, decimal derateFee, decimal totOwnFee, decimal rate)
        {


            if (!isRate) //�����
            {
                if (derateFee <= 0)
                {
                    MessageBox.Show(Language.Msg("��������ȷ�ļ�����(������)"));
                    this.ntxtFee.Focus();
                    return -1;
                }

                if (totOwnFee <= 0)
                {
                    MessageBox.Show(Language.Msg("�Էѽ���С�ڵ���0"));
                    return -1;
                }
                if (derateFee >= totOwnFee)
                {
                    MessageBox.Show(Language.Msg("������Ӧ��С�ڻ�����Է��ܶ�"));
                    this.ntxtFee.Focus();
                    return -1;
                }

                rate = FS.FrameWork.Public.String.FormatNumber(derateFee / totOwnFee, 4);
                this.ntxRate.Text = rate.ToString();
            }



            if (rate > 1 || rate < 0)
            {
                MessageBox.Show(Language.Msg("�����������ȷ,���ʵ"));
                return -1;
            }

            if (isRate)
            {
                derateFee = FS.FrameWork.Public.String.FormatNumber(totOwnFee * rate, 2);
            }
            //�Ѿ�����Ľ��
            decimal deratedFee = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_TotFee.Cells[0, 4].Text);
            if (totOwnFee <= deratedFee + derateFee)
            {
                MessageBox.Show(Language.Msg("����Ľ��ܴ��ڻ��ߵĻ����ܶ�"));
                return -1;
            }

            ArrayList al = new ArrayList();

            FS.HISFC.Models.Fee.DerateFee derateFeeObj = null;
            decimal sumTotCost = 0;
            for (int i = 0; i < this.fpFeeInfo_FeeCode.RowCount; i++)
            {
                derateFeeObj = new FS.HISFC.Models.Fee.DerateFee();
                derateFeeObj.InpatientNO = this.patientInfo.ID;
                derateFeeObj.DerateKind.ID = "0";
                derateFeeObj.DerateKind.Name = this.GetDerateKindName(derateFeeObj.DerateKind.ID);
                derateFeeObj.FeeCode = this.fpFeeInfo_FeeCode.Cells[i, 1].Text;
                derateFeeObj.FeeName = this.fpFeeInfo_FeeCode.Cells[i, 2].Text;
                derateFeeObj.ItemCode = this.fpFeeInfo_FeeCode.Cells[i, 1].Text;
                derateFeeObj.ItemName = this.fpFeeInfo_FeeCode.Cells[i, 2].Text;
                derateFeeObj.IsValid = true;
                derateFeeObj.DerateOper.Dept.ID = this.patientInfo.PVisit.PatientLocation.Dept.ID;//((FS.FrameWork.Management.Connection.Operator) as FS.HISFC.Models.Base.Employee).Dept.ID;
                derateFeeObj.DerateOper.Dept.Name = this.patientInfo.PVisit.PatientLocation.Dept.Name;
                derateFeeObj.DerateOper.ID = this.derateMgr.Operator.ID;
                derateFeeObj.DerateType = this.GetDerateType();
                decimal ownCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_FeeCode.Cells[i, 4].Text);

                if (!isRate) //���ǰ����������һ����ƽ
                {
                    if (i == this.fpFeeInfo_FeeCode.RowCount - 1)
                    {
                        derateFeeObj.DerateCost = derateFee - sumTotCost;
                    }
                    else
                    {
                        rate *= 10000;
                        decimal dc = rate - (int)rate;
                        if (dc > 0)
                        {
                            rate += 1;
                        }
                        rate = rate / 10000;

                        derateFeeObj.DerateCost = ownCost * rate;
                    }
                }
                else //�Ǳ������ձ�������
                {
                    derateFeeObj.DerateCost = FS.FrameWork.Public.String.FormatNumber(ownCost * rate, 2);
                }

                sumTotCost += derateFeeObj.DerateCost;

                if (sumTotCost > derateFee)
                {
                    derateFeeObj.DerateCost = derateFeeObj.DerateCost + sumTotCost - derateFee;
                }
                al.Add(derateFeeObj);
                this.SetAfterDerateMinFee(i, derateFeeObj.DerateCost);
                if (sumTotCost > derateFee)
                {
                    break;
                }

            }

            //����dataset
            this.InSertDataSet(al);

            //this.fpFeeInfo_TotFee.Cells[0, 1].Text = (totOwnFee - derateFee).ToString();
            this.fpFeeInfo_TotFee.Cells[0, 4].Text = (deratedFee + derateFee).ToString();

            //this.dtDerated.AcceptChanges();
            this.AddHasTable();
            return 1;
        }

        /// <summary>
        /// ������С���ü���
        /// </summary>
        /// <param name="isRate"></param>
        /// <param name="derateFee"></param>
        /// <param name="rate"></param>
        /// <param name="isAll"></param>
        /// <returns></returns>
        protected int DerateMinFee(bool isRate, decimal derateFee, decimal rate, bool isAll)
        {
            decimal leftCost = 0m;  //ʣ����
            bool isAllowDerate = true;
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Fee.DerateFee derateFeeObj = null;

            if (!isRate) //�����
            {
                if (derateFee <= 0)
                {
                    MessageBox.Show(Language.Msg("��������ȷ�ļ�����(������)"));
                    this.ntxtFee.Focus();
                    return -1;
                }
            }
            else
            {
                if (rate > 1 || rate < 0)
                {
                    MessageBox.Show(Language.Msg("�����������ȷ,���ʵ"));
                    return -1;
                }
            }

            decimal deratedFeeTotal = 0;
            if (isAll) //ȫѡ
            {

                for (int i = 0; i < this.fpFeeInfo_FeeCode.RowCount; i++)
                {
                    decimal deratedFee = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_FeeCode.Cells[i, 4].Text);
                    leftCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_FeeCode.Cells[i, 8].Text);

                    //�Ƿ��������
                    if (!isRate) //���Ǳ���
                    {
                        isAllowDerate = this.ValidDerateCost(derateFee, leftCost);

                    }
                    else //���ձ���
                    {
                        decimal ownCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_FeeCode.Cells[i, 4].Text);
                        leftCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_FeeCode.Cells[i, 8].Text);
                        derateFee = ownCost * rate;

                        isAllowDerate = this.ValidDerateCost(derateFee, leftCost);
                    }
                    if (!isAllowDerate)
                    {
                        MessageBox.Show(Language.Msg(this.fpFeeInfo_FeeCode.Cells[i, 2].Text + "�ɼ������!"));
                        return -1;
                    }
                }
                //��ʼ����
                for (int i = 0; i < this.fpFeeInfo_FeeCode.RowCount; i++)
                {
                    decimal deratedFee = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_FeeCode.Cells[i, 4].Text);
                    leftCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_FeeCode.Cells[i, 8].Text);

                    //�Ƿ��������
                    if (isRate) //���Ǳ���
                    {
                        decimal ownCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_FeeCode.Cells[i, 4].Text);
                        leftCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_FeeCode.Cells[i, 8].Text);
                        derateFee = ownCost * rate;
                    }

                    derateFeeObj = new FS.HISFC.Models.Fee.DerateFee();
                    derateFeeObj.InpatientNO = this.patientInfo.ID;
                    derateFeeObj.DerateKind.ID = "1";
                    derateFeeObj.DerateKind.Name = this.GetDerateKindName(derateFeeObj.DerateKind.ID);
                    derateFeeObj.FeeCode = this.fpFeeInfo_FeeCode.Cells[i, 1].Text;
                    derateFeeObj.FeeName = this.fpFeeInfo_FeeCode.Cells[i, 2].Text;
                    derateFeeObj.ItemCode = this.fpFeeInfo_FeeCode.Cells[i, 1].Text;
                    derateFeeObj.ItemName = this.fpFeeInfo_FeeCode.Cells[i, 2].Text;
                    derateFeeObj.DerateCost = derateFee;
                    derateFeeObj.IsValid = true;
                    derateFeeObj.DerateOper.Dept.ID = this.patientInfo.PVisit.PatientLocation.Dept.ID;//((FS.FrameWork.Management.Connection.Operator) as FS.HISFC.Models.Base.Employee).Dept.ID;
                    derateFeeObj.DerateType = this.GetDerateType();
                    derateFeeObj.DerateOper.Dept.Name = this.patientInfo.PVisit.PatientLocation.Dept.Name;
                    derateFeeObj.DerateOper.ID = this.derateMgr.Operator.ID;
                    //���ü������
                    this.SetAfterDerateMinFee(i, derateFee);
                    deratedFeeTotal += derateFee;

                    al.Add(derateFeeObj);

                }

                this.InSertDataSet(al);
                this.SetAtferDerateTotFee(deratedFeeTotal);
            }
            else //����
            {

                decimal ownCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_FeeCode.Cells[this.fpFeeInfo_FeeCode.ActiveRowIndex, 4].Text);
                leftCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_FeeCode.Cells[this.fpFeeInfo_FeeCode.ActiveRowIndex, 8].Text);
                if (isRate)
                {
                    derateFee = ownCost * rate;
                }

                isAllowDerate = this.ValidDerateCost(derateFee, leftCost);
                if (!isAllowDerate)
                {
                    MessageBox.Show(Language.Msg(this.fpFeeInfo_FeeCode.Cells[this.fpFeeInfo_FeeCode.ActiveRowIndex, 2].Text + "�Ŀɼ������"));
                    return -1;
                }

                derateFeeObj = new FS.HISFC.Models.Fee.DerateFee();
                derateFeeObj.InpatientNO = this.patientInfo.ID;
                derateFeeObj.DerateKind.ID = "1";
                derateFeeObj.DerateKind.Name = this.GetDerateKindName(derateFeeObj.DerateKind.ID);
                derateFeeObj.FeeCode = this.fpFeeInfo_FeeCode.Cells[this.fpFeeInfo_FeeCode.ActiveRowIndex, 1].Text;
                derateFeeObj.FeeName = this.fpFeeInfo_FeeCode.Cells[this.fpFeeInfo_FeeCode.ActiveRowIndex, 2].Text;
                derateFeeObj.ItemCode = this.fpFeeInfo_FeeCode.Cells[this.fpFeeInfo_FeeCode.ActiveRowIndex, 1].Text;
                derateFeeObj.ItemName = this.fpFeeInfo_FeeCode.Cells[this.fpFeeInfo_FeeCode.ActiveRowIndex, 2].Text;
                derateFeeObj.DerateCost = derateFee;
                derateFeeObj.IsValid = true;
                derateFeeObj.DerateOper.Dept.ID = this.patientInfo.PVisit.PatientLocation.Dept.ID;//((FS.FrameWork.Management.Connection.Operator) as FS.HISFC.Models.Base.Employee).Dept.ID;
                derateFeeObj.DerateType = this.GetDerateType();
                derateFeeObj.DerateOper.Dept.Name = this.patientInfo.PVisit.PatientLocation.Dept.Name;
                derateFeeObj.DerateOper.ID = this.derateMgr.Operator.ID;
                //���ü������
                this.SetAfterDerateMinFee(this.fpFeeInfo_FeeCode.ActiveRowIndex, derateFee);

                al.Add(derateFeeObj);
                this.InSertDataSet(al);
                this.SetAtferDerateTotFee(derateFee);
            }

            this.AddHasTable();
            return 1;
        }

        /// <summary>
        /// ������Ŀ����
        /// </summary>
        /// <param name="isRate"></param>
        /// <param name="derateFee"></param>
        /// <param name="rate"></param>
        /// <param name="isAll"></param>
        /// <returns></returns>
        protected int DerateFeeDetail(bool isRate, decimal derateFee, decimal rate, bool isAll)
        {
            Hashtable ht = new Hashtable();
            decimal leftCost = 0m;  //ʣ����
            bool isAllowDerate = true;
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Fee.DerateFee derateFeeObj = null;

            if (!isRate) //�����
            {
                if (derateFee <= 0)
                {
                    MessageBox.Show(Language.Msg("��������ȷ�ļ�����(������)"));
                    this.ntxtFee.Focus();
                    return -1;
                }
            }
            else
            {
                if (rate > 1 || rate < 0)
                {
                    MessageBox.Show(Language.Msg("�����������ȷ,���ʵ"));
                    return -1;
                }
            }

            decimal deratedFeeTotal = 0;
            if (isAll) //ȫѡ
            {
                for (int i = 0; i < this.fpFeeInfo_Items.RowCount; i++)
                {
                    string feeCode = this.fpFeeInfo_Items.Cells[i, 0].Text;
                    decimal deratedFee = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_Items.Cells[i, 6].Text);
                    leftCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_Items.Cells[i, 7].Text);

                    //�Ƿ��������
                    if (!isRate) //���Ǳ���
                    {
                        isAllowDerate = this.ValidDerateCost(derateFee, leftCost);

                    }
                    else //���ձ���
                    {
                        decimal ownCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_Items.Cells[i, 5].Text);
                        leftCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_Items.Cells[i, 7].Text);
                        derateFee = ownCost * rate;

                        isAllowDerate = this.ValidDerateCost(derateFee, leftCost);
                    }
                    if (!isAllowDerate)
                    {
                        MessageBox.Show(Language.Msg(this.fpFeeInfo_Items.Cells[i, 2].Text + "�Ŀɼ������"));
                        this.AddHasTable();
                        return -1;
                    }

                    bool isExist = this.minFeeHastable.ContainsKey(feeCode);

                    if (isExist)
                    {
                        decimal myLeftcost = FS.FrameWork.Function.NConvert.ToDecimal(minFeeHastable[feeCode]);
                        if (myLeftcost <= derateFee)
                        {
                            MessageBox.Show(Language.Msg(this.fpFeeInfo_Items.Cells[i, 1].Text + "���С�ڼ�����"));
                            this.AddHasTable();
                            return -1;
                        }
                        else
                        {
                            minFeeHastable[feeCode] = myLeftcost - derateFee;
                        }
                    }

                }
                //��ʼ����
                for (int i = 0; i < this.fpFeeInfo_Items.RowCount; i++)
                {
                    decimal deratedFee = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_Items.Cells[i, 6].Text);
                    leftCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_Items.Cells[i, 7].Text);

                    //�Ƿ��������
                    if (isRate) //���Ǳ���
                    {
                        decimal ownCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_Items.Cells[i, 5].Text);
                        leftCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_Items.Cells[i, 7].Text);
                        derateFee = ownCost * rate;
                    }

                    if (!isAllowDerate)
                    {
                        MessageBox.Show(Language.Msg(this.fpFeeInfo_Items.Cells[i, 1].Text + "�Ŀɼ������"));
                        this.AddHasTable();
                        return -1;
                    }

                    derateFeeObj = new FS.HISFC.Models.Fee.DerateFee();
                    derateFeeObj.InpatientNO = this.patientInfo.ID;
                    derateFeeObj.DerateKind.ID = "2";
                    derateFeeObj.DerateKind.Name = this.GetDerateKindName(derateFeeObj.DerateKind.ID);
                    derateFeeObj.FeeCode = this.fpFeeInfo_Items.Cells[i, 0].Text;
                    derateFeeObj.FeeName = this.fpFeeInfo_Items.Cells[i, 1].Text;
                    derateFeeObj.DerateCost = derateFee;
                    derateFeeObj.ItemCode = this.fpFeeInfo_Items.Cells[i, 2].Text;
                    derateFeeObj.ItemName = this.fpFeeInfo_Items.Cells[i, 3].Text;
                    derateFeeObj.IsValid = true;
                    derateFeeObj.DerateOper.Dept.ID = this.patientInfo.PVisit.PatientLocation.Dept.ID;//((FS.FrameWork.Management.Connection.Operator) as FS.HISFC.Models.Base.Employee).Dept.ID;
                    derateFeeObj.DerateType = this.GetDerateType();
                    derateFeeObj.DerateOper.Dept.Name = this.patientInfo.PVisit.PatientLocation.Dept.Name;
                    derateFeeObj.DerateOper.ID = this.derateMgr.Operator.ID;
                    //���ü������

                    int row = this.GetRowIndex(derateFeeObj.FeeCode);

                    if (row < 0)
                    {
                        MessageBox.Show(Language.Msg("��С������Ϣ��û��" + derateFeeObj.FeeName + "��Ӧ����"));
                        return -1;
                    }

                    this.SetAfterDerateMinFee(row, derateFee);

                    this.SetAfterDerateFeeDetail(i, derateFee);
                    deratedFeeTotal += derateFee;

                    al.Add(derateFeeObj);

                }

                this.InSertDataSet(al);
                this.SetAtferDerateTotFee(deratedFeeTotal);
            }
            else //����
            {

                decimal ownCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_Items.Cells[this.fpFeeInfo_Items.ActiveRowIndex, 5].Text);
                leftCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_Items.Cells[this.fpFeeInfo_Items.ActiveRowIndex, 7].Text);
                if (isRate)
                {
                    derateFee = ownCost * rate;
                }

                if (leftCost < derateFee)
                {
                    MessageBox.Show(Language.Msg("������ܴ������"));
                    return -1;
                }

                derateFeeObj = new FS.HISFC.Models.Fee.DerateFee();
                derateFeeObj.InpatientNO = this.patientInfo.ID;
                derateFeeObj.DerateKind.ID = "2";
                derateFeeObj.DerateKind.Name = this.GetDerateKindName(derateFeeObj.DerateKind.ID);
                derateFeeObj.FeeCode = this.fpFeeInfo_Items.Cells[this.fpFeeInfo_Items.ActiveRowIndex, 0].Text;
                derateFeeObj.FeeName = this.fpFeeInfo_Items.Cells[this.fpFeeInfo_Items.ActiveRowIndex, 1].Text;
                derateFeeObj.ItemCode = this.fpFeeInfo_Items.Cells[this.fpFeeInfo_Items.ActiveRowIndex, 2].Text;
                derateFeeObj.ItemName = this.fpFeeInfo_Items.Cells[this.fpFeeInfo_Items.ActiveRowIndex, 3].Text;
                derateFeeObj.DerateCost = derateFee;
                derateFeeObj.IsValid = true;
                derateFeeObj.DerateOper.Dept.ID = this.patientInfo.PVisit.PatientLocation.Dept.ID;//((FS.FrameWork.Management.Connection.Operator) as FS.HISFC.Models.Base.Employee).Dept.ID;
                derateFeeObj.DerateType = this.GetDerateType();
                derateFeeObj.DerateOper.Dept.Name = this.patientInfo.PVisit.PatientLocation.Dept.Name;
                derateFeeObj.DerateOper.ID = this.derateMgr.Operator.ID;
                //���ü������
                //this.setAfterDerateMinFee(this.fpFeeInfo_Items.ActiveRowIndex, derateFee);

                al.Add(derateFeeObj);
                this.InSertDataSet(al);
                int row = this.GetRowIndex(derateFeeObj.FeeCode);

                if (row < 0)
                {
                    MessageBox.Show(Language.Msg("��С������Ϣ��û��" + derateFeeObj.FeeName + "��Ӧ����"));
                    return -1;
                }

                this.SetAfterDerateMinFee(row, derateFee);


                this.SetAfterDerateFeeDetail(this.fpFeeInfo_Items.ActiveRowIndex, derateFee);

                this.SetAtferDerateTotFee(derateFee);
            }

            return 1;
        }

        #endregion

        #region ȡ������

        /// <summary>
        /// ȫ��ȡ������
        /// </summary>
        /// <returns></returns>
        protected int CancelDerateAll()
        {
            for (int i = this.fpDerateInfo_Sheet1.RowCount - 1; i >= 0; i--)
            {
                this.CancelDerateOne(i);
            }
            return 1;

        }

        /// <summary>
        /// ����ȡ������
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        protected int CancelDerateOne(int row)
        {
            if (row < 0)
            {
                MessageBox.Show(Language.Msg("��ѡ������¼"));
            }

            FS.HISFC.Models.Fee.DerateFee derateFeeObj = new FS.HISFC.Models.Fee.DerateFee();
            derateFeeObj.DerateCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpDerateInfo_Sheet1.Cells[row, 8].Text);
            derateFeeObj.FeeCode = this.fpDerateInfo_Sheet1.Cells[row, 6].Text;
            derateFeeObj.FeeName = this.fpDerateInfo_Sheet1.Cells[row, 7].Text;
            derateFeeObj.ItemCode = this.fpDerateInfo_Sheet1.Cells[row, 18].Text;
            derateFeeObj.ItemName = this.fpDerateInfo_Sheet1.Cells[row, 19].Text;
            derateFeeObj.DerateKind.ID = this.fpDerateInfo_Sheet1.Cells[row, 2].Text;
            derateFeeObj.HappenNO = FS.FrameWork.Function.NConvert.ToInt32(this.fpDerateInfo_Sheet1.Cells[row, 1].Text);
            derateFeeObj.InpatientNO = this.fpDerateInfo_Sheet1.Cells[row, 0].Text;
            derateFeeObj.DerateOper.ID = this.fpDerateInfo_Sheet1.Cells[row, 16].Text;
            derateFeeObj.DerateOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.fpDerateInfo_Sheet1.Cells[row, 17].Text);
            derateFeeObj.DerateType.ID = this.fpDerateInfo_Sheet1.Cells[row, 4].Text;
            derateFeeObj.BalanceState = "0";
            derateFeeObj.DerateOper.Dept = ((FS.FrameWork.Management.Connection.Operator) as FS.HISFC.Models.Base.Employee).Dept;
            if (derateFeeObj.DerateKind.ID == "2")
            {
                int rowItemsIndex = this.GetRowItemsIndex(derateFeeObj.ItemCode);
                if (rowItemsIndex < 0)
                {
                    MessageBox.Show(Language.Msg("��Ŀ��Ϣ��û��" + derateFeeObj.ItemName + "��Ӧ����"));
                    return -1;
                }
                this.SetAfterDerateFeeDetail(rowItemsIndex, -derateFeeObj.DerateCost);
            }


            int rowIndex = this.GetRowIndex(derateFeeObj.FeeCode);

            if (row < 0)
            {
                MessageBox.Show(Language.Msg("��С������Ϣ��û��" + derateFeeObj.FeeName + "��Ӧ����"));
                return -1;
            }

            this.SetAfterDerateMinFee(rowIndex, -derateFeeObj.DerateCost);
            this.SetAtferDerateTotFee(-derateFeeObj.DerateCost);
            this.fpDerateInfo_Sheet1.RemoveRows(row, 1);

            //
            if (derateFeeObj.HappenNO != 0)
            {
                alDelete.Add(derateFeeObj);
            }

            return 1;
        }


        #endregion

        #region У��
        protected int ValidDerateType()
        {
            if (this.cmbDerateType.Tag == null || this.cmbDerateType.Tag.ToString() == "" || this.cmbDerateType.SelectedItem == null)
            {
                MessageBox.Show("��ѡ���������");
                this.cmbDerateType.Focus();
                return -1;
            }
            return 1;
        }
        #endregion

        #region ����

        protected override int OnSave(object sender, object neuObject)
        {
            this.fpDerateInfo.StopCellEditing();
            DataTable dtAdd = this.dtDerated.GetChanges(DataRowState.Added);
            DataTable dtDelete = this.dtDerated.GetChanges(DataRowState.Deleted);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.derateMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            this.feeInpatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            int returnValue = 1;
            DateTime operDateTime = FS.FrameWork.Function.NConvert.ToDateTime(this.derateMgr.GetSysDateTime());
            if (dtAdd != null)
            {
                ArrayList alAdd = this.GetChanges(dtAdd);

                foreach (FS.HISFC.Models.Fee.DerateFee derateFeeObj in alAdd)
                {
                    derateFeeObj.HappenNO = this.derateMgr.GetHappenNO(derateFeeObj.InpatientNO);
                    derateFeeObj.DerateOper.ID = this.derateMgr.Operator.ID;
                    derateFeeObj.DerateOper.OperTime = operDateTime;

                    returnValue = this.derateMgr.InsertDerateFeeInfo(derateFeeObj);
                    if (returnValue < 0)
                    {
                        MessageBox.Show(Language.Msg(this.derateMgr.Err));
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                }
            }
            //ȡ������
            if (this.alDelete != null && this.alDelete.Count > 0)
            {
                foreach (FS.HISFC.Models.Fee.DerateFee derateFeeObj in this.alDelete)
                {
                    derateFeeObj.CancelDerateOper.ID = this.derateMgr.Operator.ID;
                    derateFeeObj.CancelDerateOper.OperTime = operDateTime;
                    derateFeeObj.IsValid = false;
                    returnValue = this.derateMgr.UpdateDerateFeeInfo(derateFeeObj);
                    if (returnValue < 0)
                    {
                        MessageBox.Show(Language.Msg(this.derateMgr.Err));
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                }

            }
            returnValue = this.feeInpatient.OpenAccount(this.patientInfo.ID);
            {
                if (returnValue == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.Msg("����ʧ�ܷ���ʧ��" + this.feeInpatient.Err, 211);
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            this.dtDerated.AcceptChanges();

            MessageBox.Show(Language.Msg("����ɹ�"));

            //this.ResetFp();

            //this.GetTotCost();
            //this.GetMinfee();
            //this.GetFeeDetail();
            //this.GetDeratedFeeInfo();
            //this.alDelete.Clear();

            this.Clear();

            return base.OnSave(sender, neuObject);
        }

        public override int Exit(object sender, object neuObject)
        {
            if (this.patientInfo == null)
            {
                return base.Exit(sender, neuObject);
            }
            else
            {
                if (this.patientInfo.ID == null || this.patientInfo.ID.Trim() == "") return base.Exit(sender, neuObject);
            }

            if (this.feeInpatient.OpenAccount(this.patientInfo.ID) == -1)
            {
                MessageBox.Show("����ʧ��" + this.feeInpatient.Err);
                return -1;
            }
            return base.Exit(sender, neuObject);
        }
        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        protected virtual int Init()
        {
            this.rbCost.Checked = true;
            this.ntxtFee.Enabled = true;

            this.ntxRate.Enabled = false;

            this.InitDataSetDerated();

            this.InitControl();

            return 1;
        }

        /// <summary>
        /// ��ʼ���ؼ���Ϣ
        /// </summary>
        /// <returns></returns>
        protected virtual int InitControl()
        {
            ArrayList alDerateTypeList = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.DERATEFEETYPE);
            this.cmbDerateType.AddItems(alDerateTypeList);

            for (int i = 0; i < this.fpDerateInfo_Sheet1.Columns.Count; i++)
            {
                this.fpDerateInfo_Sheet1.Columns[i].Locked = true;
            }

            for (int i = 0; i < this.fpFeeInfo_TotFee.Columns.Count; i++)
            {
                this.fpFeeInfo_TotFee.Columns[i].Locked = true;
            }
            for (int i = 0; i < this.fpFeeInfo_FeeCode.Columns.Count; i++)
            {
                this.fpFeeInfo_FeeCode.Columns[i].Locked = true;
            }
            for (int i = 0; i < this.fpFeeInfo_Items.Columns.Count; i++)
            {
                this.fpFeeInfo_Items.Columns[i].Locked = true;
            }
            return 1;
        }

        #endregion

        /// <summary>
        /// ����
        /// </summary>
        protected virtual void Clear()
        {
            //���߻�����Ϣ
            this.ucInpatientInfo1.Clear();

            //
            this.ucQueryInpatientNo1.Focus();

            //���fp
            this.ResetFp();

            this.alDelete.Clear();
            if (this.minFeeHastable != null)
            {
                this.minFeeHastable.Clear();
            }
            if (this.patientInfo != null || patientInfo.ID == "")
            {
                //����


                if (this.feeInpatient.OpenAccount(this.patientInfo.ID) == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.Msg("����ʧ��" + this.feeInpatient.Err, 211);
                    return;
                }
            }
            return;
        }

        /// <summary>
        /// �������
        /// </summary>
        protected virtual void ResetFp()
        {
            //���fpFeeInfo_TotFee
            int count = this.fpFeeInfo_TotFee.RowCount;
            if (count > 0)
            {
                this.fpFeeInfo_TotFee.RemoveRows(0, count);
            }

            this.dtDerated.Clear();

            if (this.fpFeeInfo_Items.RowCount > 0)
            {
                this.fpFeeInfo_Items.RemoveRows(0, this.fpFeeInfo_Items.RowCount);
            }
            if (this.fpFeeInfo_FeeCode.RowCount > 0)
            {
                this.fpFeeInfo_FeeCode.RemoveRows(0, this.fpFeeInfo_FeeCode.RowCount);
            }
            if (this.fpFeeInfo_TotFee.RowCount > 0)
            {
                this.fpFeeInfo_TotFee.RemoveRows(0, this.fpFeeInfo_TotFee.RowCount);
            }


        }
        #endregion

        #region �¼�
        private void ucQueryInpatientNo1_myEvent()
        {
            //this.dtDerated.Clear();
            this.Clear();

            if (string.IsNullOrEmpty(this.ucQueryInpatientNo1.InpatientNo))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("û�иû�����Ϣ"));
                this.ucQueryInpatientNo1.Focus();
                return;
            }

            this.patientInfo = this.radtIntegrate.GetPatientInfomation(this.ucQueryInpatientNo1.InpatientNo);

            if (this.patientInfo == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg(this.radtIntegrate.Err));
                this.ucQueryInpatientNo1.Focus();
                return;
            }

            //�жϷ���
            if ((this.feeInpatient.GetStopAccount(this.patientInfo.ID)) == "1")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("�û��ߴ��ڷ���״̬,�������ڽ���,���Ժ������˲���!", 111);
                this.ucQueryInpatientNo1.Focus();
                return;
            }

            if (this.patientInfo.PVisit.InState.ID.ToString() == FS.HISFC.Models.Base.EnumInState.N.ToString())
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�û��ߴ����޷���Ժ״̬,���ܽ��з��ü���"));
                this.ucQueryInpatientNo1.Focus();
                return;
            }

            if (this.patientInfo.PVisit.InState.ID.ToString() == FS.HISFC.Models.Base.EnumInState.O.ToString())
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�û����Ѿ�����,���ܽ��з��ü���"));
                this.ucQueryInpatientNo1.Focus();
                return;
            }

            //����

            int returnValue = this.feeInpatient.CloseAccount(this.patientInfo.ID);
            {
                if (returnValue == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.Msg("����ʧ��" + this.feeInpatient.Err, 211);
                    return;
                }
            }

            //���渳ֵ
            this.ucInpatientInfo1.PatientInfoObj = patientInfo;

            this.ResetFp();

            //ȡ�ܷ���
            returnValue = this.GetTotCost();
            if (returnValue == -1)
            {
                this.ucQueryInpatientNo1.Focus();
                return;
            }

            //��С����
            returnValue = this.GetMinfee();
            if (returnValue == -1)
            {
                this.ucQueryInpatientNo1.Focus();
                return;
            }

            //������Ŀ
            returnValue = this.GetFeeDetail();
            if (returnValue == -1)
            {
                this.ucQueryInpatientNo1.Focus();
                return;
            }

            returnValue = this.GetDeratedFeeInfo();
            if (returnValue < 0)
            {
                this.ucQueryInpatientNo1.Focus();
                return;
            }

            this.AddHasTable();

            //���ý���
            if (this.rbCost.Checked)
            {
                this.ntxtFee.Focus();
            }

            if (this.rbRate.Checked)
            {
                this.ntxRate.Focus();
            }



        }

        private void rbCost_CheckedChanged(object sender, EventArgs e)
        {
            this.ntxtFee.Enabled = this.rbCost.Checked;
            this.ntxtFee.Focus();

        }

        private void rbRate_CheckedChanged(object sender, EventArgs e)
        {
            this.ntxRate.Enabled = this.rbRate.Checked;
            this.ntxRate.Focus();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() == "DEVENV")
            {
                return;
            }
            //��ʼ��
            this.Init();
            ToolTip tip = new ToolTip();
            tip.SetToolTip(this.btCancelAll, "ȡ��ȫ�������¼");
            tip = new ToolTip();
            tip.SetToolTip(this.btCancelOne, "ȡ��ѡ�м����¼");
            tip = new ToolTip();
            tip.SetToolTip(this.btDerateAll, "���������¼");
            tip = new ToolTip();
            tip.SetToolTip(this.btDerateOne, "����ѡ�м�¼");
            base.OnLoad(e);
        }

        private void btDerateOne_Click(object sender, EventArgs e)
        {
            int returnValue = this.ValidDerateType();
            if (returnValue < 0)
            {
                return;
            }

            if (this.fpFeeInfo.ActiveSheetIndex == 0)
            {
                decimal totOwncost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_TotFee.Cells[0, 1].Text);
                this.DerateTotoalFee(this.rbRate.Checked, FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtFee.Text), totOwncost, FS.FrameWork.Function.NConvert.ToDecimal(this.ntxRate.Text));
            }
            if (this.fpFeeInfo.ActiveSheetIndex == 1)
            {
                this.DerateMinFee(this.rbRate.Checked, FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtFee.Text), FS.FrameWork.Function.NConvert.ToDecimal(this.ntxRate.Text), false);
            }
            if (this.fpFeeInfo.ActiveSheetIndex == 2)
            {
                this.DerateFeeDetail(this.rbRate.Checked, FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtFee.Text), FS.FrameWork.Function.NConvert.ToDecimal(this.ntxRate.Text), false);
            }
        }

        private void btDerateAll_Click(object sender, EventArgs e)
        {
            int returnValue = this.ValidDerateType();
            if (returnValue < 0)
            {
                return;
            }
            if (this.fpFeeInfo.ActiveSheetIndex == 0)
            {
                decimal totOwncost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeInfo_TotFee.Cells[0, 1].Text);
                this.DerateTotoalFee(this.rbRate.Checked, FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtFee.Text), totOwncost, FS.FrameWork.Function.NConvert.ToDecimal(this.ntxRate.Text));
            }
            if (this.fpFeeInfo.ActiveSheetIndex == 1)
            {
                this.DerateMinFee(this.rbRate.Checked, FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtFee.Text), FS.FrameWork.Function.NConvert.ToDecimal(this.ntxRate.Text), true);
            }
            if (this.fpFeeInfo.ActiveSheetIndex == 2)
            {
                this.DerateFeeDetail(this.rbRate.Checked, FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtFee.Text), FS.FrameWork.Function.NConvert.ToDecimal(this.ntxRate.Text), true);
            }
        }

        private void btCancelOne_Click(object sender, EventArgs e)
        {
            //int returnValue = this.ValidDerateType();
            //if (returnValue < 0)
            //{
            //    return;
            //}
            if (this.fpDerateInfo_Sheet1.RowCount <= 0)
            {
                MessageBox.Show(Language.Msg("û�м����¼"));
                return;
            }
            this.CancelDerateOne(this.fpDerateInfo_Sheet1.ActiveRowIndex);

        }

        private void btCancelAll_Click(object sender, EventArgs e)
        {
            //int returnValue = this.ValidDerateType();
            //if (returnValue < 0)
            //{
            //    return;
            //}
            if (this.fpDerateInfo_Sheet1.RowCount <= 0)
            {
                MessageBox.Show(Language.Msg("û�м����¼"));
                return;
            }
            this.CancelDerateAll();
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            tooBarService.AddToolButton("����", "����", FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);

            this.FindForm().FormClosing += new FormClosingEventHandler(ucInpatientDerateFee_FormClosing);

            return this.tooBarService;
        }

        void ucInpatientDerateFee_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.patientInfo == null)
            {
                return;
            }
            else
            {
                if (this.patientInfo.ID == null || this.patientInfo.ID.Trim() == "")
                {
                    return;
                }
            }

            if (this.feeInpatient.OpenAccount(this.patientInfo.ID) == -1)
            {
                MessageBox.Show("����ʧ��" + this.feeInpatient.Err);

                return;
            }

            return;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    {
                        this.Clear();
                        break;
                    }
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        private void ntxtFee_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cmbDerateType.Focus();
            }
        }

        private void ntxRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cmbDerateType.Focus();
            }
        }

        #endregion


    }

}
