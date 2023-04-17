using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Speciment.Print
{
    public partial class frmPrintApplyTable :FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private string title;
        private ApplyTableManage applyTableManage;
        private ApplyTable applyTable;
        private ApplyTableForReport applyForReport;
        private SpecTypeManage specTypeMange;
        private DisTypeManage disTypeManage;
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService;

        public frmPrintApplyTable()
        {
            InitializeComponent();
            applyTable = new ApplyTable();
            applyTableManage = new ApplyTableManage();
            applyForReport = new ApplyTableForReport();
            specTypeMange = new SpecTypeManage();
            disTypeManage = new DisTypeManage();
            toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
            title = "��ӡ���뵥";
        }  

        private void SetApplyReport()
        {
            applyForReport.ApplyEmail = applyTable.ApplyEmail;
            applyForReport.ApplyId = applyTable.ApplyId.ToString();
            applyForReport.ApplyTel = applyTable.ApplyTel;
            applyForReport.ApplyTime = applyTable.ApplyTime.ToShortDateString();
            applyForReport.ApplyUserName = applyTable.ApplyUserName;
            applyForReport.DeptName = applyTable.DeptName;
            applyForReport.FundEndTime = applyTable.FundEndTime.ToShortDateString();
            applyForReport.FundId = applyTable.FundId;
            applyForReport.FundName = applyTable.FundName;
            applyForReport.FundStartTime = applyTable.FundStartTime.ToShortDateString();
            applyForReport.LeftAmount = applyTable.LeftAmount;
            applyForReport.OtherDemand = applyTable.OtherDemand;
            applyForReport.Percent = applyTable.Percent;
            applyForReport.ResPlan = applyTable.ResPlan;
            applyForReport.SpecAmout = applyTable.SpecAmout.ToString();
            applyForReport.SpecCountInDpet = applyTable.SpecCountInDpet;
            applyForReport.SpecDetAmout = applyTable.SpecDetAmout;
            applyForReport.ImpEmail = applyTable.ImpEmail;
            applyForReport.ImpName = applyTable.ImpName;
            applyForReport.ImpTel = applyTable.ImpTel;
            //string[] specCancerList = applyTable.SpecIsCancer.Split(',');
            //string specCancer = "";
            //for (int i = 0; i < specCancerList.Length; i++)
            //{
            //    Constant.TumorType TumorType = (Constant.TumorType)(Convert.ToInt32(specCancerList[i].ToString()));
            //    specCancer += TumorType.ToString();               
            //}
            //// specCancer.Remove(specCancer.Length - 1, 1);
            //applyForReport.SpecIsCancer = specCancer;//specCancer.Substring(0, specCancer.Length - 1);

            //�걾����
            //if(applyTable.SpecType=="")
            //{
            //    applyForReport.SpecType = "";
            //}
            //else
            //{
            //    string[] specType = applyTable.SpecType.Split(',');
            //    int index = 0;
            //    foreach (string type in specType)
            //    {
            //        applyForReport.SpecType += specTypeMange.GetSpecTypeById(type).SpecTypeName;
            //        if (index != specType.Length - 1)
            //        {
            //            applyForReport.SpecType += ",";
            //        }
            //        index++;
            //    }
            //}
            applyForReport.SpecType = applyTable.SpecType;
            
            //����
            if(applyTable.DiseaseType=="")
            {
                applyForReport.DiseaseType = "";
            }
            else
            {
                string[] disType = applyTable.DiseaseType.Split(',');
                int index = 0;
                foreach (string dis in disType)
                {
                    applyForReport.DiseaseType += disTypeManage.SelectDisByID(dis).DiseaseName;
                    if (index != disType.Length - 1)
                    {
                        applyForReport.DiseaseType += ",";
                    }
                    index++;
                }
            }

            applyForReport.SpecList = applyTable.SpecList;
            applyForReport.SubjectId = applyTable.SubjectId;
            applyForReport.SubjectName = applyTable.SubjectName;
        }

        private void RecentlyApply()
        {
            string sql = " Select SPEC_APPLICATIONTABLE.APPLICATIONID ����ID, SUBJECTNAME ��������,FUNDNAME ��������,FUNDSTARTTIME ����ʼʱ��,FUNDENDTIME �������ʱ��,\n" +
                         " Case IMPPROCESS when 'O' then '��������' when 'C' then 'ȡ��' when 'U' then '������' end ��������,\n" +
                         " case IMPRESLUT when '1' then 'ͬ��' when '0' then '�ܾ�' end �������\n" +
                         " FROM SPEC_USERAPPLICATION RIGHT JOIN SPEC_APPLICATIONTABLE ON SPEC_USERAPPLICATION.APPLICATIONID = SPEC_APPLICATIONTABLE.APPLICATIONID\n" +
                         " WHERE SPEC_APPLICATIONTABLE.APPLICATIONID>0 AND APPLYTIME between to_date('{0}','yyyy-mm-dd hh24:mi:ss') and to_date('{1}','yyyy-mm-dd hh24:mi:ss') and APPLYUSERID = '{2}'";

            sql = string.Format(sql, dtpFundStartTime.Value.Date.ToString(), dtpFundEndTime.Value.AddDays(1.0).Date.ToString(),applyTableManage.Operator.ID);
             
            DataSet dsTmp = new DataSet();
            applyTableManage.ExecQuery(sql, ref dsTmp);
            if (dsTmp.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = dsTmp.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["����ʼʱ��"] != null)
                    {
                        dt.Rows[i]["����ʼʱ��"] = Convert.ToDateTime(dt.Rows[i]["����ʼʱ��"].ToString()).ToString("yyyy-MM-dd");
                    }
                    if (dt.Rows[i]["�������ʱ��"] != null)
                    {
                        dt.Rows[i]["�������ʱ��"] = Convert.ToDateTime(dt.Rows[i]["�������ʱ��"].ToString()).ToString("yyyy-MM-dd");
                    }
                }
                neuSpread1_Sheet1.DataSource = null;
                neuSpread1_Sheet1.DataSource = dt;
            }
        }

        private void LoadApply(int applyNum)
        {
            if (applyNum == 0)
            {
                return;
            }
            try
            {
                applyTable = new ApplyTable();
                applyTable = applyTableManage.QueryApplyByID(applyNum.ToString());
                //ΪapplyTable��ӱ걾�б�
                applyForReport = new ApplyTableForReport();
                SetApplyReport();
                string specList = "";
                SpecOutManage outMgr = new SpecOutManage();
                DataSet ds = new DataSet();
                string sql = "select SUBSPECBARCODE from SPEC_APPLY_OUT where RELATEID =" + applyTable.ApplyId.ToString() + " and OPER <> 'Del'";
                outMgr.ExecQuery(sql, ref ds);

                if (ds == null || ds.Tables.Count <= 0)
                {
                    //MessageBox.Show("��ȡ����걾ʧ��");
                    applyForReport.SpecList = "";
                }
                else
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        try
                        {
                            //lsSpecOut.Items.Add(r[0].ToString());
                            specList += r[0].ToString() + ",";
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    if (specList == "")
                    {
                        applyForReport.SpecList = "";
                    }
                    else
                    {
                        specList = specList.Substring(0, specList.Length - 1);
                        applyForReport.SpecList = specList;
                    }
                }
                List<ApplyTableForReport> list = new List<ApplyTableForReport>();
                list.Add(applyForReport);
                rptApplyTable rpt = new rptApplyTable();
                rpt.SetDataSource(list);
                crystalReportViewer1.ReportSource = rpt;
             }
            catch
            {
                MessageBox.Show("��������ȷ�ĵ���!", title, MessageBoxButtons.OKCancel);
                return;
            }
        }

        public void PrintApplication(int applyNum)
        {
            this.LoadApply(applyNum);
            this.crystalReportViewer1.PrintReport();
        }

        private void rbt_Changed(object sender, EventArgs e)
        {
            RecentlyApply();
        }

        private void neuSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            int rowIndex = neuSpread1_Sheet1.ActiveRowIndex;
            int applyNum = 0;
            if (rowIndex >= 0 && neuSpread1_Sheet1.RowCount > 0)
            {
                applyNum = Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIndex, 0].Text);
                LoadApply(applyNum);
            }
        }

        private void frmPrintApplyTable_Load(object sender, EventArgs e)
        {
            dtpFundStartTime.Value = System.DateTime.Now.AddMonths(-1);
            RecentlyApply();
        }

        public override int Print(object sender, object neuObject)
        {
            this.crystalReportViewer1.PrintReport();
            return base.Print(sender, neuObject);
        }

        public override int Query(object sender, object neuObject)
        {
            this.RecentlyApply();
            return base.Query(sender, neuObject);
        }
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            
            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                
                case "����":
                    int rowIndex = neuSpread1_Sheet1.ActiveRowIndex;
                    if (rowIndex < 0)
                    {
                        return;
                    }
                    int applyNum = 0;
                    if (rowIndex >= 0 && neuSpread1_Sheet1.RowCount > 0)
                    {
                        applyNum = Convert.ToInt32(neuSpread1_Sheet1.Cells[rowIndex, 0].Text); 
                    }
                    frmApplyTable frmApply = new frmApplyTable();
                    frmApply.ApplyNum = Convert.ToInt32(applyNum);
                    Size size = new Size();
                    size.Height = 700;
                    size.Width = 1000;
                    frmApply.Size = size;
                    frmApply.ApplyOrImp = "";
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(frmApply, FormBorderStyle.FixedSingle, FormWindowState.Normal);

                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
      
    }
}