using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace GZSI.ApiClass
{
    /// <summary>
    /// ����ҽ��API����������
    /// </summary>
     public class GzsiAPI
     {

         #region �������壨ҽ���ӿڣ�
         /// <summary>
         /// �ӿ�ʵ����
         /// </summary>
         /// <returns>-1��ʧ�� </returns>
         [DllImport("HG_Interface.dll")]
         public static extern IntPtr newinterface();

         /// <summary>
         /// ��ʼ���ӿ�
         /// </summary>
         /// <param name="pint">ʵ���������newinterface()����newinterfacewithinit�ķ���ֵ��</param>
         /// <param name="addr">Ӧ�÷�������ַ</param>
         /// <param name="port">Ӧ�÷������˿ں�</param>
         /// <param name="servlet">Ӧ�÷��������Servlet������</param>
         /// <returns>-1��ʧ�� 1���ɹ�</returns>
         [DllImport("HG_Interface.dll")]
         public static extern long init(IntPtr pint, string addr, long port, string servlet);

         /// <summary>
         /// �ӿ�ʵ��������ʼ���ӿ�
         /// </summary>
         /// <param name="addr">Ӧ�÷�������ַ</param>
         /// <param name="port">Ӧ�÷������˿ں�</param>
         /// <param name="servlet">Ӧ�÷��������Servlet������</param>
         /// <returns> -1��ʧ�� </returns>
         [DllImport("HG_Interface.dll")]
         public static extern IntPtr newinterfacewithinit(string addr, int port, string servlet);

         /// <summary>
         /// �ӿڵ��ÿ�ʼ
         /// </summary>
         /// <param name="pint">ʵ���������newinterface()����newinterfacewithinit�ķ���ֵ��</param>
         /// <param name="func_id">ҵ��Ĺ��ܺ�</param>
         /// <returns>-1��ʧ�� 1���ɹ�</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int start(IntPtr pint, string func_id);

         /// <summary>
         /// �ӿڴ������
         /// </summary>
         /// <param name="pint">ʵ���������newinterface()����newinterfacewithinit�ķ���ֵ��</param>
         /// <param name="row">�������к�</param>
         /// <param name="pname">��������</param>
         /// <param name="pvalue">����ֵ</param>
         /// <returns> -1:ʧ��  >0 ���ɹ�</returns>
         [DllImport("HG_Interface.dll", 
             EntryPoint = "put",
             CharSet=CharSet.Ansi,
             SetLastError=true,
             ExactSpelling=true,
             CallingConvention=CallingConvention.StdCall)]
         public static extern int put(IntPtr pint, int row, string pname, string pvalue);

         /// <summary>
         /// �ӿڴ������
         /// </summary>
         /// <param name="pint">ʵ���������newinterface()����newinterfacewithinit�ķ���ֵ��</param>
         /// <param name="pname">��������</param>
         /// <param name="pvalue">����ֵ</param>
         /// <returns> -1:ʧ��  >0 ���ɹ� ��ǰ���к�</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int putcol(IntPtr pint, string pname, string pvalue);

         /// <summary>
         /// �ӿ�����
         /// </summary>
         /// <param name="pint">ʵ���������newinterface()����newinterfacewithinit�ķ���ֵ��</param>
         /// <returns> -1:ʧ�� >0:�ɹ� ���ز����ļ�¼����</returns>
         [DllImport("HG_Interface.dll", EntryPoint = "run", CharSet = CharSet.Ansi)]
         public static extern int run(IntPtr pint);

         /// <summary>
         /// ����ָ����¼����ȡ���ؼ�¼�� �� ������εļ�¼����
         /// </summary>
         /// <param name="pint">ʵ���������newinterface()����newinterfacewithinit�ķ���ֵ��</param>
         /// <param name="result_name">��¼������</param>
         /// <returns>-1��ʧ�� >=0:��¼���ļ�¼��</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int setresultset(IntPtr pint, string result_name);

         /// <summary>
         /// ���ýӿڵ�����ģʽ
         /// flagΪ1ʱ������������Ϣ����д��ָ��Ŀ¼direct�µ���־�ļ���
         /// </summary>
         /// <param name="pint">ʵ�������</param>
         /// <param name="flag">����ģʽ��־ 0��0��ʾ�������� 1������</param>
         /// <param name="in_direct">��ŵ�����Ϣ��־�ļ���Ŀ¼����Ŀ¼�����Ǵ��ڵģ�</param>
         /// <returns>С��0 :ʧ��  >=0:�ɹ�</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int setdebug(IntPtr pint, int flag, string in_direct);

         /// <summary>
         /// �ӿ�ȡ�÷��صĲ���ֵ
         /// </summary>
         /// <param name="pint">ʵ�������</param>
         /// <param name="pname">�ӿڷ��ص��ֶ���</param>
         /// <param name="pvalue">�ӿڷ��ص���ֵ</param>
         /// <returns> С��0 :ʧ�� >0:�ɹ� ��ǰ��¼���ĵڼ�����¼</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int getbyname(IntPtr pint, string pname,  StringBuilder pvalue);

         /// <summary>
         /// �ӿ�ȡ�÷��صĲ���ֵ
         /// </summary>
         /// <param name="pint">ʵ�������</param>
         /// <param name="index">���ص��ֶ���������ֵ</param>
         /// <param name="pname">�ӿڷ��ص��ֶ���</param>
         /// <param name="pvalue">�ӿڷ��ص���ֵ</param>
         /// <returns>С��0 :ʧ�� >0:�ɹ�</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int getbyindex(IntPtr pint, int index, string pname, ref string pvalue);

         /// <summary>
         /// ��ȡ�ӿں���������Ϣ
         /// </summary>
         /// <param name="pint">ʵ�������</param>
         /// <param name="msg">������Ϣ</param>
         /// <returns> С��0��ʧ��</returns>
         [DllImport("HG_Interface.dll", EntryPoint = "getmessage", CharSet = CharSet.Ansi)]
         public static extern int getmessage(IntPtr pint,   StringBuilder msg);

         /// <summary>
         /// ��ȡ�ӿں����쳣��Ϣ
         /// </summary>
         /// <param name="pint">ʵ�������</param>
         /// <param name="exception">�쳣��Ϣ</param>
         /// <returns> С��0��ʧ��</returns>
         [DllImport("HG_Interface.dll", EntryPoint = "getexception", CharSet = CharSet.Ansi)]
         public static extern int getexception(IntPtr pint,  string exception);

         /// <summary>
         /// �ӿ�ȡ�õ�index�ļ�¼����
         /// </summary>
         /// <param name="pint">ʵ�������</param>
         /// <param name="index"></param>
         /// <param name="resultname">��¼������</param>
         /// <returns>С��0��ʧ��</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int getresultnamebyindex(IntPtr pint, int index, ref string resultname);

         /// <summary>
         /// ���صĵ�ǰ��¼���ļ�¼����
         /// </summary>
         /// <param name="pint">ʵ�������</param>
         /// <returns>С��0��ʧ��</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int getrowcount(IntPtr pint);

         /// <summary>
         /// �ͷŽӿڵ�ʵ��
         /// </summary>
         /// <param name="pint">ʵ�������</param>
         /// <returns> -1:ʧ��</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int destoryinterface(IntPtr pint);

         /// <summary>
         /// ��һ��
         /// </summary>
         /// <param name="pint">ʵ�������</param>
         /// <returns> -1��ʧ�� >0 :�ɹ� ��ǰ���к�</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int firstrow(IntPtr pint);

         /// <summary>
         /// ��һ��
         /// </summary>
         /// <param name="pint">ʵ�������</param>
         /// <returns>-1��ʧ�� >0 :�ɹ� ��ǰ���к�</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int nextrow(IntPtr pint);

         /// <summary>
         /// ǰһ��
         /// </summary>
         /// <param name="pint">ʵ�������</param>
         /// <returns>-1��ʧ�� >0 :�ɹ� ��ǰ���к�</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int prevrow(IntPtr pint);

         /// <summary>
         /// ���һ��
         /// </summary>
         /// <param name="pint">ʵ�������</param>
         /// <returns>-1��ʧ�� >0 :�ɹ� ��ǰ���к�</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int lastrow(IntPtr pint);

         /// <summary>
         /// ����IC���豸�Ĵ��ں�
         /// </summary>
         /// <param name="pint">ʵ�������</param>
         /// <param name="comm">IC�����ӵĴ��ں�</param>
         /// <returns></returns>
         [DllImport("HG_Interface.dll")]
         public static extern long set_ic_commport(IntPtr pint, int comm);

         /// <summary>
         /// �����ݰ�base64��ʽ����
         /// </summary>
         /// <param name="pSrc">Դ����</param>
         /// <param name="nSize">Դ���ݳ���</param>
         /// <param name="pDest">����������</param>
         /// <returns>С��0��ʧ�� ����0���ɹ����������ֽ���</returns>
         [DllImport("HG_Interface.dll")]
         public static extern long encode64(string pSrc, int nSize,string pDest);

         /// <summary>
         /// ���ݰ�base64��ʽ����
         /// </summary>
         /// <param name="pSrc">Դ����</param>
         /// <param name="nSize">Դ���ݳ���</param>
         /// <param name="pDest">����������</param>
         /// <returns>С��0��ʧ�� ����0���ɹ����������ֽ���</returns>
         [DllImport("HG_Interface.dll")]
         public static extern long decode64(string pSrc, int nSize, string pDest);

         /// <summary>
         /// ��base64��ʽ����ʱ����ñ��������ݳ���
         /// </summary>
         /// <param name="nSize">Դ���ݳ���</param>
         /// <returns>С��0��ʧ�� ����0���ɹ����������ֽ���</returns>
         [DllImport("HG_Interface.dll")]
         public static extern long encodesize(int nSize);

         /// <summary>
         /// ��base64��ʽ����ʱ,��ȡ���������ݳ���
         /// </summary>
         /// <param name="nSize">Դ���ݳ���</param>
         /// <returns>С��0��ʧ�� ����0���ɹ����������ֽ���</returns>
         [DllImport("HG_Interface.dll")]
         public static extern long decodesize(int nSize);

         /// <summary>
         /// ��base64��ʽ���룬�������������ݴ浽filename�ļ�
         /// </summary>
         /// <param name="pSrc">Դ����</param>
         /// <param name="nSize">Դ���ݳ���</param>
         /// <param name="filename">����������Ҫ������ļ���</param>
         /// <returns>С��0��ʧ�� ����0���ɹ����������ֽ���</returns>
         [DllImport("HG_Interface.dll")]
         public static extern long decode64_tofile(string pSrc, int nSize, string filename);


         #endregion

     }


}
