using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Preparation.Process
{
    /// <summary>
    /// <br></br>
    /// [��������: ���Ʒ��������¼��]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2008-03]<br></br>
    /// <˵��>
    /// </˵��>
    /// </summary>
    public partial class ucSemiAssayProcess : FS.HISFC.Components.Preparation.Process.ucProcessBase
    {
        public ucSemiAssayProcess()
        {
            InitializeComponent();

            this.Init();
        }

        /// <summary>
        /// ��Ա�б�
        /// </summary>
        System.Collections.ArrayList alStaticEmployee = null;

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected void Init()
        {
            #region ��ϣ�����ݳ�ʼ��

            this.cmbAssayResult.Tag = Function.NoneData;
            this.hsProcessControl.Add("AssayResult", this.cmbAssayResult);

            this.hsProcessControl.Add("AssayQty", this.numAssayNum);
            this.hsProcessControl.Add("ApplyOper", this.cmbApplyOper);
            this.hsProcessControl.Add("ApplyDate", this.dtpApplyDate);
            this.hsProcessControl.Add("AssayOper", this.cmbAssayOper);
            this.hsProcessControl.Add("AssayDate", this.dtpAssayDate);

            FS.HISFC.Models.Preparation.Process pItem = new FS.HISFC.Models.Preparation.Process();
            pItem.ProcessItem.ID = "AssayResult";
            pItem.ProcessItem.Name = "����ϸ�";
            this.hsProcessItem.Add(this.cmbAssayResult.Name, pItem.Clone());

            pItem.ProcessItem.ID = "AssayQty";
            pItem.ProcessItem.Name = "�ͼ���";
            this.hsProcessItem.Add(this.numAssayNum.Name, pItem.Clone());

            pItem.ProcessItem.ID = "ApplyOper";
            pItem.ProcessItem.Name = "�ͼ���";
            this.hsProcessItem.Add(this.cmbApplyOper.Name, pItem.Clone());

            pItem.ProcessItem.ID = "ApplyDate";
            pItem.ProcessItem.Name = "�ͼ�����";
            this.hsProcessItem.Add(this.dtpApplyDate.Name, pItem.Clone());

            pItem.ProcessItem.ID = "AssayOper";
            pItem.ProcessItem.Name = "������";
            this.hsProcessItem.Add(this.cmbAssayOper.Name, pItem.Clone());

            pItem.ProcessItem.ID = "AssayDate";
            pItem.ProcessItem.Name = "��������";
            this.hsProcessItem.Add(this.dtpAssayDate.Name, pItem.Clone());

            #endregion

            if (alStaticEmployee == null)
            {
                FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
                alStaticEmployee = managerIntegrate.QueryEmployeeAll();
                if (alStaticEmployee == null)
                {
                    MessageBox.Show("������Ա�б�������" + managerIntegrate.Err);
                    return;
                }
            }

            this.cmbApplyOper.AddItems(alStaticEmployee);
            this.cmbAssayOper.AddItems(alStaticEmployee);
        }

        #endregion

        /// <summary>
        /// ���ش���������Ϣ
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int QueryPrescriptionData()
        {
            string drugCode = this.preparation.Drug.ID;

            this.fpSemiAssay_Sheet1.Rows.Count = 0;

            FS.HISFC.BizLogic.Pharmacy.Preparation pprManager = new FS.HISFC.BizLogic.Pharmacy.Preparation();
            List<FS.HISFC.Models.Preparation.Prescription> al = pprManager.QueryDrugPrescription(drugCode, FS.HISFC.Models.Preparation.EnumMaterialType.Material);
            if (al == null)
            {
                MessageBox.Show(Language.Msg("��ȡ��ǰѡ���Ʒ�����ƴ�����Ϣ����\n" + drugCode));
                return -1;
            }
           
            foreach (FS.HISFC.Models.Preparation.Prescription info in al)
            {
                int i = this.fpSemiAssay_Sheet1.Rows.Count;

                this.fpSemiAssay_Sheet1.Rows.Add(i, 1);
                this.fpSemiAssay_Sheet1.Cells[i, (int)ReportColumnEnum.ColItemID].Text = info.Material.ID;
                this.fpSemiAssay_Sheet1.Cells[i, (int)ReportColumnEnum.ColItemName].Text = info.Material.Name;
                if (this.ProcessList != null)
                {
                    foreach (FS.HISFC.Models.Preparation.Process p in this.ProcessList)
                    {
                        if (p.ProcessItem.ID == info.Material.ID)
                        {
                            this.fpSemiAssay_Sheet1.Cells[i, (int)ReportColumnEnum.ColDes].Text = p.ResultStr;
                            this.fpSemiAssay_Sheet1.Cells[i, (int)ReportColumnEnum.ColContent].Text = p.Extend;
                            this.fpSemiAssay_Sheet1.Cells[i, (int)ReportColumnEnum.ColResult].Text = p.ResultQty.ToString();
                        }
                    }
                }
                else
                {
                    this.fpSemiAssay_Sheet1.Cells[i, (int)ReportColumnEnum.ColDes].Text = "";
                    this.fpSemiAssay_Sheet1.Cells[i, (int)ReportColumnEnum.ColContent].Text = "0";
                    this.fpSemiAssay_Sheet1.Cells[i, (int)ReportColumnEnum.ColResult].Text = "0";
                }

                this.fpSemiAssay_Sheet1.Rows[i].Tag = info.Material;
            }

            return 1;
        }

        /// <summary>
        /// �Ƽ����Ʒ��Ϣ����
        /// </summary>
        /// <param name="preparation"></param>
        /// <returns></returns>
        public new int SetPreparation(FS.HISFC.Models.Preparation.Preparation preparation)
        {
            this.lbPreparationInfo.Text = string.Format(this.strPreparation, preparation.Drug.Name, preparation.Drug.Specs, preparation.BatchNO, preparation.PlanQty, preparation.Unit);

            this.numAssayNum.Text = preparation.AssayQty.ToString();

            base.SetPreparation(preparation);

            return this.QueryPrescriptionData();
        }

        public override int PrintProcess()
        {
            return base.PrintProcess();
        }

        /// <summary>
        /// �����������̱���
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public override int SaveProcess()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            FS.HISFC.BizLogic.Pharmacy.Preparation pprManager = new FS.HISFC.BizLogic.Pharmacy.Preparation();

            DateTime sysTime = pprManager.GetDateTimeFromSysDateTime();

            for (int i = 0; i < this.fpSemiAssay_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.Preparation.Process p = this.GetProcessInstance(i);

                p.Oper.OperTime = sysTime;
                p.Oper.ID = pprManager.Operator.ID;

                if (pprManager.SetProcess(p) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�����Ƽ�����������Ϣʧ��" + pprManager.Err);
                }
            }

            this.preparation.AssayQty = FS.FrameWork.Function.NConvert.ToDecimal(this.numAssayNum.Text);

            if (base.SaveProcess(false) >= 1)
            {
                FS.FrameWork.Management.PublicTrans.Commit();

                MessageBox.Show("��������ִ����Ϣ����ɹ�");

                this.PrintProcess();
            }

            return 1;
        }

        /// <summary>
        /// ������������ȡFp��Ϣ
        /// </summary>
        /// <param name="rowIndex">������</param>
        /// <returns>�ɹ����ع���������Ϣ ʧ�ܷ���null</returns>
        protected FS.HISFC.Models.Preparation.Process GetProcessInstance(int rowIndex)
        {
            FS.HISFC.Models.Preparation.Process process = new FS.HISFC.Models.Preparation.Process();

            process.Preparation = this.preparation;
            process.ProcessItem.ID = this.fpSemiAssay_Sheet1.Cells[rowIndex, (int)ReportColumnEnum.ColItemID].Text;
            process.ProcessItem.Name = this.fpSemiAssay_Sheet1.Cells[rowIndex, (int)ReportColumnEnum.ColItemName].Text;
            process.ResultQty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSemiAssay_Sheet1.Cells[rowIndex, (int)ReportColumnEnum.ColResult].Text);      //��ʾ��
            process.Extend = this.fpSemiAssay_Sheet1.Cells[rowIndex, (int)ReportColumnEnum.ColContent].Text;        //����
            process.ResultStr = this.fpSemiAssay_Sheet1.Cells[rowIndex, (int)ReportColumnEnum.ColDes].Text;         //����
          
            return process;
        }

        #region ��ö��

        protected enum ReportColumnEnum
        {
            /// <summary>
            /// ��������
            /// </summary>
            ColItemName,
            /// <summary>
            /// ����
            /// </summary>
            ColDes,
            /// <summary>
            /// ����
            /// </summary>
            ColContent,
            /// <summary>
            /// ��ʾ��
            /// </summary>
            ColResult,
            /// <summary>
            /// ��������
            /// </summary>
            ColItemID
        }

        #endregion
    }
}
