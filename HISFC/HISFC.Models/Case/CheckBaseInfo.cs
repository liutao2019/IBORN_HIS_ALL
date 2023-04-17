using System;

namespace neusoft.HISFC.Object.Case
{
    /*----------------------------------------------------------------
    // Copyright (C) 2004 ����ɷ����޹�˾
    // ��Ȩ���С� 
    //
    // �ļ�����CheckBaseInfo.cs
    // �ļ��������������ʵ��
    //
    // 
    // ������ʶ:
    //
    // �޸ı�ʶ����ѩ�� 20060420
    // �޸�������
    //
    // �޸ı�ʶ��
    // �޸�������
    //----------------------------------------------------------------*/
	public class CheckBaseInfo : neusoft.neuFC.Object.neuObject
	{
		public CheckBaseInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ָ����ע(��黯������)
		/// </summary>
		/// <param name="memo"></param>
		public CheckBaseInfo( string memo )
		{
			this.Memo = memo;
		}
	#region ˽�б���	
		
		private string code;
		private int times;
		//private string memo;
		
	#endregion

	#region ����

		/// <summary>
		/// ��黯�����
		/// </summary>
		public string Code
		{
			get
			{
				return code;
			}
			set
			{
				if( CaseFunc.ExLength( value, 20, "��黯����" ) )
				{
					code = value;
				}		
			}
		}

		/// <summary>
		/// ��黯�����
		/// </summary>
		public int Times
		{
			get
			{
				return times;
			}
			set
			{
				if( CaseFunc.ExLength( value, 20, "��黯�����" ) )
				{
					times = value;
				}
			}
		}
		
		

	#endregion
	
	#region ���к���
        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
		public new CheckBaseInfo Clone()
		{
			CheckBaseInfo CheckBaseInfoClone = base.MemberwiseClone() as CheckBaseInfo;

			return CheckBaseInfoClone;
		}
	#endregion
	}
}
