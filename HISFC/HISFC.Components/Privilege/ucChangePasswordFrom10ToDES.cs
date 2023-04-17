using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Privilege
{
    /// <summary>
    /// {D515E09B-E299-47e0-BF19-EDFDB6E4C775}
    /// </summary>
    public partial class ucChangePasswordFrom10ToDES : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucChangePasswordFrom10ToDES()
        {
            InitializeComponent();
        }

        #region 变量
        FS.HISFC.BizLogic.Privilege.UserLogic userLogic = new FS.HISFC.BizLogic.Privilege.UserLogic();
        #endregion

        #region 方法
        /// <summary>
        /// 查询操作员信息
        /// </summary>
        /// <returns></returns>
        protected virtual int QueryDetail()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
            List<FS.HISFC.BizLogic.Privilege.Model.User> userList = this.userLogic.Query();

            if (userList == null)
            {
                MessageBox.Show("查询用户表失败!\n" + this.userLogic.Err);
                return -1;
            }

            //this.neuSpread1_Sheet1.RowCount = userList.Count;
            int i = 0;
            foreach (FS.HISFC.BizLogic.Privilege.Model.User item in userList)
            {
                if (item.Account == "admin")  //手工处理
                {
                    continue;
                }
                this.neuSpread1_Sheet1.Rows.Add(0, 1);
               
                this.neuSpread1_Sheet1.Cells[0, (int)EnumCol.USERID].Text = item.Id;
                this.neuSpread1_Sheet1.Cells[0, (int)EnumCol.UESRNAME].Text = item.Name;
                this.neuSpread1_Sheet1.Cells[0, (int)EnumCol.ACCOUT].Text = item.Account;
                this.neuSpread1_Sheet1.Cells[0, (int)EnumCol.PASSWORD].Text = item.Password;
                this.neuSpread1_Sheet1.Cells[0, (int)EnumCol.APPID].Text = item.AppId;
                this.neuSpread1_Sheet1.Cells[0, (int)EnumCol.PERSONID].Text = item.PersonId;
                this.neuSpread1_Sheet1.Cells[0, (int)EnumCol.DESCPRIPTION].Text = item.Description;
                this.neuSpread1_Sheet1.Cells[0, (int)EnumCol.DESCPRIPTION].Value = item.IsLock;
                this.neuSpread1_Sheet1.Cells[0, (int)EnumCol.ISMANAGER].Value = item.IsManager;
                 this.neuSpread1_Sheet1.Rows[0].Tag = item;
                //i++;

            }
            return 1;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryDetail();
            return base.OnQuery(sender, neuObject);
        }
        protected virtual int UpdatePassword()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            int returnValue = 1;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                FS.HISFC.BizLogic.Privilege.Model.User user = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.BizLogic.Privilege.Model.User;
                FS.HISFC.BizLogic.Privilege.Model.User userClone = user.Clone();
                //用1.0方式还原密码
                string common = FS.HisCrypto.HisDecrypt.Decrypt(userClone.Password);
                userClone.Password = FS.HisCrypto.DESCryptoService.DESEncrypt(common, FS.FrameWork.Management.Connection.DESKey);

                returnValue = this.userLogic.Update(userClone);

                if (returnValue < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新密码失败!\n" + this.userLogic.Err);
                  
                    return -1;
                }

                //if (returnValue == 0)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show(string.Format("没有找到{0}的用户信息!",userClone.Name));
                //    return -1;
                //}

            }
            this.QueryDetail();
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("转换成功！");

           
            return 1;
        }

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("转换", "转换", (int)FS.FrameWork.WinForms.Classes.EnumImageList.A安排, true, false, null);
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "转换":
                    {
                        this.UpdatePassword();

                        break;
                    }
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override void OnLoad(EventArgs e)
        {
           
            this.neuSpread1_Sheet1.Columns[(int)EnumCol.APPID].Visible = false;
            base.OnLoad(e);
        }
       

        /// <summary>
        /// 列枚举
        /// </summary>
        private enum EnumCol
        {
            USERID,
            UESRNAME,
            ACCOUT,
            PASSWORD,
            APPID,
            PERSONID,
            DESCPRIPTION,
            ISLOCK,
            ISMANAGER

        }
        #endregion
    }
}