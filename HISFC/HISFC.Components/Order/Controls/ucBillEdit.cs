using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// 执行单编辑 by huangchw 2012-09-04
    /// </summary>
    public partial class ucBillEdit : UserControl
    {
        public ArrayList alExecBill = new ArrayList();
        private ArrayList arr = new ArrayList();
        private string BillNO = null;
        private string oldName = null;
        public FS.FrameWork.Models.NeuObject objExecBill = new FS.FrameWork.Models.NeuObject();

        public ucBillEdit()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtExecBillName.Text.Trim() == "" || this.txtExecBillName.Text.Length == 0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("执行单名称不能为空。"));
                return;
            }

            for (int i = 0; i < arr.Count; i++)   //循环判断重复
            {
                if (txtExecBillName.Text.Trim() == arr[i].ToString().Trim()
                    && txtExecBillName.Text.Trim() != this.oldName )
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("执行单名字重复，请修改。"));
                    return;
                }
            }
            #region 更新的值

            string newName = this.txtExecBillName.Text.Trim();//执行单名称
            if (txtMemo.Text != "")    //执行单备注
            {
                txtMemo.Text = txtMemo.Text.Trim();
            }
            string memo = null;       //是否项目执行单
            if (this.chkItemBill.Checked == true) 
            {
                memo = "1";
            }
            else
            {
                memo = "0";
            }
            #endregion

            FS.HISFC.BizLogic.Order.ExecBill oExecBill = new FS.HISFC.BizLogic.Order.ExecBill();
            if (oExecBill.UpdateExecBill(this.BillNO, newName, this.txtMemo.Text, memo) != -1)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("修改成功！"));

                #region 修改成功后往回传值
                this.objExecBill.Name = txtExecBillName.Text.Trim(); //执行单名称
                this.objExecBill.User01 = this.txtMemo.Text;   //备注
                if (this.chkItemBill.Checked == true)   //项目执行单
                {
                    this.objExecBill.Memo = "1";
                }
                else
                {
                    this.objExecBill.Memo = "0";
                }
                #endregion

                this.FindForm().Close();
            }
            else
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("修改失败"));
            }
        }

        /// <summary>
        /// 窗口传值
        /// </summary>
        /// <param name="tempObjExecBill"></param>
        public void setValues(FS.FrameWork.Models.NeuObject tempObjExecBill, cResult cr)
        {
            this.objExecBill = tempObjExecBill;
            
            this.txtExecBillName.Text = tempObjExecBill.Name; //执行单名称
            this.oldName = tempObjExecBill.Name;
            
            if (tempObjExecBill.Memo.Equals("1"))   //项目执行单
            {
                this.chkItemBill.Checked = true;
            }
            else if (tempObjExecBill.Memo.Equals("0"))
            {
                this.chkItemBill.Checked = false;
            }
            this.txtMemo.Text = tempObjExecBill.User01; //备注
            
            this.arr = cr.al;
            this.BillNO = tempObjExecBill.ID; //执行单ID
        }

    }
}
