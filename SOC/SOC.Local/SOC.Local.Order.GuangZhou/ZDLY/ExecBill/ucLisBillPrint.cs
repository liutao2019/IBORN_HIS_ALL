using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Order.GuangZhou.ZDLY.ExecBill
{
    
    /// <summary>
    /// 住院检验单打印
    /// </summary>
    public partial class ucLisBillPrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucLisBillPrint()
        {
            InitializeComponent();
        }

        #region 变量

        #region 业务层

    

        FS.HISFC.BizLogic.Order.OutPatient.Order orderManager = new FS.HISFC.BizLogic.Order.OutPatient.Order();


        FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();


        FS.HISFC.BizLogic.Manager.Frequency frequencyManagement = new FS.HISFC.BizLogic.Manager.Frequency();


        FS.HISFC.BizLogic.RADT.InPatient inPatient = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 科室分类维护
        /// </summary>
        FS.HISFC.BizLogic.Manager.DepartmentStatManager deptStat = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();

        FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();

        #endregion


        #endregion

   

        /// <summary>
        /// 设置患者基本信息
        /// </summary>
        /// <param name="register"></param>
        public void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            #region 基本信息

            patientInfo = inPatient.GetPatientInfoByPatientNO(patientInfo.ID);

            this.lblName.Text = patientInfo.Name;
            this.lblSex.Text = patientInfo.Sex.Name; //性别
            this.lblCardNo.Text = patientInfo.PID.PatientNO; ;//门诊号
            this.lblDept.Text = patientInfo.PVisit.PatientLocation.Dept.Name ; ;//申请科室
            this.lblAge.Text = this.orderManager.GetAge(patientInfo.Birthday, false); //年龄
            #endregion
            
        }

        /// <summary>
        /// 设置打印
        /// </summary>
        /// <param name="IList"></param>
        public void SetPrintValue(IList<FS.HISFC.Models.Order.Inpatient.Order> IList, bool isPreview)
        {
            decimal phaMoney = 0m;//金额
            string tempID = string.Empty;   //临时保存组合号为了区分
            string tempItem = string.Empty;   //临时保存组合号为了区分
            lbData.Text = IList[0].MOTime.ToString("yyyy-MM-dd");
            //lbTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            lblPrintTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


            string sample = "";
            string applyNo = "";

            Hashtable hsCombID = new Hashtable();

            for (int i = 0; i < IList.Count; i++)
            {
                tempItem += IList[i].Item.Name + "       " + FS.FrameWork.Public.String.ToSimpleString(IList[i].Item.Qty) + IList[i].Unit + "\r\n";
                phaMoney += IList[i].Item.Qty * IList[i].Item.Price;

                if (!sample.Contains(IList[i].Sample.Name))
                {
                    sample += " " + IList[i].Sample.Name;
                }

                lblDocName.Text = IList[i].ReciptDoctor.Name;
            }

            if (IList.Count > 0)
            {
                applyNo += " " + IList[0].ApplyNo;

                ArrayList deptMemo = deptStat.LoadByChildren("00", IList[0].ExeDept.ID);
                if (deptMemo.Count > 0)
                {
                    this.lblExeDept.Text = (deptMemo[0] as FS.HISFC.Models.Base.DepartmentStat).Name;
                }
            }

            lblCheckItem.Text = tempItem;
            lblCheckFee.Text = "合计金额：" + FS.FrameWork.Public.String.ToSimpleString(phaMoney) + "元";


            lblSample.Text = sample;
            npbRecipeNo.Text = applyNo;

            if (lblCheckItem.Text != "" && !isPreview)
            {
                PrintPage();
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void PrintPage()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            //print.IsLandScape = true;
            //print.SetPageSize(new FS.HISFC.Models.Base.PageSize("A5", 575, 800));

            FS.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("ZYLIS");
            if (pSize == null)
            {
                pSize = new FS.HISFC.Models.Base.PageSize("", 850, 336);
            }

            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.SetPageSize(pSize);
            //21.59 9.35
            print.IsDataAutoExtend = false;
            if ( ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
        }

       

      
















        #region IOutPatientOrderPrint 成员

        public void PreviewOutPatientOrderBill(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, System.Collections.Generic.IList<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview)
        {
            return;
        }

        #endregion
    }
}
