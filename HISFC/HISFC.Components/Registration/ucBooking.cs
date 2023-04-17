using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Xml;

namespace FS.HISFC.Components.Registration
{
    /// <summary>
    /// ԤԼ�Һ�
    /// </summary>
    public partial class ucBooking : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucBooking()
        {
            InitializeComponent();

            this.Load += new EventHandler(ucBooking_Load);
            this.txtCardNo.KeyDown += new KeyEventHandler(txtCardNo_KeyDown);
            this.txtName.KeyDown += new KeyEventHandler(txtName_KeyDown);
            this.txtPhone.KeyDown += new KeyEventHandler(txtPhone_KeyDown);
            this.txtIdenNo.KeyDown += new KeyEventHandler(txtIdenNo_KeyDown);
            this.txtAdress.KeyDown += new KeyEventHandler(txtAdress_KeyDown);
            this.cmbDept.KeyDown += new KeyEventHandler(cmbDept_KeyDown);
            this.cmbDept.SelectedIndexChanged += new EventHandler(cmbDept_SelectedIndexChanged);
            this.cmbDoct.KeyDown += new KeyEventHandler(cmbDoct_KeyDown);
            this.cmbDoct.SelectedIndexChanged += new EventHandler(cmbDoct_SelectedIndexChanged);
            this.dtBookingDate.ValueChanged += new EventHandler(dtBookingDate_ValueChanged);
            this.dtBookingDate.KeyDown += new KeyEventHandler(dtBookingDate_KeyDown);
            this.dtBegin.ValueChanged += new EventHandler(dtBegin_ValueChanged);
            this.dtBegin.KeyDown += new KeyEventHandler(dtBegin_KeyDown);
            this.dtEnd.ValueChanged += new EventHandler(dtEnd_ValueChanged);
            this.dtEnd.KeyDown += new KeyEventHandler(dtEnd_KeyDown);
            this.bnQuery.Click += new EventHandler(bnQuery_Click);
            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellDoubleClick);
            
            this.label13.Click += new EventHandler(label13_Click);
            this.txtOrder.KeyDown += new KeyEventHandler(txtOrder_KeyDown);
            this.txtOrder.Validating += new CancelEventHandler(txtOrder_Validating);
            this.txtAge.KeyDown+=new KeyEventHandler(txtAge_KeyDown);
            this.dtBirthday.KeyDown+=new KeyEventHandler(dtBirthday_KeyDown);
            this.cmbSex.KeyDown+=new KeyEventHandler(cmbSex_KeyDown);
            this.cmbUnit.SelectedIndexChanged+=new EventHandler(cmbUnit_SelectedIndexChanged);
            this.cmbUnit.KeyDown+=new KeyEventHandler(cmbUnit_KeyDown);
        }

        #region ����
        /// <summary>
        /// ��������б�
        /// </summary>
        private ArrayList alDept = new ArrayList();
        /// <summary>
        /// ����ҽ���б�
        /// </summary>
        private ArrayList alDoct = new ArrayList();

        /// <summary>
        /// ������ԤԼƽ̨
        /// </summary>
        private AppointmentService appointmentService = new AppointmentService();
        /// <summary>
        /// ԤԼ����������
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Appointment appointmentMgr = new FS.HISFC.BizLogic.Registration.Appointment();
        /// <summary>
        /// ԤԼ������
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Booking bookingMgr = new FS.HISFC.BizLogic.Registration.Booking();
        /// <summary>
        /// �Ű������
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Schema schemaMgr = new FS.HISFC.BizLogic.Registration.Schema();
        /// <summary>
        /// ��Ա������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager Mgr = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// �ҺŹ�����
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
        /// <summary>
        /// ������Ϣ������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT patMgr = new FS.HISFC.BizProcess.Integrate.RADT();
        /// <summary>
        /// ԤԼʵ����
        /// </summary>
        private FS.HISFC.Models.Registration.Booking booking;
        
        /// <summary>
        /// ԤԼʱ��ѡ���
        /// </summary>
        private ucChooseBookingDate ucChooseDate;
        /// <summary>
        /// �Ƿ񴥷�SelectedIndexChanged�¼�
        /// </summary>
        private bool IsTriggerSelectedIndexChanged = true;
        /// <summary>
        /// ��ͬ��λ������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();
        #endregion

        #region �¼�
        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucBooking_Load(object sender, EventArgs e)
        {
            this.InitDept();
            this.InitDoct();
            this.InitChooseDate();

            this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());
            this.cmbSex.IsFlat = true;
            this.Clear();

            this.Retrieve();
            
            this.cmbDoct.Focus();

            #region by lijp 2012-09-25 {C108A02B-D1A3-4c0b-B024-67EECA401A6C}
            if (isShowBookingType)
            {
                this.cmbBookingType.Visible = true;
                this.cmbBookingType.Enabled = true;
                this.lblBookingType.Visible = true;
                this.InitBookingType();
            }
            else
            {
                this.lblBookingType.Visible = false;
                this.cmbBookingType.Visible = false;
            }
            #endregion
        }


        /// <summary>
        /// ����������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// by lijp 2012-09-25 {C108A02B-D1A3-4c0b-B024-67EECA401A6C}
        /// ��ʼ��ԤԼ����"BookingType"
        /// </summary>
        private void InitBookingType()
        {
            try
            {
                ArrayList arrBookingType = new ArrayList();
                arrBookingType = this.conMgr.QueryConstantList("BookingType");
                this.cmbBookingType.AddItems(arrBookingType);
            }
            catch
            {
                MessageBox.Show("��ȡ����ԤԼ�Һ����ʧ�ܣ�");
                return;
            }
        }

        /// <summary>
        /// ���ݲ����ż������߻�����Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cardNo = this.txtCardNo.Text.Trim();

                if (cardNo == "")
                {
                    if (string.IsNullOrEmpty(cardNo))
                    {
                        if (MessageBox.Show("������Ϊ�գ��Ƿ��������������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            this.txtCardNo.Focus();
                            return;
                        }
                        else
                        {
                            //ֱ������������,�ɸ����������������������Ϣ
                            this.txtName.Focus();
                            return;
                        }
                    }

                    return;
                }


                if (this.ValidCardNO(cardNo) < 0)
                {
                    this.txtCardNo.Focus();
                    return;
                }

                this.ClearPatient();

                FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
                //�������ҿ���¼ʱ�ĹҺű��
                //accountCard.Memo = "1";
                int rev = this.feeMgr.ValidMarkNO(cardNo, ref accountCard);

                if (rev > 0)
                {
                    cardNo = accountCard.Patient.PID.CardNO;
                }

                cardNo = cardNo.PadLeft(10, '0');


                this.booking = this.getPatientInfo(cardNo);
                if (this.booking == null) return;

                //��ֵ
                this.SetPatient(this.booking);

                //if(this.booking.Name == null ||this.booking.Name.Trim() == "")
                //{
                this.txtName.Focus();
                //}
                //else
                //{
                //this.cmbDoct.Focus() ;
                //}
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.setPriorControlFocus();
            }
        }
        /// <summary>
        /// �����س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txtName.Text.Trim() == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����뻼������"), "��ʾ");
                    this.txtName.Focus();
                    return;
                }

                //û�����벡����,����ݻ������������Һ���Ϣ
                if (this.txtCardNo.Text.Trim() == "")
                {
                    string CardNo = this.GetCardNoByName(this.txtName.Text.Trim());

                    if (CardNo == "")
                    {
                        int autoGetCardNO;
                        autoGetCardNO = regMgr.AutoGetCardNO();
                        if (autoGetCardNO == -1)
                        {
                            MessageBox.Show("δ�ܳɹ��Զ��������ţ����ֶ����룡", "��ʾ");
                        }
                        else
                        {
                            this.txtCardNo.Text = autoGetCardNO.ToString().PadLeft(10, '0');
                        }

                        this.booking = new FS.HISFC.Models.Registration.Booking();
                        this.booking.PID.CardNO = this.txtCardNo.Text;
                        this.booking.Name = this.txtName.Text;
                        this.cmbSex.Focus();
                        return;
                    }
                    else
                    {
                        this.txtCardNo.Enabled = false;
                    }
                    this.txtCardNo.Text = CardNo;

                    this.txtCardNo_KeyDown(new object(), new KeyEventArgs(Keys.Enter));

                }
                else
                {
                    this.cmbSex.Focus();
                }

            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.setPriorControlFocus();
            }
        }

        /// <summary>
        /// �绰�س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //				if(this.txtPhone.Text == null || this.txtPhone.Text.Trim() == "")
                //				{
                //					MessageBox.Show("�����뻼����ϵ�绰!","��ʾ") ;
                //					this.txtPhone.Focus() ;
                //					return ;
                //				}

                this.txtIdenNo.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.setPriorControlFocus();
            }
        }

        /// <summary>
        /// ���֤�Żس�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtIdenNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtAdress.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.setPriorControlFocus();
            }
        }

        /// <summary>
        /// ��ַ�س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAdress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Save();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.setPriorControlFocus();
            }
        }

        /// <summary>
        /// ���һس�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //�趨ԤԼʱ���,Ĭ��Ϊ����				
                if (this.cmbDept.Tag == null || this.cmbDept.Tag.ToString() == "")//û��ѡ�����
                {
                    DateTime today = this.bookingMgr.GetDateTimeFromSysDateTime();

                    this.SetBookingDate(today);

                    //û��ѡ�����,ҽ���б���ʾȫ��ҽ��
                    this.cmbDoct.AddItems(this.alDoct);
                    this.cmbDoct.Tag = "";
                    //�趨ԤԼʱ���,�������Ű���Ϣ,����Ĭ��ѡ��
                    this.SetDefaultBookingTime(today);
                }

                this.cmbDoct.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }


        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.IsTriggerSelectedIndexChanged == false) return;

            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;

            //ѡ�����
            DateTime today = this.bookingMgr.GetDateTimeFromSysDateTime();
            //�趨ԤԼ����,Ĭ��Ϊ����
            this.SetBookingDate(today);
            //����ʾҽ��
            this.cmbDoct.Tag = "";
            //��ʾ�ÿ������Ű�ҽ���б�
            this.GetDoctByDept(this.cmbDept.Tag.ToString());
            //�趨����ԤԼ����ʱ���
            this.SetDeptZone(this.cmbDept.Tag.ToString(), today);
            //��ʾ��ˮ��
            this.GetOrder();
        }
        /// <summary>
        /// ҽ���س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDoct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //û��ѡ��ҽ��,��ΪԤԼ��ר��
                if (this.cmbDoct.Tag == null || this.cmbDoct.Tag.ToString() == "")
                {
                    if (this.cmbDept.Tag == null || this.cmbDept.Tag.ToString() == "")
                    {
                        MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ָ��ԤԼҽ��" ), "��ʾ" );
                        this.cmbDoct.Focus();
                        return;
                    }

                    //					if( this.getLastSchema(FS.HISFC.Models.Registration.SchemaTypeNUM.Dept,null,
                    //						this.cmbDept.Tag.ToString(), "") == -1)
                    //					{
                    //						this.cmbDept.Focus() ;
                    //						return ;
                    //					}
                }
                //				else
                //				{
                //					if( this.getLastSchema(FS.HISFC.Models.Registration.SchemaTypeNUM.Doct,null ,
                //						"", this.cmbDoct.Tag.ToString()) == -1)
                //					{
                //						this.cmbDoct.Focus() ;
                //						return ;
                //					}
                //				}

                this.dtBookingDate.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.setPriorControlFocus();
            }
        }


        private void cmbDoct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.IsTriggerSelectedIndexChanged == false) return;

            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;

            //ѡ��ҽ��
            DateTime today = this.bookingMgr.GetDateTimeFromSysDateTime();
            //�趨ԤԼ����,Ĭ��Ϊ����
            this.SetBookingDate(today);
            //�趨ҽ��Ĭ�ϰ���ʱ���
            this.SetDoctZone(this.cmbDoct.Tag.ToString(), today);
            //��ʾ��ˮ��
            this.GetOrder();
        }
        /// <summary>
        /// ���ڱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtBookingDate_ValueChanged(object sender, EventArgs e)
        {
            this.SetBookingTag(null);
            //�������
            this.lbWeek.Text = this.getWeek(this.dtBookingDate.Value);

            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;
        }
        /// <summary>
        /// �س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtBookingDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.dtBookingDate.Value.Date < this.bookingMgr.GetDateTimeFromSysDateTime().Date)
                {
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "ԤԼ���ڲ���С�ڵ�ǰ����" ), "��ʾ" );
                    this.dtBookingDate.Focus();
                    return;
                }

                this.dtBegin.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.bnQuery_Click(new object(), new System.EventArgs());
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.setPriorControlFocus();
            }
        }
        /// <summary>
        /// �س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtBegin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.dtEnd.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.bnQuery_Click(new object(), new System.EventArgs());
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.setPriorControlFocus();
            }
        }
        /// <summary>
        /// �س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtEnd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtCardNo.Focus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.bnQuery_Click(new object(), new System.EventArgs());
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.setPriorControlFocus();
            }
        }

        private void dtBegin_ValueChanged(object sender, EventArgs e)
        {
            this.SetBookingTag(null);
            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;
        }

        private void dtEnd_ValueChanged(object sender, EventArgs e)
        {
            this.SetBookingTag(null);
            if (this.ucChooseDate.Visible) this.ucChooseDate.Visible = false;
        }
        /// <summary>
        /// bnQuery button clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bnQuery_Click(object sender, EventArgs e)
        {
            if (this.ucChooseDate.Visible)
            {
                this.ucChooseDate.Visible = false;
                this.dtBookingDate.Focus();
            }
            else
            {
                this.SetZone();
            }
        }
        /// <summary>
        /// ѡ��ԤԼʱ���
        /// </summary>
        /// <param name="sender"></param>
        private void ucChooseDate_SelectedItem(FS.HISFC.Models.Registration.Schema sender)
        {
            this.ucChooseDate.Visible = false;

            if (sender == null) return;

            //			if(sender.Templet.TelLmt <= sender.TelReging)
            //			{
            //				if(MessageBox.Show("ԤԼ�����Ѿ����������,�Ƿ����?","��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Question,
            //					MessageBoxDefaultButton.Button2) == DialogResult.No)
            //				{
            //					this.dtBookingDate.Focus() ;
            //					return ;
            //				}
            //			}
            //����
            this.IsTriggerSelectedIndexChanged = false;
            this.cmbDept.Tag = sender.Templet.Dept.ID;
            //ҽ��
            if (sender.Templet.Doct.ID == "None")
            {
                this.cmbDoct.Tag = "";
            }
            else
            {
                this.cmbDoct.Tag = sender.Templet.Doct.ID;
            }
            this.IsTriggerSelectedIndexChanged = true;

            //ԤԼʱ��
            this.SetBookingDate(sender.SeeDate);
            //ԤԼʱ���
            this.SetBookingTime(sender);
            this.dtEnd.Focus();

        }
        /// <summary>
        /// farpoint cell doubleclick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader || this.fpSpread1_Sheet1.RowCount == 0) return;

            //int row = this.fpSpread1_Sheet1.ActiveRowIndex;

            //this.SetBookingInfo((FS.HISFC.Models.Registration.Booking)this.fpSpread1_Sheet1.Rows[row].Tag);
        }
        #endregion

        #region ����

        /// <summary>
        /// ��������ȫ�������б�
        /// </summary>
        /// <returns></returns>
        private int InitDept()
        {
            FS.HISFC.BizProcess.Integrate.Manager deptMgr = new FS.HISFC.BizProcess.Integrate.Manager();

            this.alDept = deptMgr.QueryRegDepartment();
            if (alDept == null)
            {
                MessageBox.Show("��ȡ��������б�ʱ����!" + deptMgr.Err, "��ʾ");
                return -1;
            }

            this.cmbDept.AddItems(alDept);

            return 0;
        }
        /// <summary>
        /// ��������ȫ��ҽ���б�
        /// </summary>
        /// <returns></returns>
        private int InitDoct()
        {
            FS.HISFC.BizProcess.Integrate.Manager personMgr = new FS.HISFC.BizProcess.Integrate.Manager();

            alDoct = personMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (alDoct == null)
            {
                MessageBox.Show("��ȡ����ҽ���б�ʱ����!" + personMgr.Err, "��ʾ");
                return -1;
            }

            this.cmbDoct.AddItems(alDoct);

            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitChooseDate()
        {
            this.ucChooseDate = new ucChooseBookingDate();

            this.panel1.Controls.Add(ucChooseDate);

            this.ucChooseDate.BringToFront();
            this.ucChooseDate.Location = new Point(this.dtBookingDate.Left, this.dtBookingDate.Top + this.dtBookingDate.Height);
            this.ucChooseDate.Visible = false;
            this.ucChooseDate.SelectedItem += new Registration.ucChooseBookingDate.dSelectedItem(ucChooseDate_SelectedItem);
        }

        /// <summary>
        /// ���
        /// </summary>
        private void Clear()
        {
            this.cmbDept.Tag = "";
            this.cmbDoct.Tag = ""; 
            this.cmbSex.Text = "��";
            this.cmbDoct.AddItems(this.alDoct);//��ʾȫԺҽ��

            DateTime current = this.bookingMgr.GetDateTimeFromSysDateTime();

            this.SetBookingDate(current);
            this.SetDefaultBookingTime(current);
            this.SetBookingTag(null);

            this.lbOrder.Text = "";

            this.ClearPatient();
        }
        /// <summary>
        /// ���������Ϣ
        /// </summary>
        private void ClearPatient()
        {
            this.txtCardNo.Text = "";
            this.txtCardNo.Enabled = true;
            this.txtName.Text = "";
            this.txtIdenNo.Text = "";
            this.txtPhone.Text = "";
            this.txtAdress.Text = "";

            this.booking = null;
        }

        /// <summary>
        /// ��������ԤԼ��Ϣ
        /// </summary>
        private void Retrieve()
        {
            DateTime today = this.bookingMgr.GetDateTimeFromSysDateTime();
            ArrayList al = this.bookingMgr.Query(today, this.bookingMgr.Operator.ID);

            if (al == null)
            {
                MessageBox.Show("��ȡ����ԤԼ��Ϣʱ����!" + this.bookingMgr.Err, "��ʾ");
                return;
            }

            if (this.fpSpread1_Sheet1.RowCount > 0)
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);

            foreach (FS.HISFC.Models.Registration.Booking obj in al)
            {
                this.AddBookingToFP(obj);
            }
        }
        /// <summary>
        /// ��ȡ����ԤԼ��Ϣ
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Booking getPatientInfo(string CardNo)
        {
            FS.HISFC.Models.Registration.Booking objBooking;

            //�ȼ������߻�����Ϣ��,���Ƿ���ڸû�����Ϣ
            FS.HISFC.BizProcess.Integrate.RADT PatientMgr = new FS.HISFC.BizProcess.Integrate.RADT();
            FS.HISFC.BizLogic.Registration.Register RegMgr = new FS.HISFC.BizLogic.Registration.Register();

            FS.HISFC.Models.RADT.PatientInfo objPatient = PatientMgr.QueryComPatientInfo(CardNo);
            if (objPatient == null || objPatient.Name == "")
            {
                //�����ڻ�����Ϣ,���Ƿ����ԤԼ��Ϣ
                
                objBooking = this.getBooking(CardNo);                
            }
            else
            {
                //���ڻ��߻�����Ϣ,ȡ������Ϣ
                objBooking = new FS.HISFC.Models.Registration.Booking();
                objBooking.PID.CardNO = CardNo;
                objBooking.Name = objPatient.Name;
                objBooking.IDCard = objPatient.IDCard;
                objBooking.Sex.ID = objPatient.Sex.ID;
                objBooking.Birthday = objPatient.Birthday;
                objBooking.PhoneHome = objPatient.PhoneHome;
                objBooking.AddressHome = objPatient.AddressHome;
                objBooking.Pact = objPatient.Pact;
                objBooking.Pact.PayKind.ID = objPatient.Pact.PayKind.ID;
                objBooking.SSN = objPatient.SSN;
                objBooking.Memo = objPatient.Memo;//֤������

            }

            return objBooking;
        }
        /// <summary>
        /// ��ȡ����ԤԼ��Ϣ
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Booking getBooking(string CardNo)
        {
            FS.HISFC.Models.Registration.Booking objBooking;

            objBooking = this.bookingMgr.Get(CardNo);
            if (objBooking == null)
            {
                MessageBox.Show("��ȡ����ԤԼ��Ϣʱ����!" + this.bookingMgr.Err, "��ʾ");
                return null;
            }

            if (objBooking.ID == null || objBooking.ID == "")
            {
                objBooking.PID.CardNO = CardNo;
                objBooking.Pact.PayKind.ID = "01";//�Է�
            }

            objBooking.IsSee = false;

            return objBooking;
        }
        /// <summary>
        /// ���ý�����Ϣ
        /// </summary>
        /// <param name="objBooking"></param>
        private void SetPatient(FS.HISFC.Models.Registration.Booking objBooking)
        {
            this.txtCardNo.Text = objBooking.PID.CardNO;
            this.txtName.Text = objBooking.Name;
            this.txtPhone.Text = objBooking.PhoneHome;
            this.txtAdress.Text = objBooking.AddressHome;
            this.txtIdenNo.Text = objBooking.IDCard;
            this.cmbSex.Text = objBooking.Sex.Name;
            this.dtBirthday.Value = objBooking.Birthday;
            this.setAge(objBooking.Birthday);
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
            if (CardNO != "" && CardNO != string.Empty&&CardNO.Length==10)
            {
                if (CardNO.Substring(0, 1) == cardRule)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�˺Ŷ�Ϊֱ���շ�ʹ�ã���ѡ�������Ŷ�"), FS.FrameWork.Management.Language.Msg("��ʾ"));
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// set booking date
        /// </summary>
        /// <param name="seeDate"></param>
        private void SetBookingDate(DateTime seeDate)
        {
            this.dtBookingDate.Value = seeDate.Date;
            this.lbWeek.Text = this.getWeek(seeDate);

        }
        /// <summary>
        /// ������ԤԼ�Ű���Ϣʱ,ʱ�����ʾ���
        /// </summary>
        /// <param name="seeDate"></param>
        private void SetDefaultBookingTime(DateTime seeDate)
        {
            FS.HISFC.Models.Registration.Schema schema = new FS.HISFC.Models.Registration.Schema();
            schema.Templet.Begin = seeDate.Date;
            schema.Templet.End = seeDate.Date;

            this.SetBookingTime(schema);
        }
        /// <summary>
        /// Set booking time;
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        private void SetBookingTime(FS.HISFC.Models.Registration.Schema schema)
        {
            this.dtBegin.Value = schema.Templet.Begin;
            this.dtEnd.Value = schema.Templet.End;

            this.SetBookingTag(schema);
        }
        /// <summary>
        /// ����ʹ�õ�ԤԼ�Ű���Ϣ
        /// </summary>
        /// <param name="schema"></param>
        private void SetBookingTag(FS.HISFC.Models.Registration.Schema schema)
        {
            this.dtBookingDate.Tag = schema;

            if (schema != null)
            {
                this.lbTelLmt.Text = schema.Templet.TelQuota.ToString();
                this.lbTelReging.Text = schema.TelingQTY.ToString();
            }
            else
            {
                this.lbTelLmt.Text = "0";
                this.lbTelReging.Text = "0";
            }
        }
        /// <summary>
        /// ��ȡʹ�õ�ԤԼ�Ű���Ϣ
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Schema GetBookingTag()
        {
            if (this.dtBookingDate.Tag == null) return null;

            return (FS.HISFC.Models.Registration.Schema)this.dtBookingDate.Tag;
        }

        /// <summary>
        /// ����ԤԼ��ˮ��
        /// </summary>
        private void GetOrder()
        {
            if (this.lbOrder.Text == "")
            {
                this.lbOrder.Text = this.bookingMgr.GetSequence("Registration.Booking.Query.3");
            }
        }
        /// <summary>
        /// �������ڻ�ȡ����
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private string getWeek(DateTime current)
        {
            string[] week = new string[] { "��", "һ", "��", "��", "��", "��", "��" };

            return "����" + week[(int)current.DayOfWeek];
        }
        /// <summary>
        /// �趨����ԤԼ����ʱ���
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="bookingDate"></param>
        /// <returns></returns>
        private int SetDeptZone(string deptID, DateTime bookingDate)
        {
            this.ucChooseDate.QueryDeptBooking(bookingDate, deptID, Registration.RegTypeNUM.Booking);

            //Ĭ����ʾ��һ������������ʱ��δ���ڡ��޶�δ�������Ű���Ϣ
            FS.HISFC.Models.Registration.Schema schema = this.ucChooseDate.GetValidBooking(RegTypeNUM.Booking);

            if (schema == null)//û�з���������
            {
                this.SetDefaultBookingTime(bookingDate.Date);
            }
            else
            {
                this.SetBookingTime(schema);
            }

            return 0;
        }

        /// <summary>
        /// ���ݿ��Ҵ���,����ʱ���ѯ�Ű�ҽ��
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        private int GetDoctByDept(string deptID)
        {
            ArrayList al = this.Mgr.QueryEmployeeForScama(FS.HISFC.Models.Base.EnumEmployeeType.D, deptID);
            if (al == null)
            {
                MessageBox.Show("��ȡ�Ű�ҽ��ʱ����!" + this.Mgr.Err, "��ʾ");
                return -1;
            }

            this.cmbDoct.AddItems(al);

            return 0;
        }
        /// <summary>
        /// ������Ա�����ȡ��Ա��Ϣ
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Base.Employee GetPerson(string ID)
        {
            if (this.alDoct == null) return null;

            foreach (FS.HISFC.Models.Base.Employee p in alDoct)
            {
                if (p.ID == ID) return p;
            }

            return null;
        }
        /// <summary>
        /// �趨ҽ��ԤԼ����ʱ���
        /// </summary>
        /// <param name="doctID"></param>
        /// <param name="bookingDate"></param>
        /// <returns></returns>
        private int SetDoctZone(string doctID, DateTime bookingDate)
        {
            this.ucChooseDate.QueryDoctBooking(bookingDate, doctID, Registration.RegTypeNUM.Booking);

            //Ĭ����ʾ��һ������������ʱ��δ���ڡ��޶�δ�������Ű���Ϣ
            FS.HISFC.Models.Registration.Schema schema = this.ucChooseDate.GetValidBooking(Registration.RegTypeNUM.Booking);

            if (schema == null)//û�з���������
            {
                this.SetDefaultBookingTime(bookingDate.Date);
            }
            else
            {
                this.SetBookingTime(schema);
                //û�п���,ָ������
                if (this.cmbDept.Tag.ToString() == "")
                {
                    this.IsTriggerSelectedIndexChanged = false;
                    this.cmbDept.Tag = schema.Templet.Dept.ID;
                    this.IsTriggerSelectedIndexChanged = true;
                }
            }

            return 0;
        }
        /// <summary>
        /// ����ԤԼʱ���
        /// </summary>
        /// <returns></returns>
        private int SetZone()
        {
            string deptID = this.cmbDept.Tag.ToString();
            string doctID = this.cmbDoct.Tag.ToString();

            DateTime bookingDate = this.dtBookingDate.Value;
            DateTime current = this.bookingMgr.GetDateTimeFromSysDateTime();

            if (bookingDate.Date < current.Date)
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "ԤԼ���ڲ���С�ڵ�ǰ����" ), "��ʾ" );
                this.dtBookingDate.Focus();
                return -1;
            }

            if (doctID == null || doctID == "")//û��ѡ��ҽ��,ԤԼ��ר��
            {
                if (deptID == null || deptID == "")//Ҳû��ԤԼ����,��ʾĬ��
                {
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ָ��ԤԼר��" ), "��ʾ" );
                    this.cmbDoct.Focus();
                    return 0;
                    //this.SetBookingTime(bookingDate,bookingDate) ;
                }
                else//ԤԼ������
                {
                    this.SetDeptZone(deptID, bookingDate);

                    if (this.ucChooseDate.Count > 0)
                    {
                        this.ucChooseDate.Visible = true;
                        this.ucChooseDate.Focus();
                    }
                    else if (this.ucChooseDate.Bookings.Count > 0)
                    {
                        MessageBox.Show( FS.FrameWork.Management.Language.Msg( "û�з����������Ű���Ϣ,������ѡ��ԤԼ����" ), "��ʾ" );
                        this.dtBookingDate.Focus();
                        return -1;
                    }
                    else
                    {
                        MessageBox.Show( FS.FrameWork.Management.Language.Msg( "ר��û���Ű�" ), "��ʾ" );
                        this.dtBookingDate.Focus();
                        return -1;
                    }
                }
            }
            else//ԤԼ��ҽ��
            {
                this.SetDoctZone(doctID, bookingDate);

                if (this.ucChooseDate.Count > 0)
                {
                    this.ucChooseDate.Visible = true;
                    this.ucChooseDate.Focus();
                }
                else if (this.ucChooseDate.Bookings.Count > 0)
                {
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "û�з����������Ű���Ϣ,������ѡ��ԤԼ����" ), "��ʾ" );
                    this.dtBookingDate.Focus();
                    return -1;
                }
                else
                {
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "ר��û���Ű�" ), "��ʾ" );
                    this.dtBookingDate.Focus();
                    return -1;
                }
            }

            return 0;
        }
        /// <summary>
        /// ��ʾ��ԤԼ�Ǽǻ�����Ϣ
        /// </summary>
        /// <param name="booking"></param>
        private void SetBookingInfo(FS.HISFC.Models.Registration.Booking booking)
        {
            this.Clear();

            this.txtCardNo.Text = booking.Card.ID;
            this.txtName.Text = booking.Name;
            this.txtIdenNo.Text = booking.IDCard;
            this.txtPhone.Text = booking.PhoneHome;
            this.txtAdress.Text = booking.AddressHome;

            this.IsTriggerSelectedIndexChanged = false;
            this.cmbDept.Tag = booking.DoctorInfo.Templet.Dept.ID;
            this.cmbDoct.Tag = booking.DoctorInfo.Templet.Doct.ID;
            this.IsTriggerSelectedIndexChanged = true;

            this.dtBookingDate.Value = booking.DoctorInfo.SeeDate;
            this.dtBegin.Value = booking.DoctorInfo.Templet.Begin;
            this.dtEnd.Value = booking.DoctorInfo.Templet.End;

            this.lbOrder.Text = booking.ID;
        }

        
        /// <summary>
        /// ͨ�����������������߹Һ���Ϣ
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private string GetCardNoByName(string Name)
        {
            frmQueryPatientByName f = new frmQueryPatientByName();

            if (f.QueryByName(Name) > 0)
            {
                DialogResult dr = f.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    string CardNo = f.SelectedCardNo;
                    f.Dispose();
                    return CardNo;
                }

                f.Dispose();
            }

            return "";
        }
        #endregion

        #region PageUp,PageDown�л�������ת
        /// <summary>
        /// ������һ���ؼ���ý���
        /// </summary>
        private void setPriorControlFocus()
        {
            System.Windows.Forms.SendKeys.Send("+{TAB}");
        }

        /// <summary>
        /// ������һ���ؼ���ý���
        /// </summary>
        private void setNextControlFocus()
        {
            System.Windows.Forms.SendKeys.Send("{TAB}");
        }
        #endregion

        #region toolbarClick    

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        private int Save()
        {
            if (this.Valid() == -1) return -1;

            if (this.GetValue() == -1) return -1;
            if (this.ValidCardNO(this.booking.PID.CardNO) < 0)
            {
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(this.bookingMgr.con);
            //SQLCA.BeginTransaction();

            try
            {
                this.bookingMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.schemaMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.patMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                //���¿�����
                FS.HISFC.Models.Registration.Schema schema = this.GetBookingTag();

                if (this.schemaMgr.Increase(schema.Templet.ID, false, true, false, false) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ҽ���Ű���Ϣʱ����!" + this.schemaMgr.Err, "��ʾ");
                    return -1;
                }

                schema = this.schemaMgr.GetByID(schema.Templet.ID);
                if (schema == null || schema.Templet.ID == "" || schema.Templet.TelQuota < schema.TelingQTY)
                {
                    if (MessageBox.Show("�Ű���Ϣ�޶�����,�Ƿ����?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.dtBookingDate.Focus();
                        return -1;
                    }
                }

                //�Ǽ�ԤԼ��Ϣ
                this.booking.DoctorInfo.Templet.ID = schema.Templet.ID;


                string Err = "";

                #region ���¿������
                int orderNo = 0;

                //2�������		
                if (this.UpdateSeeID(this.booking.DoctorInfo.Templet.Dept.ID, this.booking.DoctorInfo.Templet.Doct.ID,
                    this.booking.DoctorInfo.Templet.Noon.ID, this.booking.DoctorInfo.SeeDate, ref orderNo,
                    ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Err, "��ʾ");
                    return -1;
                }

                booking.DoctorInfo.SeeNO = orderNo;

                //1ȫԺ��ˮ��			
                if (this.Update(this.regMgr, this.booking.DoctorInfo.SeeDate, ref orderNo, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Err, "��ʾ");
                    return -1;
                }

                booking.OrderNO = orderNo;
                #endregion


                if (this.bookingMgr.Insert(this.booking) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�Ǽǻ���ԤԼ��Ϣʱ����!" + this.bookingMgr.Err, "��ʾ");
                    return -1;
                }

                //���»�����Ϣ
                if (this.UpdatePatientinfo(booking, patMgr, regMgr, ref Err) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���»��߻�����Ϣ����!" + Err, "��ʾ");
                    return -1;
                }

            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("�Ǽǻ���ԤԼ��Ϣʱ����!" + e.Message, "��ʾ");
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(string.Format("����ɹ����Ǽǲ��˵Ŀ������Ϊ{0}({1})",booking.OrderNO,booking.DoctorInfo.SeeNO), "��ʾ" );

            this.AddBookingToFP(this.booking);
            this.Clear();

            this.cmbDoct.Focus();

            return 0;
        }


        #region ���¿������
        /// <summary>
        /// ����ȫԺ�������
        /// </summary>
        /// <param name="rMgr"></param>
        /// <param name="current"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int Update(FS.HISFC.BizLogic.Registration.Register rMgr, DateTime current, ref int seeNo,
            ref string Err)
        {
            //���¿������
            //ȫԺ��ȫ����������������Ч��Ĭ�� 1
            if (rMgr.UpdateSeeNo("4", current, "ALL", "1") == -1)
            {
                Err = rMgr.Err;
                return -1;
            }

            //��ȡȫԺ�������
            if (rMgr.GetSeeNo("4", current, "ALL", "1", ref seeNo) == -1)
            {
                Err = rMgr.Err;
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// ����ҽ������ҵĿ������
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="doctID"></param>
        /// <param name="noonID"></param>
        /// <param name="regDate"></param>
        /// <param name="seeNo"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int UpdateSeeID(string deptID, string doctID, string noonID, DateTime regDate,
            ref int seeNo, ref string Err)
        {
            string Type = "", Subject = "";

            #region ""

            if (doctID != null && doctID != "")
            {
                Type = "1";//ҽ��
                Subject = doctID;
            }
            else
            {
                Type = "2";//����
                Subject = deptID;
            }

            #endregion

            //���¿������
            if (this.regMgr.UpdateSeeNo(Type, regDate, Subject, noonID) == -1)
            {
                Err = this.regMgr.Err;
                return -1;
            }

            //��ȡ�������		
            if (this.regMgr.GetSeeNo(Type, regDate, Subject, noonID, ref seeNo) == -1)
            {
                Err = this.regMgr.Err;
                return -1;
            }

            return 0;
        }

        #endregion

        /// <summary>
        /// ��֤
        /// </summary>
        /// <returns></returns>
        private int Valid()
        {
            DateTime begin = this.dtBegin.Value;
            DateTime end = this.dtEnd.Value;
            if (this.booking == null || this.booking.PID.CardNO == "")
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "������ԤԼ������Ϣ" ), "��ʾ" );
                this.txtCardNo.Focus();
                return -1;
            }
            //			if(this.txtName.Text.Trim() == "")
            //			{
            //				MessageBox.Show("�����뻼������!","��ʾ") ;
            //				this.txtName.Focus() ;
            //				return -1;
            //			}
            //			if(this.txtPhone.Text.Trim() == "")
            //			{
            //				MessageBox.Show("�����뻼�ߵ绰!","��ʾ") ;
            //				this.txtPhone.Focus() ;
            //				return -1 ;
            //			}
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtName.Text.Trim(), 16) == false)
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "�������Ʋ���Ϊ�գ�������¼��20������" ), "��ʾ" );
                this.txtName.Focus();
                return -1;
            }
            if (this.txtName.Text.Trim() == null || this.txtName.Text.Trim() == "")
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "�������Ʋ���Ϊ�գ�������¼��20������" ), "��ʾ" );
                this.txtName.Focus();
                return -1;
            }
            if (FS.FrameWork.Public.String.ValidMaxLengh(this.txtAdress.Text.Trim(), 60) == false)
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ϵ�˵�ַ����¼��30������" ), "��ʾ" );
                this.txtAdress.Focus();
                return -1;
            }
            if ((this.cmbDept.Tag == null || this.cmbDept.Tag.ToString() == "") &&
                (this.cmbDoct.Tag == null || this.cmbDoct.Tag.ToString() == ""))
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ѡ��ԤԼר��" ), "��ʾ" );
                this.cmbDoct.Focus();
                return -1;
            }

            #region add by lijp 2012-08-24 {5195341F-12C4-41cb-B2E0-EB35365990FC}
            if (this.intCanBookingDays != -1)
            {
                if (DateTime.Compare(this.dtBookingDate.Value, this.regMgr.GetDateTimeFromSysDateTime().Date.AddDays(intCanBookingDays)) > 0)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("ֻ����ǰ" + this.IntCanBookingDays + "��ԤԼ��"));
                    return -1;
                }
            }

            if (this.isIdentityCardMustFill)
            {
                if (isIdentityCardMustFill && string.IsNullOrEmpty(this.txtIdenNo.Text))
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("���֤Ϊ������Ϣ��"), "��ʾ");
                    this.txtIdenNo.Focus();
                    return -1;
                }
                //У�����֤��
                if (!string.IsNullOrEmpty(this.txtIdenNo.Text))
                {

                    int reurnValue = FS.HISFC.Components.Registration.Function.ProcessIDENNO(this.txtIdenNo.Text, FS.HISFC.Components.Registration.Function.EnumCheckIDNOType.Saveing);

                    if (reurnValue < 0)
                    {
                        this.txtIdenNo.Focus();
                        return -1;
                    }
                }
            }
            #endregion

            if (begin.TimeOfDay > end.TimeOfDay)
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "ԤԼ��ʼʱ�䲻�ܴ��ڽ���ʱ��" ) );
                return -1;
            }

            #region ""
            /*FS.HISFC.Models.Registration.Schema schema = this.GetBookingTag() ;
			if(schema == null || schema.Templet.ID == null ||schema.Templet.ID == "")
			{
				MessageBox.Show("��ѡ��ԤԼʱ��!","��ʾ") ;
				this.dtBookingDate.Focus() ;
				return -1 ;
			}			
			
			if(this.dtBegin.Value.TimeOfDay >this.dtEnd.Value.TimeOfDay)
			{
				MessageBox.Show("ԤԼ��ʼʱ�䲻�ܴ��ڽ���ʱ��!","��ʾ") ;
				this.dtBegin.Focus() ;
				return -1;
			}			

			DateTime current = this.bookingMgr.GetDateTimeFromSysDateTime() ;
			DateTime bookingDate = DateTime.Parse(this.dtBookingDate.Value.Date.ToString() +" "+this.dtEnd.Value.Hour.ToString() +
				":" + this.dtEnd.Value.Minute.ToString() + ":00") ;
			if(bookingDate <current) 
			{
				MessageBox.Show("ԤԼʱ��С�ڵ�ǰʱ��!","��ʾ") ;
				this.dtBegin.Focus() ;
				return -1 ;
			}*/
            #endregion

            return 0;
        }
        /// <summary>
        /// ʵ�帳ֵ
        /// </summary>
        /// <returns></returns>
        private int GetValue()
        {
            this.booking.ID = this.lbOrder.Text;
            
            //{A47CE41F-3AC7-4289-AFB2-01DC5481F917}
            this.booking.Name = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtName.Text.Trim(), "'", "[", "]");
            //{A47CE41F-3AC7-4289-AFB2-01DC5481F917}
            this.booking.IDCard = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtIdenNo.Text.Trim(),"'","[", "]");
            //{A47CE41F-3AC7-4289-AFB2-01DC5481F917}
            this.booking.PhoneHome = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtPhone.Text.Trim(), "'", "[", "]");
            //{A47CE41F-3AC7-4289-AFB2-01DC5481F917}
            this.booking.AddressHome = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtAdress.Text.Trim(),"'", "[", "]");

            FS.HISFC.Models.Registration.Schema schema = this.GetBookingTag();
            //������������1��û�з����������Ű���Ϣ,2���䶯��ԤԼ���ڡ�ʱ��,�������¼���һ�����ȷ��
            if (schema == null || schema.Templet.ID == null || schema.Templet.ID == "")
            {
                schema = this.GetValidSchema();
                if (schema == null)
                {
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "ԤԼʱ��ָ������,û�з����������Ű���Ϣ" ), "��ʾ" );
                    this.dtBookingDate.Focus();
                    return -1;
                }

                this.SetBookingTag(schema);
            }           

            this.booking.DoctorInfo = schema.Clone();
           
            if (this.booking.DoctorInfo.Templet.Doct.ID == "None") this.booking.DoctorInfo.Templet.Doct.ID = "";
            this.booking.DoctorInfo.SeeDate = DateTime.Parse(schema.SeeDate.ToString("yyyy-MM-dd") + " " +
                                                schema.Templet.Begin.ToString("HH:mm:ss"));
            /*
            this.booking.DoctorInfo.Templet.Begin = schema.Templet.Begin;
            this.booking.DoctorInfo.Templet.End = schema.Templet.End;
            this.booking.DoctorInfo.Templet.Dept.ID = schema.Templet.Dept.ID;
            this.booking.DoctorInfo.Templet.Dept.Name = schema.Templet.Dept.Name;
            this.booking.DoctorInfo.Templet.Doct.ID = schema.Templet.Doct.ID;
            if (this.booking.DoctorInfo.Templet.Doct.ID == "None") this.booking.DoctorInfo.Templet.Doct.ID = "";
            this.booking.DoctorInfo.Templet.Doct.Name = schema.Templet.Doct.Name;
            this.booking.Noon = schema.Templet.NoonID;
            this.booking.IsAppend = schema.Templet.IsAppend;*/
            this.booking.DoctorInfo.Templet.Begin = this.dtBegin.Value;
            this.booking.DoctorInfo.Templet.End = dtEnd.Value;
            this.booking.Oper.ID = this.bookingMgr.Operator.ID;
            this.booking.Oper.OperTime = this.bookingMgr.GetDateTimeFromSysDateTime();

            //{0B4C5A74-98EB-4adc-9E52-47295201EB97}
            this.booking.DoctorInfo.Templet.RegLevel.ID = schema.Templet.RegLevel.ID;
            this.booking.BookingTypeId = this.cmbBookingType.Tag == null? string.Empty:this.cmbBookingType.Tag.ToString();
            this.booking.BookingTypeName = this.cmbBookingType.Text.Trim();
            if (this.cmbSex.Tag != null)
            {
                this.booking.Sex.ID = this.cmbSex.Tag.ToString();
                this.booking.Sex.Name = this.cmbSex.Text;
            }
            this.booking.Birthday = this.dtBirthday.Value;

            return 0;
        }
        /// <summary>
        /// ���»�ȡ��Ч���Ű���Ϣ
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Schema GetValidSchema()
        {
            string deptID = this.cmbDept.Tag.ToString();
            string doctID = this.cmbDoct.Tag.ToString();

            DateTime bookingDate = this.dtBookingDate.Value.Date;

            ArrayList al;

            if (doctID == "")//ԤԼר��
            {
                al = this.schemaMgr.QueryByDept(bookingDate, deptID);
                if (al == null || al.Count == 0) return null;

            }
            else//ԤԼר��
            {
                al = this.schemaMgr.QueryByDoct(bookingDate, doctID);
                if (al == null || al.Count == 0) return null;
            }

            return this.GetValidSchema(al);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Schemas"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.Schema GetValidSchema(ArrayList Schemas)
        {

            DateTime current = this.schemaMgr.GetDateTimeFromSysDateTime();
            DateTime begin = this.dtBegin.Value;
            DateTime end = this.dtEnd.Value;

            foreach (FS.HISFC.Models.Registration.Schema obj in Schemas)
            {
                if (obj.SeeDate < current.Date) continue;//С�ڵ�ǰ����
                //if(obj.Templet.TelLmt == 0)continue ;//û���趨ԤԼ�޶�

                //by niuxy  ���Ű෶Χ������ԤԼʱ��
                //if (obj.Templet.Begin.TimeOfDay != begin.TimeOfDay) continue;//��ʼʱ�䲻��
                //if (obj.Templet.End.TimeOfDay != end.TimeOfDay) continue;//����ʱ�䲻��
                if ((obj.Templet.Begin.TimeOfDay > begin.TimeOfDay) || (obj.Templet.End.TimeOfDay < end.TimeOfDay)) continue;//��ʼʱ�䲻��


                //if(obj.Templet.TelLmt <= obj.TelReging) continue;//�����޶�
                //
                //ֻ��������ͬ,���ж�ʱ���Ƿ�ʱ,�������ԤԼ���Ժ�����,ʱ�䲻���ж�
                //
                if (current.Date == this.dtBookingDate.Value.Date)
                {
                    if (obj.Templet.End.TimeOfDay < current.TimeOfDay) continue;//ʱ��С�ڵ�ǰʱ��
                }

                return obj;
            }
            return null;
        }
        /// <summary>
        /// ���ԤԼ��Ϣ��farpoint
        /// </summary>
        /// <param name="booking"></param>
        private void AddBookingToFP(FS.HISFC.Models.Registration.Booking booking)
        {
            this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);
            int row = this.fpSpread1_Sheet1.RowCount - 1;

            this.fpSpread1_Sheet1.SetValue(row, 0, booking.ID, false);
            this.fpSpread1_Sheet1.SetValue(row, 1, booking.PID.CardNO, false);
            this.fpSpread1_Sheet1.SetValue(row, 2, booking.Name, false);
            this.fpSpread1_Sheet1.SetValue(row, 3, booking.DoctorInfo.Templet.Dept.Name, false);
            this.fpSpread1_Sheet1.SetValue(row, 4, booking.DoctorInfo.Templet.Doct.Name, false);
            this.fpSpread1_Sheet1.SetValue(row, 5, booking.DoctorInfo.SeeDate.ToString("yyyy-MM-dd") + "[" + booking.DoctorInfo.Templet.Begin.ToString("HH:mm") + "~" + booking.DoctorInfo.Templet.End.ToString("HH:mm") + "]", false);
            this.fpSpread1_Sheet1.Rows[row].Tag = booking;
        }
        /// <summary>
        /// ���»��߻�����Ϣ
        /// </summary>
        /// <param name="booking"></param>
        /// <param name="patMgr"></param>
        /// <param name="registerMgr"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int UpdatePatientinfo(FS.HISFC.Models.Registration.Booking booking,
            FS.HISFC.BizProcess.Integrate.RADT patMgr, FS.HISFC.BizLogic.Registration.Register registerMgr,
            ref string Err)
        {
            FS.HISFC.Models.Registration.Register regInfo = new FS.HISFC.Models.Registration.Register();


            regInfo.PID.CardNO = booking.PID.CardNO;
            regInfo.Name = booking.Name;
            regInfo.Sex.ID = booking.Sex.ID;
            regInfo.Birthday = booking.Birthday;
            regInfo.Pact = booking.Pact;
            regInfo.Pact.PayKind.ID = booking.Pact.PayKind.ID;
            regInfo.SSN = booking.SSN;
            regInfo.PhoneHome = booking.PhoneHome;
            regInfo.AddressHome = booking.AddressHome;
            regInfo.IDCard = booking.IDCard;
            regInfo.CardType.ID = booking.Memo;

            int rtn = registerMgr.Update(FS.HISFC.BizLogic.Registration.EnumUpdateStatus.PatientInfo,
                                            regInfo);

            if (rtn == -1)
            {
                Err = registerMgr.Err;
                return -1;
            }

            if (rtn == 0)//û�и��µ�������Ϣ������
            {
                FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();

                p.PID.CardNO = regInfo.PID.CardNO;
                p.Name = regInfo.Name;
                p.Sex.ID = regInfo.Sex.ID;
                p.Birthday = regInfo.Birthday;
                p.Pact = regInfo.Pact;
                p.Pact.PayKind.ID = regInfo.Pact.PayKind.ID;
                p.SSN = regInfo.SSN;
                p.PhoneHome = regInfo.PhoneHome;
                p.AddressHome = regInfo.AddressHome;
                p.IDCard = regInfo.IDCard;
                p.Memo = regInfo.CardType.ID;

                if (patMgr.RegisterComPatient(p) == -1)
                {
                    Err = patMgr.Err;
                    return -1;
                }
            }

            return 0;
        }
        /// <summary>
        /// ɾ��
        /// </summary>
        /// <returns></returns>
        private int Delete()
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;

            if (row < 0 || this.fpSpread1_Sheet1.RowCount == 0) return 0;

            if (this.Delete((FS.HISFC.Models.Registration.Booking)this.fpSpread1_Sheet1.Rows[row].Tag) == -1)
            {
                return -1;
            }

            this.fpSpread1_Sheet1.Rows.Remove(row, 1);

            return 0;
        }
        private int CancelAppointment()
        {
            Forms.frmInputSerialNo newfrm = new Forms.frmInputSerialNo();
            if (newfrm.ShowDialog() == DialogResult.OK)
            {
                Models.Registration.Booking booking = bookingMgr.GetByID(newfrm.SerialNo);
                if (booking.IsSee == true)
                {
                    MessageBox.Show("��ԤԼ�ѿ���,������ɾ��");
                    return -1;
                }
                if (booking == null||string.IsNullOrEmpty(booking.ID))
                {
                    MessageBox.Show("û�ҵ���ԤԼ��Ϣ");
                    return -1;
                }
                else return Delete(booking);
            }
            return -1;
        }
        /// <summary>
        /// ɾ��ԤԼ��Ϣ
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private int Delete(FS.HISFC.Models.Registration.Booking b)
        {
            if (MessageBox.Show("�Ƿ�ɾ����" + b.Name + "����ԤԼ��" + b.DoctorInfo.Templet.Doct.Name +"����"+b.DoctorInfo.Templet.Begin +"����Ϣ?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1) == DialogResult.No) return -1;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(this.bookingMgr.con);
            //SQLCA.BeginTransaction();

            try
            {
                this.bookingMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.schemaMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                int rtn = this.bookingMgr.Delete(b.ID);

                if (rtn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("ɾ��ԤԼ��Ϣʱ����!" + this.bookingMgr.Err, "��ʾ");
                    return -1;
                }

                if (rtn == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ԤԼ��Ϣ�Ѿ��Һ�,����ɾ��" ), "��ʾ" );
                    return -1;
                }

                ///�ָ�ԤԼ�����޶�
                ///
                rtn = this.schemaMgr.Reduce(b.DoctorInfo.Templet.ID, false, true, false, false);
                if (rtn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ԤԼ�޶����!" + this.schemaMgr.Err, "��ʾ");
                    return -1;
                }
                if (rtn == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "�����Ű���Ϣ,�޷��ָ��޶�" ), "��ʾ" );
                    return -1;
                }

                FS.HISFC.Models.Registration.AppointmentOrder appOrder = appointmentMgr.QueryAppointmentOrderBySerialNO(b.ID);
                //�����Ϊ�յĻ�,��ô˵���������������ԤԼ�����ĺ�
                if (appOrder != null)
                {
                    rtn = appointmentMgr.UpdateAppointmentOrder(appOrder.OrderID, "2", FS.FrameWork.Management.Connection.Operator.ID);
                    if (rtn > 0)
                    {
                        AppointmentService appointmentService = new AppointmentService();
                        //���߳��첽����WebService֪ͨ������ add by yerl
                        appointmentService.Invoke(AppointmentService.funs.cancelOrderbyHis,
                            new AppointmentService.InvokeCompletedEventHandler(appointmentService_InvokeCompleted),
                            appOrder.OrderID, 
                            "����ȡ��ԤԼ",
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            AppointmentService.funs.cancelOrderbyHis.ToString());
                    }
                }
                
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show( FS.FrameWork.Management.Language.Msg( "ɾ���ɹ�" ), "��ʾ" );

            return 0;
        }

        /// <summary>
        /// �첽��ȡWeb���ý��
        /// </summary>
        /// <param name="result"></param>
        private void appointmentService_InvokeCompleted(AppointmentService.InvokeResult result)
        {
            if (result.ResultCode == "0")
                MessageBox.Show("֪ͨ��������ȡ���ɹ�");
            else
                MessageBox.Show("֪ͨ��������ȡ��ʧ��,ԭ��: " + result.ResultDesc);
        }

        #endregion
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                bool IsSelected = false;

                if (this.ucChooseDate.Visible)
                {
                    IsSelected = true;

                    this.ucChooseDate.Visible = false;
                    this.dtBookingDate.Focus();
                }

                if (!IsSelected)
                {
                    this.FindForm().Close();
                }

                return true;
            }// �Զ����ݼ����� {6A58ADC6-04D1-48a5-AF0C-82B730D55094}
            //else if (keyData == Keys.F8)
            //{
            //    this.Clear();
            //    this.cmbDoct.Focus();

            //    return true;
            //}            
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.X.GetHashCode())
            {
                this.FindForm().Close();
                return true;
            }//�Զ����ݼ����� {6A58ADC6-04D1-48a5-AF0C-82B730D55094}
            //else if (keyData == Keys.F12)
            //{
            //    this.Save();

            //    return true;
            //}
            //else if (keyData == Keys.Subtract || keyData == Keys.OemMinus)
            //{
            //    this.Delete();
            //    this.cmbDoct.Focus();

            //    return true;
            //}
            //else if (keyData == Keys.F1)
            //{
            //    this.Switch();
            //    return true;
            //}
            //else if (keyData == Keys.F9)
            //{
            //    this.ChangeCard();
            //}

            return base.ProcessDialogKey(keyData);
        }
        /// <summary>
        /// �л����ҺŴ���
        /// </summary>
        private void Switch()
        {
            //Form[] forms = this.ParentForm.MdiChildren;

            //foreach (Form f in forms)
            //{
            //    if (f.GetType().FullName == "Registration.frmRegister")
            //    {
            //        f.Show();
            //        f.BringToFront();
            //        return;
            //    }
            //}

            //frmRegister form = new frmRegister(var);

            //form.MdiParent = this.ParentForm;
            //form.Show();
        }

        /// <summary>
        /// ����
        /// </summary>
        private void ChangeCard()
        {
            //Local.Clinic.Form.frmCreateCard f = new Local.Clinic.Form.frmCreateCard(var);
            //f.ShowDialog();
            //f.Dispose();
        }


        /// <summary>
        /// ��ȡ��������
        /// </summary>
        private void getBirthday()
        {
            string age = this.txtAge.Text.Trim();
            int i = 0;

            if (age == "") age = "0";

            try
            {
                i = int.Parse(age);
            }
            catch (Exception e)
            {
                string error = e.Message;
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�������䲻��ȷ,����������"), "��ʾ");
                this.txtAge.Focus();
                return;
            }

            if (FS.FrameWork.Function.NConvert.ToInt32(age) > 110)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����������,����������"), "��ʾ");
                this.txtAge.Focus();
                return;
            }
            ///
            ///

            DateTime birthday = DateTime.MinValue;

            this.getBirthday(i, this.cmbUnit.Text, ref birthday);

            if (birthday < this.dtBirthday.MinDate)
            {
                MessageBox.Show("���䲻�ܹ���!", "��ʾ");
                this.txtAge.Focus();
                return;
            }

            //this.dtBirthday.Value = birthday ;

            if (this.cmbUnit.Text == "��")
            {

                //���ݿ��д���ǳ�������,������䵥λ����,��������ĳ������ں����ݿ��г������������ͬ
                //�Ͳ��������¸�ֵ,��Ϊ����ĳ�����������Ϊ����,���������ݿ���Ϊ׼

                if (this.dtBirthday.Value.Year != birthday.Year)
                {
                    this.dtBirthday.Value = birthday;
                }
            }
            else
            {
                this.dtBirthday.Value = birthday;
            }
        }


        /// <summary>
        /// ��������õ���������
        /// </summary>
        /// <param name="age"></param>
        /// <param name="ageUnit"></param>
        /// <param name="birthday"></param>
        private void getBirthday(int age, string ageUnit, ref DateTime birthday)
        {
            DateTime current = this.regMgr.GetDateTimeFromSysDateTime();

            if (ageUnit == "��")
            {
                birthday = current.AddYears(-age);
            }
            else if (ageUnit == "��")
            {
                birthday = current.AddMonths(-age);
            }
            else if (ageUnit == "��")
            {
                birthday = current.AddDays(-age);
            }
        }


        /// <summary>
        /// Set Age
        /// </summary>
        /// <param name="birthday"></param>
        private void setAge(DateTime birthday)
        {
            this.txtAge.Text = "";

            if (birthday == DateTime.MinValue)
            {
                return;
            }

            DateTime current;
            int year = 0, month = 0, day = 0;

            current = this.regMgr.GetDateTimeFromSysDateTime();
            this.regMgr.GetAge(birthday, current, ref year, ref month, ref day);
            //year = current.Year - birthday.Year;
            //month = current.Month - birthday.Month;
            //day = current.Day - birthday.Day;

            if (year > 1)
            {
                this.txtAge.Text = year.ToString();
                this.cmbUnit.SelectedIndexChanged -= this.cmbUnit_SelectedIndexChanged;
                this.cmbUnit.SelectedIndex = 0;
                this.cmbUnit.SelectedIndexChanged += this.cmbUnit_SelectedIndexChanged;
            }
            else if (year == 1)
            {
                if (month >= 0)//һ��
                {
                    this.txtAge.Text = year.ToString();
                    this.cmbUnit.SelectedIndexChanged -= this.cmbUnit_SelectedIndexChanged;
                    this.cmbUnit.SelectedIndex = 0;
                    this.cmbUnit.SelectedIndexChanged += this.cmbUnit_SelectedIndexChanged;
                }
                else
                {
                    this.txtAge.Text = Convert.ToString(12 + month);
                    this.cmbUnit.SelectedIndexChanged -= this.cmbUnit_SelectedIndexChanged;
                    this.cmbUnit.SelectedIndex = 1;
                    this.cmbUnit.SelectedIndexChanged += this.cmbUnit_SelectedIndexChanged;
                }
            }
            else if (month > 0)
            {
                this.txtAge.Text = month.ToString();
                this.cmbUnit.SelectedIndexChanged -= this.cmbUnit_SelectedIndexChanged;
                this.cmbUnit.SelectedIndex = 1;
                this.cmbUnit.SelectedIndexChanged += this.cmbUnit_SelectedIndexChanged;
            }
            else if (day > 0)
            {
                this.txtAge.Text = day.ToString();
                this.cmbUnit.SelectedIndexChanged -= this.cmbUnit_SelectedIndexChanged;
                this.cmbUnit.SelectedIndex = 2;
                this.cmbUnit.SelectedIndexChanged += this.cmbUnit_SelectedIndexChanged;
            }

        }

        private void txtAge_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.getBirthday();

                this.cmbUnit.Focus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        private void dtBirthday_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DateTime current = this.regMgr.GetDateTimeFromSysDateTime().Date;

                if (this.dtBirthday.Value.Date > current)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�������ڲ��ܴ��ڵ�ǰʱ��"), "��ʾ");
                    this.dtBirthday.Focus();
                    return;
                }

                //��������
                if (this.dtBirthday.Value.Date != current)
                {
                    this.setAge(this.dtBirthday.Value);
                }

            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        private void cmbSex_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.cmbSex.Text.Trim().Length > 0 && this.cmbSex.Text.Trim().Length < 2)
                {
                    try
                    {
                        int intsex = int.Parse(this.cmbSex.Text);
                        switch (intsex)
                        {
                            case 1:
                                this.cmbSex.Tag = "M";
                                break;
                            case 2:
                                this.cmbSex.Tag = "F";
                                break;
                            default:
                                break;
                        }
                    }
                    catch
                    {

                    }

                }
                if (this.cmbSex.Tag == null || this.cmbSex.Tag.ToString() == "")
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ѡ�����Ա�"), "��ʾ");
                    this.cmbSex.Focus();
                    return;
                }

                this.dtBirthday.Focus();

            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.getBirthday();
        }
        private void cmbUnit_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtPhone.Focus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                //������ת
                this.setPriorControlFocus();
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                this.setNextControlFocus();
            }
        }

        #region ɾ��ԤԼ��Ϣ

        bool isLeave = true;
        /// <summary>
        /// ������ˮ�ż���ԤԼ��Ϣ,Ȼ��ɾ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label13_Click(object sender, EventArgs e)
        {
            this.isLeave = true;
            this.txtOrder.Visible = true;
            this.txtOrder.Focus();
        }


        /// <summary>
        /// ɾ��ԤԼ��Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                this.isLeave = false;
                string ID = this.txtOrder.Text.Trim();

                if (ID == "")
                {
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "��ָ����ˮ��" ), "��ʾ" );
                    this.txtOrder.Focus();
                    this.isLeave = true;
                    return;
                }
                //��ȡԤԼʵ��

                FS.HISFC.Models.Registration.Booking b = this.bookingMgr.GetByID(ID);
                if (b == null || b.ID == "")
                {
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "û�ж�Ӧ����ˮ�ŵ�ԤԼ��Ϣ" ), "��ʾ" );
                    this.txtOrder.Focus();
                    this.isLeave = true;
                    return;
                }

                this.SetBookingInfo(b);

                //ɾ��ԤԼ��Ϣ
                if (this.Delete(b) == -1)
                {
                    this.txtOrder.Focus();
                    this.isLeave = true;
                    return;
                }

                //this.txtOrder.Visible = false ;				
                this.isLeave = true;
                this.Retrieve();
                this.Clear();
                this.cmbDoct.Focus();

            }
        }


        private void txtOrder_Validating(object sender, CancelEventArgs e)
        {
            if (this.isLeave)
            {
                this.txtOrder.Visible = false;
            }
        }
        #endregion

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            //����{6A58ADC6-04D1-48a5-AF0C-82B730D55094}
            //this.toolBarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.A����, true, false, null);
            this.toolBarService.AddToolButton("ɾ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            this.toolBarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            toolBarService.AddToolButton("ˢ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            toolBarService.AddToolButton("ȡ��ԤԼ", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            return this.toolBarService;
        }

        //�¼�{6A58ADC6-04D1-48a5-AF0C-82B730D55094}
        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return base.OnSave(sender, neuObject);
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "ȡ��ԤԼ":
                    CancelAppointment();
                    break;

                //���� {6A58ADC6-04D1-48a5-AF0C-82B730D55094}
                //case "����":
                //    this.Save();

                //    break;
                case "ɾ��":
                    this.Delete();
                    this.cmbDoct.Focus();

                    break;
                case "����":
                    this.Clear();
                    this.cmbDoct.Focus();

                    break;
                case "ˢ��":
                    {
                        string cardNo = "";
                        string error = "";
                        if (Function.OperCard(ref cardNo, ref error) == -1)
                        {
                            CommonController.Instance.MessageBox(error, MessageBoxIcon.Error);
                            return;
                        }

                        txtCardNo.Text = cardNo;
                        txtCardNo.Focus();
                        this.txtCardNo_KeyDown(null, new KeyEventArgs(Keys.Enter));

                        break;
                    }
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// ԤԼ�������� add by lijp 2012-08-24 {5195341F-12C4-41cb-B2E0-EB35365990FC}
        /// </summary>
        private int intCanBookingDays;

        /// <summary>
        /// ԤԼ�������� add by lijp 2012-08-24 {5195341F-12C4-41cb-B2E0-EB35365990FC}
        /// </summary>
        [Category("�ؼ�����"), Description("��ԤԼ�Һ�����,��д������"), DefaultValue("")]
        public string IntCanBookingDays
        {
            set
            {
                try
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        intCanBookingDays = -1;
                    }
                    else
                    {
                        intCanBookingDays = Convert.ToInt32(value);
                    }
                    //this.dtBookingDate.MinDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(-1);
                    //this.dtBookingDate.MaxDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(intCanBookingDays);
                }
                catch
                {
                    intCanBookingDays = -1;
                    // Do nothing.
                }
            }
            get
            {
                return intCanBookingDays.ToString();
            }
        }

        /// <summary>
        /// �Ƿ����֤�ű��� add by lijp 2012-08-23 {5195341F-12C4-41cb-B2E0-EB35365990FC}
        /// </summary>
        private bool isIdentityCardMustFill = false;

        /// <summary>
        /// �Ƿ����֤�ű��� add by lijp 2012-08-23 {5195341F-12C4-41cb-B2E0-EB35365990FC}
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ����֤�ű���"), DefaultValue(false)]
        public bool IsIdentityCardMustFill
        {
            set
            {
                isIdentityCardMustFill = value;

                if (isIdentityCardMustFill)
                {
                    this.label7.ForeColor = Color.Blue;
                }
                else
                {
                    this.label7.ForeColor = Color.Black;
                }
            }
            get
            {
                return isIdentityCardMustFill;
            }
        }

        #region by lijp 2012-09-25 {C108A02B-D1A3-4c0b-B024-67EECA401A6C}
        /// <summary>
        /// �Ƿ���ʾԤԼ����
        /// </summary>
        private bool isShowBookingType = false;

        /// <summary>
        /// �Ƿ���ʾԤԼ����
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ���ʾԤԼ����"), DefaultValue(false)]
        public bool IsShowBookingType
        {
            get { return isShowBookingType; }
            set { isShowBookingType = value; }
        }
        #endregion
    }
}
