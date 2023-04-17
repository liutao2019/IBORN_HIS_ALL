using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.OutPatientOrder.Forms
{
    /// <summary>
    /// <br></br>
    /// [功能描述: 门诊医生主界面]<br></br>
    /// [创 建 者: houwb]<br></br>
    /// [创建时间: 2014-5-13]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class frmOutPatientOrder : FS.FrameWork.WinForms.Forms.frmBaseForm, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public frmOutPatientOrder()
        {
            //base.tree = this.tvOutOrderPatientList1;
            InitializeComponent();
            base.tree = this.tvOutOrderPatientList1;
            base.initTree();
            this.tvOutOrderPatientList1.Refresh();
        }

        #region 控件接口



        #endregion


        //1、初始化控制参数
        //2、初始化插件
        //3、加载插件

        //CurrentControl当前界面
        

        #region IPreArrange 成员

        /// <summary>
        /// 登陆界面前判断
        /// </summary>
        /// <returns></returns>
        public int PreArrange()
        {
            //isHosDeptCanLogin = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("LHMZ08", false, "0"));

            //if (currentDept.DeptType.ID.ToString() == "I")
            //{
            //    if (isHosDeptCanLogin)
            //    {
            //        if (MessageBox.Show("您当前登陆的科室为住院科室，是否继续？",
            //                             "询问",
            //                             MessageBoxButtons.YesNo,
            //                             MessageBoxIcon.Question,
            //                             MessageBoxDefaultButton.Button2) == DialogResult.No)
            //        {
            //            return -1;
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("您当前登陆的科室为住院科室，请重新选择。", "门诊医生站不允许登录住院科室");
            //        return -1;
            //    }
            //}

            return 1;
        }

        #endregion
    }
}
