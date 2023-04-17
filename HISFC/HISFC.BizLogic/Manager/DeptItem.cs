using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizLogic.Manager
{
    /// <summary>
    /// ���ҳ�����Ŀά��
    /// </summary>
    public class DeptItem : DataBase
    {
        /// <summary>
        /// ��ȡ���ҳ�����Ŀ��Ϣ
        /// </summary>
        /// <param name="lstItems">��Ŀ��Ϣ</param>
        /// <returns>1,�ɹ�;  -1,ʧ��</returns>
        public int SelectItem(ref List<FS.HISFC.Models.Base.DeptItem> lstItems)
        {
            string strsql = "";

            //
            // �����ȡ�õ�ǰ����
            //
            if (this.GetSQL("Manager.DeptItem.SelectItem.2", ref strsql) == -1)
            {
                return -1;
            }

            try
            {
                //����ط��õ�ǰ���ұ��������
                strsql = String.Format(strsql, ((FS.HISFC.Models.Base.Employee)this.Operator).Dept.ID);
            }
            catch
            {
                return -1;
            }

            if (this.ExecQuery(strsql) == -1)
            {
                return -1;
            }

            while (this.Reader.Read())
            {
                FS.HISFC.Models.Base.DeptItem di = new FS.HISFC.Models.Base.DeptItem();
                //di.DeptCode.Name = this.Reader.IsDBNull(1) ? "" : this.Reader.GetString(1);

                //di.Dept.ID = this.Reader.IsDBNull(0) ? "" : this.Reader.GetString(0);
                //di.ItemProperty.ID = this.Reader.IsDBNull(1) ? "" : this.Reader.GetString(1);
                //di.ItemProperty.Name = this.Reader.IsDBNull(2) ? "" : this.Reader.GetString(2);
                //di.UnitFlag = this.Reader.IsDBNull(3) ? "" : this.Reader.GetString(3);
                //di.BookLocate = this.Reader.IsDBNull(4) ? "" : this.Reader.GetString(4);
                //di.BookTime = this.Reader.IsDBNull(5) ? "" : this.Reader.GetString(5);
                //di.ExecuteLocate = this.Reader.IsDBNull(6) ? "" : this.Reader.GetString(6);
                //di.ReportDate = this.Reader.IsDBNull(7) ? "" : this.Reader.GetString(7);
                //di.HurtFlag = this.Reader.IsDBNull(8) ? "" : this.Reader.GetString(8);
                //di.SelfBookFlag = this.Reader.IsDBNull(9) ? "" : this.Reader.GetString(9);
                //di.ReasonableFlag = this.Reader.IsDBNull(10) ? "" : this.Reader.GetString(10);
                //di.Speciality = this.Reader.IsDBNull(11) ? "" : this.Reader.GetString(11);
                //di.ClinicMeaning = this.Reader.IsDBNull(12) ? "" : this.Reader.GetString(12);
                //di.SampleKind = this.Reader.IsDBNull(13) ? "" : this.Reader.GetString(13);
                //di.SampleWay = this.Reader.IsDBNull(14) ? "" : this.Reader.GetString(14);
                //di.SampleUnit = this.Reader.IsDBNull(15) ? "" : this.Reader.GetString(15);
                //di.SampleQty = this.Reader.IsDBNull(16) ? 0 : this.Reader.GetDecimal(16);//decimal����
                //di.SampleContainer = this.Reader.IsDBNull(17) ? "" : this.Reader.GetString(17);
                //di.Scope = this.Reader.IsDBNull(18) ? "" : this.Reader.GetString(18);
                //di.IsStat = this.Reader.IsDBNull(19) ? "" : this.Reader.GetString(19);
                //di.IsAutoBook = this.Reader.IsDBNull(20) ? "" : this.Reader.GetString(20);
                //di.ItemTime = this.Reader.IsDBNull(21) ? "" : this.Reader.GetString(21);
                //di.Memo = this.Reader.IsDBNull(22) ? "" : this.Reader.GetString(22);

                di.Dept.ID = (this.Reader[0] == null ? "" : this.Reader[0].ToString());
                di.ItemProperty.ID = (this.Reader[1] == null ? "" : this.Reader[1].ToString());
                di.ItemProperty.Name = (this.Reader[2] == null ? "" : this.Reader[2].ToString());
                di.UnitFlag = (this.Reader[3] == null ? "" : this.Reader[3].ToString());
                di.BookLocate = (this.Reader[4] == null ? "" : this.Reader[4].ToString());
                di.BookTime = (this.Reader[5] == null ? "" : this.Reader[5].ToString());
                di.ExecuteLocate = (this.Reader[6] == null ? "" : this.Reader[6].ToString());
                di.ReportDate = (this.Reader[7] == null ? "" : this.Reader[7].ToString());
                di.HurtFlag = (this.Reader[8] == null ? "" : this.Reader[8].ToString());
                di.SelfBookFlag = (this.Reader[9] == null ? "" : this.Reader[9].ToString());
                di.ReasonableFlag = (this.Reader[10] == null ? "" : this.Reader[10].ToString());
                di.Speciality = (this.Reader[11] == null ? "" : this.Reader[11].ToString());
                di.ClinicMeaning = (this.Reader[12] == null ? "" : this.Reader[12].ToString());
                di.SampleKind = (this.Reader[13] == null ? "" : this.Reader[13].ToString());
                di.SampleWay = (this.Reader[14] == null ? "" : this.Reader[14].ToString());
                di.SampleUnit = (this.Reader[15] == null ? "" : this.Reader[15].ToString());
                di.SampleQty = (this.Reader[16] == null ? 0 : Convert.ToDecimal(this.Reader[16]));//decimal����
                di.SampleContainer = (this.Reader[17] == null ? "" : this.Reader[17].ToString());
                di.Scope = (this.Reader[18] == null ? "" : this.Reader[18].ToString());
                di.IsStat = (this.Reader[19] == null ? "" : this.Reader[19].ToString());
                di.IsAutoBook = (this.Reader[20] == null ? "" : this.Reader[20].ToString());
                di.ItemTime = (this.Reader[21] == null ? "" : this.Reader[21].ToString());
                di.Memo = (this.Reader[22] == null ? "" : this.Reader[22].ToString());
                di.CustomName = (this.Reader[23] == null ? "" : this.Reader[23].ToString());

                lstItems.Add(di);
            }
            this.Reader.Close();

            return 1;
        }

        /// <summary>
        /// ������Ŀ��Ϣ
        /// </summary>
        /// <param name="DeptItem">��Ŀ��Ϣ</param>
        /// <returns>1,�ɹ�;  -1,ʧ��</returns>
        public int InsertItem(FS.HISFC.Models.Base.DeptItem deptitem)
        {
            string dcode = ((FS.HISFC.Models.Base.Employee)this.Operator).Dept.ID;
            /* insert into fin_com_deptitem (parent_code,current_code,dept_code, item_code, item_name) values (fun_get_parentcode(),fun_get_currentcode(),'{0}','{1}','{2}');
             */
            string strsql = "";
            if (this.GetSQL("Manager.DeptItem.InsertItem", ref strsql) == -1)
            {
                return -1;
            }
            try
            {
                strsql = String.Format(strsql,
                                       dcode/*dcode ʵ����Ӧ�������,���ڲ���3001*/,
                                       deptitem.ItemProperty.ID,
                                       deptitem.ItemProperty.Name,
                                       deptitem.UnitFlag/*.Trim().Equals("��ϸ") ? "1" : "2"*/,
                                       deptitem.BookLocate,
                                       deptitem.BookTime,
                                       deptitem.ExecuteLocate,
                                       deptitem.ReportDate,
                                       deptitem.HurtFlag/*.Trim().Equals("��") ? "0" : "1"*/,
                                       deptitem.SelfBookFlag/*.Trim().Equals("��") ? "0" : "1"*/,
                                       deptitem.ReasonableFlag/*.Trim().Equals("��Ҫ") ? "0" : "1"*/,
                                       deptitem.Speciality,
                                       deptitem.ClinicMeaning,
                                       deptitem.SampleKind,
                                       deptitem.SampleWay,
                                       deptitem.SampleUnit,
                                       deptitem.SampleQty,/*decimal����*/
                                       deptitem.SampleContainer,
                                       deptitem.Scope,
                                       deptitem.IsStat/*.Trim().Equals("��Ҫ") ? "0" : "1"*/,
                                       deptitem.IsAutoBook/*.Trim().Equals("��Ҫ") ? "0" : "1"*/,
                                       deptitem.ItemTime,
                                       deptitem.Memo,
                                       this.Operator.ID,
                                       deptitem.CustomName
                                       );
            }
            catch
            {
                return -1;
            }
            if (this.ExecNoQuery(strsql) <= 0)
            {
                return -1;
            }
            return 1;
        }

        public int UpdateItem(FS.HISFC.Models.Base.DeptItem deptItem)
        {
            string dcode = ((FS.HISFC.Models.Base.Employee)this.Operator).Dept.ID;
            string strsql = "";
            if (this.GetSQL("Manager.DeptItem.UpdateItem.1", ref strsql) == -1)
            {
                return -1;
            }

            try
            {
                strsql = String.Format(strsql,
                                       deptItem.UnitFlag/*.Trim().Equals("��ϸ") ? "1" : "2"*/,
                                       deptItem.BookLocate,
                                       deptItem.BookTime,
                                       deptItem.ExecuteLocate,
                                       deptItem.ReportDate,
                                       deptItem.HurtFlag/*.Trim().Equals("��") ? "0" : "1"*/,
                                       deptItem.SelfBookFlag/*.Trim().Equals("��") ? "0" : "1"*/,
                                       deptItem.ReasonableFlag/*.Trim().Equals("��Ҫ") ? "0" : "1"*/,
                                       deptItem.Speciality,
                                       deptItem.ClinicMeaning,
                                       deptItem.SampleKind,
                                       deptItem.SampleWay,
                                       deptItem.SampleUnit,
                                       deptItem.SampleQty,/*decimal����*/
                                       deptItem.SampleContainer,
                                       deptItem.Scope,
                                       deptItem.IsStat/*.Trim().Equals("��Ҫ") ? "0" : "1"*/,
                                       deptItem.IsAutoBook/*.Trim().Equals("��Ҫ") ? "0" : "1"*/,
                                       deptItem.ItemTime,
                                       deptItem.Memo,
                                       this.Operator.ID,
                                       dcode/*dcode ʵ����Ӧ�������,���ڲ���3001*/,
                                       deptItem.ItemProperty.ID,
                                       deptItem.ItemProperty.Name,
                                       deptItem.CustomName
                                       );
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Source;
                return -1;
            }

            if (this.ExecNoQuery(strsql) <= 0)
            {
                return -1;
            }

            return 1;
        }
        /// <summary>
        /// ��ȡ���ҳ�����Ŀ��Ϣ(�����)
        /// </summary>
        /// <param name="lstItems">��Ŀ��Ϣ</param>
        /// <returns>1,�ɹ�;  -1,ʧ��</returns>
        public int SelectItemByUint(ref List<FS.HISFC.Models.Base.DeptItem> lstItems, string UnitFlag)
        {
            string strsql = "";

            //
            // �����ȡ�õ�ǰ����
            //
            if (this.GetSQL("Manager.DeptItem.SelectItem.4", ref strsql) == -1)
            {
                return -1;
            }

            try
            {
                //����ط��õ�ǰ���ұ��������
                strsql = String.Format(strsql, ((FS.HISFC.Models.Base.Employee)this.Operator).Dept.ID, UnitFlag);
            }
            catch
            {
                return -1;
            }

            if (this.ExecQuery(strsql) == -1)
            {
                return -1;
            }

            while (this.Reader.Read())
            {
                FS.HISFC.Models.Base.DeptItem di = new FS.HISFC.Models.Base.DeptItem();
                //di.DeptCode.Name = this.Reader.IsDBNull(1) ? "" : this.Reader.GetString(1);

                //di.Dept.ID = this.Reader.IsDBNull(0) ? "" : this.Reader.GetString(0);
                //di.ItemProperty.ID = this.Reader.IsDBNull(1) ? "" : this.Reader.GetString(1);
                //di.ItemProperty.Name = this.Reader.IsDBNull(2) ? "" : this.Reader.GetString(2);
                //di.UnitFlag = this.Reader.IsDBNull(3) ? "" : this.Reader.GetString(3);
                //di.BookLocate = this.Reader.IsDBNull(4) ? "" : this.Reader.GetString(4);
                //di.BookTime = this.Reader.IsDBNull(5) ? "" : this.Reader.GetString(5);
                //di.ExecuteLocate = this.Reader.IsDBNull(6) ? "" : this.Reader.GetString(6);
                //di.ReportDate = this.Reader.IsDBNull(7) ? "" : this.Reader.GetString(7);
                //di.HurtFlag = this.Reader.IsDBNull(8) ? "" : this.Reader.GetString(8);
                //di.SelfBookFlag = this.Reader.IsDBNull(9) ? "" : this.Reader.GetString(9);
                //di.ReasonableFlag = this.Reader.IsDBNull(10) ? "" : this.Reader.GetString(10);
                //di.Speciality = this.Reader.IsDBNull(11) ? "" : this.Reader.GetString(11);
                //di.ClinicMeaning = this.Reader.IsDBNull(12) ? "" : this.Reader.GetString(12);
                //di.SampleKind = this.Reader.IsDBNull(13) ? "" : this.Reader.GetString(13);
                //di.SampleWay = this.Reader.IsDBNull(14) ? "" : this.Reader.GetString(14);
                //di.SampleUnit = this.Reader.IsDBNull(15) ? "" : this.Reader.GetString(15);
                //di.SampleQty = this.Reader.IsDBNull(16) ? 0 : this.Reader.GetDecimal(16);//decimal����
                //di.SampleContainer = this.Reader.IsDBNull(17) ? "" : this.Reader.GetString(17);
                //di.Scope = this.Reader.IsDBNull(18) ? "" : this.Reader.GetString(18);
                //di.IsStat = this.Reader.IsDBNull(19) ? "" : this.Reader.GetString(19);
                //di.IsAutoBook = this.Reader.IsDBNull(20) ? "" : this.Reader.GetString(20);
                //di.ItemTime = this.Reader.IsDBNull(21) ? "" : this.Reader.GetString(21);
                //di.Memo = this.Reader.IsDBNull(22) ? "" : this.Reader.GetString(22);

                di.Dept.ID = (this.Reader[0] == null ? "" : this.Reader[0].ToString());
                di.ItemProperty.ID = (this.Reader[1] == null ? "" : this.Reader[1].ToString());
                di.ItemProperty.Name = (this.Reader[2] == null ? "" : this.Reader[2].ToString());
                di.UnitFlag = (this.Reader[3] == null ? "" : this.Reader[3].ToString());
                di.BookLocate = (this.Reader[4] == null ? "" : this.Reader[4].ToString());
                di.BookTime = (this.Reader[5] == null ? "" : this.Reader[5].ToString());
                di.ExecuteLocate = (this.Reader[6] == null ? "" : this.Reader[6].ToString());
                di.ReportDate = (this.Reader[7] == null ? "" : this.Reader[7].ToString());
                di.HurtFlag = (this.Reader[8] == null ? "" : this.Reader[8].ToString());
                di.SelfBookFlag = (this.Reader[9] == null ? "" : this.Reader[9].ToString());
                di.ReasonableFlag = (this.Reader[10] == null ? "" : this.Reader[10].ToString());
                di.Speciality = (this.Reader[11] == null ? "" : this.Reader[11].ToString());
                di.ClinicMeaning = (this.Reader[12] == null ? "" : this.Reader[12].ToString());
                di.SampleKind = (this.Reader[13] == null ? "" : this.Reader[13].ToString());
                di.SampleWay = (this.Reader[14] == null ? "" : this.Reader[14].ToString());
                di.SampleUnit = (this.Reader[15] == null ? "" : this.Reader[15].ToString());
                di.SampleQty = (this.Reader[16] == null ? 0 : Convert.ToDecimal(this.Reader[16]));//decimal����
                di.SampleContainer = (this.Reader[17] == null ? "" : this.Reader[17].ToString());
                di.Scope = (this.Reader[18] == null ? "" : this.Reader[18].ToString());
                di.IsStat = (this.Reader[19] == null ? "" : this.Reader[19].ToString());
                di.IsAutoBook = (this.Reader[20] == null ? "" : this.Reader[20].ToString());
                di.ItemTime = (this.Reader[21] == null ? "" : this.Reader[21].ToString());
                di.Memo = (this.Reader[22] == null ? "" : this.Reader[22].ToString());
                di.CustomName = (this.Reader[23] == null ? "" : this.Reader[23].ToString());
                di.ItemProperty.GBCode = (this.Reader[24] == null ? "" : this.Reader[24].ToString());
                di.ItemProperty.SpellCode = (this.Reader[25] == null ? "" : this.Reader[25].ToString());
                di.ItemProperty.WBCode = (this.Reader[26] == null ? "" : this.Reader[26].ToString());

                lstItems.Add(di);
            }
            this.Reader.Close();

            return 1;
        }

        /// <summary>
        /// ɾ��һ����Ŀ
        /// </summary>
        /// <param name="deptID">���ұ��</param>
        /// <param name="itemID">��Ŀ���</param>
        /// <returns>1,�ɹ�; -1,ʧ��</returns>
        public int DeleteItem(string deptID, string itemID)
        {
            string strsql = "";
            if (this.GetSQL("Manager.DeptItem.DeleteItem", ref strsql) == -1)
            {
                return -1;
            }
            try
            {
                strsql = String.Format(strsql, deptID, itemID);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Source;
                return -1;
            }

            if (this.ExecNoQuery(strsql) <= 0)
            {
                return -1;
            }
            return 1;

        }

        /// <summary>
        /// ��ȡ���ҳ�����Ŀ��Ϣ
        /// </summary>
        /// <param name="deptID">���ű��</param>
        /// <returns>�ɹ�����һ������, ���򷵻�null</returns>
        public ArrayList QueryItemByDeptID(string deptID)
        {
            ArrayList alItem = new ArrayList();

            string strsql = "";
            if (this.GetSQL("Manager.DeptItem.SelectItemByDeptID", ref strsql) == -1)
            {
                return null;
            }
            try
            {
                strsql = String.Format(strsql, deptID);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Source;
                return null;
            }
            if (this.ExecQuery(strsql) == -1)
            {
                return null;
            }
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Base.DeptItem item = new FS.HISFC.Models.Base.DeptItem();
                item.ID = this.Reader[0].ToString();
                item.Name = this.Reader[1].ToString();
                item.UserCode = this.Reader[2].ToString();
                item.SpellCode = this.Reader[3].ToString();
                item.WBCode = this.Reader[4].ToString();
				item.CustomName = this.Reader[5].ToString();
                alItem.Add(item);
            }
            this.Reader.Close();
            return alItem;
        }

        /// <summary>
        /// ��ȡ���п��ҳ�����Ŀ
        /// </summary>
        /// <returns>nullʧ��</returns>
        public ArrayList QueryItems()
        {
            string strsql = "";
            if (this.GetSQL("Manager.DeptItem.QueryItems", ref strsql) == -1)
            {
                return null;
            }
            if (this.ExecQuery(strsql) == -1)
            {
                return null;
            }
            ArrayList alItems = new ArrayList();
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Base.DeptItem di = new FS.HISFC.Models.Base.DeptItem();
                di.ItemProperty.ID = this.Reader[0].ToString();
                di.ItemProperty.Name = this.Reader[1].ToString();
                di.UserCode = this.Reader[2].ToString();
                di.SpellCode = this.Reader[3].ToString();
                di.WBCode = this.Reader[4].ToString();
				di.CustomName = this.Reader[5].ToString();
                alItems.Add(di);
            }
            this.Reader.Close();
            return alItems;
        }

        /// <summary>
        /// ��ȡ���пƳ��ò���
        /// </summary>
        /// <returns>nullʧ��</returns>
        public ArrayList QueryDept()
        {
            string strsql = "";
            if (this.GetSQL("Manager.DeptItem.QueryDepts", ref strsql) == -1)
            {
                return null;
            }
            if (this.ExecQuery(strsql) == -1)
            {
                return null;
            }
            ArrayList alDepts = new ArrayList();
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Base.DeptItem di = new FS.HISFC.Models.Base.DeptItem();
                di.Dept.ID = this.Reader[0].ToString();
                di.Dept.Name = this.Reader[1].ToString();
                di.UserCode = this.Reader[2].ToString();
                di.SpellCode = this.Reader[3].ToString();
                di.WBCode = this.Reader[4].ToString();
                alDepts.Add(di);
            }
            this.Reader.Close();
            return alDepts;
        }
        
        /// <summary>
        /// ����������Ч��ҩƷ��Ŀ
        /// </summary>
        /// <param name="ds">���ؽ��</param>
        /// <returns>1,�ɹ�; -1,ʧ��</returns>
        public int QueryPhaItem(ref System.Data.DataSet ds)
        {
            string strsql = "";
            if (this.GetSQL("Manager.DeptItem.QueryAllPhaItem", ref strsql) == -1)
            {
                return -1;
            }
            if (this.ExecQuery(strsql, ref ds) == -1)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ����������Ч��ҩƷ��Ŀ
        /// </summary>
        /// <param name="ds">���ؽ��</param>
        /// <returns></returns>
        public int QueryUndrugItem(ref System.Data.DataSet ds)
        {
            string strsql = "";
            if (this.GetSQL("Manager.DeptItem.QueryAllUndrugItem", ref strsql) == -1)
            {
                return -1;
            }
            if (this.ExecQuery(strsql, ref ds) == -1)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ����������Ч������Ŀ
        /// </summary>
        /// <param name="ds">���ؽ��</param>
        /// <returns>1,�ɹ�; -1,ʧ��</returns>
        public int QueryComboItem(ref System.Data.DataSet ds)
        {
            string strsql = "";
            if (this.GetSQL("Manager.DeptItem.QueryAllComboItem", ref strsql) == -1)
            {
                return -1;
            }
            if (this.ExecQuery(strsql, ref ds) == -1)
            {
                return -1;
            }
            return 1;
        }

        #region ��ʱ����

        /// <summary>
        /// ��ȡ���ҳ�����Ŀ��Ϣ
        /// </summary>
        /// <param name="lstItems">��Ŀ��Ϣ</param>
        /// <param name="deptID">���ұ��</param>
        /// <returns>1,�ɹ�;  -1,ʧ��</returns>
        public int SelectItem(ref List<FS.HISFC.Models.Base.DeptItem> lstItems, string deptID)
        {
            string strsql = "";

            //
            // �����ȡ�õ�ǰ����
            //
            if (this.GetSQL("Manager.DeptItem.SelectItem.3", ref strsql) == -1)
            {
                return -1;
            }

            try
            {
                //����ط��õ�ǰ���ұ��������
                strsql = String.Format(strsql, deptID);
            }
            catch
            {
                return -1;
            }

            if (this.ExecQuery(strsql) == -1)
            {
                return -1;
            }

            while (this.Reader.Read())
            {
                FS.HISFC.Models.Base.DeptItem di = new FS.HISFC.Models.Base.DeptItem();

                di.Dept.ID = (this.Reader[0] == null ? "" : this.Reader[0].ToString());
                di.ItemProperty.ID = (this.Reader[1] == null ? "" : this.Reader[1].ToString());
                di.ItemProperty.Name = (this.Reader[2] == null ? "" : this.Reader[2].ToString());
                di.UnitFlag = (this.Reader[3] == null ? "" : this.Reader[3].ToString());
                di.BookLocate = (this.Reader[4] == null ? "" : this.Reader[4].ToString());
                di.BookTime = (this.Reader[5] == null ? "" : this.Reader[5].ToString());
                di.ExecuteLocate = (this.Reader[6] == null ? "" : this.Reader[6].ToString());
                di.ReportDate = (this.Reader[7] == null ? "" : this.Reader[7].ToString());
                di.HurtFlag = (this.Reader[8] == null ? "" : this.Reader[8].ToString());
                di.SelfBookFlag = (this.Reader[9] == null ? "" : this.Reader[9].ToString());
                di.ReasonableFlag = (this.Reader[10] == null ? "" : this.Reader[10].ToString());
                di.Speciality = (this.Reader[11] == null ? "" : this.Reader[11].ToString());
                di.ClinicMeaning = (this.Reader[12] == null ? "" : this.Reader[12].ToString());
                di.SampleKind = (this.Reader[13] == null ? "" : this.Reader[13].ToString());
                di.SampleWay = (this.Reader[14] == null ? "" : this.Reader[14].ToString());
                di.SampleUnit = (this.Reader[15] == null ? "" : this.Reader[15].ToString());
                di.SampleQty = (this.Reader[16] == null ? 0 : Convert.ToDecimal(this.Reader[16]));//decimal����
                di.SampleContainer = (this.Reader[17] == null ? "" : this.Reader[17].ToString());
                di.Scope = (this.Reader[18] == null ? "" : this.Reader[18].ToString());
                di.IsStat = (this.Reader[19] == null ? "" : this.Reader[19].ToString());
                di.IsAutoBook = (this.Reader[20] == null ? "" : this.Reader[20].ToString());
                di.ItemTime = (this.Reader[21] == null ? "" : this.Reader[21].ToString());
                di.Memo = (this.Reader[22] == null ? "" : this.Reader[22].ToString());
                di.CustomName = (this.Reader[23] == null ? "" : this.Reader[23].ToString());
                di.ItemProperty.GBCode = (this.Reader[24] == null ? "" : this.Reader[24].ToString());
                di.ItemProperty.SpellCode = (this.Reader[25] == null ? "" : this.Reader[25].ToString());
                di.ItemProperty.WBCode = (this.Reader[26] == null ? "" : this.Reader[26].ToString());

                lstItems.Add(di);
            }
            this.Reader.Close();

            return 1;
        }
        
        #endregion

        #region һֱû��
        ///// <summary>
        ///// ��ȡ��ǰ���ҳ�����Ŀ��Ϣ
        ///// </summary>
        ///// <param name="lstItems">��Ŀ��Ϣ</param>
        ///// <returns>1,�ɹ�;  -1,ʧ��</returns>
        ///// <param name="bCurrent">�Ƿ��ѯ��ǰ���ҵ���Ŀ��Ϣ</param>
        //public int SelectItem(ref List<FS.HISFC.Models.Base.DeptItem> lstItems)
        //{
        //    string strsql = "";

        //    //
        //    // �����ȡ�õ�ǰ����
        //    //
        //    if (this.GetSQL("Manager.DeptItem.SelectItem.2", ref strsql) == -1)
        //    {
        //        return -1;
        //    }

        //    try
        //    {
        //        strsql = String.Format(strsql, ((FS.HISFC.Models.Base.Employee)this.Operator).Dept.ID);
        //    }
        //    catch
        //    {
        //        return -1;
        //    }

        //    if (this.ExecQuery(strsql) == -1)
        //    {
        //        return -1;
        //    }

        //    while (this.Reader.Read())
        //    {
        //        FS.HISFC.Models.Base.DeptItem di = new FS.HISFC.Models.Base.DeptItem();
        //        di.DeptCode.ID = this.Reader.IsDBNull(0) ? "" : this.Reader.GetString(0);
        //        di.DeptCode.Name = this.Reader.IsDBNull(1) ? "" : this.Reader.GetString(1);
        //        di.ItemProperty.ID = this.Reader.IsDBNull(2) ? "" : this.Reader.GetString(2);
        //        di.ItemProperty.Name = this.Reader.IsDBNull(3) ? "" : this.Reader.GetString(3);

        //        di.UnitFlag = this.Reader.IsDBNull(4) ? "" : this.Reader.GetString(4);

        //        di.BookLocate = this.Reader.IsDBNull(5) ? "" : this.Reader.GetString(5);

        //        di.BookTime = this.Reader.IsDBNull(6) ? "" : this.Reader.GetString(6);

        //        di.ExecuteLocate = this.Reader.IsDBNull(7) ? "" : this.Reader.GetString(7);

        //        di.ReportDate = this.Reader.IsDBNull(8) ? "" : this.Reader.GetString(8);

        //        di.HurtFlag = this.Reader.IsDBNull(9) ? "" : this.Reader.GetString(9);

        //        di.SelfBookFlag = this.Reader.IsDBNull(10) ? "" : this.Reader.GetString(10);

        //        di.ReasonableFlag = this.Reader.IsDBNull(11) ? "" : this.Reader.GetString(11);

        //        di.Speciality = this.Reader.IsDBNull(12) ? "" : this.Reader.GetString(12);

        //        di.ClinicMeaning = this.Reader.IsDBNull(13) ? "" : this.Reader.GetString(13);

        //        di.SampleKind = this.Reader.IsDBNull(14) ? "" : this.Reader.GetString(14);

        //        di.SampleWay = this.Reader.IsDBNull(15) ? "" : this.Reader.GetString(15);

        //        di.SampleUnit = this.Reader.IsDBNull(16) ? "" : this.Reader.GetString(16);

        //        di.SampleQty = this.Reader.IsDBNull(17) ? 0 : this.Reader.GetDecimal(17);//decimal����

        //        di.SampleContainer = this.Reader.IsDBNull(18) ? "" : this.Reader.GetString(18);

        //        di.Scope = this.Reader.IsDBNull(19) ? "" : this.Reader.GetString(19);

        //        di.IsStat = this.Reader.IsDBNull(20) ? "" : this.Reader.GetString(20);

        //        di.IsAutoBook = this.Reader.IsDBNull(21) ? "" : this.Reader.GetString(21);

        //        di.ItemTime = this.Reader.IsDBNull(22) ? "" : this.Reader.GetString(22);

        //        di.Memo = this.Reader.IsDBNull(23) ? "" : this.Reader.GetString(23);

        //        lstItems.Add(di);
        //    }
        //    this.Reader.Close();

        //    return 1;
        //}
        #endregion
    }
}