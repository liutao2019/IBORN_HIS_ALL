using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Preparation.Process
{
    /// <summary>
    /// <br></br>
    /// [��������: ��������(����ģ��¼��)����]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2008-03]<br></br>
    /// <���������>
    /// </���������>
    /// </summary>
    public partial class frmProcessBase : FS.FrameWork.WinForms.Forms.BaseStatusBar
    {
        public frmProcessBase()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ��Ա�б�
        /// </summary>
        protected System.Collections.ArrayList alStaticEmployee = null;

        /// <summary>
        /// �����б�
        /// </summary>
        protected System.Collections.ArrayList alStaticDept = null;

        /// <summary>
        /// ģ������
        /// </summary>
        protected FS.HISFC.Models.Preparation.EnumStencialType stencilType = FS.HISFC.Models.Preparation.EnumStencialType.SemiAssayStencial;

        #endregion

        #region ����

        /// <summary>
        /// ģ������
        /// </summary>
        public FS.HISFC.Models.Preparation.EnumStencialType StencilType
        {
            get
            {
                return this.stencilType;
            }
            set
            {
                this.stencilType = value;
            }
        }

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected virtual void Init()
        {
            if (alStaticEmployee == null)
            {
                FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
                alStaticEmployee = managerIntegrate.QueryEmployeeAll();
                if (alStaticEmployee == null)
                {
                    MessageBox.Show("������Ա�б�������" + managerIntegrate.Err);
                    return;
                }
            }

            if (this.alStaticDept == null)
            {
                FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
                this.alStaticDept = managerIntegrate.QueryDeptmentsInHos(false);
                if (alStaticDept == null)
                {
                    MessageBox.Show("���ؿ����б�������" + managerIntegrate.Err);
                    return;
                }
            }
        }

        #endregion

        /// <summary>
        /// ��"��"��"��" �ַ���ת��ΪBool
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        protected bool ConvertStringToBool(string str)
        {
            if (str == "��")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ��Boolת��Ϊ"��"��"��" �ַ���
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        protected string ConvertBoolToString(bool bl)
        {
            if (bl)
            {
                return "��";
            }
            else
            {
                return "��";
            }
        }

        /// <summary>
        /// ���ݲ�ͬ����Ŀ���� ���õ�Ԫ������
        /// </summary>
        /// <param name="fp">����ĵ�FarPoint</param>
        /// <param name="itemType">��Ŀ����</param>
        /// <param name="rowIndex">��Ԫ��������</param>
        /// <param name="columnIndex">��Ԫ��������</param>
        protected void SetReportCellType(FPItem fp,FarPoint.Win.Spread.SheetView sv,FS.HISFC.Models.Preparation.EnumStencilItemType itemType, int rowIndex, int columnIndex)
        {
            switch (itemType)
            {
                case FS.HISFC.Models.Preparation.EnumStencilItemType.Person:
                    fp.SetColumnList(sv, this.alStaticEmployee, columnIndex);
                    break;
                case FS.HISFC.Models.Preparation.EnumStencilItemType.Dept:
                    fp.SetColumnList(sv, this.alStaticDept, columnIndex);
                    break;
                case FS.HISFC.Models.Preparation.EnumStencilItemType.Date:
                    FS.FrameWork.WinForms.Classes.MarkCellType.DateTimeCellType markDateCellType = new FS.FrameWork.WinForms.Classes.MarkCellType.DateTimeCellType();
                    sv.Cells[rowIndex, columnIndex].CellType = markDateCellType;
                    break;
                case FS.HISFC.Models.Preparation.EnumStencilItemType.Number:
                    FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType numCellType = new FS.FrameWork.WinForms.Classes.MarkCellType.NumCellType();
                    sv.Cells[rowIndex, columnIndex].CellType = numCellType;
                    break;
                default:
                    break;
            }
        }        
    }
}