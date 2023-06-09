using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.Nurse.Controls
{
    /// <summary>
    /// [功能描述: 病床列表]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2006-11-30]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    public partial class ucBedListView : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucBedListView()
        {
            InitializeComponent();
        }

        #region 变量
        public event ListViewItemSelectionChangedEventHandler ListViewItemChanged;

        protected ArrayList alBeds = null;

        /// <summary>
        /// 病区病床列表
        /// </summary>
        protected ArrayList AlBeds
        {
            get
            {
                {
                    //***************获得病床列表*************
                    FS.HISFC.Models.Base.Employee empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

                    //取本护理站可用的床位列表:暂时取的是本病区全部的床位列表
                    alBeds = manager.QueryBedList( empl.Dept.ID );
                }
                return alBeds;
            }
        }

        protected FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();
        protected FS.HISFC.BizLogic.RADT.OutPatient radtOutPatientManager = new FS.HISFC.BizLogic.RADT.OutPatient();
        protected FS.HISFC.BizLogic.Registration.Register regManager = new FS.HISFC.BizLogic.Registration.Register();

        protected FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();

        protected FS.HISFC.Components.Common.Controls.tvPatientList tv = null;//当前患者树

        private string Err;
        System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip();
        #endregion

        #region 属性
        protected bool bShowDept = false;
        /// <summary>
        /// 
        /// </summary>
        public bool IsShowDeptName
        {
            get
            {
                return bShowDept;
            }
            set
            {
                bShowDept = value;
            }
        }
        #endregion

        #region 函数
        /// <summary>
        /// 刷新列表
        /// </summary>
        public void RefreshView()
        {
            this.lsvBedView.BeginUpdate();
            //显示数据
            this.PaintListView();
            this.lsvBedView.EndUpdate();
        }


        /// <summary>
        /// 创建ListView的列
        /// </summary>
        private void CreateHeaders()
        {
            //
            ColumnHeader colHead;
            colHead = new ColumnHeader();
            colHead.Text = "患者姓名";
            colHead.Width = 150;
            this.lsvBedView.Columns.Add( colHead );
            //
            colHead = new ColumnHeader();
            colHead.Text = "性别";
            colHead.Width = 40;
            this.lsvBedView.Columns.Add( colHead );
            //
            colHead = new ColumnHeader();
            colHead.Text = "病历号";
            colHead.Width = 80;
            this.lsvBedView.Columns.Add( colHead );
            //
            colHead = new ColumnHeader();
            colHead.Text = "科室";
            colHead.Width = 100;
            this.lsvBedView.Columns.Add( colHead );
            //
            colHead = new ColumnHeader();
            colHead.Text = "状态";
            colHead.Width = 100;
            this.lsvBedView.Columns.Add( colHead );

        }


        /// <summary>
        /// 显示数据
        /// </summary>
        protected virtual void PaintListView()
        {
            //清空
            this.lsvBedView.Items.Clear();

            try
            {
                //循环将床位添加到列表中
                foreach (FS.HISFC.Models.Base.Bed bed in this.AlBeds)
                {
                    ListViewItem lst = this.GetListViewInfo( bed );
                    if (lst != null)
                        this.lsvBedView.Items.Add( lst );

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message );
                return;
            }
        }


        /// <summary>
        /// 显示患者信息
        /// 当双击床位列表时执行此方法
        /// </summary>
        private void ShowPatientInfo()
        {
            if (this.lsvBedView.SelectedItems.Count > 0)
            {
                FS.HISFC.Models.Registration.Register patient = this.lsvBedView.SelectedItems[0].Tag as FS.HISFC.Models.Registration.Register;

                if (patient != null && tv != null)
                {
                    //取患者信息在树型列表中的节点位置
                    TreeNode node = this.tv.FindNode( 0, patient );
                    //触发树型节点选中找到的节点
                    if (node != null)
                        this.tv.SelectedNode = node;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public override FS.FrameWork.WinForms.Forms.ToolBarService Init(object sender, object neuObject, object param)
        {
            try
            {
                this.tv = sender as FS.HISFC.Components.Common.Controls.tvPatientList;
            }
            catch
            {
            }
            //清屏
            this.lsvBedView.Clear();
            //创建列
            this.CreateHeaders();
            this.RefreshView();
            return null;
        }

        /// <summary>
        /// 根据床位信息生成ListView的节点
        /// </summary>
        /// <param name="bed"></param>
        /// <returns></returns>
        private ListViewItem GetListViewInfo(FS.HISFC.Models.Base.Bed bed)
        {
            System.Windows.Forms.ListViewItem lvi = new ListViewItem();

            //去掉床号的前四位,取其余的字符串 
            string tempBedNo = bed.ID.Length > 4 ? bed.ID.Substring( 4 ) : bed.ID;

            //Get patientinfo
            FS.HISFC.Models.Registration.Register patient = null;
            //如果存在患者,则将患者信息保存在lvi的Tag属性中(包床也是这样处理)
            if (bed.InpatientNO.Trim() != "N" && bed.InpatientNO.Trim() != "")
            {
                //取患者基本信息
                patient = this.regManager.GetByClinic( bed.InpatientNO );
                if (patient == null || patient.ID == "")
                {
                    MessageBox.Show( bed.InpatientNO + FS.FrameWork.Management.Language.Msg( "患者没找到！" ) );

                }
                try
                {
                    //如果是包床,则将床位付给此患者
                    if (bed.Status.ID.ToString() == FS.HISFC.Models.Base.EnumBedStatus.W.ToString())
                    {
                        bed.Memo = patient.Name;
                        lvi.Tag = bed;
                    }
                    else
                    {
                        patient.PVisit.PatientLocation.Bed = bed;
                        lvi.Tag = patient;
                    }
                    lvi.SubItems.Clear();
                    lvi.SubItems.Add( patient.Sex.Name );
                    lvi.SubItems.Add( patient.PID.PatientNO );
                    lvi.SubItems.Add( patient.PVisit.PatientLocation.Dept.Name );
                    lvi.SubItems.Add( bed.Status.Name );

                    //根据参数是否显示科室名称
                    if (bShowDept)
                    {
                        lvi.Text = bed.Dept.Name + "【" + tempBedNo + "】" + patient.Name;
                    }
                    else
                    {
                        lvi.Text = "【" + tempBedNo + "】\n" + patient.Name;
                    }
                }
                catch
                {
                }
            }
            else
            {
                //将床位信息保存在lvi的Tag属性中
                lvi.Tag = bed;
                lvi.SubItems.Clear();
                lvi.SubItems.Add( bed.Sex.Name );
                lvi.SubItems.Add( "" );
                lvi.SubItems.Add( bed.NurseStation.Name );
                lvi.SubItems.Add( bed.Status.Name );
                lvi.Text = "【" + tempBedNo + "】";
            }


            //如果床位被占用(有患者,包床,挂床,请假)
            if (bed.InpatientNO != "N" &&
                bed.Status.ID.ToString() != "W" &&
                bed.Status.ID.ToString() != "R"
                && bed.Status.ID.ToString() != "H")
            {
                //lvi.ImageIndex = GetIconIndex(patient.Disease.Tend.Name);
                lvi.ImageIndex = 4;
            }
            else
            {
                lvi.ImageIndex = GetIconIndex( bed.Sex.ID.ToString(), bed );
            }


            return lvi;
        }


        /// <summary>
        /// 通过床位属性和状态返回图片index
        /// </summary>
        /// <param name="property"></param>
        /// <param name="bed"></param>
        /// <returns></returns>
        private int GetIconIndex(string property, FS.HISFC.Models.Base.Bed bed)
        {
            int n = 0;
            if (bed.IsPrepay)
            {//预约
                n = 12;
            }
            else
            {//正常不预约的床
                switch (bed.Status.ID.ToString())
                {
                    case "C"://close
                        n = 3;
                        break;
                    case "H"://挂床
                        n = 11;
                        break;
                    case "O"://占用
                        //Occupied 
                        n = 4;
                        break;
                    case "U":
                        //Unoccupied 
                        n = 0;
                        break;
                    case "K":
                        n = 13;
                        //污染的
                        break;
                    case "I":
                        n = 13;
                        //隔离的
                        break;
                    case "W"://包床
                        n = 9;
                        break;
                    //放假
                    case "R":
                        n = 10;
                        break;
                    default:
                        n = 0;
                        break;

                }
            }
            if (n == 0)
            {
                switch (property)
                {
                    case "M":
                        n = 2;
                        break;
                    case "F":
                        n = 1;
                        break;
                    default:
                        n = 0;
                        break;
                }
            }
            return n;

        }


        /// <summary>
        /// 通过患者护理属性返回图片index
        /// </summary>
        /// <param name="nursetype"></param>
        /// <returns></returns>
        private int GetIconIndex(string nursetype)
        {
            int n;
            switch (nursetype)
            {
                case "一级护理":// 1 grade
                    n = 6;
                    break;
                case "二级护理"://2 grade
                    n = 5;
                    break;
                case "三级护理"://3 grade
                    n = 4;
                    break;
                case "病危": //病危
                    n = 7;
                    break;
                case "重症"://重症
                    n = 8;
                    break;
                default:
                    n = 4;
                    break;
            }
            return n;
        }



        /// <summary>
        /// 控制设置菜单List的显示方式
        /// </summary>
        /// <param name="flag"></param>
        private void MenuSetControl(bool flag)
        {
            //床位列表可见时,不显示"卡片"菜单;否则显示"卡片"菜单.
            if (this.lsvBedView.Visible)
            {
                switch (this.lsvBedView.View)
                {
                    case View.LargeIcon:
                        this.mnuI_Large.Checked = flag;
                        break;
                    case View.SmallIcon:
                        this.mnuI_Small.Checked = flag;
                        break;
                    case View.List:
                        this.mnuI_List.Checked = flag;
                        break;
                    case View.Details:
                        this.mnuI_Detail.Checked = flag;
                        break;
                    default:
                        this.mnuI_Large.Checked = flag;
                        break;
                }
                this.mnuI_card.Checked = false;
            }
            else
            {
                this.mnuI_Large.Checked = false;
                this.mnuI_Small.Checked = false;
                this.mnuI_List.Checked = false;
                this.mnuI_Detail.Checked = false;
                this.mnuI_card.Checked = true;
            }

        }

        /// <summary>
        /// 将床位菜单中的check属性置为false
        /// </summary>
        private void Clearmenu()
        {
            for (int i = 0; i < this.mnuSet.Items.Count; i++)
            {
                ((ToolStripMenuItem)this.mnuSet.Items[i]).Checked = false;
            }
        }


        ///// <summary>
        ///// 
        ///// </summary>
        //private void ControlAdd()
        //{
        //    if (this.lsvBedView.SelectedItems[0].Tag.GetType().ToString() == "FS.HISFC.Models.RADT.PatientInfo")
        //    {
        //        FS.HISFC.Models.RADT.PatientInfo patient = (FS.HISFC.Models.RADT.PatientInfo)this.lsvBedView.SelectedItems[0].Tag;
        //        this.ucBedChange1.NurseCell = this.NurseCode;
        //        this.ucBedChange1.myPatientInfo = patient;

        //        FS.FrameWork.Interface.Classes.Function.PopShowControl(this.ucBedChange1);
        //        if (FS.FrameWork.Interface.Classes.Function.PopForm.DialogResult == DialogResult.OK)
        //        {
        //            //刷新床位列表
        //            this.RefreshView();
        //        }
        //    }

        //}


        /// <summary>
        /// 换床处理
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private int ChangeItems(int a, int b)
        {
            /*int parm;
            FS.HISFC.Models.RADT.PatientInfo obj_a, obj_b;

            //如果是同一个人,则返回
            if (a == b) return -1;

            //两人对调床位
            if (this.lsvBedView.Items[a].Tag.GetType().ToString() == "FS.HISFC.Models.RADT.PatientInfo" && this.lsvBedView.Items[b].Tag.GetType().ToString() == "FS.HISFC.Models.RADT.PatientInfo")
            {
                obj_a = (FS.HISFC.Models.RADT.PatientInfo)this.lsvBedView.Items[a].Tag;
                obj_b = (FS.HISFC.Models.RADT.PatientInfo)this.lsvBedView.Items[b].Tag;

                if (obj_a.PVisit.PatientLocation.Bed.Status.ID.ToString() == "W" || obj_a.PVisit.PatientLocation.Bed.Status.ID.ToString() == "W")
                {
                    MessageBox.Show("被调换的床位其一状态为包床，不能调换！", "提示：");
                    return -1;
                }
                //
                if (MessageBox.Show("是否预将“" + obj_a.Name + "”与“" + obj_b.Name + "”调床", "提示：", System.Windows.Forms.MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Cancel) return -1;
                //
                try
                {
                    FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                    SQLCA.BeginTransaction();
                    this.radtManager.SetTrans(SQLCA.Trans);
                    //两患者床位对调处理
                    parm = this.radtManager.SwapPatientBed(obj_a, obj_b);
                    if (parm == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.Err = "换床失败！\n" + this.radtManager.Err;
                        return -1;
                    }
                    else if (parm == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.Err = "换床失败! \n患者信息有变动,请刷新当前窗口";
                        return -1;
                    }

                    SQLCA.Commit();
                    base.OnRefreshTree();

                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    return -1;
                }

                return 1;

            }
            else if (this.lsvBedView.Items[a].Tag.GetType().ToString() == "FS.HISFC.Models.RADT.PatientInfo")
            {
                //患者a换到b床
                return (this.TransPatientToBed(a, b));

            }
            else if (this.lsvBedView.Items[b].Tag.GetType().ToString() == "FS.HISFC.Models.RADT.PatientInfo")
            {
                //患者b换到a床
                return (this.TransPatientToBed(b, a));
            }
            


            return 0;*/
            int parm;
            FS.HISFC.Models.Registration.Register obj_a, obj_b;

            //如果是同一个人,则返回
            if (a == b)
                return -1;

            //两人对调床位
            if (this.lsvBedView.Items[a].Tag.GetType().ToString() == "FS.HISFC.Models.Registration.Register" && this.lsvBedView.Items[b].Tag.GetType().ToString() == "FS.HISFC.Models.Registration.Register")
            {
                obj_a = (FS.HISFC.Models.Registration.Register)this.lsvBedView.Items[a].Tag;
                obj_b = (FS.HISFC.Models.Registration.Register)this.lsvBedView.Items[b].Tag;

                if (obj_a.PVisit.PatientLocation.Bed.Status.ID.ToString() == "W" || obj_a.PVisit.PatientLocation.Bed.Status.ID.ToString() == "W")
                {
                    MessageBox.Show( "被调换的床位其一状态为包床，不能调换！", "提示：" );
                    return -1;
                }
                //
                if (MessageBox.Show( "是否预将“" + obj_a.Name + "”与“" + obj_b.Name + "”调床", "提示：", System.Windows.Forms.MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2 ) == System.Windows.Forms.DialogResult.Cancel)
                    return -1;
                //
                try
                {

                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                    //SQLCA.BeginTransaction();

                    this.radtOutPatientManager.SetTrans( FS.FrameWork.Management.PublicTrans.Trans );

                    //两患者床位对调处理
                    parm = 0;
                    parm = this.radtOutPatientManager.SwapPatientBed( obj_a, obj_b );
                    if (parm == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.Err = "换床失败！\n" + this.radtManager.Err;
                        return -1;
                    }
                    else if (parm == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.Err = "换床失败! \n患者信息有变动,请刷新当前窗口";
                        return -1;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();
                    base.OnRefreshTree();

                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    return -1;
                }

                return 1;

            }
            else if (this.lsvBedView.Items[a].Tag.GetType().ToString() == "FS.HISFC.Models.Registration.Register")
            {
                //患者a换到b床
                return (this.TransPatientToBed( a, b ));

            }
            else if (this.lsvBedView.Items[b].Tag.GetType().ToString() == "FS.HISFC.Models.Registration.Register")
            {
                //患者b换到a床
                return (this.TransPatientToBed( b, a ));
            }

            return 0;
        }


        /// <summary>
        /// 单人换床处理
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private int TransPatientToBed(int a, int b)
        {
            /*int parm = 0;
            FS.HISFC.Models.RADT.Location obj_location = new FS.HISFC.Models.RADT.Location();
            FS.HISFC.Models.RADT.PatientInfo obj_a;

            //取ListView中的床位和患者信息
            obj_a = (FS.HISFC.Models.RADT.PatientInfo)this.lsvBedView.Items[a].Tag;
            obj_location.Bed = (FS.HISFC.Models.Base.Bed)this.lsvBedView.Items[b].Tag;

            //床号除去前四位
            string bedNo = obj_location.Bed.ID.Length > 4 ? obj_location.Bed.ID.Substring(4) : obj_location.Bed.ID;

            if (obj_location.Bed.Status.ID.ToString() == "W")
            {
                MessageBox.Show("床号为 [" + bedNo + " ]的床位状态为包床，不能占用！", "提示：");
                return -1;
            }
            else if (obj_location.Bed.Status.ID.ToString() == "C")
            {
                MessageBox.Show("床号为 [" + bedNo + " ]的床位状态为关闭，不能占用！", "提示：");
                return -1;
            }
            else if (obj_location.Bed.IsPrepay)
            {
                MessageBox.Show("床号为 [" + bedNo + " ]的床位已经预约，不能占用！", "提示：");
                return -1;
            }
            else if (!obj_location.Bed.IsValid)
            {
                MessageBox.Show("床号为 [" + bedNo + " ]的床位已经停用，不能占用！", "提示：");
                return -1;
            }
            //
            if (MessageBox.Show("是否预将“" + obj_a.Name + "”转移到[" + bedNo + "]号床", "提示：", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No) return -1;
            //
            try
            {
                FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(this.radtManager.Connection);
                SQLCA.BeginTransaction();
                this.radtManager.SetTrans(SQLCA.Trans);
                //单人换床处理
                parm = this.radtManager.TransferPatient(obj_a, obj_location);
                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "换床失败！\n" + this.radtManager.Err;
                    return -1;
                }
                else if (parm == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "换床失败! \n患者信息有变动或者已经出院,请刷新当前窗口";
                    return -1;
                }

                SQLCA.Commit();
                base.OnRefreshTree();
              
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return 1;*/
            int parm = 0;
            FS.HISFC.Models.RADT.Location obj_location = new FS.HISFC.Models.RADT.Location();
            FS.HISFC.Models.Registration.Register obj_a;

            //取ListView中的床位和患者信息
            obj_a = (FS.HISFC.Models.Registration.Register)this.lsvBedView.Items[a].Tag;
            obj_location.Bed = (FS.HISFC.Models.Base.Bed)this.lsvBedView.Items[b].Tag;

            //床号除去前四位
            string bedNo = obj_location.Bed.ID.Length > 4 ? obj_location.Bed.ID.Substring( 4 ) : obj_location.Bed.ID;

            if (obj_location.Bed.Status.ID.ToString() == "W")
            {
                MessageBox.Show( "床号为 [" + bedNo + " ]的床位状态为包床，不能占用！", "提示：" );
                return -1;
            }
            else if (obj_location.Bed.Status.ID.ToString() == "C")
            {
                MessageBox.Show( "床号为 [" + bedNo + " ]的床位状态为关闭，不能占用！", "提示：" );
                return -1;
            }
            else if (obj_location.Bed.IsPrepay)
            {
                MessageBox.Show( "床号为 [" + bedNo + " ]的床位已经预约，不能占用！", "提示：" );
                return -1;
            }
            else if (!obj_location.Bed.IsValid)
            {
                MessageBox.Show( "床号为 [" + bedNo + " ]的床位已经停用，不能占用！", "提示：" );
                return -1;
            }
            //
            if (MessageBox.Show( "是否预将“" + obj_a.Name + "”转移到[" + bedNo + "]号床", "提示：", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2 ) == System.Windows.Forms.DialogResult.No)
                return -1;
            //
            try
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(this.radtManager.Connection);
                //SQLCA.BeginTransaction();

                this.radtOutPatientManager.SetTrans( FS.FrameWork.Management.PublicTrans.Trans );

                //单人换床处理
                parm = this.radtOutPatientManager.TransferPatient( obj_a, obj_location );
                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "换床失败！\n" + this.radtManager.Err;
                    return -1;
                }
                else if (parm == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "换床失败! \n患者信息有变动或者已经出院,请刷新当前窗口";
                    return -1;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                base.OnRefreshTree();

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return 1;
        }

        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad( e );
        }
        private int ai;
        private int bi;
        private void lsvBedView_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
        {
            lsvBedView.DoDragDrop( e.Item, DragDropEffects.Move );
            ListViewItem lvi = (ListViewItem)e.Item;
            ai = lvi.Index;
            //换床处理
            if (ChangeItems( ai, bi ) == 1)
            {
                //刷新床位***待添加
            }
        }
        private void lsvBedView_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            Point clientPoint = lsvBedView.PointToClient( new Point( e.X, e.Y ) );
            bi = this.lsvBedView.GetItemAt( clientPoint.X, clientPoint.Y ).Index;
            if (ai != bi)
            {
                this.lsvBedView.Items[bi].Focused = true;
            }
        }

        private void lsvBedView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        //右键菜单控制
        private void lsvBedView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.lsvBedView.SelectedItems.Count == 0)
                {
                    this.lsvBedView.ContextMenuStrip = this.mnuSet;
                    this.MenuSetControl( true );
                }
                else if (this.lsvBedView.SelectedItems[0].Tag.GetType().ToString() == "FS.HISFC.Models.RADT.PatientInfo")
                {
                    this.lsvBedView.ContextMenuStrip = this.mnuBed;
                    Clearmenu();

                }
                else if (this.lsvBedView.SelectedItems[0].Tag.GetType().ToString() == "FS.HISFC.Models.Base.Bed")
                {
                    if (((FS.HISFC.Models.Base.Bed)this.lsvBedView.SelectedItems[0].Tag).Status.ID.ToString() == "W")
                    {
                        this.lsvBedView.ContextMenuStrip = this.mnuDealBed;
                    }
                    else
                    {
                        this.lsvBedView.ContextMenuStrip = null;
                    }
                }
                else
                {
                    this.lsvBedView.ContextMenuStrip = null;
                }
            }

        }


        private void lsvBedView_ItemActivate(object sender, System.EventArgs e)
        {
            //当激活节点时,如果床位被患者占用,则显示患者信息
            this.ShowPatientInfo();
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (e == null || e.Tag == null)
            {
                this.RefreshView();
                return 0;
            }
            if (e.Tag.GetType() == typeof( FS.HISFC.Models.RADT.PatientInfo ))
            {
            }
            else if (e.Tag != null)
            {
                switch (e.Tag.ToString())
                {
                    case "In"://本区患者
                        this.RefreshView();
                        break;
                    default:
                        break;
                }
            }
            return 0;
        }
        #endregion

        #region 菜单
        private ucPatientCardList myucPCList = null;
        protected ucPatientCardList ucPatientCardList
        {
            get
            {
                if (this.myucPCList == null)
                {
                    ArrayList al = new ArrayList();

                    if (this.alBeds == null)
                        return null;
                    //取本护理站床位信息
                    for (int i = 0; i < this.alBeds.Count; i++)
                    {
                        FS.HISFC.Models.Base.Bed bed = this.alBeds[i] as FS.HISFC.Models.Base.Bed;
                        FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();

                        //如果床位上有患者,则取此患者信息
                        if (bed.InpatientNO != "N")
                        {
                            patient = this.radtManager.QueryPatientInfoByInpatientNO( bed.InpatientNO );
                            patient.Disease.Memo = this.radtManager.GetFoodName( bed.InpatientNO );
                            if (patient == null)
                            {
                                MessageBox.Show( this.radtManager.Err );
                                return null;
                            }
                        }
                        else
                        {
                            patient.PVisit.PatientLocation.Bed = bed;
                        }

                        al.Add( patient );
                    }
                    this.myucPCList = new ucPatientCardList();
                    this.myucPCList.SetPatients( al );
                    this.myucPCList.Dock = DockStyle.Fill;
                    this.Controls.Add( this.myucPCList );
                    this.myucPCList.ContextMenuStrip = this.mnuSet;

                }
                return this.myucPCList;
            }
            set
            {
                myucPCList = value;
            }
        }
        private void mnuI_Large_Click(object sender, EventArgs e)
        {
            this.lsvBedView.Visible = true;


            if (this.myucPCList != null)
                this.myucPCList.Visible = false;
            this.MenuSetControl( false );
            this.lsvBedView.View = View.LargeIcon;
            this.MenuSetControl( true );
        }

        private void mnuI_Small_Click(object sender, EventArgs e)
        {
            this.lsvBedView.Visible = true;


            if (this.myucPCList != null)
                this.myucPCList.Visible = false;
            this.MenuSetControl( false );
            this.lsvBedView.View = View.SmallIcon;
            this.MenuSetControl( true );
        }

        private void mnuI_List_Click(object sender, EventArgs e)
        {
            this.lsvBedView.Visible = true;


            if (this.myucPCList != null)
                this.myucPCList.Visible = false;

            this.MenuSetControl( false );
            this.lsvBedView.View = View.List;
            this.MenuSetControl( true );
        }

        private void mnuI_Detail_Click(object sender, EventArgs e)
        {

            this.lsvBedView.Visible = true;
            if (this.myucPCList != null)
                this.myucPCList.Visible = false;
            this.MenuSetControl( false );
            this.lsvBedView.View = View.Details;
            this.MenuSetControl( true );
        }

        private void mnuI_card_Click(object sender, EventArgs e)
        {

            this.lsvBedView.Visible = false;
            this.ucPatientCardList.Visible = true;
            this.MenuSetControl( false );
            this.MenuSetControl( true );

        }

        private void mnuI_Info_Click(object sender, EventArgs e)
        {
            ShowPatientInfo();
        }

        private void mnuI_Wap_Click(object sender, EventArgs e)
        {
            if (this.ucBedChange1 == null)
                this.ucBedChange1 = new ucBedChange();
            this.ucBedChange1.SetType = BedType.Wap;

            this.ControlAdd();
        }

        private void mnuI_Unwap_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();
            FS.HISFC.Models.Base.Bed objBed = new FS.HISFC.Models.Base.Bed();

            objBed = (FS.HISFC.Models.Base.Bed)this.lsvBedView.SelectedItems[0].Tag;

            patient.ID = objBed.InpatientNO;
            try
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(this.radtManager.Connection);
                //SQLCA.BeginTransaction();

                this.radtManager.SetTrans( FS.FrameWork.Management.PublicTrans.Trans );

                //解包处理
                if (this.radtManager.UnWrapPatientBed( patient, objBed.ID, "2" ) == 0)
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                    //重新绘制浏览床位界面
                    objBed.InpatientNO = "N";
                    objBed.Status.ID = FS.HISFC.Models.Base.EnumBedStatus.U;
                    this.lsvBedView.Items[this.lsvBedView.SelectedIndices[0]] = this.GetListViewInfo( objBed );
                    //
                    this.Err = "保存成功！";
                    MessageBox.Show( this.Err );
                    //刷新床位列表
                    this.RefreshView();
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "保存失败！" + this.radtManager.Err;
                    MessageBox.Show( this.Err );
                }
            }
            catch
            {
            }
        }

        ucBedChange ucBedChange1 = new ucBedChange();
        private void ControlAdd()
        {
            if (this.lsvBedView.SelectedItems[0].Tag.GetType()
                == typeof( FS.HISFC.Models.RADT.PatientInfo ))
            {
                FS.HISFC.Models.RADT.PatientInfo patient = (FS.HISFC.Models.RADT.PatientInfo)this.lsvBedView.SelectedItems[0].Tag;


                this.ucBedChange1.PatientInfo = patient;

                FS.FrameWork.WinForms.Classes.Function.PopShowControl( this.ucBedChange1 );
                if (FS.FrameWork.WinForms.Classes.Function.PopForm.DialogResult == DialogResult.OK)
                {
                    //刷新床位列表
                    this.RefreshView();
                }
            }
        }

        #endregion

        private void lsvBedView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (this.ListViewItemChanged != null)
                this.ListViewItemChanged( sender, e );
        }

    }
}
