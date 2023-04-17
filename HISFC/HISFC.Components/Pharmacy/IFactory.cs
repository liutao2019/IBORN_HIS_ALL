using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Components.Pharmacy
{
    /// <summary>
    /// [��������: �����ҵ�񹤳��ӿ�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-09]<br></br>
    /// </summary>
    public interface IFactory
    {
        /// <summary>
        /// ��ȡ���ӿ���
        /// </summary>
        /// <param name="inPrivType"></param>
        /// <param name="ucPhaManager"></param>
        /// <returns></returns>
        FS.HISFC.Components.Pharmacy.In.IPhaInManager GetInInstance(FS.FrameWork.Models.NeuObject inPrivType, FS.HISFC.Components.Pharmacy.In.ucPhaIn ucPhaManager);

        /// <summary>
        /// ��ȡ����ӿ���
        /// </summary>
        /// <param name="outPrivType"></param>
        /// <param name="ucPhaManager"></param>
        /// <returns></returns>
        FS.HISFC.Components.Pharmacy.In.IPhaInManager GetOutInstance(FS.FrameWork.Models.NeuObject outPrivType, FS.HISFC.Components.Pharmacy.Out.ucPhaOut ucPhaManager);
    }
}
