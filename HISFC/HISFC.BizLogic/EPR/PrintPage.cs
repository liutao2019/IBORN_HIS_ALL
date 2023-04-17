using System;
using System.Xml;
using System.Collections;
using System.Text;

namespace FS.HISFC.BizLogic.EPR
{
    public class PrintPage:FS.FrameWork.Management.Database 
    {
        
		/// <summary>
		/// �����ӡҳ
		/// </summary>
		/// <param name="PrintPage">Ȩ��ʵ��</param>
        /// <param name="img">�޸ĺۼ�</param>
		/// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
		public int SetPrintPage(FS.HISFC.Models.EPR.EPRPrintPage printPage, byte[] img) 
		{
			//ѡ��
			FS.HISFC.Models.EPR.EPRPrintPage obj = GetPrintPage(printPage);

			//���û���ҵ����Ը��µ����ݣ������һ���¼�¼
            if (obj == null)
            {
                return InsertPrintPage(printPage, img);
            }
            else
            {
                return UpdatePrintPage(printPage, img);
            }
		}
        /// <summary>
		/// �޸�һ���ϼ��޸ļ�¼
		/// </summary>
		/// <returns></returns>
		public int UpdatePrintPage(FS.HISFC.Models.EPR.EPRPrintPage printPage, byte[] img)
		{
			string strSql = "";
			if(this.Sql.GetSql("EPR.EMR.UpdatePrintPage",ref strSql)==-1) return -1;
			strSql = string.Format(strSql, printPage.ID, printPage.Page, printPage.Name, printPage.Memo, printPage.SortedControlsXml.ToString(), printPage.BeginDate.ToString("yyyy-MM-dd HH:mm:ss"), printPage.EndDate.ToString("yyyy-MM-dd HH:mm:ss"), printPage.StartRow.ToString(),FS.FrameWork.Management.Connection.Operator.ID);

			return this.InputBlob(strSql, img);
		}
        /// <summary>
        /// ɾ��һ���ϼ��޸ļ�¼
        /// </summary>
        /// <returns></returns>
        public int DeletePrintPage(FS.HISFC.Models.EPR.EPRPrintPage printPage, byte[] img)
        {
            string strSql = "";
            if (this.Sql.GetSql("EPR.EMR.DeletePrintPage", ref strSql) == -1) return -1;
            strSql = string.Format(strSql, printPage.ID, printPage.Page.ToString());

            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ����һ���ϼ��޸ļ�¼
        /// </summary>
        /// <returns></returns>
        public int InsertPrintPage(FS.HISFC.Models.EPR.EPRPrintPage printPage, byte[] img)
        {
            string strSql = "";
            if (this.Sql.GetSql("EPR.EMR.InsertPrintPage", ref strSql) == -1) return -1;
            strSql = string.Format(strSql, printPage.ID, printPage.Page, printPage.Name, printPage.Memo, "<?xml version=\"1.0\" encoding=\"GB2312\"?><Controls Version=\"1.0\"></Controls>", //printPage.SortedControlsXml.ToString(), 
                printPage.BeginDate.ToString("yyyy-MM-dd HH:mm:ss"), printPage.EndDate.ToString("yyyy-MM-dd HH:mm:ss"), printPage.StartRow.ToString(), FS.FrameWork.Management.Connection.Operator.ID);
            return this.InputBlob(strSql, img);
        }

        /// <summary>
		/// ��ô�ӡҳ
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public FS.HISFC.Models.EPR.EPRPrintPage GetPrintPage(FS.HISFC.Models.EPR.EPRPrintPage obj)
		{
			string strSql = "";
            if (this.Sql.GetSql("EPR.EMR.GetPrintPage", ref strSql) == -1) return null;
			strSql = string.Format(strSql,obj.ID, obj.Page.ToString());
            
			ArrayList al =  this.myGetPrintPage(strSql);
			if(al ==null || al.Count == 0) return null;
			return al[0] as FS.HISFC.Models.EPR.EPRPrintPage;
		}

        /// <summary>
		/// ��ô�ӡҳ
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public ArrayList GetPrintPageList(FS.HISFC.Models.EPR.EPRPrintPage obj)
		{
			string strSql = "";
            if (this.Sql.GetSql("EPR.EMR.GetPrintPageList", ref strSql) == -1) return null;
			strSql = string.Format(strSql,obj.ID);
            
			ArrayList al =  this.myGetPrintPage(strSql);
			if(al ==null || al.Count == 0) return null;
            return al;
		}

        /// <summary>
		/// ��ô�ӡҳ
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
        public byte[] GetPrintPageImage(FS.HISFC.Models.EPR.EPRPrintPage obj)
        {
            string strSql = "";

            if (this.Sql.GetSql("EPR.EMR.GetPrintPageImage", ref strSql) == -1) return null;
            strSql = string.Format(strSql, obj.ID, obj.Page.ToString());
            return this.OutputBlob(strSql);
        } 

        #region "˽��"
        private ArrayList myGetPrintPage(string sql)
        {
            if (this.ExecQuery(sql) == -1) return null;
            ArrayList al = new ArrayList();
            while (this.Reader.Read())
            {
                FS.HISFC.Models.EPR.EPRPrintPage printPage = new FS.HISFC.Models.EPR.EPRPrintPage();
                printPage.ID = this.Reader[0].ToString();
                printPage.Page = int.Parse(this.Reader[1].ToString());
                printPage.Name = this.Reader[2].ToString();
                printPage.Memo = this.Reader[3].ToString();
                FS.FrameWork.Xml.XML xml = new FS.FrameWork.Xml.XML();
                System.Xml.XmlDocument doc = new XmlDocument();
                printPage.SortedControlsXml = this.Reader[4].ToString();
                printPage.BeginDate = DateTime.Parse(this.Reader[5].ToString());
                printPage.EndDate = DateTime.Parse(this.Reader[6].ToString());
                printPage.StartRow = int.Parse(this.Reader[7].ToString());
                al.Add(printPage);
            }
            this.Reader.Close();
            return al;
        }

		#endregion

    }
}
