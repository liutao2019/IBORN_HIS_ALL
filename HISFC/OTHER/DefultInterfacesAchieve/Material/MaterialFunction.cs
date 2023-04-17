using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FS.DefultInterfacesAchieve.Material
{
    public class MaterialFunction : FS.FrameWork.Management.Database
    {
        public DataSet GetMaterialBaseInfo()
        {
            string sql = string.Empty;
            DataSet dt=new DataSet ();
            if (this.Sql.GetSql("Material.BaseInfo.Query", ref sql) == -1)
            {
                this.Err = "没有找到Material.BaseInfo.Query";
                return null;
            }

            if (this.Sql.ExecQuery(sql,ref dt) == -1)
            {
                this.Err = "查询出错";
                return null;
            }

            return dt;
        }
    }
}
