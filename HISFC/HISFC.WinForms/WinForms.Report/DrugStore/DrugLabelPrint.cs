using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.WinForms.Report.DrugStore
{

	[StructLayout( LayoutKind.Sequential)]
	public struct DOCINFO 
	{
		[MarshalAs(UnmanagedType.LPWStr)]public string pDocName;
		[MarshalAs(UnmanagedType.LPWStr)]public string pOutputFile; 
		[MarshalAs(UnmanagedType.LPWStr)]public string pDataType;
	}

	#region ��ӡ�������
	/// <summary>
	/// Summary description for DirectPrinter.
	/// </summary>
	public class DirectPrinter
	{
		public DirectPrinter()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		public static string printerName = "\\\\168.160.41.135\\Zebra  Z4M (200dpi)";

		[ DllImport( "winspool.drv",CharSet=CharSet.Unicode,ExactSpelling=false,
			  CallingConvention=CallingConvention.StdCall )]
		public static extern long OpenPrinter(string pPrinterName,ref IntPtr phPrinter, int pDefault);
	
		[ DllImport( "winspool.drv",CharSet=CharSet.Unicode,ExactSpelling=false,
			  CallingConvention=CallingConvention.StdCall )]
		public static extern long StartDocPrinter(IntPtr hPrinter, int Level, ref DOCINFO pDocInfo);
		
		[ DllImport( "winspool.drv",CharSet=CharSet.Unicode,ExactSpelling=true,
			  CallingConvention=CallingConvention.StdCall)]
		public static extern long StartPagePrinter(IntPtr hPrinter);
		
		[ DllImport( "winspool.drv",CharSet=CharSet.Ansi,ExactSpelling=true,
			  CallingConvention=CallingConvention.StdCall)]
		public static extern long WritePrinter(IntPtr hPrinter,string data, int buf,ref int pcWritten);
		
		[ DllImport( "winspool.drv" ,CharSet=CharSet.Unicode,ExactSpelling=true,
			  CallingConvention=CallingConvention.StdCall)]
		public static extern long EndPagePrinter(IntPtr hPrinter);	
		
		[ DllImport( "winspool.drv" ,CharSet=CharSet.Unicode,ExactSpelling=true,
			  CallingConvention=CallingConvention.StdCall)]
		public static extern long EndDocPrinter(IntPtr hPrinter);	
		
		[ DllImport( "winspool.drv",CharSet=CharSet.Unicode,ExactSpelling=true,	
			  CallingConvention=CallingConvention.StdCall )]
		public static extern long ClosePrinter(IntPtr hPrinter);
		
		[DllImport("FNTHEX32.DLL", CharSet = CharSet.Auto )]
		public static extern int GETFONTHEX([MarshalAs(UnmanagedType.LPStr)] string outStr,[MarshalAs(UnmanagedType.LPStr)] string fontName,int orientation,int heigth,int width,int boldFlag,int italicFlag, [MarshalAs(UnmanagedType.LPStr)] StringBuilder hexBuf);

		public static void PrintFontToZbl(int x,int y,string strData,int height,int boldFlag)
		{
			PrintFontToZbl(x,y,strData,height,18,boldFlag);

		}
		public static void PrintFontToZbl(int x,int y,string strData,int height,int width,int boldFlag)
		{
			StringBuilder sb = new StringBuilder(21 * 1024);
			int count;
			count = GETFONTHEX(strData,"����",0,height,width,boldFlag,0, sb);

			string cBuf = sb.ToString();
			string imageName = "WinFNT00";

			string temp = cBuf.Substring(3,8);
			cBuf = cBuf.Substring(0,3) + imageName + cBuf.Substring(11);

			SendToPrinter("",cBuf.Substring(0,count),printerName);

			SendToPrinter("","^FO" + x.ToString() + "," + y.ToString() + "^XG" + imageName + "^FS",printerName) ;
		}


		public static void SendToPrinter(string jobName, string printCmd, string printerName)
		{			
			System.IntPtr lhPrinter=new System.IntPtr();
				
			DOCINFO di = new DOCINFO();

			int pcWritten = 0;			
			di.pDocName = jobName;
			di.pDataType = "RAW";				

			OpenPrinter(printerName,ref lhPrinter,0);	
		
			StartDocPrinter(lhPrinter,1,ref di);
			StartPagePrinter(lhPrinter);			
		
			long i = WritePrinter(lhPrinter,printCmd,printCmd.Length,ref pcWritten);								
			if (i == 0)
			{
				System.Windows.Forms.MessageBox.Show("Err");
			}

			EndPagePrinter(lhPrinter);
			EndDocPrinter(lhPrinter);

			ClosePrinter(lhPrinter);
		}


	}

	#endregion

	/// <summary>
	/// DrugLabelPrint ��ժҪ˵����
    /// 
    /// <����˵��>
    ///     1�������ǩ��ӡʵ�� ����Ӧ�ڰ����ӡ�� ͨ����ӡ���ʽʵ��
    /// </����˵��>
	/// </summary>
    public class DrugLabelPrint : FS.HISFC.Models.Pharmacy.IRecipeLabel, FS.HISFC.BizProcess.Interface.Pharmacy.IDrugPrint
	{
		public DrugLabelPrint()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			this.Init();
			
			//ѡ��Ĭ�ϴ�ӡ�����д�ӡ
			System.Drawing.Printing.PrintDocument pr = new System.Drawing.Printing.PrintDocument();
			DirectPrinter.printerName = pr.PrinterSettings.PrinterName;
		}


		/// <summary>
		/// Ƶ�ΰ�����
		/// </summary>
		private FS.FrameWork.Public.ObjectHelper frequencyHelper = null;
		/// <summary>
		/// �÷�������
		/// </summary>
		private FS.FrameWork.Public.ObjectHelper usageHelper = null;
		/// <summary>
		/// �Ƿ���Ҫ���е�λת��
		/// </summary>
		private bool isNeedInvertUnit = true;
		/// <summary>
		/// ��ת��ҩƷ
		/// </summary>
		private System.Collections.Hashtable hsInvertDrug = null;
		/// <summary>
		/// ��ת������ ����С��1
		/// </summary>
		private System.Collections.Hashtable hsInvertNum = null;
		/// <summary>
		/// ��Ҫת���ĵ�λ
		/// </summary>
		private System.Collections.Hashtable hsInvertUnit = null;

        /// <summary>
        /// ��ǩ��ӡ��Ŀ��Ϣ
        /// </summary>
        private static System.Collections.Hashtable hsLabelItem = new Hashtable();


		/// <summary>
		/// ��ʼ����������
		/// </summary>
		private void Init()
		{
			#region �������Ƶ����Ϣ 
			if (this.frequencyHelper == null)
			{
				FS.HISFC.BizLogic.Manager.Frequency frequencyManagement = new FS.HISFC.BizLogic.Manager.Frequency();
				ArrayList alFrequency = frequencyManagement.GetAll("Root");
				if (alFrequency == null)
				{
					MessageBox.Show("��ȡƵ���б�������");
					return;
				}

				this.frequencyHelper = new FS.FrameWork.Public.ObjectHelper(alFrequency);
			}
			#endregion

			FS.HISFC.BizLogic.Manager.Constant c = new FS.HISFC.BizLogic.Manager.Constant();

			#region ��ȡ�����÷�
			if (this.usageHelper == null)
			{
				ArrayList alUsage = c.GetList(FS.HISFC.Models.Base.EnumConstant.USAGE);
				if (alUsage == null)
				{
					MessageBox.Show("��ȡ�÷��б����!");
					return;
				}
				ArrayList tempAl = new ArrayList();
				foreach(FS.FrameWork.Models.NeuObject info in alUsage)
				{
					if (info.Memo != "")
						info.Name = info.Memo;
					tempAl.Add(info);
				}

				this.usageHelper = new FS.FrameWork.Public.ObjectHelper(tempAl);
			}
			#endregion

			#region ��ȡ����
			ArrayList alInvertUnit = c.GetList("InvertUnit");
			if (alInvertUnit != null && alInvertUnit.Count > 0)		//�������������ά�� ˵�����շѴ��շ�ʱ�Ѿ�������ת��
			{
				this.isNeedInvertUnit = false;
				if (this.hsInvertUnit == null)
				{
					this.hsInvertUnit = new Hashtable();
				}
				foreach(FS.HISFC.Models.Base.Const cons in alInvertUnit)
				{
					this.hsInvertUnit.Add(cons.Name,null);
				}
			}
			ArrayList alInvertDrug = c.GetList("InvertDrug");
			if (alInvertDrug != null && alInvertDrug.Count > 0)
			{
				if (this.hsInvertDrug == null)
					this.hsInvertDrug = new Hashtable();
				foreach(FS.HISFC.Models.Base.Const cons in alInvertDrug)
				{
					this.hsInvertDrug.Add(cons.ID,cons.Name);
				}
			}
			ArrayList alInvertNum = c.GetList("InvertNum");
			if(alInvertNum != null && alInvertNum.Count > 0)
			{
				if (this.hsInvertNum == null)
					this.hsInvertNum = new Hashtable();
				foreach(FS.HISFC.Models.Base.Const cons in alInvertNum)
				{
					this.hsInvertNum.Add(cons.ID,cons.Name);
				}
			}
			#endregion
		}


		/// <summary>
		/// ת��������λ����С��λ��ʾ
		/// </summary>
		/// <param name="doseOnce">ÿ�μ���</param>
		/// <param name="baseDose">��������</param>
		/// <returns>������Ӧ�ı�ʾ�ַ��� �Դ���1�İ�С����ʾ С��1�İ�������ʽ��ʾ</returns>
		protected string DoseToMin(decimal doseOnce,decimal baseDose)
		{
			if (baseDose == 0)
				baseDose = 1;
			decimal result = doseOnce / baseDose;
			decimal maxCD = 1;

			if (this.hsInvertNum != null)
			{
				if (this.hsInvertNum.ContainsKey(result.ToString()))
				{
					return this.hsInvertNum[result.ToString()].ToString();
				}
			}

            ////added by zengft 2007-11-19
            ////����1/3��2/3������
            //// System.Math.Round(result, 2)������hsInvertNum�У������¸�Ϊ��ȷ
            //if(doseOnce == System.Math.Round(baseDose / 3, 2))
            //{
            //    return "1/3";
            //}
            //if(doseOnce == System.Math.Round(2 * baseDose / 3, 2))
            //{
            //    return "2/3";
            //}
            ////end added

			if (result >= 1)
				return System.Math.Round(result,2).ToString();
			else  //���㹫Լ�� ��ʾΪ������ʽ
			{
				maxCD = this.MaxCD(doseOnce,baseDose);
				if (maxCD > 10)
				{
					return System.Math.Round(result,2).ToString();
				}
				return (doseOnce / maxCD).ToString() + "/" + (baseDose / maxCD).ToString();
			}
		}
		public decimal MaxCD(decimal i, decimal j) 
		{ 
			decimal a,b,temp; 
			if(i>j) 
			{ 
				a = i; 
				b = j; 
			} 
			else 
			{ 
				b = i; 
				a = j; 
			} 
			temp = a % b; 
			while(temp!=0) 
			{ 
				a = b; 
				b = temp; 
				temp = a % b; 
			} 
			return b; 
		} 


		/// <summary>
		/// ��ȡҩƷ��Ϣ
		/// </summary>
		/// <param name="drugDept">ҩ������</param>
		/// <param name="drugCode">ҩƷ����</param>
		/// <param name="item">����ҩƷʵ��</param>
		private void GetRecipeLabelItem(string drugDept,string drugCode,ref FS.HISFC.Models.Pharmacy.Item item)
		{
            FS.HISFC.Models.Pharmacy.Item recipeLabelItem = new FS.HISFC.Models.Pharmacy.Item();
            if (hsLabelItem.ContainsKey(drugDept + drugCode))
            {
                recipeLabelItem = hsLabelItem[drugDept + drugCode] as FS.HISFC.Models.Pharmacy.Item;               
            }
            else
            {
                #region ��ȡ���� 

                FS.FrameWork.Management.DataBaseManger dataBaseMgr = new FS.FrameWork.Management.DataBaseManger();
                string strSql = @"select   t.trade_name,t.english_name,t.pack_unit,t.drug_quality,t.caution,t.base_dose,s.place_code,t.split_type,t.regular_name
								from   pha_com_stockinfo s,pha_com_baseinfo t
								where  t.drug_code = s.drug_code
								and    s.drug_dept_code = '{0}'
								and    s.drug_code = '{1}'";
                strSql = string.Format(strSql, drugDept, drugCode);
                if (dataBaseMgr.ExecQuery(strSql) != -1)
                {
                    if (dataBaseMgr.Reader.Read())
                    {
                        recipeLabelItem.Name = dataBaseMgr.Reader[0].ToString();
                        recipeLabelItem.NameCollection.EnglishName = dataBaseMgr.Reader[1].ToString();
                        recipeLabelItem.PackUnit = dataBaseMgr.Reader[2].ToString();
                        recipeLabelItem.Quality.ID = dataBaseMgr.Reader[3].ToString();
                        recipeLabelItem.Product.Caution = dataBaseMgr.Reader[4].ToString();
                        recipeLabelItem.BaseDose = FS.FrameWork.Function.NConvert.ToDecimal(dataBaseMgr.Reader[5].ToString());
                        recipeLabelItem.User01 = dataBaseMgr.Reader[6].ToString();
                        recipeLabelItem.User02 = dataBaseMgr.Reader[7].ToString();
                        recipeLabelItem.NameCollection.RegularName = dataBaseMgr.Reader[8].ToString();
                    }
                }

                hsLabelItem.Add(drugDept + drugCode, recipeLabelItem);

                #endregion
            }

            if (recipeLabelItem != null)
            {
                item.Name = recipeLabelItem.Name;
                item.NameCollection.EnglishName = recipeLabelItem.NameCollection.EnglishName;
                item.PackUnit = recipeLabelItem.PackUnit;
                item.Quality.ID = recipeLabelItem.Quality.ID;
                item.Product.Caution = recipeLabelItem.Product.Caution;
                item.BaseDose = recipeLabelItem.BaseDose;
                item.User01 = recipeLabelItem.User01;
                item.User02 = recipeLabelItem.User02;
                item.NameCollection.RegularName = recipeLabelItem.NameCollection.RegularName;
            }
		}


		public void SentDirCmdToPrinter(string dirPrintCmd)
		{
			DirectPrinter.SendToPrinter("PrintLabel",dirPrintCmd,DirectPrinter.printerName);
		}
		

		#region IRecipeLabel ��Ա
		/// <summary>
		/// ������Ϣ
		/// </summary>
		private FS.HISFC.Models.Registration.Register patientInfo = null;
		/// <summary>
		/// ������Ϣ
		/// </summary>
		public FS.HISFC.Models.Registration.Register PatientInfo
		{
			get
			{
				// TODO:  ��� ucComboRecipeLabel.PatientInfo getter ʵ��
				return this.patientInfo;
			}
			set
			{
				// TODO:  ��� ucComboRecipeLabel.PatientInfo setter ʵ��
				this.patientInfo = value;
			}
		}


		/// <summary>
		/// ���δ�����ӡҳ��
		/// </summary>
		protected decimal labelNum;

		/// <summary>
		/// ���δ�����ӡ��ҳ��
		/// </summary>
		protected decimal labelTotNum;
		/// <summary>
		/// һ�δ�ӡҩƷ������ҳ��
		/// </summary>
		protected decimal drugTotNum;
		/// <summary>
		/// һ�δ�ӡҩƷ������ҳ��
		/// </summary>
		public decimal DrugTotNum
		{
			set
			{
				this.drugTotNum = value;
				this.labelNum = 1;
			}
		}

		/// <summary>
		/// ���δ�����ӡ��ҳ��
		/// </summary>
		public decimal LabelTotNum
		{
			set
			{
				this.labelTotNum = value;
				this.labelNum = 1;
			}
		}


		/// <summary>
		/// ��ӡ����ҩƷ
		/// </summary>
		/// <param name="applyOut"></param>
		public void AddSingle(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
		{		
			#region ��ʼ����������ʾ
			FS.HISFC.Models.Pharmacy.Item item = applyOut.Item;
			this.GetRecipeLabelItem(applyOut.StockDept.ID,applyOut.Item.ID,ref item);

			this.SentDirCmdToPrinter("^XA^LH0,10^PR4^CW1,E:MSUNG.FNT^FS^SEE:GB.DAT^FS");

			//ҩƷ����[���]  ����Һ��ʾӢ������ ����Ҫ����
            //if (applyOut.Item.Quality.ID == "T" && applyOut.Item.NameCollection.EnglishName != "")
            //    applyOut.Item.Name = applyOut.Item.NameCollection.EnglishName;

			//modify by zengft 2007-5-11
			//string name = applyOut.Item.Name + "[" + applyOut.Item.Specs + "]";//δ������Ʒ��ǰ
			//ͨ����������9λ
            string name = "";
            //if(name.Length > 9)
            //{
            //    name = name.Substring(0,9);
            //}
			//name += "("+applyOut.Item.Name+")" + applyOut.Item.Specs;            
            string regularName = "";
            if (applyOut.Item.NameCollection.RegularName != "" && applyOut.Item.NameCollection.RegularName != "")
            {
                regularName = "(" + applyOut.Item.NameCollection.RegularName.Substring(0, 2) + ")";
            }
            name = applyOut.Item.Name + regularName + "[" + applyOut.Item.Specs + "��" + Function.DrugDosage.GetStaticDosage(applyOut.Item.ID) + "]";

			if (name.Length > 28)	//δ������Ʒ��ǰ30λ
				name = name.Substring(0,28);	//δ������Ʒ��ǰ30λ
			//end modify

			DirectPrinter.PrintFontToZbl(20,46,name,45,11,1);
			#endregion

			//���� ��λ
			string numUnit = "";
			if (item.User02 == "1")
			{
				string strNum = (applyOut.Operation.ApplyQty / applyOut.Item.PackQty).ToString();
				if(strNum.IndexOf(".") == -1)
				{
                    numUnit = System.Math.Round((applyOut.Operation.ApplyQty / applyOut.Item.PackQty), 2).ToString() + applyOut.Item.PackUnit;
				}
				else
				{
                    numUnit = applyOut.Operation.ApplyQty.ToString() + applyOut.Item.MinUnit;
				}
			}
			else
			{
                numUnit = applyOut.Operation.ApplyQty.ToString() + applyOut.Item.MinUnit;
			}

			DirectPrinter.PrintFontToZbl(560,50,numUnit,45,12,1);

			string doseOnce = applyOut.DoseOnce.ToString();
			string doseUnit = applyOut.Item.DoseUnit.ToString();

			if (doseOnce == "0" || doseOnce == "")
			{
				doseOnce = "��ҽ��";
				doseUnit = "";
			}
			else if(applyOut.Item.BaseDose != 0)
			{
				//modify by zengft 2007-5-14 ����
				//��ӡ�add by zengft 2007-5-14��
//				if (this.isNeedInvertUnit)
//				{
//					if (applyOut.Item.MinUnit == "��" || applyOut.Item.MinUnit == "Ƭ" || applyOut.Item.MinUnit == "��")
//					{
//						doseOnce = this.DoseToMin(applyOut.DoseOnce,applyOut.Item.BaseDose).ToString();
//						doseUnit = applyOut.Item.MinUnit;
//					}
//				
//					if (this.hsInvertDrug != null && this.hsInvertDrug.ContainsKey(applyOut.Item.ID))
//					{	
//						doseOnce = this.DoseToMin(applyOut.DoseOnce,applyOut.Item.BaseDose).ToString();
//						doseUnit = applyOut.Item.MinUnit;
//					}
//				}
				//end modify

				//add by zengft 2007-5-14
				
				if(applyOut.Item.MinUnit != applyOut.Item.DoseUnit)
				{
					if (this.hsInvertDrug != null && this.hsInvertDrug.ContainsKey(applyOut.Item.ID))
					{	
						doseOnce = this.DoseToMin(applyOut.DoseOnce,applyOut.Item.BaseDose).ToString();
						doseUnit = applyOut.Item.MinUnit;
					}
					else
					{
						//Ӧ��������Ӣ�ĵ�λ��д���ˣ���ȡ�ٶ�
						bool needDoseToMin = false;
						switch(applyOut.Item.DoseUnit)
						{
							case "MIU":
							case "au":
							case "cat":
							case "g":
							case "iu":
							case "kg":
							case "ku":
							case "mg":
							case "ml":
							case "pe":
							case "u":
							case "ug":
							case "��u":						
								needDoseToMin = true;
								break;
						}
						if(needDoseToMin)
						{
							doseOnce = this.DoseToMin(applyOut.DoseOnce,applyOut.Item.BaseDose).ToString();
							doseUnit = applyOut.Item.MinUnit;
						}
					}
				}
				//��������ת�����У�С��1�ķ�����ʽ������1��С����ʽ��С��1��С����ʽ
				//end add

				//С��1��С��ת��Ϊ��Ӧ������ʽ
				if (this.hsInvertNum != null && this.hsInvertNum.ContainsKey(doseOnce))
				{
					doseOnce = this.hsInvertNum[doseOnce].ToString();
				}
					//add by zengft 2007-5-14
				else
				{
					//������Щ������������û�г���ת���ķ���Ƭ������ԭ
					string intdata = (applyOut.DoseOnce/applyOut.Item.BaseDose).ToString();
					if( intdata.IndexOf(".") != -1 && !this.hsInvertUnit.Contains(applyOut.Item.MinUnit))
					{
						doseOnce = applyOut.DoseOnce.ToString();
						doseUnit = applyOut.Item.DoseUnit.ToString();
					}
				}				
				//end add
			}

			string drugUseInfo = "";
			if (doseOnce == "��ҽ��")
			{
				drugUseInfo = doseOnce;
			}
			else
			{
				try
				{
					if (this.frequencyHelper.GetName(applyOut.Frequency.ID) == "����")
						drugUseInfo = string.Format("{0}  {1}",this.usageHelper.GetName(applyOut.Usage.ID),this.frequencyHelper.GetName(applyOut.Frequency.ID));
					else
						drugUseInfo = string.Format("{0}  {1}  ÿ�� {2} {3}",this.usageHelper.GetName(applyOut.Usage.ID),this.frequencyHelper.GetName(applyOut.Frequency.ID),doseOnce,doseUnit);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			DirectPrinter.PrintFontToZbl(20,110,drugUseInfo,43,11,1);

			#region ������Ϣ��ʾ

			//ע������
			string caution = applyOut.Item.Product.Caution;
            if (caution != null)
            {
                if (caution.Length > 23)
                    caution = caution.Substring(0, 23);
            }
            else
            {
                caution = "";
            }
			DirectPrinter.PrintFontToZbl(20,160,caution,24,12,1);

			//���� �Ա� ���� ��ҩ���ں�
			string sexStr = "";
			if (this.patientInfo.Sex.Name != "δ֪")
				sexStr = this.patientInfo.Sex.Name;
			string patiInfo = "";
			if (this.patientInfo.PID.CardNO != null && this.patientInfo.PID.CardNO.Substring(0,1) == "9")
				patiInfo = string.Format("{0} {1}",this.patientInfo.Name,sexStr);	//����[�Ա�] ����			
			else
				patiInfo = string.Format("{0} {1} {2}",this.patientInfo.Name,sexStr,this.patientInfo.Age);	//����[�Ա�] ����			
			DirectPrinter.PrintFontToZbl(20,230,patiInfo,35,15,1);

			//��ҩ���ں�
			string sendWindow = applyOut.SendWindow;
			DirectPrinter.PrintFontToZbl(450,240,sendWindow,24,15,1);

			//����ʱ��
			string applyDate = applyOut.Operation.ApplyOper.OperTime.ToString();
			//this.SentDirCmdToPrinter("^CI10^FO30,278^A1N,24,24^FD" + applyDate + "^FS");
            //DirectPrinter.PrintFontToZbl(30, 278, applyDate, 24, 1);
            this.SentDirCmdToPrinter("^CI10^FO30,278^A1N,16,12^FD" + applyDate + "^FS");
			//ҳ��
			string page = labelNum + "/" + this.labelTotNum;
			this.SentDirCmdToPrinter("^CI10^FO300,250^A1N,28,18^FD" + page + "^FS");
			//����
			string barCode = applyOut.RecipeNO;
			this.SentDirCmdToPrinter("^BY2,3.0^FS ^FT650,60^BCR,80,Y,N,Y^FD>:" + barCode + "^FS");

			//��λ��
			//DirectPrinter.PrintFontToZbl(450,280,applyOut.Item.User01,24,1);
			DirectPrinter.PrintFontToZbl(400,280,applyOut.Item.User01,24,1);
			//ҽԺ����
			//																					    8000 ��� 2 �߶� 3000 �ڶ�
			//this.SentDirCmdToPrinter("^CI14^FO100,10^A1N,24,48^FD��ɽ��ѧ��������ҽԺ^FS^CI10^FO0,28^GB8000,2,3000,B^FS");
            DirectPrinter.PrintFontToZbl(150, 5, "��ɽ��ѧ��������ҽԺ", 24,18, 1);
            
			this.labelNum = this.labelNum + 1;

			#endregion
		}

		/// <summary>
		/// ��ӡһ��ҩƷ
		/// </summary>
		/// <param name="alCombo"></param>
		public void AddCombo(ArrayList alCombo)
		{
			int iNum = 0;
			bool isPrintStoreCode = false;
			for(int i = 0;i < alCombo.Count;i++)
			{
				FS.HISFC.Models.Pharmacy.ApplyOut applyOut = alCombo[i] as FS.HISFC.Models.Pharmacy.ApplyOut;
				//����ҩƷ��Ϣ
				FS.HISFC.Models.Pharmacy.Item item = applyOut.Item;
				this.GetRecipeLabelItem(applyOut.StockDept.ID,applyOut.Item.ID,ref item);

				if (iNum == 0)
				{
					this.SentDirCmdToPrinter("^XA");
					this.SentDirCmdToPrinter("^LH0,10");
					this.SentDirCmdToPrinter("^CW1,E:MSUNG24.FNT^FS");
					this.SentDirCmdToPrinter("^SEE:GB.DAT^FS");
				}

				//����  ����Һ��ʾӢ������ ���ô���
                //if (applyOut.Item.Quality.ID == "T" && applyOut.Item.NameCollection.EnglishName != "")
                //    applyOut.Item.Name = applyOut.Item.NameCollection.EnglishName;

				//modify by zengft 2007-5-11
				//string drugInfo = applyOut.Item.Name + "[" + applyOut.Item.Specs + "]";//δ����ͨ����ǰ
				//ͨ��������Ϊ5λ
				//string drugInfo = applyOut.Item.NameCollection.RegularName;
                string drugInfo = "";
                //if(drugInfo.Length > 5)
                //{
                //    drugInfo = drugInfo.Substring(0,5);
                //}
				//drugInfo += "("+applyOut.Item.Name+")"+applyOut.Item.Specs;

                string regularName = "";
                if (applyOut.Item.NameCollection.RegularName != "" && applyOut.Item.NameCollection.RegularName != "")
                {
                    regularName = "(" + applyOut.Item.NameCollection.RegularName.Substring(0, 2) + ")";
                }

                drugInfo = applyOut.Item.Name + regularName + "[" + Function.DrugDosage.GetStaticDosage(applyOut.Item.ID) + "��" + applyOut.Item.Specs + "]";
				if (drugInfo.Length > 23)//δ����ͨ����ǰ25λ
					drugInfo = drugInfo.Substring(0,23);//δ����ͨ����ǰ25λ
				//end modify

				DirectPrinter.PrintFontToZbl(20,iNum * 50,drugInfo,40,11,1);
				//ÿ����
				string dose = "ÿ��" + applyOut.DoseOnce.ToString() + applyOut.Item.DoseUnit;
				DirectPrinter.PrintFontToZbl(445,iNum * 50,dose,40,11,1);
				//���� ��λ
                string numUnit = applyOut.Operation.ApplyQty.ToString() + applyOut.Item.MinUnit;//����������λ
				DirectPrinter.PrintFontToZbl(580,iNum * 50,numUnit,40,12,1);
			
				if (iNum == 0)
				{
					//Ժע
					string cau =string.Format("Ժע��{0}",applyOut.ExecNO);			
					DirectPrinter.PrintFontToZbl(30,160,cau,36,12,1);
					//�÷�
					string use = this.usageHelper.GetName(applyOut.Usage.ID);	
					DirectPrinter.PrintFontToZbl(30,200,use,36,12,1);
					//Ƶ��	
					string fre = this.frequencyHelper.GetName(applyOut.Frequency.ID);
					if (fre.Length > 4)
						fre = fre.Substring(0,4);
					DirectPrinter.PrintFontToZbl(230,200,fre,36,12,1);

					//���� �Ա� ���� ��ҩ���ں�
					string sexStr = "";
					if (this.patientInfo.Sex.Name != "δ֪")
						sexStr = this.patientInfo.Sex.Name;
					string patiInfo = "";
					if (this.patientInfo.PID.CardNO.Substring(0,1) == "9")
						patiInfo = string.Format("{0} {1}",this.patientInfo.Name,sexStr);	//����[�Ա�] ����			
					else
						patiInfo = string.Format("{0} {1} {2}",this.patientInfo.Name,sexStr,this.patientInfo.Age);	//����[�Ա�] ����			
					DirectPrinter.PrintFontToZbl(30,235,patiInfo,35,1);

					//��ҩ���ں�
					string sendWindow = applyOut.User01;
					DirectPrinter.PrintFontToZbl(450,240,sendWindow,24,15,1);

					//ҳ��
					string page = labelNum + "/" + this.labelTotNum;
					//this.SentDirCmdToPrinter("^CI10^FO300,250^A1N,56,36^FD" + page + "^FS");
                    this.SentDirCmdToPrinter("^CI10^FO300,250^A1N,28,18^FD" + page + "^FS");
					//����
					string barCode = applyOut.RecipeNO;
					this.SentDirCmdToPrinter("^BY2,3.0^FS ^FT650,60^BCR,80,Y,N,Y^FD>:" + barCode + "^FS");
					//��λ��
					//DirectPrinter.PrintFontToZbl(450,280,applyOut.Item.User01,24,1);
				}
				if(applyOut.Item.User01 != null && !isPrintStoreCode && 
					(applyOut.Item.User01.Trim().ToUpper() != "O" && applyOut.Item.User01.Trim().ToUpper() != "0"))
				{
					isPrintStoreCode = true;
					//MessageBox.Show(applyOut.Item.User01);
					//DirectPrinter.PrintFontToZbl(450,280,applyOut.Item.User01,24,1);
					DirectPrinter.PrintFontToZbl(400,280,applyOut.Item.User01,24,1);
				}

				//��¼�Ѵ�ӡ���� �ѳ���һҳ������ʾ��ҩ������ ��ȥ���л�����Ϣ �����÷���ע������
				iNum = iNum + 1;
				if (alCombo.Count > 3)
				{
					if (i == alCombo.Count - 1)
					{
						//����ʱ��
						string applyDate = applyOut.Operation.ApplyOper.OperTime.ToString();;
//						DirectPrinter.PrintFontToZbl(30,278,applyDate,24,12,1);
                        DirectPrinter.PrintFontToZbl(30, 278, applyDate, 16, 12, 1);
                        //this.SentDirCmdToPrinter("^CI10^FO30,278^A1N,16,12^FD" + applyDate + "^FS");
					}
					else
					{
						if (iNum >= 3)
						{
							//����ʱ��
							string applyDate = "�� " + applyOut.Operation.ApplyOper.OperTime.ToString();
							//DirectPrinter.PrintFontToZbl(30,278,applyDate,24,12,1);
                            DirectPrinter.PrintFontToZbl(30, 278, applyDate, 16, 12, 1);

							this.Print();
							iNum = 0;
						}
					}
				}
				else
				{
					//����ʱ��
					string applyDate = applyOut.Operation.ApplyOper.OperTime.ToString();
					//DirectPrinter.PrintFontToZbl(30,278,applyDate,24,12,1);
                    DirectPrinter.PrintFontToZbl(30, 278, applyDate, 16, 12, 1);
				}
			}
			
			this.labelNum = this.labelNum + 1;
		}


		public void Print()
		{
			// TODO:  ��� DrugLabelPrint.Print ʵ��
			this.SentDirCmdToPrinter("^XZ");
		}


		public void AddAllData(ArrayList al)
		{
			// TODO:  ��� DrugLabelPrint.AddAllData ʵ��
		}

		#endregion

        #region IDrugPrint ��Ա

        public void AddAllData(ArrayList al, FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void AddAllData(ArrayList al, FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public FS.HISFC.Models.RADT.PatientInfo InpatientInfo
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public FS.HISFC.Models.Registration.Register OutpatientInfo
        {
            get
            {
                return this.patientInfo;
            }
            set
            {
                this.patientInfo = value;
            }
        }

        public void Preview()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}


#region ԭ����������ʽ�Ĵ�ӡ����
/*
 * 
 * using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Local.ZsHis
{
	/// <summary>
	/// ucComboRecipeLabel ��ժҪ˵����
	/// </summary>
	public class ucComboRecipeLabel : System.Windows.Forms.UserControl,FS.HISFC.Models.Pharmacy.IRecipeLabel
	{
		private System.Windows.Forms.Label lbBarCode;
		private FarPoint.Win.Spread.FpSpread fpSpread1;
		private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
		private FarPoint.Win.Spread.SheetView fpSpread1_Sheet2;
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ucComboRecipeLabel()
		{
			// �õ����� Windows.Forms ���������������ġ�
			InitializeComponent();

			// TODO: �� InitializeComponent ���ú�����κγ�ʼ��

			this.Init();

		}

		/// <summary> 
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

#region �����������ɵĴ���
		/// <summary> 
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭�� 
		/// �޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("zh-CN", false);
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ucComboRecipeLabel));
			this.lbBarCode = new System.Windows.Forms.Label();
			this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
			this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
			this.fpSpread1_Sheet2 = new FarPoint.Win.Spread.SheetView();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet2)).BeginInit();
			this.SuspendLayout();
			// 
			// lbBarCode
			// 
			this.lbBarCode.BackColor = System.Drawing.SystemColors.Control;
			this.lbBarCode.Font = new System.Drawing.Font("Code39 Text", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbBarCode.Location = new System.Drawing.Point(215, 104);
			this.lbBarCode.Name = "lbBarCode";
			this.lbBarCode.Size = new System.Drawing.Size(149, 43);
			this.lbBarCode.TabIndex = 1;
			this.lbBarCode.Text = "*0123456789*";
			this.lbBarCode.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// fpSpread1
			// 
			this.fpSpread1.BackColor = System.Drawing.Color.White;
			this.fpSpread1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.fpSpread1.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
			this.fpSpread1.Location = new System.Drawing.Point(5, 9);
			this.fpSpread1.Name = "fpSpread1";
			this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
																				   this.fpSpread1_Sheet1,
																				   this.fpSpread1_Sheet2});
			this.fpSpread1.Size = new System.Drawing.Size(349, 139);
			this.fpSpread1.TabIndex = 3;
			this.fpSpread1.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
			this.fpSpread1.ActiveSheetIndex = 1;
			// 
			// fpSpread1_Sheet1
			// 
			this.fpSpread1_Sheet1.Reset();
			this.fpSpread1_Sheet1.ColumnCount = 4;
			this.fpSpread1_Sheet1.RowCount = 7;
			this.fpSpread1_Sheet1.ActiveRowIndex = 5;
			this.fpSpread1_Sheet1.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.Both, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, false, false);
			this.fpSpread1_Sheet1.Cells.Get(0, 0).ColumnSpan = 2;
			this.fpSpread1_Sheet1.Cells.Get(0, 0).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Cells.Get(0, 0).Text = "������ĳ����ע��Һ(10g/5֧/1��)";
			this.fpSpread1_Sheet1.Cells.Get(0, 1).Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Cells.Get(0, 2).Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Cells.Get(0, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
			this.fpSpread1_Sheet1.Cells.Get(0, 2).Text = "ÿ��20��";
			this.fpSpread1_Sheet1.Cells.Get(0, 3).Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Cells.Get(0, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
			this.fpSpread1_Sheet1.Cells.Get(0, 3).Text = "800ƿ";
			this.fpSpread1_Sheet1.Cells.Get(1, 0).ColumnSpan = 2;
			this.fpSpread1_Sheet1.Cells.Get(1, 0).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Cells.Get(1, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
			this.fpSpread1_Sheet1.Cells.Get(1, 0).Text = "���ֻ���Һ[100ml:2g]";
			this.fpSpread1_Sheet1.Cells.Get(1, 1).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Cells.Get(1, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
			this.fpSpread1_Sheet1.Cells.Get(1, 2).Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Cells.Get(1, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
			this.fpSpread1_Sheet1.Cells.Get(1, 2).Text = "ÿ��100ml";
			this.fpSpread1_Sheet1.Cells.Get(1, 3).Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Cells.Get(1, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
			this.fpSpread1_Sheet1.Cells.Get(1, 3).Text = "200ƿ";
			this.fpSpread1_Sheet1.Cells.Get(2, 0).ColumnSpan = 2;
			this.fpSpread1_Sheet1.Cells.Get(2, 0).Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Cells.Get(2, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
			this.fpSpread1_Sheet1.Cells.Get(2, 0).Text = "��������[50mg:5ml]";
			this.fpSpread1_Sheet1.Cells.Get(2, 1).Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Cells.Get(2, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
			this.fpSpread1_Sheet1.Cells.Get(2, 2).Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Cells.Get(2, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
			this.fpSpread1_Sheet1.Cells.Get(2, 2).Text = "ÿ��50mg";
			this.fpSpread1_Sheet1.Cells.Get(2, 3).Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Cells.Get(2, 3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
			this.fpSpread1_Sheet1.Cells.Get(2, 3).Text = "230֧";
			this.fpSpread1_Sheet1.Cells.Get(3, 0).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Cells.Get(3, 0).Text = "������ע";
			this.fpSpread1_Sheet1.Cells.Get(3, 1).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Cells.Get(3, 1).Text = "ÿ��һ��";
			this.fpSpread1_Sheet1.Cells.Get(3, 2).ColumnSpan = 2;
			this.fpSpread1_Sheet1.Cells.Get(3, 2).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Cells.Get(3, 2).Text = "Ժע��2��";
			this.fpSpread1_Sheet1.Cells.Get(4, 0).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Cells.Get(4, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
			this.fpSpread1_Sheet1.Cells.Get(4, 0).Text = "��������[�Ա�]";
			this.fpSpread1_Sheet1.Cells.Get(4, 1).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Cells.Get(4, 1).Text = "100��";
			this.fpSpread1_Sheet1.Cells.Get(4, 2).ColumnSpan = 2;
			this.fpSpread1_Sheet1.Cells.Get(4, 2).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Cells.Get(4, 2).Text = "BMW999";
			this.fpSpread1_Sheet1.Cells.Get(5, 0).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Cells.Get(5, 0).Text = "��ҩ���ںŶ�ʮ��";
			this.fpSpread1_Sheet1.Cells.Get(5, 1).ColumnSpan = 2;
			this.fpSpread1_Sheet1.Cells.Get(5, 1).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Cells.Get(6, 0).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Cells.Get(6, 0).Text = "��2006-04-10 12:23:52";
			this.fpSpread1_Sheet1.Cells.Get(6, 1).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Cells.Get(6, 1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
			this.fpSpread1_Sheet1.Cells.Get(6, 1).Text = "8/9";
			this.fpSpread1_Sheet1.Cells.Get(6, 2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
			this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
			this.fpSpread1_Sheet1.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
			this.fpSpread1_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
			this.fpSpread1_Sheet1.Columns.Get(0).Width = 129F;
			this.fpSpread1_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
			this.fpSpread1_Sheet1.Columns.Get(1).Width = 99F;
			this.fpSpread1_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
			this.fpSpread1_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
			this.fpSpread1_Sheet1.Columns.Get(2).Width = 66F;
			this.fpSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
			this.fpSpread1_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
			this.fpSpread1_Sheet1.Columns.Get(3).Width = 45F;
			this.fpSpread1_Sheet1.RowHeader.Cells.Get(3, 0).Text = "4";
			this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
			this.fpSpread1_Sheet1.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
			this.fpSpread1_Sheet1.RowHeader.DefaultStyle.Parent = "HeaderDefault";
			this.fpSpread1_Sheet1.RowHeader.Visible = false;
			this.fpSpread1_Sheet1.Rows.Get(0).Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Rows.Get(1).Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Rows.Get(2).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet1.Rows.Get(3).Height = 18F;
			this.fpSpread1_Sheet1.Rows.Get(4).Height = 18F;
			this.fpSpread1_Sheet1.Rows.Get(5).Height = 18F;
			this.fpSpread1_Sheet1.Rows.Get(6).Height = 18F;
			this.fpSpread1_Sheet1.SheetCornerStyle.BackColor = System.Drawing.Color.White;
			this.fpSpread1_Sheet1.SheetCornerStyle.Parent = "HeaderDefault";
			this.fpSpread1_Sheet1.SheetName = "Sheet1";
			// 
			// fpSpread1_Sheet2
			// 
			this.fpSpread1_Sheet2.Reset();
			this.fpSpread1_Sheet2.ColumnCount = 4;
			this.fpSpread1_Sheet2.RowCount = 7;
			this.fpSpread1_Sheet2.ActiveSkin = new FarPoint.Win.Spread.SheetSkin("CustomSkin1", System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.LightGray, FarPoint.Win.Spread.GridLines.None, System.Drawing.Color.White, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, System.Drawing.Color.Empty, false, false, false, false, false);
			this.fpSpread1_Sheet2.Cells.Get(0, 0).ColumnSpan = 4;
			this.fpSpread1_Sheet2.Cells.Get(0, 0).Font = new System.Drawing.Font("�����п�", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet2.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
			this.fpSpread1_Sheet2.Cells.Get(0, 0).Text = "��ɽ��ѧ������һҽԺ";
			this.fpSpread1_Sheet2.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
			this.fpSpread1_Sheet2.Cells.Get(1, 0).ColumnSpan = 3;
			this.fpSpread1_Sheet2.Cells.Get(1, 0).Font = new System.Drawing.Font("����", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet2.Cells.Get(1, 0).Text = "����ƽ�����[10mg:10ml]";
			this.fpSpread1_Sheet2.Cells.Get(1, 3).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet2.Cells.Get(1, 3).Text = "1000ƿ";
			this.fpSpread1_Sheet2.Cells.Get(2, 0).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet2.Cells.Get(2, 0).Text = "�����������";
			this.fpSpread1_Sheet2.Cells.Get(2, 1).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet2.Cells.Get(2, 1).Text = "ÿ������";
			this.fpSpread1_Sheet2.Cells.Get(2, 2).ColumnSpan = 2;
			this.fpSpread1_Sheet2.Cells.Get(2, 2).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet2.Cells.Get(2, 2).Text = "ÿ��1ml";
			this.fpSpread1_Sheet2.Cells.Get(3, 0).ColumnSpan = 4;
			this.fpSpread1_Sheet2.Cells.Get(3, 0).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet2.Cells.Get(3, 0).Text = "ע�����������";
			this.fpSpread1_Sheet2.Cells.Get(4, 2).ColumnSpan = 2;
			this.fpSpread1_Sheet2.Cells.Get(4, 2).Text = "BMW012";
			this.fpSpread1_Sheet2.Cells.Get(5, 0).Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet2.Cells.Get(5, 0).Text = "������[δ֪]20�� ��ҩ������";
			this.fpSpread1_Sheet2.Cells.Get(5, 1).Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet2.Cells.Get(5, 1).Text = "��ҩ������";
			this.fpSpread1_Sheet2.Cells.Get(6, 0).ParseFormatInfo = ((System.Globalization.DateTimeFormatInfo)(cultureInfo.DateTimeFormat.Clone()));
			this.fpSpread1_Sheet2.Cells.Get(6, 0).ParseFormatString = "yyyy-MM-dd H:mm:ss";
			this.fpSpread1_Sheet2.Cells.Get(6, 0).Text = "2006-06-12 10:35:28";
			this.fpSpread1_Sheet2.Cells.Get(6, 1).Text = "10/10";
			this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
			this.fpSpread1_Sheet2.ColumnHeader.DefaultStyle.Parent = "HeaderDefault";
			this.fpSpread1_Sheet2.Columns.Get(0).Width = 138F;
			this.fpSpread1_Sheet2.Columns.Get(1).Width = 90F;
			this.fpSpread1_Sheet2.Columns.Get(2).Width = 62F;
			this.fpSpread1_Sheet2.Columns.Get(3).Width = 49F;
			this.fpSpread1_Sheet2.RowHeader.Columns.Default.Resizable = false;
			this.fpSpread1_Sheet2.RowHeader.DefaultStyle.BackColor = System.Drawing.Color.White;
			this.fpSpread1_Sheet2.RowHeader.DefaultStyle.Parent = "HeaderDefault";
			this.fpSpread1_Sheet2.RowHeader.Visible = false;
			this.fpSpread1_Sheet2.Rows.Get(2).Font = new System.Drawing.Font("����", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.fpSpread1_Sheet2.Rows.Get(3).Height = 18F;
			this.fpSpread1_Sheet2.Rows.Get(4).Height = 18F;
			this.fpSpread1_Sheet2.Rows.Get(5).Height = 18F;
			this.fpSpread1_Sheet2.Rows.Get(6).Height = 17F;
			this.fpSpread1_Sheet2.SheetCornerStyle.BackColor = System.Drawing.Color.White;
			this.fpSpread1_Sheet2.SheetCornerStyle.Parent = "HeaderDefault";
			this.fpSpread1_Sheet2.SheetName = "Sheet2";
			// 
			// ucComboRecipeLabel
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.fpSpread1);
			this.Controls.Add(this.lbBarCode);
			this.Name = "ucComboRecipeLabel";
			this.Size = new System.Drawing.Size(376, 164);
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet2)).EndInit();
			this.ResumeLayout(false);

		}
#endregion

//		#region  ֽ������
//		/*
//		 * �� 900 �� 400
//		 * �ұߴ�ӡ��ȫ ����ȵ���
//		 * �����ӡ���� ���߶ȵ���
//		 * 16	RecipeLabel	RecipeLabel	400	900	ALL	001406	2006-6-1 20:18:57	������ҩ��ǩ		0	0
//		 */
//		#endregion
///// <summary>
//		/// Ƶ�ΰ�����
//		/// </summary>
//		private FS.FrameWork.Public.ObjectHelper frequencyHelper = null;
//		/// <summary>
//		/// �÷�������
//		/// </summary>
//		private FS.FrameWork.Public.ObjectHelper usageHelper = null;
//		/// <summary>
//		/// �Ƿ��ӡ����
//		/// </summary>
//		private bool isPrintTitle = true;
//
//		private void GetRecipeLabelItem(string drugDept,string drugCode,ref FS.HISFC.Models.Pharmacy.Item item)
//		{
//			FS.FrameWork.Management.DataBaseManger dataBaseMgr = new FS.FrameWork.Management.DataBaseManger();
//			string strSql = @"select t.trade_name,t.regular_name,t.formal_name,t.other_name,
//       t.english_regular,t.english_other,t.english_name,t.caution,t.store_condition,t.base_dose,s.place_code
//from   pha_com_stockinfo s,pha_com_baseinfo t
//where  t.parent_code = '000010' 
//and    t.current_code = '004004'
//and    s.parent_code = t.parent_code
//and    s.current_code = t.current_code
//and    t.drug_code = s.drug_code
//and    s.drug_dept_code = '{0}'
//and    s.drug_code = '{1}'";
//			strSql = string.Format(strSql,drugDept,drugCode);
//			if (dataBaseMgr.ExecQuery(strSql) != -1)
//			{
//				if (dataBaseMgr.Reader.Read())
//				{
//					item.Name = dataBaseMgr.Reader[0].ToString();
//					item.RegularName = dataBaseMgr.Reader[1].ToString();
//					item.FormalName = dataBaseMgr.Reader[2].ToString();
//					item.OtherName = dataBaseMgr.Reader[3].ToString();
//					item.EnglishRegularName = dataBaseMgr.Reader[4].ToString();
//					item.EnglishOtherName = dataBaseMgr.Reader[5].ToString();
//					item.EnglishName = dataBaseMgr.Reader[6].ToString();
//					item.Caution = dataBaseMgr.Reader[7].ToString();
//					item.StoreCondition = dataBaseMgr.Reader[8].ToString();
//					item.BaseDose = FS.FrameWork.Function.NConvert.ToDecimal(dataBaseMgr.Reader[9].ToString());
//					item.User01 = dataBaseMgr.Reader[10].ToString();
//				}
//			}
//		}
//
//		/// <summary>
//		/// ��ʼ����������
//		/// </summary>
//		private void Init()
//		{
//			//�������Ƶ����Ϣ 
//			if (this.frequencyHelper == null)
//			{
//				FS.HISFC.BizLogic.Manager.Frequency frequencyManagement = new FS.HISFC.BizLogic.Manager.Frequency();
//				ArrayList alFrequency = frequencyManagement.GetAll("Root");
//				if (alFrequency != null)
//					this.frequencyHelper = new FS.FrameWork.Public.ObjectHelper(alFrequency);
//			}
//			//��ȡ�����÷�
//			if (this.usageHelper == null)
//			{
//				FS.HISFC.BizLogic.Manager.Constant  c=new FS.HISFC.BizLogic.Manager.Constant();
//				ArrayList alUsage = c.GetList(FS.HISFC.Models.Base.enuConstant.USAGE);
//				if (alUsage == null)
//				{
//					MessageBox.Show("��ȡ�÷��б����!");
//					return;
//				}
//				ArrayList tempAl = new ArrayList();
//				foreach(FS.FrameWork.Models.NeuObject info in alUsage)
//				{
//					if (info.Memo != "")
//						info.Name = info.Memo;
//					tempAl.Add(info);
//				}
//
//				this.usageHelper = new FS.FrameWork.Public.ObjectHelper(tempAl);
//			}
//
//			this.fpSpread1_Sheet2.Cells[0,0].Border = new underlineborder(1,null);
//		}
//
//		/// <summary>
//		/// �����ʾ 
//		/// </summary>
//		protected void Clear()
//		{
//			
//			this.lbBarCode.Text = "";
//			for(int i = 0;i < this.fpSpread1_Sheet1.Rows.Count; i++)
//			{
//				for(int j = 0;j < this.fpSpread1_Sheet1.Columns.Count;j++)
//				{
//					this.fpSpread1_Sheet1.Cells[i,j].Text = "";
//				}				
//			}
//			for(int i = 0;i < this.fpSpread1_Sheet2.Rows.Count;i++)
//			{
//				for(int j = 0;j < this.fpSpread1_Sheet2.Columns.Count;j++)
//				{
//					this.fpSpread1_Sheet2.Cells[i,j].Text = "";
//				}
//			}
//		}
//
//
//		/// <summary>
//		/// ���û�����Ϣ
//		/// </summary>
//		protected void SetPatiAndSendInfo(FS.HISFC.Models.Pharmacy.ApplyOut applyOut,decimal labelNum)
//		{
//			//ʹ��Code 39���� ����Code 39��׼��Ҫ����ʼ* ��ֹ*��־
//			this.lbBarCode.Text = "*" + applyOut.RecipeNo + "*";
//			if (this.fpSpread1.ActiveSheet == this.fpSpread1_Sheet2)
//			{
//				//���û�����Ϣ 
//				if (this.patientInfo != null)
//				{
//					//����[�Ա�] ����			
//					this.fpSpread1_Sheet1.Cells[(int)RowSet.RowPatiInfo,0].Text = this.patientInfo.Name + "[" + this.patientInfo.Sex.Name + "]" + this.patientInfo.Age;	//����[�Ա�] ����			
//				}
//				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowPatiInfo,1].Text = applyOut.User01;		//��ҩ���ں�
//			}
//			else
//			{
//				if (this.patientInfo != null)
//				{
//					//����[�Ա�] ����			
//					this.fpSpread1_Sheet1.Cells[(int)RowSet.RowFreUse,0].Text = string.Format("{0}[{1}]",this.patientInfo.Name,this.patientInfo.Sex.Name);
//					this.fpSpread1_Sheet1.Cells[(int)RowSet.RowFreUse,1].Text = this.patientInfo.Age;
//				}
//				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowPatiInfo,0].Text = applyOut.User01;		//��ҩ���ں�
//			}			
//			//���÷�ҩ��Ϣ			
//			this.fpSpread1_Sheet1.Cells[(int)RowSet.RowSendInfo,0].Text = applyOut.ApplyDate.ToString();
//			//��ҩ��ǩ��ҳ��/��ǰҳ��
//			this.fpSpread1_Sheet1.Cells[(int)RowSet.RowSendInfo,1].Text = labelNum + "/" + this.labelTotNum;
//		}
//		/// <summary>
//		/// ���û�����Ϣ
//		/// </summary>
//		protected void SetPatiAndSendInfo(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
//		{
//			this.SetPatiAndSendInfo(applyOut,this.labelNum);
//		}
//
//		
//		#region IRecipeLabel ��Ա
//		/// <summary>
//		/// ������Ϣ
//		/// </summary>
//		private FS.HISFC.Models.Registration.Register patientInfo = null;
//		/// <summary>
//		/// ������Ϣ
//		/// </summary>
//		public FS.HISFC.Models.Registration.Register PatientInfo
//		{
//			get
//			{
//				// TODO:  ��� ucComboRecipeLabel.PatientInfo getter ʵ��
//				return this.patientInfo;
//			}
//			set
//			{
//				// TODO:  ��� ucComboRecipeLabel.PatientInfo setter ʵ��
//				this.patientInfo = value;
//			}
//		}
//
//
//		/// <summary>
//		/// ���δ�����ӡҳ��
//		/// </summary>
//		protected decimal labelNum;
//		/// <summary>
//		/// ���δ�����ӡ��ҳ��
//		/// </summary>
//		protected decimal labelTotNum;
//		protected decimal drugTotNum;
//		/// <summary>
//		/// һ�δ�ӡҩƷ������ҳ��
//		/// </summary>
//		public decimal DrugTotNum
//		{
//			set
//			{
//				this.drugTotNum = value;
//				this.labelNum = 1;
//			}
//		}
//
//		/// <summary>
//		/// ���δ�����ӡ��ҳ��
//		/// </summary>
//		public decimal LabelTotNum
//		{
//			set
//			{
//				this.labelTotNum = value;
//				this.labelNum = 1;
//			}
//		}
//		/// <summary>
//		/// ��ӡ����ҩƷ
//		/// </summary>
//		/// <param name="applyOut"></param>
//		public void AddSingle(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
//		{		
//			this.Clear();
//			if (this.fpSpread1.Sheets.Contains(this.fpSpread1_Sheet1))
//			{
//				this.fpSpread1.Sheets.Remove(this.fpSpread1_Sheet1);
//			}
//			if (!this.fpSpread1.Sheets.Contains(this.fpSpread1_Sheet2))
//			{
//				this.fpSpread1.Sheets.Add(this.fpSpread1_Sheet2);				
//			}
//			
////			this.fpSpread1_Sheet2.Cells[0,0].Border = new underlineborder(1,null);
//
//			FS.HISFC.Models.Pharmacy.Item item = applyOut.Item;
//			this.GetRecipeLabelItem(applyOut.TargetDept.ID,applyOut.Item.ID,ref item);
//
//			this.lbBarCode.Text = "*" + applyOut.RecipeNo + "*";
//			if (this.patientInfo != null)
//			{
//				this.fpSpread1_Sheet2.Cells[(int)RowSet.RowPatiInfo,0].Text = string.Format("{0}[{1}]{2}",this.patientInfo.Name,this.patientInfo.Sex.Name,this.patientInfo.Age);	//����[�Ա�] ����			
//			}
//			this.fpSpread1_Sheet2.Cells[(int)RowSet.RowPatiInfo,1].Text = applyOut.User01;		//��ҩ���ں�
//
//			this.fpSpread1_Sheet2.Cells[(int)RowSet.RowSendInfo,0].Text = applyOut.ApplyDate.ToString();
//			this.fpSpread1_Sheet2.Cells[(int)RowSet.RowSendInfo,1].Text = labelNum + "/" + this.labelTotNum;
//
//			this.fpSpread1_Sheet2.Cells[(int)RowSet.RowDrugBegin,0].Text = "��ɽ��ѧ������һҽԺ";
//			this.fpSpread1_Sheet2.Cells[(int)RowSet.RowDrugBegin + 1,0].Text = applyOut.Item.Name + "[" + applyOut.Item.Specs + "]";	//����
//			this.fpSpread1_Sheet2.Cells[(int)RowSet.RowDrugBegin + 1,3].Text = applyOut.ApplyNum.ToString() + applyOut.Item.MinUnit;//����������λ
//			this.fpSpread1_Sheet2.Cells[(int)RowSet.RowDrugBegin + 2,0].Text = this.usageHelper.GetName(applyOut.Usage.ID);		//�÷�
//			this.fpSpread1_Sheet2.Cells[(int)RowSet.RowDrugBegin + 2,1].Text = this.frequencyHelper.GetName(applyOut.Frequency.ID);//Ƶ��
//			this.fpSpread1_Sheet2.Cells[(int)RowSet.RowDrugBegin + 2,2].Text = "ÿ��" + applyOut.DoseOnce.ToString() + applyOut.Item.DoseUnit;//ÿ����
//			this.fpSpread1_Sheet2.Cells[(int)RowSet.RowDrugBegin + 3,0].Text = "ע�����" + applyOut.Item.Caution;
//			this.fpSpread1_Sheet2.Cells[(int)RowSet.RowFreUse,2].Text = applyOut.Item.User01;
//
//			#region ����ԭ��ʽ ����
//			/*
//			//����ҩƷ��Ϣ
//			FS.HISFC.Models.Pharmacy.Item item = applyOut.Item;
//			this.GetRecipeLabelItem(applyOut.TargetDept.ID,applyOut.Item.ID,ref item);
//			//���û�����Ϣ��ʾ����ҩ��Ϣ
//			this.SetPatiAndSendInfo(applyOut);								
//			//���ô����ڷ�ҩҩƷ��Ϣ
//			if (this.isPrintTitle)
//			{
//			#region ��ӡ����
//				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin,0].ColumnSpan = 4;
//				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin,0].Text = "��ɽ��ѧ������һҽԺ";
//				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin,0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
//
//				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 1,0].ColumnSpan = 3;
//				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 1,0].Text = applyOut.Item.Name + "[" + applyOut.Item.Specs + "]";	//����
//				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 1,3].Text = applyOut.ApplyNum.ToString() + applyOut.Item.MinUnit;//����������λ
//				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 2,0].ColumnSpan = 1;
//				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 2,0].Text = this.usageHelper.GetName(applyOut.Usage.ID);		//�÷�
//				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 2,1].Text = this.frequencyHelper.GetName(applyOut.Frequency.ID);//Ƶ��
//				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 2,2].Text = "ÿ��" + applyOut.DoseOnce.ToString() + applyOut.Item.DoseUnit;//ÿ����
//				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 3,0].ColumnSpan = 3;				//�ϲ���
//				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 3,0].Text = "ע�����" + applyOut.Item.Caution;
//			#endregion
//			}
//			else
//			{	
//			#region ����ӡ����
//				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin,0].ColumnSpan = 3;
//				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin,0].Text = applyOut.Item.Name + "[" + applyOut.Item.Specs + "]";	//����
//				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin,3].Text = applyOut.ApplyNum.ToString() + applyOut.Item.MinUnit;//����������λ
//				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 1,0].ColumnSpan = 1;
//				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 1,0].Text = this.usageHelper.GetName(applyOut.Usage.ID);		//�÷�
//				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 1,1].Text = this.frequencyHelper.GetName(applyOut.Frequency.ID);//Ƶ��
//				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 1,2].Text = "ÿ��" + applyOut.DoseOnce.ToString() + applyOut.Item.DoseUnit;//ÿ����
//				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 2,0].ColumnSpan = 3;				//�ϲ���
//				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + 2,0].Text = "ע�����" + applyOut.Item.Caution;
//			#endregion
//			}
//
//			this.fpSpread1_Sheet1.Cells[(int)RowSet.RowFreUse,2].ColumnSpan = 2;
//			this.fpSpread1_Sheet1.Cells[(int)RowSet.RowFreUse,2].Text = applyOut.Item.User01;
//			
////			*/
////			#endregion
////
////			this.labelNum = this.labelNum + 1;
////		}
////
////		/// <summary>
////		/// ��ӡһ��ҩƷ
////		/// </summary>
////		/// <param name="alCombo"></param>
////		public void AddCombo(ArrayList alCombo)
////		{
////			int iNum = 0;
////			this.Clear();
////
////			if (this.fpSpread1.Sheets.Contains(this.fpSpread1_Sheet2))
////			{
////				this.fpSpread1.Sheets.Remove(this.fpSpread1_Sheet2);
////			}
////			if (!this.fpSpread1.Sheets.Contains(this.fpSpread1_Sheet1))
////			{
////				this.fpSpread1.Sheets.Add(this.fpSpread1_Sheet1);
////			}
////
////			for(int i = 0;i < alCombo.Count;i++)
////			{
////				FS.HISFC.Models.Pharmacy.ApplyOut applyOut = alCombo[i] as FS.HISFC.Models.Pharmacy.ApplyOut;
////				if (i == 0)
////				{
////					this.SetPatiAndSendInfo(applyOut);
////					this.labelNum = this.labelNum + 1;
////				}
////				//����ҩƷ��Ϣ
////				FS.HISFC.Models.Pharmacy.Item item = applyOut.Item;
////				this.GetRecipeLabelItem(applyOut.TargetDept.ID,applyOut.Item.ID,ref item);
////				//���ô����ڷ�ҩҩƷ��Ϣ
////				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + iNum,0].Text = applyOut.Item.Name + "[" + applyOut.Item.Specs + "]";	//����
////				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + iNum,2].Text = "ÿ��" + applyOut.DoseOnce.ToString() + applyOut.Item.DoseUnit;//ÿ����
////				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + iNum,3].Text = applyOut.ApplyNum.ToString() + applyOut.Item.PriceUnit;//����������λ
////				//��ʾƵ����ע������
////				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowCaution,0].Text = this.usageHelper.GetName(applyOut.Usage.ID);		//�÷�
////				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowCaution,1].Text = this.frequencyHelper.GetName(applyOut.Frequency.ID);	//Ƶ��
////				
////				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowFreUse,2].Text = applyOut.Item.User01;
////
////				//Ժע����
////				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowCaution,3].Text = string.Format("Ժע��{0}",applyOut.ExecNo);			//Ժע����
////
////				//��¼�Ѵ�ӡ���� �ѳ���һҳ������ʾ��ҩ������ ��ȥ���л�����Ϣ �����÷���ע������
////				iNum = iNum + 1;
////				if ((iNum >= this.fpSpread1_Sheet1.Rows.Count - 4) && (i != alCombo.Count - 1))
////				{
////					// �� ��ʾ 
////					string str = this.fpSpread1_Sheet1.Cells[(int)RowSet.RowSendInfo,0].Text;
////					if (str.Substring(0,1) != "��")
////						this.fpSpread1_Sheet1.Cells[(int)RowSet.RowSendInfo,0].Text = "��" + str;				
////					this.Print();
////					this.SetPatiAndSendInfo(applyOut,this.labelNum - 1);
////					iNum = 0;
////				}			
////			}	
////
////			#region ����ԭ��ӡ��ʽ
////			/*
////			for(int i = 0;i < alCombo.Count;i++)
////			{
////				FS.HISFC.Models.Pharmacy.ApplyOut applyOut = alCombo[i] as FS.HISFC.Models.Pharmacy.ApplyOut;
////				if (i == 0)
////				{
////					this.SetPatiAndSendInfo(applyOut);
////					this.labelNum = this.labelNum + 1;
////				}
////				//����ҩƷ��Ϣ
////				FS.HISFC.Models.Pharmacy.Item item = applyOut.Item;
////				this.GetRecipeLabelItem(applyOut.TargetDept.ID,applyOut.Item.ID,ref item);
////				//���ô����ڷ�ҩҩƷ��Ϣ
////				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + iNum,0].ColumnSpan = 2;
////				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + iNum,0].Text = applyOut.Item.Name + "[" + applyOut.Item.Specs + "]";	//����
////				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + iNum,2].Text = "ÿ��" + applyOut.DoseOnce.ToString() + applyOut.Item.DoseUnit;//ÿ����
////				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowDrugBegin + iNum,3].Text = applyOut.ApplyNum.ToString() + applyOut.Item.PriceUnit;//����������λ
////				//��ʾƵ����ע������
////				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowFreUse,0].Text = this.usageHelper.GetName(applyOut.Usage.ID);		//�÷�
////				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowFreUse,1].Text = this.frequencyHelper.GetName(applyOut.Frequency.ID);	//Ƶ��
////				//��λ��
////				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowFreUse,2].ColumnSpan = 2;
////				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowFreUse,2].Text = applyOut.Item.User01;
////
////				#region ��ϲ���ӡע������
//////				//ע������ ����ע������ͬʱ��ʾ
//////				if (i == 0)
//////				{
//////					this.fpSpread1_Sheet1.Cells[(int)RowSet.RowCaution,0].ColumnSpan = 3;
//////					this.fpSpread1_Sheet1.Cells[(int)RowSet.RowCaution,0].Text = "ע������:" + applyOut.Item.Caution;
//////				}
//////				else
//////				{
//////					this.fpSpread1_Sheet1.Cells[(int)RowSet.RowCaution,0].Text = this.fpSpread1_Sheet1.Cells[(int)RowSet.RowCaution,0].Text + applyOut.Item.Caution;
//////				}
////				#endregion
////
////				//Ժע����
////				this.fpSpread1_Sheet1.Cells[(int)RowSet.RowCaution,3].Text = "��" + applyOut.ExecNo + "��";			//Ժע����
////
////				//��¼�Ѵ�ӡ���� �ѳ���һҳ������ʾ��ҩ������ ��ȥ���л�����Ϣ �����÷���ע������
////				iNum = iNum + 1;
////				if ((iNum >= this.fpSpread1_Sheet1.Rows.Count - 4) && (i != alCombo.Count - 1))
////				{
////					// �� ��ʾ 
////					string str = this.fpSpread1_Sheet1.Cells[(int)RowSet.RowSendInfo,0].Text;
////					if (str.Substring(0,1) != "��")
////						this.fpSpread1_Sheet1.Cells[(int)RowSet.RowSendInfo,0].Text = "��" + str;				
////					this.Print();
////					this.SetPatiAndSendInfo(applyOut,this.labelNum - 1);
////					iNum = 0;
////				}			
////			}	
////			
////			*/
////			#endregion
////		}
////
////		/// <summary>
////		/// ��ӡȫ��ҩƷ ��ҩ�嵥
////		/// </summary>
////		/// <param name="al"></param>
////		public void AddAllData(ArrayList al)
////		{
////			// TODO:  ��� ucComboRecipeLabel.AddAllData ʵ��
////		}
////
////		/// <summary>
////		/// ��ӡ����
////		/// </summary>
////		public void Print()
////		{
////			// TODO:  ��� ucComboRecipeLabel.Print ʵ��
////			FS.FrameWork.WinForms.Classes.Print p=new FS.FrameWork.WinForms.Classes.Print();
////			FS.HISFC.Models.Base.PageSize page=new FS.HISFC.Models.Base.PageSize();
////
////			FS.Common.Class.Function.GetPageSize("RecipeLabel",ref p);
////			p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
////			System.Windows.Forms.Control c = this;
////			c.Width = this.Width;
////			c.Height = this.Height;
////			//			p.PrintPreview(12,1,c);
////			p.PrintPage(12,1,c);
////
////			this.Clear();
////		}
////
////		#endregion
////
////		private enum RowSet
////		{
////			/// <summary>
////			/// ��ҩ������ʼ����
////			/// </summary>
////			RowDrugBegin = 0,
////			/// <summary>
////			/// ע������ 3
////			/// </summary>
////			RowCaution = 3,
////			/// <summary>
////			/// Ƶ�� 4
////			/// </summary>
////			RowFreUse = 4,
////			/// <summary>
////			/// ������Ϣ 5
////			/// </summary>
////			RowPatiInfo = 5,
////			/// <summary>
////			/// ��ҩ��Ϣ 6
////			/// </summary>
////			RowSendInfo = 6
////		}
////		
////
////		/// <summary>
////		/// Summary description for underlineborder.
////		/// </summary>
////		public class underlineborder : FarPoint.Win.IBorder
////		{
////
////			int underlineCount;
////			private FarPoint.Win.Inset m_Inset;
////			private FarPoint.Win.IBorder baseBorder;
////
////			public FarPoint.Win.Inset Inset
////			{
////				get
////				{
////					return m_Inset;
////				}
////			}
////
////			public underlineborder(int underlineCount, FarPoint.Win.IBorder baseBorder)
////			{
////				if (underlineCount < 1) 
////				{
////					throw new ArgumentException();
////				}
////				FarPoint.Win.Inset baseinset; 
////				if (baseBorder == null)
////				{
////					baseinset = new FarPoint.Win.Inset(0, 0, 0, 0);
////				}
////				else
////				{					 
////					baseinset = baseBorder.Inset;
////				}					
////			
////				this.underlineCount = underlineCount;
////				this.baseBorder = baseBorder;
////				m_Inset = new FarPoint.Win.Inset(baseinset.Left, baseinset.Top, baseinset.Right, Math.Max(baseinset.Bottom, 2 * underlineCount - 1));	
////		
////			}
////			void FarPoint.Win.IBorder.Paint(System.Drawing.Graphics g, int x, int y, int width, int height) 
////			{
////				g.FillRectangle(System.Drawing.Brushes.Black, x , y + 18, width, 1);
////
////				//				if (width > 0 & height > 0) 
////				//				{
////				//					int underlineheight  = Math.Min(2 * underlineCount - 1, height);
////				//					int underlinewidth  = width;
////				//					int underlinex  = x + ((width - underlinewidth) / 2);
////				//					int underliney = y + height - underlineheight;
////				//					g.FillRectangle(System.Drawing.Brushes.White, x, underliney, width, underlineheight);
////				//					if (baseBorder != null) 
////				//					{
////				//						baseBorder.Paint(g, x, y, width, height);
////				//					}
////				//					while (underlineheight > 0)
////				//					{
////				//						g.FillRectangle(System.Drawing.Brushes.Black, underlinex, underliney + underlineheight - 1, underlinewidth, 1);
////				//						underlineheight -= 2;
////				//					}
////				//				}
////			}
////		}
////	}
////}
//*/		
//
#endregion




