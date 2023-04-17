using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Models;

namespace FS.HISFC.Components.Account.Forms
{
    public partial class frmFinPassWord : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmFinPassWord()
        {
            InitializeComponent();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case "tbEdit":
                    {
                        this.ucFindAccountPassWord1.EditPassWord();
                        break;
                    }
                case "tbClear":
                    {
                        this.ucFindAccountPassWord1.Clear();
                        break;
                    }
                case "tbClose":
                    {
                        this.Close();
                        break;
                    }
            }
        }

        private void frmFinPassWord_Load(object sender, EventArgs e)
        {
            #region Ȩ������
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager user = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            NeuObject dept = (user.Operator as HISFC.Models.Base.Employee).Dept;
            //�ж��Ƿ����ע������Ȩ��
            bool blpri = user.JudgeUserPriv(user.Operator.ID, dept.ID, "5101", "01");
            if (!blpri)
            {
                MessageBox.Show("��������ע���ʻ�����Ȩ�ޣ�");
                this.ucFindAccountPassWord1.Enabled = false;
            }
            #endregion
        }
    }
}