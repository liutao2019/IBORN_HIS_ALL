using System;
using System.Collections;
using FS.HISFC;
namespace FS.HISFC.Models.Order
{
    /// <summary>
    /// FS.HISFC.Models.Order.Frequency<br></br>
    /// [��������: ҽ��Ƶ��ʵ��]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2006-09-10]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [Serializable]
    public class Frequency : FS.HISFC.Models.Base.Spell, FS.HISFC.Models.Base.ISort
    {

        public Frequency()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ����

        /// <summary>
        /// �Ƿ���Ĭ�ϵ�Ƶ����
        /// </summary>
        private bool isUseDefaultName = true;

        /// <summary>
        /// ����
        /// </summary>
        private FS.FrameWork.Models.NeuObject dept;
        /// <summary>
        /// �÷�
        /// </summary>
        private FS.FrameWork.Models.NeuObject usage;

        /// <summary>
        /// ����
        /// </summary>
        private int sortID;

        /// <summary>
        /// Ƶ��ִ��ʱ��
        /// 8:00,12:00,16:00
        /// </summary>
        protected string[] strTimes;
        /// <summary>
        /// Ƶ��ִ������or�����
        /// 1
        /// </summary>
        protected string[] strDays;

        /// <summary>
        /// ��õ�ʱ��
        /// </summary>
        protected string strTime;

        /// <summary>
        /// �Ƿ����������ﴦ������
        /// </summary>
        private bool isUseForOutPatient = true;

        /// <summary>
        /// �Ƿ����������ﴦ������
        /// </summary>
        public bool IsUseForOutPatient
        {
            get { return isUseForOutPatient; }
            set { isUseForOutPatient = value; }
        }

        /// <summary>
        /// �Ƿ�������סԺ��������
        /// </summary>
        private bool isUserForInpatientLZ = true;

        /// <summary>
        /// �Ƿ�������סԺ��������
        /// </summary>
        public bool IsUserForInpatientLZ
        {
            get { return isUserForInpatientLZ; }
            set { isUserForInpatientLZ = value; }
        }

        /// <summary>
        /// �Ƿ�������סԺ��������
        /// </summary>
        private bool isUseForInpatientCZ = true;

        /// <summary>
        /// �Ƿ�������סԺ��������
        /// </summary>
        public bool IsUseForInpatientCZ
        {
            get { return isUseForInpatientCZ; }
            set { isUseForInpatientCZ = value; }
        }

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ���Ĭ�ϵ�Ƶ����
        /// </summary>
        public bool IsUseDefaultName
        {
            get
            {
                return this.isUseDefaultName;
            }
            set
            {
                this.isUseDefaultName = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public FS.FrameWork.Models.NeuObject Dept
        {
            get
            {
                if (dept == null)
                {
                    dept = new FS.FrameWork.Models.NeuObject();
                }
                return this.dept;
            }
            set
            {
                this.dept = value;
            }
        }

        /// <summary>
        /// Ƶ��ID
        /// </summary>
        public new string ID
        {
            get
            {
                if (base.ID == null)
                {
                    base.ID = string.Empty;
                }
                return base.ID;
            }
            set
            {
                string str = "";
                //value = value.TrimEnd();
                //value = value.TrimStart();
                value = value.ToUpper();
                if (this.isUseDefaultName)
                {
                    string[] s = value.Split(' ');
                    str = this.f_getName(s[0]);
                    try
                    {
                        if (s.Length > 1) str = str + " " + this.f_getName(s[1]);
                    }
                    catch { }
                    if (str != "")
                    {
                        base.Name = str;
                    }
                }
                base.ID = value;
            }
        }

        /// <summary>
        /// Ƶ��ִ��ʱ��
        /// 8:00,12:00,16:00
        /// </summary>
        public string[] Times
        {
            get
            {
                strTimes = Time.Split('-');
                return strTimes;
            }
        }

        /// <summary>
        /// Ƶ��ִ������or�����
        /// 1
        /// </summary>
        public string[] Days
        {
            get
            {
                if (strDays == null)
                {
                    this.strDays = new string[1];
                    strDays[0] = "1";
                }
                return strDays;
            }
        }

        /// <summary>
        /// ��õ�ʱ��
        /// </summary>
        public string Time
        {
            get
            {
                if (strTime == null || strTime == "") strTime = "8:00";//ʱ���Ϊ�գ�Ĭ��Ϊ8��
                return strTime;
            }
            set
            {
                strTime = value;
            }
        }

        /// <summary>
        /// �÷�
        /// </summary>
        public FS.FrameWork.Models.NeuObject Usage
        {
            get
            {
                if (usage == null)
                {
                    usage = new FS.FrameWork.Models.NeuObject();
                }
                return this.usage;
            }
            set
            {
                this.usage = value;
            }
        }

        #region ���ϵ�

        /// <summary>
        /// ���ȫ���б�
        /// </summary>
        /// <returns>ArrayList(SysClass)</returns>
        [Obsolete("��ʱ������", true)]
        public static ArrayList List()
        {
            Frequency o;
            EnumFrequency e = new EnumFrequency();
            ArrayList alReturn = new ArrayList();
            int i;
            for (i = 0; i <= System.Enum.GetValues(e.GetType()).GetUpperBound(0); i++)
            {
                o = new Frequency();
                o.ID = ((EnumFrequency)i).ToString();
                o.Memo = i.ToString();
                alReturn.Add(o);
            }
            return alReturn;
        }
        #endregion

        #region ����

        /// <summary>
        /// ����ToString
        /// </summary>
        public override string ToString()
        {
            return base.Name;
        }
        #endregion

        protected bool f_IsNumber(string s)
        {
            try
            {
                if (s.ToUpper().IndexOf("A") > 0) return false;
                if (s.ToUpper().IndexOf("B") > 0) return false;
                if (s.ToUpper().IndexOf("C") > 0) return false;
                if (s.ToUpper().IndexOf("D") > 0) return false;
                if (s.ToUpper().IndexOf("E") > 0) return false;
                if (s.ToUpper().IndexOf("F") > 0) return false;
                if (s.ToUpper().IndexOf("G") > 0) return false;
                if (s.ToUpper().IndexOf("H") > 0) return false;
                if (s.ToUpper().IndexOf("I") > 0) return false;
                if (s.ToUpper().IndexOf("J") > 0) return false;
                if (s.ToUpper().IndexOf("K") > 0) return false;
                if (s.ToUpper().IndexOf("L") > 0) return false;
                if (s.ToUpper().IndexOf("M") > 0) return false;
                if (s.ToUpper().IndexOf("N") > 0) return false;
                if (s.ToUpper().IndexOf("O") > 0) return false;
                if (s.ToUpper().IndexOf("P") > 0) return false;
                if (s.ToUpper().IndexOf("Q") > 0) return false;
                if (s.ToUpper().IndexOf("R") > 0) return false;
                if (s.ToUpper().IndexOf("S") > 0) return false;
                if (s.ToUpper().IndexOf("T") > 0) return false;
                if (s.ToUpper().IndexOf("U") > 0) return false;
                if (s.ToUpper().IndexOf("V") > 0) return false;
                if (s.ToUpper().IndexOf("W") > 0) return false;
                if (s.ToUpper().IndexOf("X") > 0) return false;
                if (s.ToUpper().IndexOf("Y") > 0) return false;
                if (s.ToUpper().IndexOf("Z") > 0) return false;
                int i = System.Convert.ToInt16(s, 10);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// ������� {006E13C1-572B-49EE-A9E4-5FF872E99507}�޸����ƴ����ݿ��ȡ
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string f_getName(string s)
        {
            //����Ƶ�α�Ż�ȡ����{A46B15F7-CA11-4E0C-93E1-341AFD679963} ccz��� ע�ͳ���ת��{5194480D-3CBA-4866-877A-8BC7464502F1}
            //return GJLocal.HISFC.Components.OpGuide.DrugStore.Common.Function.GetFrequenceName(s);

            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            string fName = dbMgr.ExecSqlReturnOne("SELECT FREQUENCY_NAME FROM MET_COM_DEPTFREQUENCY WHERE FREQUENCY_CODE ='" + s + "'");


            //{CDA7C373-BDCF-4870-8282-0C801D7E1451}
            //��������Ƶ�εķֽ�����
            #region
            s = s.Replace(".", "");
            string str = "";
            string sInterval = "", sDay = "1";
            int myDays = 0;
            if (s.Length <= 0) return "";
            try
            {
                try
                {
                    if (s.Substring(0, 2) == "QJ")
                    {
                        s = "Q1J" + s.Substring(2);
                    }
                }
                catch { }
                if (s.Substring(0, 1) == "Q")
                {
                    int i = 0;
                    for (i = s.Length - 1; i >= 1; i--)
                    {
                        if (this.f_IsNumber(s.Substring(1, i)))
                        {
                            sInterval = (s.Substring(1, i));
                            i = s.Length - 2 - i;
                            break;
                        }
                        else
                        {

                        }
                    }

                    if (this.f_IsNumber(s.Substring(s.Length - i)))
                    {
                        sDay = (s.Substring(s.Length - i));
                    }
                }
            }
            catch { }
            try
            {
                if (sInterval.Length > sDay.Length)//�Ƚ�˭����˭
                {
                    if (sInterval != "")
                    {
                        s = s.Replace(sInterval, "_integer_");
                    }
                    if (sDay != "")
                    {
                        s = s.Replace(sDay, "_day_");
                    }
                }
                else
                {
                    if (sDay != "")
                        s = s.Replace(sDay, "_day_");
                    if (sInterval != "")
                        s = s.Replace(sInterval, "_integer_");
                }
            }
            catch { }

            try
            {
                if (s.Length >= 2 && s.Substring(s.Length - 2, 2) == "ID")
                {
                    if (this.f_IsNumber(s.Substring(0, s.Length - 2)))
                    {
                        sInterval = (s.Substring(0, s.Length - 2));
                        s = s.Replace(sInterval, "_integer_");
                    }
                }
            }
            catch { }

            try
            {
                switch (s)
                {
                    // һ��
                    case "Once":
                        str = "һ��";
                        this.strDays = new string[1];
                        strDays[0] = "1";
                        break;

                    // �������
                    case "Q_integer_S":
                        str = "���" + sInterval + "��";
                        break;

                    // �������
                    case "Q_integer_M":
                        str = "���" + sInterval + "����";
                        break;

                    // ���Сʱ
                    case "Q_integer_H":
                        str = "���" + sInterval + "Сʱ";
                        break;

                    // �����
                    case "Q_integer_D": str = "���" + sInterval + "��";
                        this.strDays = new string[1];
                        strDays[0] = sInterval;
                        break;

                    // �����
                    case "Q_integer_W": str = "���" + sInterval + "��";
                        this.strDays = new string[1];
                        myDays = int.Parse(sInterval) * 7;
                        strDays[0] = myDays.ToString();
                        break;

                    // �������
                    case "Q_integer_J_day_":
                        str = "���" + sInterval + "�� ����";
                        this.strDays = new string[sDay.Length + 1];
                        myDays = int.Parse(sInterval) * 7;
                        strDays[0] = myDays.ToString();
                        string d = "";
                        for (int i = 0; i < sDay.Length; i++)
                        {
                            d = d + sDay.Substring(i, 1) + ",";
                            strDays[i + 1] = sDay.Substring(i, 1);
                        }
                        str = str + d.Substring(0, d.Length - 1);
                        break;

                    case "Q_day_J_day_":

                        str = "���" + sInterval + "�� ����";
                        this.strDays = new string[sDay.Length + 1];
                        myDays = int.Parse(sInterval) * 7;
                        strDays[0] = myDays.ToString();
                        string d1 = "";
                        for (int i = 0; i < sDay.Length; i++)
                        {
                            d1 = d1 + sDay.Substring(i, 1) + ",";
                            strDays[i + 1] = sDay.Substring(i, 1);
                        }
                        str = str + d1.Substring(0, d1.Length - 1);
                        break;

                    //ÿ��
                    case "QW":
                        str = "���һ��";
                        this.strDays = new string[1];
                        strDays[0] = "7";
                        break;

                    // ÿ��
                    case "QD":
                        str = "ÿ��";
                        this.strDays = new string[1];
                        strDays[0] = "1";
                        break;

                    // ����=Q1D
                    case "QOD":
                        str = "����һ��";
                        this.strDays = new string[1];
                        strDays[0] = "2";
                        break;

                    // ���϶�ʱ
                    case "QAM":
                        str = "���϶�ʱ";
                        break;

                    // ���϶�ʱ
                    case "QPM":
                        str = "���϶�ʱ";
                        break;

                    // ��˯ʱ
                    case "QHS":
                        str = "��˯ʱ";
                        break;

                    // ���ǰ
                    case "ACM":
                        str = "���ǰ";
                        break;

                    // ��ͺ�
                    case "PCM":
                        str = "��ͺ�";
                        break;

                    // �����֮��
                    case "ICM":
                        str = "�����֮��";
                        break;

                    // ���ǰ
                    case "ACD":
                        str = "���ǰ";
                        break;

                    // ��ͺ�
                    case "PCD":
                        str = "��ͺ�";
                        break;

                    // ��ͺ����֮��
                    case "ICD":
                        str = "��ͺ����֮��";
                        break;

                    // ���ǰ
                    case "ACV":
                        str = "���ǰ";
                        break;

                    // ��ͺ�
                    case "PCV":
                        str = "��ͺ�";
                        break;

                    // ��ͺ���˯֮��
                    case "ICV":
                        str = "��ͺ���˯֮��";
                        break;

                    // ������
                    case "BID":
                        str = "������";
                        break;

                    // ������
                    case "TID":
                        str = "������";
                        break;


                    // ���Ĵ�
                    case "QID":
                        str = "���Ĵ�";
                        break;

                    // ��n��
                    case "_integer_ID":
                        str = "��" + sInterval + "��";
                        break;

                    // ��ʱ����
                    case "PRN":
                        str = "��Ҫʱ����";
                        break;

                    // ��������
                    case "ST":
                        str = "����ʹ��";
                        break;
                    //{D4A128C2-9112-402a-A610-DB1937C53A5F}feng.chһЩ���ڹ���֮�ڵ�Ƶ��2010-12-21
                    //ÿ�������1,2,3,4,5
                    case "5/W":
                        str = "ÿ�����(1,2,3,4,5)";
                        this.strDays = new string[6];
                        strDays[0] = "7";
                        strDays[1] = "1";
                        strDays[2] = "2";
                        strDays[3] = "3";
                        strDays[4] = "4";
                        strDays[5] = "5";
                        break;
                    //ÿ�����Σ���һ������
                    case "BIW_day_":
                        str = "ÿ������(1,4)";
                        this.strDays = new string[3];
                        strDays[0] = "7";
                        strDays[1] = "1";
                        strDays[2] = "4";
                        break;
                    //ÿ�����Σ���һ������
                    case "BIW1":
                        str = "ÿ������(1,4)";
                        this.strDays = new string[3];
                        strDays[0] = "7";
                        strDays[1] = "1";
                        strDays[2] = "4";
                        break;
                    //ÿ�����Σ��ܶ�������
                    case "BIW2":
                        str = "ÿ������(2,5)";
                        this.strDays = new string[3];
                        strDays[0] = "7";
                        strDays[1] = "2";
                        strDays[2] = "5";
                        break;
                    //ÿ�����Σ��ܶ�������
                    case "BIW3":
                        str = "ÿ������(3,6)";
                        this.strDays = new string[3];
                        strDays[0] = "7";
                        strDays[1] = "3";
                        strDays[2] = "6";
                        break;
                    //ÿ�����Σ���һ������
                    case "BIW4":
                        str = "ÿ������(1,4)";
                        this.strDays = new string[3];
                        strDays[0] = "7";
                        strDays[1] = "1";
                        strDays[2] = "4";
                        break;
                    //ÿ�����Σ�����������
                    case "BIW5":
                        str = "ÿ������(3,6)";
                        this.strDays = new string[3];
                        strDays[0] = "7";
                        strDays[1] = "3";
                        strDays[2] = "6";
                        break;
                    //ÿ���Ĵ�1,2,3,4
                    case "QIW_day_":
                        str = "ÿ���Ĵ�(1,2,3,4)";
                        this.strDays = new string[5];
                        strDays[0] = "7";
                        strDays[1] = "1";
                        strDays[2] = "2";
                        strDays[3] = "3";
                        strDays[4] = "4";
                        break;
                    //ÿ���Ĵ�1,3,5,7
                    case "QIW2":
                        str = "ÿ���Ĵ�(1,3,5,7)";
                        this.strDays = new string[5];
                        strDays[0] = "7";
                        strDays[1] = "1";
                        strDays[2] = "3";
                        strDays[3] = "5";
                        strDays[4] = "7";
                        break;
                    //ÿ��һ����1
                    case "QW_day_":
                        str = "ÿ��һ��(1)";
                        this.strDays = new string[2];
                        strDays[0] = "7";
                        strDays[1] = "1";
                        break;
                    //ÿ��һ����2
                    case "QW2":
                        str = "ÿ��һ��(2)";
                        this.strDays = new string[2];
                        strDays[0] = "7";
                        strDays[1] = "2";
                        break;
                    //ÿ��һ����3
                    case "QW3":
                        str = "ÿ��һ��(3)";
                        this.strDays = new string[2];
                        strDays[0] = "7";
                        strDays[1] = "3";
                        break;
                    //ÿ��һ����4
                    case "QW4":
                        str = "ÿ��һ��(4)";
                        this.strDays = new string[2];
                        strDays[0] = "7";
                        strDays[1] = "4";
                        break;
                    //ÿ��һ����5
                    case "QW5":
                        str = "ÿ��һ��(5)";
                        this.strDays = new string[2];
                        strDays[0] = "7";
                        strDays[1] = "5";
                        break;
                    //ÿ��һ����6
                    case "QW6":
                        str = "ÿ��һ��(6)";
                        this.strDays = new string[2];
                        strDays[0] = "7";
                        strDays[1] = "6";
                        break;
                    //ÿ��һ����4
                    case "QW7":
                        str = "ÿ��һ��(7)";
                        this.strDays = new string[2];
                        strDays[0] = "7";
                        strDays[1] = "7";
                        break;
                    case "TIW_day_":
                        str = "ÿ������(1,3,5)";
                        this.strDays = new string[4];
                        strDays[0] = "7";
                        strDays[1] = "1";
                        strDays[2] = "3";
                        strDays[3] = "5";
                        break;
                    //ÿ��������1,3,5
                    case "TIW1":
                        str = "ÿ������(1,3,5)";
                        this.strDays = new string[4];
                        strDays[0] = "7";
                        strDays[1] = "1";
                        strDays[2] = "3";
                        strDays[3] = "5";
                        break;
                    //ÿ��������2,4,6
                    case "TIW2":
                        str = "ÿ������(2,4,6)";
                        this.strDays = new string[4];
                        strDays[0] = "7";
                        strDays[1] = "2";
                        strDays[2] = "4";
                        strDays[3] = "6";
                        break;
                    //ÿ��������2,5,7
                    case "TIW3":
                        str = "ÿ������(2,5,7)";
                        this.strDays = new string[4];
                        strDays[0] = "7";
                        strDays[1] = "2";
                        strDays[2] = "5";
                        strDays[3] = "7";
                        break;
                    //���ߴ�
                    case "SID": str = "���ߴ�"; break;
                }
            }
            catch
            {
            }
            //ֻ�ж����ڻ�ȡ strDays���飬����Ҫ����Ƶ������
            //str = str.Replace("���1��", "ÿ��");
            //return str;
            #endregion

            return fName;
        }

        #region ��¡

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new Frequency Clone()
        {
            Frequency obj = base.Clone() as Frequency;
            obj.dept = this.Dept.Clone();
            return obj;
        }

        #endregion

        #endregion

        #region �ӿ�ʵ��

        #region ISort ��Ա

        /// <summary>
        /// ����
        /// </summary>
        public int SortID
        {
            get
            {
                // TODO:  ��� Frequency.SortID getter ʵ��
                return this.sortID;
            }
            set
            {
                // TODO:  ��� Frequency.SortID setter ʵ��
                this.sortID = value;
            }
        }

        #endregion

        #endregion
    }


    public enum EnumFrequency
    {
        /// <summary>
        /// һ��
        /// </summary>
        Once,
        /// <summary>
        /// �������
        /// </summary>
        Q_integer_S,
        /// <summary>
        /// �������
        /// </summary>
        Q_integer_M,
        /// <summary>
        /// ���Сʱ
        /// </summary>
        Q_integer_H,
        /// <summary>
        /// �����
        /// </summary>
        Q_integer_D,
        /// <summary>
        /// �����
        /// </summary>
        Q_integer_W,
        /// <summary>
        /// �������
        /// </summary>
        Q_integer_J_day_,
        /// <summary>
        /// ÿ��
        /// </summary>
        QD,
        /// <summary>
        /// ����=Q1D
        /// </summary>
        QOD,
        /// <summary>
        /// ���϶�ʱ
        /// </summary>
        QAM,
        /// <summary>
        /// ���϶�ʱ
        /// </summary>
        QPM,
        /// <summary>
        /// ��˯ʱ
        /// </summary>
        QHS,
        /// <summary>
        /// ���ǰ
        /// </summary>
        ACM,
        /// <summary>
        /// ��ͺ�
        /// </summary>
        PCM,
        /// <summary>
        /// �����֮��
        /// </summary>
        ICM,
        /// <summary>
        /// ���ǰ
        /// </summary>
        ACD,
        /// <summary>
        /// ��ͺ�
        /// </summary>
        PCD,
        /// <summary>
        /// ��ͺ����֮��
        /// </summary>
        ICD,
        /// <summary>
        /// ���ǰ
        /// </summary>
        ACV,
        /// <summary>
        /// ��ͺ�
        /// </summary>
        PCV,
        /// <summary>
        /// ��ͺ���˯֮��
        /// </summary>
        ICV,
        /// <summary>
        /// ������
        /// </summary>
        BID,
        /// <summary>
        /// ������
        /// </summary>
        TID,
        /// <summary>
        /// ���Ĵ�
        /// </summary>
        QID,
        /// <summary>
        /// ��n��
        /// </summary>
        _integer_ID,
        /// <summary>
        /// ��Ҫʱ����
        /// </summary>
        PRN
    }
}
