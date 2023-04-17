using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Components.Preparation.Prescription
{
    /// <summary>
    /// <br></br>
    /// [��������: ��Ʒ����ά����ԭ���ϲ����ӿ���]<br></br>
    /// [�� �� ��: ��˹]<br></br>
    /// [����ʱ��: 2008-05]<br></br>
    /// <˵��>
    ///    
    /// </˵��>
    /// </summary>
    public interface IPrescriptionMaterial
    {
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        int AddMaterial();

        /// <summary>
        /// ԭ������Ϣ��ʾ
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        int ShowMaterial(FS.FrameWork.Models.NeuObject product);

        /// <summary>
        /// ԭ����ɾ��
        /// </summary>
        /// <returns></returns>
        int DeleteMaterial();

        /// <summary>
        /// ԭ�������
        /// </summary>
        /// <returns></returns>
        int Clear();

        /// <summary>
        /// ԭ������Ϣ��ȡ(�ɽ����ȡ����)
        /// </summary>
        /// <returns></returns>
        List<FS.HISFC.Models.Preparation.PrescriptionBase> GetMaterial();

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
