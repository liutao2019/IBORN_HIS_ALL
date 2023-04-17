using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.DrugStore.GuangZhou.GYZL.Compound
{/// <summary>
    /// [功能描述: 药品管理中常数维护]<br></br>
    /// [创 建 者: Cuip]<br></br>
    /// [创建时间: 2005-02]<br></br>
    /// <修改记录>
    ///     1、屏蔽取药科室内部无用的函数
    ///     2、药品全院特限的权限医师授权启用有效性字段by Sunjh 2010-11-23 {B5995BC9-E571-44ba-84C9-D65382C64F16}
    /// </修改记录>
    /// </summary>
    public class Compound : FS.FrameWork.Management.Database
    {
        public Compound()
        {

        }

        /// <summary>
        /// 插入配液中心配置费、附材与批次对应关系
        /// </summary>
        /// <param name="item">费用信息</param>
        /// <returns></returns>
        public int InsertCompoundFeeDetial(FS.HISFC.Models.Base.Item item)
        {
            string strSQL = "";
            if (this.Sql.GetSql("Pharmacy.Compound.InsertCompoundFeeDetial", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = { item.ID, item.Qty.ToString(), item.Price.ToString(), "3002" };   //取参数列表
                strSQL = string.Format(strSQL, strParm);                //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "付数值时候出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

    }
}
