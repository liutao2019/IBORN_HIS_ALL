using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Supply
{
    /// <summary>    
    /// [��������: ��Ӧ������������]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2008-05-26]<br></br>
    /// <˵��
    ///		
    ///  />
    /// </summary>   
    [Serializable]
    public class Product : FS.FrameWork.Models.NeuObject
    {
        public Product()
        {
            
        }

        #region ����

        /// <summary>
        /// ��Ʒ
        /// </summary>
        private FS.HISFC.Models.Fee.Item.Undrug undrug = new FS.HISFC.Models.Fee.Item.Undrug();

        /// <summary>
        /// ������� 
        /// </summary>
        private string productiveListNO = "";

        /// <summary>
        /// �ƻ�������
        /// </summary>
        private decimal planQty;

        /// <summary>
        /// ����Ʒ��
        /// </summary>
        private decimal inputQty;

        /// <summary>
        /// �Ƽ� ��λ
        /// </summary>
        private string unit;

        /// <summary>
        /// ������Ϣ--��Ա������
        /// </summary>
        private FS.HISFC.Models.Base.OperEnvironment operEnv = new FS.HISFC.Models.Base.OperEnvironment();

        /// <summary>
        /// ��չ���
        /// </summary>
        private string extend1;

        /// <summary>
        /// ��չ���1
        /// </summary>
        private string extend2;

        #endregion

        #region ����

        /// <summary>
        /// �Ƽ���Ʒ
        /// </summary>
        public FS.HISFC.Models.Fee.Item.Undrug UnDrug
        {
            get
            {
                return this.undrug;
            }
            set
            {
                this.undrug = value;
            }
        }

        /// <summary>
        /// �����ƻ����
        /// </summary>
        public string ProductiveListNO
        {
            get
            {
                return this.productiveListNO;
            }
            set
            {
                this.ID = value;
                this.productiveListNO = value;
            }
        }

        /// <summary>
        /// �ƻ�������
        /// </summary>
        public decimal PlanQty
        {
            get
            {
                return this.planQty;
            }
            set
            {
                this.planQty = value;
            }
        }

        /// <summary>
        /// ��Ʒ�����
        /// </summary>
        public decimal InputQty
        {
            get
            {
                return this.inputQty;
            }
            set
            {
                this.inputQty = value;
            }
        }

        /// <summary>
        /// �Ƽ� ��λ
        /// </summary>
        public string Unit
        {
            get
            {
                return this.unit;
            }
            set
            {
                this.unit = value;
            }
        }

        /// <summary>
        /// ������Ϣ--��Ա������
        /// </summary>
        public FS.HISFC.Models.Base.OperEnvironment OperEnv
        {
            get
            {
                return this.operEnv;
            }
            set
            {
                this.operEnv = value;
            }
        }

        /// <summary>
        /// ��չ���1
        /// </summary>
        public string Extend1
        {
            get
            {
                return this.extend1;
            }
            set
            {
                this.extend1 = value;
            }
        }

        /// <summary>
        /// ��չ���2
        /// </summary>
        public string Extend2
        {
            get
            {
                return this.extend2;
            }
            set
            {
                this.extend2 = value;
            }
        }

        #endregion        

        #region ����

        /// <summary>
        /// ���ƶ���
        /// </summary>
        /// <returns>PPRBase</returns>
        public new Product Clone()
        {
            Product product = base.Clone() as Product;
            product.undrug = this.undrug.Clone();
            product.operEnv = this.operEnv.Clone();

            return product;
        }

        #endregion
    }
}
