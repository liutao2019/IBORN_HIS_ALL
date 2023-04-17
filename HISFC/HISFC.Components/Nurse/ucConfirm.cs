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
    public partial class ucConfirm : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucConfirm()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// ������ù�����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee patientMgr = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// Ժע������
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Inject InjMgr = new FS.HISFC.BizLogic.Nurse.Inject();
        private FS.HISFC.BizProcess.Integrate.Pharmacy drugMgr = new FS.HISFC.BizProcess.Integrate.Pharmacy();
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        private FS.HISFC.BizProcess.Integrate.Manager DeptMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizProcess.Integrate.Manager PsMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizProcess.Integrate.Manager Constant = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.Models.Registration.Register reg = null;

        /// <summary>
        /// ��������
        /// </summary>
        private Hashtable htSamples = new Hashtable();
        /// <summary>
        /// ҽ������
        /// </summary>
        private Hashtable htDoctors = new Hashtable();
        private ArrayList al = new ArrayList();
        private ArrayList alConstant = new ArrayList();
        private string formSet;
        //{B89590FC-406F-4ff8-9CFB-C71E990827A1}
        private bool isSave = false;
        #endregion

        #region ��ʼ

        private void Init()
        {
            this.InitDoctor();
            this.InitCareList();
        }
        /// <summary>
        /// ��ʼ��ҽ��
        /// </summary>
        private void InitDoctor()
        {
            FS.HISFC.BizProcess.Integrate.Manager doctMgr = new FS.HISFC.BizProcess.Integrate.Manager();

            al = doctMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (al != null)
            {
                foreach (FS.HISFC.Models.Base.Employee p in al)
                {
                    this.htDoctors.Add(p.ID, p.Name);
                }
            }
        }

        private void InitCareList()
        {
            ArrayList alConstant = Constant.QueryConstantList("CLININCON");
        }

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("�б�", "", FS.FrameWork.WinForms.Classes.EnumImageList.MĬ��, true, false, null);
            this.toolBarService.AddToolButton("ȫѡ", "", FS.FrameWork.WinForms.Classes.EnumImageList.Qȫѡ, true, false, null);
            this.toolBarService.AddToolButton("ȡ��", "", FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);

            return this.toolBarService;
        }

        protected override int OnSave(object sender, object neuObject)
        {
            //{B89590FC-406F-4ff8-9CFB-C71E990827A1}
            this.isSave = true;
            this.Save();
            return 1;            
        }
        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.txtCardNo.Text.Trim() == "" || this.txtCardNo.Text.Trim() == null)
            {
                this.QueryAll();
            }
            else
            {
                this.Query();
            }
            return 1;
        }
        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();
            return 1;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "�б�":
                    this.SetQueryBar();
                    break;
                case "ȫѡ":
                    this.SelectAll(true);
                    break;
                case "ȡ��":
                    this.SelectAll(false);
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region ����
        /// <summary>
        /// ȷ�ϱ���
        /// </summary>
        private int Save()
        {
            this.neuSpread1.StopCellEditing();
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
            {
                MessageBox.Show("û��Ҫ���������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
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
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //SQLCA.BeginTransaction();

            try
            {
                this.InjMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                DateTime confirmDate = this.InjMgr.GetDateTimeFromSysDateTime();

                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    FS.HISFC.Models.Nurse.Inject info =
                        (FS.HISFC.Models.Nurse.Inject)this.neuSpread1_Sheet1.Rows[i].Tag;

                    info.ID = this.neuSpread1_Sheet1.GetValue(i, 1).ToString();

                    //��ҩ��Ϣ
                    info.MixOperInfo.ID = this.neuSpread1_Sheet1.Cells[i, 13].Tag.ToString();
                    info.MixOperInfo.Name = this.neuSpread1_Sheet1.Cells[i, 13].Text;
                    info.MixTime = this.InjMgr.GetDateTimeFromSysDateTime();

                    //ע����Ϣ
                    info.InjectOperInfo.ID = this.neuSpread1_Sheet1.Cells[i, 14].Tag.ToString();
                    info.InjectOperInfo.Name = this.neuSpread1_Sheet1.Cells[i, 14].Text;
                    info.InjectTime = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.GetValue(i, 15));
                    string strSpeed = this.neuSpread1_Sheet1.Cells[i, 16].Text;
                    if (strSpeed == null || strSpeed == "") strSpeed = "0";
                    info.InjectSpeed = FS.FrameWork.Function.NConvert.ToInt32(strSpeed);

                    //������Ϣ
                    if (this.neuSpread1_Sheet1.Cells[i, 17].Tag != null)
                    {
                        info.StopOper.ID = this.neuSpread1_Sheet1.Cells[i, 17].Tag.ToString();
                    }
                    info.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.GetValue(i, 18));
                    info.SendemcTime = FS.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.GetValue(i, 19));
                    info.Memo = this.neuSpread1_Sheet1.Cells[i, 20].Text;

                    //��ȷ��ʱ����Ļ���Сֵ
                    if (this.neuSpread1_Sheet1.GetValue(i, 0).ToString().ToUpper() == "FALSE")
                    {
                        info.MixTime = System.DateTime.MinValue;
                        info.InjectTime = System.DateTime.MinValue;
                        info.EndTime = System.DateTime.MinValue;
                    }
                    if (this.formSet == "��ҩ")
                    {
                        if (this.InjMgr.UpdateMix(info) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.InjMgr.Err, "��ʾ");
                            return -1;
                        }
                    }
                    if (this.formSet == "ע��")
                    {
                        if (this.InjMgr.UpdateInject(info) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.InjMgr.Err, "��ʾ");
                            return -1;
                        }
                    }
                    if (this.formSet == "����")
                    {
                        if (this.InjMgr.UpdateStop(info) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(this.InjMgr.Err, "��ʾ");
                            return -1;
                        }
                    }
                }

                FS.FrameWork.Management.PublicTrans.Commit();

            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(e.Message, "��ʾ");
                return -1;
            }

            MessageBox.Show("����ɹ�!", "��ʾ");
            
            this.QueryAll();
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        private void Clear()
        {
            if (this.neuSpread1_Sheet1.RowCount > 0)
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
        }
        /// <summary>
        /// ��ѯ
        /// </summary>
        private void Query()
        {
            string cardNo;
            cardNo = this.txtCardNo.Text.Trim().PadLeft(10, '0');
            string orderNo;
            orderNo = this.txtOrder.Text.Trim().ToString();
            bool IsFound = false;
            if (cardNo != null && cardNo != "0000000000" && cardNo != "")
            {
                //���ղ����Ų�ѯ��λ
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    if (this.neuSpread1_Sheet1.Cells[i, 3].Tag.ToString() == cardNo)
                    {
                        this.neuSpread1.Focus();
                        this.neuSpread1_Sheet1.AddSelection(i, 0, 1, 1);

                        this.SetPatient(i);
                        IsFound = true;
                        break;
                    }
                }
                if (!IsFound)
                {
                    MessageBox.Show("û����Ҫȷ�ϵ�ҽ����Ϣ!", "��ʾ");
                    this.txtCardNo.Focus();
                    return;
                }
            }
            else if (orderNo != null && orderNo != "")
            {
                //���ն���˳��Ų�ѯ��λ
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    if (this.neuSpread1_Sheet1.GetValue(i, 2).ToString() == orderNo)
                    {
                        this.neuSpread1.Focus();
                        this.neuSpread1_Sheet1.AddSelection(i, 0, 1, 1);

                        this.SetPatient(i);
                        IsFound = true;
                        break;
                    }
                }
                if (!IsFound)
                {
                    MessageBox.Show("û����Ҫȷ�ϵ�ҽ����Ϣ!", "��ʾ");
                    this.txtOrder.Focus();
                    return;
                }
            }
        }
        /// <summary>
        /// ��ѯ
        /// </summary>
        private void QueryAll()
        {
            this.Clear();
            string str = "all";
            //��ȡ������Ŀ

            al = this.InjMgr.QueryAll(str,
                FS.FrameWork.Function.NConvert.ToDateTime(dtpExec.Value.ToString("yyyy-MM-dd 00:00:00")));
            ArrayList alConfirm = new ArrayList();
            foreach (FS.HISFC.Models.Nurse.Inject detail in al)
            {
                if (this.funcation == Funcations.��ҩ && (detail.MixOperInfo.ID == "" || detail.MixOperInfo.ID == null))
                {
                    alConfirm.Add(detail);
                }
                //{14EECF9B-FEA3-4bd4-92C5-91E1FE7F7171} 
                else if (this.funcation == Funcations.ע�� && (detail.InjectOperInfo.ID == "" || detail.InjectOperInfo.ID == null) && !(detail.MixOperInfo.ID == "" || detail.MixOperInfo.ID == null))
                {
                    alConfirm.Add(detail);
                }
                //{14EECF9B-FEA3-4bd4-92C5-91E1FE7F7171}
                else if (this.funcation == Funcations.���� && (detail.StopOper.ID == "" || detail.StopOper.ID == null) && !(detail.InjectOperInfo.ID == "" || detail.InjectOperInfo.ID == null) && !(detail.MixOperInfo.ID == "" || detail.MixOperInfo.ID == null))
                {
                    alConfirm.Add(detail);
                }
            }
            if (al == null || al.Count == 0)
            {
                MessageBox.Show("û����Ҫȷ�ϵ�ҽ����Ϣ!", "��ʾ");
                this.txtCardNo.Focus();
                return;
            }
            this.AddDetail(alConfirm);
            //this.AddDetail(al);
            if (this.neuSpread1_Sheet1.RowCount == 0)
            {

                #region {B89590FC-406F-4ff8-9CFB-C71E990827A1}

                if (!isSave)
                {
                    MessageBox.Show("��ʱ�����û�л�����Ϣ!", "��ʾ");
                }
                else
                {
                    isSave = false;
                }

                #endregion

                //MessageBox.Show("��ʱ�����û�л�����Ϣ!", "��ʾ");
                this.txtCardNo.Focus();
                return;
            }

            this.SetComb();
            this.LessShow();
            this.SetFP();
            this.txtOrder.Focus();
            //this.Query();
        }
        /// <summary>
        /// ������Ϻ�
        /// </summary>
        private void SetComb()
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
                string currentRowExecDate = this.neuSpread1_Sheet1.Cells[i, 6].Tag.ToString();
                string currentRowCombNo = this.neuSpread1_Sheet1.Cells[i, 8].Tag.ToString();
                string priorRowExecDate = this.neuSpread1_Sheet1.Cells[prior, 6].Tag.ToString();
                string priorRowCombNo = this.neuSpread1_Sheet1.Cells[prior, 8].Tag.ToString();
                string nextRowExecDate = this.neuSpread1_Sheet1.Cells[next, 6].Tag.ToString();
                string nextRowCombNo = this.neuSpread1_Sheet1.Cells[next, 8].Tag.ToString();

                #region ""
                bool bl1 = true;
                bool bl2 = true;
                if (currentRowExecDate != priorRowExecDate || currentRowCombNo != priorRowCombNo)
                    bl1 = false;
                if (currentRowExecDate != nextRowExecDate || currentRowCombNo != nextRowCombNo)
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

        /// <summary>
        /// �����Ŀ��ϸ
        /// </summary>
        /// <param name="detail"></param>
        private void AddDetail(ArrayList details)
        {
            if (this.neuSpread1_Sheet1.RowCount > 0) this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);

            if (details != null)
            {
                foreach (FS.HISFC.Models.Nurse.Inject detail in details)
                {
                    this.AddDetail(detail);
                }
            }
        }

        /// <summary>
        /// �����ϸ
        /// </summary>
        /// <param name="detail"></param>
        private void AddDetail(FS.HISFC.Models.Nurse.Inject info)
        {
            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
            int row = this.neuSpread1_Sheet1.RowCount - 1;
            this.neuSpread1_Sheet1.Rows[row].Tag = info;

            #region "���ڸ�ֵ"
            #region ��� Ƥ�� ʱ��
            //���
            string strNoon = this.GetNoon(info.Booker.OperTime);
            if (info.Item.Order.Frequency.ID == "QD")
            {
                strNoon = "ȫ��";
            }
            //Ƥ��
            string strTest = "��";
            if (info.Hypotest == FS.HISFC.Models.Order.EnumHypoTest.NeedHypoTest)
            {
                strTest = "��";
            }
            if (info.InjectOperInfo.ID == null || info.InjectOperInfo.ID == "")
            {
                info.InjectTime = this.InjMgr.GetDateTimeFromSysDateTime();
            }
            if (info.StopOper.ID == null || info.StopOper.ID == "")
            {
                info.EndTime = this.InjMgr.GetDateTimeFromSysDateTime();
            }
            #endregion
            if (this.formSet == "��ҩ")
            {
                if (info.MixOperInfo.ID == "")
                {
                    this.neuSpread1_Sheet1.SetValue(row, 0, false);
                }
                else
                {
                    this.neuSpread1_Sheet1.SetValue(row, 0, true);
                }
            }
            if (this.formSet == "ע��")
            {
                if (info.InjectOperInfo.ID == "")
                {
                    this.neuSpread1_Sheet1.SetValue(row, 0, false);
                }
                else
                {
                    this.neuSpread1_Sheet1.SetValue(row, 0, true);
                }
            }
            if (this.formSet == "����")
            {
                if (info.StopOper.ID == "")
                {
                    this.neuSpread1_Sheet1.SetValue(row, 0, false);
                }
                else
                {
                    this.neuSpread1_Sheet1.SetValue(row, 0, true);
                }
            }
            this.neuSpread1_Sheet1.SetValue(row, 1, info.ID, false);//��ˮ��
            this.neuSpread1_Sheet1.SetValue(row, 2, info.OrderNO, false);//����˳���
            this.neuSpread1_Sheet1.Cells[row, 2].Tag = info.OrderNO.ToString();//
            this.neuSpread1_Sheet1.SetValue(row, 3, info.Patient.Name.ToString());//��������
            this.neuSpread1_Sheet1.Cells[row, 3].Tag = info.Patient.PID.CardNO.ToString();//������
            this.neuSpread1_Sheet1.SetValue(row, 4, this.GetDoctByID(info.Item.Order.ReciptDoctor.ID), false);//����ҽ��
            this.neuSpread1_Sheet1.Cells[row, 4].Tag = info.Item.Order.ReciptDoctor.ID.ToString();
            this.neuSpread1_Sheet1.SetValue(row, 5, info.Item.Order.DoctorDept.Name, false);//�Ʊ�
            this.neuSpread1_Sheet1.Cells[row, 5].Tag = info.Item.Order.DoctorDept.Name.ToString();
            this.neuSpread1_Sheet1.SetValue(row, 6, strNoon, false);//���
            this.neuSpread1_Sheet1.Cells[row, 6].Tag = info.Booker.OperTime.ToString();
            this.neuSpread1_Sheet1.SetValue(row, 7, info.Item.Name, false);//ҩƷ����
            this.neuSpread1_Sheet1.Cells[row, 8].Tag = info.Item.Order.Combo.ID.ToString();//��Ϻ�
            this.neuSpread1_Sheet1.SetValue(row, 9, info.Item.Order.DoseOnce.ToString() + info.Item.Order.DoseUnit, false);//ÿ����
            this.neuSpread1_Sheet1.SetValue(row, 10, info.Item.Order.Frequency.ID, false);//Ƶ��
            this.neuSpread1_Sheet1.SetValue(row, 11, info.Item.Order.Usage.Name, false);//�÷�
            this.neuSpread1_Sheet1.SetValue(row, 12, strTest, false);//Ƥ�ԣ�
            this.neuSpread1_Sheet1.SetValue(row, 13, info.MixOperInfo.Name, false);//��ҩ��
            this.neuSpread1_Sheet1.Cells[row, 13].Tag = info.MixOperInfo.ID.ToString();//��ҩ�˴���
            this.neuSpread1_Sheet1.SetValue(row, 14, info.InjectOperInfo.Name, false);//ע����
            this.neuSpread1_Sheet1.Cells[row, 14].Tag = info.InjectOperInfo.ID.ToString();//ע���˴���
            this.neuSpread1_Sheet1.SetValue(row, 15, info.InjectTime, false);//ע������
            this.neuSpread1_Sheet1.SetValue(row, 16, info.InjectSpeed, false);//����
            this.neuSpread1_Sheet1.Cells[row, 17].Tag = info.StopOper.ID;//���뻤ʿ����
            if (info.StopOper.ID != null && info.StopOper.ID != "")
            {
                this.neuSpread1_Sheet1.SetValue(row, 17, this.GetNameByOperID(info.StopOper.ID), false);//������
            }
            this.neuSpread1_Sheet1.SetValue(row, 18, info.EndTime, false);//����ʱ��
            if (info.SendemcTime == DateTime.MinValue)
            {
                this.neuSpread1_Sheet1.SetValue(row, 19, null, false);//�ͼ���ʱ��
            }
            else
            {
                this.neuSpread1_Sheet1.SetValue(row, 19, info.SendemcTime, false);//�ͼ���ʱ��
            }
            this.neuSpread1_Sheet1.SetValue(row, 20, info.Memo, false);//��ע
            #endregion
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        private void Print()
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.PrintPage(0, 0, this.neuSpread1);
        }

        /// <summary>
        /// �б�
        /// </summary>
        private void SetQueryBar()
        {
            if (!this.panel2.Visible)
            {
                this.panel2.Visible = true;
            }
            else
            {
                this.panel2.Visible = false;
            }
        }

        /// <summary>
        /// ȫѡ
        /// </summary>
        private void SelectAll(bool isSelected)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                this.neuSpread1_Sheet1.SetValue(i, 0, isSelected, false);
                int n = 13;
                if (this.formSet == "ע��")
                {
                    n++;
                }
                if (this.formSet == "����")
                {
                    n = n + 4;
                }
                if (isSelected)
                {
                    if (this.neuSpread1_Sheet1.Cells[i, n].Text == null
                        || this.neuSpread1_Sheet1.Cells[i, n].Text == "")
                    {
                        this.neuSpread1_Sheet1.SetValue(i, n, FS.FrameWork.Management.Connection.Operator.Name /*var.User.Name.ToString()*/);
                        this.neuSpread1_Sheet1.Cells[i, n].Tag = FS.FrameWork.Management.Connection.Operator.ID /*var.User.ID.ToString();*/;
                    }
                }
                else
                {
                    if (this.neuSpread1_Sheet1.Cells[i, n].Text == FS.FrameWork.Management.Connection.Operator.Name /*var.User.Name.ToString()*/)
                    {
                        this.neuSpread1_Sheet1.SetValue(i, n, "");
                        this.neuSpread1_Sheet1.Cells[i, n].Tag = "";
                    }
                }
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ���ݴ����ȡҽ������
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private string GetDoctByID(string ID)
        {
            IDictionaryEnumerator dict = htDoctors.GetEnumerator();
            while (dict.MoveNext())
            {
                if (dict.Key.ToString() == ID)
                    return dict.Value.ToString();
            }
            return "";
        }

        /// <summary>
        /// ������Ա�����ȡ��Ա��Ϣ
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private string GetNameByOperID(string ID)
        {
            FS.HISFC.Models.Base.Employee ps = new FS.HISFC.Models.Base.Employee();
            ps = this.PsMgr.GetEmployeeInfo(ID);
            if (ps == null)
            {
                MessageBox.Show("��ȡ��Ա��Ϣ����!");
            }
            return ps.Name;
        }

        /// <summary>
        /// ���ø�ʽ
        /// </summary>
        private void SetFP()
        {
            FarPoint.Win.Spread.CellType.TextCellType txtOnly = new FarPoint.Win.Spread.CellType.TextCellType();
            txtOnly.ReadOnly = true;
            FarPoint.Win.Spread.CellType.TextCellType txt = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType dateType = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            dateType.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.ShortDateWithTime;


            this.neuSpread1_Sheet1.Columns[1].Visible = false;
            this.neuSpread1_Sheet1.Columns[2].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[3].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[4].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[5].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[6].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[7].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[8].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[9].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[10].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[11].CellType = txtOnly;

            this.neuSpread1_Sheet1.Columns[13].CellType = txtOnly;//��ҩ��ʿ
            this.neuSpread1_Sheet1.Columns[14].CellType = txtOnly;//ע�令ʿ
            this.neuSpread1_Sheet1.Columns[17].CellType = txtOnly;//���뻤ʿ
            if (this.formSet == "��ҩ")  //��ҩȷ��
            {
                this.neuSpread1_Sheet1.Columns[14].Visible = false;//ע�令ʿ
                this.neuSpread1_Sheet1.Columns[15].Visible = false;//ע��ʱ��
                this.neuSpread1_Sheet1.Columns[17].Visible = false;//������
                this.neuSpread1_Sheet1.Columns[18].Visible = false;//����ʱ��
                this.neuSpread1_Sheet1.Columns[19].Visible = false;//�ͼ���ʱ��
            }
            if (this.formSet == "ע��")  //ע��ȷ��
            {
                this.neuSpread1_Sheet1.Columns[13].Visible = false;//��ҩ��ʿ
                this.neuSpread1_Sheet1.Columns[17].Visible = false;//������
                this.neuSpread1_Sheet1.Columns[18].Visible = false;//����ʱ��
            }
            if (this.formSet == "����")  //����ȷ��
            {
                this.neuSpread1_Sheet1.Columns[13].Visible = false;//��ҩ��ʿ
                this.neuSpread1_Sheet1.Columns[14].Visible = false;//ע�令ʿ
                this.neuSpread1_Sheet1.Columns[15].Visible = false;//ע��ʱ��
                this.neuSpread1_Sheet1.Columns[19].Visible = false;//�ͼ���ʱ��
            }
            this.neuSpread1_Sheet1.Columns[9].Visible = false;//
            this.neuSpread1_Sheet1.Columns[10].Visible = false;//
            this.neuSpread1_Sheet1.Columns[11].Visible = false;//
            this.neuSpread1_Sheet1.Columns[12].Visible = false;//
            this.neuSpread1_Sheet1.Columns[16].Visible = false;//����
        }

        /// <summary>
        /// ��ȡ���
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string GetNoon(DateTime dt)
        {
            string strNoon = "����";
            if (FS.FrameWork.Function.NConvert.ToInt32(dt.ToString("HH")) >= 12)
            {
                strNoon = "����";
            }
            return strNoon;
        }

        /// <summary>
        /// ѹ����ʾ
        /// </summary>
        private void LessShow()
        {
            //���������ѹ����ʾ��ʱ�򣬿��԰�����ͬʱѹ�����Ͳ�����ѭ����
            if (this.neuSpread1_Sheet1.RowCount < 1) return;
            for (int j = 2; j < 4; j++)
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
                        //1.����ѹ���ϲ�
                        if (endRow - startRow > 0)
                        {
                            for (int k = startRow + 1; k <= endRow; k++)
                            {
                                this.neuSpread1_Sheet1.SetValue(k, j, "");
                            }
                            FarPoint.Win.Spread.Model.CellRange cr = new FarPoint.Win.Spread.Model.CellRange(startRow, j, endRow - startRow + 1, 1);
                            this.neuSpread1.ActiveSheet.Models.Span.Add(cr.Row, cr.Column, cr.RowCount, cr.ColumnCount);
                        }
                        //2.������ɫ
                        this.neuSpread1.StopCellEditing();
                        for (int m = startRow; m <= endRow; m++)
                        {
                            if (this.neuSpread1_Sheet1.Rows[startRow - 1].BackColor != System.Drawing.Color.White)
                            {
                                this.neuSpread1_Sheet1.Rows[m].BackColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                this.neuSpread1_Sheet1.Rows[m].BackColor = System.Drawing.Color.Linen;
                            }
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
                        //2.������ɫ
                        this.neuSpread1.StopCellEditing();
                        for (int m = startRow; m <= i; m++)
                        {
                            if (this.neuSpread1_Sheet1.Rows[startRow - 1].BackColor != System.Drawing.Color.White)
                            {
                                this.neuSpread1_Sheet1.Rows[m].BackColor = System.Drawing.Color.White;
                            }
                            else
                            {
                                this.neuSpread1_Sheet1.Rows[m].BackColor = System.Drawing.Color.Linen;
                            }
                        }
                    }//end if(i == this.fpSpread1_Sheet1.RowCount-1)
                }
            }//end private void lessShow()
        }

        /// <summary>
        /// ���ò�����Ϣ
        /// </summary>
        /// <param name="rowno"></param>
        private void SetPatient(int rowno)
        {
            this.neuSpread1.StopCellEditing();
            if (!this.neuSpread1.Focused || rowno < 0)
            {
                this.txtName.Text = "";
                this.txtSex.Text = "";
                this.txtAge.Text = "";
                this.txtBirthday.Text = "";
                return;
            }
            //���ò�����Ϣ
            FS.HISFC.Models.Nurse.Inject info
                = (FS.HISFC.Models.Nurse.Inject)this.neuSpread1_Sheet1.Rows[rowno].Tag;

            if (info == null)
            {
                this.txtName.Text = "";
                this.txtSex.Text = "";
                this.txtAge.Text = "";
                this.txtBirthday.Text = "";
                return;
            }
            this.txtName.Text = info.Patient.Name;

            //FS.HISFC.Models.RADT.Sex se = new FS.HISFC.Models.RADT.Sex();
            //se.ID = info.Patient.Sex.ID;
            this.txtSex.Text = info.Patient.Sex.Name;
            //this.txtAge.Text = this.conMgr.GetAge(info.Patient.Birthday, info.Booker.OperTime);
            this.txtAge.Text = this.InjMgr.GetAge(info.Patient.Birthday, info.Booker.OperTime);
            this.txtBirthday.Text = info.Patient.Birthday.ToString();
        }

        #endregion

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txtCardNo.Text.Trim() == "")
                {
                    MessageBox.Show("�����벡����!", "��ʾ");
                    this.txtCardNo.Focus();
                    return;
                }
                this.Query();
            }
        }

        private void txtOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txtOrder.Text.Trim() == "")
                {
                    MessageBox.Show("������˳���!", "��ʾ");
                    this.txtOrder.Focus();
                    return;
                }
                this.txtCardNo.Text = "";
                this.Query();
            }
        }

        private void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            try
            {
                //ͬ��ĵ������ͬʱ��
                this.neuSpread1.StopCellEditing();
                if (!this.neuSpread1.Focused || e.Row < 0) return;
                string strTemp = this.neuSpread1_Sheet1.GetValue(e.Row, 0).ToString().ToUpper();
                if (e.Column == 0)
                {
                    //��ҩ
                    int n = 13;
                    //ע��
                    if (this.formSet == "ע��")
                    {
                        n++;
                    }
                    //����
                    if (this.formSet == "����")
                    {
                        n = n + 4;
                    }
                    for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                    {
                        //������,˳���,���,���
                        if (this.neuSpread1_Sheet1.Cells[i, 2].Tag.ToString() == this.neuSpread1_Sheet1.Cells[e.Row, 2].Tag.ToString()
                            && this.neuSpread1_Sheet1.Cells[i, 3].Tag.ToString() == this.neuSpread1_Sheet1.Cells[e.Row, 3].Tag.ToString()
                            && this.neuSpread1_Sheet1.Cells[i, 6].Tag.ToString() == this.neuSpread1_Sheet1.Cells[e.Row, 6].Tag.ToString()
                            && this.neuSpread1_Sheet1.Cells[i, 8].Tag.ToString() == this.neuSpread1_Sheet1.Cells[e.Row, 8].Tag.ToString()
                            && this.neuSpread1_Sheet1.Cells[i, 8].Tag.ToString() != null && this.neuSpread1_Sheet1.Cells[i, 8].Tag.ToString() != ""
                            && i != e.Row)
                        {

                            if (strTemp == "TRUE")
                            {
                                this.neuSpread1_Sheet1.SetValue(i, 0, true, false);
                                if (this.neuSpread1_Sheet1.Cells[i, n].Text == null
                                    || this.neuSpread1_Sheet1.Cells[i, n].Text == "")
                                {
                                    this.neuSpread1_Sheet1.SetValue(i, n, ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Name /*var.User.Name.ToString()*/);
                                    this.neuSpread1_Sheet1.Cells[i, n].Tag = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).ID; /*var.User.ID.ToString();*/
                                }

                            }
                            else
                            {
                                this.neuSpread1_Sheet1.SetValue(i, 0, false, false);
                                if (this.neuSpread1_Sheet1.GetValue(i, n).ToString() == ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Name /*var.User.Name.ToString()*/)
                                {
                                    this.neuSpread1_Sheet1.SetValue(i, n, "");
                                    this.neuSpread1_Sheet1.Cells[i, n].Tag = "";
                                }
                            }

                        }//end if
                    }//end for()
                }
                //��ȷ����
                this.neuSpread1.StopCellEditing();
                if (e.Column == 0)
                {
                    int n = 13;
                    if (this.formSet == "ע��")
                    {
                        n++;
                    }
                    if (this.formSet == "����")
                    {
                        n = n + 4;
                    }
                    if (this.neuSpread1_Sheet1.GetValue(e.Row, 0).ToString().ToUpper() == "TRUE")
                    {
                        if (this.neuSpread1_Sheet1.Cells[e.Row, n].Text == null
                            || this.neuSpread1_Sheet1.Cells[e.Row, n].Text == "")
                        {
                            this.neuSpread1_Sheet1.SetValue(e.Row, n, ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Name /*var.User.Name.ToString()*/);
                            this.neuSpread1_Sheet1.Cells[e.Row, n].Tag = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).ID; /*var.User.ID.ToString();*/
                        }
                    }
                    else
                    {
                        if (this.neuSpread1_Sheet1.Cells[e.Row, n].Text == ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Name /*var.User.Name.ToString()*/)
                        {
                            this.neuSpread1_Sheet1.SetValue(e.Row, n, "");
                            this.neuSpread1_Sheet1.Cells[e.Row, n].Tag = "";
                        }

                    }
                    this.neuSpread1_Sheet1.SetActiveCell(e.Row, 1);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "1");
                return;
            }
        }

        private void neuSpread1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.neuSpread1_Sheet1.ActiveColumnIndex == 19)
            {
                if (e.KeyData == Keys.Space)
                {
                    FS.FrameWork.Models.NeuObject neuObj = new FS.FrameWork.Models.NeuObject();
                    /* ���������[2007/03/10]
                    if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(Constant.GetList("CLININCON"), ref neuObj) == 1)
                    {
                        this.neuSpread1_Sheet1.SetValue(this.neuSpread1_Sheet1.ActiveRowIndex,
                            this.neuSpread1_Sheet1.ActiveColumnIndex, neuObj.Name);
                    }
                    */

                }
            }
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column == 20)
            {
                /* ����ط�������[2007/03/10]
                FS.FrameWork.Models.NeuObject neuObj = new FS.FrameWork.Models.NeuObject();
                if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(Constant.GetList("CLININCON"), ref neuObj) == 1)
                {
                    this.neuSpread1_Sheet1.SetValue(e.Row, e.Column, neuObj.Name);
                }
                 * 
                 */
            }
        }

        //private void neuSpread1_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        //{
        //    //this.SetPatient(e.NewRow);
        //}

        protected override bool ProcessDialogKey(Keys keyData)
        {
            int altKey = Keys.Alt.GetHashCode();

            if (keyData == Keys.F1)
            {
                this.SelectAll(true);
                return true;
            }
            if (keyData == Keys.F2)
            {
                this.SelectAll(false);
                return true;
            }
            if (keyData == Keys.F12)
            {
                this.SetQueryBar();
                return true;
            }
            if (keyData.GetHashCode() == altKey + Keys.S.GetHashCode())
            {
                if (this.Save() == 0)
                { }
                return true;
            }
            if (keyData.GetHashCode() == altKey + Keys.Q.GetHashCode())
            {
                this.Query();
                return true;
            }
            if (keyData.GetHashCode() == altKey + Keys.P.GetHashCode())
            {
                //
                return true;
            }
            if (keyData.GetHashCode() == altKey + Keys.X.GetHashCode())
            {
                this.FindForm().Close();
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void ucConfirm_Load(object sender, EventArgs e)
        {
            if (this.funcation.ToString() != null)
                this.formSet = this.funcation.ToString();
            
            this.Text = this.formSet + "ȷ��";
            this.Init();
            this.SetFP();
            this.QueryAll();
            //{03E7916F-5AA8-4e95-BBE2-61EB6FDEB96C} ��Һ����
            this.SetAlterControl();
        }

        /// <summary>
        /// ��ҩ��ʾ
        /// {03E7916F-5AA8-4e95-BBE2-61EB6FDEB96C}
        /// </summary>
        private void SetAlterControl()
        {
            if (this.funcation != Funcations.��ҩ)
            {
                return;
            }
            this.ucDosageAlter1.Visible = true;
            this.ucDosageAlter1.QueryDate = this.dtpExec.Value.Date;
            this.ucDosageAlter1.Init();
            
        }

        private Funcations funcation = Funcations.��ҩ;

        public Funcations Funcation 
        {
            get 
            {
                return this.funcation;
            }
            set 
            {
                this.funcation = value;
            }
        }
         
        public enum Funcations 
        {
            ��ҩ = 0,

            ע��,

            ����
        }

        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader) 
            {
                return;
            }
            
            this.SetPatient(e.Row);
        }
    }
}