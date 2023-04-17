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
    public partial class ucSubSpecForOrg : UserControl
    {
        #region 私有变量
        private ucColForOrg ucSubOrgSource;
        private ucColForOrg[] ucSubOrgSourceInFlp;

        private List<SpecBox> curUseBoxList = new List<SpecBox>();

        private SpecSourcePlan specSourcePlan;
        private SubSpec subSpec;
        private FS.HISFC.Models.Base.Employee loginPerson;

        private SubSpecManage subSpecManage;
        private SpecSourcePlanManage specSourcePlanManage;
        private SpecTypeManage specTypeManage;
        private SpecBoxManage specBoxManage;

        private string flag;
        private int tumorType;
        private int specId;
        private string sideFrom = "";

        //当前操作属性，是修改Or新建
        private string curOperPro = "";
        private int disTypeId;
        private string tumorPor = "";
        private string tumorPos = "";
        private string transPos = "";
        private string tumorDet = "";

        /// <summary>
        /// 如果标志为空，则加载标本类型，否则不加载
        /// </summary>
        public string Flag
        {
            get
            {
                return flag;
            }
            set
            {
                flag = value;
            }
        }

        /// <summary>
        /// 当前使用的标本盒
        /// </summary>
        public List<SpecBox> UseFullBoxList
        {
            get
            {
                return curUseBoxList;
            }
            set
            {
                curUseBoxList = value;
            }
        }

        /// <summary>
        /// 所属的标本Id
        /// </summary>
        public int SpecId
        {
            get
            {
                return specId;
            }
            set
            {
                specId = value;
            }
        }

        /// <summary>
        /// 当前控件的操作属性，修改Or新建
        /// M：修改，N：新建
        /// </summary>
        public string CurOperPro
        {
            get
            {
                return curOperPro;
            }
            set
            {
                curOperPro = value;
            }
        }

        /// <summary>
        /// 肿物类型
        /// </summary>
        public int TumorType
        {
            get
            {
                return tumorType;
            }
            set
            {
                tumorType = value;
            }
        }

        /// <summary>
        /// 病种Id
        /// </summary>
        public int DisTypeId
        {
            get
            {
                return disTypeId;
            }
            set
            {
                disTypeId = value;
            }
        }

        /// <summary>
        /// 癌性质
        /// </summary>
        public string TumorPor
        {
            get
            {
                return tumorPor;
            }
            set
            {
                tumorPor = value;
            }
        }

        /// <summary>
        /// 取材脏器详细说明
        /// </summary>
        public string PosDet
        {
            set
            {
                tumorDet = value;
            }

        }

        /// <summary>
        /// 取自的侧别
        /// </summary>
        public string SideFrom
        {
            set
            {
                sideFrom = value;
            }
        }

        /// <summary>
        /// 肿物部位
        /// </summary>
        public string TumorPos
        {
            set
            {
                tumorPos = value;
            }
        }

        public string TransPos
        {
            set
            {
                transPos = value;
            }
        }

        public ucColForOrg[] SubOrgSourceInFlp
        {
            set
            {
                ucSubOrgSourceInFlp = value;
            }
            get
            {
                return ucSubOrgSourceInFlp;
            }
        }

        /// <summary>
        /// 返回标本号
        /// </summary>
        public string Seq
        {
            get
            {
 
                 foreach (ucColForOrg c in ucSubOrgSourceInFlp)
                {
                    if (c.Seq != null && c.Seq != string.Empty)
                    {
                        return c.Seq;
                    }
                    else
                    {
                        continue;
                    }
                }
                return string.Empty;
            }
        }

        #endregion

        public ucSubSpecForOrg()
        {
            InitializeComponent();
            specTypeManage = new SpecTypeManage();
            ucSubOrgSource = new ucColForOrg();
            specSourcePlan = new SpecSourcePlan();
            subSpec = new SubSpec();
            subSpecManage = new SubSpecManage();
            specSourcePlanManage = new SpecSourcePlanManage();
            specBoxManage = new SpecBoxManage();
            flag = "";
            tumorType = 0;
            //当前的的分装标本属于的原标本ID
            specId = 0;
            loginPerson = new FS.HISFC.Models.Base.Employee();
            FlpBinding();
        }

        private void RbtChanged(object sender, EventArgs e)
        {
            RadioButton rbt = sender as RadioButton;
            grp.Text = rbt.Text;
            if (ucSubOrgSourceInFlp != null)
            {
                SetTumorKind();
            }
        }

        private void SetTumorKind()
        {
            foreach (ucColForOrg org in ucSubOrgSourceInFlp)
            {
                if (rbtTumor.Checked) { org.TumorKind = "T"; org.TumorPos = rbtTumor.Text; }
                if (rbtSonTumor.Checked) { org.TumorKind = "S"; org.TumorPos = rbtSonTumor.Text; }
                if (rbtShuang.Checked) { org.TumorKind = "E"; org.TumorPos = rbtShuang.Text; }
                if (rbtNormal.Checked) { org.TumorKind = "N"; org.TumorPos = rbtNormal.Text; }
                if (rbtSide.Checked) { org.TumorKind = "P"; org.TumorPos = rbtSide.Text; }
                if (rbtLinBa.Checked) { org.TumorKind = "L"; org.TumorPos = rbtLinBa.Text; }
            }
        }

        /// <summary>
        /// 加载flp中的控件
        /// </summary>
        private void FlpBinding()
        {
            ArrayList arrTmp = new ArrayList();
            arrTmp = specTypeManage.GetSpecByOrgName("组织");
            ucSubOrgSourceInFlp = new ucColForOrg[arrTmp.Count];
            int i = 0;
            foreach (FS.HISFC.Models.Speciment.SpecType s in arrTmp)
            {
                ucSubOrgSource = new ucColForOrg();
                ucSubOrgSource.SetSpecType(s);
                ucSubOrgSourceInFlp[i] = ucSubOrgSource;
                ucSubOrgSourceInFlp[i].Name = "ucSubOrgSource" + i.ToString();
                flpPlan.Controls.Add(ucSubOrgSource);
                i++;
            }
            //foreach (ucColForOrg org in ucSubOrgSourceInFlp)
            //{
            //    flpPlan.Controls.Add(org);
            //}
        }

        private void SetPlanSoure(ref SpecSourcePlan s)
        {
            s.TumorPos = txtTumorPos.Text;
            int tumorType = 0;
            ///标本取材部位
            if (rbtTumor.Checked) tumorType = Convert.ToInt32(Constant.TumorType.肿物);
            if (rbtSonTumor.Checked) tumorType = Convert.ToInt32(Constant.TumorType.子瘤);
            if (rbtShuang.Checked) tumorType = Convert.ToInt32(Constant.TumorType.癌栓);
            if (rbtNormal.Checked) tumorType = Convert.ToInt32(Constant.TumorType.正常);
            if (rbtSide.Checked) tumorType = Convert.ToInt32(Constant.TumorType.癌旁);
            if (rbtLinBa.Checked) tumorType = Convert.ToInt32(Constant.TumorType.淋巴结);

            s.TumorType = tumorType.ToString();

            /////包膜是否完整
            //if (rbtYes.Checked) s.BaoMoEntire = '1';
            //if (rbtNo.Checked) s.BaoMoEntire = '0';

            /////是否碎组织
            //if (rbtPieceNo.Checked) s.BreakPiece = '0';
            //if (rbtPieceYes.Checked) s.BreakPiece = '1';
            s.Comment = txtComment.Text;
        }

        public void InitSpecPlan(ArrayList arrSpecPlan)
        {
            if (arrSpecPlan == null)
            {
                return;
            }
            foreach (SpecSourcePlan plan in arrSpecPlan)
            {
                for (int k = 0; k < ucSubOrgSourceInFlp.Length; k++)
                {
                    foreach (Control cp in ucSubOrgSourceInFlp[k].Controls[0].Controls)
                    {
                        if (cp.Name == "chkName")
                        {
                            CheckBox ck = cp as CheckBox;
                            if (ck.Tag.ToString() == plan.SpecType.SpecTypeID.ToString())
                            {
                                ucSubOrgSourceInFlp[k].SpecSourcePlan = plan;
                                ucSubOrgSourceInFlp[k].GetColOrgInfo(ck.Tag.ToString(), plan.Count, plan.LimitUse, plan.SubSpecCodeList);
                                //ucSubOrgSourceInFlp[k].SpecId = plan.SpecID;
                                this.specSourcePlan = plan;
                                this.specId = plan.SpecID;
                                break;
                            }
                        }
                        ucSubOrgSourceInFlp[k].SpecId = plan.SpecID;
                        if (rbtTumor.Checked) ucSubOrgSourceInFlp[k].TumorKind = "T";
                        if (rbtSonTumor.Checked) ucSubOrgSourceInFlp[k].TumorKind = "S";
                        if (rbtShuang.Checked) ucSubOrgSourceInFlp[k].TumorKind = "E";
                        if (rbtNormal.Checked) ucSubOrgSourceInFlp[k].TumorKind = "N";
                        if (rbtSide.Checked) ucSubOrgSourceInFlp[k].TumorKind = "P";
                        if (rbtLinBa.Checked) ucSubOrgSourceInFlp[k].TumorKind = "L";
                    }

                    //ucSpecSourceInFlp[i].Tag = plan;
                }
            }

            foreach (ucColForOrg cp in ucSubOrgSourceInFlp)
            {
                SpecSourcePlan tmp = cp.SpecSourcePlan;
                if (tmp == null || tmp.PlanID <= 0)
                {
                    tmp = new SpecSourcePlan();
                    cp.SpecSourcePlan = tmp;
                    cp.GetColOrgInfo(cp.Tag.ToString(), tmp.Count, tmp.LimitUse, tmp.SubSpecCodeList);
                }
            }

            //ucSpecSourceInFlp[i].Tag = plan;

        }

        private int UpdateSubSpec(ucColForOrg c, System.Data.IDbTransaction trans)
        {
            #region 更新已经存在的标本

            ucColForOrg ucOrg = c;
            SpecSourcePlan s = specSourcePlanManage.GetSourcePlan("SELECT * FROM SPEC_SOURCE_STORE WHERE SOTREID=" + c.SpecSourcePlan.PlanID.ToString())[0] as SpecSourcePlan;
            int oriCount = s.Count;
            //更新分装标本的属性
            this.SetPlanSoure(ref s);
            //int rt = specSourcePlanManage.UpdateSpecPlan(s);
            c.SpecSourcePlan = s;

            if (specSourcePlanManage.UpdateSpecPlan(c.SpecSourcePlan) <= 0)
            {
                return -1;
            }


            if (c.UpdateUserPro(trans) < 0)
            {
                MessageBox.Show("更新专用属性失败!");
                return -1;
            }

            ArrayList arrSubSpec = new ArrayList();
            arrSubSpec = subSpecManage.GetSubSpecByStoreId(s.PlanID.ToString());
            if (arrSubSpec == null || arrSubSpec.Count <= 0)
            {
                MessageBox.Show("获取分装标本失败！", "更新");
                return -1;
                //trans.RollBack();
                //return;
            }
            ControlCollection controls = (ucOrg.Controls[0] as GroupBox).Controls;
            foreach (Control cl in controls)
            {
                if (cl.Name.Contains("chkName"))
                {
                    CheckBox chk = cl as CheckBox;
                    string specTypeName = specTypeManage.GetSpecTypeById(s.SpecType.SpecTypeID.ToString()).SpecTypeName;
                    #region 如果标本类型选中
                    if (chk.Checked)
                    {
                        s.LimitUse = c.SpecSourcePlan.LimitUse;
                        s.SubSpecCodeList = c.SpecSourcePlan.SubSpecCodeList;
                        s.Count = c.SpecSourcePlan.Count;

                        //如果数量相等,仅更新条码
                        #region
                        if (s.Count == oriCount)
                        {
                            int index = 0;
                            if (!ucOrg.BarCodeCountValidate())
                            {
                                return -1;
                            }
                            foreach (SubSpec spec in arrSubSpec)
                            {
                                int result = ucOrg.UpdateBarCode(spec.SubSpecId, index, trans);
                                if (result <= 0)
                                {
                                    MessageBox.Show("更新失败！", "更新");
                                    return -1;
                                }
                                index++;
                            }
                        }
                        #endregion
                        //如果减少分装标本
                        #region
                        if (oriCount > s.Count && s.Count != 0)
                        {

                            DialogResult dialog = MessageBox.Show("确定要减少 " + specTypeName + " 类型的标本？", "更新", MessageBoxButtons.YesNo);
                            if (dialog == DialogResult.No)
                            {
                                return -1;
                            }
                            if (!ucOrg.BarCodeCountValidate())
                            {
                                return -1;
                            }
                            int cutNum = oriCount - s.Count;
                            //更新库存
                            s.StoreCount -= cutNum;
                            for (int i = 0; i < cutNum; i++)
                            {
                                SubSpec tmpCutSpec = new SubSpec();
                                tmpCutSpec = arrSubSpec[arrSubSpec.Count - 1] as SubSpec;
                                SpecBox useBox = specBoxManage.GetBoxById(tmpCutSpec.BoxId.ToString());
                                int occupyCount = useBox.OccupyCount - 1;
                                if (occupyCount >= 0)
                                {
                                    if (specBoxManage.UpdateOccupyCount(occupyCount.ToString(), useBox.BoxId.ToString()) <= 0)
                                    {
                                        return -1;
                                    }
                                }
                                if (useBox.IsOccupy == '1')
                                {
                                    if (specBoxManage.UpdateSotreFlag("0", useBox.BoxId.ToString()) <= 0)
                                    {
                                        return -1;
                                    }
                                }
                                tmpCutSpec.BoxEndCol = 0;
                                tmpCutSpec.BoxEndRow = 0;
                                tmpCutSpec.BoxId = 0;
                                tmpCutSpec.BoxStartCol = 0;
                                tmpCutSpec.BoxStartRow = 0;
                                tmpCutSpec.SpecCap = 0.0M;
                                tmpCutSpec.SpecCount = 0;
                                tmpCutSpec.SubBarCode = "";
                                tmpCutSpec.StoreID = 0;
                                tmpCutSpec.Comment = "减少分装标本";
                                tmpCutSpec.SpecId = 0;
                                if (subSpecManage.UpdateSubSpec(tmpCutSpec) <= 0)
                                {
                                    MessageBox.Show("更新失败！", "更新");
                                    return -1;
                                }
                                arrSubSpec.RemoveAt(arrSubSpec.Count - 1);
                            }
                            //更新条码
                            int index = 0;
                            foreach (SubSpec spec in arrSubSpec)
                            {
                                int result = ucOrg.UpdateBarCode(spec.SubSpecId, index, trans);
                                if (result <= 0)
                                {
                                    MessageBox.Show("更新失败！", "更新");
                                    return -1;
                                }
                                index++;
                            }
                        }
                        #endregion
                        //如果增加分装标本
                        #region
                        if (s.Count > oriCount)
                        {
                            int addNum = s.Count - oriCount;
                            if (!ucOrg.BarCodeCountValidate())
                            {
                                return -1;
                            }
                            SpecSourcePlan tmpPlan1 = s.Clone();
                            tmpPlan1.Count = s.Count - oriCount;
                            SpecInOper specInOper = new SpecInOper();
                            specInOper.LoginPerson = loginPerson;
                            specInOper.SpecTypeId = tmpPlan1.SpecType.SpecTypeID.ToString();
                            specInOper.DisTypeId = disTypeId.ToString();
                            specInOper.Trans = trans;
                            specInOper.SetTrans();

                            List<SubSpec> subList = new List<SubSpec>();
                            //需要增加的标本数
                            //SpecSourcePlan tmpPlan = new SpecSourcePlan();
                            //tmpPlan = c.SpecSourcePlan.Clone();
                            //tmpPlan.Count = addNum;
                            int subResult = specInOper.SaveSubSpec(tmpPlan1, ref subList);
                            //更新库存
                            s.StoreCount += addNum;
                            if (subResult == -2)
                            {
                                MessageBox.Show("此病种的 " + chk.Text + " 类型无标本盒存放", "标本保存");
                                return -1;
                            }
                            if (subResult <= 0)
                            {
                                MessageBox.Show("更新失败！", "更新");
                                return -1;
                            }

                            this.UseFullBoxList = specInOper.UseFullBoxList;
                            //重新获取标本列表
                            arrSubSpec = subSpecManage.GetSubSpecByStoreId(s.PlanID.ToString());
                            //更新条码
                            int index = 0;
                            foreach (SubSpec spec in arrSubSpec)
                            {
                                int result = ucOrg.UpdateBarCode(spec.SubSpecId, index, trans);
                                if (result <= 0)
                                {
                                    MessageBox.Show("更新失败！", "更新");
                                    return -1;
                                }
                                index++;
                            }
                        }
                        #endregion
                    }
                    #endregion
                    if (!chk.Checked || (chk.Checked && s.Count == 0))
                    {
                        DialogResult dialog = MessageBox.Show("确定删除 " + specTypeName + " 类型的标本？", "更新", MessageBoxButtons.YesNo);
                        if (dialog == DialogResult.No)
                        {
                            return -1;
                        }
                        s.Count = 0;
                        s.Capacity = 0;
                        s.StoreCount = 0;
                        s.SpecID = 0;
                        foreach (SubSpec spec in arrSubSpec)
                        {
                            SpecBox useBox = specBoxManage.GetBoxById(spec.BoxId.ToString());
                            int occupyCount = useBox.OccupyCount - 1;
                            if (occupyCount >= 0)
                            {
                                if (specBoxManage.UpdateOccupyCount(occupyCount.ToString(), useBox.BoxId.ToString()) <= 0)
                                {
                                    MessageBox.Show("更新失败！", "更新");
                                    return -1;
                                }
                            }
                            if (useBox.IsOccupy == '1')
                            {
                                if (specBoxManage.UpdateSotreFlag("0", useBox.BoxId.ToString()) <= 0)
                                {
                                    MessageBox.Show("更新失败！", "更新");
                                    return -1;
                                }
                            }
                            SubSpec tmpReSpec = spec.Clone();
                            tmpReSpec.SubBarCode = "";
                            tmpReSpec.Comment = "减少分装标本";
                            tmpReSpec.StoreID = 0;
                            tmpReSpec.BoxEndCol = 0;
                            tmpReSpec.BoxEndRow = 0;
                            tmpReSpec.BoxId = 0;
                            tmpReSpec.BoxStartCol = 0;
                            tmpReSpec.BoxStartRow = 0;
                            tmpReSpec.SpecCap = 0.0M;
                            tmpReSpec.SpecCount = 0;
                            tmpReSpec.SpecId = 0;
                            if (subSpecManage.UpdateSubSpec(tmpReSpec) <= 0)
                            {
                                MessageBox.Show("更新失败！", "更新");
                                return -1;
                            }
                        }
                    }
                }
            }
            s.BaoMoEntire = tumorDet;
            if (specSourcePlanManage.UpdateSpecPlan(s) <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("更新失败！", "更新");
                //trans.RollBack();
                return -1;
            }
            //trans.Commit();
            // MessageBox.Show("更新成功！", "更新");
            //当前标本盒已满
            if (UseFullBoxList != null)
            {
                foreach (SpecBox box in UseFullBoxList)
                {
                    MessageBox.Show(box.BoxBarCode + " 标本盒已满，请添加新的标本盒！", "添加标本盒");
                    //提示用户添加新的标本盒
                    FS.HISFC.Components.Speciment.Setting.frmSpecBox newSpecBox = new FS.HISFC.Components.Speciment.Setting.frmSpecBox();
                    if (box.DesCapType == 'B')
                        newSpecBox.CurLayerId = box.DesCapID;
                    else
                        newSpecBox.CurShelfId = box.DesCapID;
                    newSpecBox.Show();
                }
                UseFullBoxList = new List<SpecBox>();
            }
            return 1;
            #endregion
        }

        /// <summary>
        /// 保存分装标本库存
        /// </summary>
        /// <param name="specId"></param>
        /// <param name="tumorPor"></param>
        /// <returns></returns>
        public int SaveSourcePlan(System.Data.IDbTransaction trans)
        {
            specSourcePlanManage.SetTrans(trans);
            specTypeManage.SetTrans(trans);
            subSpecManage.SetTrans(trans);
            //specSourcePlanManage.SetTrans(trans.Trans);
            //specTypeManage.SetTrans(trans.Trans);

            specBoxManage.SetTrans(trans);

            #region 保存分装标本
            foreach (ucColForOrg c in ucSubOrgSourceInFlp)
            {
                if (c.UpdateSeq(trans) <= 0)
                {
                    return -1;
                }
                ControlCollection controls = (c.Controls[0] as GroupBox).Controls;
                foreach (Control cl in controls)
                {
                    //grp.Text;                    

                    if (cl.Name.Contains("chkName"))
                    {
                        CheckBox chk = cl as CheckBox;
                        //如果count是0，返回
                        if (c.SpecSourcePlan.Count <= 0)
                            continue;
                        c.SpecSourcePlan.SpecID = specId;
                        c.SpecSourcePlan.TumorPor = tumorPor;
                        c.SpecSourcePlan.TumorPos = tumorPos;
                        c.SpecSourcePlan.TransPos = transPos;
                        c.SpecSourcePlan.BaoMoEntire = tumorDet;
                        ///标本取材部位
                        if (rbtTumor.Checked) c.SpecSourcePlan.TumorType = Convert.ToInt32(Constant.TumorType.肿物).ToString();
                        if (rbtSonTumor.Checked) c.SpecSourcePlan.TumorType = Convert.ToInt32(Constant.TumorType.子瘤).ToString();
                        if (rbtShuang.Checked) c.SpecSourcePlan.TumorType = Convert.ToInt32(Constant.TumorType.癌栓).ToString();
                        if (rbtNormal.Checked) c.SpecSourcePlan.TumorType = Convert.ToInt32(Constant.TumorType.正常).ToString();
                        if (rbtSide.Checked) c.SpecSourcePlan.TumorType = Convert.ToInt32(Constant.TumorType.癌旁).ToString();
                        if (rbtLinBa.Checked) c.SpecSourcePlan.TumorType = Convert.ToInt32(Constant.TumorType.淋巴结).ToString();

                        /////包膜是否完整
                        //if (rbtYes.Checked) c.SpecSourcePlan.BaoMoEntire = '1';
                        //if (rbtNo.Checked) c.SpecSourcePlan.BaoMoEntire = '0';

                        ///是否碎组织
                        //if (rbtPieceNo.Checked) c.SpecSourcePlan.BreakPiece = "0";
                        //if (rbtPieceYes.Checked) c.SpecSourcePlan.BreakPiece = "1";
                        c.SpecSourcePlan.Comment = txtComment.Text;
                        int specTypeId = specTypeManage.GetSpecIDByName(chk.Text);
                        c.SpecSourcePlan.SpecType.SpecTypeID = specTypeId;
                        c.SpecSourcePlan.SideFrom = sideFrom;

                        if (!chk.Checked)
                        {
                            //如果PlanId》0 表示 更新
                            if (c.SpecSourcePlan.PlanID > 0)
                            {
                                if (UpdateSubSpec(c, trans) <= 0)
                                {
                                    return -1;
                                }
                            }
                        }
                        if (chk.Checked)
                        {
                            //插入到Spec_Source_Store中

                            //如果PlanId》0 表示 更新
                            if (c.SpecSourcePlan.PlanID > 0)
                            {
                                if (UpdateSubSpec(c, trans) <= 0)
                                {
                                    return -1;
                                }
                            }

                            else
                            {
                                if (!c.BarCodeCountValidate())
                                {
                                    return -1;
                                }
                                string sequence = specSourcePlanManage.GetNextSequence();
                                c.SpecSourcePlan.PlanID = Convert.ToInt32(sequence);
                                if (specSourcePlanManage.InsertSourcePlan(c.SpecSourcePlan) == -1)
                                    return -1;

                                SpecInOper specInOper = new SpecInOper();
                                specInOper.S863 = c.C863;
                                specInOper.S115 = c.C115;
                                specInOper.DisTypeId = disTypeId.ToString();
                                specInOper.LoginPerson = loginPerson;
                                specInOper.SpecTypeId = specTypeId.ToString();
                                specInOper.OrgOrBld = "O";
                                specInOper.Trans = trans;
                                specInOper.SetTrans();
                                List<SubSpec> subList = new List<SubSpec>();
                                int subResult = specInOper.SaveSubSpec(c.SpecSourcePlan, ref subList);
                                if (subResult == -2)
                                {
                                    c.SpecSourcePlan.PlanID = 0;
                                    MessageBox.Show("此病种的 " + chk.Text + " 类型无标本盒存放", "标本保存");
                                    return -2;
                                }
                                if (subResult <= 0)
                                {
                                    return -1;
                                }

                                if (c.UpdateUserPro(trans)<0)
                                {
                                    MessageBox.Show("更新专用属性失败！");
                                    return -1;
                                }

                                this.UseFullBoxList.AddRange(specInOper.UseFullBoxList);// = specInOper.UseFullBoxList;
                            }
                        }
                    }
                }
            }
            #endregion
            

            return 1;
        }

        private void ucSubSpecForOrg_Load(object sender, EventArgs e)
        {
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            //FlpBinding();

            if (tumorType != 0)
            {
                Constant.TumorType type = (Constant.TumorType)tumorType;
                switch (type)
                {
                    case Constant.TumorType.子瘤:
                        rbtSonTumor.Checked = true;
                        break;
                    case Constant.TumorType.癌栓:
                        rbtShuang.Checked = true;
                        break;
                    case Constant.TumorType.正常:
                        rbtNormal.Checked = true;
                        break;
                    case Constant.TumorType.癌旁:
                        rbtSide.Checked = true;
                        break;
                    case Constant.TumorType.淋巴结:
                        rbtLinBa.Checked = true;
                        break;
                    default:
                        rbtTumor.Checked = true;
                        break;
                }
            }
            SetTumorKind();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            btnDel.Parent.Parent.Parent.Parent.Dispose();
        }

        public void GetPlanSource(SpecSourcePlan s)
        {
            txtTumorPos.Text = s.TumorPos;
            char[] tumorType = s.TumorType.ToString().ToCharArray();

            foreach (char t in tumorType)
            {
                Constant.TumorType TumorType = (Constant.TumorType)(Convert.ToInt32(t.ToString()));
                switch (TumorType)
                {
                    case Constant.TumorType.肿物:
                        rbtTumor.Checked = true;
                        grp.Text = rbtTumor.Text;
                        break;
                    case Constant.TumorType.子瘤:
                        rbtSonTumor.Checked = true;
                        grp.Text = rbtSonTumor.Text;
                        break;
                    case Constant.TumorType.癌旁:
                        grp.Text = rbtSide.Text;
                        rbtSide.Checked = true;
                        break;
                    case Constant.TumorType.癌栓:
                        grp.Text = rbtShuang.Text;
                        rbtShuang.Checked = true;
                        break;
                    case Constant.TumorType.正常:
                        grp.Text = rbtNormal.Text;
                        rbtNormal.Checked = true;
                        break;
                    case Constant.TumorType.淋巴结:
                        grp.Text = rbtLinBa.Text;
                        rbtLinBa.Checked = true;
                        break;
                    default:
                        break;
                }
            }
            //if (s.BaoMoEntire == '0')
            //    rbtNo.Checked = true;
            //else
            //    rbtYes.Checked = true;
            //if (s.BreakPiece == "0")
            //    rbtPieceNo.Checked = true;
            //else
            //    rbtPieceYes.Checked = true;
            txtComment.Text = s.Comment;

        }

        private void btnPrintLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
