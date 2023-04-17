using System;
using System.Collections;

namespace FS.HISFC.BizLogic.Registration
{
    public class Noon:FS.FrameWork.Management.Database
    {
        private ArrayList al = null;
        /// <summary>
        /// ���ʵ��
        /// </summary>
        private FS.HISFC.Models.Base.Noon noon = null;

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="noon"></param>
        /// <returns></returns>
        public int Insert(FS.HISFC.Models.Base.Noon noon)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.DoctSchema.Insert.2", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, noon.ID, noon.Name, noon.StartTime.ToString(), noon.EndTime.ToString(),
                    "", DateTime.MinValue.ToString());

                return this.ExecNoQuery(sql);

            }
            catch (Exception e)
            {
                this.Err = "���������Ϣ�����!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// ɾ��һ������¼
        /// </summary>
        /// <param name="noon"></param>
        /// <returns></returns>
        public int Delete(FS.HISFC.Models.Base.Noon noon)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Registration.DoctSchema.Delete.2", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, noon.ID);

                return this.ExecNoQuery(sql);
            }
            catch (Exception e)
            {
                this.Err = "ɾ�������Ϣʱ����!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
        }

        /// <summary>
        /// ��ѯ���
        /// </summary>
        /// <returns></returns>
        public ArrayList Query()
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.DoctSchema.Query.1", ref sql) == -1) return null;
            if (this.ExecQuery(sql) == -1) return null;

            al = new ArrayList();
            try
            {
                while (this.Reader.Read())
                {
                    noon = new FS.HISFC.Models.Base.Noon();
                    noon.ID = this.Reader[2].ToString();//id
                    noon.Name = this.Reader[3].ToString();//name

                    if (Reader.IsDBNull(4) == false)
                        noon.StartTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[4].ToString());//��ʼʱ��
                    if (Reader.IsDBNull(5) == false)
                        noon.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[5].ToString());//����ʱ��
                    if (Reader.IsDBNull(6) == false)
                        noon.IsAutoEmergency = FS.FrameWork.Function.NConvert.ToBoolean(Reader[6].ToString());//�Ƿ���

                    //noon.OperID = this.Reader[7].ToString();//����Ա
                    //noon.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[8].ToString());

                    al.Add(noon);
                }
                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = "��ȡ������" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            return al;
        }
        /// <summary>
        /// �����������ѯ�������
        /// </summary>
        /// <param name="noon_id"></param>
        /// <returns></returns>
        public string Query(string noon_id)
        {
             string sql = "";
            if (this.Sql.GetCommonSql("Registration.Noon.Query.1", ref sql) == -1) return "";
            try
            {
                sql = string.Format(sql, noon_id);
                return this.ExecSqlReturnOne(sql);

            }
            catch (Exception ex)
            {
                this.Err = "��ȡ������Ƴ���" + ex.Message;
                this.ErrCode = ex.Message;
                return "";
               
            }

        }

        /// <summary>
        /// ��ȡ��ǰʱ���������
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.Noon getNoon(DateTime current)
        {
            System.Collections.ArrayList alNoon = this.Query();
            if (alNoon == null)
            {
                this.Err = "��ȡ�����Ϣʱ����";
                return null;
            }
            /*
             * ��������Ϊ���Ӧ���ǰ���һ��ȫ��ʱ�����磺06~12,����:12~18����Ϊ����,
             * ʵ�����Ϊҽ������ʱ���,�������Ϊ08~11:30������Ϊ14~17:30
             * ��������Һ�Ա����������ʱ��ιҺ�,���п�����ʾ���δά��
             * ���Ը�Ϊ���ݴ���ʱ�����ڵ�������磺9��30��06~12֮�䣬��ô���ж��Ƿ��������
             * 06~12֮�䣬ȫ������˵��9:30���Ǹ�������
             */
            //			foreach(FS.HISFC.Models.Registration.Noon obj in alNoon)
            //			{
            //				if(int.Parse(current.ToString("HHmmss"))>=int.Parse(obj.BeginTime.ToString("HHmmss"))&&
            //					int.Parse(current.ToString("HHmmss"))<int.Parse(obj.EndTime.ToString("HHmmss")))
            //				{
            //					return obj.ID;
            //				}
            //			}
            int[,] zones = new int[,] { { 0, 120000 }, { 120000, 180000 }, { 180000, 235959 } };
            int time = int.Parse(current.ToString("HHmmss"));
            int begin = 0, end = 0;

            for (int i = 0; i < 3; i++)
            {
                if (zones[i, 0] <= time && zones[i, 1] > time)
                {
                    begin = zones[i, 0];
                    end = zones[i, 1];
                    break;
                }
            }

            foreach (FS.HISFC.Models.Base.Noon obj in alNoon)
            {
                if (int.Parse(obj.StartTime.ToString("HHmmss")) >= begin &&
                    int.Parse(obj.EndTime.ToString("HHmmss")) <= end)
                {
                    return obj;
                }
            }

            return null;
        }
    }
}
