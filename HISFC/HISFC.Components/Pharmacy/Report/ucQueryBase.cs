using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Pharmacy.Report
{
    /// <summary>
    /// [��������: ҩƷ������ѯ�ؼ�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-10]<br></br>
    /// </summary>
    public partial class ucQueryBase : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucQueryBase()
        {
            InitializeComponent();
        }

        #region ��������

        /// <summary>
        /// ͳ����ʼʱ��
        /// </summary>
        public DateTime BeginDate
        {
            get
            {
                return FS.FrameWork.Function.NConvert.ToDateTime(this.dtpBegin.Text);
            }
            set
            {
                this.dtpBegin.Value = value;
            }
        }

        /// <summary>
        /// ͳ�ƽ���ʱ��
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                return FS.FrameWork.Function.NConvert.ToDateTime(this.dtpEnd.Text);
            }
            set
            {
                this.dtpEnd.Value = value;
            }
        }

        /// <summary>
        /// ������Ŀ1
        /// </summary>
        public string FirstItemData
        {
            get
            {
                if (this.cmbItem1.Tag != null)
                {
                    return this.cmbItem1.Tag.ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.cmbItem1.Tag = value;
            }
        }

        /// <summary>
        /// ������Ŀ2
        /// </summary>
        public string SecondItemData
        {
            get
            {
                if (this.cmbItem2.Tag != null)
                {
                    return this.cmbItem2.Tag.ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.cmbItem2.Tag = value;
            }
        }

        /// <summary>
        /// ������Ŀ3
        /// </summary>
        public string ThirdItemData
        {
            get
            {
                if (this.cmbItem3.Tag != null)
                {
                    return this.cmbItem3.Tag.ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.cmbItem3.Tag = value;
            }
        }


        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {           
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();

            return base.OnQuery(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            return 1;
        }

        #endregion

        /// <summary>
        /// DataManagerҵ����
        /// </summary>
        protected FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();

        /// <summary>
        /// �������ݳ�ʼ��
        /// </summary>
        /// <param name="itemIndex">������Item���� ȡֵ��Χ 0 1 2</param>
        /// <param name="customItemType">��������</param>
        /// <param name="customTitle">Item����</param>
        /// <param name="alCustomData">Item����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        protected virtual int InitItemData(int itemIndex,CustomItemTypeEnum customItemType,string customTitle,ArrayList alCustomData)
        {
            ArrayList alData = new ArrayList();
            string itemTitle = customTitle;

            #region ���ػ�������

            if (customItemType == CustomItemTypeEnum.Drug)
            {
                FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
                List<FS.HISFC.Models.Pharmacy.Item> drugList = itemManager.QueryItemList(false);
                if (drugList == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ҩƷ��Ϣ��������" + itemManager.Err));
                    return -1;
                }
                alData = new ArrayList(drugList.ToArray());
                itemTitle = "��ѯҩƷ";
            }
            else if (customItemType == CustomItemTypeEnum.Dept)
            {
                FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
                alData = deptManager.GetDeptmentAll();
                if (alData == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("���ؿ�����Ϣ��������" + deptManager.Err));
                    return -1;
                }
                itemTitle = "��ѯ����";
            }
            else if (customItemType == CustomItemTypeEnum.Employee)
            {
                FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
                alData = personManager.GetEmployeeAll();
                if (alData == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("������Ա��Ϣ��������" + personManager.Err));
                    return -1;
                }
                itemTitle = "��ѯ��Ա";
            }
            else
            {
                alData = alCustomData;
            }

            #endregion

            switch (itemIndex)
            {
                case 0:
                    this.lbItem1.Text = itemTitle;
                    this.cmbItem1.AddItems(alData);

                    this.lbItem1.Visible = true;
                    this.cmbItem1.Visible = true;
                    break;
                case 1:
                    this.lbItem2.Text = itemTitle;
                    this.cmbItem2.AddItems(alData);

                    this.lbItem2.Visible = true;
                    this.cmbItem2.Visible = true;
                    break;
                case 2:
                    this.lbItem3.Text = itemTitle;
                    this.cmbItem3.AddItems(alData);

                    this.lbItem3.Visible = true;
                    this.cmbItem3.Visible = true;
                    break;
            }

            return 1;
        }

        /// <summary>
        /// ��ȡSql����
        /// </summary>
        /// <returns></returns>
        protected virtual string GetSqlIndex()
        {
            return null;
        }

        /// <summary>
        /// ��ȡ��ִ�е�Sql�������
        /// </summary>
        /// <param name="sqlIndex"></param>
        /// <returns></returns>
        protected virtual string GetExecSql(string sqlIndex)
        {
            string strSQL = "";
            if (this.dataManager.Sql.GetSql(sqlIndex, ref strSQL) == -1)
            {
                return null;
            }

            return strSQL;
        }

        /// <summary>
        /// sql����ʽ��
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected virtual string FormatExecSql(string sql)
        {
            return sql; 
        }

        /// <summary>
        /// ִ��Sql��� ��ȡDataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected virtual DataSet QueryDataSet(string sql)
        {
            DataSet ds = new DataSet();

            if (this.dataManager.ExecQuery(sql, ref ds) == -1)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("ִ��Sql��ȡ��ѯ���ݷ�������"));
                return null;
            }
            
            return ds;
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <returns></returns>
        public virtual int Query()
        {
            string sqlIndex = this.GetSqlIndex();

            string sql = this.GetExecSql(sqlIndex);
            if (sql == null)
            {
                MessageBox.Show("����Sql������" + sqlIndex + " ��ȡSql���ʧ��");
                return -1;
            }

            sql = this.FormatExecSql(sql);
            if (sql == null)
            {
                return -1;
            }

            DataSet ds = this.QueryDataSet(sql);
            if (ds == null)
            {
                return -1;
            }

            this.neuSpread1_Sheet1.DataSource = ds;

            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        public virtual void Export()
        {
            if (this.neuSpread1.Export() == 1)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ɹ�"));
                return;
            }
        }
    }

    /// <summary>
    /// ��Ŀ����
    /// </summary>
    public enum CustomItemTypeEnum
    {
        /// <summary>
        /// ҩƷ
        /// </summary>
        Drug,
        /// <summary>
        /// ����
        /// </summary>
        Dept,
        /// <summary>
        /// ��Ա
        /// </summary>
        Employee,
        /// <summary>
        /// �Զ���
        /// </summary>
        Custom
    }
}
