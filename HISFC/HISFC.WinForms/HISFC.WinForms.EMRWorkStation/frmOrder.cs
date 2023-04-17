using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.WinForms.EMRWorkStation
{
    /// <summary>
    /// 住院医嘱开立主界面  
    /// 只用于电子病历调用 不用于实现业务代码
    /// </summary>
    public partial class frmOrder : HISFC.WinForms.WorkStation.frmOrder, FS.Emr.DoctorStation.Port.IPlugin
    {
        public frmOrder()
        {
            InitializeComponent();

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv")
            {
                return;
            }
            this.IsEMROrder = true;
            this.formID = "BHEMROrder";

            //FS.HISFC.BizLogic.Admin.FunSetting manager = new FS.HISFC.BizLogic.Admin.FunSetting();
            //FS.HISFC.Models.Admin.FunSetting obj = manager.GetFunSetting("BHEMROrder");

            //if (obj == null)
            //{
            //    return;
            //}
            this.SetFormID("BHEMROrder");
        }

        /// <summary>
        /// 是否初始化结束
        /// </summary>
        //bool isInitEnd = false;

        #region EMR相关

        protected override void OnLoad(EventArgs e)
        {
            this.SetEMRPatient();
            base.OnLoad(e);
            //isInitEnd = true;
        }

        /// <summary>
        /// 当前科室，用于电子病历的切换科室功能
        /// </summary>
        string currentDept = "";

        /// <summary>
        /// {42F24BE7-A852-447e-B371-22B1F7439802}
        /// Emr调用设置
        /// </summary>
        private void SetEMRPatient()
        {
            try
            {
                if (this.Host == null || this.Host.InPatientInfo == null)
                {
                    return;
                }

                string hisInpatientNo = this.Host.InPatientInfo.HisInpatientNo;

                #region 切换科室功能

                if (string.IsNullOrEmpty(currentDept))
                {
                    currentDept = (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).Dept.ID;
                }

                if (currentDept != Host.InPatientInfo.DeptCode)
                {
                    currentDept = Host.InPatientInfo.DeptCode;
                    FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
                    empl.ID = (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).ID;
                    empl.Name = (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).Name;
                    empl.Nurse.ID = (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).Nurse.ID;
                    empl.Dept.ID = currentDept;

                    TvDoctorPatientList.RefreshInfo(empl);
                }

                #endregion

                //this.IsShowTreeView = false;
                for (int i = 0; i < this.TvDoctorPatientList.Nodes.Count; i++)
                {
                    foreach (TreeNode node in TvDoctorPatientList.Nodes[i].Nodes)
                    {
                        Patient = node.Tag as FS.HISFC.Models.RADT.PatientInfo;
                        if (Patient != null)
                        {
                            if (Patient.ID == hisInpatientNo)
                            {
                                this.TvDoctorPatientList.SelectedNode = node;

                                this.TvDoctorPatientList1_AfterSelect(Patient, null);

                                return;
                            }
                        }
                    }
                }
            }
            catch { }
        }


        #endregion

        #region IPlugin 成员{42F24BE7-A852-447e-B371-22B1F7439802}

        /// <summary>
        /// 对插件可执行的操作对象数组。
        /// 这些对象需要由各插件自己提供，（必须实现IAction接口）
        /// IHost会根据这些对象生成相应的右键菜单项，
        /// 当用户在菜单树中的节点处右键点击后，会展现相应的菜单，
        /// 单击菜单项后会调用相应IAction的Execute方法。
        /// </summary>
        public IList<FS.Emr.DoctorStation.Port.IAction> Actions
        {
            get;
            set;
        }

        /// <summary>
        /// 插件容器实例（WinForm或WPF Window）
        /// 该值由容器提供，插件无需管理
        /// </summary>
        public FS.Emr.DoctorStation.Port.IHost Host
        {
            get;
            set;
        }

        /// <summary>
        /// 插件对应的菜单项。该值由容器提供，插件无需管理
        /// 如果各插件要添加子节点，可调用该对象的AddChildItem方法
        /// </summary>
        public FS.Emr.DoctorStation.Model.TreeItem MenuItem
        {
            get;
            set;
        }

        /// <summary>
        /// 当插件实例化后调用此方法，例如：可在此方法中初始化Actions
        /// </summary>
        public void OnActionsInitialized()
        {

        }

        /// <summary>
        /// 插件被卸载前触发的方法。
        /// </summary>
        public void OnDisposing(FS.Emr.DoctorStation.Port.DisposingEventArgs e)
        {

        }

        /// <summary>
        /// 插件对象的Host对象被初始化后调用此方法。调用his.exe，传入
        /// </summary>
        public void OnHostInitialized()
        {
            //if (isInitEnd)
            //{
            //    this.SetEMRPatient();
            //}
        }

        /// <summary>
        /// 插件模式（只读或编辑，该值由各插件自己控制）
        /// </summary>
        public FS.Emr.DoctorStation.Port.OperationModel OperationModel
        {
            get;
            set;
        }

        #endregion

        #region INotifyPropertyChanged 成员{42F24BE7-A852-447e-B371-22B1F7439802}

        /// <summary>
        /// 在更改属性值时发生。
        /// </summary>
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            this.Close();
        }

        #endregion
    }
}
