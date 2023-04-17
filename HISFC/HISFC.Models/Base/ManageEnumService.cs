using System;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Base
{
	/// <summary>
	/// SexEnumService <br></br>
	/// [功能描述: 管理级别]<br></br>
	/// [创 建 者: s lion h]<br></br>
	/// [创建时间: 2021-05-26]<br></br>
    /// {6F68AB52-332C-4efa-A6DD-F6BDB37B1283}
	/// <修改记录
	///		修改人=''
	///		修改时间=''
	///		修改目的=''
	///		修改描述=''
	///  />
	/// </summary>
    [System.Serializable]
    public class ManageEnumService : EnumServiceBase
	{
        public ManageEnumService()
        {
            items[EnumManageClass.I] = "I类";
            items[EnumManageClass.II] = "II类";
            items[EnumManageClass.III] = "III类";
            items[EnumManageClass.NM] = "不作为医疗器械管理";
            items[EnumManageClass.N] = "无";
        }

		#region 变量

		/// <summary>
		/// 患者类别
		/// </summary>
        EnumManageClass enumManageClass;

		/// <summary>
		/// 存储枚举定义
		/// </summary>
		protected static Hashtable items = new Hashtable();

		#endregion

		#region 属性

		/// <summary>
		/// 存贮枚举名称
		/// </summary>
		protected override Hashtable Items
		{
			get
			{
				return items;
			}
		}
		
		/// <summary>
		/// 枚举项目
		/// </summary>
		protected override Enum EnumItem
		{
			get
			{
                return this.enumManageClass;
			}
		}

		#endregion

		/// <summary>
		/// 克隆
		/// </summary>
		/// <returns></returns>
        public new ManageEnumService Clone()
		{
            ManageEnumService enumService = base.Clone() as ManageEnumService;
            
			return enumService;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new static ArrayList List()
        {
            return (new ArrayList( GetObjectItems(items)));
        }
	}



    /// <summary>
    /// 器材管理类别
    /// </summary>
    public enum EnumManageClass
    {
        /// <summary>
        /// I类
        /// </summary>
        I = 1,
        /// <summary>
        /// II类
        /// </summary>
        II = 2,
        /// <summary>
        /// III类
        /// </summary>
        III = 3,
        /// <summary>
        /// 不作为医疗器械管理
        /// </summary>
        NM = 4,
        /// <summary>
        /// 无
        /// </summary>
        N = 5
    }
}
