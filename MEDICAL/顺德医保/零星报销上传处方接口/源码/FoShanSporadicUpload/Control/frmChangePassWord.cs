using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FoShanSporadicUpload.Control
{
    /// <summary>
    /// 修改密码
    /// </summary>
    public partial class frmChangePassWord : FS.FrameWork.WinForms.Forms.BaseForm
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmChangePassWord()
        {
            InitializeComponent();
        }

        #region 变量和属性

        /// <summary>
        /// 用户ID
        /// </summary>
        private string userID;

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        /// <summary>
        /// 用户密码
        /// </summary>
        private string userPw;

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPw
        {
            get { return userPw; }
            set { userPw = value; }
        }

        /// <summary>
        /// 是否修改成功
        /// </summary>
        private bool isSuccess = false;

        /// <summary>
        /// 是否修改成功
        /// </summary>
        public bool IsSuccess
        {
            get { return isSuccess; }
            set { isSuccess = value; }
        }

        /// <summary>
        /// 接口管理类
        /// </summary>
        private SIBizProcess siBizMgr = new SIBizProcess();

        #endregion

        #region 方法和事件

        /// <summary>
        /// 修改密码
        /// </summary>
        private void ModifyPW()
        {
            #region 判断

            //医院编码
            if (string.IsNullOrEmpty(Function.HospitalCode))
            {
                MessageBox.Show("请联系信息，维护医院编码!", "错误");
                return;
            }
            //用户名
            if (string.IsNullOrEmpty(this.userID))
            {
                MessageBox.Show("请输入社保登录账户!", "错误");
                return;
            }
            //传进来的登录密码
            if (string.IsNullOrEmpty(this.userPw))
            {
                MessageBox.Show("请返回主界面登录成功再修改!", "错误");
                return;
            }

            //原密码
            string oldPw = this.txtOldPassWord.Text.Trim();
            if (string.IsNullOrEmpty(oldPw))
            {
                MessageBox.Show("请输入原密码!", "错误");
                this.txtOldPassWord.Focus();
                return;
            }
            if (!oldPw.Equals(this.userPw))
            {
                MessageBox.Show("输入原密码错误!", "错误");
                this.txtOldPassWord.Focus();
                return;
            }

            //新密码
            string newPw = this.txtNewPassWord.Text.Trim();
            if (string.IsNullOrEmpty(newPw))
            {
                MessageBox.Show("请输入新密码!", "错误");
                this.txtNewPassWord.Focus();
                return;
            }
            //确认密码
            string confirmPw = this.txtConfirmPassWord.Text.Trim();
            if (string.IsNullOrEmpty(confirmPw))
            {
                MessageBox.Show("请输入确认密码!", "错误");
                this.txtConfirmPassWord.Focus();
                return;
            }
            //确认密码
            if (!newPw.Equals(confirmPw))
            {
                MessageBox.Show("新密码和确认密码不一致!", "错误");
                this.txtConfirmPassWord.Focus();
                return;
            }

            #endregion

            string transNO = Function.ChangePwTransNO;
            string inXML = string.Format(Function.ChangePwXML, Function.HospitalCode, userID, oldPw, newPw);

            Model.ResultHead result = this.siBizMgr.ChangePw(transNO, inXML);

            if (result.Code == "1")
            {
                //登录成功
                MessageBox.Show(result.Message + "\r\n" + "请重新登录！");

                this.isSuccess = true;
                this.Close();
            }
            else
            {
                //登录失败
                MessageBox.Show(result.Message);

                this.isSuccess = false;
            }
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            this.txtOldPassWord.Text = string.Empty;
            this.txtNewPassWord.Text = string.Empty;
            this.txtConfirmPassWord.Text = string.Empty;
            this.isSuccess = false;

            base.OnLoad(e);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSave_Click(object sender, EventArgs e)
        {
            this.ModifyPW();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.isSuccess = false;
            this.Close();
        }

        #endregion
    }
}
