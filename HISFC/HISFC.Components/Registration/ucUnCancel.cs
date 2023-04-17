using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Registration
{
    public partial class ucUnCancel : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucUnCancel()
        {
            InitializeComponent();

            this.fpSpread1.KeyDown += new KeyEventHandler(fpSpread1_KeyDown);
            this.txtRecipeNo.KeyDown += new KeyEventHandler(txtRecipeNo_KeyDown);
            this.txtCardNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCardNo_KeyDown);

            this.Init();
        }

        #region ��
        /// <summary>
        /// �ҺŹ�����
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
        private FS.HISFC.BizLogic.Registration.Schema schMgr = new FS.HISFC.BizLogic.Registration.Schema();
        /// <summary>
        /// ���ƹ�����
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();
        /// <summary>
        /// �ɴ�ӡ�Һŷ�Ʊ����
        /// </summary>
        private int PermitDays = 0;
        private ArrayList al = new ArrayList();

        #endregion
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns></returns>
        private int Init()
        {
            string Days = this.ctlMgr.QueryControlerInfo("400006");

            if (Days == null || Days == "" || Days == "-1")
            {
                this.PermitDays = 1;
            }
            else
            {
                this.PermitDays = int.Parse(Days);
            }
            this.txtCardNo.Focus();
            return 0;
        }

        /// <summary>
        /// ���ݲ����ż������߹Һ���Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cardNo = this.txtCardNo.Text.Trim();
                if (cardNo == "")
                {
                    MessageBox.Show("�����Ų���Ϊ��!", "��ʾ");
                    this.txtCardNo.Focus();
                    return;
                }

                cardNo = cardNo.PadLeft(10, '0');
                this.txtCardNo.Text = cardNo;

                DateTime permitDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(-this.PermitDays).Date;
                //�����������Ϻ�
                this.al = this.regMgr.QueryCancel(cardNo, permitDate);
                if (this.al == null)
                {
                    MessageBox.Show("�������߹Һ���Ϣʱ����!" + this.regMgr.Err, "��ʾ");
                    return;
                }

                if (this.al.Count == 0)
                {
                    MessageBox.Show("�û���û�����Ϻ�!", "��ʾ");
                    this.txtCardNo.Focus();
                    return;
                }
                else
                {
                    this.addRegister(al);

                    this.fpSpread1.Focus();
                    this.fpSpread1_Sheet1.SetActiveCell(0, 2, false);
                }
            }
        }


        /// <summary>
        /// �����Żس�,���ݴ����ż������߹Һ���Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRecipeNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string recipeNo = this.txtRecipeNo.Text.Trim();
                if (recipeNo == "")
                {
                    MessageBox.Show("�����Ų���Ϊ��!", "��ʾ");
                    this.txtRecipeNo.Focus();
                    return;
                }

                DateTime permitDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(-this.PermitDays).Date;

                //�������߹Һ�
                this.al = this.regMgr.QueryByRecipe(recipeNo);
                if (this.al == null)
                {
                    MessageBox.Show("�������߹Һ���Ϣʱ����!" + this.regMgr.Err, "��ʾ");
                    return;
                }

                ArrayList alRegCollection = new ArrayList();

                ///�Ƴ������޶�ʱ��ĹҺ���Ϣ
                ///
                foreach (FS.HISFC.Models.Registration.Register obj in this.al)
                {
                    if (obj.DoctorInfo.SeeDate.Date < permitDate.Date) continue;

                    alRegCollection.Add(obj);
                }

                if (alRegCollection.Count == 0)
                {
                    MessageBox.Show("�û���û�����Ϻ�!", "��ʾ");
                    this.txtRecipeNo.Focus();
                    return;
                }
                else
                {
                    this.addRegister(alRegCollection);

                    this.fpSpread1.Focus();
                    this.fpSpread1_Sheet1.SetActiveCell(0, 2, false);
                }
            }
        }
        /// <summary>
        /// ��ӻ��߹Һ���ϸ
        /// </summary>
        /// <param name="registers"></param>
        private void addRegister(ArrayList registers)
        {
            if (this.fpSpread1_Sheet1.RowCount > 0)
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);

            FS.HISFC.Models.Registration.Register obj;

            for (int i = registers.Count - 1; i >= 0; i--)
            {
                obj = (FS.HISFC.Models.Registration.Register)registers[i];
                this.addRegister(obj);
            }
        }
        /// <summary>
        /// add a record to farpoint
        /// </summary>
        /// <param name="reg"></param>
        private void addRegister(FS.HISFC.Models.Registration.Register reg)
        {
            this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);

            int cnt = this.fpSpread1_Sheet1.RowCount - 1;

            this.fpSpread1_Sheet1.SetValue(cnt, 0, reg.Name, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 1, reg.Sex.Name, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 2, reg.DoctorInfo.SeeDate, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 3, reg.DoctorInfo.Templet.Dept.Name, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 4, reg.DoctorInfo.Templet.RegLevel.Name, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 5, reg.DoctorInfo.Templet.Doct.Name, false);
            this.fpSpread1_Sheet1.SetValue(cnt, 6, reg.RegLvlFee.RegFee , false);
            this.fpSpread1_Sheet1.SetValue(cnt, 7, reg.RegLvlFee.OwnDigFee + reg.RegLvlFee.OthFee + reg.RegLvlFee.ChkFee, false);
            this.fpSpread1_Sheet1.Rows[cnt].Tag = reg;

            if (reg.IsSee)
            {
                this.fpSpread1_Sheet1.Rows[cnt].BackColor = Color.Cyan;
            }
            if (reg.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back||
                reg.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
            {
                this.fpSpread1_Sheet1.Rows[cnt].BackColor = Color.Red;
            }
        }
        
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        private int save()
        {
            #region ��֤
            if (this.fpSpread1_Sheet1.RowCount == 0)
            {
                MessageBox.Show("û�йҺż�¼!", "��ʾ");
                return -1;
            }

            int row = this.fpSpread1_Sheet1.ActiveRowIndex;

            //ʵ��
            FS.HISFC.Models.Registration.Register reg;

            reg = this.regMgr.GetByClinic((this.fpSpread1_Sheet1.Rows[row].Tag as FS.HISFC.Models.Registration.Register).ID);
            if (reg == null || reg.ID == null || reg.ID == "")
            {
                //t.RollBack() ;
                MessageBox.Show("û�иùҺ���Ϣ!" + this.regMgr.Err, "��ʾ");
                return -1;
            }

            if (reg.Status != FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
            {
                //t.RollBack() ;
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "�ùҺ���Ϣ�������Ϻ�,����ȡ������" ), "��ʾ" );
                return -1;
            }

            if (reg.BalanceOperStat.IsCheck)
            {
                MessageBox.Show( FS.FrameWork.Management.Language.Msg( "�ùҺ���Ϣ�Ѿ��ս�,����ȡ������" ), "��ʾ" );
                return -1;
            }

            //�ָ�ԭ���Ű��޶�
            //���ԭ�������޶�,��ô�ָ��޶�
            if (reg.DoctorInfo.Templet.ID != null && reg.DoctorInfo.Templet.ID != "")
            {
                //�ֳ��š�ԤԼ�š������


                bool IsReged = false, IsTeled = false, IsSped = false;

                if (reg.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)
                {
                    IsTeled = true; //ԤԼ��
                }
                else if (reg.RegType == FS.HISFC.Models.Base.EnumRegType.Reg)
                {
                    IsReged = true;//�ֳ���
                }
                else
                {
                    IsSped = true;//�����
                }

                int rtn = this.schMgr.AddLimit(reg.DoctorInfo.Templet.ID, IsReged, false, IsTeled, IsSped);
                if (rtn == -1)
                {
                   // t.RollBack();
                    MessageBox.Show(this.schMgr.Err, "��ʾ");
                    return -1;
                }

                if (rtn == 0)
                {
                    //t.RollBack();
                    MessageBox.Show( FS.FrameWork.Management.Language.Msg( "�����Ű���Ϣ,�޷��ָ��޶�" ), "��ʾ" );
                    return -1;
                }
            }

            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(regMgr.con);
            //SQLCA.BeginTransaction();

            try
            {
                this.regMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (this.regMgr.Update(FS.HISFC.BizLogic.Registration.EnumUpdateStatus.Uncancel, reg) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("ȡ�����ϳ���!" + this.regMgr.Err, "��ʾ");
                    return -1;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return -1;
            }

            MessageBox.Show( FS.FrameWork.Management.Language.Msg( "ȡ�����ϳɹ�" ), "��ʾ" );

            this.Clear();

            return 0;
        }
        /// <summary>
        /// ����
        /// </summary>
        private void Clear()
        {
            if (this.fpSpread1_Sheet1.RowCount > 0)
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);

            this.txtCardNo.Focus();
        }

        /// <summary>
        /// �����ݼ�
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.S.GetHashCode())
            {
                this.save();

                return true;
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.X.GetHashCode())
            {
                this.FindForm().Close();
            }
            else if (keyData == Keys.Escape)
            {
                this.FindForm().Close();
            }

            return base.ProcessDialogKey(keyData);
        }
        /// <summary>
        /// fp�س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.save();
            }
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.save();

            return base.OnSave(sender, neuObject);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtRecipeNo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
