using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.WinForms.Report.FinSim
{
    public partial class ucFinSimIpbDeptIn_1 : Common.ucQueryBaseForDataWindow
    {
        public ucFinSimIpbDeptIn_1()
        {
            InitializeComponent();
        }
        private string metCode = string.Empty;
        private string metName = string.Empty;
        protected override void OnLoad()
        {
            this.Init();

            FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            System.Collections.ArrayList consList = manager.GetConstantList("PACTUNIT");
            foreach (FS.HISFC.Models.Base.Const con in consList)
            {
                metComboBox1.Items.Add(con);
            }
            if (metComboBox1.Items.Count >= 0)
            {

                metComboBox1.SelectedIndex = 0;
                metCode = ((FS.HISFC.Models.Base.Const)metComboBox1.Items[0]).ID;
                metName = ((FS.HISFC.Models.Base.Const)metComboBox1.Items[0]).Name;

            }
            
            this.InitCheckCmb();
            this.InitCombox();

            base.OnLoad();
        }
        /// <summary>
        /// �����жϱ������Ƿ����������;�������
        /// </summary>
        private void InitCheckCmb()
        {
            ArrayList al =new ArrayList();

            ///�ӹ����Ƿ������;�������ݵ��ж�
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "����";
            obj.Name = "������;����";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "��Ϊ��Ժ���㻼�ߵ�";
            obj.Name = "��Ϊ��Ժ���㻼�ߵ���;����";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "����";
            obj.Name = "������;����";
            al.Add(obj);

            this.checkcomboBox1.AddItems(al);

        }

         /// <summary>
         /// �����жϱ����е��б����ͣ�����ְ���ͳ������
         /// </summary>
        private void InitCombox()
        {
            ArrayList al = new ArrayList();

            ///�ӹ����Ƿ������;�������ݵ��ж�
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "����ְ��";
            obj.Name = "����ְ��";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "�������";
            obj.Name = "�������";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "ũ����Ա";
            obj.Name = "ũ����Ա";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "���л���";
            obj.Name = "���л���";
            al.Add(obj);

            this.neuComboBox1.AddItems(al);

        }


        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            //ȡ�������б��ֵ
            string id = this.checkcomboBox1.Tag.ToString();
            string idsim = this.neuComboBox1.Tag.ToString();

            dwMain.Retrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, metCode,id,idsim);
            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, metCode,id,idsim);
        }
        private void metComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metComboBox1.SelectedIndex >= 0)
            {
                metCode = ((FS.HISFC.Models.Base.Const)metComboBox1.Items[this.metComboBox1.SelectedIndex]).ID;
                metName = ((FS.HISFC.Models.Base.Const)metComboBox1.Items[this.metComboBox1.SelectedIndex]).Name;
            }
        }

        //����������б�ѡ��ı��¼�(�Ƿ����������;�������)
        private void checkcomboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.OnRetrieve();
        }

        //����������б�ѡ��ı��¼�(�жϱ����е��б�����(����ְ���ͳ������))
        private void neuComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.OnRetrieve();
        }
    }
}
