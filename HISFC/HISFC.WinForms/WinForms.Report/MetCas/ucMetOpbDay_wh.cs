using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Neusoft.Report.MetCas
{
    public partial class ucMetOpbDay_wh : Neusoft.Report.Common.ucQueryBaseForDataWindow
    {
        public ucMetOpbDay_wh()
        {
            InitializeComponent();
        }
        protected override int OnRetrieve(params object[] objects)
        {           
            return base.OnRetrieve(dtpBeginTime.Value.Date);
        }
        public void update()
        {
            Sybase.DataWindow.Transaction trans = new Sybase.DataWindow.Transaction();
            System.Data.OracleClient.OracleConnectionStringBuilder ocs =
            new System.Data.OracleClient.OracleConnectionStringBuilder(Neusoft.NFC.Management.Connection.Instance.ConnectionString);
            trans.Password = ocs.Password;
            trans.ServerName = ocs.DataSource;
            trans.UserId = ocs.UserID;

            trans.Dbms = Sybase.DataWindow.DbmsType.Oracle8i;


            trans.AutoCommit = false;
            trans.DbParameter = "PBCatalogOwner='lchis'";

            trans.Connect();
            this.dwMain.SetTransaction(trans);
            try
            {
                this.dwMain.UpdateData(true);
            }
            catch (Exception e)
            {
                trans.Rollback();
                MessageBox.Show("����ʧ�ܣ�" + e.Message);
                return;
            }
            trans.Commit();
            MessageBox.Show("����ɹ���");
        }
        protected override int OnSave(object sender, object neuObject)
        {
            this.update();

            return base.OnSave(sender, neuObject);
        }

        private void dwMain_EditChanged(object sender, Sybase.DataWindow.EditChangedEventArgs e)
        {
            this.dwMain.AcceptText();
        }
        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            try
            {

                this.dwMain.Print();
                return 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("��ӡ����", "��ʾ");
                return -1;
            }

        }
    }
}
