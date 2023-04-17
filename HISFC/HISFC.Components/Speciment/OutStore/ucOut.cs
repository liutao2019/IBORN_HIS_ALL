using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Speciment.OutStore
{
    public partial class ucOut : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        private FarPoint.Win.Spread.SheetView sheetView = new FarPoint.Win.Spread.SheetView();
        private DataTable curDt = new DataTable();
        private List<OutInfo> outList = new List<OutInfo>();
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        private FS.HISFC.BizLogic.Speciment.SpecApplyOutManage appMgr = new FS.HISFC.BizLogic.Speciment.SpecApplyOutManage();
        private FS.HISFC.BizLogic.Speciment.SubSpecManage subMgr = new FS.HISFC.BizLogic.Speciment.SubSpecManage();
        private FS.HISFC.BizLogic.Speciment.ApplyTableManage applyTableManage = new FS.HISFC.BizLogic.Speciment.ApplyTableManage();
        //标本条码所在的列
        private int barCodeCol = -1;

        private string specBar = string.Empty;

        private ArrayList appTab = new ArrayList();
        private FS.HISFC.Models.Speciment.ApplyTable curApplyTable = new FS.HISFC.Models.Speciment.ApplyTable();

        /// <summary>
        /// 当前操作员
        /// </summary>
        private FS.HISFC.Models.Base.Employee loginPerson = new FS.HISFC.Models.Base.Employee();

        /// <summary>
        /// 管理业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        // <summary>
        /// 当前科室人员列表
        /// </summary>
        private ArrayList alCurDeptEmpl = new ArrayList();

        public ucOut()
        {
            InitializeComponent();
        }

        private void OpenFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            txtFileName.Text = ofd.FileName;
        }

        private void OpenExcel()
        {
            FS.HISFC.BizLogic.Speciment.ExlToDb2Manage exlMgr = new FS.HISFC.BizLogic.Speciment.ExlToDb2Manage();
            DataSet ds = new DataSet();
            try
            {
                exlMgr.ExlConnect(txtFileName.Text, ref ds);
                if (ds == null || ds.Tables.Count <= 0)
                {
                    return;
                }
                curDt = ds.Tables[0];
            }
            catch
            {
                MessageBox.Show("打开文件失败！");
            }
        }

        private void ReadData()
        {
            try
            {
                sheetView.RowCount=0;
                sheetView.ColumnCount=0;
                this.sheetView.ColumnCount = curDt.Columns.Count + 4;
                this.sheetView.ColumnHeader.Rows[0].Height = 40;

                FarPoint.Win.Spread.CellType.CheckBoxCellType chkType0 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                FarPoint.Win.Spread.CellType.CheckBoxCellType chkType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                chkType1.TextFalse = "不归还";
                chkType1.TextIndeterminate = "多次出库";
                chkType1.TextTrue = "归还";
                chkType1.ThreeState = true;

                sheetView.Columns[0].CellType = chkType0;
                sheetView.Columns[1].CellType = chkType1;

                sheetView.Columns[0].Label = "选择";
                sheetView.Columns[1].Label = "归还";
                sheetView.Columns[2].Label = "期限";
                sheetView.Columns[3].Label = "份量";

                for (int i = 0; i < curDt.Columns.Count; i++)
                {
                    sheetView.Columns[i + 4].Label = curDt.Columns[i].ColumnName;
                    if (sheetView.Columns[i + 4].Label == "标本条码")
                    {
                        barCodeCol = i + 4;
                    }
                }
                for (int jk = 0; jk < this.sheetView.Columns.Count; jk++)
                {
                    sheetView.Columns[jk].Width = 65;
                    if ((jk == 0) || (jk == 3) || (jk == 10) || (jk == 15) || (jk == 16))
                    {
                        sheetView.Columns[jk].Width = 50;
                    }
                    if (sheetView.Columns[jk].Label == "标本盒位置")
                    {
                        sheetView.Columns[jk].Width = 255;
                    }
                    if (sheetView.Columns[jk].Label == "标本条码")
                    {
                        sheetView.Columns[jk].Width = 110;
                    }
                }
                for (int i = 0; i < curDt.Rows.Count; i++)
                {
                    sheetView.Rows.Add(i, 1);
                    for (int j = 0; j < curDt.Columns.Count; j++)
                    {
                        sheetView.Cells[i, j + 4].Value = curDt.Rows[i][j];
                        if (j == 10)
                        {
                            sheetView.Cells[i, j + 4].Tag = curDt.Rows[i][j];
                        }
                    }
                }
                sheetView.Columns[2].Visible = false;
                this.SetEditMode();
            }
            catch
            {
                MessageBox.Show("读取文件失败！");
            }
        }

        /// <summary>
        /// 获取要出库的标本
        /// </summary>
        private int GetOutData()
        {
            if (barCodeCol == -1)
            {
                MessageBox.Show("请保证Excel中有 '标本条码' 列");
                return -1;
            }

            for (int i = 0; i < sheetView.RowCount; i++)
            {
                string selected = "";
                try
                {
                    selected = sheetView.Cells[i, 0].Value.ToString();
                }
                catch
                {
                    continue;
                }
                if (selected == "1" || selected.ToUpper() == "TRUE")
                {
                    OutInfo o = new OutInfo();

                    decimal cnt = 0.0m;

                    try
                    {
                        cnt = Convert.ToDecimal( sheetView.Cells[i, 3].Value);
                    }
                    catch 
                    { }
                    o.Count = cnt;
                    o.SpecBarCode = sheetView.Cells[i, barCodeCol].Value.ToString();
                    #region 新增用于打印
                    o.SpecId = sheetView.Cells[i, 5].Value.ToString();
                    if (o.SpecBarCode.Length == 13)
                    {
                        o.SubQly = o.SpecBarCode.Substring(11, 1);
                    }
                    o.SubType = sheetView.Cells[i, 9].Value.ToString();
                    o.SubDis = sheetView.Cells[i, 10].Value.ToString();
                    o.SubCol = sheetView.Cells[i, 16].Value.ToString();
                    o.SubRow = sheetView.Cells[i, 15].Value.ToString();
                    o.Position = sheetView.Cells[i, 14].Value.ToString();
                    #endregion
                    string isReturn = "0";
                    try
                    {
                        if (sheetView.Cells[i, 1].Value == null)
                        {
                            isReturn = "0";
                        }
                        else
                        {
                            isReturn = sheetView.Cells[i, 1].Value.ToString();
                        } 
                    }
                    catch
                    { }
                    o.ReturnAble = isReturn;
                    outList.Add(o);
                }
            }
            return 0;
        }

        private void SetEditMode()
        {
            if (this.sheetView.RowCount == 0)
            {
                return;
            }
            else
            {
                sheetView.Columns.Get(barCodeCol).Locked = true;
                sheetView.Columns.Get(4).Locked = true;
                sheetView.Columns.Get(5).Locked = true;
                sheetView.Columns.Get(6).Locked = true;
                sheetView.Columns.Get(7).Locked = true;
                sheetView.Columns.Get(8).Locked = true;
                sheetView.Columns.Get(10).Locked = true;
                sheetView.Columns.Get(11).Locked = true;
                sheetView.Columns.Get(12).Locked = true;
                sheetView.Columns.Get(13).Locked = true;
                sheetView.Columns.Get(14).Locked = true;
                sheetView.Columns.Get(15).Locked = true;
                sheetView.Columns.Get(16).Locked = true;
                //sheetView.Columns.Get(17).Locked = true;
            }
            for (int k = 0; k <= sheetView.Columns.Count - 1; k++)
            {
                if (k != 0)
                {
                    sheetView.Columns[k].AllowAutoFilter = true;
                    sheetView.Columns[k].AllowAutoSort = true;
                }
            }
        }

        private void Out(bool direct)
        {
            //不直接出库步骤，申请单号一定要设置
            //1. 插入ApplyOut表中，申请单号
            //出库表中少了一张申请单号

            try
            {
                outList = new List<OutInfo>();
                GetOutData();
            }
            catch
            {
                MessageBox.Show("读取数据失败!");
                return;
            }
            if (txtApplyNum.Text.Trim() == "")
            {
               DialogResult dr = MessageBox.Show("申请号为空，确定不填写申请号吗?", "申请号", MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2);
               if (dr == DialogResult.No)
               {
                   return;
               }
            }

            int tmp = 0;
            if(txtApplyNum.Text.Trim()!="")
            {
                try
                {
                    tmp = Convert.ToInt32(txtApplyNum.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("申请单号必须是数字,请重新填写");
                    return;
                }
            }

            SpecOutOper outOper = new SpecOutOper(loginPerson);
            outOper.IsDirect = direct;
            FS.HISFC.BizLogic.Speciment.UserApplyManage userApplyManage = new FS.HISFC.BizLogic.Speciment.UserApplyManage();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                outOper.ApplyNum = tmp.ToString();
                outOper.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                userApplyManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (txtApplyNum.Text.Trim() != "")
                {
                    outOper.ApplyNum = txtApplyNum.Text.Trim();
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("申请单号不能为空！");
                    return;
                }

                #region 增加申请单状态及出库状态判断
                if (this.curApplyTable == null)
                {
                    this.curApplyTable = applyTableManage.QueryApplyByID(outOper.ApplyNum);
                }
                else
                {
                    //如果不相同，肯定修改过申请号，重新取过了
                    if ((this.curApplyTable.ApplyId == 0) || (this.curApplyTable.ApplyId.ToString() != outOper.ApplyNum))
                    {
                        this.curApplyTable = applyTableManage.QueryApplyByID(outOper.ApplyNum);
                    }
                }
                if (this.curApplyTable == null)
                {
                    MessageBox.Show("没有对应的申请单号，请查询！");
                    return;
                }
                else
                {
                    if (this.curApplyTable.ApplyId == 0)
                    {
                        MessageBox.Show("没有对应的申请单号，请查询！");
                        return;
                    }
                    else
                    {
                        if (this.curApplyTable.User03 == "已出库")
                        {
                            MessageBox.Show("该申请单标本已出库，请仔细核对！");
                            return;
                        }

                    }
                }
                #endregion

                #region 增加出库说明
                //如果出库说明不为空在保存时候即可以保存在对应的申请单信息的备注中SPEC_APPLICATIONTABLE.COMMENT
                string strCmt = "无";
                FS.FrameWork.Models.NeuObject strOutInfo = new FS.FrameWork.Models.NeuObject();
                if (!string.IsNullOrEmpty(this.txtOtherDemand.Text.Trim()))
                {
                    strCmt = this.txtOtherDemand.Text.Trim();
                }
                if (!string.IsNullOrEmpty(this.tbOutInfo.Text.Trim()))
                {
                    strOutInfo.Memo = this.tbOutInfo.Text.Trim();
                }
                if (!string.IsNullOrEmpty(this.cmbOutputOperName.Text.Trim()))
                {
                    strOutInfo.Name = this.cmbOutputOperName.Text.Trim();
                }
                string tmpSql = @"update SPEC_APPLICATIONTABLE set COMMENT = '{0}',OUTPUTRESULT = '{1}',IMPLEMENTNAME = '{2}'
                                        where APPLICATIONID = {3}";
                tmpSql = string.Format(tmpSql, strCmt, strOutInfo.Memo, strOutInfo.Name, tmp.ToString());
                if (outOper.UpdateSpecOut(tmpSql) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新出库说明失败！");
                    return;
                }
                #endregion

                #region 进度情况插入
                FS.HISFC.Models.Speciment.UserApply userApply = new FS.HISFC.Models.Speciment.UserApply();
                userApply.ApplyId = tmp;
                userApply.UserId = loginPerson.ID.ToString();
                string rbtChd = "已出库";
                userApply.Schedule = rbtChd;
                userApply.ScheduleId = "OT";
                userApply.CurDate = System.DateTime.Now;
                userApply.OperId = loginPerson.ID;
                userApply.OperName = loginPerson.Name;
                int result = -1;
                string sequence = "";
                userApplyManage.GetNextSequence(ref sequence);
                userApply.UserAppId = Convert.ToInt32(sequence);
                result = userApplyManage.InsertUserApply(userApply);

                if (result == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("插入进度表失败!", userApply.Schedule);
                    return;
                }
                #endregion

                if (outOper.SpecOut(outList) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("出库失败！");
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                this.PrintPreOut("OUT");
                this.sheetView.RowCount = 0;
                MessageBox.Show("出库成功！");
                //outOper.PrintOutSpec();
                return;
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("出库失败！");
                return;
            }
        } 

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                sheetView.RowCount = 0;
                this.OpenFile();
                this.OpenExcel();
                this.ReadData();
                if (barCodeCol == -1)
                {
                    MessageBox.Show("请保证Excel中有 '标本条码' 列");
                    
                }
            }
            catch
            { }
        }

        protected override void OnLoad(EventArgs e)
        {
            FarPoint.Win.Spread.FpSpread spread = new FarPoint.Win.Spread.FpSpread();
            spread.Sheets.Add(sheetView);
            panel3.Controls.Add(spread);
            spread.Dock = DockStyle.Fill;

            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

            this.alCurDeptEmpl = this.managerIntegrate.QueryEmployeeByDeptID(loginPerson.Dept.ID);
            if (this.alCurDeptEmpl != null)
            {
                if (this.alCurDeptEmpl.Count > 0)
                {
                    this.cmbOutputOperName.AddItems(this.alCurDeptEmpl);
                }
            }
            
            base.OnLoad(e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryByApplyID();
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveChoosedSub();
            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// 出库单打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            this.PrintPreOut("OUT");
            return base.OnPrint(sender, neuObject);
        }

        /// <summary>
        /// 打印出库单据
        /// </summary>
        /// <param name="type">出库类型</param>
        private void PrintPreOut(string type)
        {
            //先变为空，防止重复数据
            try
            {
                outList = new List<OutInfo>();
                GetOutData();
            }
            catch
            {
                MessageBox.Show("读取数据失败!");
                return;
            }
            string applyID = this.txtApplyNum.Text.Trim();
            if (string.IsNullOrEmpty(applyID))
            {
                MessageBox.Show("请输入申请号后查询！");
                return;
            }

            if (this.curApplyTable == null)
            {
                this.curApplyTable = applyTableManage.QueryApplyByID(applyID);
            }
            else
            {
                //如果不相同，肯定修改过申请号，重新取过了
                if ((this.curApplyTable.ApplyId == 0) || (this.curApplyTable.ApplyId.ToString() != applyID))
                {
                    this.curApplyTable = applyTableManage.QueryApplyByID(applyID);
                }
            }
            FS.HISFC.Components.Speciment.Print.ucPreOutBillPrint ucPOBill = new FS.HISFC.Components.Speciment.Print.ucPreOutBillPrint();
            if (!string.IsNullOrEmpty(this.txtOtherDemand.Text.Trim()))
            {
                this.curApplyTable.Comment = this.txtOtherDemand.Text.Trim();
            }
            if (this.curApplyTable == null)
            {
                MessageBox.Show("当前申请号不存在！");
                return;
            }
            else
            {
                ucPOBill.AppTab = curApplyTable;
            }
            if (this.outList.Count <= 0)
            {
                MessageBox.Show("请选择需要打印数据[勾选全选或选中对应行]！");
                return;
            }
            ucPOBill.OutType = type;
            ucPOBill.AlPreOutList = this.outList;
            ucPOBill.AddData();
            ucPOBill.Print();
        }

        /// <summary>
        /// 保存已存标本
        /// </summary>
        private void SaveChoosedSub()
        {
            if (sheetView.RowCount == 0)
            {
                MessageBox.Show("没有任何标本！请查询数据或者导入标本！","保存失败");
                return;
            }
            else
            {
                //FS.HISFC.Object.Base.Employee loginPerson = FS.NFC.Management.Connection.Operator as FS.HISFC.Object.Base.Employee;
                int tmp = 0;
                if (txtApplyNum.Text.Trim() != "")
                {
                    try
                    {
                        tmp = Convert.ToInt32(txtApplyNum.Text.Trim());
                    }
                    catch
                    {
                        MessageBox.Show("申请单号必须是数字,请重新填写", "保存失败");
                        return;
                    }
                }
                bool isDifferent = false;
                if (this.curApplyTable == null)
                {
                    this.curApplyTable = applyTableManage.QueryApplyByID(tmp.ToString());
                }
                else
                {
                    //如果不相同，肯定修改过申请号，重新取过了
                    if (this.curApplyTable.ApplyId == 0)
                    {
                        this.curApplyTable = applyTableManage.QueryApplyByID(tmp.ToString());
                        isDifferent = false;
                    }
                    else if (this.curApplyTable.ApplyId != tmp)
                    {
                        this.curApplyTable = applyTableManage.QueryApplyByID(tmp.ToString());
                        isDifferent = true;
                    }
                    else
                    {

                    }
                }
                SpecOutOper outOper = new SpecOutOper(loginPerson);
                outOper.IsDirect = false;
                FS.HISFC.BizLogic.Speciment.UserApplyManage userApplyManage = new FS.HISFC.BizLogic.Speciment.UserApplyManage();
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                try
                {
                    outOper.ApplyNum = tmp.ToString();
                    outOper.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    userApplyManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    string strCmt = "无";
                    FS.FrameWork.Models.NeuObject strOutInfo = new FS.FrameWork.Models.NeuObject();
                    string tmpSql = string.Empty;

                    if (this.curApplyTable != null)
                    {
                        if (this.curApplyTable.ImpProcess.StartsWith("O"))
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("申请单号为[" + tmp.ToString() + "]的申请单已经审批不能再次修改！");
                            return;
                        }
                        if (this.curApplyTable.User03 == "已出库")
                        {
                            #region 增加出库说明
                            //如果出库说明不为空在保存时候即可以保存在对应的申请单信息的备注中SPEC_APPLICATIONTABLE.COMMENT
                            
                            if (!string.IsNullOrEmpty(this.txtOtherDemand.Text.Trim()))
                            {
                                strCmt = this.txtOtherDemand.Text.Trim();
                            }
                            if (!string.IsNullOrEmpty(this.tbOutInfo.Text.Trim()))
                            {
                                strOutInfo.Memo = this.tbOutInfo.Text.Trim();
                            }
                            if (!string.IsNullOrEmpty(this.cmbOutputOperName.Text.Trim()))
                            {
                                strOutInfo.Name = this.cmbOutputOperName.Text.Trim();
                            }
                            if (!string.IsNullOrEmpty(strCmt) || !string.IsNullOrEmpty(strOutInfo.Memo) || !string.IsNullOrEmpty(strOutInfo.Name))
                            {
                                tmpSql = @"update SPEC_APPLICATIONTABLE set COMMENT = '{0}',OUTPUTRESULT = '{1}',IMPLEMENTNAME = '{2}'
                                        where APPLICATIONID = {3}";
                                tmpSql = string.Format(tmpSql, strCmt, strOutInfo.Memo, strOutInfo.Name, tmp.ToString());
                                if (outOper.UpdateSpecOut(tmpSql) < 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("更新出库说明失败！");
                                    return;
                                }
                                else
                                {
                                    FS.FrameWork.Management.PublicTrans.Commit();
                                    MessageBox.Show("更新出库情况成功！");
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("申请单号为[" + tmp.ToString() + "]的申请单已经出库不能再次修改其他信息！");
                                return;
                            }
                            #endregion
                        }
                    }

                    #region 增加出库说明
                    //如果出库说明不为空在保存时候即可以保存在对应的申请单信息的备注中SPEC_APPLICATIONTABLE.COMMENT
                    //string strCmt = "无";
                    //FS.NFC.Object.NeuObject strOutInfo = new FS.NFC.Object.NeuObject();
                    if (!string.IsNullOrEmpty(this.txtOtherDemand.Text.Trim()))
                    {
                        strCmt = this.txtOtherDemand.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(this.tbOutInfo.Text.Trim()))
                    {
                        strOutInfo.Memo = this.tbOutInfo.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(this.cmbOutputOperName.Text.Trim()))
                    {
                        strOutInfo.Name = this.cmbOutputOperName.Text.Trim();
                    }
                    tmpSql = @"update SPEC_APPLICATIONTABLE set COMMENT = '{0}',OUTPUTRESULT = '{1}',IMPLEMENTNAME = '{2}'
                                        where APPLICATIONID = {3}";
                    tmpSql = string.Format(tmpSql, strCmt, strOutInfo.Memo, strOutInfo.Name, tmp.ToString());
                    if (outOper.UpdateSpecOut(tmpSql) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新出库说明失败！");
                        return;
                    }
                    #endregion

                    if ((this.appTab == null) || (this.appTab.Count == 0))
                    {
                        appTab = outOper.GetSubSpecOut(tmp.ToString());
                    }
                    else
                    {
                        if (((FS.HISFC.Models.Speciment.SpecOut)appTab[0]).RelateId != tmp)
                        {
                            appTab = outOper.GetSubSpecOut(tmp.ToString());
                        }
                    }
                    string rbtChd = "";
                    if ((appTab != null) && (appTab.Count > 0))
                    {
                        //判断是否为追加数据（specOut.Oper="Imp"申请状态）,还是覆盖数据（原specOut.Oper="Del"删除状态,新数据specOut.Oper="Merge"合并状态）
                        if (rbtEnd.Checked)
                        {
                            string updSql = @"update SPEC_APPLY_OUT set OPER = 'Del' 
                                          where RELATEID = {0}";
                            updSql = string.Format(updSql, tmp.ToString());
                            if (outOper.UpdateSpecOut(updSql) < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("更新原有申请数据失败！");
                                return;
                            }
                            rbtChd = "覆盖筛选";
                            outOper.Oper = "Merge";
                        }
                        if (rbtFirst.Checked)
                        {
                            rbtChd = "追加筛选";
                            outOper.Oper = "Imp";
                        }

//                        outOper.Oper = "Merge";
//                        string updSql = @"update SPEC_APPLY_OUT set OPER = 'Del' 
//                                          where RELATEID = {0}";
//                        updSql = string.Format(updSql, tmp.ToString());
//                        if (outOper.UpdateSpecOut(updSql) < 0)
//                        {
//                            FS.FrameWork.Management.PublicTrans.RollBack();
//                            MessageBox.Show("更新原有申请数据失败！");
//                            return;
//                        }
                    }
                    else
                    {
                        outOper.Oper = "Imp";
                    }
                    //ArrayList outSpec = this.appMgr.GetSubSpecOut(tmp.ToString());
                    for (int i = 0; i < curDt.Rows.Count; i++)
                    {
                        string specBarCode = sheetView.Cells[i, barCodeCol].Text.Trim();
                        if (!string.IsNullOrEmpty(specBarCode))
                        {
                            if (rbtFirst.Checked)
                            {
                                bool sign = false;
                                foreach (FS.HISFC.Models.Speciment.SpecOut outTmp in appTab)
                                {
                                    if (specBarCode == outTmp.SubSpecBarCode)
                                    {

                                        //FS.FrameWork.Management.PublicTrans.RollBack();
                                        //MessageBox.Show("追加的标本,标本号为"+specBarCode+"的标本已经存在，追加失败！");
                                        //return;
                                        sign = true;
                                        break;
                                    }
                                }
                                if (sign)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("追加的标本,标本号为[" + specBarCode + "]的标本已经存在，追加失败！");
                                    return;
                                }
                            }
                            FS.HISFC.Models.Speciment.SubSpec tmpSpec = new FS.HISFC.Models.Speciment.SubSpec();
                            if (outOper.GetSubSpecById(specBarCode, ref tmpSpec) < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("根据标本条码获取标本出错！");
                                return;
                            }
                            tmpSpec.BoxBarCode = sheetView.Cells[i, 14].Tag.ToString();
                            if (tmpSpec != null)
                            {
                                //判断是否为新加数据（specOut.Oper="Imp"申请状态）,还是覆盖数据（原specOut.Oper="Del"删除状态,新数据specOut.Oper="Merge"合并状态）

                                if (outOper.SaveApplyOutInfo(tmpSpec, 1, "2") <= 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("保存已选标本出错！");
                                    return;
                                }
                            }
                            else
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("根据标本条码获取标本出错，条码号为[" + specBarCode + "]");
                                return;
                            }
                        }
                    }

                    FS.HISFC.Models.Speciment.UserApply userApply = new FS.HISFC.Models.Speciment.UserApply();
                    userApply.ApplyId = tmp;
                    userApply.UserId = loginPerson.ID.ToString();
                    //string rbtChd = "终筛";
                    userApply.Schedule = rbtChd;
                    if (rbtFirst.Checked)
                    {
                        userApply.ScheduleId = "Q4";
                    }
                    else
                    {
                        userApply.ScheduleId = "Q5";
                    }
                    userApply.CurDate = System.DateTime.Now;
                    userApply.OperId = loginPerson.ID;
                    userApply.OperName = loginPerson.Name;
                    int result = -1;
                    string sequence = "";
                    userApplyManage.GetNextSequence(ref sequence);
                    userApply.UserAppId = Convert.ToInt32(sequence);
                    result = userApplyManage.InsertUserApply(userApply);

                    if (result == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("插入进度表失败!", userApply.Schedule);
                        return;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show("成功保存已选标本！");
                }
                catch
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("保存已选标本异常错误！");
                    return;
                }
            }
        }

        /// <summary>
        /// 根据申请ID查询申请出库数据
        /// </summary>
        private void QueryByApplyID()
        {
            string applyID = this.txtApplyNum.Text.Trim();
            if (string.IsNullOrEmpty(applyID))
            {
                MessageBox.Show("请输入申请号后查询！");
                return;
            }
            else
            {
                if (this.sheetView.RowCount > 0)
                {
                    DialogResult result = MessageBox.Show("查询数据将清除当前已数据是否继续？", "提示", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }
                if (this.curApplyTable == null)
                {
                    this.curApplyTable = applyTableManage.QueryApplyByID(applyID);
                }
                else
                {
                    if (this.curApplyTable.ApplyId == 0)
                    {
                        this.curApplyTable = applyTableManage.QueryApplyByID(applyID);
                    }
                    else if (this.curApplyTable.ApplyId.ToString() != applyID) //新的申请号
                    {
                        this.curApplyTable = applyTableManage.QueryApplyByID(applyID);
                    }
                }
                if (this.curApplyTable == null)
                {
                    MessageBox.Show("未找到该申请单的对应标本！");
                    return;
                }
                else
                {
                    if (this.curApplyTable.ApplyId == 0)
                    {
                        MessageBox.Show("未找到该申请单的对应标本！");
                        return;
                    }
                    else
                    {
                        this.lbStatus.Text = this.curApplyTable.User03;
                    }
                }
                //出库输出情况
                if (!string.IsNullOrEmpty(this.curApplyTable.OutPutResult))
                {
                    this.tbOutInfo.Text = this.curApplyTable.OutPutResult;
                }
                //出库执行人
                if (!string.IsNullOrEmpty(this.curApplyTable.ImpDocId))
                {
                    this.cmbOutputOperName.Text = this.curApplyTable.ImpDocId;
                }
                //出库说明
                if (!string.IsNullOrEmpty(this.curApplyTable.Comment))
                {
                    this.txtOtherDemand.Text = this.curApplyTable.Comment;
                }
                Hashtable hsOutNums = new Hashtable();
                bool isAddNums = false;
                if (this.curApplyTable.User03 == "已出库")
                {
                    isAddNums = true;
                }
                this.appTab = this.appMgr.GetSubSpecOut(applyID);
                if ((appTab != null) && (appTab.Count > 0))
                {
                    string alBar = string.Empty;
                    foreach (FS.HISFC.Models.Speciment.SpecOut spOut in appTab)
                    {
                        if ((isAddNums) && (!hsOutNums.Contains(spOut.SubSpecBarCode)))
                        {
                            hsOutNums.Add(spOut.SubSpecBarCode, spOut);
                        }
                        if (string.IsNullOrEmpty(alBar))
                        {
                            alBar = "'" + spOut.SubSpecBarCode + "'";
                        }
                        else
                        {
                            alBar = alBar + "," + "'" + spOut.SubSpecBarCode + "'";
                        }
                    }
                    if (!string.IsNullOrEmpty(alBar))
                    {
                        this.specBar = alBar;
                    }
                    string curSql = string.Empty;
                    if (this.curApplyTable.User03 == "已出库")
                    {
                        curSql = this.GetSql() + " where ss.SUBBARCODE in ({0})" + " \n ) select * from t";
                    }
                    else
                    {
                        curSql = this.GetSql() + " where ss.SUBBARCODE in ({0})" + " and ss.STATUS in ('1','3')\n ) select * from t";
                    }
                    curSql = string.Format(curSql, specBar);
                    DataSet ds = new DataSet();
                    if (this.applyTableManage.appExecQuery(curSql, ref ds) >= 0)
                    {
                        curDt = ds.Tables[0];
                        this.ReadData();
                        for (int j = 0; j < curDt.Rows.Count; j++)
                        {
                            string boxBarCode = sheetView.Cells[j, 14].Text;
                            string strBar = sheetView.Cells[j, barCodeCol].Value.ToString();
                            if (hsOutNums.ContainsKey(strBar))
                            {
                                sheetView.Cells[j, 3].Text = ((FS.HISFC.Models.Speciment.SpecOut)hsOutNums[strBar]).Count.ToString();
                                if (string.IsNullOrEmpty(boxBarCode))
                                {
                                    boxBarCode = ((FS.HISFC.Models.Speciment.SpecOut)hsOutNums[strBar]).BoxBarCode;
                                }
                                sheetView.Cells[j, 15].Text = ((FS.HISFC.Models.Speciment.SpecOut)hsOutNums[strBar]).BoxRow.ToString();
                                sheetView.Cells[j, 16].Text = ((FS.HISFC.Models.Speciment.SpecOut)hsOutNums[strBar]).BoxCol.ToString();
                                sheetView.Cells[j, 1].Value = (object)(Convert.ToInt32(((FS.HISFC.Models.Speciment.SpecOut)hsOutNums[strBar]).IsRetuanAble));
                            }
                            try
                            {
                                string loc = ParseLocation.ParseSpecBox(boxBarCode);
                                sheetView.Cells[j, 14].Text = loc;
                                sheetView.Cells[j, 14].Tag = boxBarCode;
                            }
                            catch
                            {
                            }
                        }
                        lbStatus.Text += lbStatus.Text + "  共：" + curDt.Rows.Count.ToString() + "条记录";
                    }
                }
            }
        }

        /// <summary>
        /// 明细的查询条件
        /// </summary>
        /// <returns></returns>
        private string GetSql()
        {
            #region
            /*return @"
                         with t as
                         (
                         SELECT DISTINCT 
                         s.SPECID 标本源ID,
                         s.HISBARCODE 源条码, 
                         s.OPEREMP 操作人,
                         s.COMMENT 备注,
                         ( case s.MATCHFLAG when '1' then '是' else '否'end) 配对, p.name 姓名, 
                         s.SPEC_NO 标本号,
  						 p.CARD_NO 病历号,
                         ss.SUBBARCODE 标本条码,
                         d.DISEASENAME 病种, 
                         s.OPERTIME 操作时间, 
						(case s.ISHIS when 'O' then s.SENDDOCID else	(SELECT e.EMPL_NAME FROM COM_EMPLOYEE e WHERE e.EMPL_CODE =  s.SENDDOCID) end) 送存医生,
                        (case s.ISHIS when 'O' then s.DEPTNO else (SELECT cd.DEPT_NAME FROM COM_DEPARTMENT cd WHERE cd.DEPT_CODE = s.DEPTNO FETCH FIRST 1 ROWS ONLY) end)科室,
                        (case s.ORGORBLOOD when 'O' then st.TUMORPOS else '' end) 取材脏器,
						(case st.TUMORTYPE when '1' then '肿物' when '2' then '子瘤' when '3' then '癌旁' when '4' then '正常' when '5' then '癌栓' when '8' then '淋巴结' else '' end) 肿物性质,
						(case ss.STATUS when '1' then '在库' when '2' then '借出' when '3' then '已还' when '4' then '用完' else '' end) 在库状态,
						(case st.TUMORPOR when '1' then '原发' when '2' then '复发' when '3' then '转移' when '13' then '原发，转移' when '23' then '复发，转移' else '' end) 标本属性,
						ss.LASTRETURNTIME 上次返回时间, 
                         ss.DATERETURNTIME 约定返回时间, 
                         st.TRANSPOS 转移部位, 
                         st.CAPACITY 标本容量,
						st.BAOMOENTIRE 脏器描述, 
                         st.COMMENT 标本详细描述, 
                         p.IC_CARDNO 诊疗卡号,
                         s.INPATIENT_NO 住院流水号,
						s.GETPEORID 当前治疗阶段, 
                         s.OPERPOSNAME 手术名称, 
                         sd.MAIN_DIANAME 主诊断,
                         sd.MAIN_DIACODE 主诊断形态码,
						sd.MAIN_DIANAME1 诊断1, 
                         sd.MAIN_DIACODE1 诊断1形态码,
                         sd.MAIN_DIANAME2 诊断2, 
                         sd.MAIN_DIACODE2 诊断2形态码,
						sd.COMMENT 诊断备注, 
                         (case p.GENDER when 'F' then '女' else '男' end) 性别,
						(select name  from COM_DICTIONARY where type = 'COUNTRY' and code = p.NATIONALITY fetch first 1 rows only)国籍,
            			(select name  from COM_DICTIONARY where type = 'NATION' and code = p.NATION fetch first 1 rows only) 民族,
            			p.ADDRESS 住址,
                         (select name from COM_DICTIONARY where type = 'MaritalStatus' and code = p.ISMARR fetch first 1 rows only)婚姻状况,
           				p.BIRTHDAY 生日, 
                         p.CONTACTNUM 联系电话, 
                         p.HOMEPHONENUM 家庭电话, 
                         b.BOXBARCODE 标本盒位置, 
                         ss.BOXENDROW 行,ss.BOXENDCOL 列,
						 t.SPECIMENTNAME 标本类型,
                         st.SPECTYPEID
                       from SPEC_SOURCE s join SPEC_SOURCE_STORE st on s.SPECID = st.SPECID 
                       join SPEC_DISEASETYPE d on d.DISEASETYPEID = s.DISEASETYPEID
                       join SPEC_TYPE t ON st.SPECTYPEID = t.SPECIMENTTYPEID
                       join SPEC_PATIENT p on p.PATIENTID = s.PATIENTID
					   join SPEC_SUBSPEC ss on s.SPECID = ss.SPECID and st.SOTREID = ss.STOREID
                       left join SPEC_DIAGNOSE sd on sd.SPECID = s.SPECID
					   left join SPEC_BOX b on ss.BOXID = b.BOXID";*/
            return @"
                         with t as
                         (
                         SELECT DISTINCT
                         s.HISBARCODE 源条码,
                         s.SPEC_NO 标本号,
                         p.name 姓名,
  						 p.CARD_NO 病历号,
                         ss.SUBBARCODE 标本条码,
                         t.SPECIMENTNAME 标本类型,
                         d.DISEASENAME 病种, 
						(case s.ISHIS when 'O' then s.SENDDOCID else	(SELECT e.EMPL_NAME FROM COM_EMPLOYEE e WHERE e.EMPL_CODE =  s.SENDDOCID) end) 送存医生, 
                         sd.MAIN_DIANAME 主诊断,
                         sd.MAIN_DIACODE 主诊断形态码, 
                         b.BOXBARCODE 标本盒位置, 
                         ss.BOXENDROW 行,
                        ss.BOXENDCOL 列
                       from SPEC_SOURCE s join SPEC_SOURCE_STORE st on s.SPECID = st.SPECID 
                       join SPEC_DISEASETYPE d on d.DISEASETYPEID = s.DISEASETYPEID
                       join SPEC_TYPE t ON st.SPECTYPEID = t.SPECIMENTTYPEID
                       join SPEC_PATIENT p on p.PATIENTID = s.PATIENTID
					   join SPEC_SUBSPEC ss on s.SPECID = ss.SPECID and st.SOTREID = ss.STOREID
                       left join SPEC_DIAGNOSE sd on sd.SPECID = s.SPECID
					   left join SPEC_BOX b on ss.BOXID = b.BOXID";
            #endregion
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("直接出库", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z执行, true, false, null);
            this.toolBarService.AddToolButton("出库", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C出院登记, true, false, null);
            this.toolBarService.AddToolButton("打印预出库单", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印预览, true, false, null);
            return this.toolBarService;
           // return base.OnInit(sender, neuObject, param);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //直接出库不需要再次确认（在SPPEC_OUT 中插入了记录），出库还需要确认出库
            switch (e.ClickedItem.Text.Trim())
            {
                case "出库":
                    DialogResult result = MessageBox.Show("确定出库?", "出库", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                    this.Out(false);
                    break;
                case "直接出库":
                    result = MessageBox.Show("确定不需要确认，直接出库?", "直接出库", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                    this.Out(true);
                    break;
                case "打印预出库单":
                    this.PrintPreOut("PREOUT");
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked)
            {
                for (int i = 0; i < sheetView.Rows.Count; i++)
                {
                    
                        sheetView.Cells[i, 0].Value = (object)1;
                        //rowIndexList.Add(i);
                    
                }
            }
            else
            {
                for (int i = 0; i < sheetView.Rows.Count; i++)
                {
                    sheetView.Cells[i, 0].Value = (object)0;
                }
            }
        }

        /// <summary>
        /// 申请号回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtApplyNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.QueryByApplyID();
            }
        }

        private void chkBack_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkBack.CheckState == CheckState.Checked)
            {
                chkBack.Text = "归还";
                for (int i = 0; i < sheetView.Rows.Count; i++)
                {

                    sheetView.Cells[i, 1].Value = (object)(1);
                    //rowIndexList.Add(i);
                }
            }
            else if (chkBack.CheckState == CheckState.Unchecked)
            {
                chkBack.Text = "不还";
                for (int i = 0; i < sheetView.Rows.Count; i++)
                {

                    sheetView.Cells[i, 1].Value = (object)(0);
                    //rowIndexList.Add(i);

                }
            }
            else
            {
                chkBack.Text = "多次出库";
                for (int i = 0; i < sheetView.Rows.Count; i++)
                {

                    sheetView.Cells[i, 1].Value = (object)(2);
                    //rowIndexList.Add(i);

                }
            }
        }

        private void nudCount_ValueChanged(object sender, EventArgs e)
        {
            decimal count = Convert.ToDecimal(nudCount.Value.ToString());
            if (count > 0.0M)
            {
                for (int i = 0; i < sheetView.Rows.Count; i++)
                {
                    string check = "";
                    check = sheetView.Cells[i, 0].Value == null ? "" : sheetView.Cells[i, 0].Value.ToString();
                    if (check == "1" || check.ToUpper() == "TRUE")
                        sheetView.Cells[i, 3].Text = count.ToString();
                }
            }
        }

        private void rbURt_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < sheetView.Rows.Count; i++)
            {
                sheetView.Cells[i, 1].Value = (object)(0);
            }
        }

        private void rbRt_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < sheetView.Rows.Count; i++)
            {
                sheetView.Cells[i, 1].Value = (object)(1);
            }
        }

        private void rbMt_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < sheetView.Rows.Count; i++)
            {
                sheetView.Cells[i, 1].Value = (object)(2);
            }
        }

        public override int Export(object sender, object neuObject)
        {
            if (this.sheetView.Rows.Count > 0)
            {
                FS.FrameWork.WinForms.Controls.NeuSpread ns = new FS.FrameWork.WinForms.Controls.NeuSpread();
               
                string path = string.Empty;
                SaveFileDialog saveFileDiaglog = new SaveFileDialog();

                saveFileDiaglog.Title = "查询结果导出，选择Excel文件保存位置";
                saveFileDiaglog.RestoreDirectory = true;
                saveFileDiaglog.InitialDirectory = Application.StartupPath;
                saveFileDiaglog.Filter = "Excel files (*.xls)|*.xls";
                saveFileDiaglog.FileName = DateTime.Now.ToShortDateString() + "标本";
                DialogResult dr = saveFileDiaglog.ShowDialog();
                SpreadToExlHelp.ExportExl(sheetView, 16, new int[] { }, saveFileDiaglog.FileName, false);
            }
            return base.Export(sender, neuObject);
        }
    }
}
