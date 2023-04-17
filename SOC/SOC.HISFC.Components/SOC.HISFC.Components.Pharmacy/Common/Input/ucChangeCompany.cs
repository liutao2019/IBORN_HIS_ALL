using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.SOC.HISFC.Components.Pharmacy.Common.Input
{
    public partial class ucChangeCompany : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucChangeCompany()
        {
            InitializeComponent();
            this.lnbNOType.LinkClicked += new LinkLabelLinkClickedEventHandler(lnbNOType_LinkClicked);
            this.lnbCompany.LinkClicked += new LinkLabelLinkClickedEventHandler(lnbCompany_LinkClicked);
            this.txtNO.KeyPress += new KeyPressEventHandler(txtNO_KeyPress);
            this.nlbInfo.DoubleClick += new EventHandler(nlbInfo_DoubleClick);
        }

      

        /// <summary>
        /// Ȩ�ޱ���
        /// </summary>
        private string privePowerString = "0310";

        /// <summary>
        /// Ȩ�ޱ���
        /// </summary>
        [Description("Ȩ�ޱ���"), Category("����"), Browsable(true)]
        public string PrivePowerString
        {
            get { return privePowerString; }
            set { privePowerString = value; }
        }


        /// <summary>
        /// ������˾��Ϣ
        /// </summary>
        private ArrayList alCompany = new ArrayList();

        /// <summary>
        /// ������˾������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper companyHerlper = null;

        /// <summary>
        /// ��ѯ������� 0 ��Ʊ�� 1 ��ⵥ�ݺ�
        /// </summary>
        private string noType = "0";

        /// <summary>
        /// �¹�����˾
        /// </summary>
        private FS.FrameWork.Models.NeuObject newCompany = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��ǰ��½������Ϣ
        /// </summary>
        private FS.FrameWork.Models.NeuObject deptInfo = new FS.FrameWork.Models.NeuObject();

        #region ������

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();

            return 1;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();

            return base.OnQuery(sender, neuObject);
        }
        #endregion

        private void Init()
        {
            FS.SOC.HISFC.BizLogic.Pharmacy.Constant phaConsManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Constant();
            this.alCompany = phaConsManager.QueryCompany("1");
            if (this.alCompany == null)
            {
                MessageBox.Show("��ȡ������λ�б�������");
                return;
            }

            this.companyHerlper = new FS.FrameWork.Public.ObjectHelper(this.alCompany);

        }

        /// <summary>
        /// ����Ȩ�޿���
        /// </summary>
        /// <returns></returns>
        private int SetPriveDept()
        {
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager userPowerDetailManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            if (string.IsNullOrEmpty(PrivePowerString))
            {
                PrivePowerString = "0310";
            }
            int param = Function.ChoosePriveDept(PrivePowerString, ref this.deptInfo);
            if (param == 0 || param == -1)
            {
                return -1;
            }


            this.nlbInfo.Text = "��ѡ��Ŀ����ǡ�" + this.deptInfo.Name + "��";

            return 1;

        }

        private void Query()
        {
            ArrayList al = new ArrayList();


            FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemManager = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();
            if (this.noType == "0")
            {
                al = itemManager.QueryInputInfoByInvoice(this.deptInfo.ID, this.txtNO.Text, "0");
                if (al == null)
                {
                    Function.ShowMessage("���ݷ�Ʊ�Ż�ȡ�����Ϣ��������", MessageBoxIcon.Error);
                    return;
                }
                if (al.Count == 0)
                {
                    al = itemManager.QueryInputInfoByInvoice(this.deptInfo.ID, this.txtNO.Text, "1");
                    if (al == null)
                    {
                        Function.ShowMessage("���ݷ�Ʊ�Ż�ȡ�����Ϣ��������", MessageBoxIcon.Error);
                        return;
                    }
                }
                if (al.Count == 0)
                {
                    al = itemManager.QueryInputInfoByInvoice(this.deptInfo.ID, this.txtNO.Text, "2");
                    if (al == null)
                    {
                        Function.ShowMessage("���ݷ�Ʊ�Ż�ȡ�����Ϣ��������", MessageBoxIcon.Error);
                        return;
                    }
                    if (al.Count > 0)
                    {
                        Function.ShowMessage("�����Ѿ���׼�������Ը���", MessageBoxIcon.Information);
                        return;
                    }
                }
                if (al.Count == 0)
                {
                    Function.ShowMessage("��Ʊ�Ų���ȷ����ȷ��¼����ȷ", MessageBoxIcon.Information);
                    return;
                }
            }
            else
            {
                al = itemManager.QueryInputInfoByListID(this.deptInfo.ID, this.txtNO.Text, "AAAA", "0");
                if (al == null)
                {
                    Function.ShowMessage("������ⵥ�Ż�ȡ�����Ϣ��������", MessageBoxIcon.Error);
                    return;
                }
                if (al.Count == 0)
                {
                    al = itemManager.QueryInputInfoByListID(this.deptInfo.ID, this.txtNO.Text, "AAAA", "1");
                    if (al == null)
                    {
                        Function.ShowMessage("������ⵥ�Ż�ȡ�����Ϣ��������", MessageBoxIcon.Error);
                        return;
                    }
                }
                if (al.Count == 0)
                {
                    al = itemManager.QueryInputInfoByListID(this.deptInfo.ID, this.txtNO.Text, "AAAA", "2");
                    if (al == null)
                    {
                        Function.ShowMessage("������ⵥ�Ż�ȡ�����Ϣ��������", MessageBoxIcon.Error);
                        return;
                    }
                    if (al.Count > 0)
                    {
                        Function.ShowMessage("�����Ѿ���׼�������Ը���", MessageBoxIcon.Information);
                        return;
                    }
                }
                if (al.Count == 0)
                {
                    Function.ShowMessage("��ⵥ�Ų���ȷ����ȷ��¼����ȷ", MessageBoxIcon.Information);
                    return;
                }
            }

            this.neuSpread1_Sheet1.Rows.Count = 0;

            foreach (FS.HISFC.Models.Pharmacy.Input info in al)
            {
                this.neuSpread1_Sheet1.Rows.Add(0, 1);
                this.neuSpread1_Sheet1.Cells[0, 0].Value = true;
                this.neuSpread1_Sheet1.Cells[0, 1].Text = info.InListNO;
                this.neuSpread1_Sheet1.Cells[0, 2].Text = info.InvoiceNO;
                this.neuSpread1_Sheet1.Cells[0, 3].Text = this.companyHerlper.GetName(info.Company.ID);
                this.neuSpread1_Sheet1.Cells[0, 4].Text = info.Item.Name + "��" + info.Item.Specs + "��";
                this.neuSpread1_Sheet1.Cells[0, 5].Text = (info.Quantity / info.Item.PackQty).ToString("N");
                this.neuSpread1_Sheet1.Cells[0, 6].Text = info.Item.PackUnit;
                this.neuSpread1_Sheet1.Rows[0].Tag = info;
            }
        }

        private void Save()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
                return;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.SOC.HISFC.BizLogic.Pharmacy.InOut itemManager = new FS.SOC.HISFC.BizLogic.Pharmacy.InOut();

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {

                //if (FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, 0].Value))
                {
                    FS.HISFC.Models.Pharmacy.Input input = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.Input;

                    if (itemManager.UpdateInputCompany(input.ID,input.SerialNO,this.newCompany.ID) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("���¹�����˾��Ϣʧ�ܣ����ݿ����Ѿ���׼",MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("����ɹ�");
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.Init();
            }
            catch
            { }

            base.OnLoad(e);
        }

        private void lnbCompany_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            //����Ա�Դ���ѡ�񷵻ص���Ϣ
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alCompany, ref this.newCompany) == 0)
            {
                return;
            }
            else
            {
                this.lbCompany.Text = this.newCompany.Name;
            }
        }

        private void lnbNOType_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            if (this.noType == "0")
            {
                this.noType = "1";
                this.lnbNOType.Text = "���ݺ�";
            }
            else
            {
                this.noType = "0";
                this.lnbNOType.Text = "��Ʊ��";
            }
            this.txtNO.Text = "";
        }

        void nlbInfo_DoubleClick(object sender, EventArgs e)
        {
            this.SetPriveDept();
        }

        void txtNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.Query();
            }
        }

        #region IPreArrange ��Ա

        public int PreArrange()
        {
            if (this.DesignMode)
            {
                return 0;
            }

            return this.SetPriveDept();
        }

        #endregion

    }
}
