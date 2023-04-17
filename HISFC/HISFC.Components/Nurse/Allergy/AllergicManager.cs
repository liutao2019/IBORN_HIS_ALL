using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Nurse.Allergy
{
    /// <summary>
    /// 过敏史管理
    /// </summary>
    public partial class AllergicManager : UserControl
    {
        public AllergicManager()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 使用类型（住院or门诊）
        /// </summary>
        private FS.HISFC.Models.Base.ServiceTypes useKind;

        /// <summary>
        /// 是否有修改权限
        /// </summary>
        private bool isCanModify = false;

        /// <summary>
        /// 当前患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.Patient currentPatientInfo;

        #endregion

        #region 属性

        /// <summary>
        /// 是否有修改权限（增加、保存、作废）
        /// </summary>
        public bool IsCanModify
        {
            get
            {
                return isCanModify;
            }
            set
            {
                isCanModify = value;
            }
        }

        /// <summary>
        /// 使用类型（住院or门诊）
        /// </summary>
        public FS.HISFC.Models.Base.ServiceTypes UperKind
        {
            get
            {
                return useKind;
            }
            set
            {
                useKind = value;
            }
        }

        /// <summary>
        /// 当前患者实体
        /// </summary>
        public FS.HISFC.Models.RADT.Patient CurrentPatientInfo
        {
            get
            {
                return this.currentPatientInfo;
            }
            set
            {
                this.currentPatientInfo = value;
                if (value != null)
                {
                    if (this.useKind == FS.HISFC.Models.Base.ServiceTypes.C)
                    {
                        FS.HISFC.Models.Registration.Register regInfo = this.currentPatientInfo as FS.HISFC.Models.Registration.Register;
                        lblPatientInfo.Text = "卡号: " + regInfo.PID.CardNO + " 姓名: " + regInfo.Name + " 性别: " + regInfo.Sex.Name + " 挂号时间: " + regInfo.DoctorInfo.SeeDate.ToString() + " 挂号科室: " + regInfo.DoctorInfo.Templet.Dept.Name;
                    }
                }
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 查询过敏史记录
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        private int Query(string patientID)
        {
            return 1;
        }

        #endregion

        #region 事件

        private void btAdd_Click(object sender, EventArgs e)
        {
            this.fpAllergic_Sheet1.AddRows(this.fpAllergic_Sheet1.RowCount - 1, 1);
        }

        #endregion

        private void btCancel_Click(object sender, EventArgs e)
        {

        }

        private void btSave_Click(object sender, EventArgs e)
        {

        }
    }
}
