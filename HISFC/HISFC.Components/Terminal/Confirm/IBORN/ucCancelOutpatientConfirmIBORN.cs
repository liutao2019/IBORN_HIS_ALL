﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.BizProcess.Integrate.Terminal;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Pharmacy;
using FS.HISFC.Models.RADT;
using FS.HISFC.Models.Registration;
using FS.HISFC.Models.Terminal;
using FS.FrameWork.WinForms.Forms;
using FS.FrameWork.Management;
using FS.HISFC.Components.Terminal.Confirm;
namespace FS.HISFC.Components.Terminal.Confirm.IBORN
{
    /// <summary>
    /// ucCancelOutpatientConfirm <br></br>
    /// [功能描述: 门诊终端确认取消]<br></br>
    /// [创 建 者: 喻赟]<br></br>
    /// [创建时间: 2008-07-11]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucCancelOutpatientConfirmIBORN : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCancelOutpatientConfirmIBORN()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 患者实体

        /// </summary>
        private FS.HISFC.Models.Registration.Register register = new Register();


        // 业务层
        private FS.HISFC.BizProcess.Integrate.Terminal.Confirm confirmIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Confirm();


        FS.HISFC.BizProcess.Integrate.Manager managerIntergrate = new FS.HISFC.BizProcess.Integrate.Manager();


        FS.HISFC.BizProcess.Integrate.PhysicalExamination.ExamiManager examMgrIntegrate = new FS.HISFC.BizProcess.Integrate.PhysicalExamination.ExamiManager();

        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// 工具栏服务
        /// </summary>
        FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new ToolBarService();

        /// <summary>
        /// 刷新事件
        /// </summary>
        protected event System.EventHandler RefreshEvent;
        /// <summary>
        /// 删除事件
        /// </summary>
        protected event System.EventHandler DeleteEvent;

        #endregion
        
        #region 属性
        /// <summary>
        /// 患者信息
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FS.HISFC.Models.Registration.Register Register
        {
            get
            {
                return this.register;
            }
            set
            {
                this.register = value;
            }
        }
        #endregion

        #region 函数

        /// <summary>
        /// 初始化UC
        /// </summary>
        private void InitUC()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请等待，正在进行初始化...");
            Application.DoEvents();

            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            //住院号不可见，因为用不上
            this.ucPatientInformation.ucQueryInpatientNo1.Visible = false;
            // 患者检索事件
            this.ucPatientInformation.KeyDownInQureyCode += new ucPatientInformation.MyDelegate(ucPatientInformation_KeyDownInQureyCode);
            //确认流水号不可见
            this.neuSpread1_Sheet1.Columns[9].Visible = false;
            this.neuSpread2_Sheet1.Columns[9].Visible = false;

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }        

        private void QueryConfirmInfo(string cardNo)
        {
            // 操作结果
            FS.HISFC.BizProcess.Integrate.Terminal.Result result = new Result();

            List<HISFC.Models.Terminal.TerminalApply> apply = new List<TerminalApply>();

            string deptID = "";
            deptID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;

            result = confirmIntegrate.QueryConfirmInfoByClinicNo(cardNo, deptID, ref apply);
            
            if (result == FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
            {
                if (apply != null)
                {
                    //MessageBox.Show("查询终端确认明细成功。");
                    //将查询得到的数据插入sheet1
                    this.InsertSheetData1(apply);
                }
                else
                {
                    MessageBox.Show("该患者没有终端确认明细。");
                }
            }
            else
            {
                MessageBox.Show("查找患者挂号信息失败!");
                this.ucPatientInformation.Focus();
            }
        }

        private void InsertSheetData1(List<TerminalApply> apply)
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
            }
            if (this.neuSpread2_Sheet1.RowCount > 0)
            {
                this.neuSpread2_Sheet1.RemoveRows(0, this.neuSpread2_Sheet1.RowCount);
            }

            int rowIndex = 0;
            /*
            obj.Item.ID = this.Reader[0].ToString();
            obj.Item.Name = this.Reader[1].ToString();
            obj.Item.ConfirmedQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2].ToString());
            obj.ConfirmOperEnvironment.ID = this.Reader[3].ToString();
            obj.ConfirmOperEnvironment.Dept.ID = this.Reader[4].ToString();
            obj.Order.ID = this.Reader[5].ToString();
            obj.Machine.ID = this.Reader[6].ToString();
            obj.ExecOper.ID = this.Reader[7].ToString();
            obj.ConfirmOperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8].ToString());
            obj.ID = this.Reader[9].ToString();
             * obj.User02 = this.Reader[10].ToString();
             */
            foreach (TerminalApply terApply in apply)
            {
                this.neuSpread1_Sheet1.Rows.Add(rowIndex, 1);
                neuSpread1_Sheet1.Cells[rowIndex, 0].Text = terApply.Item.ID;//项目编号0
                neuSpread1_Sheet1.Cells[rowIndex, 1].Text = terApply.Item.Name;//项目名1
                neuSpread1_Sheet1.Cells[rowIndex, 2].Text = terApply.Item.ConfirmedQty.ToString();//已确认数量2
                //确认人
                FS.HISFC.Models.Base.Employee oper = new Employee();
                oper = managerIntergrate.GetEmployeeInfo(terApply.ConfirmOperEnvironment.ID);
                neuSpread1_Sheet1.Cells[rowIndex, 3].Tag = oper;
                neuSpread1_Sheet1.Cells[rowIndex, 3].Text = oper.Name;//确认人3
                //确认科室
                FS.HISFC.Models.Base.Department dept = new Department();
                dept = managerIntergrate.GetDepartment(terApply.ConfirmOperEnvironment.Dept.ID);
                neuSpread1_Sheet1.Cells[rowIndex, 4].Tag = dept;
                neuSpread1_Sheet1.Cells[rowIndex, 4].Text = dept.Name;//确认科室4

                neuSpread1_Sheet1.Cells[rowIndex, 5].Text = terApply.Order.ID;//医嘱号
                neuSpread1_Sheet1.Cells[rowIndex, 6].Text = terApply.Machine.ID;//执行设备号

                oper = managerIntergrate.GetEmployeeInfo(terApply.ExecOper.ID);
                if (oper != null)
                {
                    neuSpread1_Sheet1.Cells[rowIndex, 7].Tag = oper;
                    neuSpread1_Sheet1.Cells[rowIndex, 7].Text = oper.Name;
                }//执行人
                neuSpread1_Sheet1.Cells[rowIndex, 8].Text = terApply.ConfirmOperEnvironment.OperTime.ToString();//执行时间
                neuSpread1_Sheet1.Cells[rowIndex, 9].Text = terApply.ID;
                neuSpread1_Sheet1.Rows[rowIndex].Tag = terApply;
                rowIndex++;
            }
            //this.neuSpread1_Sheet1.Columns[0, this.neuSpread1_Sheet1.ColumnCount - 1].Locked = true;

        }

        private int DeleteSelectedRow()
        {
            //temp为了解决一个诡异的问题
            string temp = string.Empty;
            if (this.neuSpread2_Sheet1.ActiveRowIndex < this.neuSpread2_Sheet1.RowCount - 1)
            {
                temp = this.neuSpread2_Sheet1.Cells[this.neuSpread2_Sheet1.ActiveRowIndex + 1, this.neuSpread2_Sheet1.ActiveColumnIndex].Text;
            }
            else
            {
                if (this.neuSpread2_Sheet1.RowCount > 1)
                {
                    temp = this.neuSpread2_Sheet1.Cells[this.neuSpread2_Sheet1.ActiveRowIndex - 1, this.neuSpread2_Sheet1.ActiveColumnIndex].Text;
                }
            }
            try
            {
                this.neuSpread2_Sheet1.RemoveRows(this.neuSpread2_Sheet1.ActiveRowIndex, 1);
                if (this.neuSpread2_Sheet1.RowCount > 0)
                {
                    this.neuSpread2_Sheet1.ActiveCell.Text = temp;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 清空界面
        /// </summary>
        private void Clear()
        {
            this.ucPatientInformation.Register = null;
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
            }
            if (this.neuSpread2_Sheet1.RowCount > 0)
            {
                this.neuSpread2_Sheet1.RemoveRows(0, this.neuSpread2_Sheet1.RowCount);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            #region 变量和trans
            // 操作结果
            FS.HISFC.BizProcess.Integrate.Terminal.Result result = new Result();
            // 业务层
            FS.HISFC.BizProcess.Integrate.Terminal.Confirm confirmIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Confirm();
            // 申请单实体
            FS.HISFC.Models.Terminal.TerminalApply apply = new TerminalApply();
            //是否有预约
            bool hasApply = false;

            //{04B57368-8DE0-4e46-8F27-1374F37C36AE}无需保存
            if (this.neuSpread2_Sheet1.RowCount <= 0)
            {
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.confirmIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            #endregion

            //逐条保存sheet2中的数据
            foreach (FarPoint.Win.Spread.Row r in this.neuSpread2_Sheet1.Rows)
            {
                decimal cancelQty = 0m;
                decimal oldConfirmedQty = 0m;
                //将表格数据赋值给apply实体
                apply = this.neuSpread2_Sheet1.Rows[r.Index].Tag as TerminalApply;
                //{8351D2B0-8C72-43e0-ADE6-4CEFF6ED3C2C}
                if (apply == null)
                {
                    return;
                }
                apply.Item.User03 = apply.Item.ConfirmedQty.ToString();
                oldConfirmedQty = FS.FrameWork.Function.NConvert.ToDecimal(apply.Item.User03);
                cancelQty = FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread2_Sheet1.Cells[r.Index, 2].Value.ToString());//取消数量
                
                if (cancelQty > oldConfirmedQty)
                {
                    FS.FrameWork.Management.Language.Msg("取消数量不能大于已确认数量");
                    return;
                }
                //剩余已确认数量
                apply.Item.ConfirmedQty = apply.Item.ConfirmedQty - cancelQty;//已确认数量变更
                ////如果有申请预约记录则不允许取消，需要先取消预约申请
                //hasApply = confirmIntegrate.GetTerminalApply(apply.Order.ID);
                //if (hasApply == true)
                //{
                //    MessageBox.Show("存在“" + apply.Item.Name + "”的预约信息，请先取消预约。");
                //    return;
                //}
                //更新met_tec_terminalapply中的确认数量
                result = confirmIntegrate.UpdateApplyConfirmQty(apply.User02, cancelQty);//confirm_num=confirm_num-cancelQty
                if (result != Result.Success)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新终端确认申请表中的确认数量失败。"+ confirmIntegrate.Err);                    

                    return;
                }
                //更新fin_opb_feedetail中对应医嘱的可退数量
                result = confirmIntegrate.UpdateNobackNum(apply.Order.ID, apply.Item.ID, cancelQty);//noback_num=noback_num+cancelQty;confirm_num=confirm_num-cancelQty
                if (result != Result.Success)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新可退数量失败。" + confirmIntegrate.Err);

                    return;
                }
                //直接更新met_tec_ta_detail中的已确认数量，将之前的已确认数量存入extend_field3
                result = confirmIntegrate.UpdateConfirmedQty(apply.ID, apply.Item.ConfirmedQty, oldConfirmedQty.ToString());
                if (result != Result.Success)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新已确认数量失败。" + confirmIntegrate.Err);

                    return;
                }

                //更新作废标志
                //部分确认暂不考虑-即不允许部分确认
                //直接更新met_tec_ta_detail中的已确认数量，将之前的已确认数量存入extend_field3
                result = confirmIntegrate.UpdateConfirmedFlag(apply.ID, FS.FrameWork.Management.Connection.Operator.ID, "");
                if (result != Result.Success)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新已确认标志。" + confirmIntegrate.Err);

                    return;
                }

            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("终端确认取消成功。");
        }
        /// <summary>
        /// 设置焦点到指定CELL
        /// [参数1: int row - 行号]
        /// [参数2: int column - 列号]
        /// </summary>
        /// <param name="row">行号</param>
        /// <param name="column">列号</param>
        public void CellFocus(int row, int column)
        {
            this.neuSpread2.Focus();
            //this.neuSpread2_Sheet1.ActiveRowIndex = row;
            //this.neuSpread2_Sheet1.ActiveColumnIndex = column;
            this.neuSpread2_Sheet1.SetActiveCell(row, column, false);
            //this.neuSpread2.EditMode = true;
        }

        /// <summary>
        /// 设置Register
        /// </summary>
        /// <param name="argRegister">传入的register</param>
        public void SetAllRegister(FS.HISFC.Models.Registration.Register argRegister)
        {
            this.Register = argRegister;
            this.ucPatientInformation.Register = argRegister;
        }

        /// <summary>
        /// 获取挂号科室和合同单位

        /// </summary>
        /// <param name="argRegister">返回的挂号实体</param>
        public int SetPactAndRegDept(ref FS.HISFC.Models.Registration.Register argRegister)
        {
            FS.HISFC.BizProcess.Integrate.Terminal.Result result = new Result();
            // 业务
            FS.HISFC.BizProcess.Integrate.Terminal.Confirm confirmIntegrate = new FS.HISFC.BizProcess.Integrate.Terminal.Confirm();
            // 合同单位实体
            FS.HISFC.Models.Base.PactInfo pact = new PactInfo();
            // 科室实体
            FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();

            // 获取合同单位信息
            result = confirmIntegrate.GetPact(ref pact, argRegister.Pact.ID);
            if (result != Result.Success)
            {
                FS.FrameWork.Management.Language.Msg("获取合同单位失败。" + confirmIntegrate.Err);

                return -1;
            }

            argRegister.Pact = pact; 
            // 获取科室信息
            result = confirmIntegrate.GetDept(ref dept, argRegister.DoctorInfo.Templet.Dept.ID);
            if (result != Result.Success)
            {
                FS.FrameWork.Management.Language.Msg("获取科室信息失败。" + confirmIntegrate.Err);

                return -1;                
            }
            argRegister.DoctorInfo.Templet.Dept = dept;
 
            return 1;
        }

        #endregion

        #region 事件
        /// <summary>
        /// 初始化uc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucCancelOutpatientConfirm_Load(object sender, EventArgs e)
        {

            InitUC();
        }
        /// <summary>
        /// 初始化工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override ToolBarService OnInit(object sender, object neuObject, object param)
        {
            if (this.DesignMode)
            {
                return base.OnInit(sender, neuObject, param);
            }
            // 按钮对应事件
            this.RefreshEvent += new EventHandler(ucCancelOutpatientConfirm_RefreshEvent);
            this.DeleteEvent += new EventHandler(ucCancelOutpatientConfirm_DeleteEvent);
            // 显示按钮
            this.toolbarService.AddToolButton("刷新", "刷新界面", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, this.RefreshEvent);
            this.toolbarService.AddToolButton("删除选定行", "删除第二个表格中的选定行", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, this.DeleteEvent);

            return this.toolbarService;
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            this.Clear();
            this.ucPatientInformation.Focus();
            return base.OnSave(sender, neuObject);
        }        

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            foreach (FarPoint.Win.Spread.Row r in neuSpread2_Sheet1.Rows)
            {
                if (r.Tag == neuSpread1_Sheet1.ActiveRow.Tag)
                {
                    MessageBox.Show("该行已被选择过！");
                    this.CellFocus(r.Index, 2);
                    return;
                }
            }

            int rowIndex = this.neuSpread2_Sheet1.RowCount;
            //将双击行的数据传到下面表格
            this.neuSpread2_Sheet1.Rows.Add(rowIndex, 1);
            neuSpread2_Sheet1.Cells[rowIndex, 0].Text = neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text;//项目编号0
            neuSpread2_Sheet1.Cells[rowIndex, 1].Text = neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 1].Text;//项目名1
            neuSpread2_Sheet1.Cells[rowIndex, 2].Text = neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 2].Text;//取消数量2

            neuSpread2_Sheet1.Cells[rowIndex, 3].Tag = neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 3].Tag;
            neuSpread2_Sheet1.Cells[rowIndex, 3].Text = neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 3].Text;//确认人3

            neuSpread2_Sheet1.Cells[rowIndex, 4].Tag = neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 4].Tag;
            neuSpread2_Sheet1.Cells[rowIndex, 4].Text = neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 4].Text;//确认科室4

            neuSpread2_Sheet1.Cells[rowIndex, 5].Text = neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 5].Text;//医嘱号
            neuSpread2_Sheet1.Cells[rowIndex, 6].Text = neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 6].Text;//执行设备号

            if (neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 7].Tag != null)
            {
                neuSpread2_Sheet1.Cells[rowIndex, 7].Tag = neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 7].Tag;
                neuSpread2_Sheet1.Cells[rowIndex, 7].Text = neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 7].Text;//执行人
            }
            neuSpread2_Sheet1.Cells[rowIndex, 8].Text = neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 8].Text;//执行时间
            neuSpread2_Sheet1.Cells[rowIndex, 9].Text = neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 9].Text;
            neuSpread2_Sheet1.Rows[rowIndex].Tag = neuSpread1_Sheet1.ActiveRow.Tag;

            //this.neuSpread2_Sheet1.Columns[0, this.neuSpread2_Sheet1.ColumnCount - 1].Locked = true;
            //取消数量不可修改
            //this.neuSpread2_Sheet1.Columns[2].Locked = false;

            this.CellFocus(rowIndex, 2);

        }

        private void neuSpread2_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DeleteSelectedRow();
        }

        void ucCancelOutpatientConfirm_DeleteEvent(object sender, EventArgs e)
        {
            int iReturn = 0;
            iReturn = this.DeleteSelectedRow();
            if (iReturn < 0)
            {
                MessageBox.Show("第二个表格中没有选中行。");
                return;
            }

        }

        void ucCancelOutpatientConfirm_RefreshEvent(object sender, EventArgs e)
        {
            this.Clear();
            this.ucPatientInformation.Focus();
        }
        /// <summary>
        /// 在患者病历号回车检索明细 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ucPatientInformation_KeyDownInQureyCode(object sender, KeyEventArgs e)
        {


            // 检索码
            string queryCode = "";
            // 患者信息
            FS.HISFC.Models.Registration.Register queryRegister = new Register();
            // 患者姓名确认
            Terminal.Confirm.frmPatientName frmPatient = new frmPatientName();
            // 操作结果
            FS.HISFC.BizProcess.Integrate.Terminal.Result result = new Result();
            // 执行检索
            #region 门诊病历号

            //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}{F5F57671-B453-45ff-A663-A682A000F567}{F5F57671-B453-45ff-A663-A682A000F567}
            #region 账户新增
            

             HISFC.Components.Common.Forms.frmQueryPatientByConditions frmQuery = new FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions();
              
                frmQuery.IsFliterUnValid = true;
                frmQuery.QueryByName(this.ucPatientInformation.textBoxCode.Text.Trim());
                frmQuery.ShowDialog();

                if (frmQuery.DialogResult == DialogResult.OK)
                {
                    queryCode = frmQuery.PatientInfo.PID.CardNO;
                    this.ucPatientInformation.textBoxCode.Text = queryCode;
                }

            FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
            if (feeIntegrate.ValidMarkNO(queryCode, ref accountCard) > 0)
            {
                queryCode = accountCard.Patient.PID.CardNO;
            }
            else
            {
                queryCode = this.ucPatientInformation.textBoxCode.Text.PadLeft(10, '0');
                // 显示填充后的病历号
            }

            #endregion

            //queryCode = this.ucPatientInformation.textBoxCode.Text.PadLeft(10, '0');
            // 显示填充后的病历号
            this.ucPatientInformation.textBoxCode.Text = queryCode;
            // 根据病历号从申请单获取患者基本信息 
            result = confirmIntegrate.GetOutpatientByCardNOFromRegister(ref queryRegister, queryCode);
            if (result == FS.HISFC.BizProcess.Integrate.Terminal.Result.Success)
            {
                this.ucPatientInformation.Register = queryRegister;
                //查询患者终端已确认信息并插入到sheet1
                this.QueryConfirmInfo(queryRegister.PID.CardNO);
            }
            else
            {
                MessageBox.Show("查找患者挂号信息失败!");
                this.ucPatientInformation.Focus();
            }
            #endregion


        }
        #endregion
        
       

        



    }

}
