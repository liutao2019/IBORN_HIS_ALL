using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Operation;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [��������: ������ά��]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-01-18]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class frmOperationRoom : Form
    {
        public frmOperationRoom()
        {
            InitializeComponent();
            this.cmbValid.SelectedIndex = 0;
            this.txtDept.Text = Environment.OperatorDept.Name;
        }

#region �ֶ�
        private bool isNew;
        private OpsRoom room = new OpsRoom();
#endregion

        #region ����


        public OpsRoom Room
        {
            get
            {
                return this.room;
            }
            set
            {
                this.Reset();
                this.room = value;
                this.txtID.Text = room.ID;
                this.txtName.Text = room.Name;
                this.txtInputCode.Text = room.InputCode;
                this.txtIpAddress.Text = room.IpAddress;
                if (room.IsValid)
                    this.cmbValid.SelectedIndex = 0;
                else
                    this.cmbValid.SelectedIndex = 1;
            }
        }

        public bool IsNew
        {
            get
            {
                return this.isNew;
            }
            set
            {
                this.isNew = value;
                if(this.isNew)
                {
                    this.Reset();
                }
                this.room.ID = Environment.TableManager.GetNewRoomID();
                this.txtID.Text = this.room.ID;
            }
        }


       #endregion

#region ����
        private bool IsValid()
        {
            string text = this.txtName.Text.Trim();
            if (text == "")
            {
                MessageBox.Show("�������Ʋ���Ϊ��!", "��ʾ");
                return false;
            }

            return true;
        }

        private new void Update()
        {
            this.room.ID = this.txtID.Text;
            this.room.Name = this.txtName.Text;
            this.room.InputCode = this.txtInputCode.Text;
            this.room.IsValid = this.cmbValid.SelectedIndex == 0 ? true : false;
            this.room.DeptID = Environment.OperatorDeptID;
            this.room.IpAddress = this.txtIpAddress.Text;
            
        }

        private void Reset()
        {
            this.txtID.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.txtInputCode.Text = string.Empty;
            this.cmbValid.SelectedIndex = 0;

            this.room.ID = string.Empty;
            this.room.Name = string.Empty;
            this.room.InputCode = string.Empty;
            this.room.IsValid = true;
            this.room.IpAddress = string.Empty;
        }
#endregion
        private void neuLabelTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.IsValid())
            {
                this.Update();

                int ret;

                if(this.isNew)
                {
                    ret=Environment.TableManager.AddOpsRoom(this.room);
                    if (ret == -1)
                    {
                        MessageBox.Show("������������Ϣ�����!" + Environment.TableManager.Err, "��ʾ");
                    }
                }else
                {
                    ret = Environment.TableManager.UpdateOpsRoom(this.room);
                    if (ret == -1)
                    {
                        MessageBox.Show("������������Ϣ�����!" + Environment.TableManager.Err, "��ʾ");
                    }
                }

                
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}