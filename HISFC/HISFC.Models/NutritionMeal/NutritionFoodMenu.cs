using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.NutritionMeal
{
    /// <summary>
    /// Terminal<br></br>
    /// [��������: ����ά��]<br></br>
    /// [�� �� ��: ��ΰ��]<br></br>
    /// [����ʱ��: 2007-9]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// ID�����ױ��룬NAME���������ƣ�MEMO����������(0����ͨ��ʳ���ס�1��������ʳ����)
    /// </summary>
    /// 
    [System.Serializable]
    public class NutritionFoodMenu : FS.FrameWork.Models.NeuObject, FS.HISFC.Models.Base.IValid, FS.HISFC.Models.Base.ISpell
    {
        /// <summary>
        /// ��Ч��
        /// </summary>
        private bool isValid;

        /// <summary>
        /// ���׽��
        /// </summary>
        private decimal money;

        /// <summary>
        /// ��������
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment createEnv = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ȡ������
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment cancelEnv = new FS.HISFC.Models.Base.OperEnvironment();

        private string spellCode;
        private string wbCode;
        private string userCode;


        #region IValid ��Ա

        /// <summary>
        /// ��Ч��
        /// </summary>
        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
            set
            {
                this.isValid = value;
            }
        }

        #endregion

        /// <summary>
        /// ���׽��
        /// </summary>
        public decimal Money
        {
            get
            {
                return this.money;
            }
            set
            {
                this.money = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment CreateEnv
        {
            get
            {
                return this.createEnv;
            }
            set
            {
                this.createEnv = value;
            }
        }

        /// <summary>
        /// ȡ������
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment CancelEnv
        {
            get
            {
                return this.cancelEnv;
            }
            set
            {
                this.cancelEnv = value;
            }
        }

        public new NutritionFoodMenu Clone()
        {
            NutritionFoodMenu n = base.Clone() as NutritionFoodMenu;
            n.createEnv = this.createEnv.Clone();
            n.cancelEnv = this.cancelEnv.Clone();

            return n;
        }

        #region ISpell ��Ա

        /// <summary>
        /// ƴ����
        /// </summary>
        public string SpellCode
        {
            get
            {
                return this.spellCode;
            }
            set
            {
                this.spellCode = value;
            }
        }

        /// <summary>
        /// �����
        /// </summary>
        public string WBCode
        {
            get
            {
                return this.wbCode;
            }
            set
            {
                this.wbCode = value;
            }
        }

        /// <summary>
        /// �Զ�����
        /// </summary>
        public string UserCode
        {
            get
            {
                return this.userCode;
            }
            set
            {
                this.userCode = value;
            }
        }

        #endregion
    }
}
