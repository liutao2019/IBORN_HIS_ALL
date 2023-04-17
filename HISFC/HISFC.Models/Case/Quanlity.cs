using System;

namespace neusoft.HISFC.Object.Case
{
	/// <summary>
	/// ����ʵ����:�������
	/// </summary>
	public class Quanlity
	{
		public Quanlity()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region �ż�_��Ժ���϶��� string Varchar(1)
		/// <summary>
		/// �ż�_��Ժ���� string Varchar(1)
		/// </summary>
		public string cePi
		{
			set
			{
				if( this.ExLength( value, 1, "�ż�_��Ժ����" ) )
				{
					cePi = value;
				}
			}
		}
		#endregion
		#region ���_Ժ���϶��� string Varchar(1)
		/// <summary>
		/// ���_Ժ���� string Varchar(1)
		/// </summary>
		public string piPo
		{
			set
			{
				if( this.ExLength( value, 1, "���_Ժ����" ) )
				{
					piPo = value;
				}
			}
		}
		#endregion
		#region ��ǰ_����϶��� string Varchar(1)
		/// <summary>
		/// ��ǰ_����� string Varchar(1)
		/// </summary>
		public string opbOpa
		{
			set
			{
				if( this.ExLength( value, 1, "��ǰ_�����" ) )
				{
					opbOpa = value;
				}
			}
		}
		#endregion
		#region �ٴ�_X����϶��� string Varchar(1)
		/// <summary>
		/// �ٴ�_X����� string Varchar(1)
		/// </summary>
		public string clX
		{
			set
			{
				if( this.ExLength( value, 1, "�ٴ�_X�����" ) )
				{
					clX = value;
				}
			}
		}
		#endregion
		#region �ٴ�_CT���϶��� string Varchar(1)
		/// <summary>
		/// �ٴ�_CT���� string Varchar(1)
		/// </summary>
		public string ctCT
		{
			set
			{
				if( this.ExLength( value, 1, "�ٴ�_CT����" ) )
				{
					ctCT = value;
				}
			}
		}
		#endregion
		#region �ٴ�_MRI���϶��� string Varchar(1)
		/// <summary>
		/// �ٴ�_MRI���� string Varchar(1)
		/// </summary>
		public string clMRI
		{
			set
			{
				if( this.ExLength( value, 1, "�ٴ�_MRI����" ) )
				{
					clMRI = value;
				}
			}
		}
		#endregion
		#region �ٴ�_������϶��� string Varchar(1)
		/// <summary>
		/// �ٴ�_������� string Varchar(1)
		/// </summary>
		public string clPA
		{
			set
			{
				if( this.ExLength( value, 1, "�ٴ�_�������" ) )
				{
					clPA = value;
				}
			}
		}
		#endregion
		#region ����_������϶��� string Varchar(1)
		/// <summary>
		/// ����_������� string Varchar(1)
		/// </summary>
		public string fsBL
		{
			set
			{
				if( this.ExLength( value, 1, "����_�������" ) )
				{
					fsBL = value;
				}
			}
		}
		#endregion

		private bool ExLength( System.Object Obj, int length, string exMessage )
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

		public new Quanlity Clone()
		{
			Quanlity QuanlityClone = base.MemberwiseClone() as Quanlity;

			return QuanlityClone;
		} 

	}
}
