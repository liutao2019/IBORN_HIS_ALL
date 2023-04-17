using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Preparation.Process
{
    /// <summary>
    /// <br></br>
    /// [��������: ���װ��������¼��]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2008-03]<br></br>
    /// <˵��>
    /// </˵��>
    /// </summary>
    public partial class ucPackageProcess : FS.HISFC.Components.Preparation.Process.ucProcessBase
    {
        public ucPackageProcess()
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

            this.cmbClear.Tag = Function.NoneData;
            this.hsProcessControl.Add("Clear", this.cmbClear);
            this.cmbClean.Tag = Function.NoneData;
            this.hsProcessControl.Add("EquipmentClean", this.cmbClean);

            this.hsProcessControl.Add("Regulation", this.txtRegulation);
            this.hsProcessControl.Add("Exucte", this.txtExucte);
            this.hsProcessControl.Add("Quantity", this.txtQuantity);
            this.hsProcessControl.Add("PackageOper", this.cmbPackageOper);
            this.hsProcessControl.Add("PackageDate", this.dtpPackageDate);
            this.hsProcessControl.Add("InceptOper", this.cmbInceptOper);
            this.hsProcessControl.Add("PackageQty", this.txtPackageNum);
            this.hsProcessControl.Add("WasteQty", this.txtWasteNum);
            this.hsProcessControl.Add("FinParam", this.txtFinParam);

            FS.HISFC.Models.Preparation.Process pItem = new FS.HISFC.Models.Preparation.Process();

            pItem.ProcessItem.ID = "PackageQty";
            pItem.ProcessItem.Name = "��Ʒ��";
            this.hsProcessItem.Add(this.txtPackageNum.Name, pItem.Clone());

            pItem.ProcessItem.ID = "WasteQty";
            pItem.ProcessItem.Name = "��Ʒ��";
            this.hsProcessItem.Add(this.txtWasteNum.Name, pItem.Clone());

            pItem.ProcessItem.ID = "FinParam";
            pItem.ProcessItem.Name = "��Ʒ��";
            this.hsProcessItem.Add(this.txtFinParam.Name, pItem.Clone());

            pItem.ProcessItem.ID = "Clear";
            pItem.ProcessItem.Name = "�Ƿ��峡";
            this.hsProcessItem.Add(this.cmbClear.Name, pItem.Clone());

            pItem.ProcessItem.ID = "EquipmentClean";
            pItem.ProcessItem.Name = "�Ƿ����";
            this.hsProcessItem.Add(this.cmbClean.Name, pItem.Clone());

            pItem.ProcessItem.ID = "Regulation";
            pItem.ProcessItem.Name = "���ִ��";
            this.hsProcessItem.Add(this.txtRegulation.Name, pItem.Clone());

            pItem.ProcessItem.ID = "Quantity";
            pItem.ProcessItem.Name = "�������";
            this.hsProcessItem.Add(this.txtQuantity.Name, pItem.Clone());

            pItem.ProcessItem.ID = "Exucte";
            pItem.ProcessItem.Name = "����ִ��";
            this.hsProcessItem.Add(this.txtExucte.Name, pItem.Clone());

            pItem.ProcessItem.ID = "PackageOper";
            pItem.ProcessItem.Name = "��װԱ";
            this.hsProcessItem.Add(this.cmbPackageOper.Name, pItem.Clone());

            pItem.ProcessItem.ID = "PackageDate";
            pItem.ProcessItem.Name = "��װ����";
            this.hsProcessItem.Add(this.dtpPackageDate.Name, pItem.Clone());

            pItem.ProcessItem.ID = "InceptOper";
            pItem.ProcessItem.Name = "����Ա";
            this.hsProcessItem.Add(this.cmbInceptOper.Name, pItem.Clone());

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

            this.cmbPackageOper.AddItems(alStaticEmployee);
            this.cmbInceptOper.AddItems(alStaticEmployee);
        }

        #endregion

        /// <summary>
        /// �Ƽ����װ��Ϣ����
        /// </summary>
        /// <param name="preparation"></param>
        /// <returns></returns>
        public new int SetPreparation(FS.HISFC.Models.Preparation.Preparation preparation)
        {
            this.lbPreparationInfo.Text = string.Format(this.strPreparation, preparation.Drug.Name, preparation.Drug.Specs, preparation.BatchNO, preparation.PlanQty, preparation.Unit);

            return base.SetPreparation(preparation);
        }

        private void txtPackageNum_Leave(object sender, EventArgs e)
        {
            decimal packQty = FS.FrameWork.Function.NConvert.ToDecimal(this.txtPackageNum.Text);
            decimal wastQty = FS.FrameWork.Function.NConvert.ToDecimal(this.txtWasteNum.Text);

            decimal finalParam = System.Math.Round(packQty * this.preparation.Drug.PackQty / this.preparation.PlanQty * 100, 2);

            this.txtFinParam.Text = string.Format("{0}", finalParam.ToString());
        }
    }
}
