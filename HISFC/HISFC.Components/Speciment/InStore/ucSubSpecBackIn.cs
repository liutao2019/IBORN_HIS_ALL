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

namespace FS.HISFC.Components.Speciment.InStore
{
    public partial class ucSubSpecBackIn :FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 私有变量
        private SpecTypeManage specTypeManage;
        private IceBoxManage iceBoxManage;
        private SpecBoxManage specBoxManage;
        private SubSpecManage subSpecManage;
        private BoxSpecManage boxSpecManage;
        private SubSpec subSpec;
        private SpecInOper specInOper;
        private FS.HISFC.Models.Base.Employee loginPerson;
        private string title;
        private string disTypeId;
        #endregion

        #region 构造函数
        public ucSubSpecBackIn()
        {
            InitializeComponent();
            specTypeManage = new SpecTypeManage();
            iceBoxManage = new IceBoxManage();
            specBoxManage = new SpecBoxManage();
            subSpecManage = new SubSpecManage();
            boxSpecManage = new BoxSpecManage();
            subSpec = new SubSpec();
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            specInOper = new SpecInOper();
            title = "标本重定位";
            disTypeId = "";
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 格式化标本的位置信息
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="boxBarCode">盒子条形码</param>
        /// <param name="specCol">标本列</param>
        /// <param name="specRow">标本行</param>
        /// <param name="desCap"></param>
        private void LocateInfo(ListBox lv, string boxBarCode, string specRow, string specCol, string desCap)
        {
            ///位置信息
            ///冰箱号，盒子条形码的前三位
            string iceBoxId = boxBarCode.Substring(0, 3);
            string iceBox = iceBoxManage.GetIceBoxById(iceBoxId).IceBoxName;
            iceBox = "冰箱：" + iceBox;
            string iceBoxLayer = boxBarCode.Substring(3, 2);
            iceBox += (" 第 " + iceBoxLayer + " 层");
            lv.Items.Clear();
            if (desCap == "B")
            {
                lv.Items.Add("该标本直接存于冰箱中,标本盒位置信息如下：");
                lv.Items.Add(iceBox);
                string row = boxBarCode.Substring(5, 2);
                string col = boxBarCode.Substring(7, 2);
                string layer = boxBarCode.Substring(9, 2);
                lv.Items.Add("第 " + row + " 行, 第 " + col + " 列, 第 " + layer + " 层");
                //lvOldLacate.Items.Add(
            }
            if (desCap == "J")
            {
                lv.Items.Add("该标本存于架中,标本盒位置信息如下：");
                lv.Items.Add(iceBox);
                string shelfNum = boxBarCode.Substring(5, 2);
                lv.Items.Add("第 " + shelfNum + " 个架子");
                string inShelfNum = boxBarCode.Substring(7, 2);
                string row = boxBarCode.Substring(9, 2);
                string col = boxBarCode.Substring(11, 2);
                lv.Items.Add("第 " + row + " 行, 第 " + col + " 列, 第 " + inShelfNum + " 层");
            }
            lv.Items.Add("位于盒子中： 第" + specRow + " 行, 第 " + specCol + "列");
            
        }

        /// <summary>
        /// 标本重定位
        /// </summary>
        private void SpecReLocate()
        {
            if (subSpec.Status == "1" || subSpec.Status == "3")
            {
                MessageBox.Show("此标本已经存在库中！", title);
                return;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            subSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            iceBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            specBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            boxSpecManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);         
            int result = -1;
            try
            {
                subSpec.Comment = "标本返回入库";
                specInOper.Trans = FS.FrameWork.Management.PublicTrans.Trans;
                specInOper.SetTrans();
                specInOper.SubSpec = subSpec;
                result = specInOper.InOper();
                if (result <= -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新失败！", title);
                    return;
                }
                if (subSpec.BoxEndRow == 0 || subSpec.BoxEndCol==0)
                {
                    DialogResult diagResult = MessageBox.Show("原位置被占用, 是否重新生成位置？", title, MessageBoxButtons.YesNo);
                    if (diagResult == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    DialogResult diagResult = MessageBox.Show("是否放回原位置？", title, MessageBoxButtons.YesNo);
                    if (diagResult == DialogResult.Yes)
                    {
                        DateTime dtReturn = DateTime.Now;
                        string updateSql = "UPDATE SPEC_SUBSPEC" +
                                     " SET LASTRETURNTIME = to_date('" + dtReturn.ToString() + "','yyyy-mm-dd hh24:mi:ss'), STATUS = '3', ISRETURNABLE =  1" +
                                     " WHERE SUBSPECID=" + subSpec.SubSpecId.ToString();
                        result = subSpecManage.UpdateSubSpec(updateSql);
                        if (result == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新失败！", title);
                        }
                        FS.FrameWork.Management.PublicTrans.Commit();
                        return;
                    }
                    //更新旧标本盒的占用数量
                    if (diagResult == DialogResult.No)
                    {
                        SpecBox specBox = specBoxManage.GetBoxById(subSpec.BoxId.ToString());
                        //更新标本盒的的已满标志
                        result = specBoxManage.UpdateOccupy(subSpec.BoxId.ToString(), "0");
                        if (result == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新标本盒失败！", title);
                        }
                        int occupyCount = specBox.OccupyCount - 1;
                        if (occupyCount <= 0)
                            occupyCount = 0;
                        result = specBoxManage.UpdateOccupyCount(occupyCount.ToString(), subSpec.BoxId.ToString());
                        if (result == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新标本盒失败！", title);
                        }
                        string locateSql = "UPDATE SPEC_SUBSPEC SET BOXID=0 ,BOXSTARTROW= 0,BOXSTARTCOL=0, BOXENDROW= 0,BOXENDCOL=0"+
                                     " WHERE SPEC_SUBSPEC.SUBBARCODE= '" + txtSpecBarCode.Text.Trim() + "'";
                        //置空原先位置信息
                        result = subSpecManage.UpdateSubSpec(locateSql);
                        if (result == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("置空位置失败！", title);
                        }
                    }
                }

                ArrayList arrBox = specBoxManage.GetLastLocation(disTypeId, cmbSpecType.SelectedValue.ToString()); ;
                if (arrBox == null || arrBox.Count <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("此标本类型, 没有可使用的标本盒!", title);
                    return ;
                }

                SpecBox currentBox = new SpecBox();
                int boxCount = 0;
                foreach (SpecBox b in arrBox)
                {
                    if (b.BoxId > 0)
                    {
                        currentBox = b;
                        break;
                    }
                    boxCount++;
                }
                if (boxCount == arrBox.Count)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("此标本类型, 没有可使用的标本盒!", title);
                    return ;
                }

                //SpecBox currentBox = specBoxManage.GetLastLocation(disTypeId, cmbSpecType.SelectedValue.ToString());
                //if (currentBox.BoxId <= 0)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show("此标本类型, 没有可使用的标本盒!", title);                    
                //    return;
                //}
                subSpec.BoxId = currentBox.BoxId;
                BoxSpec boxSpec = boxSpecManage.GetSpecByBoxId(currentBox.BoxId.ToString());
                int maxRow = boxSpec.Row;
                int maxCol = boxSpec.Col;
                //查找标本位置
                SubSpec lastSubSpec = subSpecManage.ScanSpecBox(subSpec.BoxId.ToString(), boxSpec);
                int currentEndRow = lastSubSpec.BoxEndRow;
                int currentEndCol = lastSubSpec.BoxEndCol;
                if (currentEndCol < maxCol)
                {
                    subSpec.BoxStartCol = currentEndCol + 1;
                    subSpec.BoxEndCol = currentEndCol + 1;
                    subSpec.BoxEndRow = currentEndRow;
                    subSpec.BoxStartRow = subSpec.BoxEndRow;
                }
                if (currentEndCol == maxCol && currentEndRow < maxRow)
                {
                    subSpec.BoxEndCol = 1;
                    subSpec.BoxStartCol = 1;
                    subSpec.BoxStartRow = currentEndRow + 1;
                    subSpec.BoxEndRow = currentEndRow + 1;
                }
                string sql = "UPDATE SPEC_SUBSPEC SET SPECCAP = " + nudSpecCap.Value.ToString() + ", SPECTYPEID = " + cmbSpecType.SelectedValue.ToString() +
                             " , LASTRETURNTIME = to_date('" + DateTime.Now.ToString() + "','yyyy-mm-dd hh24:mi:ss'), " +
                             " STATUS = '3', BOXID=" + subSpec.BoxId.ToString() + ",BOXSTARTROW=" + subSpec.BoxStartRow.ToString() + ",BOXSTARTCOL=" + subSpec.BoxStartCol.ToString() +
                             ",BOXENDROW=" + subSpec.BoxEndRow.ToString() + ",BOXENDCOL=" + subSpec.BoxEndCol.ToString() + ",COMMENT= '" + txtComment.Text +"'"+
                             " WHERE SPEC_SUBSPEC.SUBBARCODE= '" + txtSpecBarCode.Text.Trim()+"'";
                //更新标本的位置信息
                result = subSpecManage.UpdateSubSpec(sql);
                if (result == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("生成新位置失败！", title);
                }
                //更新当前盒子的占用量
                result = specBoxManage.UpdateOccupyCount((currentBox.OccupyCount + 1).ToString(), currentBox.BoxId.ToString());
                if (result == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新标本盒失败！", title);
                }
                //如果当前盒子满了提示入库,并更新
                if (currentBox.OccupyCount == currentBox.Capacity)
                {
                    specBoxManage.UpdateOccupy(currentBox.BoxId.ToString(), "1");
                    DialogResult diagResult = MessageBox.Show("该标本盒已满，是否入库?", "标本盒入库", MessageBoxButtons.YesNo);
                    if (diagResult == DialogResult.Yes)
                    {
                        result = specBoxManage.UpdateSotreFlag("1", currentBox.BoxId.ToString());
                        if (result == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新标本盒失败！", title);
                        }
                    }                    
                }
                LocateInfo(lvNewLocate, currentBox.BoxBarCode, subSpec.BoxEndRow.ToString(), subSpec.BoxEndCol.ToString(), currentBox.DesCapType.ToString());
                lvNewLocate.Visible = true;
                label8.Visible = true;
                //当前标本为返回在库状态
                subSpec.Status = "3";
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("操作成功！", title);
                if (currentBox.OccupyCount == currentBox.Capacity)
                {
                    MessageBox.Show("当前标本盒已满，请添加新的标本盒！", title);
                    //提示用户添加新的标本盒
                    FS.HISFC.Components.Speciment.Setting.frmSpecBox newSpecBox = new FS.HISFC.Components.Speciment.Setting.frmSpecBox();
                    if (currentBox.DesCapType == 'B')
                        newSpecBox.CurLayerId = currentBox.DesCapID;
                    else
                        newSpecBox.CurShelfId = currentBox.DesCapID;
                    newSpecBox.Show();
                }                
            }
            catch
            {
                MessageBox.Show("生成新位置失败！", title);          
                FS.FrameWork.Management.PublicTrans.RollBack();
            }
        }

        /// <summary>
        /// 设置标本信息
        /// </summary>
        private void SpecInfo()
        {
            try
            {
                string barCode = txtSpecBarCode.Text.Trim();
                #region sql语句
                string sql = " select DISTINCT SPEC_SUBSPEC.STATUS, SPEC_SUBSPEC.SUBSPECID, SPEC_SUBSPEC.SPECTYPEID, SPEC_DISEASETYPE.DISEASENAME,SPEC_SOURCE_STORE.TUMORTYPE,SPEC_SOURCE.TUMORPOR,\n" +
                             " SPEC_SUBSPEC.SPECCAP, SPEC_SUBSPEC.SPECCOUNT,SPEC_SUBSPEC.LASTRETURNTIME,SPEC_SOURCE.DISEASETYPEID,\n" +
                             " (SELECT COUNT(*) FROM SPEC_OUT WHERE SPEC_OUT.SUBSPECID=SPEC_SUBSPEC.SUBSPECID) OUTCOUNT,SPEC_SUBSPEC.BOXID,\n" +
                             " SPEC_SUBSPEC.COMMENT,SPEC_BOX.BOXBARCODE,SPEC_SUBSPEC.BOXENDROW,SPEC_SUBSPEC.BOXENDCOL,DESCAPTYPE\n" +
                             " from SPEC_SUBSPEC LEFT JOIN SPEC_SOURCE_STORE ON SPEC_SUBSPEC.STOREID = SPEC_SOURCE_STORE.SOTREID \n" +
                             " LEFT JOIN SPEC_SOURCE ON SPEC_SUBSPEC.SPECID = SPEC_SOURCE.SPECID \n" +
                             " LEFT JOIN SPEC_DISEASETYPE ON SPEC_SOURCE.DISEASETYPEID = SPEC_DISEASETYPE.DISEASETYPEID \n" +
                             " LEFT JOIN SPEC_OUT ON SPEC_OUT.SUBSPECID = SPEC_SUBSPEC.SUBSPECID \n" +
                             " LEFT JOIN SPEC_BOX ON SPEC_SUBSPEC.BOXID = SPEC_BOX.BOXID \n" +
                             " WHERE SPEC_SUBSPEC.SUBBARCODE = '" + barCode + "'";
                #endregion
                DataSet ds = new DataSet();
                specTypeManage.ExecQuery(sql, ref ds);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    subSpec.Status = dt.Rows[0]["STATUS"].ToString();
                    subSpec.BoxEndCol = Convert.ToInt32(dt.Rows[0]["BOXENDCOL"].ToString());
                    subSpec.BoxEndRow = Convert.ToInt32(dt.Rows[0]["BOXENDROW"].ToString());
                    subSpec.Comment = dt.Rows[0]["COMMENT"].ToString();
                    subSpec.LastReturnTime = Convert.ToDateTime(dt.Rows[0]["LASTRETURNTIME"].ToString());
                    subSpec.SpecCap = Convert.ToDecimal(dt.Rows[0]["SPECCAP"].ToString());
                    subSpec.SpecCount = Convert.ToInt32(dt.Rows[0]["SPECCOUNT"].ToString());
                    subSpec.SpecTypeId = Convert.ToInt32(dt.Rows[0]["SPECTYPEID"].ToString());
                    subSpec.SubSpecId = Convert.ToInt32(dt.Rows[0]["SUBSPECID"].ToString());
                    subSpec.BoxId = Convert.ToInt32(dt.Rows[0]["BOXID"].ToString());
                    disTypeId = dt.Rows[0]["DISEASETYPEID"].ToString();
                    txtComment.Text = subSpec.Comment;
                    txtDisType.Text = dt.Rows[0]["DISEASENAME"].ToString();
                    txtLastReturn.Text = subSpec.LastReturnTime.ToString("yyyy-MM-dd");
                    txtOutCount.Text = dt.Rows[0]["OUTCOUNT"].ToString();
                    #region 癌性质
                    if (dt.Rows[0]["TUMORPOR"].ToString() != "")
                    {
                        char[] tumorPor = dt.Rows[0]["TUMORPOR"].ToString().ToCharArray();
                        Constant.TumorPro TumorPro = Constant.TumorPro.原发癌;
                        foreach (char t in tumorPor)
                        {
                            TumorPro = (Constant.TumorPro)(Convert.ToInt32(t.ToString()));
                            txtTumorPro.Text = "";
                            switch (TumorPro)
                            {
                                //标本属性1.原发癌 2.复发癌 3.转移癌
                                case Constant.TumorPro.原发癌:
                                    txtTumorPro.Text += "原发癌";
                                    break;
                                case Constant.TumorPro.复发癌:
                                    txtTumorPro.Text += "复发癌";
                                    break;
                                case Constant.TumorPro.转移癌:
                                    txtTumorPro.Text += "转移癌";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    #endregion
                    #region 癌种类
                    if (dt.Rows[0]["TUMORTYPE"].ToString().Trim() != "")
                    {
                        //1.肿物 2.子瘤 3.癌旁 4，正茬、切端 5.癌栓 6, 癌(血液), 7 正常(血液)
                        char[] tumorType = dt.Rows[0]["TUMORTYPE"].ToString().ToCharArray();
                        foreach (char t in tumorType)
                        {
                            txtTumorType.Text = "";
                            Constant.TumorType TumorType = (Constant.TumorType)(Convert.ToInt32(t.ToString()));
                            switch (TumorType)
                            {
                                case Constant.TumorType.肿物:
                                    txtTumorType.Text += "肿物";
                                    break;
                                case Constant.TumorType.子瘤:
                                    txtTumorType.Text += "子瘤";
                                    break;
                                //case Constant.TumorType.肿瘤:
                                //    txtTumorType.Text += "癌";
                                //    break;
                                case Constant.TumorType.癌旁:
                                    txtTumorType.Text += "癌旁";
                                    break;
                                case Constant.TumorType.癌栓:
                                    txtTumorType.Text += "癌栓";
                                    break;
                                //case Constant.TumorType.切端:
                                //    txtTumorType.Text += "切端";
                                //    break;
                                case Constant.TumorType.正常:
                                    txtTumorType.Text += "正常";
                                    break;
                                case Constant.TumorType.淋巴结:
                                    txtTumorType.Text += "淋巴结";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    #endregion
                    nudSpecCap.Value = Convert.ToDecimal(dt.Rows[0]["SPECCAP"].ToString());
                    nudSpecCount.Value = Convert.ToDecimal(dt.Rows[0]["SPECCOUNT"].ToString());
                    cmbSpecType.SelectedValue = subSpec.SpecTypeId;
                    string boxBarCode = dt.Rows[0]["BOXBARCODE"].ToString();
                    if (subSpec.BoxEndCol == 0 || subSpec.BoxEndRow == 0)
                    {
                        lvOldLacate.Items.Add("原位置被占用！");
                    }
                    #region 标本的原位置信息
                    ///位置信息
                    LocateInfo(lvOldLacate, boxBarCode, dt.Rows[0]["BOXENDROW"].ToString(), dt.Rows[0]["BOXENDCOL"].ToString(), dt.Rows[0]["DESCAPTYPE"].ToString());                   
                    #endregion
                }

            }
            catch
            {
 
            }
        }

        /// <summary>
        /// 标本类型绑定
        /// </summary>
        private void SpecTypeBinding()
        {
            string sql = "select * from SPEC_TYPE";
            ArrayList arrSpecType = specTypeManage.GetSpecType(sql);
            cmbSpecType.ValueMember = "SpecTypeID";  
            cmbSpecType.DisplayMember = "SpecTypeName";
            cmbSpecType.DataSource = arrSpecType;
            //specTypeManage
        }
        #endregion

        #region 事件
        private void ucSubSpecBackIn_Load(object sender, EventArgs e)
        {
            SpecTypeBinding();
        }

        private void txtSpecBarCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSpecBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                lvOldLacate.Items.Clear();
                lvNewLocate.Items.Clear();
                SpecInfo();
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            SpecReLocate();
        }
        #endregion
    }
}
