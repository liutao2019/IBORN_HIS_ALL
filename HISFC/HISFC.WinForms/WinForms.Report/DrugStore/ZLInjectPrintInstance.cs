using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.WinForms.Report.DrugStore
{
    /// <summary>
    /// ����ע�䵥�ݴ�ӡ��
    /// 
    /// <����˵��>
    ///     1���õ���Ŀǰ������ҩ����ӡ
    ///     2���ڸ�����ʵ��ʱ��ͬʱ��ӡ����ע����Һ�嵥������ע����Һ��ǩ
    ///     3���õ��ݸ�ʽ�ο�����ҽԺ��Ŀ�γ�
    /// </����˵��>
    /// </summary>
    public class ZLInjectPrintInstance : FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint
    {
        /// <summary>
        /// 
        /// </summary>
        public ZLInjectPrintInstance()
        {
 
        }

        #region  �����

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.HISFC.Models.Registration.Register patientInfo;

        /// <summary>
        /// ��ǩ��ӡ
        /// </summary>
        private ucCompoundLabel ucLabel = null;

        /// <summary>
        /// ����ע���嵥
        /// </summary>
        private ucZLInjectList ucInject = null;

        /// <summary>
        /// �Ƿ��ӡ���ñ�ǩ
        /// </summary>
        private bool isPrintCompoundLabel = false;

        /// <summary>
        /// ����ӡ���ñ�ǩ
        /// </summary>
        private System.Collections.ArrayList alGroupCompound = new ArrayList();

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ��ӡ���ñ�ǩ
        /// </summary>
        public bool IsPrintCompoundLabel
        {
            get
            {
                return this.isPrintCompoundLabel;
            }
            set
            {
                this.isPrintCompoundLabel = value;
            }
        }

        #endregion

        #region IDrugPrint ��Ա

        public void AddAllData(System.Collections.ArrayList al, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void AddAllData(System.Collections.ArrayList al, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void AddAllData(System.Collections.ArrayList alData)
        {
            if (alData.Count <= 0)
            {
                return;
            }

            #region ������Ϣ��ֵ

            FS.HISFC.Models.Pharmacy.ApplyOut tempApply = alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
            string clinicNO = tempApply.PatientNO;

            FS.HISFC.BizLogic.Registration.Register registerManager = new FS.HISFC.BizLogic.Registration.Register();
            FS.HISFC.Models.Registration.Register register = registerManager.GetByClinic(clinicNO);

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut temp in alData)
            {
                temp.UseTime = temp.Operation.ApplyOper.OperTime;
                temp.PatientNO = register.PID.CardNO;
                temp.User02 = register.Name;
            }

            #endregion

            if (this.ucInject == null)
            {
                this.ucInject = new ucZLInjectList();
            }

            this.ucInject.AddAllData(alData);

            //�Դ�ӡ��ǩ����� �����ݰ���Ϸ���
            ComboSort comboSort = new ComboSort();
            alData.Sort(comboSort);

            ArrayList alGroupApplyOut = new ArrayList();
            ArrayList alCombo = new ArrayList();
            string privCombo = "-1";

            #region ��ǩ��ӡ

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alData)
            {
                if (privCombo == "-1" || (privCombo == info.CombNO && info.CombNO != ""))
                {
                    alCombo.Add(info.Clone());
                    privCombo = info.CombNO;
                    continue;
                }
                else			//��ͬ������
                {
                    alGroupApplyOut.Add(alCombo);

                    privCombo = info.CombNO;
                    alCombo = new ArrayList();

                    alCombo.Add(info.Clone());
                }
            }

            if (alCombo.Count > 0)
            {
                alGroupApplyOut.Add(alCombo);
            }

            this.alGroupCompound = alGroupApplyOut;

            #endregion
        }

        public void AddCombo(System.Collections.ArrayList alCombo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void AddSingle(FS.HISFC.Models.Pharmacy.ApplyOut info)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public decimal DrugTotNum
        {
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public FS.HISFC.Models.RADT.PatientInfo InpatientInfo
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public decimal LabelTotNum
        {
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public FS.HISFC.Models.Registration.Register OutpatientInfo
        {
            get
            {
                return this.patientInfo;
            }
            set
            {
                // TODO:  ��� ucClinicBill.PatientInfo setter ʵ��
                this.patientInfo = value;
                this.Clear();
            }
        }

        public void Preview()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Print()
        {
            this.ucInject.Print();

            if (this.alGroupCompound != null && this.alGroupCompound.Count > 0)
            {
                if (this.ucLabel == null)
                {
                    this.ucLabel = new ucCompoundLabel();
                }

                this.ucLabel.LabelTotNum = this.alGroupCompound.Count;
                this.ucLabel.AddCombo(this.alGroupCompound);
                this.ucLabel.Print();
            }
        }

        #endregion

        /// <summary>
        /// ��������
        /// </summary>
        public void Clear()
        {
            if (this.ucLabel != null)
            {
                this.ucLabel.Clear();
            }
            if (this.ucInject != null)
            {
                this.ucInject.Clear();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected class ComboSort : System.Collections.IComparer
        {
            public ComboSort() { }


            #region IComparer ��Ա

            public int Compare(object x, object y)
            {
                // TODO:  ��� FeeSort.Compare ʵ��
                FS.HISFC.Models.Pharmacy.ApplyOut obj1 = x as FS.HISFC.Models.Pharmacy.ApplyOut;
                FS.HISFC.Models.Pharmacy.ApplyOut obj2 = y as FS.HISFC.Models.Pharmacy.ApplyOut;
                if (obj1 == null || obj2 == null)
                    throw new Exception("�����ڱ���ΪPharmacy.ApplyOut����");
                int i1 = NConvert.ToInt32(obj1.CombNO);
                int i2 = NConvert.ToInt32(obj2.CombNO);
                return i1 - i2;
            }

            #endregion
        }
    }
}
