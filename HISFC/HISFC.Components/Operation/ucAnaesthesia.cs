using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Operation;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [功能描述: 麻醉安排控件]<br></br>
    /// [创 建 者: 王铁全]<br></br>
    /// [创建时间: 2006-12-11]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucAnaesthesia : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucAnaesthesia()
        {
            InitializeComponent();
        }

        #region 字段
        private ArrayList alApplys;

        //{4F4C0095-4E5A-4e48-AD22-D38A2894A31F}
        /// <summary>
        /// 科室分类表维护管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager deptStatMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        private FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();
        //{30819195-8798-446e-84C7-2B331A5ADDD3}feng.ch是否合并手术安排与麻醉安排
        /// <summary>
        /// 用于直接麻醉安排时候检索麻醉安排信息
        /// 开始时间
        /// </summary>
        private DateTime beginTime = new DateTime();
        /// <summary>
        /// 结束时间
        /// </summary>
        private DateTime endTime = new DateTime();
        /// <summary>
        /// 是否合并手术安排与麻醉安排
        /// </summary>
        private bool isJoinUc = false;
        /// <summary>
        /// 申请实体
        /// </summary>
        private List<OperationAppllication> apply = new List<OperationAppllication>();
        //{70B49442-8CA3-4fe7-95F0-D54E24CE2374}feng.ch
        /// <summary>
        /// 麻醉安排是否必须手术安排之后
        /// </summary>
        private bool isMustAfterArrangement = true;
        #endregion
        #region 属性
        //{30819195-8798-446e-84C7-2B331A5ADDD3}feng.ch
        public bool IsJoinUc
        {
            get
            {
                return isJoinUc;
            }
            set
            {
                isJoinUc = value;
            }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime
        {
            get
            {
                return beginTime;
            }
            set
            {
                beginTime = value;
            }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
            }
        }
        public List<OperationAppllication> Apply
        {
            get
            {
                return apply;
            }
            set
            {
                apply = value;
            }
        }
        //{70B49442-8CA3-4fe7-95F0-D54E24CE2374}feng.ch
        /// <summary>
        /// 麻醉安排是否必须手术安排之后
        /// </summary>
        [Category("控件设置"), Description("麻醉安排是否必须手术安排之后")]
        public bool IsMustAfterArrangement
        {
            get
            {
                return isMustAfterArrangement;
            }
            set
            {
                isMustAfterArrangement = value;
            }
        }

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #endregion
        #region 方法

        /// <summary>
        /// 刷新手术申请列表
        /// </summary>
        /// <returns></returns>
        public int RefreshApplys()
        {
            this.ucAnaesthesiaSpread1.Reset();                    
            //开始时间
            DateTime beginTime = this.dateTimePicker1.Value.Date;
            //结束时间
            DateTime endTime = this.dateTimePicker1.Value.Date.AddDays(1);
            #region 时间赋值
            //{30819195-8798-446e-84C7-2B331A5ADDD3}feng.ch
            if (this.isJoinUc)
            {
                beginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.beginTime);
                endTime = FS.FrameWork.Function.NConvert.ToDateTime(this.endTime);
            }
            #endregion
            //FS.HISFC.Components.Interface.Classes.Function.ShowWaitForm("正在载入数据,请稍后...");
            Application.DoEvents();
            try
            {
                this.ucAnaesthesiaSpread1.Reset();
                //{70B49442-8CA3-4fe7-95F0-D54E24CE2374}feng.ch
                if (!this.isMustAfterArrangement)
                {
                    //如果不必要非要手术安排后进行麻醉安排的话直接取申请信息
                    alApplys = Environment.OperationManager.GetOpsAppList(beginTime, endTime,"1");
                }
                else
                {
                    //必须手术安排以后的取手术安排信息
                    alApplys = Environment.OperationManager.GetOpsAppList(beginTime, endTime);
                }
                if (alApplys != null)
                {
                    foreach (OperationAppllication apply in alApplys)
                    {
                        //{25E1FC1A-66A0-4e40-9236-9CC6710A5704} 手术室麻醉室对

                        #region 载入手术室麻醉室关系，进行过滤；只能过滤出本科室上面对应的手术室的申请
                        ArrayList alAnesDepts = this.deptStatMgr.LoadChildren("10", apply.ExeDept.ID, 1);
                        if (alAnesDepts == null)
                        {
                            MessageBox.Show("查找科室对应关系时出错：" + this.deptStatMgr.Err);
                            return -1;
                        }
                        if (alAnesDepts.Count == 0)
                        {
                            //FS.HISFC.BizProcess.Integrate.Manager depMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                            apply.ExeDept.Name = deptStatMgr.GetDepartment(apply.ExeDept.ID).Name;
                            MessageBox.Show("手术科室：“" + apply.ExeDept.Name + "”找不到与麻醉室的对应关系，请在科室结构树中维护！");
                            return -1;
                        }
                        foreach (FS.HISFC.Models.Base.DepartmentStat deptStat in alAnesDepts)
                        {
                            #region {2F58330D-0BEC-4a68-AE06-6C2868CFE545}
                            //{E4C275E8-6E12-4a42-A60A-0EB9A8CB52BD}
                            if (deptStat.DeptCode == (this.dataManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID)
                            {
                                this.ucAnaesthesiaSpread1.AddOperationApplication(apply);
                                break;
                            }
                            //{30819195-8798-446e-84C7-2B331A5ADDD3}feng.ch
                            if (this.isJoinUc)
                            {
                                if (deptStat.PardepCode == (this.dataManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID)
                                {
                                    this.apply.Add(apply);
                                    break;
                                }
                            }
                            #endregion
                        }
                        #endregion
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("生成手术申请信息出错!" + e.Message, "提示");
                return -1;
            }

            //FS.HISFC.Components.Interface.Classes.Function.HideWaitForm();
            //if (fpSpread1_Sheet1.RowCount > 0)
            //{
            //    FarPoint.Win.Spread.LeaveCellEventArgs e = new FarPoint.Win.Spread.LeaveCellEventArgs
            //        (new FarPoint.Win.Spread.SpreadView(fpSpread1), 0, 0, 0, (int)Cols.anaeType);
            //    fpSpread1_LeaveCell(fpSpread1, e);
            //fpSpread1.Focus();
            //fpSpread1_Sheet1.SetActiveCell(0, (int)Cols.anaeType, true);
            //}

            return 0;
        }
        #endregion

        #region 事件

        protected override int OnQuery(object sender, object neuObject)
        {
            this.RefreshApplys();
            this.Fileter();
            
            return base.OnQuery(sender, neuObject);
        }

        private void Fileter() 
        {
            if (this.toolBarService.GetToolButton("未安排").CheckState == System.Windows.Forms.CheckState.Checked)
            {
                this.ucAnaesthesiaSpread1.Filter = ucAnaesthesiaSpread.EnumFilter.NotYet;
            }
            else
            {
                this.ucAnaesthesiaSpread1.Filter = ucAnaesthesiaSpread.EnumFilter.Already;
            }
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.ucAnaesthesiaSpread1.Save();
            this.Fileter();
            return base.OnSave(sender, neuObject);
        }
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("已安排", "已安排", FS.FrameWork.WinForms.Classes.EnumImageList.D打印输液卡, true, false, null);
            this.toolBarService.AddToolButton("未安排", "未安排", FS.FrameWork.WinForms.Classes.EnumImageList.D打印执行单, true, false, null);
            this.toolBarService.AddToolButton("全选", "全选", FS.FrameWork.WinForms.Classes.EnumImageList.Q全选, true, false, null);
            this.toolBarService.AddToolButton("全不选", "全不选", FS.FrameWork.WinForms.Classes.EnumImageList.Q全不选, true, false, null);
            this.toolBarService.GetToolButton("未安排").CheckState = System.Windows.Forms.CheckState.Checked;
            return this.toolBarService;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.ucAnaesthesiaSpread1.Date = this.dateTimePicker1.Value;
            this.ucAnaesthesiaSpread1.Print();
            return base.OnPrint(sender, neuObject);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "已安排")
            {
                this.toolBarService.GetToolButton("已安排").CheckState = System.Windows.Forms.CheckState.Checked;
           
                this.toolBarService.GetToolButton("未安排").CheckState = System.Windows.Forms.CheckState.Unchecked;

                this.ucAnaesthesiaSpread1.Filter = ucAnaesthesiaSpread.EnumFilter.Already; 
            }
            else if (e.ClickedItem.Text == "未安排")
            {
                this.toolBarService.GetToolButton("未安排").CheckState = System.Windows.Forms.CheckState.Checked;
              
                this.toolBarService.GetToolButton("已安排").CheckState = System.Windows.Forms.CheckState.Unchecked;

                this.ucAnaesthesiaSpread1.Filter = ucAnaesthesiaSpread.EnumFilter.NotYet;
            }
            else if (e.ClickedItem.Text == "全选")
            {
                this.AllSelect(true);
            }
            else if (e.ClickedItem.Text == "全不选")
            {
                this.AllSelect(false);
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        public void AllSelect(bool isSelect)
        {
            this.ucAnaesthesiaSpread1.AllSelect(isSelect);
        }

        public override int Export(object sender, object neuObject)
        {
            return this.ucAnaesthesiaSpread1.Export();
        }
        #endregion

        private void ucAnaesthesiaSpread1_applictionSelected(object sender, OperationAppllication e)
        {
            if (e != null)
            {
                this.ucArrangementInfo1.OperationApplication = e;
            }
          
        }
    }
}
