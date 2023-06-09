namespace FS.HISFC.Models.PhysicalExamination.Management 
{
	/// <summary>
	/// Business <br></br>
	/// [功能描述: 体检业务实体]<br></br>
	/// [创 建 者: 飞斯]<br></br>
	/// [创建时间: 2006-11-10]<br></br>
	/// <修改记录
	///		修改人=''
	///		修改时间=''
	///		修改目的=''
	///		修改描述=''
	///  />
	/// </summary>
    [System.Serializable]
    public class Business : FS.HISFC.Models.PhysicalExamination.Base.PE 
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public Business()
		{
			
		}
		
		/// <summary>
		/// 克隆
		/// </summary>
		/// <returns></returns>
		public new Business Clone()
		{
			return base.Clone() as Business;
		}
	}
}
