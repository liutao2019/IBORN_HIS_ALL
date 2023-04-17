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

namespace FS.HISFC.Components.Speciment
{
    public partial class ucBloodBarcode : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        //业务层管理对象
        private SpecSourceManage specSourceManage;
        private SpecSourcePlanManage specSourcePlanManage;    
        private SpecTypeManage specTypeManage;
        private SpecBarCodeManage barCodeManage;
        private DisTypeManage disTypeManage;
        private OperApplyManage operApplyMgr;
        private DiagnoseManage diagManage;

        //集合
        private Dictionary<string, List<string>> dicBarCode;      
        //已满标本盒集合
        private List<SpecBox> useFullBoxList;
        //加载的标本类型控件
        private ucSpecSourceForBlood[] ucSourceForBlood;

        //保存原标本条形码及分装信息
        private Dictionary<SpecSource, List<SpecSourcePlan>> dicSourcePlanList;
        private List<SubSpec> subList;
        private Dictionary<string, int> dicDiagToPos;

        private FS.HISFC.Models.Base.Employee loginPerson;
        private System.Data.IDbTransaction trans;
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService;
        private FS.HISFC.Models.Speciment.SpecType specType;

        /// <summary>
        /// 发送到贝迪打印机的参数
        ///  Print2DBarCode(List<string> barCode, List<string> sequence, string host, List<string> disType, List<string> num)
        /// </summary>
        private List<string> barCodeList;
        private List<string> sequenceList;
        private List<string> disTypeList;
        private List<string> numList;
        private List<string> specTypeList;
 
        //如果是门诊病人必须录入诊断
        private bool needSaveDiagnose = false;
        private bool customDis = false;
        private bool enbleDis = true;

        public bool EnbleDis
        {
            get
            {
                return enbleDis;
            }
            set
            {
                enbleDis = value;
            }
        }

        public ucBloodBarcode()
        {
            InitializeComponent();        
            specSourcePlanManage = new SpecSourcePlanManage();
            specSourceManage = new SpecSourceManage();
            specType = new FS.HISFC.Models.Speciment.SpecType();
            specTypeManage = new SpecTypeManage();
            barCodeManage = new SpecBarCodeManage();
            diagManage = new DiagnoseManage();

            dicBarCode = new Dictionary<string, List<string>>();
            dicSourcePlanList = new Dictionary<SpecSource, List<SpecSourcePlan>>();
            disTypeManage = new DisTypeManage();
            operApplyMgr = new OperApplyManage();
            useFullBoxList = new List<SpecBox>();
            subList = new List<SubSpec>();
            loginPerson = new FS.HISFC.Models.Base.Employee();
            barCodeList = new List<string>();
            sequenceList = new List<string>();
            disTypeList = new List<string>();
            numList = new List<string>();
            specTypeList = new List<string>();
            dicDiagToPos = new Dictionary<string, int>();
            toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        }

        /// <summary>
        /// 保存诊断对应的病种
        /// </summary>
        private void SaveDiagToDis()
        {
            FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
            //type,cons.ID,cons.Name,cons.Memo,cons.SpellCode,cons.WBCode,cons.UserCode,cons.SortID,FS.NFC.Function.NConvert.ToInt32(cons.IsValid),this.Operator.ID);

            FS.HISFC.Models.Base.Const c = new FS.HISFC.Models.Base.Const();
            string type = "DiagnosebyNurse";
            c.ID = "";
            string id = "";
            disTypeManage.GetNextSequence(ref id);
            c.ID = id;

            c.Name = cmbDiagnose.Text;
            c.Memo = loginPerson.Dept.ID;
            c.UserCode = cmbDisType.Text.Trim();
            c.SortID = 1;
            c.IsValid = true;
            int inRes = conMgr.InsertItem(type, c);
        }

        private void ClearData()
        {
            foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp in dicSourcePlanList)
            {
                tmp.Key.SpecId = 0;
            }
            useFullBoxList = new List<SpecBox>();
            subList = new List<SubSpec>();
            barCodeList = new List<string>();
            sequenceList = new List<string>();
            disTypeList = new List<string>();
            numList = new List<string>();
            specTypeList = new List<string>();
            txtBarCode.Focus();
        }

        /// <summary>
        /// 绑定病种类型
        /// </summary>
        private void DisTypeBinding()
        {
            //Dictionary<int, string> dicDisType = disTypeManage.GetAllDisType();
            //if (dicDisType.Count > 0)
            //{
            //    BindingSource bs = new BindingSource();
            //    bs.DataSource = dicDisType;
            //    cmbDisType.DisplayMember = "Value";
            //    cmbDisType.ValueMember = "Key";
            //    cmbDisType.DataSource = bs;
            //}
            //cmbDisType.Text = "";
            ArrayList alDisType = disTypeManage.GetAllValidDisType();
            if (alDisType != null)
            {
                if (alDisType.Count > 0)
                {
                    cmbDisType.AddItems(alDisType);
                }
            }
            cmbDisType.Text = "";
        }

        private string GenerateSpecSql()
        {
            string sql = "SELECT distinct * FROM SPEC_SOURCE WHERE ORGORBLOOD = 'B' ";
            if (dtpStart.Value != null) sql += " and OPERTIME>=to_date('" + dtpStart.Value.Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
            if (dtpEnd.Value != null)
                sql += " and OPERTIME<=to_date('" + dtpEnd.Value.AddDays(1.0).Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
            sql += " order by OPERTIME desc";
            return sql;
        }

        private string GenerateApplySql()
        {
            string sql = "select distinct * from SPEC_OPERAPPLY where ORGORBLOOD='B'";
            if (txtBarCodeQ.Text.Trim() != "")
            {
                sql += " and Id =" + txtBarCodeQ.Text.Trim();
            }
            else
            {
                if (dtpStart.Value != null)
                    sql += " and OPERTIME>=to_date('" + dtpStart.Value.Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
                if (dtpEnd.Value != null)
                    sql += " and OPERTIME<=to_date('" + dtpEnd.Value.AddDays(1.0).Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
                if (cmbOperDept.Tag != null && cmbOperDept.Text != "") sql += " and OPERDEPTNAME = '" + cmbOperDept.Text.Trim() + "'";
            }
            return sql;
        }

        /// <summary>
        /// 加载手术申请单
        /// </summary>
        private void LoadOperApply()
        {
            string sql = GenerateApplySql();
            ArrayList arrList = operApplyMgr.GetOperApplyBySql(sql);
            if (arrList != null || arrList.Count > 0)
            {
                tvSpec.ShowNodeToolTips = true;
                foreach (OperApply apply in arrList)
                {
                    //未取标本
                    if (apply.HadCollect == "0" || apply.HadCollect == "4" || apply.HadCollect == "5" || apply.HadCollect == "6")
                    {
                        TreeNode tnNotGet = tvSpec.Nodes[2];
                        TreeNode tn = new TreeNode();
                        tn.Text = apply.PatientName + " 【" + apply.OperDeptName.ToString() + "】 ";
                        tn.ToolTipText = apply.OperApplyId.ToString().PadLeft(12, '0');

                        tn.Tag = apply;
                        tnNotGet.Nodes.Add(tn);
                    }

                    //取消不取
                    if (apply.HadCollect == "3")
                    {
                        TreeNode tnNotGet = tvSpec.Nodes[3];
                        TreeNode tn = new TreeNode();
                        tn.Text = apply.PatientName + " 【" + apply.OperDeptName + "】 ";
                        tn.Tag = apply;
                        tn.ToolTipText = apply.OperApplyId.ToString().PadLeft(12, '0');
                        tnNotGet.Nodes.Add(tn);
                    }

                    //已送
                    if (apply.HadCollect == "2")
                    {
                        TreeNode tnNotGet = tvSpec.Nodes[1];
                        TreeNode tn = new TreeNode();
                        tn.Text = apply.PatientName + " 【" + apply.OperDeptName + "】 ";
                        tn.ToolTipText = apply.OperApplyId.ToString().PadLeft(12, '0');
                        tn.Tag = apply;
                        tnNotGet.Nodes.Add(tn);
                    }
                }
            }
            tvSpec.Nodes[2].Text = "护士站未送  (共:" + tvSpec.Nodes[2].Nodes.Count.ToString() + " 例)";
            tvSpec.Nodes[3].Text = "已取消  (共:" + tvSpec.Nodes[3].Nodes.Count.ToString() + " 例)";
            tvSpec.Nodes[1].Text = "护士站已送  (共:" + tvSpec.Nodes[1].Nodes.Count.ToString() + " 例)";

        }

        /// <summary>
        /// 加载样本列表
        /// </summary>
        private void LoadSpecSource()
        {
            tvSpec.Nodes[0].Nodes.Clear();
            string sql = GenerateSpecSql();
            ArrayList arrList = specSourceManage.GetSpecSource(sql);
            if (arrList != null || arrList.Count > 0)
            {
                foreach (SpecSource source in arrList)
                {
                    TreeNode tn = tvSpec.Nodes[0];
                    TreeNode node = new TreeNode();
                    node.Text = source.HisBarCode + " 【" + source.SendTime.ToString() + "】";
                    node.Tag = source;
                    tn.Nodes.Add(node);
                }
            }
            tvSpec.Nodes[0].Text = "已收标本  (共:" + tvSpec.Nodes[0].Nodes.Count.ToString() + " 例)";
            if (tvSpec.Nodes[0].Nodes.Count > 0)
            {
                tvSpec.SelectedNode = tvSpec.Nodes[0].Nodes[0];
            }
        }

        /// <summary>
        /// 取消样本
        /// </summary>
        private void CancelSpec()
        {
            DialogResult d = MessageBox.Show("确定取消?", "取消", MessageBoxButtons.YesNo);
            if (d == DialogResult.No)
            {
                return; 
            }
            TreeNode tn = tvSpec.SelectedNode;
            if (tn == null)
            {
                MessageBox.Show("请选择需要取消的样本");
                return;
            }
            if (tn.Tag == null)
            {
                MessageBox.Show("获取样本信息失败");
                return;
            }

            try
            {
                OperApply operApply = tn.Tag as OperApply;
                if (operApplyMgr.UpdateColFlag(operApply.OperApplyId.ToString(), "3") == -1)
                {
                    MessageBox.Show("操作失败");
                    return;
                }
                MessageBox.Show("操作成功");
            }
            catch
            {
                MessageBox.Show("已送标本不能取消！");
            }
        }

        /// <summary>
        /// 撤销取消标本
        /// </summary>
        private void ReCancelSpec()
        {
            TreeNode tn = tvSpec.SelectedNode;
            if (tn == null)
            {
                MessageBox.Show("请选择需要撤销取消的样本");
                return;
            }
            if (tn.Tag == null)
            {
                MessageBox.Show("获取样本信息失败");
                return;
            }

            try
            {
                OperApply operApply = tn.Tag as OperApply;
                if (operApplyMgr.UpdateColFlag(operApply.OperApplyId.ToString(), "0") == -1)
                {
                    MessageBox.Show("操作失败");
                    return;
                }
                MessageBox.Show("操作成功");
            }
            catch
            {
                //MessageBox.Show("已送标本不能取消！");
            }
        }

        private void ReLoad(object sender, EventArgs e)
        {
            Query(null, null);
        }
        private void GenerateColForm()
        {
            FS.HISFC.Components.Speciment.Print.ucBColForm frmCol = new FS.HISFC.Components.Speciment.Print.ucBColForm();
            frmCol.OnReLoad += new Speciment.Print.ucBColForm.ReLoad(ReLoad);            
            frmCol.Show();
            //frmL.OnSetContainerId += new frmLocate.SetContainerId(OnSetContainerId);
           
        }

        /// <summary>
        /// 加载血标本类型
        /// </summary>
        private void InitSpecType()
        {
            ArrayList arr = specTypeManage.GetSpecByOrgName("血");
            if (arr == null || arr.Count == 0)
            {
                return;
            }
            ArrayList temp = new ArrayList();
            //int count = arr.Count;
            for (int i = 0; i < arr.Count; i++)
            {
                FS.HISFC.Models.Speciment.SpecType st = arr[i] as FS.HISFC.Models.Speciment.SpecType;
                if (st.IsShow == "0")
                {
                    continue;
                }
                temp.Add(st);                
            }
            ucSourceForBlood = new ucSpecSourceForBlood[temp.Count];
            int index = 0;
            foreach (FS.HISFC.Models.Speciment.SpecType type in temp)
            {
                ucSourceForBlood[index] = new ucSpecSourceForBlood();
                ucSourceForBlood[index].SetSpecTypeName(type.SpecTypeID.ToString());               
                flpSubDetail.Controls.Add(ucSourceForBlood[index]);
                index++;
            } 
        }

        /// <summary>
        /// 动态加载FarPoint列
        /// </summary>
        private void GenerateColumn()
        {
            neuSpread1_Sheet1.ColumnCount = flpSubDetail.Controls.Count + 2;
            int index = 1;
           
            foreach (Control c in flpSubDetail.Controls)
            {
                ucSpecSourceForBlood subBlood = c as ucSpecSourceForBlood;
                GroupBox grp = subBlood.Controls[0] as GroupBox;
                int count = 0;
                string planInfo = "";
                foreach (Control cSub in grp.Controls)
                {
                    if (cSub.Name == "chkName")
                    {
                        CheckBox chkName = cSub as CheckBox;
                        neuSpread1_Sheet1.Columns[index].Label = chkName.Text;
                        planInfo += chkName.Text;
                    }
                    if (cSub.Name == "nudCount")
                    {
                        NumericUpDown nud = cSub as NumericUpDown;
                        count = Convert.ToInt32(nud.Value);
                        neuSpread1_Sheet1.Columns[index].Width = 90 * count;
                    }
                }
                FarPoint.Win.Spread.CellType.TextCellType cellType = new FarPoint.Win.Spread.CellType.TextCellType();
                neuSpread1_Sheet1.Columns[index].CellType = cellType;
                neuSpread1_Sheet1.Columns[index].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                neuSpread1_Sheet1.Columns[index].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                index++;
            }
        }

        /// <summary>
        /// 自动生成条形码
        /// </summary>
        private void GenerateBarCode(SpecSource source)
        {
            dicSourcePlanList = new Dictionary<SpecSource, List<SpecSourcePlan>>();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            specSourceManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            specSourcePlanManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            barCodeManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            diagManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #region 形成条码
            try
            {
                //当前分装标本的序列号
                string sequence = "";
                string curSourceCode = source.HisBarCode;
                //生成了新的条码就意味着没有保存
                //saved = false;
                int rowIndex = neuSpread1_Sheet1.ActiveCell.Row.Index;
                neuSpread1_Sheet1.Rows.Add(rowIndex + 1, 1);
                neuSpread1_Sheet1.Cells[rowIndex, 0].Text = curSourceCode;
                if (!dicBarCode.ContainsKey(curSourceCode))
                {
                    dicBarCode.Add(curSourceCode, new List<string>());
                }

                //设置当前标本源信息
                if (source == null)
                {
                    source = new SpecSource();
                }
                dicSourcePlanList.Add(source, new List<SpecSourcePlan>());
                int column = 1;

                //要生成的标本类型列表
                //List<string> specTypeList = new List<string>();
                //当前的病种
                string disType = "";

                foreach (Control c in flpSubDetail.Controls)
                {
                    //获取当前标本源的分装信息
                    ucSpecSourceForBlood subBlood = c as ucSpecSourceForBlood;
                    GroupBox grp = subBlood.Controls[0] as GroupBox;
                    int count = 0;
                    //指定特定人使用
                    bool specialUse = false;
                    //特定人的名字
                    string specialName = "";
                    //是否需要当前类型的标本
                    bool curChecked = false;
                    //是否自动生成条码
                    bool autoGenCode = true;
                    //生成的条形码
                    string codes = "";
                    string specType = "";

                    List<string> tmpBarCode = new List<string>();
                    SpecSourcePlan tmpPlan = new SpecSourcePlan();
                    foreach (Control cSub in grp.Controls)
                    {
                        if (cSub.Name == "nudCount")
                        {
                            NumericUpDown nud = cSub as NumericUpDown;
                            count = Convert.ToInt32(nud.Value);
                            tmpPlan.Count = count;
                            tmpPlan.StoreCount = count;
                        }
                        if (cSub.Name == "chkName")
                        {
                            CheckBox chk = cSub as CheckBox;
                            curChecked = chk.Checked ? true : false;
                            tmpPlan.SpecType.SpecTypeID = Convert.ToInt32(cSub.Tag.ToString());
                            if (!curChecked)
                            {
                                break;
                            }
                            specType = chk.Text.Trim();
                        }
                        if (cSub.Name == "nudCapcity")
                        {
                            NumericUpDown nudCap = cSub as NumericUpDown;
                            tmpPlan.Capacity = nudCap.Value;
                        }
                        if (cSub.Name == "nudUse")
                        {
                            NumericUpDown nudUse = cSub as NumericUpDown;
                            tmpPlan.ForSelfUse = Convert.ToInt32(nudUse.Value);
                        }
                        if (cSub.Name == "chkOnlySelf")
                        {
                            CheckBox chkCheck = cSub as CheckBox;
                            specialUse = chkCheck.Checked ? true : false;
                        }
                        if (cSub.Name == "txtName")
                        {
                            TextBox txt = cSub as TextBox;
                            specialName = txt.Text;
                        }
                        if (cSub.Name == "chkAutoGen")
                        {
                            CheckBox chkAuto = cSub as CheckBox;
                            autoGenCode = chkAuto.Checked;
                        }
                    }
                    if (!curChecked)
                    {
                        column++;
                        continue;
                    }
                    if (specialUse && specialName.Trim() != "")
                    {
                        tmpPlan.LimitUse = specialName;
                    }
                    //string codes = "";
                    disType = cmbDisType.Text.TrimEnd().TrimStart();

                    //取血标本的最大序列号
                    sequence = barCodeManage.GetMaxSeqByDisAndType(disType, "1");
                    //分装标本的序号                   
                    if (autoGenCode)
                    {
                        SpecBarCode tmpSubBarCode = barCodeManage.GetSpecBarCode(disType, specType);
                        if (tmpSubBarCode == null || tmpSubBarCode.Sequence == "")
                        {
                            ClearData();
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("生成条码失败，查不到病种为" + disType + " 标本类型为" + specType + " 的信息");
                            return;
                        }
                        if (sequence == null || sequence == "")
                        {
                            ClearData();
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("查找标本序列号失败");
                            return;
                        }
                        string barCodePre = tmpSubBarCode.Sequence.PadLeft(6, '0') + tmpSubBarCode.DisAbrre + tmpSubBarCode.SpecTypeAbrre;
                        for (int i = 0; i < count; i++)
                        {
                            string barCode = barCodePre + (i + 1).ToString();
                            codes += barCode;

                            if (i < count - 1)
                            {
                                codes += ",";
                            }
                            dicBarCode[curSourceCode].Add(barCode);
                            tmpPlan.SubSpecCodeList.Add(barCode);
                            barCodeList.Add(barCode);
                            sequenceList.Add(sequence);
                            numList.Add((i + 1).ToString());
                            disTypeList.Add(disType);
                            specTypeList.Add(specType);
                        }
                    }
                    if (!autoGenCode)
                    {
                        foreach (Control cSub in grp.Controls)
                        {
                            if (cSub.Name == "lsbBarCode")
                            {
                                ListBox lb = cSub as ListBox;
                                foreach (string s in lb.Items)
                                {
                                    tmpBarCode.Add(s);
                                }
                            }
                        }
                        tmpPlan.SubSpecCodeList = tmpBarCode;
                        for (int i = 0; i < tmpBarCode.Count; i++)
                        {
                            codes += tmpBarCode[i];//
                            if (i < tmpBarCode.Count - 1)
                                codes += ",";
                            barCodeList.Add(tmpBarCode[i]);
                            sequenceList.Add("");
                            numList.Add("");
                            disTypeList.Add(disType);
                        }
                    }
                    neuSpread1_Sheet1.Cells[rowIndex, column].Text = codes;
                    dicSourcePlanList[source].Add(tmpPlan);
                    column++;
                }

                if (barCodeManage.UpdateMaxSeqByDisAndType(disType, "1", (Convert.ToInt32(sequence) + 1).ToString()) <= 0)
                {
                    ClearData();
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show("更新条码序号失败");
                    return;
                }
            #endregion

            #region 保存源标本及分装标本
                if (dicSourcePlanList.Count <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    return;
                }

                SpecInOper specInOper = new SpecInOper();
                //subList.Clear();
                foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp in dicSourcePlanList)
                {
                    if (tmp.Key.SpecId > 0)
                    {
                        if (specSourceManage.UpdateSpecSource(tmp.Key) <= 0)
                        {
                            MessageBox.Show("保存失败！", "条码生成");
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            return;

                        }
                    }
                    if (tmp.Key.SpecId <= 0)
                    {
                        string seqSource = "";
                        specSourceManage.GetNextSequence(ref seqSource);
                        tmp.Key.SpecId = Convert.ToInt32(seqSource);
                        tmp.Key.SpecNo = sequence.PadLeft(6,'0');
                        if (specSourceManage.InsertSpecSource(tmp.Key) <= 0)
                        {
                            ClearData();
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            //foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp1 in dicSourcePlanList)
                            //{
                            //    tmp1.Key.SpecId = 0;
                            //}
                            MessageBox.Show("保存失败！", "条码生成");
                            return;

                        }
                        if(needSaveDiagnose)
                        {
                            tmp.Key.IsInBase = '1';
                            SpecDiagnose diag = new SpecDiagnose();
                            string seq = "";
                            diagManage.GetNextSequence(ref seq);
                            diag.BaseID = Convert.ToInt32(seq);
                            diag.Diag.IcdName = cmbDiagnose.Text;
                            diag.InBaseTime = DateTime.Now;
                            diag.SpecSource.SpecId = tmp.Key.SpecId;
                            if (diagManage.InsertDiagnose(diag) == -1)
                            {
                                ClearData();
                                FS.FrameWork.Management.PublicTrans.RollBack();;
                                //foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp1 in dicSourcePlanList)
                                //{
                                //    tmp1.Key.SpecId = 0;
                                //}
                                MessageBox.Show("保存诊断信息失败！", "条码生成");
                                return;
                            }
                        }
                    }
                    foreach (SpecSourcePlan plan in tmp.Value)
                    {
                        plan.SpecID = tmp.Key.SpecId;
                        plan.TumorType = Convert.ToInt32(Constant.TumorType.正常).ToString();
                        string planSequence = "";
                        planSequence = specSourcePlanManage.GetNextSequence();
                        plan.PlanID = Convert.ToInt32(planSequence);
                        if (specSourcePlanManage.InsertSourcePlan(plan) <= 0)
                        {
                            ClearData();
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("保存失败！", "条码生成");
                            //foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp1 in dicSourcePlanList)
                            //{
                            //    tmp1.Key.SpecId = 0;
                            //}
                            return;
                        }
                        specInOper.DisTypeId = tmp.Key.DiseaseType.DisTypeID.ToString();
                        specInOper.LoginPerson = loginPerson;
                        specInOper.Trans = trans;
                        specInOper.SpecTypeId = plan.SpecType.SpecTypeID.ToString();
                        specInOper.SetTrans();

                        int saveResult = specInOper.SaveSubSpec(plan, ref subList);
                        if (saveResult == -1)
                        //if (SaveSubSpec(plan,tmp.Key.DiseaseType.DisTypeID.ToString()) == -1)
                        {
                            ClearData();
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("保存失败！", "条码生成");
                            //foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp1 in dicSourcePlanList)
                            //{
                            //    tmp1.Key.SpecId = 0;
                            //}
                            return;
                        }
                        if (saveResult == -2)
                        {
                            ClearData();
                            //foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp1 in dicSourcePlanList)
                            //{
                            //    tmp1.Key.SpecId = 0;
                            //}
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("查找标本盒失败！", "条码生成");
                            return;
                        } 
                    }
                }
                //防止一个标签生成2个不同的序列号。
                if (sequenceList.Count > 0)
                {
                    string seq = sequenceList[0];
                    //string seq1 = "";
                    for (int i = 1; i < sequenceList.Count; i++)
                    {
                        if (sequenceList[i] != seq)
                        {
                            ClearData();
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("生成标签出错！");
                            return ;
                        }
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                dicSourcePlanList = new Dictionary<SpecSource, List<SpecSourcePlan>>();
                //saved = true;
                this.useFullBoxList = specInOper.UseFullBoxList;
                MessageBox.Show("保存成功！", "条码生成");
                chkCli.Checked = false;
                dtpColTime.Focus();
                //nudHour.Focus();
                //nudHour.Select(0, 2);
                nudCount.Value = 1;
                nudNoCount.Value = 1;
                neuSpread1_Sheet1.Rows.Count = 0;//.Clear();
                neuSpread1_Sheet1.Rows.Add(0, 1);
                //清空该标志
                //return 1;
            }

            catch
            {
                ClearData();
                //foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp in dicSourcePlanList)
                //{
                //    tmp.Key.SpecId = 0;
                //}
                FS.FrameWork.Management.PublicTrans.RollBack();;
                MessageBox.Show("保存失败！", "条码生成");
                return;
            }
                #endregion

            #region 废弃

            //保存
            //if (SaveSource() == -1)
            //{
            //    barCodeList = new List<string>();
            //    sequenceList = new List<string>();
            //    disTypeList = new List<string>();
            //    numList = new List<string>();
            //    List<string> tmpList = new List<string>();
            //    foreach (string type in specTypeList)
            //    {
            //        tmpList.Add(type);
            //        if (tmpList.Contains(type))
            //        {
            //            continue;
            //        }
            //        SpecBarCode tmp = barCodeManage.GetSpecBarCode(disType, type);
            //        if (tmp != null && tmp.Sequence.Trim() != "")
            //        {
            //            int seq = Convert.ToInt32(tmp.Sequence);
            //            seq = seq - 1;                      
            //            int s = barCodeManage.UpdateBarCode(disType, type, seq.ToString());
            //        }
            //    }
            //    return;
            //}
            #endregion

            #region  打印标签

            DialogResult dr = MessageBox.Show("是否打印标签?", "标签打印", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                int result = PrintLabel.Print2DBarCode(barCodeList, sequenceList, specTypeList, disTypeList, numList);
                if (result == -2)
                {
                    MessageBox.Show("连接打印机失败");

                }
                if (result == -1)
                {
                    MessageBox.Show("打印失败");
                }
            }
            barCodeList = new List<string>();
            sequenceList = new List<string>();
            specTypeList = new List<string>();
            disTypeList = new List<string>();
            numList = new List<string>();
            foreach (SpecBox box in this.useFullBoxList)
            {
                FS.HISFC.Models.Speciment.SpecType tmp = specTypeManage.GetSpecTypeByBoxId(box.BoxId.ToString());
                string strHint = box.BoxBarCode;
                if (tmp != null)
                {
                    strHint += "  " + tmp.SpecTypeName;
                }
                strHint += " 标本盒已满，请添加新的标本盒！";
                MessageBox.Show(strHint);
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
            #endregion

        }

        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <param name="inHosNum">住院流水号</param>
        /// <returns>病人的ID</returns>
        private int GetPatientInfo(string inHosNum)
        {
            FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            FS.HISFC.BizLogic.RADT.InPatient patientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
            string cardNo = "";
            if (inHosNum.Contains("C") || inHosNum.Contains("M")|| chkCli.Checked)
            {
                patientInfo = patientMgr.QueryComPatientInfo(inHosNum);
                cardNo = inHosNum;
                needSaveDiagnose = true;
            }
            else
            {
                patientInfo = patientMgr.QueryComPatientInfo(inHosNum.Substring(4));
                cardNo = inHosNum.Substring(4);
            }
            if (patientInfo == null)
            {
                MessageBox.Show("获取病人信息失败！");
                return -1;
            }
            //根据病历号查询标本库病人信息，如果存在更新，不存在插入
            PatientManage pm = new PatientManage();
            SpecPatient specPatient = new SpecPatient();           
            string sql = "select distinct * from SPEC_PATIENT where CARD_NO = '" + cardNo + "'";
            sql += " union select distinct * from SPEC_PATIENT where IC_CARDNO = '" + patientInfo.Card.ICCard.ID + "'";
            ArrayList arr = pm.GetPatientInfo(sql);

            //获取病人信息索引表信息
            specPatient.Address = patientInfo.AddressHome;
            specPatient.Birthday = patientInfo.Birthday;
            specPatient.BloodType = patientInfo.BloodType.ID.ToString();
            specPatient.IcCardNo = patientInfo.Card.ICCard.ID;
            specPatient.ContactNum = patientInfo.PhoneBusiness;
            specPatient.Gender = Convert.ToChar(patientInfo.Sex.ID.ToString());
            specPatient.Home = patientInfo.DIST;
            specPatient.HomePhoneNum = patientInfo.PhoneHome;
            specPatient.IdCardNo = patientInfo.IDCard;
            specPatient.IsMarried = patientInfo.MaritalStatus.ID.ToString();
            specPatient.CardNo = inHosNum.Substring(4);
            switch (patientInfo.MaritalStatus.ID.ToString())
            {
                case "W":
                    specPatient.IsMarried = "4";
                    break;
                case "A":
                    specPatient.IsMarried = "5";
                    break;
                case "R":
                    specPatient.IsMarried = "5";
                    break;
                case "D":
                    specPatient.IsMarried = "3";
                    break;
                case "M":
                    specPatient.IsMarried = "2";
                    break;
                case "S":
                    specPatient.IsMarried = "1";
                    break;
            }
            specPatient.Nation = patientInfo.Nationality.ID;
            specPatient.Nationality = patientInfo.Country.ID;
            specPatient.PatientName = patientInfo.Name;
            if (arr != null && arr.Count > 0)
            {
                SpecPatient tmp = arr[0] as SpecPatient;
                specPatient.PatientID = tmp.PatientID;
                pm.UpdatePatient(specPatient);
                return specPatient.PatientID;
            }
            else
            {
                string sequence = "";
                specPatient.CardNo = cardNo;
                pm.GetNextSequence(ref sequence);
                if (sequence == "")
                {
                    MessageBox.Show("获取病人索引失败！");
                    return -1;
                }
                specPatient.PatientID = Convert.ToInt32(sequence);
                if (pm.InsertPatient(specPatient) <= 0)
                {
                    MessageBox.Show("更新病人信息失败！");
                    return -1;
                }
                return specPatient.PatientID;
            }       
          
        }

        /// <summary>
        /// 从SPEC_OPERAPPLY中获取采集单信息，并生成条码
        /// </summary>
        /// <param name="barCode"></param>
        private void GetApplyInfoAndSubBarCode(string barCode)
        {
            try
            {   
                //id就是条码值
                int id = Convert.ToInt32(barCode);
                FS.HISFC.Models.Speciment.OperApply operApply = operApplyMgr.GetById(id.ToString(),"B");

                if (operApply == null || operApply.OperApplyId <= 0)
                {
                    MessageBox.Show("获取信息失败，请检查是否存在该条码号！");
                    txtBarCode.Text = "";
                    cmbDisType.Text = "";
                    txtBarCode.Focus();
                    return;
                }
                if (operApply.HadCollect == "3")
                {
                    txtBarCode.Text = "";
                    MessageBox.Show("该样本已取消");
                    return;
                }

                int patientId = GetPatientInfo(operApply.InHosNum);
                if (patientId == -1)
                {
                    return;
                }

                txtInHosNum.Text = operApply.InHosNum;
                txtName.Text = operApply.PatientName;
                if (!txtInHosNum.Text.Trim().Contains("C") && !txtInHosNum.Text.Trim().Contains("M") && !customDis)
                {
                    cmbDiagnose.Text = operApply.MainDiaName;
                }

                if (dicDiagToPos.ContainsKey(operApply.MainDiaName) && !customDis)
                {
                    cmbDisType.Tag = dicDiagToPos[operApply.MainDiaName];

                    //cmbDisType.Text = ((KeyValuePair<int, string>)(cmbDisType.SelectedItem)).Value;
                }

                if (cmbDisType.Tag == null || cmbDisType.Text == "")
                {
                    MessageBox.Show("请选择诊断，病种将自动关联!", "条码打印");
                    cmbDiagnose.Focus();
                    //cmbDisType.Focus();
                    return;
                }
                //如果是骨髓，默认分装血浆，白细胞
                if (cmbDisType.Text.Contains("骨髓"))
                {
                    nudNoCount.Value = 0;
                }
                if (needSaveDiagnose && cmbDiagnose.Text.Trim() == "")
                {
                    MessageBox.Show("门诊病人必须录入诊断!");
                    cmbDiagnose.Focus();
                    return;
                }

                DialogResult confirm = MessageBox.Show("信息核对完毕", "打印", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.No)
                {
                    return;
                }

                ArrayList arr = specSourceManage.GetSpecSource("select distinct * from spec_source where hisbarcode = '" + barCode + "'");
                if (arr != null && arr.Count > 0)
                {
                    MessageBox.Show("此标本已经分装！");
                    txtBarCode.Focus();
                    return;
                }
                SpecSource tmpSource = new SpecSource();
                tmpSource.AnticolBldCapcacity = nudCapcity.Value;
                tmpSource.AnticolBldCount = Convert.ToInt32(nudCount.Value);
                tmpSource.NonAntiBldCapcacity = nudNoCapacity.Value;
                tmpSource.NonantiBldCount = Convert.ToInt32(nudNoCount.Value);
                tmpSource.Patient.PatientID = patientId;
                tmpSource.HisBarCode = barCode;
                tmpSource.InPatientNo = operApply.InHosNum;
                tmpSource.IsHis = '1';
                tmpSource.MediDoc = operApply.MediDoc.Clone();
                tmpSource.DeptNo = operApply.OperDeptId;
                tmpSource.SendDoctor = operApply.MediDoc.MainDoc.Clone();
                tmpSource.OrgOrBoold = "B";
                tmpSource.DiseaseType.DisTypeID = Convert.ToInt32(cmbDisType.Tag.ToString());
                tmpSource.GetSpecPeriod = operApply.GetPeriod;
                tmpSource.TumorPor = operApply.TumorPor;
                tmpSource.OperPosCode = operApply.OperPosId;
                tmpSource.OperPosName = operApply.OperPosName;
                tmpSource.OperApplyId = operApply.OperId;
                tmpSource.ColDoctor.ID = loginPerson.ID;
                tmpSource.OperEmp.Name = loginPerson.Name;
                tmpSource.SendTime = dtpColTime.Value; //new DateTime(dtpColTime.Value.Year,dtpColTime.Value.Month,dtpColTime.Value.Day,Convert.ToInt32(nudHour.Value.ToString()),Convert.ToInt32(nudMin.Value.ToString()),0);
                tmpSource.Ext2 = cmbDiagnose.Text.Trim();
                if (grpReason.Visible)
                {
                    tmpSource.Commet += grpReason.Text + " ";
                    if (rbtBreak.Checked)
                    {
                        tmpSource.Commet += rbtBreak.Text;
                    }
                    if (rbtNotGet.Checked)
                    {
                        tmpSource.Commet += rbtNotGet.Text;
                    }
                    if (rbtOther.Checked)
                    {
                        tmpSource.Commet += rbtOther.Text;
                    }
                }
                operApply.HadCollect = "1";
                operApply.GetOperInfoTime = DateTime.Now;
                if (operApplyMgr.UpdateOperApply(operApply) == -1)
                {
                    MessageBox.Show("更新收集标志失败");
                    return;
                }
                GenerateBarCode(tmpSource);                
                txtBarCode.Text = "";
                cmbDisType.Text = "";
                cmbDiagnose.Text = "";
                grpReason.Visible = false;
                nudCount.Value = 1;
                nudNoCount.Value = 1;
            }
            catch
            {
                MessageBox.Show("获取信息失败");
                return;
            }
        } 

        /// <summary>
        /// 保存分装标本及分装标本信息
        /// </summary>
        /// <returns></returns>
        private int SaveSource()
        {
            if (dicSourcePlanList.Count <= 0)
            {                
                return 0;
            }

            try
            {
                //如果诊断病种没有维护，就自动保存
                string curDiag = cmbDiagnose.Text.TrimEnd().TrimStart();
                if (curDiag != "")
                {
                    if (!dicDiagToPos.ContainsKey(curDiag))
                    {
                        this.SaveDiagToDis();
                        dicDiagToPos = disTypeManage.GetDiagToDis();
                    }
                }
            }
            catch
            { }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            specSourceManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            specSourcePlanManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                SpecInOper specInOper = new SpecInOper();
                //subList.Clear();
                foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp in dicSourcePlanList)
                {
                    if (tmp.Key.SpecId > 0)
                    {
                        if (specSourceManage.UpdateSpecSource(tmp.Key) <= 0)
                        {
                            MessageBox.Show("保存失败！", "条码生成");
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            return -1;

                        }
                    }
                    if (tmp.Key.SpecId <= 0)
                    {
                        string sequence = "";
                        specSourceManage.GetNextSequence(ref sequence);
                        tmp.Key.SpecId = Convert.ToInt32(sequence);
                        if (specSourceManage.InsertSpecSource(tmp.Key) <= 0)
                        {
                            ClearData();
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            //foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp1 in dicSourcePlanList)
                            //{
                            //    tmp1.Key.SpecId = 0;
                            //}
                            MessageBox.Show("保存失败！", "条码生成");
                            return -1;

                        }
                    }                    
                    foreach (SpecSourcePlan plan in tmp.Value)
                    {
                        plan.SpecID = tmp.Key.SpecId;
                        plan.TumorType = Convert.ToInt32(Constant.TumorType.正常).ToString();
                        string planSequence = "";
                        planSequence = specSourcePlanManage.GetNextSequence();
                        plan.PlanID = Convert.ToInt32(planSequence);
                        if (specSourcePlanManage.InsertSourcePlan(plan) <= 0)
                        {
                            ClearData();
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("保存失败！", "条码生成");
                            //foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp1 in dicSourcePlanList)
                            //{
                            //    tmp1.Key.SpecId = 0;
                            //}
                            return -1; 
                        }                        
                        specInOper.DisTypeId = tmp.Key.DiseaseType.DisTypeID.ToString();
                        specInOper.LoginPerson = loginPerson;
                        specInOper.Trans = trans;
                        specInOper.SpecTypeId = plan.SpecType.SpecTypeID.ToString();
                        specInOper.SetTrans();

                        int saveResult = specInOper.SaveSubSpec(plan, ref subList);
                        if (saveResult == -1)
                        //if (SaveSubSpec(plan,tmp.Key.DiseaseType.DisTypeID.ToString()) == -1)
                        {                            
                            ClearData();
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("保存失败！", "条码生成");
                            //foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp1 in dicSourcePlanList)
                            //{
                            //    tmp1.Key.SpecId = 0;
                            //}
                            return -1; 
                        }
                        if (saveResult == -2)
                        {
                            ClearData();
                            //foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp1 in dicSourcePlanList)
                            //{
                            //    tmp1.Key.SpecId = 0;
                            //}                            
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("查找标本盒失败！", "条码生成");
                            return -1; 
                        }
                    }
                }         
                FS.FrameWork.Management.PublicTrans.Commit();                
                dicSourcePlanList = new Dictionary<SpecSource, List<SpecSourcePlan>>();
                //saved = true;
                this.useFullBoxList = specInOper.UseFullBoxList;
                MessageBox.Show("保存成功！", "条码生成");                
                neuSpread1_Sheet1.Rows.Count = 0;//.Clear();
                neuSpread1_Sheet1.Rows.Add(0, 1);
                dtpColTime.Focus();
                //清空该标志
                needSaveDiagnose = false;
                customDis = false;
                cmbDiagnose.Text = "";
                return 1;
            }
            catch
            {
                ClearData();
                //foreach (KeyValuePair<SpecSource, List<SpecSourcePlan>> tmp in dicSourcePlanList)
                //{
                //    tmp.Key.SpecId = 0;
                //}

                FS.FrameWork.Management.PublicTrans.RollBack();;
                MessageBox.Show("保存失败！", "条码生成");
                return -1;
            }
           
        }

        private void txtBarCode_TextChanged(object sender, EventArgs e)
        {
            string barCode = txtBarCode.Text.Trim();
            
            if (barCode.Length == 12)
            {
                //txtBarCode_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                //GetApplyInfoAndSubBarCode(barCode.PadLeft(12, '0'));
            }
        }

        private void ucBloodBarcode_Load(object sender, EventArgs e)
        {
            //loginPerson = FS.NFC.Management.Connection.Operator as FS.HISFC.Object.Base.Employee;
            //FS.HISFC.Management.Manager.DepartmentStatManager manager = new FS.HISFC.Management.Manager.DepartmentStatManager();
            ////alDepts = manager.GetMultiDept(loginPerson.ID);
            //ArrayList alDepts = new ArrayList();
            //alDepts = manager.LoadAll();           
            //cmbOperDept.AddItems(alDepts);
            //LoadOperApply();
            //LoadSpecSource();
            //InitSpecType();
            //GenerateColumn();
            //DisTypeBinding();
            //dicDiagToPos = disTypeManage.GetDiagToDis();
            ////加载入院诊断
            //FS.HISFC.Management.Manager.Constant conMgr = new FS.HISFC.Management.Manager.Constant();
            //ArrayList arrDiagnoseList = conMgr.GetList("DiagnosebyNurse");
            //if (arrDiagnoseList != null && arrDiagnoseList.Count > 0)
            //{
            //    cmbDiagnose.AddItems(arrDiagnoseList);
            //}
            //dtpColTime.Focus();
        }

        protected override void OnLoad(EventArgs e)
        {
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.BizLogic.Manager.Department manager = new FS.HISFC.BizLogic.Manager.Department();
            //alDepts = manager.GetMultiDept(loginPerson.ID);
            ArrayList alDepts = new ArrayList();
            alDepts = manager.GetDeptmentByType("I");//.GetMultiDept(loginPerson.ID);
            cmbOperDept.AddItems(alDepts);
            LoadOperApply();
            LoadSpecSource();
            InitSpecType();
            GenerateColumn();
            DisTypeBinding();
            dicDiagToPos = disTypeManage.GetDiagToDis();
            //加载入院诊断
            FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList arrDiagnoseList = conMgr.GetList("DiagnosebyNurse");
            if (arrDiagnoseList != null && arrDiagnoseList.Count > 0)
            {
                cmbDiagnose.AddItems(arrDiagnoseList);
            }
            base.OnLoad(e);
            dtpColTime.Focus();

        }

        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter && txtBarCode.Text.Trim() != "")
            {
                cmbDisType.Text = "";
                //dtpColTime.Focus();
                string barCode = txtBarCode.Text.Trim();
                try
                {
                    needSaveDiagnose = false;
                    customDis = false;
                    int id = Convert.ToInt32(barCode);
                    GetApplyInfoAndSubBarCode(id.ToString().PadLeft(12, '0'));
                }
                catch
                {
                    MessageBox.Show("获取条码失败");
                    return;
                }
            }
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("采集单生成", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);
            //this.toolBarService.AddToolButton("打印位置信息", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.A打印, true, false, null);
            this.toolBarService.AddToolButton("取消样本", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);
            this.toolBarService.AddToolButton("病人信息查询", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);

            this.toolBarService.AddToolButton("撤销取消", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.F返回, true, false, null);

            return this.toolBarService;
            //return base.OnInit(sender, neuObject, param);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "采集单生成":

                    GenerateColForm();
                    break;
                case "取消样本":
                    this.CancelSpec();
                    Query(null, null);
                    break;
                case "病人信息查询":
                    ucPatInfoQuery ucPatQuery = new ucPatInfoQuery();
                    Size size = new Size();
                    size.Height = 800;
                    size.Width = 1200;
                    ucPatQuery.Size = size;
                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucPatQuery, FormBorderStyle.FixedSingle, FormWindowState.Normal);
                    break;
                case "撤销取消":
                    this.ReCancelSpec();
                    Query(null, null);
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        public override int Save(object sender, object neuObject)
        {
            //this.SaveSource();
            return base.Save(sender, neuObject);
        }

        public override int Print(object sender, object neuObject)
        {
            //if (barCodeList.Count <= 0)
            //{
            //    MessageBox.Show("没有需要对应的条码");
            //    return 0; 
            //}
            //if (!saved)
            //{
            //    DialogResult diagResult = MessageBox.Show("打印之前请保存信息，如果未保存，可能生成重复的条形码!","条码打印", MessageBoxButtons.YesNo);
            //    if (diagResult == DialogResult.Yes)
            //    {
            //        Save(sender, neuObject);
            //    }                
            //}
            //int result = PrintLabel.Print2DBarCode(barCodeList, sequenceList, "", disTypeList, numList) ;
            //if (result == -2)
            //{
            //    MessageBox.Show("连接打印机失败");
            //    return -1;
            //}
            //if (result == -1)
            //{
            //    MessageBox.Show("打印失败");
            //    return -1;
            //}
            //barCodeList = new List<string>();
            //sequenceList = new List<string>();
            //disTypeList = new List<string>();
            //numList = new List<string>();
            //neuSpread1_Sheet1.Rows.Count = 0;//.Clear();
            //neuSpread1_Sheet1.Rows.Add(0, 1);

            SpecOutOper operOut = new SpecOutOper();             
            operOut.PrintTitle = "标本位置信息";
            try
            {
                operOut.PrintOutSpec(subList, null);
                subList = new List<SubSpec>();
            }
            catch
            {
                MessageBox.Show("打印位置信息失败！", "条码生成");                
                return -1;
            }
            return base.Print(sender, neuObject);
        }

        private void nudCount_ValueChanged(object sender, EventArgs e)
        {

            try
            {
                if (Convert.ToInt32(nudCount.Value) == 0)
                {
                    MessageBox.Show("请填写没有收到的原因");
                    grpReason.Text = "未收到" + "抗凝血的原因:";
                    grpReason.Visible = true;
                    rbtBreak.Focus();// txtReason.Focus();
                    //最初抗凝血为0时 自动不选择血浆和白细胞，后来标本库的人说，为了不让标本的位置放乱，后来讨论的方案是不管有没有 当要求分装
                    if (!cmbDisType.Text.Contains("骨髓"))
                    {
                        return;
                    }
                    foreach (Control c in flpSubDetail.Controls)
                    {
                        foreach (Control sc in c.Controls[0].Controls)
                        {
                            if (sc.Text == "血浆" || sc.Text == "白细胞")
                            {
                                CheckBox chk = sc as CheckBox;
                                chk.Checked = false;
                            }
                        }
                    }

                }
                if (nudCount.Value >= 1)
                {
                    foreach (Control c in flpSubDetail.Controls)
                    {
                        foreach (Control sc in c.Controls[0].Controls)
                        {
                            if (sc.Text == "血浆" || sc.Text == "白细胞")
                            {
                                CheckBox chk = sc as CheckBox;
                                chk.Checked = true;
                            }
                        }
                    }
                    grpReason.Visible = false;
                }
            }
            catch
            { }
        }

        private void nudNoCount_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(nudNoCount.Value) == 0)
                {
                    MessageBox.Show("请填写没有收到的原因");
                    grpReason.Text = "未收到" + "非抗凝血的原因:";
                    grpReason.Visible = true;
                    rbtBreak.Focus();
                    //最初抗凝血为0时 自动不选择血清，后来标本库的人说，为了不让标本的位置放乱，后来讨论的方案是不管有没有 当要求分装
                    //除骨髓病种 
                    if (!cmbDisType.Text.Contains("骨髓"))
                    {
                        return;
                    }
                    foreach (Control c in flpSubDetail.Controls)
                    {
                        foreach (Control sc in c.Controls[0].Controls)
                        {
                            if (sc.Text == "血清" || sc.Text == "红细胞")
                            {
                                CheckBox chk = sc as CheckBox;
                                chk.Checked = false;
                            }
                        }
                    }

                }
                if (nudNoCount.Value >= 1)
                {
                    foreach (Control c in flpSubDetail.Controls)
                    {
                        foreach (Control sc in c.Controls[0].Controls)
                        {
                            if (sc.Text == "血清" || sc.Text == "红细胞")
                            {
                                CheckBox chk = sc as CheckBox;
                                chk.Checked = true;
                            }
                        }
                    }
                    grpReason.Visible = false;                    
                }
            }
            catch
            { }
        }

        private void cmbDisType_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Control c in flpSubDetail.Controls)
            {
                ucSpecSourceForBlood ub = c as ucSpecSourceForBlood;
                ub.DisTypeName = cmbDisType.Text;
                //ub.DisTypeId = cmbDisType.SelectedValue;
            }
        }

        private void cmbDisType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtBarCode.Text.Trim() != "")
                {
                    customDis = true;
                    string barCode = txtBarCode.Text.Trim();
                    try
                    {
                        needSaveDiagnose = false;
                        int id = Convert.ToInt32(barCode);
                        GetApplyInfoAndSubBarCode(id.ToString().PadLeft(12, '0'));
                    }
                    catch
                    {
                        MessageBox.Show("获取条码失败");
                        return;
                    }
                }
                else
                {
                    txtBarCode.Focus();
                }
            }
        }

        public override int Exit(object sender, object neuObject)
        {
            //if (!saved && dicSourcePlanList!=null & dicSourcePlanList.Count>0)
            //{
            //    //DialogResult diagResult = MessageBox.Show("信息没有保存", "条码打印", MessageBoxButtons.YesNo);
            //    //if (diagResult == DialogResult.Yes)
            //    //{
            //        Save(sender, neuObject);
            //    //}
            //}
            return base.Exit(sender, neuObject);
        }

        public override int Query(object sender, object neuObject)
        {
            for (int i = 0; i < tvSpec.Nodes.Count; i++)
            {
                tvSpec.Nodes[i].Nodes.Clear();
            }
            LoadSpecSource();
            LoadOperApply();
            return base.Query(sender, neuObject);
        }

        private void dtpColTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtBarCode.Text.Trim() != "")
            {
                string barCode = txtBarCode.Text.Trim();
                try
                {
                    needSaveDiagnose = false;
                    customDis = false;
                    int id = Convert.ToInt32(barCode);
                    GetApplyInfoAndSubBarCode(id.ToString().PadLeft(12, '0'));
                }
                catch
                {
                    MessageBox.Show("获取条码失败");
                    return;
                }
            }
        }

        private void cmbDiagnose_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbDiagnose.Text.Trim() != "")
                {
                    customDis = true;
                }
                if (dicDiagToPos.ContainsKey(cmbDiagnose.Text) && customDis)
                {
                    cmbDisType.Tag = dicDiagToPos[cmbDiagnose.Text];

                    //cmbDisType.Text = dicDiagToPos[cmbDiagnose.Text];
                }
                else
                {
                    cmbDisType.Text = "";
                }
            }
            catch
            { }
        }

        private void dtpColTime_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBarCode.Focus();
            //    nudHour.Select(0,2);//.Select();
            //    nudHour.Focus();
            }
        }

        private void nudHour_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                nudMin.Select(0,2);
                nudMin.Focus();
            }
        }

        private void nudMin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBarCode.Focus();
            }
        }

        private void nudHour_ValueChanged(object sender, EventArgs e)
        {
            if(Convert.ToInt32(nudHour.Value.ToString())>=24)
            {              
                nudHour.Value = 23;
            }
            if (Convert.ToInt32(nudMin.Value.ToString()) >= 59)
            {               
                nudMin.Value = 59;
            }
        }
    }
}
