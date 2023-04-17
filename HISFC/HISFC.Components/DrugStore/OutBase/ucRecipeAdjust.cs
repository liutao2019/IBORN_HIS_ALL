using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.DrugStore.OutBase
{
    /// <summary>
    /// [功能描述: 门诊处方调剂]<br></br>
    /// [创 建 者: 杨永刚]<br></br>
    /// [创建时间: 2006-11-30]<br></br>
    /// [说明:平均调剂主要看当日已发送处方品种数、对于竞争调剂主要看当日待配药处方品种数、当日均分次数]
    /// <修改记录 
    ///		修改人='梁俊泽' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的='增加调剂依据/调剂方式'
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucRecipeAdjust : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucRecipeAdjust( )
        {
            InitializeComponent( );
        }

        #region 变量

        /// <summary>
        /// 业务管理类
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore( );

        /// <summary>
        /// 操作员权限科室
        /// </summary>
        FS.FrameWork.Models.NeuObject privDept = new FS.FrameWork.Models.NeuObject( );

        /// <summary>
        /// 是否有权限编辑
        /// </summary>
        private bool isPrivilegeEdit = false;

        #endregion

        #region 属性

        /// <summary>
        /// 是否允许编辑
        /// </summary>
        public bool IsEdit
        {
            get
            {
                return this.isPrivilegeEdit;
            }
            set
            {
                this.isPrivilegeEdit = value;
                this.toolBarService.SetToolButtonEnabled( "保存" , value );
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化配药调剂方式 0 平均 1 竞争
        /// </summary>
        public void ShowAdjustType( )
        {
            //FS.HISFC.BizLogic.Manager.Controler controlerManager = new FS.HISFC.BizLogic.Manager.Controler( );
            //string ctrl = controlerManager.QueryControlerInfo( "500006" );
            //if( ctrl == null || ctrl == "-1" || ctrl == "0" )
            //{
            //    this.rbAverage.Checked = true;
            //    this.adjustType = "0";
            //}
            //else
            //{
            //    this.rbCompete.Checked = true;
            //    this.adjustType = "1";
            //}

            FS.FrameWork.Management.ExtendParam extManager = new ExtendParam();
            FS.HISFC.Models.Base.ExtendInfo deptExt = extManager.GetComExtInfo(FS.HISFC.Models.Base.EnumExtendClass.DEPT, "TerminalAdjust", this.privDept.ID);
            if (deptExt == null)
            {
                MessageBox.Show(Language.Msg("获取科室扩展属性内配药调剂参数失败！"));
                return;
            }

            if (deptExt.StringProperty == "1")		//竞争
            {
                this.rbCompete.Checked = true;
            }
            else
            {
                this.rbAverage.Checked = true;
            }

            deptExt = extManager.GetComExtInfo(FS.HISFC.Models.Base.EnumExtendClass.DEPT,"AdjustGist", this.privDept.ID);
            if (deptExt == null)
            {
                MessageBox.Show(Language.Msg("获取科室扩展属性内配药调剂依据设定失败！"));
                return;
            }

            if (deptExt.StringProperty == "1")		//发药
            {
                this.rbSend.Checked = true;
            }
            else
            {
                this.rbDrug.Checked = true;
            }
        }

        /// <summary>
        /// 初始化配药台信息
        /// </summary>
        public void ShowTerminalInfo( )
        {
            ArrayList al = this.drugStoreManager.QueryDrugTerminalByDeptCode( this.privDept.ID , FS.FrameWork.Function.NConvert.ToInt32(FS.HISFC.Models.Pharmacy.EnumTerminalType.配药台).ToString());
            if( al == null )
            {
                MessageBox.Show( Language.Msg("获取配药台列表出错") + this.drugStoreManager.Err );
                return;
            }

            this.neuSpread1_Sheet1.Rows.Count = al.Count;
            FS.HISFC.Models.Pharmacy.DrugTerminal info = null;
            for( int i = 0 ; i < al.Count ; i++ )
            {
                info = al[ i ] as FS.HISFC.Models.Pharmacy.DrugTerminal;
                //配药台编码
                this.neuSpread1_Sheet1.Cells[ i , 0 ].Text = info.ID;
                //配药台名称
                this.neuSpread1_Sheet1.Cells[ i , 1 ].Text = info.Name;
                //是否关闭
                this.neuSpread1_Sheet1.Cells[ i , 2 ].Text = info.IsClose ? "是" : "否";

                if( info.IsClose )
                {
                    this.neuSpread1_Sheet1.Rows[ i ].ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    this.neuSpread1_Sheet1.Rows[ i ].ForeColor = System.Drawing.SystemColors.WindowText;
                }
                //已发送品种数
                this.neuSpread1_Sheet1.Cells[ i , 3 ].Text = info.SendQty.ToString( );
                //待配药品种数
                this.neuSpread1_Sheet1.Cells[ i , 4 ].Text = info.DrugQty.ToString( );
                //竞争调剂的均分次数
                this.neuSpread1_Sheet1.Cells[ i , 5 ].Text = info.Average.ToString( );

                this.neuSpread1_Sheet1.Rows[ i ].Tag = info;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void SaveData( )
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.FrameWork.Management.ExtendParam extManager = new ExtendParam();

            //Transaction t = new Transaction( Connection.Instance );
            //t.BeginTransaction( );

            this.drugStoreManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //extManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //更新调剂方式
            if (!this.SaveAdjustParam(extManager))
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return;
            }
            //更新配药台
            if( !this.SaveTerminalParam( ) )
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(Language.Msg( "保存成功" ));
        }

        /// <summary>
        /// 更新配药台
        /// </summary>
        /// <returns></returns>
        private bool SaveTerminalParam(  )
        {
            FS.HISFC.Models.Pharmacy.DrugTerminal info = null;

            decimal sendQty = 0;
            decimal drugQty = 0;
            decimal averageNum = 0;

            for( int i = 0 ; i < this.neuSpread1_Sheet1.Rows.Count ; i++ )
            {
                info = this.neuSpread1_Sheet1.Rows[ i ].Tag as FS.HISFC.Models.Pharmacy.DrugTerminal;
                //已发送品种数
                sendQty = FS.FrameWork.Function.NConvert.ToDecimal( this.neuSpread1_Sheet1.Cells[ i , 3 ].Text ) - info.SendQty;
                if (sendQty > 999)
                {
                    MessageBox.Show(Language.Msg(info.Name + " 已发送数量必须介于0与999之间"));
                    return false;
                }
                //待配药品种数
                drugQty = FS.FrameWork.Function.NConvert.ToDecimal( this.neuSpread1_Sheet1.Cells[ i , 4 ].Text ) - info.DrugQty;
                if (drugQty > 999)
                {
                    MessageBox.Show(Language.Msg(info.Name + " 待配药品种数必须介于0与999之间"));
                    return false;
                }
                //竞争调剂的均分次数
                averageNum = FS.FrameWork.Function.NConvert.ToDecimal( this.neuSpread1_Sheet1.Cells[ i , 5 ].Text ) - info.Average;
                if (averageNum > 999)
                {
                    MessageBox.Show(Language.Msg(info.Name + " 竞争调剂的均分次数必须介于0与999之间"));
                    return false;
                }
                //更新配药台信息
                if( this.drugStoreManager.UpdateTerminalAdjustInfo( info.ID , sendQty , drugQty , averageNum ) == -1 )
                {
                    MessageBox.Show(Language.Msg( "更新第") + ( i + 1 ).ToString( ) + Language.Msg("条配药台记录失败") + this.drugStoreManager.Err );
                    return false;
                }
            }


            return true;
        }

        /// <summary>
        /// 更新调剂方式
        /// </summary>
        /// <param name="controlerManager"></param>
        /// <returns></returns>
        private bool SaveAdjustParam(FS.FrameWork.Management.ExtendParam extManager)
        {
            #region 保存调剂方式

            FS.HISFC.Models.Base.ExtendInfo extAdjustType = extManager.GetComExtInfo(FS.HISFC.Models.Base.EnumExtendClass.DEPT,"TerminalAdjust", this.privDept.ID);
            if (extAdjustType == null)
            {
                MessageBox.Show(Language.Msg("获取科室扩展属性内配药调剂参数失败！"));
                return false;
            }            

            if (extAdjustType.Item.ID == "")
            {
                #region 无此参数 初次插入
                extAdjustType.Item.ID = this.privDept.ID;
                extAdjustType.StringProperty = this.rbAverage.Checked ? "0" : "1"; ;
                extAdjustType.PropertyCode = "TerminalAdjust";
                extAdjustType.PropertyName = "门诊配药终端调剂方式0平均 1竞争";

                if (extManager.SetComExtInfo(extAdjustType) == -1)
                {
                    MessageBox.Show(Language.Msg("更新科室扩展属性内配药终端调剂方式失败！"));
                    return false;
                }
                #endregion
            }
            else
            {
                extAdjustType.Item.ID = this.privDept.ID;
                extAdjustType.StringProperty = this.rbAverage.Checked ? "0" : "1"; ;
                extAdjustType.PropertyCode = "TerminalAdjust";
                extAdjustType.PropertyName = "门诊配药终端调剂方式0平均 1竞争";

                if (extManager.SetComExtInfo(extAdjustType) == -1)
                {
                    MessageBox.Show(Language.Msg("更新科室扩展属性内配药终端调剂方式失败！"));
                    return false;
                }
            }

            #endregion

            #region 保存调剂依据

            FS.HISFC.Models.Base.ExtendInfo extAdjustGist = extManager.GetComExtInfo(FS.HISFC.Models.Base.EnumExtendClass.DEPT,"AdjustGist", this.privDept.ID);
            if (extAdjustGist == null)
            {
                MessageBox.Show(Language.Msg("获取科室扩展属性内配药调剂依据失败！"));
                return false;
            }

            if (extAdjustGist.Item.ID == "")
            {
                #region 无此参数 初次插入

                extAdjustGist.Item.ID = this.privDept.ID;
                extAdjustGist.StringProperty = this.rbDrug.Checked ? "0" : "1"; ;
                extAdjustGist.PropertyCode = "AdjustGist";
                extAdjustGist.PropertyName = "门诊配药终端调剂依据 1 发药 0 配药";

                if (extManager.SetComExtInfo(extAdjustGist) == -1)
                {
                    MessageBox.Show("更新科室扩展属性内配药终端调剂依据失败！");
                    return false;
                }
                #endregion
            }
            else
            {
                extAdjustGist.Item.ID = this.privDept.ID;
                extAdjustGist.StringProperty = this.rbDrug.Checked ? "0" : "1"; ;
                extAdjustGist.PropertyCode = "AdjustGist";
                extAdjustGist.PropertyName = "门诊配药终端调剂依据 1 发药 0 配药";

                if (extManager.SetComExtInfo(extAdjustGist) == -1)
                {
                    MessageBox.Show("更新科室扩展属性内配药终端调剂依据失败！");
                    return false;
                }
            }

            #endregion

            return true;

            #region 屏蔽原保存方式

            ////判断调剂方式是否变化，没有变化返回
            //bool isChange = false;
            //if( ( this.adjustType == "0" ) && this.rbCompete.Checked )
            //{
            //    isChange = true;
            //}
            //if( ( this.adjustType == "1" ) && this.rbAverage.Checked )
            //{
            //    isChange = true;
            //}
            //if( isChange )
            //{
            //    FS.HISFC.Models.Base.Controler controler = new FS.HISFC.Models.Base.Controler( );
            //    controler.ID = "500006";
            //    controler.Name = "配药调剂方式 0 平均 1 竞争";
            //    controler.ControlerValue = this.rbAverage.Checked ? "0" : "1";
            //    controler.VisibleFlag = true;
            //    int parm = controlerManager.UpdateControlerInfo( controler );
            //    if( parm == -1 )
            //    {
            //        MessageBox.Show( Language.Msg("更新配药调剂方式出错") + controlerManager.Err );
            //        return false;
            //    }
            //    else if( parm == 0 )
            //    {
            //        if( controlerManager.AddControlerInfo( controler ) == -1 )
            //        {
            //            MessageBox.Show( Language.Msg("更新配药调剂方式出错") + controlerManager.Err );
            //            return false;
            //        }
            //    }

            //}
            //return true;

            #endregion
        }

        #endregion

        #region 事件

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad( EventArgs e )
        {
            //取操作员权限科室（暂时以所在科室代替 ）
            this.privDept = ( ( FS.HISFC.Models.Base.Employee )this.drugStoreManager.Operator ).Dept;

            //判断是否有模版维护权限
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager userManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager( );
            List<FS.FrameWork.Models.NeuObject> alPrivDetail = userManager.QueryUserPrivCollection( this.drugStoreManager.Operator.ID , "0350" , this.privDept.ID );
            if( alPrivDetail != null )
            {
                foreach( FS.FrameWork.Models.NeuObject privInfo in alPrivDetail )
                {
                    //门诊终端维护权限
                    if( privInfo.ID == "01" )
                    {
                        this.isPrivilegeEdit = true;
                        break;
                    }
                }
            }
            else
            {
                this.isPrivilegeEdit = true;
            }

            this.IsEdit = this.isPrivilegeEdit;

            this.ShowAdjustType( );
            this.ShowTerminalInfo( );

            //改变FarPoint的回车事件，使回车转到下一行当前列
            FarPoint.Win.Spread.InputMap im;
            im = this.neuSpread1.GetInputMap( FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused );
            im.Put( new FarPoint.Win.Spread.Keystroke( Keys.Enter , Keys.None ) , FarPoint.Win.Spread.SpreadActions.MoveToNextRow );

            this.neuSpread1.EditModeReplace = true;

            FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType markNumCell = new FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType();
            markNumCell.DecimalPlaces = 0;
            markNumCell.MinimumValue = 0;
            markNumCell.MaximumValue = 9999;

            this.neuSpread1_Sheet1.Columns[3].CellType = markNumCell;
            this.neuSpread1_Sheet1.Columns[4].CellType = markNumCell;
            this.neuSpread1_Sheet1.Columns[5].CellType = markNumCell;

            base.OnLoad( e );
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave( object sender , object neuObject )
        {
            this.SaveData( );

            return base.OnSave( sender , neuObject );
        }

        /// <summary>
        /// 平均调剂
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbAverage_CheckedChanged( object sender , EventArgs e )
        {
            if( this.rbAverage.Checked )
            {
                //已发送品种数
                this.neuSpread1_Sheet1.Columns.Get( 3 ).Locked = false;
                //待配药品种数
                this.neuSpread1_Sheet1.Columns.Get( 4 ).Locked = true;
                //竞争调剂的均分次数
                this.neuSpread1_Sheet1.Columns.Get( 5 ).Locked = true;
            }

        }

        /// <summary>
        /// 竞争调剂
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbCompete_CheckedChanged( object sender , EventArgs e )
        {
            if( this.rbCompete.Checked )
            {
                //已发送品种数
                this.neuSpread1_Sheet1.Columns.Get( 3 ).Locked = true;
                //待配药品种数
                this.neuSpread1_Sheet1.Columns.Get( 4 ).Locked = false;
                //竞争调剂的均分次数
                this.neuSpread1_Sheet1.Columns.Get( 5 ).Locked = false;
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
            this.toolBarService.AddToolButton( "刷新" , "刷新" , 0 , true , false , null );
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
                case "刷新":

                    this.ShowTerminalInfo( );

                    this.ShowAdjustType();

                    break;
            }

        }

        #endregion


    }
}
