using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.HISFC.Components.Manager.Controls
{
    public partial class ucPactCompare : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPactCompare()
        {
            InitializeComponent();
        }
        #region  全局变量
        private DataView myDataView;
        private DataTable myDataSet = new DataTable();
        Neusoft.HISFC.BizLogic.Manager.PactStatRelation comp = new Neusoft.HISFC.BizLogic.Manager.PactStatRelation();
        //定义 实例化 业务类
        Neusoft.HISFC.BizLogic.Manager.Spell mySpell = new Neusoft.HISFC.BizLogic.Manager.Spell();
        #endregion

        private Neusoft.FrameWork.WinForms.Forms.ToolBarService toolBarService = new Neusoft.FrameWork.WinForms.Forms.ToolBarService();

        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("添加", "添加", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.T添加, true, false, null);
            toolBarService.AddToolButton("删除", "删除", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            toolBarService.AddToolButton("刷新", "刷新", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.S刷新, true, false, null);
            

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "删除")
            {
                if (MessageBox.Show("删除后的信息不可以恢复，确认删除吗？", "提示>>", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                {
                    return;
                }
                this.DeleteData();
            }
            else if (e.ClickedItem.Text == "添加")
            {
                this.Add();
            }
            else if (e.ClickedItem.Text == "刷新")
            {
                this.Refresh();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        public void DeleteData()
        {
            try
            {
                if (this.fpCompany_Sheet1.RowCount > 0)
                {
                    this.fpCompany_Sheet1.Rows.Remove(this.fpCompany_Sheet1.ActiveRowIndex, 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private bool Valid(ArrayList list)
        {
            if (list == null)
            {
                return true;
            }
            foreach (Neusoft.HISFC.Models.Base.PactCompare comp in list)
            {
                if (comp.PactName == null || comp.PactName == "")
                {
                    MessageBox.Show("合同单位名称不能为空");
                    return false;
                }
                if (comp.PactCode == null || comp.PactCode == "")
                {
                    MessageBox.Show("合同单位编码不能为空");
                    return false;
                }

                if (comp.PactHead == null || comp.PactHead == "")
                {
                    MessageBox.Show("合同单位不能为空");
                    return false;
                }
               /* if (!Neusoft.FrameWork.Public.String.ValidMaxLengh(comp.StatClass.Name, 16))
                {
                    MessageBox.Show("统计大类名称过长");
                    return false;
                }
                if (comp.StatClass.ID == null || comp.StatClass.ID == "")
                {
                    MessageBox.Show("统计大类编码不能为空");
                    return false;
                }
                if (comp.BaseCost > 9999999999)
                {
                    MessageBox.Show("起伏线过大");
                    return false;
                }
                if (comp.DayQuota > 9999999999)
                {
                    MessageBox.Show("日限额过大");
                    return false;
                }
                if (comp.Quota > 9999999999)
                {
                    MessageBox.Show("限额过大");
                    return false;
                }
                */ 

            }
            return true;
        }
        /// <summary>
        /// 获取改动过的数据
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private ArrayList GetChanges(System.Data.DataTable table)
        {
            try
            {
                ArrayList list = new ArrayList();
                //取修改和增加的数据
                DataTable dataChanges = this.myDataSet.GetChanges(DataRowState.Modified | DataRowState.Added);
                if (dataChanges != null)
                {
                    foreach (DataRow row in dataChanges.Rows)
                    {
                      Neusoft.HISFC.Models.Base.PactCompare company = new Neusoft.HISFC.Models.Base.PactCompare();
                        company.PactCode = row["合同单位编码"].ToString();  //公司编码 //0 
                        company.PactHead = row["合同单位统计字头"].ToString(); //公司名称//1
                        company.PactName = row["合同单位名称"].ToString(); //单位类别//2
                        company.ParentPact = row["父级合同单位代码"].ToString(); //拼音码//3
                        company.ParentName = row["父级合同单位名称"].ToString();
                        company.PactFlag = row["合同单位属性"].ToString();

                          if ((row["结算类别"].ToString()).Equals("自费"))
                          {
                              company.PayKind.ID = "01";
                          }
                          else if ((row["结算类别"].ToString()).Equals("医保"))
                          {
                              company.PayKind.ID = "02";
                          }
                          else if ((row["结算类别"].ToString()).Equals("公费"))
                          {
                              company.PayKind.ID = "03";
                          }
                          if (row["有效性"].ToString()== "有效")
                          {
                              company.ValldState ="0";
                          }
                          else
                          {
                              company.ValldState = "1";     
                          }
                        
                       /* company.BaseCost = Neusoft.FrameWork.Function.NConvert.ToInt32(row["起伏线"]);
                        company.Quota = Neusoft.FrameWork.Function.NConvert.ToInt32(row["限额"]);
                        company.DayQuota = Neusoft.FrameWork.Function.NConvert.ToInt32(row["日限额"]);
                        company.Group.ID = row["组套"].ToString();
                        company.SortID = Neusoft.FrameWork.Function.NConvert.ToInt32(row["顺序号"]);
                        company.ID = row["主键列"].ToString();//公司联系人8
                        */
 
                        list.Add(company);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 保存数据 成功返回 1 失败返回－1
        /// </summary>
        public int Save()
        {
            try
            {
                fpCompany.StopCellEditing();
                if (this.myDataSet == null)
                {
                    return -1;
                }

                //取修改和增加的数据
                DataTable dataChanges = this.myDataSet.GetChanges(DataRowState.Modified | DataRowState.Added);
                ArrayList list = GetChanges(dataChanges);
                if (list == null)
                {
                    return -1;
                }
                //有效性判断
                if (!Valid(list))
                {
                    return -1;
                };

                //定义数据库处理事务
                Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
                comp.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

                bool isUpdate = false; //判断是否更新或者删除过数据

                //取修改和增加的数据
                foreach (Neusoft.HISFC.Models.Base.PactCompare company in list)
                {
                    //执行更新操作，先更新，如果没有成功则插入新数据
                    if (comp.SetItem(company) == -1)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(comp.Err);
                        return -1;
                    }
                }

                isUpdate = true;
                //取删除的数据
                dataChanges = this.myDataSet.GetChanges(DataRowState.Deleted);
                if (dataChanges != null)
                {
                    dataChanges.RejectChanges();
                    foreach (DataRow row in dataChanges.Rows)
                    {
                        Neusoft.HISFC.Models.Base.PactCompare pp = new Neusoft.HISFC.Models.Base.PactCompare();
                        pp.PactCode= row["合同单位编码"].ToString();
                        //执行删除操作
                        if (comp.DeleteCompareItem(pp.PactCode) == -1)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(comp.Err);
                            return 0;
                        }
                    }
                    isUpdate = true;
                }
                myDataSet.AcceptChanges();
                Neusoft.FrameWork.Management.PublicTrans.Commit();
                this.lockFp(); //锁定编码
                if (isUpdate) MessageBox.Show("保存成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;
        }


        private void ucCompanyManager_Load(object sender, System.EventArgs e)
        {
            try
            {
                #region 合同单位
                Neusoft.HISFC.BizLogic.Fee.PactUnitInfo pact = new Neusoft.HISFC.BizLogic.Fee.PactUnitInfo();
                //ArrayList conList = pact.GetPactUnitInfo();
                // ArrayList conList = pact.QueryPactCompareAll();
                ArrayList conList = pact.QueryPactCompareAll();
                if (conList == null)
                {
                    MessageBox.Show("合同单位获取失败");
                    return;
                }
                #endregion
                #region 统计大类
                /*               
                ArrayList Feelist = comp.GetFeeCodeState();
                if (Feelist == null)
                {
                    MessageBox.Show("获取统计大类出错");
                    return;
                }
                this.fpCompany.SetColumnList(this.fpCompany_Sheet1, (int)Cols.PactName, conList);
                this.fpCompany.SetColumnList(this.fpCompany_Sheet1, (int)Cols.ClassName, Feelist);
               
*/
                 #endregion

                this.InitDataSet();
               
                #region 获取单位
                /*
                ArrayList list = this.comp.GetItemList();
                if (list == null)
                {
                    MessageBox.Show("获取单位失败" + comp.Err);
                    return;
                }
                 */

                #endregion


                FillDate(conList);
                this.fpCompany.KeyEnter += new Neusoft.FrameWork.WinForms.Controls.NeuFpEnter.keyDown(fpEnter1_KeyEnter);
                fpCompany.SetItem += new Neusoft.FrameWork.WinForms.Controls.NeuFpEnter.setItem(fpEnter1_SetItem);
                fpCompany.ShowListWhenOfFocus = true;
                this.fpCompany_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;

                this.fpCompany.SetColumnList(this.fpCompany_Sheet1, (int)Cols.PactName, conList);
                this.fpCompany_Sheet1.SetColumnAllowAutoSort(-1, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private enum Cols
    {
        PactCode, //合同单位代码 
	    PactHead,//合同单位统计字头
	    PactName,//合同单位统计名称
	    ParentPact , //父级合同单位代码
        ParentName,
	    PactFlag,//合同单位属性
        PayKind,//合同单位结算类别
        ValldState
        }
        /// <summary>
        /// 按键响应处理
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int fpEnter1_KeyEnter(Keys key)
        {
            if (key == Keys.Enter)
            {
                //				MessageBox.Show("Enter,可以自己添加处理事件，比如跳到下一cell");
                //回车
                if (this.fpCompany.ContainsFocus)
                {
                    int i = this.fpCompany_Sheet1.ActiveColumnIndex;
                    #region
                    switch (i)
                    {
                        case 0: //合同单位编码
                            this.fpCompany_Sheet1.SetActiveCell(this.fpCompany_Sheet1.ActiveRowIndex, (int)Cols.PactCode);
                            break;
                        case 1:// 合同单位统计字头
                            this.fpCompany_Sheet1.SetActiveCell(this.fpCompany_Sheet1.ActiveRowIndex, (int)Cols.PactHead);
                            break;
                        case 2:
                            //合同单位名称
                            this.ProcessDept();
                            break;
                       
                        case 3://父级合同单位代码
                            this.fpCompany_Sheet1.SetActiveCell(this.fpCompany_Sheet1.ActiveRowIndex, (int)Cols.ParentPact);
                            break;
                        case 4: //父级合同单位名称
                            this.fpCompany_Sheet1.SetActiveCell(this.fpCompany_Sheet1.ActiveRowIndex, (int)Cols.ParentName);
                            break;
                        case 5://合同单位属性
                            this.fpCompany_Sheet1.SetActiveCell(this.fpCompany_Sheet1.ActiveRowIndex, (int)Cols.PactFlag);
                            break;
                        case 6://结算类别
                            this.fpCompany_Sheet1.SetActiveCell(this.fpCompany_Sheet1.ActiveRowIndex, (int)Cols.PayKind);
                            break;
                        case 7://有效性
                            this.fpCompany_Sheet1.SetActiveCell(this.fpCompany_Sheet1.ActiveRowIndex, (int)Cols.ValldState);
                            break;
                        case 8:
                            if (fpCompany_Sheet1.ActiveRowIndex < fpCompany_Sheet1.Rows.Count - 1)
                            {
                                this.fpCompany_Sheet1.SetActiveCell(fpCompany_Sheet1.ActiveRowIndex + 1, 1);
                            }
                            else
                            {
                                this.Add();
                            }
                            break;

                    }
                    #endregion
                }
            }
            else if (key == Keys.Up)
            {
                //				MessageBox.Show("Up,可以自己添加处理事件，比如无下拉列表时，跳到下列，显示下拉控件时，在下拉控件上下移动");
            }
            else if (key == Keys.Down)
            {
                //				MessageBox.Show("Down，可以自己添加处理事件，比如无下拉列表时，跳到上列，显示下拉控件时，在下拉控件上下移动");
            }
            else if (key == Keys.Escape)
            {
                //				MessageBox.Show("Escape,取消列表可见");
            }
            return 0;
        }
        /// <summary>
        /// 选则选中的项
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private int fpEnter1_SetItem(Neusoft.FrameWork.Models.NeuObject obj)
        {
            this.ProcessDept();
            return 0;
        }
        /// <summary>
        /// 处理回车操作 ，并且取出数据
        /// </summary>
        /// <returns></returns>
        private int ProcessDept()
        {
            int CurrentRow = fpCompany_Sheet1.ActiveRowIndex;
            if (CurrentRow < 0) return 0;

            if (fpCompany_Sheet1.ActiveColumnIndex == (int)Cols.PactName)
            {
                
                //Neusoft.FrameWork.WinForms.Controls.NeuListBox listBox = this.fpCompany.getCurrentList(this.fpCompany_Sheet1, 1);
                Neusoft.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpCompany.getCurrentList(this.fpCompany_Sheet1, 2);
                //获取选中的信息
                Neusoft.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                //类别名称
                fpCompany_Sheet1.ActiveCell.Text = item.Name;
                fpCompany_Sheet1.Cells[this.fpCompany_Sheet1.ActiveRowIndex, (int)Cols.PactCode].Text = item.ID;
                this.fpCompany.Focus();
                fpCompany_Sheet1.SetActiveCell(fpCompany_Sheet1.ActiveRowIndex, (int)Cols.ParentPact);
                return 0;
            }
            else if (fpCompany_Sheet1.ActiveColumnIndex == (int)Cols.PactFlag)
            {
                Neusoft.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpCompany.getCurrentList(this.fpCompany_Sheet1, 4);
                //获取选中的信息
                Neusoft.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                //类别名称
                fpCompany_Sheet1.ActiveCell.Text = item.Name;
                fpCompany_Sheet1.Cells[this.fpCompany_Sheet1.ActiveRowIndex, (int)Cols.PayKind].Text = item.ID;
                this.fpCompany.Focus();
                fpCompany_Sheet1.SetActiveCell(fpCompany_Sheet1.ActiveRowIndex, (int)Cols.ValldState);
                return 0;
            }
            return 0;
        }
        /// <summary>
        ///  初始化DataSet,并与fpCompany绑定
        /// </summary>
        private void InitDataSet()
        {

            //定义类型
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtInt = System.Type.GetType("System.Int32");
            System.Type dtDTime = System.Type.GetType("System.DateTime");
            System.Type dtBool = System.Type.GetType("System.Boolean");

            //在myDataTable中添加列
            this.myDataSet.Columns.AddRange(new DataColumn[] {
																  new DataColumn("合同单位编码",   dtStr), //0
																  new DataColumn("合同单位统计字头",   dtStr),//1
																  new DataColumn("合同单位名称",   dtStr),//2
																  new DataColumn("父级合同单位代码",   dtStr),//3
																  new DataColumn("父级合同单位名称",   dtStr),//4
																  new DataColumn("合同单位属性",   dtStr),//5
                                                                  new DataColumn("结算类别",    dtStr),//6
																  new DataColumn("有效性",    dtStr),//7
																
																
		
            
            
            });
            this.myDataView = new DataView(myDataSet);
            this.fpCompany.DataSource = this.myDataView;
        }
        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private int FillDate(System.Collections.ArrayList list)
        {
            if (list == null)
            {
                return -1;
            }

            foreach (Neusoft.HISFC.Models.Base.PactCompare company in list)
            {
                myDataSet.Rows.Add(new object[]{company.PactCode, //合同单位编码 //0 
												   company.PactHead,//合同单位统计字头//1
												   company.PactName,//合同单位名称2
												   company.ParentPact , //父级合同单位代码//3
                                                   company.ParentName,  //父级合同单位名称//4
												   company.PactFlag,//合同单位属性5
                                                   company.PayKind.ID,//结算类别
                                                   company.ValldState//有效性
											   });
            }
            myDataSet.AcceptChanges();
            this.myDataView = new DataView(this.myDataSet);
            this.fpCompany_Sheet1.DataSource = this.myDataView;
            lockFp();
            return 1;
        }
        private void lockFp()
        {
            FarPoint.Win.Spread.CellType.CheckBoxCellType cb = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.fpCompany_Sheet1.Columns[(int)Cols.PactCode].Visible = true;//合同单位编码
            this.fpCompany_Sheet1.Columns[(int)Cols.PactCode].Width = 100;//合同单位编码
            this.fpCompany_Sheet1.Columns[(int)Cols.PactHead].Width = 120;//合同单位统计字头
            this.fpCompany_Sheet1.Columns[(int)Cols.PactName].Width = 120;//合同单位名称
            this.fpCompany_Sheet1.Columns[(int)Cols.ParentPact].Visible = true;//大类编码
            this.fpCompany_Sheet1.Columns[(int)Cols.ParentPact].Width = 120;//父级合同单位代码
            this.fpCompany_Sheet1.Columns[(int)Cols.ParentName].Width = 120;//父级合同单位名称
            this.fpCompany_Sheet1.Columns[(int)Cols.PactFlag].Width = 120;//合同单位属性 
            this.fpCompany_Sheet1.Columns[(int)Cols.ValldState].Width = 60;//有效性
        }
        /// <summary>
        /// 查询 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQueryCode_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (this.chbMisty.Checked) //模糊查询 
                {
                    string queryCode = "";
                    queryCode = "%" + this.txtQueryCode.Text.Trim() + "%";

                    string filter = "(合同单位编码 LIKE '" + queryCode + "') " ;

                    //设置过滤条件
                    this.myDataView.RowFilter = filter;
                }
                else //准确查询
                {
                    string queryCode = "";
                    queryCode = this.txtQueryCode.Text.Trim() + "%";

                    string filter = "(合同单位编码 LIKE '" + queryCode + "') " ;

                    //设置过滤条件
                    this.myDataView.RowFilter = filter;
                }
                lockFp();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public int Add()
        {
            // TODO:  添加 ucPactstatrelation.Add 实现
            this.fpCompany_Sheet1.Rows.Add(this.fpCompany_Sheet1.RowCount, 1);
            string temp = comp.GetPactSequence();
            this.fpCompany_Sheet1.Cells[this.fpCompany_Sheet1.RowCount - 1, (int)Cols.PactCode].Text = temp;
            this.fpCompany_Sheet1.SetActiveCell(this.fpCompany_Sheet1.RowCount - 1, (int)Cols.PactName);
            return 0;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.A.GetHashCode())
            {
                this.Add();
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.D.GetHashCode())
            {
                this.DeleteData();
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.S.GetHashCode())
            {
                this.Save();
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.R.GetHashCode())
            {
                this.Refresh();
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.X.GetHashCode())
            {
                this.FindForm().Close();
            }
            return base.ProcessDialogKey(keyData);
        }
    }

}
