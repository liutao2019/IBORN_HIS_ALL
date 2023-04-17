using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Manager.Controls
{
    public partial class ucPactStatRelation : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPactStatRelation()
        {
            InitializeComponent();
        }
        #region  全局变量
        private DataView myDataView;
        private DataTable myDataSet = new DataTable();
        FS.HISFC.BizLogic.Manager.PactStatRelation comp = new FS.HISFC.BizLogic.Manager.PactStatRelation();
        //定义 实例化 业务类
        FS.HISFC.BizLogic.Manager.Spell mySpell = new FS.HISFC.BizLogic.Manager.Spell();
        #endregion

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("添加", "添加", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T添加, true, false, null);
            toolBarService.AddToolButton("删除", "删除", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            toolBarService.AddToolButton("刷新", "刷新", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S刷新, true, false, null);
            

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
                    fpCompany.StopCellEditing();
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
            foreach (FS.HISFC.Models.Base.PactStatRelation comp in list)
            {
                if (comp.Pact.Name == null || comp.Pact.Name == "")
                {
                    MessageBox.Show("合同单位不能为空");
                    return false;
                }
                if (comp.Pact.ID == null || comp.Pact.ID == "")
                {
                    MessageBox.Show("合同单位编码不能为空");
                    return false;
                }

                if (comp.StatClass.Name == null || comp.StatClass.Name == "")
                {
                    MessageBox.Show("统计大类不能为空");
                    return false;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(comp.StatClass.Name, 16))
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
                        FS.HISFC.Models.Base.PactStatRelation company = new FS.HISFC.Models.Base.PactStatRelation();
                        company.Pact.ID = row["合同单位编码"].ToString();  //公司编码 //0 
                        company.Pact.Name = row["合同单位"].ToString(); //公司名称//1
                        company.StatClass.ID = row["统计大类"].ToString(); //单位类别//2
                        company.StatClass.Name = row["大类名称"].ToString(); //拼音码//3
                        company.BaseCost = FS.FrameWork.Function.NConvert.ToInt32(row["起伏线"]);
                        company.Quota = FS.FrameWork.Function.NConvert.ToInt32(row["限额"]);
                        company.DayQuota = FS.FrameWork.Function.NConvert.ToInt32(row["日限额"]);
                        company.Group.ID = row["组套"].ToString();
                        company.SortID = FS.FrameWork.Function.NConvert.ToInt32(row["急诊"]);
                        company.ID = row["主键列"].ToString();//公司联系人8
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
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                comp.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                bool isUpdate = false; //判断是否更新或者删除过数据

                //取修改和增加的数据
                foreach (FS.HISFC.Models.Base.PactStatRelation company in list)
                {
                    //执行更新操作，先更新，如果没有成功则插入新数据
                    if (comp.SetItem(company) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
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
                        FS.HISFC.Models.Base.PactStatRelation pp = new FS.HISFC.Models.Base.PactStatRelation();
                        pp.ID = row["主键列"].ToString();
                        //执行删除操作
                        if (comp.DeleteItem(pp.ID) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(comp.Err);
                            return 0;
                        }
                    }
                    isUpdate = true;
                }
                myDataSet.AcceptChanges();
                FS.FrameWork.Management.PublicTrans.Commit();
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
                FS.HISFC.BizLogic.Fee.PactUnitInfo pact = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
                //ArrayList conList = pact.GetPactUnitInfo();
                ArrayList conList = pact.QueryPactUnitAll();
                if (conList == null)
                {
                    MessageBox.Show("合同单位失败");
                    return;
                }
                #endregion
                #region 统计大类
                ArrayList Feelist = comp.GetFeeCodeState();
                if (Feelist == null)
                {
                    MessageBox.Show("获取统计大类出错");
                    return;
                }
                this.fpCompany.SetColumnList(this.fpCompany_Sheet1, (int)Cols.PactName, conList);
                this.fpCompany.SetColumnList(this.fpCompany_Sheet1, (int)Cols.ClassName, Feelist);
                #endregion

                this.InitDataSet();
                ArrayList list = this.comp.GetItemList();
                if (list == null)
                {
                    MessageBox.Show("获取单位失败" + comp.Err);
                    return;
                }
                FillDate(list);
                this.fpCompany.KeyEnter += new FS.FrameWork.WinForms.Controls.NeuFpEnter.keyDown(fpEnter1_KeyEnter);
                fpCompany.SetItem += new FS.FrameWork.WinForms.Controls.NeuFpEnter.setItem(fpEnter1_SetItem);
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
            PactCode,
            PactName,
            GroupId,
            ClassID,
            ClassName,
            Cost,
            Quota,
            DayQuota,
            SortID,
            PrimaryKey
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
                            this.fpCompany_Sheet1.SetActiveCell(this.fpCompany_Sheet1.ActiveRowIndex, (int)Cols.PactName);
                            break;
                        case 1:
                            //合同单位名称
                            this.ProcessDept();
                            break;
                        case 2:// 组套
                            this.fpCompany_Sheet1.SetActiveCell(this.fpCompany_Sheet1.ActiveRowIndex, (int)Cols.ClassName);
                            break;
                        case 3://统计大类编码
                            this.fpCompany_Sheet1.SetActiveCell(this.fpCompany_Sheet1.ActiveRowIndex, (int)Cols.ClassName);
                            break;
                        case 4: //统计大类名称
                            this.ProcessDept();
                            break;
                        case 5://起伏线
                            this.fpCompany_Sheet1.SetActiveCell(this.fpCompany_Sheet1.ActiveRowIndex, (int)Cols.Quota);
                            break;
                        case 6://限额
                            this.fpCompany_Sheet1.SetActiveCell(this.fpCompany_Sheet1.ActiveRowIndex, (int)Cols.DayQuota);
                            break;
                        case 7://限额
                            this.fpCompany_Sheet1.SetActiveCell(this.fpCompany_Sheet1.ActiveRowIndex, (int)Cols.SortID);
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
        private int fpEnter1_SetItem(FS.FrameWork.Models.NeuObject obj)
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
                //FS.FrameWork.WinForms.Controls.NeuListBox listBox = this.fpCompany.getCurrentList(this.fpCompany_Sheet1, 1);
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpCompany.getCurrentList(this.fpCompany_Sheet1, 1);
                //获取选中的信息
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                //类别名称
                fpCompany_Sheet1.ActiveCell.Text = item.Name;
                fpCompany_Sheet1.Cells[this.fpCompany_Sheet1.ActiveRowIndex, (int)Cols.PactCode].Text = item.ID;
                this.fpCompany.Focus();
                fpCompany_Sheet1.SetActiveCell(fpCompany_Sheet1.ActiveRowIndex, (int)Cols.ClassName);
                return 0;
            }
            else if (fpCompany_Sheet1.ActiveColumnIndex == (int)Cols.ClassName)
            {
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpCompany.getCurrentList(this.fpCompany_Sheet1, 4);
                //获取选中的信息
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                //				if(rtn==-1)return -1;
                if (item == null) return -1;
                //类别名称
                fpCompany_Sheet1.ActiveCell.Text = item.Name;
                fpCompany_Sheet1.Cells[this.fpCompany_Sheet1.ActiveRowIndex, (int)Cols.ClassID].Text = item.ID;
                this.fpCompany.Focus();
                fpCompany_Sheet1.SetActiveCell(fpCompany_Sheet1.ActiveRowIndex, (int)Cols.Cost);
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
																  new DataColumn("合同单位",   dtStr),//1
																  new DataColumn("组套",   dtStr),//1
																  new DataColumn("统计大类",   dtStr),//2
																  new DataColumn("大类名称",   dtStr),//3
																  new DataColumn("起伏线",   dtInt),//4
																  new DataColumn("限额",    dtInt),//5
																  new DataColumn("日限额",    dtInt),//6
																  new DataColumn("急诊",     dtInt),//7
																  new DataColumn("主键列",     dtStr),//8
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

            foreach (FS.HISFC.Models.Base.PactStatRelation company in list)
            {
                myDataSet.Rows.Add(new object[]{company.Pact.ID , //合同单位编码 //0 
												   company.Pact.Name,//合同单位//1
												   company.Group.ID,//组套编码
												   company.StatClass.ID, //大类编码//2
												   company.StatClass.Name,//大类名称//3
												   company.BaseCost,//起伏线//4
												   company.Quota , //限额//5
												   company.DayQuota,//日限额/6
												   company.SortID , //序号
												   company.ID //主键
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
            this.fpCompany_Sheet1.Columns[(int)Cols.PactName].Width = 120;//合同单位
            this.fpCompany_Sheet1.Columns[(int)Cols.GroupId].Width = 80;//组套编码
            this.fpCompany_Sheet1.Columns[(int)Cols.ClassID].Visible = false;//大类编码
            this.fpCompany_Sheet1.Columns[(int)Cols.ClassName].Width = 60;//大类名称
            this.fpCompany_Sheet1.Columns[(int)Cols.Cost].Width = 60;//起伏线
            this.fpCompany_Sheet1.Columns[(int)Cols.Quota].Width = 60;//限额
            this.fpCompany_Sheet1.Columns[(int)Cols.DayQuota].Width = 60;//日限额
            this.fpCompany_Sheet1.Columns[(int)Cols.SortID].Width = 80;//序号
            this.fpCompany_Sheet1.Columns[(int)Cols.PrimaryKey].Visible = false;//主键
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
            this.fpCompany_Sheet1.Cells[this.fpCompany_Sheet1.RowCount - 1, (int)Cols.PrimaryKey].Text = temp;
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
