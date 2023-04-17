using System;
using System.Collections;
namespace FS.HISFC.BizLogic.EPR
{
	/// <summary>
	/// QCInfo ��ժҪ˵����
	/// </summary>
	public class QCInfo:FS.FrameWork.Management.Database
	{
		public QCInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region �ʿ���Ϣ��ѯ���

		private string myInpatientNo ="";
        /// <summary>
        /// ������Ϣ���ݿ�
        /// </summary>
        public string DataStoreEMR = "DataStore_emr";
		EMR EMRManager = new EMR();//��������õĹ����
		QC  QCManager = new QC(); //�ʿز��������
		//private ArrayList alConditions =null; //����
		/// <summary>
		/// ִ���ʿ���Ϣ -
		/// </summary>
		/// <param name="inpatientNo"></param>
		/// <param name="iSql"></param>
		/// <param name="QC"></param>
		/// <returns></returns>
		public bool ExecQCInfo(string inpatientNo,FS.FrameWork.Management.Interface iSql,FS.HISFC.Models.EPR.QCConditions  QC)
		{
			int i =0;
			if(myInpatientNo != inpatientNo)//û�б�����߲����»��ߵ���Ϣ
			{
				myInpatientNo = inpatientNo;
				//���³��ñ���
                i = iSql.GetPosition("[HIS_INPATIENT_NO]");
                if (i == -1)
                {
                    this.Err = "û���ҵ�[HIS_INPATIENT_NO]";
                    return false;
                }
                iSql.SetValue("[HIS_INPATIENT_NO]", inpatientNo);
				iSql.RefreshVariant(FS.FrameWork.Models.NeuInfo.infoType.Temp,i);
			}
            ArrayList alInputs = this.QCManager.GetQCInputCondition(inpatientNo);//������ֵ
			for(i =0;i<QC.AlConditions.Count;i++)//����ÿ���ʿ�����-��д
			{
				FS.HISFC.Models.EPR.QCCondition condition = QC.AlConditions[i] as FS.HISFC.Models.EPR.QCCondition;//����
				
				if(condition == null || condition.InfoName =="" || condition.InfoCondition =="") 
				{
					this.Err ="�ʿػ������������⣡";
					this.WriteErr();
					return false;
				}
				//	"������ \'��Ϣ\'������\'����\'", 0
				//	"��HIS\'��Ϣ\'������\'����\'",  1
				//	"������ \'����\'���Ѿ�\'����\'", 2
				//	"������-\'����\'���Ѿ�\'ǩ��\'", 3
				//	"������+\'����\'������ʱ��,����\'ʱ��\'��", 4
				//	"���ؼ� \'����\'������\'����\'" 5
				string sTemp = condition.InfoName + " "+ condition.InfoCondition;

                FS.FrameWork.Management.Caculation caculation = new FS.FrameWork.Management.Caculation();
				bool bResult = false;
				FS.HISFC.Models.EPR.QC data = null;
				bool bOld = false;
				bool bNew = false;
				string a = "",b = "";
				ArrayList al = null;
				switch(condition.ID)
				{
					case "0"://����Ŀؼ����
						//a = EMRManager.GetNodeValue("datastore_emr",inpatientNo,condition.InfoName);
                        #region ����ؼ��ڵ�
                        a = "-1";
                        foreach (FS.FrameWork.Models.NeuObject obj in alInputs)
                        {
                            if (obj.Name.Trim() == condition.InfoName.Trim())
                            {
                                a = obj.User01;
                                break;
                            }
                        }
						if(a =="-1" || a =="") 
							a ="";

						sTemp = a + " "+ condition.InfoCondition;

						if(condition.InfoCondition.ToLower()=="true" )//ֻ������ֵ
						{
							if( a.Trim()!="")
								bResult = true;
							else
								bResult = false;
						}
						else
						{
							sTemp = iSql.TransSql(sTemp);
							bResult = caculation._condition(sTemp);
                        }
                        #endregion
                        break;
					case "1"://his
						sTemp = iSql.TransSql(sTemp);
						bResult = caculation._condition(sTemp);
						break;
					case "2"://���� ����
						 al = QCManager.GetQCData(inpatientNo,condition.InfoName);
						if(condition.InfoCondition =="����" || condition.InfoCondition.ToUpper()=="TRUE") bNew = true;
						if(al !=null && al.Count>0)
						{
							data  =al[0] as FS.HISFC.Models.EPR.QC;
							if(data !=null)
							{
								if(data.QCData.Creater.ID =="") bOld = false;
								else bOld = true;
								sTemp = data.QCData.Saver.ID;
							}
							else
							{
								bOld = false;
							}
						}
						else
						{
							bOld = false;
						}
						if(bNew == bOld ) 
							bResult = true;
						else
							bResult = false;
						
						sTemp = sTemp + bOld.ToString() + "&" + bNew.ToString();
						break;
					case "3"://���� ǩ��
						 al = QCManager.GetQCData(inpatientNo,condition.InfoName);
						if(condition.InfoCondition =="ǩ��" || condition.InfoCondition.ToUpper()=="TRUE") bNew = true;
						if(al !=null && al.Count>0)
						{
							data  =al[0] as FS.HISFC.Models.EPR.QC;
							if(data !=null)
							{
								if(data.QCData.Saver.ID =="") bOld = false;
								else bOld = true;

								sTemp = data.QCData.Saver.ID;
							}
							else
							{
								bOld = false;
							}
						}
						else
						{
							bOld = false;
						}

						if(bNew == bOld ) 
							bResult = true;
						else
							bResult = false;

						sTemp = sTemp + bOld.ToString() +"&"+ bNew.ToString();
						break;
					case "4"://��������ʱ��
						al = QCManager.GetQCData(inpatientNo,condition.InfoName);
						sTemp ="{0} - {1} <= {2}";
						string[] sconditions = condition.InfoCondition.Split(',');
						if(al == null) return false;		
						//���̼�¼����Ҫ������¼��
						if(sconditions.Length>1)
						{
							for(int iCondtitions =0;iCondtitions<sconditions.Length;iCondtitions++)
							{
								foreach(FS.HISFC.Models.EPR.QC data1 in al)
								{	
									if(data1 !=null)
									{
										sTemp = string.Format(sTemp,data1.QCData.Creater.Memo, caculation.f_cal(sconditions[iCondtitions]),24);
										sTemp = iSql.TransSql(sTemp);
										bResult = caculation._condition(sTemp);
										if(bResult)//�ɹ�
											break;
									}
									else
									{
										bResult = false;
									}
								}
								if(bResult == false) break;
							}
						}
						else //ʱ������һ��������
						{
							if(i >=1)
							{
								sTemp = "{0} - {1} <= {2}";
                                a = EMRManager.GetNodeValue(DataStoreEMR, inpatientNo, ((FS.HISFC.Models.EPR.QCCondition)QC.AlConditions[i - 1]).InfoName);
                                b = EMRManager.GetNodeValue(DataStoreEMR, inpatientNo, condition.InfoName);					
								if(a =="-1") 
									a ="";
								if(b =="-1") 
									b ="";
								sTemp = string.Format(sTemp,a,b,condition.InfoCondition);
								bResult = caculation._condition(sTemp);
							}
							else
							{
								sTemp = "[NOW] - {0} <= {1}";
                                a = EMRManager.GetNodeValue(DataStoreEMR, inpatientNo, condition.InfoName);
								if(a =="-1") 
									a ="";
								a = a+ condition.InfoCondition;
								sTemp = string.Format(sTemp,a,condition.InfoCondition);
								bResult = caculation._condition(sTemp);
							}
						}
						break;
					case "5"://�ؼ�������
                        a = EMRManager.GetNodeValue(DataStoreEMR, inpatientNo, condition.InfoName);
						if(a =="-1") 
							a ="";
						sTemp = a + condition.InfoCondition;
						bResult = caculation._condition(sTemp);
						break;
					default:
						break;
				}
				if(bResult== false) 
				{
					condition.Memo = sTemp +"�����"+bResult;
					return false;
				}
				condition.Memo = sTemp +"�����"+bResult;
			}
			return true;
		}
		#endregion
	}
}
