using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment
{
    public partial class ucSpecSourceForBlood : UserControl
    {
        private SpecSourcePlanManage specPlanManage;
        private SpecTypeManage specTypeManage;
        private SubSpecManage subSpecManage;
        private SpecBoxManage specBoxManage;
        private SpecSourcePlan specPlan;
        private SpecBarCodeManage barCodeManage;
        private FS.HISFC.Models.Base.Employee loginPerson;
        private System.Data.IDbTransaction trans;
        private string disTypeId;
        private string flag;
        private int specId;
        private int barCodeCount;
        private string disTypeName;
        //当前操作属性，是修改Or新建
        private string curOperPro;
        private List<SpecBox> curUseBoxList = new List<SpecBox>();
        private bool inited = false;


        private List<string> barCodeList;
        private List<string> sequenceList;
        private List<string> disTypeList;
        private List<string> numList;
        private List<string> specTypeList;

        //序列号
        private string sequence;
        //是否需要更新当前使用的序列号
        private bool needUpdateSeq = false;

        #region 属性
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
        /// 当前病种
        /// </summary>
        public string DisTypeId
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

        public string DisTypeName
        {
            get
            {
                return disTypeName;
            }
            set
            {
                disTypeName = value;
            }
        }

        public SpecSourcePlan SpecSourcePlan
        {
            get
            {
                return specPlan;
            }
            set
            {
                specPlan = value;
            }
        }

        public string Seq
        {
            get
            {
                try
                {
                    return lsbBarCode.Items[0].ToString();
                }
                catch
                {
                    return "";
                }
            }
        }
        #endregion

        public ucSpecSourceForBlood()
        {
            InitializeComponent();
            specPlan = new SpecSourcePlan();
            specPlanManage = new SpecSourcePlanManage();
            specTypeManage = new SpecTypeManage();
            subSpecManage = new SubSpecManage();
            specBoxManage = new SpecBoxManage();
            barCodeManage = new SpecBarCodeManage();
            loginPerson = new FS.HISFC.Models.Base.Employee();
            barCodeList = new List<string>();
            sequenceList = new List<string>();
            disTypeList = new List<string>();
            specTypeList = new List<string>();
            numList = new List<string>();
        }

        /// <summary>
        /// 是否自动形成条形码
        /// </summary>
        private void AutoGenerateBarCode()
        {

            int count = Convert.ToInt32(nudCount.Value);
            int barCount = lsbBarCode.Items.Count;
            if (chkAutoGen.Checked && chkName.Checked)
            {
                if (lsbBarCode.Items.Count == count)
                {
                    return;
                }
                if (count < barCount)
                {
                    for (int i = 0; i < barCount - count; i++)
                    {
                        lsbBarCode.Items.RemoveAt(lsbBarCode.Items.Count - 1 );
                    }
                    return;
                }
                if (count > barCount)
                {
                    int num = 0;
                    string barCodePre = "";
                    sequence = "";
                    //如果有数据
                    if (lsbBarCode.Items.Count > 0)
                    {
                        foreach (string s in lsbBarCode.Items)
                        {
                            int lastNum = Convert.ToInt32(s[s.Length - 1].ToString());
                            if (lastNum > num)
                            {
                                num = lastNum;
                            }
                            barCodePre = s.Substring(0, s.Length - 1);
                            string firstLabel = s.Substring(0, 1);
                            //以数字开头
                            if (System.Text.RegularExpressions.Regex.Match(firstLabel, "^\\d+$").Success)
                            {
                                sequence = s.Substring(0, 6);
                            }
                            //条码以字母开头
                            if (System.Text.RegularExpressions.Regex.Match(firstLabel, "^[a-zA-Z]+$").Success)
                            {
                                sequence = s.Substring(1, 6);
                            }
                        }                      
                    }
                    else
                    {            
                        //查看同一源样本的其他标本有没有数据，如果有数据沿用它们的序列号
                        if (SpecId > 0)
                        {
                            ArrayList arr = subSpecManage.GetSubSpecBySpecId(SpecId.ToString());
                            if (arr != null && arr.Count >0)
                            {
                                try
                                {
                                    SubSpec sub = arr[0] as SubSpec;
                                    string firstLabel = sub.SubBarCode.Substring(0, 1);
                                    //以数字开头
                                    if (System.Text.RegularExpressions.Regex.Match(firstLabel, "^\\d+$").Success)
                                    {
                                        sequence = sub.SubBarCode.Substring(0, 6);
                                    }
                                    //条码以字母开头
                                    if (System.Text.RegularExpressions.Regex.Match(firstLabel, "^[a-zA-Z]+$").Success)
                                    {
                                        sequence = sub.SubBarCode.Substring(1, 6);
                                    }
                                    needUpdateSeq = false;
                                }
                                catch
                                { }
                            }                            
                        }

                        SpecBarCode tmpSubBarCode = barCodeManage.GetSpecBarCode(disTypeName, chkName.Text);
                        if (tmpSubBarCode == null)
                        {
                            MessageBox.Show("生成条码失败，查不到病种为" + disTypeName + " 标本类型为" + chkName.Text + " 的信息");
                            return;
                        }
                        if (sequence == "")
                        {
                            sequence = tmpSubBarCode.Sequence;
                            needUpdateSeq = true;
                        }
                        barCodePre = sequence.PadLeft(6, '0') + tmpSubBarCode.DisAbrre + tmpSubBarCode.SpecTypeAbrre;
                        //needUpdateSeq = true;
                    }
                    num++;
                    for (int i = 0; i < count - barCount; i++)
                    {
                        string barCode = barCodePre + (num + i).ToString();
                        lsbBarCode.Items.Add(barCode);
                        barCodeList.Add(barCode);
                        numList.Add((num + i).ToString());
                        disTypeList.Add(this.disTypeName);
                        sequenceList.Add(barCode.Substring(0, 6));
                    }

                }
            }
            else
            {
                lsbBarCode.Items.Clear();
            }
        }

        /// <summary>
        /// 加载条码
        /// </summary>
        private void AddBarCode()
        {
            string barCode = txtBarCode.Text.Trim();
            int barCodeCount = lsbBarCode.Items.Count;
            if (barCode.Length >= 6 && barCodeCount < Convert.ToInt32(nudCount.Value) && chkName.Checked && !chkAutoGen.Checked)
            {
                //防止加入重复的条形码
                int index = 0;
                foreach (string s in lsbBarCode.Items)
                {
                    if (s == barCode)
                        break;
                    index++;
                }
                if (barCode.Length > 6)
                {
                    if (index <= lsbBarCode.Items.Count)
                    {
                        lsbBarCode.Items.Add(barCode);
                        sequenceList.Add("");
                    }
                }
                if (barCode.Length == 6)
                {
                    SpecBarCode bar = barCodeManage.GetSpecBarCode(disTypeName, chkName.Text);
                    if (bar != null)
                    {
                        barCode = bar.DisAbrre + barCode.PadLeft(6, '0') + bar.SpecTypeAbrre + barCodeCount.ToString();
                        sequenceList.Add(barCode.Substring(2,6));
                    }
                }
                lsbBarCode.Items.Add(barCode);
                barCodeList.Add(barCode);
                numList.Add(barCodeCount.ToString());
                disTypeList.Add(disTypeName);
                barCodeCount++;
                txtBarCode.Text = "";
            }
        }

        /// <summary>
        /// 设置事务
        /// </summary>
        /// <param name="curTtrans"></param>
        public void SetTrans(System.Data.IDbTransaction curTrans)
        {
            this.trans = curTrans;
            specTypeManage.SetTrans(this.trans);
            specPlanManage.SetTrans(this.trans);
            subSpecManage.SetTrans(this.trans);
            specBoxManage.SetTrans(this.trans);
            barCodeManage.SetTrans(this.trans);
        }

        public SpecSourcePlan GetSpecSourePlan()
        {
            //specPlan = new SpecSourcePlan();            
            specPlan.Capacity = nudCapcity.Value;
            specPlan.Count = Convert.ToInt32(nudCount.Value);
            specPlan.ForSelfUse = Convert.ToInt32(nudUse.Value);
            specPlan.Unit = "支";
            specPlan.StoreCount = specPlan.Count;
            //if(chkOnlyDept.Checked)
            //    specPlan.LimitUse = '2';
            //else 
            //    specPlan.LimitUse = '0';
            //specPlan.Comment = txtComment.Text;
            if (chkOnlySelf.Checked)
            {
                specPlan.LimitUse = txtName.Text.Trim();
            }
            foreach (string s in lsbBarCode.Items)
            {
                specPlan.SubSpecCodeList.Add(s);
                barCodeCount++;
            }
            return specPlan;
        }

        public void SetSpecSourcePlan(SpecSourcePlan plan)
        {
            lsbBarCode.Items.Clear();
            if (plan.PlanID <= 0)
            {
                chkAutoGen.Checked = false;
                chkName.Checked = false;
                return;
            }
            //specPlan = new SpecSourcePlan();   
            if (plan.Capacity > 0)
            {
                nudCapcity.Value = plan.Capacity;
            }
            if (plan.Count > 0)
            {
                nudCount.Value = plan.Count;
            }
            nudUse.Value = plan.ForSelfUse;
            lsbBarCode.Items.Clear();
            foreach (string s in plan.SubSpecCodeList)
            {
                lsbBarCode.Items.Add(s);
            }
            chkAutoGen.Checked = false;
            if (chkName.Tag.ToString() == plan.SpecType.SpecTypeID.ToString())
            {
                chkName.Checked = true;
                this.Tag = plan;
            }
            if (plan.LimitUse.Trim() != "")
            {
                chkOnlySelf.Checked = true;
                txtName.Text = plan.LimitUse;
            }
        }

        /// <summary>
        /// 设置标本类型及ID
        /// </summary>
        /// <param name="specTypeId">标本类型Id</param>
        public void SetSpecTypeName(string specTypeId)
        {
            FS.HISFC.Models.Speciment.SpecType st = specTypeManage.GetSpecTypeById(specTypeId);
            try
            {
                if (st != null)
                {
                    if (st.IsShow == "0" || st.DefaultCnt ==0)
                    {
                        chkName.Checked = false;
                    }
                    chkName.Text = st.SpecTypeName;
                    chkName.Tag = st.SpecTypeID;
                    nudCount.Value = st.DefaultCnt;
                    nudCapcity.Value = Convert.ToDecimal(st.Ext1);
                    inited = true;
                }
            }
            catch
            { }
        }

        /// <summary>
        /// 标本重定位
        /// </summary>
        /// <param name="subSpec">分装标本</param>
        /// <returns></returns>
        private int SubSpecReLocate(ref SubSpec subSpec)
        {
            SpecInOper specInOper = new SpecInOper();
            specInOper.Trans = trans;
            specInOper.SetTrans();
            specInOper.SubSpec = subSpec;
            specInOper.DisTypeId = this.disTypeId;
            specInOper.SpecTypeId = subSpec.SpecTypeId.ToString();

            int result = specInOper.LocateSubSpec();
            if (result == -2)
            {
                MessageBox.Show("此病种类型没有可使用的标本盒!", "标本定位");
                return -1;
            }
            if (result <= 0)
            {
                return -1;
            }
            subSpec = specInOper.SubSpec;

            if (subSpecManage.UpdateSubSpec(subSpec) <= 0)
            {
                return -1;
            }

            if (specInOper.InOperInit() == -1)
            {
                return -1;
            }

            if (specInOper.FullSpecBox != null && specInOper.FullSpecBox.BoxId > 0)
            {
                this.UseFullBoxList.Add(specInOper.FullSpecBox);
            }
            return 1;
        }

        private bool BarCodeCountValidate()
        {
            if (lsbBarCode.Items.Count < Convert.ToInt32(nudCount.Value))
            {
                DialogResult ds = MessageBox.Show("条形码数量不足，如果强制进行，将导致信息错误！继续？", "更新条码", MessageBoxButtons.YesNo);
                if (ds == DialogResult.No)
                {
                    return false;
                }
            }
            return true;
        }

        private int UpdateBarCode(int subSpecId, int index)
        {
            string barCode = "";
            if (lsbBarCode.Items.Count > index)
            {
                barCode = lsbBarCode.Items[index] == null ? "" : (lsbBarCode.Items[index]).ToString();
            }
            decimal capcacity = nudCapcity.Value;
            string sql = " UPDATE SPEC_SUBSPEC SET SPEC_SUBSPEC.SUBBARCODE='" + barCode + "',SPEC_SUBSPEC.SPECCAP =" + capcacity.ToString() + " WHERE SUBSPECID = " + subSpecId.ToString();
            return subSpecManage.UpdateSubSpec(sql);
        }

        /// <summary>
        /// 保存存储标本总信息
        /// </summary>
        /// <param name="isHis">标本是否来源于His</param>
        /// <param name="tumorType">标本的癌变性质</param>
        /// <returns></returns>
        public int SaveSourcePlan(char isHis, string tumorType)
        {
            if (!chkName.Checked)
            {
                return 0;
            }
            if (Convert.ToInt32(nudCount.Value) <= 0)
            {
                return 0;
            }
            specPlan.Capacity = nudCapcity.Value;
            specPlan.StoreCount = Convert.ToInt32(nudCount.Value);
            specPlan.Count = Convert.ToInt32(nudCount.Value);
            specPlan.ForSelfUse = Convert.ToInt32(nudUse.Value);
            if (chkOnlySelf.Checked)
            {
                specPlan.LimitUse = txtName.Text;
            }
            string planId = specPlanManage.GetNextSequence();
            specPlan.PlanID = Convert.ToInt32(planId);
            specPlan.SpecID = Convert.ToInt32(specId);
            specPlan.IsHis = isHis;
            specPlan.SpecType.SpecTypeID = Convert.ToInt32(chkName.Tag.ToString());
            specPlan.TumorType = tumorType;
            for (int i = 0; i < specPlan.Count; i++)
            {
                if (i < lsbBarCode.Items.Count)
                {
                    specPlan.SubSpecCodeList.Add(lsbBarCode.Items[i].ToString());
                }
            }
            if (specPlanManage.InsertSourcePlan(specPlan) <= 0)
            {
                return -1;
            }
            //更新此次使用的序号
            if (needUpdateSeq)
            {
                sequence = (Convert.ToInt32(sequence)+1).ToString();
                if (barCodeManage.UpdateMaxSeqByDisAndType(DisTypeName,"1",sequence) <= 0)
                {
                    return -1;
                }
                needUpdateSeq = false;
            }

            SpecInOper specInOper = new SpecInOper();
            specInOper.DisTypeId = disTypeId.ToString();
            specInOper.LoginPerson = loginPerson;
            specInOper.SpecTypeId = specPlan.SpecType.SpecTypeID.ToString();
            specInOper.Trans = trans;
            specInOper.SetTrans();

            List<SubSpec> subList = new List<SubSpec>();
            int subResult = specInOper.SaveSubSpec(specPlan, ref subList);
            if (subResult == -2)
            {
                MessageBox.Show(chkName.Text + "类型的标本无标本盒存放", "标本保存");
                return -1;
            }
            if (subResult <= 0)
            {
                return -1;
            }
            this.UseFullBoxList = specInOper.UseFullBoxList;
            return 1;

        }

        /// <summary>
        ///  更新分装标本
        /// </summary>
        /// <param name="isHis">是否来源于His</param>
        /// <param name="tumorType">标本的癌变性质</param>
        /// <returns></returns>
        public int UpdateSourcePlan(char isHis, string tumorType)
        {           
            specPlan = this.Tag as SpecSourcePlan;
            if (specPlan == null)
            {
                return 0;
            }
            if (specPlan.PlanID <= 0 && !chkName.Checked)
            {
                return 0;
            }

            if (specPlan.SpecID <= 0 && chkName.Checked)
            {
                return this.SaveSourcePlan(isHis, tumorType);
            }

            #region  更新已存在的标本
            if (specPlan.PlanID > 0)
            {
                specPlan.IsHis = isHis;
                specPlan.TumorType = tumorType;
                ArrayList arrSubSpec = subSpecManage.GetSubSpecByStoreId(specPlan.PlanID.ToString());
                int oriCount = specPlan.Count;
                if (arrSubSpec == null)
                {
                    return -1;
                }
                #region  需要删除该标本
                if (!chkName.Checked || oriCount == 0)
                {
                    specPlan.Capacity = 0;
                    specPlan.Count = 0;
                    specPlan.ForSelfUse = 0;
                    specPlan.StoreCount = 0;
                    specPlan.SpecID = 0;
                    foreach (SubSpec spec in arrSubSpec)
                    {
                        SpecBox useBox = specBoxManage.GetBoxById(spec.BoxId.ToString());
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

                        SubSpec tmpReSpec = new SubSpec();
                        tmpReSpec.StoreID = 0;
                        tmpReSpec = spec.Clone();
                        tmpReSpec.BoxEndCol = 0;
                        tmpReSpec.BoxEndRow = 0;
                        tmpReSpec.BoxId = 0;
                        tmpReSpec.BoxStartCol = 0;
                        tmpReSpec.BoxStartRow = 0;
                        tmpReSpec.SpecCap = 0.0M;
                        tmpReSpec.SpecCount = 0;
                        tmpReSpec.SpecId = 0;
                        tmpReSpec.Comment = "标本删除";
                        if (subSpecManage.UpdateSubSpec(tmpReSpec) <= 0)
                        {
                            return -1;
                        }
                    }
                }
                #endregion

                if (chkName.Checked)
                {
                    int count = Convert.ToInt32(nudCount.Value);
                    GetSpecSourePlan();

                    //如果数量相等,仅更新条码
                    #region
                    if (count == oriCount)
                    {
                        if (!BarCodeCountValidate())
                        {
                            return -1;
                        }
                        int index = 0;
                        foreach (SubSpec spec in arrSubSpec)
                        {
                            if (UpdateBarCode(spec.SubSpecId, index) <= 0)
                            {
                                return -1;
                            }
                            if (spec.BoxId <= 0 && spec.SpecId > 0 && spec.StoreID > 0)
                            {
                                SubSpec tmpSpec = spec;
                                if (SubSpecReLocate(ref tmpSpec) == -1)
                                {
                                    return -1;
                                }
                            }
                            index++;
                        }
                    }
                    #endregion
                    //如果减少分装标本
                    #region
                    if (oriCount > count && count != 0)
                    {

                        DialogResult dialog = MessageBox.Show("确定要减少 " + chkName.Text + " 类型的标本？", "更新", MessageBoxButtons.YesNo);
                        if (dialog == DialogResult.No)
                        {
                            return -1;
                        }
                        if (!BarCodeCountValidate())
                        {
                            return -1;
                        }
                        int cutNum = oriCount - count;

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
                            //减少之后依然保留位置，只是标本容量，其它信息删除
                            //tmpCutSpec.BoxEndCol = 0;
                            //tmpCutSpec.BoxEndRow = 0;
                            //tmpCutSpec.BoxId = 0;
                            //tmpCutSpec.BoxStartCol = 0;
                            //tmpCutSpec.BoxStartRow = 0;
                            tmpCutSpec.SpecCap = 0.0M;
                            tmpCutSpec.SpecCount = 0;
                            tmpCutSpec.SubBarCode = "";
                            tmpCutSpec.StoreID = 0;
                            tmpCutSpec.SpecId = 0;
                            tmpCutSpec.Comment = "减少分装标本";
                            if (subSpecManage.UpdateSubSpec(tmpCutSpec) <= 0)
                            {
                                return -1;
                            }
                            arrSubSpec.RemoveAt(arrSubSpec.Count - 1);
                        }
                        //更新条码
                        int index = 0;
                        foreach (SubSpec spec in arrSubSpec)
                        {
                            if (UpdateBarCode(spec.SubSpecId, index) <= 0)
                            {
                                return -1;
                            }
                            if (spec.BoxId <= 0 && spec.SpecId > 0 && spec.StoreID > 0)
                            {
                                SubSpec tmpSpec = spec;
                                if (SubSpecReLocate(ref tmpSpec) == -1)
                                {
                                    return -1;
                                }
                            }
                            index++;
                        }
                    }
                    #endregion
                    //如果增加分装标本
                    #region
                    if (count > oriCount)
                    {
                        int addNum = count - oriCount;
                        if (!BarCodeCountValidate())
                        {
                            return -1;
                        }
                        SpecSourcePlan tmpPlan1 = specPlan.Clone();
                        tmpPlan1.Count = count - oriCount;


                        SpecInOper specInOper = new SpecInOper();
                        specInOper.DisTypeId = disTypeId.ToString();
                        specInOper.LoginPerson = loginPerson;
                        specInOper.SpecTypeId = tmpPlan1.SpecType.SpecTypeID.ToString();
                        specInOper.Trans = trans;
                        specInOper.SetTrans();

                        List<SubSpec> subList = new List<SubSpec>();
                        int subResult = specInOper.SaveSubSpec(tmpPlan1, ref subList);
                        if (subResult == -2)
                        {
                            MessageBox.Show("此标本类型无标本盒存放", "标本保存");
                            return -1;
                        }
                        if (subResult <= 0)
                        {
                            return -1;
                        }
                        this.UseFullBoxList = specInOper.UseFullBoxList;
                        //重新获取标本列表
                        arrSubSpec = subSpecManage.GetSubSpecByStoreId(specPlan.PlanID.ToString());
                        //更新条码
                        int index = 0;
                        foreach (SubSpec spec in arrSubSpec)
                        {
                            if (UpdateBarCode(spec.SubSpecId, index) <= 0)
                            {
                                return -1;
                            }
                            if (spec.BoxId <= 0 && spec.SpecId > 0 && spec.StoreID > 0)
                            {
                                SubSpec tmpSpec = spec;
                                if (SubSpecReLocate(ref tmpSpec) == -1)
                                {
                                    return -1;
                                }
                            }
                            index++;
                        }
                    }
                    #endregion

                }
                if (specPlanManage.UpdateSpecPlan(specPlan) <= 0)
                {
                    return -1;
                }
                //更新此次使用的序号
                if (needUpdateSeq)
                {
                    sequence = (Convert.ToInt32(sequence) + 1).ToString();
                    if (barCodeManage.UpdateBarCode(disTypeName, chkName.Text, sequence) <= 0)
                    {
                        return -1;
                    }
                    needUpdateSeq = false;
                }
            }
            #endregion

            return 1;

        }

        private void ucSpecSourceForBlood_Load(object sender, EventArgs e)
        {
            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            grpType.Text = chkName.Text;
        }

        private void chkAutoGen_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoGen.Checked)
            {
                AutoGenerateBarCode();
            }
        }

        private void txtBarCode_TextChanged(object sender, EventArgs e)
        {
            AddBarCode();
        }

        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtBarCode.Text.Length <= 6)
                {
                    txtBarCode.Text =  txtBarCode.Text.PadLeft(6, '0');
                }
                AddBarCode();
            }
        }

        private void nudCount_ValueChanged(object sender, EventArgs e)
        {

            if (inited)
            {
                int oldCount = lsbBarCode.Items.Count;
                int newCount = Convert.ToInt32(nudCount.Value);
                int diff = newCount - oldCount;
                //if (diff > 0 && chkAutoGen.Checked)
                //{
                //    this.AutoGenerateBarCode();
                //}
                if (diff < 0)
                {
                    diff = Math.Abs(diff);
                    for (int i = 0; i < diff; i++)
                    {
                        lsbBarCode.Items.RemoveAt(lsbBarCode.Items.Count-1);
                    }
                }
            }
        }

        private void lsbBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                lsbBarCode.Items.Remove(lsbBarCode.SelectedItem);
                barCodeCount--;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (barCodeList.Count <= 0)
            {
                if (lsbBarCode.SelectedItems.Count <= 0)
                {
                    MessageBox.Show("没有新的条码，如需打印请选择！");
                    return; 
                }
                foreach (string s in lsbBarCode.SelectedItems)
                {
                    barCodeList.Add(s);
                    numList.Add(s.Substring(s.Length - 1, 1));
                    disTypeList.Add(disTypeName);
                    specTypeList.Add(chkName.Text);
                    string sequence = "";
                    if (s.Length == 9)
                    {
                        sequence = s.Substring(1, 6);
                    }
                    if (s.Length == 11)
                    {
                        sequence = s.Substring(0, 6);
                    }
                    sequenceList.Add(sequence);
                }
            }
            if (PrintLabel.Print2DBarCode(barCodeList, sequenceList, specTypeList, disTypeList, numList) == -1)
            {
                MessageBox.Show("打印失败");
                return;
            }
            barCodeList = new List<string>();
            sequenceList = new List<string>();
            disTypeList = new List<string>();
            numList = new List<string>();
        }
    }
}
