using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.Case
{
    public partial class ucCaseStore : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 设计思路：表（MET_CAS_STORE） 记录当前状态，住院号和住院次数为唯一主键（来源于条码） 表 MET_CAS_STOREDETAIL 记录每次操作痕迹 主键（store_no）用于记录一次操作的病案数
        /// 操作：1、首先选择-操作类型  在住院号处：输入“住院号+"-"+住院次数 回车获取信息；
        /// 关键点：病案的管理最好通过条码[手工也行]（住院号+"-"+住院次数）管理；  当然 如果单通过住院号管理，修改一下获取信息的条件也没有问题；
        /// 2011-6-20 成郁明
        /// 
        /// Add 2011-7-4 增加从广东省3.0处根据病案号段导入数据到库房的功能（暂时：不对外开放） 
        /// </summary>
        public ucCaseStore()
        {
            InitializeComponent();
        }
        #region 变量
        /// <summary>
        /// 工具栏
        /// </summary>
        FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        /// <summary>
        /// 常数字典业务类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
        /// <summary>
        /// 人员业务类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();
        /// <summary>
        /// 出入院管理类
        /// </summary>
        FS.HISFC.BizProcess.Integrate.RADT radtMana = new FS.HISFC.BizProcess.Integrate.RADT();

        FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        FS.HISFC.Models.HealthRecord.Case.CaseStore caseStore = new FS.HISFC.Models.HealthRecord.Case.CaseStore();

        FS.HISFC.BizLogic.HealthRecord.Case.CaseStroe caseStoreMgr = new FS.HISFC.BizLogic.HealthRecord.Case.CaseStroe();
        /// <summary>
        /// 库房号 柜排号获取函数
        /// </summary>
        functionStore fun = new functionStore();
        FS.FrameWork.Public.ObjectHelper caseStoreHelper = new FS.FrameWork.Public.ObjectHelper();
        FS.FrameWork.Public.ObjectHelper caseCabinetHelper = new FS.FrameWork.Public.ObjectHelper();


        private bool isUserInterFace=false;//是否使用接口  --广东省病案接口  
        /// <summary>
        /// 是否使用广东省病案接口
        /// </summary>
        [Category("是否使用广东省病案接口"), Description("从广东省病案系统获取数据")]
        public bool IsUserInterFace
        {
            get { return this.isUserInterFace; }
            set { this.isUserInterFace = value; }
        }

        #endregion 
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            try
            {
                this.Clear();
                this.Init();
                toolbarService.AddToolButton("修改病案号", "修改病案号", FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
                toolbarService.AddToolButton("查询库房情况", "查询库房情况", FS.FrameWork.WinForms.Classes.EnumImageList.C查找, true, false, null);
                toolbarService.AddToolButton("清空", "清空", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
                toolbarService.AddToolButton("删除", "删除", FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);


            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "错误信息");
            }
            return toolbarService;
        }
        /// <summary>
        /// 工具栏单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "修改病案号":
                    this.CancelCaseNO();
                    break;
                case"查询库房情况":
                    this.ShowQuery();
                    break;
                case"清空":
                    this.Clear();
                    break;
                case "删除":
                    this.RemoveRow();
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return base.OnSave(sender, neuObject);
        }
        private void Clear()
        {
            this.cmbCaseState.Tag = null;
            this.cmbStoreCode.Tag = null;
            this.txtCaseStoreBegin.Text = "";
            this.txtCaseStoreEnd.Text = "";
            this.cmbCabinetCode.Tag = null;
            this.txtCabinetBegin.Text = "";
            this.txtCabinetEnd.Text = "";
            this.txtGrid.Text = "";
            this.txtMemo.Text = "";
            this.cmbOper.Tag = null;
            this.txtCaseNO.Text = "";
            this.txtIntimes.Text = "";
            this.txtName.Text = "";
            this.fpSpread_Sheet.RowCount = 0;
        }
        /// <summary>
        /// 初始化下拉列表
        /// </summary>
        private void Init()
        {
            //类型
            ArrayList StateAl = new ArrayList();
            StateAl = this.conMgr.GetAllList("CaseState");
            this.cmbCaseState.AddItems(StateAl);
            //库房编号
            ArrayList StoreAl = new ArrayList();
            StoreAl = this.conMgr.GetAllList("CaseStore");
            this.cmbStoreCode.AddItems(StoreAl);
            this.caseStoreHelper.ArrayObject = StoreAl;
            //病案柜编号
            ArrayList CabinetAl = new ArrayList();
            CabinetAl = this.conMgr.GetAllList("CaseCabinet");
            this.cmbCabinetCode.AddItems(CabinetAl);
            this.caseCabinetHelper.ArrayObject = CabinetAl;
            //人员
            ArrayList personAl = this.personMgr.GetUserEmployeeAll();
            this.cmbOper.AddItems(personAl);
            this.cmbOper.Tag = this.conMgr.Operator.ID;

            if (this.personMgr.Operator.ID == "009999")
            {
                this.txtImportBegin.Visible = true;
                this.txtImportEnd.Visible = true;
                this.button1.Visible = true;
            }
        }
        /// <summary>
        /// 住院号回车事件
        /// 1、库房中是否存在信息  2是否从广东省病案中获得姓名 3、在com_patientinfo中获取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCaseNO_KeyDown(object sender, KeyEventArgs e)
        {
            //需要增加 记录查询-赋值

            if (e.KeyData == Keys.Enter)
            {
                string patientNo = string.Empty;//住院号
                string inTimes = "1";//住院次数

                string caseNo = "";
                caseNo = this.txtCaseNO.Text;

                //add by chengym 东莞特殊处理
                if (caseNo.IndexOf('A') >= 0 || caseNo.IndexOf('B') >= 0 || caseNo.IndexOf('C') >= 0 || caseNo.IndexOf('D') >= 0 || caseNo.IndexOf('E') >= 0)
                {
                    caseNo = caseNo.Replace('A', '0');
                    caseNo = caseNo.Replace('B', '0');
                    caseNo = caseNo.Replace('C', '0');
                    caseNo = caseNo.Replace('D', '0');
                    caseNo = caseNo.Replace('E', '0');
                    caseNo = caseNo.TrimStart('0').PadLeft(6, '0') + "-" + "1";
                }
                //end
                caseNo = caseNo.Replace('—', '-');
                if (caseNo.IndexOf('-') > 0)
                {
                    string[] CaseNO = caseNo.Split('-');
                    this.txtCaseNO.Text = CaseNO[0].ToString().TrimStart('0').PadLeft(6, '0');
                    this.txtIntimes.Text = CaseNO[1].ToString().Trim();

                    patientNo = CaseNO[0].ToString().TrimStart('0').PadLeft(6, '0');
                    inTimes = CaseNO[1].ToString().Trim();
                }
                else
                {
                    patientNo = caseNo.TrimStart('0').PadLeft(10,'0');
                }
                
                //判断是否在库房
                caseStore = new FS.HISFC.Models.HealthRecord.Case.CaseStore();
                caseStore= this.caseStoreMgr.QueryCaseStore(patientNo, inTimes);
                if (caseStore != null && caseStore.PatientInfo.PID.PatientNO != "")
                {
                    this.txtCaseNO.Text = caseStore.PatientInfo.PID.PatientNO;
                    this.txtIntimes.Text = caseStore.PatientInfo.InTimes.ToString();
                    this.txtName.Text = caseStore.PatientInfo.Name;
                    if (caseStore.Store.ID.ToString() == "")
                    {
                        //获取病案库房号
                        caseStore.Store.ID=fun.GetCaseStore(caseStore.PatientInfo.PID.PatientNO.TrimStart('0'));
                    }
                    this.cmbStoreCode.Tag = caseStore.Store.ID.ToString();                   
                    if (caseStore.Cabinet.ID.ToString() == "")
                    {
                        //获取柜排号
                        caseStore.Cabinet.ID = fun.GetCabinet(caseStore.PatientInfo.PID.PatientNO.TrimStart('0'));
                    }
                    this.cmbCabinetCode.Tag = caseStore.Cabinet.ID.ToString();
                    this.txtGrid.Text = caseStore.Grid.ID.ToString();
                    this.txtMemo.Text = caseStore.CaseMemo.ToString();
                    this.cmbOper.Tag = this.caseStoreMgr.Operator.ID;

                    //MessageBox.Show("请选择操作类型！", "提示");
                    this.SetFarPointCaseStore(caseStore);

                }
                else
                {
                    //号段赋值
                    this.cmbStoreCode.Tag = fun.GetCaseStore(patientNo.TrimStart('0'));
                    this.cmbCabinetCode.Tag = fun.GetCabinet(patientNo.TrimStart('0'));
                    
                    //this.GetCaseStore(FS.FrameWork.Function.NConvert.ToInt32(this.txtCaseNO.Text.TrimStart('0')));//根据住院号-获取病案库房号
                    //this.GetCabinet(FS.FrameWork.Function.NConvert.ToInt32(this.txtCaseNO.Text.TrimStart('0')));//根据住院号-获取病案库房柜格号
                    if (this.cmbCaseState.Tag == null || this.cmbCaseState.Tag.ToString() == "")
                    {
                        this.cmbCaseState.Focus();
                        MessageBox.Show("请选择操作类型！", "提示");
                        return;
                    }
                    if (this.cmbStoreCode.Tag == null || this.cmbStoreCode.Tag.ToString()=="")
                    {
                        this.cmbStoreCode.Focus();
                        MessageBox.Show("请选择病案库房编号！", "提示");
                        return;
                    }
                    if (this.cmbCabinetCode.Tag == null||this.cmbCabinetCode.Tag.ToString()=="")
                    {
                        this.cmbCabinetCode.Focus();
                        MessageBox.Show("请选择病案柜编号！", "提示");
                        return;
                    }

                    patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                    if (this.isUserInterFace == true)
                    {
                        FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface uploadMgr = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface();//广东省病案接口

                        patientInfo = uploadMgr.GetPatientByIdAndTimes(patientNo.TrimStart('0'), FS.FrameWork.Function.NConvert.ToInt32(inTimes));
                        if (patientInfo == null || patientInfo.ID == "")
                        {
                            MessageBox.Show("广东省病案接口中，未找到患者信息!","提示");
                            this.txtCaseNO.Text = "";
                            this.txtCaseNO.Focus();
                            return;
                        }
                        this.txtName.Text = patientInfo.Name;
                    }
                    else
                    {
                        string argCardNo = "T" + patientNo.TrimStart('0').PadLeft(9, '0');  
                        patientInfo = this.radtMana.QueryComPatientInfo(argCardNo);//查com_patientinfo（卡号：T + 住院号（用0补齐9位）） 
                        if (patientInfo == null || patientInfo.PID.CardNO == null || patientInfo.PID.CardNO == string.Empty)
                        {
                            argCardNo = argCardNo.Replace('T', '0').PadLeft(10, '0');
                            patientInfo = this.radtMana.QueryComPatientInfo(argCardNo);//查com_patientinfo 卡号：住院号（用0补齐10位）
                            if (patientInfo == null || patientInfo.PID.CardNO == null || patientInfo.PID.CardNO == string.Empty)
                            {
                                MessageBox.Show("com_patientinfo未找到患者信息!" + radtMana.Err);
                                return;
                            }
                        }
                        patientInfo.ID = patientInfo.PID.CardNO.Replace('T', '0').PadLeft(10, '0');
                        patientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(inTimes) ;//肯定获取不到住院次数
                        this.txtIntimes.Text = inTimes.ToString();
                        this.txtName.Text = patientInfo.Name;
                    }
                    this.SetFarPoint(patientInfo);
                }
                this.txtCaseNO.Text = "";
                this.txtCaseNO.Focus();
            }
        }
        /// <summary>
        /// 将界面值赋到farpiont中
        /// </summary>
        private void SetFarPointCaseStore(FS.HISFC.Models.HealthRecord.Case.CaseStore caseStore)
        {
            if (this.CheckCaseStore(caseStore.PatientInfo.PID.PatientNO) < 0)//判断选择库房号是否正确
            {
                MessageBox.Show("该患者不在号段为“" + this.txtCaseStoreBegin.Text + "”到“" + this.txtCaseStoreEnd.Text + "”范围内", "提示");
                return;
            }
            if (this.CheckCabinet(caseStore.PatientInfo.PID.PatientNO) < 0)//判断选择库房柜格号是否正确
            {
                MessageBox.Show("该患者不在号段为“" + this.txtCabinetBegin.Text + "”到“" + this.txtCabinetEnd.Text + "”范围内", "提示");
                return;
            }

            for (int i = 0; i < this.fpSpread_Sheet.RowCount; i++)
            {
                if (this.fpSpread_Sheet.Cells[i, 0].Text == caseStore.PatientInfo.PID.PatientNO.PadLeft(10, '0') && this.fpSpread_Sheet.Cells[i, 1].Text == caseStore.PatientInfo.InTimes.ToString())
                {
                    MessageBox.Show("该患者已经操作！在" + i.ToString() + "行", " 提示");
                    return;
                }
            }
            this.fpSpread_Sheet.Rows.Add(this.fpSpread_Sheet.RowCount, 1);

            int row = this.fpSpread_Sheet.RowCount - 1;

            this.fpSpread_Sheet.Cells[row, 0].Text = caseStore.PatientInfo.PID.PatientNO.PadLeft(10, '0');
            this.fpSpread_Sheet.Cells[row, 1].Text = caseStore.PatientInfo.InTimes.ToString();
            this.fpSpread_Sheet.Cells[row, 2].Text = caseStore.PatientInfo.Name;
            this.fpSpread_Sheet.Cells[row, 3].Text = caseStore.CaseState;
            this.fpSpread_Sheet.Cells[row, 4].Text = this.cmbCaseState.Text.ToString();
            this.fpSpread_Sheet.Cells[row, 5].Text = caseStore.Store.ID;
            this.fpSpread_Sheet.Cells[row, 6].Text = this.cmbStoreCode.Text;
            this.fpSpread_Sheet.Cells[row, 7].Text = caseStore.Cabinet.ID;
            this.fpSpread_Sheet.Cells[row, 8].Text = this.cmbCabinetCode.Text.ToString();
            this.fpSpread_Sheet.Cells[row, 9].Text = caseStore.Grid.ID;
            this.fpSpread_Sheet.Cells[row, 10].Text = caseStore.CaseMemo;
            this.fpSpread_Sheet.Cells[row, 11].Text = caseStore.OperEnv.ID;
            this.fpSpread_Sheet.Cells[row, 12].Text = this.cmbOper.Text;
        }
        /// <summary>
        /// 将界面值赋到farpiont中
        /// </summary>
        private void SetFarPoint(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            if (this.CheckCaseStore(patientInfo.ID) < 0)//判断选择库房号是否正确
            {
                return;
            }
            if (this.CheckCabinet(patientInfo.ID) < 0)//判断选择库房柜格号是否正确
            {
                MessageBox.Show("该患者不在号段为“" + this.txtCabinetBegin.Text + "”到“" + this.txtCabinetEnd.Text + "”范围内", "提示");
                return;
            }
            for (int i = 0; i < this.fpSpread_Sheet.RowCount; i++)
            {
                if (this.fpSpread_Sheet.Cells[i, 0].Text == this.txtCaseNO.Text.PadLeft(10, '0') && this.fpSpread_Sheet.Cells[i, 1].Text == this.txtIntimes.Text)
                {
                    MessageBox.Show("该患者已经操作！在"+i.ToString()+"行", " 提示");
                    return;
                }
            }
            this.fpSpread_Sheet.Rows.Add(this.fpSpread_Sheet.RowCount, 1);

            int row =this.fpSpread_Sheet.RowCount - 1;

            this.fpSpread_Sheet.Cells[row, 0].Text = this.txtCaseNO.Text.PadLeft(10,'0');
            this.fpSpread_Sheet.Cells[row, 1].Text = this.txtIntimes.Text;
            this.fpSpread_Sheet.Cells[row, 2].Text = this.txtName.Text;
            if (this.cmbCaseState.Tag == null || this.cmbCaseState.Tag.ToString() == "")
            {
                this.fpSpread_Sheet.Cells[row, 3].Text = "";
                this.fpSpread_Sheet.Cells[row, 4].Text = "";
            }
            else
            {
                this.fpSpread_Sheet.Cells[row, 3].Text = this.cmbCaseState.Tag.ToString();
                this.fpSpread_Sheet.Cells[row, 4].Text = this.cmbCaseState.Text.ToString();
            }
            if (this.cmbStoreCode.Tag == null || this.cmbStoreCode.Tag.ToString() == "")
            {
                this.fpSpread_Sheet.Cells[row, 5].Text = "";
                this.fpSpread_Sheet.Cells[row, 6].Text = "";
            }
            else
            {
                this.fpSpread_Sheet.Cells[row, 5].Text = this.cmbStoreCode.Tag.ToString();
                this.fpSpread_Sheet.Cells[row, 6].Text = this.cmbStoreCode.Text;
            }
            if (this.cmbCabinetCode.Tag == null || this.cmbCabinetCode.Tag.ToString() == "")
            {
                this.fpSpread_Sheet.Cells[row, 7].Text = "";
                this.fpSpread_Sheet.Cells[row, 8].Text = "";
            }
            else
            {
                this.fpSpread_Sheet.Cells[row, 7].Text = this.cmbCabinetCode.Tag.ToString();
                this.fpSpread_Sheet.Cells[row, 8].Text = this.cmbCabinetCode.Text.ToString();
            }
            this.fpSpread_Sheet.Cells[row, 9].Text = this.txtGrid.Text;
            this.fpSpread_Sheet.Cells[row, 10].Text = this.txtMemo.Text;
            if (this.cmbOper.Tag != null && this.cmbOper.Tag.ToString() != "")
            {
                this.fpSpread_Sheet.Cells[row, 11].Text = this.cmbOper.Tag.ToString();
            }
            else
            {
                this.fpSpread_Sheet.Cells[row, 11].Text = this.caseStoreMgr.Operator.ID;
            }
            this.fpSpread_Sheet.Cells[row, 12].Text = this.cmbOper.Text;
        }
        /// <summary>
        /// 判断库房号是否选择正确
        /// </summary>
        /// <returns></returns>
        private int CheckCaseStore(string patientNo)
        {
            int rtn = -1;
            if (this.cmbStoreCode.Tag != null || this.cmbStoreCode.Tag.ToString() != "")
            {
                int pNo = FS.FrameWork.Function.NConvert.ToInt32(patientNo.TrimStart('0').ToString());

                FS.FrameWork.Models.NeuObject obj = this.conMgr.GetConstant("CaseStore", this.cmbStoreCode.Tag.ToString());
                if (obj != null && obj.ID.ToString() != "")
                {
                    string[] fd = obj.Memo.Split('|');
                    if (fd.Length > 0)
                    {
                        for (int i = 0; i < fd.Length; i++)
                        {
                            string[] fd1 = fd[i].ToString().Split('-');
                            int fd1Begin = 0;
                            int fd1End = 0;
                            if (fd1.Length > 0)
                            {
                                fd1Begin = FS.FrameWork.Function.NConvert.ToInt32(fd1[0].ToString());
                                fd1End = FS.FrameWork.Function.NConvert.ToInt32(fd1[1].ToString());
                                if (pNo >= fd1Begin && pNo <= fd1End)
                                {
                                    this.txtCaseStoreBegin.Text = fd1Begin.ToString();
                                    this.txtCaseStoreEnd.Text = fd1End.ToString();
                                    rtn = 1;
                                    break;
                                }
                                else
                                {
                                    rtn = -1;
                                }
                            }
                        }
                    }
                }
            }
            return rtn;
           
        }
        /// <summary>
        /// 根据住院号-获得库房号
        /// </summary>
        /// <param name="CaseNo"></param>
        private void GetCaseStore(int CaseNo)
        {
            if (CaseNo == 0)//存在特殊字符退出 --按道理应该就不会存在特殊字符，有再处理吧2011-6-20
            {
                return;
            }
            foreach (FS.FrameWork.Models.NeuObject obj in this.caseStoreHelper.ArrayObject)//存在多个段情况
            {
                string[] fd=obj.Memo.Split('|');
                if (fd.Length > 0)
                {
                    for (int i = 0; i < fd.Length; i++)
                    {
                        string[] fd1 = fd[i].ToString().Split('-');
                        int fd1Begin = 0;
                        int fd1End = 0;
                        if(fd1.Length>0)
                        {
                            fd1Begin = FS.FrameWork.Function.NConvert.ToInt32(fd1[0].ToString());
                            fd1End = FS.FrameWork.Function.NConvert.ToInt32(fd1[1].ToString());
                            if (CaseNo >= fd1Begin && CaseNo <= fd1End)
                            {
                                this.cmbStoreCode.Tag = obj.ID.ToString();
                                this.txtCaseStoreBegin.Text = fd1Begin.ToString();
                                this.txtCaseStoreEnd.Text = fd1End.ToString();
                                break;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 判断病案柜格号是否选择正确
        /// </summary>
        /// <returns></returns>
        private int CheckCabinet(string patientNo)
        {
            int rtn = -1;

            if (this.cmbCabinetCode.Tag != null && this.cmbCabinetCode.Tag.ToString() != "")
            {
                FS.FrameWork.Models.NeuObject obj = this.conMgr.GetConstant("CaseCabinet", this.cmbCabinetCode.Tag.ToString());
                if (obj == null || obj.ID == "")
                {
                    rtn=-1;
                }
                string[] cabinetNO = obj.Memo.Split('-');

                int pNo = FS.FrameWork.Function.NConvert.ToInt32(patientNo.TrimStart('0').ToString());
                int begin1 = FS.FrameWork.Function.NConvert.ToInt32(cabinetNO[0].Trim().ToString());
                int end1 = FS.FrameWork.Function.NConvert.ToInt32(cabinetNO[1].Trim().ToString());
                if (begin1 <= pNo && pNo <= end1)
                {
                    this.txtCabinetBegin.Text = begin1.ToString();
                    this.txtCabinetEnd.Text = end1.ToString();
                    rtn = 1;
                }
                else
                {
                    rtn = -1;
                }
            }
            return rtn;
        }

        /// <summary>
        /// 根据住院号--获得病案柜号
        /// </summary>
        /// <param name="CaseNo"></param>
        private void GetCabinet(int CaseNo)
        {
            if (CaseNo == 0)//存在特殊字符退出 --按道理应该就不会存在特殊字符，有再处理吧2011-6-20
            {
                return;
            }
            foreach (FS.FrameWork.Models.NeuObject obj in this.caseCabinetHelper.ArrayObject)
            {
                string[] str = obj.Memo.Split('-');
                int storeBegin = 0;
                int storeEnd = 0;
                if (str.Length > 0)
                {
                    storeBegin = FS.FrameWork.Function.NConvert.ToInt32(str[0].ToString());
                    storeEnd = FS.FrameWork.Function.NConvert.ToInt32(str[1].ToString());
                    if (CaseNo >= storeBegin && CaseNo <= storeEnd)
                    {
                        this.cmbCabinetCode.Tag = obj.ID.ToString();
                        break;
                    }
              
                }
            }
        }
        /// <summary>
        /// 获取界面数据
        /// </summary>
        /// <returns></returns>
        private ArrayList GetInfo()
        {
            ArrayList al = new ArrayList();
            for(int i=0;i < this.fpSpread_Sheet.RowCount;i++)
            {
                caseStore = new FS.HISFC.Models.HealthRecord.Case.CaseStore();
                caseStore.PatientInfo.PID.PatientNO = this.fpSpread_Sheet.Cells[i, 0].Text.Trim().PadLeft(10,'0');
                caseStore.PatientInfo.InTimes = FS.FrameWork.Function.NConvert.ToInt32(this.fpSpread_Sheet.Cells[i, 1].Text.Trim());
                caseStore.PatientInfo.Name = this.fpSpread_Sheet.Cells[i, 2].Text.Trim();
                caseStore.IsVaild = true;//作废时设置为无效
                caseStore.CaseState = this.fpSpread_Sheet.Cells[i, 3].Text.Trim();
                caseStore.Store.ID = this.fpSpread_Sheet.Cells[i, 5].Text.Trim();
                caseStore.Store.Name = this.fpSpread_Sheet.Cells[i, 6].Text.Trim();
                caseStore.Cabinet.ID = this.fpSpread_Sheet.Cells[i, 7].Text.Trim();
                caseStore.Cabinet.Name = this.fpSpread_Sheet.Cells[i, 8].Text.Trim();
                caseStore.Grid.ID = this.fpSpread_Sheet.Cells[i, 9].Text.Trim();
                caseStore.CaseMemo = this.fpSpread_Sheet.Cells[i, 10].Text.Trim();
                caseStore.OperEnv.ID = this.fpSpread_Sheet.Cells[i, 11].Text.Trim();
                caseStore.OperEnv.Name = this.fpSpread_Sheet.Cells[i, 12].Text.Trim();
                caseStore.OperEnv.OperTime = this.conMgr.GetDateTimeFromSysDateTime();
                caseStore.Extend1 = "";
                caseStore.Extend2 = "";
                caseStore.Extend3 = "";
                caseStore.Extend4 = "";
                al.Add(caseStore);
            }
            return al;
        }
        /// <summary>
        /// 保存 获取farpiont数据
        /// </summary>
        /// <returns></returns>
        private int Save()
        {
            ArrayList caseAl = new ArrayList();
            caseAl= this.GetInfo();
            if (caseAl == null || caseAl.Count == 0)
            {
                MessageBox.Show("无需要保存的数据！", "提示");
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.caseStoreMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            string CaseNum = this.caseStoreMgr.GetSequence("HealthReacord.Case.CaseStore.Seq");

            foreach (FS.HISFC.Models.HealthRecord.Case.CaseStore info in caseAl)
            {
                if (this.caseStoreMgr.InsertCaseStore(info) < 0)
                {
                    if (this.caseStoreMgr.UpdateCaseStore(info) < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存病案库房信息失败！", "提示");
                        return -1;
                    }
                }

                if (this.caseStoreMgr.InsertCaseStoreDetail(info,CaseNum) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("保存病案库房明细信息失败！", "提示");
                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("保存成功！", "提示");
            this.Clear();//保存后清空界面
            return 1;
        }

        private void cmbStoreCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbStoreCode.Tag != null || this.cmbStoreCode.Tag.ToString() != "")
            {
                //FS.FrameWork.Models.NeuObject obj = this.conMgr.GetConstant("CaseStore", this.cmbStoreCode.Tag.ToString());
                //if (obj != null && obj.ID.ToString() != "")
                //{
                //    string[] str = obj.Memo.Split('-');
                //    if (str.Length > 0)
                //    {
                //        this.txtCaseStoreBegin.Text = str[0].ToString();
                //        this.txtCaseStoreEnd.Text = str[1].ToString();
                //    }
                //}

                for (int i = 0; i < this.fpSpread_Sheet.RowCount; i++)
                {
                    if (this.fpSpread_Sheet.Cells[i, 0].Text.ToString() != this.txtCaseNO.Text.PadLeft(10, '0').ToString() )
                    {
                        continue;
                    }
                    if (this.CheckCaseStore(this.txtCaseNO.Text.PadLeft(10, '0').ToString()) < 0)//判断选择库房号是否正确
                    {
                        this.txtCaseStoreBegin.Text = "";
                        this.txtCaseStoreEnd.Text = "";
                        this.cmbStoreCode.Tag = null;
                        return;
                    }
                    this.fpSpread_Sheet.Cells[i, 5].Text = this.cmbStoreCode.Tag.ToString();
                    this.fpSpread_Sheet.Cells[i, 6].Text = this.cmbStoreCode.Text;
                }
            }
        }

        private void cmbCaseState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbCaseState.Tag != null && this.cmbCaseState.Tag.ToString() != "")
            {
                for (int i = 0; i < this.fpSpread_Sheet.RowCount; i++)
                {
                    if (this.fpSpread_Sheet.Cells[i, 0].Text.ToString() != this.txtCaseNO.Text.PadLeft(10, '0').ToString())
                    {
                        continue;
                    }
                    this.fpSpread_Sheet.Cells[i, 3].Text = this.cmbCaseState.Tag.ToString();
                    this.fpSpread_Sheet.Cells[i, 4].Text = this.cmbCaseState.Text.ToString();
                }
                this.txtCaseNO.Focus();
            }
        }

        private void cmbCabinetCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbCabinetCode.Tag != null && this.cmbCabinetCode.Tag.ToString() != "")
            {
                FS.FrameWork.Models.NeuObject obj = this.conMgr.GetConstant("CaseCabinet", this.cmbCabinetCode.Tag.ToString());
                if (obj != null || obj.ID.ToString() != "")
                {
                    string[] str = obj.Memo.Split('-');
                    if (str.Length > 0)
                    {
                        this.txtCabinetBegin.Text = str[0].ToString();
                        this.txtCabinetEnd.Text = str[1].ToString();
                    }
                }

                for (int i = 0; i < this.fpSpread_Sheet.RowCount; i++)
                {
                    if (this.fpSpread_Sheet.Cells[i, 0].Text.ToString() != this.txtCaseNO.Text.PadLeft(10, '0').ToString())
                    {
                        continue;
                    }
                    if (this.CheckCabinet(this.txtCaseNO.Text.PadLeft(10, '0').ToString()) < 0)//判断选择库房柜格号是否正确
                    {
                        this.txtCabinetBegin.Text = "";
                        this.txtCabinetEnd.Text = "";
                        this.cmbCabinetCode.Tag = null;
                        return;
                    }
                    this.fpSpread_Sheet.Cells[i, 7].Text = this.cmbCabinetCode.Tag.ToString();
                    this.fpSpread_Sheet.Cells[i, 8].Text = this.cmbCabinetCode.Text.ToString();
                }
            }
        }

        private void CancelCaseNO()
        {
            frmCaseStoreCancel frm = new frmCaseStoreCancel();
            frm.ShowDialog();
        }

        private void ShowQuery()
        {
            frmCaseStoreQuery frmQ = new frmCaseStoreQuery();
            frmQ.ShowDialog();
        }

        private void btLend_Click(object sender, EventArgs e)
        {
            //FS.HISFC.Components.HealthRecord.CaseLend.frmLendCase frmLend = new FS.HISFC.Components.HealthRecord.CaseLend.frmLendCase();
            //frmLend.ShowDialog();
        }

        private void btBack_Click(object sender, EventArgs e)
        {
            //FS.HISFC.Components.HealthRecord.CaseLend.frmBackCase frmBack = new FS.HISFC.Components.HealthRecord.CaseLend.frmBackCase();
            //frmBack.ShowDialog();
        }
        /// <summary>
        /// 移除当前行
        /// </summary>
        private void RemoveRow()
        {
            this.fpSpread_Sheet.Rows[this.fpSpread_Sheet.ActiveRowIndex].Remove();
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.RemoveRow();
        }
        /// <summary>
        ///  从广东省病案系统3.0
        ///  根据库房号柜排号导入数据（初始化库房用）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface uploadMgr = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface();//广东省病案接口
            string strBegin = this.txtImportBegin.Text.Trim();
            string strEnd = this.txtImportEnd.Text.Trim();
            List<FS.HISFC.Models.HealthRecord.Case.CaseStore> list = new List<FS.HISFC.Models.HealthRecord.Case.CaseStore>();
            list = uploadMgr.GetPatientByCaseNo(strBegin, strEnd);
            if (list == null)
            {
                return;
            }
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在更新数据，请稍候！");
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.caseStoreMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            string CaseNum = "2";

            foreach (FS.HISFC.Models.HealthRecord.Case.CaseStore info in list)
            {
                info.IsVaild = true;
                info.CaseState = "1";
                if (this.cmbOper.Tag != null && this.cmbOper.Tag.ToString() != "")
                {
                    info.OperEnv.ID = this.cmbOper.Tag.ToString();
                }
                else
                {
                    info.OperEnv.ID = "009999";//不获取了直接赋值未管理员工号（rlq）
                }
                info.OperEnv.OperTime = this.conMgr.GetDateTimeFromSysDateTime();
                if (this.cmbStoreCode.Tag != null && this.cmbStoreCode.Tag.ToString()!="")
                {
                    info.Store.ID = this.cmbStoreCode.Tag.ToString();
                }
                if (this.cmbCabinetCode.Tag != null && this.cmbCabinetCode.Tag.ToString() != "")
                {
                    info.Cabinet.ID = this.cmbCabinetCode.Tag.ToString();
                }
                info.CaseMemo = "Import";
                info.Extend1 = "";
                info.Extend2 = "";
                info.Extend3 = "";
                info.Extend4 = "chengym";
                if (this.caseStoreMgr.InsertCaseStore(info) < 0)
                {
                    if (this.caseStoreMgr.UpdateCaseStore(info) < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存病案库房信息失败！", "提示");
                        return ;
                    }
                }

                if (this.caseStoreMgr.InsertCaseStoreDetail(info, CaseNum) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("保存病案库房明细信息失败！", "提示");
                    return ;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("保存成功！", "提示");

        }
    }
}
