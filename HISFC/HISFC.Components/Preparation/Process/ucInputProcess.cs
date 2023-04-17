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
    /// [��������: ��⹤������¼��]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2008-02]<br></br>
    /// <˵��>
    /// </˵��>
    /// </summary>
    public partial class ucInputProcess : ucProcessBase
    {
        public ucInputProcess()
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

            this.cmbMaterial.Tag = Function.NoneData;
            this.hsProcessControl.Add("MaterialParam", this.cmbMaterial);
            this.cmbExceute.Tag = Function.NoneData;
            this.hsProcessControl.Add("ExceuteParam", this.cmbExceute);

            this.hsProcessControl.Add("CheckResult", this.txtCheckResult);
            this.hsProcessControl.Add("InputOper", this.cmbInputOper);
            this.hsProcessControl.Add("InputDate", this.dtpInputDate);
            this.hsProcessControl.Add("InceptOper", this.cmbInceptOper);
            this.hsProcessControl.Add("InputQty", this.txtInputNum);

            FS.HISFC.Models.Preparation.Process pItem = new FS.HISFC.Models.Preparation.Process();

            pItem.ProcessItem.ID = "InputQty";
            pItem.ProcessItem.Name = "�����";
            this.hsProcessItem.Add(this.txtInputNum.Name, pItem.Clone());

            pItem.ProcessItem.ID = "MaterialParam";
            pItem.ProcessItem.Name = "����ƽ��";
            this.hsProcessItem.Add(this.cmbMaterial.Name, pItem.Clone());

            pItem.ProcessItem.ID = "ExceuteParam";
            pItem.ProcessItem.Name = "�����ʿ�";
            this.hsProcessItem.Add(this.cmbExceute.Name, pItem.Clone());

            pItem.ProcessItem.ID = "CheckResult";
            pItem.ProcessItem.Name = "������";
            this.hsProcessItem.Add(this.txtCheckResult.Name, pItem.Clone());

            pItem.ProcessItem.ID = "InputOper";
            pItem.ProcessItem.Name = "���Ա";
            this.hsProcessItem.Add(this.cmbInputOper.Name, pItem.Clone());

            pItem.ProcessItem.ID = "InputDate";
            pItem.ProcessItem.Name = "�������";
            this.hsProcessItem.Add(this.dtpInputDate.Name, pItem.Clone());

            pItem.ProcessItem.ID = "InceptOper";
            pItem.ProcessItem.Name = "���Ա";
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

            this.cmbInputOper.AddItems(alStaticEmployee);
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
            this.lbUnit.Text = preparation.Drug.PackUnit;            

            base.SetPreparation(preparation);

            this.txtInputNum.Text = (preparation.InputQty / preparation.Drug.PackQty).ToString();

            return 1;
        }

        protected override void btnOK_Click(object sender, EventArgs e)
        {
            this.preparation.InputQty = FS.FrameWork.Function.NConvert.ToDecimal(this.txtInputNum.Text) * preparation.Drug.PackQty;

            base.btnOK_Click(sender, e);
        }
    }
}
