using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.InpatientFee.Maintenance
{
    /// <summary>
    /// ��Ժ����Ƿ�Ѳ�ѯ�ؼ�
    /// </summary>
    public partial class ucQueryPatienByAlert : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucQueryPatienByAlert()
        {
            InitializeComponent();
        }
                
        #region ����

        /// <summary>
        /// RADTҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.RADT radtManager = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// Managerҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ���߱�
        /// </summary>
        DataTable dtPatient = new DataTable();
        DataView dvPatient = new DataView();
        //
        //��ʿվ����
        string nurseCell = string.Empty;
        //
        //��������
        ArrayList alDept = new ArrayList();

        #endregion

        #region ����

        /// <summary>
        /// ��ʿվ����
        /// </summary>
        public string NurseCellCode
        {
            get 
            {
                return nurseCell;
            }
            set
            {
                nurseCell = value;
            }
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <returns></returns>
        private ArrayList GetDept(string nurseCode)
        {
            alDept = this.manager.QueryDepartment(nurseCode);
            return alDept;
        }

        #endregion

        #region ˽�з���

        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        protected virtual void InitControl()
        {
            this.txtMoney.Visible = false;
        }

        /// <summary>
        /// ��ʼ����
        /// </summary>
        protected virtual void InitDataTable()
        {
            this.dtPatient.Columns.AddRange(new DataColumn []
            {
                new DataColumn ("סԺ��",typeof(string)),
                new DataColumn ("����",typeof(string)),
                new DataColumn ("����",typeof(string)),
                new DataColumn ("����",typeof(string)),
                new DataColumn ("��ͬ��λ",typeof(string)),
                new DataColumn ("δ��Ԥ����",typeof(decimal)),
                new DataColumn ("δ���ܽ��",typeof(decimal)),
                new DataColumn ("���",typeof(decimal)),
                new DataColumn ("������",typeof(decimal))
                //new DataColumn ("",typeof()),
            });
            this.dvPatient = new DataView(this.dtPatient);
            this.fpPatient_Sheet1.DataSource = this.dvPatient;
        }

        /// <summary>
        /// �����в�������
        /// </summary>
        /// <param name="patInfo"></param>
        /// <param name="limit"></param>
        protected virtual void InsertData(FS.HISFC.Models.RADT.PatientInfo patInfo,decimal limit)
        {
            if (patInfo.FT.LeftCost < limit)
            {
                DataRow row = this.dtPatient.NewRow();
                row["סԺ��"] = patInfo.PID.PatientNO;
                row["����"] = patInfo.PVisit.PatientLocation.Dept.Name;
                row["����"] = patInfo.PVisit.PatientLocation.Bed.ID;
                row["����"] = patInfo.Name;
                row["��ͬ��λ"] = patInfo.Pact.Name;
                row["δ��Ԥ����"] = patInfo.FT.PrepayCost;
                row["δ���ܽ��"] = patInfo.FT.TotCost;
                row["���"] = patInfo.FT.LeftCost;
                row["������"] = patInfo.PVisit.MoneyAlert;
                this.dtPatient.Rows.Add(row);
            }
        }

        /// <summary>
        /// ����ʿվ��ѯ
        /// </summary>
        protected virtual void QueryByNurse()
        {
            this.dtPatient.Clear();
            alDept = this.GetDept(this.nurseCell);
            
            foreach (FS.HISFC.Models.Base.Department dept in alDept)
            {
                ArrayList alPat = this.radtManager.QueryPatient(dept.ID, FS.HISFC.Models.Base.EnumInState.I);
                foreach (FS.HISFC.Models.RADT.PatientInfo patientInfo in alPat)
                {
                    decimal limit = 0;
                    if (this.rdoAlert.Checked)
                    {
                        limit = patientInfo.PVisit.MoneyAlert;
                    }
                    if (this.rdoMoneyLevel.Checked)
                    {
                        limit = Convert.ToDecimal(this.txtMoney.Text.Trim());
                    }
                    this.InsertData(patientInfo, limit);
                }
            }
            this.dtPatient.AcceptChanges();
        }

        #endregion
                
        #region ���з���

        #endregion

        #region �¼�

        private void ucQueryPatienByAlert_Load(object sender, EventArgs e)
        {
            this.InitControl();
            this.InitDataTable();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.QueryByNurse();
        }

        private void rdoAlert_CheckedChanged(object sender, EventArgs e)
        {
            this.txtMoney.Visible = !this.rdoAlert.Checked;
            
        }

        private void rdoMoneyLevel_CheckedChanged(object sender, EventArgs e)
        {
            this.txtMoney.Visible = this.rdoMoneyLevel.Checked;
        }
               
        #endregion
                
    }
}

