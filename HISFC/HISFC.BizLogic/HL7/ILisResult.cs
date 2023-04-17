using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizLogic.HL7
{
    /// <summary>
    /// [��������: LIS����ӿ�]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-05-10]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public interface ILisResult
    {
        /// <summary>
        /// ��ҽ����ʾ������
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int ShowResult(string id);

        /// <summary>
        /// �������Ƿ��Ѿ�����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IsValid(string id);
    }
}
