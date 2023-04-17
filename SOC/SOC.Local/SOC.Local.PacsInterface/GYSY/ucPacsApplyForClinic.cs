using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace SOC.Local.PacsInterface.GYSY
{
	/// <summary>
	/// ucPacsApply ��ժҪ˵����
	/// </summary>
	partial class ucPacsApplyForClinic : System.Windows.Forms.UserControl
    {

        #region ����

        private FS.HISFC.Models.Order.PacsBill pacsbill = null;
        private FS.HISFC.Models.Registration.Register myReg = null;

        FS.HISFC.BizLogic.Manager.Person person = new FS.HISFC.BizLogic.Manager.Person();

        FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();

        FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        FS.HISFC.BizLogic.Fee.UndrugPackAge ztManager = new FS.HISFC.BizLogic.Fee.UndrugPackAge();

        FS.HISFC.BizLogic.Manager.Constant cnstManager = new FS.HISFC.BizLogic.Manager.Constant();

        FS.HISFC.BizLogic.Registration.Register regManager = new FS.HISFC.BizLogic.Registration.Register();

        FS.HISFC.BizLogic.Order.PacsBill PacsBillManager = new FS.HISFC.BizLogic.Order.PacsBill();

        // FS.HISFC.BizLogic.Order.PacsCompare compare = new FS.HISFC.BizLogic.Order.PacsCompare();

        private ArrayList alitems;//������Ŀ

        private ArrayList al;//���

        string itemCode;//��Ŀ����

        decimal tot_cost = 0m;//�ܼ�Ǯ

        DataSet dsPacsItems = new DataSet();

        Hashtable hsPatient = new Hashtable();

        DateTime dtBegin;

        DateTime dtend;

        /// <summary>
        /// ��Ŀ��Ϻ�
        /// </summary>
        private string billNo;


        public delegate void PacsApplyHandlerForClinic(string CheckPart, string memo);

        /// <summary>
		/// ����
		/// </summary>
		public FS.HISFC.Models.Registration.Register reg
		{
		
			get
			{
				return this.myReg;
			}
			set
			{
				if(value ==null) return;
				this.myReg = value;
                this.lblAge.Text = SOC.Local.PacsInterface.GYSY.PacsCompareFunction.GetAge(myReg.Birthday);								//����
				this.lblName.Text =this.myReg.Name;	//����
				if(this.pacsbill == null || this.pacsbill.Dept.Name == null || this.pacsbill.Dept.Name.Length <= 0)
				{
					//this.lblNurseStation.Text = (this.cnstManager.Operator as FS.HISFC.Models.RADT.Person).Dept.Name;	//����
				}
				else
				{
					this.lblDept.Text = this.pacsbill.Dept.Name;
				}
				this.lblAddressPhone.Text = this.myReg.AddressHome + "/" + this.myReg.PhoneHome;  //סַ/�绰
				this.lblPatientNo.Text ="*" + this.myReg.Card.ID + "*";						//סԺ��
                this.npbBarCode.Image = this.CreateBarCode(myReg.PID.CardNO);
				this.label7.Text=this.myReg.PID.CardNO; //����� 20111201 by lou
				this.lblPaykind.Text = this.myReg.Pact.Name;						//�������
				this.lblMedicalCardNo.Text = this.myReg.SSN;        //ҽ��֤����
				this.lblSex.Text = this.myReg.Sex.Name;						//�Ա�
				#region ���
				try
				{     
					this.txtDiagnose.Text = "";
                    al = this.diagManager.QueryCaseDiagnoseForClinic(this.myReg.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
					if(al!= null)
					{
						this.txtDiagnose.Text = "";
						for(int i=0;i<al.Count;i++)
						{
							FS.HISFC.Models.HealthRecord.Diagnose diag = al[i] as FS.HISFC.Models.HealthRecord.Diagnose;
							int j=i+1;
							this.txtDiagnose.Text += "���"+j.ToString()+"��"+diag.DiagInfo.ICD10.Name+"\n";
						}
					}
				}
				catch
				{}
				#endregion
			}
        }

        /// <summary>
        /// ��Ŀ��Ϣ
        /// </summary>
        public ArrayList alItems
        {
            get
            {
                return this.alitems;
            }
            set
            {
                if (value == null) return;
                this.alitems = value;

                #region �豸����
                ArrayList alMachineType = this.cnstManager.GetAllList("MACHINETYPE");
                this.cmbMachine.SelectedIndexChanged -= new EventHandler(cmbMachine_SelectedIndexChanged);
                this.cmbMachine.AddItems(alMachineType);
                this.cmbMachine.SelectedIndexChanged += new EventHandler(cmbMachine_SelectedIndexChanged);
                #endregion

                FS.HISFC.Models.Fee.Item.Undrug myItem = null;//����ʵ��

                FS.HISFC.Models.Order.Order order = value[0] as FS.HISFC.Models.Order.Order;

                this.pacsbill = PacsBillManager.QueryPacsBill(order.Combo.ID.ToString());//��ѯ

                this.lblExeDept.Text = order.ExeDept.Name;

                this.lblDept.Text = order.ReciptDept.Name;

                string queryItemCode = "";

                for (int i = value.Count - 1; i >= 0; i--)
                {
                    queryItemCode += (value[i] as FS.HISFC.Models.Order.Order).Item.ID + "','";
                }

                if (queryItemCode.Length > 0)
                {
                    queryItemCode = queryItemCode.Remove(queryItemCode.Length - 3, 3);
                }

                FS.HISFC.Models.Order.Order tempOrder = value[0] as FS.HISFC.Models.Order.Order;

                if (this.pacsbill == null) //�µ�
                {

                    this.txtHistory.Text = "";//��ʷ������
                    this.billNo = order.Combo.ID;
                    this.lblPacsBillID.Text += order.Combo.ID.ToString();//���뵥��
                    this.txtMemo.Text = "";//��ע
                    this.txtAttention.Text = "";//ע������
                    if ((value[0] as FS.HISFC.Models.Order.Order).IsEmergency)
                        this.lblEmergency.Visible = true;//�Ӽ�

                    this.lblDate.Text += cnstManager.GetSysDate();	//��������
                  
                    this.lblDoc.Text = this.person.Operator.ToString();//����ҽʦ

                    #region ��Ŀ����
                    for (int i = value.Count - 1; i >= 0; i--)
                    {
                        myItem = (value[i] as FS.HISFC.Models.Order.Order).Item as FS.HISFC.Models.Fee.Item.Undrug;
                        if (myItem == null)
                        {
                            MessageBox.Show("ҽ��ʵ��ת�ɷ�ҩƷʧ��!", "SORRY");
                            return;
                        }

                        //������Ŀ
                        if (myItem.UnitFlag == "1")
                        {
                           //�õ��۸�
                            decimal price = ztManager.GetUndrugCombPrice(myItem.ID);
                            this.txtItems.Text += myItem.Name + "\n";
                        }
                        else
                        {
                            this.txtItems.Text += myItem.Name + "\n";
                        }
                    }

                    //��Ŀ����
                    this.lblApplyName.Text = myItem.SpecialFlag4;
                    //ע������
                    this.txtAttention.Text = myItem.SpecialFlag3 == "0" ? "" : myItem.SpecialFlag3;
                    //��ʷ�����
                    this.txtHistory.Text = myItem.SpecialFlag1 == "0" ? "" : myItem.SpecialFlag1;
                    //���Ҫ��
                    this.txtItems.Text += myItem.SpecialFlag2;	 
                    #endregion
                 
                }
                else
                {
                    this.billNo = pacsbill.ComboNO;
                    this.tot_cost = pacsbill.TotCost;//�ܷ���
                    foreach (FS.FrameWork.Models.NeuObject obj in this.cmbMachine.alItems)
                    {
                        if (obj.Name == pacsbill.MachineType)
                            this.cmbMachine.Tag = obj.ID;//�豸����

                    }
                    this.cmbMachine.Text = pacsbill.MachineType;

                    this.cmbPacsCheckType0.Items.Clear();
                    this.cmbPacsCheckType1.Items.Clear();
                    this.cmbPacsCheckType2.Items.Clear();
                    this.cmbPacsCheckType3.Items.Clear();
                    this.cmbPacsCheckType4.Items.Clear();

                    this.cmbPacsItem0.Items.Clear();
                    this.cmbPacsItem1.Items.Clear();
                    this.cmbPacsItem2.Items.Clear();
                    this.cmbPacsItem3.Items.Clear();
                    this.cmbPacsItem4.Items.Clear();

                    this.dsPacsItems = new DataSet();

                    SOC.Local.PacsInterface.GYSY.PacsCompareFunction.GetPacsItemList(ref this.dsPacsItems);
					
                    Hashtable hsItems = new Hashtable();
					ArrayList alPacsItems = new ArrayList();

					foreach(DataRow row in this.dsPacsItems.Tables[0].Rows)
					{
						if(row["MACHINE_TYPE"].ToString() == this.cmbMachine.Text)
						{
							if(!hsItems.Contains(row["ITEM_NAME"].ToString()))
							{
								hsItems.Add(row["ITEM_NAME"].ToString(),null);

								FS.HISFC.Models.Base.Spell obj = new FS.HISFC.Models.Base.Spell();
						
								obj.ID = row["ITEM_CODE"].ToString();
								obj.Name = row["ITEM_NAME"].ToString();
								obj.SpellCode = row["SPELL_CODE"].ToString();

								alPacsItems.Add(obj);
							}
						}
					}

                    this.cmbPacsItem0.SelectedIndexChanged -= new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
                    this.cmbPacsItem1.SelectedIndexChanged -= new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
                    this.cmbPacsItem2.SelectedIndexChanged -= new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
                    this.cmbPacsItem3.SelectedIndexChanged -= new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
                    this.cmbPacsItem4.SelectedIndexChanged -= new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);

                    this.cmbPacsItem0.AddItems(alPacsItems);
                    this.cmbPacsItem1.AddItems(alPacsItems); ;
                    this.cmbPacsItem2.AddItems(alPacsItems);
                    this.cmbPacsItem3.AddItems(alPacsItems);
                    this.cmbPacsItem4.AddItems(alPacsItems);

                    this.cmbPacsItem0.SelectedIndexChanged += new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
                    this.cmbPacsItem1.SelectedIndexChanged += new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
                    this.cmbPacsItem2.SelectedIndexChanged += new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
                    this.cmbPacsItem3.SelectedIndexChanged += new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
                    this.cmbPacsItem4.SelectedIndexChanged += new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);

                    string[] myPacsBill = pacsbill.PacsItem.Split('|');
                    int i = myPacsBill.Length;
                    if (i == 2)
                    {
                        this.cmbPacsItem0.Text = myPacsBill[0];
                        this.cmbPacsCheckType0.Text = myPacsBill[1];
                    }
                    if (i == 4)
                    {
                        this.cmbPacsItem0.Text = myPacsBill[0];
                        this.cmbPacsCheckType0.Text = myPacsBill[1];
                        this.cmbPacsItem1.Text = myPacsBill[2];
                        this.cmbPacsCheckType1.Text = myPacsBill[3];
                    }
                    if (i == 6)
                    {
                        this.cmbPacsItem0.Text = myPacsBill[0];
                        this.cmbPacsCheckType0.Text = myPacsBill[1];
                        this.cmbPacsItem1.Text = myPacsBill[2];
                        this.cmbPacsCheckType1.Text = myPacsBill[3];
                        this.cmbPacsItem2.Text = myPacsBill[4];
                        this.cmbPacsCheckType2.Text = myPacsBill[5];
                    }
                    if (i == 8)
                    {
                        this.cmbPacsItem0.Text = myPacsBill[0];
                        this.cmbPacsCheckType0.Text = myPacsBill[1];
                        this.cmbPacsItem1.Text = myPacsBill[2];
                        this.cmbPacsCheckType1.Text = myPacsBill[3];
                        this.cmbPacsItem2.Text = myPacsBill[4];
                        this.cmbPacsCheckType2.Text = myPacsBill[5];
                        this.cmbPacsItem3.Text = myPacsBill[6];
                        this.cmbPacsCheckType3.Text = myPacsBill[7];
                    }
                    if (i == 10)
                    {
                        this.cmbPacsItem0.Text = myPacsBill[0];
                        this.cmbPacsCheckType0.Text = myPacsBill[1];
                        this.cmbPacsItem1.Text = myPacsBill[2];
                        this.cmbPacsCheckType1.Text = myPacsBill[3];
                        this.cmbPacsItem2.Text = myPacsBill[4];
                        this.cmbPacsCheckType2.Text = myPacsBill[5];
                        this.cmbPacsItem3.Text = myPacsBill[6];
                        this.cmbPacsCheckType3.Text = myPacsBill[7];
                        this.cmbPacsItem4.Text = myPacsBill[8];
                        this.cmbPacsCheckType4.Text = myPacsBill[9];
                    }


                    foreach (FS.FrameWork.Models.NeuObject obj in this.cmbPacsItem0.alItems)
                    {
                        if (obj.Name == this.cmbPacsItem0.Text)
                            this.cmbPacsItem0.Tag = obj.ID;
                    }

                    this.txtMemo.Text = pacsbill.Memo;//��ע
                    if (pacsbill.Caution != "")//ע������
                    {
                        if (pacsbill.Caution.IndexOf("True") != -1)
                        {
                            this.txtAttention.Text = pacsbill.Caution.Substring(4) == "0" ? "" : pacsbill.Caution.Substring(4);
                        }
                        else
                            this.txtAttention.Text = pacsbill.Caution.Substring(5) == "0" ? "" : pacsbill.Caution.Substring(5);
                    }
                   
                    //��֤�޸�ҽ���Ӽ���־�ܹ���Ӧ����
                    if ((value[0] as FS.HISFC.Models.Order.Order).IsEmergency)
                        this.lblEmergency.Visible = true;//�Ӽ�
                   
                    this.txtDiagnose.Text = "";
                    if (pacsbill.Diagnose1 != null && pacsbill.Diagnose1 != "")//���1
                    {
                        this.txtDiagnose.Text += "���1��" + pacsbill.Diagnose1 + "\n"; ;
                    }

                    if (pacsbill.Diagnose2 != null && pacsbill.Diagnose2 != "")//���2
                    {
                        this.txtDiagnose.Text += "���2��" + pacsbill.Diagnose2 + "\n"; ;
                    }

                    if (pacsbill.Diagnose3 != null && pacsbill.Diagnose3 != "")//���3
                    {
                        this.txtDiagnose.Text += "���3��" + pacsbill.Diagnose3;
                    }

                    this.lblDate.Text = pacsbill.ApplyDate;//��������
                    this.txtItems.Text = pacsbill.CheckOrder;//�����Ŀ
                    this.txtHistory.Text = pacsbill.IllHistory == "0" ? "" : pacsbill.IllHistory;//��ʷ
                    this.itemCode = pacsbill.ItemCode;//��Ŀ����
                    this.lblPacsBillID.Text = pacsbill.ComboNO;//���뵥��
                    this.lblApplyName.Text = pacsbill.BillName;//���뵥����
                    this.lblDoc.Text = pacsbill.Doctor.Name;//ҽ������

                    //this.dtSample.Value = FS.FrameWork.Function.NConvert.ToDateTime(pacsbill.SampleDate);
                    //this.dtJJ.Value = FS.FrameWork.Function.NConvert.ToDateTime(pacsbill.YJDate);
                    //this.chkJJ.Checked = pacsbill.JJ;
                }

            }
        }
        /// <summary>
        /// ��ǰ���뵥
        /// </summary>
        public FS.HISFC.Models.Order.PacsBill PacsBill
        {
            set
            {
                this.pacsbill = value;
                this.SetPacsBill();
            }
        }
        /// <summary>
        /// ��ʼ����
        /// </summary>
        public DateTime DtBegin
        {
            get
            {
                return this.dtBegin;
            }
            set
            {
                this.dtBegin = value;
            }
        }
        /// <summary>
        /// ��ֹ����
        /// </summary>
        public DateTime DtEnd
        {
            get
            {
                return this.dtend;
            }
            set
            {
                this.dtend = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ȡҽԺ���ƺ�ҽԺLOGO
        /// </summary>
        private void GetHospLogo()
        {
            string erro = "����";
            string imgpath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + SOC.Local.PacsInterface.GYSY.PacsCompareFunction.GetHospitalLogo("Xml\\HospitalLogoInfo.xml", "Hospital", "Logo", erro);
            picbLogo.Image = Image.FromFile(imgpath);
        }

        /// <summary>
		/// ��ʾ���뵥
		/// </summary>
		private void SetPacsBill()
		{
			this.txtDiagnose.Text = "";
			this.billNo = pacsbill.ComboNO;
			this.tot_cost = pacsbill.TotCost;//�ܷ���
			
			this.cmbMachine.Text = pacsbill.MachineType;//�豸����
			this.txtResult.Text = pacsbill.LisResult;//�����
			this.txtMemo.Text = pacsbill.Memo;//��ע
			if(pacsbill.Caution != "")//ע������
			{
				if(pacsbill.Caution.IndexOf("True") != -1) 
				{
                    this.txtAttention.Text = pacsbill.Caution.Substring(4) == "0" ? "" : pacsbill.Caution.Substring(4); ;
					this.lblEmergency.Visible = true;
				}
				else
                    this.txtAttention.Text = pacsbill.Caution.Substring(5) == "0" ? "" : pacsbill.Caution.Substring(5); ;
			}
            if (pacsbill.Diagnose1 != null && pacsbill.Diagnose1 != "")//���1
			{
                this.txtDiagnose.Text += "���1��" + pacsbill.Diagnose1 + "\n";
			}

            if (pacsbill.Diagnose2 != null && pacsbill.Diagnose2 != "")//���2
			{
                this.txtDiagnose.Text += "���2��" + pacsbill.Diagnose2 + "\n";
			}

            if (pacsbill.Diagnose3 != null && pacsbill.Diagnose3 != "")//���3
			{
                this.txtDiagnose.Text += "���3��" + pacsbill.Diagnose3;
			}
			this.lblDate.Text = pacsbill.ApplyDate;//��������
			this.txtItems.Text = pacsbill.CheckOrder;//�����Ŀ
			this.txtHistory.Text = pacsbill.IllHistory == "0" ? "" : pacsbill.IllHistory;//��ʷ
			this.itemCode = pacsbill.ItemCode;//��Ŀ����
			this.lblPacsBillID.Text = pacsbill.ComboNO;//���뵥��
			this.lblApplyName.Text = pacsbill.BillName;//���뵥����
			this.lblDoc.Text = pacsbill.Doctor.Name;//ҽ������


			//this.dtSample.Value = FS.FrameWork.Function.NConvert.ToDateTime(pacsbill.SampleDate);
			//this.dtJJ.Value = FS.FrameWork.Function.NConvert.ToDateTime(pacsbill.YJDate);
			
            //this.chkJJ.Checked = pacsbill.JJ;
		}
		
		/// <summary>
		/// ����
		/// </summary>
		/// <returns></returns>
		public void Save()
		{

			int i = 0;
			if(this.pacsbill!= null&&this.pacsbill.IsValid )
			{
				MessageBox.Show("��������Ϣ�Ѿ����ϣ������ٱ���","��ʾ");
				return;
			}
			try
			{
				if(this.FindForm().Tag != null && this.FindForm().Tag.ToString() == "1")
				{
					this.pacsbill.LisResult = this.txtResult.Text.Trim();
					this.pacsbill.Oper.ID = this.PacsBillManager.Operator.ID;
				}
				else
				{
					this.pacsbill = this.GetPacsBillInfo();
				}
				if(this.CheckPacsBill(this.pacsbill) == -1)
				{
					return;
				}
				i = this.PacsBillManager.SetPacsBill(pacsbill);
			}
			catch
			{
				i = -1;
			}
			if(i == -1)
				MessageBox.Show("��鵥����ʧ�ܣ�","��ʾ");
			else
				MessageBox.Show("��鵥����ɹ���","��ʾ");
		}
		/// <summary>
		/// ������뵥
		/// </summary>
		/// <param name="bill"></param>
		/// <returns></returns>
		private int CheckPacsBill(FS.HISFC.Models.Order.PacsBill bill)
		{
			if(bill == null)
				return -1;
            //if(bill.MachineType == "")
            //{
            //    MessageBox.Show("��ѡ���豸����");
            //    this.cmbMachine.Focus();
            //    return -1;
            //}
            //if (bill.PacsItem == "")
            //{
            //    MessageBox.Show("��ѡ��ҽ����Ŀ��");
            //    return -1;
            //}

            //if (!string.IsNullOrEmpty(cmbPacsItem0.Tag.ToString()) && string.IsNullOrEmpty(cmbPacsCheckType0.Text))
            //{
            //    MessageBox.Show("��ѡ���鷽����");
            //    return -1;

            //}
            //if (!string.IsNullOrEmpty(cmbPacsItem1.Tag.ToString()) && string.IsNullOrEmpty(cmbPacsCheckType1.Text))
            //{
            //    MessageBox.Show("��ѡ���鷽����");
            //    return -1;

            //}
            //if (!string.IsNullOrEmpty(cmbPacsItem2.Tag.ToString()) && string.IsNullOrEmpty(cmbPacsCheckType2.Text))
            //{
            //    MessageBox.Show("��ѡ���鷽����");
            //    return -1;

            //}


			return 1;
		}
        
        /// <summary>
		/// ����ǰ��ü�鵥ʵ����Ϣ
		/// </summary>
		private FS.HISFC.Models.Order.PacsBill GetPacsBillInfo() 
		{
			FS.HISFC.Models.Order.PacsBill p = new FS.HISFC.Models.Order.PacsBill();
			FS.HISFC.Models.Base.Employee person = this.PacsBillManager.Operator as FS.HISFC.Models.Base.Employee;
			string billName = "";
			p.PatientNO = this.myReg.PID.CardNO;
			p.PatientType = FS.HISFC.Models.Order.PatientType.OutPatient;//�������
			if(this.pacsbill==null)
			{
				p.Doctor.ID = person.ID;//����ҽʦ����
				p.Doctor.Name = person.Name;//����ҽʦ����
			}
			else
			{
                p.Doctor.ID = this.pacsbill.Doctor.ID;
                p.Doctor.Name = this.pacsbill.Doctor.Name;
			}
			p.ComboNO = this.lblPacsBillID.Text.Trim();//���뵥��
			p.ClinicCode = this.myReg.ID;//���߿���
			p.Dept.ID = person.Dept.ID;//�������
			p.Dept.Name = person.Dept.Name;//��������
			billName =  this.lblApplyName.Text;//���뵥����
	
			p.ItemCode = this.itemCode;//��Ŀ����
			if(billName == "")
				p.BillName = "������뵥";
			else
				p.BillName = billName;
			p.TotCost = this.tot_cost;//��Ŀ�ܷ���
           	 
			if(this.al !=null)
			{
				if(this.al.Count==1)
				{
					p.Diagnose1 = (al[0] as FS.HISFC.Models.HealthRecord.Diagnose).DiagInfo.ICD10.Name;
				}
				else if(al.Count==2)
				{
                    p.Diagnose1 = (al[0] as FS.HISFC.Models.HealthRecord.Diagnose).DiagInfo.ICD10.Name;
                    p.Diagnose2 = (al[1] as FS.HISFC.Models.HealthRecord.Diagnose).DiagInfo.ICD10.Name;
				}
				else if(al.Count==3)
				{
                    p.Diagnose1 = (al[0] as FS.HISFC.Models.HealthRecord.Diagnose).DiagInfo.ICD10.Name;//���1
                    p.Diagnose2 = (al[1] as FS.HISFC.Models.HealthRecord.Diagnose).DiagInfo.ICD10.Name;//���2
                    p.Diagnose3 = (al[2] as FS.HISFC.Models.HealthRecord.Diagnose).DiagInfo.ICD10.Name;//���3
				}
			}
		    p.MachineType = this.cmbMachine.Text;//�豸����
          
			p.Memo = this.txtMemo.Text.Trim();//��ע
			if(alitems != null)//�Ӽ���־
				p.Caution = (alitems[0] as FS.HISFC.Models.Order.OutPatient.Order).IsEmergency.ToString()+this.txtAttention.Text.Trim();
			p.ExeDept = (alitems[0] as FS.HISFC.Models.Order.OutPatient.Order).ExeDept.ID;
			p.LisResult = this.txtResult.Text.Trim();//�����
			p.IllHistory = this.txtHistory.Text.Trim();//��ʷ������
			p.CheckOrder = this.txtItems.Text.Trim();//����
			p.Oper.ID = this.person.Operator.ID;//����Ա
			if(p.ApplyDate == null || p.ApplyDate.Length <= 0)
			{
				p.ApplyDate = this.cnstManager.GetSysDateTime();//��������
			}
			else
			{
				p.ApplyDate = this.pacsbill.ApplyDate;
			}
		
            #region һ�����뵥���������Ŀ����Ŀ������|������ʽ����pacsϵͳ�ӿ�
			string lPacsItem0 ="" ;
			if (this.cmbPacsItem0.Text != "" && this.cmbPacsItem0.Text !=null )
			{
				lPacsItem0 = this.cmbPacsItem0.Text  ;
			}
			string lPacsItem1="" ;
			if (this.cmbPacsItem1.Text != "" && this.cmbPacsItem1.Text !=null)
			{
				lPacsItem1="|" +this.cmbPacsItem1.Text;
			}
			string lPacsItem2="" ;
			if (this.cmbPacsItem2.Text != "" && this.cmbPacsItem2.Text !=null)
			{
				lPacsItem2="|" +this.cmbPacsItem2.Text ;
			}
			string lPacsItem3="" ;
			if (this.cmbPacsItem3.Text != "" && this.cmbPacsItem3.Text !=null)
			{
				lPacsItem3="|" +this.cmbPacsItem3.Text ;
			}
			string lPacsItem4="" ;
			if (this.cmbPacsItem4.Text != "" && this.cmbPacsItem3.Text !=null )
			{
				lPacsItem4="|" +this.cmbPacsItem4.Text ;
			}
			string lcmbPacsCheckType0="" ;
			if (this.cmbPacsCheckType0.Text != null && this.cmbPacsCheckType0.Text != ""  )
			{
				lcmbPacsCheckType0="|" +this.cmbPacsCheckType0.Text ;
			}
			string lcmbPacsCheckType1="" ;
			if (this.cmbPacsCheckType1.Text != null && this.cmbPacsCheckType1.Text != ""  )
			{
				lcmbPacsCheckType1="|" +this.cmbPacsCheckType1.Text ;
			}
			string lcmbPacsCheckType2="" ;
			if (this.cmbPacsCheckType2.Text != null && this.cmbPacsCheckType2.Text != ""   )
			{
				lcmbPacsCheckType2="|" +this.cmbPacsCheckType2.Text  ;
			}
			string lcmbPacsCheckType3="" ;

			if (this.cmbPacsCheckType3.Text != null && this.cmbPacsCheckType3.Text != ""  )
			{
				lcmbPacsCheckType3="|"+ this.cmbPacsCheckType3.Text  ;
			}
			string lcmbPacsCheckType4="" ;
			if (this.cmbPacsCheckType4.Text != null && this.cmbPacsCheckType4.Text != "" )
			{
				lcmbPacsCheckType4= "|" +this.cmbPacsCheckType4.Text ;
			}

			string pacs =  lPacsItem0+lcmbPacsCheckType0 + lPacsItem1+lcmbPacsCheckType1 +lPacsItem2 + lcmbPacsCheckType2 +lPacsItem3 +lcmbPacsCheckType3 +lPacsItem4+lcmbPacsCheckType4;
			p.PacsItem = pacs ;
         #endregion 
            /*
			p.SampleDate = this.dtSample.Value.ToString();
			p.YJDate = this.dtJJ.Value.ToString();
			p.JJ = this.chkJJ.Checked;
            */ 
			return p;
		}
	
        /// <summary>
		/// �˳�
		/// </summary>
		public void Exit()
		{
			DialogResult r;

			FS.HISFC.Models.Order.PacsBill pp = this.PacsBillManager.QueryPacsBill(this.billNo);

			if(pp == null)
			{
				r = MessageBox.Show("�����뵥��Ϣ��δ���棡ȷ��Ҫ�˳���","��ʾ",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2);

				if(r == DialogResult.Cancel)
				{
					return;
				}
			}
			this.FindForm().Close();
		}
		
		/// <summary>
		/// ��ӡ
		/// </summary>
		public void Print()
		{
			FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
			p.ControlBorder = 0;
			System.Drawing.Printing.PageSettings pSet = new System.Drawing.Printing.PageSettings();
			pSet.Landscape = true;
			p.PrintDocument.DefaultPageSettings.Landscape = false;  //���  by lou
			p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            foreach (Control contr in panel1.Controls)
            {
                contr.BackColor = System.Drawing.Color.White;
            }
			p.PrintPage(110,10,this.panel1);
		}
		/// <summary>
		/// ��ӡԤ��
		/// </summary>
		public void PrintPreview()
		{
			FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.ControlBorder = 0;

            System.Drawing.Printing.PageSettings pSet = new System.Drawing.Printing.PageSettings();
            pSet.Landscape = true;

            p.PrintDocument.DefaultPageSettings.Landscape = false;  //���  by lou
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            //foreach (Control contr in panel1.Controls)
            //{
            //    contr.BackColor = System.Drawing.Color.White;
            //}
			p.PrintPreview(110,10,this.panel1);
		}
		/// <summary>
		/// ��ӡʱȥ���߿�
		/// </summary>
		private void RemoveBorder() 
		{
			this.txtHistory.BorderStyle = BorderStyle.None;
			this.txtDiagnose.BorderStyle = BorderStyle.None;
			this.txtItems.BorderStyle = BorderStyle.None;
			this.txtResult.BorderStyle = BorderStyle.None;
		}
		/// <summary>
		/// ��ӡ��ָ��߿�
		/// </summary>
		private void regainBorder() 
		{
			this.txtResult.BorderStyle = BorderStyle.Fixed3D;
			this.txtItems.BorderStyle = BorderStyle.Fixed3D;
			this.txtHistory.BorderStyle = BorderStyle.Fixed3D;
			this.txtDiagnose.BorderStyle = BorderStyle.Fixed3D;
		}
		/// <summary>
		/// ���¼�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int SetUnvalid()
        {
			if(this.pacsbill!=null)
			{
				int iReturn =  this.PacsBillManager.UpdatePacsBillState(this.pacsbill);
				this.pacsbill = this.PacsBillManager.QueryPacsBill(this.pacsbill.ComboNO);
				return iReturn;
			}
			else  
            return 0;
        }
        /// <summary>
        /// ���
        /// </summary>
        private void Clear()
        {

        }
       
        /// <summary>
        /// ˢ��
        /// </summary>
        public void Refresh(bool isRefresh)
        {
          
        }

        #endregion 

        #region �¼�
        private void mnuAdd_Click(object sender, System.EventArgs e)
        {

        }

        /// <summary>
        /// Load����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucPacsApply_Load(object sender, EventArgs e)
        {
            try
            {
                components = new Container();
                components.Add(this.txtHistory);//��ʷ������
                components.Add(this.txt2);
                components.Add(this.txtAttention);//ע������
                components.Add(this.txtDiagnose);//���
                components.Add(this.txtItems);//��Ŀ
                components.Add(this.txtMemo);//��ע
                this.ucUserText2.SetControl(this.components);

                if (this.FindForm().Tag != null && this.FindForm().Tag.ToString() == "1")
                {
                    this.panel3.Visible = true;
                    this.treeView1.Visible = true;
                    this.treeView1.BringToFront();
                    this.ucUserText2.Visible = false;

                    if (this.dtend == DateTime.MinValue)
                    {
                        this.dtend = this.PacsBillManager.GetDateTimeFromSysDateTime();
                        this.dtBegin = this.dtend.AddDays(-1);
                    }


                    this.cmbMachine.Enabled = false;
                    this.txtHistory.ReadOnly = true;
                    this.txtMemo.ReadOnly = true;
                    this.txtAttention.ReadOnly = true;

                }
                else
                {
                    this.treeView1.Visible = false;
                    this.ucUserText2.Visible = true;
                    this.ucUserText2.BringToFront();
                    this.panel3.Visible = false;
                }

                this.dsPacsItems = new DataSet();
                SOC.Local.PacsInterface.GYSY.PacsCompareFunction.GetPacsItemList(ref this.dsPacsItems);

                GetHospLogo();
                this.cmbMachine.Focus();
            }
            catch { }
        }

        /// <summary>
        /// ˫��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_DoubleClick(object sender, System.EventArgs e)
        {
            if (this.treeView1.SelectedNode == null)
            {
                return;
            }

            if (this.treeView1.SelectedNode.Tag == null)
            {
                return;
            }

            FS.HISFC.Models.Order.PacsBill p = this.treeView1.SelectedNode.Tag as FS.HISFC.Models.Order.PacsBill;

            this.PacsBill = p;

            if (this.hsPatient.Contains(p.PatientNO))
            {
                this.reg = this.hsPatient[p.PatientNO] as FS.HISFC.Models.Registration.Register;
            }
            else
            {
                //this.reg = this.regManager.QueryPatient(p.PatientNO);
            }
        }

        /// <summary>
        /// ���Żس�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCardNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //if(e.KeyCode == Keys.Enter)
            //{
            //    ArrayList alPacs = this.PacsBillManager.QueryPacsBillByCardNo(this.tbCardNo.Text.Trim(),this.dtBegin,this.dtend);

            //    if(alPacs != null && alPacs.Count > 0)
            //    {
            //        OutPatient.frmPopPacsInfo frm = new pacsInterface.OutPatient.frmPopPacsInfo();

            //        frm.AlPacsBill = alPacs;

            //        frm.ShowDialog();

            //        if(frm.P != null)
            //        {
            //            this.PacsBill = frm.P;

            //            if(this.hsPatient.Contains(this.pacsbill.ClinicCode))
            //            {
            //                this.reg = this.hsPatient[this.pacsbill.ClinicCode] as FS.HISFC.Models.Registration.Register;
            //            }
            //            else
            //            {
            //                this.reg = this.regManager.QueryByClinic(this.pacsbill.ClinicCode);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("û���ҵ��û��ߵ���Ч�����Ϣ�����޸�ʱ�����¼�����");
            //        return;
            //    }
            //}
        }
        /// <summary>
        /// ʱ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        /// <summary>
        /// �ı��豸����ʱ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbMachine_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.cmbPacsCheckType0.Items.Clear();
            this.cmbPacsCheckType1.Items.Clear();
            this.cmbPacsCheckType2.Items.Clear();
            this.cmbPacsCheckType3.Items.Clear();
            this.cmbPacsCheckType4.Items.Clear();

            this.cmbPacsItem0.Items.Clear();
            this.cmbPacsItem1.Items.Clear();
            this.cmbPacsItem2.Items.Clear();
            this.cmbPacsItem3.Items.Clear();
            this.cmbPacsItem4.Items.Clear();

            Hashtable hsItems = new Hashtable();
            ArrayList alPacsItems = new ArrayList();

			foreach(DataRow row in this.dsPacsItems.Tables[0].Rows)
			{
				if(row["MACHINE_TYPE"].ToString() == this.cmbMachine.Text)
				{
					if(!hsItems.Contains(row["ITEM_NAME"].ToString()))
					{
						hsItems.Add(row["ITEM_NAME"].ToString(),null);

						FS.HISFC.Models.Base.Spell obj = new FS.HISFC.Models.Base.Spell();
						
						obj.ID = row["ITEM_CODE"].ToString();
						obj.Name = row["ITEM_NAME"].ToString();
						obj.SpellCode = row["SPELL_CODE"].ToString();

						alPacsItems.Add(obj);
					}
				}
			}
            this.cmbPacsItem0.SelectedIndexChanged -= new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
            this.cmbPacsItem1.SelectedIndexChanged -= new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
            this.cmbPacsItem2.SelectedIndexChanged -= new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
            this.cmbPacsItem3.SelectedIndexChanged -= new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
            this.cmbPacsItem4.SelectedIndexChanged -= new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);

            this.cmbPacsItem0.AddItems(alPacsItems);
            this.cmbPacsItem1.AddItems(alPacsItems);
            this.cmbPacsItem2.AddItems(alPacsItems);
            this.cmbPacsItem3.AddItems(alPacsItems);
            this.cmbPacsItem4.AddItems(alPacsItems);

            this.cmbPacsItem0.SelectedIndexChanged += new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
            this.cmbPacsItem1.SelectedIndexChanged += new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
            this.cmbPacsItem2.SelectedIndexChanged += new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
            this.cmbPacsItem3.SelectedIndexChanged += new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
            this.cmbPacsItem4.SelectedIndexChanged += new System.EventHandler(this.cmbPacsItem0_SelectedIndexChanged);
        }

        /// <summary>
        /// �л���Ŀʱ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPacsItem0_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (sender == this.cmbPacsItem0)
            {
                this.cmbPacsCheckType0.alItems = new ArrayList();
            }
            if (sender == this.cmbPacsItem1)
            {
                this.cmbPacsCheckType1.alItems = new ArrayList();
            }
            if (sender == this.cmbPacsItem2)
            {
                this.cmbPacsCheckType2.alItems = new ArrayList();
            }
            if (sender == this.cmbPacsItem3)
            {
                this.cmbPacsCheckType3.alItems = new ArrayList();
            }
            if (sender == this.cmbPacsItem4)
            {
                this.cmbPacsCheckType4.Items.Clear();
            }

            ArrayList alPacsCheckType = new ArrayList();
            foreach (DataRow row in this.dsPacsItems.Tables[0].Rows)
            {
                if (row["MACHINE_TYPE"].ToString() == this.cmbMachine.Text && (sender as FS.FrameWork.WinForms.Controls.NeuComboBox).Text == row["ITEM_NAME"].ToString())
                {
                    FS.HISFC.Models.Base.Spell obj = new FS.HISFC.Models.Base.Spell();
                    obj.Name = row["CHECK_BODY"].ToString();

                    alPacsCheckType.Add(obj);
                }
            }

            if (sender == this.cmbPacsItem0)
            {
                this.cmbPacsCheckType0.AddItems(alPacsCheckType);
            }
            if (sender == this.cmbPacsItem1)
            {
                this.cmbPacsCheckType1.AddItems(alPacsCheckType);
            }
            if (sender == this.cmbPacsItem2)
            {
                this.cmbPacsCheckType2.AddItems(alPacsCheckType);
            }
            if (sender == this.cmbPacsItem3)
            {
                this.cmbPacsCheckType3.AddItems(alPacsCheckType);
            }
            if (sender == this.cmbPacsItem4)
            {
                this.cmbPacsCheckType4.AddItems(alPacsCheckType);
            }
        }

        /// <summary>
        /// ���������뷽��
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private Image CreateBarCode(string code)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;
            BarcodeLib.AlignmentPositions Align = BarcodeLib.AlignmentPositions.CENTER;
            b.IncludeLabel = true;
            b.Alignment = Align;
            return b.Encode(type, code, System.Drawing.Color.Black, System.Drawing.Color.White, this.npbBarCode.Size.Width, this.npbBarCode.Height);
        }

        #endregion





       


	}
}
