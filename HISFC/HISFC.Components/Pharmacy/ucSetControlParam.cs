using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.BizProcess.Integrate;
using System.Collections;

namespace FS.HISFC.Components.Pharmacy
{
    /// <summary>
    /// [��������: ҩƷ����������]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// <�޸ļ�¼>
    ///     1�����ζ��ڡ���ʾ��ҩ��Ԥ�������������á��ò������岻��
    ///     2�������Ƿ������̵���� ������ͨ�����ſ�泣����ȡ
    ///     3������סԺ�Ƿ�ʹ��Ԥ�ۿ�淽ʽ�Ŀ��Ʋ���
    ///     4������Э�������Ƿ������Ŀ��Ʋ���
    ///     5��һ������Ƿ�����ϴ�����Զ������Ч�ڡ����š������P01016 by Sunjh 2010-10-28 {97C93751-7EED-4160-931A-EC77C1F4E291}
    ///     6���������ݱ����ҩƷ��׼���ʱ�Ƿ����ҩƷ�ֵ���Ϣ����ۼ���׼�ĺ� 0 ������ 1 ���� P00572 by Sunjh 2010-10-29 {1DF0FB1D-070E-4ee6-B631-047D340C70F1}
    /// </�޸ļ�¼>
    /// </summary>
    public partial class ucSetControlParam : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.Common.IControlParamMaint
    {
        public ucSetControlParam()
        {
            InitializeComponent();

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();
            }
        }

        #region �����

        /// <summary>
        /// ��������
        /// </summary>
        private string description = "ҩƷ�����������";

        /// <summary>
        /// ������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// ҩƷ���Ʋ�����Ϣ
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.PharmacyConstant phaConsInfo = new PharmacyConstant();

        /// <summary>
        /// ��ȡ���γ�ʼ��ʱ�Ĳ���ֵ ����ʱ���ڱ仯�Ĳ������б���
        /// </summary>
        private System.Collections.Hashtable hsOriginalParam = new System.Collections.Hashtable();

        /// <summary>
        /// ������ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        #endregion

        #region ����

        /// <summary>
        /// ��ȡ���в���
        /// </summary>
        /// <returns></returns>
        public List<FS.HISFC.Models.Base.ControlParam> GetControlerList()
        {
            List<FS.HISFC.Models.Base.ControlParam> alControler = new List<FS.HISFC.Models.Base.ControlParam>();

            FS.HISFC.Models.Base.ControlParam tempControler = new FS.HISFC.Models.Base.ControlParam();

            #region סԺҩ������

            #region �������

            ///ҩ����������Ƿ��ά��Ϊ���ɲ�ֵ���ȡ��
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckNosplitAndDayToInteger.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckNosplitAndDayToInteger.Checked).ToString();
            alControler.Add(tempControler.Clone());

            //ҩ����������Ƿ��ά��Ϊ���ɲ��
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckNoSplitAtAll.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckNoSplitAtAll.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///ҩƷ��������Ƿ������Ϊ�ɲ�ֲ�ȡ��
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckSplitAndNoInteger.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckSplitAndNoInteger.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///ҩƷ��������Ƿ������Ϊ�ɲ����ȡ��
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckSplitAndUpperToInteger.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckSplitAndUpperToInteger.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///ҩƷ��������Ƿ������Ϊ�ɲ�ְ�����ȡ��
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckSplitAndDeptToInteger.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckSplitAndDeptToInteger.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///ҩƷ��������Ƿ������Ϊ������ȡ��
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckSplitAndNurceCellToInteger.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckSplitAndNurceCellToInteger.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///ҩƷ��������Ƿ������Ϊ����װ��λȡ�� {9D24EE50-57C5-4cae-8B70-8D8C3E7B4BDC}
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckNosplitAndPackUnit.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckNosplitAndPackUnit.Checked).ToString();
            alControler.Add(tempControler.Clone());

            #endregion

            #region ��ҩ���̿���

            ///��ҩ�Ƿ����׼
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckInNeedApprove.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckInNeedApprove.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///�Ƿ�ҩʦ�ſ��Ժ�׼
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckInNeedPriv.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckInNeedPriv.Checked).ToString();
            alControler.Add(tempControler.Clone());

            #endregion

            #region ��ҩ���ܿ���

            //�������ϲ���������Ϣ
            //tempControler = new FS.HISFC.Models.Base.ControlParam();
            //tempControler.ID = this.ckInPartApprove.Tag.ToString();
            //tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            //tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckInPartApprove.Checked).ToString();
            //alControler.Add(tempControler.Clone());

            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckInPatientPreOut.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckInPatientPreOut.Checked).ToString();
            alControler.Add(tempControler.Clone());

            //�������ϲ���������Ϣ
            //tempControler = new FS.HISFC.Models.Base.ControlParam();
            //tempControler.ID = this.ckAutoPrintOutput.Tag.ToString();
            //tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            //tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckAutoPrintOutput.Checked).ToString();
            //alControler.Add(tempControler.Clone());

            #endregion

            #region ��ҩ�������

            ///��ҩ�Ƿ�Ԥ��
            ///            //�������ϲ���������Ϣ
            //tempControler = new FS.HISFC.Models.Base.ControlParam();
            //tempControler.ID = this.ckInPriviewBill.Tag.ToString();
            //tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            //tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckInPriviewBill.Checked).ToString();
            //alControler.Add(tempControler.Clone());

            ///�¼���Ϣ�Ƿ��Զ�ѡ��
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckInAutoCheck.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckInAutoCheck.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///�Ƿ���ʾ���߻���
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckPatientTot.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckPatientTot.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///�Ƿ���ʾ���һ���
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckDeptTot.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckDeptTot.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///�Ƿ���ʾ�������ȷ�ʽ��ʾ
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckDeptFirst.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckDeptFirst.Checked).ToString();
            alControler.Add(tempControler.Clone());

            #endregion

            #endregion

            #region ����ҩ������

            ///���﷢ҩ�ն˱���� �Ƿ�ˢ��
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckSaveRefresh.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckSaveRefresh.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///���﷢ҩ�����Ƿ���ʾ����
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckOutShowDays.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckOutShowDays.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///��ҩȷ�Ϻ��Ƿ��ӡ��ҩ�嵥
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckOutPrintList.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckOutPrintList.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///��ҩȷ�Ϻ��Ƿ��ӡ����
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckOutPrintRecipe.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckOutPrintRecipe.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///��ҩ����ʱ���Ų���λ�� ��1 ���貹λ
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ndUOperLength.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = this.ndUOperLength.Value.ToString();
            alControler.Add(tempControler.Clone());

            ///��/��ҩ�����Ƿ����Ȩ�޿��� (ֻ��ҩʦ���Բ���) 
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckOutNeedPriv.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckOutNeedPriv.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///�����ǩ�Զ���ӡʱ  �Ƿ�Դ������ϼ�¼�Ĵ�������ӡ
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckPrintBackRecipe.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckPrintBackRecipe.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///������ҩʱ �Ƿ���п�澯���ж� 
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckClinicWarDrug.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckClinicWarDrug.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///���﷢ҩʱ �Ƿ���п�澯���ж� 
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckClinicWarnSend.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckClinicWarnSend.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///�����շ�ʱ�Ƿ�Ԥ�ۿ��
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckClinicPreOut.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckClinicPreOut.Checked).ToString();
            alControler.Add(tempControler.Clone());

            //���﷢ҩ�����Ƿ���ʾ�������ҩ����
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckShowOldSendedInfo.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckShowOldSendedInfo.Checked).ToString();
            alControler.Add(tempControler.Clone());

            //������ҩ�����Զ���ҩģʽ
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckAutoDruged.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckAutoDruged.Checked).ToString();
            alControler.Add(tempControler.Clone());

            //���﷢ҩ�����Զ���ҩģʽ
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckAutoSend.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckAutoSend.Checked).ToString();
            alControler.Add(tempControler.Clone());

            //����Ԥ�ۿ������ 0ҽ��վԤ��1�շ�ʱԤ�� by Sunjh 2010-9-28 {0B55D338-67A8-415a-84F1-7287FB1454A5}
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.rbtChargePreOut.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.rbtChargePreOut.Checked).ToString();
            alControler.Add(tempControler.Clone());

            //�����䷢ҩʱ�Ƿ񵥺��ڼ������лس����Զ��䷢ҩȷ��by Sunjh 2010-11-1 {E1FDEF4A-BBA8-4210-BDBA-6FA8130244B9}
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.chkAutoSaveByEnter.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.chkAutoSaveByEnter.Checked).ToString();
            alControler.Add(tempControler.Clone());

            #endregion

            #region �ֵ�ά������

            ///����ҩƷ�Ƿ�����˺���Ч
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckNewDrugNeedApprove.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckNewDrugNeedApprove.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///ҩƷ�ֵ���Ϣ������ά����Ϣ����   {6F6120F5-6D88-47ce-AF9C-0CF781DE412F}
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = FS.HISFC.BizProcess.Integrate.PharmacyConstant.Set_Item_SpecialFlag;
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);

            string ctrlValue = "";
            if (this.ckItemSpeFlag1.Checked)
            {
                ctrlValue = ctrlValue + "A1" + this.txtItemSpeFlag1.Text;
            }
            else
            {
                ctrlValue = ctrlValue + "A0" + this.txtItemSpeFlag1.Text;
            }
            if (this.ckItemSpeFlag2.Checked)
            {
                ctrlValue = ctrlValue + "B1" + this.txtItemSpeFlag2.Text;
            }
            else
            {
                 ctrlValue = ctrlValue + "B0" + this.txtItemSpeFlag2.Text;
            }
            if (this.ckItemSpeFlag3.Checked)
            {
                ctrlValue = ctrlValue + "C1" + this.txtItemSpeFlag3.Text;
            }
            else
            {
                ctrlValue = ctrlValue + "C0" + this.txtItemSpeFlag3.Text;
            }
            tempControler.ControlerValue = ctrlValue;

            alControler.Add(tempControler.Clone());

            //�������ϲ���
            /////����Ա���޸�Ȩ��ʱ �Ƿ������ҩƷ��Ϣ��ѯ���
            //tempControler = new FS.HISFC.Models.Base.ControlParam();
            //tempControler.ID = this.ckQueryNoPriv.Tag.ToString();
            //tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            //tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckQueryNoPriv.Checked).ToString();
            //alControler.Add(tempControler.Clone());

            /////����Ա���޸�Ȩ��ʱ �Ƿ������ҩƷ��Ϣ����/��ӡ
            //tempControler = new FS.HISFC.Models.Base.ControlParam();
            //tempControler.ID = this.ckExportNoPriv.Tag.ToString();
            //tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            //tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckExportNoPriv.Checked).ToString();
            //alControler.Add(tempControler.Clone());

            ///ҩƷ��Ϣά��ʱ  ���ڰ�װ����������������λ��
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.nUDPackLength.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = this.nUDPackLength.Value.ToString();
            alControler.Add(tempControler.Clone());

            ///ҩƷ��Ϣά��ʱ  ���ڻ�������������������λ��
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.nUDBaseDoseLength.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = this.nUDBaseDoseLength.Value.ToString();
            alControler.Add(tempControler.Clone());

            ///ҩƷ��Ϣά��ʱ  ���ڼ۸�������������λ��
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.nUDPriceLength.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = this.nUDPriceLength.Value.ToString();
            alControler.Add(tempControler.Clone());

            ///��Ʒ���Զ�����������������λ��
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.nUDNameCodeLength.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = this.nUDNameCodeLength.Value.ToString();
            alControler.Add(tempControler.Clone());

            ///�����Զ�����������������λ��
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.nUDCodeLength.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = this.nUDCodeLength.Value.ToString();
            alControler.Add(tempControler.Clone());

            ///�Ƿ�����ͨ����ά�����Tab˳��
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckRegularTab.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckRegularTab.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///�Ƿ�����Ӣ����ά�����Tab˳��
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckEnglishTab.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckEnglishTab.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///�Ƿ��������/���ұ���ά�����Tab˳��
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckCodeTab.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckCodeTab.Checked).ToString();
            alControler.Add(tempControler.Clone());

            //�ò������� Э������ֻ�������洦��
            /////Э�������Ƿ������ ����������� ���շ�ʱ�����ϸ ���򲻽��в��
            //tempControler = new FS.HISFC.Models.Base.ControlParam();
            //tempControler.ID = this.ckNostrumManageStore.Tag.ToString();
            //tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            //tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckNostrumManageStore.Checked).ToString();

            alControler.Add(tempControler.Clone());

            #endregion

            #region ���������

            ///ҩƷ�̵��Ƿ����Ž����̵�
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckCheckBatch.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckCheckBatch.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///��ʷ�̵㵥��ȡʱ�Ƿ�ֻȡ���״̬
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.cmbCheckHistoryState.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            if (this.cmbCheckHistoryState.Text == "����̵㵥")
            {
                tempControler.ControlerValue = "1";
            }
            else
            {
                tempControler.ControlerValue = "0";
            }
            alControler.Add(tempControler.Clone());


            ///����λ�������������λ��
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.nUDMaxPlace.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = this.nUDMaxPlace.Value.ToString();
            alControler.Add(tempControler.Clone());

            ///�Ƿ�������Ч�ھ�ʾ
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckValidEnable.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckValidEnable.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///��Ч�����ʾ����
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.nUDWarnDays.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = this.nUDWarnDays.Value.ToString();
            alControler.Add(tempControler.Clone());

            ///��Ч�ھ�ʾ��ɫ
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.btValidColor.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = this.btValidColor.BackColor.ToArgb().ToString();
            alControler.Add(tempControler.Clone());

            ///��Ч�ڲ���ʵʱ��ȡ��ȡ��ʽ
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckValidRealTime.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckValidRealTime.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///�Ƿ���ÿ�������ޱ���
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckStoreValid.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckStoreValid.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///����ʱ�Ƿ���õ�����Ϣ��ʽ ��ʹ��������ɫ��ʾ
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckWarnMsg.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckWarnMsg.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///����������ɫ��ʾ
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.btStoreColor.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = this.btStoreColor.BackColor.ToArgb().ToString();
            alControler.Add(tempControler.Clone());

            #endregion

            #region �����������

            ///�Զ����ɼƻ�ʱ �����վ��������������� ͳ��Ĭ������
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.nUDExpandDays.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = this.nUDExpandDays.Value.ToString();
            alControler.Add(tempControler.Clone());

            ///�б�ѡ��ؼ��Ƿ���ʾ�б���
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckPlanRowHeader.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckPlanRowHeader.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///�Ƿ�����ͨ��������ȷ��ѡ������
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckNumSelectRow.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckNumSelectRow.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///�Ƿ�Լƻ����Ƿ�Ϊ������ж�
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckPlanZeroValid.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckPlanZeroValid.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///�ɹ��Ƿ���Ҫ��˺���Ч
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckStockNeedApprove.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckStockNeedApprove.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///����ʱ�Ƿ����ѡ������
            ///{DE934736-B2C2-44a4-A218-2DC38E1620BA}
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckOutChooseBatch.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckOutChooseBatch.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///�ɹ����ʱ �Ƿ������޸���Ӧ�ļƻ���Ϣ
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckEditInplan.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckEditInplan.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///�ɹ��ƻ�/���ʱ �Ƿ������޸ļƻ������
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckEditPrice.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckEditPrice.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///�ɹ�ָ��ʱ�Ƿ�ʹ���ֵ���Ϣ��Ĭ�ϵĹ�����˾
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckStockUseDefaultData.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckStockUseDefaultData.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///ҩƷ����Ƿ���Ҫ��� ԭ���� 500002
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckInputNeedApprove.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckInputNeedApprove.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///�˳�ʱ�Ƿ񱣴���һ�β���Ȩ������
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckInSavePriv.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckInSavePriv.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///ҩƷ��׼���ʱ�Ƿ����ҩƷ�ֵ���Ϣ�����
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckInEditPrice.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckInEditPrice.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///���ó���ʱ�Ƿ�ʹ��������
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.ckTransferOutUseWholePrice.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.ckTransferOutUseWholePrice.Checked).ToString();
            alControler.Add(tempControler.Clone());

            ///���ó���Ĭ�ϼӼ���
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.txtTransferOutRate.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToDecimal(this.txtTransferOutRate.Text).ToString();
            alControler.Add(tempControler.Clone());

            ///ҩ��/ҩ��ͨ�ò�ѯ����            
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.txtCommonQuery.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = this.txtCommonQuery.Text;
            alControler.Add(tempControler.Clone());

            ///ҩ���ѯ����
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.txtPIQuery.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = this.txtPIQuery.Text;
            alControler.Add(tempControler.Clone());

            //һ������Ƿ�����ϴ�����Զ������Ч�ڡ����š������ by Sunjh 2010-10-28 {97C93751-7EED-4160-931A-EC77C1F4E291}
            tempControler = new FS.HISFC.Models.Base.ControlParam();
            tempControler.ID = this.chkIsAutoFillInputInfo.Tag.ToString();
            tempControler.Name = this.phaConsInfo.GetParamDescription(tempControler.ID);
            tempControler.ControlerValue = FS.FrameWork.Function.NConvert.ToInt32(this.chkIsAutoFillInputInfo.Checked).ToString();
            alControler.Add(tempControler.Clone());

            #endregion

            #region ���ز���

            string strErr = "";

            FS.FrameWork.WinForms.Classes.Function.SaveDefaultValue("ClinicDrug", "PrintList", out strErr,FS.FrameWork.Function.NConvert.ToInt32(this.ckPrintLabel.Checked).ToString());         

            FS.FrameWork.WinForms.Classes.Function.SaveDefaultValue("ClinicDrug", "TerminalCode", out strErr, this.txtDefaultTCode.Text);
            #endregion

            return alControler;
        }

        #endregion

        #region IControlParamMaint ��Ա

        public int Apply()
        {
            throw new Exception("The method or operation is not implemented.");
            
            
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        public string ErrText
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public int Init()
        {

            #region סԺҩ������

            #region �������

            ///ҩ����������Ƿ��ά��Ϊ���ɲ�ֵ���ȡ��
            this.ckNosplitAndDayToInteger.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Can_Set_NosplitAndDayToInteger, true, true);
            this.ckNosplitAndDayToInteger.Tag = PharmacyConstant.Can_Set_NosplitAndDayToInteger;
            this.hsOriginalParam.Add(PharmacyConstant.Can_Set_NosplitAndDayToInteger, this.ckNosplitAndDayToInteger.Checked.ToString());

            //ҩ����������Ƿ��ά��Ϊ���ɲ��
            this.ckNoSplitAtAll.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Can_Set_NoSplitAtAll, true, true);
            this.ckNoSplitAtAll.Tag = PharmacyConstant.Can_Set_NoSplitAtAll;
            this.hsOriginalParam.Add(PharmacyConstant.Can_Set_NoSplitAtAll, this.ckNoSplitAtAll.Checked.ToString());

            ///ҩƷ��������Ƿ������Ϊ�ɲ�ֲ�ȡ��
            this.ckSplitAndNoInteger.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Can_Set_SplitAndNoInteger, true, false);
            this.ckSplitAndNoInteger.Tag = PharmacyConstant.Can_Set_SplitAndNoInteger;
            this.hsOriginalParam.Add(PharmacyConstant.Can_Set_SplitAndNoInteger, this.ckSplitAndNoInteger.Checked.ToString());

            ///ҩƷ��������Ƿ������Ϊ�ɲ����ȡ��
            this.ckSplitAndUpperToInteger.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Can_Set_SplitAndUpperToInteger, true, true);
            this.ckSplitAndUpperToInteger.Tag = PharmacyConstant.Can_Set_SplitAndUpperToInteger;
            this.hsOriginalParam.Add(PharmacyConstant.Can_Set_SplitAndUpperToInteger, this.ckSplitAndUpperToInteger.Checked.ToString());

            ///ҩƷ��������Ƿ������Ϊ�ɲ�ְ�����ȡ��
            this.ckSplitAndDeptToInteger.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Can_Set_SplitAndDeptToInteger, true, false);
            this.ckSplitAndDeptToInteger.Tag = PharmacyConstant.Can_Set_SplitAndDeptToInteger;
            this.hsOriginalParam.Add(PharmacyConstant.Can_Set_SplitAndDeptToInteger, this.ckSplitAndDeptToInteger.Checked.ToString());

            ///ҩƷ��������Ƿ������Ϊ������ȡ��
            this.ckSplitAndNurceCellToInteger.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Can_Set_SplitAndNurceCellToInteger, true, false);
            this.ckSplitAndNurceCellToInteger.Tag = PharmacyConstant.Can_Set_SplitAndNurceCellToInteger;
            this.hsOriginalParam.Add(PharmacyConstant.Can_Set_SplitAndNurceCellToInteger, this.ckSplitAndNurceCellToInteger.Checked.ToString());

            ///ҩƷ��������Ƿ������Ϊ������ȡ�� {9D24EE50-57C5-4cae-8B70-8D8C3E7B4BDC}
            this.ckNosplitAndPackUnit.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Can_Set_NoSplitAndPackUnit, true, false);
            this.ckNosplitAndPackUnit.Tag = PharmacyConstant.Can_Set_NoSplitAndPackUnit;
            this.hsOriginalParam.Add(PharmacyConstant.Can_Set_NoSplitAndPackUnit, this.ckNosplitAndPackUnit.Checked.ToString());

            #endregion

            #region ��ҩ���̿���
            
            ///��ҩ�Ƿ����׼
            this.ckInNeedApprove.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.InDrug_Need_Approve, true, true);
            this.ckInNeedApprove.Tag = PharmacyConstant.InDrug_Need_Approve;
            this.hsOriginalParam.Add(PharmacyConstant.InDrug_Need_Approve, this.ckInNeedApprove.Checked.ToString());

            ///�Ƿ�ҩʦ�ſ��Ժ�׼
            this.ckInNeedPriv.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.InDrug_Need_Priv, true, false);
            this.ckInNeedPriv.Tag = PharmacyConstant.InDrug_Need_Priv;
            this.hsOriginalParam.Add(PharmacyConstant.InDrug_Need_Priv, this.ckInNeedPriv.Checked.ToString());

            #endregion

            #region ��ҩ���ܿ���

            //�������ϲ���������Ϣ
            //this.ckInPartApprove.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.InDrug_Part_Approve, true, false);
            //this.ckInPartApprove.Tag = PharmacyConstant.InDrug_Part_Approve;
            //this.hsOriginalParam.Add(PharmacyConstant.InDrug_Part_Approve, this.ckInPartApprove.Checked.ToString());

            this.ckInPatientPreOut.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.InDrug_Pre_Out, true, true);
            this.ckInPatientPreOut.Tag = PharmacyConstant.InDrug_Pre_Out;
            this.hsOriginalParam.Add(PharmacyConstant.InDrug_Pre_Out, this.ckInPatientPreOut.Checked.ToString());

            //�������ϲ���������Ϣ
            //this.ckAutoPrintOutput.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.InDrug_AutoPrint_Output, true, false);
            //this.ckAutoPrintOutput.Tag = PharmacyConstant.InDrug_AutoPrint_Output;
            //this.hsOriginalParam.Add(PharmacyConstant.InDrug_AutoPrint_Output, this.ckAutoPrintOutput.Checked.ToString());

            #endregion

            #region ��ҩ�������

            ///��ҩ�Ƿ�Ԥ��
            ///            //�������ϲ���������Ϣ
            //this.ckInPriviewBill.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.InDrug_Priview_Bill, true, false);
            //this.ckInPriviewBill.Tag = PharmacyConstant.InDrug_Priview_Bill;

            ///�¼���Ϣ�Ƿ��Զ�ѡ��
            this.ckInAutoCheck.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.InDrug_Auto_Check, true, false);
            this.ckInAutoCheck.Tag = PharmacyConstant.InDrug_Auto_Check;

            ///�Ƿ���ʾ���߻���
            this.ckPatientTot.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.InDrug_Show_PatientTot, true, false);
            this.ckPatientTot.Tag = PharmacyConstant.InDrug_Show_PatientTot;

            ///�Ƿ���ʾ���һ���
            this.ckDeptTot.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.InDrug_Show_DeptTot, true, false);
            this.ckDeptTot.Tag = PharmacyConstant.InDrug_Show_DeptTot;

            ///�Ƿ���ʾ�������ȷ�ʽ��ʾ
            this.ckDeptFirst.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.InDrug_Show_DeptFirst, true, true);
            this.ckDeptFirst.Tag = PharmacyConstant.InDrug_Show_DeptFirst;
            #endregion

            #endregion

            #region ����ҩ������

            ///���﷢ҩ�ն˱���� �Ƿ�ˢ��
            this.ckSaveRefresh.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Terminal_Save_Refresh, true, true);
            this.ckSaveRefresh.Tag = PharmacyConstant.Terminal_Save_Refresh;

            ///���﷢ҩ�����Ƿ���ʾ����
            this.ckOutShowDays.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.OutDrug_Show_Days, true, false);
            this.ckOutShowDays.Tag = PharmacyConstant.OutDrug_Show_Days;

            ///��ҩȷ�Ϻ��Ƿ��ӡ��ҩ�嵥
            this.ckOutPrintList.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.OutDrug_Print_List, true, false);
            this.ckOutPrintList.Tag = PharmacyConstant.OutDrug_Print_List;

            ///��ҩȷ�Ϻ��Ƿ��ӡ����
            this.ckOutPrintRecipe.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.OutDrug_Print_Recipe, true, false);
            this.ckOutPrintRecipe.Tag = PharmacyConstant.OutDrug_Print_Recipe;

            ///��ҩ����ʱ���Ų���λ�� ��1 ���貹λ
            this.ndUOperLength.Value = this.ctrlIntegrate.GetControlParam<decimal>(PharmacyConstant.OutDrug_OperCode_Length, true, 6);
            this.ndUOperLength.Tag = PharmacyConstant.OutDrug_OperCode_Length;

            ///��/��ҩ�����Ƿ����Ȩ�޿��� (ֻ��ҩʦ���Բ���) 
            this.ckOutNeedPriv.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.OutDrug_Need_Priv, true, true);
            this.ckOutNeedPriv.Tag = PharmacyConstant.OutDrug_Need_Priv;

            ///�����ǩ�Զ���ӡʱ  �Ƿ�Դ������ϼ�¼�Ĵ�������ӡ
            this.ckPrintBackRecipe.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.OutDrug_Print_BackRecipe, true, false);
            this.ckPrintBackRecipe.Tag = PharmacyConstant.OutDrug_Print_BackRecipe;

            ///������ҩʱ �Ƿ���п�澯���ж� 
            this.ckClinicWarDrug.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.OutDrug_Warn_Druged, true, false);
            this.ckClinicWarDrug.Tag = PharmacyConstant.OutDrug_Warn_Druged;

            ///���﷢ҩʱ �Ƿ���п�澯���ж� 
            this.ckClinicWarnSend.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.OutDrug_Warn_Send, true, false);
            this.ckClinicWarnSend.Tag = PharmacyConstant.OutDrug_Warn_Send;

            ///�����շ�ʱ�Ƿ����Ԥ�ۿ�����
            this.ckClinicPreOut.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.OutDrug_Pre_Out, true, true);
            this.ckClinicPreOut.Tag = PharmacyConstant.OutDrug_Pre_Out;

            this.ckShowOldSendedInfo.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.OutDrug_Show_OldSended, true, false);
            this.ckShowOldSendedInfo.Tag = PharmacyConstant.OutDrug_Show_OldSended;

            //������ҩʱ�����Զ���ҩģʽ
            this.ckAutoDruged.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.OutDrug_Auto_Druged, true, false);
            this.ckAutoDruged.Tag = PharmacyConstant.OutDrug_Auto_Druged;

            //������ҩʱ�����Զ���ҩģʽ
            this.ckAutoSend.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.OutDrug_Auto_Send, true, false);
            this.ckAutoSend.Tag = PharmacyConstant.OutDrug_Auto_Send;

            //����Ԥ�ۿ������ 0ҽ��վԤ��1�շ�ʱԤ�� by Sunjh 2010-9-28 {0B55D338-67A8-415a-84F1-7287FB1454A5}
            this.rbtChargePreOut.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.OutDrug_Pre_Out_Type, true, true);
            this.rbtDoctorPreOut.Checked = !this.rbtChargePreOut.Checked;
            this.rbtChargePreOut.Tag = PharmacyConstant.OutDrug_Pre_Out_Type;

            //�����䷢ҩʱ�Ƿ񵥺��ڼ������лس����Զ��䷢ҩȷ��by Sunjh 2010-11-1 {E1FDEF4A-BBA8-4210-BDBA-6FA8130244B9}
            this.chkAutoSaveByEnter.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.OutDrug_AutoSave_ByEnter, true, false);
            this.chkAutoSaveByEnter.Tag = PharmacyConstant.OutDrug_AutoSave_ByEnter;

            #endregion

            #region �ֵ�ά������

            ///����ҩƷ�Ƿ�����˺���Ч
            this.ckNewDrugNeedApprove.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.NewDrug_Need_Approve, true, false);
            this.ckNewDrugNeedApprove.Tag = PharmacyConstant.NewDrug_Need_Approve;

            ///ҩƷ�ֵ���Ϣ������ά����Ϣ����  {6F6120F5-6D88-47ce-AF9C-0CF781DE412F}
            string ctrlValue = this.ctrlIntegrate.GetControlParam<string>( PharmacyConstant.Set_Item_SpecialFlag, true,"");

            if (ctrlValue.IndexOf( "A" ) != -1 && ctrlValue.IndexOf( "B" ) != -1 && ctrlValue.IndexOf( "C" ) != -1)
            {
                string strFlag1 = ctrlValue.Substring( 0, ctrlValue.IndexOf( "B" ) );
                string strFlag2 = ctrlValue.Substring( ctrlValue.IndexOf( "B" ), ctrlValue.IndexOf( "C" ) - ctrlValue.IndexOf( "B" ) );
                string strFlag3 = ctrlValue.Substring( ctrlValue.IndexOf( "C" ) );

                this.ckItemSpeFlag1.Checked = FS.FrameWork.Function.NConvert.ToBoolean( strFlag1.Substring( 1, 1 ) );       //�Ƿ�ѡ��
                this.txtItemSpeFlag1.Text = strFlag1.Substring( 2 );

                this.ckItemSpeFlag2.Checked = FS.FrameWork.Function.NConvert.ToBoolean( strFlag2.Substring( 1, 1 ) );       //�Ƿ�ѡ��
                this.txtItemSpeFlag2.Text = strFlag2.Substring( 2 );

                this.ckItemSpeFlag3.Checked = FS.FrameWork.Function.NConvert.ToBoolean( strFlag3.Substring( 1, 1 ) );       //�Ƿ�ѡ��
                this.txtItemSpeFlag3.Text = strFlag3.Substring( 2 );
            }

            //�������ϲ���
            ///����Ա���޸�Ȩ��ʱ �Ƿ������ҩƷ��Ϣ��ѯ���
            //this.ckQueryNoPriv.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Query_No_EditPriv, true, true);
            //this.ckQueryNoPriv.Tag = PharmacyConstant.Query_No_EditPriv;

            /////����Ա���޸�Ȩ��ʱ �Ƿ������ҩƷ��Ϣ����/��ӡ
            //this.ckExportNoPriv.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Export_No_EditPriv, true, true);
            //this.ckExportNoPriv.Tag = PharmacyConstant.Export_No_EditPriv;

            ///ҩƷ��Ϣά��ʱ  ���ڰ�װ����������������λ��
            this.nUDPackLength.Value = this.ctrlIntegrate.GetControlParam<decimal>(PharmacyConstant.Max_PackQty_Digit, true, 4);
            this.nUDPackLength.Tag = PharmacyConstant.Max_PackQty_Digit;

            ///ҩƷ��Ϣά��ʱ  ���ڻ�������������������λ��
            this.nUDBaseDoseLength.Value = this.ctrlIntegrate.GetControlParam<decimal>(PharmacyConstant.Max_BaseDose_Digit, true, 10);
            this.nUDBaseDoseLength.Tag = PharmacyConstant.Max_BaseDose_Digit;

            ///ҩƷ��Ϣά��ʱ  ���ڼ۸�������������λ��
            this.nUDPriceLength.Value = this.ctrlIntegrate.GetControlParam<decimal>(PharmacyConstant.Max_Price_Digit, true, 12);
            this.nUDPriceLength.Tag = PharmacyConstant.Max_Price_Digit;

            ///��Ʒ���Զ�����������������λ��
            this.nUDNameCodeLength.Value = this.ctrlIntegrate.GetControlParam<decimal>(PharmacyConstant.Max_NameCustomeCode_Digit, true, 16);
            this.nUDNameCodeLength.Tag = PharmacyConstant.Max_NameCustomeCode_Digit;

            ///�����Զ�����������������λ��
            this.nUDCodeLength.Value = this.ctrlIntegrate.GetControlParam<decimal>(PharmacyConstant.Max_CustomeCode_Digit, true, 16);
            this.nUDCodeLength.Tag = PharmacyConstant.Max_CustomeCode_Digit;

            ///�Ƿ�����ͨ����ά�����Tab˳��
            this.ckRegularTab.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Have_Regular_Tab, true, false);
            this.ckRegularTab.Tag = PharmacyConstant.Have_Regular_Tab;

            ///�Ƿ�����Ӣ����ά�����Tab˳��
            this.ckEnglishTab.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Have_English_Tab, true, false);
            this.ckEnglishTab.Tag = PharmacyConstant.Have_English_Tab;

            ///�Ƿ��������/���ұ���ά�����Tab˳��
            this.ckCodeTab.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Have_Code_Tab, true, false);
            this.ckCodeTab.Tag = PharmacyConstant.Have_Code_Tab;

            //�ò������� Э������ֻ�������洦��
            /////Э�������Ƿ������ ����������� ���շ�ʱ�����ϸ ���򲻽��в��
            //this.ckNostrumManageStore.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Nostrum_Manage_Store, true, true);
            //this.ckNostrumManageStore.Tag = PharmacyConstant.Nostrum_Manage_Store;

            #endregion

            #region ���������

            ///ҩƷ�̵��Ƿ����Ž����̵�
            this.ckCheckBatch.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Check_With_Batch, true, false);
            this.ckCheckBatch.Tag = PharmacyConstant.Check_With_Batch;

            ///��ʷ�̵㵥��ȡʱ�Ƿ�ֻȡ���״̬
            string historyState = this.ctrlIntegrate.GetControlParam<string>(PharmacyConstant.Check_History_State, true, "1");
            if (historyState == "1")
            {
                this.cmbCheckHistoryState.Text = "����̵㵥";
            }
            else
            {
                this.cmbCheckHistoryState.Text = "����̵㵥";
            }
            this.cmbCheckHistoryState.Tag = PharmacyConstant.Check_History_State;

            ///����λ�������������λ��
            this.nUDMaxPlace.Value = this.ctrlIntegrate.GetControlParam<decimal>(PharmacyConstant.Max_Place_Code, true, 12);
            this.nUDMaxPlace.Tag = PharmacyConstant.Max_Place_Code;

            ///�Ƿ�������Ч�ھ�ʾ
            this.ckValidEnable.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Valid_Warn_Enabled, true, false);
            this.ckValidEnable.Tag = PharmacyConstant.Valid_Warn_Enabled;

            ///��Ч�����ʾ����
            this.nUDWarnDays.Value = this.ctrlIntegrate.GetControlParam<decimal>(PharmacyConstant.Valid_Warn_Days, true, 60);
            this.nUDWarnDays.Tag = PharmacyConstant.Valid_Warn_Days;

            ///��Ч�ھ�ʾ��ɫ
            this.btValidColor.BackColor = Color.FromArgb(this.ctrlIntegrate.GetControlParam<int>(PharmacyConstant.Valid_Warn_Color, true, System.Drawing.Color.Red.ToArgb()));
            this.btValidColor.Tag = PharmacyConstant.Valid_Warn_Color;

            ///��Ч�ڲ���ʵʱ��ȡ��ȡ��ʽ
            this.ckValidRealTime.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Valid_Warn_SourceRealTime,true,true);
            this.ckValidRealTime.Tag = PharmacyConstant.Valid_Warn_SourceRealTime;
            
            ///�Ƿ���ÿ�������ޱ���
            this.ckStoreValid.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Store_Warn_Enabled,true,false);
            this.ckStoreValid.Tag = PharmacyConstant.Store_Warn_Enabled;

            ///����ʱ�Ƿ���õ�����Ϣ��ʽ ��ʹ��������ɫ��ʾ
            this.ckWarnMsg.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Store_Warn_Msg,true,false);
            this.ckWarnMsg.Tag = PharmacyConstant.Store_Warn_Msg;

            ///����������ɫ��ʾ
            this.btStoreColor.BackColor = Color.FromArgb(this.ctrlIntegrate.GetControlParam<int>(PharmacyConstant.Store_Warn_Color, true, System.Drawing.Color.Blue.ToArgb()));
            this.btStoreColor.Tag = PharmacyConstant.Store_Warn_Color;

            #endregion

            #region �����������

            ///�Զ����ɼƻ�ʱ �����վ��������������� ͳ��Ĭ������
            this.nUDExpandDays.Value = this.ctrlIntegrate.GetControlParam<decimal>(PharmacyConstant.Plan_Expand_Days, true, 30);
            this.nUDExpandDays.Tag = PharmacyConstant.Plan_Expand_Days;

            ///�б�ѡ��ؼ��Ƿ���ʾ�б���
            this.ckPlanRowHeader.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Plan_Show_RowHeader, true, true);
            this.ckPlanRowHeader.Tag = PharmacyConstant.Plan_Show_RowHeader;

            ///�Ƿ�����ͨ��������ȷ��ѡ������
            this.ckNumSelectRow.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Plan_Num_SelectRow, true, false);
            this.ckNumSelectRow.Tag = PharmacyConstant.Plan_Num_SelectRow;

            ///�Ƿ�Լƻ����Ƿ�Ϊ������ж�
            this.ckPlanZeroValid.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Plan_NumZero_Valid, true, true);
            this.ckPlanZeroValid.Tag = PharmacyConstant.Plan_NumZero_Valid;

            ///�ɹ��Ƿ���Ҫ��˺���Ч
            this.ckStockNeedApprove.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Stock_Need_Approve, true, true);
            this.ckStockNeedApprove.Tag = PharmacyConstant.Stock_Need_Approve;

            ///����ʱ�Ƿ����ѡ������
            ///{DE934736-B2C2-44a4-A218-2DC38E1620BA}
            this.ckOutChooseBatch.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Out_Choose_BatchNO, true, false);
            this.ckOutChooseBatch.Tag = PharmacyConstant.Out_Choose_BatchNO;

            ///�ɹ����ʱ �Ƿ������޸���Ӧ�ļƻ���Ϣ
            this.ckEditInplan.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Stock_Edit_InPlan, true, false);
            this.ckEditInplan.Tag = PharmacyConstant.Stock_Edit_InPlan;

            ///�ɹ��ƻ�/���ʱ �Ƿ������޸ļƻ������
            this.ckEditPrice.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Stock_Edit_Price, true, false);
            this.ckEditPrice.Tag = PharmacyConstant.Stock_Edit_Price;

            ///�ɹ�ָ��ʱ�Ƿ�ʹ���ֵ���Ϣ��Ĭ�ϵĹ�����˾
            this.ckStockUseDefaultData.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Stock_Use_DefaultData, true, true);
            this.ckStockUseDefaultData.Tag = PharmacyConstant.Stock_Use_DefaultData;

            ///ҩƷ����Ƿ���Ҫ��� ԭ���� 500002
            this.ckInputNeedApprove.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.In_Need_Approve, true, true);
            this.ckInputNeedApprove.Tag = PharmacyConstant.In_Need_Approve;

            ///�˳�ʱ�Ƿ񱣴���һ�β���Ȩ������
            this.ckInSavePriv.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.In_Save_Priv, true, true);
            this.ckInSavePriv.Tag = PharmacyConstant.In_Save_Priv;

            ///ҩƷ��׼���ʱ�Ƿ����ҩƷ�ֵ���Ϣ�����
            this.ckInEditPrice.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.In_EditPrice_WhenApprove, true, false);
            this.ckInEditPrice.Tag = PharmacyConstant.In_EditPrice_WhenApprove;

            ///���ó���ʱ�Ƿ�ʹ��������
            this.ckTransferOutUseWholePrice.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.Out_Transfer_UseWholePrice, true, false);
            this.ckTransferOutUseWholePrice.Tag = PharmacyConstant.Out_Transfer_UseWholePrice;

            ///���ó���ʱ�Ƿ�ʹ��������
            this.txtTransferOutRate.Text = this.ctrlIntegrate.GetControlParam<decimal>(PharmacyConstant.Out_Transfer_DefaultRate, true, 1.05M).ToString();
            this.txtTransferOutRate.Tag = PharmacyConstant.Out_Transfer_DefaultRate;

            ///ҩ��/ҩ��ͨ�ò�ѯ����
            this.txtCommonQuery.Text = this.ctrlIntegrate.GetControlParam<string>(PharmacyConstant.Query_Commo_Type, true, "���|In,����|Out,�̵�|Check,����|Adjust");
            this.txtCommonQuery.Tag = PharmacyConstant.Query_Commo_Type;

            ///ҩ���ѯ����
            this.txtPIQuery.Text = this.ctrlIntegrate.GetControlParam<string>(PharmacyConstant.Query_PI_Type, true, "���ƻ�|InPlan,�ɹ�|Stock,̨��|Record");
            this.txtPIQuery.Tag = PharmacyConstant.Query_PI_Type;

            //һ������Ƿ�����ϴ�����Զ������Ч�ڡ����š������ by Sunjh 2010-10-28 {97C93751-7EED-4160-931A-EC77C1F4E291}
            this.chkIsAutoFillInputInfo.Checked = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.CommonInput_Auto_FillInfo, true, true);
            this.chkIsAutoFillInputInfo.Tag = PharmacyConstant.CommonInput_Auto_FillInfo;

            #endregion

            #region ���ز���

            string strErr = "";

            ArrayList alValue = FS.FrameWork.WinForms.Classes.Function.GetDefaultValue("ClinicDrug", "PrintList", out strErr);
            if (alValue == null || alValue.Count == 0)
            {
                this.ckPrintLabel.Checked = true;
            }
            else
            {
                this.ckPrintLabel.Checked = false;
            }
            this.ckPrintLabel.Tag = "LocalParam";

            ArrayList alTCode = FS.FrameWork.WinForms.Classes.Function.GetDefaultValue("ClinicDrug", "TerminalCode", out strErr);
            if (alTCode == null || alTCode.Count == 0)
            {
                this.txtDefaultTCode.Text = "";
            }
            else
            {
                this.txtDefaultTCode.Text = alTCode[0] as string;
            }
            this.txtDefaultTCode.Tag = "LocalParam";
            #endregion

            return 1;
        }

        public bool IsModify
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public bool IsShowOwnButtons
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public int Save()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            List<FS.HISFC.Models.Base.ControlParam> alCtrlList = this.GetControlerList();
            if (alCtrlList == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();

                return -1;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(FS.FrameWork.Management.Language.Msg("���ڱ��� ���Ժ�..."));
            Application.DoEvents();

            this.managerIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            foreach (FS.HISFC.Models.Base.ControlParam c in alCtrlList)
            {
                int iReturn = this.managerIntegrate.InsertControlerInfo(c);
                if (iReturn == -1)
                {
                    //�����ظ���˵���Ѿ����ڲ���ֵ,��ôֱ�Ӹ���
                    if (managerIntegrate.DBErrCode == 1)
                    {
                        iReturn = this.managerIntegrate.UpdateControlerInfo(c);
                        if (iReturn <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show("���¿��Ʋ���[" + c.Name + "]ʧ��! ���Ʋ���ֵ:" + c.ID + "\n������Ϣ:" + this.managerIntegrate.Err);

                            return -1;
                        }
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("������Ʋ���[" + c.Name + "]ʧ��! ���Ʋ���ֵ:" + c.ID + "\n������Ϣ:" + this.managerIntegrate.Err);

                        return -1;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            MessageBox.Show("����ɹ�!");

            return 1;
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            if (this.neuTabControl1.TabPages.Contains(this.tabPage5))
            {
                this.neuTabControl1.TabPages.Remove(this.tabPage5);
            }
            base.OnLoad(e);
        }

        private void btnValidColorSet_Click(object sender, EventArgs e)
        {
            DialogResult result = this.colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.btValidColor.BackColor = colorDialog1.Color;
            }
        }

        private void btnStoreColorSet_Click(object sender, EventArgs e)
        {
            DialogResult result = this.colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.btStoreColor.BackColor = colorDialog1.Color;
            }
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();

            return base.OnSave(sender, neuObject);
        }

        private void ckItemSpeFlag1_CheckedChanged(object sender, EventArgs e)
        {
            this.txtItemSpeFlag1.Enabled = this.ckItemSpeFlag1.Checked;
        }

        private void ckItemSpeFlag2_CheckedChanged(object sender, EventArgs e)
        {
            this.txtItemSpeFlag2.Enabled = this.ckItemSpeFlag2.Checked;
        }

        private void ckItemSpeFlag3_CheckedChanged(object sender, EventArgs e)
        {
            this.txtItemSpeFlag3.Enabled = this.ckItemSpeFlag3.Checked;
        }

        private void ckClinicPreOut_CheckedChanged(object sender, EventArgs e)
        {
            if (ckClinicPreOut.Checked)
            {
                this.rbtDoctorPreOut.Enabled = true;
                this.rbtChargePreOut.Enabled = true;
            }
            else
            {
                this.rbtDoctorPreOut.Enabled = false;
                this.rbtChargePreOut.Enabled = false; 
            }
        }

    }
}
