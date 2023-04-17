using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
//{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
using FS.HISFC.Models.Registration;

namespace FS.HISFC.Components.Registration
{
    /// <summary>
    /// �˺�/ע��
    /// </summary>
    public partial class ucCancel : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer//{B700292D-50A6-4cdf-8B03-F556F990BB9B}
    {
        FS.HISFC.BizLogic.Fee.Account account = new FS.HISFC.BizLogic.Fee.Account();
        public ucCancel()
        {
            InitializeComponent();

            this.fpSpread1.KeyDown  += new KeyEventHandler(fpSpread1_KeyDown);
            this.txtInvoice.KeyDown += new KeyEventHandler(txtInvoice_KeyDown);
            this.txtCardNo.KeyDown  += new System.Windows.Forms.KeyEventHandler(this.txtCardNo_KeyDown);
            this.fpSpread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(fpSpread1_SelectionChanged);

            this.Init();
        }

        #region ��

        /// <summary>
        /// �ҺŹ�����
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// ���ƹ�����
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// �Ű������
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Schema schMgr = new FS.HISFC.BizLogic.Registration.Schema();

        /// <summary>
        /// ����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ���������
        /// </summary>
        //private FS.HISFC.BizProcess.Integrate assMgr = new FS.HISFC.BizLogic.Nurse.Assign();

        /// <summary>
        /// ���˺�����
        /// </summary>
        private int PermitDays = 0;
        private ArrayList al = new ArrayList();

        //{DA67A335-E85E-46e1-A672-4DB409BCC11B}

        private bool isQuitAccount = false;

        /// <summary>
        /// �Ƿ��ӡ�˺�Ʊ
        /// {B700292D-50A6-4cdf-8B03-F556F990BB9B}
        /// </summary>
        private bool isPrintBackBill = false;
      
        #endregion

        //{B700292D-50A6-4cdf-8B03-F556F990BB9B}
        #region ����

        /// <summary>
        /// �Ƿ��ӡ�˺�Ʊ
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ��ӡ�˺�Ʊ(δʵ��)"), DefaultValue(false)]
        public bool IsPrintBackBill
        {
            set
            {
                this.isPrintBackBill = value;
            }
            get
            {
                return this.isPrintBackBill;
            }
        } 

        /// <summary>
        /// //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
        /// </summary>
        [Category("�ؼ�����"), Description("�ʻ������Ƿ����ʻ�"), DefaultValue(false)]
        public bool IsQuitAccount
        {
            get { return isQuitAccount; }
            set { isQuitAccount = value; }
        }

        //{182DA62D-6BCE-4c4c-956F-6F2A363138A0}
        private bool isSeeedCanCancelRegInfo = false;

        //{182DA62D-6BCE-4c4c-956F-6F2A363138A0}
        [Category("�ؼ�����"), Description("�ѿ���Һż�¼�Ƿ����˺ţ�"), DefaultValue(false)]
        public bool IsSeeedCanCancelRegInfo
        {
            get { return isSeeedCanCancelRegInfo; }
            set { isSeeedCanCancelRegInfo = value; }
        }
       
        #endregion

        #region ҽ���ӿ�

        /// <summary>
        /// ҽ���ӿڴ��������
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        /// <summary>
        /// ����Һţ��Һŷ���otherfee������ 0:����(��ҽר��) 1���������� 2��������
        /// </summary>
        string otherFeeType = string.Empty;

        #endregion

        #region ����
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        private int Init()
        {
            //�����˺ţ������˺�����
            string Days = this.ctlMgr.QueryControlerInfo("400006");

            if (Days == null || Days == "" || Days == "-1")
            {
                this.PermitDays = 1;
            }
            else
            {
                this.PermitDays = int.Parse(Days);
            }

            //����Һţ��Һŷ���otherfee������ 0:����(��ҽר��) 1���������� 2��������
            //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
            Days = this.ctlMgr.QueryControlerInfo("400027");

            if (string.IsNullOrEmpty(Days))
            {
                Days = "2"; //Ĭ��������
            }

            this.otherFeeType = Days;

            if (this.otherFeeType == "1")
            {
                this.chbQuitFeeBookFee.Checked = true;
                this.chbQuitFeeBookFee.Visible = true;
            }
            else
            {
                this.chbQuitFeeBookFee.Visible = false;
                this.chbQuitFeeBookFee.Checked = true;
            }

            this.txtCardNo.Focus();

            return 0;
        }
        
        /// <summary>
        /// ��ӻ��߹Һ���ϸ
        /// </summary>
        /// <param name="registers"></param>
        private void addRegister(ArrayList registers)
        {
            if (this.fpSpread1_Sheet1.RowCount > 0)
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);

            FS.HISFC.Models.Registration.Register obj;

            for (int i = registers.Count - 1; i >= 0; i--)
            {
                obj = (FS.HISFC.Models.Registration.Register)registers[i];
                this.addRegister(obj);
            }
        }
        /// <summary>
        /// ������ʹ��ֱ���շ����ɵĺ��ٽ��йҺ�
        /// </summary>
        /// <param name="CardNO"></param>
        /// <returns></returns>
        private int ValidCardNO(string CardNO)
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParams = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            string cardRule = controlParams.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.NO_REG_CARD_RULES, false, "9");
            if (CardNO != "" && CardNO != string.Empty)
            {
                if (CardNO.Substring(0, 1) == cardRule)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�˺Ŷ�Ϊֱ���շ�ʹ�ã��������˺�"), FS.FrameWork.Management.Language.Msg("��ʾ"));
                    return -1;
                }
            }
            return 1;
        }
        /// <summary>
        /// ��ʾ�Һ���Ϣ
        /// </summary>
        /// <param name="reg"></param>
        private void addRegister(FS.HISFC.Models.Registration.Register reg)
        {
            this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);

            int cnt = this.fpSpread1_Sheet1.RowCount - 1;

            this.fpSpread1_Sheet1.SetValue(cnt, 0, reg.Name, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 1, reg.Sex.Name, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 2, reg.DoctorInfo.SeeDate.ToString(), false);
            this.fpSpread1_Sheet1.SetValue(cnt, 3, reg.DoctorInfo.Templet.Dept.Name, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 4, reg.DoctorInfo.Templet.RegLevel.Name, false);
            //������ǣ��Ƿ��ѿ��� {7ADE3D11-1E7E-42ea-988B-0B23D9726300}
            this.fpSpread1_Sheet1.SetValue(cnt, 5, reg.IsSee, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 6, reg.DoctorInfo.Templet.Doct.Name, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 7, reg.RegLvlFee.RegFee , false);
            this.fpSpread1_Sheet1.SetValue(cnt, 8, reg.RegLvlFee.OwnDigFee + reg.RegLvlFee.ChkFee + reg.RegLvlFee.OthFee, false);
            this.fpSpread1_Sheet1.Rows[cnt].Tag = reg;

            if (reg.IsSee)
            {
                this.fpSpread1_Sheet1.Rows[cnt].BackColor = Color.LightCyan;
            }
            if (reg.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back||
                reg.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
            {
                this.fpSpread1_Sheet1.Rows[cnt].BackColor = Color.MistyRose;
            }
        }
       
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        private int save()
        {
            #region ��֤
            if (this.fpSpread1_Sheet1.RowCount == 0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("û�п��˹Һż�¼"), "��ʾ");
                return -1;
            }

            int row = this.fpSpread1_Sheet1.ActiveRowIndex;

            //ʵ��
            FS.HISFC.Models.Registration.Register reg = (FS.HISFC.Models.Registration.Register)this.fpSpread1_Sheet1.Rows[row].Tag;
            if (reg.IsSee)
            {
                MessageBox.Show("�ú��Ѿ�����������˺ţ�", "��ʾ");
                return -1;
            }
            else if (reg.PVisit.InState.ID.ToString() == "I" || reg.PVisit.InState.ID.ToString() == "R")//{A0D66B1F-78B5-440c-A7C0-E98C56CFBCF1}
            {
                MessageBox.Show("���ۻ��߲������˺�", "��ʾ");
                return -1;
            }

            #endregion

            if (MessageBox.Show(FS.FrameWork.Management.Language.Msg("�Ƿ�Ҫ���ϸùҺ���Ϣ") + "?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.regMgr.con);
            //t.BeginTransaction();

            this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.schMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.feeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //this.assMgr.SetTrans(t.Trans);

            int rtn;
            FS.HISFC.BizLogic.Registration.EnumUpdateStatus flag = FS.HISFC.BizLogic.Registration.EnumUpdateStatus.Cancel;

            try
            {
                DateTime current = this.regMgr.GetDateTimeFromSysDateTime();


                //���»�ȡ����ʵ��,��ֹ����

                reg = this.regMgr.GetByClinic(reg.ID);
                if (this.ValidCardNO(reg.PID.CardNO) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
                //����
                if (reg == null || reg.ID == null || reg.ID == "")
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.regMgr.Err, "��ʾ");
                    return -1;
                }

                //ʹ��,��������
                //{05E82D53-9B25-44b1-902E-36F8FF4F50F3}
                //{182DA62D-6BCE-4c4c-956F-6F2A363138A0}
                //if ((reg.IsSee || reg.IsFee) && !this.isSeeedCanCancelRegInfo)
                if (reg.IsSee && !this.isSeeedCanCancelRegInfo)
                {


                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�ú��Ѿ�����,��������"), "��ʾ");
                    return -1;
                }

                //�Ƿ��Ѿ��˺�
                if (reg.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�ùҺż�¼�Ѿ��˺ţ������ٴ��˺�"), "��ʾ");
                    return -1;
                }

                //�Ƿ��Ѿ�����
                if (reg.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�ùҺż�¼�Ѿ����ϣ����ܽ����˺�"), "��ʾ");
                    return -1;
                }

                #region �ж��ǲ��������ʻ�����
                decimal vacancy = 0;

                int result = this.feeMgr.GetAccountVacancy(reg.PID.CardNO, ref vacancy);
                if (result < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.feeMgr.Err);
                    return -1;
                }
                #endregion
                //{5839C7FC-8162-4586-8473-B5F26C018DDE}
                //if (reg.InputOper.ID == regMgr.Operator.ID && reg.BalanceOperStat.IsCheck == false && result == 0  )
                //{
                //    #region ����
                //    #endregion
                //}
                //else
                //{
                #region �˺�
                FS.HISFC.Models.Registration.Register objReturn = reg.Clone();
                objReturn.RegLvlFee.ChkFee = -reg.RegLvlFee.ChkFee;//����
                objReturn.RegLvlFee.OwnDigFee = -reg.RegLvlFee.OwnDigFee;//����


                objReturn.RegLvlFee.OthFee = -reg.RegLvlFee.OthFee;//������
                objReturn.RegLvlFee.RegFee = -reg.RegLvlFee.RegFee;//�Һŷ�
                if (result > 0) //���������ʻ��Ļ��ߣ��ʻ�ȫ���˵��Է���
                {
                    objReturn.OwnCost = -(reg.OwnCost + reg.PayCost);
                    objReturn.PayCost = 0;
                }
                else
                {
                    objReturn.PayCost = -reg.PayCost;
                    objReturn.OwnCost = -reg.OwnCost;
                }
                objReturn.PubCost = -reg.PubCost;
                objReturn.BalanceOperStat.IsCheck = false;//�Ƿ����
                objReturn.BalanceOperStat.ID = "";
                objReturn.BalanceOperStat.Oper.ID = "";
                //objReturn.BeginTime = DateTime.MinValue; 
                objReturn.CheckOperStat.IsCheck = false;//�Ƿ�˲�
                objReturn.Status = FS.HISFC.Models.Base.EnumRegisterStatus.Back;//�˺�
                objReturn.InputOper.OperTime = current;//����ʱ��
                objReturn.InputOper.ID = regMgr.Operator.ID;//������
                objReturn.CancelOper.ID = regMgr.Operator.ID;//�˺���
                objReturn.CancelOper.OperTime = current;//�˺�ʱ��
                //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
                //objReturn.OwnCost = -reg.OwnCost;//�Է�
                //objReturn.PayCost = -reg.PayCost;
                objReturn.PubCost = -reg.PubCost;
                //���������Ѵ���
                //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
                if (this.otherFeeType == "1" && !this.chbQuitFeeBookFee.Checked)
                {
                    objReturn.OwnCost = objReturn.OwnCost - objReturn.RegLvlFee.OthFee;
                    objReturn.RegLvlFee.OthFee = 0;
                }

                objReturn.TranType = FS.HISFC.Models.Base.TransTypes.Negative;

                if (this.regMgr.Insert(objReturn) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.regMgr.Err, "��ʾ");
                    return -1;
                }

                flag = FS.HISFC.BizLogic.Registration.EnumUpdateStatus.Return;
                #endregion
                //}

                reg.CancelOper.ID = regMgr.Operator.ID;
                reg.CancelOper.OperTime = current;

                //����ԭ����ĿΪ����
                rtn = this.regMgr.Update(flag, reg);
                if (rtn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.regMgr.Err, "��ʾ");
                    return -1;
                }
                if (rtn == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�ùҺ���Ϣ״̬�Ѿ����,�����¼�������"), "��ʾ");
                    return -1;
                }

                //ȡ������4.5
                //if (this.assMgr.Delete(reg.ID) == -1)
                //{
                //    t.RollBack();
                //    MessageBox.Show("ɾ��������Ϣ����!" + this.assMgr.Err, "��ʾ");
                //    return -1;
                //}

                #region �ָ��޶�
                //�ָ�ԭ���Ű��޶�
                //���ԭ�������޶�,��ô�ָ��޶�
                if (reg.DoctorInfo.Templet.ID != null && reg.DoctorInfo.Templet.ID != "")
                {
                    //�ֳ��š�ԤԼ�š������

                    bool IsReged = false, IsTeled = false, IsSped = false;

                    if (reg.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)
                    {
                        IsTeled = true; //ԤԼ��
                    }
                    else if (reg.RegType == FS.HISFC.Models.Base.EnumRegType.Reg)
                    {
                        if (reg.DoctorInfo.SeeDate > current)
                        {
                            IsTeled = true;//ԤԼ��
                        }
                        else
                        {
                            IsReged = true;//�ֳ���
                        }
                    }
                    else
                    {
                        IsSped = true;//�����
                    }

                    rtn = this.schMgr.Reduce(reg.DoctorInfo.Templet.ID, IsReged, false, IsTeled, IsSped);
                    if (rtn == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.schMgr.Err, "��ʾ");
                        return -1;
                    }

                    if (rtn == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����Ű���Ϣ,�޷��ָ��޶�"), "��ʾ");
                        return -1;
                    }
                }
                #endregion


                long returnValue = 0;
                FS.HISFC.Models.Registration.Register myYBregObject = reg.Clone();
                this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.medcareInterfaceProxy.SetPactCode(reg.Pact.ID);
                //��ʼ��ҽ��dll
                returnValue = this.medcareInterfaceProxy.Connect();
                if (returnValue == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ʼ��ʧ��") + this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                //����ȡ������Ϣ
                returnValue = this.medcareInterfaceProxy.GetRegInfoOutpatient(myYBregObject);
                if (returnValue == -1)
                {

                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ������Ϣʧ��") + this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                //ҽ����Ϣ��ֵ
                reg.SIMainInfo = myYBregObject.SIMainInfo;
                //�˺�
                reg.User01 = "-1";//�˺Ž���
                //����ĵ����˹Һŷ���{719DEE22-E3E3-4d3c-8711-829391BEA73C} by GengXiaoLei
                //returnValue = this.medcareInterfaceProxy.UploadRegInfoOutpatient(reg);
                reg.TranType = FS.HISFC.Models.Base.TransTypes.Negative;
                returnValue = this.medcareInterfaceProxy.CancelRegInfoOutpatient(reg);
                if (returnValue == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����˺�ʧ��") + this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                returnValue = this.medcareInterfaceProxy.Commit();
                if (returnValue == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����˺��ύʧ��") + this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                this.medcareInterfaceProxy.Disconnect();


                FS.FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("�˺ų���!" + e.Message, "��ʾ");
                return -1;
            }

            this.fpSpread1_Sheet1.Rows.Remove(row, 1);
            //����Ѿ���ӡ��Ʊ,��ʾ�ջط�Ʊ
            MessageBox.Show(FS.FrameWork.Management.Language.Msg("�˺ųɹ�"), "��ʾ");

            //{B700292D-50A6-4cdf-8B03-F556F990BB9B}
            if (this.IsPrintBackBill)
            {
                //��ӡ���˺�Ʊ
                this.Print(reg);

            }
            this.Clear();

            return 0;
        }
        /// <summary>
        /// ����
        /// </summary>
        private void Clear()
        {
            if (this.fpSpread1_Sheet1.RowCount > 0)
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);

            this.txtCardNo.Text = "";
            this.txtInvoice.Text = "";
            this.lbTot.Text = "";
            this.lbReturn.Text = "";

            this.txtCardNo.Focus();
        }

        /// <summary>
        /// ��ʾӦ�˹ҺŽ��
        /// </summary>
        /// <param name="row"></param>
        private void SetReturnFee(int row)
        {
            if (this.fpSpread1_Sheet1.RowCount <= 0) return;

            FS.HISFC.Models.Registration.Register obj = (FS.HISFC.Models.Registration.Register)this.fpSpread1_Sheet1.Rows[row].Tag;
 
            if (obj == null) return;

            decimal ownCost = 0;
            //����������
            //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
            if (this.otherFeeType == "1" && !this.chbQuitFeeBookFee.Checked) //���˲�����
            {
                ownCost = obj.OwnCost - obj.RegLvlFee.OthFee;//��ȥ������
            }

            //{DA67A335-E85E-46e1-A672-4DB409BCC11B}
            //�ʻ�����
            if (!IsQuitAccount)
            {
                if (this.otherFeeType == "1" && !this.chbQuitFeeBookFee.Checked) //���˲�����
                {
                    ownCost = obj.OwnCost - obj.RegLvlFee.OthFee + obj.PayCost;//��ȥ������
                }
                else
                {
                    ownCost = obj.OwnCost + obj.PayCost;
                }
            }


            this.lbTot.Text = Convert.ToString(ownCost + obj.PayCost + obj.PubCost);
            this.lbReturn.Text = ownCost.ToString();
        }

        //{B700292D-50A6-4cdf-8B03-F556F990BB9B}
        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="regObj"></param>
        private void Print(FS.HISFC.Models.Registration.Register regObj)
        {

            FS.HISFC.BizProcess.Interface.Registration.IRegPrint regprint = null;
            regprint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Registration.IRegPrint)) as FS.HISFC.BizProcess.Interface.Registration.IRegPrint;

            if (regprint == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ӡƱ��ʧ��,���ڱ���ά����ά���˺�Ʊ"));
            }
            else
            {

                if (regObj.IsEncrypt)
                {
                    regObj.Name = FS.FrameWork.WinForms.Classes.Function.Decrypt3DES(regObj.NormalName);
                }

                regprint.SetPrintValue(regObj);
                regprint.Print();
            }



        }

        

        #endregion

        #region �¼�
        /// <summary>
        /// �����ݼ�
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            //if (keyData == Keys.F12)
            //{
            //    this.save();

            //    return true;
            //}
            //else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.X.GetHashCode())
            //{
            //    this.FindForm().Close();

            //    return true;
            //}
            //else if (keyData == Keys.Escape)
            //{
            //    this.FindForm().Close();

            //    return true;
            //}
            //else if (keyData == Keys.F8)
            //{
            //    this.Clear();

            //    return true;
            //}

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// fp�س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.save();
            }
        }

        private void fpSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            this.SetReturnFee(e.Range.Row);
        }
        
        /// <summary>
        /// ���ݲ����ż������߹Һ���Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FS.HISFC.Models.Account.AccountCard accountObj=new FS.HISFC.Models.Account.AccountCard();
                if (this.feeMgr.ValidMarkNO(this.txtCardNo.Text, ref accountObj) == -1)
                {
                    MessageBox.Show(feeMgr.Err);
                    return;
                }

                string cardNum = accountObj.Patient.PID.CardNO;

                //{4661623D-235A-4380-A7E0-476C977650CD}
                cardNum = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtCardNo.Text.Trim(), "'", "[", "]");//cardNum:���￨��
                if (cardNum == ""||cardNum==null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("���￨�Ų���Ϊ��"), "��ʾ");
                    this.txtCardNo.Focus();
                    return;
                }

                cardNum = cardNum.PadLeft(10, '0');
                this.txtCardNo.Text = cardNum;
                string cardNo = "";//����Ψһ��ʶ
                bool flag = account.GetCardNoByMarkNo(cardNum, ref cardNo);
                if (!flag)
                {
                    if (accountObj.MarkType.ID == "Card_No" && accountObj.Patient.PID.CardNO.Length > 0)
                    {
                        cardNo = accountObj.Patient.PID.CardNO;
                    }
                    else
                    {

                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("������ľ��￨�Ų�����"), "��ʾ");
                        this.txtCardNo.Text = "";
                        this.txtCardNo.Focus();
                        return;
                    }
                }
              
                DateTime permitDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(-this.PermitDays).Date;
                //����������Ч��
                this.al = this.regMgr.Query(cardNo, permitDate);
                if (this.al == null)
                {
                    MessageBox.Show("�������߹Һ���Ϣʱ����!" + this.regMgr.Err, "��ʾ");
                    return;
                }

                if (this.al.Count == 0)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�û���û�п��˺�"), "��ʾ");
                    this.txtCardNo.Text = "";
                    this.txtCardNo.Focus();
                    return;
                }
                else
                {
                    this.addRegister(al);

                    this.SetReturnFee(0);

                    this.fpSpread1.Focus();
                    this.fpSpread1_Sheet1.ActiveRowIndex = 0;
                    this.fpSpread1_Sheet1.AddSelection(0, 0, 1, 0);
                }
            }
        }

        /// <summary>
        /// ���ݴ����ż����Һ���Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //{4661623D-235A-4380-A7E0-476C977650CD}
                string invoiceNo = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtInvoice.Text.Trim(), "'", "[", "]");
                //string recipeNo = this.txtInvoice.Text.Trim();
                if (invoiceNo == "")
                {
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��Ʊ�Ų���Ϊ��" ), "��ʾ" );
                    this.txtInvoice.Focus();
                    return;
                }
                invoiceNo=invoiceNo.PadLeft(12,'0');
                txtInvoice.Text = invoiceNo;

                DateTime permitDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(-this.PermitDays).Date;
                //����������Ч��
                //{B6E76F4C-1D79-4fa2-ABAD-4A22DE89A6F7} by ţ��Ԫ

                //֮ǰ�ô����Ų�ѯ�ģ���Ϊ�÷�Ʊ�Ų�ѯ{7ADE3D11-1E7E-42ea-988B-0B23D9726300}
                //this.al = this.regMgr.QueryByRecipe( recipeNo);
                this.al = this.regMgr.QueryByRegInvoice(invoiceNo);
                if (this.al == null)
                {
                    MessageBox.Show("�������߹Һ���Ϣʱ����!" + this.regMgr.Err, "��ʾ");
                    return;
                }
                else if (this.al.Count == 0)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�޸÷�Ʊ�Ŷ�Ӧ�ĵĹҺ���Ϣ!"), "��ʾ");
                    this.txtInvoice.Focus();
                    return;
                }

                ArrayList alRegCollection = new ArrayList();

                //�Ƴ������޶�ʱ��ĹҺ���Ϣ
                foreach (FS.HISFC.Models.Registration.Register obj in this.al)
                {
                    if (obj.DoctorInfo.SeeDate.Date < permitDate.Date) continue;

                    alRegCollection.Add(obj);
                }

                if (alRegCollection.Count == 0)
                {
                    //�������޵ľ�����ʾ{7ADE3D11-1E7E-42ea-988B-0B23D9726300}
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����˺�����Ϊ" + this.PermitDays.ToString() + "�죬�˹Һ�Ʊ�ѳ������ޣ��������˺ţ�"), "��ʾ");
                    this.txtInvoice.Focus();
                    return;
                }
                else
                {
                    this.addRegister(alRegCollection);

                    this.SetReturnFee(0);

                    this.fpSpread1.Focus();
                    this.fpSpread1_Sheet1.ActiveRowIndex = 0;
                    this.fpSpread1_Sheet1.AddSelection(0, 0, 1, 0);
                }
            }
        }

        #endregion

        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("�˺�", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            toolbarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);

            return toolbarService;
        }        

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "�˺�":
                    //if (txtCardNo.Text == null || txtCardNo.Text.Trim() == "")
                    //{
                    //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("���벡����"), FS.FrameWork.Management.Language.Msg("��ʾ"));
                    //    return;
                    //}
                    e.ClickedItem.Enabled = false;
                    if (this.save() == -1)
                    {
                        e.ClickedItem.Enabled = true;
                        return;
                    }
                    e.ClickedItem.Enabled = true;

                    break;
                case "����":

                    this.Clear();

                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        private void txtCardNo_TextChanged(object sender, EventArgs e)
        {

        }

        #region IInterfaceContainer ��Ա
        //{B700292D-50A6-4cdf-8B03-F556F990BB9B}
        public Type[] InterfaceTypes
        {

            get
            {
                Type[] type = new Type[1];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.Registration.IRegPrint);

                return type;
            }
        }

        #endregion
        //{F3258E87-7BCC-411a-865E-A9843AD2C6DD}
        private void chbQuitFeeBookFee_CheckedChanged(object sender, EventArgs e)
        {
            this.SetReturnFee(this.fpSpread1_Sheet1.ActiveRowIndex);
        }

       
    }
}
