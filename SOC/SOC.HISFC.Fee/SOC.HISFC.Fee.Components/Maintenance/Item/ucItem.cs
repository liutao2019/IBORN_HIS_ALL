using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;

namespace FS.SOC.HISFC.Fee.Components.Maintenance.Item
{
    /// <summary>
    /// [功能描述: 非药品基本信息维护控件]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// <修改记录>
    /// </修改记录>
    /// </summary>
    partial class ucItem : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucItem()
        {
            InitializeComponent();
            this.Load += new EventHandler(ucItem_Load);
            this.btnSave.Click+=new EventHandler(btnSave_Click);
            this.btnCancel.Click+=new EventHandler(btnCancel_Click);
            this.nbtBack.Click+=new EventHandler(nbtBack_Click);
            this.nbtNext.Click+=new EventHandler(nbtNext_Click);
            this.chbIsStop.CheckedChanged+=new EventHandler(chbIsStop_CheckedChanged);
            this.txtName.KeyDown += new KeyEventHandler(txtName_KeyDown);
            txtOtherName.TextChanged += new EventHandler(txtOtherName_TextChanged);
            this.btn_Add.Click += new EventHandler(btn_Add_Click);
            this.txtMaterialCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaterialCode_KeyDown);

            btSelectExecDept.Click += new EventHandler(btSelectExecDept_Click);

            this.cmbApplyClass.KeyPress += new KeyPressEventHandler(KeyPress);
            this.btSelectExecDept.KeyPress += new KeyPressEventHandler(KeyPress);
            cmbExecDeptOut.KeyPress += new KeyPressEventHandler(KeyPress);
            cmbExecDeptIn.KeyPress += new KeyPressEventHandler(KeyPress);
        }

        void KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (((Control)sender).Name == "cmbApplyClass")
                {
                    btSelectExecDept.Focus();
                }
                else if (((Control)sender).Name == "btSelectExecDept")
                {
                    cmbExecDeptOut.Focus();
                }
                else if (((Control)sender).Name == "cmbExecDeptOut")
                {
                    cmbExecDeptIn.Focus();
                }
                else if (((Control)sender).Name == "cmbExecDeptIn")
                {
                    cmbItemGrade.Focus();
                }
            }
        }

        public delegate void SaveItemHandler( FS.SOC.HISFC.Fee.Models.Undrug item);
        public delegate void GetNextItemHandler(int span);

        public event SaveItemHandler EndSave;
        public event GetNextItemHandler GetNextItem;

        /// <summary>
        /// 控件内操作的药品实体
        /// </summary>
        private  FS.SOC.HISFC.Fee.Models.Undrug item = null;

        #region 属性
        /// <summary>
        /// 当前非药品实体是否为可维护的医技
        /// </summary>
        private bool isMedTechnoloty = false;

        private bool isGroup = false;

        /// <summary>
        /// 是否是组套
        /// </summary>
        public bool IsGroup
        {
            get
            {
                return isGroup;
            }
            set
            {
                isGroup = value;
            }
        }


        /// <summary>
        /// 是否显示物资编码
        /// </summary>
        private bool isMaterialCodeShow = false;

        public bool IsMaterialCodeShow
        {
            get 
            {
                return isMaterialCodeShow;
            }
            set
            {
                isMaterialCodeShow = value;
            }
        
        
        }

        private bool isUserCodeAutoGenerate = false;

        /// <summary>
        /// 是否自动生成自定义码
        /// </summary>
        public bool IsUserCodeAutoGenerate
        {
            get
            {
                return isUserCodeAutoGenerate;
            }
            set
            {
                this.isUserCodeAutoGenerate = value;
            }
        }
        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public int Init()
        {
            this.cmbApplicabilityArea.AddItems(CommonController.CreateInstance().QueryConstant("APPLICABILITYAREA"));
            this.cmbApplyClass.AddItems(CommonController.CreateInstance().QueryConstant("ApplyBillClass"));
            this.cmbCheckPart.AddItems(CommonController.CreateInstance().QueryConstant("CHECKPART"));
            this.cmbSample.AddItems(CommonController.CreateInstance().QueryConstant("LABSAMPLE"));
            this.cmbFunctionClass.AddItems(CommonController.CreateInstance().QueryConstant("ITEMPRICETYPE"));
            this.cmbFeeType.AddItems(CommonController.CreateInstance().QueryConstant(FS.HISFC.Models.Base.EnumConstant.MINFEE));
            this.cmbSysClass.AddItems(FS.HISFC.Models.Base.SysClassEnumService.List());
            //this.cmbExecDepts.AddItems(CommonController.CreateInstance().QueryDepartment());
            this.cmbExecDeptOut.AddItems(CommonController.CreateInstance().QueryDepartment());
            this.cmbExecDeptIn.AddItems(CommonController.CreateInstance().QueryDepartment());
            this.cmbItemGrade.AddItems(CommonController.CreateInstance().QueryConstant("ITEMGRADE"));
            this.cmbCompany.AddItems(CommonController.CreateInstance().QueryConstant("PRODUCER"));

            //显示物资编码录入
            if (isMaterialCodeShow == false)
            {
                txtMaterialCode.Visible = false;
                lbMaterial.Visible = false;
            }
            else
            {
                txtMaterialCode.Visible = true;
                lbMaterial.Visible = true;
            }

            

            return 1;
        }

        FS.SOC.HISFC.Fee.BizLogic.Undrug itemManager = new FS.SOC.HISFC.Fee.BizLogic.Undrug();

        public void SetUserCode()
        {
            if (isUserCodeAutoGenerate)
            {
                string userCode = "Neu" + itemManager.GetSequence("Fee.Item.Undrug.GetUserCode");
                this.txtUserCode.Text = userCode;
            }
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <returns></returns>
        public int Clear()
        {
            foreach (System.Windows.Forms.Control c in this.tpNormal.Controls)
            {
                if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuGroupBox))
                {
                    foreach (System.Windows.Forms.Control crl in c.Controls)
                    {
                        if (crl.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuComboBox))
                        {
                            crl.Text = null;
                            crl.Tag = null;
                            continue;
                        }
                        if (crl.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuLabel) && crl.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox))
                        {
                            crl.Tag = "";
                            crl.Text = "";
                            continue;
                        }
                         if (crl.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox))
                        {
                            ((FS.FrameWork.WinForms.Controls.NeuCheckBox)crl).Checked = false;
                            continue;
                        }
                         if (crl.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuNumericTextBox))
                        {
                            crl.Text = "0";
                            continue;
                        }
                         if (crl.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuTextBox))
                         {
                             crl.Text = "";
                             continue;
                        }
                    }
                }
            }

            foreach (System.Windows.Forms.Control c in this.tpOther.Controls)
            {
                if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuGroupBox))
                {
                    foreach (System.Windows.Forms.Control crl in c.Controls)
                    {
                        if (crl.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuLabel) && crl.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox))
                        {
                            crl.Tag = "";
                            crl.Text = "";
                        }
                        else if (crl.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuNumericTextBox))
                        {
                            crl.Text = "0";
                        }
                    }
                }
            }
            this.tbItemCode.Text = "";
            this.cmbApplicabilityArea.Tag = "0";
            this.txtMaterialCode.Text = "";
            this.item = null;

            return 1;
        }

        /// <summary>
        /// 根据字符串获得科室名称
        /// </summary>
        /// <param name="strDepts"></param>
        /// <returns></returns>
        private string GetDeptName(string strDepts)
        {
            string[] depts = strDepts.Split('|');
            string strDeptName = "|";
            if (depts != null && depts.Length > 0)
            {
                foreach (string id in depts)
                {
                    strDeptName += SOC.HISFC.BizProcess.Cache.Common.GetDeptName(id);
                }
            }

            strDeptName = strDeptName.Substring(1, strDeptName.Length - 1);

            if (string.IsNullOrEmpty(strDeptName))
            {
                strDeptName = "全部";
            }
            return strDeptName;
        }

        /// <summary>
        /// 根据传入的Item实体信息 设置控件显示
        /// </summary>
        public void SetItem(FS.SOC.HISFC.Fee.Models.Undrug item, bool isUsed)
        {
            this.Clear();

            this.item = item;

            this.tbItemCode.Text = item.ID;//0
            this.txtName.Text = item.Name;//1
            this.tbItemRestrict.Text = item.ItemException;//45
            this.tbNotice.Text = item.Notice;//41
            this.tbItemArea.Text = item.ItemArea;//44
            this.cmbSysClass.Tag= item.SysClass.ID;//2

            #region 执行科室

            txtExecDeptShow.Tag = item.ExecDept;
            if (item.ExecDept == null || string.IsNullOrEmpty(item.ExecDept))
            {
                this.txtExecDept.Text = "";
                this.txtExecDeptShow.Text = "";
            }
            else if (item.ExecDept.Contains("ALL"))
            {
                this.txtExecDept.Text = "";
                this.txtExecDeptShow.Text = "全部";
            }
          
            else
            {
                string[] dept = item.ExecDept.Split('|');

                string strDeptName = "";
                for (int k = 0; k < dept.Length; k++)
                {
                    string deptName = CommonController.CreateInstance().GetDepartmentName(dept[k]);
                    if (!string.IsNullOrEmpty(deptName))
                    {
                        strDeptName += "|" + deptName;
                    }
                }
                if (string.IsNullOrEmpty(strDeptName))
                {
                    strDeptName = "全部";
                }
                else
                {
                    strDeptName = strDeptName.Substring(1);
                }

                txtExecDeptShow.Text = strDeptName;
            }

            cmbExecDeptOut.Tag = item.DefaultExecDeptForOut;
            cmbExecDeptIn.Tag = item.DefaultExecDeptForIn;

            #endregion

            this.cmbFeeType.Tag = item.MinFee.ID;//3----------
            this.chbIsStop.Checked = !FS.FrameWork.Function.NConvert.ToBoolean(item.ValidState);//16
            this.tbCheck.Text = item.CheckRequest;//40
            this.tbUnit.Text = item.PriceUnit;//10
            this.txtSpecs.Text = item.Specs;//17
            this.cmbApplyClass.Tag = item.CheckApplyDept;//42
            this.tbIllSort.Text = item.DiseaseType.ID;//36
            this.tbOprCode.Text = item.OperationInfo.ID;//21
            this.tbOprSort.Text = item.OperationType.ID;//22
            this.tbOprScale.Text = item.OperationScale.ID;//23
            this.tbAcademyName.Text = item.SpecialDept.ID;//37
            this.tbMachineNO.Text = item.MachineNO;//19

            this.txtUnitPrice.NumericValue = item.Price;//9
            this.tbChildPrice.NumericValue = item.ChildPrice;//27
            this.tbSpecialPrice.NumericValue = item.SpecialPrice;//28
            this.tbEmergePrice.NumericValue = item.FTRate.EMCRate;//11
            this.tbOtherPrice1.NumericValue = 0;//34
            this.txtPurchaseprice.NumericValue = item.DefPrice; ;//35

            this.txtUserCode.Text = item.UserCode;//4
            this.txtSpellCode.Text = item.SpellCode;//5
            this.txtWbCode.Text = item.WBCode;//6
            this.txtGbCode.Text = item.GBCode;//7
            this.txtInternationalCode.Text = item.NationCode;//8
            this.tbMedical.Text = item.MedicalRecord;//39
            this.tbMemo.Text = item.Memo;//25
            if (FS.HISFC.Models.Base.EnumSysClass.UC.ToString().Equals(item.SysClass.ID))
            {
                this.cmbCheckPart.Text = item.CheckBody;//
            }
            else if (FS.HISFC.Models.Base.EnumSysClass.UL.ToString().Equals(item.SysClass.ID))
            {
                this.cmbSample.Text = item.CheckBody;//
            }

            if (string.IsNullOrEmpty(item.ApplicabilityArea))
            {
                this.cmbApplicabilityArea.Tag = "0";
            }
            else
            {
                this.cmbApplicabilityArea.Tag = item.ApplicabilityArea;
            }
            
            this.chkProvince.Checked = FS.FrameWork.Function.NConvert.ToBoolean(item.SpecialFlag);
            this.chkTown.Checked = FS.FrameWork.Function.NConvert.ToBoolean(item.SpecialFlag1);
            this.chkSelf.Checked = FS.FrameWork.Function.NConvert.ToBoolean(item.SpecialFlag2);

            //确认标记
            this.ckOutpatientConfirm.Checked = false;
            this.chkConfirm.Checked = false;
            if (item.NeedConfirm == FS.HISFC.Models.Fee.Item.EnumNeedConfirm.Outpatient)
            {
                this.ckOutpatientConfirm.Checked = true;
            }
            else if (item.NeedConfirm == FS.HISFC.Models.Fee.Item.EnumNeedConfirm.Inpatient)
            {
                this.chkConfirm.Checked = true;
            }
            else if (item.NeedConfirm == FS.HISFC.Models.Fee.Item.EnumNeedConfirm.All)
            {
                this.ckOutpatientConfirm.Checked = true;
                this.chkConfirm.Checked = true;
            }
            else
            {
                this.chkConfirm.Checked = item.IsNeedConfirm/*终端确认*/;//15
            }

            this.chkPrecontract.Checked = item.IsNeedBespeak/*是否需要预约*/;//43
            this.chkFamilyPlane.Checked = item.IsFamilyPlanning/*计划生育标记*/;//12
            this.chkSpecialItem.Checked = FS.FrameWork.Function.NConvert.ToBoolean(item.SpecialFlag3);
            this.ckOwnFee.Checked = FS.FrameWork.Function.NConvert.ToBoolean(item.SpecialFlag4);
            /*中山一使用,是否强制出单*/ ;//33
            this.chkConsent.Checked = item.IsConsent/*知情同意书*/;//38
            this.chkCollate.Checked = item.IsCompareToMaterial/*对照*/;//24
            this.chkUnitFlag.Checked = FS.FrameWork.Function.NConvert.ToBoolean(item.UnitFlag);
            this.cmbItemGrade.Tag = item.Grade.ID;

            if (item.DeptList == "ALL")
            {
                this.txtKfDept.Text = "";
            }
            else
            {
                this.txtKfDept.Text = item.DeptList;
            }

            this.cmbFunctionClass.Tag = item.ItemPriceType;
            this.chkOrderPrint.Checked = FS.FrameWork.Function.NConvert.ToBoolean(item.IsOrderPrint);

            //新增别名 英文名
            this.txtOtherName.Text = item.NameCollection.OtherName;
            this.txtOtherSpellCode.Text = item.NameCollection.OtherSpell.SpellCode;
            this.txtOtherWBCode.Text = item.NameCollection.OtherSpell.WBCode;
            this.txtOtherCustomCode.Text = item.NameCollection.OtherSpell.UserCode;

            this.txtEnglishName.Text = item.NameCollection.EnglishName;
            this.txtEnglishOtherName.Text = item.NameCollection.EnglishOtherName;
            this.txtRegularName.Text = item.NameCollection.EnglishRegularName;

            this.isGroup=FS.FrameWork.Function.NConvert.ToBoolean(this.item.UnitFlag);
            this.txtRegisterCode.Text = item.RegisterCode;
            this.cmbCompany.Text = item.Producer;
            if (item.RegisterDate > DateTime.MinValue)
            {
                this.dtpRegisterDate.Value = DateTime.Now;
                this.dtpRegisterDate.Value = item.RegisterDate;
            }

            this.txtUserCode.Enabled = !isUsed;

            this.SetGroupEnable(this.isGroup);

            #region 根据非药品信息生成对应的医技类型
            ArrayList MTTypeList = new FS.HISFC.BizLogic.Manager.Constant().GetList("MTType");
            ArrayList mt = new ArrayList();
            int i = 0;
            int index = 0;
            foreach (FS.HISFC.Models.Base.Const c in MTTypeList)
            {
                if (c.Memo == item.MinFee.ID)
                {
                    mt.Add(c);
                    if (c.ID.Equals(item.MTType.ID))
                    {
                        index = i;
                    }
                    i++;
                }
            }
            //如果mt的数量大于0,则说明这个非药品实体是可维护的医技
            isMedTechnoloty = mt.Count > 0;
            //不可维护医技的话,那么就将该勾选框禁用,以免出错
            cbMtType.Enabled = isMedTechnoloty;

            if (isMedTechnoloty)
            {
                this.cmbMTType.AddItems(mt);
                cmbMTType.SelectedIndex = index;
                cbMtType.Checked = !string.IsNullOrEmpty(item.MTType.ID);
            }

            #endregion
        }

        /// <summary>
        /// 有效性检测
        /// </summary>
        /// <returns></returns>
        private bool CheckValid()
        {
            if (this.txtName.Text == "")
            {
                CommonController.CreateInstance().MessageBox(FS.FrameWork.Management.Language.Msg("项目名称不能为空!"), MessageBoxIcon.Error);
                this.txtName.Focus();
                return false;
            }
            if (this.cmbSysClass.Text == "")
            {
                CommonController.CreateInstance().MessageBox(FS.FrameWork.Management.Language.Msg("系统类别不能为空!"), MessageBoxIcon.Error);
                this.cmbSysClass.Focus();
                return false;
            }
            if (this.cmbFeeType.Text == "")
            {
                CommonController.CreateInstance().MessageBox(FS.FrameWork.Management.Language.Msg("费用类别不能为空!"), MessageBoxIcon.Error);
                this.cmbFeeType.Focus();
                return false;
            }

            //if (this.cmbFunctionClass.Text == "")
            //{
            //    CommonController.CreateInstance().MessageBox(FS.FrameWork.Management.Language.Msg("功能分类不能为空!"), MessageBoxIcon.Error);
            //    this.cmbFunctionClass.Focus();
            //    return false;
            //}
            //if (this.tbSample.Text == "")
            //{
            //    CommonController.CreateInstance().MessageBox(FS.FrameWork.Management.Language.Msg("检查部位或标本不能为空!"), MessageBoxIcon.Error);
            //    this.tbSample.Focus();
            //    return -1;
            //}
            //if (this.tbUnit.Text == "")
            //{
            //    CommonController.CreateInstance().MessageBox(FS.FrameWork.Management.Language.Msg("单位不能为空!"), MessageBoxIcon.Error);
            //    this.tbUnit.Focus();
            //    return -1;                
            //}
            //if (this.tbSpec.Text == "")
            //{
            //    CommonController.CreateInstance().MessageBox(FS.FrameWork.Management.Language.Msg("规格不能为空!"), MessageBoxIcon.Error);
            //    this.tbSpec.Focus();
            //    return -1;
            //}
            if (this.txtUserCode.Text == "")
            {
                CommonController.CreateInstance().MessageBox(FS.FrameWork.Management.Language.Msg("自定义码不能为空!"), MessageBoxIcon.Error);
                this.txtUserCode.Focus();
                return false;
            }
            if (this.txtSpellCode.Text == "")
            {
                CommonController.CreateInstance().MessageBox(FS.FrameWork.Management.Language.Msg("拼音码不能为空!"), MessageBoxIcon.Error);
                this.txtSpellCode.Focus();
                return false;
            }

            if (this.txtWbCode.Text == "")
            {
                CommonController.CreateInstance().MessageBox(FS.FrameWork.Management.Language.Msg("五笔码不能为空!"), MessageBoxIcon.Error);
                this.txtWbCode.Focus();
                return false;
            }

            //非药品物价项目的国家编码重复需要有提示功能 add by yerl 2013-09-12
            if (!string.IsNullOrEmpty(txtGbCode.Text))
            {
                FS.SOC.HISFC.Fee.BizLogic.Undrug undrugMrg = new FS.SOC.HISFC.Fee.BizLogic.Undrug();
                DataSet ds = new DataSet();
                string sql = string.Format("select t.* from fin_com_undruginfo t where t.gb_code='{0}'", txtGbCode.Text);
                string wheresql = string.Format(" and t.item_code != '{0}'", tbItemCode.Text);
                if (string.IsNullOrEmpty(tbItemCode.Text))
                    wheresql = "and t.item_code is not null";

                undrugMrg.ExecQuery(sql + wheresql, ref ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DialogResult dr = CommonController.CreateInstance().MessageBox(FS.FrameWork.Management.Language.Msg("国家编码已存在,是否继续操作?"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dr == DialogResult.Cancel)
                        return false;
                }
            }

            if (isUserCodeAutoGenerate)
            {
                 List<FS.SOC.HISFC.Fee.Models.Undrug> undrugList = itemManager.QueryCountByUserCode(this.txtUserCode.Text);

                //新增
                if (this.item==null && undrugList.Count > 0)
                {
                    CommonController.CreateInstance().MessageBox(FS.FrameWork.Management.Language.Msg("自定义码已经存在!"), MessageBoxIcon.Error);
                    this.txtUserCode.Focus();
                    return false;
                }

                //修改
                if (this.item != null && undrugList.Count > 0)
                {
                    foreach (FS.SOC.HISFC.Fee.Models.Undrug item in undrugList)
                    {
                        if (item.ID != this.item.ID)
                        {
                            CommonController.CreateInstance().MessageBox(FS.FrameWork.Management.Language.Msg("自定义码已经存在!"), MessageBoxIcon.Error);
                            this.txtUserCode.Focus();
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 获取基本信息
        /// </summary>
        private FS.SOC.HISFC.Fee.Models.Undrug GetItem()
        {

            if (this.item == null)
            {
                this.item = new FS.SOC.HISFC.Fee.Models.Undrug();
            }
            FS.SOC.HISFC.Fee.Models.Undrug item = this.item.Clone();
            item.ID = this.tbItemCode.Text;//0
            item.Name = this.txtName.Text;//1
            item.ValidState = FS.FrameWork.Function.NConvert.ToInt32(!this.chbIsStop.Checked).ToString();
            item.SysClass.ID = this.cmbSysClass.Tag.ToString();

            #region 执行科室


            if (this.cmbExecDeptOut.Tag != null
                && !string.IsNullOrEmpty(this.cmbExecDeptOut.Tag.ToString()))
            {
                item.DefaultExecDeptForOut = this.cmbExecDeptOut.Tag.ToString();
            }
            else
            {
                item.DefaultExecDeptForOut = null;
            }

            if (this.cmbExecDeptIn.Tag != null
                && !string.IsNullOrEmpty(this.cmbExecDeptIn.Tag.ToString()))
            {
                item.DefaultExecDeptForIn = this.cmbExecDeptIn.Tag.ToString();
            }
            else
            {
                item.DefaultExecDeptForIn = null;
            }


            //item.ExecDept = this.cmbExecDepts.Tag.ToString();

            if (txtExecDeptShow.Tag != null&& txtExecDeptShow.Tag != "")
            {
                item.ExecDept = txtExecDeptShow.Tag.ToString();
            }
            else
            {
                item.ExecDept = "ALL";
            }

            //默认执行科室要加载到执行科室列表中
            if (!item.ExecDept.Contains("ALL") && !string.IsNullOrEmpty(item.DefaultExecDeptForOut)
                &&!item.ExecDept.Contains(item.DefaultExecDeptForOut))
            {
                item.ExecDept += (string.IsNullOrEmpty(item.ExecDept) ? "" : "|") + item.DefaultExecDeptForOut;
            }

            if (!item.ExecDept.Contains("ALL") && !string.IsNullOrEmpty(item.DefaultExecDeptForIn)
                && !item.ExecDept.Contains(item.DefaultExecDeptForIn))
            {
                item.ExecDept += (string.IsNullOrEmpty(item.ExecDept) ? "" : "|") + item.DefaultExecDeptForIn;
            }

            if (item.ExecDept.Contains("ALL"))
            {
                item.ExecDept = string.Empty;
            }

            #endregion

            item.MinFee.ID = this.cmbFeeType.Tag.ToString();
            item.ItemException = this.tbItemRestrict.Text;//45
            item.Notice = this.tbNotice.Text;//41
            item.ItemArea = this.tbItemArea.Text;//44
            item.CheckRequest = this.tbCheck.Text;//40
            if (FS.HISFC.Models.Base.EnumSysClass.UC.ToString().Equals(this.cmbSysClass.Tag))
            {
                item.CheckBody = this.cmbCheckPart.Text;
            }
            else if (FS.HISFC.Models.Base.EnumSysClass.UL.ToString().Equals(this.cmbSysClass.Tag))
            {
                item.CheckBody = this.cmbSample.Text;
            }

            item.DefPrice = (decimal)this.txtPurchaseprice.NumericValue;//进价
            item.Price = (decimal)this.txtUnitPrice.NumericValue;//9
            item.ChildPrice = (decimal)this.tbChildPrice.NumericValue;//27
            item.SpecialPrice = (decimal)this.tbSpecialPrice.NumericValue;//28
            item.FTRate.EMCRate = (decimal)this.tbEmergePrice.NumericValue;//11
            item.PriceUnit = this.tbUnit.Text;//10
            if (string.IsNullOrEmpty(item.PriceUnit))
            {
                item.PriceUnit = "无";
            }
            item.Specs = this.txtSpecs.Text;//17
            if (item.Specs != null)
            {
                string specs = string.Empty;
                int count = 0;
                for (int i = 0; i < item.Specs.Length; i++)
                {
                    byte[] specsByte = System.Text.Encoding.Default.GetBytes(item.Specs[i].ToString());
                    count += specsByte.Length;
                    if (count <= 32)
                    {
                        specs += item.Specs[i];
                    }
                }
                item.Specs = specs;
            }

            if (this.cmbApplyClass.Tag != null)
            {
                item.CheckApplyDept = this.cmbApplyClass.Tag.ToString();//42
            }
            item.DiseaseType.ID = this.tbIllSort.Text;//36
            item.OperationInfo.ID = this.tbOprCode.Text;//21
            item.OperationType.ID = this.tbOprSort.Text;//22
            item.OperationScale.ID = this.tbOprScale.Text;//23
            item.SpecialDept.ID = this.tbAcademyName.Text;//37
            item.MachineNO = this.tbMachineNO.Text;//19

            //三种码,[2006/12/15, xuweizhe]改,可能有问题
            item.UserCode = this.txtUserCode.Text;//4
            item.SpellCode = this.txtSpellCode.Text;//5
            item.WBCode = this.txtWbCode.Text;//6

            item.GBCode = this.txtGbCode.Text;//7
            item.NationCode = this.txtInternationalCode.Text;//8
            item.MedicalRecord = this.tbMedical.Text;//39
            item.Memo = this.tbMemo.Text;//25
            if (cmbApplicabilityArea.Tag != null)
            {
                item.ApplicabilityArea = cmbApplicabilityArea.Tag.ToString();
            }
            item.SpecialFlag = FS.FrameWork.Function.NConvert.ToInt32(this.chkProvince.Checked).ToString();//29
            item.SpecialFlag1 = FS.FrameWork.Function.NConvert.ToInt32(this.chkTown.Checked).ToString();//30
            item.SpecialFlag2 = FS.FrameWork.Function.NConvert.ToInt32(this.chkSelf.Checked).ToString();

            item.IsNeedConfirm = false;
            if (this.ckOutpatientConfirm.Checked && this.chkConfirm.Checked)
            {
                item.NeedConfirm = FS.HISFC.Models.Fee.Item.EnumNeedConfirm.All;
                item.IsNeedConfirm = true;
            }
            else if (this.ckOutpatientConfirm.Checked)
            {
                item.NeedConfirm = FS.HISFC.Models.Fee.Item.EnumNeedConfirm.Outpatient;
            }
            else if (this.chkConfirm.Checked)
            {
                item.NeedConfirm = FS.HISFC.Models.Fee.Item.EnumNeedConfirm.Inpatient;
                item.IsNeedConfirm = true;
            }
            else
            {
                item.NeedConfirm = FS.HISFC.Models.Fee.Item.EnumNeedConfirm.None;
            }

            item.IsNeedBespeak = this.chkPrecontract.Checked /*是否需要预约*/;//43
            item.IsFamilyPlanning = this.chkFamilyPlane.Checked /*计划生育标记*/;//12

            item.SpecialFlag3 = FS.FrameWork.Function.NConvert.ToInt32(this.chkSpecialItem.Checked).ToString();
            item.SpecialFlag4 = FS.FrameWork.Function.NConvert.ToInt32(this.ckOwnFee.Checked).ToString(); /*中山一使用,是否强制出单*/ ;//33
            item.IsConsent = this.chkConsent.Checked /*知情同意书*/;//38
            item.IsCompareToMaterial = this.chkCollate.Checked /*对照*/;//24

            if (this.cmbItemGrade.SelectedItem != null)
            {
                item.Grade = this.cmbItemGrade.SelectedItem;
            }

            //单位标识(0,明细; 1,组套)[2007/01/01 ]
            item.UnitFlag = FS.FrameWork.Function.NConvert.ToInt32(this.chkUnitFlag.Checked).ToString();
            //操作员
            item.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;
            if (this.txtKfDept.Text == string.Empty)
            {
                item.DeptList = "ALL";
            }
            else
            {
                item.DeptList = this.txtKfDept.Text;
            }
            if (this.cmbFunctionClass.Tag != null)
            {
                item.ItemPriceType = this.cmbFunctionClass.Tag.ToString();
            }
            item.IsOrderPrint = FS.FrameWork.Function.NConvert.ToInt32(this.chkOrderPrint.Checked).ToString();

            //新增别名 英文名
            item.NameCollection.OtherName = this.txtOtherName.Text;
            item.NameCollection.OtherSpell.UserCode = this.txtOtherCustomCode.Text;
            item.NameCollection.OtherSpell.SpellCode = this.txtOtherSpellCode.Text;
            item.NameCollection.OtherSpell.WBCode = this.txtOtherWBCode.Text;

            item.NameCollection.EnglishOtherName = this.txtEnglishOtherName.Text;
            item.NameCollection.EnglishName = this.txtEnglishName.Text;
            item.NameCollection.EnglishRegularName = this.txtRegularName.Text;
            item.RegisterCode = this.txtRegisterCode.Text;
            item.RegisterDate = this.dtpRegisterDate.Value;
            item.Producer = this.cmbCompany.Text;

            #region 添加医技预约类型
            if (cbMtType.Checked)
            {
                FS.FrameWork.Models.NeuObject mtType = new FS.FrameWork.Models.NeuObject();
                if (cmbMTType.SelectedItem !=null)
                {
                    mtType.ID = cmbMTType.SelectedItem.ID;
                    mtType.Name = cmbMTType.SelectedItem.Name;
                }
                item.MTType = mtType;
            }
            else
            {
                item.MTType = new FS.FrameWork.Models.NeuObject();
            }
            #endregion
            return item;

        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            if (this.CheckValid())
            {
                 FS.SOC.HISFC.Fee.Models.Undrug item = this.GetItem();
               
                if (item != null)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    FS.SOC.HISFC.Fee.BizLogic.Undrug itemMgr = new FS.SOC.HISFC.Fee.BizLogic.Undrug();
                    itemMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    if (string.IsNullOrEmpty(item.ID))
                    {
                        if (itemMgr.Insert(item) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            CommonController.CreateInstance().MessageBox("保存失败，请向系统管理员联系并报告错误：" + itemMgr.Err, MessageBoxIcon.Error);
                            return;
                        }
                        if (InterfaceManager.GetISaveItem().SaveCommitting(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Insert, item) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            CommonController.CreateInstance().MessageBox("保存失败，请向系统管理员联系并报告错误：" + InterfaceManager.GetISaveItem().Err, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        if (itemMgr.Update(item) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            CommonController.CreateInstance().MessageBox("保存失败，请向系统管理员联系并报告错误：" + itemMgr.Err, MessageBoxIcon.Error);
                            return;
                        }
                        if (this.item.ValidState.Equals(item.ValidState))
                        {
                            if (InterfaceManager.GetISaveItem().SaveCommitting(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Update, item) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                CommonController.CreateInstance().MessageBox("保存失败，请向系统管理员联系并报告错误：" + InterfaceManager.GetISaveItem().Err, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            if (InterfaceManager.GetISaveItem().SaveCommitting(this.item.ValidState == "1" ? FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Valid : FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.InValid, item) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                CommonController.CreateInstance().MessageBox("保存失败，请向系统管理员联系并报告错误：" + InterfaceManager.GetISaveItem().Err, MessageBoxIcon.Error);
                            }
                        }
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();

                    #region 项目信息（停用，价格修改）时，复合项目明细跟着变化，复合项目价格也跟着变化
                    if (this.item.ValidState==null||!this.item.ValidState.Equals(item.ValidState) || !this.item.Price.Equals(item.Price))
                    {
                        string err = string.Empty;
                        int result = Function.UpdateForFinUndrugztinfo(item, this.item, ref err);
                        if (result == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            CommonController.CreateInstance().MessageBox("更新复合项目失败，请向系统管理员联系并报告错误：" + err, MessageBoxIcon.Error);
                            return;
                        }
                        //发送非药品信息消息
                        //获取复合项目非药品信息
                        if (result == 1)//存储过程执行了，才走下面
                        {
                            List<FS.SOC.HISFC.Fee.Models.Undrug> list = (new SOC.HISFC.Fee.BizLogic.Undrug()).GetUndrugList(item.ID);
                            if (list != null)
                            {
                                foreach (FS.SOC.HISFC.Fee.Models.Undrug objItem in list)
                                {
                                    if (InterfaceManager.GetISaveItem().SaveCommitting(this.item.ValidState == "1" ? FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Valid : FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.InValid, objItem) == -1)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        CommonController.CreateInstance().MessageBox("保存失败，请向系统管理员联系并报告错误：" + InterfaceManager.GetISaveItem().Err, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                CommonController.CreateInstance().MessageBox("获取复合项目信息出错，请向系统管理员联系并报告错误：" + err, MessageBoxIcon.Error);
                                return;
                            }
                            //获取复合项目明细非药品信息
                            List<FS.HISFC.Models.Fee.Item.UndrugComb> alUndrugComb = (new SOC.HISFC.Fee.BizLogic.Undrug()).GetUndrugCombList(item);
                            if (alUndrugComb != null)
                            {
                                if (alUndrugComb.Count > 0)
                                {
                                    if (InterfaceManager.GetISaveItemGroupDetail().SaveCommitting(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Update, new ArrayList(alUndrugComb)) == -1)
                                    {
                                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                        CommonController.CreateInstance().MessageBox(this, "保存失败，请与系统管理员联系并报告错误：" + err, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                CommonController.CreateInstance().MessageBox("获取复合项目明细信息出错，请向系统管理员联系并报告错误：" + err, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    #endregion

                    if (this.EndSave != null)
                    {
                        this.EndSave(item);
                    }

                    CommonController.CreateInstance().MessageBox("保存成功！", MessageBoxIcon.Information);
                    if (this.continueCheckBox.Checked)
                    {
                        this.Clear();
                    }
                    else
                    {
                        if (this.FindForm() != null)
                        {
                            this.FindForm().Close();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 设置可用性
        /// </summary>
        private void SetGroupEnable(bool isEnable)
        {
            this.txtGbCode.Enabled = isEnable;
            this.txtInternationalCode.Enabled = isEnable;
            this.txtSpecs.Enabled = isEnable;
            this.txtUnitPrice.Enabled = isEnable;
            this.tbChildPrice.Enabled = isEnable;
            //this.tbEmergePrice.Enabled = isEnable;
            this.tbIllSort.Enabled = isEnable;
            this.tbUnit.Enabled = isEnable;
            this.tbSpecialPrice.Enabled = isEnable;
            //this.chkUnitFlag.Enabled = isEnable;
            this.chkUnitFlag.Checked = isGroup;
        }

        #endregion

        #region 事件
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.FindForm() != null)
            {
                this.FindForm().Close();
            }
        }

        private void ucItem_Load(object sender, EventArgs e)
        {
            this.neuTabControl1.SelectedTab = this.tpNormal;

            this.SetGroupEnable(!this.isGroup);
            this.txtName.Focus();
            this.txtName.SelectAll();
        }

        void nbtNext_Click(object sender, EventArgs e)
        {
            if (this.GetNextItem != null)
            {
                this.GetNextItem(1);
            }
        }

        void nbtBack_Click(object sender, EventArgs e)
        {
            if (this.GetNextItem != null)
            {
                this.GetNextItem(-1);
            }
        }

        void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            FS.HISFC.Models.Base.Spell spell = Function.GetSpellCode(this.txtName.Text);
            this.txtSpellCode.Text = spell.SpellCode;
            this.txtWbCode.Text = spell.WBCode;
            if (this.txtSpellCode.Text.Length > 8)
            {
                this.txtSpellCode.Text = this.txtSpellCode.Text.Substring(0, 8);
            }
            if (this.txtWbCode.Text.Length > 8)
            {
                this.txtWbCode.Text = this.txtWbCode.Text.Substring(0, 8);
            }
            #region 增加笔画码
            string charOrderString = FS.FrameWork.Function.NConvert.ToCharSortCode(this.txtName.Text);
            if (charOrderString.Length > 20)
            {
                charOrderString = charOrderString.Substring(0, 20);
            }
            this.txtWbCode.Text = this.txtWbCode.Text + charOrderString;
            #endregion


        }

        void txtOtherName_TextChanged(object sender, EventArgs e)
        {
            FS.HISFC.Models.Base.Spell spell = Function.GetSpellCode(this.txtOtherName.Text);
            this.txtOtherSpellCode.Text = spell.SpellCode;
            this.txtOtherWBCode.Text = spell.WBCode;
        }

        void chbIsStop_CheckedChanged(object sender, EventArgs e)
        {
            this.txtStopReason.Visible = this.chbIsStop.Checked;
            this.lblStopReason.Visible = this.chbIsStop.Checked;
        }

        void btn_Add_Click(object sender, EventArgs e)
        {
            FrmSetDeptForItem frm = new FrmSetDeptForItem();
            frm.DeptStr = this.txtKfDept.Text;
            frm.ShowDialog();
            if (frm.Rs == DialogResult.OK)
            {
                string chooseDeptList = "";
                chooseDeptList = frm.ChooseDeptList;
                if (chooseDeptList.Length <= 0)
                {
                    this.txtKfDept.Text = "";
                    return;
                }
                this.txtKfDept.Text = chooseDeptList;
            }
        }

        void btSelectExecDept_Click(object sender, EventArgs e)
        {
            FrmSetDeptForItem frm = new FrmSetDeptForItem();
            if (txtExecDeptShow.Tag != null)
            {
                frm.DeptStr = this.txtExecDeptShow.Tag.ToString();
            }

            frm.ShowDialog();
            if (frm.Rs == DialogResult.OK)
            {
                string chooseDeptList = "";
                chooseDeptList = frm.ChooseDeptList;
                if (chooseDeptList.Length <= 0)
                {
                    this.txtExecDept.Text = "";
                    return;
                }
                this.txtExecDeptShow.Tag = chooseDeptList;                
                txtExecDeptShow.Text = frm.ChooseDeptListName;
            }
        }

        #endregion

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.txtName.Focused)
                {
                    this.txtName_KeyDown(null, null);
                }
                if (this.txtMaterialCode.Focused)
                {
                    this.txtMaterialCode_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                }

                if (this.tbMemo.Focused==false)
                {
                    SendKeys.Send("{TAB}");
                    return true;
                }

            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.S.GetHashCode())
            {
                if (this.btnSave.Visible && this.btnSave.Enabled)
                    this.btnSave_Click(null, null);
                return true;
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.C.GetHashCode())
            {
                if (this.btnCancel.Visible && this.btnCancel.Enabled)
                    this.btnCancel_Click(null, null);
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                if (this.btnCancel.Visible && this.btnCancel.Enabled)
                    this.btnCancel_Click(null, null);
                return true;
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.Up.GetHashCode())
            {
                if (this.GetNextItem != null)
                {
                    this.GetNextItem(-1);
                }

            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.Down.GetHashCode())
            {
                if (this.GetNextItem != null)
                {
                    this.GetNextItem(1);
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        private void txtMaterialCode_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (txtMaterialCode.Text.Trim() == "")
                {
                    MessageBox.Show("请输入物资编码,再回车检索物资信息");
                }

                FS.SOC.HISFC.Fee.Models.MatBase material = new FS.SOC.HISFC.Fee.Models.MatBase();
                FS.SOC.HISFC.Fee.BizLogic.Material materialMgr = new FS.SOC.HISFC.Fee.BizLogic.Material();
                material = materialMgr.GetBaseInfoByID(txtMaterialCode.Text.Trim());

                if (material == null)
                {
                    MessageBox.Show("未找到此物资编码信息,请核对物资编码！");
                }
                else
                {
                   // txtName.Text = material.Name;
                    txtOtherName.Text = material.Name.Trim();
                    txtOtherCustomCode.Text = material.ID; //物资编码
                    txtUnitPrice.Text = material.Price.ToString();
                    txtSpecs.Text = material.Specs;
                    tbUnit.Text = material.PackUnit;
                    tbMemo.Text = material.RegisterNo + Environment.NewLine + material.Specs + Environment.NewLine;
                    tbNotice.Text = material.Mader;//品牌
                    txtName.Text = material.ApproveInfo.Trim();

                }

            }

        }

        //计算进价
        private void txtPurchaseprice_Leave(object sender, EventArgs e)
        {
            if (FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtPurchaseprice.Text), 2) < 1000 || FS.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtPurchaseprice.Text), 2) == 1000)
            {
                this.txtUnitPrice.Text = FS.FrameWork.Public.String.FormatNumberReturnString(decimal.Parse(this.txtPurchaseprice.Text) * FS.FrameWork.Function.NConvert.ToDecimal(0.1) + decimal.Parse(txtPurchaseprice.Text), 2);
            }
            else
            {
                this.txtUnitPrice.Text = ((1000 * FS.FrameWork.Function.NConvert.ToDecimal(0.1)) + (decimal.Parse(this.txtPurchaseprice.Text) - 1000) * FS.FrameWork.Function.NConvert.ToDecimal(0.08) + decimal.Parse(this.txtPurchaseprice.Text)).ToString();
            }
            if ((1000 * FS.FrameWork.Function.NConvert.ToDecimal(0.1) + (decimal.Parse(this.txtPurchaseprice.Text) - 1000) * FS.FrameWork.Function.NConvert.ToDecimal(0.08) > 800))
            {
                this.txtUnitPrice.Text = (decimal.Parse(this.txtPurchaseprice.Text) + 800).ToString();
            }
        }

        private void cbMtType_CheckedChanged(object sender, EventArgs e)
        {
            this.cmbMTType.Enabled = cbMtType.Checked;
        }

    }
}
