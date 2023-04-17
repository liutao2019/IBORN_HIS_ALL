using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Neusoft.FrameWork.Management;
using Neusoft.HISFC.Models.RADT;
using Neusoft.HISFC.Models.Fee.Inpatient;
using Neusoft.HISFC.Models.Fee;
using Neusoft.HISFC.Models.Base;
using Neusoft.FrameWork.Function;
using Neusoft.FrameWork.WinForms.Forms;
using ATL_COMLib;
using GDCAtest;

namespace Neusoft.SOC.Local.RADT.ZhuHai.ZDWY.Controls
{
    /// <summary>
    /// ucModifyPatientInfo<br></br>
    /// [功能描述: CA对照]<br></br>
    /// [创 建 者: ]<br></br>
    /// [创建时间: 2015-1-14]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucCompare : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCompare()
        {
            InitializeComponent();
            //this.txtPatientNO.KeyDown+=new KeyEventHandler(txtPatientNO_KeyDown);
            //this.rbtnTempPatientno.CheckedChanged += new EventHandler(rbtnTempPatientno_CheckedChanged);
        }



        private void ucCompare_Load(object sender, EventArgs e)
        {
            Init();
        }

        #region 字段

        /// <summary>
        /// 打印病案
        /// </summary>
        Neusoft.HISFC.BizProcess.Interface.HealthRecord.HealthRecordInterface healthPrint = null;

        /// <summary>
        /// 患者信息实体

        /// </summary>
        private Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = new PatientInfo();

        /// <summary>
        /// 患者信息实体副本

        /// </summary>
        private Neusoft.HISFC.Models.RADT.PatientInfo patientInfoOld;

        /// <summary>
        /// Manager业务层

        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Manager managerIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 住院入出转业务层
        /// </summary>
        private Neusoft.HISFC.BizLogic.RADT.InPatient inpatientManager = new Neusoft.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 住院费用大业务层
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Fee feeManager = new Neusoft.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 患者入出转转业务层
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.RADT radtIntegrate = new Neusoft.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// toolBarService
        /// </summary>
        protected Neusoft.FrameWork.WinForms.Forms.ToolBarService toolBarService = new Neusoft.FrameWork.WinForms.Forms.ToolBarService();

        #region add by xuewj 2010-03-10 {010BAFC3-96E2-4acc-AAD4-55320B9C2229}
        /// <summary>
        /// ADT接口
        /// </summary>
        private Neusoft.HISFC.BizProcess.Interface.IHE.IADT adt = null;

        #endregion

        #endregion

        #region 方法

        /// <summary>
        /// 增加ToolBar控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("对照", "对照", Neusoft.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolBarService.AddToolButton("清屏", "清屏", Neusoft.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            toolBarService.AddToolButton("帮助", "打开帮助文件", Neusoft.FrameWork.WinForms.Classes.EnumImageList.B帮助, true, false, null);
            toolBarService.AddToolButton("退出", "退出", Neusoft.FrameWork.WinForms.Classes.EnumImageList.T退出, true, true, null);

            return this.toolBarService;
        }

        /// <summary>
        /// 定义toolbar按钮click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "对照":

                    this.Confirm();
                    break;

                case "清屏":
                    this.Clear();
                    //this.ucQueryInpatientNo1.Text = "";
                    //this.ucQueryInpatientNo1.Focus();
                    break;

                case "帮助":

                    break;
                case "退出":
                    this.FindForm().Close();
                    break;
               
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// 初始化控件,等信息

        /// </summary>
        /// <returns>成功 1 失败: -1</returns>
        protected virtual int Init()
        {

            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("正在初始化窗口，请稍候^^"));
            Application.DoEvents();

            try
            {
                //医生信息
                this.cmbDoctor.AddItems(managerIntegrate.QueryEmployeeAll());
            }
            catch (Exception e)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(e.Message);

                return -1;
            }

            //this.RefreshPatientLists();
            this.SetReadOnly(true);
            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 1;
        }

        //private void readkey_Click(object sender, EventArgs e)
        //{
        //    this.emrButton1_Click();
        //}
        private void Compare_Click(object sender, EventArgs e)
        {
            this.Confirm();
        }
        private void cancel_Click(object sender, EventArgs e)
        {
            this.Cancel();
        }

        #endregion
        

        ATL_COMLib.GdcaClass bb = new ATL_COMLib.GdcaClass();
        string CertData = null;
        GDCAWSCOMLib.WSClientComClass cc = new GDCAWSCOMLib.WSClientComClass();



        //初始化
        private void readkey_Click(object sender, EventArgs e)
        {

            int DeviceType = bb.GDCA_GetDeviceType();
            //MessageBox.Show(DeviceType.ToString());
            int SetDeviceType = bb.GDCA_SetDeviceType(DeviceType);
            if (SetDeviceType != 0)
            {
                MessageBox.Show("初始化失败,请检查是否插入Key");
                return;
            }

            int Initialize = bb.GDCA_Initialize();
            //MessageBox.Show(Initialize.ToString());
            if (Initialize == 0)
            {
                //读取签名证书
                CertData = bb.GDCA_ReadLabel("LAB_USERCERT_SIG", 7);
                //读取加密证书
                //bb.GDCA_ReadLabel("LAB_USERCERT_SIG", 8);
                string[] temp = new String[10];
                temp[0] = bb.GDCA_GetInfoByOID(CertData, 1, "2.5.4.3", 0);//Key的医生/护士姓名
                temp[1] = bb.GDCA_GetInfoByOID(CertData, 2, "1.2.86.11.7.1", 0);
                temp[2] = bb.GDCA_GetInfoByOID(CertData, 1, "2.5.4.10", 0);
                temp[3] = bb.GDCA_GetInfoByOID(CertData, 1, "2.5.4.11", 0);
                temp[4] = bb.GDCA_GetInfoByOID(CertData, 1, "2.5.4.7", 0);
                temp[5] = bb.GDCA_GetInfoByOID(CertData, 1, "2.5.4.8", 0);
                temp[6] = bb.GDCA_GetInfoByOID(CertData, 1, "2.5.4.6", 0);
                temp[7] = bb.GDCA_GetInfoByOID(CertData, 2, "1.2.86.21.1.1", 0);
                temp[8] = bb.GDCA_GetInfoByOID(CertData, 2, "1.2.86.21.1.3", 0);//Key的唯一标识
                //MessageBox.Show(temp[0] + "\n" + temp[1]);
                //KeyName.Text = temp[0];
                textReadKey.Text = temp[8];
                
                if (this.textinformation.Text=="")
                {
                    this.textinformation.Text=temp[0];
                }
                /*
                document.form1.username.value = UseCom.GDCA_GetInfoByOID(CertData[0], 1, "2.5.4.3", 0);
                document.form1.idnum.value = UseCom.GDCA_GetInfoByOID(CertData[0], 2, "1.2.86.11.7.1", 0);
                document.form1.oname.value = UseCom.GDCA_GetInfoByOID(CertData[0], 1, "2.5.4.10", 0);
                document.form1.ouname.value = UseCom.GDCA_GetInfoByOID(CertData[0], 1, "2.5.4.11", 0);
                document.form1.lname.value = UseCom.GDCA_GetInfoByOID(CertData[0], 1, "2.5.4.7", 0);
                document.form1.sname.value = UseCom.GDCA_GetInfoByOID(CertData[0], 1, "2.5.4.8", 0);
                document.form1.cname.value = UseCom.GDCA_GetInfoByOID(CertData[0], 1, "2.5.4.6", 0);
                */


            }
            else
            {
                MessageBox.Show("初始化失败,请检查是否插入Key");
            }


        }
        /// <summary>
        /// 确定方法
        /// </summary>
        /// <returns></returns>
        protected virtual int Confirm()
        {
          
            //创建数据库连接

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            //Neusoft.FrameWork.Management.Transaction t = new Transaction(this.inpatientManager.Connection);
            ////开始事物
            //Neusoft.HISFC.BizLogic.RADT.InPatient radtManager = new Neusoft.HISFC.BizLogic.RADT.InPatient();
            if (this.radtIntegrate.QueryAllCompare(cmbDoctor.Tag.ToString(),textReadKey.Text) == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                Neusoft.FrameWork.WinForms.Classes.Function.Msg("对照医生信息错误,该人员已经做了对照!" + this.inpatientManager.Err, 211);

                return -1;
            }
            //对照信息
            if (this.radtIntegrate.InsertCACompare(cmbDoctor.Tag.ToString(),textReadKey.Text) == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                Neusoft.FrameWork.WinForms.Classes.Function.Msg("插入对照信息出错!" + this.inpatientManager.Err, 211);

                return -1;
            }
         

            //提交事物
            Neusoft.FrameWork.Management.PublicTrans.Commit();
            Neusoft.FrameWork.WinForms.Classes.Function.Msg("对照成功!", 111);
            //清空控件
            this.Clear();

            return 1;
        }
        /// <summary>
        /// 作废 方法
        /// </summary>
        /// <returns></returns>
        protected virtual int Cancel()
        {

            //创建数据库连接

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            //Neusoft.FrameWork.Management.Transaction t = new Transaction(this.inpatientManager.Connection);
            ////开始事物
            Neusoft.HISFC.BizLogic.RADT.InPatient radtManager = new Neusoft.HISFC.BizLogic.RADT.InPatient();
            //对照信息
            if (this.radtIntegrate.UpdateCancelCACompare(textReadKey.Text) == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                Neusoft.FrameWork.WinForms.Classes.Function.Msg("作废信息出错,已经处于作废状态!" + this.inpatientManager.Err, 211);

                return -1;
            }

            //提交事物
            Neusoft.FrameWork.Management.PublicTrans.Commit();
            Neusoft.FrameWork.WinForms.Classes.Function.Msg("作废成功!", 111);
            //清空控件
            this.Clear();

            return 1;
        }

        /// <summary>
        /// 清空控件信息
        /// </summary>
        protected virtual void Clear()
        {
            this.cmbDoctor.Text = string.Empty;
            this.textReadKey.Text = string.Empty;
            this.textinformation.Text = string.Empty;
        }
        public void SetReadOnly(bool type)
        {

            //姓名
            this.textinformation.ReadOnly = type;
            this.textinformation.Enabled = !type;
            this.textinformation.BackColor = System.Drawing.Color.White;
            //KEY
            this.textReadKey.ReadOnly = type;
            this.textReadKey.Enabled = !type;
            this.textReadKey.BackColor = System.Drawing.Color.White;
        }

      
    }
}
