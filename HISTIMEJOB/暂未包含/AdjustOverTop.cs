using System;
using System.Collections;
using Neusoft.HISFC.BizProcess.Interface.Common;

namespace HISTIMEJOB
{
	/// <summary>
	/// AdjustOverTop 的摘要说明。
	/// </summary>
	public class AdjustOverTop:IJob	
	{
		public AdjustOverTop()
		{
		}
		
		private string myMessage = ""; 
		#region IJob 成员

		public string Message
		{
			get
			{
				return this.myMessage;
			}
		}

		public int Start()
		{
			// TODO:  添加 AdjustOverTop.Start 实现
            Neusoft.HISFC.BizLogic.Fee.InPatient feeMgr = new Neusoft.HISFC.BizLogic.Fee.InPatient();
            Neusoft.HISFC.BizLogic.Fee.PactUnitInfo myPact = new Neusoft.HISFC.BizLogic.Fee.PactUnitInfo();
            Neusoft.HISFC.BizLogic.RADT.InPatient patientMgr = new Neusoft.HISFC.BizLogic.RADT.InPatient();
            Neusoft.HISFC.BizProcess.Integrate.Manager conMgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();
			string begin = DateTime.MinValue.ToString();
			string end = feeMgr.GetSysDateTime();
			ArrayList al = patientMgr.PatientQuery(begin,end,"I","03");
			Hashtable htPact = new Hashtable();

			if(al==null)
			{
				this.myMessage = "查询患者信息出错"+patientMgr.Err;
				return -1;
			}
            //ArrayList alcon = new Neusoft.HISFC.BizLogic.Manager.Constant().GetList("NEEDADJUST");
            //if(alcon == null)
            //{
            //    this.myMessage = "查询需要调整合同单位出错";
            //    return -1;
            //}
            //if(alcon.Count < 1)
            //{
            //    this.myMessage = "需要调整合同单位没有维护";
            //    return -1;
            //}
            //else
            //{
            //    foreach(Neusoft.FrameWork.Models.NeuObject objP in alcon)
            //    {
            //        htPact.Add(objP.ID,objP.Name);
            //    }
            //}
            foreach (Neusoft.HISFC.Models.RADT.PatientInfo pinfo in al)
			{	
//				if(htPact.ContainsKey(pinfo.Patient.Pact.ID))
//				{
                //if(pinfo.Patient.Pact.ID.IndexOf("省") >= 0)              
				try
				{
                    if (pinfo.Pact.Name.IndexOf("省") >= 0)
                    {
                        continue;
                    }
                    Neusoft.HISFC.Models.Base.Const con = conMgr.GetConstant("BILLPACT", pinfo.Pact.ID) as Neusoft.HISFC.Models.Base.Const;
                    if (con != null)
                    {
                        if (con.Name.IndexOf("省") >= 0)
                        {
                            continue;
                        }
                    }
                    //if (conMgr.GetConstant("BILLPACT", pinfo.Pact.ID).Name.IndexOf("省") >= 0)
                    //{
                    //    continue;
                    //}
                    Neusoft.HISFC.Models.Base.PactInfo pactInfo = myPact.GetPactUnitInfoByPactCode(pinfo.Pact.ID);
					if(pactInfo != null && pactInfo.Rate.PayRate == 0)
					{
						continue;
					}
				}
				catch
				{}
                Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
                feeMgr.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
				if(feeMgr.AdjustOverLimitFee(pinfo)==-1)
				{
					Neusoft.FrameWork.Management.PublicTrans.RollBack();
					this.myMessage = this.myMessage+feeMgr.Err+pinfo.ID+pinfo.Name+"/n";
					continue;
				}
				Neusoft.FrameWork.Management.PublicTrans.Commit();
//				}
				
			}
			
			return 0;
		}

		#endregion
	}
}
