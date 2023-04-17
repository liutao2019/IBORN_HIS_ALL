using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.BizProcess.DCPInterface
{
    /// <summary>
    /// [����˵��: �����ӿڣ����ڶ�ȡ��д�롢���ӡ�ɾ�����޸��Լ���֤������Ϣ]
    /// [�� �� ��:   zj]
    /// [�������: 2008-08-25]
    /// </summary>
    public interface IAddition
    {
        /// <summary>
        /// ���߱��
        /// </summary>
        string PatientNO
        {
            get;
            set;
        }

        /// <summary>
        /// ��������
        /// </summary>
        string PatientName
        {
            get;
            set;
        }

        /// <summary>
        /// ����ʵ��
        /// </summary>
        FS.HISFC.DCP.Object.CommonReport Report
        {
            get;
            set;
        }
        /// <summary>
        /// ��֤������Ϣ��������
        /// </summary>
        /// <param name="msg">��ʾ��Ϣ</param>
        /// <returns>-1 ������  1 ����</returns>
        int IsValid(ref string msg);

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <returns>������Ϣʵ��</returns>
        FS.HISFC.DCP.Object.AdditionReport GetAdditionInfo(Control container);

        /// <summary>
        /// ���ݱ���NO��ȡ������Ϣ
        /// </summary>
        /// <param name="reportNO">����NO</param>
        /// <returns>������Ϣʵ��</returns>
        FS.HISFC.DCP.Object.AdditionReport GetAdditionInfo(string reportNO);

        /// <summary>
        /// д�븽����Ϣ
        /// </summary>
        /// <param name="addition">������Ϣʵ��</param>
        void SetAdditionInfo(FS.HISFC.DCP.Object.AdditionReport addition, Control container);

        /// <summary>
        /// �޸ĸ�����Ϣ
        /// </summary>
        /// <param name="addition">������Ϣʵ��</param>
        /// <param name="t">����</param>
        int UpdateAdditionInfo(FS.HISFC.DCP.Object.AdditionReport addition);

        /// <summary>
        /// ���븽����Ϣ
        /// </summary>
        /// <param name="addition">������Ϣʵ��</param>
        /// <param name="t">����</param>
        int InsertAdditionInfo(FS.HISFC.DCP.Object.AdditionReport addition);

        /// <summary>
        /// ɾ��������Ϣ
        /// </summary>
        /// <param name="addition">������Ϣʵ��</param>
        /// <param name="t">����</param>
        int DeleteAdditionInfo();

    }
}
