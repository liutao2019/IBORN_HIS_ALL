using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Equipment
{
    /// <summary>
    /// ���캯��
    /// </summary>
    /// 
    [System.Serializable]
    public class GainReg : Spell, IValid
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public GainReg() 
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����

        /// <summary>
        /// Ч��Ǽ���ˮ��
        /// </summary>
        private string gainRegNO;

        /// <summary>
        /// Ч�������ˮ��
        /// </summary>
        private string gainKindNO;
     
        /// <summary>
        /// Ч��Ǽǵ��ݺ�
        /// </summary>
        private string gainRegListCode;
         
        /// <summary>
        /// �豸������Ϣ
        /// id=�豸���ұ���
        /// name=�豸��������
        /// </summary>
        private FS.FrameWork.Models.NeuObject deptInfo = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��Ƭ��ˮ��
        /// </summary>
        private string seqNO;

        /// <summary>
        /// ���ܿ���
        /// ����
        /// </summary>
        private string storDept;

        /// <summary>
        /// ������
        /// ����
        /// </summary>
        private string storOper;

        /// <summary>
        /// �豸��Ϣ
        /// id=�豸����
        /// name=�豸����
        /// </summary>
        private FS.FrameWork.Models.NeuObject equInfo = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ���
        /// </summary>
        private string specs;

        /// <summary>
        /// �ͺ�
        /// </summary>
        private string model;
    
        /// <summary>
        /// ��λ
        /// </summary>
        private string unit;

        /// <summary>
        /// ���ܵȼ�
        /// </summary>
        private string storClass;

        /// <summary>
        /// ����Ա��Ϣ
        /// ����Ա
        /// ����ʱ��
        /// </summary>
        private OperEnvironment operInfo = new OperEnvironment();

        /// <summary>
        /// ��ע
        /// </summary>
        private string remark;

        /// <summary>
        /// Ч����
        /// </summary>
        private int gainCost;

        /// <summary>
        /// �Ƿ���Ч1��Ч0ͣ��
        /// </summary>
        private string validFlag = "1";
        #endregion

        #region ����
        /// <summary>
        /// Ч��Ǽ���ˮ��
        /// </summary>       
        public string GainRegNO
        {
            get { return gainRegNO; }
            set { gainRegNO = value; }
        }

        /// <summary>
        /// Ч�������ˮ��
        /// </summary>
        public string GainKindNO
        {
            get { return gainKindNO; }
            set { gainKindNO = value; }
        }

        /// <summary>
        /// Ч��Ǽǵ��ݺ�
        /// </summary>
        public string GainRegListCode
        {
            get { return gainRegListCode; }
            set { gainRegListCode = value; }
        }
       
        /// <summary>
        /// �豸������Ϣ
        /// id=�豸���ұ���
        /// name=�豸��������
        /// </summary>
        public FS.FrameWork.Models.NeuObject DeptInfo
        {
            get { return deptInfo; }
            set { deptInfo = value; }
        }

        /// <summary>
        /// ��Ƭ��ˮ��
        /// </summary>
        public string SeqNO
        {
            get { return seqNO; }
            set { seqNO = value; }
        }
        /// <summary>
        /// ���ܿ���
        /// ����
        /// </summary>
        public string StorDept
        {
            get { return storDept; }
            set { storDept = value; }
        }
        /// <summary>
        /// ������
        /// ����
        /// </summary>
        public string StorOper
        {
            get { return storOper; }
            set { storOper = value; }
        }
        /// <summary>
        /// �豸��Ϣ
        /// id=�豸����
        /// name=�豸����
        /// </summary>
        public FS.FrameWork.Models.NeuObject EquInfo
        {
            get { return equInfo; }
            set { equInfo = value; }
        }

        /// <summary>
        /// ���
        /// </summary>
        public string Specs
        {
            get { return specs; }
            set { specs = value; }
        }

        /// <summary>
        /// �ͺ�
        /// </summary>
        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        /// <summary>
        /// ��λ
        /// </summary>
        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        /// <summary>
        /// ���ܵȼ�
        /// </summary>
        public string StorClass
        {
            get { return storClass; }
            set { storClass = value; }
        }

        /// <summary>
        /// ����Ա��Ϣ
        /// ����Ա
        /// ����ʱ��
        /// </summary>
        public OperEnvironment OperInfo
        {
            get { return operInfo; }
            set { operInfo = value; }
        }

        /// <summary>
        /// Ч����
        /// </summary>
        public int GainCost
        {
            get { return gainCost; }
            set { gainCost = value; }
        }

        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }
        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns>���ص�ǰ�����ʵ������</returns>
        public new GainReg Clone()
        {
            GainReg gainReg = base.Clone() as GainReg;

            gainReg.deptInfo = this.deptInfo.Clone();
            gainReg.operInfo = this.operInfo.Clone();
            gainReg.equInfo = this.equInfo.Clone();
            return gainReg;
        }

        #endregion

        #region IValid ��Ա

        public bool IsValid
        {
            get
            {
                if (this.validFlag == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                    this.validFlag = "1";
                else
                    this.validFlag = "0";
            }
        }

        #endregion
    }
}
