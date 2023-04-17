using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Interface
{
    /// <summary>
    /// ҩƷִ��ҽ����ӡ�ӿ� 
    /// ��Һ������ҩ��ʹ�ô˽ӿ�
    /// </summary>
    public interface IPrintExecDrug
    {
        /// <summary>
        /// ����������Ϣ
        /// </summary>
        /// <param name="oper">��Һ��Ա��Ϣ</param>
        /// <param name="dept">����Һ����</param>
        void SetTitle(FS.HISFC.Models.Base.OperEnvironment oper,FS.FrameWork.Models.NeuObject dept);

        /// <summary>
        /// ִ��ҽ����Ϣ
        /// </summary>
        /// <param name="alExecOrder">ִ��ҽ����Ϣ</param>
        /// <param name="hsPatient">ҽ���ڰ���������Ϣ</param>
        void SetExecOrder(System.Collections.ArrayList alExecOrder, System.Collections.Hashtable hsPatient);

        /// <summary>
        /// ִ��ҽ����Ϣ
        /// </summary>
        /// <param name="alExecOrder">ִ��ҽ����Ϣ</param>
        void SetExecOrder(System.Collections.ArrayList alExecOrder);

        /// <summary>
        /// ��ӡ
        /// </summary>
        void Print();

        /// <summary>
        /// ��ӡԤ��
        /// </summary>
        void PrintPreview();
    }
}
