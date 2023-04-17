using System;
using System.Collections;

namespace neusoft.HISFC.Object.Case 
{
    /*----------------------------------------------------------------
    // Copyright (C) 2004 ����ɷ����޹�˾
    // ��Ȩ���С� 
    //
    // �ļ�����BedReportType.cs
    // �ļ�����������סԺ��̬�ձ�ʵ��
    //
    // 
    // ������ʶ:
    //
    // �޸ı�ʶ����ѩ�� 20060420
    // �޸�����������һƬ�հ�
    //
    // �޸ı�ʶ��
    // �޸�������
    //----------------------------------------------------------------*/
	public class BedReportType: neusoft.neuFC.Object.neuObject 
    {
		
        public BedReportType() 
        {
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
        
        /// <summary>
        /// �ձ�ö��
        /// </summary>
		public enum enuBedReportType 
        {
			/// <summary>
			/// ������Ժ
			/// </summary>
			IN_NORMAL = 0,
			/// <summary>
			/// ����ת��
			/// </summary>
			IN_TRANSFER = 1,
			/// <summary>
			/// �ٻ���Ժ
			/// </summary>
			IN_RETURN = 2		
		}
	
        /// <summary>
        /// ����
        /// </summary>
		public new string Name 
        {
			get 
            {
				string strName;
				switch ((int)this.ID) 
                {
					case 1:
						strName = "����ҩƷ��ҩ";
						break;
					case 2:
						strName = "��Ժ��ҩ��ҩ";
						break;
					default:
						strName = "һ���ҩ";
						break;
				}
				return	strName;
			}
		}

		/// <summary>
		/// ����ID
		/// </summary>
		private enuBedReportType myID;
		public new System.Object ID 
        {
			get 
            {
				return this.myID;
			}
			set 
            {
				try 
                {
					this.myID=(this.GetIDFromName (value.ToString())); 
				}
				catch 
                {
					string err="�޷�ת��"+this.GetType().ToString()+"���룡";
				}
				base.ID=this.myID.ToString();
				base.Name = this.Name;
			}
		}

        /// <summary>
        /// �������Ʒ���ö��
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
		public enuBedReportType GetIDFromName(string Name) 
        {
			enuBedReportType c = new enuBedReportType();
			for(int i=0;i<100;i++) 
            {
				c = (enuBedReportType)i;
				if(c.ToString()==Name) return c;
			}
			return (enuBedReportType)int.Parse(Name);
		}

		/// <summary>
		/// ��������
		/// </summary>
		/// <summary>
		/// ���ȫ���б�
		/// </summary>
		/// <returns>ArrayList(DrugAttribute)</returns>
		public static ArrayList List()
        {
			BedReportType o;
			enuBedReportType e=new enuBedReportType();
			ArrayList alReturn=new ArrayList();
			int i;
			for(i=0;i<=System.Enum.GetValues(e.GetType()).GetUpperBound(0);i++)
            {
				o=new BedReportType();
				o.ID=(enuBedReportType)i;
				o.Memo=i.ToString();
				alReturn.Add(o);
			}
			return alReturn;
		}
        
        /// <summary>
        /// ��¡����
        /// </summary>
        /// <returns></returns>
		public new BedReportType Clone() 
        {
			return base.Clone() as BedReportType;
		}
	}
}
