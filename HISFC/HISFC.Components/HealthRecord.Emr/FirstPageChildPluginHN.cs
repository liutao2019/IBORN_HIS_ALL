using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.HealthRecord.Emr
{
    public partial class FirstPageChildPluginHN : FS.Emr.Record.UI.Internal.Controls.Plugin.FirstPageChildPlugin
    {
        public FirstPageChildPluginHN()
        {
            InitializeComponent();
        }

        protected override void tsbtnPrintRecord_Click(object sender, EventArgs e)
        {
            if (base.inpatientRecordInfo == null)
            {
                FS.Emr.Base.Win32UI.Forms.EmrMessageBox.Show("病历未保存,不能打印", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string isCanPrint = FS.Emr.Record.UI.Internal.Proxy.FacadeProxy.RecordUI.GetParamValue(FS.Emr.Record.IBll.Const.Params.RecordParams.CaseIsCanPrint);
            
            if (!string.IsNullOrEmpty(isCanPrint) && isCanPrint == "1")
            {
                if (inpatientRecordInfo.RecordState != FS.Emr.Record.IBll.Enums.RecordEnum.RecordStateEnum.Submit.ToString())
                {
                    FS.Emr.Base.Win32UI.Forms.EmrMessageBox.Show("病历未提交,不能打印", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            HISFC.Components.HealthRecord.Emr.ucCaseMainInfoEmrNewControl ucCase = 
                this.GetControl<FS.HISFC.Components.HealthRecord.Emr.ucCaseMainInfoEmrNewControl>();

            if (ucCase != null)
            {
                ucCase.PrintInterface();
            }
            else
            {
                base.tsbtnPrintRecord_Click(sender, e);
            }
        }

        private T GetControl<T>() where T : Control
        {
            if (this.emrPanel5.Controls.Count > 0 && this.emrPanel5.Controls[0].Controls.Count > 0)
            {
                FS.Emr.RecordTpl.UI.External.Controls.IEditorControl iEditorControl =
                    this.emrPanel5.Controls[0].Controls[0] as FS.Emr.RecordTpl.UI.External.Controls.IEditorControl;
                if (iEditorControl != null)
                {
                    T templateFarpoint = iEditorControl.GetControlByType(typeof(T)) as T;
                    //templateFarpoint.FpSpreadMainBody.Sheets[0].Protect = true;
                    if (templateFarpoint != null)
                    {
                        return templateFarpoint;
                    }
                }
            }
            return null;
        }


        protected override void InitToolBar()
        {
            base.InitToolBar();

            bool isUseInsertTableAndImage = false;//muyun 2012-06-26 add
            this.tabtnSaveRecord.Visible = true;  //保存
            this.tsbtnSignRecord.Visible = false; //签名
            this.tsbtnOpenSign.Visible = false;//解签
            this.tsbtnPrintRecord.Visible = true; //打印
            //this.tsbtnSoundRecording.Visible = false; //录制语音
            this.tsbtnSoundPlay.Visible = false; //播放语音
            this.tsbtnOrderRelation.Visible = false; //关联医嘱
            this.tsbtnTextOper.Visible = false; //上下标
            this.tsmsSuperScript.Visible = false; //上标
            this.tsmsNormallScript.Visible = false; //正常
            this.tsmsSubScript.Visible = false; //下标
            this.tsbtnOperateRecord.Visible = true; //病历操作记录
            this.tsbtnDelRecord.Visible = true; //删除
            //this.tsbtnModifyTrail.Visible = false; //修改痕迹
            this.tsbtnFont.Visible = false; //字体
            this.tsbtnTextColor.Visible = false; //文字颜色
            this.tsbtnMark.Visible = false; //标记
            this.tsbtnPostil.Visible = false; //批注
            this.tsbtnAddRow.Visible = false; //增加行
            this.tsbtnDelRow.Visible = false; //删除行
            this.tsbtnSetRowHeight.Visible = false;
            this.tsbtnCoalescent.Visible = false; //合并单元格
            this.tsbtnSummarize.Visible = false; //24小时总结
            this.tsbtnGrossCalculate.Visible = false; //总量计算
            this.tsbtnSearch.Visible = false; //查找
            this.tsbtnOrientation.Visible = false; //定位
            this.tsbtnInputDias.Visible = false;

            if (this.IsWordRecord)
            {
                bool isUseInsertNewPage = FS.Emr.Record.UI.Internal.Proxy.FacadeProxy.RecordUI.IsUseInsertNewPage();//muyun 2012-07-11 add
                this.tsbtnWord.Visible = true; //Word操作
                this.tsbtnWordRedo.Visible = true;
                this.tsbtnWordUndo.Visible = true;
                this.tsbtnInsertMedicalImage.Visible = false;
                this.tsbtnFont.Visible = false; //字体
                this.tsbtnTextColor.Visible = false; //文字颜色
                this.tsmsNormallScript.Visible = false;
                //muyun 2012-06-26 add
                this.tsmsSubInsertTable.Visible = isUseInsertTableAndImage;
                this.tsbtnInsertImage.Visible = isUseInsertTableAndImage;

                //muyun 2012-07-11
                this.tbtnInsertNewPage.Visible = isUseInsertNewPage;
            }
            else
            {
                //this.tsbInsertImage.Visible = false;
                this.tsbtnWord.Visible = false; //Word操作
                this.tsbtnWordRedo.Visible = false;
                this.tsbtnWordUndo.Visible = false;

                this.tsbtnInsertMedicalImage.Visible = false;//muyun 2012-06-26 add
            }
            this.tsbtnInputDias.Visible = false;  //插入诊断
            this.tsbtnLisResult.Visible = false;  //插入检验结果
            this.tsbtnCaseSubmit.Visible = false; //病案首页提交
            this.tsbtnReject.Visible = false;//拒绝
            this.tsbtnRecive.Visible = false;//接收
            this.tsbtnMedicalFormula.Visible = false;//医学公式
            this.tsbtnQuickPutIn.Visible = false; //快速录入码
            this.tsbtnModifyRecord.Visible = false;//病历修改记录
            this.nbSet.Visible = false;//组套
            this.tsbtnSet.Visible = false;
            this.tsSetMaster.Visible = false;

            //this.tsbtnRecordReuse.Visible = false;
            
            this.tsbtnInserMedicalformula.Visible = false;

            //修改按钮文字,当使用暂存功能时，
            //string isUseTempSave = Proxy.FacadeProxy.RecordUI.GetParamValue(RecordParams.IsUseTempSave);
            //if (!string.IsNullOrEmpty(isUseTempSave) && isUseTempSave == "1") //启用暂存的时候
            //{
            //    this.tabtnSaveRecord.Text = "暂存";//保存
            //    this.tsbtnSignRecord.Text = "保存+签名"; ; //签名
            //}

            this.tabtnSaveRecord.Text = "保存";
        }

    }
}
