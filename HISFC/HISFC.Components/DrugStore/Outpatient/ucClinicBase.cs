using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.DrugStore.Outpatient
{
    /// <summary>
    /// [��������: �����䷢ҩ����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11]<br></br>
    /// <�޸ļ�¼ 
    ///		
    ///  />
    /// </summary>
    public partial class ucClinicBase : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucClinicBase()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ��ǰ�����ⷿ
        /// </summary>
        private FS.FrameWork.Models.NeuObject operDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��ǰ������Ա��Ϣ
        /// </summary>
        private FS.FrameWork.Models.NeuObject operInfo = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��ǰ�����ն�
        /// </summary>
        internal FS.HISFC.Models.Pharmacy.DrugTerminal terminal = new FS.HISFC.Models.Pharmacy.DrugTerminal();

        /// <summary>
        /// ��׼�ⷿ
        /// </summary>
        private FS.FrameWork.Models.NeuObject approveDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ģ�鹦��
        /// </summary>
        internal DrugStore.OutpatientFun funModle = OutpatientFun.Drug;

        #endregion

        #region ����

        /// <summary>
        /// ��ǰ�����ⷿ
        /// </summary>
        [Description("��ǰ��������"),Category("����"),DefaultValue(null)]
        public FS.FrameWork.Models.NeuObject OperDept
        {
            get
            {
                return this.operDept;
            }
            set
            {
                this.operDept = value;
            }
        }

        /// <summary>
        /// ��ǰ������Ա��Ϣ
        /// </summary>
        [Description("��ǰ������Ա��Ϣ"), Category("����"), DefaultValue(null)]
        public virtual FS.FrameWork.Models.NeuObject OperInfo
        {
            get
             {
                return this.operInfo;
            }
            set
            {
                this.operInfo = value;
            }
        }

        /// <summary>
        /// ��׼�ⷿ
        /// </summary>
        [Description("��׼�ⷿ"), Category("����"), DefaultValue(null)]
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

        /// <summary>
        /// ����FunMode ���ڹ���
        /// </summary>
        public virtual void SetFunMode(DrugStore.OutpatientFun winFunMode)
        {
            this.funModle = winFunMode;
        }

        /// <summary>
        /// ���õ�ǰ�����ն�
        /// </summary>
        /// <param name="winTerminal">���������ն�ʵ����Ϣ</param>
        public virtual void SetTerminal(FS.HISFC.Models.Pharmacy.DrugTerminal winTerminal)
        {
            this.terminal = winTerminal;
        }


        #region ��ӡ��̬�ӿ�

        /// <summary>
        ///// ��ҩ������ӡ�ӿ�
        /// </summary>
        public static FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint RecipePrint = null;

        /// <summary>
        /// ��ҩ�嵥��ӡ�ӿ�
        /// </summary>
        public static FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint ListingPrint = null;

        #endregion
    }
}
