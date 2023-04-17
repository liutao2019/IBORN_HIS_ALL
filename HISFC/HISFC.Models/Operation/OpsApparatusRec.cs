using System;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Operation 
{
	/// <summary>
	/// OpsApparatusRec ��ժҪ˵����
	/// �������ϰ���ʵ����
	/// </summary>
    [Serializable]
    public class OpsApparatusRec : FS.FrameWork.Models.NeuObject
	{
		public OpsApparatusRec()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// �������
		/// </summary>
		public string OperationNo = "";
		/// <summary>
		/// �����豸ʵ�����
		/// </summary>
		public OpsApparatus OpsAppa = new OpsApparatus();
		/// <summary>
		/// 1��ǰ����2�����¼
		/// </summary>
		public int foreflag = 1;
		/// <summary>
		/// ����
		/// </summary>
		public int Qty = 0;
		/// <summary>
		/// ���ϵ�λ
		/// </summary>
		public string AppaUnit = "";		
		/// <summary>
		/// ����Ա
		/// </summary>
		public FS.HISFC.Models.Base.Employee User = new Employee();

		public new OpsApparatusRec Clone()
		{
			OpsApparatusRec newRec = new OpsApparatusRec();
			newRec.OperationNo = this.OperationNo;
			newRec.OpsAppa = this.OpsAppa.Clone();
			newRec.foreflag = this.foreflag;
			newRec.Qty = this.Qty;
			newRec.AppaUnit = this.AppaUnit;
			newRec.User = this.User.Clone();
			return newRec;
		}
	}
}
