using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FS.HISFC.Components.Manager.Controls
{
    public partial class ucPactUnitMaintenance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPactUnitMaintenance()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ��ͬ��λ������Ϣ
        /// </summary>
        DataTable dtMain = new DataTable();

        /// <summary>
        /// ��ͬ��λ������Ϣ��ͼ
        /// </summary>
        DataView dvMain = new DataView();

        /// <summary>
        /// ��ͬ��λ������Ϣ����
        /// </summary>
        private string mainSettingFilePath = Application.StartupPath + @".\Setting\profiles\mainSettingFilePath.xml";

        #endregion

        #region ˽�з���

        public int Init() 
        {
            //��ʼ����ͬ��λ��Ҫ��Ϣ
            if (InitDataTableMain() == -1) 
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ��ʼ����ͬ��λ��Ҫ��Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int InitDataTableMain()
        {
            if (File.Exists(this.mainSettingFilePath))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(this.mainSettingFilePath, this.dtMain, ref this.dvMain, this.fpMain_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpMain_Sheet1, this.mainSettingFilePath);
            }
            else
            {
                this.dtMain.Columns.AddRange(new DataColumn[] 
                {
                    new DataColumn("��λ����", typeof(string)),
                    new DataColumn("��λ����", typeof(string)),
                    new DataColumn("�������", typeof(string)),
                    new DataColumn("�۸���ʽ", typeof(string)),
                    new DataColumn("���ѱ���", typeof(decimal)),
                    new DataColumn("�Ը�����", typeof(decimal)),
                    new DataColumn("�Էѱ���", typeof(decimal)),
                    new DataColumn("�Żݱ���", typeof(decimal)),
                    new DataColumn("Ƿ�ѱ���", typeof(decimal)),
                    new DataColumn("Ӥ����־", typeof(bool)),
                    new DataColumn("�Ƿ���", typeof(bool)),
                    new DataColumn("��־", typeof(bool)),
                    new DataColumn("��ҽ��֤", typeof(bool)),
                    new DataColumn("��ҽ��֤", typeof(bool)),
                    new DataColumn("���޶�", typeof(decimal)),
                    new DataColumn("���޶�", typeof(decimal)),
                    new DataColumn("���޶�", typeof(decimal)),
                    new DataColumn("һ���޶�", typeof(decimal)),
                    new DataColumn("��λ����", typeof(decimal)),
                    new DataColumn("�յ�����", typeof(decimal)),
                    new DataColumn("���", typeof(string)),
                    new DataColumn("���", typeof(int))
                });

                this.dvMain = new DataView(this.dtMain);

                this.fpMain_Sheet1.DataSource = this.dvMain;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpMain_Sheet1, this.mainSettingFilePath);
            }

            return 1;
        }

        #endregion

        #region ���з���

        #endregion
    }
}
