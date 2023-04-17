using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using FS.HISFC.Models.Base;
using System.Collections;

namespace FS.HISFC.Components.Speciment.OutStore
{
    public partial class ucConfirmOut : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private ApplyTableManage applyTableManage;
        private ApplyTable applyTable;
        private IceBoxManage iceboxManage;
        private SubSpecManage specManage;
        private string sql;
        private List<OutInfo> outInfoList;
        private List<string> outList;
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService;
        private FS.HISFC.Models.Base.Employee loginPerson;

        public ucConfirmOut()
        {
            InitializeComponent();
            applyTableManage = new ApplyTableManage();
            applyTable = new ApplyTable();
            outInfoList = new List<OutInfo>();
            outList = new List<string>();
            toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
            specManage = new SubSpecManage();
        }

        private void SetSql()
        {
            sql = "select s.SUBSPECID,s.SUBBARCODE,b.BOXBARCODE, d.DISEASENAME, t.SPECIMENTNAME\n" +
                  " from SPEC_SUBSPEC s inner join SPEC_BOX b on s.BOXID = b.BOXID\n" +
                  " inner join SPEC_DISEASETYPE d on b.DISEASETYPEID = d.DISEASETYPEID \n" +
                  " inner join SPEC_TYPE t on s.SPECTYPEID = t.SPECIMENTTYPEID\n" +
                  "where s.SUBBARCODE = '";
        }

        private string ParseBoxCode(string boxCode)
        {
            iceboxManage = new IceBoxManage();
            string tmpBarCode = boxCode;
            string boxId = Convert.ToInt32(tmpBarCode.Substring(2, 3)).ToString();
            string otherInfo = tmpBarCode.Substring(5);
            string parseResult = "";
            parseResult = iceboxManage.GetIceBoxById(boxId).IceBoxName + " ";
            parseResult += otherInfo;
            return parseResult;
        }

        private void Parse()
        {

            string specBarCodeList = applyTable.SpecList;
            string returnList = applyTable.IsImmdBackList;
            string[] barCode = specBarCodeList.Split(',');
            string[] returnAble = returnList.Split(',');
            //int k = 0;
            //foreach (string s in barCode)
            //{                
            //    if (k == barCode.Length - 1)
            //    {
            //        sql += s + "'";
            //    }
            //    sql += s + "' or s.SUBBARCODE = '' ";
            //}
            for (int i = 0; i < barCode.Length; i++)
            {
                string c = barCode[i];
                string r = returnAble[i];
                if (c != null && c != "")
                {
                    OutInfo o = new OutInfo();
                    string tmpSql = sql + c.Trim() + "'";
                    DataSet ds = new DataSet();
                    specManage.ExecQuery(tmpSql, ref ds);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            neuSpread1_Sheet1.Rows.Add(i, 1);
                            o.BoxBarCode = dt.Rows[0]["BOXBARCODE"].ToString();
                            o.SpecBarCode = dt.Rows[0]["SUBBARCODE"].ToString();
                            o.SpecId = dt.Rows[0]["SUBSPECID"].ToString();
                            o.ReturnAble = r;
                            o.Count = 1.0M;
                            outList.Add(o.SpecBarCode);
                            outInfoList.Add(o);
                            neuSpread1_Sheet1.Cells[i, 0].Text = o.SpecBarCode;
                            neuSpread1_Sheet1.Rows[i].Tag = o;
                            neuSpread1_Sheet1.Cells[i, 1].Text = dt.Rows[0]["DISEASENAME"].ToString();
                            neuSpread1_Sheet1.Cells[i, 2].Text = dt.Rows[0]["SPECIMENTNAME"].ToString();
                            neuSpread1_Sheet1.Cells[i, 3].Text = ParseBoxCode(dt.Rows[0]["BOXBARCODE"].ToString());
                            dt.Dispose();
                            ds.Dispose();
                        }
                    }
                }
            }
        }

        private void Out()
        {
            SpecOutOper specOut = new SpecOutOper(applyTable, loginPerson);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            specOut.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            try
            {

                //if (specOut.ConfirmSpecOut(outInfoList) < 0)
                //{
                //    MessageBox.Show("操作失败！");
                //    trans.RollBack();
                //}
                ////trans.RollBack();
                //trans.Commit(); 
                //neuSpread1_Sheet1.Rows.Count = 0;
                //applyTable = new ApplyTable();
                //MessageBox.Show("操作成功！");


            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("操作失败！");
            }
        }

        private void Remove()
        {
            if (neuSpread1_Sheet1.ActiveRow.Index < 0)
            {
                return;
            }
            OutInfo o = neuSpread1_Sheet1.ActiveRow.Tag as OutInfo;
            if (o != null)
            {
                outInfoList.Remove(o);
                if (outList.Contains(o.SpecBarCode))
                {
                    outList.Remove(o.SpecBarCode);
                }
                neuSpread1_Sheet1.RemoveRows(neuSpread1_Sheet1.ActiveRow.Index, 1);
            }
        }

        private void txtCurNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    string curApplyNum = txtApplyNum.Text.Trim();
                    applyTable = applyTableManage.QueryApplyByID(curApplyNum);
                    if (applyTable == null || applyTable.ApplyId <= 0)
                    {
                        MessageBox.Show("申请单号不存在！");
                        return;
                    }
                    tpScan.Text = applyTable.ApplyUserName + "的出库标本";
                    Parse();
                    txtApplyNum.Text = "";
                    //specOut = new SpecOutOper(currentTable, curApplyNum, loginPerson);

                }
                catch
                {
                    return;
                }
            }
        }

        private void txtBarCode_TextChanged(object sender, EventArgs e)
        {
            string tmpBarCode = txtBarCode.Text.Trim();
            if (outList.Contains(tmpBarCode))
            {
                int index = outList.IndexOf(tmpBarCode);
                neuSpread1_Sheet1.Rows[index].BackColor = Color.Aqua;
                txtBarCode.Text = "";
            }
            else
            {
                if (tmpBarCode.Length > 6)
                {
                    string tmpSql = sql + tmpBarCode + "'";
                    DataSet ds = new DataSet();
                    specManage.ExecQuery(tmpSql, ref ds);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        OutInfo o = new OutInfo();
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {

                            o.SpecBarCode = dt.Rows[0]["SUBBARCODE"].ToString();
                            o.SpecId = dt.Rows[0]["SUBSPECID"].ToString();
                            o.BoxBarCode = dt.Rows[0]["BOXBARCODE"].ToString();
                            o.ReturnAble = "0";
                            o.Count = 1.0M;
                            outInfoList.Add(o);
                            
                            int rowCount = neuSpread1_Sheet1.Rows.Count;
                            neuSpread1_Sheet1.Rows.Add(rowCount, 1);
                            neuSpread1_Sheet1.Rows[rowCount].Tag = o;
                            
                            neuSpread1_Sheet1.Cells[rowCount, 0].Text = o.SpecBarCode;
                            neuSpread1_Sheet1.Cells[rowCount, 1].Text = dt.Rows[0]["DISEASENAME"].ToString();
                            neuSpread1_Sheet1.Cells[rowCount, 2].Text = dt.Rows[0]["SPECIMENTNAME"].ToString();
                            neuSpread1_Sheet1.Cells[rowCount, 3].Text = ParseBoxCode(dt.Rows[0]["BOXBARCODE"].ToString());
                            neuSpread1_Sheet1.Rows[rowCount].BackColor = Color.Green;
                            dt.Dispose();
                            ds.Dispose();
                            txtBarCode.Text = "";
                        }
                    }
                }
            }
        }

        private void ucConfirmOut_Load(object sender, EventArgs e)
        {
            SetSql();
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
        }
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("出库", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C出库单, true, false, null);
            this.toolBarService.AddToolButton("删除", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            return this.toolBarService;
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "出库":
                    DialogResult result = MessageBox.Show("确定出库?", "出库", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                    this.Out();
                    break;
                case "删除":
                    this.Remove();
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

    }
}
