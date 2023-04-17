using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

//[assembly: CLSCompliant(true)]
namespace FS.HISFC.BizLogic.HL7
{
    /// <summary>
    /// [��������: LIS�������]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-05-10]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class LisManagment : FS.FrameWork.Management.Database
    {
        public string GetResult(string id)
        {
            string sql = string.Format(CultureInfo.InvariantCulture,"select result from hl7_lis_result where id='{0}'", id);
            string ret = string.Empty;

            this.ExecQuery(sql);

            if (this.Reader == null)
                throw new DatabaseException(string.Format(CultureInfo.InvariantCulture, "{0} ִ�д���δ�ܷ��ؼ�¼��", sql));

            if(this.Reader.Read())
            {
                ret = this.Reader[0].ToString();
            }
            this.Reader.Dispose();

            return ret;
        }

        public bool IsResultExist(string id)
        {
            string sql = string.Format(CultureInfo.InvariantCulture, "select * from hl7_lis_result where id='{0}'", id);
            bool ret = false;

            this.ExecQuery(sql);

            if (this.Reader == null)
                throw new DatabaseException(string.Format(CultureInfo.InvariantCulture, "{0} ִ�д���δ�ܷ��ؼ�¼��", sql));

            if (this.Reader.Read())
            {
                ret = true;
            }
            this.Reader.Dispose();

            return ret;
        }

        public int InsertOrder(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            string tt=message.Replace("&","'||'&'||'");
            string sql = string.Format(CultureInfo.InvariantCulture, "insert into HL7_MESSAGE (ID,STATUS,DIRECTION,MESSAGE) values(seq_hl7_message.nextval,'W','O','{0}')", tt);

            return this.ExecNoQuery(sql);
             
            
        }
    }
}
