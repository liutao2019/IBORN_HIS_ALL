using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Fee.Inpatient;

namespace FS.HISFC.Components.Terminal.Confirm
{
    /// <summary>
    /// ucCancelInpatientConfirm <br></br>
    /// [��������: סԺ�ն�ȷ��ȡ��]<br></br>
    /// [�� �� ��: ���S]<br></br>
    /// [����ʱ��: 2008-07-11]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucCancelInpatientConfirm : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCancelInpatientConfirm()
        {
            InitializeComponent();

            this.neuSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread1_ColumnWidthChanged);

            this.fpSpread1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpSpread1_ColumnWidthChanged);
        }

        void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1, this.cancelFilePath);
        }

        #region ����

        /// <summary>
        /// ����ҵ��
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeManager = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ҽ��ҵ��
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Order orderManager = new FS.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// ����ҵ��
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtManager = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// �ն�ȷ��ҵ��
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Terminal.Confirm confirmIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Confirm();

        /// <summary>
        /// ϵͳ����ҵ��
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager deptManager = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// �ն�ҵ��
        /// </summary>
        private FS.HISFC.BizLogic.Terminal.TerminalConfirm tecManager = new FS.HISFC.BizLogic.Terminal.TerminalConfirm();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// ��ǰ����Ա
        /// </summary>
        private FS.HISFC.Models.Base.Employee oper = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

        private FS.HISFC.BizProcess.Integrate.Terminal.Result result = FS.HISFC.BizProcess.Integrate.Terminal.Result.Failure;
        private bool seeAll = false;

        private string filePath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @"\Profile\CancelInpatientConfirm.xml";

        private string cancelFilePath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @"\Profile\CancelInpatientConfirmed.xml";

        #endregion

        #region ����
        /// <summary>
        /// �鿴���п����ն�ȷ����Ŀ
        /// </summary>
        [Category("�ؼ�����"), Description("�鿴���п����ն�ȷ����Ŀ")]
        public bool SeeAll
        {
            get
            {
                return seeAll;
            }
            set
            {
                seeAll = value;
            }
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            set
            {
                this.patientInfo = value;
                this.txtName.Text = this.patientInfo.Name;
                this.txtPact.Text = this.patientInfo.Pact.Name;
                this.AddDataToFp(this.QueryExeData(this.patientInfo.ID));
            }
        }

        [Category("�ؼ�����"), Description("סԺ�������Ĭ������0סԺ�ţ�5����")]
        public int DefaultInput
        {
            get
            {
                return this.ucQueryInpatientNo1.DefaultInputType;
            }
            set
            {
                this.ucQueryInpatientNo1.DefaultInputType = value;
            }
        }


        [Category("�ؼ�����"), Description("�����Ų�ѯ������Ϣʱ�������ߵ�״̬��ѯ�����ȫ����'ALL'")]
        public string PatientInState
        {
            get
            {
                return this.ucQueryInpatientNo1.PatientInState;
            }
            set
            {
                this.ucQueryInpatientNo1.PatientInState = value;
            }
        }

        [Category("�ؼ�����"), Description("��סԺ�Ų�ѯ������Ϣʱ�������ߵ�״̬��ѯ�����ȫ����'ALL'")]
        public FS.HISFC.Components.Common.Controls.enuShowState ShowState
        {
            get
            {
                return this.ucQueryInpatientNo1.ShowState;
            }
            set
            {
                this.ucQueryInpatientNo1.ShowState = value;
            }
        }

        private EnumQuitFeeType quitFeeType = EnumQuitFeeType.QuitApply;
        [Category("�ؼ�����"), Description("ȡ���ն�ȷ��ʱ���˷ѵ���ʽ��UpdateQutiFeeQty ֻ���¿�������,QuitApply �γ��˷�����,QuitFee ֱ���˷�")]
        public EnumQuitFeeType QuitFeeType
        {
            get
            {
                return quitFeeType;
            }
            set
            {
                quitFeeType = value;
            }
        }
        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected virtual void Init()
        {
            //this.neuSpread1_Sheet1.Columns[0].Visible = false;
            //this.neuSpread1_Sheet1.Columns[1].Visible = false;
            //this.neuSpread1_Sheet1.Columns[6].Visible = false;
            //this.neuSpread1_Sheet1.Columns[7].Visible = false;
            for (int i = 0; i < this.fpSpread1_Sheet1.ColumnCount; i++)
            {
                //ȡ�������������޸�
                //if (i != 5)
                //{
                this.fpSpread1_Sheet1.Columns[i].Locked = true;
                //}
            }
            this.ucQueryInpatientNo1.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.ucQueryInpatientNo1_myEvent);

            if (System.IO.File.Exists(this.filePath))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1_Sheet1, this.filePath);
            }

            if (System.IO.File.Exists(this.cancelFilePath))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1, this.cancelFilePath);
            }
        }

        void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.filePath);
        }

        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// <returns></returns>
        private ArrayList QueryExeData(string inpatientNO)
        {
            string operDept = this.oper.Dept.ID;
            if (seeAll)
            {
                operDept = "all";
            }

            ArrayList al = new ArrayList();
            
            result = this.confirmIntegrate.QueryConfirmInfoByInpatientNO(inpatientNO, operDept, ref al);
            if (result != FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
            {
                MessageBox.Show("���һ����ն�ȷ����Ϣʧ��!" + tecManager.Err);
                this.ucQueryInpatientNo1.Focus();

                return null;
            }

            return al;
        }

        /// <summary>
        /// ������ݵ����
        /// </summary>
        /// <param name="al"></param>
        protected virtual void AddDataToFp(ArrayList al)
        {
            #region ����������Ŀ{5C2A9C83-D165-434c-ACA4-86F23E956442}
            List<string> combIDList = new List<string>();
            #endregion

            this.neuSpread1_Sheet1.RowCount = 0;
            this.fpSpread1_Sheet1.RowCount = 0;

            if (al != null && al.Count > 0)
            {
                foreach (FS.HISFC.Models.Terminal.TerminalConfirmDetail confirmDetail in al)
                {
                    #region ����������Ŀ{5C2A9C83-D165-434c-ACA4-86F23E956442}
                    int rowIndex = 0;
                    bool isComb = false;
                    FS.HISFC.Models.Fee.Inpatient.FeeItemList itemList = this.feeManager.GetItemListByRecipeNO(confirmDetail.Apply.Item.RecipeNO, confirmDetail.Apply.Item.SequenceNO, FS.HISFC.Models.Base.EnumItemType.UnDrug);
                    if (itemList != null)
                    {
                        if (!string.IsNullOrEmpty(itemList.UndrugComb.ID))
                        {
                            isComb = true;
                            if (!combIDList.Contains(itemList.UndrugComb.ID + confirmDetail.ExecMoOrder))
                            {
                                combIDList.Add(itemList.UndrugComb.ID + confirmDetail.ExecMoOrder);
                                this.neuSpread1_Sheet1.Rows.Add(0, 1);
                                this.neuSpread1_Sheet1.RowHeader.Cells[0, 0].Text = "+";
                                this.neuSpread1_Sheet1.RowHeader.Cells[0, 0].BackColor = Color.YellowGreen;
                                this.neuSpread1_Sheet1.Rows[0].BackColor = Color.LightBlue;
                                this.neuSpread1_Sheet1.Cells[0, 0].Text = confirmDetail.MoOrder;//ҽ����
                                this.neuSpread1_Sheet1.Cells[0, 1].Text = confirmDetail.ExecMoOrder;//ҽ��ִ�к�
                                this.neuSpread1_Sheet1.Cells[0, 3].Text = itemList.UndrugComb.ID;//��Ŀ����
                                this.neuSpread1_Sheet1.Cells[0, 4].Text = itemList.UndrugComb.Name;//��Ŀ����
                                this.neuSpread1_Sheet1.Cells[0, 5].Text = confirmDetail.Apply.Item.ConfirmedQty.ToString();//��ȷ������
                                this.neuSpread1_Sheet1.Cells[0, 6].Text = this.GetPriceUnit(itemList.UndrugComb.ID);
                                FS.HISFC.Models.Base.Employee tmpEmp = this.deptManager.GetEmployeeInfo(confirmDetail.Apply.Item.ConfirmOper.ID);
                                this.neuSpread1_Sheet1.Cells[0, 7].Text = tmpEmp.Name;
                                FS.HISFC.Models.Base.Department tmpDept = this.deptManager.GetDepartment(confirmDetail.Apply.ConfirmOperEnvironment.Dept.ID);//ȷ�Ͽ���
                                this.neuSpread1_Sheet1.Cells[0, 8].Tag = tmpDept.ID;
                                this.neuSpread1_Sheet1.Cells[0, 8].Text = tmpDept.Name;
                                this.neuSpread1_Sheet1.Cells[0, 9].Text = confirmDetail.Apply.ConfirmOperEnvironment.OperTime.ToString();//����ʱ��                                

                                this.neuSpread1_Sheet1.Rows[0].ForeColor = confirmDetail.Status.ID.Equals("2") ? Color.Black : confirmDetail.Status.ID.Equals("1") ? Color.Red : Color.Blue;
                                rowIndex++;
                            }
                            else
                            {
                                //�������׽ڵ�
                                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                                {
                                    if (this.neuSpread1_Sheet1.Cells[i, 2].Text == "" && this.neuSpread1_Sheet1.Cells[i, 1].Text == confirmDetail.ExecMoOrder && this.neuSpread1_Sheet1.Cells[i, 3].Text == itemList.UndrugComb.ID)
                                    {
                                        rowIndex = i + 1;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    //this.neuSpread1_Sheet1.Rows.Add(0, 1);
                    //{5C2A9C83-D165-434c-ACA4-86F23E956442}
                    this.neuSpread1_Sheet1.Rows.Add(rowIndex, 1);


                    if (isComb)
                    {
                        this.neuSpread1_Sheet1.RowHeader.Cells[rowIndex, 0].BackColor = Color.Yellow;
                        this.neuSpread1_Sheet1.Rows[rowIndex].BackColor = Color.LightYellow;
                        this.neuSpread1_Sheet1.RowHeader.Cells[rowIndex, 0].Text = ".";
                        this.neuSpread1_Sheet1.Rows[rowIndex].Visible = false;
                    }

                    FS.HISFC.Models.Base.Employee employee = new FS.HISFC.Models.Base.Employee();
                    FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();

                    this.neuSpread1_Sheet1.Cells[rowIndex, 0].Text = confirmDetail.MoOrder;//ҽ����
                    this.neuSpread1_Sheet1.Cells[rowIndex, 1].Text = confirmDetail.ExecMoOrder;//ҽ��ִ�к�
                    this.neuSpread1_Sheet1.Cells[rowIndex, 2].Text = confirmDetail.Sequence;//��ˮ��  ����
                    this.neuSpread1_Sheet1.Cells[rowIndex, 3].Text = confirmDetail.Apply.Item.ID;//��Ŀ����
                    this.neuSpread1_Sheet1.Cells[rowIndex, 4].Text = confirmDetail.Apply.Item.Name;//��Ŀ����
                    this.neuSpread1_Sheet1.Cells[rowIndex, 5].Text = confirmDetail.Apply.Item.ConfirmedQty.ToString();//��ȷ������
                    this.neuSpread1_Sheet1.Cells[rowIndex, 6].Text = this.GetPriceUnit(confirmDetail.Apply.Item.ID);//��λ
                    this.neuSpread1_Sheet1.Cells[rowIndex, 7].Tag = confirmDetail.Apply.Item.ConfirmOper.ID;//ȷ����
                    employee = this.deptManager.GetEmployeeInfo(confirmDetail.Apply.Item.ConfirmOper.ID);
                    this.neuSpread1_Sheet1.Cells[rowIndex, 7].Text = employee.Name;
                    dept = this.deptManager.GetDepartment(confirmDetail.Apply.ConfirmOperEnvironment.Dept.ID);//ȷ�Ͽ���
                    this.neuSpread1_Sheet1.Cells[rowIndex, 8].Tag = dept.ID;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 8].Text = dept.Name;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 9].Text = confirmDetail.Apply.ConfirmOperEnvironment.OperTime.ToString();//����ʱ��
                    this.neuSpread1_Sheet1.Cells[rowIndex, 10].Text = confirmDetail.Apply.Item.RecipeNO;//������
                    this.neuSpread1_Sheet1.Cells[rowIndex, 11].Text = confirmDetail.Apply.Item.SequenceNO.ToString();//��������ˮ��
                    this.neuSpread1_Sheet1.Cells[rowIndex, 12].Text = confirmDetail.ExecDevice;//ִ���豸
                    this.neuSpread1_Sheet1.Cells[rowIndex, 13].Text = confirmDetail.Oper.ID;//ִ�м�ʦ

                    if (itemList != null)
                    {
                        this.neuSpread1_Sheet1.Cells[rowIndex, 14].Text = itemList.FT.TotCost.ToString();//���
                    }

                    this.neuSpread1_Sheet1.Rows[rowIndex].ForeColor = confirmDetail.Status.ID.Equals("2") ? Color.Black : confirmDetail.Status.ID.Equals("1") ? Color.Red : Color.Blue;

                    this.neuSpread1_Sheet1.Rows[rowIndex].Tag = confirmDetail;
                }
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        private int Save()
        {
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                return 0;
            }
            if (this.fpSpread1_Sheet1.RowCount <= 0)
            {
                MessageBox.Show("��ѡ����ȡ������ϸ");
                return 0;
            }

            if (MessageBox.Show("�Ƿ�ȡ���ô��ն�ȷ�ϣ�\r\n ȡ��ȷ�ϲ������ɻ���", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return 0;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.HISFC.BizLogic.Terminal.TerminalConfirm terMgr = new FS.HISFC.BizLogic.Terminal.TerminalConfirm();
            FS.HISFC.BizLogic.Fee.ReturnApply returnApplyManager = new FS.HISFC.BizLogic.Fee.ReturnApply();

            returnApplyManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.feeManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.orderManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #region ȡ������

            System.Collections.Hashtable hsMoExecNOList = new Hashtable();
            DateTime dtNow=returnApplyManager.GetDateTimeFromSysDateTime();
            ArrayList alSendInfo = new ArrayList();
            foreach (FarPoint.Win.Spread.Row r in this.fpSpread1_Sheet1.Rows)
            {

                FS.HISFC.Models.Terminal.TerminalConfirmDetail obj = ((FS.HISFC.Models.Terminal.TerminalConfirmDetail)this.fpSpread1_Sheet1.Cells[r.Index, (int)Cols.MoOrder].Tag).Clone();
                obj.FreeCount = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[r.Index, (int)Cols.ConfirmQty].Value.ToString());

                #region ҽ��

                if (!hsMoExecNOList.ContainsKey(obj.MoOrder + obj.ExecMoOrder))
                {
                    if (!string.IsNullOrEmpty(obj.MoOrder))//�����ʣһ������˵�����еĶ�ȡ����  ???����
                    {
                        if (terMgr.CancelInpatientConfirmMoOrder(obj) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����ҽ��ʧ��" + terMgr.Err);

                            return -1;
                        }
                    }

                    hsMoExecNOList.Add(obj.MoOrder + obj.ExecMoOrder, null);
                }

                #endregion

                if (obj.Status.ID.Equals("0"))//��������²��˷�
                {
                    #region ����

                    FeeItemList feeItemListTemp = this.feeManager.GetItemListByRecipeNO(obj.Apply.Item.RecipeNO, obj.Apply.Item.SequenceNO, obj.Apply.Item.Item.ItemType);
                    if (feeItemListTemp.Days == 0)
                    {
                        feeItemListTemp.Days = 1;
                    }

                    alSendInfo.Add(feeItemListTemp);

                    //����ȷ������
                    if (feeManager.UpdateConfirmNumForUndrug(feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO, 
                        0, feeItemListTemp.BalanceState) <= 0)
                    {
                        feeManager.Rollback();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("���·�����ϸȷ������ʧ�ܣ�" + this.feeManager.Err));
                        return -1;
                    }

                    if (feeItemListTemp.NoBackQty > 0)
                    {
                        feeItemListTemp.Item.Qty = obj.FreeCount;
                        feeItemListTemp.NoBackQty = 0;
                        feeItemListTemp.FT.TotCost = feeItemListTemp.Item.Price * feeItemListTemp.Item.Qty / feeItemListTemp.Item.PackQty;
                        feeItemListTemp.FT.OwnCost = feeItemListTemp.FT.TotCost;
                        feeItemListTemp.IsNeedUpdateNoBackQty = true;

                        if (this.quitFeeType == EnumQuitFeeType.QuitApply || this.quitFeeType == EnumQuitFeeType.QuitFee)
                        {
                            string applyBillCode = returnApplyManager.GetReturnApplyBillCode();
                            if (applyBillCode == null || applyBillCode == string.Empty)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(this, "��ȡ�˷����뷽�ų���!", "��ʾ>>", MessageBoxButtons.OK);
                                return -1;
                            }
                            string msg=string.Empty;

                            if (this.feeManager.QuitFeeApply(this.patientInfo, feeItemListTemp, this.quitFeeType == EnumQuitFeeType.QuitApply ? true : false, applyBillCode, dtNow, ref msg) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(this, "�����˷�����ʧ��!" + returnApplyManager.Err, "��ʾ>>", MessageBoxButtons.OK);
                                return -1;
                            }
                        }
                    }
                    else
                    {
                        ArrayList al= returnApplyManager.QueryReturnApplysByRecipeNoSequenceNo(this.patientInfo.ID, feeItemListTemp.RecipeNO, feeItemListTemp.SequenceNO.ToString());
                        if (al == null)
                        {
                            this.feeManager.Rollback();
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this, "��ȡ�˷�������Ϣʧ��!" + returnApplyManager.Err, "��ʾ>>", MessageBoxButtons.OK);
                            return -1;
                        }

                        foreach (FS.HISFC.Models.Fee.ReturnApply returnApply in al)
                        {
                            if (returnApply.CancelType != FS.HISFC.Models.Base.CancelTypes.Canceled)
                            {
                                continue;
                            }

                            returnApply.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                            returnApply.ConfirmOper.ID = returnApplyManager.Operator.ID;
                            returnApply.ConfirmOper.OperTime =dtNow;

                            if (returnApplyManager.ConfirmApply(returnApply) <= 0)
                            {
                                this.feeManager.Rollback();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("��׼�˷�����ʧ��!"+ returnApplyManager.Err);
                                return -1;
                            }

                            feeItemListTemp.Item.Qty = obj.FreeCount;
                            feeItemListTemp.NoBackQty = 0;
                            feeItemListTemp.FT.TotCost = feeItemListTemp.Item.Price * feeItemListTemp.Item.Qty / feeItemListTemp.Item.PackQty;
                            feeItemListTemp.FT.OwnCost = feeItemListTemp.FT.TotCost;
                            feeItemListTemp.IsNeedUpdateNoBackQty = false;
                            if (this.feeManager.QuitItem(this.patientInfo, feeItemListTemp) == -1)
                            {
                                this.feeManager.Rollback();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("�˷�ʧ��!" + this.feeManager.Err);
                                return -1;
                            }
                        }
                    }

                    #endregion
                }
               
                #region �ж��Ƿ���Ҫ����ִ�е�������ȫ��ȷ�������Ŀ����ִ�е���δȫ��ȷ�ϵ���Ŀ������

                //{0A8C4027-210C-49e0-977F-576789F46946} by yuyun 08-8-13
                //ȡҽ��ִ�е�������
                //decimal execOrderQty = terMgr.GetExecOrderQty(obj.ExecMoOrder);
                //if (execOrderQty == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show(terMgr.Err);
                //    return -1;
                //}
                //ȡ��ȷ�ϵ�������
                if (string.IsNullOrEmpty(obj.MoOrder) == false)
                {
                    decimal confirmedQty = terMgr.GetAlreadConfirmNum(obj.MoOrder, obj.ExecMoOrder, obj.Apply.Item.ID);
                    if (confirmedQty - obj.Apply.Item.ConfirmedQty < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(obj.Apply.Item.Name + "ȡ������������ȷ��������" + terMgr.Err);
                        return -1;
                    }
                }
                ////�Ա���������  �ж��Ƿ���Ҫ����ִ�е�  
                //if (confirmedQty == execOrderQty)
                //{
                //    //����ִ�е�
                //    if (!string.IsNullOrEmpty(obj.ExecMoOrder))  //Ϊҽ��������Ŀ�Ž���ִ�е�����
                //    {
                //        if (terMgr.CancelExecOrder(obj.ExecMoOrder) <= 0)
                //        {
                //            FS.FrameWork.Management.PublicTrans.RollBack();
                //            MessageBox.Show("����ҽ��ִ�е�ʧ��" + terMgr.Err);

                //            return -1;
                //        }
                //    }
                //    //MessageBox.Show("����");
                //}

                #endregion
                #region ȷ����ϸ

                if (terMgr.CancelInpatientConfirmDetail(obj) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ȷ����ϸʧ��" + terMgr.Err);

                    return -1;
                }

                #endregion
               
            }

            #endregion

            //������Ϣ
            #region HL7��Ϣ����
            if (alSendInfo.Count > 0)
            {
                object curInterfaceImplement = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.BizProcess.Integrate.Fee), typeof(FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder));
                if (curInterfaceImplement is FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder)
                {
                    FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder curIOrderControl = curInterfaceImplement as FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder;

                    int param = curIOrderControl.SendDrugInfo(patientInfo, alSendInfo, false);
                    if (param == -1)
                    {
                        feeManager.Rollback();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("������Ϣʧ�ܣ�" + curIOrderControl.Err));
                        return -1;
                    }
                }
            }

            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("ȡ���ɹ�");
            //����һ��ȡ������ by yuyun 08-8-12 {58B76F7C-A35D-4cbb-8948-8163EA3C5191}
            //this.fpSpread1_Sheet1.Rows.Remove(this.fpSpread1_Sheet1.ActiveRowIndex,1);
            this.fpSpread1_Sheet1.RowCount = 0;
            //---------------
            return 1;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Init();
        }

        protected override int OnSave(object sender, object neuObject)
        {
            int returnValue  = this.Save();
            if (returnValue != 0)
            {
                this.AddDataToFp(this.QueryExeData(this.ucQueryInpatientNo1.InpatientNo));
            }
            return base.OnSave(sender, neuObject);
        }

        #endregion

        #region �¼�

        private void ucQueryInpatientNo1_myEvent()
        {

            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo == string.Empty)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�û��߲�����!����֤������"));

                return;
            }

            FS.HISFC.Models.RADT.PatientInfo patientTemp = this.radtManager.GetPatientInfomation(this.ucQueryInpatientNo1.InpatientNo);
            if (patientTemp == null || patientTemp.ID == null || patientTemp.ID == string.Empty)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�û��߲�����!����֤������"));

                return;
            }

            if (patientTemp.PVisit.InState.ID.ToString() == "N")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�û����Ѿ��޷���Ժ���������շ�!"));

                //this.Clear();
                this.ucQueryInpatientNo1.Focus();

                return;
            }

            if (patientTemp.PVisit.InState.ID.ToString() == "O")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�û����Ѿ���Ժ���㣬�������շ�!"));

                //this.Clear();
                this.ucQueryInpatientNo1.Focus();

                return;
            }
            this.patientInfo = patientTemp;

            this.txtName.Text = this.patientInfo.Name;
            this.txtPact.Text = this.patientInfo.Pact.Name;
            this.AddDataToFp(this.QueryExeData(this.patientInfo.ID));
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.ucQueryInpatientNo1_myEvent();
            return base.OnQuery(sender, neuObject);
        }

        #endregion

        private int AddDetailToFp(FS.HISFC.Models.Terminal.TerminalConfirmDetail tecDetail)
        {
            //����һ��ȡ������ by yuyun 08-8-12 {58B76F7C-A35D-4cbb-8948-8163EA3C5191}
            int rowCount = this.fpSpread1_Sheet1.RowCount;
            this.fpSpread1_Sheet1.Rows.Add(rowCount, 1);
            this.fpSpread1_Sheet1.Cells[rowCount, (int)Cols.MoOrder].Text = tecDetail.MoOrder;
            this.fpSpread1_Sheet1.Cells[rowCount, (int)Cols.ExecMoOrder].Text = tecDetail.ExecMoOrder;
            this.fpSpread1_Sheet1.Cells[rowCount, (int)Cols.ApplyNum].Text = tecDetail.Sequence;
            this.fpSpread1_Sheet1.Cells[rowCount, (int)Cols.ItemID].Text = tecDetail.Apply.Item.ID;
            this.fpSpread1_Sheet1.Cells[rowCount, (int)Cols.ItemName].Text = tecDetail.Apply.Item.Name;
            this.fpSpread1_Sheet1.Cells[rowCount, (int)Cols.ConfirmQty].Text = tecDetail.Apply.Item.ConfirmedQty.ToString();
            this.fpSpread1_Sheet1.Cells[rowCount, (int)Cols.PrinctUnit].Text = this.GetPriceUnit(tecDetail.Apply.Item.ID);
            this.fpSpread1_Sheet1.Cells[rowCount, (int)Cols.OperCode].Text = tecDetail.Apply.Item.ConfirmOper.ID;
            this.fpSpread1_Sheet1.Cells[rowCount, (int)Cols.OperDept].Text = tecDetail.Apply.ConfirmOperEnvironment.Dept.ID;
            this.fpSpread1_Sheet1.Cells[rowCount, (int)Cols.OperTime].Text = tecDetail.Apply.ConfirmOperEnvironment.OperTime.ToString();
            this.fpSpread1_Sheet1.Cells[rowCount, (int)Cols.RecipeNo].Text = tecDetail.Apply.Item.RecipeNO;
            this.fpSpread1_Sheet1.Cells[rowCount, (int)Cols.SequenceNo].Text = tecDetail.Apply.Item.SequenceNO.ToString();
            //by yuyun 08-7-7
            this.fpSpread1_Sheet1.Cells[rowCount, (int)Cols.Operator].Text = tecDetail.Oper.ID;//��ʦ��Ĭ���ǵ�ǰ����Ա�������޸�
            this.fpSpread1_Sheet1.Cells[rowCount, (int)Cols.Machine].Text = tecDetail.ExecDevice;//��Ŀʹ���豸����ҽ���������в���
            this.fpSpread1_Sheet1.Cells[rowCount, (int)Cols.MoOrder].Tag = tecDetail;
            this.fpSpread1_Sheet1.Rows[rowCount].Tag = tecDetail;
            //------------{58B76F7C-A35D-4cbb-8948-8163EA3C5191}

            return 1;
        }

        private enum Cols
        {
            MoOrder,//0
            ExecMoOrder,//1
            ApplyNum,//2
            ItemID,//3
            ItemName,//4
            ConfirmQty,//5
            PrinctUnit,//6��λ
            OperCode,//7
            OperDept,//8
            OperTime,//9
            RecipeNo,//10
            SequenceNo,//11
            //by yuyun 08-7-7
            Operator,//12��ʦ��Ĭ���ǵ�ǰ����Ա�������޸�
            Machine//13��Ŀʹ���豸����ҽ���������в���
            //by yuyun 08-7-7
        }

        public enum EnumQuitFeeType
        {
            //ֻ�����˷�����
            UpdateQutiFeeQty,
            /// <summary>
            /// �˷�����
            /// </summary>
            QuitApply,
            /// <summary>
            /// ֱ���˷�
            /// </summary>
            QuitFee
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            #region ���������Ŀ{5C2A9C83-D165-434c-ACA4-86F23E956442}
            FS.HISFC.Models.Terminal.TerminalConfirmDetail confirmDetail = this.fpSpread1_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.Terminal.TerminalConfirmDetail;
            if (!string.IsNullOrEmpty(confirmDetail.User01))
            {
                int rowCount = this.fpSpread1_Sheet1.RowCount;
                for (int i = rowCount - 1; i >= 0; i--)
                {
                    FS.HISFC.Models.Terminal.TerminalConfirmDetail tmpConfirmDetail = this.fpSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Terminal.TerminalConfirmDetail;
                    if (tmpConfirmDetail.User01 == confirmDetail.User01)
                    {
                        this.fpSpread1_Sheet1.RemoveRows(i, 1);
                    }
                }
                return;
            }
            #endregion

            this.fpSpread1_Sheet1.RemoveRows(this.fpSpread1_Sheet1.ActiveRowIndex, 1);
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            #region ���������Ŀ{5C2A9C83-D165-434c-ACA4-86F23E956442}
            int rowIndex = e.Row;
            //ѡ�������Ŀ��ϸ-������Ͻڵ���д���
            if (this.neuSpread1_Sheet1.RowHeader.Cells[e.Row, 0].Text == ".")
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    FS.HISFC.Models.Terminal.TerminalConfirmDetail confirmDetail = this.neuSpread1_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.Terminal.TerminalConfirmDetail;
                    if (this.neuSpread1_Sheet1.Cells[i, 0].Text == confirmDetail.MoOrder && //ҽ���� 
                        this.neuSpread1_Sheet1.Cells[i, 1].Text == confirmDetail.ExecMoOrder &&//ҽ��ִ�к�
                        string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[i, 2].Text))
                    {
                        rowIndex = i;
                        break;
                    }
                }
            }
            if (string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[rowIndex, 2].Text))
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    FS.HISFC.Models.Terminal.TerminalConfirmDetail confirmDetail = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Terminal.TerminalConfirmDetail;
                    if (confirmDetail == null)
                    {
                        continue;
                    }
                    if (this.neuSpread1_Sheet1.Cells[rowIndex, 0].Text == confirmDetail.MoOrder && //ҽ���� 
                        this.neuSpread1_Sheet1.Cells[rowIndex, 1].Text == confirmDetail.ExecMoOrder &&//ҽ��ִ�к�
                        this.neuSpread1_Sheet1.RowHeader.Cells[i, 0].Text == ".")
                    {
                        //�ж��Ƿ�����ظ���¼
                        foreach (FarPoint.Win.Spread.Row r in this.fpSpread1_Sheet1.Rows)
                        {
                            if (this.fpSpread1_Sheet1.Rows[r.Index].Tag == confirmDetail)
                            {
                                MessageBox.Show("�����ѱ�ѡ���");

                                return;
                            }
                        }
                        //ҽ��ִ�к�+�����Ŀ����  --����ȡ��
                        confirmDetail.User01 = this.neuSpread1_Sheet1.Cells[rowIndex, 3].Text + this.neuSpread1_Sheet1.Cells[rowIndex, 1].Text;
                        AddDetailToFp(confirmDetail);
                    }
                }
                return;
            }

            #endregion

            //����һ��ȡ������ by yuyun 08-8-12 {58B76F7C-A35D-4cbb-8948-8163EA3C5191}
            //this.fpSpread1_Sheet1.RowCount = 0;
            //---------------------------------------
            int RowIndex = this.neuSpread1_Sheet1.ActiveRowIndex;

            FS.HISFC.Models.Terminal.TerminalConfirmDetail tecDetail = new FS.HISFC.Models.Terminal.TerminalConfirmDetail();

            tecDetail = this.neuSpread1_Sheet1.ActiveRow.Tag as FS.HISFC.Models.Terminal.TerminalConfirmDetail;
            //����һ��ȡ������ by yuyun 08-8-12 {58B76F7C-A35D-4cbb-8948-8163EA3C5191}
            //�ж��Ƿ�����ظ���¼
            foreach (FarPoint.Win.Spread.Row r in this.fpSpread1_Sheet1.Rows)
            {
                if (this.fpSpread1_Sheet1.Rows[r.Index].Tag == tecDetail)
                {
                    MessageBox.Show("�����ѱ�ѡ���");

                    return;
                }
            }
            //---------------------------------------
            AddDetailToFp(tecDetail);
        }

        /// <summary>
        /// ��Ԫ�񵥻�������������Ŀ�۵�{5C2A9C83-D165-434c-ACA4-86F23E956442}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.RowHeader && this.neuSpread1_Sheet1.Cells[e.Row, 2].Text == "")
            {
                this.ExpendOrCloseRows(e.Row);
            }
        }

        /// <summary>
        /// ����������Ŀ�۵�{5C2A9C83-D165-434c-ACA4-86F23E956442}
        /// </summary>
        /// <param name="rowIndex"></param>
        private void ExpendOrCloseRows(int rowIndex)
        {
            if (!string.IsNullOrEmpty(this.neuSpread1_Sheet1.Cells[rowIndex, 2].Text))
            {
                return;
            }
            bool isExpend = true;
            if (this.neuSpread1_Sheet1.RowHeader.Cells[rowIndex, 0].Text == "+")
            {
                this.neuSpread1_Sheet1.RowHeader.Cells[rowIndex, 0].Text = "-";
                isExpend = true;
            }
            else
            {
                this.neuSpread1_Sheet1.RowHeader.Cells[rowIndex, 0].Text = "+";
                isExpend = false;
            }

            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                FS.HISFC.Models.Terminal.TerminalConfirmDetail confirmDetail = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Terminal.TerminalConfirmDetail;
                if (confirmDetail == null)
                {
                    continue;
                }
                if (this.neuSpread1_Sheet1.Cells[rowIndex, 0].Text == confirmDetail.MoOrder && //ҽ���� 
                    this.neuSpread1_Sheet1.Cells[rowIndex, 1].Text == confirmDetail.ExecMoOrder &&//ҽ��ִ�к�
                    this.neuSpread1_Sheet1.RowHeader.Cells[i, 0].Text == ".")
                {
                    this.neuSpread1_Sheet1.Rows[i].Visible = isExpend;
                }
            }

        }

        /// <summary>
        /// ������Ŀ�����ȡ�Ƽ۵�λ
        /// </summary>
        /// <param name="mo_moder"></param>
        /// <returns></returns>
        private string GetPriceUnit(string item_code)
        {
            string sql = @"select u.stock_unit from fin_com_undruginfo u where u.item_code='{0}'";
            try
            {
                sql = string.Format(sql, item_code);
               return this.tecManager.ExecSqlReturnOne(sql,"");

            }
            catch (Exception exp)
            {
                return "";
            }
 
        }

    }
}

