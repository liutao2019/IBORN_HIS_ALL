using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;
namespace FS.HISFC.Components.Manager.Forms
{
    public partial class frmChangeContralValues : Form
    {
        public frmChangeContralValues()
        {
            InitializeComponent();
        }
        #region 变量

        private FS.HISFC.BizLogic.Manager.Constant dba=new FS.HISFC.BizLogic.Manager.Constant();
        /// <summary>
        /// 控制参数
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlArguments = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        /// <summary>
        /// SQL
        /// </summary>
        string sql = "update com_controlargument h set h.control_value = '{0}', h.oper_code = '{1}', h.oper_date = sysdate where h.control_code = '{2}'";

        /// <summary>
        /// 错误信息
        /// </summary>
        string err = string.Empty;

        #endregion

        #region 方法
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <returns></returns>
        private int InitDate()
        {
            //允许开立科室参数
            this.chk1.Checked = controlArguments.GetControlParam<bool>("201026");
            //医嘱单打印标示参数
            this.chk2.Checked = controlArguments.GetControlParam<bool>("B00002");
            //物价费用类别显示参数
            this.chk3.Checked = controlArguments.GetControlParam<bool>("B00001");
            return 1;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns>成功返回1</returns>
        private int Save()
        {
            int val1 = 0;
            int val2 = 0;
            int val3 = 0;
            string sql = string.Empty;
            val1 = NConvert.ToInt32(this.chk1.Checked);
            val2 = NConvert.ToInt32(this.chk2.Checked);
            val3 = NConvert.ToInt32(this.chk3.Checked);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if (this.UpdateControlValues(val1.ToString(), "201026") == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.err = dba.Err;
                return -1;
            }
            if (this.UpdateControlValues(val2.ToString(), "B00002") == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.err = dba.Err;
                return -1;
            }
            if (this.UpdateControlValues(val3.ToString(), "B00001") == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.err = dba.Err;
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }
        /// <summary>
        /// 执行更新操作（简单SQL就不往业务层和数据库中写了）
        /// </summary>
        /// <param name="value">更新的值</param>
        /// <param name="tag">控制参数</param>
        /// <returns>成功返回1</returns>
        private int UpdateControlValues(string value, string tag)
        {
            try
            {
                string str = string.Empty;
                str = string.Format(this.sql, value, dba.Operator.ID,tag);
                dba.ExecNoQuery(str);
            }
            catch
            {
                dba.Err = "更新控制参数为" + tag + "出错!";
                return -1;
            }
            return 1;
        } 
        #endregion

        #region 事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (this.InitDate() == -1)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("初始化数据出错！"));
            }
            base.OnLoad(e);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (this.Save() > -1)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存成功！"));
            }
            else
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg(this.err));
            }
        }
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_cancle_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        } 
        #endregion
        
    }
}
