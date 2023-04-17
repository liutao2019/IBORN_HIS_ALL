using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Function;
namespace FS.HISFC.Components.Order.Medical.Controls
{
    /// <summary>
    /// [功能描述: 过敏管理]
    /// [创 建 者: wangw]
    /// [创建时间: 2008-03-28]
    /// [修改者:   张城]
    /// [修改时间: 2008-06-27]
    /// </summary> 
    public partial class ucAllergyIn : FS.FrameWork.WinForms.Controls.ucBaseControl
    {


        public ucAllergyIn()
        {
            InitializeComponent();
        }

        #region 管理类

        #region 修改的地方

        private const int WM_KEYDOWN = 0x0100;
        private const int VK_RETURN = 0x0D;

        #endregion

        /// <summary>
        /// 过敏管理业务类
        /// </summary>
        private FS.HISFC.BizLogic.Order.Medical.AllergyManager allergyManager = new FS.HISFC.BizLogic.Order.Medical.AllergyManager();

        /// <summary>
        /// 住院患者实体
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo myPatients = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 过敏综合实体
        /// </summary>
        private FS.HISFC.Models.Order.Medical.AllergyInfo allergyInfo = null;
                
        private object currentPatient = new object();

        /// <summary>
        /// DataTable
        /// </summary>
        private DataTable dt = new DataTable();

        /// <summary>
        /// DataView
        /// </summary>
        private DataView dv = null;

        private List<FS.HISFC.Models.Pharmacy.Item> items = new List<FS.HISFC.Models.Pharmacy.Item>();

        #endregion

        #region 属性

        /// <summary>
        /// 患者实体
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo MyPatients
        {
            get
            {
                return this.myPatients;
            }
            set
            {
                this.myPatients = value;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void Init()
        {
            this.cmbAllergyDegree.AddItems(CacheManager.GetConList("ALLERGEN_SYMPTOM"));
            this.cmbAllergyType.AddItems(FS.HISFC.Models.Order.Medical.AllergyTypeEnumService.List());
            this.cmbPatientKind.AddItems(CacheManager.GetConList("APPLICABILITYAREA"));
            this.items = CacheManager.PhaIntegrate.QueryItemList(false);

            //this.cmbPatientKind.SelectedIndex = 0;
            this.cmbAllergyDegree.ResetText();
            this.cmbAllergyType.ResetText();
            this.cmbDrug.ResetText();
            this.lbHappenNo.Text = "发生序号";
            this.neuTextBox1.Clear();
            this.lbInpatientNo.Visible = false;

            cmbAllergyDegree.IsListOnly = true;
            cmbAllergyType.IsListOnly = true;
            cmbDrug.IsListOnly = false;
        }

        /// <summary>
        /// 初始化FP
        /// </summary>
        private void InitFp()
        {
            this.dt.Reset();

            System.Type dtBol = System.Type.GetType("System.Boolean");
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtInt = System.Type.GetType("System.Int32");
            System.Type dtDateTime = System.Type.GetType("System.DateTime");
            this.dt.Columns.AddRange(
                new System.Data.DataColumn[]{
                    new DataColumn("病例/住院号",dtStr),
                    new DataColumn("患者类型",dtStr),
                    new DataColumn("过敏类型",dtStr),
                    new DataColumn("过敏源代码",dtStr),
                    new DataColumn("过敏源名称",dtStr),
                    new DataColumn("过敏症状",dtStr),
                    new DataColumn("有效性",dtStr),
                    new DataColumn("备注",dtStr),
                    new DataColumn("操作员",dtStr),
                    new DataColumn("操作时间",dtDateTime),
                    new DataColumn("作废人",dtStr),
                    new DataColumn("作废时间",dtStr),
                    new DataColumn("流水号",dtStr),
                    new DataColumn("发生序号",dtInt)
                }
                );
        }

        /// <summary>
        /// 有效性
        /// </summary>
        /// <returns></returns>
        private bool Vaild()
        {
            if (this.cmbPatientKind.Text == null || this.cmbPatientKind.Text == "")
            {
                MessageBox.Show("请选择患者类型!");
                return false;
            }
            if (this.cmbAllergyDegree.Text == null || this.cmbAllergyDegree.Text == "")
            {
                MessageBox.Show("请选择患者过敏症状!");
                return false;
            }
            if (this.cmbAllergyType.Text == null || this.cmbAllergyType.Text == "")
            {
                MessageBox.Show("请选择患者过敏类型!");
                return false;
            }

            #region 修改的地方
            if (!FS.FrameWork.Public.String.ValidMaxLengh(neuTextBox1.Text, 100))
            {
                MessageBox.Show("备注超过长度！");
                return false;
            }
            #endregion

            return true;
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            if (!this.Vaild())
            {
                return;
            }

            try
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                this.allergyManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                allergyInfo = new FS.HISFC.Models.Order.Medical.AllergyInfo();
                DateTime sysTime = this.allergyManager.GetDateTimeFromSysDateTime();
                
                //1门诊患者/2住院患者
                allergyInfo.PatientType = (FS.HISFC.Models.Base.ServiceTypes)FrameWork.Function.NConvert.ToInt32(this.cmbPatientKind.Tag);
                //病历号或者住院号
                allergyInfo.PatientNO = this.MyPatients.PID.CaseNO;

                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                //药品院内代码
                //obj.ID = this.cmbDrug.SelectedItem.ID.ToString();
                //此处允许手动修改过敏源头
                if (string.IsNullOrEmpty(obj.ID))
                {
                    obj.ID = "A999";
                }
                else
                {
                    obj.ID = this.cmbDrug.Tag.ToString();
                }
                //过敏药物
                //obj.Name = this.cmbDrug.SelectedItem.Name.ToString();
                obj.Name = this.cmbDrug.Text;
                allergyInfo.Allergen = obj;
                //1：皮试阳性 2：休克 3：药疹
                FS.FrameWork.Models.NeuObject tempObj = new FS.FrameWork.Models.NeuObject();
                tempObj.ID = this.cmbAllergyDegree.SelectedItem.ID;
                tempObj.Name = this.cmbAllergyDegree.SelectedItem.Name;
                allergyInfo.Symptom = tempObj;
                //1有效/0无效
                allergyInfo.ValidState = !this.neuCheckBox1.Checked;
                //备注
                allergyInfo.Remark = this.neuTextBox1.Text;
                //操作员代码
                allergyInfo.Oper.ID = this.allergyManager.Operator.ID;
                //操作时间(最新)
                allergyInfo.Oper.OperTime = sysTime;
                if (this.neuCheckBox1.Checked == true)
                {
                    //作废人
                    allergyInfo.CancelOper.ID = this.allergyManager.Operator.ID;
                    //作废时间
                    allergyInfo.CancelOper.OperTime = sysTime;
                }
                //过敏类型
                allergyInfo.Type = (FS.HISFC.Models.Order.Medical.AllergyType)Enum.Parse(typeof(FS.HISFC.Models.Order.Medical.AllergyType), this.cmbAllergyType.Tag.ToString());
                //住院流水号
                allergyInfo.ID = this.MyPatients.ID;
                //发生序号
                if (this.lbHappenNo.Text == "发生序号")
                {
                    allergyInfo.HappenNo = this.allergyManager.GetMaxHappenNo(allergyInfo.ID, NConvert.ToInt32(allergyInfo.PatientType).ToString());
                }
                else
                {
                    allergyInfo.HappenNo = NConvert.ToInt32(this.lbHappenNo.Text.ToString());
                }
                
                if (this.allergyManager.SetAllergyInfo(allergyInfo) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("保存过敏信息发生错误!");
                    return;
                }
            }
            catch (System.Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(ex.Message);
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            this.QueryAllergyInfo();
            MessageBox.Show("保存成功");
            //this.Clear();
        }

        /// <summary>
        /// 查询过敏信息
        /// </summary>
        private void QueryAllergyInfo()
        {
            this.dt.Rows.Clear();
            this.neuSpread1_Sheet1.Rows.Count = 0;

            if (this.myPatients == null)
            {
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询过敏信息...请稍后!");
            Application.DoEvents();
            ArrayList al = new ArrayList();
            //查询过敏信息
            if (this.cmbPatientKind.SelectedIndex == 0)
            {
                al = this.allergyManager.QueryAllergyInfo();
            }
            else if (string.Equals(this.currentPatient.GetType(), typeof(FS.HISFC.Models.RADT.PatientInfo)))
            {
                al = this.allergyManager.QueryAllergyInfo((this.currentPatient as FS.HISFC.Models.RADT.PatientInfo).PID.PatientNO,"2");
            }
            else if(string.Equals(this.currentPatient.GetType(), typeof(FS.HISFC.Models.Registration.Register)))
            {
                al = this.allergyManager.QueryAllergyInfo((this.currentPatient as FS.HISFC.Models.Registration.Register).PID.CardNO,"1");
            }

            if (al == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(this.allergyManager.Err);
                return;
            }

            try
            {
                foreach (object obj in al)
                {
                    allergyInfo = obj as FS.HISFC.Models.Order.Medical.AllergyInfo;
                    if (allergyInfo == null)
                    {
                        continue;
                    }

                    this.AddDataToTable(allergyInfo);
                }
                this.dv = new DataView(this.dt);

                this.neuSpread1_Sheet1.DataAutoSizeColumns = true;

                this.neuSpread1_Sheet1.DataSource = this.dv;

                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColAllergenID].Visible = false;
                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColInpatientNo].Visible = false;

            }
            catch (System.Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("查询过敏信息发生错误!" + ex.Message);
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        /// DataTable赋值
        /// </summary>
        /// <param name="allergy"></param>
        protected void AddDataToTable(FS.HISFC.Models.Order.Medical.AllergyInfo allergy)
        {
            try
            {
                if (this.dt == null)
                {
                    this.InitFp();
                }

                string symptomName = string.Empty;

                switch (allergy.Symptom.ID)
                {
                    case "1":
                        symptomName = "皮试阳性";
                        break;
                    case "2":
                        symptomName = "休克";
                        break;
                    case "3":
                        symptomName = "药疹";
                        break;
                }

                string operTime = allergy.CancelOper.OperTime.Year < 1996 ? "" : allergy.CancelOper.OperTime.Year.ToString();


                this.dt.Rows.Add(new object[]{
                                                            allergy.PatientNO,//病历号或者住院号
                                                            string.Equals(allergy.PatientType.ToString(),"I")?"住院患者":"门诊患者",//1门诊患者/2住院患者
                                                            GetAllergyTypeName( allergy.Type.ToString()),//过敏类型
                                                            allergy.Allergen.ID,//药品院内代码
                                                            allergy.Allergen.Name,//过敏药物
                                                            symptomName,//1：皮试阳性 2：休克 3：药疹
                                                            allergy.ValidState?"有效":"无效",//1有效/0无效
                                                            allergy.Remark,//备注
                                                            SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(allergy.Oper.ID),//操作员代码
                                                            allergy.Oper.OperTime.ToString(),//操作时间(最新)
                                                            SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(allergy.CancelOper.ID),//作废人
                                                            operTime,//作废时间                                                           
                                                            allergy.ID,//门诊号或者住院流水号
                                                            allergy.HappenNo.ToString()//发生序号
                                                        });
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("DataTable内赋值发生错误" + ex.Message);
                return;
            }
        }

        /// <summary>
        /// 双击Fp将数据添加到控件
        /// </summary>
        /// <param name="allergy"></param>
        protected void AddDataToControl(FS.HISFC.Models.Order.Medical.AllergyInfo allergy)
        {
            this.MyPatients.ID = allergy.ID;
            this.MyPatients.PID.CaseNO = allergy.PatientNO;

            //患者类型
            switch (NConvert.ToInt32(allergy.PatientType))
            {
                case 2:
                    this.cmbPatientKind.Tag = "2";
                    break;
                case 1:
                    this.cmbPatientKind.Tag = "1";
                    break;
                default:
                    this.cmbPatientKind.Tag = "0";
                    break;
            }

            this.cmbAllergyType.Tag = allergy.Type.ToString();
            this.cmbDrug.Tag = allergy.Allergen.ID.ToString();
            if (allergy.Allergen.ID.ToString() == "A999")
            {
                this.cmbDrug.Text = allergy.Allergen.Name.ToString();
            }
            this.neuTextBox1.Text = allergy.Remark;
            this.neuCheckBox1.Checked = !allergy.ValidState;
            this.cmbAllergyDegree.Tag = allergy.Symptom.ID;
            this.lbInpatientNo.Text = "流水号:" + this.MyPatients.ID;
            this.lbHappenNo.Text = allergy.HappenNo.ToString();

        }

        #region 修改
        protected override int OnSetValue(object neuObject, TreeNode e)
        {

            cmbDrug.SelectedIndex = -1;
            cmbAllergyDegree.SelectedIndex = -1;
            cmbAllergyType.SelectedIndex = -1;
            cmbDrug.SelectedIndex = -1;
            if (e == null || e.Parent == null)
            {
                this.Clear();
                cmbPatientKind.Enabled = true;
            }
            else
            {
                this.Clear();
                cmbPatientKind.Enabled = false;
                if (string.Equals(neuObject.GetType(), typeof(FS.HISFC.Models.RADT.PatientInfo)))
                {
                    FS.HISFC.Models.RADT.PatientInfo obj = neuObject as FS.HISFC.Models.RADT.PatientInfo;
                    cmbPatientKind.Tag = "2";
                    this.neuSpread1_Sheet1.RowCount = 0;
                    this.MyPatients.ID = obj.ID;
                    this.MyPatients.PID = obj.PID;
                    this.MyPatients.PID.CaseNO = obj.PID.PatientNO;
                    this.currentPatient = neuObject;
                    this.lblPatientInfo.Text = "姓名："+obj.Name + " 性别："+obj.Sex.Name +" 出生日期: "+obj.Birthday.ToString() +" 联系电话："+obj.PhoneHome;
                }
                if (string.Equals(neuObject.GetType(), typeof(FS.HISFC.Models.Registration.Register)))
                {
                    FS.HISFC.Models.Registration.Register obj = neuObject as FS.HISFC.Models.Registration.Register;
                    cmbPatientKind.Tag = "1";
                    this.neuSpread1_Sheet1.RowCount = 0;
                    this.MyPatients.ID = obj.ID;
                    this.MyPatients.PID = obj.PID;
                    this.currentPatient = neuObject;
                    this.MyPatients.PID.CaseNO = obj.PID.CardNO;

                    this.lblPatientInfo.Text = "姓名：" + obj.Name + " 性别：" + obj.Sex.Name + " 出生日期: " + obj.Birthday.ToString() + " 联系电话：" + obj.PhoneHome;
                }
                this.QueryAllergyInfo();
            }

            return base.OnSetValue(neuObject, e);
        }

        private void Clear()
        {

            this.cmbAllergyDegree.ResetText();
            this.cmbAllergyType.ResetText();
            this.cmbDrug.ResetText();
            this.lbHappenNo.Text = "发生序号";
            
            this.neuTextBox1.Clear();
            this.lbInpatientNo.Visible = false;
            this.lbInpatientNo.Text = string.Empty;
            this.myPatients = new FS.HISFC.Models.RADT.PatientInfo();
            cmbPatientKind.SelectedIndex = -1;
            this.neuCheckBox1.Checked = false;
        }

        private string GetAllergyTypeName(string type)
        {
            switch (type)
            {
                case "DA": return "药物过敏";
                case "FA": return "食物过敏";
                case "MA": return "混合型过敏";
                case "MC": return "混合型禁忌";
            }
            return "";
        }

        #endregion

        #endregion

        #region 事件

        private void ucAllergyIn_Load(object sender, EventArgs e)
        {
            this.Init();
            this.InitFp();
            this.InitIHypoTest();
            this.dv = new DataView(this.dt);
            this.neuSpread1_Sheet1.DataSource = this.dv;
        }
         /// <summary>
        /// 初始化皮试接口实例{D1B1616C-3863-40f6-AAD5-11D9161C6B14}
        /// </summary>
        private void InitIHypoTest()
        {
   
        }

        private void cmbPatientKind_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lbInpatientNo.Text = string.Empty;
            if (this.cmbPatientKind.Tag.ToString() == "2")
            {
                this.lbInpatientNo.Visible = false;
            }
            else if (this.cmbPatientKind.Tag.ToString() == "1")
            {
                this.lbInpatientNo.Visible = false;
            }
            else if (this.cmbPatientKind.Tag.ToString() == "0")
            {
                this.lbInpatientNo.Visible = false;
            }
            else
            {
                this.lbInpatientNo.Visible = false;
            }
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void cmbAllergyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbDrug.SelectedIndex = -1;
            ArrayList al = new ArrayList();

            if (this.cmbAllergyType.Tag.ToString() == "FA")
            {
                this.cmbDrug.AddItems(CacheManager.GetConList("ALLERGEN_SOURCE"));
            }
            else
            {
                al = new ArrayList(this.items);
                
                this.cmbDrug.AddItems(al);
            }
            cmbDrug.SelectedIndex = -1;
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string patientNo = this.neuSpread1_Sheet1.Cells[e.Row, 0].Text;
            string inpatientNO = this.neuSpread1_Sheet1.Cells[e.Row, 12].Text;
            int happenNO = NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[e.Row, 13].Text);
            ArrayList al = this.allergyManager.GetAllergyInfo(patientNo, inpatientNO, happenNO);
            FS.HISFC.Models.Order.Medical.AllergyInfo allergy = al[0] as FS.HISFC.Models.Order.Medical.AllergyInfo;
            this.AddDataToControl(allergy);
        }

        #endregion

        #region 列枚举

        private enum ColumnSet
        {
            //病历号或者住院号
            ColPatientNO,
            //患者类型
            ColPatientKind,
            //过敏类型
            ColAllergyType,
            //药品院内代码
            ColAllergenID,
            //药品名称
            ColAllergenName,
            //过敏症状
            ColSymptomName,
            //有效性
            ColVaild,
            //备注
            ColMark,
            //操作员编码
            ColOperCode,
            //操作时间
            ColOperTime,
            //作废人编码
            ColCanclerID,
            //作废时间
            ColCancleTime,
            //流水号
            ColInpatientNo,
            //发生序号
            ColHappenNo
        }

        #endregion
    }
}
