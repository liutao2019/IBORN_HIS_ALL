using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Pharmacy.Report.NanZhuang
{
    public partial class ucRecipeAppraise : SOC.Local.Pharmacy.Base.BaseReport
    {
        public ucRecipeAppraise()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 初始化
        /// </summary>
        private void InitData()
        {
            try
            {
                FS.HISFC.BizProcess.Integrate.Manager inteMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                System.Collections.ArrayList alDoctor = inteMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
                if (alDoctor == null)
                {
                    MessageBox.Show("查询医生出错：" + inteMgr.Err);
                    return;
                }

                this.ncbDoctor.AddItems(alDoctor);

                FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase healthRecordBaseMgr = new FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase();
                System.Collections.ArrayList alICD = healthRecordBaseMgr.ICDQuery(FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes.ICD10, FS.HISFC.Models.HealthRecord.EnumServer.QueryTypes.Valid);
                if (alICD == null)
                {
                    MessageBox.Show("查询ICD10出错：" + healthRecordBaseMgr.Err);
                    return;
                }
                this.ncbDiagnose.AddItems(alICD);
            }
            catch (Exception ex)
            {
                MessageBox.Show("程序发生错误：" + ex.Message, "错误>>");
            }
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns></returns>
        private new string[] GetQueryConditions()
        {
            if (this.IsDeptAsCondition && this.IsTimeAsCondition)
            {
                string[] parm = { "", "", "", "", "", "" };
                if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                {
                    parm[0] = this.cmbDept.Tag.ToString();
                }
                parm[1] = this.dtStart.Value.ToString();
                parm[2] = this.dtEnd.Value.ToString();
                parm[3] = this.GetParm()[0];
                parm[4] = this.GetParm()[1];
                parm[5] = this.GetParm()[2];

                return parm;
            }
            if (!this.IsDeptAsCondition && this.IsTimeAsCondition)
            {
                string[] parm = { "", "", "", "", "", "", "", "" };
                parm[0] = this.dtStart.Value.ToString();
                parm[1] = this.dtEnd.Value.ToString();
                parm[2] = this.GetParm()[0];
                parm[3] = this.GetParm()[1];
                parm[4] = this.GetParm()[2];
                return parm;
            }
            if (this.IsDeptAsCondition && !this.IsTimeAsCondition)
            {
                string[] parm = { "", "", "", "", "", "", "" };
                if (this.cmbDept.Tag != null && this.cmbDept.Tag.ToString() != "")
                {
                    parm[0] = this.cmbDept.Tag.ToString();
                }
                parm[1] = this.GetParm()[0];
                parm[2] = this.GetParm()[1];
                parm[3] = this.GetParm()[2];
                return parm;
            }

            string[] parmNull = { "Null", "Null", "Null", "Null", "Null", "Null" };
            return parmNull;
        }

        /// <summary>
        /// 获取不定查询条件
        /// </summary>
        /// <returns></returns>
        private string[] GetParm()
        {
            string doctorNO = "All";
            if (this.ncbDoctor.Tag != null && !string.IsNullOrEmpty(this.ncbDoctor.Tag.ToString()) && !string.IsNullOrEmpty(this.ncbDoctor.Text.Trim()))
            {
                doctorNO = this.ncbDoctor.Tag.ToString();
            }
            string diagnose = this.ncbDiagnose.Text.Trim();
            if (string.IsNullOrEmpty(diagnose))
            {
                diagnose = "All";
            }
            string patientInfo = this.nPatientInfo.Text;
            if (string.IsNullOrEmpty(patientInfo))
            {
                patientInfo = "All";
            }
            return new string[] { doctorNO, patientInfo, diagnose };

        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            if (!this.JugePrive())
            {
                MessageBox.Show("您没有权限：开通权限，请与系统管理员联系！");
                return 0;
            }
            this.QueryAdditionConditions = this.GetQueryConditions();
            this.IsNeedAdditionConditions = true;
            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// 判断是否有权限
        /// </summary>
        /// <param name="class2Code">二级权限</param>
        /// <param name="class3Code">三级权限</param>
        /// <returns>true有，false无</returns>
        private bool JugePrive()
        {
           return this.PriveClassTwos.Contains(FS.FrameWork.Management.Connection.Operator.ID);
        }

        /// <summary>
        /// 加载窗口
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            //界面打开时不查询
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
