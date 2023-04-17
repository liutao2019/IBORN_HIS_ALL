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
    public partial class ucCaseMainInfoEmrNewPlugin :ucCaseMainInfoEmrNew
    {
        public ucCaseMainInfoEmrNewPlugin()
        {
            InitializeComponent();
        }

        protected override void InitToolBar()
        {
            base.InitToolBar();
            base.tsbtnReject.Visible = false;
            base.tsbtnRecive.Visible = false;
            this.tsbtnModifyRecord.Visible = false;//病历修改记录
            this.tsbtnLisResult.Visible = false;  //插入检验结果
            this.tsbtnInsertMedicalImage.Visible = false;
            this.tsbtnTextOper.Visible = false; //上下标
            this.tsmsSuperScript.Visible = false; //上标
            this.tsmsNormallScript.Visible = false; //正常
            this.tsmsSubScript.Visible = false; //下标
            this.tsbtnOperateRecord.Visible = false;//病历操作记录
            this.tsbtnDelRecord.Visible = false; //删除
            //this.tsbtnModifyTrail.Visible = false; //修改痕迹
            this.tsbtnFont.Visible = false; //字体
            this.tsbtnTextColor.Visible = false; //文字颜色
            this.tabtnSaveRecord.Visible = true;  //保存
            this.tsbtnSignRecord.Visible = false; //签名
            this.tsbtnPrintRecord.Visible = true; //打印
            base.tsbtnInserMedicalformula.Visible = false;
        }


        FS.HISFC.Components.HealthRecord.CaseFirstPage.ucMetCasBaseInfo ucCase =
            new FS.HISFC.Components.HealthRecord.CaseFirstPage.ucMetCasBaseInfo();

        protected override void OpenRecord()
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

        protected override void SetMenuItem()
        {
            base.SetMenuItem();
        }

        protected override void tabtnSaveRecord_Click(object sender, EventArgs e)
        {
            //IList<InpatientRecordSetInfo> inpatientRecordSetInfolist = inpatientRecordSetService.QueryRecordSetsByInpatientNo(this.Host.InPatientInfo.Id.Value);
            //if (inpatientRecordSetInfolist != null)
            //{
            //    foreach (InpatientRecordSetInfo info in inpatientRecordSetInfolist)
            //    {
            //        if (info.ArchiveState.Equals(RecordEnum.SubmitStatusEnum.Commit.ToString()) && info.ArchiveState.Equals(RecordEnum.SubmitStatusEnum.Submit.ToString()))
            //        {
            //            FS.Emr.Base.Win32UI.Forms.EmrMessageBox.Show("病历已提交，无法修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            return;
            //        }
            //    }
            //}
            //else
            //{
            //    FS.Emr.Base.Win32UI.Forms.EmrMessageBox.Show("病历集未创建，请联系管理员！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //FS.Emr.RecordTpl.UI.External.Controls.IEditorControl iEditorControl = this.pRecordEditor.Controls[0] as FS.Emr.RecordTpl.UI.External.Controls.IEditorControl;
            ////签名病历保存进行校验
            //if (this.inpatientRecordInfo != null)
            //{
            //    if (!string.IsNullOrEmpty(this.inpatientRecordInfo.RecordState)
            //&& this.inpatientRecordInfo.RecordState != IBll.Enums.RecordEnum.RecordStateEnum.Create.ToString()
            //&& this.inpatientRecordInfo.RecordState != IBll.Enums.RecordEnum.RecordStateEnum.Save.ToString())
            //    {
            //        //验证控件是否有效
            //        bool VerifyResult = iEditorControl.Verify(this.Host.InPatientInfo.Id.Value, -1);
            //        if (!VerifyResult)
            //        {
            //            //FS.Emr.Base.Win32UI.Forms.EmrMessageBox.Show("病历验证没有成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            return;
            //        }
            //    }
            //}
            //base.Save();
            //if (this.Text.Contains("*"))
            //{
            //    this.Text = this.Text.Trim("*".ToCharArray());
            //    this.MenuItem.Text = this.Text;
            //    if (this.PropertyChanged != null)
            //    {
            //        this.PropertyChanged(this, new PropertyChangedEventArgs("Text"));
            //    }
            //    this.tmSave.Enabled = false;
            //    try
            //    {
            //        System.IO.File.Delete(Application.StartupPath + @"\Tmp\" + this.Host.InPatientInfo.Id.Value.ToString() + "," + this.inpatientRecordInfo.Id + ".xml");
            //    }
            //    catch { }
            //}
            ucCase.Save(this);
            FS.Emr.Base.Win32UI.Forms.EmrMessageBox.Show("保存成功");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected override void tsbtnPrintRecord_Click(object sender, EventArgs e)
        {
            if (this.inpatientRecordInfo == null)
            {
                FS.Emr.Base.Win32UI.Forms.EmrMessageBox.Show("病历未保存,不能打印", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //this.Print();
            ucCase.PrintInterface();
        }
        public override void OnHostInitialized()
        {
            this.Host.InPatientInfo.PatientFee = FS.Emr.Record.UI.Internal.Proxy.FacadeProxy.RecordUI.GetIpmFeeInfo(this.Host.InPatientInfo.Id.Value);
            base.OnHostInitialized();

        }
    }
}
