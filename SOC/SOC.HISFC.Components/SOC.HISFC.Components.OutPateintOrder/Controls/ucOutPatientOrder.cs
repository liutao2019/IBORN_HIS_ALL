using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.OutPatientOrder.Controls
{
    /// <summary>
    /// <br></br>
    /// [功能描述: 门诊医生开立界面]<br></br>
    /// [创 建 者: houwb]<br></br>
    /// [创建时间: 2014-5-13]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucOutPatientOrder : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOutPatientOrder()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 登陆界面
        /// </summary>
        /// <returns></returns>
        public int LogIn()
        {
            FS.HISFC.Models.Base.Department currentDept = SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);

            if (currentDept == null || string.IsNullOrEmpty(currentDept.ID))
            {
                FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "错误", "获取登陆科室信息出错！\r\n请联系系统管理员！", ToolTipIcon.Error);
                return -1;
            }

            //bool isHosDeptCanLogin = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("LHMZ08", false, "0"));

            if (currentDept.DeptType.ID.ToString() == "I")
            {
                //if (isHosDeptCanLogin)
                //{
                    if (MessageBox.Show("您当前登陆的科室为住院科室，是否继续？",
                                         "询问",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question,
                                         MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return -1;
                    }
                //}
                //else
                //{
                //    MessageBox.Show("您当前登陆的科室为住院科室，请重新选择。", "门诊医生站不允许登录住院科室");
                //    return -1;
                //}
            }

            return 1;
        }
    }
}
