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
    public partial class ucBarCode : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucBarCode()
        {
            InitializeComponent();
        }
        FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface uploadBaInterFace = new FS.HISFC.BizLogic.HealthRecord.UploadGuangDong.UploadBAInterface();
        FS.HISFC.BizProcess.Integrate.RADT patientMgr = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();
        FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();

        private int type = 1;

        /// <summary>
        /// 打印联数类型
        /// </summary>
        [Category("打印纸张联数"), Description("一联 四联")]
        public int Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        private List<FS.HISFC.Models.RADT.PatientInfo> patientList= new List<FS.HISFC.Models.RADT.PatientInfo>() ; 
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.Init();
            return base.OnInit(sender, neuObject, param);
        }
        private void Init()
        {

            //人员
            ArrayList personAl = this.personMgr.GetUserEmployeeAll();
            this.cmbOper.AddItems(personAl);
            this.cmbOper.Tag = this.conMgr.Operator.ID;
        }
        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return base.OnQuery(sender, neuObject);
        }
        private void Query()
        {
            DateTime dtBegin = this.dtpBeginTime.Value.Date;
            DateTime dtEnd = this.dtpEndTime.Value.Date;
            string operCode = "";
            if (this.cmbOper.Text.Trim() != "" && this.cmbOper.Text.Trim()!="系统管理员")
            {
                operCode = this.cmbOper.Text.Trim();
            }
            else
            {
                operCode = "ALL";
            }
            List<FS.HISFC.Models.RADT.PatientInfo> infoList = this.uploadBaInterFace.GetPatientBarCode(dtBegin, dtEnd, operCode);
            if (infoList == null)
            {
                return;
            }
            this.patientList = infoList;

            if (this.type == 4)
            {
                int totRecord = infoList.Count;
                int totpaper = 0;
                if (totRecord % 16 == 0)
                {
                    totpaper = totRecord / 16;
                }
                else
                {
                    totpaper = totRecord / 16 + 1;
                }
                this.lalbel1.Text = "共"+totpaper+"张";
            }

            this.AddPanel(infoList);
        }
        /// <summary>
        /// 逐个添加清单控件
        /// </summary>
        private void AddPanel(List<FS.HISFC.Models.RADT.PatientInfo> infoList)
        {
           

            int pointX = 0;
            int pointY = 10;

            int patienTOT=infoList.Count;
            int show=1;
            try
            {
                if (this.type == 1)
                {
                    FS.HISFC.Models.RADT.PatientInfo info = infoList[0] as FS.HISFC.Models.RADT.PatientInfo;
                    for (int i = 0; i < 2; i++)
                    {
                        ucPaitentBarCode ucBar = new ucPaitentBarCode();
                        ucBar.PatientInfo = info;
                        ucBar.Location = new Point(pointX, pointY);
                        pointY = pointY + ucBar.Height;
                        this.AddFocus(ucBar);
                        this.panMain.Controls.Add(ucBar);
                    }
                }
                else if (this.type == 4)
                {

                    foreach (FS.HISFC.Models.RADT.PatientInfo info in infoList)
                    {
                        if (show > 16)
                        {
                            return;
                        }

                        for (int i = 0; i < 2; i++)
                        {
                            ucPaitentBarCode ucBar = new ucPaitentBarCode();
                            ucBar.PatientInfo = info;
                            ucBar.Location = new Point(pointX, pointY);
                            if (show > 1 && show % 2 == 0 && i == 1)
                            {
                                pointY = pointY + ucBar.Height + 5;
                                pointX = 0;
                            }
                            else
                            {
                                pointX = pointX + ucBar.Width + 10;
                            }
                            this.AddFocus(ucBar);
                            this.panMain.Controls.Add(ucBar);
                        }


                        show++;
                    }
                }
                this.AddFocus(panMain);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void AddFocus(System.Windows.Forms.Control control)
        {
            control.Click += new EventHandler(c_Click);
            foreach (System.Windows.Forms.Control c in control.Controls)
            {
                c.Click += new EventHandler(c_Click);

                if (c.Controls.Count > 0)
                {
                    this.AddFocus(c);
                }
            }
        }

        void c_Click(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Control)sender).Focus();
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            int pointX = 0;
            int pointY = 10;

            int TOT = 0;//总记录
            int paperNum = 0;//共几张 
            if (this.type == 1)
            {
                foreach (FS.HISFC.Models.RADT.PatientInfo info in patientList)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        ucPaitentBarCode ucBar = new ucPaitentBarCode();
                        ucBar.PatientInfo = info;
                        ucBar.Location = new Point(pointX, pointY);
                        pointY = pointY + ucBar.Height;
                        this.AddFocus(ucBar);
                        this.panMain.Controls.Add(ucBar);
                        FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                        p.PrintPage(0, 0, panMain);
                    }
                }
            }
            else if (this.type == 4)
            {
                
                if (this.patientList.Count < 0)
                {
                    return -1;
                }
                TOT = this.patientList.Count;
                if (TOT % 16 == 0)
                {
                    paperNum = TOT / 16;
                }
                else
                {
                    paperNum = TOT / 16 + 1;
                }
                int PrintNum = 0;
                if (!this.checkBox1.Checked)
                {

                    for (int j = 0; j < paperNum; j++)
                    {
                        this.Print(j);
                        FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                        p.PrintPage(0, 0, this.panMain);
                    }
                }
                else
                {
                    PrintNum = FS.FrameWork.Function.NConvert.ToInt32(this.textBox1.Text);
                    if (PrintNum > paperNum)
                    {
                        return -1;
                    }
                    else
                    {
                        this.Print(PrintNum);
                        FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                        p.PrintPage(0, 0, this.panMain);
                    }
                }
            }
            return base.OnPrint(sender, neuObject);
        }

        private void Print(int paper)
        {
            int pointX = 0;
            int pointY = 10;

            

            int show = 1;
           
            int end=16*paper +16;
            int ftest = 16 * paper;
            if (end > this.patientList.Count)
            {
                end = this.patientList.Count;
            }
            if (ftest > this.patientList.Count)
            {
                paper--;
            }
            try
            {
                List<FS.HISFC.Models.RADT.PatientInfo> PrintList=new List<FS.HISFC.Models.RADT.PatientInfo>();
                for ( int first=16 * paper; first < end; first++)
                {
                    FS.HISFC.Models.RADT.PatientInfo info=new FS.HISFC.Models.RADT.PatientInfo();
                    info=this.patientList[first] as FS.HISFC.Models.RADT.PatientInfo;
                    PrintList.Add(info);
                }
                this.panMain.Controls.Clear();
                foreach (FS.HISFC.Models.RADT.PatientInfo info in PrintList)
                {
                    if (show > 16)
                    {
                       return;
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        ucPaitentBarCode ucBar = new ucPaitentBarCode();
                        ucBar.PatientInfo = info;
                        ucBar.Location = new Point(pointX, pointY);
                        if (show > 1 && show % 2 == 0 && i == 1)
                        {
                            pointY = pointY + ucBar.Height + 5;
                            pointX = 0;
                        }
                        else
                        {
                            pointX = pointX + ucBar.Width + 10;
                        }
                        this.AddFocus(ucBar);
                        this.panMain.Controls.Add(ucBar);
                    }
                    show++;
                }
                this.AddFocus(panMain);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                this.textBox1.ReadOnly = false;
            }
            else if (!this.checkBox1.Checked)
            {
                this.textBox1.ReadOnly = true;
            }
        }

    }
}
