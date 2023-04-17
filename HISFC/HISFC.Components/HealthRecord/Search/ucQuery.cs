using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.HealthRecord.Search
{
    public partial class ucQuery : Common.Controls.ucQuery
    {
        public ucQuery(string type)
        {
            InitializeComponent();
            ConTpye = type;
        }

        private FS.HISFC.BizLogic.Manager.Constant con = new FS.HISFC.BizLogic.Manager.Constant();
        /// <summary>
        /// ��������
        /// </summary>
        private string ConTpye = string.Empty;
        private void ucQuery_Load(object sender, System.EventArgs e)
        {
            //�趨��ť���ɼ�
            this.ButtonOK.Visible = false;
            this.ButtonReset.Visible = false;
            this.ButtonExit.Visible = false;
            this.btnDefault.Visible = false;
            //��������
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                InitDragList(ConTpye);
            }
        }
        /// <summary>
        /// ��������˵� 
        /// </summary>
        private void InitDragList()
        {
            ArrayList al = new ArrayList();
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "PATIENT_NO";
            obj.Name = "סԺ��";
            obj.Memo = "";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "CARD_NO";
            obj.Name = "���￨��";
            obj.Memo = "";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "MCARD_NO";
            obj.Name = "ҽ��֤��";
            obj.Memo = "";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "NAME";
            obj.Name = "����";
            obj.Memo = "";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "SEX_CODE";
            obj.Name = "�Ա�";
            obj.Memo = "SEX";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "IDENNO";
            obj.Name = "���֤";
            obj.Memo = "";
            al.Add(obj);
            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "DEPT_NAME";
            obj.Name = "����";
            obj.Memo = "DEPARTMENT";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "BIRTHDAY";
            obj.Name = "����";
            obj.Memo = "DATETIME";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "WORK_NAME";
            obj.Name = "������λ";
            obj.Memo = "";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "WORK_TEL";
            obj.Name = "������λ�绰";
            obj.Memo = "";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "WORK_ZIP";
            obj.Name = "��λ�ʱ�";
            obj.Memo = "";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "HOME";
            obj.Name = "���ڻ��ͥ��ַ";
            obj.Memo = "";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "HOME_TEL";
            obj.Name = "��ͥ�绰";
            obj.Memo = "";

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "HOME_ZIP";
            obj.Name = "���ڻ��ͥ�ʱ�";
            obj.Memo = "";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "DIST";
            obj.Name = "����";
            obj.Memo = "";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "NATION_CODE";
            obj.Name = "����";
            obj.Memo = "";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "LINKMAN_NAME";
            obj.Name = "��ϵ������";
            obj.Memo = "";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "LINKMAN_TEL";
            obj.Name = "��ϵ�˵绰";
            obj.Memo = "";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "LINKMAN_ADD";
            obj.Name = "��ϵ�˵�ַ";
            obj.Memo = "";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "MARI";
            obj.Name = "����״��";
            obj.Memo = "MARI";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "BLOOD_CODE";
            obj.Name = "Ѫ�ͱ���";
            obj.Memo = "";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "PACT_NAME";
            obj.Name = "��ͬ��λ";
            obj.Memo = "";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "IN_CIRCS";
            obj.Name = "��Ժ���";
            obj.Memo = "";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "IN_AVENUE";
            obj.Name = "��Ժ;��";
            obj.Memo = "";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "OUT_DATE";
            obj.Name = "��Ժ����";
            obj.Memo = "DATETIME";
            al.Add(obj);

            obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "BED_NO";
            obj.Name = "����";
            obj.Memo = "";
            al.Add(obj);

            al.Add(obj);
            //������
            this.InitCondition(al);
        }

        /// <summary>
        /// ͨ���������ͻ�ȡ
        /// </summary>
        /// <param name="conType"></param>
        private void InitDragList(string conType)
        {
            ArrayList al = con.GetList(conType);
            this.InitCondition(al);
        }
    }
}
