using System;

namespace neusoft.HISFC.Object.Case
{
	/// <summary>
	/// ���������Ϣ��ID ����Ա���� Name ����Ա���� memo��ע
	/// </summary>
	public class Diagnose : neusoft.neuFC.Object.neuObject
	{
		public Diagnose()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		
	#region ˽�б���

		private string diagOutState ;
		private string secondICD ;
		private string synDromeID ;
		private string clpa ;
		private string dubDiagFlag ;
		private string mainFlag ;
		private string levelCode ;
		private string periorCode ;
		private DateTime operDate;
		private neusoft.HISFC.Object.Case.DiagnoseBase myDiagInfo = new neusoft.HISFC.Object.Case.DiagnoseBase();
		private neusoft.HISFC.Object.RADT.PVisit myPVisit = new neusoft.HISFC.Object.RADT.PVisit();
		private string operType; //�������ͣ��ж���ҽ��վ¼�뻹�ǲ�����¼��� ��
		private string operationFlag ;
		private string is30Disease ;
		private int infectNum ; //Ժ�ڸ�Ⱦ����
	#endregion

		#region ����
		/// <summary>
		/// ��Ⱦ����
		/// </summary>
		public int InfectNum
		{
			get
			{
				return infectNum;
			}
			set
			{
				infectNum = value;
			}
		}
		/// <summary>
		/// �Ƿ�ʦ30�ּ���
		/// </summary>
		public string Is30Disease
		{
			get
			{
				if(is30Disease == null)
				{
					is30Disease = "";
				}
				return is30Disease;
			}
			set
			{
				is30Disease = value;
			}
		}
		/// <summary>
		/// �Ƿ�������
		/// </summary>
		public string OperationFlag
		{
			get
			{
				if(operationFlag == null)
				{
					operationFlag = "";
				}
				return operationFlag;
			}
			set
			{
				operationFlag = value;
			}
		}
		/// <summary>
		/// �ּ�
		/// </summary>
		public string LevelCode
		{
			get
			{
				if(levelCode == null)
				{
					levelCode = "";
				}
				return levelCode; 
			}
			set{ levelCode = value; }
		}
		/// <summary>
		/// ����
		/// </summary>
		public string PeriorCode
		{
			get
			{
				if(periorCode == null)
				{
					periorCode = "";
				}
				return periorCode; 
			}
			set{ periorCode = value; }
		}
		/// <summary>
		/// ���߷�����
		/// </summary>
		public neusoft.HISFC.Object.RADT.PVisit Pvisit
		{
			get{ return myPVisit; }
			set{ myPVisit = value; }
		}

		/// <summary>
		/// �����������
		/// </summary>
		public string DiagOutState
		{
			get
			{
				if(diagOutState == null)
				{
					diagOutState = "";
				}
				return diagOutState;
			}
			set
			{
				if( CaseFunc.ExLength( value, 2, "�������" ) )
				{
					diagOutState = value;
				}
			}
		}
		
		/// <summary>
		/// �ڶ�ICD����
		/// </summary>
		public string SecondICD
		{
			get
			{
				if(secondICD == null)
				{
					secondICD = "";
				}
				return secondICD;
			}
			set
			{
				if( CaseFunc.ExLength( value, 10, "�ڶ�ICD���" ) )
				{
					secondICD = value;
				}
			}
		}
		
		/// <summary>
		/// �ϲ�֢����
		/// </summary>
		public string SynDromeID
		{
			get
			{
				if(synDromeID == null)
				{
					synDromeID ="";
				}
				return synDromeID;
			}
			set
			{
				if( CaseFunc.ExLength( value, 1, "�ϲ�֢����" ) )
				{
					synDromeID = value;
				}
			}
		}
		
		/// <summary>
		/// �������
		/// </summary>
		public string CLPA
		{
			get
			{
				if(clpa == null)
				{
					clpa = "";
				}
				return clpa;
			}
			set
			{
				if( CaseFunc.ExLength( value, 1, "�������" ))
				{
					clpa = value;
				}
			}
		}
	
		/// <summary>
		/// �Ƿ�����
		/// </summary>
		public string DubDiagFlag
		{
			get
			{
				if(dubDiagFlag == null)
				{
					dubDiagFlag = "";
				}
				return dubDiagFlag; 
			}
			set
			{
				if( CaseFunc.ExLength( value, 1, "�Ƿ�����" ) )
				{
					dubDiagFlag = value;
				}
			}
		}
		
		/// <summary>
		/// �Ƿ������
		/// </summary>
		public string MainFlag
		{
			get
			{
				if( mainFlag == null)
				{
					mainFlag = "";
				}
				return mainFlag;
			}
			set
			{
				if( CaseFunc.ExLength( value, 1, "�Ƿ������" ) )
				{
					mainFlag = value;
				}
			}
		}
	
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime OperDate
		{
			get
			{
				return operDate; 
			}
			set{ operDate = value; }
		}
		
		/// <summary>
		/// �����Ϣ
		/// </summary>
		public neusoft.HISFC.Object.Case.DiagnoseBase DiagInfo
		{
			get{ return myDiagInfo; }
			set{ myDiagInfo = value; }
		}
	
		/// <summary>
		/// �������ͣ��ж���ҽ��վ¼�뻹�ǲ�����¼��� ��
		/// </summary>
		public string OperType
		{
			get
			{
				if(operType == null)
				{
					operType = "";
				}
				return operType ;
			}
			set
			{
				operType = value;
			}
		}
	#endregion

		#region ���ú���
		
		public new Diagnose Clone()
		{
			Diagnose DiagnoseClone = (Diagnose) base.Clone();;

			DiagnoseClone.myDiagInfo = this.myDiagInfo.Clone();
			DiagnoseClone.myPVisit = this.myPVisit.Clone();

			return DiagnoseClone;
		}

		#endregion
		
	}
}
