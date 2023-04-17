using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Manager
{
    /// <summary>
    /// 执行单批量转换
    /// </summary>
    public partial class ucExceBill : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucExceBill()
        {
            InitializeComponent();
        }
		  
		#region 全局变量 
		//业务层函数 
		FS.HISFC.BizLogic.Order.ExecBill execBillMgr = new FS.HISFC.BizLogic.Order.ExecBill();
		ArrayList ExecBillList = null; //主挡列表 
		#endregion 

		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.ExecBillList = null;

			//删除原有的数据
			if(this.fpSpread1_Sheet1.RowCount >0)
			{
				this.fpSpread1_Sheet1.Rows.Remove(0,this.fpSpread1_Sheet1.Rows.Count);
			}
			if(comboBox1.Tag == null)
			{
				MessageBox.Show("请选择病区");
				return ;
			}
			ExecBillList= execBillMgr.QueryExecBill(this.comboBox1.Tag.ToString());
			if(ExecBillList == null )
			{
				MessageBox.Show("查询执行单出错" + execBillMgr.Err);
				return ;
			}
            FarPoint.Win.Spread.CellType.CheckBoxCellType cb = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();

            this.fpSpread1_Sheet1.Columns[0].CellType = cb;
            this.fpSpread1_Sheet1.Columns[2].CellType = t;

            foreach (FS.FrameWork.Models.NeuObject obj in ExecBillList)
			{
				this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.Rows.Count,1);
				int i = this.fpSpread1_Sheet1.Rows.Count -1;
                this.fpSpread1_Sheet1.Cells[i, 0].Value = true;
                this.fpSpread1_Sheet1.Cells[i, 1].Text = obj.Name;
				this.fpSpread1_Sheet1.Cells[i,2].Text = obj.ID;
				this.fpSpread1_Sheet1.Cells[i,1].Tag = obj;
			}
		}

        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveInfo();

            return base.OnSave(sender, neuObject);
        }

		/// <summary>
		/// 保存数据
		/// </summary>
        private void SaveInfo()
        {
            if (this.comboBox1.Tag == null)
            {
                MessageBox.Show("请选择目标护理站");
                this.comboBox1.Focus();
                return;
            }
            //科室编码
            string OldDeptCode = this.comboBox1.Tag.ToString();
            //执行
            string ExecNo = "";
            ArrayList deptlist = GetDept();
            if (deptlist.Count == 0)
            {
                //没有需要转化的
                return;
            }
            if (this.ExecBillList == null)
            {
                MessageBox.Show("请选择原科室");
                this.comboBox1.Focus();
                return;
            }
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在转换数据，请稍候...");
            Application.DoEvents();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            execBillMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            try
            {
                foreach (FS.FrameWork.Models.NeuObject obj in deptlist) //循环科室 
                {
                    if (obj.ID == OldDeptCode) //如果目标科室等于原科室 跳过
                    {
                        continue;
                    }
                    #region 删除执行单
                    if (execBillMgr.DeleteAllExecBill(obj.ID) == -1) //删除护理站的所有原来的数据
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("删除原有数据失败 : " + execBillMgr.Err);
                        return;
                    }
                    #endregion
                }
                foreach (FS.FrameWork.Models.NeuObject info in ExecBillList) //循环执行档 
                {
                    ArrayList DetailList = execBillMgr.QueryExecBillDetailByBillNo(info.ID);
                    foreach (FS.FrameWork.Models.NeuObject obj in deptlist) //循环科室 
                    {
                        #region 插入主档
                        //FS.FrameWork.Models.neuObject MainObj = (FS.FrameWork.Models.neuObject ) fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex,0].Tag ;
                        //ArrayList DetailList = GetDetail();
                        if (obj.ID == OldDeptCode) //如果目标科室等于原科室 跳过
                        {
                            continue;
                        }

                        if (DetailList.Count == 0)
                        {
                            continue;
                        }
                        if (execBillMgr.SetExecBill(DetailList, info, obj.ID, ref ExecNo) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("保存失败" + execBillMgr.Err);
                            return;
                        }
                        #endregion
                    }
                }
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                FS.FrameWork.Management.PublicTrans.Commit(); ;
                MessageBox.Show("保存成功");

            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveAsAdd()
        {
            string curSQL = @"insert into met_ipm_execbill
                                select '{2}',
                                       '{4}',
                                       t.bill_name,
                                       t.bill_kind,
                                       '{3}',
                                       sysdate,
                                       t.mark,
                                       t.item_flag
                                  from met_ipm_execbill t
                                 where t.nurse_cell_code = '{0}'
                                 and   t.bill_no = '{1}'";

            ArrayList alExeBill = this.GetExeBill();
            if (alExeBill.Count == 0)
            {
                MessageBox.Show("请选择执行单！");
                return;
            }
            ArrayList alDept = this.GetDept();
            if (alDept.Count == 0)
            {
                MessageBox.Show("请选择科室！");
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            foreach (string billNO in alExeBill)
            {
                foreach (FS.FrameWork.Models.NeuObject dept in alDept)
                {
                    string newBillNO = this.execBillMgr.GetNewExecBillID();
                    if (string.IsNullOrEmpty(newBillNO))
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("获取执行单流水号发生错误：" + this.execBillMgr.Err);

                        return;
                    }
                    string sql = string.Format(curSQL, this.comboBox1.Tag.ToString(), billNO, dept.ID, this.execBillMgr.Operator.ID, newBillNO);
                    if (this.execBillMgr.ExecNoQuery(sql) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("赋值执行单发生错误：" + this.execBillMgr.Err);

                        return;
                    }

                    string insertDrugSQL = @"
                            insert into met_ipm_drugbilldetail
                              select '{2}',
                                     '{4}',
                                     d.type_code,
                                     d.drug_type,
                                     d.usage_code,
                                     '{3}',
                                     sysdate
                                from met_ipm_drugbilldetail d
                               where d.nurse_cell_code = '{0}'
                               and   d.bill_no = '{1}'
                    ";

                    insertDrugSQL = string.Format(insertDrugSQL, this.comboBox1.Tag.ToString(), billNO, dept.ID, this.execBillMgr.Operator.ID, newBillNO);
                    if (this.execBillMgr.ExecNoQuery(insertDrugSQL) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("赋值执行单发生错误：" + this.execBillMgr.Err);

                        return;
                    }


                    string insertUnDrugSQL = @"
                            insert into met_ipm_undrugbilldetail
                                        select '{2}',
                                               '{4}',
                                               u.type_code,
                                               u.class_code,
                                               '{3}',
                                               sysdate,
                                               u.item_code,
                                               u.item_name
                                          from met_ipm_undrugbilldetail u
                                         where u.nurse_cell_code = '{0}'
                                         and   u.bill_no = '{1}'
                                                            ";

                    insertUnDrugSQL = string.Format(insertUnDrugSQL, this.comboBox1.Tag.ToString(), billNO, dept.ID, this.execBillMgr.Operator.ID, newBillNO);
                    if (this.execBillMgr.ExecNoQuery(insertUnDrugSQL) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("赋值执行单发生错误：" + this.execBillMgr.Err);

                        return;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("操作完成！");
        }

		/// <summary>
		/// 获取需要转化的目标护理站 
		/// </summary>
		/// <returns></returns>
		private ArrayList GetDept()
		{
			ArrayList list = new ArrayList();
			for(int i =0;i < fpDept.RowCount;i++)
			{
				if(this.fpDept.Cells[i,0].Value.ToString().ToUpper() =="TRUE")
				{
					FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
					obj.Name = this.fpDept.Cells[i,1].Text;
					obj.ID = this.fpDept.Cells[i,2].Text;
					list.Add(obj);
				}
			}
			return list;
		}


        /// <summary>
        /// 获取需要转化的目标护理站 
        /// </summary>
        /// <returns></returns>
        private ArrayList GetExeBill()
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                if (this.fpSpread1_Sheet1.Cells[i, 0].Value.ToString().ToUpper() == "TRUE")
                {
                    list.Add(this.fpSpread1_Sheet1.Cells[i, 2].Text);
                }
            }
            return list;
        }

		private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
		{
//			try
//			{
//				//删除原有的数据

//				if(this.fpSpread2_Sheet1.RowCount >0)
//				{
//					this.fpSpread2_Sheet1.Rows.Remove(0,this.fpSpread2_Sheet1.Rows.Count);
//				}
//				if(this.fpSpread1_Sheet1.RowCount ==0)
//				{
//					return;
//				}
//				ArrayList list = execBill.GetExecBillDetail(this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex,1].Text);
//				if(list == null)
//				{
//					MessageBox.Show("查询执行单明细出错" + execBill.Err);
//					return ;
//				}
//
//				foreach(FS.HISFC.Models.Order.Order  obj in list)
//				{
//					this.fpSpread2_Sheet1.Rows.Add(this.fpSpread2_Sheet1.Rows.Count,1);
//					int i = this.fpSpread2_Sheet1.Rows.Count -1;
//					this.fpSpread2_Sheet1.Cells[i,0].Text = obj.OrderType.Name; //医嘱类型 
//					if(obj.Memo == "1")
//					{
//						this.fpSpread2_Sheet1.Cells[i,1].Text = "药品";
//					}
//					else if(obj.Memo == "2")
//					{
//						this.fpSpread2_Sheet1.Cells[i,1].Text = "非药品"; //药品非药品

//					}
//					this.fpSpread2_Sheet1.Cells[i,2].Text = obj.Item.SysClass.ID.ToString(); //系统类别 
//					this.fpSpread2_Sheet1.Cells[i,3].Text = obj.Usage.Name; //用法
//					this.fpSpread2_Sheet1.Cells[i,0].Tag  = obj;
//				}
//			}
//			catch(Exception ex)
//			{
//				MessageBox.Show(ex.Message);
//			}
		}

		private void frmExecBill_Load(object sender, System.EventArgs e)
		{
			FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
			ArrayList deptList = dept.GetNurseAll();
			if(deptList == null)
			{
				MessageBox.Show("获取护理站出错" + dept.Err);
				return ;
			}
			FarPoint.Win.Spread.CellType.CheckBoxCellType cb = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
			fpDept.Columns[0].CellType = cb;
			this.comboBox1.AddItems(deptList);
			int i =0;

            //避免科室编码列被当成数字类型截取了... houwb 2011-5-25
            this.fpDept.Columns[2].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
			foreach(FS.HISFC.Models.Base.Department obj in deptList)
			{
				this.fpDept.Rows.Add(this.fpDept.RowCount,1);
				this.fpDept.Cells[i,0].Value = false;
				this.fpDept.Cells[i,1].Text  = obj.Name;
				this.fpDept.Cells[i,2].Text = obj.ID ;
				i++;
			}
            this.ncbChooseAll.Checked = true;

            this.ncbChooseAll.CheckedChanged+=new EventHandler(ncbChooseAll_CheckedChanged);
            this.fpSpread1.CellDoubleClick+=new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellDoubleClick);

		}
        /// <summary>
        /// 全选 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkAll_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.checkAll.Checked)
            {
                for (int i = 0; i < this.fpDept.RowCount; i++)
                {
                    this.fpDept.Cells[i, 0].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < this.fpDept.RowCount; i++)
                {
                    this.fpDept.Cells[i, 0].Value = false;
                }
                this.checkAll.Checked = false;
            }
        }

        /// <summary>
        /// 全选 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ncbChooseAll_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.ncbChooseAll.Checked)
            {
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    this.fpSpread1_Sheet1.Cells[i, 0].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    this.fpSpread1_Sheet1.Cells[i, 0].Value = false;
                }
                this.ncbChooseAll.Checked = false;
            }
        }


        #region 工具栏
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("增量保存", "将选中的执行单增加到选中的病区或科室，不删除现有单据", FS.FrameWork.WinForms.Classes.EnumImageList.S刷新, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "增量保存")
            {
                this.SaveAsAdd();
            }

        }

        #endregion

    }
}
