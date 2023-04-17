using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Components.Preparation.Prescription
{
    /// <summary>
    /// <br></br>
    /// [��������: ��Ʒ����ά������Ʒ�����ӿ���]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2008-05]<br></br>
    /// <˵��>
    ///    
    /// </˵��>
    /// </summary>
    public interface IPrescriptionProduct
    {
        /// <summary>
        /// ��ϸ�����¼�
        /// </summary>
        event System.EventHandler ShowPrescriptionEvent;

        /// <summary>
        /// ���ӳ�Ʒ
        /// </summary>
        /// <returns></returns>
        int AddProduct();

        /// <summary>
        /// ��Ʒ��Ϣ��ʾ
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        int ShowProduct(FS.FrameWork.Models.NeuObject product);

        /// <summary>
        /// ɾ����Ʒ
        /// </summary>
        /// <returns></returns>
        int DeleteProduct();

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        int Clear();

        /// <summary>
        /// ����չ��UI
        /// </summary>
        FS.FrameWork.WinForms.Controls.ucBaseControl ProductControl
        {
            get;
        }

        /// <summary>
        /// ��Ŀ���
        /// </summary>
        FS.HISFC.Models.Base.EnumItemType ItemType
        {
            set;
        }
    }
}
