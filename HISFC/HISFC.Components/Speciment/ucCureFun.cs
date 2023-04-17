using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace UFC.Speciment
{
    public partial class ucCureFun : UserControl
    {
        #region 私有变量
        /// <summary>
        /// 治疗信息
        /// </summary>
        private CureInfo cureInfo;
        #endregion

        #region 构造函数
        public ucCureFun()
        {
            InitializeComponent();
            cureInfo = new CureInfo();
        }
        #endregion

        #region 公有属性
        public CureInfo CureInfo
        {
            get
            {
                GetCureInfo();
                return cureInfo;
            }
            set
            {
                cureInfo = value;
            }
        }
        #endregion

        #region 公有方法
        /// <summary>
        /// 获取治疗信息
        /// </summary>
        public void GetCureInfo()
        {
            if(rbtAfterCure.Checked) cureInfo.getPeroid = "2";
            else cureInfo.getPeroid = "1";
            cureInfo.operPos = txtOperPos.Text;
            if(txtRadSch.Tag!=null) cureInfo.radScheme = txtRadSch.Tag.ToString();
            if(txtMedSch.Tag!=null) cureInfo.medScheme = txtMedSch.Tag.ToString();
            cureInfo.Comment = txtComment.Text;
        }
        #endregion

        #region 事件
        private void ucCureFun_Load(object sender, EventArgs e)
        {
            FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
            //放疗方式 
            ArrayList RmodeidList = con.GetList(FS.HISFC.Models.Base.EnumConstant.RADIATETYPE);
            txtRadSch.AddItems(RmodeidList);

            //化疗方式
            ArrayList CmodeidList = con.GetList(FS.HISFC.Models.Base.EnumConstant.CHEMOTHERAPY);

            txtMedSch.AddItems(CmodeidList);
        }
        #endregion
    }

    #region 内部公有类
    /// <summary>
    /// 治疗信息
    /// </summary>
    public class CureInfo
    {
        public string getPeroid;
        public string operPos;
        public string radScheme;
        public string medScheme;
        public string Comment;
    }
    #endregion
}
