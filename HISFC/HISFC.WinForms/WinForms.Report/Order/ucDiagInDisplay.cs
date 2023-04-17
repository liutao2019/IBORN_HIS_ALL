using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.Order
{
    /// <summary>
    /// ����ҽ���к�
    /// </summary>
    public partial class ucDiagInDisplay : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.IDiagInDisplay
    {
        /// <summary>
        /// ����ҽ���к�
        /// </summary>
        public ucDiagInDisplay()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// �Һ���Ϣ
        /// </summary>
        private FS.HISFC.Models.Registration.Register register = new FS.HISFC.Models.Registration.Register();

        /// <summary>
        /// ����
        /// </summary>
        private FS.FrameWork.Models.NeuObject objRoom = new FS.FrameWork.Models.NeuObject();

        #endregion


        #region IDiagInDisplay ��Ա

        /// <summary>
        /// ʵ�ֽӿ�
        /// </summary>
        public void DiagInDisplay()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// ����
        /// </summary>
        public FS.FrameWork.Models.NeuObject ObjRoom
        {
            get
            {
                return this.objRoom;
            }
            set
            {
                this.objRoom = value;
            }
        }

        /// <summary>
        /// �Һ���Ϣ
        /// </summary>
        public FS.HISFC.Models.Registration.Register RegInfo
        {
            get
            {
                return this.register;
            }
            set
            {
                this.register = value;
            }
        }

        #endregion
    }
}

