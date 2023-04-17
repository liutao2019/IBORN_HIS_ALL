﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizLogic.MedicalPackage.Base
{
    public class DataBase : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 业务层统一的获取sql方法
        /// </summary>
        /// <param name="index">sql的ID</param>
        /// <param name="SQL">SQL</param>
        /// <returns>-1失败</returns>
        public int GetSQL(string index, ref string SQL)
        {
            return this.Sql.GetSql(index, "COM_SQL", ref SQL);
        }

        /// <summary>
        /// 将sql缓存
        /// </summary>
        /// <param name="index">sql的ID</param>
        /// <param name="SQL">SQL</param>
        /// <returns>-1失败</returns>
        public int CacheSQL(string index, string SQL)
        {
            return this.Sql.CacheSql(index, SQL);
        }
    }
}
