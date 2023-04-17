using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace GZSI.ApiClass
{
    /// <summary>
    /// 广州医保API函数定义类
    /// </summary>
     public class GzsiAPI
     {

         #region 函数定义（医保接口）
         /// <summary>
         /// 接口实例化
         /// </summary>
         /// <returns>-1：失败 </returns>
         [DllImport("HG_Interface.dll")]
         public static extern IntPtr newinterface();

         /// <summary>
         /// 初始化接口
         /// </summary>
         /// <param name="pint">实例化句柄（newinterface()或者newinterfacewithinit的返回值）</param>
         /// <param name="addr">应用服务器地址</param>
         /// <param name="port">应用服务器端口号</param>
         /// <param name="servlet">应用服务器入口Servlet的名称</param>
         /// <returns>-1：失败 1：成功</returns>
         [DllImport("HG_Interface.dll")]
         public static extern long init(IntPtr pint, string addr, long port, string servlet);

         /// <summary>
         /// 接口实例化并初始化接口
         /// </summary>
         /// <param name="addr">应用服务器地址</param>
         /// <param name="port">应用服务器端口号</param>
         /// <param name="servlet">应用服务器入口Servlet的名称</param>
         /// <returns> -1：失败 </returns>
         [DllImport("HG_Interface.dll")]
         public static extern IntPtr newinterfacewithinit(string addr, int port, string servlet);

         /// <summary>
         /// 接口调用开始
         /// </summary>
         /// <param name="pint">实例化句柄（newinterface()或者newinterfacewithinit的返回值）</param>
         /// <param name="func_id">业务的功能号</param>
         /// <returns>-1：失败 1：成功</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int start(IntPtr pint, string func_id);

         /// <summary>
         /// 接口传入参数
         /// </summary>
         /// <param name="pint">实例化句柄（newinterface()或者newinterfacewithinit的返回值）</param>
         /// <param name="row">参数的行号</param>
         /// <param name="pname">参数名称</param>
         /// <param name="pvalue">参数值</param>
         /// <returns> -1:失败  >0 ：成功</returns>
         [DllImport("HG_Interface.dll", 
             EntryPoint = "put",
             CharSet=CharSet.Ansi,
             SetLastError=true,
             ExactSpelling=true,
             CallingConvention=CallingConvention.StdCall)]
         public static extern int put(IntPtr pint, int row, string pname, string pvalue);

         /// <summary>
         /// 接口传入参数
         /// </summary>
         /// <param name="pint">实例化句柄（newinterface()或者newinterfacewithinit的返回值）</param>
         /// <param name="pname">参数名称</param>
         /// <param name="pvalue">参数值</param>
         /// <returns> -1:失败  >0 ：成功 当前的行号</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int putcol(IntPtr pint, string pname, string pvalue);

         /// <summary>
         /// 接口运行
         /// </summary>
         /// <param name="pint">实例化句柄（newinterface()或者newinterfacewithinit的返回值）</param>
         /// <returns> -1:失败 >0:成功 返回参数的记录条数</returns>
         [DllImport("HG_Interface.dll", EntryPoint = "run", CharSet = CharSet.Ansi)]
         public static extern int run(IntPtr pint);

         /// <summary>
         /// 设置指定记录集（取返回记录集 或 设置入参的记录集）
         /// </summary>
         /// <param name="pint">实例化句柄（newinterface()或者newinterfacewithinit的返回值）</param>
         /// <param name="result_name">记录集名称</param>
         /// <returns>-1：失败 >=0:记录集的记录数</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int setresultset(IntPtr pint, string result_name);

         /// <summary>
         /// 设置接口的运行模式
         /// flag为1时将产生调试信息并且写入指定目录direct下的日志文件中
         /// </summary>
         /// <param name="pint">实例化句柄</param>
         /// <param name="flag">运行模式标志 0：0表示不作调试 1：调试</param>
         /// <param name="in_direct">存放调试信息日志文件的目录（此目录必须是存在的）</param>
         /// <returns>小于0 :失败  >=0:成功</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int setdebug(IntPtr pint, int flag, string in_direct);

         /// <summary>
         /// 接口取得返回的参数值
         /// </summary>
         /// <param name="pint">实例化句柄</param>
         /// <param name="pname">接口返回的字段名</param>
         /// <param name="pvalue">接口返回的数值</param>
         /// <returns> 小于0 :失败 >0:成功 当前记录集的第几条记录</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int getbyname(IntPtr pint, string pname,  StringBuilder pvalue);

         /// <summary>
         /// 接口取得返回的参数值
         /// </summary>
         /// <param name="pint">实例化句柄</param>
         /// <param name="index">返回的字段名的索引值</param>
         /// <param name="pname">接口返回的字段名</param>
         /// <param name="pvalue">接口返回的数值</param>
         /// <returns>小于0 :失败 >0:成功</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int getbyindex(IntPtr pint, int index, string pname, ref string pvalue);

         /// <summary>
         /// 获取接口函数错误信息
         /// </summary>
         /// <param name="pint">实例化句柄</param>
         /// <param name="msg">错误信息</param>
         /// <returns> 小于0：失败</returns>
         [DllImport("HG_Interface.dll", EntryPoint = "getmessage", CharSet = CharSet.Ansi)]
         public static extern int getmessage(IntPtr pint,   StringBuilder msg);

         /// <summary>
         /// 获取接口函数异常信息
         /// </summary>
         /// <param name="pint">实例化句柄</param>
         /// <param name="exception">异常信息</param>
         /// <returns> 小于0：失败</returns>
         [DllImport("HG_Interface.dll", EntryPoint = "getexception", CharSet = CharSet.Ansi)]
         public static extern int getexception(IntPtr pint,  string exception);

         /// <summary>
         /// 接口取得第index的记录集名
         /// </summary>
         /// <param name="pint">实例化句柄</param>
         /// <param name="index"></param>
         /// <param name="resultname">记录集名称</param>
         /// <returns>小于0：失败</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int getresultnamebyindex(IntPtr pint, int index, ref string resultname);

         /// <summary>
         /// 返回的当前记录集的记录行数
         /// </summary>
         /// <param name="pint">实例化句柄</param>
         /// <returns>小于0：失败</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int getrowcount(IntPtr pint);

         /// <summary>
         /// 释放接口的实例
         /// </summary>
         /// <param name="pint">实例化句柄</param>
         /// <returns> -1:失败</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int destoryinterface(IntPtr pint);

         /// <summary>
         /// 第一行
         /// </summary>
         /// <param name="pint">实例化句柄</param>
         /// <returns> -1：失败 >0 :成功 当前的行号</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int firstrow(IntPtr pint);

         /// <summary>
         /// 后一行
         /// </summary>
         /// <param name="pint">实例化句柄</param>
         /// <returns>-1：失败 >0 :成功 当前的行号</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int nextrow(IntPtr pint);

         /// <summary>
         /// 前一行
         /// </summary>
         /// <param name="pint">实例化句柄</param>
         /// <returns>-1：失败 >0 :成功 当前的行号</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int prevrow(IntPtr pint);

         /// <summary>
         /// 最后一行
         /// </summary>
         /// <param name="pint">实例化句柄</param>
         /// <returns>-1：失败 >0 :成功 当前的行号</returns>
         [DllImport("HG_Interface.dll")]
         public static extern int lastrow(IntPtr pint);

         /// <summary>
         /// 设置IC卡设备的串口号
         /// </summary>
         /// <param name="pint">实例化句柄</param>
         /// <param name="comm">IC卡连接的串口号</param>
         /// <returns></returns>
         [DllImport("HG_Interface.dll")]
         public static extern long set_ic_commport(IntPtr pint, int comm);

         /// <summary>
         /// 将数据按base64格式编码
         /// </summary>
         /// <param name="pSrc">源数据</param>
         /// <param name="nSize">源数据长度</param>
         /// <param name="pDest">编码后的数据</param>
         /// <returns>小于0：失败 大于0：成功，编码后的字节数</returns>
         [DllImport("HG_Interface.dll")]
         public static extern long encode64(string pSrc, int nSize,string pDest);

         /// <summary>
         /// 数据按base64格式解码
         /// </summary>
         /// <param name="pSrc">源数据</param>
         /// <param name="nSize">源数据长度</param>
         /// <param name="pDest">解码后的数据</param>
         /// <returns>小于0：失败 大于0：成功，解码后的字节数</returns>
         [DllImport("HG_Interface.dll")]
         public static extern long decode64(string pSrc, int nSize, string pDest);

         /// <summary>
         /// 按base64格式编码时，获得编码后的数据长度
         /// </summary>
         /// <param name="nSize">源数据长度</param>
         /// <returns>小于0：失败 大于0：成功，编码后的字节数</returns>
         [DllImport("HG_Interface.dll")]
         public static extern long encodesize(int nSize);

         /// <summary>
         /// 按base64格式解码时,获取解码后的数据长度
         /// </summary>
         /// <param name="nSize">源数据长度</param>
         /// <returns>小于0：失败 大于0：成功，解码后的字节数</returns>
         [DllImport("HG_Interface.dll")]
         public static extern long decodesize(int nSize);

         /// <summary>
         /// 按base64格式解码，并将解码后的数据存到filename文件
         /// </summary>
         /// <param name="pSrc">源数据</param>
         /// <param name="nSize">源数据长度</param>
         /// <param name="filename">解码后的数据要保存的文件名</param>
         /// <returns>小于0：失败 大于0：成功，解码后的字节数</returns>
         [DllImport("HG_Interface.dll")]
         public static extern long decode64_tofile(string pSrc, int nSize, string filename);


         #endregion

     }


}
