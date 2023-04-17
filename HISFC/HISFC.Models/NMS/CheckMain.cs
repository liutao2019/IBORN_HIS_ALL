using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.HISFC.Models.NMS
{
    /// <summary>
    /// [���������������ʱ����˼�¼����ʵ��]
    /// [�� �� �ߣ�����]
    /// [����ʱ�䣺2008-10-7]
    /// [ID:�ʱ�������ˮ�� MEMO:��ע]
    /// </summary>
    [System.Serializable]
    public class CheckMain:Neusoft.FrameWork.Models.NeuObject
    {
        #region ���캯��

        /// <summary>
        /// ���캯��[ID:�ʱ�������ˮ�� MEMO:��ע]
        /// </summary>
        public CheckMain()
        {
        }

        #endregion 

        #region ����

        /// <summary>
        /// ģ����Ϣ(ģ������ˮ�ţ�ģ�����ƣ�ģ�帱���⣬ģ���ѯ�룬ģ������)
        /// </summary>
        private ModelMain modelMain = new ModelMain();

        /// <summary>
        /// �������״̬1����2�鿴3���
        /// </summary>
        private string nuState;

        /// <summary>
        /// �ʿؿ����״̬1����2�鿴3���
        /// </summary>
        private string quState;

        /// <summary>
        /// �Ƿ���Ч1��Ч0ͣ��
        /// </summary>
        private bool validFlag;

        ///// <summary>
        ///// ��ͷ˵��
        ///// </summary>
        //private string headerMark;

        ///// <summary>
        ///// ҳ��˵��
        ///// </summary>
        //private string footerMark;

        /// <summary>
        /// �״�¼��ʱ��
        /// </summary>
        private DateTime firstDate;

        /// <summary>
        /// �����˻���(�����ˣ�����ʱ��)
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment itemOper = new Neusoft.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// �������(��������ˣ��������ʱ�䣬����������)
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment nuApp = new Neusoft.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// �ʿؿ����(�ʿؿ�����ˣ��ʿؿ����ʱ�䣬�ʿؿ�������)
        /// </summary>
        private Neusoft.HISFC.Models.Base.OperEnvironment qcApp = new Neusoft.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ������/�ʱ���
        /// </summary>
        private string checkOper;

        /// <summary>
        /// ������/�ʱ�����
        /// </summary>
        private string checkWard;

        /// <summary>
        /// ������/�ʱ�����
        /// </summary>
        private string checkDept;

        /// <summary>
        /// ��������(Ĭ����������)
        /// </summary>
        private DateTime checkDate;

        /// <summary>
        /// ���˼���
        /// </summary>
        private string checkLevel;

        /// <summary>
        /// �������
        /// </summary>
        private string checkType;

        /// <summary>
        /// ������Ա����
        /// </summary>
        private string operType;

        /// <summary>
        /// ������/�ʱ���Ա����
        /// </summary>
        private string checkOperType;

        #endregion

        #region ����

        /// <summary>
        /// ģ����Ϣ��ģ������ˮ�ţ�ģ�����ƣ�ģ�帱���⣬ģ���ѯ�룬ģ�����ͣ�
        /// </summary>
        public ModelMain ModelMain
        {
            get
            {
                return modelMain;
            }
            set
            {
                modelMain = value;
            }
        }

        /// <summary>
        /// �������״̬1����2�鿴3���
        /// </summary>
        public string NuState
        {
            get
            {
                return nuState;
            }
            set
            {
                nuState = value;
            }
        }

        /// <summary>
        /// �ʿؿ����״̬1����2�鿴3���
        /// </summary>
        public string QuState
        {
            get
            {
                return quState;
            }
            set
            {
                quState = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ч1��Ч0ͣ��
        /// </summary>
        public bool ValidFlag
        {
            get
            {
                return validFlag;
            }
            set
            {
                validFlag = value;
            }
        }

        ///// <summary>
        ///// ��ͷ˵��
        ///// </summary>
        //public string HeaderMark
        //{
        //    get
        //    {
        //        return headerMark;
        //    }
        //    set
        //    {
        //        headerMark = value;
        //    }
        //}

        ///// <summary>
        ///// ҳ��˵��
        ///// </summary>
        //public string FooterMark
        //{
        //    get
        //    {
        //        return footerMark;
        //    }
        //    set
        //    {
        //        footerMark = value;
        //    }
        //}

        /// <summary>
        /// �״�¼��ʱ��
        /// </summary>
        public DateTime FirstDate
        {
            get
            {
                return firstDate;
            }
            set
            {
                firstDate = value;
            }
        }

        /// <summary>
        /// �����˻���(�����ˣ�����ʱ��)
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment ItemOper
        {
            get
            {
                return itemOper;
            }
            set
            {
                itemOper = value;
            }
        }

        /// <summary>
        /// �������(��������ˣ��������ʱ�䣬����������)
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment NuApp
        {
            get
            {
                return nuApp;
            }
            set
            {
                nuApp = value;
            }
        }

        /// <summary>
        /// �ʿؿ����(�ʿؿ�����ˣ��ʿؿ����ʱ�䣬�ʿؿ�������)
        /// </summary>
        public Neusoft.HISFC.Models.Base.OperEnvironment QcApp
        {
            get
            {
                return qcApp;
            }
            set
            {
                qcApp = value;
            }
        }

        /// <summary>
        /// ������/�ʱ���
        /// </summary>
        public string CheckOper
        {
            get
            {
                return checkOper;
            }
            set
            {
                checkOper = value;
            }
        }

        /// <summary>
        /// ������/�ʱ�����
        /// </summary>
        public string CheckWard
        {
            get
            {
                return checkWard;
            }
            set
            {
                checkWard = value;
            }
        }

        /// <summary>
        /// ������/�ʱ�����
        /// </summary>
        public string CheckDept
        {
            get
            {
                return checkDept;
            }
            set
            {
                checkDept = value;
            }
        }

        /// <summary>
        /// ��������(Ĭ����������)
        /// </summary>
        public DateTime CheckDate
        {
            get
            {
                return checkDate;
            }
            set
            {
                checkDate = value;
            }
        }

        /// <summary>
        /// ���˼���
        /// </summary>
        public string CheckLevel
        {
            get
            {
                return checkLevel;
            }
            set
            {
                checkLevel = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public string CheckType
        {
            get
            {
                return checkType;
            }
            set
            {
                checkType = value;
            }
        }

        /// <summary>
        /// ������Ա����
        /// </summary>
        public string OperType
        {
            get
            {
                return operType;
            }
            set
            {
                operType = value;
            }
        }

        /// <summary>
        /// ������/�ʱ���Ա����
        /// </summary>
        public string CheckOperType
        {
            get
            {
                return checkOperType;
            }
            set
            {
                checkOperType = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns></returns>
        public new CheckMain Clone()
        {
            CheckMain checkmain=base.Clone() as CheckMain;
            checkmain.ModelMain = this.modelMain.Clone();
            checkmain.ItemOper = this.itemOper.Clone();
            return checkmain;
        }

        #endregion

    }
}
