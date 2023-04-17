using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.Preparation
{

    /// <summary>
    /// �Ƽ�״̬��
    /// </summary>
    public enum EnumState
    {
        /// <summary>
        /// �ƻ�
        /// </summary>
        Plan,
        /// <summary>
        /// ����
        /// </summary>
        Confect,
        /// <summary>
        /// ���Ʒ��װ
        /// </summary>
        Division,
        /// <summary>
        /// ���Ʒ����
        /// </summary>
        SemiAssay,
        /// <summary>
        /// ��Ʒ���װ
        /// </summary>
        Package,
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        PackAssay,
        /// <summary>
        /// ��Ʒ���
        /// </summary>
        Input
    }

    /// <summary>
    /// �Ƽ�ģ������
    /// </summary>
    public enum EnumStencialType
    {
        /// <summary>
        /// ���Ʒ����
        /// </summary>
        SemiAssayStencial,
        /// <summary>
        /// ��Ʒ����ģ��
        /// </summary>
        ProductAssayStencial,
        /// <summary>
        /// ��������
        /// </summary>
        ProductStencial,
        /// <summary>
        /// ��չ
        /// </summary>
        Extend
    }

    /// <summary>
    /// �Ƽ�����ԭ������ö����
    /// </summary>
    public enum EnumMaterialType
    {
        /// <summary>
        /// ����ԭ��
        /// </summary>
        Material = 1,
        /// <summary>
        /// ��װ����
        /// </summary>
        Wrapper = 0
    }

}
