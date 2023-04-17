using System;
using FS.HISFC.Models.Base;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Fee.Item
{
    [Serializable]
    public class ItemLevel :  FS.HISFC.Models.Base.Spell	
    {
        /// <summary>
        /// �㼶ҽ��
        /// {1EB2DEC4-C309-441f-BCCE-516DB219FD0E} 
        /// </summary>
        public ItemLevel() 
		{
			
		}


        #region ����

        /// <summary>
        /// ʹ�÷�Χ 1����/2סԺ/0ȫ��
        /// </summary>
        private int inOutType;

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private NeuObject dept = new NeuObject();

        /// <summary>
        /// ҽ����Ϣ
        /// </summary>
        private NeuObject onwer = new NeuObject();

        /// <summary>
        /// �Ƿ���1�ǣ�0��
        /// </summary>
        private bool isShared;

        /// <summary>
        /// �Ƿ��Ѿ��ͷ���Դ Ĭ��Ϊfalseû���ͷ�
        /// </summary>
        private bool alreadyDisposed = false;

        /// <summary>
        /// ���ڵ�ID
        /// </summary>
        private string parentID;

        /// <summary>
        /// �������
        /// </summary>
        private NeuObject levelClass = new NeuObject();

        private int sortID = 0;
        #endregion

        #region ����

        /// <summary>
        /// ʹ�÷�Χ 1����/2סԺ/0ȫ�� 
        /// </summary>
        public int InOutType
        {
            get
            {
                return this.inOutType;
            }
            set
            {
                this.inOutType = value;
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public NeuObject Dept
        {
            get
            {
                return this.dept;
            }
            set
            {
                this.dept = value;
            }
        }

        /// <summary>
        /// ҽ����Ϣ
        /// </summary>
        public NeuObject Owner
        {
            get
            {
                return this.onwer;
            }
            set
            {
                this.onwer = value;
            }
        }

        /// <summary>
        /// �Ƿ���1�ǣ�0��
        /// </summary>
        public bool IsShared
        {
            get
            {
                return this.isShared;
            }
            set
            {
                this.isShared = value;
            }
        }

        /// <summary>
        /// ���ڵ�ID
        /// </summary>
        public string ParentID
        {
            get
            {
                return parentID;
            }
            set
            {
                parentID = value;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public NeuObject LevelClass
        {
            get { return levelClass; }
            set { levelClass = value; }
        }

        public int SortID
        {
            get { return this.sortID; }
            set { this.sortID = value; }
        }
        #endregion

        #region ����

        #region �ͷ���Դ
        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        /// <param name="isDisposing">�Ƿ��ͷ� true�� false��</param>
        protected override void Dispose(bool isDisposing)
        {
            if (alreadyDisposed)
            {
                return;
            }

            if (this.dept != null)
            {
                this.dept.Dispose();
                this.dept = null;
            }
            if (this.onwer != null)
            {
                this.onwer.Dispose();
                this.onwer = null;
            }

            base.Dispose(isDisposing);

            alreadyDisposed = true;
        }
        #endregion

        #region ��¡
        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns>���ص�ǰ���󸱱�</returns>
        public new ItemLevel Clone()
        {
            ItemLevel itemLevel = base.Clone() as ItemLevel;

            itemLevel.Dept = this.Dept.Clone();
            itemLevel.Owner = this.Owner.Clone();
            itemLevel.LevelClass = this.LevelClass.Clone();

            return itemLevel;
        }
        #endregion

        #endregion
	}
}
