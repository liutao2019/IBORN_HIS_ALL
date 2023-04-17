using System;
using System.Collections;
namespace FS.HISFC.BizLogic.Manager
{
    /// <summary>
    /// Frequency ��ժҪ˵����
    /// </summary>
    public class Frequency : DataBase, FS.HISFC.Models.Base.IManagement
    {
        public Frequency()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region IManagement ��Ա

        public ArrayList GetList()
        {
            // TODO:  ��� Frequency.GetList ʵ��
            return null;
        }

        public int Del(object obj)
        {
            // TODO:  ��� Frequency.Del ʵ��
            #region "�ӿ�"
            //�ӿ����� Manager.Frequency.Update.1
            //<!--0 idƵ��id, 1 nameƵ������, 2 deptcode, 3 ִ��ʱ���, 4 �÷�id, 5 �÷�name,6 SortID,
            //	 7 operator id, 8 operator name,9 operator time -->
            #endregion
            string strSql = "";
            if (this.GetSQL("Manager.Frequency.Delete.1", ref strSql) == -1) return -1;
            FS.HISFC.Models.Order.Frequency o = obj as FS.HISFC.Models.Order.Frequency;
            try
            {
                string[] s = new string[10];
                try
                {
                    s[0] = o.ID;//idƵ��id
                }
                catch { }
                try
                {
                    s[1] = o.Name;//nameƵ������
                }
                catch { }
                try
                {
                    s[2] = o.Dept.ID;//deptcode
                }
                catch { }
                try
                {
                    s[3] = o.Time;//ִ��ʱ���
                }
                catch { }
                try
                {
                    s[4] = o.Usage.ID;//�÷�id
                }
                catch { }
                try
                {
                    s[5] = o.Usage.Name;//�÷�name
                }
                catch { }
                try
                {
                    s[6] = o.SortID.ToString();//SortID
                }
                catch { }
                try
                {
                    s[7] = this.Operator.ID;//operator id
                }
                catch { }
                try
                {
                    s[8] = this.Operator.Name;//operator name
                }
                catch { }
                try
                {
                    s[9] = this.GetSysDate();//operator time
                }
                catch { }
                strSql = string.Format(strSql, s);

            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            if (this.ExecQuery(strSql) < 0)
            {
                return -1;

            }

            return 0;
        }

        /// <summary>
        ///�Ѿ�ʹ�õ�Ƶ�β���ɾ��
        /// </summary>
        /// <param name="al"></param>
        /// <returns></returns>
        public int ExistFrequencyCounts(FS.HISFC.Models.Order.Frequency frequency)
        {
            string strSql = "";
            if (this.GetSQL("Manager.Frequency.DeleteCheck.1", ref strSql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ: Manager.Frequency.DeleteCheck.1��SQL���!";

                return -1;
            }

            try
            {
                strSql = string.Format(strSql, frequency.ID);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ�����" + ex.Message;
                this.WriteErr();

                return -1;
            }

            return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strSql));
        }

        public int SetList(ArrayList al)
        {
            // TODO:  ��� Frequency.SetList ʵ��
            return 0;
        }

        public FS.FrameWork.Models.NeuObject Get(object obj)
        {
            return null;
        }
        public FS.FrameWork.Models.NeuObject Get(object obj, string DeptCode)
        {
            //ѡ��
            string sql = "";
            FS.HISFC.Models.Order.Frequency f = null;
            try
            {
                f = (FS.HISFC.Models.Order.Frequency)obj;
            }
            catch { this.Err = "�������Ͳ���Ƶ�Σ�"; return null; }

            if (this.GetSQL("Manager.Frequency.Get.1", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql, f.ID, f.Usage.ID, f.Dept.ID);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }

            ArrayList al = new ArrayList();
            al = myList(sql);
            if (al == null || al.Count == 0)
            {
                this.Err = "û���ҵ�Ƶ��" + f.ID + "��ʱ�������!";
                this.WriteErr();
                return null;
            }
            if (al.Count == 1)
            {
                return al[0] as FS.FrameWork.Models.NeuObject;//һ�����ص�ǰ
            }

            int fit = 0;
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Order.Frequency tmpf = (FS.HISFC.Models.Order.Frequency)al[i];
                if (tmpf.Dept.ID == DeptCode) fit = i;
                if (tmpf.Usage.ID == f.Usage.ID) fit = i;
                if (tmpf.Usage.ID == f.Usage.ID && tmpf.Dept.ID == DeptCode)
                {
                    fit = i;
                    break;
                }
            }

            // TODO:  ��� Frequency.Get ʵ��
            return al[fit] as FS.FrameWork.Models.NeuObject;//һ�����ص�ǰ;
        }
        /// <summary>
        /// ȡȫ����Ƶ��
        /// </summary>
        /// <param name="DeptCode"></param>
        /// <returns></returns>
        public ArrayList GetAll(string DeptCode)
        {
            string strSql = "";
            if (this.GetSQL("Manager.Frequency.Get.All", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Manager.Frequency.Get.All�ֶ�";
                return null;
            }
            try
            {
                strSql = string.Format(strSql, DeptCode);
            }
            catch
            {
                this.Err = "��ʽ��������";
                return null;
            }
            return this.myList(strSql);
        }

        public int Set(FS.FrameWork.Models.NeuObject obj)
        {
            // TODO:  ��� Frequency.Set ʵ��
            #region "�ӿ�"
            //�ӿ����� Manager.Frequency.Update.1
            //<!--0 idƵ��id, 1 nameƵ������, 2 deptcode, 3 ִ��ʱ���, 4 �÷�id, 5 �÷�name,6 SortID,
            //	 7 operator id, 8 operator name,9 operator time -->
            #endregion
            string strSql = "", strSql1 = "";
            if (this.GetSQL("Manager.Frequency.Update.1", ref strSql) == -1) return -1;
            if (this.GetSQL("Manager.Frequency.Insert.1", ref strSql1) == -1) return -1;
            FS.HISFC.Models.Order.Frequency o = obj as FS.HISFC.Models.Order.Frequency;
            try
            {
                string[] s = new string[10];
                try
                {
                    s[0] = o.ID;//idƵ��id
                }
                catch { }
                try
                {
                    s[1] = o.Name;//nameƵ������
                }
                catch { }
                try
                {
                    s[2] = o.Dept.ID;//deptcode
                }
                catch { }
                try
                {
                    s[3] = o.Time;//ִ��ʱ���
                }
                catch { }
                try
                {
                    s[4] = o.Usage.ID;//�÷�id
                }
                catch { }
                try
                {
                    s[5] = o.Usage.Name;//�÷�name
                }
                catch { }
                try
                {
                    s[6] = o.SortID.ToString();//SortID
                }
                catch { }
                try
                {
                    s[7] = this.Operator.ID;//operator id
                }
                catch { }
                try
                {
                    s[8] = this.Operator.Name;//operator name
                }
                catch { }
                try
                {
                    s[9] = this.GetSysDate();//operator time
                }
                catch { }
                strSql = string.Format(strSql, s);
                strSql1 = string.Format(strSql1, s);

            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ�����" + ex.Message;
                this.WriteErr();
                return -1;
            }
            if (this.ExecNoQuery(strSql1) <= 0)
            {
                if (this.ExecNoQuery(strSql) <= 0)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }

        #endregion
        #region
        /// <summary>
        /// ��ÿ��ҵ�Ƶ��
        /// </summary>
        /// <param name="DeptCode">���ұ���</param>
        /// <returns></returns>
        public ArrayList GetList(string DeptCode)
        {
            // TODO:  ��� Frequency.GetList ʵ��
            string sql = "";
            if (this.GetSQL("Manager.Frequency.GetList.1", ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql, DeptCode);
            }
            catch { return null; }
            return myList(sql);
        }
        private ArrayList myList(string sql)
        {
            if (this.ExecQuery(sql) == -1) return null;
            ArrayList al = new ArrayList();
            #region "�ӿ�"
            //�ӿ����� Manager.Frequency.GetList.1
            //<!--0 idƵ��id, 1 nameƵ������, 2 deptcode, 3 ִ��ʱ���, 4 �÷�id, 5 �÷�name,6 SortID,
            //	 7 operator id, 8 operator name,9 operator time -->
            #endregion
            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Order.Frequency obj = new FS.HISFC.Models.Order.Frequency();
                    obj.ID = this.Reader[0].ToString();//idƵ��id
                    obj.Name = this.Reader[1].ToString();//nameƵ������
                    obj.Dept.ID = this.Reader[2].ToString();//deptcode
                    obj.Time = this.Reader[3].ToString();//ִ��ʱ���
                    obj.Memo = obj.Time;
                    obj.Usage.ID = this.Reader[4].ToString();//�÷�id
                    obj.Usage.Name = this.Reader[5].ToString();//�÷�name
                    obj.SortID = FrameWork.Function.NConvert.ToInt32(this.Reader[6].ToString());//sortid
                    obj.User02 = this.Reader[7].ToString();//operator id
                    //obj.User02 =this.Reader[8].ToString();//operator name
                    obj.User03 = this.Reader[9].ToString();//operator time
                    //Ƶ���Զ�����{6509C8F0-FC2D-40b5-AE66-AF97F8F60940}
                    obj.UserCode = this.Reader[10].ToString();//English_AB
                    al.Add(obj);
                }
                return al;
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }


        /// <summary>
        /// ��ȡ����һ��Ƶ��
        /// {24F859D1-3399-4950-A79D-BCCFBEEAB939} ������ʱ�����Ĵ���
        /// </summary>
        /// <param name="DeptCode">���Ҵ���</param>
        /// <param name="sysClass">ϵͳ���</param>
        /// <returns></returns>
        public ArrayList GetBySysClassAndID(string DeptCode, string sysClass, string frequencyID)
        {
            string strSql = "";
            if (this.GetSQL("Manager.Frequency.GetByType.FrequencyID.1", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Manager.Frequency.GetByType.1�ֶ�";
                return null;
            }
            try
            {
                strSql = string.Format(strSql, DeptCode, sysClass, frequencyID);
            }
            catch
            {
                this.Err = "��ʽ��������";
                return null;
            }
            return this.myList(strSql);
        }

        #endregion
        #region �������Ƶ��
        /// <summary>
        /// �������Ƶ�ε��ʱ��
        /// </summary>
        /// <param name="moOrder"></param>
        /// <param name="comNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Order.Frequency GetDfqspecial(string moOrder, string comNo)
        {
            string strSql = "";
            FS.HISFC.Models.Order.Frequency info = null;
            if (this.GetSQL("Order.Dfqspecial.GetDfqspecial", ref strSql) == -1) return null;

            try
            {
                strSql = string.Format(strSql, moOrder, comNo);
                if (this.ExecQuery(strSql) == -1) return null;
                if (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Order.Frequency();
                    info.ID = Reader[0].ToString();
                    info.Name = Reader[1].ToString();
                    info.Time = Reader[2].ToString();
                    info.User01 = Reader[3].ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                info = null;
            }
            finally
            {
                this.Reader.Close();
            }
            return info;
        }
        #endregion
    }
}
