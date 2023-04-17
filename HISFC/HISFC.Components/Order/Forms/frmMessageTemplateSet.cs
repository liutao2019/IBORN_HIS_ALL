using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Order;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.HISFC.Components.Order.Forms
{
    public partial class frmMessageTemplateSet : Form
    {
        public frmMessageTemplateSet()
        {
            InitializeComponent();
            Init();
        }

        #region 变量
        /// <summary>
        /// 控件内操作的消息模板实体{D37652D3-1DB3-4f8c-AFE6-BE21625F082C}
        /// </summary>
        // private 
        private MessageTemplate msg = null;
        #endregion

        #region 方法
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        /// 
        public int Init()
        {
            this.cbtype.AddItems(CommonController.CreateInstance().QueryConstant("HIS_MSGTEMPLATETYPE"));
            return 1;
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <returns></returns>
        public int Clear()
        {
            foreach (System.Windows.Forms.Control c in this.Controls)
            {
                if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuGroupBox))
                {
                    foreach (System.Windows.Forms.Control crl in c.Controls)
                    {
                        if (crl.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuComboBox))
                        {
                            crl.Text = null;
                            crl.Tag = null;
                            continue;
                        }
                        if (crl.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuLabel) && crl.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox))
                        {
                            crl.Tag = "";
                            crl.Text = "";
                            continue;
                        }
                        if (crl.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox))
                        {
                            ((FS.FrameWork.WinForms.Controls.NeuCheckBox)crl).Checked = false;
                            continue;
                        }
                        if (crl.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuNumericTextBox))
                        {
                            crl.Text = "0";
                            continue;
                        }
                        if (crl.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuTextBox))
                        {
                            crl.Text = "";
                            continue;
                        }
                    }
                }
            }

           
            this.cbstate.Text = "";
            this.cbstate.Tag = "";

            this.cbtype.Tag = "";
            this.cbtype.Text = "";

            this.txtcontent.Text = "";
            this.txtsortid.Text = "";
            this.txttitle.Text = "";



            this.msg = null;

            return 1;
        }


        /// <summary>
        /// 根据传入的Item实体信息 设置控件显示
        /// </summary>
        public void SetItem(MessageTemplate msg)
        {
            this.Clear();
            this.msg = msg;
            this.cbtype.Tag = msg.MsgTemplateType;
            if (msg.State == "1"||string.IsNullOrEmpty(msg.State))
            {
                cbstate.Text = "启用";
            }
            else
            {
                cbstate.Text ="作废";
            }
            this.cbstate.Tag = msg.State;
            this.txttitle.Text = msg.MsgTemplateTitle;
            this.txtsortid.Text = msg.Sortid;
            this.txtcontent.Text = msg.MsgTemplateContent;
        }

        /// <summary>
        /// 有效性检测
        /// </summary>
        /// <returns></returns>
        private bool CheckValid()
        {
            if (this.cbtype.Tag==null)
            {
                CommonController.CreateInstance().MessageBox(FS.FrameWork.Management.Language.Msg("模板类型不能为空!"), MessageBoxIcon.Error);
                this.cbtype.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.txttitle.Text))
            {
                CommonController.CreateInstance().MessageBox(FS.FrameWork.Management.Language.Msg("模板标题不能为空!"), MessageBoxIcon.Error);
                this.txttitle.Focus();
                return false;
            }
           
            return true;
        }


        /// <summary>
        /// 获取基本信息
        /// </summary>
        private MessageTemplate GetItem()
        {
            if (this.msg == null)
            {
                this.msg = new MessageTemplate();
                this.msg.Createcode = FS.FrameWork.Management.Connection.Operator.ID;
                this.msg.Createname = FS.FrameWork.Management.Connection.Operator.Name;
                this.msg.Createtime = DateTime.Now;
            }
            MessageTemplate newmsg = this.msg.Clone();
            newmsg.MsgTemplateType = this.cbtype.Tag.ToString();
            newmsg.MsgTemplateTitle = this.txttitle.Text;
            newmsg.MsgTemplateContent = this.txtcontent.Text;
            newmsg.Sortid = this.txtsortid.Text;
            newmsg.Opercode=FS.FrameWork.Management.Connection.Operator.ID;// Neusoft.FrameWork.Management.Connection.Operator
            newmsg.Opername=FS.FrameWork.Management.Connection.Operator.Name;
            newmsg.Opertime=DateTime.Now;
            
            if (cbstate.Text == "启用")
            {
                newmsg.State = "1";
            }

            else
            {
                newmsg.State = "0";
            }
            return newmsg;
        }


        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.CheckValid())
            {
                MessageTemplate item = this.GetItem();
               
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                HISFC.BizLogic.Order.MessageTemplateLogic itemMgr = new FS.HISFC.BizLogic.Order.MessageTemplateLogic();
                itemMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                if (string.IsNullOrEmpty(this.msg.MessageTemplateId)) //新增
                {
                    item.MessageTemplateId = itemMgr.GetNewMessageID();
                    if (itemMgr.InsertMessageTemplate(item) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        CommonController.CreateInstance().MessageBox("保存失败，请向系统管理员联系并报告错误：" + itemMgr.Err, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.Commit();
                        CommonController.CreateInstance().MessageBox("保存成功" + itemMgr.Err, MessageBoxIcon.Information);
                        this.Close();
                      
                    }
                }
                else //修改
                {
                    if (itemMgr.UpdateMessageTemplate(item) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        CommonController.CreateInstance().MessageBox("保存失败，请向系统管理员联系并报告错误：" + itemMgr.Err, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.Commit();
                        CommonController.CreateInstance().MessageBox("保存成功" + itemMgr.Err, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

   
}
