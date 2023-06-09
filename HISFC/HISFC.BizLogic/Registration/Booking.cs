using System;
using System.Collections;

namespace FS.HISFC.BizLogic.Registration
{
	/// <summary>
	/// 预约挂号管理类
	/// </summary>
	public class Booking : FS.FrameWork.Management.Database
	{
		/// <summary>
		/// 
		/// </summary>
		public Booking()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 增加
		/// <summary>
		/// 登记一条预约挂号信息
		/// </summary>
		/// <param name="booking"></param>
		/// <returns></returns>
		public int Insert (FS.HISFC.Models.Registration.Booking booking)
		{
			string sql = "";

			if(this.Sql.GetCommonSql("Registration.Booking.Insert",ref sql ) == -1)return -1;

			try
			{
				sql = string.Format(sql,booking.ID,booking.PID.CardNO,booking.DoctorInfo.SeeDate.ToString(),booking.DoctorInfo.Templet.Noon.ID,
					booking.Name,booking.IDCard,booking.Sex.ID.ToString(),booking.Birthday.ToString(),booking.PhoneHome,
					booking.AddressHome,booking.DoctorInfo.Templet.Dept.ID,booking.DoctorInfo.Templet.Dept.Name,
                    booking.DoctorInfo.Templet.Begin.ToString(),booking.DoctorInfo.Templet.End.ToString(),
                    booking.DoctorInfo.Templet.Doct.ID,booking.DoctorInfo.Templet.Doct.Name,
                    FS.FrameWork.Function.NConvert.ToInt32(booking.IsSee),
					booking.Oper.ID,booking.Oper.OperTime.ToString(),FS.FrameWork.Function.NConvert.ToInt32(booking.DoctorInfo.Templet.IsAppend),
					booking.DoctorInfo.Templet.ID,
                    booking.DoctorInfo.Templet.RegLevel.ID ,
                    booking.BookingTypeId != null? booking.BookingTypeId:string.Empty, 
                    booking.BookingTypeName!=null ?booking.BookingTypeName:string.Empty,
                    booking.OrderNO.ToString(),
                    booking.DoctorInfo.SeeNO.ToString()
                    ) ;
			}
			catch(Exception e)
			{
				this.Err = "[Registration.Booking.Insert]格式不匹配!"+e.Message;
				this.ErrCode = e.Message;
				return -1;
			}

			return this.ExecNoQuery(sql);			
		}
		#endregion

		#region 删除
		/// <summary>
		/// 根据id删除一条预约挂号信息
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public int Delete (string ID)
		{
			string sql = "";

			if(this.Sql.GetCommonSql("Registration.Booking.Delete",ref sql ) == -1)return -1;

			try
			{
				sql = string.Format(sql,ID);
			}
			catch(Exception e)
			{
				this.Err = "[Registration.Booking.Delete]格式不匹配!"+e.Message;
				this.ErrCode = e.Message;
				return -1;
			}

			return this.ExecNoQuery(sql);	
		}
		#endregion

		#region 更新
		/// <summary>
		/// 置预约信息看诊标志
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="IsSee"></param>
        /// <param name="ConfirmID"></param>
        /// <param name="ConfirmDate"></param>
		/// <returns></returns>
		public int Update(string ID,bool IsSee,string ConfirmID,DateTime ConfirmDate)
		{
			string sql = "";

			if(this.Sql.GetCommonSql("Registration.Booking.Update",ref sql ) == -1)return -1;

			try
			{
				sql = string.Format(sql,ID,FS.FrameWork.Function.NConvert.ToInt32(IsSee),ConfirmID,ConfirmDate.ToString());
			}
			catch(Exception e)
			{
				this.Err = "[Registration.Booking.Update]格式不匹配!"+e.Message;
				this.ErrCode = e.Message;
				return -1;
			}

			return this.ExecNoQuery(sql);	
		}
		
		#endregion

		#region 查询
		private ArrayList al ;
		private FS.HISFC.Models.Registration.Booking objBooking;

		/// <summary>
		/// 按sql查询
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public ArrayList QueryBase(string sql)
		{
			if(this.ExecQuery(sql) == -1) return null;

			this.al = new ArrayList();

			try
			{
				while(this.Reader.Read())
				{
					this.objBooking = new FS.HISFC.Models.Registration.Booking() ;
					
					this.objBooking.ID = this.Reader[0].ToString() ;
					this.objBooking.PID.CardNO = this.Reader[1].ToString() ;
					this.objBooking.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[2].ToString()) ;
					this.objBooking.DoctorInfo.Templet.Noon.ID = this.Reader[3].ToString() ;
					this.objBooking.Name = this.Reader[4].ToString() ;
					this.objBooking.IDCard = this.Reader[5].ToString() ;
					this.objBooking.Sex.ID = this.Reader[6].ToString() ;
					this.objBooking.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7].ToString());
					this.objBooking.PhoneHome = this.Reader[8].ToString() ;
					this.objBooking.AddressHome = this.Reader[9].ToString() ;
					this.objBooking.DoctorInfo.Templet.Dept.ID = this.Reader[10].ToString(); 
					this.objBooking.DoctorInfo.Templet.Dept.Name = this.Reader[11].ToString() ;
					this.objBooking.DoctorInfo.Templet.Begin = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[12].ToString());
					this.objBooking.DoctorInfo.Templet.End = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[13].ToString());
					this.objBooking.DoctorInfo.Templet.Doct.ID = this.Reader[14].ToString();
					this.objBooking.DoctorInfo.Templet.Doct.Name = this.Reader[15].ToString() ;
					this.objBooking.IsSee = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[16].ToString());
					this.objBooking.Oper.ID = this.Reader[17].ToString() ;
					this.objBooking.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[18].ToString()) ;
					this.objBooking.DoctorInfo.Templet.IsAppend = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[19].ToString()) ;
					this.objBooking.DoctorInfo.Templet.ID = this.Reader[20].ToString() ;
                    this.objBooking.ConfirmOper.ID = this.Reader[21].ToString();//确认人
                    this.objBooking.ConfirmOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[22].ToString());
                    this.objBooking.DoctorInfo.Templet.RegLevel.ID = this.Reader[23].ToString();;
                    this.objBooking.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[24].ToString());
                    
                    //预约信息
                    if (this.Reader.FieldCount > 25)
                    {
                        this.objBooking.RegID = this.Reader[25].ToString();
                    }
                    //预约信息
                    if (this.Reader.FieldCount > 27)
                    {
                        this.objBooking.BookingTypeId = this.Reader[26] != DBNull.Value ? this.Reader[26].ToString() : string.Empty;
                        this.objBooking.BookingTypeName = this.Reader[27] != DBNull.Value ? this.Reader[27].ToString() : string.Empty;
                    }

                    //全院序号和当前序号
                    if (this.Reader.FieldCount > 29)
                    {
                        this.objBooking.OrderNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[28].ToString());
                        this.objBooking.DoctorInfo.SeeNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[29].ToString());
                    }

					this.al.Add(this.objBooking);
				}

				this.Reader.Close();
			}
			catch(Exception e)
			{
				this.Err = "查询患者预约挂号信息出错!" + e.Message;
				this.ErrCode = e.Message;
				return null;
			}

			return al;
		}

		/// <summary>
		/// 根据病历号查询患者预约信息
		/// </summary>
		/// <param name="CardNo"></param>
		/// <returns></returns>
		public FS.HISFC.Models.Registration.Booking Get(string CardNo)
		{
			string sql = "",where = "";

			if(this.Sql.GetCommonSql("Registration.Booking.Query.1",ref sql) == -1)return null;
			if(this.Sql.GetCommonSql("Registration.Booking.Query.2",ref where ) == -1)return null;

			sql = sql + " " + where;

			try
			{
				sql = string.Format(sql,CardNo) ;
			}
			catch(Exception e)
			{
				this.Err = "[Registration.Booking.Query.2]格式不匹配!"+e.Message;
				this.ErrCode = e.Message;
				return null;
			}
			if(this.QueryBase(sql) == null)return null; 

			if(al == null )
			{
				return null ;
			}
			else if(al.Count == 0)
			{
				return new FS.HISFC.Models.Registration.Booking() ;
			}
			else
			{
				return (FS.HISFC.Models.Registration.Booking)al[0] ;
			}
			
		}

		/// <summary>
		/// 检索操作员每日预约患者
		/// </summary>
		/// <param name="OperDate"></param>
		/// <param name="OperID"></param>
		/// <returns></returns>
		public ArrayList Query(DateTime OperDate,string OperID)
		{
			string sql = "",where = "";

			if(this.Sql.GetCommonSql("Registration.Booking.Query.1",ref sql) == -1)return null;
			if(this.Sql.GetCommonSql("Registration.Booking.Query.4",ref where ) == -1)return null;

			sql = sql + " " + where;

			try
			{
				sql = string.Format(sql,OperDate.Date.ToString(),OperID) ;
			}
			catch(Exception e)
			{
				this.Err = "[Registration.Booking.Query.4]格式不匹配!"+e.Message;
				this.ErrCode = e.Message;
				return null;
			}
			return this.QueryBase(sql);
		}

		/// <summary>
		/// 按预约号查询挂号信息
		/// </summary>
		/// <param name="ClinicCode"></param>
		/// <returns></returns>
		public FS.HISFC.Models.Registration.Booking GetByID(string ClinicCode)
		{
			string sql = "",where = "";

			if(this.Sql.GetCommonSql("Registration.Booking.Query.1",ref sql) == -1)return null;
			if(this.Sql.GetCommonSql("Registration.Booking.Query.5",ref where ) == -1)return null;

			sql = sql + " " + where;

			try
			{
				sql = string.Format(sql,ClinicCode) ;
			}
			catch(Exception e)
			{
				this.Err = "[Registration.Booking.Query.5]格式不匹配!"+e.Message;
				this.ErrCode = e.Message;
				return null;
			}

			if(this.QueryBase(sql) == null)return null ;

			if(al == null )
			{
				return null ;
			}
			else if(al.Count == 0)
			{
				return new FS.HISFC.Models.Registration.Booking() ;
			}
			else
			{
				return (FS.HISFC.Models.Registration.Booking)al[0] ;
			}
		}

         /// <summary>
         /// 检索操作员每日预约患者
         /// </summary>
         /// <param name="OperDate"></param>
         /// <param name="OperID"></param>
         /// <returns></returns>
         public ArrayList Query(DateTime OperDate)
         {
             string sql = "", where = "";
 
             if (this.Sql.GetCommonSql("Registration.Booking.Query.1", ref sql) == -1) return null;
             if (this.Sql.GetCommonSql("Registration.Booking.Query.6", ref where) == -1) return null;
 
             sql = sql   +" " +  where;
 
             try
             {
                 sql = string.Format(sql, OperDate.Date.ToString());
             }
             catch (Exception e)
             {
                 this.Err = "[Registration.Booking.Query.6]格式不匹配!" +  e.Message;
                 this.ErrCode = e.Message;
                 return null;
             }
             return this.QueryBase(sql);
         }

        /// <summary>
        /// 挂号时候回写预约表，以便跟踪
        /// </summary>
        /// <param name="bookId">预约ID</param>
        /// <param name="RegId">挂号ID</param>
        /// <returns></returns>
         public int UpdateBookReg(string bookId, string RegId)
         {
             string sql = string.Empty;
             if (this.Sql.GetCommonSql("Registration.Booking.UpdateRegId", ref sql) == -1)
                 return -1;
             try
             {
                 sql = string.Format(sql, bookId, RegId);
                return this.ExecNoQuery(sql);
             }
             catch (Exception ex)
             {
                 this.ErrCode = ex.Message;
                 return -1;
             }
         }

		#endregion
	}
}
