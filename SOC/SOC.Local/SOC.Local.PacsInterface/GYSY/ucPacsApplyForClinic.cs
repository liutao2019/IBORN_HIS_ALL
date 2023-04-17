using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace SOC.Local.PacsInterface.GYSY
{
	/// <summary>
	/// ucPacsApply 的摘要说明。
	/// </summary>
	partial class ucPacsApplyForClinic : System.Windows.Forms.UserControl
    {

        #region 变量

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

        private ArrayList alitems;//开立项目

        private ArrayList al;//诊断

        string itemCode;//项目编码

        decimal tot_cost = 0m;//总价钱

        DataSet dsPacsItems = new DataSet();

        Hashtable hsPatient = new Hashtable();

        DateTime dtBegin;

        DateTime dtend;

        /// <summary>
        /// 项目组合号
        /// </summary>
        private string billNo;


        public delegate void PacsApplyHandlerForClinic(string CheckPart, string memo);

        /// <summary>
		/// 患者
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
                this.lblAge.Text = SOC.Local.PacsInterface.GYSY.PacsCompareFunction.GetAge(myReg.Birthday);								//年龄
				this.lblName.Text =this.myReg.Name;	//姓名
				if(this.pacsbill == null || this.pacsbill.Dept.Name == null || this.pacsbill.Dept.Name.Length <= 0)
				{
					//this.lblNurseStation.Text = (this.cnstManager.Operator as FS.HISFC.Models.RADT.Person).Dept.Name;	//病区
				}
				else
				{
					this.lblDept.Text = this.pacsbill.Dept.Name;
				}
				this.lblAddressPhone.Text = this.myReg.AddressHome + "/" + this.myReg.PhoneHome;  //住址/电话
				this.lblPatientNo.Text ="*" + this.myReg.Card.ID + "*";						//住院号
                this.npbBarCode.Image = this.CreateBarCode(myReg.PID.CardNO);
				this.label7.Text=this.myReg.PID.CardNO; //门诊号 20111201 by lou
				this.lblPaykind.Text = this.myReg.Pact.Name;						//费用类别
				this.lblMedicalCardNo.Text = this.myReg.SSN;        //医疗证号码
				this.lblSex.Text = this.myReg.Sex.Name;						//性别
				#region 诊断
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
							this.txtDiagnose.Text += "诊断"+j.ToString()+"："+diag.DiagInfo.ICD10.Name+"\n";
						}
					}
				}
				catch
				{}
				#endregion
			}
        }

        /// <summary>
        /// 项目信息
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

                #region 设备类型
                ArrayList alMachineType = this.cnstManager.GetAllList("MACHINETYPE");
                this.cmbMachine.SelectedIndexChanged -= new EventHandler(cmbMachine_SelectedIndexChanged);
                this.cmbMachine.AddItems(alMachineType);
                this.cmbMachine.SelectedIndexChanged += new EventHandler(cmbMachine_SelectedIndexChanged);
                #endregion

                FS.HISFC.Models.Fee.Item.Undrug myItem = null;//费用实体

                FS.HISFC.Models.Order.Order order = value[0] as FS.HISFC.Models.Order.Order;

                this.pacsbill = PacsBillManager.QueryPacsBill(order.Combo.ID.ToString());//查询

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

                if (this.pacsbill == null) //新的
                {

                    this.txtHistory.Text = "";//病史及特征
                    this.billNo = order.Combo.ID;
                    this.lblPacsBillID.Text += order.Combo.ID.ToString();//申请单号
                    this.txtMemo.Text = "";//备注
                    this.txtAttention.Text = "";//注意事项
                    if ((value[0] as FS.HISFC.Models.Order.Order).IsEmergency)
                        this.lblEmergency.Visible = true;//加急

                    this.lblDate.Text += cnstManager.GetSysDate();	//申请日期
                  
                    this.lblDoc.Text = this.person.Operator.ToString();//申请医师

                    #region 项目处理
                    for (int i = value.Count - 1; i >= 0; i--)
                    {
                        myItem = (value[i] as FS.HISFC.Models.Order.Order).Item as FS.HISFC.Models.Fee.Item.Undrug;
                        if (myItem == null)
                        {
                            MessageBox.Show("医嘱实体转成非药品失败!", "SORRY");
                            return;
                        }

                        //复合项目
                        if (myItem.UnitFlag == "1")
                        {
                           //得到价格
                            decimal price = ztManager.GetUndrugCombPrice(myItem.ID);
                            this.txtItems.Text += myItem.Name + "\n";
                        }
                        else
                        {
                            this.txtItems.Text += myItem.Name + "\n";
                        }
                    }

                    //项目名称
                    this.lblApplyName.Text = myItem.SpecialFlag4;
                    //注意事项
                    this.txtAttention.Text = myItem.SpecialFlag3 == "0" ? "" : myItem.SpecialFlag3;
                    //病史及检查
                    this.txtHistory.Text = myItem.SpecialFlag1 == "0" ? "" : myItem.SpecialFlag1;
                    //检查要求
                    this.txtItems.Text += myItem.SpecialFlag2;	 
                    #endregion
                 
                }
                else
                {
                    this.billNo = pacsbill.ComboNO;
                    this.tot_cost = pacsbill.TotCost;//总费用
                    foreach (FS.FrameWork.Models.NeuObject obj in this.cmbMachine.alItems)
                    {
                        if (obj.Name == pacsbill.MachineType)
                            this.cmbMachine.Tag = obj.ID;//设备类型

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

                    this.txtMemo.Text = pacsbill.Memo;//备注
                    if (pacsbill.Caution != "")//注意事项
                    {
                        if (pacsbill.Caution.IndexOf("True") != -1)
                        {
                            this.txtAttention.Text = pacsbill.Caution.Substring(4) == "0" ? "" : pacsbill.Caution.Substring(4);
                        }
                        else
                            this.txtAttention.Text = pacsbill.Caution.Substring(5) == "0" ? "" : pacsbill.Caution.Substring(5);
                    }
                   
                    //保证修改医嘱加急标志能够反应出来
                    if ((value[0] as FS.HISFC.Models.Order.Order).IsEmergency)
                        this.lblEmergency.Visible = true;//加急
                   
                    this.txtDiagnose.Text = "";
                    if (pacsbill.Diagnose1 != null && pacsbill.Diagnose1 != "")//诊断1
                    {
                        this.txtDiagnose.Text += "诊断1：" + pacsbill.Diagnose1 + "\n"; ;
                    }

                    if (pacsbill.Diagnose2 != null && pacsbill.Diagnose2 != "")//诊断2
                    {
                        this.txtDiagnose.Text += "诊断2：" + pacsbill.Diagnose2 + "\n"; ;
                    }

                    if (pacsbill.Diagnose3 != null && pacsbill.Diagnose3 != "")//诊断3
                    {
                        this.txtDiagnose.Text += "诊断3：" + pacsbill.Diagnose3;
                    }

                    this.lblDate.Text = pacsbill.ApplyDate;//申请日期
                    this.txtItems.Text = pacsbill.CheckOrder;//检查项目
                    this.txtHistory.Text = pacsbill.IllHistory == "0" ? "" : pacsbill.IllHistory;//病史
                    this.itemCode = pacsbill.ItemCode;//项目编码
                    this.lblPacsBillID.Text = pacsbill.ComboNO;//申请单号
                    this.lblApplyName.Text = pacsbill.BillName;//申请单名称
                    this.lblDoc.Text = pacsbill.Doctor.Name;//医生代码

                    //this.dtSample.Value = FS.FrameWork.Function.NConvert.ToDateTime(pacsbill.SampleDate);
                    //this.dtJJ.Value = FS.FrameWork.Function.NConvert.ToDateTime(pacsbill.YJDate);
                    //this.chkJJ.Checked = pacsbill.JJ;
                }

            }
        }
        /// <summary>
        /// 当前申请单
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
        /// 开始日期
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
        /// 截止日期
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

        #region 函数

        /// <summary>
        /// 获取医院名称和医院LOGO
        /// </summary>
        private void GetHospLogo()
        {
            string erro = "出错";
            string imgpath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + SOC.Local.PacsInterface.GYSY.PacsCompareFunction.GetHospitalLogo("Xml\\HospitalLogoInfo.xml", "Hospital", "Logo", erro);
            picbLogo.Image = Image.FromFile(imgpath);
        }

        /// <summary>
		/// 显示申请单
		/// </summary>
		private void SetPacsBill()
		{
			this.txtDiagnose.Text = "";
			this.billNo = pacsbill.ComboNO;
			this.tot_cost = pacsbill.TotCost;//总费用
			
			this.cmbMachine.Text = pacsbill.MachineType;//设备类型
			this.txtResult.Text = pacsbill.LisResult;//检查结果
			this.txtMemo.Text = pacsbill.Memo;//备注
			if(pacsbill.Caution != "")//注意事项
			{
				if(pacsbill.Caution.IndexOf("True") != -1) 
				{
                    this.txtAttention.Text = pacsbill.Caution.Substring(4) == "0" ? "" : pacsbill.Caution.Substring(4); ;
					this.lblEmergency.Visible = true;
				}
				else
                    this.txtAttention.Text = pacsbill.Caution.Substring(5) == "0" ? "" : pacsbill.Caution.Substring(5); ;
			}
            if (pacsbill.Diagnose1 != null && pacsbill.Diagnose1 != "")//诊断1
			{
                this.txtDiagnose.Text += "诊断1：" + pacsbill.Diagnose1 + "\n";
			}

            if (pacsbill.Diagnose2 != null && pacsbill.Diagnose2 != "")//诊断2
			{
                this.txtDiagnose.Text += "诊断2：" + pacsbill.Diagnose2 + "\n";
			}

            if (pacsbill.Diagnose3 != null && pacsbill.Diagnose3 != "")//诊断3
			{
                this.txtDiagnose.Text += "诊断3：" + pacsbill.Diagnose3;
			}
			this.lblDate.Text = pacsbill.ApplyDate;//申请日期
			this.txtItems.Text = pacsbill.CheckOrder;//检查项目
			this.txtHistory.Text = pacsbill.IllHistory == "0" ? "" : pacsbill.IllHistory;//病史
			this.itemCode = pacsbill.ItemCode;//项目编码
			this.lblPacsBillID.Text = pacsbill.ComboNO;//申请单号
			this.lblApplyName.Text = pacsbill.BillName;//申请单名称
			this.lblDoc.Text = pacsbill.Doctor.Name;//医生代码


			//this.dtSample.Value = FS.FrameWork.Function.NConvert.ToDateTime(pacsbill.SampleDate);
			//this.dtJJ.Value = FS.FrameWork.Function.NConvert.ToDateTime(pacsbill.YJDate);
			
            //this.chkJJ.Checked = pacsbill.JJ;
		}
		
		/// <summary>
		/// 保存
		/// </summary>
		/// <returns></returns>
		public void Save()
		{

			int i = 0;
			if(this.pacsbill!= null&&this.pacsbill.IsValid )
			{
				MessageBox.Show("该申请信息已经作废，不能再保存","提示");
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
				MessageBox.Show("检查单保存失败！","提示");
			else
				MessageBox.Show("检查单保存成功！","提示");
		}
		/// <summary>
		/// 检查申请单
		/// </summary>
		/// <param name="bill"></param>
		/// <returns></returns>
		private int CheckPacsBill(FS.HISFC.Models.Order.PacsBill bill)
		{
			if(bill == null)
				return -1;
            //if(bill.MachineType == "")
            //{
            //    MessageBox.Show("请选择设备类型");
            //    this.cmbMachine.Focus();
            //    return -1;
            //}
            //if (bill.PacsItem == "")
            //{
            //    MessageBox.Show("请选择医技项目！");
            //    return -1;
            //}

            //if (!string.IsNullOrEmpty(cmbPacsItem0.Tag.ToString()) && string.IsNullOrEmpty(cmbPacsCheckType0.Text))
            //{
            //    MessageBox.Show("请选择检查方法！");
            //    return -1;

            //}
            //if (!string.IsNullOrEmpty(cmbPacsItem1.Tag.ToString()) && string.IsNullOrEmpty(cmbPacsCheckType1.Text))
            //{
            //    MessageBox.Show("请选择检查方法！");
            //    return -1;

            //}
            //if (!string.IsNullOrEmpty(cmbPacsItem2.Tag.ToString()) && string.IsNullOrEmpty(cmbPacsCheckType2.Text))
            //{
            //    MessageBox.Show("请选择检查方法！");
            //    return -1;

            //}


			return 1;
		}
        
        /// <summary>
		/// 保存前获得检查单实体信息
		/// </summary>
		private FS.HISFC.Models.Order.PacsBill GetPacsBillInfo() 
		{
			FS.HISFC.Models.Order.PacsBill p = new FS.HISFC.Models.Order.PacsBill();
			FS.HISFC.Models.Base.Employee person = this.PacsBillManager.Operator as FS.HISFC.Models.Base.Employee;
			string billName = "";
			p.PatientNO = this.myReg.PID.CardNO;
			p.PatientType = FS.HISFC.Models.Order.PatientType.OutPatient;//患者类别
			if(this.pacsbill==null)
			{
				p.Doctor.ID = person.ID;//申请医师代码
				p.Doctor.Name = person.Name;//申请医师姓名
			}
			else
			{
                p.Doctor.ID = this.pacsbill.Doctor.ID;
                p.Doctor.Name = this.pacsbill.Doctor.Name;
			}
			p.ComboNO = this.lblPacsBillID.Text.Trim();//申请单号
			p.ClinicCode = this.myReg.ID;//患者卡号
			p.Dept.ID = person.Dept.ID;//申请科室
			p.Dept.Name = person.Dept.Name;//科室名称
			billName =  this.lblApplyName.Text;//申请单名称
	
			p.ItemCode = this.itemCode;//项目编码
			if(billName == "")
				p.BillName = "检查申请单";
			else
				p.BillName = billName;
			p.TotCost = this.tot_cost;//项目总费用
           	 
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
                    p.Diagnose1 = (al[0] as FS.HISFC.Models.HealthRecord.Diagnose).DiagInfo.ICD10.Name;//诊断1
                    p.Diagnose2 = (al[1] as FS.HISFC.Models.HealthRecord.Diagnose).DiagInfo.ICD10.Name;//诊断2
                    p.Diagnose3 = (al[2] as FS.HISFC.Models.HealthRecord.Diagnose).DiagInfo.ICD10.Name;//诊断3
				}
			}
		    p.MachineType = this.cmbMachine.Text;//设备类型
          
			p.Memo = this.txtMemo.Text.Trim();//备注
			if(alitems != null)//加急标志
				p.Caution = (alitems[0] as FS.HISFC.Models.Order.OutPatient.Order).IsEmergency.ToString()+this.txtAttention.Text.Trim();
			p.ExeDept = (alitems[0] as FS.HISFC.Models.Order.OutPatient.Order).ExeDept.ID;
			p.LisResult = this.txtResult.Text.Trim();//检查结果
			p.IllHistory = this.txtHistory.Text.Trim();//病史及特征
			p.CheckOrder = this.txtItems.Text.Trim();//查体
			p.Oper.ID = this.person.Operator.ID;//操作员
			if(p.ApplyDate == null || p.ApplyDate.Length <= 0)
			{
				p.ApplyDate = this.cnstManager.GetSysDateTime();//申请日期
			}
			else
			{
				p.ApplyDate = this.pacsbill.ApplyDate;
			}
		
            #region 一张申请单开出多个项目，项目名称以|隔开形式传到pacs系统接口
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
		/// 退出
		/// </summary>
		public void Exit()
		{
			DialogResult r;

			FS.HISFC.Models.Order.PacsBill pp = this.PacsBillManager.QueryPacsBill(this.billNo);

			if(pp == null)
			{
				r = MessageBox.Show("该申请单信息尚未保存！确定要退出吗？","提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2);

				if(r == DialogResult.Cancel)
				{
					return;
				}
			}
			this.FindForm().Close();
		}
		
		/// <summary>
		/// 打印
		/// </summary>
		public void Print()
		{
			FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
			p.ControlBorder = 0;
			System.Drawing.Printing.PageSettings pSet = new System.Drawing.Printing.PageSettings();
			pSet.Landscape = true;
			p.PrintDocument.DefaultPageSettings.Landscape = false;  //横打  by lou
			p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            foreach (Control contr in panel1.Controls)
            {
                contr.BackColor = System.Drawing.Color.White;
            }
			p.PrintPage(110,10,this.panel1);
		}
		/// <summary>
		/// 打印预览
		/// </summary>
		public void PrintPreview()
		{
			FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.ControlBorder = 0;

            System.Drawing.Printing.PageSettings pSet = new System.Drawing.Printing.PageSettings();
            pSet.Landscape = true;

            p.PrintDocument.DefaultPageSettings.Landscape = false;  //横打  by lou
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            //foreach (Control contr in panel1.Controls)
            //{
            //    contr.BackColor = System.Drawing.Color.White;
            //}
			p.PrintPreview(110,10,this.panel1);
		}
		/// <summary>
		/// 打印时去掉边框
		/// </summary>
		private void RemoveBorder() 
		{
			this.txtHistory.BorderStyle = BorderStyle.None;
			this.txtDiagnose.BorderStyle = BorderStyle.None;
			this.txtItems.BorderStyle = BorderStyle.None;
			this.txtResult.BorderStyle = BorderStyle.None;
		}
		/// <summary>
		/// 打印后恢复边框
		/// </summary>
		private void regainBorder() 
		{
			this.txtResult.BorderStyle = BorderStyle.Fixed3D;
			this.txtItems.BorderStyle = BorderStyle.Fixed3D;
			this.txtHistory.BorderStyle = BorderStyle.Fixed3D;
			this.txtDiagnose.BorderStyle = BorderStyle.Fixed3D;
		}
		/// <summary>
		/// 空事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

        /// <summary>
        /// 作废
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
        /// 清空
        /// </summary>
        private void Clear()
        {

        }
       
        /// <summary>
        /// 刷新
        /// </summary>
        public void Refresh(bool isRefresh)
        {
          
        }

        #endregion 

        #region 事件
        private void mnuAdd_Click(object sender, System.EventArgs e)
        {

        }

        /// <summary>
        /// Load函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucPacsApply_Load(object sender, EventArgs e)
        {
            try
            {
                components = new Container();
                components.Add(this.txtHistory);//病史及特征
                components.Add(this.txt2);
                components.Add(this.txtAttention);//注意事项
                components.Add(this.txtDiagnose);//诊断
                components.Add(this.txtItems);//项目
                components.Add(this.txtMemo);//备注
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
        /// 双击
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
        /// 卡号回车
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
            //        MessageBox.Show("没有找到该患者的有效检查信息，请修改时间重新检索！");
            //        return;
            //    }
            //}
        }
        /// <summary>
        /// 时间间隔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        /// <summary>
        /// 改变设备类型时触发
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
        /// 切换项目时触发
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
        /// 生成条形码方法
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
