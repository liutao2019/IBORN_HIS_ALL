using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace GJLocal.HISFC.Components.OpGuide.RegistionExtend
{
    public partial class ucGJOutPatientInfoBill2 : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        //{AB392EE7-0666-4456-B29F-458730318812}
        public ucGJOutPatientInfoBill2()
        {
            InitializeComponent();
            //this.ucMain2.AutoScroll = true;
            //this.Clean();
        }

        #region 变量与属性
        /// <summary>
        /// 门诊流水号
        /// </summary>
        private string clinc_code = "";

        /// <summary>
        /// 门诊流水号
        /// </summary>
        public string Clinc_code
        {
            get { return clinc_code; }
            set { clinc_code = value; }
        }

        /// <summary>
        /// 挂号日期
        /// </summary>
        private string dtReg = "";

        /// <summary>
        /// 挂号日期
        /// </summary>
        public string DtReg
        {
            get { return dtReg; }
            set
            {
                dtReg = value;
                this.lbDateTimeReg.Text = "日期：" + dtReg;
            }
        }

        #region 业务变量
        /// <summary>
        /// 费用管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeManage = new FS.HISFC.BizProcess.Integrate.Fee();
        
        FS.HISFC.BizLogic.Registration.GJLocal.GJOutPatientInfoRegister gjMgr
                    = new FS.HISFC.BizLogic.Registration.GJLocal.GJOutPatientInfoRegister();
        #endregion
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            this.Query();
            base.OnLoad(e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return 1;
        }

        /// <summary>
        /// 数节点查询
        /// </summary>
        private void Query()
        {
            this.Clean();
            this.neuTreeView1.Nodes.Clear();

            string sql = "";
            if (string.IsNullOrEmpty(this.textBox1.Text.Trim()))
            {
                sql = @"
                  select to_char(r.reg_date,'yyyy-MM-dd'),
                    r.clinic_code,
                    r.name
                    from fin_opr_register r
                    where r.reg_date between to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                    and  to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                    and  r.valid_flag='1'
                    order by r.reg_date desc
                    ";  //{BE53AD4A-F4BE-4d6a-8E82-F7F6718D6691}
                sql = string.Format(sql, this.ndtpBegin.Value.Date.ToString(), this.ndtpEnd.Value.AddDays(1).Date.ToString());

            }
            else
            {
                #region 作废重新写 
                //                sql = @"
//                  select to_char(r.reg_date,'yyyy-MM-dd'),
//                    r.clinic_code,
//                    r.name
//                    from fin_opr_register r,fin_opb_accountcard_record a
//                    where (r.card_no like '%{0}' or a.markno like '%{0}')
//                    and r.card_no=a.card_no
//                    and  r.valid_flag='1'
//                    ";
//                sql = string.Format(sql, this.textBox1.Text.Trim());
                //{9043E842-7A83-4213-AB6D-E7E04B2EF76E} lfhm
                string medicalCardNo = this.textBox1.Text.Trim();
                if (medicalCardNo == string.Empty)
                {
                    return;
                }
                FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
                int resultValue = feeManage.ValidMarkNO(medicalCardNo, ref accountCard);

                if (resultValue <= 0)
                {
                    MessageBox.Show("没有查询到患者信息！" + feeManage.Err);
                    this.textBox1.Focus();
                    this.textBox1.SelectAll();
                    return;
                }
                sql = @"
                    select to_char(r.reg_date,'yyyy-MM-dd'),
                    r.clinic_code,
                    r.name
                    from fin_opr_register r
                    where r.reg_date between to_date('{0}','yyyy-MM-dd hh24:mi:ss')
                    and  to_date('{1}','yyyy-MM-dd hh24:mi:ss')
                    and r.card_no = '{2}' 
                    and  r.valid_flag='1'
                     order by r.reg_date desc"; //{BE53AD4A-F4BE-4d6a-8E82-F7F6718D6691}
                sql = string.Format(sql, this.ndtpBegin.Value.Date.ToString(), this.ndtpEnd.Value.AddDays(1).Date.ToString(),accountCard.Patient.PID.CardNO);
                #endregion
            }
            DataSet dsRes = new DataSet();

            this.gjMgr.ExecQuery(sql, ref dsRes);

            Hashtable hsDate = new Hashtable();

            foreach (DataRow row in dsRes.Tables[0].Rows)
            {
                if (!hsDate.Contains(row[0].ToString()))
                {
                    TreeNode nodeDate = new TreeNode();
                    nodeDate.Text = row[0].ToString();

                    TreeNode nodeRep = new TreeNode();
                    nodeRep.Text = "患者:" + row[2].ToString();
                    nodeRep.Tag = row[1].ToString();

                    nodeDate.Nodes.Add(nodeRep);

                    this.neuTreeView1.Nodes.Add(nodeDate);
                    hsDate.Add(row[0].ToString(), nodeDate);
                }
                else
                {
                    TreeNode nodeDate = hsDate[row[0].ToString()] as TreeNode;

                    TreeNode nodeRep = new TreeNode();
                    nodeRep.Text = "患者:" + row[2].ToString();
                    nodeRep.Tag = row[1].ToString();

                    nodeDate.Nodes.Add(nodeRep);
                }
            }


            this.neuTreeView1.ExpandAll();
        }

        /// <summary>
        /// 清除
        /// </summary>
        private void Clean()
        {
            this.cbD.Checked = false;
            this.cbG.Checked = false;
            this.cbN.Checked = false;
            this.ucConsultation1.Clean();
            this.ucDentist11.Clean();
            this.ucGeneral11.Clean();
            this.ucNerve11.Clean();
            this.ucBackPage1.Clean();
            this.ucNurseRecords1.Clean();
            this.tbD.Parent = null;
            this.tbG.Parent = null;
            this.tbN.Parent = null;
        }


        private int Save()
        {
            if (string.IsNullOrEmpty(this.clinc_code))
            {
                MessageBox.Show("请先选择患者！");
                return -1;
            }
            System.Collections.ArrayList alSave = new System.Collections.ArrayList();
            if (this.cbD.Checked)
            {
                System.Collections.ArrayList al1 = this.ucDentist11.GetValue();
                alSave.AddRange(al1);
            }
            if (this.cbG.Checked)
            {
                System.Collections.ArrayList al2 = this.ucGeneral11.GetValue();
                alSave.AddRange(al2);
            }
            if (this.cbN.Checked)
            {
                System.Collections.ArrayList al3 = this.ucNerve11.GetValue();
                alSave.AddRange(al3);
            }
            System.Collections.ArrayList al4 = this.ucBackPage1.GetValue();
            alSave.AddRange(al4);
            System.Collections.ArrayList al5 = this.ucNurseRecords1.GetValue();
            alSave.AddRange(al5);
            if (gjMgr.DeleteGJRegisterInfo(this.clinc_code) < 0)
            {
                MessageBox.Show("保存失败:" + gjMgr.Err);
                return -1;
            }
            if (gjMgr.InsertGJRegisterInfo(alSave) < 0)
            {
                MessageBox.Show("保存失败:" + gjMgr.Err);
                return -1;
            }

            MessageBox.Show("保存成功！");
            this.Clean();
            return 1;
        }

        private void SetPatientInfo()
        {
            if (string.IsNullOrEmpty(this.clinc_code))
            {
                MessageBox.Show("请选择一位患者！");
                return;
            }
            this.ucConsultation1.Clinc_code = this.clinc_code;
            this.ucDentist11.Clinic_code = this.clinc_code;
            this.ucGeneral11.Clinic_code = this.clinc_code;
            this.ucNerve11.Clinic_code = this.clinc_code;
            this.ucBackPage1.Clinic_code = this.clinc_code;
            this.ucNurseRecords1.Clinic_code = this.clinc_code;
            this.lbID.Text = "ID:" + this.clinc_code;
            this.SetValue();
        }

        private void SetPatientInfo2()
        {
            if (string.IsNullOrEmpty(this.clinc_code))
            {
                MessageBox.Show("请选择一位患者！");
                return;
            }
            this.ucConsultation1.Clinc_code = this.clinc_code;
            this.ucDentist11.Clinic_code = this.clinc_code;
            this.ucGeneral11.Clinic_code = this.clinc_code;
            this.ucNerve11.Clinic_code = this.clinc_code;
            this.ucBackPage1.Clinic_code = this.clinc_code;
            this.ucNurseRecords1.Clinic_code = this.clinc_code;
            this.lbID.Text = "ID:" + this.clinc_code;
            this.SetValue2();
        }

        private void SetValue()
        {
            this.ucConsultation1.SetValue();
            System.Collections.Hashtable hsD = gjMgr.QueryGJRegisterInfo(this.clinc_code, "D1");
            if (hsD.Count > 0)
            {
                this.ucDentist11.SetValue(hsD);
                this.cbD.Checked = true;
            }
            System.Collections.Hashtable hsG = gjMgr.QueryGJRegisterInfo(this.clinc_code, "G1");
            if (hsG.Count > 0)
            {
                this.ucGeneral11.SetValue(hsG);
                this.cbG.Checked = true;
            }
            System.Collections.Hashtable hsN = gjMgr.QueryGJRegisterInfo(this.clinc_code, "N1");
            if (hsN.Count > 0)
            {
                this.ucNerve11.SetValue(hsN);
                this.cbN.Checked = true;
            }
            System.Collections.Hashtable hsBG = gjMgr.QueryGJRegisterInfo(this.clinc_code, "BG");
            if (hsBG.Count > 0)
            {
                this.ucBackPage1.SetValue(hsBG);
            }

            System.Collections.Hashtable hsNR = gjMgr.QueryGJRegisterInfo(this.clinc_code, "NR");
            if (hsNR.Count > 0)
            {
                this.ucNurseRecords1.SetValue(hsNR);
            }
        }

        private void SetValue2()
        {
            this.ucConsultation1.SetValue();
            System.Collections.Hashtable hsD = gjMgr.QueryGJRegisterInfo(this.clinc_code, "D1");
            if (hsD.Count > 0)
            {
                this.cbD.Checked = true;// {9F524B95-FBB8-42cb-B69B-BB565F6EB2BD} lfhm
                this.ucDentist11.SetValue(hsD);
            }
            else
            {
                this.cbD.Checked = false;
                this.tbD.Parent = null;
            }
            System.Collections.Hashtable hsG = gjMgr.QueryGJRegisterInfo(this.clinc_code, "G1");
            if (hsG.Count > 0)
            {
                this.cbG.Checked = true;
                this.ucGeneral11.SetValue(hsG);
            }
            else
            {
                this.cbG.Checked = false;
                this.tbG.Parent = null;
            }
            System.Collections.Hashtable hsN = gjMgr.QueryGJRegisterInfo(this.clinc_code, "N1");
            if (hsN.Count > 0)
            {
                this.cbN.Checked = true;
                this.ucNerve11.SetValue(hsN);
            }
            else
            {
                this.cbN.Checked = false;
                this.tbN.Parent = null;
            }
            System.Collections.Hashtable hsBG = gjMgr.QueryGJRegisterInfo(this.clinc_code, "BG");
            if (hsBG.Count > 0)
            {
                this.ucBackPage1.SetValue(hsBG);
            }

            System.Collections.Hashtable hsNR = gjMgr.QueryGJRegisterInfo(this.clinc_code, "NR");
            if (hsNR.Count > 0)
            {
                this.ucNurseRecords1.SetValue(hsNR);
            }
        }

        public void QueryPatientInfo(string clinic_code1)
        {
            this.clinc_code = clinic_code1;
            this.splitContainer1.Panel1Collapsed = true;
            this.SetPatientInfo2();
        }

        /// <summary>
        /// 整体查询函数，外接总入口
        /// </summary>
        /// <param name="clinic_code">门诊流水号</param>
        /// <param name="isFillBill">true 填单 false 查看</param>
        private void Query(string clinic_code1, bool isFillBill)
        {
            this.clinc_code = clinic_code1;
            this.SetPatientInfo();
        }

        protected override int OnSave(object sender, object neuObject)
        {
            return this.Save();
        }


        private void neuTreeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Text.Contains("患者"))
            {
                this.Clean();
                //this.ucMain2.DtReg = e.Node.Parent.Text;
                this.Query(e.Node.Tag.ToString(), true);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                this.Query();
            }
        }

        private void cbD_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbD.Checked&&tbD.Parent==null)
            {
                tbD.Parent = tabControl1;
            }
            else
            {
                tbD.Parent = null;
            }
        }

        private void cbG_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbG.Checked&&this.tbG.Parent==null)
            {
                this.tbG.Parent = tabControl1;
            }
            else 
            {
                this.tbG.Parent = null;
            }
        }

        private void cbN_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbN.Checked&&tbN.Parent==null)
            {
                this.tbN.Parent = tabControl1;
            }
            else
            {
                this.tbN.Parent = null;
            }
        }
    }
}
