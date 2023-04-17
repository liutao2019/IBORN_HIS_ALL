using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Components.Speciment
{
    public class SpecUsedSum
    {
        private int storeCount = 0;
        private int colCount = 0;
        private string dept = "";
        private DateTime dtStartTime = DateTime.Now;
        private DateTime dtEndTime = DateTime.Now;
        private int usedCount = 0;
        private string userName = "";
        private string disType = "";
        private string specType="";

        /// <summary>
        /// ��������
        /// </summary>
        public string DisType
        {
            get
            {
                return disType;
            }
            set
            {
                disType = value;
            }
        }

        /// <summary>
        /// �걾����
        /// </summary>
        public string SpecType
        {
            get
            {
                return specType;
            }
            set
            {
                specType = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public int StoreCount
        {
            get
            {
                return storeCount;
            }
            set
            {
                storeCount = value;
            }
        }

        /// <summary>
        /// �ռ�������
        /// </summary>
        public int ColCount
        {
            get
            {
                return colCount;
            }
            set
            {
                colCount = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Dept
        {
            get
            {
                return dept;
            }
            set
            {
                dept = value;
            }
        }

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return dtStartTime;
            }
            set
            {
                dtStartTime = value;
            }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return dtEndTime;
            }
            set
            {
                dtEndTime = value;
            }
        }

        /// <summary>
        /// ��ȥ������
        /// </summary>
        public int UsedCount
        {
            get
            {
                return usedCount;
            }
            set
            {
                usedCount = value;
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }
    }
}
