using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.DrugStore.Inpatient
{
    /// <summary>
    /// <br></br>
    /// [��������: ��ҩ���һ�����ʾ�ؼ�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11]<br></br>
    /// </summary>
    public partial class ucDrugMessage : ucDrugBase,FS.HISFC.BizProcess.Interface.Pharmacy.IInpatientDrug
    {
        public ucDrugMessage()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                try
                {
                    this.Init();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("��ʼ��ʧ��! " + ex.Message);
                }
            }
        }

        #region �����

        /// <summary>
        /// �������ʱ �Ƿ��Զ�ѡ��
        /// </summary>
        private bool autoCheck = false;

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper deptHelper = null;

        /// <summary>
        /// ҩƷ��������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ҩ������������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.DrugStore drugstoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

        #endregion

        #region ����

        /// <summary>
        /// �������ʱ �Ƿ��Զ�ѡ��
        /// </summary>
        [Description("���������ʱ �Ƿ��Զ�ѡ�и���"),Category("����"),DefaultValue(false)]
        public bool AutoCheck
        {
            get
            {
                return this.autoCheck;
            }
            set
            {
                this.autoCheck = value;
            }
        }

        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected virtual void Init()
        {
            if (deptHelper == null)
            {
                FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
                ArrayList al = deptManager.GetDeptmentAll();
                if (al == null)
                {
                    MessageBox.Show(Language.Msg("��ȡ�����б�������") + deptManager.Err);
                    return;
                }

                this.deptHelper = new FS.FrameWork.Public.ObjectHelper();
            }

            this.InitControlParam();
        }

        /// <summary>
        /// ���Ʋ�����ʼ��
        /// </summary>
        private void InitControlParam()
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            this.AutoCheck = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.InDrug_Auto_Check, true, false);
        }

        /// <summary>
        /// ��ʾ��ҩ֪ͨ��Ϣ
        /// </summary>
        /// <param name="drugMessage">��ҩ֪ͨ����</param>
        private void AddDataToFp(FS.HISFC.Models.Pharmacy.DrugMessage drugMessage)
        {
            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);
            
            int iIndex = this.neuSpread1_Sheet1.Rows.Count - 1;

            if (drugMessage.ApplyDept.Name == "")
                drugMessage.ApplyDept.Name = this.deptHelper.GetName(drugMessage.ApplyDept.ID);

            if (this.autoCheck)
                this.neuSpread1_Sheet1.Cells[iIndex, (int)ColumnSet.ColCheck].Value = true;
            this.neuSpread1_Sheet1.Cells[iIndex, (int)ColumnSet.ColPrintType].Value = "��ӡ";
            this.neuSpread1_Sheet1.Cells[iIndex, (int)ColumnSet.ColSendDept].Value = drugMessage.ApplyDept.Name;
            this.neuSpread1_Sheet1.Cells[iIndex, (int)ColumnSet.ColBillType].Value = drugMessage.DrugBillClass.Name;
            this.neuSpread1_Sheet1.Cells[iIndex, (int)ColumnSet.ColSendTime].Value = drugMessage.SendTime.ToString();
            this.neuSpread1_Sheet1.Cells[iIndex, (int)ColumnSet.ColSendOper].Value = drugMessage.Name;
            this.neuSpread1_Sheet1.Rows[iIndex].Tag = drugMessage;
        }

        /// <summary>
        /// ����������Ϣ��ʾ
        /// </summary>
        /// <param name="deptFirst">�б���ʾʱ �Ƿ��տ�����ʾ</param>
        /// <param name="arrayDrugMessage">��ҩ֪ͨ��Ϣ</param>
        /// <returns>���سɹ�����1 �������󷵻�-1</returns>
        public int ShowData(bool deptFirst,ArrayList arrayDrugMessage)
        {
            //��ձ���е�����
            this.Clear();
            try
            {
                string privDeptCode = "";
                string privBillCode = "";               
                foreach (FS.HISFC.Models.Pharmacy.DrugMessage drugMessage in arrayDrugMessage)
                {
                    if (deptFirst)              //���տ�����ʾ
                    {
                        if (drugMessage.DrugBillClass.ID != privBillCode)
                        {
                            this.AddDataToFp(drugMessage);
                            privBillCode = drugMessage.DrugBillClass.ID;
                        }
                    }
                    else                       //���յ�����ʾ
                    {
                        if (drugMessage.ApplyDept.ID != privDeptCode)
                        {
                            this.AddDataToFp(drugMessage);
                            privDeptCode = drugMessage.ApplyDept.ID;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("��������������Ϣ��ʾʱ��������") + ex.Message);
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ��ȡ��ǰ�û�ѡ�е�����
        /// </summary>
        private ArrayList GetCheckData()
        {
            ArrayList al = new ArrayList();
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColCheck].Value))
                    al.Add(this.neuSpread1_Sheet1.Rows[i].Tag);
            }

            return al;
        }

        /// <summary>
        /// ��ȡ����Ա�� ѡ���ӡ/Ԥ��
        /// </summary>
        /// <returns>��ҪԤ������True ֱ�Ӵ�ӡ����False</returns>
        private bool IsSelectPreview(int iIndex)
        {
            if (this.neuSpread1_Sheet1.Cells[iIndex, 1].Text == "��ӡ")
                return false;
            else
                return true;
        }

        /// <summary>
        /// ��ӡ/Ԥ��
        /// </summary>
        /// <param name="isPreview">�Ƿ�Ԥ��</param>
        private void Print(bool isPreview)
        {
            ArrayList alCheckData = this.GetCheckData();
            int i = 0;
            foreach (FS.HISFC.Models.Pharmacy.DrugMessage message in alCheckData)
            {
                ArrayList al = this.itemManager.QueryApplyOutList(message);
                if (al == null)
                {
                    MessageBox.Show(Language.Msg("���ݰ�ҩ֪ͨ��Ϣ��ȡ��ҩ������ϸ��Ϣ�������� ") + this.itemManager.Err);
                    return;
                }

                FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass = this.drugstoreManager.GetDrugBillClass(message.DrugBillClass.ID);
                drugBillClass.Memo = message.DrugBillClass.Memo;//��ҩ����

                Function.Print(al,drugBillClass,false, this.IsPrintLabel, this.IsSelectPreview(i));
                i++;
            }
        }

        #region IInpatientDrug ��Ա

        /// <summary>
        /// ����ǰ
        /// </summary>
        public event EventHandler BeginSaveEvent;

        /// <summary>
        /// �����
        /// </summary>
        public event EventHandler EndSaveEvent;

        /// <summary>
        /// ѡ��ȫ������
        /// </summary>
        public void CheckAll()
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                this.neuSpread1_Sheet1.Cells[i, 0].Value = true;
            }
        }

        /// <summary>
        /// ��ѡ���κ�����
        /// </summary>
        public void CheckNone()
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                this.neuSpread1_Sheet1.Cells[i, 0].Value = false;
            }
        }

        /// <summary>
        /// ��ձ���е�����
        /// </summary>
        public void Clear()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;

            //��հ�ҩ����ʾ
            Function.IDrugPrint.AddAllData(new ArrayList());
        }

        /// <summary>
        /// ��ҩ����
        /// </summary>
        /// <param name="drugMessage">��ҩ֪ͨ��Ϣ</param>
        public int Save(FS.HISFC.Models.Pharmacy.DrugMessage drugMessage)
        {
            if (this.neuSpread1_Sheet1.Rows.Count == 0)
            {
                MessageBox.Show(Language.Msg("û�п��Ժ�׼�����ݡ�"));
                return -1;
            }

            if (this.BeginSaveEvent != null)
                this.BeginSaveEvent(drugMessage, null);

            ArrayList alCheckData = this.GetCheckData();
            if (alCheckData.Count <= 0)
            {
                MessageBox.Show(Language.Msg("��ѡ��Ҫ��׼�����ݡ�"));
                return -1;
            }
            int iIndex = 0;
            foreach (FS.HISFC.Models.Pharmacy.DrugMessage message in alCheckData)
            {
                #region ��ѡ�е��������ݽ��б���

                message.SendFlag = 1;                     //��ҩ֪ͨ�е�����ȫ������׼SendFlag=1�����°�ҩ֪ͨ��Ϣ��
                message.SendType = drugMessage.SendType; //����˰�ҩ֪ͨ�еİ�ҩ��������ʱ��ȡ��ҩ̨�ķ������͡�

                //�������Ұ�ҩ������ϸ����
                ArrayList al = this.itemManager.QueryApplyOutList(message);
                if (al == null)
                {
                    MessageBox.Show(Language.Msg("���ݰ�ҩ֪ͨ��Ϣ��ȡ��ҩ������ϸ��Ϣ�������� ") + this.itemManager.Err);
                    return -1;
                }
                if (message.DrugBillClass.ID == "R")
                {
                    if (DrugStore.Function.DrugReturnConfirm(al, message,this.ArkDept,this.ApproveDept) != 1)
                        return -1;
                }
                else
                {
                    if (DrugStore.Function.DrugConfirm(al, message,this.ArkDept,this.ApproveDept) != 1)
                        return -1;
                }

                FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass = this.drugstoreManager.GetDrugBillClass(message.DrugBillClass.ID);

                //�����ҩ����
                drugBillClass.Memo = message.DrugBillClass.Memo;//��ҩ����
                drugBillClass.DrugBillNO = message.DrugBillClass.Memo;

                Function.Print(al,drugBillClass,this.IsAutoPrint,this.IsPrintLabel,this.IsSelectPreview(iIndex));
                iIndex++;

                #endregion
            }
            if (this.EndSaveEvent != null)
                this.EndSaveEvent(drugMessage, null);
            return 1;
        }

        /// <summary>
        /// ����������ʾ
        /// </summary>
        /// <param name="alApplyOut">������������</param>
        public void ShowData(ArrayList alApplyOut)
        {
            this.ShowData(this.IsDeptFirst, alApplyOut);
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        public void Print()
        {
            this.Print(false);
        }

        /// <summary>
        /// ��ӡԤ��
        /// </summary>
        public void Preview()
        {
            this.Print(true);
        }

        #endregion

        /// <summary>
        /// ������
        /// </summary>
        public enum ColumnSet
        {
            /// <summary>
            /// ѡ��
            /// </summary>
            ColCheck = 0,
            /// <summary>
            /// ��ӡ����
            /// </summary>
            ColPrintType = 1,
            /// <summary>
            /// ���Ϳ���
            /// </summary>
            ColSendDept,
            /// <summary>
            /// ��ҩ������
            /// </summary>
            ColBillType,
            /// <summary>
            /// ����ʱ��
            /// </summary>
            ColSendTime,
            /// <summary>
            /// ������
            /// </summary>
            ColSendOper
        }
    }

    
}
