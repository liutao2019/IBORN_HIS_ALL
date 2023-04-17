using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Pharmacy;
using FS.FrameWork.Models;

namespace FS.SOC.HISFC.Components.DrugStore.Inpatient.Maintenence
{
    /// <summary>
    /// [控件名称:ucDrugBill]<br></br>
    /// [功能描述: 摆药单设置]<br></br>
    /// [创 建 者: 杨永刚]<br></br>
    /// [创建时间: 2006-11-10]<br></br>
    /// <修改记录>
    ///     2011-03 整合 by cube
    /// </修改记录>
    ///  />
    /// </summary>
    public partial class ucDrugBillClass : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDrugBillClass( )
        {
            InitializeComponent( );
        }

        #region 变量

        //摆药单分类实体类
        private DrugBillClass drugBillClassInfo;
        //药房管理类
        private FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore( ); 

        #endregion

        #region 方法

        #region 摆药单设置

        /// <summary>
        /// 判断传入摆药单信息是否有效
        /// </summary>
        /// <param name="drugBillClass">摆药单信息</param>
        /// <returns>成功返回True  失败返回False</returns>
        private bool IsDrugBillDataValid(DrugBillClass drugBillClass)
        {
            if (!FS.FrameWork.Public.String.ValidMaxLengh(drugBillClass.Memo, 150))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("备注字段超长"));
                return false;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(drugBillClass.Name, 30))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("摆药单名称超长"));
                return false;
            }

            foreach (ListViewItem lv in this.lvPutDrugBill1.Items)
            {
                DrugBillClass tempDrugBill = lv.Tag as DrugBillClass;
                if (tempDrugBill == null)
                {
                    continue;
                }

                if (!FS.FrameWork.Public.String.ValidMaxLengh(tempDrugBill.Memo, 150))
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("备注字段超长"));
                    return false;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(tempDrugBill.Name, 30))
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("摆药单名称超长"));
                    return false;
                }

                if (tempDrugBill.ID == drugBillClass.ID)
                {
                    continue;
                }                

                if (lv.Text == drugBillClass.Name)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg(drugBillClass.Name + "摆药单名称重复 请重新维护"));
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 从tabpage2中中取数据，保存在myDrugBillClass中。
        /// </summary>
        private DrugBillClass GetDrugBillItem( )
        {
            if( this.drugBillClassInfo == null )
            {
                drugBillClassInfo = new DrugBillClass( );
            }

            this.drugBillClassInfo.Name = this.txtName.Text;                       //摆药分类名称
            this.drugBillClassInfo.PrintType.ID = this.cbxPrinttype.Tag;           //打印类型
            this.drugBillClassInfo.DrugAttribute.ID = this.cbxPutType.Tag.ToString(); //摆药类型
            this.drugBillClassInfo.IsValid = this.cbxIsValid.Checked;              //是否有效
            this.drugBillClassInfo.Memo = this.txtMark.Text;               //备注
            return this.drugBillClassInfo;
        }

        /// <summary>
        /// 从myDrugBillClass中取数据，显示在tabpage2中。
        /// </summary>
        private void SetDrugBillItem( DrugBillClass drugbill )
        {
            this.drugBillClassInfo = drugbill;

            this.txtName.Text = this.drugBillClassInfo.Name;          //摆药分类名称
            this.cbxPrinttype.Tag = this.drugBillClassInfo.PrintType.ID;  //打印类型
            this.cbxPutType.Tag = this.drugBillClassInfo.DrugAttribute.ID;   //摆药类型
            this.cbxIsValid.Checked = this.drugBillClassInfo.IsValid;       //是否有效
            this.txtMark.Text = this.drugBillClassInfo.Memo;          //备注
            //手术室摆药单不能修改
            if( this.drugBillClassInfo.ID == "P" || this.drugBillClassInfo.ID == "R" )
            {
                this.lvPutDrugBill1.AllowEdit = false;
            }
            else
            {
                this.lvPutDrugBill1.AllowEdit = true;
            }
        }

        #endregion

        #region 初始化

        private void Init()
        {
            //初始化打印类型
            this.cbxPrinttype.AddItems(BillPrintType.List());
            //初始化摆药类型
            this.cbxPutType.AddItems(DrugAttribute.List());
            //隐藏tabpage2
            this.neuTabControl1.TabPages.Remove(this.tabPage2);


            this.lvPutDrugBill1.CheckBoxes = false;
            this.lvPutDrugBill1.MultiSelect = false;

            FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();
            ArrayList drugBillClassList = new ArrayList();
            drugBillClassList = drugStoreMgr.QueryDrugBillClassList();
            if (drugBillClassList == null)
            {
                this.ShowMessage("获取摆药单分类列表发生错误："+drugStoreMgr.Err, MessageBoxIcon.Error);
                return;
            }

            if (drugBillClassList.Count == 0)
            {
                object iBillClass = Components.DrugStore.Inpatient.Common.InterfaceManager.GetDrugBillClass();
                //没有实现接口的使用核心默认的摆药单
                if (iBillClass == null)
                {
                    drugBillClassList = this.SaveDefaultBill();
                }
                //已经本地化了就使用本地化设置
                if (iBillClass is FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IDrugBillClass)
                {
                    List<FS.HISFC.Models.Pharmacy.DrugBillClass> listDrubBillClass = ((FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IDrugBillClass)iBillClass).GetList();
                    if (listDrubBillClass != null)
                    {
                        drugBillClassList.AddRange(listDrubBillClass);
                    }
                    else
                    {
                        this.ShowMessage("获取本地化的摆药单类型列表发生了错误\n请通知系统管理员查看接口FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IDrubBillClass实现", MessageBoxIcon.Error);
                    }
                }
                string errInfo = "";
                if (Function.SendBizMessage(drugBillClassList, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.DrugBillClass, ref errInfo) == -1)
                {
                    this.ShowMessage("发送消息失败，请向系统管理员联系并报告错误：" + errInfo, MessageBoxIcon.Error);
                }
            }
            this.lvPutDrugBill1.ShowDrugBillClassList(drugBillClassList);
        }
       

        #endregion

        #region 数据操作

        /// <summary>
        /// 增加摆药单
        /// </summary>
        private void AddDrugBill()
        {
            if (this.neuTabControl1.TabPages.ContainsKey(tabPage2.Name))
            {
                this.neuTabControl1.TabPages.Remove(this.tabPage2);
            }
            //设置要插入的节点
            DrugBillClass info = new DrugBillClass( );
            info.Name = "新建摆药单";
            info.IsValid = true;

            //在详细信息中显示新增加的摆药单
            this.SetDrugBillItem( info );

            this.neuTabControl1.TabPages.Add( this.tabPage2 );
            this.neuTabControl1.SelectedIndex = 1;
        }

        /// <summary>
        /// 修改摆药单类别
        /// </summary>
        private void ModifyDrugBill( )
        {
            if( this.lvPutDrugBill1.SelectedItems.Count > 0 )
            {
                if (this.drugBillClassInfo.ID == "" || this.drugBillClassInfo.ID == null)
                {
                    this.lvPutDrugBill1.ClearSelection();

                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择要修改的摆药单"));
                    return;
                }

                //显示摆药单编辑信息
                this.neuTabControl1.TabPages.Add( this.tabPage2 );
                this.neuTabControl1.SelectedIndex = 1;
            }
            else
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "请选择要修改的摆药单"));
                //重新设置摆药单属性
                this.neuTabControl1.SelectedIndex = 1;
                this.drugBillClassInfo = new DrugBillClass( );

            }
        }

        /// <summary>
        /// 删除一条摆药单分类数据
        /// </summary>
        private void DeleteDrugBill( )
        {
            //判断是否选中一个摆药单
            if( this.lvPutDrugBill1.SelectedItems.Count > 0 )
            {
                //获取当前摆药单信息
                this.GetDrugBillItem( );
            }
            else
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "请选择您要删除的摆药单！" ));

                return;
            }

            if (this.drugBillClassInfo.ID == "P" || this.drugBillClassInfo.ID == "R")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("退药单及非医嘱摆药单不允许删除"),"",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            if( this.drugBillClassInfo.ID != "" )
            {
    
                //弹出提示窗口
                System.Windows.Forms.DialogResult result;
                result = MessageBox.Show( FS.FrameWork.Management.Language.Msg( "摆药单删除后将不可恢复,您确定要删除【" + drugBillClassInfo.Name + "】摆药单吗？" ), FS.FrameWork.Management.Language.Msg( "删除提示") , System.Windows.Forms.MessageBoxButtons.OKCancel );
                if( result == DialogResult.Cancel ) return;

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm( FS.FrameWork.Management.Language.Msg( "正在删除摆药单及其明细信息，请稍等..." ));
                Application.DoEvents( );

                //删除事务
                int parmClass;
                //int parmList;

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction( FS.FrameWork.Management.Connection.Instance );

                drugStoreManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //先删除摆药单属性信息
                //parmList = drugStoreManager.DeleteDrugBillList( this.drugBillClassInfo.ID );
                //再删除摆药单信息
                parmClass = drugStoreManager.DeleteDrugBillClass( this.drugBillClassInfo.ID );

                if(parmClass == -1 )
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show( this.drugStoreManager.Err , FS.FrameWork.Management.Language.Msg( "FS.FrameWork.Management.Language.Msg( 提示"));
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm( );
                    return;
                }
                else
                {
                    ArrayList alBillClass=new ArrayList();
                    alBillClass.Add(this.drugBillClassInfo);
                    string errInfo="";
                    if (Function.SendBizMessage(alBillClass, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Delete, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.DrugBillClass, ref errInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("删除失败："+errInfo));
                        return;
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "删除成功！") );
                    
                }
            }
            //删除节点
            this.lvPutDrugBill1.DeleteItem( this.lvPutDrugBill1.SelectedIndices[ 0 ] );
            this.lvPutDrugBill1.Focus( );
            this.neuTabControl1.SelectedIndex = 0;
            this.drugBillClassInfo = new DrugBillClass( );
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm( );
        }

        /// <summary>
        /// 保存摆药单类别
        /// </summary>
        private void SaveDrugBill()
        {
            //获取当前摆药单最新的编辑信息
            this.GetDrugBillItem();
            //有效性判断
            if (!this.IsDrugBillDataValid(this.drugBillClassInfo))
            {
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            drugStoreManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            bool isNewDrugBill = false;
            //保存操作
            if (this.drugBillClassInfo.ID == "")
            {
                isNewDrugBill = true;
            }

            int parm = drugStoreManager.SetDrugBillClass(this.drugBillClassInfo);
            if (parm == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(this.drugStoreManager.Err);
                return;
            }

            ArrayList alBillClass = new ArrayList();
            alBillClass.Add(this.drugBillClassInfo);
            string errInfo = "";
            FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType = FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Modify;
            if (isNewDrugBill)
            {
                operType = FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add;
            }
            if (Function.SendBizMessage(alBillClass, operType, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.DrugBillClass, ref errInfo) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存失败：" + errInfo));
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(FS.FrameWork.Management.Language.Msg("保存成功"));

            if (isNewDrugBill)
            {
                this.lvPutDrugBill1.AddItem(this.drugBillClassInfo, true);

                this.drugBillClassInfo = new DrugBillClass();
            }
            else
            {
                //用更新后的节点信息修改ListView中对应的节点
                this.lvPutDrugBill1.ModifyItem(this.drugBillClassInfo, this.lvPutDrugBill1.SelectedIndices[0]);
            }

            this.neuTabControl1.SelectedIndex = 0;
        }      

        /// <summary>
        /// 默认摆药单保存
        /// </summary>
        private ArrayList SaveDefaultBill()
        {
            ArrayList al = new ArrayList();

            FS.HISFC.Models.Pharmacy.DrugBillClass rDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            rDrugBill.ID = "R";
            rDrugBill.Name = "退药单";                       //摆药分类名称
            rDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //打印类型
            rDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            rDrugBill.IsValid = true;              //是否有效
            rDrugBill.Memo = "系统必须包含的摆药单，不能更改、删除";               //备注
            al.Add(rDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass pDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            pDrugBill.ID = "P";
            pDrugBill.Name = "非医嘱摆药单";                       //摆药分类名称
            pDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //打印类型
            pDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            pDrugBill.IsValid = true;              //是否有效
            pDrugBill.Memo = "系统必须包含的摆药单，不能更改、删除";               //备注
            al.Add(pDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass cDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            cDrugBill.ID = "C";
            cDrugBill.Name = "草药摆药单";                       //摆药分类名称
            cDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //打印类型
            cDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            cDrugBill.IsValid = true;              //是否有效
            cDrugBill.Memo = "系统默认摆药单";               //备注
            al.Add(cDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass oDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            oDrugBill.ID = "O";
            oDrugBill.Name = "出院带药摆药单";                       //摆药分类名称
            oDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //打印类型
            oDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            oDrugBill.IsValid = true;              //是否有效
            oDrugBill.Memo = "系统默认摆药单";               //备注
            al.Add(oDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass aDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            aDrugBill.ID = "A";
            aDrugBill.Name = "综合摆药单";                       //摆药分类名称
            aDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //打印类型
            aDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            aDrugBill.IsValid = true;              //是否有效
            aDrugBill.Memo = "系统必须包含的摆药单，不能更改、删除";               //备注
            al.Add(aDrugBill);


            FS.HISFC.Models.Pharmacy.DrugBillClass dcDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            dcDrugBill.ID = "DC";
            dcDrugBill.Name = "西药长期医嘱口服摆药单";                       //摆药分类名称
            dcDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //打印类型
            dcDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            dcDrugBill.IsValid = true;              //是否有效
            dcDrugBill.Memo = "系统默认的摆药单";               //备注
            al.Add(dcDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass dlDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            dlDrugBill.ID = "DL";
            dlDrugBill.Name = "西药临时医嘱口服摆药单";                       //摆药分类名称
            dlDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //打印类型
            dlDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            dlDrugBill.IsValid = true;              //是否有效
            dlDrugBill.Memo = "系统默认的摆药单";               //备注
            al.Add(dlDrugBill);


            FS.HISFC.Models.Pharmacy.DrugBillClass tcDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            tcDrugBill.ID = "TC";
            tcDrugBill.Name = "西药长期医嘱针剂摆药单";                       //摆药分类名称
            tcDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.T;           //打印类型
            tcDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            tcDrugBill.IsValid = true;              //是否有效
            tcDrugBill.Memo = "系统默认的摆药单";               //备注
            al.Add(tcDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass tlDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            tlDrugBill.ID = "TL";
            tlDrugBill.Name = "西药临时医嘱针剂摆药单";                       //摆药分类名称
            tlDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.T;           //打印类型
            tlDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            tlDrugBill.IsValid = true;              //是否有效
            tlDrugBill.Memo = "系统默认的摆药单";               //备注
            al.Add(tlDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass gDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            gDrugBill.ID = "G";
            gDrugBill.Name = "大输液摆药单";                       //摆药分类名称
            gDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.T;           //打印类型
            gDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            gDrugBill.IsValid = true;              //是否有效
            gDrugBill.Memo = "系统默认的摆药单";               //备注
            al.Add(gDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass sDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            sDrugBill.ID = "S";
            sDrugBill.Name = "毒麻精摆药单";                       //摆药分类名称
            sDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //打印类型
            sDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            sDrugBill.IsValid = true;              //是否有效
            sDrugBill.Memo = "系统默认的摆药单";               //备注
            al.Add(sDrugBill);

            FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            foreach (FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass in al)
            {
                if (drugStoreManager.InsertOneDrugBillClass(drugBillClass) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return null;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            return al;
        }
        #endregion

        #endregion

        #region 事件
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad( EventArgs e )
        {
            try
            {               
                this.Init();
            }
            catch { }

            base.OnLoad( e );
        }

        /// <summary>
        /// 保存摆药单类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click( object sender , EventArgs e )
        {
            this.SaveDrugBill( );
        }

        /// <summary>
        /// 选择摆药单信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvPutDrugBill1_SelectedIndexChanged( object sender , EventArgs e )
        {
            if( this.lvPutDrugBill1.SelectedItems.Count > 0 )
            {
                //置所有的非当前摆药单为未选中状态
                foreach( ListViewItem lvi in this.lvPutDrugBill1.CheckedItems )
                {
                    lvi.Checked = false;
                }
                this.lvPutDrugBill1.SelectedItems[ 0 ].Checked = true;
                //设置当前摆药单信息
                this.SetDrugBillItem( this.lvPutDrugBill1.SelectedItems[ 0 ].Tag as DrugBillClass );
                if( this.drugBillClassInfo.ID != null )
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(FS.FrameWork.Management.Language.Msg( "正在生成预览信息..." ));
                    Application.DoEvents( );
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm( );
                }
            }
            else
            {
                //重新设置摆药单属性
                this.drugBillClassInfo = new DrugBillClass( );
            }
        }

        /// <summary>
        /// tabpage切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTabControl1_SelectedIndexChanged( object sender , EventArgs e )
        {
            if (this.neuTabControl1.SelectedIndex == 0)
            {
                this.neuTabControl1.TabPages.Remove(this.tabPage2);
            }
        }

        #endregion

        #region 工具栏信息

        /// <summary>
        /// 定义工具栏服务
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService( );
        /// <summary>
        /// 初始化工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="NeuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit( object sender , object NeuObject , object param )
        {
            //增加工具栏
            this.toolBarService.AddToolButton( "增加" , "增加摆药单" , FS.FrameWork.WinForms.Classes.EnumImageList.T添加 , true , false , null );
            this.toolBarService.AddToolButton( "编辑" , "编辑摆药单" , FS.FrameWork.WinForms.Classes.EnumImageList.X修改 , true , false , null );
            this.toolBarService.AddToolButton( "删除" , "删除摆药单" , FS.FrameWork.WinForms.Classes.EnumImageList.S删除 , true , false , null );
            return this.toolBarService;

            
        }

        /// <summary>
        /// 工具栏按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked( object sender , ToolStripItemClickedEventArgs e )
        {
            switch( e.ClickedItem.Text )
            {
                case "增加":
                    this.AddDrugBill( );
                    break;
                case "编辑":
                    this.ModifyDrugBill( );
                    break;
                case "删除":
                    this.DeleteDrugBill( );
                    break;
                default:
                    break;
            }

        }
        #endregion

        /// <summary>
        /// MessageBox统一形式
        /// </summary>
        /// <param name="text"></param>
        public void ShowMessage(string text, MessageBoxIcon messageBoxIcon)
        {
            string caption = "";
            switch (messageBoxIcon)
            {
                case MessageBoxIcon.Warning:
                    caption = "警告>>";
                    break;
                case MessageBoxIcon.Error:
                    caption = "错误>>";
                    break;
                default:
                    caption = "提示>>";
                    break;
            }

            MessageBox.Show(this, text, caption, MessageBoxButtons.OK, messageBoxIcon);
        }
    }
}
