using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizLogic.Manager
{
    public class UndrugztManager : DataBase
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public UndrugztManager()
        {
        }

        /// <summary>
        /// ������Ч��������Ϣ
        /// </summary>
        /// <param name="lstUndrugzt">���ص����ݼ�</param>
        /// <returns>1,�ɹ�; -1,ʧ��</returns>
        public int QueryAllValidItemzt(ref List<FS.HISFC.Models.Fee.Item.Undrug> lstUndrugzt)
        {
            string strsql = "";
            if( this.GetSQL("Fee.Itemzt.Info", ref strsql) ==-1)
            {
                return -1;
            }
			
			//ִ�е�ǰSql���
			if (this.ExecQuery(strsql) == -1)
			{
				this.Err = this.Sql.Err;

				return -1;
			}

            try
            {
                //ѭ����ȡ����
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Fee.Item.Undrug item = new FS.HISFC.Models.Fee.Item.Undrug();

                    item.ID = this.Reader[0].ToString();//��ҩƷ���� 
                    item.Name = this.Reader[1].ToString(); //��ҩƷ���� 
                    item.SysClass.ID = this.Reader[2].ToString(); //ϵͳ���
                    item.MinFee.ID = this.Reader[3].ToString();  //��С���ô��� 
                    item.UserCode = this.Reader[4].ToString(); //������
                    item.SpellCode = this.Reader[5].ToString(); //ƴ����
                    item.WBCode = this.Reader[6].ToString();    //�����
                    item.GBCode = this.Reader[7].ToString();    //���ұ���
                    item.NationCode = this.Reader[8].ToString();//���ʱ���
                    item.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9].ToString()); //���׼�
                    item.PriceUnit = this.Reader[10].ToString();  //�Ƽ۵�λ
                    item.FTRate.EMCRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[11].ToString()); // ����ӳɱ���
                    item.IsFamilyPlanning = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[12].ToString()); // �ƻ�������� 
                    item.User01 = this.Reader[13].ToString(); //�ض�������Ŀ
                    item.Grade = this.Reader[14].ToString();//�������־
                    item.IsNeedConfirm = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[15].ToString());//ȷ�ϱ�־ 1 ��Ҫȷ�� 0 ����Ҫȷ��
                    item.ValidState = this.Reader[16].ToString(); //��Ч�Ա�ʶ ���� 1 ͣ�� 0 ���� 2   
                    item.Specs = this.Reader[17].ToString(); //���
                    item.ExecDept = this.Reader[18].ToString();//ִ�п���
                    item.MachineNO = this.Reader[19].ToString(); //�豸��� �� | ���� 
                    item.CheckBody = this.Reader[20].ToString(); //Ĭ�ϼ�鲿λ��걾
                    item.OperationInfo.ID = this.Reader[21].ToString(); // �������� 
                    item.OperationType.ID = this.Reader[22].ToString(); // ��������
                    item.OperationScale.ID = this.Reader[23].ToString(); //������ģ 
                    item.IsCompareToMaterial = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[24].ToString());//�Ƿ���������Ŀ��֮����(1�У�0û��) 
                    item.Memo = this.Reader[25].ToString(); //��ע  
                    item.ChildPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[26].ToString()); //��ͯ��
                    item.SpecialPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[27].ToString()); //�����
                    item.SpecialFlag = this.Reader[28].ToString(); //ʡ����
                    item.SpecialFlag1 = this.Reader[29].ToString(); //������
                    item.SpecialFlag2 = this.Reader[30].ToString(); //�Է���Ŀ
                    item.SpecialFlag3 = this.Reader[31].ToString();// ������
                    item.SpecialFlag4 = this.Reader[32].ToString();// ����		
                    item.DiseaseType.ID = this.Reader[35].ToString(); //��������
                    item.SpecialDept.ID = this.Reader[36].ToString();  //ר������
                    item.MedicalRecord = this.Reader[37].ToString(); //  --��ʷ�����
                    item.CheckRequest = this.Reader[38].ToString();//--���Ҫ��
                    item.Notice = this.Reader[39].ToString();//--  ע������  
                    item.IsConsent = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[40].ToString());
                    item.CheckApplyDept = this.Reader[41].ToString();//������뵥����
                    item.IsNeedBespeak = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[42].ToString());//�Ƿ���ҪԤԼ
                    item.ItemArea = this.Reader[43].ToString();//��Ŀ��Χ
                    item.ItemException = this.Reader[44].ToString();//��ĿԼ��

                    //��λ��ʶ(0,��ϸ; 1,����)[2007/01/01  xuweizhe]
                    item.UnitFlag = this.Reader.IsDBNull(45) ? "" : this.Reader.GetString(45);

                    lstUndrugzt.Add(item);
                }//ѭ������

                //�ر�Reader
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.Reader.Close();
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ������Ч�ķ�ҩƷ��Ŀ
        /// </summary>
        /// <param name="dvAllItems">���ص����ݼ�</param>
        /// <returns>1,�ɹ�; -1,ʧ��</returns>
        public int QueryAllValidItems(ref System.Data.DataView dvAllItems)
        {
            string strsql = "";
            if (this.GetSQL("Fee.Itemzt.Info.AllItem", ref strsql) == -1)
            {
                return -1;
            }

            System.Data.DataSet ds = new System.Data.DataSet();
            if (this.ExecQuery(strsql, ref ds) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            dvAllItems.Table = ds.Tables[0];
            return 1;
        }

        /// <summary>
        /// ������ϸ
        /// </summary>
        /// <param name="lstzt">���ص����ݼ�</param>
        /// <returns>1,�ɹ�; -1,ʧ��</returns>
        public int QueryUnDrugztDetail(ref List<FS.HISFC.Models.Fee.Item.UndrugComb> lstzt)
        {
            string strsql = "";
            if (this.GetSQL("Fee.Itemzt.Info.QueryZTDetails", ref strsql) == -1)
            {
                return -1;
            }
            if (this.ExecQuery(strsql) == -1)
            {
                return -1;
            }
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Fee.Item.UndrugComb zt = new FS.HISFC.Models.Fee.Item.UndrugComb();
                zt.Package.ID = this.Reader.IsDBNull(0) ? "" : this.Reader.GetString(0);
                zt.Package.Name = this.Reader.IsDBNull(1) ? "" : this.Reader.GetString(1);
                zt.ID = this.Reader.IsDBNull(2) ? "" : this.Reader.GetString(2);
                zt.Name = this.Reader.IsDBNull(3) ? "" : this.Reader.GetString(3);
                zt.SortID = this.Reader.IsDBNull(4) ? 0 : Convert.ToInt32(this.Reader.GetDecimal(4));
                zt.Qty = this.Reader.IsDBNull(5) ? 0 : this.Reader.GetDecimal(5);
                zt.ValidState = this.Reader.IsDBNull(6) ? "" : this.Reader.GetString(6);
                zt.SpellCode = this.Reader.IsDBNull(7) ? "" : this.Reader.GetString(7);
                zt.WBCode = this.Reader.IsDBNull(8) ? "" : this.Reader.GetString(8);
                zt.UserCode = this.Reader.IsDBNull(9) ? "" : this.Reader.GetString(9);

                zt.Memo = "11";//����һ����־λ,���Ϊ11��,�ٲ���ʱ��update,������insert;

                lstzt.Add(zt);
            }
            this.Reader.Close();
            return 1;
        }

        /// <summary>
        /// ��ȡ��ҩƷ������ϸ
        /// </summary>
        /// <param name="pcode">���ױ���</param>
        /// <param name="pname">��������</param>
        /// <param name="listzt">�����</param>
        /// <returns>1,�ɹ�; -1,ʧ��</returns>
        public int QueryUnDrugztDetail(string pcode,ref List<FS.HISFC.Models.Fee.Item.UndrugComb> listzt)
        {
            string strsql = "";
            if (this.GetSQL("Fee.Itemzt.Info.QueryZTDetailsByCodeName1", ref strsql) == -1)
            {
                return -1;
            }
            try
            {
                strsql = String.Format(strsql, pcode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            if (this.ExecQuery(strsql) == -1)
            {
                return -1;
            }

            while (this.Reader.Read())
            {
                FS.HISFC.Models.Fee.Item.UndrugComb zt = new FS.HISFC.Models.Fee.Item.UndrugComb();
                zt.Package.ID = this.Reader.IsDBNull(0) ? "" : this.Reader.GetString(0);
                zt.Package.Name = this.Reader.IsDBNull(1) ? "" : this.Reader.GetString(1);
                zt.ID = this.Reader.IsDBNull(2) ? "" : this.Reader.GetString(2);
                zt.Name = this.Reader.IsDBNull(3) ? "" : this.Reader.GetString(3);
                zt.SortID = this.Reader.IsDBNull(4) ? 0 : Convert.ToInt32(this.Reader.GetDecimal(4));
                zt.Qty = this.Reader.IsDBNull(5) ? 0 : this.Reader.GetDecimal(5);
                zt.ValidState = this.Reader.IsDBNull(6) ? "" : this.Reader.GetString(6);
                zt.SpellCode = this.Reader.IsDBNull(7) ? "" : this.Reader.GetString(7);
                zt.WBCode = this.Reader.IsDBNull(8) ? "" : this.Reader.GetString(8);
                zt.UserCode = this.Reader.IsDBNull(9) ? "" : this.Reader.GetString(9);
                zt.Memo = "11";//����һ����־λ,���Ϊ11��,�ٲ���ʱ��update,������insert;
                zt.Price = this.Reader.IsDBNull(10) ? 0 : Convert.ToDecimal(this.Reader.GetDecimal(10));
                zt.ChildPrice = this.Reader.IsDBNull(11) ? 0 : Convert.ToDecimal(this.Reader.GetDecimal(11));
                zt.SpecialPrice = this.Reader.IsDBNull(12) ? 0 : Convert.ToDecimal(this.Reader.GetDecimal(12));
                //{C030AC2E-6161-44a8-B82C-BF152B4FF426}
                zt.Oper.Name = this.Reader.IsDBNull(13) ? "" : this.Reader.GetString(13);
                zt.Oper.OperTime = this.Reader.IsDBNull(14) ? DateTime.MinValue : this.Reader.GetDateTime(14);

                if (this.Reader.FieldCount > 15)
                {
                    zt.ItemRate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[15]);
                }

                listzt.Add(zt);
            }
            this.Reader.Close();
            return 1;
        }

        /// <summary>
        /// ��ȡ��ҩƷ������ϸ
        /// </summary>
        /// <param name="pcode">���ױ���</param>
        /// <param name="pname">��������</param>
        /// <param name="listzt">�����</param>
        /// <returns>1,�ɹ�; -1,ʧ��</returns>
        public int QueryUnDrugztDetail(string pcode, string pname, ref List<FS.HISFC.Models.Fee.Item.UndrugComb> listzt)
        {            
            string strsql = "";
            if (this.GetSQL("Fee.Itemzt.Info.QueryZTDetailsByCodeName", ref strsql) == -1)
            {
                return -1;
            }
            try
            {
                strsql = String.Format(strsql, pcode, pname);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            if (this.ExecQuery(strsql) == -1)
            {
                return -1;
            }
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Fee.Item.UndrugComb zt = new FS.HISFC.Models.Fee.Item.UndrugComb();
                zt.Package.ID = this.Reader.IsDBNull(0) ? "" : this.Reader.GetString(0);
                zt.Package.Name = this.Reader.IsDBNull(1) ? "" : this.Reader.GetString(1);
                zt.ID = this.Reader.IsDBNull(2) ? "" : this.Reader.GetString(2);
                zt.Name = this.Reader.IsDBNull(3) ? "" : this.Reader.GetString(3);
                zt.SortID = this.Reader.IsDBNull(4) ? 0 : Convert.ToInt32(this.Reader.GetDecimal(4));
                zt.Qty = this.Reader.IsDBNull(5) ? 0 : this.Reader.GetDecimal(5);
                zt.ValidState = this.Reader.IsDBNull(6) ? "" : this.Reader.GetString(6);
                zt.SpellCode = this.Reader.IsDBNull(7) ? "" : this.Reader.GetString(7);
                zt.WBCode = this.Reader.IsDBNull(8) ? "" : this.Reader.GetString(8);
                zt.UserCode = this.Reader.IsDBNull(9) ? "" : this.Reader.GetString(9);

                zt.Memo = "11";//����һ����־λ,���Ϊ11��,�ٲ���ʱ��update,������insert;

                listzt.Add(zt);
            }
            this.Reader.Close();
            return 1;
        }

        /// <summary>
        /// �ж���Ŀ���������Ƿ����
        /// </summary>
        /// <param name="package">���ױ��</param>
        /// <param name="item">��Ŀ���</param>
        /// <returns>true,�Ѿ�ʹ��; false,û��ʹ��</returns>
        public bool IsUsed(string package, string item)
        {
            string strsql = "";
            if (this.GetSQL("Fee.Itemzt.Info.IsUsed", ref strsql) == -1)
            {
                return true;
            }
            try
            {
                strsql = String.Format(strsql, package, item);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return true;
            }
            try
            {
                int itmp = FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strsql));
                //if (this.ExecSqlReturnOne(strsql).Trim().Equals("0"))
                if( itmp <= 0)
                {
                    return false;//û��ʹ��,������
                }
            }
            catch
            {
                return true;
            }
            return true;
        }

        /// <summary>
        /// ����������ϸ
        /// </summary>
        /// <param name="lstUndrugzt">������ϸ����</param>
        /// <returns>1,�ɹ�; -1,ʧ��</returns>
        public int SaveUndrugzt(List<FS.HISFC.Models.Fee.Item.UndrugComb> lstUndrugzt)
        {
            if( lstUndrugzt.Count == 0)
            {
                return -1;
            }
            for (int i = 0, j = lstUndrugzt.Count; i < j; i++)
            {
                #region
                //insert
                //if (lstUndrugzt[i].Memo.Trim() == "")
                //{
                //    string strInsert = "";
                //    if (this.GetSQL("Fee.Itemzt.Info.Insert", ref strInsert) == -1)
                //    {
                //        return -1;
                //    }
                //    try
                //    {
                //        strInsert = String.Format(strInsert,
                //                                  lstUndrugzt[i].Package.ID,
                //                                  lstUndrugzt[i].Package.Name,
                //                                  lstUndrugzt[i].ID,
                //                                  lstUndrugzt[i].Name,
                //                                  lstUndrugzt[i].SortID,
                //                                  this.Operator.ID,
                //                                  lstUndrugzt[i].SpellCode,
                //                                  lstUndrugzt[i].WBCode,
                //                                  lstUndrugzt[i].UserCode,
                //                                  lstUndrugzt[i].ValidState,
                //                                  lstUndrugzt[i].Qty);
                //    }
                //    catch (Exception ex)
                //    {
                //        this.Err = ex.Message;
                //        return -1;
                //    }
                //    if (this.ExecNoQuery(strInsert) <= 0)
                //    {
                //        return -1;
                //    }

                //}
                //else//update
                //{
                //    string strUpdate = "";
                //    if (this.GetSQL("Fee.Itemzt.Info.Update", ref strUpdate) == -1)
                //    {
                //        return -1;
                //    }
                //    try
                //    {
                //        strUpdate = String.Format(strUpdate,
                //                                  lstUndrugzt[i].SortID,
                //                                  this.Operator.ID,
                //                                  lstUndrugzt[i].ValidState,
                //                                  lstUndrugzt[i].Qty,
                //                                  lstUndrugzt[i].Package.ID,
                //                                  lstUndrugzt[i].ID);
                //    }
                //    catch (Exception ex)
                //    {
                //        this.Err = ex.Message;
                //        return -1;
                //    }
                //    if (this.ExecNoQuery(strUpdate) <= 0)
                //    {
                //        return -1;
                //    }
                //}
                #endregion

                string strInsert = "";
                if (this.GetSQL("Fee.Itemzt.Info.Insert", ref strInsert) == -1)
                {
                    return -1;
                }
                try
                {
                    strInsert = String.Format(strInsert,
                                              lstUndrugzt[i].Package.ID,
                                              lstUndrugzt[i].Package.Name,
                                              lstUndrugzt[i].ID,
                                              lstUndrugzt[i].Name,
                                              lstUndrugzt[i].SortID,
                                              this.Operator.ID,
                                              lstUndrugzt[i].SpellCode,
                                              lstUndrugzt[i].WBCode,
                                              lstUndrugzt[i].UserCode,
                                              lstUndrugzt[i].ValidState,
                                              lstUndrugzt[i].Qty,
                                              lstUndrugzt[i].ItemRate
                                              );
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    return -1;
                }
                if (this.ExecNoQuery(strInsert) <= 0)
                {
                    string strUpdate = "";
                    if (this.GetSQL("Fee.Itemzt.Info.Update", ref strUpdate) == -1)
                    {
                        return -1;
                    }
                    try
                    {
                        strUpdate = String.Format(strUpdate,
                                                  lstUndrugzt[i].SortID,
                                                  this.Operator.ID,
                                                  lstUndrugzt[i].ValidState,
                                                  lstUndrugzt[i].Qty,
                                                  lstUndrugzt[i].Package.ID,
                                                  lstUndrugzt[i].ID,
                                                  lstUndrugzt[i].ItemRate
                                                  );
                    }
                    catch (Exception ex)
                    {
                        this.Err = ex.Message;
                        return -1;
                    }
                    if (this.ExecNoQuery(strUpdate) <= 0)
                    {
                        return -1;
                    }
                }

            }
            return 1;
        }
        /// <summary>
        /// �������׼۸�
        /// </summary>
        /// <param name="itemCode">����</param>
        /// <param name="price">���׼�</param>
        /// <param name="childPrice">��ͯ��</param>
        /// <param name="specialPrice">�����</param>
        /// <returns></returns>
        public int UpdateUndrugztPrice(string itemCode, decimal price, decimal childPrice, decimal specialPrice)
        {
            string Sql = string.Empty;
            if (this.GetSQL("Fee.Itemzt.UpdatePrice", ref Sql) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return -1;
            }
            try
            {
                Sql = string.Format(Sql, price, childPrice, specialPrice, itemCode);
            }
            catch
            {
                Err = "��ʽ��SQL���ʧ�ܣ�";
                return -1;
            }
            return this.ExecNoQuery(Sql);
        }
    }
}
