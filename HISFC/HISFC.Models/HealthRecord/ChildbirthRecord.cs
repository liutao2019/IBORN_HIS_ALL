using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.Models.HealthRecord
{
    /// <summary>
	/// Const<br></br>
	/// [��������: �����¼ʵ��]<br></br>
	/// [�� �� ��: ������]<br></br>
	/// [����ʱ��: 2007-09-03]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class ChildbirthRecord:FS.FrameWork.Models.NeuObject
   {
       #region ����

       /// <summary>
       /// ������Ϣ
       /// </summary>
       private RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();
       /// <summary>
       /// Ӥ���Ա�
       /// </summary>
       private FS.HISFC.Models.Base.EnumSex babySex = FS.HISFC.Models.Base.EnumSex.M;
       /// <summary>
       /// ��������
       /// </summary>
       private FS.HISFC.Models.Base.OperEnvironment oper = new FS.HISFC.Models.Base.OperEnvironment();
       /// <summary>
       /// ���
       /// </summary>
       private int happenNo;
       /// <summary>
       /// �Ƿ���������
       /// </summary>
       private bool isNormalChildbirth;
       /// <summary>
       /// �Ƿ��Ѳ�
       /// </summary>
       private bool isDystocia;
       /// <summary>
       /// �ƻ�������ʽ
       /// </summary>
       private HISFC.Models.Base.Const familyPlanning = new FS.HISFC.Models.Base.Const();
       /// <summary>
       /// �����Ƿ�����
       /// </summary>
       private bool isPerineumBreak;
       /// <summary>
       /// ��������
       /// </summary>
       private FS.HISFC.Models.Base.Const womenKind = new FS.HISFC.Models.Base.Const();
       /// <summary>
       /// �Ƿ�����
       /// </summary>
       private bool isBreak;
       /// <summary>
       /// ���ѳ̶�
       /// </summary>
       private HISFC.Models.Base.Const breakLevel = new FS.HISFC.Models.Base.Const();
       /// <summary>
       /// С������
       /// </summary>
       private decimal babyWeight = 0m;

       #endregion 

       #region ����
       /// <summary>
       /// ������Ϣ
       /// </summary>
       public RADT.PatientInfo Patient
       {
           get
           {
               return this.patient;
           }
           set
           {
               this.patient = value;
           }
       }
       /// <summary>
       /// ���
       /// </summary>
       public int HappenNO
       {
           get
           {
               return this.happenNo;
           }
           set
           {
               this.happenNo = value;
           }
       }
       /// <summary>
       /// Ӥ���Ա�
       /// </summary>
       public FS.HISFC.Models.Base.EnumSex BabySex
       {
           get
           {
               return this.babySex;
           }
           set
           {
               this.babySex = value;
           }
       }
       /// <summary>
       /// ��������
       /// </summary>
       public FS.HISFC.Models.Base.OperEnvironment Oper
       {
           get
           {
               return this.oper;
           }
           set
           {
               this.oper = value;
           }
       }
       /// <summary>
       /// �Ƿ���������
       /// </summary>
       public bool IsNormalChildbirth
       {
           get
           {
               return this.isNormalChildbirth;
           }
           set
           {
               this.isNormalChildbirth = value;
           }
       }

       /// <summary>
       /// �Ƿ��Ѳ�
       /// </summary>
       public bool IsDystocia
       {
           get
           {
               return this.isDystocia;
           }
           set
           {
               this.isDystocia = value;
           }
       }
       /// <summary>
       /// �ƻ�������ʽ
       /// </summary>
       public HISFC.Models.Base.Const FamilyPlanning
       {
           get
           {
               return this.familyPlanning;
           }
           set
           {
               this.familyPlanning = value;
           }
       }
       /// <summary>
       /// �����Ƿ�����
       /// </summary>
       public bool IsPerineumBreak
       {
           get
           {
               return this.isPerineumBreak;
           }
           set
           {
               this.isPerineumBreak = value;
           }
       }
       /// <summary>
       /// ��������
       /// </summary>
       public FS.HISFC.Models.Base.Const WomenKind
       {
           get
           {
               return this.womenKind;
           }
           set
           {
               this.womenKind = value;
           }
       }
       /// <summary>
       /// �Ƿ�����
       /// </summary>
       public bool IsBreak
       {
           get
           {
               return this.isBreak;
           }
           set
           {
               this.isBreak = value;
           }
       }
       /// <summary>
       /// ���ѳ̶�
       /// </summary>
       public FS.HISFC.Models.Base.Const BreakLevel
       {
           get
           {
               return this.breakLevel;
           }
           set
           {
               this.breakLevel = value;
           }
       }
       /// <summary>
       /// С������
       /// </summary>
       public decimal BabyWeight
       {
           get
           {
               return this.babyWeight;
           }
           set
           {
               this.babyWeight = value;
           }
       }

       #endregion

       #region ����

       #region �˽�

       /// <summary>
       /// ��¡����
       /// </summary>
       /// <returns>���ص�ǰʵ���ĸ���</returns>
       public new ChildbirthRecord Clone()
       {
           ChildbirthRecord childbirthRecord = base.Clone() as ChildbirthRecord;

           childbirthRecord.Patient = this.Patient.Clone();

           childbirthRecord.Oper = this.Oper.Clone();

           return childbirthRecord;
       }

       #endregion

       #endregion
   }

  
}
