using System.Text;
using System.Collections;
using System.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FS.SOC.Local.PubReport.BizLogic
{
    public class StatReport : FS.FrameWork.Management.Database
	{
		public StatReport()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ��ȡ������Ϣ��1�½��걨��
		/// 2ʡ��ҽ--סԺ
		/// 3�й�ҽ--סԺ
		/// 4����ҽ--סԺ
		/// 6��Ժ--סԺ
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public ArrayList GetStatByID(string ID)
		{
			string strSql1 = " select * from com_statlevel where report_code='"+ID+"'"+
				" Order by stat_code, memo";
			return this.mySatReport(strSql1);
		}
		/// <summary>
		/// ��ȡ����ҽ�ĸ�������
		/// </summary>
		/// <returns></returns>
		public ArrayList GetStatForArea()
		{
			string strSql = "select distinct c.report_code,c.report_name,"+
				" c.stat_code,c.stat_name,'CC','DD' "+
				" from com_statlevel c where c.report_code='4' order by c.stat_code";
			return this.mySatReport(strSql);
		}
		private ArrayList mySatReport(string Sql1)
		{
			ArrayList al = new ArrayList();
            SOC.Local.PubReport.Models.StatReport obj;
			if(this.ExecQuery(Sql1)==-1)return null;
			try
			{
				while(Reader.Read())
				{
                    obj = new SOC.Local.PubReport.Models.StatReport();
					obj.ID = this.Reader[0].ToString();
					obj.Name = this.Reader[1].ToString();
					obj.stat.ID = this.Reader[2].ToString();
					obj.stat.Name = this.Reader[3].ToString();
					obj.Card_No = this.Reader[4].ToString();
					obj.Memo =this.Reader[5].ToString();
					al.Add(obj);
				}
				Reader.Close();
				return al;
			}
			catch(Exception ex)
			{
				if(!Reader.IsClosed)Reader.Close();
				this.Err="�����Ϣ����"+ex.Message;
				return null;
			}
			
		}
		/// <summary>
		/// ��ȡ���ܱ���ڵ�
		/// </summary>
		/// <returns></returns>
		public ArrayList GetSimpleReport()
		{
			ArrayList al = new ArrayList();
			FS.FrameWork.Models.NeuObject obj;
			string Sql1 = "select distinct report_name,report_tag from com_statlevel where report_code = 'AAAA' order by report_tag";
			if(this.ExecQuery(Sql1)==-1)return null;
			try
			{
				while(Reader.Read())
				{
					obj = new FS.FrameWork.Models.NeuObject();
					obj.ID = this.Reader[1].ToString();
					obj.Name = this.Reader[0].ToString();					
					al.Add(obj);
				}
				Reader.Close();
				return al;
			}
			catch(Exception ex)
			{
				if(!Reader.IsClosed)Reader.Close();
				this.Err="�����Ϣ����"+ex.Message;
				return null;
			}
		}

		/// <summary>
		/// ��ȡ���ܱ���ڵ�
		/// </summary>
		/// <returns></returns>
		public ArrayList GetReportIndex()
		{
			ArrayList al = new ArrayList();
			FS.FrameWork.Models.NeuObject obj;
			string Sql1 = "select d.code,d.name,d.mark from com_dictionary d  where d.type='GFINDEX' and d.spell_code='T'order by sort_id";
			if(this.ExecQuery(Sql1)==-1)return null;
			try
			{
				while(Reader.Read())
				{
					obj = new FS.FrameWork.Models.NeuObject();
					obj.ID = this.Reader[0].ToString();
					obj.Name = this.Reader[1].ToString();			
		            obj.User01 = this.Reader[2].ToString();			
					al.Add(obj);
				}
				Reader.Close();
				return al;
			}
			catch(Exception ex)
			{
				if(!Reader.IsClosed)Reader.Close();
				this.Err="�����Ϣ����"+ex.Message;
				return null;
			}
		}


        /// <summary>
        /// ��ȡ�������Ե�������ڵ�
        /// </summary>
        /// <returns></returns>
        public ArrayList GetExportIndex()
        {
            ArrayList al = new ArrayList();
            FS.FrameWork.Models.NeuObject obj;
            string Sql1 = "select d.code,d.name,d.mark from com_dictionary d  where d.type='GFEXPORT' order by sort_id";
            if (this.ExecQuery(Sql1) == -1)
                return null;
            try
            {
                while (Reader.Read())
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.Reader[0].ToString();
                    obj.Name = this.Reader[1].ToString();
                    obj.User01 = this.Reader[2].ToString();
                    al.Add(obj);
                }
                Reader.Close();
                return al;
            }
            catch (Exception ex)
            {
                if (!Reader.IsClosed)
                    Reader.Close();
                this.Err = "�����Ϣ����" + ex.Message;
                return null;
            }
        }

		/// <summary>
		/// ��ȡ��Լ��λ
		/// </summary>
		/// <returns></returns>
		public ArrayList GetSpecialUnit()
		{
			ArrayList al = new ArrayList();
			FS.FrameWork.Models.NeuObject obj;
			string Sql1 = "select g.code,g.name from com_dictionary g where g.type='TUNIT'";
			if(this.ExecQuery(Sql1)==-1)return null;
			try
			{
				while(Reader.Read())
				{
					obj = new FS.FrameWork.Models.NeuObject();
					obj.ID = this.Reader[0].ToString();
					obj.Name = this.Reader[1].ToString();			
					//obj.User01 = this.Reader[2].ToString();			
					al.Add(obj);
				}
				Reader.Close();
				return al;
			}
			catch(Exception ex)
			{
				if(!Reader.IsClosed)Reader.Close();
				this.Err="�����Ϣ����"+ex.Message;
				return null;
			}
		}
	}
}
