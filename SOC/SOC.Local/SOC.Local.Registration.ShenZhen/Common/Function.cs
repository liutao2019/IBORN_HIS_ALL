using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Registration.ShenZhen.Common
{
    /// <summary>
    /// [功能描述: 本地化公用函数]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-1]<br></br>
    /// 说明：
    /// </summary>
    public class Function
    {


        /// <summary>
        /// 获取二维条形码图片，注：没有异常处理
        /// </summary>
        /// <param name="code">需要编码的字符串</param>
        /// <returns>二维码图片，null第三方dll返回值不是图片格式</returns>
        public static System.Drawing.Image Create2DBarcode(string code)
        {
            FS.SOC.Public.Assembly.Models.SOCPropert propertInfo = new FS.SOC.Public.Assembly.Models.SOCPropert();
            propertInfo.Name = "QRCodeErrorCorrect";
            propertInfo.Value = "H";

            List<FS.SOC.Public.Assembly.Models.SOCPropert> listPropert = new List<FS.SOC.Public.Assembly.Models.SOCPropert>();
            listPropert.Add(propertInfo);

            object value = FS.SOC.Public.Assembly.Method.Invoke
                (
                "ThoughtWorks.QRCode.dll",//dll名称
                "ThoughtWorks.QRCode.Codec.QRCodeEncoder",//类名称
                "Encode",//方法名称
                new Type[] { typeof(string) },//参数类型，必须保证顺序、个数一致
                new object[] { code },//参数，必须保证顺序类型、正确
                listPropert//属性值，指定属性名称和属性的值
                );

            if (value is System.Drawing.Image)
            {
                return value as System.Drawing.Image;
            }
            return null;
        }

        /// <summary>
        /// 生成条形码方法
        /// </summary>
        /// <param name="code">条形码编码</param>
        /// <param name="with">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>null 提供条形码的dll中返回值不是imge类型</returns>
        public static System.Drawing.Image CreateBarCode(string code, int with, int height)
        {
            List<FS.SOC.Public.Assembly.Models.SOCPropert> listPropert = new List<FS.SOC.Public.Assembly.Models.SOCPropert>();
            FS.SOC.Public.Assembly.Models.SOCPropert propertInfo = new FS.SOC.Public.Assembly.Models.SOCPropert();
            propertInfo.Name = "IncludeLabel";
            propertInfo.Value = "True";
            listPropert.Add(propertInfo);

            propertInfo = new FS.SOC.Public.Assembly.Models.SOCPropert();
            propertInfo.Name = "Alignment";
            propertInfo.Value = "CENTER";
            listPropert.Add(propertInfo);

            //反射dll
            System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom("BarcodeLib.dll");

            //获取dll中的class
            Type tType = assembly.GetType("BarcodeLib.TYPE");

            //参数类型，必须保证顺序、个数一致
            Type[] parametersType = new Type[] { tType, typeof(string), typeof(System.Drawing.Color), typeof(System.Drawing.Color), typeof(int), typeof(int) };
            //参数，必须保证顺序类型、正确
            object[] parametersValue = new object[] { System.Enum.Parse(tType, "CODE128"), code, System.Drawing.Color.Black, System.Drawing.Color.White, with, height };

            object value = FS.SOC.Public.Assembly.Method.Invoke
                (
                "BarcodeLib.dll",//dll名称
                "BarcodeLib.Barcode",//类名称
                "Encode",//方法名称
                parametersType,
                parametersValue,
                listPropert//属性值，指定属性名称和属性的值
                );

            if (value is System.Drawing.Image)
            {
                return value as System.Drawing.Image;
            }
            return null;
        }


        #region 获取门诊收费日结管理类

        public static FS.SOC.HISFC.OutpatientFee.BizProcess.DayReport GetDayReportBizProcess()
        {
            return new FS.SOC.HISFC.OutpatientFee.BizProcess.DayReport();
        }

        #endregion


    }
}
