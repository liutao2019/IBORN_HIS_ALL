using System;
using System.Collections;
namespace FS.HISFC.BizLogic.Terminal 
{
	/// <summary>
	/// ���ҽ����Ŀ��ԤԼ���롣
	/// 1.ҽ����ĿԤԼ����Ԥ����--����ҽ��վ���� write by zhouxs
	/// </summary>
	public class MedTechItemBook : FS.FrameWork.Management.Database
	{
		public MedTechItemBook()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		//#region "ҽ��ԤԼ����"
		///// <summary>
		///// ҽ��ԤԼ����
		///// </summary>
		///// <param name="objFeeItemlist"></param>
		///// <returns></returns>
		//public int CreateMedTechBookApply(FS.HISFC.Models.Fee.Outpatient.FeeItemList objFeeItemlist, FS.FrameWork.Management.Transaction t)
		//{

		//    FS.HISFC.Models.Terminal.MedTechItem objMedTechItem = new FS.HISFC.Models.Terminal.MedTechItem();
		//    FS.HISFC.BizLogic.Terminal.MedTechItem MedTechItem = new MedTechItem();
		//    //���ݿ��Һ���Ŀ�õ�ҽ����Ŀ��չ��Ϣ
		//    objMedTechItem = MedTechItem.SelectDeptItem(objFeeItemlist.FeeInfo.ExeDept.ID, objFeeItemlist.Item.ID);
		//    if (objMedTechItem == null)
		//    {
		//    }
		//    return 0;
		//}
		//#endregion
		//#region "ҽ��ԤԼ��׼"
		//public int AffirmMedTechBookApply(FS.HISFC.Models.Fee.FeeItemList objFeeItemList)
		//{

		//    return 0;
		//}

		//#endregion
		//#region "ҽ��ԤԼ����"
		///// <summary>
		///// ԤԼ����
		///// </summary>
		///// <returns></returns>
		//public int PlanTerminalBook()
		//{
		//    return 0;
		//}
		//#endregion
		//#region "ҽ��ԤԼȡ��"
		///// <summary>
		///// ҽ��ԤԼȡ��
		///// </summary>
		///// <param name="objTerminalBookInfo"></param>
		///// <returns></returns>
		//public int CancelMedTechBookApply(FS.HISFC.Models.Terminal.TerminalBookInfo objTerminalBookInfo)
		//{
		//    return 0;
		//}
		//#endregion
		//#region "����Ŀ�����ҡ�ԤԼʱ�䡢����ѯ�Ű���Ϣ"
		///// <summary>
		///// ����Ŀ�����ҡ�ԤԼʱ�䡢����ѯ�Ű���Ϣ
		///// </summary>
		///// <param name="strItem"></param>
		///// <param name="strDeptCode"></param>
		///// <param name="dtBookDate"></param>
		///// <param name="strNoonCode"></param>
		///// <returns></returns>
		//public int QueryTerminalSchema(string strItem, string strDeptCode, System.DateTime dtBookDate, string strNoonCode)
		//{
		//    return 0;
		//}

		//#endregion
		//#region "��ȡ���뵥��"

		///// <summary>
		///// ���ҽ��ԤԼ���뵥��
		///// </summary>
		///// <returns></returns>
		//private string GetMedTechBookApplyID()
		//{
		//    return "0";
		//}
		///// <summary>
		///// ��ȡҽ��ԤԼ����Ŀ�ÿ��Ҹ�ʱ���������ID��
		///// </summary>
		///// <returns></returns>
		//private string GetMedTechBookApplySortID(string strItem, string strDeptCode, System.DateTime dtBookDate, string strNoonCode)
		//{
		//    return "";
		//}

		//#endregion
		//#region "����ҽ��ԤԼ������Ŀ��"

		//private int InsertTerminalApplyInfo(FS.HISFC.Models.Terminal.MedTechBookApply objMedTechBookApply)
		//{
		//    string strSql = "";
		//    if (this.Sql.GetSql("Met.CreateMedTechBookApplyInfo", ref strSql) == -1)
		//        return -1;
		//    try
		//    {
		//        string OperId = this.Operator.ID;
		//        strSql = string.Format(strSql, GetParam(objMedTechBookApply));
		//        return this.ExecNoQuery(strSql);
		//    }
		//    catch (Exception ee)
		//    {
		//        this.Err = ee.Message;
		//        return -1;
		//    }
		//}
		//#endregion

		//#region "��ʼ��������Ϣ"
		///// <summary>
		///// 
		///// </summary>
		///// <param name="obj"></param>
		///// <returns></returns>
		//private string[] GetParam(FS.HISFC.Models.Terminal.MedTechBookApply objMedTechBookApply)
		//{
		//    string[] str = new string[] { };
		//    return str;
		//}
		//#endregion
	}
}
