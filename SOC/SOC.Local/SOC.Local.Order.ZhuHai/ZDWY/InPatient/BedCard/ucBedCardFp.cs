using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.BedCard
{
    /// <summary>
    /// 床头卡显示
    /// </summary>
    public partial class ucBedCardFp : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IControlPrintable
    {

        private int vnum = 0;
        private int hnum = 0;

        /// <summary>
        /// 
        /// </summary>
        public ucBedCardFp()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgBedNo"></param>
        /// <returns></returns>
        private string BedDisplay(string orgBedNo)
        {
            if (orgBedNo == "")
            {
                return orgBedNo;
            }

            string tempBedNo = "";
            //int tempBedNoInt = 0;

            if (orgBedNo.Length > 4)
            {
                tempBedNo = orgBedNo.Substring(4);
            }
            else
            {
                return orgBedNo;
            }
            return tempBedNo;
        }

        #region 变量

        //传递患者信息类
        private FS.HISFC.Models.RADT.PatientInfo patient;

        /// <summary>
        /// 接收传过来的患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get
            {
                return this.patient;
            }
            set
            {
                this.patient = value;
                SetInfo(patient); 
            }

        }

        #endregion

        #region 方法

        /// <summary>
        /// 传递病人实体信息
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.Patient = neuObject as FS.HISFC.Models.RADT.PatientInfo;
            return base.OnSetValue(neuObject, e);
        }

        FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

        /// <summary>
        /// 设置显示信息
        /// </summary>
        private void SetInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            if (patientInfo != null)
            {
                this.fpBedCard_Sheet1.Cells[0, 0].Text = patientInfo.Name;
                this.fpBedCard_Sheet1.Cells[1, 3].Text = patientInfo.Sex.Name;
                this.fpBedCard_Sheet1.Cells[1, 5].Text = deptMgr.GetAge(patientInfo.Birthday);
                this.fpBedCard_Sheet1.Cells[3, 3].Text = patientInfo.PID.PatientNO;
                this.fpBedCard_Sheet1.Cells[5, 3].Text = patientInfo.PVisit.InTime.ToString("yyyy-MM-dd");
            }
        }

        #endregion

        #region IControlPrintable 成员

        public int BeginHorizontalBlankWidth
        {
            get
            {
                return 5;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        public int BeginVerticalBlankHeight
        {
            get
            {
                return 10;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        public System.Collections.ArrayList Components
        {
            get
            {
                return null;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        public Size ControlSize
        {
            get 
            {
                return this.Size;
            }
        }

        public object ControlValue
        {
            get
            {
                return null;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                FS.HISFC.Models.RADT.PatientInfo patientObj = value as FS.HISFC.Models.RADT.PatientInfo;
                if (patientObj == null)
                {
                    return;
                }

                SetInfo(patientObj);
            }
        }

        public int HorizontalBlankWidth
        {
            get
            {
                return 10;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        public int HorizontalNum
        {
            get
            {
                return this.hnum;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                this.hnum = value;
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        public bool IsCanExtend
        {
            get
            {
                return false;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        public bool IsShowGrid
        {
            get
            {
                return false;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        public int VerticalBlankHeight
        {
            get
            {
                return 10;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        public int VerticalNum
        {
            get
            {
                return this.vnum;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                this.vnum = value;
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        #endregion


    }
}
