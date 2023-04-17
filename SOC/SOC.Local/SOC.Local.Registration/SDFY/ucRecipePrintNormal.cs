using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Registration.SDFY
{
    public partial class ucRecipePrintNormal : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.IRecipePrint
    {
        /// <summary>
        /// 处方打印
        /// </summary>
        public ucRecipePrintNormal()
        {
            InitializeComponent();
        }

        #region 变量

        private string myRecipeNO = "";

        /// <summary>
        /// 挂号信息
        /// </summary>
        private FS.HISFC.Models.Registration.Register myRegister = new FS.HISFC.Models.Registration.Register();

        /// <summary>
        /// 挂号业务类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// 综合业务类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        #endregion

        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public FS.HISFC.Models.Registration.Register PatientInfo
        {
            get
            {
                return this.myRegister;
            }
            set
            {
                this.myRegister = value;
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置患者基本信息
        /// </summary>
        private void SetPatient()
        {
            this.clear();

            if (this.myRegister == null)
            {
                return;
            }

            this.lblTitle.Text = this.interMgr.GetHospitalName() + " 处 方 笺";

            if (this.myRegister.Pact.PayKind.ID == "01")
            {
                this.lblPact.Text = "√";
            }
            else if (this.myRegister.Pact.PayKind.ID == "02")
            {
                this.lblYB.Text = "√";
            }
            else if (this.myRegister.Pact.PayKind.ID == "03")
            {
                this.lblGF.Text = "√";
            }
            else
            {
                this.lblQT.Text = "√";
            }
            this.lblDept.Text = this.myRegister.DoctorInfo.Templet.Dept.Name;

            //顺德本地化需求 挂号处方单上打印看诊医生姓名和看诊序号  ygch  {10F6E92B-76C1-4c16-88D3-2E39E0E4F4FC}
            this.lblDoctName.Text += this.myRegister.DoctorInfo.Templet.Doct.Name;
            #region 以下一坨是生成和分诊那里一样的排队序号 如果中间出错 说明分诊失败 则排队序号赋为空
            try
            {
                FS.HISFC.BizLogic.Nurse.Assign assginMgr = new FS.HISFC.BizLogic.Nurse.Assign();
                FS.HISFC.Models.Nurse.Assign assion = new FS.HISFC.Models.Nurse.Assign();
                assion = assginMgr.QueryByClinicID(this.myRegister.ID);

                FS.HISFC.BizLogic.Nurse.Queue queMgr = new FS.HISFC.BizLogic.Nurse.Queue();
                FS.HISFC.Models.Nurse.Queue queobj = new FS.HISFC.Models.Nurse.Queue();
                ArrayList queobjList = new ArrayList();
                queobjList = queMgr.QueryByQueueID(assion.Queue.ID);
                if (queobjList.Count != 0)
                {
                    queobj = queobjList[0] as FS.HISFC.Models.Nurse.Queue;
                }
                this.lblDoctSeeNO.Text += queobj.SRoom.Name + "-" + assion.SeeNO.ToString();
            }
            catch (Exception e)
            {
                this.lblDoctSeeNO.Text += "";
            }
            #endregion

            this.lblName.Text = this.myRegister.Name;
            this.lblCardNO.Text = this.myRegister.PID.CardNO;
            this.lblSex.Text = this.myRegister.Sex.Name;
            if (this.lblSex.Text == "男")
            {
                lblSex.Text = "√";
            }
            else
            {
                lblWoman.Text = "√";
            }
            this.lblAge.Text = this.regMgr.GetAge(this.myRegister.Birthday);

            #region 精确年龄 1岁以下显示月 日  14岁以下显示年月日 14岁以上显示年  ygch {45A98CF7-0239-4785-80FD-3DB8913E8F90}
            string agee = "";
            agee = this.regMgr.GetAge(this.myRegister.Birthday, true);

            agee = agee.Replace("_", "");
            int iyear = FS.FrameWork.Function.NConvert.ToInt32(agee.Substring(0, agee.IndexOf("岁")));
            int iMonth = FS.FrameWork.Function.NConvert.ToInt32(agee.Substring(agee.IndexOf("岁") + 1, agee.IndexOf("月") - agee.IndexOf("岁") - 1));
            int iDay = FS.FrameWork.Function.NConvert.ToInt32(agee.Substring(agee.IndexOf("月") + 1, agee.IndexOf("天") - agee.IndexOf("月") - 1));
            if (iyear == 0)
            {
                agee = iMonth.ToString() + "月" + iDay.ToString() + "天";
            }
            else if (iyear >= 1 && iyear < 14)
            {
                agee = iyear.ToString() + "岁" + iMonth.ToString() + "月";
            }
            else
            {
                agee = iyear.ToString() + "岁";
            }

            this.lblAgeE.Text = "年龄:  " + agee;
            #endregion

            this.lblSICard.Text = this.myRegister.SIMainInfo.RegNo;
            this.lblAddress.Text = this.myRegister.AddressHome + "   " + this.myRegister.PhoneHome;
            this.neuPictureBox1.Image = FS.FrameWork.WinForms.Classes.CodePrint.GetCode39(this.myRegister.ID);
            this.lblRegisterID.Text = this.myRegister.ID;

            DateTime sysDate = this.regMgr.GetDateTimeFromSysDateTime();
            this.lblSeeDate.Text = sysDate.ToString("yyyy     MM    dd");

            this.lblSeeDoctor.Text = this.regMgr.Operator.Name;
        }

        #endregion

        #region IRecipePrint 成员

        /// <summary>
        /// 打印机名称
        /// </summary>
        private string printer = "";

        public string Printer
        {
            get
            {
                return printer;
            }
            set
            {
                printer = value;
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        public int PrintRecipe()
        {
            this.lblTitle.Text = this.interMgr.GetHospitalName() + "处 方 笺";

            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsDataAutoExtend = true;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            //ygch  门诊挂号处方单直接打印
            p.PrintPage(45,0,this);

            return 1;
        }

        /// <summary>
        /// 实现处方打印方法PrintRecipeView()
        /// </summary>
        /// <returns></returns>
        public int PrintRecipeView(ArrayList al)
        {
            return 1;
        }

        public void clear()
        {
            this.lblPact.Text = "";
            this.lblGF.Text = "";
            this.lblQT.Text = "";
            this.lblYB.Text = "";
            this.lblSex.Text = "";
            this.lblWoman.Text = "";
        }

        /// <summary>
        /// 设置患者信息
        /// </summary>
        /// <param name="register"></param>
        public int SetPatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            this.myRegister = register;
            this.SetPatient();
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        public string RecipeNO
        {
            get
            {
                return this.myRecipeNO;
            }
            set
            {
                this.myRecipeNO = value;
            }

        }

        #endregion
    }
}

