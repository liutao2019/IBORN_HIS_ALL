using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.InpatientFee.Fee
{
    /// <summary>
    /// ucNurseQuitFee<br></br>
    /// [��������: סԺ��ʿ�˷�UC]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2006-11-06]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucNurseQuitFee : ucQuitFee
    {
        /// <summary>
        /// ��ʿվ�˷ѹ��캯��
        /// </summary>
        public ucNurseQuitFee()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            //���ò���������סԺ��
            this.IsCanInputInpatientNO = false;
            //���ñ��淽ʽΪ�˷�����
            this.operation = Operations.QuitFee;

            return base.OnInit(sender, neuObject, param);
        }

        /// <summary>
        /// ��ʾ���߻�����Ϣ
        /// </summary>
        protected override void SetPatientInfomation()
        {
            this.ucQueryPatientInfo.Text = this.patientInfo.PID.PatientNO;

            base.SetPatientInfomation();
        }

        /// <summary>
        /// ������ѡ��Ļ��߻�����Ϣ
        /// </summary>
        /// <param name="neuObject">���߻�����Ϣʵ��</param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            base.Clear();

            base.PatientInfo = neuObject as FS.HISFC.Models.RADT.PatientInfo;

            //base.PatientInfo = base.radtIntegrate.GetPatientInfomation(base.patientInfo.ID);

            return base.OnSetValue(neuObject, e);
        }
    }
}
