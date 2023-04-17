using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Pharmacy.Report
{
    public partial class ucRecipeAppraise : SOC.Local.Pharmacy.Base.BaseReport
    {
        public ucRecipeAppraise()
        {
            InitializeComponent();
        }


        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void InitData()
        {
            try
            {
                FS.HISFC.BizProcess.Integrate.Manager inteMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                System.Collections.ArrayList alDoctor = inteMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
                if (alDoctor == null)
                {
                    MessageBox.Show("��ѯҽ������"+inteMgr.Err);
                    return;
                }

                this.ncbDoctor.AddItems(alDoctor);

                FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase healthRecordBaseMgr = new FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase();
                System.Collections.ArrayList alICD = healthRecordBaseMgr.ICDQuery(FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes.ICD10, FS.HISFC.Models.HealthRecord.EnumServer.QueryTypes.Valid);
                if (alICD == null)
                {
                    MessageBox.Show("��ѯICD10����" + healthRecordBaseMgr.Err);
                    return;
                }
                this.ncbDiagnose.AddItems(alICD);
            }
            catch (Exception ex)
            {
                MessageBox.Show("����������"+ex.Message,"����>>");
            }
        }

        /// <summary>
        /// ��ȡ��ѯ����
        /// </summary>
        /// <returns></returns>
        private new string[] GetQueryConditions()
        {
            if (this.IsDeptAsCondition && this.IsTimeAsCondition)
            {
                string[] parm = { "", "", "", "", "" };
                if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                {
                    parm[0] = this.cmbDept.Tag.ToString();
                }
                parm[1] = this.dtStart.Value.ToString();
                parm[2] = this.dtEnd.Value.ToString();
                parm[3] = this.GetParm()[0];
                parm[4] = this.GetParm()[1];

                return parm;
            }
            if (!this.IsDeptAsCondition && this.IsTimeAsCondition)
            {
                string[] parm = { "", "", "", "", "", "", "" };
                parm[0] = this.dtStart.Value.ToString();
                parm[1] = this.dtEnd.Value.ToString();
                parm[2] = this.GetParm()[0];
                parm[3] = this.GetParm()[1];
                return parm;
            }
            if (this.IsDeptAsCondition && !this.IsTimeAsCondition)
            {
                string[] parm = { "", "", "", "", "", "" };
                if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                {
                    parm[0] = this.cmbDept.Tag.ToString();
                }
                parm[1] = this.GetParm()[0];
                parm[2] = this.GetParm()[1];
                return parm;
            }

            string[] parmNull = { "Null", "Null", "Null", "Null", "Null"};
            return parmNull;
        }

        /// <summary>
        /// ��ȡ������ѯ����
        /// </summary>
        /// <returns></returns>
        private string[] GetParm()
        {
            string doctorNO = "AAAA";
            if (this.ncbDoctor.Tag != null && !string.IsNullOrEmpty(this.ncbDoctor.Tag.ToString()) && !string.IsNullOrEmpty(this.ncbDoctor.Text.Trim()))
            {
                doctorNO = this.ncbDoctor.Tag.ToString();
            }
            string diagnose = this.ncbDiagnose.Text.Trim();
            if (string.IsNullOrEmpty(diagnose))
            {
                diagnose = "AAAA";
            }
            return new string[] { doctorNO, diagnose };
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryAdditionConditions = this.GetQueryConditions();
            this.IsNeedAdditionConditions = true;
            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// ���ش���
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            //�����ʱ����ѯ
            this.QueryDataWhenInit = false;

            this.InitData();
            base.OnLoad(e);

            if (this.cmbDept.alItems != null && this.cmbDept.alItems.Count > 0)
            {
                this.cmbDept.SelectedIndex = 0;
                this.cmbDept.Tag = (this.cmbDept.alItems[0] as FS.FrameWork.Models.NeuObject).ID;
            }
        }
    }
}
