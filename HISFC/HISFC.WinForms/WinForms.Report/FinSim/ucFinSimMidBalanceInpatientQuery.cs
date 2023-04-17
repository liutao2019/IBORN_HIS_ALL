using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.WinForms.Report.FinSim
{
    /// <summary>
    /// ҽ��סԺ��;���㻼�߲�ѯ
    /// 07-12-28 xuc
    /// </summary>
    public partial class ucFinSimMidBalanceInpatientQuery : Common.ucQueryBaseForDataWindow
    {
        public ucFinSimMidBalanceInpatientQuery()
        {
            InitializeComponent();
        }

        #region ˽�г�Ա
        /// <summary>
        /// �ۺ�ҵ�����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ��ͬ��λ
        /// </summary>
        private string pactUnit;

        /// <summary>
        /// �����ַ���
        /// </summary>
        private string filterStr = "(fin_ipr_siinmaininfo_���� like '{0}%') or (name_spell like '{0}%')";
        #endregion

        #region ��ʼ��
        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected override void OnLoad()
        {
            #region ���غ�ͬ��λ�б�
            //��ͬ��λ�б�
            ArrayList list = new ArrayList();
            //ȫ��
            FS.HISFC.Models.Base.Const all = new FS.HISFC.Models.Base.Const();
            all.ID = "ALL";
            all.Name = "ȫ��";
            all.SpellCode = "QB";
            all.WBCode = "WU";
            //�����ͬ��λ�б�
            list.Add(all);
            //list.AddRange(manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PACTUNIT));
            //{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
            list.AddRange(manager.QueryPactUnitAll());
            //���������б�
            this.neuComboBox1.alItems.AddRange(list);
            this.neuComboBox1.Items.AddRange(list.ToArray());
            //Ĭ�Ϻ�ͬ��λ
            if (this.neuComboBox1.Items.Count > 0)
            {
                this.neuComboBox1.SelectedIndex = 0;
                this.pactUnit = this.neuComboBox1.SelectedItem.ID;
            }
            #endregion
            base.OnLoad();
        }
        #endregion

        #region �¼�
        /// <summary>
        /// סԺ�Ų�ѯ�ؼ��¼�
        /// </summary>
        private void ucQueryInpatientNo1_myEvent()
        {
            if (this.ucQueryInpatientNo1.InpatientNo.Equals(""))
            {
                MessageBox.Show("�û��߲����ڣ�");
            }
            else
            {
                if (this.GetQueryTime() == -1)
                {
                    return;
                }
                //סԺ�Ų�ѯ�ؼ����س���ֱ�Ӽ�����Ϣ,���Ժ�ͬ��λ
                this.dwMain.Retrieve(this.beginTime, this.endTime, this.ucQueryInpatientNo1.InpatientNo, this.pactUnit);
            }

        }

        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        protected override int OnRetrieve(params object[] objects)
        {
            if (this.GetQueryTime() == -1)
            {
                return -1;
            }
            //�����ѯ��ťʱֱ�Ӱ���ͬ��λ������Ϣ
            return base.OnRetrieve(this.beginTime, this.endTime, "ALL", this.pactUnit);
        }

        /// <summary>
        /// ���˿��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTextBox1_TextChanged(object sender, EventArgs e)
        {
            string str = this.neuTextBox1.Text.Trim().Replace(@"\", "").ToUpper();
            if (str.Equals(""))
            {
                this.dwMain.SetFilter("");
                this.dwMain.Filter();
            }
            else
            {
                str = string.Format(this.filterStr, str);
                this.dwMain.SetFilter(str);
                this.dwMain.Filter();
            }
        }

        /// <summary>
        /// ��ͬ��λѡ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.pactUnit = this.neuComboBox1.SelectedItem.ID;

        }

        #endregion
    }
}

