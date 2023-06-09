using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FarPoint.Win.Spread;
using FS.HISFC.Models.HealthRecord.EnumServer;
namespace FS.HISFC.Components.HealthRecord.CaseFirstPage
{
    /// <summary>
    /// ucOperation<br></br>
    /// [功能描述: 病案手术信息录入]<br></br>
    /// [创 建 者: 张俊义]<br></br>
    /// [创建时间: 2007-04-20]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucMetCasOperation : UserControl
    {
        public ucMetCasOperation()
        {
            InitializeComponent();
        }

        #region 控件设计思想
        //调用LoadInfo ，查询手术信息，填充数据 
        //调用GetList 获取数据 在外部保存
        //提供 Reset函数，当外部保存完毕后 清空所有的数据  
        #endregion

        #region   全局变量
        //配置文件路径 
        private string filePath = Application.StartupPath + "\\profile\\OperationCard.xml";
        //如果是 "DOC" 查询的是医生站录入的手术信息 如果输入的是“CAS”，则查询病案师录入的手术信息
        private FS.HISFC.Models.HealthRecord.EnumServer.frmTypes operType;
        //产科分娩婴儿记录表 
        private DataTable dtOperation;
        private DataView dvOperation;
        /// <summary>
        ///ICD 诊断信息 列表
        /// </summary>
        private FS.FrameWork.WinForms.Controls.PopUpListBox ICDType = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        //切口类型
        private FS.FrameWork.WinForms.Controls.PopUpListBox NickType = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        private FS.FrameWork.Public.ObjectHelper NickTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        //愈合类型
        private FS.FrameWork.WinForms.Controls.PopUpListBox CicaType = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        private FS.FrameWork.Public.ObjectHelper CicaTypeHelper = new FS.FrameWork.Public.ObjectHelper();
        //麻醉方式列表
        private FS.FrameWork.WinForms.Controls.PopUpListBox NarcType = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        private FS.FrameWork.Public.ObjectHelper NarcTypeHelper = new FS.FrameWork.Public.ObjectHelper();
        //手术麻醉医生列表
        private FS.FrameWork.WinForms.Controls.PopUpListBox DoctorType = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        private FS.FrameWork.Public.ObjectHelper DoctorHelper = new FS.FrameWork.Public.ObjectHelper();
        //手术级别列表
        private FS.FrameWork.WinForms.Controls.PopUpListBox LevelType = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        private FS.FrameWork.Public.ObjectHelper LevelHelper = new FS.FrameWork.Public.ObjectHelper();
        //手术级别列表
        private FS.FrameWork.WinForms.Controls.PopUpListBox selectOpDateType = new FS.FrameWork.WinForms.Controls.PopUpListBox();
        private FS.FrameWork.Public.ObjectHelper selectOpDateHelper = new FS.FrameWork.Public.ObjectHelper();


        private FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();

        private int operationType = 0;

        //手术ICD码
        static ArrayList icdList = new ArrayList();
        #endregion

        #region 属性
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

        private string isHaveOps=string.Empty;
        /// <summary>
        /// 是否选择手术
        /// </summary>
        public string IsHavedOps
        {
            get
            {
                if (this.cmbIsHaveOps.Tag== null)
                {
                    this.isHaveOps = string.Empty;
                }
                else
                {
                    this.isHaveOps = this.cmbIsHaveOps.Tag.ToString();
                }
                return this.isHaveOps;
            }
            set
            {
                this.isHaveOps = value;
                this.cmbIsHaveOps.Tag = this.isHaveOps;
            }
        }
        #endregion

        #region 函数
        private void SetAllListUnVisiable()
        {
            NickType.Visible = false;
            NarcType.Visible = false;
            CicaType.Visible = false;
            DoctorType.Visible = false;
            ICDType.Visible = false;
            LevelType.Visible = false;
            selectOpDateType.Visible = false;
        }
        /// <summary>
        /// 设置活动单元格
        /// </summary>
        public void SetActiveCells()
        {
            try
            {
                this.fpSpread1_Sheet1.SetActiveCell(0, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 限定格的宽度很可见性 
        /// </summary>
        private void LockFpEnter()
        {
            this.fpSpread1_Sheet1.Columns[0].Width = 75; //手术及操作编码
            this.fpSpread1_Sheet1.Columns[1].Width = 80; //手术及操作日期
            this.fpSpread1_Sheet1.Columns[2].Width = 40; //手术级别
            this.fpSpread1_Sheet1.Columns[3].Width = 190;//手术及操作名称
            this.fpSpread1_Sheet1.Columns[4].Width = 45;//术者
            this.fpSpread1_Sheet1.Columns[5].Width = 45; //I 助
            this.fpSpread1_Sheet1.Columns[6].Width = 45; //II 助
            this.fpSpread1_Sheet1.Columns[7].Width = 35; //切口类型
            this.fpSpread1_Sheet1.Columns[8].Width = 35; //愈合等级
            this.fpSpread1_Sheet1.Columns[9].Width = 50; //麻醉方式1
            this.fpSpread1_Sheet1.Columns[10].Width = 50; //麻醉方式2
            this.fpSpread1_Sheet1.Columns[11].Width = 45; //麻醉医师
            this.fpSpread1_Sheet1.Columns[12].Width = 40; //统计
            this.fpSpread1_Sheet1.Columns[12].Locked = true;//统计
            this.fpSpread1_Sheet1.Columns[12].Visible = false;
            this.fpSpread1_Sheet1.Columns[13].Visible = false; //术者编码
            this.fpSpread1_Sheet1.Columns[14].Visible = false; //助手编码1
            this.fpSpread1_Sheet1.Columns[15].Visible = false; //助手编码2
            this.fpSpread1_Sheet1.Columns[16].Visible = false; //麻醉医师编码
            this.fpSpread1_Sheet1.Columns[17].Visible = false; //发生序号
            this.fpSpread1_Sheet1.Columns[18].Visible = false; //手术序列号
            this.fpSpread1_Sheet1.Columns[19].Width = 40; //择期
        }
        /// <summary>
        /// 清空原有的数据
        /// </summary>
        /// <returns></returns>
        public int ClearInfo()
        {
            if (this.dtOperation != null)
            {
                this.dtOperation.Clear();
                LockFpEnter();
            }
            else
            {
                MessageBox.Show("手术表为null");
            }
            return 1;
        }
        /// <summary>
        /// 设置fp模式
        /// </summary>
        /// <param name="type">属性bool值</param>
        /// <param name="editType">编辑窗口的类型</param>
        /// <returns></returns>
        public int SetReadOnly(bool type, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes editType)
        {
            if (type)
            {
                this.btAdd.Enabled = !type;
                this.btDelete.Enabled = !type;
                if (editType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
                {
                    this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.Normal;

                    this.fpSpread1_Sheet1.Columns[1].Locked = true; //手术及操作日期
                    this.fpSpread1_Sheet1.Columns[2].Locked = true; //手术级别
                    this.fpSpread1_Sheet1.Columns[3].Locked = true;//手术及操作名称
                    this.fpSpread1_Sheet1.Columns[4].Locked = true;//术者
                    this.fpSpread1_Sheet1.Columns[5].Locked = true; //I 助
                    this.fpSpread1_Sheet1.Columns[6].Locked = true; //II 助
                    this.fpSpread1_Sheet1.Columns[7].Locked = true; //切口类型
                    this.fpSpread1_Sheet1.Columns[8].Locked = true; //愈合等级
                    this.fpSpread1_Sheet1.Columns[9].Locked = true; //麻醉方式1
                    this.fpSpread1_Sheet1.Columns[10].Locked = true; //麻醉方式2
                    this.fpSpread1_Sheet1.Columns[11].Locked = true; //麻醉医师
                    this.fpSpread1_Sheet1.Columns[19].Locked = true; //择期
                }
                else
                {
                    this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
                }
            }
            else
            {
                this.btAdd.Enabled = !type;
                this.btDelete.Enabled = !type;
                this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.Normal;
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
            foreach (FS.HISFC.Models.HealthRecord.OperationDetail obj in list)
            {
                if (obj.InpatientNO == "" || obj.InpatientNO == null)
                {
                    MessageBox.Show("住院流水号不能为空");
                    return -1;
                }
                if (obj.OperationInfo.ID == "" || obj.OperationInfo.Name == "")
                {
                    MessageBox.Show("手术信息不能为空");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.InpatientNO , 14))
                {
                    MessageBox.Show("住院流水号过长");
                    return -1;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(obj.HappenNO , 2))
                {
                    MessageBox.Show("发生序号过长");
                    return -1;
                }
                if (obj.OperType == "" || obj.OperType == null)
                {
                    MessageBox.Show("类别不能为空");
                    return -1;
                }
                if (obj.OperType.Length > 1)
                {
                    MessageBox.Show("类别编码过长");
                    return -1;
                }
                if (obj.OperationDate.Date < this.patient.PVisit.InTime.Date)
                {
                    MessageBox.Show("手术及操作日期不能小于入院日期！");
                    return -1;
                }
                if (this.patient.PVisit.InState.ID.ToString() != "I" && obj.OperationDate.Date > this.patient.PVisit.OutTime.Date)
                {
                    MessageBox.Show("手术及操作日期不能大于出院日期！");
                    return -1;
                }
            }
            return 0;
        }
        /// <summary>
        /// 删除当前行 
        /// </summary>
        /// <returns></returns>
        public int DeleteActiveRow()
        {
            if (this.operationType == 1) //自动加载项目不能删除
            {
                return -1;
            }
            SetAllListUnVisiable();
            this.fpSpread1.EditModePermanent = false;
            this.fpSpread1.EditModeReplace = false;
            if (fpSpread1_Sheet1.Rows.Count > 0)
            {
                this.fpSpread1_Sheet1.Rows.Remove(fpSpread1_Sheet1.ActiveRowIndex, 1);
            }
            if (fpSpread1_Sheet1.Rows.Count == 0)
            {
                ICDType.Visible = false;
                NickType.Visible = false;
                CicaType.Visible = false;
                NarcType.Visible = false;
                DoctorType.Visible = false;
                LevelType.Visible = false;
                selectOpDateType.Visible = false;
            }
            this.fpSpread1.EditModePermanent = true;
            this.fpSpread1.EditModeReplace = true;
            return 1;
        }
        /// <summary>
        /// 删除空白的行
        /// </summary>
        /// <returns></returns>
        public int deleteRow()
        {
            SetAllListUnVisiable();
            this.fpSpread1.EditModePermanent = false;
            this.fpSpread1.EditModeReplace = false;
            if (fpSpread1_Sheet1.Rows.Count == 1)
            {
                //第一行手术及操作名称为空 
                if (fpSpread1_Sheet1.Cells[0, 3].Text == "")
                {
                    fpSpread1_Sheet1.Rows.Remove(0, 1);
                }
            }
            this.fpSpread1.EditModePermanent = true;
            this.fpSpread1.EditModeReplace = true;
            return 1;
        }
        /// <summary>
        /// 保存对表做的所有修改
        /// </summary>
        /// <returns></returns>
        public int fpEnterSaveChanges()
        {
            try
            {
                this.dtOperation.AcceptChanges();
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
        /// 将保存完的数据回写到表中
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int fpEnterSaveChanges(ArrayList list)
        {
            AddInfoToTable(list);
            dtOperation.AcceptChanges();
            LockFpEnter();
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int AddInfoToTable(ArrayList list)
        {
            if (this.dtOperation != null)
            {
                this.dtOperation.Clear();
                this.dtOperation.AcceptChanges();
            }
            if (list != null)
            {
                //循环插入数据
                foreach (FS.HISFC.Models.HealthRecord.OperationDetail info in list)
                {
                    DataRow row = dtOperation.NewRow();
                    SetRow(row, info);
                    dtOperation.Rows.Add(row);
                }
            }
            else
            {
                return -1;
            }
            //更改标志
            if ((this.operType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC && this.patient.CaseState == "2") || (this.operType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS && this.patient.CaseState == "3"))
            {
                //清空表的标志位
                dtOperation.AcceptChanges();
            }

            //			if(System.IO.File.Exists(filePath))
            //			{
            //				FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1,filePath);
            //			}
            LockFpEnter();
            return 0;
        }
        /// <summary>
        /// 返回当前得行数
        /// </summary>
        /// <returns></returns>
        public int GetfpSpread1RowCount()
        {
            return this.fpSpread1_Sheet1.Rows.Count;
        }
        /// <summary>
        /// 添加一行项目
        /// </summary>
        /// <returns></returns>
        public int AddRow()
        {
            //			DialogResult result = MessageBox.Show("是否要增加一行","提示",MessageBoxButtons.YesNo);
            //			if(result == DialogResult.No)
            //			{
            //				return 0 ;
            //			}
            if (fpSpread1_Sheet1.Rows.Count < 1)
            {
                //增加一行空值
                DataRow row = dtOperation.NewRow();
                dtOperation.Rows.Add(row);
                //fpSpread1_Sheet1.Cells[0, 1].Value = System.DateTime.Now;
            }
            else if (fpSpread1_Sheet1.ActiveRowIndex == fpSpread1_Sheet1.Rows.Count - 1)
            {
                //增加一行
                int row = fpSpread1_Sheet1.ActiveRowIndex;
                int col = fpSpread1_Sheet1.Columns.Count;
                fpSpread1_Sheet1.Rows.Add(fpSpread1_Sheet1.Rows.Count, 1);
                for (int i = 0; i < col; i++)
                {
                    if (i != 0 || i != 3)
                    {
                        fpSpread1_Sheet1.Cells[row + 1, i].Value = fpSpread1_Sheet1.Cells[row, i].Value;
                    }
                }
            }
            fpSpread1.Focus();
            fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.Rows.Count, 0);
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
                if (this.fpSpread1_Sheet1.RowCount == 0)
                {
                    this.AddRow();
                }
                else
                {
                    //增加一行
                    int actRow = fpSpread1_Sheet1.ActiveRowIndex + 1;
                    this.fpSpread1_Sheet1.Rows.Add(actRow, 1);
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
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(actRow, 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }
        /// <summary>
        /// 初始化 变量
        /// </summary>
        public void InitInfo()
        {
            try
            {
                InputMap im;
                im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

                fpSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;



                InitDataTable();
                //切口类型
                IniNickType();
                //愈合类型
                IniCicaType();
                //麻醉方式
                InitNarcList();
                //医生列表
                InitDoctorList();
                //手术级别
                InitOpLevelType();
                //择期
                InitSelectOpDateType();
                //选择是否有手术 2012-9-19
                InitIsHavedOPSList();
                //InitControlParam(); //自动获取手术信息控制参数 

                if (this.operationType == 1)
                {
                    this.menuItem2.Visible = false;
                }
                fpSpread1.EditModePermanent = true;
                fpSpread1.EditModeReplace = true;
                fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.Normal;
                LockFpEnter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 初始化控制参数
        /// </summary>
        private void InitControlParam()
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            operationType = ctrlParamIntegrate.GetControlParam<int>("CASE02", true, 1);
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        public void LoadInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes Type)
        {
            if (dtOperation == null)
            {
                return;
            }
            if (patientInfo == null)
            {
                return;
            }
            //保存病人信息
            patient = patientInfo;
            //赋值操作类型
            operType = Type;
            FS.HISFC.BizLogic.HealthRecord.Operation op = new FS.HISFC.BizLogic.HealthRecord.Operation();
            if (patient.ID == "")
            {
                return;
            }
            //查询符合条件的数据

            ArrayList list = op.QueryOperation(operType, patient.ID);

            if (list == null)
            {
                MessageBox.Show("查询手术信息出错!");
                return;
            }
            if (list.Count == 0)
            {
                if (operType == FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.CAS)
                {
                    list = op.QueryOperation(FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC, patient.ID);
                }
            }
            this.AddInfoToTable(list);

            //从手术系统中检索病案信息
            list = op.QueryOperation(patient.ID); 

            if (list != null)
            {
                //循环插入数据
                foreach (FS.HISFC.Models.HealthRecord.OperationDetail info in list)
                {
                    DataRow row = dtOperation.NewRow();
                    SetRow(row, info);
                    dtOperation.Rows.Add(row);
                }
            }

            LockFpEnter();
        }

        /// <summary>
        /// 获取相关的数据
        /// creator:zhangjunyi@FS.com
        /// </summary>
        /// <param name="str"> “A”增加 “M” 修改 “D”删除</param>
        /// <returns>失败返回 false </returns>
        public bool GetList(string str, ArrayList list)
        {
            try
            {
                if (dtOperation == null)
                {
                    list = null;
                    return false;
                }
                this.fpSpread1.StopCellEditing();
                foreach (DataRow dr in this.dtOperation.Rows)
                {
                    dr.EndEdit();
                }
                switch (str)
                {
                    case "A":
                        //获取新增加的数据
                        DataTable AddTable = dtOperation.GetChanges(DataRowState.Added);
                        //提取数据
                        GetChange(AddTable, list);
                        break;
                    case "M":
                        //获取修改过的数据
                        DataTable ModTable = dtOperation.GetChanges(DataRowState.Modified);
                        //提取数据
                        GetChange(ModTable, list);
                        break;
                    case "D":
                        //获取修改过的数据
                        DataTable DelTable = dtOperation.GetChanges(DataRowState.Deleted);
                        if (DelTable != null)
                        {
                            DelTable.RejectChanges();
                        }
                        //提取数据
                        GetChange(DelTable, list);
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                list = null;
                return false;
            }
        }

        /// <summary>
        /// 获取修改过的数据 
        /// </summary>
        /// <param name="table">要提取数据的Table</param>
        /// <param name="list"> 输出的数组</param>
        /// <returns>失败返回false ,且数组返回null 成功返回 null</returns>
        private bool GetChange(DataTable table, ArrayList list)
        {
            try
            {
                if (table == null)
                {
                    return false;
                }
                FS.HISFC.Models.HealthRecord.OperationDetail bb;
                foreach (DataRow row in table.Rows)
                {
                    bb = new FS.HISFC.Models.HealthRecord.OperationDetail();

                    bb.OperType = "1";
                    bb.InpatientNO = patient.ID;
                    bb.OperationInfo.ID = row["手术及操作编码"].ToString();
                    bb.OperationDate = FS.FrameWork.Function.NConvert.ToDateTime(row["手术及操作日期"]);
                    bb.FourDoctInfo.Name = row["手术级别"].ToString();
                    bb.OperationInfo.Name =FS.HISFC.Components.HealthRecord.CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(row["手术及操作名称"].ToString(),true);
                    bb.FirDoctInfo.Name = row["术者"].ToString();
                    bb.SecDoctInfo.Name = row["I 助"].ToString();
                    bb.ThrDoctInfo.Name = row["II 助"].ToString();
                    if (row["麻醉方式1"].ToString() != "")
                    {
                        bb.MarcKind = NarcTypeHelper.GetID(row["麻醉方式1"].ToString());
                    }
                    if (row["麻醉方式2"].ToString() != "")
                    {
                        bb.OpbOpa = NarcTypeHelper.GetID(row["麻醉方式2"].ToString());
                    }
                    bb.NarcDoctInfo.Name = row["麻醉医师"].ToString();

                    if (row["切口"].ToString() != "")
                    {
                        bb.NickKind = NickTypeHelper.GetID(row["切口"].ToString());
                    }
                    if (row["愈合"].ToString() != "")
                    {
                        bb.CicaKind = CicaTypeHelper.GetID(row["愈合"].ToString());
                    }
                    if (row["统计"] != DBNull.Value)
                    {
                        if (Convert.ToBoolean(row["统计"]))
                        {
                            bb.StatFlag = "0";
                        }
                        else
                        {
                            bb.StatFlag = "1";
                        }
                    }
                    else
                    {
                        bb.StatFlag = "1";
                    }
                    bb.FirDoctInfo.ID = row["术者编码"].ToString();
                    bb.SecDoctInfo.ID = row["助手编码1"].ToString();
                    bb.ThrDoctInfo.ID = row["助手编码2"].ToString();
                    bb.NarcDoctInfo.ID = row["麻醉医师编码"].ToString();
                    bb.HappenNO = row["发生序号"].ToString();
                    bb.OperationNO = row["手术序列号"].ToString();
                    bb.OperationKind = selectOpDateHelper.GetID(row["择期"].ToString());
                    bb.OutDate = this.patient.PVisit.OutTime;//出院日期
                    bb.InDate = patient.PVisit.InTime; //入院日期 
                    bb.DeatDate = patient.DeathTime; //死亡时间 
                    bb.OperationDeptInfo.ID = ""; //手术科室
                    bb.OutDeptInfo.ID = patient.PVisit.PatientLocation.ID; //出院科室
                    list.Add(bb);
                }
                return true;
            }
            catch (Exception ex)
            {
                list = null;
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 初始化table 
        /// </summary>
        /// <returns></returns>
        private bool InitDataTable()
        {
            try
            {
                dtOperation = new DataTable("手术信息记录表");
                dvOperation = new DataView(dtOperation);
                Type strType = typeof(System.String);
                Type intType = typeof(System.Int32);
                Type dtType = typeof(System.DateTime);
                Type boolType = typeof(System.Boolean);
                Type floatType = typeof(System.Single);

                dtOperation.Columns.AddRange(new DataColumn[]{
                                                                 new DataColumn("手术及操作编码", strType),//0
																 new DataColumn("手术及操作日期", dtType),  //1
																 new DataColumn("手术级别", strType),//2                                                                
																 new DataColumn("手术及操作名称", strType), //3
																 new DataColumn("术者", strType),//4
																 new DataColumn("I 助", strType),//5
																 new DataColumn("II 助", strType),//6
                                                                 new DataColumn("切口", strType),//7
																 new DataColumn("愈合", strType),//8
                                                                 new DataColumn("麻醉方式1",strType),//9
                                                                 new DataColumn("麻醉方式2",strType),//10
																 new DataColumn("麻醉医师", strType),//11
																 new DataColumn("统计", boolType),//12
																 new DataColumn("术者编码", strType),//13
																 new DataColumn("助手编码1", strType),//14
																 new DataColumn("助手编码2", strType),//15
																 new DataColumn("麻醉医师编码", strType),//16
																 new DataColumn("发生序号", strType),//17
                                                                 new DataColumn("手术序列号", strType),//18
                                                                 new DataColumn("择期", strType)//19
                                                                 });

                //				//设置主键为序号列
                //				CreateKeys(dtOperation);

                this.fpSpread1.DataSource = dvOperation;
                fpSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
                //设置fpSpread1 的属性
                //				if(System.IO.File.Exists(filePath))
                //				{
                //					FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1_Sheet1,filePath);
                //				}
                //				else
                //				{
                //					FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1,filePath);
                //				}
                return true;
            }
            catch { return false; }
        }
        /// <summary>
        /// 将实体中的值赋值到row中
        /// </summary>
        /// <param name="row">传入的row</param>
        /// <param name="info">传入的实体</param>
        private void SetRow(DataRow row, FS.HISFC.Models.HealthRecord.OperationDetail info)
        {
            if (info.OperationInfo.ID != "MS999")
            {
                row["手术及操作编码"] = info.OperationInfo.ID;
            }
            row["手术及操作日期"] = info.OperationDate;
            row["手术级别"] = LevelHelper.GetName(info.FourDoctInfo.Name);
            row["手术及操作名称"] =FS.HISFC.Components.HealthRecord.CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(info.OperationInfo.Name,false);
            row["术者"] = info.FirDoctInfo.Name;
            row["I 助"] = info.SecDoctInfo.Name;
            row["II 助"] = info.ThrDoctInfo.Name;
            if (info.NickKind != "")
            {
                row["切口"] = NickTypeHelper.GetName(info.NickKind);
            }
            if (info.CicaKind != "")
            {
                row["愈合"] = CicaTypeHelper.GetName(info.CicaKind);
            }
            if (info.MarcKind != "")
            {
                row["麻醉方式1"] = NarcTypeHelper.GetName(info.MarcKind);
            }
            if (info.OpbOpa != "")
            {
                row["麻醉方式2"] = NarcTypeHelper.GetName(info.OpbOpa);
            }
            row["麻醉医师"] = info.NarcDoctInfo.Name;
            if (info.StatFlag == "0")
            {
                row["统计"] = true;
            }
            else
            {
                row["统计"] = false;
            }
            row["术者编码"] = info.FirDoctInfo.ID;
            row["助手编码1"] = info.SecDoctInfo.ID;
            row["助手编码2"] = info.ThrDoctInfo.ID;
            row["发生序号"] = info.HappenNO;
            row["手术序列号"] = info.OperationNO;
            row["择期"] = selectOpDateHelper.GetName(info.OperationKind);
        }


        private void ucOperation_Load(object sender, System.EventArgs e)
        {

        }

        /// <summary>
        /// 初始化 医生列表和麻醉医师列表
        /// </summary>
        private void InitDoctorList()
        {
            FS.HISFC.BizLogic.Manager.Person per = new FS.HISFC.BizLogic.Manager.Person();
            ArrayList list = new ArrayList();
            //获取手术/麻醉医生列表

            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //从常数表中获取是否需要手术有无选择
            ArrayList perList = con.GetList("CASENEEDALLPERSON");// con.GetList("ANESTYPE");
            if (perList != null && perList.Count > 0)
            {
                list = per.GetEmployeeAll();
            }
            else
            {
                list = per.GetEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            }
            if (list != null)
            {
                DoctorType.AddItems(list);
                DoctorHelper.ArrayObject = list;
            }
            //加载 listBox
            Controls.Add(DoctorType);
            //隐藏
            DoctorType.Hide();
            //设置边框
            DoctorType.BorderStyle = BorderStyle.Fixed3D;
            DoctorType.BringToFront();
            DoctorType.SelectNone = true;
            //				lbDept.Font=new System.Drawing.Font("宋体", 12F);
            //定义listBox的单击事件
            DoctorType.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(diagType_SelectItem);
        }

        /// <summary>
        /// 初始化ICD手术列表
        /// </summary>
        public void InitICDList()
        {
            //ICD手术诊断
            FS.HISFC.BizLogic.HealthRecord.ICD icd = new FS.HISFC.BizLogic.HealthRecord.ICD();
            if (icdList == null || icdList.Count == 0)
            {
                icdList = icd.Query(ICDTypes.ICDOperation, QueryTypes.Valid);
            }
            //			FS.HISFC.BizProcess.Fee.Item item = new FS.HISFC.BizProcess.Fee.Item();
            //			ArrayList  icdList = item.GetOperationItemList();
            //加载列表
            if (icdList != null)
            {

                ICDType.AddItems(icdList);
            }
            //加载 listBox
            Controls.Add(ICDType);
            //隐藏
            ICDType.Hide();
            //设置边框
            ICDType.BorderStyle = BorderStyle.Fixed3D;
            ICDType.BringToFront();
            ICDType.SelectNone = true;
            //定义listBox的单击事件
            ICDType.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(diagType_SelectItem);
        }
        /// <summary>
        /// 单击选择事件 
        /// </summary>
        /// <param name="key"></param>
        /// <returns> 成功返回 0</returns>
        private int diagType_SelectItem(Keys key)
        {
            ProcessDept();
            return 0;
        }
        /// <summary>
        /// 初始化择期手术列表
        /// </summary>
        private void InitSelectOpDateType()
        {
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //从常数表中获取择期类型
            ArrayList list = con.GetList("CASESELECTOPDATE");
            if (list != null)
            {
                selectOpDateType.AddItems(list);
                selectOpDateHelper.ArrayObject = list;
            }
            //加载 listBox
            Controls.Add(selectOpDateType);
            //隐藏
            selectOpDateType.Hide();
            //设置边框
            selectOpDateType.BorderStyle = BorderStyle.Fixed3D;
            selectOpDateType.BringToFront();
            selectOpDateType.SelectNone = true;
            //				lbDept.Font=new System.Drawing.Font("宋体", 12F);
            //定义listBox的单击事件
            selectOpDateType.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(diagType_SelectItem);
        }
        /// <summary>
        /// 初始化手术级别列表
        /// </summary>
        private void InitOpLevelType()
        {
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //从常数表中获取手术级别类型
            ArrayList list = con.GetList("CASELEVEL");
            if (list != null)
            {
                LevelType.AddItems(list);
                LevelHelper.ArrayObject = list;
            }
            //加载 listBox
            Controls.Add(LevelType);
            //隐藏
            LevelType.Hide();
            //设置边框
            LevelType.BorderStyle = BorderStyle.Fixed3D;
            LevelType.BringToFront();
            LevelType.SelectNone = true;
            //				lbDept.Font=new System.Drawing.Font("宋体", 12F);
            //定义listBox的单击事件
            LevelType.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(diagType_SelectItem);
        }
        /// <summary>
        /// 麻醉方式列表
        /// </summary>
        private void InitNarcList()
        {
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //从常数表中获取麻醉类型
            ArrayList list = con.GetList("ANESTYPE"); //con.GetList("CASEANESTYPE");
            if (list != null)
            {
                //填充下拉框
                NarcType.AddItems(list);
                NarcTypeHelper.ArrayObject = list;
            }
            //加载 listBox
            Controls.Add(NarcType);
            //隐藏
            NarcType.Hide();
            //设置边框
            NarcType.BorderStyle = BorderStyle.Fixed3D;
            NarcType.BringToFront();
            NarcType.SelectNone = true;
            //				lbDept.Font=new System.Drawing.Font("宋体", 12F);
            //定义listBox的单击事件
            NarcType.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(diagType_SelectItem);
        }
        /// <summary>
        /// 初始化切口列表
        /// </summary>
        private void IniNickType()
        {
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //从常数表中获取切口类型
            ArrayList list = con.GetList("INCITYPE");
            if (list != null)
            {
                NickType.AddItems(list);
                NickTypeHelper.ArrayObject = list;
            }
            //加载 listBox
            Controls.Add(NickType);
            //隐藏
            NickType.Hide();
            //设置边框
            NickType.BorderStyle = BorderStyle.Fixed3D;
            NickType.BringToFront();
            NickType.SelectNone = true;
            //				lbDept.Font=new System.Drawing.Font("宋体", 12F);
            //定义listBox的单击事件
            NickType.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(diagType_SelectItem);
        }
        /// <summary>
        /// 初始化愈合列表
        /// </summary>
        private void IniCicaType()
        {
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //从常数表中获取愈合类型
            ArrayList list = con.GetList("CICATYPE");
            if (list != null)
            {
                CicaType.AddItems(list);
                CicaTypeHelper.ArrayObject = list;
            }
            //加载 listBox
            Controls.Add(CicaType);
            //隐藏
            CicaType.Hide();
            //设置边框
            CicaType.BorderStyle = BorderStyle.Fixed3D;
            CicaType.BringToFront();
            CicaType.SelectNone = true;
            //				lbDept.Font=new System.Drawing.Font("宋体", 12F);
            //定义listBox的单击事件
            CicaType.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(diagType_SelectItem);
        }
        /// <summary>
        /// 有无手术选择
        /// </summary>
        private void InitIsHavedOPSList()
        {
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //从常数表中获取是否需要手术有无选择
            ArrayList list = con.GetList("CASEHAVEDOPS");// con.GetList("ANESTYPE");
            if (list != null && list.Count > 0)
            {
                this.label1.Visible = true;
                this.cmbIsHaveOps.Visible = true;
            }
            else
            {
                this.label1.Visible = false;
                this.cmbIsHaveOps.Visible = false;
            }
            ArrayList Havedlist = con.GetList("CASENOTORHAVED");
            if (Havedlist != null)
            {
                this.cmbIsHaveOps.AddItems(Havedlist);
            }
        }
        /// <summary>
        /// 处理fpSpread1,执行科室的回车
        /// </summary>
        /// <returns></returns>
        private int ProcessDept()
        {
            try
            {
                int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
                if (CurrentRow < 0) return 0;

                if (fpSpread1_Sheet1.ActiveColumnIndex == 0)
                {
                    //获取选中的信息
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = ICDType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //ICD诊断编码
                    fpSpread1_Sheet1.ActiveCell.Text = item.ID;
                    //ICD诊断名称 
                    if (fpSpread1_Sheet1.Cells[CurrentRow, 3].Text.Trim() == "")
                    {
                        fpSpread1_Sheet1.Cells[CurrentRow, 3].Text = item.Name;
                    }
                    ICDType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 1, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 2)
                {
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = LevelType.GetSelectedItem(out item);
                    if (item == null) return -1;

                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    LevelType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 3, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 4)
                {
                    //获取选中的信息
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = DoctorType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //术者A
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    //术者A编码
                    fpSpread1_Sheet1.Cells[CurrentRow, 13].Text = item.ID;
                    DoctorType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 5, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 5)
                {
                    //获取选中的信息
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = DoctorType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //一助A
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    //一助A编码
                    fpSpread1_Sheet1.Cells[CurrentRow, 14].Text = item.ID;
                    DoctorType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 6, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 6)
                {
                    //获取选中的信息
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = DoctorType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //二助
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    //二助编码
                    fpSpread1_Sheet1.Cells[CurrentRow, 15].Text = item.ID;
                    DoctorType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 7, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 7)
                {
                    //获取选中的信息
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = NickType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //切口类型
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    NickType.Visible = false;

                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 8, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 8)
                {
                    //获取选中的信息
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = CicaType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //愈合类型
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    CicaType.Visible = false;

                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 9);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 9)
                {
                    //获取选中的信息
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = NarcType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //麻醉方式
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    NarcType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 10, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 10)
                {
                    //获取选中的信息
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = NarcType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //麻醉方式
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    NarcType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 11, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 11)
                {
                    //获取选中的信息
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = DoctorType.GetSelectedItem(out item);
                    //					if(rtn==-1)return -1;
                    if (item == null) return -1;
                    //麻醉医师
                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    //麻醉医师编码
                    fpSpread1_Sheet1.Cells[CurrentRow, 16].Text = item.ID;
                    DoctorType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 19, true);
                    return 0;
                }
                else if (fpSpread1_Sheet1.ActiveColumnIndex == 19)
                {
                    FS.FrameWork.Models.NeuObject item = null;
                    int rtn = selectOpDateType.GetSelectedItem(out item);
                    if (item == null) return -1;

                    fpSpread1_Sheet1.ActiveCell.Text = item.Name;
                    selectOpDateType.Visible = false;
                    fpSpread1.Focus();
                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 20, true);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }
        private int GetStrPosition(string strStr, string subStr)
        {
            return strStr.LastIndexOf(subStr);
        }
        /// <summary>
        /// 键盘事件
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {
                switch (keyData)
                {
                    case Keys.Enter:
                        #region 回车事件
                        if (fpSpread1.ContainsFocus)
                        {
                            int i = fpSpread1_Sheet1.ActiveColumnIndex;
                            if (i == 0 || i == 2 || i == 3 || i == 4 || i == 5 || i == 6 || i == 7 || i == 8 || i == 9 || i == 10 || i == 11)
                            {
                                ProcessDept();
                            }
                            if (i == 1)
                            {
                                //手术及操作日期
                                fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 2);
                            }
                            if (i == 8)
                            {
                                //切口愈合类型
                                fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex, 9);
                            }
                            if (i == 20)
                            {
                                if (fpSpread1_Sheet1.ActiveRowIndex < fpSpread1_Sheet1.Rows.Count - 1)
                                {
                                    //如果不是最后一行 ，跳到最后一行第一格
                                    fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex + 1, 0);
                                }
                                else
                                {
                                    //									//如果是最后一行 跳到本行第一格
                                    //									fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex,0);
                                    //增加一行
                                    this.AddRow();
                                }
                            }
                        }
                        break;
                        #endregion
                    case Keys.Up:
                        #region 上键
                        if (fpSpread1.ContainsFocus)
                        {
                            //手术诊断
                            if (ICDType.Visible)
                            {
                                ICDType.PriorRow();
                            }
                            //切口类型
                            else if (NickType.Visible)
                            {
                                NickType.PriorRow();
                            }
                            //愈合类型
                            else if (CicaType.Visible)
                            {
                                CicaType.PriorRow();
                            }
                            //麻醉类型
                            else if (NarcType.Visible)
                            {
                                NarcType.PriorRow();
                            }
                            //医生列表
                            else if (DoctorType.Visible)
                            {
                                DoctorType.PriorRow();
                            }
                            else if (LevelType.Visible)
                            {
                                LevelType.PriorRow();
                            }
                            else if (selectOpDateType.Visible)
                            {
                                selectOpDateType.PriorRow();
                            }
                            else
                            {
                                int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
                                if (CurrentRow > 0)
                                {
                                    fpSpread1_Sheet1.ActiveRowIndex = CurrentRow - 1;
                                    fpSpread1_Sheet1.AddSelection(CurrentRow - 1, 0, 1, 0);
                                }
                            }
                        }
                        break;
                        #endregion
                    case Keys.Down:
                        #region  下键

                        if (fpSpread1.ContainsFocus)
                        {
                            //手术诊断
                            if (ICDType.Visible)
                            {
                                ICDType.NextRow();
                            }
                            //切口类型
                            else if (NickType.Visible)
                            {
                                NickType.NextRow();
                            }
                            //愈合类型
                            else if (CicaType.Visible)
                            {
                                CicaType.NextRow();
                            }
                            //麻醉类型
                            else if (NarcType.Visible)
                            {
                                NarcType.NextRow();
                            }
                            //医生列表
                            else if (DoctorType.Visible)
                            {
                                DoctorType.NextRow();
                            }
                            else if (LevelType.Visible)
                            {
                                LevelType.PriorRow();
                            }
                            else if (selectOpDateType.Visible)
                            {
                                selectOpDateType.PriorRow();
                            }
                            else
                            {
                                int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;

                                if (CurrentRow < fpSpread1_Sheet1.RowCount - 1)
                                {
                                    fpSpread1_Sheet1.ActiveRowIndex = CurrentRow + 1;
                                    fpSpread1_Sheet1.AddSelection(CurrentRow + 1, 0, 1, 0);
                                }
                                else
                                {
                                    //									AddRow();							
                                }
                            }
                        }
                        break;

                        #endregion
                    case Keys.NumPad1:
                        #region 数字键 1
                        //统计标志
                        if (fpSpread1_Sheet1.ActiveColumnIndex == 12)
                        {
                            //统计标志取反
                            if (fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 12].Value == null)
                            {
                                fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 12].Value = true;
                            }
                            else if (fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 12].Value.ToString() == "False")
                            {
                                fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 12].Value = true;
                            }
                            else
                            {
                                fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, 12].Value = false;
                            }
                            //							//跳转
                            //							if(fpSpread1_Sheet1.ActiveRowIndex < fpSpread1_Sheet1.Rows.Count -1)
                            //							{
                            //								//如果不是最后一行 ，跳到下一行第一格
                            //								fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex+1,0);
                            //							}
                            //							else
                            //							{
                            //								//如果是最后一行 跳到本行第一格
                            //								fpSpread1_Sheet1.SetActiveCell(fpSpread1_Sheet1.ActiveRowIndex,0);
                            //							}
                        }
                        #endregion
                        break;
                    case Keys.Escape:
                        ICDType.Visible = false;
                        NickType.Visible = false;
                        CicaType.Visible = false;
                        NarcType.Visible = false;
                        DoctorType.Visible = false;
                        LevelType.Visible = false;
                        selectOpDateType.Visible = false;
                        break;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return base.ProcessDialogKey(keyData);
        }
        /// <summary>
        /// 单元格处于编辑状态时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            try
            {
                switch (e.Column)
                {
                    case 0:
                        //过滤ICD诊断名称
                        //获取当前格的值
                        ICDType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //筛选数据
                        break;
                    case 2:
                        //手术级别
                        LevelType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        break;
                    #region  医生
                    case 4:
                        //过滤术者
                        //获取当前格的值
                        DoctorType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //筛选数据
                        break;
                    case 5:
                        //过滤一助
                        //获取当前格的值
                        DoctorType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //筛选数据
                        break;
                    case 6:
                        //过滤二助
                        //获取当前格的值
                        DoctorType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //筛选数据
                        break;
                    case 11:
                        //过滤麻醉医师名称
                        //获取当前格的值
                        DoctorType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //筛选数据
                        break;

                    #endregion
                    case 7:
                        //过滤切口名称
                        //获取当前格的值
                        NickType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //筛选数据
                        break;
                    case 8:
                        //过滤愈合名称
                        //获取当前格的值
                        CicaType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //筛选数据
                        break;
                    case 9:
                        //过滤麻醉方式名称
                        //获取当前格的值
                        NarcType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //筛选数据
                        break;
                    case 10:
                        //过滤麻醉方式名称
                        //获取当前格的值
                        NarcType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        //筛选数据
                        break;
                    case 19:
                        //择期
                        selectOpDateType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 设置下来菜单的显示位置
        /// </summary>
        /// <returns></returns>

        private int SetLocation()
        {
            Control _cell = fpSpread1.EditingControl;
            //当前活动列
            int intCol = fpSpread1_Sheet1.ActiveColumnIndex;
            //设置 ICD诊断 下拉框的位置 
            if (intCol == 0)// 
            {
                ICDType.Location = new Point(panel4.Location.X + _cell.Location.X,
                    panel4.Location.Y + panel4.Location.Y + _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                ICDType.Size = new Size(350, 200);
            }
            //设置 ICD诊断 下拉框的位置 
            else if (intCol == 2)// 
            {
                LevelType.Location = new Point(panel4.Location.X + _cell.Location.X,
                    panel4.Location.Y + panel4.Location.Y + _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                LevelType.Size = new Size(350, 200);
            }
            //设置 医生下拉框的位置 
            else if (intCol == 4 || intCol == 5 || intCol == 6 || intCol == 11)
            {
                DoctorType.Location = new Point(panel4.Location.X + _cell.Location.X,
                    panel4.Location.Y + panel4.Location.Y + _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                //				DoctorType.Size=new Size(_cell.Width+SystemInformation.Border3DSize.Width*2,150);
                DoctorType.Size = new Size(200, 150);
            }
            //设置 麻醉方式 下拉框的位置 
            else if (intCol==9 || intCol==10)
            {
                NarcType.Location = new Point(panel4.Location.X + _cell.Location.X,
                    panel4.Location.Y + panel4.Location.Y + _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                //				NarcType.Size=new Size(_cell.Width+SystemInformation.Border3DSize.Width*2,150);
                NarcType.Size = new Size(150, 100);
            }
            //设置 切口 下拉框的位置 
            else if (intCol == 7)
            {
                NickType.Location = new Point(panel4.Location.X + _cell.Location.X,
                    panel4.Location.Y + panel4.Location.Y + _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                //				NickType.Size=new Size(_cell.Width+SystemInformation.Border3DSize.Width*2,150);
                NickType.Size = new Size(150, 100);
            }
            //设置 愈合 下拉框的位置 
            else if (intCol == 8)
            {
                CicaType.Location = new Point(panel4.Location.X + _cell.Location.X,
                    panel4.Location.Y + panel4.Location.Y + _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                //				CicaType.Size=new Size(_cell.Width+SystemInformation.Border3DSize.Width*2,150);
                CicaType.Size = new Size(150, 100);
            }
            //设置 择期 下拉框的位置 
            else if (intCol == 19)// 
            {
                selectOpDateType.Location = new Point(panel4.Location.X + _cell.Location.X,
                    panel4.Location.Y + panel4.Location.Y + _cell.Location.Y + _cell.Height + SystemInformation.Border3DSize.Height * 2);
                selectOpDateType.Size = new Size(350, 200);
            }
            return 0;
        }

        private void fpSpread1_EditModeOn(object sender, System.EventArgs e)
        {
            try
            {
                SetLocation();
                int intCol = fpSpread1_Sheet1.ActiveColumnIndex;
                //设置 ICD诊断 下拉框的可见性
                if (intCol != 0)// && intCol != 2
                {
                    ICDType.Visible = false;
                }
                //设置 手术级别 下拉框的可见性
                if (intCol != 2)// && intCol != 2
                {
                    LevelType.Visible = false;
                }
                //设置 医生下拉框的可见性
                if (intCol != 4 || intCol != 5 || intCol != 6 || intCol != 11)
                {
                    DoctorType.Visible = false;
                }
                //设置 麻醉类型下拉框的可见性
                if (intCol!=9 ||intCol!=10)
                {
                    NarcType.Visible = false;
                }
                //设置 切口下拉框的可见性
                if (intCol != 7)
                {
                    NickType.Visible = false;
                }
                //设置 愈合下拉框的可见性
                if (intCol != 8)
                {
                    CicaType.Visible = false;
                }
                //设置 择期 下拉框的可见性
                if (intCol != 19)// && intCol != 2
                {
                    selectOpDateType.Visible = false;
                }

                //手术诊断
                if (intCol == 0)//|| intCol == 2
                {
                    ICDType.Filter(fpSpread1_Sheet1.ActiveCell.Text.Trim());
                    ICDType.Visible = true;
                }
                else if (intCol == 2)
                {
                    LevelType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                    LevelType.Visible = true;
                }
                //医生
                else if (intCol == 4 || intCol == 5 || intCol == 6 || intCol == 11)
                {
                    DoctorType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                    DoctorType.Visible = true;
                }
                //切口
                else if (intCol == 7)
                {
                    NickType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                    NickType.Visible = true;
                }
                //愈合
                else if (intCol == 8)
                {
                    CicaType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                    CicaType.Visible = true;
                }
                //麻醉方式1
                else if (intCol == 9)
                {
                    NarcType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                    NarcType.Visible = true;
                }
                //麻醉方式2
                else if (intCol == 10)
                {
                    NarcType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                    NarcType.Visible = true;
                }
                else if (intCol == 19)//择期
                {
                    selectOpDateType.Filter(fpSpread1_Sheet1.ActiveCell.Text);
                    selectOpDateType.Visible = true;
                }
            }
            catch { }
        }
        /// <summary>
        /// 获取列号
        /// </summary>
        /// <param name="view"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public int ColumnIndex(FarPoint.Win.Spread.SheetView view, string str)
        {
            try
            {
                foreach (FarPoint.Win.Spread.Column col in view.Columns)
                {
                    if (col.Label == str)
                    {
                        return col.Index;
                    }
                }
                MessageBox.Show("没有找到" + str + "列");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            //			//设置fpSpread1 的属性
            //			if(System.IO.File.Exists(filePath))
            //			{
            //				FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1_Sheet1,filePath);
            //			}
        }
        //设置
        private void menuItem1_Click(object sender, System.EventArgs e)
        {
            SetUp();
        }
        /// <summary>
        ///设置fpSpread1_Sheet1 的属性
        /// </summary>
        public void SetUp()
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
            LoadInfo(patient, operType); //重新加载数据

        }

        /// <summary>
        /// 删除 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem2_Click(object sender, System.EventArgs e)
        {
            DeleteRow();
        }
        /// <summary>
        /// 删除 
        /// </summary>
        /// <returns></returns>
        public int DeleteRow()
        {
            SetAllListUnVisiable();
            this.fpSpread1.EditModePermanent = false;
            this.fpSpread1.EditModeReplace = false;
            this.fpSpread1_Sheet1.Rows.Remove(fpSpread1_Sheet1.ActiveRowIndex, 1);
            this.fpSpread1.EditModePermanent = true;
            this.fpSpread1.EditModeReplace = true;
            return 1;
        }
        #endregion

        private void btAdd_Click(object sender, EventArgs e)
        {
            //this.AddRow();
            this.InsertRow();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            this.DeleteActiveRow();
        }

        /// <summary>
        /// 获取修改过的数据 
        /// </summary>
        /// <param name="table">要提取数据的Table</param>
        /// <param name="list"> 输出的数组</param>
        /// <returns>失败返回false ,且数组返回null 成功返回 null</returns>
        public bool GetOperationList(ArrayList list)
        {
            try
            {
                FS.HISFC.Models.HealthRecord.OperationDetail bb;
                for (int row = 0; row < this.fpSpread1_Sheet1.RowCount; row++)
                {
                    bb = new FS.HISFC.Models.HealthRecord.OperationDetail();

                    bb.OperType = "1";
                    bb.InpatientNO = patient.ID;

                    bb.OperationInfo.ID = this.fpSpread1_Sheet1.Cells[row, 0].Text.Trim();//手术及操作编码
                    if (bb.OperationInfo.ID == "")
                    {
                        bb.OperationInfo.ID = "MS999";
                    }
                    bb.OperationDate = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.Cells[row, 1].Text);//手术及操作日期
                    bb.FourDoctInfo.Name = LevelHelper.GetID(this.fpSpread1_Sheet1.Cells[row, 2].Text);//手术级别
                    bb.OperationInfo.Name =FS.HISFC.Components.HealthRecord.CaseFirstPage.Funtion.ReplaceSingleQuotationMarks(this.fpSpread1_Sheet1.Cells[row, 3].Text,true);//手术及操作名称
                    bb.FirDoctInfo.Name = this.fpSpread1_Sheet1.Cells[row, 4].Text; //术者
                    bb.SecDoctInfo.Name = this.fpSpread1_Sheet1.Cells[row, 5].Text; //I 助
                    bb.ThrDoctInfo.Name = this.fpSpread1_Sheet1.Cells[row, 6].Text;// II 助
                    if (this.fpSpread1_Sheet1.Cells[row, 7].Text != "")//切口
                    {
                        bb.NickKind = NickTypeHelper.GetID(this.fpSpread1_Sheet1.Cells[row, 7].Text);
                    }
                    if (this.fpSpread1_Sheet1.Cells[row, 8].Text != "")//愈合
                    {
                        bb.CicaKind = CicaTypeHelper.GetID(this.fpSpread1_Sheet1.Cells[row, 8].Text);
                    }
                    if (this.fpSpread1_Sheet1.Cells[row, 9].Text != "")//麻醉方式1
                    {
                        bb.MarcKind = NarcTypeHelper.GetID(this.fpSpread1_Sheet1.Cells[row, 9].Text);
                    }
                    if (this.fpSpread1_Sheet1.Cells[row, 10].Text != "")//麻醉方式2
                    {
                        bb.OpbOpa = NarcTypeHelper.GetID(this.fpSpread1_Sheet1.Cells[row, 10].Text);
                    }

                    bb.NarcDoctInfo.Name = this.fpSpread1_Sheet1.Cells[row, 11].Text;//麻醉医师
                    bb.StatFlag = "1";
                    bb.FirDoctInfo.ID = this.DoctorHelper.GetID(bb.FirDoctInfo.Name);//this.fpSpread1_Sheet1.Cells[row, 13].Text;// row["术者编码"].ToString();
                    bb.SecDoctInfo.ID = this.DoctorHelper.GetID(bb.SecDoctInfo.Name);//this.fpSpread1_Sheet1.Cells[row, 14].Text;// row["助手编码1"].ToString();
                    bb.ThrDoctInfo.ID = this.DoctorHelper.GetID(bb.ThrDoctInfo.Name);//this.fpSpread1_Sheet1.Cells[row, 15].Text;// row["助手编码2"].ToString();
                    bb.NarcDoctInfo.ID = this.DoctorHelper.GetID(bb.NarcDoctInfo.Name);// this.fpSpread1_Sheet1.Cells[row, 16].Text;// row["麻醉医师编码"].ToString();
                    bb.OperationKind =selectOpDateHelper.GetID(this.fpSpread1_Sheet1.Cells[row, 19].Text);// row["择期"].ToString();
                    bb.HappenNO = row.ToString();// row["发生序号"].ToString();
                    bb.OperationNO = row.ToString();// row["手术序列号"].ToString();
                    bb.OutDate = this.patient.PVisit.OutTime;//出院日期
                    bb.InDate = patient.PVisit.InTime; //入院日期 
                    bb.DeatDate = patient.DeathTime; //死亡时间 
                    bb.OperationDeptInfo.ID = ""; //手术科室
                    bb.OutDeptInfo.ID = patient.PVisit.PatientLocation.ID; //出院科室
                    TimeSpan tt = bb.OperationDate- this.patient.PVisit.InTime;
                    if (tt.Days == 0)
                    {
                        bb.BeforeOperDays = 1;
                    }
                    else
                    {
                        bb.BeforeOperDays = tt.Days;
                    }
                    list.Add(bb);
                }
                return true;
            }
            catch (Exception ex)
            {
                list = null;
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.AddRow();
        }
    }
}
