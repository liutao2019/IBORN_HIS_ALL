using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace Neusoft.SOC.Local.EMR.ZDLY.Controls
{
    public partial class ucEmrPacsInterface :Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.Emr.DoctorStation.Port.IPlugin
    {
        public ucEmrPacsInterface()
        {
            InitializeComponent();
        }

        private void ucEmrPacsInterface_Load(object sender, EventArgs e)
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

                this.lblPatientNo.Text = "住院号：" + this.Host.InPatientInfo.PatientNo;
                this.lblPatientName.Text = "姓名：" + this.Host.InPatientInfo.Name;
                this.lblPatientGender.Text = "性别：" + (this.Host.InPatientInfo.Sex=="M"? "男":"女");
                this.lblPateintDept.Text = "科室：" + this.Host.InPatientInfo.DeptName;
                this.lblPatientBedNo.Text = "床号：" + this.Host.InPatientInfo.BedNo;
               
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
        public IList<Neusoft.Emr.DoctorStation.Port.IAction> Actions
        {
            get;
            set;
        }

        /// <summary>
        /// 插件容器实例（WinForm或WPF Window）
        /// 该值由容器提供，插件无需管理
        /// </summary>
        public Neusoft.Emr.DoctorStation.Port.IHost Host
        {
            get;
            set;
        }

        /// <summary>
        /// 插件对应的菜单项。该值由容器提供，插件无需管理
        /// 如果各插件要添加子节点，可调用该对象的AddChildItem方法
        /// </summary>
        public Neusoft.Emr.DoctorStation.Model.TreeItem MenuItem
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
        public void OnDisposing(Neusoft.Emr.DoctorStation.Port.DisposingEventArgs e)
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
        public Neusoft.Emr.DoctorStation.Port.OperationModel OperationModel
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

        }

        private void tsb_Print_Click(object sender, EventArgs e)
        {

        }

        private void tsb_Exit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        
        #region 华海pacs
        private void btnPacs_Click(object sender, EventArgs e)
        {
            try
            {
                string errMsg = string.Empty;
                string ttemp = "NoStart";

                Process[] arrP = processMrg.ListProcesses();
                foreach (Process p in arrP)
                {
                    try
                    {
                        if (p.MainWindowHandle != null && !string.IsNullOrEmpty(p.MainWindowTitle))
                        {
                            if (p.MainWindowTitle.Contains("ClinicalPACS.xbap"))
                            {
                                ttemp = "START";
                                break;
                            }
                        }
                    }
                    catch {
                        
                    }
                }
                if (ttemp == "NoStart")
                {
                    System.Diagnostics.Process.Start("http://192.168.250.210/ClinicalPACS/ClinicalPACS.xbap?P=" + this.Host.InPatientInfo.PatientNo);
                }
                else
                {
                    HuaHaiServices.ConnectHISServiceClient CHS = new Neusoft.SOC.Local.EMR.HuaHaiServices.ConnectHISServiceClient();
                    CHS.InputParameter("P", this.Host.InPatientInfo.PatientNo);
 
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 心电图
        /// <summary>
        /// 心电图系统进程名
        /// add by lijp 2011-04-25
        /// </summary>
        private const string ECGPROCESSNAME = @"ECGRptView.exe";

        /// <summary>
        /// 进程管理类
        /// </summary>
        private static Neusoft.SOC.Local.EMR.ZDLY.Class.ProcessesManager processMrg = new Neusoft.SOC.Local.EMR.ZDLY.Class.ProcessesManager();

        /// <summary>
        /// 心电图调用
        /// </summary>
        /// <param name="patientNo"></param>
        /// <param name="appFolderPath"></param>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public void ECGNET(string patientNo)
        {
            try
            {
                if (string.IsNullOrEmpty(patientNo))
                {
                    return;
                }

                processMrg.KillProcess(ECGPROCESSNAME);
                string appFolderPath = Application.StartupPath;
                string fullFilePath = appFolderPath + "\\" + ECGPROCESSNAME;

                if (Directory.Exists(appFolderPath))
                {
                    if (!System.IO.File.Exists(fullFilePath))
                    {
                        System.Windows.Forms.MessageBox.Show("找不到心电图系统客户端：ECGRptView.exe，请检查是否已安装。");
                        return;
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("找不到目录 " + appFolderPath + "，请确认安装在正确的路径。");
                    return;
                }
                System.Windows.Forms.Application.DoEvents();

                Neusoft.SOC.Local.EMR.ZDLY.Class.ProcessesManager.ShellExecute(
                    this.Handle, "open", fullFilePath,
                    " -R" + patientNo + " -T1", null, (int)Neusoft.SOC.Local.EMR.ZDLY.Class.ProcessesManager.ShowWindowCommands.SW_SHOW);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEcgNet_Click(object sender, EventArgs e)
        {
            this.ECGNET(this.Host.InPatientInfo.PatientNo);
        }
        #endregion

        #region 超声
        private void btnEfilm_Click(object sender, EventArgs e)
        {
             string patientNo=this.Host.InPatientInfo.PatientNo;
            try
            {
                if (string.IsNullOrEmpty(patientNo))
                {
                    return;
                }

                processMrg.KillProcess("SuperSoundReport.exe");
                string appFolderPath = Application.StartupPath;
                string fullFilePath = appFolderPath + "\\csview\\SuperSoundReport.exe";

                if (Directory.Exists(appFolderPath))
                {
                    if (!System.IO.File.Exists(fullFilePath))
                    {
                        System.Windows.Forms.MessageBox.Show("找不到超声系统客户端，请检查是否已安装。");
                        return;
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("找不到目录 " + appFolderPath + "，请确认安装在正确的路径。");
                    return;
                }
                System.Windows.Forms.Application.DoEvents();

                Neusoft.SOC.Local.EMR.ZDLY.Class.ProcessesManager.ShellExecute(
                    this.Handle, "open", fullFilePath,
                    " "+ patientNo+",,,200", null, (int)Neusoft.SOC.Local.EMR.ZDLY.Class.ProcessesManager.ShowWindowCommands.SW_SHOW);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void btnnj_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://192.168.19.30/Report.aspx?InPatNo=" + this.Host.InPatientInfo.PatientNo
                +"&OutPatNo=&PatientID=");
        
        }
    }
}
