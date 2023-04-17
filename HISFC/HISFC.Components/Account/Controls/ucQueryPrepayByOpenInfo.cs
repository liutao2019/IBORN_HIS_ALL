using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Account.Controls
{
    /// <summary>
    /// ���ݿ�����Ϣ��ѯ����Ԥ����Ϣ
    /// </summary>
    public partial class ucQueryPrepayByOpenInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucQueryPrepayByOpenInfo()
        {
            InitializeComponent(); 
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

        /// <summary>
        /// ����ҵ�������
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant conManager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// ѡ����ʾ����
        /// </summary>
        [Description("ѡ����ʾ����"),System.ComponentModel.Category("����"),Browsable(true),DefaultValue(true)]
        public bool[] ShowColumns
        {
            get
            {
                bool[] dic = new bool[this.neuSpread1_Sheet1.ColumnCount];
                for (int i = 0; i < this.neuSpread1_Sheet1.ColumnCount; i++)
                {
                    dic[i]=this.neuSpread1_Sheet1.Columns[i].Visible;
                }
                return dic;
            }
            set
            {
                bool[] dic = value;

                for (int i=0;i<dic.Length;i++)
                {
                    this.neuSpread1_Sheet1.Columns[i].Visible = dic[i];
                }
            }
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Init()
        {
            System.Collections.ArrayList al = this.conManager.GetList(FS.HISFC.Models.Base.EnumConstant.BANK);
            if (al == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ�����г���"));
                return;
            }
            if (al.Count == 0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�޿�������Ϣ����ά����"));
                return;
            }
            this.cmbOpenBank.AddItems(al);
        }

        protected override int OnQuery(object sender, object neuObject)
        {

            if (string.IsNullOrEmpty(this.txtOpenAccount.Text) && string.IsNullOrEmpty(this.txtOpenCompany.Text) && string.IsNullOrEmpty(this.txtPostransNO.Text) && string.IsNullOrEmpty(this.cmbOpenBank.Text))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����뿪����Ϣ��һ��������Ϣ"));
                return -1;
            }
            string openAccount = this.txtOpenAccount.Text;
            if (string.IsNullOrEmpty(openAccount))
            {
                openAccount = "ALL";
            }
            string openCompany = this.txtOpenCompany.Text;
            if (string.IsNullOrEmpty(openCompany))
            {
                openCompany = "ALL";
            }
            string postransNO= this.txtPostransNO.Text;
            if (string.IsNullOrEmpty(postransNO))
            {
                postransNO = "ALL";
            }
            string openBank = this.cmbOpenBank.Text;
            if (string.IsNullOrEmpty(openBank))
            {
                openBank = "ALL";
            }


            string[] obj = new string[] { openCompany, openBank, openAccount, postransNO };

            DataSet ds=new DataSet();

            if (this.conManager.ExecQuery("Fee.Account.QueryByOpenInfo", ref ds, obj) == -1)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("ִ��Fee.Account.QueryByOpenInfo������"));
                return -1;
            }

            this.neuSpread1_Sheet1.DataSource = ds;

            return base.OnQuery(sender, neuObject);
        }
    }
}
