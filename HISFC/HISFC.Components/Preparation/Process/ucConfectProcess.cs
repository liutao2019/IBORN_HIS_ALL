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
    /// [��������: ���ù�������¼��]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2007-11]<br></br>
    /// <˵��>
    /// </˵��>
    /// </summary>
    public partial class ucConfectProcess : FS.HISFC.Components.Preparation.Process.ucProcessBase
    {
        public ucConfectProcess()
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
            this.cmbScale.Tag = Function.NoneData;
            this.hsProcessControl.Add("Scale", this.cmbScale);
            this.cmbStetlyard.Tag = Function.NoneData;
            this.hsProcessControl.Add("Stetlyard", this.cmbStetlyard);

            this.hsProcessControl.Add("Regulation", this.txtRegulation);
            this.hsProcessControl.Add("Exucte", this.txtExucte);
            this.hsProcessControl.Add("Quantity", this.txtQuantity);
            this.hsProcessControl.Add("ConfectOper", this.cmbConfectOper);
            this.hsProcessControl.Add("ConfectDate", this.dtpConfectDate);
            this.hsProcessControl.Add("CheckOper", this.cmbCheckOper);

            FS.HISFC.Models.Preparation.Process pItem = new FS.HISFC.Models.Preparation.Process();
            pItem.ProcessItem.ID = "EquipmentGood";
            pItem.ProcessItem.Name = "�豸�Ƿ����";
            this.hsProcessItem.Add(this.cmbWhole.Name, pItem.Clone());

            pItem.ProcessItem.ID = "EquipmentClean";
            pItem.ProcessItem.Name = "�豸�Ƿ����";
            this.hsProcessItem.Add(this.cmbClean.Name, pItem.Clone());

            pItem.ProcessItem.ID = "Scale";
            pItem.ProcessItem.Name = "ҩ����ƽ";
            this.hsProcessItem.Add(this.cmbScale.Name, pItem.Clone());

            pItem.ProcessItem.ID = "Stetlyard";
            pItem.ProcessItem.Name = "����";
            this.hsProcessItem.Add(this.cmbStetlyard.Name, pItem.Clone());

            pItem.ProcessItem.ID = "Regulation";
            pItem.ProcessItem.Name = "���ִ��";
            this.hsProcessItem.Add(this.txtRegulation.Name, pItem.Clone());

            pItem.ProcessItem.ID = "Quantity";
            pItem.ProcessItem.Name = "�������";
            this.hsProcessItem.Add(this.txtQuantity.Name, pItem.Clone());

            pItem.ProcessItem.ID = "Exucte";
            pItem.ProcessItem.Name = "����ִ��";
            this.hsProcessItem.Add(this.txtExucte.Name, pItem.Clone());

            pItem.ProcessItem.ID = "ConfectOper";
            pItem.ProcessItem.Name = "����Ա";
            this.hsProcessItem.Add(this.cmbConfectOper.Name, pItem.Clone());

            pItem.ProcessItem.ID = "ConfectDate";
            pItem.ProcessItem.Name = "��������";
            this.hsProcessItem.Add(this.dtpConfectDate.Name, pItem.Clone());

            pItem.ProcessItem.ID = "CheckOper";
            pItem.ProcessItem.Name = "����Ա";
            this.hsProcessItem.Add(this.cmbCheckOper.Name, pItem.Clone());

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

            this.cmbConfectOper.AddItems(alStaticEmployee);
            this.cmbCheckOper.AddItems(alStaticEmployee);
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

            return base.SetPreparation(preparation);
        }

        public override int PrintProcess()
        {
           // MessageBox.Show("Confect");

            return base.PrintProcess();
        }
    }    
}
