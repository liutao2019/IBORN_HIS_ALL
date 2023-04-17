using System;
using System.Collections;

namespace FS.HISFC.Models.Pharmacy
{
	/// <summary>
	/// Copyright (C) 2004 ����ɷ����޹�˾
	/// ��Ȩ����
	/// 
	/// �ļ�����IRecipeLabel.cs
	/// �ļ����������������ҩ��ǩ�ӿ�
	/// 
	/// 
	/// ������ʶ�������� 2006��04��28
	/// ����˵���������ҩ��ǩ
	/// 
	/// </summary>
    //[Serializable]
    public interface IRecipeLabel
	{
		/// <summary>
		/// ������Ϣ
		/// </summary>
		FS.HISFC.Models.Registration.Register PatientInfo
		{
			get;
			set;
		}
				

		/// <summary>
		/// ���δ�ӡ��ǩ��ҳ��
		/// </summary>
		decimal LabelTotNum
		{
			set;
		}
	

		/// <summary>
		/// һ�δ�ӡҩƷ����������
		/// </summary>
		decimal DrugTotNum
		{
			set;
		}


		/// <summary>
		/// ��ӡ�°�ҩ��ǩ ����ҩƷ
		/// </summary>
		/// <param name="info">��ҩ����</param>
		void AddSingle(ApplyOut info);

		/// <summary>
		/// ��ӡ��ҩ��ǩ ��ϴ�ӡ 
		/// </summary>
		/// <param name="alCombo">��ӡ�������</param>
		void AddCombo(ArrayList alCombo);

		/// <summary>
		/// ��ӡ��ҩ�嵥
		/// </summary>
		/// <param name="al">���д���ӡ����</param>
		void AddAllData(ArrayList al);
				
		/// <summary>
		/// ��ӡ��ҩ��
		/// </summary>
		void Print();
	}
}
