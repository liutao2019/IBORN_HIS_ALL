using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Neusoft.HISFC.Models.Operation;

namespace Neusoft.HISFC.Components.Operation
{
    /// <summary>
    /// [��������: �������ſؼ�]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-12-04]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucArrangement : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucArrangement()
        {
            InitializeComponent();
        }

        #region �ֶ�
        private Neusoft.FrameWork.WinForms.Forms.ToolBarService toolBarService = new Neusoft.FrameWork.WinForms.Forms.ToolBarService();
// {CB2F6DC4-F9C6-4756-A118-CEDB907C39EC}
        private Neusoft.HISFC.BizProcess.Integrate.RADT radtIntegrate = new Neusoft.HISFC.BizProcess.Integrate.RADT();
        #endregion

        #region ����

        /// <summary>
        /// ˢ�����������б�
        /// </summary>
        /// <returns></returns>
        public int RefreshApplys()
        {
            this.ucArrangementSpread1.Reset();

            //��ʼʱ��
            string beginTime = this.neuDateTimePicker1.Value.Date.ToString();
            //����ʱ��
            string endTime = this.neuDateTimePicker1.Value.Date.AddDays(1).ToString();

            //neusoft.neNeusoft.HISFC.Components.Interface.Classes.Function.ShowWaitForm("������������,���Ժ�...");
            Application.DoEvents();
            ArrayList alApplys;
            try
            {
                this.ucArrangementSpread1.Reset();
                alApplys = Environment.OperationManager.GetOpsAppList(Environment.OperatorDeptID, beginTime, endTime);
                if (alApplys != null)
                {
                    foreach (OperationAppllication apply in alApplys)
                    {
                        this.ucArrangementSpread1.AddOperationApplication(apply);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("��������������Ϣ����!" + e.Message, "��ʾ");
                return -1;
            }

            this.ucArrangementSpread1.SetFilter();

            //neusoft.neNeusoft.HISFC.Components.Interface.Classes.Function.HideWaitForm();
            //if (fpSpread1_Sheet1.RowCount > 0)
            //{
            //FarPoint.Win.Spread.LeaveCellEventArgs e = new FarPoint.Win.Spread.LeaveCellEventArgs
            //    (new FarPoint.Win.Spread.SpreadView(fpSpread1), 0, 0, 0, (int)Cols.opDate);
            //fpSpread1_LeaveCell(fpSpread1, e);
            //    fpSpread1.Focus();
            //    fpSpread1_Sheet1.SetActiveCell(0, (int)Cols.opDate, true);
            //}

            return 0;
        }
        #endregion

        #region �¼�

        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {

            this.toolBarService.AddToolButton("ȫ��", "ȫ��", Neusoft.FrameWork.WinForms.Classes.EnumImageList.F�ֽ�, true, true, null);
            this.toolBarService.AddToolButton("�Ѱ���", "�Ѱ���", Neusoft.FrameWork.WinForms.Classes.EnumImageList.D��ӡ��Һ��, true, false, null);
            this.toolBarService.AddToolButton("δ����", "δ����", Neusoft.FrameWork.WinForms.Classes.EnumImageList.D��ӡִ�е�, true, false, null);
            this.toolBarService.AddToolButton("����", "����������", Neusoft.FrameWork.WinForms.Classes.EnumImageList.Zת��, true, false, null);
            return this.toolBarService;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.ucArrangementSpread1.Date = this.neuDateTimePicker1.Value;
            this.ucArrangementSpread1.Print();
            return base.OnPrint(sender, neuObject);
        }
        protected override int OnQuery(object sender, object neuObject)
        {
            this.RefreshApplys();
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.ucArrangementSpread1.Save();
            return base.OnSave(sender, neuObject);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            if (e.ClickedItem.Text == "�Ѱ���")
            {
                this.toolBarService.GetToolButton("�Ѱ���").CheckState = System.Windows.Forms.CheckState.Checked;
                this.toolBarService.GetToolButton("ȫ��").CheckState = System.Windows.Forms.CheckState.Unchecked;
                this.toolBarService.GetToolButton("δ����").CheckState = System.Windows.Forms.CheckState.Unchecked;

                this.ucArrangementSpread1.Filter = ucArrangementSpread.EnumFilter.Already;
            }
            else if (e.ClickedItem.Text == "δ����")
            {
                this.toolBarService.GetToolButton("δ����").CheckState = System.Windows.Forms.CheckState.Checked;
                this.toolBarService.GetToolButton("ȫ��").CheckState = System.Windows.Forms.CheckState.Unchecked;
                this.toolBarService.GetToolButton("�Ѱ���").CheckState = System.Windows.Forms.CheckState.Unchecked;

                this.ucArrangementSpread1.Filter = ucArrangementSpread.EnumFilter.NotYet;
            }
            else if (e.ClickedItem.Text == "ȫ��")
            {
                this.toolBarService.GetToolButton("ȫ��").CheckState = System.Windows.Forms.CheckState.Checked;
                this.toolBarService.GetToolButton("δ����").CheckState = System.Windows.Forms.CheckState.Unchecked;
                this.toolBarService.GetToolButton("�Ѱ���").CheckState = System.Windows.Forms.CheckState.Unchecked;

                this.ucArrangementSpread1.Filter = ucArrangementSpread.EnumFilter.All;
            }
            else if (e.ClickedItem.Text == "����")
            {
                if (this.ucArrangementSpread1.ChangeDept() < 0) return;
                this.RefreshApplys();
            }

            base.ToolStrip_ItemClicked(sender, e);
        }
        #endregion

        private void ucArrangementSpread1_applictionSelected(object sender, OperationAppllication e)
        {

            if (e != null)
            {


                if (e.PatientSouce == "2")
                {
                    Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = radtIntegrate.GetPatientInfomation(e.PatientInfo.ID);

                    if (patientInfo == null)
                    {
                        MessageBox.Show(radtIntegrate.Err);
                        this.ucArrangementInfo1.OperationApplication = new OperationAppllication();

                        return;
                    }



                    //if ((Neusoft.HISFC.Models.Base.EnumInState)this.patientInfo.PVisit.InState.ID == Neusoft.HISFC.Models.Base.EnumInState.N
                    //    || (Neusoft.HISFC.Models.Base.EnumInState)this.patientInfo.PVisit.InState.ID == Neusoft.HISFC.Models.Base.EnumInState.O)
                    if (patientInfo.PVisit.InState.ID.ToString() == Neusoft.HISFC.Models.Base.EnumInState.N.ToString() || patientInfo.PVisit.InState.ID.ToString() == Neusoft.HISFC.Models.Base.EnumInState.O.ToString())
                    {
                        Neusoft.FrameWork.WinForms.Classes.Function.Msg("�û����Ѿ���Ժ!", 111);
                        //this.ucArrangementInfo1.OperationApplication = new OperationAppllication();
                        return;
                    }

                }

                this.ucArrangementInfo1.OperationApplication = e;
            }
        }
    }
}
