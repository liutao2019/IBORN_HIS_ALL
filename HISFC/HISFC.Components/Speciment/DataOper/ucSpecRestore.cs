using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment.DataOper
{
    public partial class ucSpecRestore : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        private IceBoxManage iceBoxManage;
        private SpecBoxManage boxManage;
        private SubSpecManage subSpecManage;
        private SpecInManage specInManage;
        private SpecOutManage specOutManage;
        private Dictionary<Locate, Locate> dicLocate;
        private List<SpecBox> fullBoxList;
        private string title = "标本盒合并撤销";

        public ucSpecRestore()
        {
            InitializeComponent();
            iceBoxManage = new IceBoxManage();                   
            boxManage = new SpecBoxManage(); 
            subSpecManage = new SubSpecManage();
            specInManage = new SpecInManage();
            specOutManage = new SpecOutManage();
        }

        /// <summary>
        /// 获取标本盒
        /// </summary>
        private void GetBox()
        {
            neuSpread2_Sheet1.RowCount = 0;
            DateTime dt = DateTime.Now.Date;
            string date = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString();
            string hourStart = cmbHour.Text.Trim();
            string hourEnd = cmbHour1.Text.Trim();
            string minStrat = Convert.ToInt32(nudMin.Value).ToString();
            string minEnd = Convert.ToInt32(nudMin1.Value).ToString();

            DateTime dtStart = Convert.ToDateTime(date + " " + hourStart +":" + minStrat);
            DateTime dtEnd = Convert.ToDateTime(date + " " + hourEnd+ ":" + minEnd);

            string sql = " select DISTINCT SPEC_IN.INID, SPEC_IN.SUBSPECID,SPEC_IN.SUBSPECBARCODE INSUBSPECBARCODE,SPEC_IN.BOXBARCODE INBARCODE,SPEC_IN.BOXCOL INCOL,SPEC_IN.BOXROW INROW, SPEC_IN.BOXID INBOXID,\n" +
                         " SPEC_OUT.OUTID, SPEC_OUT.BOXID OUTBOXID, SPEC_OUT.BOXBARCODE OUTBARCODE,SPEC_OUT.BOXCOL OUTCOL,SPEC_OUT.BOXROW OUTROW, SPEC_OUT.SUBSPECBARCODE OUTSUBSPECBARCODE\n" +
                         " from SPEC_IN inner join SPEC_OUT on SPEC_IN.SUBSPECID = SPEC_OUT.SUBSPECID\n" +
                         " WHERE SPEC_OUT.OUTDATE >= to_date('" + dtStart.ToString() + "','yyyy-mm-dd hh24:mi:ss') AND SPEC_OUT.OUTDATE<= to_date('" + dtEnd.ToString() + "','yyyy-mm-dd hh24:mi:ss')";

            DataSet ds = new DataSet();
            boxManage.ExecQuery(sql, ref ds);
            if (ds == null || ds.Tables.Count <= 0)
            {
                return;
            }
            DataBinding(ds.Tables[0]);
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="arrBox"></param>
        private void DataBinding(DataTable dtTable)
        {
            //对应的位置，入-出信息
            dicLocate = new Dictionary<Locate, Locate>();
            if (dtTable == null || dtTable.Rows.Count <= 0)
            {
                return;
            }
            Dictionary<string, List<string>> dicBoxId = new Dictionary<string, List<string>>();
            foreach (DataRow dr in dtTable.Rows)
            {
                string inBoxId = dr["INBOXID"].ToString();
                string outBoxId = dr["OUTBOXID"].ToString();
                Locate inLocate = new Locate();
                Locate outLocate = new Locate();
                inLocate.boxCol = dr["INCOL"].ToString();
                inLocate.boxId = dr["INBOXID"].ToString();
                //inLocate.boxBarCode = dr["INBARCODE"].ToString();
                inLocate.boxRow = dr["INROW"].ToString();
                inLocate.subSpecId = dr["SUBSPECID"].ToString();
                inLocate.subBarCode = dr["INSUBSPECBARCODE"].ToString();
                inLocate.id = dr["INID"].ToString();                

                outLocate.boxCol = dr["OUTCOL"].ToString();
                outLocate.boxId = dr["OUTBOXID"].ToString();
                //outLocate.boxBarCode = dr["OUTBARCODE"].ToString();
                outLocate.boxRow = dr["OUTROW"].ToString();
                outLocate.subSpecId = dr["SUBSPECID"].ToString();
                outLocate.subBarCode = dr["OUTSUBSPECBARCODE"].ToString();
                outLocate.id = dr["OUTID"].ToString();

                dicLocate.Add(inLocate, outLocate);

                if (dicBoxId.ContainsKey(inBoxId))
                {
                    dicBoxId[inBoxId].Add(outBoxId);
                }
                else
                {
                    dicBoxId.Add(inBoxId, new List<string>());
                    dicBoxId[inBoxId].Add(outBoxId);
                } 
            }

            DataTable dt = new DataTable();

            DataColumn dcl = new DataColumn();
            dcl.DataType = System.Type.GetType("System.String");
            dcl.ColumnName = "标本盒序列号";
            dt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.DataType = System.Type.GetType("System.String");
            dcl.ColumnName = "条码";
            dt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.DataType = System.Type.GetType("System.String");
            dcl.ColumnName = "位置";
            dt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.DataType = System.Type.GetType("System.String");
            dcl.ColumnName = "源盒位置";
            dt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.DataType = System.Type.GetType("System.String");
            dcl.ColumnName = "源盒序列号";
            dt.Columns.Add(dcl);

            dcl = new DataColumn();
            dcl.DataType = System.Type.GetType("System.String");
            dcl.ColumnName = "源盒条码";
            dt.Columns.Add(dcl);
            foreach (KeyValuePair<string, List<string>> tmp in dicBoxId)
            {
                DataRow dr = dt.NewRow();
                SpecBox inBox = boxManage.GetBoxById(tmp.Key);
                dr["标本盒序列号"] = inBox.BoxId.ToString();
                string barCode = inBox.BoxBarCode;
                dr["条码"] = barCode;
                dr["位置"] = iceBoxManage.GetIceBoxById(barCode.Substring(2, 3)).IceBoxName;
                dr["位置"] += barCode.Substring(5);// +"-" + barCode.Substring(7, 2) + "-" + barCode.Substring(9, 2);
                for (int i = 0; i < tmp.Value.Count; i++)
                {
                    SpecBox outBox = boxManage.GetBoxById(tmp.Value[i]);
                    string outBarCode = outBox.BoxBarCode;
                    dr["源盒序列号"] += outBox.BoxId.ToString();
                    dr["源盒条码"] += outBarCode;
                    string locate = iceBoxManage.GetIceBoxById(outBarCode.Substring(2, 3)).IceBoxName;
                    locate += outBarCode.Substring(5);// +"-" + outBarCode.Substring(7, 2) + "-" + outBarCode.Substring(9, 2);
                    if (!dr["源盒位置"].ToString().Contains(locate))
                    {
                        dr["源盒位置"] += locate + "  ";
                    }
                    if (i < tmp.Value.Count - 1)
                    {
                        dr["源盒条码"] += ",";                       
                        dr["源盒序列号"] += ",";
                    }
                }
                dt.Rows.Add(dr);   
            }

            neuSpread2_Sheet1.RowCount = 0;
            neuSpread2_Sheet1.AutoGenerateColumns = false;
            neuSpread2_Sheet1.DataSource = dt;
            neuSpread2_Sheet1.BindDataColumn(1, "标本盒序列号");
            neuSpread2_Sheet1.BindDataColumn(2, "条码");
            neuSpread2_Sheet1.BindDataColumn(3, "位置");
            neuSpread2_Sheet1.BindDataColumn(4, "源盒位置");
            neuSpread2_Sheet1.BindDataColumn(5, "源盒序列号");
            neuSpread2_Sheet1.BindDataColumn(6, "源盒条码");
        }

        /// <summary>
        /// 回滚操作
        /// </summary>
        /// <returns>1：成功，-1 失败 ，0 取消操作</returns>
        private int RollBack()
        {
            //Dictionary<string, string> dicBoxAndSource = new Dictionary<string,string>();
            //存储需要撤销的盒子及相关盒子信息SpecBox:目的盒，List<SpecBox> 源标本盒
            //Dictionary<SpecBox, List<SpecBox>> dicSpecBox = new Dictionary<SpecBox, List<SpecBox>>();
            //满盒子列表
            neuSpread1_Sheet1.Rows.Count = 0;
            fullBoxList = new List<SpecBox>();
            List<string> boxIdList = new List<string>();
            #region 选择需要还原的标本盒
            for (int i = 0; i < neuSpread2_Sheet1.Rows.Count; i++)
            {                
                if(neuSpread2_Sheet1.Cells[i,0].Value!=null)
                {
                    if (neuSpread2_Sheet1.Cells[i, 0].Value.ToString() == "1" || neuSpread2_Sheet1.Cells[i, 0].Value.ToString().ToLower() == "true")
                    {
                        string desBoxId = neuSpread2_Sheet1.Cells[i, 1].Text.Trim();
                        if (desBoxId != "")
                        {
                            boxIdList.Add(desBoxId);
                        }
                        //string sourceBoxId = neuSpread2_Sheet1.Cells[i, 5].Text.Trim();
                        //dicBoxAndSource.Add(desBoxId, sourceBoxId);
                    }
                }
            }
            if (boxIdList.Count <= 0)
            {
                return 0;
            }
            if (boxIdList.Count >= 1)
            {
               DialogResult result = MessageBox.Show("确定还原数据？", "数据还原", MessageBoxButtons.YesNo);
               if (result == DialogResult.No)
               {
                   return 0;
               }
           }           
            //对应的位置，入-出信息
            Dictionary<Locate, Locate> dicRollBack = new Dictionary<Locate, Locate>();
           foreach (string boxId in boxIdList)
           {
               ArrayList inSpecList = specInManage.GetInSpecInBox(boxId);
               if (inSpecList == null || inSpecList.Count <= 0)
               {
                   continue;
               }
               foreach (SpecIn spec in inSpecList)
               {
                   Locate locate = new Locate();
                   //locate.boxBarCode= spec.BoxBarCode;
                   locate.boxCol = spec.Col.ToString();
                   locate.boxRow = spec.Row.ToString();
                   locate.boxId = spec.BoxId.ToString();
                   locate.subSpecId = spec.SubSpecId.ToString();
                   locate.id = spec.InId.ToString();
                   locate.subBarCode = spec.SubSpecBarCode;
                   int locateIndex = 0;
                   //dicLoate.ContainsKey不好用 ,只有遍历
                   foreach (Locate cate in dicLocate.Keys)
                   {
                       if (locate.boxCol != cate.boxCol)
                       {
                           locateIndex++;
                           locateIndex = 0;
                           continue;
                       }
                       if (locate.boxRow != cate.boxRow)
                       {
                           locateIndex++;
                           locateIndex = 0;
                           continue;
                       }
                       if (locate.boxId != cate.boxId)
                       {
                           locateIndex++;
                           locateIndex = 0;
                           continue;
                       }
                       if (locate.subSpecId != cate.subSpecId)
                       {
                           locateIndex++;
                           locateIndex = 0;
                           continue;
                       }
                       if (locate.id != cate.id)
                       {
                           locateIndex++;
                           locateIndex = 0;
                           continue;
                       }
                       if (locateIndex == 0)
                       {
                           dicRollBack.Add(cate, dicLocate[cate]);
                       }
                   }
                    
                   //if (dicLocate.ContainsKey(locate))
                   //{
                   //    dicRollBack.Add(locate, dicLocate[locate]);
                   //}
               }
           }
            #endregion
           int index = 0;
           foreach (KeyValuePair<Locate, Locate> dic in dicRollBack)
           {              
               Locate outLocate = dic.Value;
               SubSpec subSpecInLocate = subSpecManage.GetSubSpecByLocate(outLocate.boxId, outLocate.boxRow, outLocate.boxCol);
               if (subSpecInLocate != null && subSpecInLocate.SpecId > 0 && subSpecInLocate.SubSpecId > 0)
               {
                   MessageBox.Show("不能撤销，原先位置被占用!", title);
                   return 0;
               }
               Locate inLocate = dic.Key;
               if (index == 0)
               {
                   if (boxManage.UpdateOccupy(inLocate.boxId, "0") <= 0)
                   {
                       return -1;
                   }
               }
               SpecBox box = boxManage.GetBoxById(inLocate.boxId);
               if (box == null || box.BoxId <= 0)
               {
                   return -1;
               }
               int occupyCount = box.OccupyCount - 1;
               if (boxManage.UpdateOccupyCount(occupyCount.ToString(), inLocate.boxId) <= 0)
               {
                   return -1;
               }               
               SpecBox outBox = boxManage.GetBoxById(outLocate.boxId);
               if (outBox == null || outBox.BoxId <= 0)
               {
                   return -1;
               }
               int occupyOutCount = outBox.OccupyCount + 1;
               if (occupyCount == outBox.Capacity)
               {
                   if (boxManage.UpdateSotreFlag("1", outLocate.boxId) <= 0)
                   {
                       return -1;
                   }
                   fullBoxList.Add(outBox);
               }
               if (boxManage.UpdateOccupyCount(occupyOutCount.ToString(), outLocate.boxId) <= 0)
               {
                   return -1;
               }
               SubSpec updatingSpec = subSpecManage.GetSubSpecById(inLocate.boxId, "");
               if (updatingSpec == null || updatingSpec.SpecId <= 0 || updatingSpec.SubSpecId<=0)
               {
                   return -1;
               }

               updatingSpec.BoxBarCode = outBox.BoxBarCode;
               updatingSpec.BoxEndCol = Convert.ToInt32(outLocate.boxCol);
               updatingSpec.BoxEndRow = Convert.ToInt32(outLocate.boxRow);
               updatingSpec.BoxId = Convert.ToInt32(outLocate.boxId);
               updatingSpec.BoxStartCol = Convert.ToInt32(outLocate.boxCol);
               updatingSpec.BoxStartRow = Convert.ToInt32(outLocate.boxRow);
               updatingSpec.InStore = "S";

               if (subSpecManage.UpdateSubSpec(updatingSpec) <= 0)
               {
                   return -1;
               }
               if (specInManage.DeleteById(inLocate.id) <= 0)
               {
                   return -1;
               }
               if (specOutManage.DeleteById(outLocate.id) <= 0)
               {
                   return -1;
               }     
               neuSpread1_Sheet1.Rows.Count++;
               neuSpread1_Sheet1.SetActiveCell(neuSpread1_Sheet1.Rows.Count - 1, 0);
               int rowIndex = neuSpread1_Sheet1.ActiveRow.Index;
               neuSpread1_Sheet1.Cells[rowIndex, 0].Text = inLocate.subSpecId;
               neuSpread1_Sheet1.Cells[rowIndex, 1].Text = inLocate.subBarCode;
               neuSpread1_Sheet1.Cells[rowIndex, 2].Text = box.BoxBarCode;
               neuSpread1_Sheet1.Cells[rowIndex, 3].Text = inLocate.boxRow;
               neuSpread1_Sheet1.Cells[rowIndex, 4].Text = inLocate.boxCol;
               neuSpread1_Sheet1.Cells[rowIndex, 5].Text = outBox.BoxBarCode;
               neuSpread1_Sheet1.Cells[rowIndex, 6].Text = outLocate.boxRow;
               neuSpread1_Sheet1.Cells[rowIndex, 7].Text = outLocate.boxCol;
               
           }
           return 1;
        }

        private void ucSpecRestore_Load(object sender, EventArgs e)
        {
            for (int i = 8; i <= 23; i++)
            {
                cmbHour.Items.Add(i.ToString());
                cmbHour1.Items.Add(i.ToString());
            }
            cmbHour.SelectedIndex = 1;
            cmbHour1.SelectedIndex = 1;
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                for (int i = 0; i < neuSpread2_Sheet1.Rows.Count; i++)
                {
                    neuSpread2_Sheet1.Cells[i, 0].Value = (object)1;
                }
            }
            else
            {
                for (int i = 0; i < neuSpread2_Sheet1.Rows.Count; i++)
                {
                    neuSpread2_Sheet1.Cells[i, 0].Value = (object)0;
                }
            }
        }

        public override int Query(object sender, object neuObject)
        {
            GetBox();
            return base.Query(sender, neuObject);
        }

        private void btnRollback_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("确定撤销？", title, MessageBoxButtons.YesNo);
            if (dialog == DialogResult.No)
            {
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            iceBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            boxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            subSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            specInManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            specOutManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                int result = RollBack();
                if (result == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("操作失败！", title);
                    return;
                }
                if (result == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("操作成功！", title);
               
                foreach (SpecBox box in fullBoxList)
                {
                    MessageBox.Show(box.BoxBarCode + " 标本盒已满，请添加新的标本盒！", title);
                    //提示用户添加新的标本盒
                    FS.HISFC.Components.Speciment.Setting.frmSpecBox newSpecBox = new FS.HISFC.Components.Speciment.Setting.frmSpecBox();
                    if (box.DesCapType == 'B')
                        newSpecBox.CurLayerId = box.DesCapID;
                    else
                        newSpecBox.CurShelfId = box.DesCapID;
                    newSpecBox.DisTypeId = box.DiseaseType.DisTypeID;
                    newSpecBox.OrgOrBlood = box.OrgOrBlood;
                    newSpecBox.SpecTypeId = box.SpecTypeID;
                    newSpecBox.Show();
                }
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("操作失败！", title);
                return;
            }

        }
    }

    internal class Locate
    {
        public string subSpecId = "";
        public string subBarCode = "";     
        public string boxId = "";
        public string boxCol = "";
        public string boxRow = "";      
        public string id = "";
    }
}
