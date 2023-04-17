using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Extend.ZDWY
{
    /// <summary>
    /// [功能描述: 提供界面供用户选择申请、计划生成的方式等]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-6]<br></br>
    /// </summary>
    public partial class frmSetPlan : Form
    {
        public frmSetPlan()
        {
            InitializeComponent();
            this.nbtOK.Click += new EventHandler(nbtOK_Click);
            this.nbtCancel.Click += new EventHandler(nbtCancel_Click);
            this.nbtDelete.Click += new EventHandler(nbtDelete_Click);
            this.nbtNew.Click += new EventHandler(nbtNew_Click);
            this.ndtpFomulaEndTime.ValueChanged += new EventHandler(ndtpFomulaEndTime_ValueChanged);
            this.ndtpFomulaBeginTime.ValueChanged += new EventHandler(ndtpFomulaBeginTime_ValueChanged);
            this.ntbRate.TextChanged += new EventHandler(ntbRate_TextChanged);
            this.ntbPlanDays.TextChanged += new EventHandler(ntbPlanDays_TextChanged);
        }

        private void GenerateFomulaString()
        {
            TimeSpan ts = this.ndtpFomulaEndTime.Value -this.ndtpFomulaBeginTime.Value;
            int ConsumuDay = ts.Days;
            this.nlbNewFomula.Text = "(cksl/" + ConsumuDay.ToString() + ")*" + this.ntbPlanDays.Text + "*" + this.ntbRate.Text;
        }

        void ntbPlanDays_TextChanged(object sender, EventArgs e)
        {
            decimal planDays = 0m;
            if (!decimal.TryParse(this.ntbPlanDays.Text, out planDays))
            {
                Function.ShowMessage("输入的比率为为数字，请修改！", MessageBoxIcon.Information);
            }
            this.GenerateFomulaString();
        }


        void ntbRate_TextChanged(object sender, EventArgs e)
        {
            decimal rate = 0m;
            if (!decimal.TryParse(this.ntbRate.Text, out rate))
            {
                Function.ShowMessage("输入的比率为为数字，请修改！", MessageBoxIcon.Information);
            }
            this.GenerateFomulaString();
        }

        void ndtpFomulaBeginTime_ValueChanged(object sender, EventArgs e)
        {
            if (this.ndtpFomulaEndTime.Value < this.ndtpFomulaBeginTime.Value)
            {
                Function.ShowMessage("结束日期不能小于开始日期，请修改！", MessageBoxIcon.Information);
                return;
            }
            this.GenerateFomulaString();
        }

        void ndtpFomulaEndTime_ValueChanged(object sender, EventArgs e)
        {
            this.GenerateFomulaString();
        }

        void nbtNew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否新增当前公式：" + this.nlbNewFomula.Text, "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                FS.FrameWork.Models.NeuObject neuNewPlanFomula = new FS.FrameWork.Models.NeuObject();
                TimeSpan ts = this.ndtpFomulaBeginTime.Value - this.ndtpFomulaEndTime.Value;
                neuNewPlanFomula.User01 = ts.Days.ToString();
                neuNewPlanFomula.User02 = this.ntbRate.Text;
                neuNewPlanFomula.User03 = this.ntbPlanDays.Text;
                neuNewPlanFomula.Name = this.nlbNewFomula.Text;
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                int param = localPlanBizlogic.InsertOnePlanFomula(neuNewPlanFomula);
                if (param <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            Function.ShowMessage("增加成功", MessageBoxIcon.Information);
            this.InitFomula();
        }

        void nbtDelete_Click(object sender, EventArgs e)
        {
            if (this.ncmbPlanList1.SelectedItem is FS.FrameWork.Models.NeuObject)
            {
              if (MessageBox.Show("是否删除当前公式：" + (this.ncmbPlanList1.SelectedItem as FS.FrameWork.Models.NeuObject).Name, "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    int param = localPlanBizlogic.DeleteOnePlanFomula((this.ncmbPlanList1.SelectedItem as FS.FrameWork.Models.NeuObject).ID);
                    if (param <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
            }
            Function.ShowMessage("删除成功", MessageBoxIcon.Information);
            this.InitFomula();
        }
         
        private string curPriveDept ="";
        private FS.HISFC.Models.Pharmacy.EnumDrugStencil curOpenType =  FS.HISFC.Models.Pharmacy.EnumDrugStencil.Plan;
        private LocalPlanBizlogic localPlanBizlogic = new LocalPlanBizlogic();
       


        public delegate void SetCompletedHander(CreatePlanType type, string formula, params string[] param);
        public SetCompletedHander SetCompletedEven;

        public enum CreatePlanType
        {
            Consume,
            Warning,
            Formula
        }

        public void Init(string deptNO, FS.HISFC.Models.Pharmacy.EnumDrugStencil openType)
        {
            this.curOpenType = openType;
            this.curPriveDept = deptNO;
            this.InitTime();
            this.InitDays();
            this.InitConstant();
            this.InitType();
            this.InitFomula();
        }

        private void InitFomula()
        {
            ArrayList allFomula = localPlanBizlogic.GetAllPlanFomula();
            if (allFomula == null || allFomula.Count == 0)
            {
                return;
            }

            this.ntbRate.Text = "1.0";
            this.ntbPlanDays.Text = "30";
            this.ncmbPlanList.Items.Clear();
            this.ncmbPlanList1.Items.Clear();
            this.ncmbPlanList.AddItems(allFomula);
            this.ncmbPlanList1.AddItems(allFomula);
            this.ncmbPlanList.SelectedIndex = 0;
            this.ncmbPlanList1.SelectedIndex = 0;
            this.ndtpFomulaEndTime.Value = this.ndtpFomulaEndTime.Value;
            this.ndtpFomulaBeginTime.Value = this.ndtpFomulaEndTime.Value.AddDays(-60);
            this.GenerateFomulaString();
        }

        private void InitTime()
        {
            this.ndtpEndTime.Value = this.ndtpEndTime.Value.Date;
            this.ndtpBeginTime.Value = this.ndtpEndTime.Value.AddDays(-7);
        }

        private void InitDays()
        {
            this.ntxtLowDays.Text = "7";
            this.ntxtUpDays.Text = "10";
        }

        private void InitConstant()
        {
            FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList alDrugType = constantMgr.GetAllList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE);
            if (alDrugType != null)
            {
                this.ncmbDrugTypeConsume.AddItems(alDrugType);
                this.ncmbDrugTypeWarning.AddItems(alDrugType);
            }

            FS.SOC.HISFC.BizLogic.Pharmacy.Constant phaConstantMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Constant();
            if (!string.IsNullOrEmpty(curPriveDept))
            {
                ArrayList alDrugStencil = phaConstantMgr.QueryDrugStencilList(curPriveDept, curOpenType);
                if (alDrugStencil != null)
                {
                    //模板
                    ArrayList alOpen = new ArrayList();
                    foreach (FS.HISFC.Models.Pharmacy.DrugStencil info in alDrugStencil)
                    {
                        alOpen.Add(info.Stencil);
                    }

                    this.ncmbDrugStencilConsume.AddItems(alOpen);
                    this.ncmbDrugStencilWarning.AddItems(alOpen);
                }
            }
        }

        private void InitType()
        {
            string type = SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PharmacySetting.xml", "Plan", "CreateType", "0");
            try
            {
                CreatePlanType t = (CreatePlanType)(FS.FrameWork.Function.NConvert.ToInt32(type));
                if (t == CreatePlanType.Consume)
                {
                    this.neuTabControl1.SelectedTab = this.tpConsume;
                }
                else if (t == CreatePlanType.Warning)
                {
                    this.neuTabControl1.SelectedTab = this.tpWarning;
                }
                else if (t == CreatePlanType.Formula)
                {
                    this.neuTabControl1.SelectedTab = this.tpFormula;
                }
            }
            catch { }

        }

        private bool CheckValid()
        {
            if (this.neuTabControl1.SelectedTab == this.tpConsume)
            {

                int lowDays = 0;
                if (!int.TryParse(this.ntxtLowDays.Text, out lowDays))
                {
                    MessageBox.Show("下限天数不是整数，请更改", "错误>>");
                    return false;
                }
                int upDays = 0;
                if (!int.TryParse(this.ntxtUpDays.Text, out upDays))
                {
                    MessageBox.Show("上限天数不是整数，请更改", "错误>>");
                    return false;
                }

                if (this.ndtpBeginTime.Value.Date >= this.ndtpEndTime.Value.Date)
                {
                    MessageBox.Show("消耗开始日期大于结束日期，请更改", "错误>>");
                    return false;
                }
                if (this.ndtpEndTime.Value.Date > DateTime.Now.Date)
                {
                    MessageBox.Show("消耗结束日期大于今天，请更改", "错误>>");
                    return false;
                }              
            }

            return true;
        }

        void nbtCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void nbtOK_Click(object sender, EventArgs e)
        {
            if (this.CheckValid())
            {
                if (this.SetCompletedEven != null)
                {
                    string drugType = "";
                   
                    string drugStencil = "";
                   
                    CreatePlanType type = CreatePlanType.Formula;
                    if (this.neuTabControl1.SelectedTab == this.tpConsume)
                    {
                        if (this.ncmbDrugTypeConsume.Tag != null)
                        {
                            drugType = this.ncmbDrugTypeConsume.Tag.ToString();
                        }
                        if (this.ncmbDrugStencilConsume.Tag != null)
                        {
                            drugStencil = this.ncmbDrugStencilConsume.Tag.ToString();
                        }
                        type = CreatePlanType.Consume;
                        //GetPlan(string deptNO, DateTime consumeBeginTime, DateTime consumeEndTime, int lowDays, int upDays, string drugType, string stencilNO)
                        this.SetCompletedEven(type, "日消耗", this.curPriveDept, this.ndtpBeginTime.Value.Date.ToString(), this.ndtpEndTime.Value.Date.ToString(), this.ntxtLowDays.Text, this.ntxtUpDays.Text, drugType, drugStencil);
                    }
                    else if (this.neuTabControl1.SelectedTab == this.tpWarning)
                    {
                        if (this.ncmbDrugTypeWarning.Tag != null)
                        {
                            drugType = this.ncmbDrugTypeWarning.Tag.ToString();
                        }
                        if (this.ncmbDrugStencilWarning.Tag != null)
                        {
                            drugStencil = this.ncmbDrugStencilWarning.Tag.ToString();
                        }
                        type = CreatePlanType.Warning;
                        this.SetCompletedEven(type, "警戒线", this.curPriveDept, drugType, drugStencil);
                    }
                    else if (this.neuTabControl1.SelectedTab == this.tpFormula)
                    {
                        if (this.ncmbPlanList.SelectedItem != null)
                        {
                            FS.FrameWork.Models.NeuObject neuPlanFomula = this.ncmbPlanList.SelectedItem as FS.FrameWork.Models.NeuObject;
                            this.SetCompletedEven(type, "自定义",this.curPriveDept ,neuPlanFomula.User01, neuPlanFomula.User02, neuPlanFomula.User03);
                        }
                    }

                    SOC.Public.XML.SettingFile.SaveSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PharmacySetting.xml", "Plan", "CreateType", ((int)type).ToString());

                }

                this.Close();
            }
            
            

        }
    }
}
