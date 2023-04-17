using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Operation
{
    public partial class ucCancelApplicationApply : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 属性及变量
        /// <summary>
        /// 当前操作的手术信息
        /// </summary>
        private FS.HISFC.Models.Operation.OperationAppllication application = new FS.HISFC.Models.Operation.OperationAppllication();
        public FS.HISFC.Models.Operation.OperationAppllication Application
        {
            get 
               { 
                 return application; 
               }
            set 
               { 
                 application = value;
                 this.Clear();
                 this.SetInfo(application);
                 curEnumOper = EnumOper.Normal;
               }
        }

        /// <summary>
        /// 当前操作类型
        /// </summary>
        private EnumOper curEnumOper = EnumOper.Normal;
        public EnumOper CurEnumOper
        {
            get
            {
                return curEnumOper;
            }
            set
            {
                curEnumOper = value;
            }
        }

        #endregion

        public ucCancelApplicationApply()
        {
            InitializeComponent();
            this.nbtOK.Click += new EventHandler(nbtOK_Click);
            this.nbtCancel.Click += new EventHandler(nbtCancel_Click);
        }

        #region 方法
        /// <summary>
        /// 清空
        /// </summary>
        private void Clear()
        {
            this.nlbApplicationID.Text = string.Empty;
            this.nlbApplicationName.Text = string.Empty;
            this.nlbApplyDate.Text = string.Empty;
            this.nlbExecDate.Text = string.Empty;
            this.ntxtCancelApplyReason.Text = string.Empty;
        }

        /// <summary>
        /// 赋值显示
        /// </summary>
        /// <param name="application"></param>
        private void SetInfo(FS.HISFC.Models.Operation.OperationAppllication application)
        {
            this.nlbApplicationID.Text = "申请单号：" + application.ID;
            this.nlbApplicationName.Text = "手术名称：" + application.MainOperationName;
            this.nlbApplyDate.Text = "申请日期：" + application.ApplyDate.ToShortDateString();
            this.nlbExecDate.Text = "拟手术日期：" + application.PreDate.ToShortDateString();

        }

        /// <summary>
        /// 取消事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void nbtCancel_Click(object sender, EventArgs e)
        {
            curEnumOper = EnumOper.Cancel;
            this.FindForm().Close();
        }

        /// <summary>
        /// 确认事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void nbtOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.ntxtCancelApplyReason.Text))
            {
                MessageBox.Show("请录入作废手术原因!", "提示");
                this.ntxtCancelApplyReason.Focus();
                return;
            }
            this.InsertCancelApply(this.Application);
            curEnumOper = EnumOper.Save;
            this.FindForm().Close();
        }

        /// <summary>
        /// 插入作废手术申请表
        /// </summary>
        /// <param name="operationAppllication"></param>
        private void InsertCancelApply(FS.HISFC.Models.Operation.OperationAppllication operationAppllication)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            Environment.OperationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            if (Environment.OperationManager.InsertCancelApply(operationAppllication,this.ntxtCancelApplyReason.Text) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Environment.OperationManager.Err, "提示");
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("发送作废申请成功，请联系手术室作废手术申请!", "提示");
        }
        #endregion
    }

    public enum EnumOper
    { 
        Normal,
        Save,
        Cancel
    }
}
