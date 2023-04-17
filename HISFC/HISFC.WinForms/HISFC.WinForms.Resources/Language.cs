using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.WinForms.Resources
{
    /// <summary>
    /// Language<br></br>
    /// [功能描述:语言资源类]<br></br>
    /// [创 建 者: ]<br></br>
    /// [创建时间: 2010-09]<br></br>
    /// </summary>
    public class Language
    {
        public Language()
        {

        }
       
        /// <summary>
        /// 获取多语言信息
        /// </summary>
        /// <param name="originalStr">原始药品字符串</param>
        /// <param name="cultureLanguageCode">所需转换的语言类型</param>
        /// <returns>成功返回对应多语言药品信息</returns>
        public static string GetLanguageString(string originalStr,string cultureLanguageCode)
        {            
            System.Resources.ResourceManager r = new System.Resources.ResourceManager( "FS.HISFC.WinForms.Resources.LanguageResource." + cultureLanguageCode, System.Reflection.Assembly.GetExecutingAssembly() );

            return r.GetString( originalStr );
        }
    }
}
