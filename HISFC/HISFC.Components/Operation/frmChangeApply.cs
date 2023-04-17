using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.HISFC.Models.Operation;

namespace Neusoft.HISFC.Components.Operation
{
    //{66E970CB-3342-4c38-9B14-0E514DDC82A3}
    public partial class frmChangeApply : Form
    {
        public frmChangeApply()
        {
            InitializeComponent();
        }
        Neusoft.HISFC.Models.RADT.PatientInfo p;
        public frmChangeApply(Neusoft.HISFC.Models.RADT.PatientInfo p2)
        {
            InitializeComponent();
            this.p = p2;
          //  btnOK.Text = p.PID.CardNO.ToString();
            this.Init();
        }
        public int opernum = 0;
#region 初始化
        private void Init()
        {
            ArrayList alApplys;
            try
            {
                this.ucArrangementSpread1.Reset();
              //  alApplys = Environment.OperationManager.GetOpsAppList(Environment.OperatorDeptID, beginTime, endTime, isNeedApprove);
                alApplys = Environment.OperationManager.GetOpsAppListByCardNo(this.p.PID.CardNO.ToString());
                opernum = alApplys.Count;
                if (alApplys != null)
                {
                    foreach (OperationAppllication apply in alApplys)
                    {
                        this.ucArrangementSpread1.AddOperationApplication(apply);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("生成手术申请信息出错!" + e.Message, "提示");
              
            }
        }
#endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.ucArrangementSpread1.FinishOperation()==1)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("更新门诊手术信息失败");
            }

        
        }
    }
}
