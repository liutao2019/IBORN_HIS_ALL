using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Components.Order.Classes;
using FS.HISFC.Components.Order.Controls;

namespace FS.HISFC.Components.Order.OutPatient.Forms
{

    //{FBE92A1C-323E-405e-9F46-ACCA9700DF2A}

    public partial class frmAppointment : Form
    {
        public frmAppointment()
        {
            InitializeComponent();
            InitControls();
            this.tbSex.Visible = false;
            this.tbAge.Visible = false;
            //this.textBox1.Visible = false;

        }

        /// <summary>
        /// 挂号级别列表
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbRegLevel = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();



        private void lbRegLevel_SelectItem(object sender, EventArgs e)
        {
            this.selectRegLevel();
            //this.visible(false);

        }

        /// <summary>
        /// 选择挂号级别
        /// </summary>
        /// <returns></returns>
        private int selectRegLevel()
        {
            //int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            FS.FrameWork.Models.NeuObject obj;

            //this.fpSpread1.StopCellEditing();

            //if (this.groupBox1.Visible)
            //{
                obj = this.lbRegLevel.GetSelectedItem();
                string id = obj.ID;
                string name = obj.Name;
                if (obj == null) return -1;


                //this.fpSpread1_Sheet1.SetValue(row, (int)cols.RegLevelName, obj.Name, false);
                //this.fpSpread1_Sheet1.SetValue(row, (int)cols.RegLevelCode, obj.ID, false);
                //this.fpSpread1.Focus();
                //this.visible(false);
            //}
            //else
            //{
            //    //string doctId = this.fpSpread1_Sheet1.GetText(row, (int)cols.RegLevelCode);

            //    //if (doctId == null || doctId == "")
            //    //    this.fpSpread1_Sheet1.SetValue(row, (int)cols.RegLevelName, "", false);
            //}

            return 0;
        }


        ArrayList idCardType = null;

        ArrayList deseaseclass2List = null;

        ArrayList bloodchilditem = null;

        ArrayList surgerychilditem = null;

        ArrayList cjitem = null;

        ArrayList FKMZPDANDFZJ = null;//盆底/腹直肌
        ArrayList FKMZGUPEN = null;//骨盆
        ArrayList FKMZSIMI = null;//私密
        ArrayList FKMZQT = null;//其他

        ArrayList therapist = null;//其他

        string crmid = "";

        /// <summary>
        /// 当前会员卡信息
        /// </summary>
        private FS.HISFC.Models.Account.AccountCard accountCardInfo = null;

        //诊间患者列表
        List<FS.HISFC.Components.Order.Classes.PatientInfoByJZ> patientList = new List<PatientInfoByJZ>();

        FS.HISFC.Components.Order.Classes.PatientInfoByJZ patient = new PatientInfoByJZ();

        /// <summary>
        /// 当前登陆人员
        /// </summary>
        private FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

        /// <summary>
        /// 账户管理类
        /// </summary>
        FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 常数管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();

        

        /// <summary>
        /// 诊室列表
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbRoom = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();


        private static FS.HISFC.BizProcess.Integrate.Manager integrateManager = new FS.HISFC.BizProcess.Integrate.Manager();

        public static FS.HISFC.BizProcess.Integrate.Manager IntegrateManager
        {
            get
            {
                return integrateManager;

            }
        }


        /// <summary>
        /// 患者基本信息
        /// </summary>
        protected FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        private static System.Collections.ArrayList alDept;     //科室列表
        private static System.Collections.ArrayList alDoct;     //医生列表

        /// <summary>
        /// 患者基本信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get { return this.patientInfo; }
            set
            {
                this.patientInfo = value;
                this.SetPatientInfo();
            }
        }



        /// <summary>
        /// 设置患者信息
        /// </summary>
        private void SetPatientInfo()
        {
            if (this.PatientInfo == null || string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
            {
                this.tbMedicalNO.Text = string.Empty;
                this.tbName.Text = string.Empty;
                this.tbCardType.Text = string.Empty;
                this.tbIDNO.Text = string.Empty;
                this.tbSex1.Text = string.Empty;
                this.tbAge1.Text = string.Empty;
                //this.tbLevel.Tag = string.Empty;
                this.tbPhone.Text = string.Empty;

                //constantMgr.Operator.Name;
                //constantMgr.Hospital.Name;
                //this.tbPact.Tag = string.Empty;

                return;
            }


            System.Collections.Generic.List<FS.HISFC.Models.Account.AccountCard> cardList = accountMgr.GetMarkList(this.PatientInfo.PID.CardNO, "ALL", "1");
            if (cardList == null || cardList.Count < 1)
            {
                MessageBox.Show("病历号不存在！");
                return;
            }

            this.accountCardInfo = cardList[cardList.Count - 1];

            this.tbMedicalNO.Text = this.accountCardInfo.Patient.PID.CardNO;
            this.tbName.Text = patientInfo.Name;
            this.tbCardType.Text = "身份证";// this.QueryNameByIDFromDictionnary(this.IDTypeList, this.patientInfo.IDCardType.ID);
            this.tbIDNO.Text = this.patientInfo.IDCard;
            this.tbSex1.Text = patientInfo.Sex.ID.ToString() == "F" ? "女" : "男";
            this.tbAge1.Text = this.accountMgr.GetAge(patientInfo.Birthday);
            //this.tbLevel.Text = this.QueryNameByIDFromDictionnary(this.memberLevel, this.accountCardInfo.AccountLevel.ID.ToString());
            this.tbPhone.Text = this.patientInfo.PhoneHome;

            DataSet ds = new DataSet();
            string sql = @"select wm_concat(distinct t2.name) package from exp_package t 
																				left join bd_com_package t1 on t.parentpackageid=t1.package_id
																				left join com_dictionary t2 on t2.type='PACKAGETYPE' and t1.package_kind=t2.code
																				where t.card_no='{0}' and t1.package_kind in('3','4','1','15')
																				order by t.oper_date desc";
            sql = string.Format(sql, this.tbMedicalNO.Text.Trim().ToString());
            patient.ExecQuery(sql, ref ds);

            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                this.txtPackage.Text = dt.Rows[0]["package"].ToString();
            }

        }


        private void InitControls()
        {
            idCardType = constantMgr.GetList("IDCard");

            //{3047D4AA-F8D4-4cb3-93F6-AC42180403AC}
            deseaseclass2List = constantMgr.GetList("DESEASE_CLASS2");
            bloodchilditem = constantMgr.GetList("BLOODCHILDITEM");
            surgerychilditem = constantMgr.GetList("SURGERYCHILDITEM");
            cjitem = constantMgr.GetList("CJLIST");
            //this.fpSpread1.CellClick += fpSpread1_CellClick;
            alDept = constantMgr.GetListAsSql("select '',DEPT_CODE,DEPT_NAME,'','','','','','','','','' from com_department", ""); //IntegrateManager.GetDepartment();
            //List<FS.HISFC.BizLogic.Manager.Constant> a=IntegrateManager.GetDepartment() as List<FS.HISFC.BizLogic.Manager.Constant>;
            alDoct = constantMgr.GetListAsSql("select '',EMPL_CODE,EMPL_NAME,'','','','','','','','','' from com_employee where EMPL_TYPE='D'", ""); // IntegrateManager.QueryEmployeeAll();
            //FS.HISFC.BizLogic.Manager.Constant c=alDept[0] as FS.HISFC.BizLogic.Manager.Constant;
            //MessageBox.Show(c.ID.ToString()+"  "+c.Name.ToString());
            //FS.FrameWork.Models.NeuObject obj=;
            //string id = obj.ID;
            //stirng name = obj.Name;
            //this.neuComboBox1.AddItems(idCardType);
            //initRegLevl();
            FKMZPDANDFZJ = constantMgr.GetList("FKMZPDANDFZJ");
            FKMZGUPEN = constantMgr.GetList("FKMZGUPEN");
            FKMZSIMI = constantMgr.GetList("FKMZSIMI");
            FKMZQT = constantMgr.GetList("FKMZQT");

            therapist = constantMgr.GetListAsSql("select '',CODE,NAME,'','','','','','','','','' from com_dictionary where TYPE='THERAPIST'", "");
        }


        private void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            bool checkedState = (bool)this.fpSpread1_Sheet1.Cells[e.Row, 0].Value;
            this.fpSpread1_Sheet1.Cells[e.Row, 0].Value = !checkedState;

            if (e.Column != 0)
            {
                this.fpSpread1_Sheet1.Cells[e.Row, 0].Value = true;
                foreach (FarPoint.Win.Spread.Row row in this.fpSpread1_Sheet1.Rows)
                {
                    if (row.Index != e.Row)
                    {
                        this.fpSpread1_Sheet1.Cells[row.Index, 0].Value = false;
                    }
                }
            }
        }

        private string QueryNameByIDFromDictionnary(ArrayList al, string ID)
        {
            try
            {
                foreach (FS.FrameWork.Models.NeuObject obj in al)
                {
                    if (obj.ID == ID)
                    {
                        return obj.Name;
                    }
                }
            }
            catch
            { }
            return string.Empty;
        }

        private void SetJZAppointInfo()
        {
            DataSet ds = new DataSet();
            string sql = "select * from met_opb_appointment where doct_name='" + employee.Name.ToString() + "' or create_oper='" + employee.Name.ToString() + "' order by oper_time desc";
            patient.ExecQuery(sql, ref ds);

            DataTable dt = new DataTable() ;
            if (ds != null && ds.Tables.Count > 0) dt = ds.Tables[0];

            this.fpSpread1_Sheet1.RowCount = 0;
            int index = 1;

            if (dt.Rows.Count > 0) 
            {


                for (int i = 0; i < dt.Rows.Count;i++ )
                {
                    this.fpSpread1_Sheet1.AddRows(this.fpSpread1_Sheet1.RowCount, 1);

                    string appointtime = dt.Rows[i]["APPOINT_TIME"].ToString();

                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 1].Text = dt.Rows[i]["CARD_NO"].ToString();
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 2].Text = dt.Rows[i]["NAME"].ToString();
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 3].Text = dt.Rows[i]["HOME_TEL"].ToString();
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 4].Text = Convert.ToDateTime(appointtime).ToString("yyyy-MM-dd"); 
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 5].Text = dt.Rows[i]["DEPT_NAME"].ToString();
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 6].Text = dt.Rows[i]["DOCT_NAME"].ToString();

                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 7].Text = dt.Rows[i]["ITEM_NAME"].ToString();
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 8].Text = dt.Rows[i]["MEMO"].ToString();
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 9].Text = dt.Rows[i]["CANCELREASON"].ToString();
                    string flag = dt.Rows[i]["STATE"].ToString();

                    this.fpSpread1.ActiveSheet.Rows[i].Height = fpSpread1.ActiveSheet.Rows[i].GetPreferredHeight();

                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 2].Tag = flag;// dt.Rows[i]["APPOINTID"].ToString();


                    if (flag == "1")
                    {
                        this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].Text = "待预约";
                    
                    }
                    else if (flag == "2")
                    {
                        this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].Text = "已预约";
                        this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.RowCount - 1].ForeColor = Color.Green;
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].Text = "取消预约";
                        this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.RowCount - 1].ForeColor = Color.Red;
                    }
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].Tag = dt.Rows[i]["APPOINTID"].ToString();
                   
                    index++;
                }

            }

        }

        private void SetAppointInfo(string cardNo)
        {
            DataSet ds = new DataSet();
            string sql = "select * from met_opb_appointment where CARD_NO='"+ cardNo + "' and (doct_name='" + employee.Name.ToString() + "' or create_oper='" + employee.Name.ToString() + "') order by oper_time desc";
            patient.ExecQuery(sql, ref ds);

            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0) dt = ds.Tables[0];

            this.fpSpread1_Sheet1.RowCount = 0;//列表清空
            int index = 1;

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.fpSpread1_Sheet1.AddRows(this.fpSpread1_Sheet1.RowCount, 1);

                    string appointtime = dt.Rows[i]["APPOINT_TIME"].ToString();

                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 1].Text = dt.Rows[i]["CARD_NO"].ToString();
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 2].Text = dt.Rows[i]["NAME"].ToString();
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 3].Text = dt.Rows[i]["HOME_TEL"].ToString();
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 4].Text = Convert.ToDateTime(appointtime).ToString("yyyy-MM-dd");
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 5].Text = dt.Rows[i]["DEPT_NAME"].ToString();
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 6].Text = dt.Rows[i]["DOCT_NAME"].ToString();

                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 7].Text = dt.Rows[i]["ITEM_NAME"].ToString();
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 8].Text = dt.Rows[i]["MEMO"].ToString();
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 9].Text = dt.Rows[i]["CANCELREASON"].ToString();
                    string flag = dt.Rows[i]["STATE"].ToString();

                    this.fpSpread1.ActiveSheet.Rows[i].Height = fpSpread1.ActiveSheet.Rows[i].GetPreferredHeight();

                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 2].Tag = flag;


                    if (flag == "1")
                    {
                        this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].Text = "待预约";

                    }
                    else if (flag == "2")
                    {
                        this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].Text = "已预约";
                        this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.RowCount - 1].ForeColor = Color.Green;
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].Text = "取消预约";
                        this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.RowCount - 1].ForeColor = Color.Red;
                    }
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].Tag = dt.Rows[i]["APPOINTID"].ToString();

                    index++;
                }

            }

        }



        private void button3_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.fpSpread1_Sheet1.ActiveRowIndex;

            if (selectedIndex < 0)
            {
                MessageBox.Show("请选择要取消预约的病人！");
                return;
            }



            string id = this.fpSpread1_Sheet1.Cells[selectedIndex, 0].Tag.ToString();
            string flag = this.fpSpread1_Sheet1.Cells[selectedIndex, 2].Tag.ToString();

            if (flag != "1") 
            {
                MessageBox.Show("不能取消预约，该状态是已预约或取消预约");
                return;
            }

            //HISFC.Components.Operation.WebReference.IbornMobileService web = new HISFC.Components.Operation.WebReference.IbornMobileService();
            //string req = "<req><id>" + id + "</id><isappoint>-1</isappoint></req>";
            //string result = web.CancelByJZAppoint(req);
            string sql = @"update met_opb_appointment set state='0',CANCEL_OPER='"+employee.Name.ToString()+"',CANCEL_TIME=to_date('"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"','yyyy-MM-dd HH24:mi:ss') where appointid='{0}'";
            int result = patient.ExecNoQuery(sql, id);
            if (result >0)
            {
                MessageBox.Show("取消成功！");
                this.fpSpread1_Sheet1.Cells[selectedIndex, 0].Text = "已取消";
                this.fpSpread1_Sheet1.Rows[selectedIndex].ForeColor = Color.Red;
            }
            else
                MessageBox.Show("取消失败！");
        }

        private void frmAppointment_Load(object sender, EventArgs e)
        {
            this.cmb1.AddItems(alDept);
            this.cmb2.AddItems(alDoct);
            this.neuComboBox7.AddItems(deseaseclass2List);
            this.neuComboBox6.AddItems(bloodchilditem);
            this.neuComboBox5.AddItems(surgerychilditem);

            this.neuComboBox4.AddItems(cjitem);
           
            this.cmb1.Text = employee.Dept.Name.ToString();
            this.cmb2.Text = employee.Name.ToString();

            //this.cbFKMZPDANDFZJ.AddItems(FKMZPDANDFZJ);
            //this.cbFKMZGUPEN.AddItems(FKMZGUPEN);
            //this.cbFKMZSIMI.AddItems(FKMZSIMI);
            //this.cbFKMZQT.AddItems(FKMZQT);

            this.cbzls.AddItems(therapist);

            this.cmb1.SelectedIndexChanged +=new EventHandler(cmb1_SelectedIndexChanged);

            if (this.cmb1.Text == "妇科门诊")
            {
                this.panel3.Visible = false;
                this.panel4.Visible = true;
                this.cbzls.Enabled = true;
            }
            else
            {
                this.panel3.Visible = true;
                this.panel4.Visible = false;
                this.cbzls.Enabled = false;
                
            }

            //this.tbMedicalNO.Text = "";
            //this.tbName.Text = "";

            SetJZAppointInfo();
        }

        public void cmb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FS.HISFC.Models.Base.Const con = this.cmb1.SelectedItem as FS.HISFC.Models.Base.Const;

            if (con.Name == "妇科门诊")
            {
                this.panel3.Visible = false;
                this.panel4.Visible = true;
                this.cbzls.Enabled = true;
            }
            else
            {
                this.panel3.Visible = true;
                this.panel4.Visible = false;
                this.cbzls.Text = "";
                this.cbzls.Tag = "";
                this.cbzls.Enabled = false;
            }

        }

        private bool queryPatientList(string condition)
        {
            //this.Clear();
            Form patientForm = new Form();
            ucPatientList patientList = new ucPatientList();
            patientForm.Size = patientList.Size;
            patientForm.Controls.Add(patientList);
            patientList.QueryCondition = condition;
            patientForm.StartPosition = FormStartPosition.Manual;
            patientForm.Location = new Point(PointToScreen(this.txtCardNO.Location).X, PointToScreen(this.txtCardNO.Location).Y + this.txtCardNO.Height * 2);
            patientForm.FormBorderStyle = FormBorderStyle.None;
            patientForm.ShowInTaskbar = false;
            patientList.patientInfo = patientInfoSet;

            if (patientList.patientList != null && patientList.patientList.Count > 0)
            {
                patientForm.ShowDialog();
                return true;
            }
            else
            {
                MessageBox.Show("没有该患者信息！");
                // tbName.Text = patient.Name;
            }

            return false;
        }

        public void patientInfoSet(PatientInfoByJZ patient)
        {
            this.tbMedicalNO.Text = patient.CARD_NO;
            this.tbName.Text = patient.NAME;
            this.tbSex1.Text = patient.Sex;
            this.tbIDNO.Text = patient.IDENNO;
            this.tbPhone.Text = patient.HOME_TEL;
            this.tbAge1.Text = patient.Age.ToString();
            this.tbCardType.Text = "身份证";

            //{588BA275-0B2A-455b-BDA1-A9A6E483693D}
            crmid =patient.CRMID==null?"": patient.CRMID.ToString();

            DataSet ds = new DataSet();
            string sql = @"select wm_concat(distinct t2.name) package from exp_package t 
																				left join bd_com_package t1 on t.parentpackageid=t1.package_id
																				left join com_dictionary t2 on t2.type='PACKAGETYPE' and t1.package_kind=t2.code
																				where t.card_no='{0}' and t1.package_kind in('3','4','1','15')
																				order by t.oper_date desc";
            sql = string.Format(sql, this.tbMedicalNO.Text.Trim().ToString());
            patient.ExecQuery(sql, ref ds);

            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                this.txtPackage.Text = dt.Rows[0]["package"].ToString();
            }

            SetAppointInfo(patient.CARD_NO);//根据卡号筛选数据

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.tbMedicalNO.Text == "" || this.tbName.Text == "")
            {
                MessageBox.Show("先检索患者信息，再进行预约！");
                return;
            }



            FS.HISFC.Models.Base.Const con = this.cmb1.SelectedItem as FS.HISFC.Models.Base.Const;

            string deptId = con.ID.ToString();

            FS.HISFC.Models.Base.Const con1 = this.cmb2.SelectedItem as FS.HISFC.Models.Base.Const;

            string doctId = con1.ID.ToString();

            //预约治疗师
            string treatdoctId = "";
            string treatdoctName = "";
            if (con.Name == "妇科门诊")
            {
                FS.HISFC.Models.Base.Const con2 = this.cbzls.SelectedItem as FS.HISFC.Models.Base.Const;
                if (this.cbzls.Text != "")
                {
                    treatdoctId = con2.ID.ToString();
                    treatdoctName = con2.Name.ToString();
                }
            }

            //备注
            string memo = "";

            if (this.checkBox3.Checked) 
            {
                memo += this.checkBox3.Text + "  ";
            }

            if (this.checkBox4.Checked) 
            {
                memo += "已开单"+ "  ";
            }

            memo += this.richTextBox1.Text;

            //string id="";

            DataSet ds = new DataSet();
            string sq = "select seq_met_opb_appointment.nextval ID from dual";
            patient.ExecQuery(sq, ref ds);

            DataTable dt = ds.Tables[0];

            string id = dt.Rows[0]["ID"].ToString();


            //{BA1088FD-4BAD-4558-AD40-96E03D4FBC48}
            string appointcontext = "";

            if (con.Name == "妇科门诊")
            {
                foreach (Control c in panel4.Controls)
                {
                    if (c is CheckBox && ((CheckBox)c).Checked == true)
                    {
                        appointcontext += ((CheckBox)c).Text.ToString() + " \r\n ";
                    }
                }

                if (this.cbFKMZPDANDFZJ.Text != "")
                {
                    appointcontext += " \r\n " + this.cbFKMZPDANDFZJ.Text;
                }

                if (this.cbFKMZGUPEN.Text != "")
                {
                    appointcontext += " \r\n " + this.cbFKMZGUPEN.Text;
                }

                if (this.cbFKMZSIMI.Text != "")
                {
                    appointcontext += " \r\n " + this.cbFKMZSIMI.Text + " \r\n ";
                }

                if (this.cbFKMZQT.Text != "")
                {
                    appointcontext += " \r\n " + this.cbFKMZQT.Text + " \r\n ";
                }
            }
            else
            {
                foreach (Control c in panel3.Controls)
                {
                    if (c is CheckBox && ((CheckBox)c).Checked == true)
                    {
                        appointcontext += ((CheckBox)c).Text.ToString() + " \r\n ";
                    }
                }

                //{B344B236-C659-4290-9F63-84B8F81D196B}

                if (this.neuComboBox4.Text != "")
                {
                    appointcontext += " \r\n " + "产检-" + this.neuComboBox4.Text + " \r\n ";
                }
            }


            if (this.neuComboBox7.Text != "")
            {
                appointcontext += " \r\n " + "超声检查-" + this.neuComboBox7.Text;
            }

            if (this.neuComboBox6.Text != "")
            {
                appointcontext += " \r\n " + "抽血-" + this.neuComboBox6.Text;
            }

            if (this.neuComboBox5.Text != "")
            {
                appointcontext += " \r\n " + "入院/手术-" + this.neuComboBox5.Text + " \r\n ";
            }
                       

            string cid = crmid;

            string clientmanagerid = "";
            string clientmanager = "";
            string consultmanagerid = "";
            string consultmanager = "";

            GetConsultAndClientPersonInfo(ref clientmanagerid, ref clientmanager, ref consultmanagerid, ref consultmanager, cid);

            //therapist_name  、therapist_code  治疗师

            string sql = @"insert into MET_OPB_APPOINTMENT(appointid,card_no,name,sex,age,idenno,home_tel,dept_code,dept_name,doct_code,doct_name,item_code,item_name,appoint_time,state,memo,create_time,create_oper,cancel_time,cancel_oper,oper_time, oper,clientmanagerid,clientmanager,consultmanagerid,consultmanager,THERAPIST_CODE ,THERAPIST_NAME) 
                                                            values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',to_date('{13}','yyyy-MM-dd'),'{14}','{15}',to_date('{16}','yyyy-MM-dd HH24:mi:ss'),'{17}',to_date('{18}','yyyy-MM-dd'),'{19}',to_date('{20}','yyyy-MM-dd HH24:mi:ss'),'{21}','{22}','{23}','{24}','{25}','{26}','{27}')";
            //sql = string.Format(sql, id, this.cmb1.Text, this.cmb2.Text, this.textBox1.Text, this.tbMedicalNO.Text, this.tbName.Text, this.tbSex1.Text, this.tbIDNO.Text, this.tbPhone.Text, this.tbAge1.Text, this.dateTimePicker1.Value.ToString("yyyy-MM-dd"), this.richTextBox1.Text,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),0);
            sql = string.Format(sql, id, this.tbMedicalNO.Text, this.tbName.Text, this.tbSex1.Text, this.tbAge1.Text, this.tbIDNO.Text, this.tbPhone.Text, deptId, this.cmb1.Text, doctId, this.cmb2.Text, "", appointcontext, this.dateTimePicker1.Value.ToString("yyyy-MM-dd"), "1", memo, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), employee.Name.ToString(), "0001-01-01", "", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), employee.Name.ToString(), clientmanagerid, clientmanager, consultmanagerid, consultmanager, treatdoctId, treatdoctName);
            int result = patient.ExecNoQuery(sql);
            if (result>0)
            {
                MessageBox.Show("预约成功");
                this.richTextBox1.Text = "";
                SetJZAppointInfo();
            }

            else
                MessageBox.Show("预约失败");
        }

        private void txtCardNO_KeyDown(object sender, KeyEventArgs e)
        {
           if (e.KeyCode == Keys.Enter)
            {
                if (this.txtCardNO.Text != "")
                {
                    queryPatientList(this.txtCardNO.Text.Trim());
                }

            }
        }

        public void GetConsultAndClientPersonInfo(ref string clientmanagerid, ref string clientmanager, ref string consultmanagerid, ref string consultmanager,string crmid)
        {
            if (crmid != "") 
            {
                string req = "<req><crmid>" + crmid + "</crmid></req>";
                string result = FS.HISFC.Components.Order.Classes.WSHelper.InvokeWebService("http://192.168.34.10:8082/IbornCrmService.asmx", "GetConsultAndClientPersonInfo", new string[] { req }) as string;

                if (result.Contains(clientmanagerid)) 
                {
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    doc.LoadXml(result);

                    clientmanagerid = doc.SelectSingleNode("res/infos/info/clientmanagerid").InnerText;
                    clientmanager = doc.SelectSingleNode("res/infos/info/clientmanager").InnerText;
                    consultmanagerid = doc.SelectSingleNode("res/infos/info/consultmanagerid").InnerText;
                    consultmanager = doc.SelectSingleNode("res/infos/info/consultmanager").InnerText;
                }

            }
        }
   
    }
}
