using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Pharmacy.Plan
{
    /// <summary>
    /// [��������: Fp�������]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-10]<br></br>
    /// <�޸�>
    /// </�޸�>
    /// </summary>
    public partial class ucSortManager : UserControl
    {
        public ucSortManager()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ���򼶱�
        /// </summary>
        private List<string> sortLevel = new List<string>();

        /// <summary>
        /// ���
        /// </summary>
        private DialogResult result = DialogResult.Cancel;

        
        #endregion

        #region ����

        /// <summary>
        /// �������ȼ�
        /// </summary>
        public List<string> SortLevel
        {
            get
            {
                return this.sortLevel;
            }
        }

        /// <summary>
        /// ����ʽ
        /// </summary>
        public SortDirection Direction
        {
            get
            {
                if (this.ckAscend.Checked)
                {
                    return SortDirection.Ascend;
                }
                else
                {
                    return SortDirection.Descend;
                }
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public DialogResult Result
        {
            get
            {
                return this.result;
            }
        }
        #endregion

        /// <summary>
        /// ��ȡ�������Fp��Ϣ
        /// </summary>
        /// <param name="sortColumn">���������</param>
        /// <returns>�ɹ���ȡ��Ϣ����1 ʧ�ܷ��أ�1</returns>
        public int SetFarPoint(params string[] sortColumn)
        {
            this.neuSpread1_Sheet1.Rows.Count = sortColumn.Length;
            for (int i = 0; i < sortColumn.Length; i++)
            {
                this.neuSpread1_Sheet1.Cells[i, 0].Text = sortColumn[i];
            }

            return 1;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.AutoSortColumn(1,true);
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, 1].Text != "")
                {
                    this.sortLevel.Add(this.neuSpread1_Sheet1.Cells[i, 0].Text);
                }
            }

            if (this.ParentForm != null)
            {
                this.ParentForm.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.result = DialogResult.Cancel;

            if (this.ParentForm != null)
            {
                this.ParentForm.Close();
            }
        }
    }

    public class MultiSortBase<T> where T : class
    {
        public delegate IComparer<T> GetCompareDelegate(List<string> hsSortColumnLevel,SortDirection direction);

        public GetCompareDelegate GetCompareInstance = null;

        public void MultiStort(List<T> subList, ref List<T> parentList,List<string> sortColumnLevelList,SortDirection direction)
        {
            IComparer<T> sortCom = GetCompareInstance(sortColumnLevelList,direction);

            this.MultiSort(subList, ref parentList, sortCom,sortColumnLevelList,direction);
        }

        public void MultiSort(List<T> subList, ref List<T> parentList, IComparer<T> compare, List<string> sortColumnLevelList,SortDirection direction)
        {
            if (subList.Count == 1)     //ֻ��һ����Ŀ
            {
                parentList.Add(subList[0]);
                return;
            }
            if (compare == null)
            {
                parentList.AddRange(subList);
                return;
            }

            subList.Sort(compare);

            parentList.AddRange(subList);

            return;

            #region ԭ�ݹ鴦��ʽ  ���δ��ִ���ʽ

            //List<List<T>> subListCollecte = QuerySubListInstance(subList, compareLevel);

            //compareLevel++;

            //foreach (List<T> list in subListCollecte)
            //{
            //    compare = GetCompareInstance(compareLevel,this.hsSortColumnLevel);

            //    MultiSort(list, ref parentList, compare, compareLevel);
            //}

            #endregion
        }
    }

    /// <summary>
    /// �������
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        /// ����
        /// </summary>
        Ascend,
        /// <summary>
        /// ����
        /// </summary>
        Descend,
    }
}
