using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Equipment
{
    #region �豸��Ŀ��Ϣʵ��

    /// <summary>
    /// Company<br></br>
    /// [��������: �豸��Ŀ��Ϣʵ�壨�豸�ֵ䣩]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2007-10-30]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    /// 
    [System.Serializable]
    public class EquipBase : FS.HISFC.Models.Base.Spell
    {
        /// <summary>
        /// ���캯��

        /// </summary>
        public EquipBase()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����

        /// <summary>
        /// �豸��������
        /// </summary>
        private NeuObject dept = new NeuObject();

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private Kind kind = new Kind();

        /// <summary>
        /// ���
        /// </summary>
        private string specs;

        /// <summary>
        /// ��λ
        /// </summary>
        private NeuObject unit = new NeuObject();

        /// <summary>
        /// ���µ���
        /// </summary>
        private decimal currPrice;

        /// <summary>
        /// ������
        /// </summary>
        private string nationCode;

        /// <summary>
        /// �豸����������1�豸2���3�̶��ʲ�
        /// </summary>
        private NeuObject equType = new NeuObject();

        /// <summary>
        /// �������豸����
        /// </summary>
        private string leadCode;

        /// <summary>
        /// �Ƿ���Ҫ�Ǽ�
        /// </summary>
        private bool isReg;

        /// <summary>
        /// �Ƿ��۾�
        /// </summary>
        private bool isDep;

        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        private bool isValid;

        /// <summary>
        /// �۾ɷ�ʽ
        /// </summary>
        private NeuObject deType = new NeuObject();

        /// <summary>
        /// �۾�����
        /// </summary>
        private decimal deYear;

        /// <summary>
        /// �Ƿ����
        /// </summary>
        private bool isGauge;

        /// <summary>
        /// �Ƿ񸽼��豸
        /// </summary>
        private bool isAppend;

        /// <summary>
        /// �����豸����
        /// </summary>
        private NeuObject chargeType = new NeuObject();

        /// <summary>
        /// ���ܵȼ�
        /// </summary>
        private NeuObject storClass = new NeuObject();

        /// <summary>
        /// �������
        /// </summary>
        private long mostNum;

        /// <summary>
        /// �������
        /// </summary>
        private long lowestNum;

        /// <summary>
        /// ˳���
        /// </summary>
        private long orderCode;

        /// <summary>
        /// Ʒ������
        /// </summary>
        private string brandName;

        /// <summary>
        /// Ӣ������
        /// </summary>
        private string englishName;

        /// <summary>
        /// �Ƿ�����1��0��
        /// </summary>
        private bool isSelf;

        /// <summary>
        /// ������������
        /// </summary>
        private NeuObject gaugeType = new NeuObject();

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();

        #endregion

        #region ����

        /// <summary>
        /// �豸��������
        /// </summary>
        public NeuObject Dept
        {
            get { return dept; }
            set { dept = value; }
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public Kind Kind
        {
            get { return kind; }
            set { kind = value; }
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
        /// ��λ
        /// </summary>
        public NeuObject Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        /// <summary>
        /// ���µ���
        /// </summary>
        public decimal CurrPrice
        {
            get { return currPrice; }
            set { currPrice = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string NationCode
        {
            get { return nationCode; }
            set { nationCode = value; }
        }

        /// <summary>
        /// �豸����������1�豸2���3�̶��ʲ�
        /// </summary>
        public NeuObject EquType
        {
            get { return equType; }
            set { equType = value; }
        }

        /// <summary>
        /// �������豸����
        /// </summary>
        public string LeadCode
        {
            get { return leadCode; }
            set { leadCode = value; }
        }

        /// <summary>
        /// �Ƿ���Ҫ�Ǽ�
        /// </summary>
        public bool IsReg
        {
            get { return isReg; }
            set { isReg = value; }
        }

        /// <summary>
        /// �Ƿ��۾�
        /// </summary>
        public bool IsDep
        {
            get { return isDep; }
            set { isDep = value; }
        }

        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }

        /// <summary>
        /// �۾ɷ�ʽ
        /// </summary>
        public NeuObject DeType
        {
            get { return deType; }
            set { deType = value; }
        }

        /// <summary>
        /// �۾�����
        /// </summary>
        public decimal DeYear
        {
            get { return deYear; }
            set { deYear = value; }
        }

        /// <summary>
        /// �Ƿ����
        /// </summary>
        public bool IsGauge
        {
            get { return isGauge; }
            set { isGauge = value; }
        }

        /// <summary>
        /// �Ƿ񸽼��豸
        /// </summary>
        public bool IsAppend
        {
            get { return isAppend; }
            set { isAppend = value; }
        }

        /// <summary>
        /// �����豸����
        /// </summary>
        public NeuObject ChargeType
        {
            get { return chargeType; }
            set { chargeType = value; }
        }

        /// <summary>
        /// ���ܵȼ�
        /// </summary>
        public NeuObject StorClass
        {
            get { return storClass; }
            set { storClass = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public long MostNum
        {
            get { return mostNum; }
            set { mostNum = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public long LowestNum
        {
            get { return lowestNum; }
            set { lowestNum = value; }
        }

        /// <summary>
        /// ˳���
        /// </summary>
        public long OrderCode
        {
            get { return orderCode; }
            set { orderCode = value; }
        }

        /// <summary>
        /// Ʒ������
        /// </summary>
        public string BrandName
        {
            get { return brandName; }
            set { brandName = value; }
        }

        /// <summary>
        /// Ӣ������
        /// </summary>
        public string EnglishName
        {
            get { return englishName; }
            set { englishName = value; }
        }

        /// <summary>
        /// �Ƿ�����1��0��
        /// </summary>
        public bool IsSelf
        {
            get { return isSelf; }
            set { isSelf = value; }
        }

        /// <summary>
        /// ������������
        /// </summary>
        public NeuObject GaugeType
        {
            get { return gaugeType; }
            set { gaugeType = value; }
        }

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment Oper
        {
            get { return oper; }
            set { oper = value; }
        }

        #endregion

        #region ����

        /// <summary>
        /// ������¡
        /// </summary>
        /// <returns>�ɹ����ؿ�¡���EquipBaseʵ�� ʧ�ܷ���null</returns>
        public new EquipBase Clone()
        {
            EquipBase equipBase = base.Clone() as EquipBase;

            equipBase.dept = this.dept.Clone();
            equipBase.kind = this.kind.Clone();
            equipBase.unit = this.unit.Clone();
            equipBase.equType = this.equType.Clone();
            equipBase.deType = this.deType.Clone();
            equipBase.chargeType = this.chargeType.Clone();
            equipBase.storClass = this.storClass.Clone();
            equipBase.gaugeType = this.gaugeType.Clone();
            equipBase.oper = this.oper.Clone();

            return equipBase;
        }

        #endregion
    }

    #endregion
}
