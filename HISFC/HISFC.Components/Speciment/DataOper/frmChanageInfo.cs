using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;

namespace FS.HISFC.Components.Speciment.DataOper
{
    public partial class frmChanageInfo : Form
    {
        private SpecSourceManage specSourceMgr;
        private DisTypeManage disTypeManage;
        SpecSource specSource;

        public frmChanageInfo()
        {
            InitializeComponent();
            specSourceMgr = new SpecSourceManage();
            disTypeManage = new DisTypeManage();
            specSource = new SpecSource();
        }　

        private void GetSpecSource(string hisBarCode)
        {
            try
            {                
                specSource = specSourceMgr.GetSource("", hisBarCode.PadLeft(12,'0'));
                if (specSource == null || specSource.SpecId <= 0)
                {
                    MessageBox.Show("获取信息失败！");
                    return;
                }
                OperApplyManage oMgr = new OperApplyManage();
                int id = Convert.ToInt32(specSource.HisBarCode);
                OperApply op = new OperApply();
                op = oMgr.GetById(id.ToString(), "B");
                if (op == null || op.OperApplyId <= 0)
                {
                    op = oMgr.GetById(id.ToString(), "O");
                    if (op == null || op.OperApplyId <= 0)
                    {
                        MessageBox.Show("获取信息失败！");
                        return;
                    }
                }
                txtInHosNum.Text = op.InHosNum;
                txtName.Text = op.PatientName;
                txtDiag.Text = op.MainDiaName;
                txtDisType.Text = disTypeManage.SelectDisByID(specSource.DiseaseType.DisTypeID.ToString()).DiseaseName;

            }
            catch
            {
                MessageBox.Show("获取信息失败！");
            }
        }

        private void Save()
        {
            SpecBoxManage boxManage = new SpecBoxManage();
            SubSpecManage subMar = new SubSpecManage();
            SpecSourcePlanManage storeMgr = new SpecSourcePlanManage();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            specSourceMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            boxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            subMar.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            storeMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                ArrayList arrSubSpec = new ArrayList();
                arrSubSpec = subMar.GetSubSpecBySpecId(specSource.SpecId.ToString());
                foreach (SubSpec s in arrSubSpec)
                {
                    SpecBox box = new SpecBox();
                    box = boxManage.GetBoxById(s.BoxId.ToString());
                    if (box == null || box.BoxId <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("删除失败！");
                        return;
                    }
                    if (boxManage.UpdateOccupy(box.BoxId.ToString(), "0") <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新标本盒失败！");
                        return;
                    }
                    int occupyCount = box.OccupyCount - 1 ;
                    if (boxManage.UpdateOccupyCount(occupyCount.ToString(), box.BoxId.ToString()) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新标本盒失败！");
                        return;
                    }
                    //s.BoxId = 0;
                    //s.BoxEndRow = 0;
                    //s.BoxEndCol = 0;
                    //s.BoxStartCol = 0;
                    //s.BoxStartRow = 0;
                    s.SpecCap = 0.0M;
                    s.SpecCount = 0;
                    s.SpecId = 0;
                    s.StoreID = 0;
                    s.SubBarCode = "";
                    s.Comment = "标本删除";
                    if (subMar.UpdateSubSpec(s) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新分装标本失败！");
                        return;
                    }
                } 
                if (specSourceMgr.ExecNoQuery("delete from SPEC_SOURCE where SPECID=" + specSource.SpecId.ToString()) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("删除源标本失败！");
                    return;
                }
                if (storeMgr.ExecNoQuery("delete from SPEC_SOURCE_STORE where SPECID = " + specSource.SpecId.ToString()) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("删除失败！");
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("操作成功");
                specSource = new SpecSource();

            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();  
                MessageBox.Show("删除失败！");
                return;              
            }
        }

        private void frmChanageInfo_Load(object sender, EventArgs e)
        {
           
        }

        private void txtBarCode_TextChanged(object sender, EventArgs e)
        {
            string barCode = txtBarCode.Text.Trim();
            if (barCode.Length == 12)
            {
                GetSpecSource(barCode);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult dR = MessageBox.Show("确认删除? 该操作危险!","删除", MessageBoxButtons.YesNo);
            if (dR == DialogResult.Yes)
            {
                Save();
            }  
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtBarCode.Text.Trim() != "")
            {
                string barCode = txtBarCode.Text.Trim();
                GetSpecSource(barCode);
            }
        }
    }
}