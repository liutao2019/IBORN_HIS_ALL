using System;
using System.Collections.Generic;
using System.Text;

namespace UFC.Lis
{
    /// <summary>
    /// [��������: Lis�ӿ����ݴ���]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2008-03]<br></br>
    /// </summary>
    public interface ILisDB
    {

        /// <summary>
        /// ����Load����Lis���ݿ�
        /// </summary>
        /// <returns></returns>
        int ConnectLisOnLoad();

        /// <summary>
        /// ��ѯʱ����Lis���ݿ�
        /// </summary>
        /// <returns></returns>
        int ConnectLisOnQuery();

        /// <summary>
        /// �ر�Lis����
        /// </summary>
        /// <returns></returns>
        int CloseLisDB();

        /// <summary>
        /// ���ݴ���
        /// </summary>
        /// <param name="p"></param>
        /// <param name="execList"></param>
        /// <returns></returns>
        int TransDataToLisDB(FS.HISFC.Models.RADT.PatientInfo p,List<FS.HISFC.Models.Order.ExecOrder> execList,ref string err);

    }
}
