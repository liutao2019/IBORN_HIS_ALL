using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FarPoint.Win.Spread;
namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    /// <summary>
    /// ucDiagNoseInput<br></br>
    /// [功能描述: 病案诊断录入]<br></br>
    /// [创 建 者: 张俊义]<br></br>
    /// [创建时间: 2007-04-20]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucMetCasDiagnose : UserControl
    {
        public ucMetCasDiagnose()
        {
            InitializeComponent();
        }

        #region  全局变量
        //诊断类别
        private ArrayList diagnoseType = new ArrayList();
        private FS.FrameWork.Public.ObjectHelper diagnoseTypeHelper = new FS.FrameWork.Public.ObjectHelper();
        //分期列表
        private ArrayList PeriorList = new ArrayList();
        private FS.FrameWork.Public.ObjectHelper PeriorListHelper = new FS.FrameWork.Public.ObjectHelper();
        //分级列表
        private ArrayList LeveList = new ArrayList();
        private FS.FrameWork.Public.ObjectHelper LeveListHelper = new FS.FrameWork.Public.ObjectHelper();
        //入院病情 列表
        private ArrayList diagOutStateList = new ArrayList();
        private FS.FrameWork.Public.ObjectHelper diagOutStateListHelper = new FS.FrameWork.Public.ObjectHelper();
        //配置文件的路径 
        private string filePath = Application.StartupPath + "\\profile\\ucDiagNoseInput.xml";
        private DataTable dtDiagnose = new DataTable("诊断信息表");
        private FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();
        //操作 手术类型 
        private ArrayList OperList = new ArrayList();
        private FS.FrameWork.Public.ObjectHelper OperListHelper = new FS.FrameWork.Public.ObjectHelper();
        //诊断信息
        public ArrayList diagList = null;
        //标识是医生站 还是 病案室
        private FS.HISFC.Models.HealthRecord.EnumServer.frmTypes frmType;

        #region  列的全局变量
        private enum Col
        {
            DiagKind = 0, //诊断类别
            Icd10Code = 1, //ICD10 编码 
            Icd10Name = 2,//ICD10 名称
            OutState = 3, //入院病情
            OperationFlag = 4, //手术
            Disease = 5, //30种疾病
            CLPa = 6,//病理符合
            Perior = 7,//分期
            Level = 8,//分级
            DubDiag = 9,//是否疑诊
            MainDiag = 10,//主诊断
            //{74A9AA46-74B3-49e8-910A-54A998E428AF} 增加拟诊功能
            IsDraftExamine = 17, //是否拟诊
            ICD=18,//ICD编码
            IcdName=19, //ICD名称
            //ICDFCode=20,//ICD附属编码
            //ICDFName=21 //ICD附属编码名称

        }

        #endregion
        #endregion

        #region 属性
        //{8BC09475-C1D9-4765-918B-299E21E04C74} 诊断录入增加医生站、门诊医生站、病案室属性
        /// <summary>
        /// 病案室还是医生站使用  true 医生站  false 病案室 
        /// </summary>
        private bool isCas = true;
        /// <summary>
        /// 病案室还是医生站使用  true 医生站  false 病案室
        /// </summary>
        public bool IsCas
        {
            get
            {
                return isCas;
            }
            set
            {
                isCas = value;
            }
        }
        /// <summary>
        /// 是否拟诊  true 拟诊 false 非拟诊 chengym
        /// 用法：拟诊 直接录入描述性诊断  可以再编辑 icd10  非拟诊 直接录入 ICD10
        /// </summary>
        private bool isDoubt = true;
        /// <summary>
        /// 拟诊属性 true 拟诊 false 非拟诊
        /// </summary>
        public bool IsDoubt
        {
            get { return this.isDoubt; }
            set { this.isDoubt = value; }
        }
        #region {6EF7D73B-4350-4790-B98C-C0BD0098516E}
        /// <summary>
        /// 科室常用诊断标志
        /// </summary>
        private bool isUseDeptICD = false;

        /// <summary>
        /// 科室常用诊断标志
        /// </summary>
        [Category("科室常用诊断"), Description("是否其使用科室常用诊断")]
        public bool IsUseDeptICD
        {
            get
            {
                return isUseDeptICD;
            }
            set
            {
                isUseDeptICD = value;
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// 病人信息
        /// </summary>
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
            }
        }

        /// <summary>
        /// 增加一个空白行 
        /// </summary>
        /// <returns></returns>
        public void AddBlankRow()
        {
            this.fpEnter1_Sheet1.Rows.Add(this.fpEnter1_Sheet1.RowCount, 1);
            this.fpEnter1.Focus();
            this.fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.RowCount-1, 0);

        }
        /// <summary>
        /// 院内感染次数 
        /// </summary>
        /// <returns></returns>
        public int GetInfectionNum()
        {
            int j = 0;
            if (fpEnter1_Sheet1.RowCount == 0)
            {
                return 0;
            }
            string strName = diagnoseTypeHelper.GetName("4");
            for (int i = 0; i < fpEnter1_Sheet1.RowCount; i++)
            {
                if (fpEnter1_Sheet1.Cells[i, 0].Text == strName)
                {
                    j++;
                }
            }
            return j;
        }
        /// <summary>
        /// 是否有并发症
        /// </summary>
        /// <returns></returns>
        public string GetSyndromeFlag()
        {
            string str = "0";
            if (fpEnter1_Sheet1.RowCount == 0)
            {
                return "0";
            }
            for (int i = 0; i < fpEnter1_Sheet1.RowCount; i++)
            {
                //if (fpEnter1_Sheet1.Cells[i, 0].Text == str)
                if(fpEnter1_Sheet1.Cells[i,0].Text=="并发症诊断")
                {
                    str = "1";
                    break;
                }
            }
            return str;
        }
        /// <summary>
        /// 设置活动单元格
        /// </summary>
        public void SetActiveCells()
        {
            try
            {
                this.fpEnter1_Sheet1.SetActiveCell(0, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 清空原有的数据
        /// </summary>
        /// <returns></returns>
        public int ClearInfo()
        {
            if (this.dtDiagnose != null)
            {
                this.dtDiagnose.Clear();
                LockFpEnter();
            }
            else
            {
                MessageBox.Show("诊断表为null");
            }
            return 1;
        }
        /// <summary>
        /// 获取所有的诊断信息
        /// </summary>
        /// <returns></returns>
        public int GetAllDiagnose(ArrayList list)
        {
            //{691E10E6-4AB5-4252-82AD-4552DB079F2F}
            this.fpEnter1.StopCellEditing();
            foreach (DataRow dr in dtDiagnose.Rows)
            {
                dr.EndEdit();
            }
            DataTable tempdt = dtDiagnose.Copy();
            tempdt.AcceptChanges();
            GetChangeInfo(tempdt, list);
            return 1;
        }
        /// <summary>
        /// 设置编辑模式
        /// </summary>
        /// <param name="type">属性bool值</param>
        /// <param name="editType">编辑窗口的类型</param>
        /// <returns></returns>
        public int SetReadOnly(bool type ,FS.HISFC.Models.HealthRecord.EnumServer.frmTypes editType)
        {
            if (type)
            {
                this.btAdd.Enabled = !type;
                this.btDelete.Enabled = !type;
                if (editType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
                {
                    this.fpEnter1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.Normal;
                    this.fpEnter1_Sheet1.Columns[0].Locked = true; //诊断类别
                    this.fpEnter1_Sheet1.Columns[1].Locked = true;
                    this.fpEnter1_Sheet1.Columns[2].Locked = true;
                    this.fpEnter1_Sheet1.Columns[3].Locked = true; //入院病情
                    this.fpEnter1_Sheet1.Columns[4].Locked = true; //有无手术
                    this.fpEnter1_Sheet1.Columns[5].Locked = true; //30种疾病
                    this.fpEnter1_Sheet1.Columns[6].Locked = true; //病理符合
                    this.fpEnter1_Sheet1.Columns[7].Locked = true; //分期
                    this.fpEnter1_Sheet1.Columns[8].Locked = true; //分级
                    this.fpEnter1_Sheet1.Columns[9].Locked = true; //是否疑诊
                }
                else
                {
                    this.fpEnter1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
                }
            }
            else
            {
                this.btAdd.Enabled = !type;
                this.btDelete.Enabled = !type;
                this.fpEnter1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.Normal;
            }
            return 0;
        }
        /// <summary>
        /// 校验数据的合法性。
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int ValueStateNew(List<FS.HISFC.Models.HealthRecord.Diagnose> list)
        {
            if (list == null)
            {
                return -2;
            }
            foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in list)
            {
                if (obj.DiagInfo.Patient.ID == "" || obj.DiagInfo.Patient.ID == null)
                {
                    MessageBox.Show("诊断信息的住院流水号不能为空");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.Patient.ID,14))
                {
                    MessageBox.Show("诊断信息的住院流水号过长");
                    return -1;
                }
                if (obj.DiagInfo.HappenNo > 999999999)
                {
                    MessageBox.Show("诊断信息的发生序号过长");
                    return -1;
                }
                if (obj.DiagInfo.DiagType.ID == "" || obj.DiagInfo.DiagType.ID == null)
                {
                    MessageBox.Show("诊断信息的诊断类型不能为空");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.DiagType.ID, 2))
                {
                    MessageBox.Show("诊断信息的诊断类型编码过长");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.LevelCode, 20))
                {
                    MessageBox.Show("诊断信息的诊断级别编码过长");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.PeriorCode,20))
                {
                    MessageBox.Show("诊断信息的诊断分期编码过长");
                    return -1;
                }
                //{74A9AA46-74B3-49e8-910A-54A998E428AF} 增加拟诊功能
                if (obj.Pvisit.Memo == "0")
                {
                    if (obj.DiagInfo.ICD10.ID.Trim() == "" || obj.DiagInfo.ICD10.ID.Trim() == null)
                    {
                        MessageBox.Show("诊断信息的ICD诊断不能为空");
                        return -1;
                    }
                    if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.ICD10.ID, 50))
                    {
                        MessageBox.Show("诊断信息的诊断编码过长");
                        return -1;
                    }
                }
                if (obj.DiagInfo.ICD10.Name == "" || obj.DiagInfo.ICD10.Name == null)
                {
                    MessageBox.Show("诊断信息的ICD诊断不能为空");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.ICD10.Name, 100))
                {
                    MessageBox.Show("诊断信息的诊断名称过长");
                    return -1;
                }
                if (obj.DiagInfo.Doctor.ID == "" || obj.DiagInfo.Doctor.ID == null)
                {
                    MessageBox.Show("诊断信息的诊断医生编码不能为空");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.Doctor.ID, 6))
                {
                    MessageBox.Show("诊断信息的医生编码过长");
                    return -1;
                }
                if (obj.DiagInfo.Doctor.Name == "" || obj.DiagInfo.Doctor.Name == null)
                {
                    MessageBox.Show("诊断信息的诊断医生不能为空");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.Doctor.Name,10))
                {
                    MessageBox.Show("诊断信息的医生名称过长");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagOutState, 20))
                {
                    MessageBox.Show("诊断信息的入院病情编码过长");
                    return -1;
                }
                if (obj.DiagInfo.DiagType.ID == "1" && obj.DiagOutState == "")
                {
                    MessageBox.Show("请选择主要诊断的入院病情！");
                    return -1;
                }
                if (obj.OperType.Length > 1)
                {
                    MessageBox.Show("诊断信息的类别编码过长");
                    return -1;
                }
            }
            return 0;
        }

        /// <summary>
        /// 校验数据的合法性。
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int ValueState(ArrayList list)
        {
            if (list == null)
            {
                return -2;
            }
            foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in list)
            {
                if (obj.DiagInfo.Patient.ID == "" || obj.DiagInfo.Patient.ID == null)
                {
                    MessageBox.Show("诊断信息的住院流水号不能为空");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.Patient.ID, 14))
                {
                    MessageBox.Show("诊断信息的住院流水号过长");
                    return -1;
                }
                if (obj.DiagInfo.HappenNo > 999999999)
                {
                    MessageBox.Show("诊断信息的发生序号过长");
                    return -1;
                }
                if (obj.DiagInfo.DiagType.ID == "" || obj.DiagInfo.DiagType.ID == null)
                {
                    MessageBox.Show("诊断信息的诊断类型不能为空");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.DiagType.ID, 2))
                {
                    MessageBox.Show("诊断信息的诊断类型编码过长");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.LevelCode, 20))
                {
                    MessageBox.Show("诊断信息的诊断级别编码过长");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.PeriorCode, 20))
                {
                    MessageBox.Show("诊断信息的诊断分期编码过长");
                    return -1;
                }
                //{74A9AA46-74B3-49e8-910A-54A998E428AF} 增加拟诊功能
                if (obj.Pvisit.Memo == "0")
                {
                    if (obj.DiagInfo.ICD10.ID.Trim() == "" || obj.DiagInfo.ICD10.ID.Trim() == null)
                    {
                        MessageBox.Show("诊断信息的ICD诊断不能为空");
                        return -1;
                    }
                    if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.ICD10.ID, 50))
                    {
                        MessageBox.Show("诊断信息的诊断编码过长");
                        return -1;
                    }
                }
                if (obj.DiagInfo.ICD10.Name == "" || obj.DiagInfo.ICD10.Name == null)
                {
                    MessageBox.Show("诊断信息的ICD诊断不能为空");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.ICD10.Name, 100))
                {
                    MessageBox.Show("诊断信息的诊断名称过长");
                    return -1;
                }
                if (obj.DiagInfo.Doctor.ID == "" || obj.DiagInfo.Doctor.ID == null)
                {
                    MessageBox.Show("诊断信息的诊断医生编码不能为空");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.Doctor.ID, 6))
                {
                    MessageBox.Show("诊断信息的医生编码过长");
                    return -1;
                }
                if (obj.DiagInfo.Doctor.Name == "" || obj.DiagInfo.Doctor.Name == null)
                {
                    MessageBox.Show("诊断信息的诊断医生不能为空");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagInfo.Doctor.Name, 10))
                {
                    MessageBox.Show("诊断信息的医生名称过长");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.DiagOutState, 20))
                {
                    MessageBox.Show("诊断信息的入院病情编码过长");
                    return -1;
                }
                if (obj.DiagInfo.DiagType.ID == "1" && obj.DiagOutState == "")
                {
                    MessageBox.Show("请选择主要诊断的入院病情！");
                    return -1;
                }
                if (obj.OperType.Length > 1)
                {
                    MessageBox.Show("诊断信息的类别编码过长");
                    return -1;
                }
            }
            return 0;
        }

        /// <summary>
        /// 保存对表做的所有修改
        /// </summary>
        /// <returns></returns>
        public int fpEnterSaveChanges()
        {
            try
            {
                this.dtDiagnose.AcceptChanges();
                LockFpEnter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 返回当前行数
        /// </summary>
        /// <returns></returns>
        public int GetfpSpreadRowCount()
        {
            return fpEnter1_Sheet1.Rows.Count;
        }
        /// <summary>
        /// 如果reset 为真 则清空现有数据 并保存更改  为假 只是保存当前更改
        /// creator:zhangjunyi@FS.com
        /// </summary>
        /// <param name="reset"></param>
        /// <returns></returns>
        public bool Reset(bool reset)
        {
            if (reset)
            {
                //清空数据 保存更改 
                if (dtDiagnose != null)
                {
                    dtDiagnose.Clear();
                    dtDiagnose.AcceptChanges();
                }
            }
            else
            {
                //保存更改
                dtDiagnose.AcceptChanges();
            }
            LockFpEnter();
            return true;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void InitInfo()
        {
            try
            {
                #region {6EF7D73B-4350-4790-B98C-C0BD0098516E}
                if (!this.DesignMode)
                {
                    ////this.ucDiagnose1.IsUseDeptICD = this.isUseDeptICD;
                    //this.ucDiagnose1.Init();
                    this.ucDiagnose1.InitCase();
                }
                #endregion
                //初始化表
                InitDateTable();
                //设置下拉列表
                this.initList();
                //InputMap im;
                //im = fpEnter1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                //im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                //im = fpEnter1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                //im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                //im = fpEnter1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                //im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
 
                fpEnter1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
                LockFpEnter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 根据输入的住院患者信息 和type参数查询诊断信息
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="Type"></param>
        /// <returns>-1 出错 0 传入的病人信息为空,不作处理，1 不允许有病案，2病案已经封存，不允许医生修改和查阅 3 查询有数据 4查询没有数据  </returns>
        public int LoadInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes Type)
        {
            try
            {
                this.rtxtDiag.Text = "";//add chengym
                frmType = Type;
                if (patientInfo == null)
                {
                    //没有该病人的信息
                    return 0;
                }

                patient = patientInfo;
                if (patientInfo.CaseState == "0")
                {
                    //不允许有病案
                    return 1;
                }
                //定义业务层的类
                FS.HISFC.BizLogic.HealthRecord.Diagnose diag = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
                diagList = new ArrayList();

                if (Type == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC) // 医生站录入病历
                {
                    #region  医生站录入病历

                    //add by chengym 2011-9-27 
                    this.DiagnoseLoadInfo(patientInfo.ID, Type);

                    //目前允许有病历 并且目前没有录入病历  或者标志位位空（默认是允许录入病历） 
                    // 医生站录入病例
                    if (patientInfo.CaseState == "1" || patientInfo.CaseState == "2")
                    {
                        //从医生站录入的信息中查询
                        diagList = diag.QueryCaseDiagnose(patientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC,FS.HISFC.Models.Base.ServiceTypes.I);
                    }
                    else
                    {
                        // 病案已经封存已经不允许医生修改和查阅
                        return 2;
                    }

                    #endregion
                }
                else if (Type == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)//病案室录入病历
                {
                    #region 病案室录入病历

                    ArrayList alTempDocDiagnose = new ArrayList();
                    ArrayList alTempCasDiagnose = new ArrayList();
                    //目前允许有病历 并且目前没有录入病历  或者标志位位空（默认是允许录入病历） 
                    if (patientInfo.CaseState == "1" || patientInfo.CaseState == "2" || patientInfo.CaseState == "3")
                    {
                        //医生站已经录入病案
                        alTempDocDiagnose = diag.QueryCaseDiagnose( patientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC,FS.HISFC.Models.Base.ServiceTypes.I );

                        //病案室已经录入病案
                        alTempCasDiagnose = diag.QueryCaseDiagnose( patientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS,FS.HISFC.Models.Base.ServiceTypes.I );

                        if (alTempDocDiagnose != null && alTempDocDiagnose.Count > 0)
                        {
                            diagList.AddRange( alTempDocDiagnose );
                        }
                        if (alTempCasDiagnose != null && alTempCasDiagnose.Count > 0)
                        {
                            diagList.AddRange( alTempCasDiagnose );
                        }
                    }
                    else
                    {
                        //病案已经封存 不允许修改。
                        diagList = diag.QueryCaseDiagnose( patientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS,FS.HISFC.Models.Base.ServiceTypes.I );
                        this.fpEnter1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
                    }                 

                    #endregion
                }
                else
                {
                    //没有传入参数 不作任何处理
                }

                if (diagList != null)
                {
                    //查询有数据
                    AddInfoToTable(diagList);
                    return 3;
                }
                else
                {//查询没有数据
                    return 4;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        //{8BC09475-C1D9-4765-918B-299E21E04C74} 诊断录入增加医生站、门诊医生站、病案室属性
        /// <summary>
        /// 根据输入的住院患者信息 和type参数查询诊断信息
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="Type"></param>
        /// <returns>-1 出错 0 传入的病人信息为空,不作处理，1 不允许有病案，2病案已经封存，不允许医生修改和查阅 3 查询有数据 4查询没有数据  </returns>
        public int LoadInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes Type, string diagInputType)
        {
           return this.LoadInfo(patientInfo, Type);
            #region  屏蔽原来没有必要处理分开处理 2012-1-10 chengym
            //try
            //{
            //    frmType = Type;
            //    if (patientInfo == null)
            //    {
            //        //没有该病人的信息
            //        return 0;
            //    }

            //    patient = patientInfo;
            //    if (patientInfo.CaseState == "0")
            //    {
            //        //不允许有病案
            //        return 1;
            //    }
            //    //定义业务层的类
            //    FS.HISFC.BizLogic.HealthRecord.Diagnose diag = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            //    diagList = new ArrayList();

            //    if (Type == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC) // 医生站录入病历
            //    {
            //        if (diagInputType == "cas")
            //        {
            //            #region  医生站录入病历

            //            //目前允许有病历 并且目前没有录入病历  或者标志位位空（默认是允许录入病历） 
            //            // 医生站录入病例
            //            if (patientInfo.CaseState == "1" || patientInfo.CaseState == "2")
            //            {
            //                //从医生站录入的信息中查询
            //                diagList = diag.QueryCaseDiagnose(patientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC,FS.HISFC.Models.Base.ServiceTypes.I);
            //            }
            //            else
            //            {
            //                // 病案已经封存已经不允许医生修改和查阅
            //                return 2;
            //            }

            //            #endregion
            //        }
            //        else
            //        {
            //            diagList = diag.QueryDiagnoseNoOps(patientInfo.ID);

            //        }
            //    }
            //    else if (Type == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)//病案室录入病历
            //    {
            //        #region 病案室录入病历
            //        //目前允许有病历 并且目前没有录入病历  或者标志位位空（默认是允许录入病历） 
            //        if (patientInfo.CaseState == "1" || patientInfo.CaseState == "2")
            //        {
            //            //医生站已经录入病案
            //            diagList = diag.QueryCaseDiagnose(patientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC,FS.HISFC.Models.Base.ServiceTypes.I);
            //        }
            //        else if (patientInfo.CaseState == "3")
            //        {
            //            //病案室已经录入病案
            //            diagList = diag.QueryCaseDiagnose(patientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS,FS.HISFC.Models.Base.ServiceTypes.I);
            //        }
            //        else
            //        {
            //            //病案已经封存 不允许修改。
            //            diagList = diag.QueryCaseDiagnose(patientInfo.ID, "%", FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS,FS.HISFC.Models.Base.ServiceTypes.I);
            //            this.fpEnter1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
            //        }

            //        #endregion
            //    }
            //    else
            //    {
            //        //没有传入参数 不作任何处理
            //    }

            //    if (diagList != null)
            //    {
            //        //查询有数据
            //        AddInfoToTable(diagList);

            //        //for (int i = 0; i < diagList.Count; i++)
            //        //{
            //        //    FS.HISFC.Models.HealthRecord.Diagnose diagInfo = diagList[i] as FS.HISFC.Models.HealthRecord.Diagnose;
            //        //    if (diagInfo.IsDraftExamine == "1") //判断是否拟诊
            //        //    {
            //        //        this.fpEnter1_Sheet1.Cells[i, (int)Col.Icd10Code].Locked = true;
            //        //        this.fpEnter1_Sheet1.Cells[i, (int)Col.Icd10Name].Locked = false;

            //        //    }
            //        //    else
            //        //    {
            //        //        this.fpEnter1_Sheet1.Cells[i, (int)Col.Icd10Code].Locked = false;
            //        //        this.fpEnter1_Sheet1.Cells[i, (int)Col.Icd10Name].Locked = true;

            //        //    }
            //        //}
            //        this.dtDiagnose.AcceptChanges();

            //        return 3;
            //    }
            //    else
            //    {//查询没有数据
            //        return 4;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    return -1;
            //}
            #endregion 
        }
        public bool GetList(string strType, ArrayList list)
        {
            try
            {
                this.fpEnter1.StopCellEditing();
                //this.fpEnter1.EditModePermanent = false;
                //this.fpEnter1.EditModeReplace = false;
                foreach (DataRow dr in this.dtDiagnose.Rows)
                {
                    dr.EndEdit();
                }
                switch (strType)
                {
                    case "A":
                        //增加的数据
                        DataTable AddTable = this.dtDiagnose.GetChanges(DataRowState.Added);
                        GetChangeInfo(AddTable, list);
                        break;
                    case "M":
                        DataTable ModTable = this.dtDiagnose.GetChanges(DataRowState.Modified);
                        GetChangeInfo(ModTable, list);
                        break;
                    case "D":
                        DataTable DelTable = this.dtDiagnose.GetChanges(DataRowState.Deleted);
                        if (DelTable != null)
                        {
                            DelTable.RejectChanges();
                        }
                        GetChangeInfo(DelTable, list);
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 删除当前行 
        /// </summary>
        /// <returns></returns>
        public int DeleteActiveRow()
        {
            this.fpEnter1.SetAllListBoxUnvisible();
            this.fpEnter1.EditModePermanent = false;
            this.fpEnter1.EditModeReplace = false;
            if (fpEnter1_Sheet1.Rows.Count > 0)
            {
                this.fpEnter1_Sheet1.Rows.Remove(fpEnter1_Sheet1.ActiveRowIndex, 1);

                //{3C71EDBD-8179-41e6-98DB-B70CC6E01D61}
                if (this.ucDiagnose1.Visible) 
                {
                    this.ucDiagnose1.Visible = false;
                }
            }
            if (fpEnter1_Sheet1.Rows.Count == 0)
            {
                this.fpEnter1.SetAllListBoxUnvisible();
                #region {3C71EDBD-8179-41e6-98DB-B70CC6E01D61}
                this.ucDiagnose1.Visible = false;
                #endregion
            } 
            this.fpEnter1.EditModePermanent = true;
            this.fpEnter1.EditModeReplace = true;
            return 1;
        }
        /// <summary>
        /// 删除空白的行
        /// </summary>
        /// <returns></returns>
        public int deleteRow()
        {
            this.fpEnter1.SetAllListBoxUnvisible();
            this.fpEnter1.EditModePermanent = false;
            this.fpEnter1.EditModeReplace = false;
            if (fpEnter1_Sheet1.Rows.Count == 1)
            {
                //第一行出院诊断名称为空 
                if (fpEnter1_Sheet1.Cells[0, 2].Text == "")
                {
                    fpEnter1_Sheet1.Rows.Remove(0, 1);
                }
            }
            #region {3C71EDBD-8179-41e6-98DB-B70CC6E01D61}
            if (fpEnter1_Sheet1.Rows.Count == 0)
            {
                this.fpEnter1.SetAllListBoxUnvisible();
                
                this.ucDiagnose1.Visible = false;
            }
            #endregion
            this.fpEnter1.EditModePermanent = true;
            this.fpEnter1.EditModeReplace = true;
            return 1;
        }
        /// <summary>
        /// 获取修改过的信息
        /// </summary>
        /// <returns></returns>
        private bool GetChangeInfo(DataTable tempTable, ArrayList list)
        {
            if (tempTable == null)
            {
                return true;
            }
            try
            {
                FS.HISFC.Models.HealthRecord.Diagnose info = null;
                foreach (DataRow row in tempTable.Rows)
                {
                    info = new FS.HISFC.Models.HealthRecord.Diagnose();
                    info.DiagInfo.Patient.ID = this.patient.ID;
                    //{8BC09475-C1D9-4765-918B-299E21E04C74} 诊断录入增加医生站、门诊医生站、病案室属性
                    info.DiagInfo.Patient.PID.CardNO = this.patient.PID.CardNO;
                    //诊断类别
                    info.DiagInfo.DiagType.ID = diagnoseTypeHelper.GetID(row["诊断类别"].ToString());
                    //出院诊断的明细类别
                    //					info.MainFlag = diagnoseTypeHelper.GetID(row["诊断类别"].ToString());
                    info.DiagInfo.ICD10.ID = row["ICD10"].ToString();//2
                    if (info.DiagInfo.DiagType.ID == "1") //将主诊断设置成 
                    {
                        info.DiagInfo.IsMain = true;
                    }
                    else
                    {
                        info.DiagInfo.IsMain = false;
                    }
                    info.DiagInfo.ICD10.Name = row["诊断名称"].ToString();
                    if (row["入院病情"] != DBNull.Value)
                    {
                        info.DiagOutState = diagOutStateListHelper.GetID(row["入院病情"].ToString()); //3
                    }
                    if (row["有无手术"] != DBNull.Value)
                    {
                        info.OperationFlag = OperListHelper.GetID(row["有无手术"].ToString());
                    }

                    if (ConvertBool(row["30种疾病"]))//5
                    {
                        info.Is30Disease = "1";
                    }
                    else
                    {
                        info.Is30Disease = "0";
                    }
                    if (ConvertBool(row["病理符合"]))//6
                    {
                        info.CLPA = "1";
                    }
                    else
                    {
                        info.CLPA = "0";
                    }
                    if (row["分级"] != DBNull.Value)
                    {
                        info.LevelCode = LeveListHelper.GetID(row["分级"].ToString()); //7
                    }
                    if (row["分期"] != DBNull.Value)
                    {
                        info.PeriorCode = PeriorListHelper.GetID(row["分期"].ToString());//8
                    }
                    if (ConvertBool(row["是否疑诊"]))//9
                    {
                        info.DubDiagFlag = "1";
                    }
                    else
                    {
                        info.DubDiagFlag = "0";
                    }
                    info.DiagInfo.HappenNo = FS.FrameWork.Function.NConvert.ToInt32(row["序号"]);//10
                    info.DiagInfo.DiagDate = FS.FrameWork.Function.NConvert.ToDateTime(row["诊断日期"]);//11
                    //{8BC09475-C1D9-4765-918B-299E21E04C74} 诊断录入增加医生站、门诊医生站、病案室属性
                    if (info.DiagInfo.DiagDate == System.DateTime.MinValue)
                    {
                        info.DiagInfo.DiagDate = (new FrameWork.Management.DataBaseManger()).GetDateTimeFromSysDateTime();
                    }
                    info.Pvisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(row["入院日期"]);//12
                    info.Pvisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(row["出院日期"]);//13
                    if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
                    {
                        info.OperType = "1";
                    }
                    else if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
                    {
                        info.OperType = "2";
                    }
                    else
                    {
                    }
                    FS.HISFC.BizLogic.HealthRecord.Diagnose dia = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
                    if (row["诊断医师代码"] != DBNull.Value)
                    {
                        info.DiagInfo.Doctor.ID = row["诊断医师代码"].ToString();
                    }
                    else
                    {
                        info.DiagInfo.Doctor.ID = dia.Operator.ID;
                        info.DiagInfo.Doctor.Name = dia.Operator.Name;
                    }
                    if (row["诊断医师"] != DBNull.Value)
                    {
                        info.DiagInfo.Doctor.Name = row["诊断医师"].ToString();
                    }
                    else
                    {
                        info.DiagInfo.Doctor.ID = dia.Operator.ID;
                        info.DiagInfo.Doctor.Name = dia.Operator.Name;
                    }
                    //{8BC09475-C1D9-4765-918B-299E21E04C74} 诊断录入增加医生站、门诊医生站、病案室属性
                    info.DiagInfo.Dept.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                    info.DiagInfo.IsValid = true;
                    //{74A9AA46-74B3-49e8-910A-54A998E428AF} 增加拟诊功能
                    if (ConvertBool(row["是否拟诊"]))
                    {
                        info.User01 = "1";
                    }
                    else
                    {
                        info.User01 = "0";
                    }
                    //info.DiagInfo.ICDF10.ID = row["ICD附属码"].ToString();
                    //info.DiagInfo.ICDF10.Name = row["ICD附属码名称"].ToString();

                    list.Add(info);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        
        /// <summary>
        /// 将实体转化成BOOL类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool ConvertBool(object obj)
        {
            if (obj == DBNull.Value)
            {
                return false;
            }
            return Convert.ToBoolean(obj);
        }
        /// <summary>
        /// 将保存完的数据回写到表中
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int fpEnterSaveChanges(ArrayList list)
        {
            AddInfoToTable(list);
            dtDiagnose.AcceptChanges();
            this.LockFpEnter();
            return 0;
        }
        /// <summary>
        /// 查询诊断信息并且填充的表中
        /// </summary>
        private void AddInfoToTable(ArrayList alReturn)
        {
            bool Result = false;
            if ((this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC && this.patient.CaseState == "2") || (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS && this.patient.CaseState == "3"))
            {
                Result = true;
            }
            //清空以前的数据
            if (this.dtDiagnose != null)
            {
                this.dtDiagnose.Clear();
                this.dtDiagnose.AcceptChanges();
            }
            //循环插入信息
            foreach (FS.HISFC.Models.HealthRecord.Diagnose obj in alReturn)
            {
                //这里只存除了门诊诊断和入院诊断之外的诊断
                //				if(obj.DiagInfo.DiagType.ID != "1"&&obj.DiagInfo.DiagType.ID != "14")
                //				{
                DataRow row = dtDiagnose.NewRow();

                SetRow(obj, row, Result);
                dtDiagnose.Rows.Add(row);
                //				}

            }
            //			if(System.IO.File.Exists(filePath))
            //			{
            //				FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpEnter1_Sheet1,filePath);
            //			}
            if ((this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC && this.patient.CaseState == "2") || (this.frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS && this.patient.CaseState == "3"))
            {
                //清空表的标志位
                dtDiagnose.AcceptChanges();
            }
            LockFpEnter();
        }

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="row"></param>
        /// <param name="info"></param>
        private void SetRow(FS.HISFC.Models.HealthRecord.Diagnose info, DataRow row, bool tempBool)
        {
            row["诊断类别"] = diagnoseTypeHelper.GetName(info.DiagInfo.DiagType.ID); //0
            row["诊断名称"] = FS.HISFC.Components.HealthRecord.CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(info.DiagInfo.ICD10.Name, false); ;//1
            if (info.DiagInfo.ICD10.ID == "MS999")
            {
                row["ICD10"] = "";
            }
            else
            {
                row["ICD10"] = info.DiagInfo.ICD10.ID;//2
            }
            row["入院病情"] = diagOutStateListHelper.GetName(info.DiagOutState); //3
            row["有无手术"] = OperListHelper.GetName(info.OperationFlag);
            if (info.Is30Disease == "0")//5
            {
                row["30种疾病"] = false;
            }
            else if (info.Is30Disease == "1")
            {
                row["30种疾病"] = true;
            }

            if (info.CLPA == "0")//6
            {
                row["病理符合"] = false;
            }
            else if (info.CLPA == "1")
            {
                row["病理符合"] = true;
            }
            row["分级"] = LeveListHelper.GetName(info.LevelCode); //7
            row["分期"] = PeriorListHelper.GetName(info.PeriorCode);//8


            if (info.DubDiagFlag == "0") //9
            {
                row["是否疑诊"] = false;
            }
            else if (info.DubDiagFlag == "1")
            {
                row["是否疑诊"] = true;
            }
            //主诊断
            row["主诊断"] = info.DiagInfo.IsMain;
            row["序号"] = info.DiagInfo.HappenNo;//10
            if (info.DiagInfo.DiagDate == System.DateTime.MinValue)
            {
                row["诊断日期"] = System.DateTime.Now;
            }
            else
            {
                row["诊断日期"] = info.DiagInfo.DiagDate;//11
            }
            row["入院日期"] = patient.PVisit.InTime;//12
            row["出院日期"] = patient.PVisit.OutTime;//13
            row["诊断医师代码"] = info.DiagInfo.Doctor.ID;
            row["诊断医师"] = info.DiagInfo.Doctor.Name;
            //if (info.DiagInfo.ICDF10.ID == "MS999")
            //{
            //    row["ICD附属码"] = "";
            //}
            //else
            //{
            //    row["ICD附属码"] = info.DiagInfo.ICDF10.ID;//2
            //}
            //row["ICD附属码名称"] = FS.HISFC.Components.HealthRecord.CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(info.DiagInfo.ICDF10.Name, false); 
            //{74A9AA46-74B3-49e8-910A-54A998E428AF} 增加拟诊功能
            if (string.IsNullOrEmpty(info.DiagInfo.ICD10.ID.Trim()) && !string.IsNullOrEmpty(info.DiagInfo.ICD10.Name.Trim()))
            {
                row["是否拟诊"] = true;
            }
            else
            {
                row["是否拟诊"] = false;
            }
            if (info.DiagInfo.ICD10.ID != "MS999")
            {
                row["ICD"] = info.DiagInfo.ICD10.ID;//2
            }
            else
            {
                row["ICD"] = "";//2
            }
            row["ICD名称"] = FS.HISFC.Components.HealthRecord.CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(info.Memo,false);
            //设置fpSpread1 的属性
        }
        private void ucDiagNoseInput_Load(object sender, System.EventArgs e)
        {
            InputMap im;
            im = fpEnter1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpEnter1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpEnter1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            //定义响应按键事件
            fpEnter1.KeyEnter += new FS.FrameWork.WinForms.Controls.NeuFpEnter.keyDown(fpEnter1_KeyEnter);
            fpEnter1.SetItem += new FS.FrameWork.WinForms.Controls.NeuFpEnter.setItem(fpEnter1_SetItem);
            fpEnter1.KeyUp += new KeyEventHandler(fpEnter1_KeyUp);
            fpEnter1.ShowListWhenOfFocus = true;
            #region {6EF7D73B-4350-4790-B98C-C0BD0098516E}
            //if (!this.DesignMode)
            //{
            //    this.ucDiagnose1.Init();
            //}
            #endregion
            //this.ucDiagnose1.SelectItem +=new Common.Controls.ucDiagnose.MyDelegate(ucDiagnose1_SelectItem);
            this.ucDiagnose1.SelectItem+=new ucDiagnose.MyDelegate(ucDiagnose1_SelectItem);
            this.ucDiagnose1.Visible = false;

            LoadTipMessage();
        }

        private void LoadTipMessage()
        {
            try
            {
                if (!System.IO.File.Exists(Application.StartupPath + "\\Setting\\FirstPageSetting.xml"))
                {
                    return;
                }
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                System.Xml.XmlNode node = null;
                doc.Load(Application.StartupPath + "\\Setting\\FirstPageSetting.xml");
                node = doc.SelectSingleNode("/设置/提示1");

                if (node != null)
                {
                    this.lblTipMessage.Text = node.InnerXml;
                }

                node = doc.SelectSingleNode("/设置/提示2");

                if (node != null)
                {
                    this.lblTipMessage2.Text = node.InnerXml;
                }
            }
            catch
            {
                ;
            }
        }

        private void InitDateTable()
        {
            try
            {
                Type strType = typeof(System.String);
                Type intType = typeof(System.Int32);
                Type dtType = typeof(System.DateTime);
                Type boolType = typeof(System.Boolean);
                Type floatType = typeof(System.Single);

                dtDiagnose.Columns.AddRange(new DataColumn[]{
														   new DataColumn("诊断类别", strType),	//0
														   new DataColumn("ICD10", strType),	 //1
														   new DataColumn("诊断名称", strType),//2
														   new DataColumn("入院病情", strType),//3
														   new DataColumn("有无手术", strType),//4
														   new DataColumn("30种疾病", boolType),//5
														   new DataColumn("病理符合", boolType),//6
														   new DataColumn("分期", strType), //7
														   new DataColumn("分级", strType),//8
														   new DataColumn("是否疑诊", boolType),//9
														   new DataColumn("主诊断", boolType),//9
														   new DataColumn("序号", intType),//10
														   new DataColumn("诊断日期", dtType),//11
														   new DataColumn("入院日期", dtType),//12
														   new DataColumn("出院日期", dtType),//13
														   new DataColumn("诊断医师代码", strType),//14 
														   new DataColumn("诊断医师", strType),//15
                                                           //{74A9AA46-74B3-49e8-910A-54A998E428AF} 增加拟诊功能
                                                           new DataColumn("是否拟诊", boolType),//16
                                                           new DataColumn("ICD",strType),//17
                                                           new DataColumn("ICD名称",strType),//18
                                                           //new DataColumn("ICD附属码",strType),//19
                                                           //new DataColumn("ICD附属码名称",strType)//20
                                                           });
                //绑定数据源
                this.fpEnter1_Sheet1.DataSource = dtDiagnose;
                //设置fpSpread1 的属性
                //				if(System.IO.File.Exists(filePath))
                //				{
                //					FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpEnter1_Sheet1,filePath);
                //				}
                //				else
                //				{
                //					FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpEnter1_Sheet1,filePath);
                //				}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 设置列下拉列表
        /// </summary>
        private void initList()
        {
            try
            {
                FS.HISFC.BizLogic.HealthRecord.Diagnose da = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
                FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
                this.fpEnter1.SelectNone = true;
                //获取出院诊断类别诊断
                //				diagnoseType = da.GetDiagnoseList();
                diagnoseType = FS.HISFC.Models.HealthRecord.DiagnoseType.SpellList();
                diagnoseTypeHelper.ArrayObject = diagnoseType;
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, 0, diagnoseType);

                //分期列表
                PeriorList = con.GetList(FS.HISFC.Models.Base.EnumConstant.DIAGPERIOD);
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, 7, PeriorList);
                PeriorListHelper.ArrayObject = PeriorList;
                //手术操作类型
                OperList = con.GetList(FS.HISFC.Models.Base.EnumConstant.OPERATIONTYPE);
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, 4, OperList);
                OperListHelper.ArrayObject = OperList;

                //分级列表 
                LeveList = con.GetList(FS.HISFC.Models.Base.EnumConstant.DIAGLEVEL);
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, 8, LeveList);
                LeveListHelper.ArrayObject = LeveList;

                //入院病情
                diagOutStateList = con.GetList("CASECOMEINSTATE");
                this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, 3, diagOutStateList);
                diagOutStateListHelper.ArrayObject = diagOutStateList;
                this.fpEnter1.SetSpecalCol((int)Col.Icd10Code);

                this.fpEnter1.SetWidthAndHeight(200, 200);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 处理回车操作 ，并且取出数据
        /// </summary>
        /// <returns></returns>
        private int ProcessDept()
        {
            int CurrentRow = fpEnter1_Sheet1.ActiveRowIndex;
            if (CurrentRow < 0) return 0;

            if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.DiagKind) //诊断类型 
            {
                //获取选中的信息
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.DiagKind);
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                //2011-10-10 chengym 存在填写多个诊断打印不出来的情况，应该限制
                if (this.fpEnter1_Sheet1.ActiveRowIndex != 0)
                {
                    if (item.ID.ToString() == "1")
                    {
                        MessageBox.Show("只允许第一行填写主要诊断", "提示！");
                        fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.DiagKind);
                        return -1;
                    }
                }
                
                //诊断类别
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Icd10Name);
                //fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICDFName);
                return 0;
            }
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.OutState)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.OutState);
                //获取选中的信息
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                // 出院信息
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                fpEnter1.Focus();
                //fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.OperationFlag);
                //add by chengym
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICD);
                //fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICDFCode);
                return 0;
            }
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.OperationFlag)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.OperationFlag);
                //获取选中的信息
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                // 出院信息
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Disease);
                return 0;
            }
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.Perior)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.Perior);
                //获取选中的信息
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                //分期
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Level);
                return 0;
            }
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.Level)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)Col.Level);
                //获取选中的信息
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                //分期
                fpEnter1_Sheet1.ActiveCell.Text = item.Name;
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.DubDiag);
                return 0;
            }
            //{8BC09475-C1D9-4765-918B-299E21E04C74} 诊断录入增加医生站、门诊医生站、病案室属性
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.Icd10Code)
            {
                #region {9FFEAAA8-2387-4b90-B3BD-D2FBFDC48E95}
                //if (!this.isCas)
                //{
                #region {9F550E5B-669F-4856-BAED-94F69B729CAE}
                //增加回车选择诊断信息
                this.GetInfo();
                #endregion
                if (this.isCas)
                {
                    fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.OutState);
                }
                else
                {
                    fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.IsDraftExamine);
                }
                //fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.IsDraftExamine);
                return 0;
                //}
                #endregion
            }
            else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.ICD)
            {

                try
                {
                    FS.HISFC.Models.HealthRecord.ICD item = null;
                    //if (this.ucDiagnose1.GetItem(ref item) == -1)
                    //{
                    //    return -1;
                    //}
                    if (this.ucDiagnose1.GetItemCase(ref item) == -1)
                    {
                        return -1;
                    }
                    if (item == null) return -1;

                    string itemCode = string.Empty;

                    for (int i = 0; i < this.fpEnter1_Sheet1.Rows.Count; i++)
                    {
                        if (i == this.fpEnter1_Sheet1.ActiveRowIndex) continue;
                        itemCode = this.fpEnter1_Sheet1.Cells[i, (int)Col.ICD].Text.Trim();
                        if (!string.IsNullOrEmpty(itemCode) && itemCode == item.ID)
                        {
                            MessageBox.Show("该诊断已存在！");
                            return -1;
                        }
                    }

                    //ICD诊断名称
                    fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICD].Text = item.ID;
                    
                    //ICD诊断编码
                    fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.IcdName].Text = item.Name;
                    
                    if (this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Icd10Name].Text.Trim() == "")
                    {
                        this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Icd10Name].Text = item.Name;
                    }
                  
                    ucDiagnose1.Visible = false;
                    fpEnter1.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return 0;
            }


            //else if (fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.ICDFCode)
            //{

            //    try
            //    {
            //        FS.HISFC.Models.HealthRecord.ICD item = null;
            //        //if (this.ucDiagnose1.GetItem(ref item) == -1)
            //        //{
            //        //    return -1;
            //        //}
            //        if (this.ucDiagnose1.GetItemCase(ref item) == -1)
            //        {
            //            return -1;
            //        }
            //        if (item == null) return -1;

            //        string itemCode = string.Empty;

            //        for (int i = 0; i < this.fpEnter1_Sheet1.Rows.Count; i++)
            //        {
            //            if (i == this.fpEnter1_Sheet1.ActiveRowIndex) continue;
            //            itemCode = this.fpEnter1_Sheet1.Cells[i, (int)Col.ICDFCode].Text.Trim();
            //            if (!string.IsNullOrEmpty(itemCode) && itemCode == item.ID)
            //            {
            //                MessageBox.Show("该诊断已存在！");
            //                return -1;
            //            }
            //        }

            //        //ICD附属诊断名称
            //        fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICDFCode].Text = item.ID;
            //        //ICD附属诊断编码

            //        if (this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICDFName].Text.Trim() == "")
            //        {
            //            this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICDFName].Text = item.Name;
            //        }

            //        ucDiagnose1.Visible = false;
            //        fpEnter1.Focus();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //    return 0;
            //}

            return 0;
        }
        /// <summary>
        /// 按键响应处理
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int fpEnter1_KeyEnter(Keys key)
        {
            if (key == Keys.Enter)
            {
                //				MessageBox.Show("Enter,可以自己添加处理事件，比如跳到下一cell");
                //回车
                if (this.fpEnter1.ContainsFocus)
                {
                    int i = this.fpEnter1_Sheet1.ActiveColumnIndex;
                    //{8BC09475-C1D9-4765-918B-299E21E04C74} 诊断录入增加医生站、门诊医生站、病案室属性
                    if (i == (int)Col.DiagKind || i == (int)Col.OutState || i == (int)Col.OperationFlag || i == (int)Col.Perior || i == (int)Col.Level || i == (int)Col.Icd10Code)
                    {
                        ProcessDept();
                    }
                    else if (i == (int)Col.DubDiag)
                    {
                        fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.IsDraftExamine);
                    }
                    else if (i == (int)Col.IsDraftExamine)
                    {
                        if (fpEnter1_Sheet1.ActiveRowIndex < fpEnter1_Sheet1.Rows.Count - 1)
                        {
                            fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex + 1, 0);
                        }
                        else
                        {
                            if (this.Tag != null)
                            {
                                this.AddBlankRow(); //增加一个空白行 
                            }
                            else
                            {
                                //增加一行
                                this.AddRow();
                            }
                        }
                    }
                    else
                    {
                        if (i < (int)Col.DubDiag)
                        {
                            fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, i + 1);
                        }
                    }
                }
            }
            else if (key == Keys.Up)
            {
                //				MessageBox.Show("Up,可以自己添加处理事件，比如无下拉列表时，跳到下列，显示下拉控件时，在下拉控件上下移动");
            }
            else if (key == Keys.Down)
            {
                //				MessageBox.Show("Down，可以自己添加处理事件，比如无下拉列表时，跳到上列，显示下拉控件时，在下拉控件上下移动");
            }
            else if (key == Keys.Escape)
            {
                //				MessageBox.Show("Escape,取消列表可见");
            }
            else if (key == Keys.Add)
            {
                if (fpEnter1_Sheet1.Rows.Count == 0 || fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.DubDiag)
                {
                    AddRow();
                }
            }
            return 0;
        }
        /// <summary>
        /// 添加一行项目
        /// </summary>
        /// <returns></returns>
        public int AddRow()
        {
            try
            {
                if (fpEnter1_Sheet1.Rows.Count < 1)
                {
                    //增加一行空值
                    DataRow row = dtDiagnose.NewRow();
                    row["序号"] = 1;
                    dtDiagnose.Rows.Add(row);
                    for (int i = 0; i < fpEnter1_Sheet1.RowCount; i++)
                    {
                        fpEnter1_Sheet1.Cells[i, (int)Col.IsDraftExamine].Value = "True";
                    }
                }
                else
                {
                    //增加一行
                    int j = fpEnter1_Sheet1.Rows.Count;
                    this.fpEnter1_Sheet1.Rows.Add(j, 1);

                    fpEnter1_Sheet1.Cells[fpEnter1_Sheet1.Rows.Count - 1, (int)Col.DiagKind].Value = "其他诊断";
                    fpEnter1_Sheet1.Cells[fpEnter1_Sheet1.Rows.Count-1, (int)Col.IsDraftExamine].Value = "True";
                    //for (int i = 0; i < fpEnter1_Sheet1.Columns.Count; i++)
                    //{
                    //    if (i == 0)
                    //    {
                    //        fpEnter1_Sheet1.Cells[j, i].Value = "其他诊断";
                    //    }
                    //    else
                    //    {
                    //        fpEnter1_Sheet1.Cells[j, i].Value = fpEnter1_Sheet1.Cells[j - 1, i].Value;
                    //    }
                    //}
                }
                fpEnter1.Focus();
                fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.Rows.Count, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }
        /// <summary>
        /// 添加一行项目
        /// </summary>
        /// <returns></returns>
        public int InsertRow()
        {
            try
            {
                if (this.fpEnter1_Sheet1.RowCount == 0)
                {
                    this.AddRow();
                }
                else
                {
                    //增加一行
                    int actRow = fpEnter1_Sheet1.ActiveRowIndex + 1;
                    this.fpEnter1_Sheet1.Rows.Add(actRow, 1);
                    fpEnter1_Sheet1.Cells[actRow, (int)Col.DiagKind].Value = "其他诊断";
                    fpEnter1_Sheet1.Cells[actRow, (int)Col.IsDraftExamine].Value = "True";
                    //for (int i = 0; i < fpEnter1_Sheet1.Columns.Count; i++)
                    //{
                    //    if (i == 0)
                    //    {
                    //        fpEnter1_Sheet1.Cells[actRow, i].Value = "其他诊断";
                    //    }
                    //    else
                    //    {
                    //        fpEnter1_Sheet1.Cells[actRow, i].Value = fpEnter1_Sheet1.Cells[actRow - 1, i].Value;
                    //    }
                    //}
                    fpEnter1.Focus();
                    fpEnter1_Sheet1.SetActiveCell(actRow, 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }
        /// <summary>
        /// 设置网格的宽度 等属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem1_Click(object sender, System.EventArgs e)
        {
            Common.Controls.ucSetColumn uc = new Common.Controls.ucSetColumn();
            uc.FilePath = this.filePath;
            //uc.GoDisplay += new Common.Controls.ucSetColumn.DisplayNow(uc_GoDisplay);
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
        }
        /// <summary>
        /// 调整fpSpread1_Sheet1的宽度等 保存后触发的事件
        /// </summary>
        private void uc_GoDisplay()
        {
            //			LoadInfo(inpatientNo,operType); //重新加载数据

        }

        /// <summary>
        /// 删除当前记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem2_Click(object sender, System.EventArgs e)
        {
            if (this.fpEnter1_Sheet1.Rows.Count > 0)
            {
                //删除当前行
                this.fpEnter1_Sheet1.Rows.Remove(this.fpEnter1_Sheet1.ActiveRowIndex, 1);
            }
        }

        /// <summary>
        /// 限定格的宽度很可见性 
        /// </summary>
        private void LockFpEnter()
        {
            this.fpEnter1_Sheet1.Columns[0].Width = 59; //诊断类别
            this.fpEnter1_Sheet1.Columns[1].Width = 124;//ICD10
            this.fpEnter1_Sheet1.Columns[2].Locked = true;
            this.fpEnter1_Sheet1.Columns[2].Width = 150;//诊断名称
            this.fpEnter1_Sheet1.Columns[3].Width = 65; //入院病情
            this.fpEnter1_Sheet1.Columns[4].Width = 40; //有无手术
            this.fpEnter1_Sheet1.Columns[5].Width = 40; //30种疾病
            this.fpEnter1_Sheet1.Columns[6].Width = 40; //病理符合
            this.fpEnter1_Sheet1.Columns[7].Width = 51; //分期
            this.fpEnter1_Sheet1.Columns[8].Width = 50; //分级
            this.fpEnter1_Sheet1.Columns[9].Width = 40; //是否疑诊
            this.fpEnter1_Sheet1.Columns[10].Width = 40; //主诊断
            this.fpEnter1_Sheet1.Columns[10].Visible = false; //主诊断
            this.fpEnter1_Sheet1.Columns[11].Visible = false; //序号
            this.fpEnter1_Sheet1.Columns[12].Visible = false; //诊断日期
            this.fpEnter1_Sheet1.Columns[13].Visible = false; //入院日期
            this.fpEnter1_Sheet1.Columns[14].Visible = false; //出院日期
            this.fpEnter1_Sheet1.Columns[15].Visible = false; //诊断医师代码
            this.fpEnter1_Sheet1.Columns[16].Visible = false; //诊断医师

            //{8BC09475-C1D9-4765-918B-299E21E04C74} 诊断录入增加医生站、门诊医生站、病案室属性
            if (!isCas) 
            {

                fpEnter1_Sheet1.Columns[(int)Col.OutState].Visible = false;//入院病情
                fpEnter1_Sheet1.Columns[(int)Col.OperationFlag].Visible = false;//有无手术
                fpEnter1_Sheet1.Columns[(int)Col.Disease].Visible = false;//30种疾病
                fpEnter1_Sheet1.Columns[(int)Col.CLPa].Visible = false;//病理符合
                fpEnter1_Sheet1.Columns[(int)Col.Perior].Visible = false;//分期
                fpEnter1_Sheet1.Columns[(int)Col.Level].Visible = false;//分级
                fpEnter1_Sheet1.Columns[(int)Col.DubDiag].Visible = false;//是否疑诊
                //fpEnter1_Sheet1.Columns[(int)Col.MainDiag].Visible = true;//主诊断
            }
            else
            {
                fpEnter1_Sheet1.Columns[(int)Col.OutState].Visible = true;//入院病情
                fpEnter1_Sheet1.Columns[(int)Col.OperationFlag].Visible = true;//有无手术
                fpEnter1_Sheet1.Columns[(int)Col.Disease].Visible = true;//30种疾病
                fpEnter1_Sheet1.Columns[(int)Col.CLPa].Visible = true;//病理符合
                fpEnter1_Sheet1.Columns[(int)Col.Perior].Visible = true;//分期
                fpEnter1_Sheet1.Columns[(int)Col.Level].Visible = true;//分级
                fpEnter1_Sheet1.Columns[(int)Col.DubDiag].Visible = true;//是否疑诊
                //fpEnter1_Sheet1.Columns[(int)Col.MainDiag].Visible = false;//主诊断

            }



            //{74A9AA46-74B3-49e8-910A-54A998E428AF} 增加拟诊功能
            fpEnter1_Sheet1.Columns[(int)Col.IsDraftExamine].Visible = true;//是否拟诊
            for (int i = 0; i < fpEnter1_Sheet1.Rows.Count; i++)
            {
                if (this.fpEnter1_Sheet1.Cells[i, (int)Col.IsDraftExamine].Value == null || this.fpEnter1_Sheet1.Cells[i, (int)Col.IsDraftExamine].Value.ToString() == "False")
                {
                    this.fpEnter1_Sheet1.Cells[i, (int)Col.Icd10Code].Locked = false;
                    this.fpEnter1_Sheet1.Cells[i, (int)Col.Icd10Name].Locked = true;
                    //this.fpEnter1_Sheet1.Cells[i, (int)Col.ICDFCode].Locked = true;
                    //this.fpEnter1_Sheet1.Cells[i, (int)Col.ICDFName].Locked = true;
                }
                else
                {
                    this.fpEnter1_Sheet1.Cells[i, (int)Col.Icd10Code].Locked = true;
                    this.fpEnter1_Sheet1.Cells[i, (int)Col.Icd10Name].Locked = false;
                    this.fpEnter1_Sheet1.Cells[i, (int)Col.Icd10Code].Text = " ";
                    //this.fpEnter1_Sheet1.Cells[i, (int)Col.ICDFCode].Locked = false;
                    //this.fpEnter1_Sheet1.Cells[i, (int)Col.ICDFName].Locked = false;
                }
            }
            //add by chengym 一般医生录入描述性诊断 ，再选择ICD
            if (isCas)
            {
                fpEnter1_Sheet1.Columns[(int)Col.Icd10Code].Visible = false;//icd10编码
                fpEnter1_Sheet1.Columns[(int)Col.OperationFlag].Visible = false;//有无手术
                fpEnter1_Sheet1.Columns[(int)Col.Disease].Visible = false;//30种疾病
                fpEnter1_Sheet1.Columns[(int)Col.CLPa].Visible = false;//病理符合
                fpEnter1_Sheet1.Columns[(int)Col.Perior].Visible = false;//分期
                fpEnter1_Sheet1.Columns[(int)Col.Level].Visible = false;//分级
                fpEnter1_Sheet1.Columns[(int)Col.DubDiag].Visible = false;//是否疑诊
                fpEnter1_Sheet1.Columns[(int)Col.IsDraftExamine].Visible = false;//是否拟诊

                this.fpEnter1_Sheet1.Columns[2].Width = 320;//诊断名称
                this.fpEnter1_Sheet1.Columns[18].Width = 59;
                this.fpEnter1_Sheet1.Columns[19].Width = 220;
                this.fpEnter1_Sheet1.Columns[(int)Col.IcdName].Visible = false;

                for (int i = 0; i < fpEnter1_Sheet1.Rows.Count; i++)
                {
                    this.fpEnter1_Sheet1.Cells[i, (int)Col.IsDraftExamine].Value = "True";
                }
            }
          
        }
        private int fpEnter1_SetItem(FS.FrameWork.Models.NeuObject obj)
        {
            this.ProcessDept();
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        { 
            if (keyData == Keys.NumPad1)
            {
                //r如果当前列是checkbox类型的 点 数字1 选中状态
                int i = fpEnter1_Sheet1.ActiveColumnIndex;
                if (i == (int)Col.Disease || i == (int)Col.CLPa || i == (int)Col.DubDiag || i == (int)Col.MainDiag)
                {
                    //统计标志取反
                    if (fpEnter1_Sheet1.Cells[fpEnter1_Sheet1.ActiveRowIndex, i].Value == null)
                    {
                        fpEnter1_Sheet1.Cells[fpEnter1_Sheet1.ActiveRowIndex, i].Value = true;
                    }
                    else if (fpEnter1_Sheet1.Cells[fpEnter1_Sheet1.ActiveRowIndex, i].Value.ToString() == "False")
                    {
                        fpEnter1_Sheet1.Cells[fpEnter1_Sheet1.ActiveRowIndex, i].Value = true;
                    }
                    else
                    {
                        fpEnter1_Sheet1.Cells[fpEnter1_Sheet1.ActiveRowIndex, i].Value = false;
                    }
                }
            }
            else if (keyData.GetHashCode() == Keys.Escape.GetHashCode())
            {
                this.ucDiagnose1.Visible = false;
            }
            else if (keyData.GetHashCode() == Keys.Up.GetHashCode())
            {
                if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.Icd10Code)
                {
                    if (this.ucDiagnose1.Visible == true)
                    {
                        this.ucDiagnose1.PriorRow();
                    }
                }
                else if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.ICD)
                {
                    if (this.ucDiagnose1.Visible == true)
                    {
                        this.ucDiagnose1.PriorRow();
                    }
                }
            }
            else if (keyData.GetHashCode() == Keys.Down.GetHashCode())
            {
                if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.Icd10Code)
                {
                    if (this.ucDiagnose1.Visible == true)
                    {
                        this.ucDiagnose1.NextRow();
                    }
                }
                else if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.ICD)
                {
                    if (this.ucDiagnose1.Visible == true)
                    {
                        this.ucDiagnose1.NextRow();
                    }
                }
            }
            else if (keyData.GetHashCode() == Keys.Enter.GetHashCode())
            {
                if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.Icd10Code)
                {
                    if (this.ucDiagnose1.Visible == true)
                    {
                        this.ucDiagnose1_SelectItem(keyData);
                    }
                }
                else if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.ICD)
                {
                    //if (this.ucDiagnose1.Visible == true)
                    //{
                    //    this.ucDiagnose1_SelectItem(keyData);
                    //}
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        private void fpEnter1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            //设置fpSpread1 的属性
            if (System.IO.File.Exists(filePath))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpEnter1_Sheet1, filePath);
            }
        }
        /// <summary>
        /// 当单元格中的数据变化时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpEnter1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            //筛选数据

            try
            {
                if (e.Column == 1)
                {
                    if (this.ucDiagnose1.Visible == false)
                    {
                        this.ucDiagnose1.Visible = true;
                    }
                    string str = fpEnter1_Sheet1.ActiveCell.Text;
                    //this.ucDiagnose1.Filter(str);
                    this.ucDiagnose1.FilterCase(str);
                }
                else if (e.Column == 18)//add by chengym
                {
                    if (this.ucDiagnose1.Visible == false)
                    {
                        this.ucDiagnose1.Visible = true;
                    }
                    string str = fpEnter1_Sheet1.ActiveCell.Text;
                    //this.ucDiagnose1.Filter(str);
                    this.ucDiagnose1.FilterCase(str);
                }
                else if (e.Column == 20)//add by ren.jch
                {
                    if (this.ucDiagnose1.Visible == false)
                    {
                        this.ucDiagnose1.Visible = true;
                    }
                    string str = fpEnter1_Sheet1.ActiveCell.Text;
                    //this.ucDiagnose1.Filter(str);
                    this.ucDiagnose1.FilterCase(str);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private int GetInfo()
        {
            try
            {
                if (this.fpEnter1_Sheet1.ActiveColumnIndex == 1)
                {
                    FS.HISFC.Models.HealthRecord.ICD item = null;
                    if (this.ucDiagnose1.GetItem(ref item) == -1)
                    {
                        //MessageBox.Show("获取项目出错!","提示");
                        return -1;
                    }
                    //			this.contralActive.Text=(item as FS.HISFC.Models.HealthRecord.ICD).Name;
                    //			this.contralActive.Tag=item;
                    //			this.ucDiag1.Visible=false;
                    if (item == null) return -1;

                    string itemCode = string.Empty;

                    for (int i = 0; i < this.fpEnter1_Sheet1.Rows.Count; i++)
                    {
                        if (i == this.fpEnter1_Sheet1.ActiveRowIndex) continue;
                        itemCode = this.fpEnter1_Sheet1.Cells[i, (int)Col.Icd10Code].Text.Trim();
                        if (!string.IsNullOrEmpty(itemCode) && itemCode == item.ID)
                        {
                            MessageBox.Show("该诊断已存在！");
                            return -1;
                        }
                    }

                    //ICD诊断名称
                    fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Icd10Code].Text = item.ID;
                    //ICD诊断编码
                    fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Icd10Name].Text = item.Name;
                    ucDiagnose1.Visible = false;
                    fpEnter1.Focus();
                    //{8BC09475-C1D9-4765-918B-299E21E04C74} 诊断录入增加医生站、门诊医生站、病案室属性
                    if (this.isCas)
                    {
                        fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.OutState);
                    }
                    else
                    {
                        fpEnter1_Sheet1.SetActiveCell(fpEnter1_Sheet1.ActiveRowIndex, (int)Col.IsDraftExamine);
                    }
                }
                else if(this.fpEnter1_Sheet1.ActiveColumnIndex==18)//add chengym 2011-9-27
                {
                    FS.HISFC.Models.HealthRecord.ICD item = null;
                    if (this.ucDiagnose1.GetItemCase(ref item) == -1)
                    {
                        return -1;
                    }
                    if (item == null) return -1;

                    string itemCode = string.Empty;

                    for (int i = 0; i < this.fpEnter1_Sheet1.Rows.Count; i++)
                    {
                        if (i == this.fpEnter1_Sheet1.ActiveRowIndex) continue;
                        itemCode = this.fpEnter1_Sheet1.Cells[i, (int)Col.ICD].Text.Trim();
                        if (!string.IsNullOrEmpty(itemCode) && itemCode == item.ID)
                        {
                            MessageBox.Show("该诊断已存在！");
                            return -1;
                        }
                    }

                    //ICD诊断名称
                    fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICD].Text = item.ID;
                    //ICD诊断编码
                    fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.IcdName].Text = item.Name;
                    if (fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Icd10Name].Text.Trim() == "")
                    {
                        fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.Icd10Name].Text = item.Name;
                    }
                    ucDiagnose1.Visible = false;
                    fpEnter1.Focus();
                    fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.IcdName);
                }
                else if (this.fpEnter1_Sheet1.ActiveColumnIndex == 20)//add 
                {
                    FS.HISFC.Models.HealthRecord.ICD item = null;
                    if (this.ucDiagnose1.GetItemCase(ref item) == -1)
                    {
                        return -1;
                    }
                    if (item == null) return -1;

                    string itemCode = string.Empty;

                    for (int i = 0; i < this.fpEnter1_Sheet1.Rows.Count; i++)
                    {
                        if (i == this.fpEnter1_Sheet1.ActiveRowIndex) continue;
                        //itemCode = this.fpEnter1_Sheet1.Cells[i, (int)Col.ICDFCode].Text.Trim();
                        if (!string.IsNullOrEmpty(itemCode) && itemCode == item.ID)
                        {
                            MessageBox.Show("该诊断已存在！");
                            return -1;
                        }
                    }

                    ////ICD诊断名称
                    //fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICDFCode].Text = item.ID;
                    ////ICD诊断编码
                    ////fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICDFName].Text = item.Name;
                    //if (fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICDFName].Text.Trim() == "")
                    //{
                    //    fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.ICDFName].Text = item.Name;
                    //}
                    ucDiagnose1.Visible = false;
                    fpEnter1.Focus();
                    fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.ActiveRowIndex, (int)Col.IcdName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }
        /// <summary>
        /// 鼠标点到单元格 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpEnter1_EditModeOn(object sender, System.EventArgs e)
        {
            if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.Icd10Code)
            {
                Control _cell = fpEnter1.EditingControl;
                //设置位置
                this.ucDiagnose1.Location = new System.Drawing.Point(_cell.Location.X, _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                ucDiagnose1.BringToFront();
                //this.ucDiagnose1.Filter(fpEnter1_Sheet1.ActiveCell.Text);
                this.ucDiagnose1.FilterCase(fpEnter1_Sheet1.ActiveCell.Text.Trim());

                this.ucDiagnose1.Visible = true;
            }
            else if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.ICD)//add by chengym
            {
                Control _cell = fpEnter1.EditingControl;
                //设置位置
                this.ucDiagnose1.Location = new System.Drawing.Point(_cell.Location.X, _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                ucDiagnose1.BringToFront();
                this.ucDiagnose1.FilterCase(fpEnter1_Sheet1.ActiveCell.Text.Trim());
                //this.ucDiagnose1.Filter(fpEnter1_Sheet1.ActiveCell.Text);
                this.ucDiagnose1.Visible = true;
            }
            //else if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)Col.ICDFCode)//add by 
            //{
            //    Control _cell = fpEnter1.EditingControl;
            //    //设置位置
            //    this.ucDiagnose1.Location = new System.Drawing.Point(_cell.Location.X, _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
            //    ucDiagnose1.BringToFront();
            //    this.ucDiagnose1.FilterCase(fpEnter1_Sheet1.ActiveCell.Text.Trim());
            //    //this.ucDiagnose1.Filter(fpEnter1_Sheet1.ActiveCell.Text);
            //    this.ucDiagnose1.Visible = true;
            //}
            else
            {
                this.ucDiagnose1.Visible = false;
            }
            if (this.ucDiagnose1.Visible == true)
            {
                this.panel5.Width = 0;
            }
            else
            {
                this.panel5.Width = 186;
            }
            //显示
        }

        private int ucDiagnose1_SelectItem(Keys key)
        {
            GetInfo();
            return 0;
        }

        private void fpEnter1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.fpEnter1_Sheet1.ActiveColumnIndex == 2)//(int)Col.Icd10Code)
                {
                    GetInfo();
                }
            }
        }

        //{74A9AA46-74B3-49e8-910A-54A998E428AF} 增加拟诊功能
        private void fpEnter1_Sheet1_CellChanged(object sender, SheetViewEventArgs e)
        {
            if (e.Column == (int)Col.IsDraftExamine)
            {
                if (this.fpEnter1_Sheet1.Cells[e.Row, e.Column].Value.ToString() == "False")
                {
                    this.fpEnter1_Sheet1.Cells[e.Row, (int)Col.Icd10Code].Locked = false;
                    this.fpEnter1_Sheet1.Cells[e.Row, (int)Col.Icd10Name].Locked = true;
                    //this.fpEnter1_Sheet1.Cells[e.Row, (int)Col.ICDFCode].Locked = true;
                    //this.fpEnter1_Sheet1.Cells[e.Row, (int)Col.ICDFName].Locked = true;
                }
                else
                {
                    this.fpEnter1_Sheet1.Cells[e.Row, (int)Col.Icd10Code].Locked = true;
                    this.fpEnter1_Sheet1.Cells[e.Row, (int)Col.Icd10Name].Locked = false;
                    this.fpEnter1_Sheet1.Cells[e.Row, (int)Col.Icd10Code].Text = " ";
                    //this.fpEnter1_Sheet1.Cells[e.Row, (int)Col.ICDFName].Locked = false;
                    //this.fpEnter1_Sheet1.Cells[e.Row, (int)Col.ICDFCode].Locked = false;
                }
            }
        }

        #region 电子病历中的诊断获取 2011-9-27 chengym
        private void DiagnoseLoadInfo(string inPatient, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes frmType)
        {
            FS.HISFC.BizLogic.HealthRecord.Diagnose diagInfo = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            List<FS.FrameWork.Models.NeuObject> diagList = new List<FS.FrameWork.Models.NeuObject>();

            if (frmType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC)
            {
                diagList = diagInfo.QueryDiagnoseFromOutCaseByInpatient(inPatient);
                if (diagList != null)
                {
                    if (diagList.Count == 0)
                    {
                        this.rtxtDiag.Text = string.Empty;
                    }
                    else
                    {
                        foreach (FS.FrameWork.Models.NeuObject info in diagList)
                        {
                            this.rtxtDiag.Text = this.rtxtDiag.Text + "\n" + info.Memo.ToString();
                        }
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 拟诊设置
        /// </summary>
        /// <param name="RowID"></param>
        private void SetDoubtFarmart(int RowID)
        {
            if (!this.isDoubt)
            {
                this.fpEnter1_Sheet1.Cells[RowID, (int)Col.IsDraftExamine].Value = "False";
            }
            else
            {
                this.fpEnter1_Sheet1.Cells[RowID, (int)Col.IsDraftExamine].Value= "True";
            }
        }
        /// <summary>
        /// 增加一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAdd_Click(object sender, EventArgs e)
        {
            this.AddRow();
        }
        /// <summary>
        /// 删除当前行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDelete_Click(object sender, EventArgs e)
        {
            this.DeleteActiveRow();
        }

        /// <summary>
        /// 获取诊断界面数据
        /// chengym 2011-9-27 
        /// 原来使用DataTable的GetChanges的方式实现总有写莫名奇妙问题查不出原因（估计是修改多了，某个地方影响了）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool GetDiagnosInfo(List<FS.HISFC.Models.HealthRecord.Diagnose> list)
        {
            try
            {
                FS.HISFC.Models.HealthRecord.Diagnose info = null;
                for (int row = 0; row < this.fpEnter1_Sheet1.RowCount; row++)
                {
                    info = new FS.HISFC.Models.HealthRecord.Diagnose();
                    info.DiagInfo.Patient.ID = this.patient.ID;//住院流水号
                    info.DiagInfo.Patient.PID.CardNO = this.patient.PID.CardNO;//卡号
                    //诊断类别
                    info.DiagInfo.DiagType.ID = diagnoseTypeHelper.GetID(this.fpEnter1_Sheet1.Cells[row, (int)Col.DiagKind].Text);
                    //出院诊断的明细类别
                    if (info.DiagInfo.DiagType.ID == "1") //将主诊断设置成 
                    {
                        info.DiagInfo.IsMain = true;
                    }
                    else
                    {
                        info.DiagInfo.IsMain = false;
                    }
                    if (this.fpEnter1_Sheet1.Cells[row, (int)Col.ICD].Text != "")//ICD编码
                    {
                        info.DiagInfo.ICD10.ID = this.fpEnter1_Sheet1.Cells[row, (int)Col.ICD].Text;
                    }
                    else
                    {
                        info.DiagInfo.ICD10.ID = "MS999";
                    }
                    info.DiagInfo.ICD10.Name =FS.HISFC.Components.HealthRecord.CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(this.fpEnter1_Sheet1.Cells[row, (int)Col.Icd10Name].Text,true);//ICD名称
                    //if (this.fpEnter1_Sheet1.Cells[row, (int)Col.ICDFCode].Text != "")//附属ICD编码
                    //{
                    //    info.DiagInfo.ICDF10.ID = this.fpEnter1_Sheet1.Cells[row, (int)Col.ICDFCode].Text;
                    //}
                    //else
                    //{
                    //    info.DiagInfo.ICDF10.ID = "MS999";
                    //}
                    //info.DiagInfo.ICDF10.Name = FS.HISFC.Components.HealthRecord.CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(this.fpEnter1_Sheet1.Cells[row, (int)Col.ICDFName].Text, true);//附属ICD名称
                    
                    if (this.fpEnter1_Sheet1.Cells[row, (int)Col.OutState].Text != "")//出院转归情况
                    {
                        info.DiagOutState = diagOutStateListHelper.GetID(this.fpEnter1_Sheet1.Cells[row, (int)Col.OutState].Text); //3
                    }

                    info.OperationFlag = "";//有无手术
                    info.Is30Disease = "0";//30种疾病
                    info.CLPA = "0";//病理符合
                    info.LevelCode = ""; //分级
                    info.PeriorCode = "";//分期
                    info.DubDiagFlag = "0";//是否疑诊
                    info.DiagInfo.HappenNo = row;//10
                    info.DiagInfo.DiagDate = patient.PVisit.InTime;//11诊断日期  
                    info.Pvisit.InTime = patient.PVisit.InTime;//12入院日期
                    info.Pvisit.OutTime = patient.PVisit.OutTime;//13出院日期
                    info.OperType = "1";//医生站录入

                    info.PerssonType = FS.HISFC.Models.Base.ServiceTypes.I;
                    info.IsValid = true;

                    FS.HISFC.BizLogic.HealthRecord.Diagnose dia = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

                    info.DiagInfo.Doctor.ID = dia.Operator.ID;//诊断医师代码
                    info.DiagInfo.Doctor.Name = dia.Operator.Name;//诊断医师
                    info.DiagInfo.Dept.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                    info.DiagInfo.IsValid = true;
                    info.Pvisit.Memo = "0";//是否拟诊
                    info.Memo = FS.HISFC.Components.HealthRecord.CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(this.fpEnter1_Sheet1.Cells[row, (int)Col.IcdName].Text,true); ;//ICD诊断名称
                    info.PerssonType = FS.HISFC.Models.Base.ServiceTypes.I;
                    info.IsValid = true;
                    list.Add(info);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void btInsert_Click(object sender, EventArgs e)
        {
            this.InsertRow();
        }
    }
}
