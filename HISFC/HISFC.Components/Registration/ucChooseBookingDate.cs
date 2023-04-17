using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Registration
{
    /// <summary>
    /// ѡ��ҽ��������Ϣ�б�
    /// </summary>
    public partial class ucChooseBookingDate : UserControl
    {
        public ucChooseBookingDate()
        {
            InitializeComponent();

            this.InitNoon();
            this.fpSpread1.KeyDown          += new KeyEventHandler(fpSpread1_KeyDown);
            this.fpSpread1.CellDoubleClick  += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellDoubleClick);

            FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            this.alSpecialDepts = conMgr.QueryConstantList("IsSpecialClinic");
            if (this.alSpecialDepts == null) this.alSpecialDepts = new ArrayList();
        }

        #region ����
        /// <summary>
		/// �Ű������
		/// </summary>
		private FS.HISFC.BizLogic.Registration.Schema SchemaMgr = new FS.HISFC.BizLogic.Registration.Schema();
		/// <summary>
		/// ���
		/// </summary>
		private Hashtable htNoon = new Hashtable() ;
		/// <summary>
		/// Array Collections
		/// </summary>
		private ArrayList al = new ArrayList() ;
		private ArrayList alSpecialDepts = new ArrayList();
        #endregion

        #region ����
        /// <summary>
		/// ԤԼ�Ű���Ϣ����
		/// </summary>
		public ArrayList Bookings
		{
			get{return al ;}
		}
		/// <summary>
		/// ��ЧԤԼ��
		/// </summary>
		public int Count
		{
			get{return this.fpSpread1_Sheet1.RowCount ;}
		}		
		/// <summary>
		/// delegate
		/// </summary>
		public delegate void dSelectedItem(FS.HISFC.Models.Registration.Schema sender) ;
		/// <summary>
		/// ѡ���Ű��¼�
		/// </summary>
		public event dSelectedItem SelectedItem;
        #endregion

        #region ˽�к���
        /// <summary>
		/// ��ʼ�����
		/// </summary>
		private void InitNoon()
		{
            FS.HISFC.BizLogic.Registration.Noon NoonMgr = new FS.HISFC.BizLogic.Registration.Noon();

			ArrayList al = NoonMgr.Query() ;
			if( al == null)
			{
				MessageBox.Show("��ȡ�����Ϣʱ����!" + NoonMgr.Err,"��ʾ") ;
				return ;
			}

			foreach(FS.HISFC.Models.Base.Noon obj in al)
			{
				this.htNoon.Add(obj.ID,obj.Name) ;
			}
		}
		
		/// <summary>
		/// ���ݴ�����������
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		private string GetNoonNameByID(string ID)
		{
			System.Collections.IDictionaryEnumerator dict = this.htNoon.GetEnumerator() ;

			while(dict.MoveNext() )
			{
				if(dict.Key.ToString() == ID)
				{
					return dict.Value.ToString() ;
				}
			}
			return ID ;
		}

		
		/// <summary>
		/// ��ѯ����ԤԼ�Ű���
		/// </summary>
		/// <param name="bookingDate"></param>
		/// <param name="deptID"></param>	
		/// <param name="regType"></param>	
		public void QueryDeptBooking(DateTime bookingDate,string deptID,RegTypeNUM regType)
		{
			if(this.fpSpread1_Sheet1.RowCount > 0)
				this.fpSpread1_Sheet1.Rows.Remove(0,this.fpSpread1_Sheet1.RowCount) ;

			al = this.SchemaMgr.QueryByDept(bookingDate.Date,deptID,"ALL") ;
			if( al == null)
			{
				MessageBox.Show("��ѯ�Ű���Ϣʱ����!" +this.SchemaMgr.Err,"��ʾ") ;
				return ;
			}

			DateTime current = this.SchemaMgr.GetDateTimeFromSysDateTime() ;

			foreach(FS.HISFC.Models.Registration.Schema obj in al)
			{
				if( !IsMaybeValid(obj, current, regType)) continue ;

				this.AddRow(obj) ;
			}

			//this.Span() ;
		}


        /// <summary>
        /// ��ѯ����ԤԼ�Ű���
        /// </summary>
        /// <param name="bookingDate"></param>
        /// <param name="deptID"></param>	
        /// <param name="regType"></param>	
        public void QueryDeptBooking(DateTime bookingDate, string deptID, RegTypeNUM regType, FS.HISFC.Models.Registration.RegLevel level)
        {
            if (this.fpSpread1_Sheet1.RowCount > 0)
                this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);

            al = this.SchemaMgr.QueryByDept(bookingDate.Date, deptID,level.ID);
            if (al == null)
            {
                MessageBox.Show("��ѯ�Ű���Ϣʱ����!" + this.SchemaMgr.Err, "��ʾ");
                return;
            }

            DateTime current = this.SchemaMgr.GetDateTimeFromSysDateTime();

            foreach (FS.HISFC.Models.Registration.Schema obj in al)
            {
                if (!IsMaybeValid(obj, current, regType)) continue;

                this.AddRow(obj);
            }

            //this.Span() ;
        }

		/// <summary>
		/// ��ѯҽ��ԤԼ�Ű���
		/// </summary>
		/// <param name="bookingDate"></param>
		/// <param name="doctID"></param>
		/// <param name="regType"></param>	
		public void QueryDoctBooking(DateTime bookingDate,string doctID,RegTypeNUM regType)
		{
			if(this.fpSpread1_Sheet1.RowCount > 0)
				this.fpSpread1_Sheet1.Rows.Remove(0,this.fpSpread1_Sheet1.RowCount) ;

			al = this.SchemaMgr.QueryByDoct(bookingDate.Date,doctID) ;
			if( al == null)
			{
				MessageBox.Show("��ѯ�Ű���Ϣʱ����!" +this.SchemaMgr.Err,"��ʾ") ;
				return ;
			}
			
			DateTime current = this.SchemaMgr.GetDateTimeFromSysDateTime() ;

			foreach(FS.HISFC.Models.Registration.Schema obj in al)
			{
				
				if( !IsMaybeValid(obj, current, regType)) continue ;

				this.AddRow(obj) ;
			}

			//this.Span() ;
        }
       
        
        /// <summary>
		/// �����ҡ�ҽ����ѯ����ҽ��
		/// </summary>
		/// <param name="bookingDate"></param>
		/// <param name="doctId"></param>
		/// <param name="deptID"></param>
		/// <param name="regType"></param>
		public void QueryDoctBooking(DateTime bookingDate,string doctId, string deptID,RegTypeNUM regType)
		{
			if(this.fpSpread1_Sheet1.RowCount > 0)
				this.fpSpread1_Sheet1.Rows.Remove(0,this.fpSpread1_Sheet1.RowCount) ;

			al = this.SchemaMgr.QueryByDoct(bookingDate.Date,deptID,doctId) ;
			if( al == null)
			{
				MessageBox.Show("��ѯ�Ű���Ϣʱ����!" +this.SchemaMgr.Err,"��ʾ") ;
				return ;
			}
			
			DateTime current = this.SchemaMgr.GetDateTimeFromSysDateTime() ;

			foreach(FS.HISFC.Models.Registration.Schema obj in al)
			{
				
				if( !IsMaybeValid(obj, current, regType)) continue ;

				this.AddRow(obj) ;
			}

		}

         #region ��ʱ����
        /*
        private void Span()
		{
			int rowLastDate = 0, rowLastNoon = 0 ;
			int rowCnt = this.fpSpread1_Sheet1.RowCount ;
			for( int i = 0 ;i < rowCnt ; i++)
			{
				if( i > 0 && this.fpSpread1_Sheet1.GetText(i,0) != this.fpSpread1_Sheet1.GetText(i-1,0))
				{
					if( i - rowLastDate > 1 )
					{						
						this.fpSpread1_Sheet1.Models.Span.Add(rowLastDate,0 , i - rowLastDate ,1) ;						
					}

					rowLastDate = i ;					
				}

				//���һ�д���
				if(i > 0&& i == rowCnt -1 && this.fpSpread1_Sheet1.GetText(i,0) == this.fpSpread1_Sheet1.GetText(i-1,0))
				{
					this.fpSpread1_Sheet1.Models.Span.Add(rowLastDate,0, i - rowLastDate + 1,1) ;
				}

				///���
				///
				if( i > 0 &&
					(this.fpSpread1_Sheet1.GetText(i,0) != this.fpSpread1_Sheet1.GetText(i-1,0)||
					this.fpSpread1_Sheet1.GetText(i,1) != this.fpSpread1_Sheet1.GetText(i-1,1)))
					
				{
					if(i - rowLastNoon >1 )
					{						
						this.fpSpread1_Sheet1.Models.Span.Add(rowLastNoon,1, i - rowLastNoon,1) ;
					}
					rowLastNoon = i ;
				}
				//���һ��
				if( i > 0 && i == rowCnt - 1 &&
					(this.fpSpread1_Sheet1.GetText(i,1) == this.fpSpread1_Sheet1.GetText(i-1,1)||
					this.fpSpread1_Sheet1.GetText(i,0) == this.fpSpread1_Sheet1.GetText(i-1,0)))
				{
					this.fpSpread1_Sheet1.Models.Span.Add(rowLastNoon,1, i - rowLastNoon + 1,1) ;
				}
			}
		}
        */
        #endregion
        
        /// <summary>
		/// �ж�һ��������Ϣ�Ƿ���Ч(�����޶��Ϊ�ж�,��������Maybe, HaHa ���� :))
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="current"></param>
		/// <param name="regType"></param>
		/// <returns></returns>
		private bool IsMaybeValid(FS.HISFC.Models.Registration.Schema obj, DateTime current, RegTypeNUM regType)
		{
			//��Ч

			if(obj.Templet.IsValid == false) return false ;
			
			//���ǼӺ�
//			if(!obj.Templet.IsAppend)
//			{
//				if(regType == RegTypeNUM.Booking)
//				{
//					if(obj.Templet.TelLmt == 0) return false ;//û��ԤԼ����,����ʾ
//				}
//				else if(regType == RegTypeNUM.Expert)
//				{
//					if(obj.Templet.RegLmt ==0) return false ;
//				}
//				else if(regType == RegTypeNUM.Faculty)
//				{
//					if(obj.Templet.RegLmt ==0) return false ;
//				}
//				else if(regType == RegTypeNUM.Special)
//				{
//					if(obj.Templet.SpeLmt == 0) return false ;
//				}
//			}

			//
			//ֻ��������ͬ,���ж�ʱ���Ƿ�ʱ,�������ԤԼ���Ժ�����,ʱ�䲻���ж�,(����ʱ��һ����>=��ǰʱ��)
			//
			if(current.Date == obj.SeeDate.Date)
			{
				if(obj.Templet.End.TimeOfDay <current.TimeOfDay) return false ;//ʱ��С�ڵ�ǰʱ��,����ʾ
			}

			return true ;
		}

		/// <summary>
		/// ȡ������Ч��һ�������¼
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="current"></param>
		/// <param name="regType"></param>
		/// <returns></returns>
		private bool IsValid(FS.HISFC.Models.Registration.Schema obj,DateTime current, RegTypeNUM regType)
		{

			if(this.IsMaybeValid(obj,current,regType) == false)return false ;

			//�ж��Ƿ��޶�
			if(!obj.Templet.IsAppend)
			{
				if(regType == RegTypeNUM.Booking)
				{					
					//��ɽ���ﲻԤԼ,�����Ű�ʱ�������������Ϊ�����Ű��¼,ԤԼ�Һ�ʱֻѡ�����,Ĭ�ϼ���һ�������������Ű��¼
					//��ʱ�ͻᾭ�������������,û������������ҵ��Ű���Ϣ,����������һ��
					bool found = false;

					foreach(FS.HISFC.Models.Base.Const con in this.alSpecialDepts)
					{
						if(obj.Templet.Dept.ID == con.ID)
						{
							found = true ;
							break;
						}
					}

					if(found)return false ;

					if(obj.Templet.TelQuota <= obj.TelingQTY) return false;//���޶�
				}
                else if (regType == RegTypeNUM.Expert || regType == RegTypeNUM.Faculty)
				{
					if(obj.Templet.RegQuota <= obj.RegedQTY&&obj.Templet.TelQuota<=obj.TelingQTY) 
                        return false;
				}
				else if(regType == RegTypeNUM.Special)
				{					
					if(obj.Templet.SpeQuota <= obj.SpedQTY) return false;
				}				
			}

			return true ;
		}

        /// <summary>
        /// add object to farpoint
        /// </summary>
        /// <param name="schema"></param>
        private void AddRow(FS.HISFC.Models.Registration.Schema schema)
        {
            this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);

            int Index = this.fpSpread1_Sheet1.RowCount - 1;

            this.fpSpread1_Sheet1.SetValue(Index, 0, 
                  schema.SeeDate.ToString("yyyy-MM-dd")
                + schema.Templet.Doct.Name
                + this.getWeek(schema.SeeDate) + " " 
                + schema.Templet.RegLevel,
                false);
            this.fpSpread1_Sheet1.SetValue(Index, 1, this.GetNoonNameByID(schema.Templet.Noon.ID), false);
            //��ʼʱ�䡢����ʱ��
            if (schema.Templet.IsAppend)
            {
                this.fpSpread1_Sheet1.SetValue(Index, 2, "�Ӻ�", false);
                this.fpSpread1_Sheet1.SetValue(Index, 3, "�Ӻ�", false);
            }
            else
            {
                this.fpSpread1_Sheet1.SetValue(Index, 2, schema.Templet.Begin.ToString("HH:mm"), false);
                this.fpSpread1_Sheet1.SetValue(Index, 3, schema.Templet.End.ToString("HH:mm"), false);
            }

            //�������
            this.fpSpread1_Sheet1.SetValue(Index, 4, schema.Templet.RegQuota, false);
            //����ȡ��
            this.fpSpread1_Sheet1.SetValue(Index, 5, schema.RegedQTY, false);
            //����
            this.fpSpread1_Sheet1.SetValue(Index, 6, schema.Templet.TelQuota, false);
            this.fpSpread1_Sheet1.SetValue(Index, 7, schema.TelingQTY, false);
            this.fpSpread1_Sheet1.SetValue(Index, 8, schema.TeledQTY, false);
            //����
            this.fpSpread1_Sheet1.SetValue(Index, 9, schema.Templet.SpeQuota, false);
            this.fpSpread1_Sheet1.SetValue(Index, 10, schema.SpedQTY, false);
            this.fpSpread1_Sheet1.SetValue(Index, 11, schema.Templet.Dept.Name, false);
            this.fpSpread1_Sheet1.Rows[Index].Tag = schema;
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private string getWeek(DateTime current)
        {
            string[] week = new string[] { "[��]", "[һ]", "[��]", "[��]", "[��]", "[��]", "[��]" };

            return week[(int)current.DayOfWeek];
        }
        #endregion

        #region ���к���
        /// <summary>
		/// ���һ����Ч�ġ�����ġ��޶�δ�����Ű���Ϣ
		/// </summary>
		/// <param name="regType"></param>
		/// <returns></returns>
		public FS.HISFC.Models.Registration.Schema GetValidBooking(RegTypeNUM regType)
		{
			return this.GetValidBooking(al, regType) ;
		}		
		
		/// <summary>
		/// ��ָ�����Ű���Ϣ��,���һ����Ч�ġ�����ġ��޶�δ�����Ű���Ϣ
		/// </summary>
		/// <param name="regType"></param>
		/// <returns></returns>
		public FS.HISFC.Models.Registration.Schema GetValidBooking(ArrayList schemaCollection, RegTypeNUM regType)
		{
			if(schemaCollection == null)return null ;
			
			DateTime current = this.SchemaMgr.GetDateTimeFromSysDateTime() ;

			foreach(FS.HISFC.Models.Registration.Schema obj in  schemaCollection)
			{
				if(!this.IsValid(obj,current,regType)) continue ;

				return obj ;
			}

			return null;
		}

		/// <summary>
		/// ����Ű���Ϣ
		/// </summary>
		public void Clear() 
		{
			this.al = new ArrayList() ;

			if(this.fpSpread1_Sheet1.RowCount> 0)
			{
				this.fpSpread1_Sheet1.Rows.Remove(0,this.fpSpread1_Sheet1.RowCount) ;
			}
        }
        #endregion

        #region �¼�
        /// <summary>
		/// �س�ѡ���Ű���Ϣ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fpSpread1_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				this.SelectItem() ;
			}
		}

		private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
		{
			this.SelectItem() ;
		}

		/// <summary>
		/// ѡ���Ű���Ϣ
		/// </summary>
		/// <returns></returns>
		private int SelectItem()
		{
			int row = this.fpSpread1_Sheet1.ActiveRowIndex ;
			if(row == -1 || this.fpSpread1_Sheet1.RowCount == 0) return 0;

			FS.HISFC.Models.Registration.Schema schema ;

			schema = (FS.HISFC.Models.Registration.Schema)this.fpSpread1_Sheet1.Rows[row].Tag ;

			if(this.SelectedItem != null)
				this.SelectedItem(schema ) ;
			
			return 0;
        }
        #endregion
    }	
	/// <summary>
	/// �Һ����
	/// </summary>
	public enum RegTypeNUM
	{
		/// <summary>
		/// ר�Һ�
		/// </summary>
		Expert,
		/// <summary>
		/// ר�ƺ�
		/// </summary>
		Faculty,
		/// <summary>
		/// �����
		/// </summary>
		Special,
		/// <summary>
		/// ԤԼ��
		/// </summary>
		Booking
	}  
}
