using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.DrugStore.ZhuHai.ZDWY.Outpatient
{
    /// <summary>
    /// beijiao LED
    /// </summary>
    public partial class frmOutpatientLEDShow : Form, SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IOutpatientLED
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmOutpatientLEDShow()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(frmOutpatientLEDShow_FormClosed);
        }

        void frmOutpatientLEDShow_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.isOpen = false;
        }

        /// <summary>
        /// 当前操作的科室
        /// </summary>
        private string curDept = string.Empty;
        public string CurDept
        {
            get { return curDept; }
            set {curDept = value;}
        }

        /// <summary>
        /// 当前操作的终端
        /// </summary>
        private string curTerminal = string.Empty;
        public string CurTerminal
        {
            get { return curTerminal; }
            set { curTerminal = value; }
        }

        public string screenSizeX = string.Empty;

        public string screenSizeY = string.Empty;

        //是否在叫号
        private bool isOperBusy = false;

        public bool IsOperBusy
        {
            get { return this.isOperBusy; }
            set { this.isOperBusy = true; }
        }

        //当前操作的处方
        FS.HISFC.Models.Pharmacy.DrugRecipe curDrugRecipe = new FS.HISFC.Models.Pharmacy.DrugRecipe();

        FS.HISFC.BizLogic.Pharmacy.DrugStore drugStorMgr = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

        FS.SOC.Local.DrugStore.ZhuHai.ZDWY.Outpatient.Bizlogic.DrugStoreAsign drugStoreAsignMGR = new FS.SOC.Local.DrugStore.ZhuHai.ZDWY.Outpatient.Bizlogic.DrugStoreAsign();

        public FS.HISFC.Models.Pharmacy.DrugRecipe CurDrugRecipe
        {
            get { return this.curDrugRecipe; }
            set { this.curDrugRecipe = value; }
        }

        FS.SOC.Local.DrugStore.ZhuHai.ZDWY.Outpatient.Bizlogic.DrugStoreAsign drugStoreAsignMgr = new FS.SOC.Local.DrugStore.ZhuHai.ZDWY.Outpatient.Bizlogic.DrugStoreAsign();


        private bool isOpen = false;
        private void frmDisplay_Load(object sender, EventArgs e)
        {
            this.isOpen = true;

            FS.HISFC.BizProcess.Integrate.Manager controlMgr = new FS.HISFC.BizProcess.Integrate.Manager();

            #region 设定显示大小

            //string screenSize = controlMgr.QueryControlerInfo("900004");
            //screenSizeX = controlMgr.QueryControlerInfo("900008");
            //screenSizeY = controlMgr.QueryControlerInfo("900009");
            //this.Location = new Point(FS.FrameWork.Function.NConvert.ToInt32(screenSize) + 1, 1);
            //this.Size = new Size(FS.FrameWork.Function.NConvert.ToInt32(screenSizeX), FS.FrameWork.Function.NConvert.ToInt32(screenSizeY));

            if (Screen.AllScreens.Length > 1)
            {
                if (Screen.AllScreens[0].Primary)
                {
                    this.DesktopBounds = Screen.AllScreens[1].Bounds;
                }
                else
                {
                    this.DesktopBounds = Screen.AllScreens[0].Bounds;
                }
            }
            else
            {
                if (FS.FrameWork.WinForms.Classes.Function.IsManager())
                {
                    this.DesktopBounds = Screen.AllScreens[0].Bounds;
                }
            }
            this.lblFirtPatient1.Text = string.Empty;
            this.lblFirstPatient2.Text = string.Empty;
            this.lblFirstPatient3.Text = string.Empty;
            this.lblWindow.Location = new Point((this.Width - this.lblWindow.Width)/2, this.lblWindow.Location.Y);
            this.nlbWait.Location = new Point((this.Width - this.nlbWait.Width)/2, this.nlbWait.Location.Y);
            this.ClearScrean();
            #endregion
        }

        private void ClearScrean()
        {
            foreach (Control c in this.neuPanel1.Controls)
            {
                c.Text = string.Empty;
            }
            foreach (Control c in this.neuPanel4.Controls)
            {
                c.Text = string.Empty;
            }
            this.nlbCallPatient.Text = string.Empty;

        }

        private void frmDisplay_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }

        #region IOutpatientLEDShow 成员

        public void SetShowData(List<FS.HISFC.Models.Pharmacy.DrugRecipe> drugRecipeList)
        {
            FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

            if (drugRecipeList != null && drugRecipeList.Count > 0)
            {
                FS.HISFC.Models.Pharmacy.DrugRecipe recipeWindow = drugRecipeList[0] as FS.HISFC.Models.Pharmacy.DrugRecipe;
               

                this.CurDept = recipeWindow.StockDept.ID;
                this.CurTerminal = recipeWindow.SendTerminal.ID;
                this.ShowGetUnSendPatint(recipeWindow);
            }
        }

        #endregion

        #region IOutpatientLED 成员

        /// <summary>
        /// 自动刷新显示数据，从发药窗口的树列表中取数据
        /// </summary>
        /// <param name="listDrugRecipe">处方调剂信息</param>
        /// <param name="operBusying">主窗口操作是否正忙</param>
        /// <returns>-1失败</returns>
        public int AutoShowData(List<FS.HISFC.Models.Pharmacy.DrugRecipe> listDrugRecipe, bool operBusying)
        {
            //如果操作忙，则返回，启用同步机制时用
            if (listDrugRecipe == null || listDrugRecipe.Count == 0)
            {
                return 1;
            }
            FS.HISFC.Models.Pharmacy.DrugRecipe recipeWindow = listDrugRecipe[0] as FS.HISFC.Models.Pharmacy.DrugRecipe;
            this.CurDept = recipeWindow.StockDept.ID;
            this.CurTerminal = recipeWindow.SendTerminal.ID;

            this.isOperBusy = true;
            this.ShowGetUnSendPatint(listDrugRecipe[0] as FS.HISFC.Models.Pharmacy.DrugRecipe);
            this.isOperBusy = false;
            return 1;
        }

        /// <summary>
        /// 实现大屏设置：暂停，开关等
        /// </summary>
        /// <returns></returns>
        public int SetLED()
        {
            if (!this.isOpen)
            {
                if (Screen.AllScreens.Length <= 1)
                {
                    if (MessageBox.Show("您的电脑只连接了一个屏幕，是否确认显示排队外屏？\r\n\r\n显示排队外屏可能会影响您的正常操作！", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        this.isOpen = true;
                        return 0;
                    }
                }

                this.Show();
            }
            else
            {
                this.Close();
            }

            return 1;
        }

        /// <summary>
        /// 选中处方后点击按钮调用，从发药窗口的树列表中取数据
        /// </summary>
        /// <param name="listDrugRecipe">未发药保存的处方</param>
        /// <param name="savedDrugRecipe">当前选中的处方</param>
        /// <returns>-1失败</returns>
        public int ShowDataAfterSave(List<FS.HISFC.Models.Pharmacy.DrugRecipe> listDrugRecipe, FS.HISFC.Models.Pharmacy.DrugRecipe savedDrugRecipe)
        {
            //this.lblFirstPatient2.Text = string.Empty;
            this.isOperBusy = true;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            int param = this.drugStoreAsignMgr.UpdateQueueState(savedDrugRecipe.RecipeNO,savedDrugRecipe.DrugedOper.Dept.ID,"2");

            if(param <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.Commit();
            }

            this.ShowGetUnSendPatint(savedDrugRecipe);

            this.isOperBusy = false;

            return 1;
        }

        /// <summary>
        /// 在保存后调用，从发药窗口的树列表中取数据
        /// </summary>
        /// <param name="listDrugRecipe">未发药保存的处方</param>
        /// <param name="selectedDrugRecipe">当前保存的处方</param>
        /// <returns>-1失败</returns>
        public int ShowDataAfterSelect(List<FS.HISFC.Models.Pharmacy.DrugRecipe> listDrugRecipe, FS.HISFC.Models.Pharmacy.DrugRecipe selectedDrugRecipe)
        {
            if (string.IsNullOrEmpty(selectedDrugRecipe.RecipeNO) || string.IsNullOrEmpty(selectedDrugRecipe.DrugDept.ID))
            {
                return -1;
            }

            this.isOperBusy = true;

            this.ShowSendLable(selectedDrugRecipe.PatientName);

            this.ShowGetUnSendPatint(selectedDrugRecipe);

            FS.SOC.Local.DrugStore.ZhuHai.ZDWY.Outpatient.Models.DrugStoreAsign drugStoreAsign = this.changeDrugRecipetoDrugStoreAsign(selectedDrugRecipe);

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            int param =  this.drugStoreAsignMgr.Insert(drugStoreAsign);
            if (param <= 0)
            {
                param = this.drugStoreAsignMgr.UpdateQueueState(drugStoreAsign.RecipeNO,drugStoreAsign.DrugDeptCode, "0");
            }

            if (param <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.Commit();
            }
 

            this.ShowSendLable(selectedDrugRecipe.PatientName.ToString());

            this.isOperBusy = false;
           
            return 1;
        }

        private void ShowGetUnSendPatint(FS.HISFC.Models.Pharmacy.DrugRecipe selectedDrugRecipe)
        {
            this.isOperBusy = false;
            this.ClearScrean();
            this.lblWindow.Text = ((FS.HISFC.Models.Pharmacy.DrugTerminal)this.drugStorMgr.GetDrugTerminalById(selectedDrugRecipe.SendTerminal.ID)).Name;
            this.lblWindow.Location = new Point((this.Width - this.lblWindow.Width)/2, this.lblWindow.Location.Y);
            this.nlbWait.Location = new Point((this.Width - this.nlbWait.Width)/2, this.nlbWait.Location.Y);
            ArrayList allPatient = this.drugStoreAsignMGR.QueryDrugRecipeByFeeDateNoCall(selectedDrugRecipe.StockDept.ID, selectedDrugRecipe.SendTerminal.ID, "2");
            allPatient.Sort(new CompareByDrugedTime());

            ShowPatient(allPatient);
        }


        /// <summary>
        /// 获取字节长度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="padLength"></param>
        /// <returns></returns>
        private string SetToByteLength(string str, int padLength)
        {
            int len = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(str[i].ToString(), "[^\x00-\xff]"))
                {
                    len += 1;
                }
            }

            if (padLength - str.Length - len > 0)
            {
                return str + "".PadRight(padLength - str.Length - len, ' ');
            }
            else
            {
                return str;
            }
        }

        #endregion

        private void ChangeColor()
        {
            System.Drawing.Color color1 = this.lblFirtPatient1.ForeColor;
            System.Drawing.Color color2 = this.lblFirstPatient2.ForeColor;
            this.lblFirtPatient1.ForeColor = color2;
            this.lblFirstPatient2.ForeColor = color1;
            this.lblFirstPatient3.ForeColor = color2;
        }

        private void ShowSendLable(string patientName)
        {

            this.lblFirtPatient1.Visible = true;

            this.lblFirstPatient2.Visible = true;

            this.lblFirstPatient3.Visible = true;

            this.lblFirtPatient1.Text = "请";

            this.lblFirstPatient2.Text = patientName;

            this.lblFirstPatient3.Text = "到本窗口取药";

            this.lblFirtPatient1.Location = new Point(this.FindForm().Width/ 2 - (this.lblFirtPatient1.Width + this.lblFirstPatient2.Width + this.lblFirstPatient3.Width) / 2, this.lblFirtPatient1.Location.Y);

            this.lblFirstPatient2.Location = new Point(this.lblFirtPatient1.Location.X + this.lblFirtPatient1.Width, this.lblFirstPatient2.Location.Y);

            this.lblFirstPatient3.Location = new Point(this.lblFirstPatient2.Location.X + this.lblFirstPatient2.Width, this.lblFirstPatient2.Location.Y);


            this.lblFirtPatient1.ForeColor = System.Drawing.Color.Blue;

            this.lblFirstPatient2.ForeColor = System.Drawing.Color.Black;

            this.lblFirstPatient3.ForeColor = System.Drawing.Color.Blue;

        }


        /// <summary>
        /// 将传入drugRecipe实体转换成DrugStoreAsign实体
        /// </summary>
        /// <param name="drugRecipe"></param>
        private  FS.SOC.Local.DrugStore.ZhuHai.ZDWY.Outpatient.Models.DrugStoreAsign changeDrugRecipetoDrugStoreAsign(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            FS.SOC.Local.DrugStore.ZhuHai.ZDWY.Outpatient.Models.DrugStoreAsign drugStoreAsign = new FS.SOC.Local.DrugStore.ZhuHai.ZDWY.Outpatient.Models.DrugStoreAsign();
            drugStoreAsign.recipeNO = drugRecipe.RecipeNO;
            drugStoreAsign.patientId = drugRecipe.ClinicNO;
            drugStoreAsign.cardNO = drugRecipe.CardNO;
            drugStoreAsign.PatientName = drugRecipe.PatientName;
            drugStoreAsign.patientSex = drugRecipe.Sex.ID.ToString();
            drugStoreAsign.deptCode = drugRecipe.DoctDept.ID;
            drugStoreAsign.drugDeptCode = drugRecipe.StockDept.ID;
            drugStoreAsign.sendTerminalCode = drugRecipe.SendTerminal.ID;
            drugStoreAsign.sendTerminalName = this.drugStoreAsignMgr.GetTerminalNameById(drugRecipe.SendTerminal.ID);
            drugStoreAsign.State = "0";
            drugStoreAsign.Oper.ID = this.drugStoreAsignMgr.Operator.ID;
            drugStoreAsign.Oper.OperTime = this.drugStoreAsignMgr.GetDateTimeFromSysDateTime();
            return drugStoreAsign;
        }

        //internal class CompareByDrugedTime : System.Collections.Generic.IComparer<FS.HISFC.Models.Pharmacy.DrugRecipe>
        internal class CompareByDrugedTime:IComparer
        {
            #region IComparer 成员

            public int Compare(object x, object y)
            {
                string oX = "";
                string oY = "";
                if((x is FS.HISFC.Models.Pharmacy.DrugRecipe)&&(y is FS.HISFC.Models.Pharmacy.DrugRecipe))
                oX = (x as FS.HISFC.Models.Pharmacy.DrugRecipe).Clone().DrugedOper.OperTime.ToString("yyyyMMddHHmmss");
                oY = (y as FS.HISFC.Models.Pharmacy.DrugRecipe).Clone().DrugedOper.OperTime.ToString("yyyyMMddHHmmss");

                int nComp;

                if (oX == null)
                {
                    nComp = (oY != null) ? 0 : 1;
                }
                else if (oY == null)
                {
                    nComp = 0;
                }
                else
                {
                    nComp = string.Compare(oX.ToString(), oY.ToString());
                }

                return nComp;
            }

            #endregion
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.isOpen && !isOperBusy)
            {
                ChangeColor();
            }
            this.ShowUnFetchPerson(curDept,CurTerminal);
            this.isOperBusy = false;
        }

        private void ShowUnFetchPerson(string curDept,string curTerminal)
        {
            string allUnFetchPerson = this.drugStoreAsignMGR.GetUnFetchPerson(curDept, CurTerminal, "3");
            string showData = string.Empty;
            if (!string.IsNullOrEmpty(allUnFetchPerson))
            {
                string[] names = allUnFetchPerson.Split(',');
                for (int index = 0; index < names.Length; index++)
                {
                    if (index != 0 && index % 3 == 0)
                    {
                        showData = showData + "\n";
                    }
                    showData += names[index].PadRight(10, ' ');
                }
            }
            this.nlbCallPatient.Text = showData;
        }

        int pageNo = 1;
        int maxPageNo = 0;

        private void timer2_Tick(object sender, EventArgs e)
        {
            
        }

        private void ShowPatient(ArrayList allPatient)
        {
            maxPageNo = (Int32)Math.Ceiling((decimal)allPatient.Count / 8);

            //ArrayList alShow = new ArrayList();
            //int count = 8;
            //if (allPatient.Count - (pageNo - 1) * 8 < 8)
            //{
            //    count = allPatient.Count - (pageNo - 1) * 8;
            //}
            //alShow = allPatient.GetRange((pageNo - 1) * 8, count);

            ArrayList allShowData = new ArrayList();

            if (allPatient != null && allPatient.Count > 0)
            {
                foreach (FS.HISFC.Models.Pharmacy.DrugRecipe info in allPatient)
                {
                    if (info.CancelOper.OperTime > info.FeeOper.OperTime)
                    {
                        continue;
                    }
                    allShowData.Add(info);
                }
            }
            for (int index = 0; index < allShowData.Count; index++)
            {
                if (index >= (pageNo - 1) * 8 && index < pageNo * 8)
                {
                    int controlIndex = index - (pageNo - 1) * 8;

                    foreach (Control c in this.neuPanel4.Controls)
                    {
                        if (c.Name == "nlbPatient" + (controlIndex + 1))
                        {
                            c.Text = (index + 1) + "、" + (allShowData[index] as FS.HISFC.Models.Pharmacy.DrugRecipe).PatientName;
                            if (controlIndex % 2 != 0)
                            {
                                c.Location = new Point(this.Width - 400, c.Location.Y);
                            }
                        }
                    }
                }
            }
            
            if (pageNo < maxPageNo)
            {
                pageNo += 1;
            }
            else
            {
                pageNo = 1;
            }

            //for (int index = 0; index < allPatient.Count; index++)
            //{
            //    foreach (Control c in this.neuPanel4.Controls)
            //    {
            //        if (c.Name == "nlbPatient" + (index + 1))
            //        {
            //            c.Text = (index + 1) + "、" + (allPatient[index] as FS.HISFC.Models.Pharmacy.DrugRecipe).PatientName;
            //            if (index % 2 != 0)
            //            {
            //                c.Location = new Point(this.Width - 300, c.Location.Y);
            //            }
            //        }
            //    }
            //}
        }

        #region IOutpatientLED 成员


        public int OverNO(List<FS.HISFC.Models.Pharmacy.DrugRecipe> listDrugRecipe, FS.HISFC.Models.Pharmacy.DrugRecipe selectedDrugRecipe)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            int param = this.drugStoreAsignMgr.UpdateQueueState(selectedDrugRecipe.RecipeNO, selectedDrugRecipe.StockDept.ID, "3");
            if (param >= 1)
            {
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
            }
            this.ShowGetUnSendPatint(selectedDrugRecipe);
            this.ShowUnFetchPerson(CurDept,CurTerminal);
            return 1;

        }

        #endregion
    }
}
