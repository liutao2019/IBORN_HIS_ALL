using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GJLocal.HISFC.Components.OpGuide.RegistionExtend
{
    public partial class ucMain : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucMain()
        {
            InitializeComponent();
        }

        #region 变量与属性
        /// <summary>
        /// 是否可以填写
        /// </summary>
        private bool isFillBill = true;

        /// <summary>
        /// 是否可以填写
        /// </summary>
        public bool IsFillBill
        {
            get { return isFillBill; }
            set { isFillBill = value; }
        }

        /// <summary>
        /// 门诊流水号
        /// </summary>
        private string clinc_code = "";

        /// <summary>
        /// 门诊流水号
        /// </summary>
        public string Clinc_code
        {
            get { return clinc_code; }
            set { clinc_code = value; }
        }

        /// <summary>
        /// 挂号日期
        /// </summary>
        private string dtReg = "";

        /// <summary>
        /// 挂号日期
        /// </summary>
        public string DtReg
        {
            get { return dtReg; }
            set 
            { 
                dtReg = value;
                this.lbDateTimeReg.Text = "日期：" + dtReg;
            }
        }

        #region 业务变量
        FS.HISFC.BizLogic.Registration.GJLocal.GJOutPatientInfoRegister gjMgr
                    = new FS.HISFC.BizLogic.Registration.GJLocal.GJOutPatientInfoRegister();
        #endregion 
        #endregion

        /// <summary>
        /// 清除
        /// </summary>
        public void Clean()
        {
            this.cbD.Checked = false;
            this.cbG.Checked = false;
            this.cbN.Checked = false;
            this.ucConsultation1.Clean();
            this.ucDentist11.Clean();
            this.ucGeneral11.Clean();
            this.ucNerve11.Clean();
            this.ucBackPage1.Clean();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.isFillBill = true;
            this.Init();
            base.OnLoad(e);
        }

        /// <summary>
        /// 整体查询函数，外接总入口
        /// </summary>
        /// <param name="clinic_code">门诊流水号</param>
        /// <param name="isFillBill">true 填单 false 查看</param>
        public void Query(string clinic_code1 ,bool isFillBill)
        {
            this.isFillBill = isFillBill;
            this.clinc_code = clinic_code1;
            this.Init();
            this.SetPatientInfo();
        }

        private void SetPatientInfo()
        {
            if (string.IsNullOrEmpty(this.clinc_code))
            {
                MessageBox.Show("请选择一位患者！");
                return;
            }
            this.ucConsultation1.Clinc_code = this.clinc_code;
            this.ucDentist11.Clinic_code = this.clinc_code;
            this.ucGeneral11.Clinic_code = this.clinc_code;
            this.ucNerve11.Clinic_code = this.clinc_code;
            this.ucBackPage1.Clinic_code = this.clinc_code;
            this.lbID.Text = "ID:" + this.clinc_code;
            this.SetValue();
        }

        private void SetValue()
        {
            this.ucConsultation1.SetValue();
            System.Collections.Hashtable hsD = gjMgr.QueryGJRegisterInfo(this.clinc_code, "D1");
            if (hsD.Count > 0)
            {
                this.ucDentist11.SetValue(hsD);
                this.cbD.Checked = true;
            }
            System.Collections.Hashtable hsG = gjMgr.QueryGJRegisterInfo(this.clinc_code, "G1");
            if (hsG.Count > 0)
            {
                this.ucGeneral11.SetValue(hsG);
                this.cbG.Checked = true;
            }
            System.Collections.Hashtable hsN = gjMgr.QueryGJRegisterInfo(this.clinc_code, "N1");
            if (hsN.Count > 0)
            {
                this.ucNerve11.SetValue(hsN);
                this.cbN.Checked = true;
            }
            System.Collections.Hashtable hsBG = gjMgr.QueryGJRegisterInfo(this.clinc_code, "BG");
            this.ucBackPage1.SetValue(hsBG);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            this.ucConsultation1.Dock = DockStyle.Fill;
            this.ucDentist11.Dock = DockStyle.Fill;
            this.ucGeneral11.Dock = DockStyle.Fill;
            this.ucNerve11.Dock = DockStyle.Fill;
            this.ucBackPage1.Dock = DockStyle.Fill;
            this.splitContainer2.Dock = DockStyle.Fill;

            this.lbCurrentPage.Text = "1";
            this.btLastPage.Enabled = false;
            this.PageFuntion();

            if (this.isFillBill)
            {
                this.cbD.Enabled = true;
                this.cbG.Enabled = true;
                this.cbN.Enabled = true;
                this.btSave.Enabled = true;
            }
            else
            {
                this.cbD.Enabled = false;
                this.cbG.Enabled = false;
                this.cbN.Enabled = false;
                this.btSave.Enabled = false;
            }
        }

        /// <summary>
        /// 设置当前显示页
        /// </summary>
        /// <param name="pageCerrent"></param>
        private void SetPageShow(int pageCerrent)
        {
            #region
            if (pageCerrent == 1)
            {
                if (this.cbD.Checked)
                {
                    this.SetPage(true, true, false, false, false);
                }
                else if (this.cbG.Checked)
                {
                    this.SetPage(true, false, true, false, false);
                }
                else if (this.cbN.Checked)
                {
                    this.SetPage(true, false, false, true, false);
                }
                else
                {
                    this.SetPage(true, false, false, false, false);
                }
            }
            #endregion
            #region
            if (pageCerrent == 2)
            {
                if (this.cbD.Checked)
                {
                    if (this.cbG.Checked)
                    {
                        this.SetPage(true, false, true, false, false);
                    }
                    else if (this.cbN.Checked)
                    {
                        this.SetPage(true, false, false, true, false);
                    }
                    else
                    {
                        this.SetPage(false, false, false, false, true);
                    }
                }
                else if (this.cbG.Checked)
                {
                    if (this.cbN.Checked)
                    {
                        this.SetPage(true, false, false, true, false);
                    }
                    else
                    {
                        this.SetPage(false, false, false, false, true);
                    }
                }
                else
                {
                    this.SetPage(false, false, false, false, true);
                }
            }
            #endregion
            #region
            if (pageCerrent == 3)
            {
                if (!this.cbD.Checked || !this.cbG.Checked || !this.cbN.Checked)
                {
                    this.SetPage(false, false, false, false, true);
                }
                else
                {
                    this.SetPage(true, false, false, true, false);
                }
            }
            #endregion
            #region
            if (pageCerrent == 4)
            {
                this.SetPage(false, false, false, false, true);
            }
            #endregion
        }

        private void SetPage(bool split2, bool d, bool g, bool n, bool bg)
        {
            this.splitContainer2.Visible = split2;
            this.ucDentist11.Visible = d;
            this.ucGeneral11.Visible = g;
            this.ucNerve11.Visible = n;
            this.ucBackPage1.Visible = bg;
            if (split2 && !d && !g && !n)
            {
                this.lbTypeShow.Text = "";
            }
            else if (d)
            {
                this.lbTypeShow.Text = "D";
            }
            else if (g)
            {
                this.lbTypeShow.Text = "G";
            }
            else if (n)
            {
                this.lbTypeShow.Text = "N";
            }
        }

        /// <summary>
        /// 翻页函数
        /// </summary>
        private void PageFuntion()
        {
            int pageCerrent =FS.FrameWork.Function.NConvert.ToInt32(this.lbCurrentPage.Text);
            int pageCount = 1;
            if (this.cbD.Checked)
            {
                pageCount = pageCount + 1;
            }
            if (this.cbG.Checked)
            {
                pageCount = pageCount + 1;
            }
            if (this.cbN.Checked)
            {
                pageCount = pageCount + 1;
            }
            this.lbHoldPage.Text = pageCount.ToString();

            this.btLastPage.Enabled = true;
            this.btNextPage.Enabled = true;
            if (pageCerrent == 1)
            {
                this.btLastPage.Enabled = false;
            }
            if (pageCerrent >= pageCount)
            {
                pageCerrent = pageCount;
                this.btNextPage.Enabled = false;
            }
            this.SetPageShow(pageCerrent);
        }

        private int Save()
        {
            if (this.isFillBill)
            {
                if (string.IsNullOrEmpty(this.clinc_code))
                {
                    MessageBox.Show("请先选择患者！");
                    return -1;
                }
                System.Collections.ArrayList alSave = new System.Collections.ArrayList();
                if (this.cbD.Checked)
                {
                    System.Collections.ArrayList al1 = this.ucDentist11.GetValue();
                    alSave.AddRange(al1);
                }
                if (this.cbG.Checked)
                {
                    System.Collections.ArrayList al2 = this.ucGeneral11.GetValue();
                    alSave.AddRange(al2);
                }
                if (this.cbN.Checked)
                {
                    System.Collections.ArrayList al3 = this.ucNerve11.GetValue();
                    alSave.AddRange(al3);
                }
                System.Collections.ArrayList al4 = this.ucBackPage1.GetValue();
                alSave.AddRange(al4);
                if(gjMgr.DeleteGJRegisterInfo(this.clinc_code)<0)
                {
                    MessageBox.Show("保存失败:" + gjMgr.Err);
                    return -1;
                }
                if (gjMgr.InsertGJRegisterInfo(alSave) < 0)
                {
                    MessageBox.Show("保存失败:" + gjMgr.Err);
                    return -1;
                }
                else
                {
                    MessageBox.Show("保存成功！");
                    return 1;
                }
            }
            return 1;
        }

        /// <summary>
        /// 向上翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btLastPage_Click(object sender, EventArgs e)
        {
            int pageCerrent = FS.FrameWork.Function.NConvert.ToInt32(this.lbCurrentPage.Text);
            pageCerrent = pageCerrent - 1;
            this.lbCurrentPage.Text = pageCerrent.ToString();
            this.PageFuntion();
            if (this.lbCurrentPage.Text == "1")
            {
                this.btLastPage.Enabled = false;
                return;
            }
        }

        /// <summary>
        /// 向下翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btNextPage_Click(object sender, EventArgs e)
        {
            int pageCerrent = FS.FrameWork.Function.NConvert.ToInt32(this.lbCurrentPage.Text);
            if (this.lbCurrentPage.Text == this.lbHoldPage.Text)
            {
                this.btNextPage.Enabled = false;
                return;
            }
            pageCerrent = pageCerrent + 1;
            this.lbCurrentPage.Text = pageCerrent.ToString();
            this.PageFuntion();
            if (this.lbCurrentPage.Text == this.lbHoldPage.Text)
            {
                this.btNextPage.Enabled = false;
                return;
            }
        }

        private void cbD_CheckedChanged(object sender, EventArgs e)
        {
            this.PageFuntion();
        }

        private void cbG_CheckedChanged(object sender, EventArgs e)
        {
            this.PageFuntion();
        }

        private void cbN_CheckedChanged(object sender, EventArgs e)
        {
            this.PageFuntion();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            this.Save();
        }
    }
}
