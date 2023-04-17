using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Registration
{
    public partial class ucChangeDeptInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private FS.HISFC.BizLogic.Fee.Account account = new FS.HISFC.BizLogic.Fee.Account();
        public ucChangeDeptInfo()
        {
            InitializeComponent();
        }
        #region ��
        /// <summary>
        /// �ҺŹ�����
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
        /// <summary>
        /// ���ƹ�����
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// �Ű������
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Schema schMgr = new FS.HISFC.BizLogic.Registration.Schema();
        /// <summary>
        /// ����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// ���������
        /// </summary>
        //private FS.HISFC.BizProcess.Integrate assMgr = new FS.HISFC.BizLogic.Management.Nurse.Assign();
        /// <summary>
        /// ���˺�����
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

            for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                this.neuSpread1_Sheet1.Columns[i].Locked = true;
            }

            #region �����Ѵ���
            ///����������0���յ���1��������2��������
            /////{E9A82E01-222A-4455-B5DD-B7AE7CB731AA}
            string rtn = this.ctlMgr.QueryControlerInfo("400027");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = "1";

            switch (rtn)
            {
                case "0":
                    {
                        //������
                        this.neuSpread1_Sheet1.Columns[8].Label = "����";
                        break;
                    }
                case "1": //��������
                    {
                        this.neuSpread1_Sheet1.Columns[8].Label = "��������";
                        break;
                    }
                case "2": //������
                    {

                        this.neuSpread1_Sheet1.Columns[8].Label = "������";
                        break;

                    }
                default:
                    break;
            }
            #endregion
            


            return 0;
        }

        /// <summary>
        /// ��ӻ��߹Һ���ϸ
        /// </summary>
        /// <param name="registers"></param>
        private void addRegister(ArrayList registers)
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);

            FS.HISFC.Models.Registration.Register obj;

            for (int i = registers.Count - 1; i >= 0; i--)
            {
                obj = (FS.HISFC.Models.Registration.Register)registers[i];
                this.addRegister(obj);
            }
        }
        /// <summary>
        /// ������ʹ��ֱ���շ����ɵĺ��ٽ��йҺ�
        /// </summary>
        /// <param name="CardNO"></param>
        /// <returns></returns>
        private int ValidCardNO(string CardNO)
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParams = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            string cardRule = controlParams.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.NO_REG_CARD_RULES, false, "9");
            if (CardNO != "" && CardNO != string.Empty)
            {
                if (CardNO.Substring(0, 1) == cardRule)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�˺Ŷ�Ϊֱ���շ�ʹ�ã������Ի���"), FS.FrameWork.Management.Language.Msg("��ʾ"));
                    return -1;
                }
            }
            return 1;
        }
        /// <summary>
        /// add a record to farpoint
        /// </summary>
        /// <param name="reg"></param>
        private void addRegister(FS.HISFC.Models.Registration.Register reg)
        {
            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);

            int cnt = this.neuSpread1_Sheet1.RowCount - 1;

            this.neuSpread1_Sheet1.SetValue(cnt, 0, reg.Name, false);
            this.neuSpread1_Sheet1.SetValue(cnt, 1, reg.Sex.Name, false);
            this.neuSpread1_Sheet1.SetValue(cnt, 2, reg.DoctorInfo.SeeDate.ToString(), false);
            this.neuSpread1_Sheet1.SetValue(cnt, 3, reg.DoctorInfo.Templet.Dept.Name, false);
            this.neuSpread1_Sheet1.SetValue(cnt, 4, reg.DoctorInfo.Templet.RegLevel.Name, false);
            this.neuSpread1_Sheet1.SetValue(cnt, 5, reg.DoctorInfo.Templet.Doct.Name, false);
            this.neuSpread1_Sheet1.SetValue(cnt, 6, reg.RegLvlFee.RegFee, false);
            //{E9A82E01-222A-4455-B5DD-B7AE7CB731AA}
            //this.neuSpread1_Sheet1.SetValue(cnt, 7, reg.RegLvlFee.OwnDigFee + reg.RegLvlFee.ChkFee + reg.RegLvlFee.OthFee, false);
            this.neuSpread1_Sheet1.SetValue(cnt, 7, reg.RegLvlFee.OwnDigFee + reg.RegLvlFee.ChkFee, false);
            this.neuSpread1_Sheet1.SetValue(cnt, 8, reg.RegLvlFee.OthFee, false);
            this.neuSpread1_Sheet1.SetValue(cnt, 9, FS.FrameWork.Function.NConvert.ToInt32(reg.IsSee));
            this.neuSpread1_Sheet1.Rows[cnt].Tag = reg;

            if (reg.IsSee)
            {
                this.neuSpread1_Sheet1.Rows[cnt].BackColor = Color.LightCyan;
            }
            if (reg.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back ||
                reg.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
            {
                this.neuSpread1_Sheet1.Rows[cnt].BackColor = Color.MistyRose;
            }
        }

        protected virtual ArrayList GetRegedInfo(ref string errText)
        {
            ArrayList alRegcollection = new ArrayList();
            switch (this.neuLabel1.Text)
            {
                //    case "��ˮ��":
                //        {
                //            // GetByClinic
                //            string clinicNo = this.txtCardNo.Text.Trim();
                //            if (clinicNo == "")
                //            {
                //                errText = "��ˮ�Ų���Ϊ��!";
                //                this.txtCardNo.Focus();
                //                return null;
                //            }
                //            //{D7742D35-6162-4b30-8F60-1F22E48C271D}
                //            //cardNo = cardNo.PadLeft(HISFC.Integrate.Common.ControlParam.GetCardNOLen(), '0');
                //            //this.txtCardNo.Text = cardNo;

                //            DateTime permitDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(-this.PermitDays).Date;
                //            //����������Ч��
                //            ArrayList almy = this.regMgr.QueryPatientList(clinicNo, "ALL");

                //            if (almy == null)
                //            {
                //                errText = "�������߹Һ���Ϣʱ����!" + this.regMgr.Err;
                //                return null;
                //            }

                //            ///�Ƴ������޶�ʱ��ĹҺ���Ϣ

                //            foreach (FS.HISFC.Models.Registration.Register obj in almy)
                //            {
                //                if (obj.DoctorInfo.SeeDate.Date < permitDate.Date) continue;

                //                alRegcollection.Add(obj);
                //            }



                //            break;
                //        }
                //case "���￨��":
                //    {
                //        string cardNo = this.txtCardNo.Text.Trim();
                //        if (cardNo == "")
                //        {
                //            errText = "�����Ų���Ϊ��!";
                //            this.txtCardNo.Focus();
                //            return null;
                //        }
                //        //{D7742D35-6162-4b30-8F60-1F22E48C271D}
                //        cardNo = cardNo.PadLeft(10, '0');
                //        this.txtCardNo.Text = cardNo;
                        
                //        DateTime permitDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(-this.PermitDays).Date;
                //        //����������Ч��
                //        alRegcollection = this.regMgr.QueryUnionNurse(cardNo, permitDate);

                //        if (alRegcollection == null)
                //        {
                //            errText = "�������߹Һ���Ϣʱ����!" + this.regMgr.Err;
                //            return null;
                //        }
                  case "���￨��":
                    {
                        string cardNum = this.txtCardNo.Text.Trim();
                        if (cardNum == ""||cardNum==null)
                        {
                            errText = "���￨�Ų���Ϊ��!";
                            this.txtCardNo.Focus();
                            return null;
                        }
                        //{D7742D35-6162-4b30-8F60-1F22E48C271D}
                        FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
                        int rev = this.feeMgr.ValidMarkNO(cardNum, ref accountCard);

                        if (rev > 0)
                        {
                            cardNum = accountCard.Patient.PID.CardNO;
                        }

                        cardNum = cardNum.PadLeft(10, '0');
                        DateTime permitDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(-this.PermitDays).Date;
                        //����������Ч��
                        alRegcollection = this.regMgr.QueryUnionNurse(cardNum, permitDate);

                        if (alRegcollection == null)
                        {
                            errText = "�������߹Һ���Ϣʱ����!" + this.regMgr.Err;
                            return null;
                        }

                        break;
                    }
                    //case "������":
                    //    {
                    //        ArrayList almy = new ArrayList();
                    //        string recipeNo = this.txtCardNo.Text.Trim();
                    //        if (recipeNo == "")
                    //        {
                    //            errText = "�����Ų���Ϊ��!";
                    //            this.txtCardNo.Focus();
                    //            return null;
                    //        }

                    //        DateTime permitDate = this.regMgr.GetDateTimeFromSysDateTime().AddDays(-this.PermitDays).Date;
                    //        //����������Ч��
                    //        almy = this.regMgr.QueryByRecipe(recipeNo);
                    //        if (almy == null)
                    //        {
                    //            errText = "�������߹Һ���Ϣʱ����!" + this.regMgr.Err;
                    //            return null;
                    //        }


                    //        ///�Ƴ������޶�ʱ��ĹҺ���Ϣ
                    //        ///
                    //        foreach (FS.HISFC.Models.Registration.Register obj in almy)
                    //        {
                    //            if (obj.DoctorInfo.SeeDate.Date < permitDate.Date) continue;

                    //            alRegcollection.Add(obj);
                    //        }


                //            break;
                //        }
                default:
                    {
                        break;
                    }

            }
            return alRegcollection;
        }

         //<summary>
         //ר��
         //</summary>
         //<param name="regObj">�Һ�ʵ��</param>
         //<returns></returns>
        protected virtual int ChangDept()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0) return -1;

            FS.HISFC.Models.Registration.Register regObj = new FS.HISFC.Models.Registration.Register();
            regObj = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Registration.Register;

            if (regObj != null)
            {
                if (!string.IsNullOrEmpty(regObj.DoctorInfo.Templet.ID))
                {
                    MessageBox.Show("ר�һ�ר�ƺŲ�������,������˺����¹ҺŲ���");
                    return -1;
                }

                if (regObj.IsSee)
                {
                    MessageBox.Show("�û����Ѿ�����,���ܽ��л���,���ʵ");
                    return -1;
                }

                if (regObj.IsTriage)
                {
                    MessageBox.Show("�û����Ѿ�����,�뵽��ʿֱ̨��ת��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
                if (regObj.Status != FS.HISFC.Models.Base.EnumRegisterStatus.Valid)
                {
                    MessageBox.Show("�øú��Ѿ�����,���ܻ���");
                    return -1;
                }

            }

            Forms.frmChangeDept frmChangeDept = new FS.HISFC.Components.Registration.Forms.frmChangeDept();
            frmChangeDept.ChangeDeptEvent += new EventHandler(frmChangeDept_ChangeDeptEvent);
            frmChangeDept.MyRegObj = regObj;
            DialogResult result = frmChangeDept.ShowDialog();
            if (result == DialogResult.OK)
            { 
            }
            return 1;
        }
        // <summary>
        // ���渳ֵ
        // </summary>
        // <param name="sender"></param>
        // <param name="e"></param>
        void frmChangeDept_ChangeDeptEvent(object sender, EventArgs e)
        {
            ArrayList al = sender as ArrayList;
            FS.FrameWork.Models.NeuObject myDept = al[0] as FS.FrameWork.Models.NeuObject;
            FS.FrameWork.Models.NeuObject myDoct = al[1] as FS.FrameWork.Models.NeuObject;
            FS.HISFC.Models.Registration.Register regObj = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Registration.Register;
            regObj.DoctorInfo.Templet.Dept = myDept;
            regObj.DoctorInfo.Templet.Doct = myDoct;
            this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag = regObj;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 3].Text = myDept.Name;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 5].Text = myDoct.Name;

        }

         //<summary>
         //���ݲ����ż������߹Һ���Ϣ
         //</summary>
         //<param name="sender"></param>
         //<param name="e"></param>
        private void txtCardNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int rowCount = neuSpread1_Sheet1.RowCount;
                if (rowCount > 0)
                {
                    this.neuSpread1_Sheet1.Rows.Remove(0, rowCount);
                }

                string errText = string.Empty;
                this.al = this.GetRegedInfo(ref errText);
                if (this.al == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg(errText));
                    return;
                }

                if (this.al.Count == 0)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�û���û�пɻ��ƺ�!"));
                    this.txtCardNo.Focus();
                    return;
                }
                else
                {
                    this.addRegister(al);


                    this.neuSpread1.Focus();
                    this.neuSpread1_Sheet1.ActiveRowIndex = 0;
                    //this.neuSpread1_Sheet1.AddSelection(0, 0, 1, 0);
                }
            }
        }
        /// <summary>
        /// ˫��ר��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_DoubleClick(object sender, EventArgs e)
        { }

        //}
        //private void txtCardNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        int rowCount = neuSpread1_Sheet1.RowCount;
        //        if (rowCount > 0)
        //        {
        //            this.neuSpread1_Sheet1.Rows.Remove(0, rowCount);
        //        }
        //        string cardNum = this.txtCardNo.Text.Trim();
        //        if (cardNum == "" || cardNum == null) {
        //            MessageBox.Show(FS.FrameWork.Management.Language.Msg("���￨�Ų���Ϊ��"), "��ʾ");
        //            this.txtCardNo.Focus();
        //            return;
        //        }

        //        cardNum = cardNum.PadLeft(10, '0');
        //        this.txtCardNo.Text = cardNum;
        //        string cardNo = "";//����Ψһ��ʶ
        //        bool flag = account.GetCardNoByMarkNo(cardNum, ref cardNo);
        //        if (!flag)
        //        {
        //            MessageBox.Show(FS.FrameWork.Management.Language.Msg("������ľ��￨�Ų�����"), "��ʾ");
        //            this.txtCardNo.Text = "";
        //            this.txtCardNo.Focus();
        //            return;
        //        }
                
        //        if (this.al.Count == 0)
        //        {
        //            MessageBox.Show(FS.FrameWork.Management.Language.Msg("�û���û�пɻ��ƺ�!"));
        //            this.txtCardNo.Focus();
        //            return;
        //        }
        //        else
        //        {
        //            this.addRegister(al);


        //            this.neuSpread1.Focus();
        //            this.neuSpread1_Sheet1.ActiveRowIndex = 0;
        //            //this.neuSpread1_Sheet1.AddSelection(0, 0, 1, 0);
        //        }
        //    }
        //}
        /// <summary>
        /// ˫��ר��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.neuSpread1_Sheet1.ActiveRowIndex >= 0)
            {
                //FS.HISFC.Models.Registration.Register regObj = new FS.HISFC.Models.Registration.Register();
                //regObj = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Registration.Register;
                ////if (regObj != null)
                //{
                //    if (!string.IsNullOrEmpty( regObj.DoctorInfo.Templet.ID))
                //    {
                //        MessageBox.Show("ר�һ�ר�ƺŲ�������,������˺����¹ҺŲ���");
                //        return;
                //    }

                //    if (regObj.IsSee)
                //    {
                //        MessageBox.Show("�û����Ѿ�����,���ܽ��л���,���ʵ");
                //        return;
                //    }
                //    if (regObj.Status != FS.HISFC.Models.Base.EnumRegisterStatus.Valid)
                //    {
                //        MessageBox.Show("�øú��Ѿ�����,���ܻ���");
                //        return;
                //    }

                //}
                if (this.ChangDept() < 0)
                {
                    return;
                }
            }
        }

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "����", (int)(int)FS.FrameWork.WinForms.Classes.EnumImageList.K����, true, false, null);
            return toolBarService;
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    {
                        this.ChangDept();
                        break;
                    }
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }
    }
}
