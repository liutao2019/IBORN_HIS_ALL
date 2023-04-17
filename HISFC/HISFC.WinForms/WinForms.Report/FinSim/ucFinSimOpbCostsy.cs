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
    public partial class ucFinSimOpbCostsy : Common.ucQueryBaseForDataWindow
    {
        public ucFinSimOpbCostsy()
        {
            InitializeComponent();
        }
       protected override void  OnLoad()
        {
                    this.Init();

                    this.InitCombox();

 	                base.OnLoad();
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


            string idsim = this.neuComboBox1.Tag.ToString();

            dwMain.Retrieve(this.dtpBeginTime.Value,this.dtpEndTime.Value,idsim);
            return base.OnRetrieve(this.dtpBeginTime.Value,this.dtpEndTime.Value,idsim);
        }

        //����������б�ѡ��ı��¼�(�жϱ����е��б�����(����ְ���ͳ������))
        private void neuComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.OnRetrieve();
        }
    }
}
