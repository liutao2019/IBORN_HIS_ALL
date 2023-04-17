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
    /// [��������: ��װ��������¼��]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2007-12]<br></br>
    /// <˵��>
    /// </˵��>
    /// </summary>
    public partial class ucDivisionProcess : ucProcessBase
    {
        public ucDivisionProcess()
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

            this.cmbWhole.Tag = Function.NoneData;
            this.hsProcessControl.Add("EquipmentGood", this.cmbWhole);
            this.cmbClean.Tag = Function.NoneData;
            this.hsProcessControl.Add("EquipmentClean", this.cmbClean);

            this.hsProcessControl.Add("Regulation", this.txtRegulation);
            this.hsProcessControl.Add("Exucte", this.txtExucte);
            this.hsProcessControl.Add("Quantity", this.txtQuantity);
            this.hsProcessControl.Add("DivisionOper", this.cmbDivOper);
            this.hsProcessControl.Add("DivisionDate", this.dtpDivDate);
            this.hsProcessControl.Add("InceptOper", this.cmbInceptOper);
            this.hsProcessControl.Add("DivisionQty", this.txtDivNum);
            this.hsProcessControl.Add("WasteQty", this.txtWasteNum);
            this.hsProcessControl.Add("AssayQty", this.txtAssayNum);
            this.hsProcessControl.Add("DivParam", this.txtParam);

            FS.HISFC.Models.Preparation.Process pItem = new FS.HISFC.Models.Preparation.Process();

            pItem.ProcessItem.ID = "DivisionQty";
            pItem.ProcessItem.Name = "���Ʒ��װ��";
            this.hsProcessItem.Add(this.txtDivNum.Name, pItem.Clone());

            pItem.ProcessItem.ID = "WasteQty";
            pItem.ProcessItem.Name = "��Ʒ��";
            this.hsProcessItem.Add(this.txtWasteNum.Name, pItem.Clone());

            pItem.ProcessItem.ID = "AssayQty";
            pItem.ProcessItem.Name = "�ͼ���";
            this.hsProcessItem.Add(this.txtAssayNum.Name, pItem.Clone());

            pItem.ProcessItem.ID = "DivParam";
            pItem.ProcessItem.Name = "����ƽ��";
            this.hsProcessItem.Add(this.txtParam.Name, pItem.Clone());

            pItem.ProcessItem.ID = "EquipmentGood";
            pItem.ProcessItem.Name = "�豸�Ƿ����";
            this.hsProcessItem.Add(this.cmbWhole.Name, pItem.Clone());

            pItem.ProcessItem.ID = "EquipmentClean";
            pItem.ProcessItem.Name = "�豸�Ƿ����";
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

            pItem.ProcessItem.ID = "DivisionOper";
            pItem.ProcessItem.Name = "��װԱ";
            this.hsProcessItem.Add(this.cmbDivOper.Name, pItem.Clone());

            pItem.ProcessItem.ID = "DivisionDate";
            pItem.ProcessItem.Name = "��װ����";
            this.hsProcessItem.Add(this.dtpDivDate.Name, pItem.Clone());

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

            this.cmbDivOper.AddItems(alStaticEmployee);
            this.cmbInceptOper.AddItems(alStaticEmployee);
        }

        #endregion

        /// <summary>
        /// �Ƽ�������Ϣ����
        /// </summary>
        /// <param name="preparation"></param>
        /// <returns></returns>
        public new int SetPreparation(FS.HISFC.Models.Preparation.Preparation preparation)
        {
            this.lbPreparationInfo.Text = string.Format(this.strPreparation, preparation.Drug.Name, preparation.Drug.Specs, preparation.BatchNO, preparation.PlanQty, preparation.Unit);

            this.txtAssayNum.Text = preparation.AssayQty.ToString();
            this.txtDivNum.Text = preparation.ConfectQty.ToString();

            return base.SetPreparation(preparation);
        }

        private void txtAssayNum_Leave(object sender, EventArgs e)
        {
            decimal deivQty = FS.FrameWork.Function.NConvert.ToDecimal(this.txtDivNum.Text);
            decimal wastQty = FS.FrameWork.Function.NConvert.ToDecimal(this.txtWasteNum.Text);
            decimal assayQty = FS.FrameWork.Function.NConvert.ToDecimal(this.txtAssayNum.Text);
            
            decimal param = (decimal)System.Math.Round((double)((deivQty + wastQty + assayQty) / this.preparation.PlanQty * 100), 2);

            this.txtParam.Text = string.Format("{0}", param.ToString());
        }
    }
}
