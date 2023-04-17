using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizLogic.Registration.GJLocal
{
    public class GJOutPatientInfoRegister : FS.FrameWork.Management.Database
    {
        /// <summary>
        ///  国际患者信息挂号管理类
        /// </summary>
        public GJOutPatientInfoRegister()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 增
        /// <summary>
        /// 插入国际患者登记信息表
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertGJRegisterInfo(FS.FrameWork.Models.NeuObject obj)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("GJlocal.Registration.Register.Insert", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, obj.ID ,
                    obj.Name,
                    obj.Memo,
                    obj.User01,
                    obj.User02,
                    obj.User03
                    );
            }
            catch (Exception e)
            {
                this.Err = "[GJlocal.Registration.Register.Insert]格式不匹配!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);			
        }

        /// <summary>
        /// 插入国际患者登记信息表
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertGJRegisterInfo(System.Collections.ArrayList al)
        {
            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                if (this.InsertGJRegisterInfo(obj) < 0)
                {
                    return -1;
                }
            }
            return 1;
        }
        #endregion

        #region 删

        /// <summary>
        /// 删除国际患者登记信息表
        /// </summary>
        /// <param name="clinic_code"></param>
        /// <returns></returns>
        public int DeleteGJRegisterInfo(string clinic_code)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("GJlocal.Registration.Register.Delete", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinic_code);
            }
            catch (Exception e)
            {
                this.Err = "[GJlocal.Registration.Register.Delete]格式不匹配!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        #endregion

        #region 查
        /// <summary>
        /// 按门诊流水号查询国际患者登记信息
        /// </summary>
        /// <param name="clinic_code"></param>
        /// <param name="page">'ALL'全部</param>
        /// <returns>key:所属页面+控件节点名称</returns>
        public System.Collections.Hashtable QueryGJRegisterInfo(string clinic_code,string page)
        {
            #region
            //**------------------------------
            //r.clinic_code-->obj.ID
            //r.root_name-->obj.Name
            //r.root_text-->obj.Memo
            //r.page-->obj.User01
            //r.root_type-->obj.User02
            //r.extent1-->obj.User03
            //**-------------------------------
            #endregion
            string sql = "";

            if (this.Sql.GetCommonSql("GJlocal.Registration.Register.Query.1", ref sql) == -1) return null;
            sql = string.Format(sql, clinic_code ,page);
            if (this.ExecQuery(sql) == -1) return null;

            System.Collections.Hashtable hsTemp = new System.Collections.Hashtable();
            FS.FrameWork.Models.NeuObject obj = null;
            try
            {
                while (this.Reader.Read())
                {
                    obj = new FS.FrameWork.Models.NeuObject();

                    obj.ID = this.Reader[0].ToString();//门诊流水号
                    obj.Name = this.Reader[1].ToString();//控件节点名称
                    obj.Memo = this.Reader[2].ToString();//控件节点Text
                    obj.User01 = this.Reader[3].ToString();//所属页面
                    obj.User02 = this.Reader[4].ToString();//控件类型
                    obj.User03 = this.Reader[5].ToString();//扩展属性1
                    hsTemp.Add(obj.User01 + obj.Name, obj);
                }
                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = "检索国际患者登记信息出错!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            return hsTemp;
        }

        #endregion
    }
}
