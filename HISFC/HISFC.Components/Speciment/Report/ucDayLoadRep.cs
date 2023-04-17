using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;

namespace FS.HISFC.Components.Speciment.Report
{
    public partial class ucDayLoadRep : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private SubSpecManage subMgr = new SubSpecManage();
        private string curSql = "";
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
         //select max(ss.SUBBARCODE), t.SPECIMENTNAME,d.DISEASENAME
         //from SPEC_SUBSPEC ss, spec_source s, SPEC_DISEASETYPE d, SPEC_TYPE t
          //where ss.SPECID = s.specid and s.DISEASETYPEID>14 and s.DISEASETYPEID = d.DISEASETYPEID and t.SPECIMENTTYPEID = ss.SPECTYPEID
          //group by t.SPECIMENTNAME, d.DISEASENAME

        private enum CurPos
        {
            标本源ID,
            源条码,
            姓名,
            病历号,
            分装标本号,
            标本类型,
            病种, 
            分装数量,                       
            操作时间,
            送存医生,
            科室,
            取材脏器,
            肿物性质,
            SPECTYPEID

        }

        public ucDayLoadRep()
        {
            InitializeComponent();
        }

        private void InitSheet()
        {
            neuSpread1_Sheet1.Columns.Count = Convert.ToInt32(CurPos.SPECTYPEID) + 1;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.标本源ID)].Width = 80;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.标本源ID)].Label = CurPos.标本源ID.ToString();
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.源条码)].Width = 100;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.源条码)].Label = CurPos.源条码.ToString();
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.姓名)].Width = 80;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.姓名)].Label = CurPos.姓名.ToString();
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.病历号)].Width = 100;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.病历号)].Label = CurPos.病历号.ToString();
           
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.分装标本号)].Width = 120;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.分装标本号)].Label = CurPos.分装标本号.ToString();
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.分装数量)].Width = 80;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.分装数量)].Label = CurPos.分装数量.ToString();
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.病种)].Width = 40;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.病种)].Label = CurPos.病种.ToString();
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.标本类型)].Width = 80;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.标本类型)].Label = CurPos.标本类型.ToString();
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.操作时间)].Width = 90;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.操作时间)].Label = CurPos.操作时间.ToString();
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.送存医生)].Width = 100;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.送存医生)].Label = CurPos.送存医生.ToString();
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.科室)].Width = 120;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.科室)].Label = CurPos.科室.ToString();
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.取材脏器)].Width = 80;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.取材脏器)].Label = CurPos.取材脏器.ToString();
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.SPECTYPEID)].Width = 80;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.SPECTYPEID)].Label = CurPos.SPECTYPEID.ToString();
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.SPECTYPEID)].Visible=false;// = 80;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.肿物性质)].Width = 80;
            neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.肿物性质)].Label = CurPos.肿物性质.ToString();
            //for (int i = 0; i < neuSpread1_Sheet1.Columns.Count; i++)
            //{
                neuSpread1_Sheet1.ColumnHeader.Rows[0].Height = 30;
            //}
        }

        private void SetSql()
        {
            string cardNo = txtCardNo.Text.Trim();
            string condition1 = "s.OPERTIME BETWEEN '{0}' AND '{1}'";
            string condition2 = "s.ORGORBLOOD='B'";
            string condition3 = "s.ORGORBLOOD='O'";
            string condition4 = "p.CARD_NO = '{2}'";
            string condtion = "";

            if (rbtBld.Checked)
            {
                curSql = @"
                       SELECT DISTINCT s.SPECID 标本源ID,s.HISBARCODE 源条码,  p.name 姓名, t.SPECIMENTNAME 标本类型,
		              (SELECT (CASE length(ss.subbarcode) WHEN 9 then substr(ss.SUBBARCODE,2,6) ELSE substr(ss.SUBBARCODE,1,6) END) FROM SPEC_SUBSPEC ss WHERE ss.SPECID = s.specid FETCH FIRST 1 ROWS only) 分装标本号,
                       st.SUBCOUNT 分装数量, d.DISEASENAME 病种, p.CARD_NO 病历号,
		               s.OPERTIME 操作时间,  (SELECT e.EMPL_NAME FROM COM_EMPLOYEE e WHERE e.EMPL_CODE =  s.SENDDOCID) 送存医生,
                       (SELECT cd.DEPT_NAME FROM COM_DEPARTMENT cd WHERE cd.DEPT_CODE = s.DEPTNO FETCH FIRST 1 ROWS ONLY)科室,
                       st.SPECTYPEID
                       from SPEC_SOURCE s join SPEC_SOURCE_STORE st on s.SPECID = st.SPECID 
                       join SPEC_DISEASETYPE d on d.DISEASETYPEID = s.DISEASETYPEID
                       join SPEC_TYPE t ON st.SPECTYPEID = t.SPECIMENTTYPEID
                       join SPEC_PATIENT p on p.PATIENTID = s.PATIENTID";
                      // WHERE (s.OPERTIME BETWEEN '{0}' AND '{1}' and s.ORGORBLOOD='B')";                
                if (cardNo != "")
                {
                    if (rbtAnd.Checked)
                    {
                        condtion = condition1 + " And " + condition2 + " And " + condition4;
                        //curSql += " And p.CARD_NO = '{2}'";
                    }
                    if (rbtOr.Checked)
                    {
                        condtion = "(" + condition1 + " OR " + condition4 + ") And " + condition2;
                        //curSql += " Or p.CARD_NO = '{2}'";
                    }
                }
                else
                {
                    condtion = condition1 + " And " + condition2;
                }
            }
            if (rbtOrg.Checked)
            {
                curSql = @"SELECT DISTINCT s.SPECID 标本源ID,s.HISBARCODE 源条码, p.name 姓名,
                          st.SUBCOUNT 分装数量, d.DISEASENAME 病种,st.tumorPOS 取材脏器,p.CARD_NO 病历号,
		             (SELECT  substr(ss.SUBBARCODE,1,6)  FROM SPEC_SUBSPEC ss WHERE ss.SPECID = s.specid FETCH FIRST 1 ROWS only) 分装标本号,
		             t.SPECIMENTNAME 标本类型,s.OPERTIME 操作时间,  (SELECT e.EMPL_NAME FROM COM_EMPLOYEE e WHERE e.EMPL_CODE =  s.SENDDOCID) 送存医生,
		             (SELECT cd.DEPT_NAME FROM COM_DEPARTMENT cd WHERE cd.DEPT_CODE = s.DEPTNO FETCH FIRST 1 ROWS ONLY)科室,
		             st.SPECTYPEID,
					 (case st.TUMORTYPE when '1' then '肿物' when '2' then '子瘤' when '3' then '癌旁' when '4' then '正常' when '5' then '癌栓' when '8' then '淋巴结' else ''end) 肿物性质
		             from SPEC_SOURCE s join SPEC_SOURCE_STORE st on s.SPECID = st.SPECID 
		             join SPEC_DISEASETYPE d on d.DISEASETYPEID = s.DISEASETYPEID
		             join SPEC_TYPE t ON st.SPECTYPEID = t.SPECIMENTTYPEID
                     join SPEC_PATIENT p on p.PATIENTID = s.PATIENTID ";
		            // WHERE (s.ORGORBLOOD='O' and s.OPERTIME BETWEEN '{0}' AND '{1}')
                if (cardNo != "")
                {
                    if (rbtAnd.Checked)
                    {
                        condtion = condition1 + " And " + condition3 + " And " + condition4;
                        //curSql += " And p.CARD_NO = '{2}'";
                    }
                    if (rbtOr.Checked)
                    {
                        condtion = "(" + condition1 + " OR " + condition4 + ") And " + condition3;
                        //curSql += " Or p.CARD_NO = '{2}'";
                    }
                }
                else
                {
                    condtion = condition1 + " And " + condition3;
                }
            }
            if (condtion.Trim() != "")
            {
                curSql += " WHERE " + condtion;
            }
            curSql += " order by s.SPECID,s.HISBARCODE";
        }

        private void ucDayLoadRep_Load(object sender, EventArgs e)
        {
            rbtBld.Checked = true;
            SetSql();
            this.InitSheet();
        }

        private void LoadPos()
        {
            if (chkPos.Checked)
            {
                neuSpread1_Sheet1.ColumnCount++;
                neuSpread1_Sheet1.Columns[neuSpread1_Sheet1.Columns.Count - 1].Label = " 标本位置详情";
                neuSpread1_Sheet1.Columns[neuSpread1_Sheet1.Columns.Count - 1].Width = 300;
                for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
                {
                    string specId = neuSpread1_Sheet1.Cells[i, 0].Text.Trim();
                    string specTypeId = neuSpread1_Sheet1.Cells[i, neuSpread1_Sheet1.ColumnCount - 2].Text.Trim();
                    ArrayList arrSub = subMgr.GetSubSpecBySpecId(specId);
                    Dictionary<string, List<string>> dicPos = new Dictionary<string, List<string>>();

                    foreach (SubSpec s in arrSub)
                    {
                        if (s.SpecTypeId.ToString() != specTypeId)
                        {
                            continue;
                        }
                        SpecBoxManage boxMgr = new SpecBoxManage();
                        SpecBox box = new SpecBox();
                        box = boxMgr.GetBoxById(s.BoxId.ToString());
                        string loc = ParseLocation.ParseSpecBox(box.BoxBarCode);
                        if (dicPos.ContainsKey(loc))
                        {
                            dicPos[loc].Add(s.SubBarCode);
                        }
                        else
                        {
                            dicPos.Add(loc, new List<string>());
                            dicPos[loc].Add(s.SubBarCode);
                        }
                    }
                    string subLoc = "";
                    foreach (KeyValuePair<string, List<string>> kv in dicPos)
                    {
                        subLoc += kv.Key + ",\n";
                        foreach (string s in kv.Value)
                        {
                            subLoc += " " + s + ",";
                        }
                    }
                    neuSpread1_Sheet1.Cells[i, neuSpread1_Sheet1.Columns.Count - 1].Text = subLoc;
                    //neuSpread1_Sheet1[i]
                }
            }             
        }

        private void GetSubPos()
        {
            try
            {
                SpecOutOper specOut = new SpecOutOper();
                ArrayList arrSpec = new ArrayList();
                string tmpSql = @"select * from SPEC_subspec  where STORETIME between '{0}' and '{1}'";
                string sql = string.Format(tmpSql, dtpStartDate.Value.Date.ToString(), dtpEndTime.Value.Date.AddDays(1).ToString(), txtCardNo.Text.Trim().PadLeft(10,'0'));
                specOut.PrintTitle = "标本位置详情";
                arrSpec = subMgr.GetSubSpec(sql);
                List<SubSpec> subList = new List<SubSpec>();               
                foreach (SubSpec s in arrSpec)
                {
                    subList.Add(s);                   
                }
                specOut.PrintOutSpec(subList, null);
            }
            catch
            { }

        }

        public override int Query(object sender, object neuObject)
        {
            try
            {
                SetSql();
                neuSpread1_Sheet1.RowCount = 0;
                string strStart = dtpStartDate.Value.Date.ToString();
                string strEnd = dtpEndTime.Value.Date.AddDays(1.0).ToString();
                string tmpSql = curSql;
                string cardNo = txtCardNo.Text.Trim().PadLeft(10, '0');
                tmpSql = string.Format(tmpSql, strStart, strEnd,cardNo);
                DataSet ds = new DataSet();
                subMgr.ExecQuery(tmpSql, ref ds);
                //neuSpread1_Sheet1.DataSource = ds;
                //neuSpread1_Sheet1.Columns[neuSpread1_Sheet1.ColumnCount - 1].Visible = false;
                neuSpread1_Sheet1.AutoGenerateColumns = false;
                neuSpread1_Sheet1.DataAutoSizeColumns = false;
                neuSpread1_Sheet1.DataSource = ds;
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.标本源ID), "标本源ID");               
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.源条码), "源条码");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.姓名), "姓名"); 
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.分装数量), "分装数量"); 
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.病种), "病种"); 
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.标本类型), "标本类型"); 
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.操作时间), "操作时间"); 
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.送存医生), "送存医生"); 
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.科室), "科室");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.病历号), "病历号");
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.分装标本号), "分装标本号");
                //neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.肿物性质), "肿物性质");
                if (!rbtBld.Checked)
                {
                    neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.取材脏器), "取材脏器");
                    neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.肿物性质), "肿物性质");
                }
                if (rbtBld.Checked)
                {
                    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.取材脏器)].Visible = false;
                    neuSpread1_Sheet1.Columns[Convert.ToInt32(CurPos.肿物性质)].Visible = false;
                }
                neuSpread1_Sheet1.BindDataColumn(Convert.ToInt32(CurPos.SPECTYPEID), "SPECTYPEID");
                for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
                {
                    neuSpread1_Sheet1.Rows[i].Height = 25;
                }
                for (int i = 1; i < neuSpread1_Sheet1.ColumnCount; i++)
                {
                    neuSpread1_Sheet1.Columns[i].AllowAutoFilter = true;
                    neuSpread1_Sheet1.Columns[i].AllowAutoSort = true;
                }
            }
            catch
            { }
            return base.Query(sender, neuObject);
        }

        public override int Print(object sender, object neuObject)
        {
            return base.Print(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            string path = string.Empty;
            SaveFileDialog saveFileDiaglog = new SaveFileDialog();

            saveFileDiaglog.Title = "查询结果导出，选择Excel文件保存位置";
            saveFileDiaglog.RestoreDirectory = true;
            saveFileDiaglog.InitialDirectory = Application.StartupPath;
            saveFileDiaglog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDiaglog.FileName = DateTime.Now.ToShortDateString() + "标本";
            DialogResult dr = saveFileDiaglog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                this.neuSpread1.SaveExcel(saveFileDiaglog.FileName, FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders);
            }
            return base.Export(sender, neuObject);
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("位置详情", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印预览, true, false, null);
            this.toolBarService.AddToolButton("打印位置信息", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            this.toolBarService.AddToolButton("标本详情", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);
            this.toolBarService.AddToolButton("病人信息查询", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            this.toolBarService.AddToolButton("撤销取消", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.F返回, true, false, null);

            return this.toolBarService;
            //return base.OnInit(sender, neuObject, param);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "位置详情":
                    this.GetSubPos();
                    break;
                case "标本详情":

                    try
                    {
                        int specId = Convert.ToInt32(neuSpread1_Sheet1.Cells[neuSpread1_Sheet1.ActiveRow.Index, Convert.ToInt32(CurPos.标本源ID)].Text.Trim());

                        if (rbtBld.Checked)
                        {
                            ucSpecimentSource ucBld = new ucSpecimentSource();
                            ucBld.SpecId = specId;
                            Size size = new Size();
                            size.Height = 800;
                            size.Width = 1280;
                            ucBld.Size = size;

                            FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucBld, FormBorderStyle.FixedSingle, FormWindowState.Maximized);
                            //FS.FrameWork.WinForms.Classes.Function.PopForm popForm = new FS.FrameWork.WinForms.Forms.BaseForm();


                        }
                        if (rbtOrg.Checked)
                        {
                            ucSpecSourceForOrg ucOrg = new ucSpecSourceForOrg();
                            ucOrg.SpecId = specId;
                            // FormBorderStyle formStyle = new FormBorderStyle();
                            Size size = new Size();
                            size.Height = 800;
                            size.Width = 1280;
                            ucOrg.Size = size;
                            FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucOrg, FormBorderStyle.FixedSingle, FormWindowState.Maximized);
                            //FS.FrameWork.WinForms.Classes.Function.PopForm popForm = new FS.FrameWork.WinForms.Forms.BaseForm();

                        }
                    }
                    catch
                    { }
                    break;
                //
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        private void chkPos_CheckedChanged(object sender, EventArgs e)
        {        
        }

    }
}
