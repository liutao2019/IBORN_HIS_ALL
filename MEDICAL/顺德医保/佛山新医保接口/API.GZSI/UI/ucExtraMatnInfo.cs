using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace API.GZSI.UI
{
    public partial class ucExtraMatnInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 业务类

        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();

        #endregion

        #region 变量

        /// <summary>
        /// 计划生育服务证号
        /// </summary>
        public string fpsc_no = "";
        /// <summary>
        /// 生育类别 Y
        /// </summary>
        public string matn_type = "";     
        /// <summary>
        /// 计划生育手术类别
        /// </summary>
        public string birctrl_type = "";  
        /// <summary>
        /// 晚育标志 Y
        /// </summary>
        public string latechb_flag = ""; 
        /// <summary>
        /// 孕周数
        /// </summary>
        public string geso_val = "";
        /// <summary>
        /// 胎次
        /// </summary>
        public string fetts = ""; 
        /// <summary>
        /// 胎儿数
        /// </summary>
        public string fetus_cnt = ""; 
        /// <summary>
        /// 早产标志 Y
        /// </summary>
        public string pret_flag = "0"; 
        /// <summary>
        /// 计划生育手术或生育日期
        /// </summary>
        public string birctrl_matn_date = "";

        public string med_type = "";

        #endregion

        #region 属性

        private bool isMoreInfomationRequire = false;
        /// <summary>
        /// 是否要求更多必填项
        /// </summary>
        public bool IsMoreInfomationRequire
        {
            set 
            {
                this.isMoreInfomationRequire = value;
                if (this.isMoreInfomationRequire)
                {
                    this.lblBirctrlType.ForeColor = Color.Blue;
                    this.lblFetusCnt.ForeColor = Color.Blue;
                    this.lblBirctrlMatnDate.ForeColor = Color.Blue;
                }
                else
                {
                    this.lblBirctrlType.ForeColor = Color.Black;
                    this.lblFetusCnt.ForeColor = Color.Black;
                    this.lblBirctrlMatnDate.ForeColor = Color.Black;
                }
            }
            get { return isMoreInfomationRequire; }
        }

        #endregion

        public ucExtraMatnInfo()
        {
            InitializeComponent();
            InitControls();
        }

        private void InitControls()
        {
            this.cmbBirctrlType.AddItems(constMgr.GetAllList(Common.Constants.GZSI_CODE_PREFIX + "birctrl_type")); // 计划生育手术类别
            this.cmbMatnType.AddItems(constMgr.GetAllList(Common.Constants.GZSI_CODE_PREFIX + "matn_type")); // 生育类别
            this.cmbLatechbFlag.AddItems(constMgr.GetAllList(Common.Constants.GZSI_CODE_PREFIX + "latechb_flag")); // 晚育标志
            this.cmbPretFlag.AddItems(constMgr.GetAllList(Common.Constants.GZSI_CODE_PREFIX + "pret_flag")); // 早产标志

            if (this.med_type == "5301" || this.med_type == "5302")
            {
                this.cmbBirctrlType.Enabled = true;
                this.cmbMatnType.Enabled = false;
            }else if (this.med_type == "51" || this.med_type == "52")
            {
                this.cmbBirctrlType.Enabled = false;
                this.cmbMatnType.Enabled = true;
            }
        }

        public int GetValue(ref string errMsg)
        {
            int returnValue = 1;

            //生育类别和生育手术类别不能同时选择
            if (this.cmbMatnType.Tag != null && !string.IsNullOrEmpty(this.cmbMatnType.Tag.ToString())
                && this.cmbBirctrlType.Tag != null && !string.IsNullOrEmpty(this.cmbBirctrlType.Tag.ToString())
                )
            {
                errMsg += "生育类别和生育手术类别不能同时选择;\r\n";
                returnValue = -1;
            }

            if ((this.cmbMatnType.Tag == null || string.IsNullOrEmpty(this.cmbMatnType.Tag.ToString()))
                && (this.cmbBirctrlType.Tag == null || string.IsNullOrEmpty(this.cmbBirctrlType.Tag.ToString()))
                )
            {
                errMsg += "生育类别和生育手术类别不能同时为空;\r\n";
                returnValue = -1;
            }

            //if (this.cmbBirctrlType.Tag == null || string.IsNullOrEmpty(this.cmbBirctrlType.Tag.ToString()))
            //{
            //    errMsg += "生育手术类别不能为空;\r\n";
            //    returnValue = -1;
            //}

            //if (this.cmbLatechbFlag.Tag == null || string.IsNullOrEmpty(this.cmbLatechbFlag.Tag.ToString()))
            //{
            //    errMsg += "晚育标志不能为空;\r\n";
            //    returnValue = -1;
            //}

            //if (this.cmbPretFlag.Tag == null || string.IsNullOrEmpty(this.cmbPretFlag.Tag.ToString()))
            //{
            //    errMsg += "早产标志不能为空;\r\n";
            //    returnValue = -1;
            //}

            if (IsMoreInfomationRequire)
            {
                //if (string.IsNullOrEmpty(this.tbFetusCnt.Text))
                //{
                //    errMsg += "胎儿数不能为空;\r\n";
                //    returnValue = -1;
                //}

                if (this.dtpBirctrlMatnDate.Value == DateTime.MinValue)
                {
                    errMsg += "手术或生育日期不能为空;\r\n";
                    returnValue = -1;
                }
            }

            this.fpsc_no = this.tbFpscNO.Text;
            this.matn_type = this.cmbMatnType.Tag.ToString();
            this.birctrl_type = this.cmbBirctrlType.Tag.ToString();
            this.latechb_flag = this.cmbLatechbFlag.Tag.ToString();
            this.pret_flag = this.cmbPretFlag.Tag.ToString();
            this.geso_val = this.tbGesoVal.Text.ToString();
            this.fetts = this.tbFetts.Text.ToString();
            this.fetus_cnt = this.tbFetusCnt.Text.ToString();
            this.birctrl_matn_date = this.dtpBirctrlMatnDate.Value.Date.ToShortDateString();

            return returnValue;
        }
    }
}
