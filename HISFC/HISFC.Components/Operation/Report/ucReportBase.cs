using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Operation.Report
{
    /// <summary>
    /// [��������: �����������ͳ��]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-01-15]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucReportBase : UserControl, FS.FrameWork.WinForms.Forms.IReport
    {
        public ucReportBase()
        {
            InitializeComponent();
            if (!Environment.DesignMode)
            {
                this.Init();
                this.InitSpread();
                this.InitCategory();
            }
        }

#region ����
        protected bool ShowCategory
        {
            get
            {
                return this.cmbCategory.Visible;
            }
            set
            {
                this.cmbCategory.Visible = value;
                this.neuLabel4.Visible = value;
            }
        }

        protected string Title
        {
            get
            {
                return this.lblTitle.Text;
            }
            set
            {
                this.lblTitle.Text = value;
            }
        }
#endregion
        #region ����
        private void Init()
        {
            this.fpSpread1_Sheet1.Columns[-1].Locked = true;
            this.fpSpread1_Sheet1.GrayAreaBackColor = Color.White;
            this.fpSpread1_Sheet1.ColumnHeader.Rows[0].BackColor = Color.White;
            
            this.dtpBegin.Value = DateTime.Parse(Environment.OperationManager.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd") + " 00:00:00");
            this.dtpEnd.Value = DateTime.Parse(Environment.OperationManager.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd") + " 23:59:59");

            //������
            ArrayList alRet = Environment.IntegrateManager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.OP);
            this.cmbDept.AddItems(alRet);
            this.cmbDept.IsListOnly = true;
            this.cmbDept.Tag = Environment.OperatorDeptID;

        }

        protected virtual void InitSpread()
        {
            
        }

        protected virtual void InitCategory()
        {
            
        }

        protected virtual void OnCategoryChanged()
        {

        }

        protected virtual int OnQuery()
        {
            return -1;
        }
        #endregion

        #region IReport ��Ա

        public int Query()
        {
            return this.OnQuery();
        }

        #endregion

        #region IReportPrinter ��Ա

        public int Print()
        {
            return Environment.Print.PrintPreview(30,0,this.neuPanel2);
        }

        public int PrintPreview()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Export()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region �¼�
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.OnCategoryChanged();
        }
     

        private void dtpBegin_ValueChanged(object sender, EventArgs e)
        {
            this.lblTime.Text = string.Concat("��ѯʱ�䣺", this.dtpBegin.Value.ToString("yyyy-MM-dd HH:mm:ss"), " -- ", this.dtpEnd.Value.ToString("yyyy-MM-dd HH:mm:ss"));
        }

   #endregion



        #region IReport ��Ա


        public int SetParm(string parm, string reportID)
        {
            return 1;
        }

        #endregion

        #region IReport ��Ա


        public int SetParm(string parm, string reportID, string emplSql, string deptSql)
        {
            return 1;
        }

        #endregion
    }


}
