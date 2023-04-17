using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.PubReport.Models
{
    /// <summary>
    /// InvoiceTypeEnumService<br></br>
    /// [��������: �վ�(��Ʊ)����ö�ٷ�����]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2006-09-01]<br></br>
    /// <�޸ļ�¼
    ///		�޸���='��ΰ��'
    ///		�޸�ʱ��='2007-10-01'
    ///		�޸�Ŀ��='����ҽԺ���ػ�����'
    ///		�޸�����='����һ���µķ�Ʊ����:Ӫ����ʳ��Ʊ'
    ///  />
    /// </summary>
    public class InvoiceTypeEnumService : FS.HISFC.Models.Base.EnumServiceBase
    {
        static InvoiceTypeEnumService()
        {
            items[EnumInvoiceType.R] = "�Һ��վ�";
            items[EnumInvoiceType.C] = "�����վ�";
            items[EnumInvoiceType.I] = "סԺ�վ�";
            items[EnumInvoiceType.P] = "Ԥ���վ�";
            items[EnumInvoiceType.A] = "�����ʻ�";
            items[EnumInvoiceType.N] = "Ӫ����ʳ��Ʊ";//�����ӵķ�Ʊ����
        }

        #region ����

        /// <summary>
        /// ����ö������
        /// </summary>
        protected static Hashtable items = new Hashtable();

        /// <summary>
        /// �վ�(��Ʊ)����
        /// </summary>
        private EnumInvoiceType enumInvoiceType;

        #endregion

        #region ����

        /// <summary>
        /// ����ö������
        /// </summary>
        protected override Hashtable Items
        {
            get
            {
                return items;
            }
        }

        /// <summary>
        /// �վ�(��Ʊ)����
        /// </summary>
        protected override Enum EnumItem
        {
            get
            {
                return this.enumInvoiceType;
            }
        }

        #endregion

        #region ����

        #region ��¡

        /// <summary>
        /// ��¡
        /// </summary>
        /// <returns>��ǰ�����ʵ������</returns>
        public new InvoiceTypeEnumService Clone()
        {
            return base.Clone() as InvoiceTypeEnumService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList(GetObjectItems(items)));
        }
        #endregion

        #endregion
    }

    /// <summary>
    /// �վ�(��Ʊ)����
    /// </summary>
    public enum EnumInvoiceType
    {
        /// <summary>
        /// �Һ��վ�
        /// </summary>
        R = 0,

        /// <summary>
        /// �����վ�
        /// </summary>
        C = 1,

        /// <summary>
        /// סԺ�վ�
        /// </summary>
        I = 2,

        /// <summary>
        /// Ԥ���վ�
        /// </summary>
        P = 4,

        /// <summary>
        /// �����ʻ�
        /// </summary>
        A = 5,

        /// <summary>
        /// Ӫ����ʳ��Ʊ(����)
        /// </summary>
        N = 6
    }
}
