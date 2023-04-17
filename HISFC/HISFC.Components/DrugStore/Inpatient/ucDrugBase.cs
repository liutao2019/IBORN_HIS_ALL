using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace FS.HISFC.Components.DrugStore.Inpatient
{
    /// <summary>
    /// <br></br>
    /// [��������: סԺ��ҩ����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11]<br></br>
    /// </summary>
    public class ucDrugBase : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDrugBase ()
        {

        }

        #region �����

        /// <summary>
        /// �Ƿ��Զ���ӡ��ҩ��
        /// </summary>
        private bool isAutoPrint = false;

        /// <summary>
        /// ��ҩ����ʽ�Ƿ��ӡ��ǩ
        /// </summary>
        private bool isPrintLabel = false;

        /// <summary>
        /// ��ҩ����ӡʱ�Ƿ���ҪԤ��
        /// </summary>
        private bool isNeedPreview = true;

        /// <summary>
        /// ��ʾʱ�Ƿ��տ���������ʾ
        /// </summary>
        private bool isDeptFirst = true;

        /// <summary>
        /// ��ҩҩ�����
        /// </summary>
        private FS.FrameWork.Models.NeuObject arkDept = null;

        /// <summary>
        /// ��ҩ��׼���� ��ʵ�ʿۿ���ң�
        /// </summary>
        private FS.FrameWork.Models.NeuObject approveDept = null;

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ��ӡ��ǩ
        /// </summary>
        [Description("��ӡʱ�Ƿ��ӡ��ǩ"), Category("����"), DefaultValue(false)]
        public bool IsPrintLabel
        {
            get
            {
                return this.isPrintLabel;
            }
            set
            {
                this.isPrintLabel = value;
            }
        }

        /// <summary>
        /// �Ƿ��Զ���ӡ��ҩ��
        /// </summary>
        [Description("�Ƿ��Զ���ӡ��ҩ��"), Category("����"), DefaultValue(false)]
        public bool IsAutoPrint
        {
            get
            {
                return this.isAutoPrint;
            }
            set
            {
                this.isAutoPrint = value;
            }
        }

        /// <summary>
        /// ��ҩ����ӡʱ�Ƿ���ҪԤ�� �Զ���ӡ���ǩ��ӡʱ�ò�����Ч
        /// </summary>
        [Description("��ҩ����ӡʱ�Ƿ���ҪԤ�� �Զ���ӡ���ǩ��ӡʱ�ò�����Ч"), Category("����"), DefaultValue(true)]
        public bool IsNeedPreview
        {
            get
            {
                return this.isNeedPreview;
            }
            set
            {
                this.isNeedPreview = value;
            }
        }

        /// <summary>
        /// ��ʾʱ�Ƿ��տ���������ʾ
        /// </summary>
        [Description("��ҩ���б���ʾʱ �Ƿ��տ���������ʾ �ò���Ӱ���ҩ֪ͨ����ʾ"), Category("����"), DefaultValue(true)]
        public bool IsDeptFirst
        {
            get
            {
                return this.isDeptFirst;
            }
            set
            {
                this.isDeptFirst = value;
            }
        }

        /// <summary>
        /// ��ҩҩ�����
        /// </summary>
        [Description("��ǰ��½ҩ����Ϣ"), Category("����"), DefaultValue(true)]
        public FS.FrameWork.Models.NeuObject ArkDept
        {
            get
            {
                return this.arkDept;
            }
            set
            {
                this.arkDept = value;
            }
        }

        /// <summary>
        /// ��ҩ��׼���� ��ʵ�ʿۿ���ң�
        /// </summary>
        public FS.FrameWork.Models.NeuObject ApproveDept
        {
            get
            {
                return this.approveDept;
            }
            set
            {
                this.approveDept = value;
            }
        }

        #endregion
    }
}
