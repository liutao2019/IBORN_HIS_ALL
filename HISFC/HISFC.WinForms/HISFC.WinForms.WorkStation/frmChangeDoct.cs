﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.WinForms.WorkStation
{
    public partial class frmChangeDoct : Form
    {
        public frmChangeDoct()
        {
            InitializeComponent();
        }

        #region 变量域

        /// <summary>
        /// Integrate业务层

        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerMag = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 实体
        /// </summary>
        private FS.FrameWork.Models.NeuObject myObject = null;
        #endregion

        #region 属性

        /// <summary>
        /// 医生实体
        /// </summary>
        public FS.FrameWork.Models.NeuObject MyObject
        {
            set 
            {
                this.myObject = value;
            }
            get
            {
                return this.myObject;
            }
        }
        #endregion
        #region 方法
        /// <summary>
        /// 初始化医生

        /// </summary>
        /// <returns></returns>
        public int initDoct()
        {
            ArrayList emplAl = new ArrayList();
            emplAl = managerMag.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D, ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);
            if (emplAl.Count > 0)
            {
                this.cmbDoct.AddItems(emplAl);
                this.cmbDoct.SelectedIndex = 0;
            }
            
            return 1;
        }
        
        #endregion

        private void frmChangeDoct_Load(object sender, EventArgs e)
        {
            this.initDoct();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (this.cmbDoct.Tag == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("请选择医生"));
                return;
            }
            this.MyObject = this.cmbDoct.SelectedItem as FS.FrameWork.Models.NeuObject;
            this.DialogResult = DialogResult.OK;
            this.Close();
            
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}