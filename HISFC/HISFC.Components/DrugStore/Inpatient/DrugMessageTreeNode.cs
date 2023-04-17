using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace FS.HISFC.Components.DrugStore.Inpatient
{
    /// <summary>
    /// <br></br>
    /// [��������: ��ҩ֪ͨ�ڵ�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2008 - 04]<br></br>
    /// <˵��>
    ///   {AB3B4EEB-A1C5-4a37-AD42-4EF66DF8F859}  
    /// </˵��>
    /// </summary>
    public partial class DrugMessageTreeNode : System.Windows.Forms.TreeNode
    {
        private DrugMessageNodeType nodeType = DrugMessageNodeType.ApplyDept;

        /// <summary>
        /// ��ҩ֪ͨ�ڵ�
        /// </summary>
        internal DrugMessageNodeType NodeType
        {
            get
            {
                return this.nodeType;
            }
            set
            {
                this.nodeType = value;
            }
        }       
    }

    /// <summary>
    /// ��ҩ֪ͨ�ڵ�����ö��
    /// </summary>
    internal enum DrugMessageNodeType
    {
        /// <summary>
        /// �������
        /// </summary>
        ApplyDept,
        /// <summary>
        /// ��ҩ��
        /// </summary>
        DrugBill,
        /// <summary>
        /// ������Ϣ
        /// </summary>
        Patient
    }
}
