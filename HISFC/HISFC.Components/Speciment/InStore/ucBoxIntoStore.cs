using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;

namespace FS.HISFC.Components.Speciment.InStore
{
    public partial class ucBoxIntoStore : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 变量域
        Dictionary<int, string> dicBaseData;
        private BoxSpecManage boxSpecManage;
        private DisTypeManage disTypeMange;
        private OrgTypeManage orgTypeManage;
        private SpecTypeManage specTypeManage;
        private SubSpecManage subSpecManage;
        private SpecBoxManage specBoxManage;
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService;

        private string title;
        private string sql;
        #endregion

        #region 构造函数
        public ucBoxIntoStore()
        {
            InitializeComponent();
            title = "标本盒入库";
            boxSpecManage = new BoxSpecManage();
            disTypeMange = new DisTypeManage();
            orgTypeManage = new OrgTypeManage();
            specTypeManage = new SpecTypeManage();
            specBoxManage = new SpecBoxManage();
            subSpecManage = new SubSpecManage();
            toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 下拉菜单数据格绑定
        /// </summary>
        private void BoxBinding()
        {
            //标本盒规格
            dicBaseData = boxSpecManage.GetAllBoxSpec();
            BindDataToComboBox(dicBaseData, cmbBoxSpec);

            //标本种类
            dicBaseData = orgTypeManage.GetAllOrgType();
            BindDataToComboBox(dicBaseData, cmbOrgType);

            //病种
            dicBaseData = disTypeMange.GetAllDisType();
            BindDataToComboBox(dicBaseData, cmbDiseaseType);

        }

        /// <summary>
        /// 绑定数据到下拉菜单上
        /// </summary>
        /// <param name="dicDataSource"></param>
        /// <param name="comBox"></param>
        private void BindDataToComboBox(Dictionary<int, string> dicDataSource, ComboBox comBox)
        {
            comBox.DataSource = null;
            if (dicBaseData.Count > 0)
            {
                BindingSource bsTemp = new BindingSource();
                bsTemp.DataSource = dicDataSource;
                comBox.DisplayMember = "Value";
                comBox.ValueMember = "Key";
                comBox.DataSource = bsTemp;
                comBox.SelectedValue="0";
            }
        }        

        /// <summary>
        /// 根据组织类型ID获取标本类型并绑定到下拉菜单
        /// </summary>
        /// <param name="orgTypeID"></param>
        private void SpecTypeBinding(string orgTypeID)
        {
            dicBaseData = specTypeManage.GetSpecTypeByOrgID(orgTypeID);
            BindDataToComboBox(dicBaseData, cmbSpecType);
        }

        private string FormatSql()
        {
            int specid = 0;
            if (tSpecId.Text != null)
            {
                try
                {
                    if (!string.IsNullOrEmpty(this.tSpecId.Text.ToString().Trim()))
                    {
                        specid = Convert.ToInt32(this.tSpecId.Text.ToString());
                    }
                }
                catch
                {
                    MessageBox.Show("标本号输入异常字符！");
                    return string.Empty;
                }
            }
            sql = "SELECT  DISTINCT SPEC_BOX.*, '3' as CNT, TRIM(CHAR(SPEC_BOX_SPEC.SPECROW))||'*'||TRIM(CHAR(SPEC_BOX_SPEC.SPECCOL)) BOXSPEC, SPEC_ORGTYPE.ORGNAME,SPEC_TYPE.SPECIMENTNAME,SPEC_DISEASETYPE.DISEASENAME\n";
                  sql += " ,(CAST(SPEC_BOX.OCCUPYCOUNT AS DECIMAL(5,2))/SPEC_BOX.CAPACITY)*100 BLANKPER\n";
                  sql += " ,SPEC_ICEBOX.ICEBOXNAME\n";
                  sql += " , (SELECT SUBSTR(SS.SUBBARCODE,1,7) FROM SPEC_SUBSPEC SS WHERE SS.BOXENDROW =1 AND SS.BOXENDCOL =1 AND SS.BOXID = SPEC_BOX.BOXID FETCH FIRST 1 ROWS ONLY) startNo\n" ;
                  //sql += " , (SELECT SUBSTR(SS.SUBBARCODE,1,7) FROM SPEC_SUBSPEC SS WHERE SS.BOXENDROW =1 AND SS.BOXENDCOL =1 AND SS.BOXID = SPEC_BOX.BOXID FETCH FIRST 1 ROWS ONLY) startNo\n";

                  sql += " FROM SPEC_ICEBOX,SPEC_BOX\n " +
                                    " LEFT JOIN SPEC_BOX_SPEC ON SPEC_BOX.BOXSPECID=SPEC_BOX_SPEC.BOXSPECID\n" +
                                    " LEFT JOIN SPEC_DISEASETYPE ON SPEC_BOX.DISEASETYPEID=SPEC_DISEASETYPE.DISEASETYPEID\n" +
                                    " LEFT JOIN SPEC_TYPE ON SPEC_BOX.SPECTYPEID=SPEC_TYPE.SPECIMENTTYPEID\n" +
                                    " LEFT JOIN SPEC_ORGTYPE ON SPEC_TYPE.ORGTYPEID = SPEC_ORGTYPE.ORGTYPEID\n"+
                                    " LEFT JOIN SPEC_SUBSPEC ON SPEC_BOX.BOXID= SPEC_SUBSPEC.BOXID\n"+
                                    " LEFT JOIN SPEC_SOURCE ON SPEC_SUBSPEC.SPECID= SPEC_SOURCE.SPECID";
                  sql += " WHERE SPEC_BOX.BOXID>0 AND integer(SUBSTR(SPEC_BOX.BOXBARCODE,3,3)) = SPEC_ICEBOX.ICEBOXID\n";
                  sql += cmbBoxSpec.SelectedValue == null ? "" : " AND SPEC_BOX.BOXSPECID =" + cmbBoxSpec.SelectedValue.ToString();
                  sql += cmbSpecType.SelectedValue == null ? "" : " AND SPEC_TYPE.SPECIMENTTYPEID =" + cmbSpecType.SelectedValue.ToString();
                  sql += cmbDiseaseType.SelectedValue == null ? "" : " AND SPEC_DISEASETYPE.DISEASETYPEID=" + cmbDiseaseType.SelectedValue.ToString();
                  sql += specid == 0 ? "" : " AND SPEC_SOURCE.SPEC_NO like '%" + specid + "%'";
                  sql += chkIsOccupy.Checked ? " AND SPEC_BOX.ISOCCUPY = '1'" : "";
                  sql += txtBoxCode.Text.Trim() == "" ? "" : " AND SPEC_BOX.BOXBARCODE LIKE '%" + txtBoxCode.Text.Trim() + "%'";
                  sql += " order by SPEC_BOX.boxid desc";
            return sql;
 
        }

        /// <summary>
        /// 将查询结果绑定到Grid上
        /// </summary>
        /// <param name="boxList"></param>
        private void BindDataToGrid(DataSet ds)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                spreadSpecBoxInfo_Sheet1.AutoGenerateColumns = false;
                spreadSpecBoxInfo_Sheet1.DataAutoSizeColumns = false;
                spreadSpecBoxInfo_Sheet1.DataSource = dt;
                spreadSpecBoxInfo_Sheet1.ColumnCount = 13;
                spreadSpecBoxInfo_Sheet1.BindDataColumn(1, "CNT");
                spreadSpecBoxInfo_Sheet1.BindDataColumn(2, "BOXBARCODE");
                spreadSpecBoxInfo_Sheet1.BindDataColumn(3, "STARTNO");
                spreadSpecBoxInfo_Sheet1.BindDataColumn(4, "ICEBOXNAME");
                spreadSpecBoxInfo_Sheet1.BindDataColumn(5, "BOXSPEC");
                spreadSpecBoxInfo_Sheet1.BindDataColumn(6, "CAPACITY");
                spreadSpecBoxInfo_Sheet1.BindDataColumn(7, "OCCUPYCOUNT");
                spreadSpecBoxInfo_Sheet1.BindDataColumn(8, "BLANKPER");
                spreadSpecBoxInfo_Sheet1.BindDataColumn(9, "ORGNAME");
                spreadSpecBoxInfo_Sheet1.BindDataColumn(10, "SPECIMENTNAME");
                spreadSpecBoxInfo_Sheet1.BindDataColumn(11, "DISEASENAME");
                spreadSpecBoxInfo_Sheet1.BindDataColumn(12, "BOXID");
                for (int i = 0; i < spreadSpecBoxInfo_Sheet1.Rows.Count; i++)
                {
                    spreadSpecBoxInfo_Sheet1.Rows[i].Height = 28;
                }
                spreadSpecBoxInfo_Sheet1.Columns[12].Visible = false;
            }
            catch
            { }
        }

        private void InStore()
        {
            //List<string> listSelected = new List<string>();
            ////foreach(CheckBox chk in spreadSpecBoxInfo_Sheet1.Columns[0]
            //for (int i = 0; i < spreadSpecBoxInfo_Sheet1.Rows.Count; i++)
            //{
            //    if (spreadSpecBoxInfo_Sheet1.Cells[i, 0].Value != null && spreadSpecBoxInfo_Sheet1.Cells[i, 0].Value.ToString() == "1")
            //    {
            //        string barCode = spreadSpecBoxInfo_Sheet1.Cells[i, 1].Value.ToString();
            //        listSelected.Add(barCode);
            //    }
            //}
            //foreach (string barCode in listSelected)
            //{
            //    string sql = "UPDATE SPEC_BOX SET INBOX='1' WHERE BOXBARCODE = '" + barCode + "'";
            //    int result = subSpecManage.UpdateSubSpec(sql);
            //    if (result == -1)
            //    {
            //        MessageBox.Show("入库失败！", title);
            //        return;
            //    }
            //}
            //MessageBox.Show("入库成功！", title);
            //Query(null, null);
            //UFC.Speciment.Setting.frmSpecBox frmspecBox = new UFC.Speciment.Setting.frmSpecBox();

        }

        private void PrintBarCode()
        {
            try
            {
                for (int i = 0; i < spreadSpecBoxInfo_Sheet1.RowCount; i++)
                {
                    string selected = "";
                    if (spreadSpecBoxInfo_Sheet1.Cells[i, 0].Value == null)
                    {
                        continue;
                    }
                    selected = spreadSpecBoxInfo_Sheet1.Cells[i, 0].Value.ToString();
                    if (selected == "1" || selected.ToUpper() == "TRUE")
                    {
                        string code = "";
                        string boxid = "";
                        if (spreadSpecBoxInfo_Sheet1.Cells[i, 2].Value == null)
                        {
                            continue;
                        }
                        if (spreadSpecBoxInfo_Sheet1.Cells[i, 12].Value == null)
                        {
                            continue;
                        }
                        code = spreadSpecBoxInfo_Sheet1.Cells[i, 2].Text;
                        boxid = spreadSpecBoxInfo_Sheet1.Cells[i, 12].Text;
                        int cnt = Convert.ToInt32(spreadSpecBoxInfo_Sheet1.Cells[i, 1].Text.ToString());

                        PrintLabel.PrintBoxBarCode(boxid, code,null,cnt);
                     }
                    
                }
            }
            catch
            { }
        }


        private void BrowseBox()
        {
            try
            {
                int rowIndex = spreadSpecBoxInfo_Sheet1.ActiveRowIndex;
                if (rowIndex < 0)
                {
                    return;
                }

                string value = "";
                if (spreadSpecBoxInfo_Sheet1.Cells[rowIndex, 1].Value == null)
                {
                    return;
                }
                value = spreadSpecBoxInfo_Sheet1.Cells[rowIndex, 1].Value.ToString();

                FS.HISFC.Models.Speciment.SpecBox tmp = specBoxManage.GetBoxByBarCode(value);
                if (tmp != null && tmp.BoxBarCode != "" && tmp.OccupyCount > 0)
                {
                    ucSpecDetInBox ucBox = new ucSpecDetInBox();
                    ucBox.BoxId = tmp.BoxId;

                    BoxSpecManage bsM = new BoxSpecManage();
                    FS.HISFC.Models.Speciment.BoxSpec bs = bsM.GetSpecByBoxId(tmp.BoxId.ToString());
                    //Size size = new Size();
                    //size.Height = (bs.Row + 1) * 25 + 20;
                    //size.Width = (bs.Col) * 75 + 20;
                    //ucBox.Size = size;
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucBox, FormBorderStyle.FixedSingle, FormWindowState.Normal);
                }


            }
            catch
            { }
        }

        #endregion

        #region 事件

        private void ucBoxIntoStore_Load(object sender, EventArgs e)
        {
            //spreadSpecBoxInfo_Sheet1.Columns[10].Visible = false;
            BoxBinding();
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("修改", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            this.toolBarService.AddToolButton("入库", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S手动录入, true, false, null);
            this.toolBarService.AddToolButton("打印标本盒条码", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印执行单, true, false, null);
            this.toolBarService.AddToolButton("查看盒内标本", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.L浏览, true, false, null);
            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "修改":
                    break;
                case "入库":
                    InStore();
                    break;
                case "打印标本盒条码":
                    PrintBarCode();
                    break;
                case "查看盒内标本":
                    BrowseBox();
                    break;
                default:
                    break;
            }
        }

        public override int Query(object sender, object neuObject)
        {
            //if (this.tSpecId.Text.ToString() != "")
            //{
            //    try
            //    {
            //        Convert.ToInt32(this.tSpecId.Text.ToString());
            //    }
            //    catch
            //    {
            //        MessageBox.Show("标本号只能有数字组成！", title);
            //        return base.Query(sender, neuObject);
            //    }
            //}
            string sql = this.FormatSql();
            DataSet ds = new DataSet();
            if (!string.IsNullOrEmpty(sql))
            {
                specBoxManage.GetBoxBySql(sql, ref ds);
            }
            if (ds == null || ds.Tables.Count <= 0)
                return 0;
            BindDataToGrid(ds);
            return base.Query(sender, neuObject);
        } 

        private void cmbSpecType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int orgId = Convert.ToInt32((cmbOrgType.SelectedValue ?? -1));
            if (orgId > 0)
            {
                SpecTypeBinding(orgId.ToString());
            }
        }
 
        #endregion

        private void nudCopy_ValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < spreadSpecBoxInfo_Sheet1.RowCount; i++)
            {
                spreadSpecBoxInfo_Sheet1.Cells[i, 1].Text = nudCopy.Value.ToString();
            }
        }
    }
}
