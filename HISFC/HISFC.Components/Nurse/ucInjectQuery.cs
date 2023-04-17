using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Nurse
{
    /// <summary>
    /// ע���ѯ�������
    /// </summary>
    public partial class ucInjectQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region ������
        /// <summary>
        /// ��Ա��Ϣ��
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager personMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// ע�亯����
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Inject injMgr = new FS.HISFC.BizLogic.Nurse.Inject();
        /// <summary>
        /// ���˷�����Ϣ
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee patientMgr = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// �ҺŹ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Registration.Registration registerManager = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        /// <summary>
        /// ��ӡ����
        /// </summary>
        private ArrayList printData = new ArrayList();
        /// <summary>
        /// ��ǰ���߹Һ���Ϣ
        /// </summary>
        private FS.HISFC.Models.Registration.Register reg = null;
        /// <summary>
        ///��Һ������
        /// </summary>
        private ArrayList injectionInfo = new ArrayList();

        ArrayList alResult = new ArrayList();
        /// <summary>
        /// ���ע��˳���
        /// </summary>
        int maxInjectOrder = 0;
        #endregion

        public ucInjectQuery()
        {
            InitializeComponent();
            this.Init();
        }

        #region ��ʼ��

        private void Init()
        {
            //����Ա
            ArrayList al = new ArrayList();
            al = personMgr.QueryEmployeeAll();
            if (al == null) al = new ArrayList();
            //�Ǽ���
            this.cmbRegOper.AddItems(al);
            //��ҩ��
            this.cmbMixOper.AddItems(al);
            //ע����
            this.cmbInjectOper.AddItems(al);

            this.txtCardNo.Focus();
            this.setFP();
            this.dtRegDate.Value = this.injMgr.GetDateTimeFromSysDateTime();
            this.dtInjectDate.Value = this.injMgr.GetDateTimeFromSysDateTime();
            this.dtMixDate.Value = this.injMgr.GetDateTimeFromSysDateTime();
        }

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("ȫѡ", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȫѡ, true, false, null);
            this.toolBarService.AddToolButton("ȡ��ȫѡ", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            this.toolBarService.AddToolButton("��ӡƿǩ", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            this.toolBarService.AddToolButton("��ӡǩ����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            this.toolBarService.AddToolButton("��ӡע�䵥", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            this.toolBarService.AddToolButton("��ӡ��Һ��", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡ, true, false, null);
            this.toolBarService.AddToolButton("ȡ���Ǽ�", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            return this.toolBarService;
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return 1;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return 1;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "ȫѡ":
                    this.SelectAll(true);
                    break;
                case "ȡ��":
                    this.SelectAll(false);
                    break;
                case "ȡ��ȫѡ":
                    this.SelectAll(false);
                    break;
                case "��ӡƿǩ":
                    this.Print();
                    break;
                case "��ӡǩ����":
                    this.PrintItinerate();
                    break;
                case "��ӡע�䵥":
                    this.PrintInject();
                    break;
                case "��ӡ��Һ��":
                    this.PrintInjectNew();
                    break;
                case "ȡ���Ǽ�":
                    this.Save();
                    break;
                default:
                    break;
            }
        }


        #endregion

        #region ����

        /// <summary>
        /// ��ȡSQL���
        /// </summary>
        /// <returns></returns>
        private int GetSQL()
        {
            alResult = new ArrayList();
            string strSQL = "";
            string strWhere = "";
            bool IsWhere = false;

            #region ��ȡ����SQL
            try
            {
                strSQL = this.injMgr.GetSqlInjectInfo();
            }
            catch (Exception e)
            {
                MessageBox.Show("��ȡSQL����!" + e.Message);
                return -1;
            }

            #endregion

            #region	 ���ݽ�������������WHERE����

            //1.����
            string cardNo = "";
            #region {D9F5A25A-7529-4415-9CAC-20D1C7667B2F}
            if (!string.IsNullOrEmpty(txtCardNo.Text.Trim()))
            {
                cardNo = this.txtCardNo.Text.Trim().PadLeft(10, '0');

                ArrayList regs = this.registerManager.Query(cardNo, this.dtRegDate.Value.AddDays(-7));
                if (regs == null || regs.Count == 0)
                {
                    MessageBox.Show("û�в�����Ϊ:" + cardNo + "�Ļ���!", "��ʾ");
                }
                else
                {
                    this.reg = regs[0] as FS.HISFC.Models.Registration.Register;
                }
                this.txtCardNo.Text = cardNo;
            } 
            #endregion
            if (cardNo != null && cardNo != "" && cardNo != "0000000000")
            {
                strWhere = " where CARD_NO = '" + cardNo + "'";
                IsWhere = true;
            }
            //2.��������
            string patientName;
            patientName = this.txtName.Text.Trim();
            if (patientName != null && patientName != "")
            {
                if (!IsWhere)
                {
                    strWhere = strWhere + " where PATIENT_NAME = '" + patientName + "'";
                }
                else
                {
                    strWhere = strWhere + " and PATIENT_NAME = '" + patientName + "'";
                }
                IsWhere = true;
            }
            //3.�Ǽ���
            if (this.cbRegOper.Checked)
            {
                if (!IsWhere)
                {
                    strWhere = strWhere + " where BOOKER_ID = '" + this.cmbRegOper.Tag.ToString().Trim() + "'";
                }
                else
                {
                    strWhere = strWhere + " and BOOKER_ID = '" + this.cmbRegOper.Tag.ToString().Trim() + "'";
                }
                IsWhere = true;
            }
            //4.�Ǽ����� 
            if (this.cbRegDate.Checked)
            {
                if (!IsWhere)
                {
                    strWhere = strWhere +
                        " where REGISTER_DATE >= to_date('" + this.dtRegDate.Value.ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss')"
                        + " and REGISTER_DATE < to_date('" + this.dtRegDate.Value.AddHours(24).ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss')";
                }
                else
                {
                    strWhere = strWhere +
                        " and REGISTER_DATE >= to_date('" + this.dtRegDate.Value.ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss')"
                        + " and REGISTER_DATE < to_date('" + this.dtRegDate.Value.AddHours(24).ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss')";
                }
                IsWhere = true;
            }
            //5.��ˮ��
            if (this.cbOrder.Checked)
            {
                if (!IsWhere)
                {
                    strWhere = strWhere + "where ORDER_NO = '" + this.txtOrder.Text.ToString().Trim() + "'";
                }
                else
                {
                    strWhere = strWhere + "and ORDER_NO = '" + this.txtOrder.Text.ToString().Trim() + "'";
                }
                IsWhere = true;
            }
            //6.��ҩ��ʿ
            if (this.cbMixOper.Checked)
            {
                if (!IsWhere)
                {
                    strWhere = strWhere + " where MIX_ID = '" + this.cmbMixOper.Tag.ToString().Trim() + "'";
                }
                else
                {
                    strWhere = strWhere + " and MIX_ID = '" + this.cmbMixOper.Tag.ToString().Trim() + "'";
                }
                IsWhere = true;
            }
            //7.��ҩʱ��
            if (this.cbMixDate.Checked)
            {
                if (!IsWhere)
                {
                    #region {D9F5A25A-7529-4415-9CAC-20D1C7667B2F}
                    strWhere = strWhere +
                        " where MIX_DATE >= to_date('" + this.dtMixDate.Value.ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss')"
                        + " and MIX_DATE < to_date('" + this.dtMixDate.Value.AddHours(24).ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss')";
                    //strWhere = strWhere +
                    //    " where MIX_DATE >= to_date('" + this.dtMixDate.Value.ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss')"
                    //    + " and MIX_DATE < to_date('" + this.dtMixDate.Value.AddHours(24).ToString("yyyy-MM-dd 00:00:00").Trim() + ",'yyyy-mm-dd hh24:mi:ss'')"; 
                    #endregion
                }
                else
                {
                    strWhere = strWhere +
                        " and MIX_DATE >= to_date('" + this.dtMixDate.Value.ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss')"
                        + " and MIX_DATE < to_date('" + this.dtMixDate.Value.AddHours(24).ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss')";
                }
                IsWhere = true;
            }
            //8.ע�令ʿ
            if (this.cbInjectOper.Checked)
            {
                if (!IsWhere)
                {
                    strWhere = strWhere + " where INJECT_CODE = '" + this.cmbInjectOper.Tag.ToString().Trim() + "'";
                }
                else
                {
                    strWhere = strWhere + " and INJECT_CODE = '" + this.cmbInjectOper.Tag.ToString().Trim() + "'";
                }
                IsWhere = true;
            }
            //9.ע��ʱ��
            if (this.cbInjectDate.Checked)
            {
                if (!IsWhere)
                {
                    strWhere = strWhere +
                        " where INJECT_DATE >= to_date('" + this.dtInjectDate.Value.ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss')"
                        + " and INJECT_DATE < to_date('" + this.dtInjectDate.Value.AddHours(24).ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss')";
                }
                else
                {
                    strWhere = strWhere +
                        " and INJECT_DATE >= to_date('" + this.dtInjectDate.Value.ToString("yyyy-MM-dd 00:00:00").Trim() + "')"
                        + " and INJECT_DATE < to_date('" + this.dtInjectDate.Value.AddHours(24).ToString("yyyy-MM-dd 00:00:00").Trim() + "','yyyy-mm-dd hh24:mi:ss') ";
                }
                IsWhere = true;
            }
            //�������
            if (!IsWhere)
            {
                MessageBox.Show("�������ѯ����!");
                return -1;
            }
            strWhere = strWhere + "order by ID";
            #endregion

            strSQL = strSQL + strWhere;
            alResult = this.injMgr.myGetInfo(strSQL);
            return 0;
        }
        /// <summary>
        /// ��ѯ
        /// </summary>
        private void Query()
        {
            
            this.Clear();
            if (this.GetSQL() == -1)
            {

                return;
            }

            if (alResult == null || alResult.Count == 0)
            {
                MessageBox.Show("û�з��ϲ�ѯ����������!", "��ʾ");
                this.txtCardNo.Focus();
                return;
            }

            this.AddDetail(alResult);
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.lessShow();
                this.SelectAll(false);
                this.SetComb();
            }

            this.setFP();
            this.txtCardNo.Focus();
            if (this.txtCardNo.Text.ToString().Trim() == "0000000000")
            {
                this.txtCardNo.Text = "";
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }
        /// <summary>
        /// ��ѯ�Ľ����ӵ�����
        /// </summary>
        /// <param name="al"></param>
        private void AddDetail(ArrayList al)
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
            }
            FS.HISFC.BizProcess.Integrate.Manager con = new FS.HISFC.BizProcess.Integrate.Manager();
            foreach (FS.HISFC.Models.Nurse.Inject info in al)
            {
                //				FS.HISFC.Components.Object.neuObject obj = con.Get(FS.HISFC.Models.Base.enuConstant.USAGE,info.Item.UsageInfo.ID);
                //				if(obj.Memo == null ||obj.Memo == "" || obj.Memo != this.Tag.ToString().Trim()) continue;

                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                int row = this.neuSpread1_Sheet1.RowCount - 1;
                this.neuSpread1_Sheet1.Rows[row].Tag = info;

                #region ʵ�帴�Ƶ�����
                string strTest = this.GetHyTestInfo(((Int32)info.Hypotest).ToString());
                
                
                FS.HISFC.BizProcess.Integrate.Manager PsMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                FS.HISFC.Models.Base.Employee ps = PsMgr.GetEmployeeInfo(info.Booker.ID);

                this.neuSpread1_Sheet1.SetValue(row, 1, info.ID.ToString());//��ˮ��
                this.neuSpread1_Sheet1.SetValue(row, 2, info.OrderNO.ToString());//��˳���
                this.neuSpread1_Sheet1.Cells[row, 2].Tag = info.OrderNO.ToString();
                this.neuSpread1_Sheet1.SetValue(row, 3, info.Patient.PID.CardNO.ToString());//������
                this.neuSpread1_Sheet1.Cells[row, 3].Tag = info.Patient.PID.CardNO.ToString();
                this.neuSpread1_Sheet1.SetValue(row, 4, info.Patient.Name.ToString());//��������
                this.neuSpread1_Sheet1.Cells[row, 4].Tag = info.Patient.Name.ToString();
                this.neuSpread1_Sheet1.SetValue(row, 5, info.Item.Order.ReciptDoctor.Name.ToString());//����ҽ��
                this.neuSpread1_Sheet1.Cells[row, 5].Tag = info.Item.Order.ReciptDoctor.ID.ToString();
                this.neuSpread1_Sheet1.SetValue(row, 6, info.Item.Order.DoctorDept.Name.ToString());//��������
                this.neuSpread1_Sheet1.Cells[row, 6].Tag = info.Item.Order.DoctorDept.ID.ToString();
                this.neuSpread1_Sheet1.SetValue(row, 7, info.Item.Name.ToString());//��Ŀ����
                //				this.fpSpread1_Sheet1.SetValue(row,8,info.Item.CombNo.ToString());//���
                this.neuSpread1_Sheet1.Cells[row, 8].Tag = info.Item.Order.Combo.ID.ToString();
                this.neuSpread1_Sheet1.SetValue(row, 9, info.Item.Order.DoseOnce.ToString() + info.Item.Order.DoseUnit.ToString());//ÿ����
                this.neuSpread1_Sheet1.SetValue(row, 10, info.Item.Order.Frequency.ID.ToString());//Ƶ��
                this.neuSpread1_Sheet1.SetValue(row, 11, info.Item.Order.Usage.Name.ToString());//�÷�
                this.neuSpread1_Sheet1.SetValue(row, 12, strTest);//Ƥ��
                this.neuSpread1_Sheet1.SetValue(row, 13, ps.Name);//�Ǽ���
                this.neuSpread1_Sheet1.SetValue(row, 14, info.Booker.OperTime.ToString());//�Ǽ�ʱ��
                this.neuSpread1_Sheet1.Cells[row, 14].Tag = info.Booker.OperTime.ToString();
                this.neuSpread1_Sheet1.SetValue(row, 15, info.MixOperInfo.Name.ToString());//��ҩ��
                this.neuSpread1_Sheet1.SetValue(row, 16, info.MixTime.ToString());//��ҩʱ��
                this.neuSpread1_Sheet1.SetValue(row, 17, info.InjectOperInfo.Name.ToString());//ע����
                this.neuSpread1_Sheet1.SetValue(row, 18, info.InjectTime.ToString());//ע��ʱ��
                this.neuSpread1_Sheet1.SetValue(row, 19, info.InjectSpeed.ToString());//����
                FS.HISFC.Models.Base.Employee stopName = PsMgr.GetEmployeeInfo(info.StopOper.ID);
                this.neuSpread1_Sheet1.SetValue(row, 20, stopName);//������
                this.neuSpread1_Sheet1.SetValue(row, 21, info.EndTime.ToString());//����ʱ��
                this.neuSpread1_Sheet1.SetValue(row, 22, info.SendemcTime.ToString());//�ͼ���ʱ��
                this.neuSpread1_Sheet1.SetValue(row, 23, info.Memo.ToString());//���������¼
                #endregion
            }
        }
        /// <summary>
        /// ȡ���Ǽ�//1.�����ǵ�����ȡ��  2.�Ѿ���ҩ��ע��Ĳ���ȡ���Ǽ�  3.ֻȡ����Թ���
        /// </summary>
        private void Save()
        {
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                MessageBox.Show("û����Ҫ���������!");

                return;
            }
            int selectNum = 0;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE" || this.neuSpread1_Sheet1.GetValue(i, 0).ToString() == "")
                {
                    selectNum++;
                }
            }
            if (selectNum >= this.neuSpread1_Sheet1.RowCount)
            {
                MessageBox.Show("��ѡ������", "��ʾ");
                return;
            }
            if (MessageBox.Show("���Ƿ�Ҫɾ����ѡ��ĵǼ���Ϣ��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.neuSpread1.StopCellEditing();

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                //SQLCA.BeginTransaction();

                this.injMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.patientMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                try
                {

                    for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                    {
                        FS.HISFC.Models.Nurse.Inject info =
                            (FS.HISFC.Models.Nurse.Inject)this.neuSpread1_Sheet1.Rows[i].Tag;

                        #region ��Ч�����ж�
                        if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE") continue;
                        if (info.MixOperInfo.ID != null && info.MixOperInfo.ID != "")
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("��" + (i + 1).ToString() + "�������Ѿ���ҩȷ�ϣ�����ȡ���Ǽ�!");
                            return;
                        }
                        if (info.InjectOperInfo.ID != null && info.InjectOperInfo.ID != "")
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("��" + (i + 1).ToString() + "�������Ѿ�ע��ȷ�ϣ�����ȡ���Ǽ�!");
                            return;
                        }
                        if (info.Booker.ID != FS.FrameWork.Management.Connection.Operator.ID)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            FS.HISFC.BizProcess.Integrate.Manager PsMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                            FS.HISFC.Models.Base.Employee ps = PsMgr.GetEmployeeInfo(info.Booker.ID);

                            MessageBox.Show("�����ǵ�" + (i + 1).ToString() + "�м�¼�ĵǼ��ˣ�����ȡ���Ǽ�!",
                                "ֻ��[" + ps.Name + "]����ȡ��������¼!");
                            return;
                        }
                        #endregion
                        //1.ɾ��met_nuo_inject��¼
                        if (this.injMgr.Delete(info.ID) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.patientMgr.Err, "��ʾ");
                            return;
                        }
                        //2.fin_ipb_feeitemlist�У���������------------û�п��ǲ���������,�Ժ���˵��----����û��moorder,����!
                        if (this.patientMgr.UpdateConfirmInject("ALL", info.Item.RecipeNO, info.Item.SequenceNO.ToString(), -1) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.patientMgr.Err, "��ʾ");
                            return;
                        }
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();
                    for (int i = this.neuSpread1_Sheet1.RowCount - 1; i >= 0; i--)
                    {
                        if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "TRUE")
                        {
                            this.neuSpread1_Sheet1.Rows[i].Remove();
                        }
                    }
                    MessageBox.Show("ȡ���Ǽǳɹ�!", "��ʾ");
                }
                catch (Exception e)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(e.Message);
                    return;
                }
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        private void Clear()
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
        }
        /// <summary>
        /// ȫѡ
        /// </summary>
        private void SelectAll(bool isSelected)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                this.neuSpread1_Sheet1.SetValue(i, 0, isSelected, false);
            }
        }

        #endregion

        #region �ڲ�����
        /// <summary>
        /// ������Ϻ�
        /// </summary>
        private void SetComb()
        {
            try
            {
                int myCount = this.neuSpread1_Sheet1.RowCount;
                int i;
                //��һ��
                this.neuSpread1_Sheet1.SetValue(0, 8, "��");
                //�����
                this.neuSpread1_Sheet1.SetValue(myCount - 1, 8, "��");
                //�м���
                for (i = 1; i < myCount - 1; i++)
                {
                    int prior = i - 1;
                    int next = i + 1;
                    string currentRowCombNo = this.neuSpread1_Sheet1.Cells[i, 8].Tag.ToString();//��Ϻ�
                    string currentRegDate = this.neuSpread1_Sheet1.Cells[i, 14].Tag.ToString();//�Ǽ�ʱ��
                    string priorRowCombNo = this.neuSpread1_Sheet1.Cells[prior, 8].Tag.ToString();
                    string priorRegDate = this.neuSpread1_Sheet1.Cells[prior, 14].Tag.ToString();//�Ǽ�ʱ��
                    string nextRowCombNo = this.neuSpread1_Sheet1.Cells[next, 8].Tag.ToString();
                    string nextRegDate = this.neuSpread1_Sheet1.Cells[next, 14].Tag.ToString();//�Ǽ�ʱ��

                    #region """""
                    bool bl1 = true;
                    bool bl2 = true;
                    if (currentRowCombNo != priorRowCombNo || currentRegDate != priorRegDate)
                        bl1 = false;
                    if (currentRowCombNo != nextRowCombNo || currentRegDate != nextRegDate)
                        bl2 = false;
                    //  ��
                    if (bl1 && bl2)
                    {
                        this.neuSpread1_Sheet1.SetValue(i, 8, "��");
                    }
                    //  ��
                    if (bl1 && !bl2)
                    {
                        this.neuSpread1_Sheet1.SetValue(i, 8, "��");
                    }
                    //  ��
                    if (!bl1 && bl2)
                    {
                        this.neuSpread1_Sheet1.SetValue(i, 8, "��");
                    }
                    //  ""
                    if (!bl1 && !bl2)
                    {
                        this.neuSpread1_Sheet1.SetValue(i, 8, "");
                    }
                    #endregion
                }
                //��û����ŵ�ȥ��
                for (i = 0; i < myCount; i++)
                {
                    if (this.neuSpread1_Sheet1.Cells[i, 8].Tag.ToString() == "")
                    {
                        this.neuSpread1_Sheet1.SetValue(i, 8, "");
                    }
                }
                //�ж���ĩ�� ����ţ���ֻ���Լ�һ�����ݵ����
                if (myCount == 1)
                {
                    this.neuSpread1_Sheet1.SetValue(0, 8, "");
                }
                //ֻ����ĩ���У���ô��Ҫ�ж���Ű�
                if (myCount == 2)
                {
                    if (this.neuSpread1_Sheet1.Cells[0, 8].Tag.ToString() != this.neuSpread1_Sheet1.Cells[1, 5].Tag.ToString())
                    {
                        this.neuSpread1_Sheet1.SetValue(0, 8, "");
                        this.neuSpread1_Sheet1.SetValue(1, 8, "");
                    }
                }
                if (myCount > 2)
                {
                    if (this.neuSpread1_Sheet1.GetValue(1, 8).ToString() != "��"
                        && this.neuSpread1_Sheet1.GetValue(1, 8).ToString() != "��")
                    {
                        this.neuSpread1_Sheet1.SetValue(0, 8, "");
                    }
                    if (this.neuSpread1_Sheet1.GetValue(myCount - 2, 8).ToString() != "��"
                        && this.neuSpread1_Sheet1.GetValue(myCount - 2, 8).ToString() != "��")
                    {
                        this.neuSpread1_Sheet1.SetValue(myCount - 1, 8, "");
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message + "������Ŵ���");
            }
        }
        /// <summary>
        /// ���ø�ʽ
        /// </summary>
        private void setFP()
        {
            FarPoint.Win.Spread.CellType.TextCellType txtOnly = new FarPoint.Win.Spread.CellType.TextCellType();
            txtOnly.ReadOnly = true;
            FarPoint.Win.Spread.CellType.CheckBoxCellType chkType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.neuSpread1_Sheet1.Columns[1].Visible = false;
            for (int i = 1; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                this.neuSpread1_Sheet1.Columns[i].CellType = txtOnly;
            }
            for (int i = 15; i < this.neuSpread1_Sheet1.ColumnCount; i++)
            {
                this.neuSpread1_Sheet1.Columns[i].Visible = false;
            }
        }
        /// <summary>
        /// ѹ����ʾ
        /// </summary>
        private void lessShow()
        {
            try
            {
                for (int j = 2; j < 5; j++)
                {
                    string startValue = this.neuSpread1_Sheet1.Cells[0, j].Tag.ToString();
                    int startRow = 0; //�����
                    int endRow = 0;

                    for (int i = 1; i < this.neuSpread1_Sheet1.RowCount; i++)
                    {
                        string currentValue = this.neuSpread1_Sheet1.Cells[i, j].Tag.ToString();
                        if (startValue != currentValue)
                        {
                            endRow = i - 1;
                            if (endRow - startRow > 0)
                            {
                                for (int k = startRow + 1; k <= endRow; k++)
                                {
                                    this.neuSpread1_Sheet1.SetValue(k, j, "");
                                }
                                FarPoint.Win.Spread.Model.CellRange cr = new FarPoint.Win.Spread.Model.CellRange(startRow, j, endRow - startRow + 1, 1);
                                this.neuSpread1.ActiveSheet.Models.Span.Add(cr.Row, cr.Column, cr.RowCount, cr.ColumnCount);
                            }
                            startValue = this.neuSpread1_Sheet1.Cells[i, j].Tag.ToString();
                            startRow = i;
                        }
                        //�����β
                        if (i == this.neuSpread1_Sheet1.RowCount - 1)
                        {
                            endRow = i - 1;
                            if (i - startRow > 0) //��ֹһ��
                            {
                                for (int k = startRow + 1; k <= i; k++)
                                {
                                    this.neuSpread1_Sheet1.SetValue(k, j, "");
                                }
                                FarPoint.Win.Spread.Model.CellRange cr = new FarPoint.Win.Spread.Model.CellRange(startRow, j, endRow - startRow + 2, 1);
                                this.neuSpread1.ActiveSheet.Models.Span.Add(cr.Row, cr.Column, cr.RowCount, cr.ColumnCount);
                            }
                            else //���һ�е��У�����Ҫ�ϲ�
                            {
                            }
                        }//end if(i == this.fpSpread1_Sheet1.RowCount-1)
                    }
                }//end private void lessShow()
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message + "����ѹ����ʾ����");
            }
        }
        /// <summary>
        /// ������ע��˳��
        /// </summary>
        /// <returns></returns>
        private int GetMaxInjectOrder()
        {
            if (this.neuSpread1_Sheet1.RowCount <= 0) return 0;
            this.neuSpread1.StopCellEditing();
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetText(i, 0).ToUpper() == "FALSE" ||
                    this.neuSpread1_Sheet1.GetText(i, 0) == "") continue;
                FS.HISFC.Models.Nurse.Inject info =
                    (FS.HISFC.Models.Nurse.Inject)this.neuSpread1_Sheet1.Rows[i].Tag;

                if (FS.FrameWork.Function.NConvert.ToInt32(info.InjectOrder) > maxInjectOrder)
                {
                    maxInjectOrder = FS.FrameWork.Function.NConvert.ToInt32(info.InjectOrder);
                }
            }
            return maxInjectOrder;
        }

        /// <summary>
        /// ��ȡƤ����Ϣ
        /// </summary>
        /// <param name="hytestID"></param>
        /// <returns></returns>
        private string GetHyTestInfo(string hytestID)
        {
            switch (hytestID)
            {
                case "1":
                    return "����";
                case "2":
                    return  "��Ƥ��";
                case "3":
                    return "����";
                case "4":
                    return "����";
                case "0":
                default :
                    return "��";
            }
        }

        #endregion

        #region ��ӡ

        FS.SOC.HISFC.BizProcess.NurseInterface.OutPatient.IInjectBottleLabel IInjectBottleLabel = null;

        /// <summary>
        /// ��ӡ
        /// </summary>
        private void Print()
        {
            printData.Clear();
            ArrayList al = new ArrayList();
            ArrayList alJiePing = new ArrayList();
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE") continue;
                FS.HISFC.Models.Nurse.Inject info =
                    (FS.HISFC.Models.Nurse.Inject)this.neuSpread1_Sheet1.Rows[i].Tag;
                //��ʱ����Ϊ����ƿǩ�ڲ�������б��
                info.User03 = "true";

                //al.Add(info);
                printData.Add(info);
            }
            if (printData == null || printData.Count <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            FS.HISFC.Models.Nurse.Inject inJect;


            //FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint curePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint;
            //if (curePrint == null)
            //{
            //    curePrint = new Nurse.Print.ucPrintCure() as FS.HISFC.BizProcess.Interface.Nurse.IInjectCurePrint;
            //    //Nurse.Print.ucPrintCure uc = new Nurse.Print.ucPrintCure();
            //}
            //curePrint.Init(al);
            ////Nurse.Print.ucPrintCure uc = new Nurse.Print.ucPrintCure();
            ////uc.Init(al);

            if (IInjectBottleLabel == null)
            {
                IInjectBottleLabel = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.BizProcess.NurseInterface.OutPatient.IInjectBottleLabel)) as FS.SOC.HISFC.BizProcess.NurseInterface.OutPatient.IInjectBottleLabel;
            }
            if (IInjectBottleLabel == null)
            {
                MessageBox.Show("��ӡ�ӿ�δʵ�֣��޷���ӡ������ϵ����Ա!", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show("�Ƿ�ȷ�ϲ��򵥾ݣ�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                IInjectBottleLabel.RegInfo = reg;
                IInjectBottleLabel.IsReprint = true;
                IInjectBottleLabel.Print(printData);
            }
        }
        FS.SOC.HISFC.BizProcess.NurseInterface.OutPatient.IInjectInfusionCardPrint IInjectInfusionCardPrint = null;

        /// <summary>
        /// ��ӡ��Һ��
        /// </summary>
        private void PrintInjectNew()
        {
            printData.Clear();
            ArrayList al = new ArrayList();
            ArrayList alJiePing = new ArrayList();
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE") continue;
                FS.HISFC.Models.Nurse.Inject info =
                    (FS.HISFC.Models.Nurse.Inject)this.neuSpread1_Sheet1.Rows[i].Tag;

                printData.Add(info);
            }
            if (printData == null || printData.Count <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }

            if (IInjectInfusionCardPrint == null)
            {
                IInjectInfusionCardPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.BizProcess.NurseInterface.OutPatient.IInjectInfusionCardPrint)) as FS.SOC.HISFC.BizProcess.NurseInterface.OutPatient.IInjectInfusionCardPrint;
            }
            if (IInjectInfusionCardPrint == null)
            {
                MessageBox.Show("��ӡ�ӿ�δʵ�֣��޷���ӡ������ϵ����Ա!", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        
            //�������Ա�Ѳ�����浱�� ������ӡ���棬���������ʾ
            if (MessageBox.Show("�Ƿ�ȷ�ϲ��򵥾ݣ�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                IInjectInfusionCardPrint.RegInfo = reg;
                IInjectInfusionCardPrint.IsReprint = true;
                IInjectInfusionCardPrint.Print(printData);
            }
        }


        /// <summary>
        /// ��ӡע�䵥
        /// </summary>
        private void PrintInject()
        {
            ArrayList al = new ArrayList();
            ArrayList alJiePing = new ArrayList();
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE") continue;
                FS.HISFC.Models.Nurse.Inject info =
                    (FS.HISFC.Models.Nurse.Inject)this.neuSpread1_Sheet1.Rows[i].Tag;

                al.Add(info);
            }
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }

            //{637EDB0D-3F39-4fde-8686-F3CD87B64581} ��ӡ��Ϊ�ӿڷ�ʽ
            FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint injectPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint;
            if (injectPrint == null)
            {
                injectPrint = new Nurse.Print.ucPrintInject() as FS.HISFC.BizProcess.Interface.Nurse.IInjectPrint;
                //Nurse.Print.ucPrintCure uc = new Nurse.Print.ucPrintCure();
            }
            //			if(alJiePing.Count > 0 )
            //			{
            //				al.AddRange(alJiePing);
            //			}

            //�������Ա�Ѳ�����浱�� ������ӡ���棬���������ʾ
            if (MessageBox.Show("�Ƿ�ȷ�ϲ��򵥾ݣ�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                injectPrint.Init(al);
            }
            //Nurse.Print.ucPrintInject uc = new Nurse.Print.ucPrintInject();
            //uc.Init(al);
        }

      
        /// <summary>
        /// ��ӡǩ����
        /// </summary>
        private void PrintItinerate()
        {
            ArrayList al = new ArrayList();
            ArrayList alJiePing = new ArrayList();
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE") continue;
                FS.HISFC.Models.Nurse.Inject info =
                    (FS.HISFC.Models.Nurse.Inject)this.neuSpread1_Sheet1.Rows[i].Tag;

                al.Add(info);
            }
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }

            //{637EDB0D-3F39-4fde-8686-F3CD87B64581} ��ӡ��Ϊ�ӿڷ�ʽ
            FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint itineratePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            if (itineratePrint == null)
            {
                itineratePrint = new Nurse.Print.ucPrintItinerate() as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
                //Nurse.Print.ucPrintItinerate uc = new Nurse.Print.ucPrintItinerate();
            }
            //�������Ա�Ѳ�����浱�� ������ӡ���棬���������ʾ
            if (MessageBox.Show("�Ƿ�ȷ�ϲ��򵥾ݣ�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                itineratePrint.IsReprint = true;
                itineratePrint.Init(al);
            }
            //Nurse.Print.ucPrintItinerate uc = new Nurse.Print.ucPrintItinerate();
            //uc.Init(al);
        }
        /// <summary>
        /// A4ֽ�ŵ�ǩ����
        /// </summary>
        private void PrintItinerateLarge()
        {
            ArrayList al = new ArrayList();
            ArrayList alJiePing = new ArrayList();
            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE") continue;
                FS.HISFC.Models.Nurse.Inject info =
                    (FS.HISFC.Models.Nurse.Inject)this.neuSpread1_Sheet1.Rows[i].Tag;
                FS.HISFC.Models.Order.OutPatient.Order orderinfo =
                    (FS.HISFC.Models.Order.OutPatient.Order)this.neuSpread1_Sheet1.Cells[i, 11].Tag;

                al.Add(info);
            }
            if (al == null || al.Count <= 0)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }

            //{637EDB0D-3F39-4fde-8686-F3CD87B64581} ��ӡ��Ϊ�ӿڷ�ʽ
            FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint itineratePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
            if (itineratePrint == null)
            {
                itineratePrint = new Nurse.Print.ucPrintItinerateLarge() as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
                //Nurse.Print.ucPrintItinerate uc = new Nurse.Print.ucPrintItinerate();
            }
            //			if(alJiePing.Count > 0 )
            //			{
            //				al.AddRange(alJiePing);
            //			}

            //�������Ա�Ѳ�����浱�� ������ӡ���棬���������ʾ
            if (MessageBox.Show("�Ƿ�ȷ�ϲ��򵥾ݣ�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                itineratePrint.IsReprint = true;
                itineratePrint.Init(al);
            }

            //Nurse.Print.ucPrintItinerateLarge uc = new Nurse.Print.ucPrintItinerateLarge();
            //uc.Init(al);
        }


        /// <summary>
        /// ��ӡ��Ѳ�ӿ��ϵ��÷�
        /// </summary>
        private string usage = "iv.drip(�ţ�;";
        /// <summary>
        /// ������Ϣ����
        /// </summary>
        Hashtable hsInfos = new Hashtable();
        /// <summary>
        /// �÷��Ƿ��ӡ��Ѳ�ӿ���
        /// {EE46827D-D081-4aa5-8653-1EF9D176A5DC}
        /// </summary>
        [Description("�÷��Ƿ��ӡ��Ѳ�ӿ��ϣ��ԣ��Ž�β"), Category("����")]
        public string Usage
        {
            get
            {
                return this.usage;
            }
            set
            {
                this.usage = value;
            }
        }

        /// <summary>
        /// ��ȡҪ��ӡ�����ݣ���ά���÷���
        ///{0D976883-8A45-4a97-AFEF-7D8ED425C89A}
        /// </summary>
        /// <returns></returns>
        private int GetAllPrintInjectList()
        {
            this.neuSpread1.StopCellEditing();
            hsInfos.Clear();
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE")
                    continue;
                FS.HISFC.Models.Nurse.Inject info =
                    (FS.HISFC.Models.Nurse.Inject)this.neuSpread1_Sheet1.Rows[i].Tag;
                if (!usage.Contains(info.Item.Order.Usage.Name))
                {
                    continue;
                }

                //info.Patient.Name = reg.Name;
                //info.Patient.Sex.ID = reg.Sex.ID;
                //info.Patient.Birthday = reg.Birthday;
                //info.Patient.Card.ID = this.txtCardNo.Text.Trim().PadLeft(10, '0');
                //info.Item.InjectCount = FS.FrameWork.Function.NConvert.ToInt32(this.neuSpread1_Sheet1.Cells[i, 2].Text);
                //info.OrderNO = this.txtOrder.Text.ToString();
                //info.Item.Order.Combo.ID = this.neuSpread1_Sheet1.Cells[i, 7].Tag.ToString();
                //ҽ������
                //FS.HISFC.Models.Base.Employee ps = this.personMgr.GetEmployeeInfo(info.Item.RecipeOper.ID);
                //if (ps == null)
                //{
                //    MessageBox.Show("�Ҳ������ҽ����Ϣ");
                //    return -1;
                //}
                //info.Item.Order.Doctor.Name = ps.Name;
                //info.Item.Order.Doctor.ID = ps.ID;
                //info.Item.Name = detail.Item.Name;
                if (!hsInfos.ContainsKey(info.Item.Order.ReciptDoctor.ID))
                {
                    ArrayList al = new ArrayList();
                    al.Add(info);
                    hsInfos.Add(info.Item.Order.ReciptDoctor.ID, al);
                }
                else
                {
                    ((ArrayList)hsInfos[info.Item.Order.ReciptDoctor.ID]).Add(info);
                }
            }
            return 1;
        }

        /// <summary>
        /// {E73C3DF3-9DCF-4a46-ADE0-3D44663F6F7A}
        /// ��ӡ������ҺѲ�ӿ�
        /// </summary>
        private void PrintInjectScoutCard()
        {
            int intReturn = this.GetAllPrintInjectList();
            if (intReturn == -1)
            {
                MessageBox.Show("û��ѡ������!");
                return;
            }
            int intCount = 0;
            foreach (ArrayList al in hsInfos.Values)
            {
                FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint itineratePrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint)) as FS.HISFC.BizProcess.Interface.Nurse.IInjectItineratePrint;
                if (itineratePrint == null)
                {
                    return;
                }
                //�������Ա�Ѳ�����浱�� ������ӡ���棬���������ʾ
                if (MessageBox.Show("�Ƿ�ȷ�ϲ��򵥾ݣ�", "ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    itineratePrint.IsReprint = true;
                    itineratePrint.Init(al);
                }
            }
            this.SelectAll(true);
        }
        #endregion

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                HISFC.Models.Account.AccountCard objCard = new FS.HISFC.Models.Account.AccountCard();
                FS.HISFC.BizProcess.Integrate.Fee feeManage = new FS.HISFC.BizProcess.Integrate.Fee();
                if (this.txtCardNo.Text.Trim() == "")
                {
                    MessageBox.Show("�����벡����!", "��ʾ");
                    this.txtCardNo.Focus();
                    return;
                }
                string strCardNO = this.txtCardNo.Text.Trim();//.PadLeft(10, '0');
                int iTemp = feeManage.ValidMarkNO(strCardNO, ref objCard);
                if (iTemp <= 0 || objCard == null)
                {
                    MessageBox.Show("��Ч���ţ�����ϵ����Ա��");
                    return;
                }
                string cardNo = objCard.Patient.PID.CardNO;
                this.txtCardNo.Text = cardNo;
                this.Query();
            }
        }

        private void txtOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Query();
            }
        }

        private void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            this.neuSpread1.StopCellEditing();
            string strTemp = this.neuSpread1_Sheet1.GetValue(e.Row, 0).ToString().ToUpper();
            if (e.Column == 0)
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    //������,˳���,���,�Ǽ�ʱ��
                    if (this.neuSpread1_Sheet1.Cells[i, 2].Tag.ToString() == this.neuSpread1_Sheet1.Cells[e.Row, 2].Tag.ToString()
                        && this.neuSpread1_Sheet1.Cells[i, 3].Tag.ToString() == this.neuSpread1_Sheet1.Cells[e.Row, 3].Tag.ToString()
                        && this.neuSpread1_Sheet1.Cells[i, 8].Tag.ToString() == this.neuSpread1_Sheet1.Cells[e.Row, 8].Tag.ToString()
                        && this.neuSpread1_Sheet1.Cells[i, 8].Tag != null && this.neuSpread1_Sheet1.Cells[i, 8].Tag.ToString() != ""
                        && this.neuSpread1_Sheet1.Cells[i, 14].Tag.ToString() == this.neuSpread1_Sheet1.Cells[e.Row, 14].Tag.ToString())
                    {
                        if (strTemp == "TRUE")
                        {
                            this.neuSpread1_Sheet1.SetValue(i, 0, true, false);
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.SetValue(i, 0, false, false);
                        }
                    }
                }
            }
        }
    }
}
