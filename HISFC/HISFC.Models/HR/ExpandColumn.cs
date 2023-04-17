using System;
using System.Collections.Generic;
using System.Text;
using Neusoft.HISFC.Models.Base;

namespace Neusoft.HISFC.Models.HR
{
    /// <summary>
    ///<br>ExpandColumn</br>
    ///<br> [��������: ����չʵ��]</br>
    ///<br> [�� �� ��: �εº�]</br>
    ///<br>[����ʱ��: 2008-09-26]</br>
    ///    <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    [System.Serializable]
    public class ExpandColumn : Neusoft.FrameWork.Models.NeuObject, Base.ISort, Base.IValid
    {

        #region �ֶ�
        /// <summary>
        /// �Ƿ����Ϊ��
        /// </summary>
        private bool isNull = true;

        /// <summary>
        /// ����
        /// </summary>
        private string tableName = string.Empty;

        /// <summary>
        /// �ֶ�����
        /// </summary>
        private string columnName = string.Empty;

        /// <summary>
        /// �ֶ�����
        /// </summary>
        private string columnType = string.Empty;

        /// <summary>
        /// �г���
        /// </summary>
        private int columnLength = 0;


        /// <summary>
        /// С������
        /// </summary>
        private int columnDecimalLen = 0;


        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        private string defaultValue = string.Empty;

        /// <summary>
        /// ��ע
        /// </summary>
        private string remark = string.Empty;


        /// <summary>
        /// ��Ч��
        /// </summary>
        private bool isValid = true;

        /// <summary>
        /// ���
        /// </summary>
        private int sortID = 0;

        /// <summary>
        /// ������Ϣ
        /// </summary>				
        private OperEnvironment oper = new OperEnvironment();
        #endregion

        #region ����

        /// <summary>
        /// �Ƿ������ֵ
        /// </summary>
        public bool IsNull
        {
            get
            {
                return isNull;
            }
            set
            {
                isNull = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string TableName
        {
            get
            {
                return tableName;
            }
            set
            {
                tableName = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string ColumnName
        {
            get
            {
                return columnName;
            }
            set
            {
                columnName = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string ColumnType
        {
            get
            {
                return columnType;
            }
            set
            {
                columnType = value;
            }
        }

        /// <summary>
        /// �г���
        /// </summary>
        public int ColumnLength
        {
            get
            {
                return columnLength;
            }
            set
            {
                columnLength = value;
            }
        }

        /// <summary>
        /// ��С������
        /// </summary>
        public int ColumnDecimalLen
        {
            get
            {
                return columnDecimalLen;
            }
            set
            {
                columnDecimalLen = value;
            }
        }

        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public string DefaultValue
        {
            get
            {
                return defaultValue;
            }
            set
            {
                defaultValue = value;
            }
        }

        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark
        {
            get
            {
                return remark;
            }
            set
            {
                remark = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        public bool IsValid
        {
            get
            {
                return isValid;
            }
            set
            {
                isValid = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public int SortID
        {
            get
            {
                return sortID;
            }
            set
            {
                sortID = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public OperEnvironment Oper
        {
            get
            {
                return this.oper;
            }
            set
            {
                this.oper = value;
            }
        }
        #endregion

        #region ��¡
        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns>Const��ʵ��</returns>
        public new ExpandColumn Clone()
        {
            ExpandColumn expandColumn = base.Clone() as ExpandColumn;
            expandColumn.Oper = this.Oper.Clone();

            return expandColumn;

        }
        #endregion
    }
}
