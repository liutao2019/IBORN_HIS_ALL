using System;

namespace neusoft.HISFC.Object.Case
{
   /// <summary>
   /// Ϊʲô�������д��ʵ��㵱���أ���Ĳ�����,Ϊɶ��д��neufc����ȥѽ
   /// </summary>
	public class CaseFunc
	{
		public CaseFunc()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// �ж����Ƿ񳬹������й涨���ȣ�������������׳��쳣
		/// </summary>
		/// <param name="Obj">������</param>
		/// <param name="length">��Ӧ�������ƶ�����</param>
		/// <param name="exMessage">�쳣������Ϣ</param>
		/// <returns>����ֵ True�������� ����ֱ���׳��쳣</returns>
		public static bool ExLength( System.Object Obj, int length, string exMessage )
		{
			if( Obj.ToString().Length > length )
			{
				Exception ExLength = new Exception( exMessage + " ����" + length.ToString() + "λ��" );
				ExLength.Source = Obj.ToString();
				throw ExLength;
			}
			else
			{
				return true;
			}
		}
	}
}
