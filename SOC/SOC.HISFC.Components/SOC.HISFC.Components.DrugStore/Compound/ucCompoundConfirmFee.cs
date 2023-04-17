using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.SOC.HISFC.Components.DrugStore.Compound
{
    public partial class ucCompoundConfirmFee : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCompoundConfirmFee()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 药品业务类
        /// </summary>
        FS.SOC.HISFC.BizLogic.Pharmacy.Compound itemManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Compound();

        /// <summary>
        /// 
        /// </summary>
        FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.ICompoundFee feeObj = null;

        /// <summary>
        /// 存储收费列表
        /// </summary>
        System.Collections.Hashtable hsFeeList = new Hashtable();
  
        #endregion

        #region 属性

        #endregion

        #region 方法

        /// <summary>
        /// 初始化界面
        /// </summary>
        private void Init()
        {
            DateTime systime = FS.FrameWork.Function.NConvert.ToDateTime(this.itemManager.GetSysDateTime());

            this.dtStart.Text = systime.ToString("yyyy-MM-dd 00:00:00");
            this.dtEnd.Text = systime.ToString("yyyy-MM-dd 23:59:59");

            ///调用收费接口
            feeObj = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.ICompoundFee)) as FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.ICompoundFee;

            this.txtGroup.Focus();
        }

        /// <summary>
        /// 查询一段时间内的批次信息
        /// </summary>
        private ArrayList QueryCompoundGroup()
        {

            FS.FrameWork.Models.NeuObject obj = ((FS.HISFC.Models.Base.Employee)itemManager.Operator).Dept;
            string[] parm;

            if (this.ckDrugBill.Checked)
            {
                parm = new string[] { "A", "A", "A", "A", this.txtDrugedBill.Text.Trim() };

            }
            else
            {
                
                parm = new string[] { /*this.cmbDept.Tag.ToString()*/"A", obj.ID, this.dtStart.Text, this.dtEnd.Text, "A" };
            }

            ArrayList alComp = this.itemManager.QueryApplyOutListForJPConfirm(parm);

            return alComp;
        }

        /// <summary>
        /// 添加批次信息
        /// </summary>
        private void AddDateToComSelect()
        {
            ArrayList alComSelect = this.QueryCompoundGroup();

            this.fpCompoundSelect_Sheet1.Rows.Count = 0;

            if (alComSelect == null || alComSelect.Count == 0)
            {
                return;
            }

            for (int i = 0; i < alComSelect.Count; i++)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut obj = alComSelect[i] as FS.HISFC.Models.Pharmacy.ApplyOut;

                this.fpCompoundSelect_Sheet1.RowCount++;

                this.fpCompoundSelect_Sheet1.Cells[i, 0].Text = obj.CompoundGroup;//批次号
                this.fpCompoundSelect_Sheet1.Cells[i, 1].Text = obj.Item.Name;//药品名称
                this.fpCompoundSelect_Sheet1.Cells[i, 2].Text = obj.Operation.ApproveQty.ToString() + obj.Item.MinUnit;//数量
                this.fpCompoundSelect_Sheet1.Cells[i, 3].Text = obj.DoseOnce.ToString() + obj.Item.DoseUnit;//用量
                this.fpCompoundSelect_Sheet1.Cells[i, 4].Text = obj.UseTime.ToString();//用药时间

                this.fpCompoundSelect_Sheet1.Rows[i].Tag = obj;
            }


        }

        /// <summary>
        /// 根据批次号移除查询列表中的批次信息
        /// </summary>
        /// <param name="groupCode">批次号</param>
        /// <returns></returns>
        private int RemoveGroup(string groupCode)
        {
            ArrayList alApply = new ArrayList();

            int index = 0;
            int count = 0;
            for (int i = 0; i < this.fpCompoundSelect_Sheet1.RowCount; i++)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut info = this.fpCompoundSelect_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;

                if (info.CompoundGroup == groupCode)
                {
                    alApply.Add(info);
                    if (count == 0)
                    {
                        index = i;
                    }
                    count++;
                }
            }
            this.fpCompoundSelect_Sheet1.Rows.Remove(index, count);

            if (alApply.Count == 0)
            {
                return 0;
            }

            if (this.AddDateToConfirm(alApply) == -1)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 根据移除的申请信息，添加到确认列表中，并完成添加费用信息功能
        /// </summary>
        /// <param name="alAdd">确认信息</param>
        /// <returns></returns>
        private int AddDateToConfirm( ArrayList alAdd)
        {
            ///添加药品信息
            ArrayList feelist = new ArrayList();
            if (feeObj == null)
            {
                 AddConfirmInfo(alAdd);
                //MessageBox.Show("无收费接口，无法获取费用信息");
            }
            else
            {
                feeObj.GetCompoundFeeList(alAdd, ref feelist);

                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = alAdd[0] as FS.HISFC.Models.Pharmacy.ApplyOut;

                if (feelist != null && feelist.Count > 0)
                {

                    ///添加收费信息
                    this.AddConfirmInfo(alAdd, feelist);

                    if (!hsFeeList.ContainsKey(applyOut.CompoundGroup))
                    {
                        hsFeeList.Add(applyOut.CompoundGroup, feelist);
                    }
                }
                else
                {
                    ///添加收费信息
                    this.AddConfirmInfo(alAdd);
                }
            }

            return 1;
        }

        /// <summary>
        /// 向确认列表中添加确认信息
        /// </summary>
        /// <param name="alApply">药品申请信息</param>
        /// <returns></returns>
        private void AddConfirmInfo(ArrayList alApply)
        {
            for (int i = 0; i < alApply.Count; i++)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut info = alApply[i] as FS.HISFC.Models.Pharmacy.ApplyOut;

                this.sheetView1.Rows.Count++;

                this.sheetView1.Cells[i, 0].Text = info.CompoundGroup;
                this.sheetView1.Cells[i, 1].Text = info.Item.Name;
                this.sheetView1.Cells[i, 1].Tag = info;
                this.sheetView1.Cells[i, 2].Text = info.Operation.ApproveQty.ToString() + info.Item.MinUnit;//数量
                this.sheetView1.Cells[i, 3].Text = info.DoseOnce.ToString() + info.Item.DoseUnit;//用量
                this.sheetView1.Cells[i, 4].Text = info.UseTime.ToString();//用药时间
            }
        }

        /// <summary>
        /// 向确认列表中添加确认信息
        /// </summary>
        /// <param name="alApply">药品申请信息</param>
        /// <param name="alFee">费用信息</param>
        /// <returns></returns>
        private void AddConfirmInfo(ArrayList alApply, ArrayList alFee)
        {
            int alCount = alApply.Count > alFee.Count ? alApply.Count : alFee.Count;

            List<string> checkList = new List<string>();
            checkList = feeObj.SetFeeState(alApply);
            string groupCode = "";
            System.Collections.Hashtable hsCheck = new Hashtable();

            for (int ii = 0; ii < checkList.Count; ii++)
            {
                groupCode = checkList[ii];
                if (!hsCheck.ContainsKey(groupCode))
                {
                    hsCheck.Add(groupCode, groupCode);
                }
            }

            for (int i = 0; i < alCount; i++)
            {
                this.sheetView1.Rows.Count++;

                if (alApply.Count > i)
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut info = alApply[i] as FS.HISFC.Models.Pharmacy.ApplyOut;
                    this.sheetView1.Cells[i, 0].Text = info.CompoundGroup;
                    this.sheetView1.Cells[i, 1].Text = info.Item.Name;
                    this.sheetView1.Cells[i, 1].Tag = info;
                    this.sheetView1.Cells[i, 2].Text = info.Operation.ApproveQty.ToString() + info.Item.MinUnit;//数量
                    this.sheetView1.Cells[i, 3].Text = info.DoseOnce.ToString() + info.Item.DoseUnit;//用量
                    this.sheetView1.Cells[i, 4].Text = info.UseTime.ToString();//用药时间

                }
                if (alFee.Count > i)
                {
                    FS.HISFC.Models.Base.Item item = alFee[i] as FS.HISFC.Models.Base.Item;

                    //this.sheetView1.Cells[i, 0].Text = item.AccountFee.User09;//批次号
                    this.sheetView1.Cells[i, 5].Text = item.Name;
                    this.sheetView1.Cells[i, 5].Tag = item;
                    this.sheetView1.Cells[i, 6].Text = item.Price.ToString();
                    this.sheetView1.Cells[i, 7].Text = item.Qty.ToString();
                }

                this.sheetView1.Cells[i, 8].Value = true;
                //if (hsCheck.ContainsKey(this.sheetView1.Cells[i, 0].Text))
                //{
                //    this.sheetView1.Cells[i, 8].Value = true;
                //}
                //else
                //{
                //    this.sheetView1.Cells[i, 8].Value = false;
                //}

            }

        }

        private int Save()
        {
            int parm = 0;
            DateTime sysTime = itemManager.GetDateTimeFromSysDateTime();

            if (this.sheetView1.Rows.Count == 0)
            {
                return 0;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("正在保存,请稍候..."));
            Application.DoEvents();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            ///更新发药申请中的确认标识
            ArrayList alCheck = new ArrayList();
            alCheck = this.GetCheckApply();
            FS.HISFC.Models.Base.OperEnvironment compoundOper = new FS.HISFC.Models.Base.OperEnvironment();
            compoundOper.OperTime = sysTime;
            compoundOper.ID = itemManager.Operator.ID;

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alCheck)
            {
                if (itemManager.UpdateCompoundApplyOut(info, compoundOper, true) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    MessageBox.Show("更新配置确认信息发生错误" + itemManager.Err);
                    return -1;
                }
            }

            
            ///收取配置费
            ///存储配置费与批次信息对照关系
            ArrayList feelist = new ArrayList();
            feelist = this.GetCheckFee();
            if (feelist != null && feelist.Count > 0)
            {
                if (feeObj != null)
                {
                    parm = feeObj.SaveFee(feelist, null, FS.FrameWork.Management.PublicTrans.Trans);
                    if (parm == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("附材项目收费出错" + itemManager.Err);

                        return -1;
                    }
                }  

            }

            FS.FrameWork.Management.PublicTrans.Commit();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show(Language.Msg("保存成功"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.AddDateToComSelect();

            this.sheetView1.Rows.Count = 0;

            this.txtGroup.Focus();

            return 1;
        }

        /// <summary>
        /// 获取核对申请列表
        /// </summary>
        /// <returns></returns>
        private ArrayList GetCheckApply()
        {
            ArrayList alApply = new ArrayList();
            for (int i = 0; i < this.sheetView1.Rows.Count; i++)
            {
                if (this.sheetView1.Cells[i, 1].Text == "")
                {
                    continue;
                }
                else
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut info = this.sheetView1.Cells[i, 1].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;

                    alApply.Add(info);
                }
            }

            return alApply;
        }

        /// <summary>
        /// 获取收费列表
        /// </summary>
        /// <returns></returns>
        private ArrayList GetCheckFee()
        {
            ArrayList alFee = new ArrayList();
            for (int i = 0; i < this.sheetView1.Rows.Count; i++)
            {
                //没有取到收费信息则看下一行
                if (this.sheetView1.Cells[i, 5].Text == "")
                {
                    continue;
                }
                //若为选择收费框，则不进行收费
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.sheetView1.Cells[i, 8].Value) == false)
                {
                    continue;
                }

                FS.HISFC.Models.Base.Item fee = this.sheetView1.Cells[i, 5].Tag as FS.HISFC.Models.Base.Item;

                alFee.Add(fee);
            }

            return alFee;
        }

        /// <summary>
        /// 根据接口，设置是否对核对项目进行收费
        /// </summary>
        /// <param name="alApply"></param>
        private void SetCheckState(ArrayList alApply)
        {

        }


        #endregion

        #region 事件

        protected override void OnLoad(EventArgs e)
        {
            this.Init();

            base.OnLoad(e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.AddDateToComSelect();

            this.sheetView1.Rows.Count = 0;

            this.txtGroup.Focus();

            return base.OnQuery(sender, neuObject);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            return this.Save();
        }

        private void txtGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            ///检索左侧列表中符合的数据从左侧列表移除
            ///将左侧列表移除的数据添加到右侧列表
            ///根据查询的到的数据传递给收取配置费接口，返回收费列表
            ///将收费列表显示在右侧界面

            string groupCode = this.txtGroup.Text;
            if (groupCode == "")
            {
                return;
            }

            this.RemoveGroup(groupCode);

            this.txtGroup.Focus();
        }

        private void ckDrugBill_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckDrugBill.Checked)
            {
                this.txtDrugedBill.Enabled = true;
            }
            else
            {
                this.txtDrugedBill.Enabled = false;
            }

        }

        #endregion

    }
}
