using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Neusoft.SOC.Local.OutpatientFee
{
    ///// <summary>
    ///// 附材维护实体
    ///// </summary>
    //[Serializable]
    //public class OrderSubtbl : Neusoft.FrameWork.Models.NeuObject
    //{
    //    public OrderSubtbl()
    //    {
    //    }

    //    /// <summary>
    //    /// 适用范围：0 门诊；1 住院；3 全部
    //    /// </summary>
    //    private string area;

    //    /// <summary>
    //    /// 适用范围：0 门诊；1 住院；3 全部
    //    /// </summary>
    //    public string Area
    //    {
    //        get
    //        {
    //            return area;
    //        }
    //        set
    //        {
    //            area = value;
    //        }
    //    }

    //    /// <summary>
    //    /// 用法分类，0 药品按用法，1 非药品按项目代码
    //    /// </summary>
    //    private string typeCode;

    //    /// <summary>
    //    /// 用法分类，0 药品按用法，1 非药品按项目代码
    //    /// </summary>
    //    public string TypeCode
    //    {
    //        get
    //        {
    //            return typeCode;
    //        }
    //        set
    //        {
    //            typeCode = value;
    //        }
    //    }

    //    /// <summary>
    //    /// 科室代码，全院统一附材'ROOT'
    //    /// </summary>
    //    private string dept_code;

    //    /// <summary>
    //    /// 科室代码，全院统一附材'ROOT'
    //    /// </summary>
    //    public string Dept_code
    //    {
    //        get
    //        {
    //            return dept_code;
    //        }
    //        set
    //        {
    //            dept_code = value;
    //        }
    //    }

    //    /// <summary>
    //    /// 组范围：0 每组收取、1 第一组收取、2 第二组起加收
    //    /// </summary>
    //    private string combArea;

    //    /// <summary>
    //    /// 组范围：0 每组收取、1 第一组收取、2 第二组起加收
    //    /// </summary>
    //    public string CombArea
    //    {
    //        get
    //        {
    //            return combArea;
    //        }
    //        set
    //        {
    //            combArea = value;
    //        }
    //    }

    //    /// <summary>
    //    /// 收费规则：固定数量、*最大院注、*组内品种数、*最大医嘱数量、*频次
    //    /// </summary>
    //    private string feeRule;

    //    /// <summary>
    //    /// 收费规则：固定数量、*最大院注、*组内品种数、*最大医嘱数量、*频次
    //    /// </summary>
    //    public string FeeRule
    //    {
    //        get
    //        {
    //            return feeRule;
    //        }
    //        set
    //        {
    //            feeRule = value;
    //        }
    //    }

    //    /// <summary>
    //    /// 项目
    //    /// </summary>
    //    private Neusoft.FrameWork.Models.NeuObject item = new Neusoft.FrameWork.Models.NeuObject();

    //    /// <summary>
    //    /// 操作员
    //    /// </summary>
    //    private Neusoft.FrameWork.Models.NeuObject oper = new Neusoft.FrameWork.Models.NeuObject();

    //    /// <summary>
    //    /// 收费数量
    //    /// </summary>
    //    private int qty = 0;

    //    /// <summary>
    //    /// 收费数量规则
    //    /// </summary>
    //    public int Qty
    //    {
    //        get
    //        {
    //            return qty;
    //        }
    //        set
    //        {
    //            qty = value;
    //        }
    //    }

    //    /// <summary>
    //    /// 操作时间
    //    /// </summary>
    //    private DateTime operDate = DateTime.Now;

    //    /// <summary>
    //    /// 操作时间
    //    /// </summary>
    //    public DateTime OperDate
    //    {
    //        get
    //        {
    //            return operDate;
    //        }
    //        set
    //        {
    //            operDate = value;
    //        }
    //    }

    //    /// <summary>
    //    /// 操作员
    //    /// </summary>
    //    public Neusoft.FrameWork.Models.NeuObject Oper
    //    {
    //        get
    //        {
    //            return this.oper;
    //        }
    //        set
    //        {
    //            this.oper = value;
    //        }
    //    }

    //    /// <summary>
    //    /// 项目
    //    /// </summary>
    //    public Neusoft.FrameWork.Models.NeuObject Item
    //    {
    //        get
    //        {
    //            return this.item;
    //        }
    //        set
    //        {
    //            this.item = value;
    //        }
    //    }

    //    /// <summary>
    //    /// 限制类别：0 不限制 1 婴儿使用 2 非婴儿使用
    //    /// </summary>
    //    private string limitType;

    //    /// <summary>
    //    /// 限制类别：0 不限制 1 婴儿使用 2 非婴儿使用
    //    /// </summary>
    //    public string LimitType
    //    {
    //        get
    //        {
    //            return limitType;
    //        }
    //        set
    //        {
    //            limitType = value;
    //        }
    //    }
    //}

    ///// <summary>
    ///// 用法带出维护业务层
    ///// </summary>
    //public class SubtblManager : Neusoft.FrameWork.Management.Database
    //{
    //    #region 院注维护

    //    /// <summary>
    //    /// 获得对象参数
    //    /// </summary>
    //    /// <param name="obj"></param>
    //    /// <returns></returns>
    //    protected string[] myGetParmSubtblInfo(OrderSubtbl obj)
    //    {
    //        string[] strParm ={	
    //                              obj.Area,
    //                              obj.TypeCode,
    //                              obj.Dept_code,
    //                             obj.Item.ID,
    //                             obj.Qty.ToString(),
    //                             obj.CombArea,
    //                             obj.FeeRule,
    //                             obj.LimitType,
    //                             obj.Oper.ID,
    //                             obj.OperDate.ToString()
    //                         };

    //        return strParm;

    //    }

    //    /// <summary>
    //    /// 根据用法删除所有项目信息
    //    /// </summary>
    //    /// <param name="usage"></param>
    //    /// <returns></returns>
    //    public int DelSubtblInfo(string usage)
    //    {
    //        string strSql = "";
    //        if (this.Sql.GetSql("Met.Com.DelSubtblInfo.DelAllByUsage", ref strSql) == -1) return -1;
    //        try
    //        {
    //            strSql = string.Format(strSql, usage);
    //        }
    //        catch (Exception ex)
    //        {
    //            this.Err = ex.Message;
    //            this.ErrCode = ex.Message;
    //            return -1;
    //        }
    //        return this.ExecNoQuery(strSql);
    //    }

    //    /// <summary>
    //    /// 删除用法项目信息
    //    /// </summary>
    //    /// <param name="obj"></param>
    //    /// <returns></returns>
    //    public int DelSubtblInfo(OrderSubtbl obj)
    //    {
    //        string strSql = "";
    //        if (this.Sql.GetSql("Met.Com.DelSubtblInfo.Del", ref strSql) == -1) return -1;
    //        try
    //        {
    //            strSql = string.Format(strSql, obj.Area, obj.TypeCode, obj.Dept_code, obj.Item.ID);
    //        }
    //        catch (Exception ex)
    //        {
    //            this.Err = ex.Message;
    //            this.ErrCode = ex.Message;
    //            return -1;
    //        }
    //        return this.ExecNoQuery(strSql);
    //    }

    //    /// <summary>
    //    /// 插入用法项目信息
    //    /// </summary>
    //    /// <param name="obj"></param>
    //    /// <returns></returns>
    //    public int InsertSubtblInfo(OrderSubtbl obj)
    //    {
    //        string sql = string.Empty;
    //        string[] strParam;

    //        if (this.Sql.GetSql("Met.Com.InsertSubtblInfo.Insert", ref sql) == -1)
    //        {
    //            this.Err = "没有找到字段!";
    //            return -1;
    //        }
    //        try
    //        {
    //            if (obj.ID == null)
    //                return -1;
    //            strParam = this.myGetParmSubtblInfo(obj);
    //        }
    //        catch (Exception ex)
    //        {
    //            this.Err = "格式化SQL语句时出错:" + ex.Message;
    //            this.WriteErr();
    //            return -1;
    //        }
    //        return this.ExecNoQuery(sql, strParam);
    //    }

    //    /// <summary>
    //    /// 获得用法项目信息sql语句
    //    /// </summary>
    //    /// <returns></returns>
    //    public string GetSqlSubtbl()
    //    {
    //        string strSql = "";
    //        if (this.Sql.GetSql("Met.Com.GetSqlSubtbl.Select", ref strSql) == -1)
    //        {
    //            return null;
    //        }
    //        return strSql;
    //    }

    //    /// <summary>
    //    /// 获取附材项目
    //    /// </summary>
    //    /// <param name="area">适用范围：0 门诊；1 住院；3 全部</param>
    //    /// <param name="typeCode">用法分类，0 药品按用法，1 非药品按项目代码</param>
    //    /// <param name="deptCode">科室代码，全院统一附材'ROOT'</param>
    //    /// <returns></returns>
    //    public ArrayList GetSubtblInfo(string area, string typeCode, string deptCode)
    //    {
    //        string strSql1 = "";
    //        string strSql2 = "";
    //        //获得项目明细的SQL语句
    //        strSql1 = this.GetSqlSubtbl();
    //        if (this.Sql.GetSql("Met.Com.GetSqlSubtbl.Where1", ref strSql2) == -1) return null;
    //        strSql1 = strSql1 + " " + strSql2;
    //        try
    //        {
    //            strSql1 = string.Format(strSql1, area, typeCode, deptCode);
    //        }
    //        catch (Exception ex)
    //        {
    //            this.ErrCode = ex.Message;
    //            this.Err = ex.Message;
    //            return null;
    //        }
    //        return this.GetSubtblInfo(strSql1);
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="strSql"></param>
    //    /// <returns></returns>
    //    private ArrayList GetSubtblInfo(string strSql)
    //    {
    //        ArrayList al = new ArrayList();
    //        OrderSubtbl obj;
    //        this.ExecQuery(strSql);
    //        while (this.Reader.Read())
    //        {
    //            #region
    //            //USAGE_CODE	VARCHAR2(4)	N			用法代码
    //            //ITEM_CODE	VARCHAR2(12)	N			项目代码
    //            //ITEM_NAME	VARCHAR2(100)	Y			项目名称
    //            //OPER_CODE	VARCHAR2(6)	Y			操作员
    //            //OPER_DATE	DATE	Y			操作时间
    //            //USAGE_NAME	VARCHAR2(50)	Y			
    //            #endregion
    //            obj = new OrderSubtbl();
    //            try
    //            {
    //                obj.Area = this.Reader[0].ToString();//适用范围：0 门诊；1 住院；3 全部
    //                obj.TypeCode = this.Reader[1].ToString();//用法分类，0 药品按用法，1 非药品按项目代码
    //                obj.Dept_code = this.Reader[2].ToString();//科室代码，全院统一附材'ROOT'
    //                obj.Item.ID = this.Reader[3].ToString();//附加项目编码
    //                obj.Qty = Neusoft.FrameWork.Function.NConvert.ToInt32(this.Reader[4].ToString());//数量
    //                obj.CombArea = this.Reader[5].ToString();//组范围：0 每组收取、1 第一组收取、2 第二组起加收
    //                obj.FeeRule = this.Reader[6].ToString();//收费规则：固定数量、*最大院注、*组内品种数、*最大医嘱数量、*频次
    //                obj.LimitType = this.Reader[7].ToString();//限制类别：0 不限制 1 婴儿使用 2 非婴儿使用
    //                obj.Oper.ID = this.Reader[8].ToString();//操作员
    //                obj.OperDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[9].ToString());//操作时间	
    //            }
    //            catch (Exception ex)
    //            {
    //                this.Err = "查询明细赋值错误" + ex.Message;
    //                this.ErrCode = ex.Message;
    //                this.WriteErr();
    //                return null;
    //            }
    //            al.Add(obj);
    //        }
    //        this.Reader.Close();
    //        return al;
    //    }

    //    #endregion
    //}
}
