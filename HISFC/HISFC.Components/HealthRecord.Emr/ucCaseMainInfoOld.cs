using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.Emr
{
    public partial class ucCaseMainInfoOld :FS.FrameWork.WinForms.Controls.ucBaseControl, FS.Emr.DoctorStation.Port.IPlugin
    {
        public ucCaseMainInfoOld()
        {
            InitializeComponent();
        }

        FS.HISFC.Components.HealthRecord.CaseFirstPage.ucCaseMainInfo ucCase =
            new FS.HISFC.Components.HealthRecord.CaseFirstPage.ucCaseMainInfo();

        private void ucCaseMainInfo_Load(object sender, EventArgs e)
        {
            this.SetEMRPatient();
        }

        #region EMR相关

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
                this.Controls.Add(ucCase);
                this.BackColor = ucCase.BackColor;
                ucCase.LoadInfo(hisInpatientNo, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
                ucCase.BringToFront();
                ucCase.Dock = DockStyle.Fill;
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
            base.Dispose();
        }

        #endregion

        private void tsb_Save_Click(object sender, EventArgs e)
        {
            ucCase.Save(sender);
        }

        private void tsb_Print_Click(object sender, EventArgs e)
        {
            ucCase.PrintInterface();
        }

        private void tsb_Exit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
