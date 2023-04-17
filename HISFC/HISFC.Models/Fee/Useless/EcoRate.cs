/*----------------------------------------------------------------
            // Copyright (C) ��˹
            // ��Ȩ���С� 
            //
            // �ļ�����			HomeBedRate.cs
            // �ļ�����������	��ͥ����������
            //
            // 
            // ������ʶ��		2006-5-16
            //
            // �޸ı�ʶ��
            // �޸�������
            //
            // �޸ı�ʶ��
            // �޸�������
//----------------------------------------------------------------*/

using System;
using FS.FrameWork.Models;

namespace FS.HISFC.Models.Fee.Useless
{
	/// <summary>
	/// ��ͥ����������
	/// </summary>
    [System.Serializable]
    public class EcoRate : FS.FrameWork.Models.NeuObject
	{
		public EcoRate()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ҽԺ�������
		/// [ID - ��������]
		/// [Name - ��������]
		/// </summary>
		FS.FrameWork.Models.NeuObject hospital = new NeuObject();
		/// <summary>
		/// �������
		/// </summary>
		FS.FrameWork.Models.NeuObject rateType = new NeuObject();
		/// <summary>
		/// ��Ŀ����
		/// [0 - ����]
		/// [1 - ��С����]
		/// [2 - ��Ŀ]
		/// </summary>
		FS.FrameWork.Models.NeuObject itemType = new NeuObject();
		/// <summary>
		/// ��Ŀ
		/// </summary>
		FS.FrameWork.Models.NeuObject item = new NeuObject();
		/// <summary>
		/// �Żݱ���
		/// </summary>
		FS.HISFC.Models.Base.FTRate rate = new FS.HISFC.Models.Base.FTRate();
		/// <summary>
		/// ����Ա
		/// </summary>
		FS.FrameWork.Models.NeuObject currentOperator = new NeuObject();
		/// <summary>
		/// ����ʱ��
		/// </summary>
		System.DateTime operateDateTime = DateTime.MinValue;

		/// <summary>
		/// ҽԺ�������
		/// [ID - ��������]
		/// [Name - ��������]
		/// </summary>
		public FS.FrameWork.Models.NeuObject Hospital
		{
			get
			{
				return this.hospital;
			}
			set
			{
				this.hospital = value;
			}
		}
		/// <summary>
		/// ������������
		/// </summary>
		public FS.FrameWork.Models.NeuObject RateType
		{
			get
			{
				return this.rateType;
			}
			set
			{
				this.rateType = value;
			}
		}
		/// <summary>
		/// ��Ŀ����
		/// [0 - ����]
		/// [1 - ��С����]
		/// [2 - ��Ŀ]
		/// </summary>
		public FS.FrameWork.Models.NeuObject ItemType
		{
			get
			{
				return this.itemType;
			}
			set
			{
				this.itemType = value;
			}
		}
		/// <summary>
		/// ��Ŀ����
		/// </summary>
		public FS.FrameWork.Models.NeuObject Item
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
			}
		}
		/// <summary>
		/// �����Żݱ���
		/// </summary>
		public FS.HISFC.Models.Base.FTRate Rate
		{
			get
			{
				return this.rate;
			}
			set
			{
				this.rate = value;
			}
		}
		/// <summary>
		/// ��ǰ����Ա
		/// </summary>
		public FS.FrameWork.Models.NeuObject CurrentOperator
		{
			get
			{
				return this.currentOperator;
			}
			set
			{
				this.currentOperator = value;
			}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public System.DateTime OperateDateTime
		{
			get
			{
				return this.operateDateTime;
			}
			set
			{
				this.operateDateTime = value;
			}
		}


		public new EcoRate Clone()
		{
			EcoRate ecoRate = base.Clone() as EcoRate;
			ecoRate.CurrentOperator = this.CurrentOperator.Clone();
			ecoRate.Hospital = this.Hospital.Clone();
			ecoRate.Item = this.Item.Clone();
			ecoRate.ItemType = this.ItemType.Clone();
			ecoRate.RateType = this.RateType.Clone();
			return ecoRate;
		}
	}
}
