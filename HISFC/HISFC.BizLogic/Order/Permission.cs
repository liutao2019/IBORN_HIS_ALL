using System;
using FS.HISFC.Models;
using System.Collections;
using FS.FrameWork.Models;

namespace FS.HISFC.BizLogic.Order
{
	/// <summary>
	/// Permission 的摘要说明。
	/// 会诊权限
	/// </summary>
	public class Permission:FS.FrameWork.Management.Database
	{
		public Permission()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region 增删改
		/// <summary>
		/// 插入权限记录
		/// </summary>
		/// <param name="consultation"></param>
		/// <returns></returns>
		public int InsertPermission(FS.HISFC.Models.Order.Consultation consultation)
		{
			#region "接口"
			//            ,   --住院流水号
			//            ,   --授权科室代码
			//            ,   --授权医师代号
			//            ,   --授权医师姓名
			//            ,   --医嘱权限
			//            ,   --处方起始日
			//            ,   --处方结束日
			//            ,   --备注
			//            ,   --用户代码
			//             ); --授权时间
			#endregion
			string strSql = "";
		
			if (this.Sql.GetCommonSql("Order.Permission.InsertItem.1",ref strSql) == -1)
			{
				this.Err = this.Sql.Err;
				return -1;
			}
			try
			{
				strSql = string.Format(strSql,consultation.InpatientNo,consultation.DeptConsultation.ID,
					consultation.DoctorConsultation.ID,consultation.DoctorConsultation.Name,consultation.Name,consultation.BeginTime.ToString(),
					consultation.EndTime.ToString(),consultation.Memo,this.Operator.ID);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// 更新权限记录
		/// </summary>
		/// <param name="consultation"></param>
		/// <returns></returns>
		public int UpdatePermission(FS.HISFC.Models.Order.Consultation consultation)
		{
			#region "接口"
			//            ,   --流水号
			//            ,   --住院流水号
			//            ,   --授权科室代码
			//            ,   --授权医师代号
			//            ,   --授权医师姓名
			//            ,   --医嘱权限
			//            ,   --处方起始日
			//            ,   --处方结束日
			//            ,   --备注
			//            ,   --用户代码
			//             ); --授权时间
			#endregion
			string strSql = "";
			if (this.Sql.GetCommonSql("Order.Permission.UpdateItem.1",ref strSql) == -1)
			{
				this.Err = this.Sql.Err;
				return -1;
			}
			try
			{
				strSql = string.Format(strSql,consultation.ID,consultation.InpatientNo,consultation.DeptConsultation.ID,
					consultation.DoctorConsultation.ID,consultation.DoctorConsultation.Name,consultation.Name,
					consultation.BeginTime.ToString(),consultation.EndTime.ToString()
					,consultation.Memo,this.Operator.ID);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// 删除权限记录
		/// </summary>
		/// <param name="consultationNo"></param>
		/// <returns></returns>
		public int DeletePermission( string consultationNo )
		{
			string strSql = "";
			#region "接口"
			//传入：0 ConsultaionNo
			//传出：0
			#endregion
		
			if (this.Sql.GetCommonSql("Order.Permission.DeleteItem.1",ref strSql) == -1)
			{
				this.Err = this.Sql.Err;
				return -1;
			}
			try
			{
				strSql = string.Format(strSql,consultationNo);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				this.WriteErr();
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		#endregion

		#region 查询
		/// <summary>
		/// 获得医疗会诊权限列表
		/// </summary>
		/// <param name="inpatientNo"></param>
		/// <returns></returns>
		public ArrayList QueryPermission(string inpatientNo)
		{
			ArrayList al = new ArrayList();
			string strSql = "";
			//Order.Permission.Select.1
			//传入：0  InpatientNo
			//传出:     0 ,   --流水号
			//           1 ,   --住院流水号
			//           2 ,   --授权科室代码
			//           3 ,   --授权医师代号
			//           4 ,   --授权医师姓名
			//           5 ,   --医嘱权限
			//           6 ,   --处方起始日
			//           7,   --处方结束日
			//           8 ,   --备注
			//           9 ,   --用户代码
			//           10  ); --授权时间
			if(this.Sql.GetCommonSql("Order.Permission.Select.1",ref strSql)==0)
			{
				try
				{
					strSql=string.Format(strSql,inpatientNo);
				}
				catch(Exception ex)
				{
					this.Err=ex.Message;
					this.ErrCode=ex.Message;
					this.WriteErr();
					return null;
				}
				if(this.ExecQuery(strSql) == -1) return null;
				while(this.Reader.Read())
				{
					FS.HISFC.Models.Order.Consultation obj = new FS.HISFC.Models.Order.Consultation();
					try
					{
						obj.ID = this.Reader[0].ToString();
						obj.InpatientNo = this.Reader[1].ToString();
						obj.DeptConsultation.ID = this.Reader[2].ToString();
						obj.DoctorConsultation.ID = this.Reader[3].ToString();
						obj.DoctorConsultation.Name = this.Reader[4].ToString();
						obj.Name = this.Reader[5].ToString();
						obj.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6].ToString());
						obj.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7].ToString());
						obj.Memo = this.Reader[8].ToString();						
						obj.User01 = this.Reader[9].ToString();
						obj.User02 = this.Reader[10].ToString();
					}
					catch{}
					al.Add(obj);
				}
				this.Reader.Close();
			}
			else
			{
				return null;
			}
			return al;
		}

        /// <summary>
        /// 获得医疗会诊权限列表
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public ArrayList QueryPermissionByDoct(string doctCode)
        {
            ArrayList al = new ArrayList();
            string strSql = "";
            //Order.Permission.Select.1
            //传入：0  doctCode
            //传出:     0 ,   --流水号
            //           1 ,   --住院流水号
            //           2 ,   --授权科室代码
            //           3 ,   --授权医师代号
            //           4 ,   --授权医师姓名
            //           5 ,   --医嘱权限
            //           6 ,   --处方起始日
            //           7,   --处方结束日
            //           8 ,   --备注
            //           9 ,   --用户代码
            //           10  ); --授权时间
            if (this.Sql.GetCommonSql("Order.Permission.Select.2", ref strSql) == 0)
            {
                try
                {
                    strSql = string.Format(strSql, doctCode);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.ErrCode = ex.Message;
                    this.WriteErr();
                    return null;
                }
                if (this.ExecQuery(strSql) == -1) return null;
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Order.Consultation obj = new FS.HISFC.Models.Order.Consultation();
                    try
                    {
                        obj.ID = this.Reader[0].ToString();
                        obj.InpatientNo = this.Reader[1].ToString();
                        obj.DeptConsultation.ID = this.Reader[2].ToString();
                        obj.DoctorConsultation.ID = this.Reader[3].ToString();
                        obj.DoctorConsultation.Name = this.Reader[4].ToString();
                        obj.Name = this.Reader[5].ToString();
                        obj.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6].ToString());
                        obj.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[7].ToString());
                        obj.Memo = this.Reader[8].ToString();
                        obj.User01 = this.Reader[9].ToString();
                        obj.User02 = this.Reader[10].ToString();
                    }
                    catch { }
                    al.Add(obj);
                }
                this.Reader.Close();
            }
            else
            {
                return null;
            }
            return al;
        }
		#endregion
	}
}
