using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Xml;

namespace FS.SOC.Local.Order.SubFeeSet.ZDLY
{
    /// <summary>
    /// 连接LIS处理
    /// </summary>
    public class LISSubManager 
    {
        private SqlConnection connection = null;

        public LISSubManager()
        {
            //connection = new SqlConnection("Data Source=192.168.252.100\\SQL2005; Initial Catalog=clab; User ID=his; Password=his;");
            GetConnect();
        }

        private int GetConnect()
        {
            try
            {
                if (System.IO.File.Exists(System.Windows.Forms.Application.StartupPath + "\\LisInterface.xml"))
                {
                    XmlDocument file = new XmlDocument();
                    file.Load(System.Windows.Forms.Application.StartupPath + "\\LisInterface.xml");

                    string dataSource = "";
                    string dbName = "";
                    string userID = "";
                    string password = "";

                    XmlNode node = file.SelectSingleNode("Config/DataSource");
                    if (node != null)
                    {
                        dataSource = node.InnerText;
                    }
                    node = file.SelectSingleNode("Config/DBName");
                    if (node != null)
                    {
                        dbName = node.InnerText;
                    }
                    node = file.SelectSingleNode("Config/UserID");
                    if (node != null)
                    {
                        userID = node.InnerText;
                    }
                    node = file.SelectSingleNode("Config/Password");
                    if (node != null)
                    {
                        password = node.InnerText;
                    }

                    string source = "Data Source=" + dataSource + "; Initial Catalog=" + dbName + "; User ID=" + userID + "; Password=" + password + ";";
                    connection = new SqlConnection(source);
                }
                else
                {
                    connection = new SqlConnection("Data Source=192.168.252.100\\SQL2005; Initial Catalog=clab_newtest; User ID=his; Password=his;");
                }
            }
            catch
            {
                connection = new SqlConnection("Data Source=192.168.252.100\\SQL2005; Initial Catalog=clab_newtest; User ID=his; Password=his;");
            }

            return 1;
        }

        /// <summary>
        /// 获取lis试管
        /// </summary>
        /// <param name="lisOrder">医嘱列表</param>
        /// <param name="MapCuvetteItems">his项目对照的试管</param>
        /// <param name="isOutPatient">是否门诊检验</param>
        /// <param name="err"></param>
        /// <returns></returns>
        public  int  GetLisSubOutPatient(ArrayList lisOrder, ref Dictionary<string,string> MapCuvetteItems, bool isOutPatient, ref string err)
        {
            #region sql
            //com_name,com_his_fee_code,sam_name,
            string sql = string.Empty;

            if (isOutPatient)
            {
                sql = " Select distinct com_his_fee_code, com_split_code,sam_name,cuv_name from  view_dict_combine_bar_his where Com_ori_id='0'  ";
            }
            else 
            {
                sql = " Select distinct com_his_fee_code, com_split_code,sam_name,cuv_name from  view_dict_combine_bar_his where Com_ori_id='1'  ";
            }

            //string lishis = " Select distinct com_his_fee_code,cuv_name from  view_dict_combine_bar_his where ";

            string where = string.Empty;
            if (lisOrder.Count > 0)
            {
                where += " and ";
                where += " ( ";
                for (int i = 0; i < lisOrder.Count; i++)
                {
                    FS.HISFC.Models.Order.Order order = lisOrder[i] as FS.HISFC.Models.Order.Order;
                    if (i == lisOrder.Count - 1)
                    {
                        where += "  com_his_fee_code='" + order.Item.ID + "'  ";

                    }
                    else
                    {
                        where += "  com_his_fee_code='" + order.Item.ID + "'  or";
                    }

                }
                where += "  ) ";
            }
          
            #endregion

            List<FS.FrameWork.Models.NeuObject> hisLisCompare = new List<FS.FrameWork.Models.NeuObject>();

            #region 取lis数据
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                //获取his编码与试管对照(对于细菌培养项目可能一个his编码对应不同的试管) 
                DataTable lis = new DataTable();
                SqlCommand sqlCommand = new SqlCommand(sql + where, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                adapter.Fill(lis);
           
                if (lis != null && lis.Rows.Count > 0)
                {
                    foreach (DataRow row in lis.Rows)
                    {
                        FS.FrameWork.Models.NeuObject item = new FS.FrameWork.Models.NeuObject();

                        item.ID = row["com_his_fee_code"].ToString();
                        item.Name = row["sam_name"].ToString();
                        item.Memo = row["cuv_name"].ToString();
                        item.User01 = row["com_split_code"].ToString();
                        hisLisCompare.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                //一定要注意！！别的系统出现错误，不要影响HIS业务进行
                //err = "连接lis数据出现错误！" + e.Message;
                //return -1;

                Classes.Function.ShowBalloonTip(3, "错误提示", "连接lis数据出现错误！\r\n请联系信息科！\r\n\r\n" + e.Message, System.Windows.Forms.ToolTipIcon.Warning);
            }
            finally 
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            #endregion

            #region 取管

            //lis合并代码
            Hashtable lisCode = new Hashtable();

            List<FS.FrameWork.Models.NeuObject> lisSub=new List<FS.FrameWork.Models.NeuObject>();
            foreach (FS.HISFC.Models.Order.Order order in lisOrder)
            {
                lisSub = hisLisCompare.Where(x => x.ID == order.Item.ID).ToList<FS.FrameWork.Models.NeuObject>();

                if (lisSub.Count > 1)
                {
                    lisSub = hisLisCompare.Where(x => (x.ID == order.Item.ID && x.Name == order.Sample.Name)).ToList<FS.FrameWork.Models.NeuObject>();

                    if (lisSub.Count == 0)
                    {
                        lisSub = hisLisCompare.Where(x => x.ID == order.Item.ID).ToList<FS.FrameWork.Models.NeuObject>();
                    }
                }

                if (lisSub.Count >0)
                {
                    order.ApplyNo = lisSub[0].User01;

                    if (!lisCode.ContainsKey(lisSub[0].User01))
                    {
                        lisCode.Add(lisSub[0].User01, lisSub[0].User01);

                        if (!MapCuvetteItems.ContainsKey(lisSub[0].ID)) 
                        {
                            MapCuvetteItems.Add(lisSub[0].ID, lisSub[0].Memo);
                        }
                    }
                }
            }

            #endregion

            return 1;
        }

    }
}
