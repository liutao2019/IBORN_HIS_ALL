using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using FS.FrameWork.Management;
using System.Xml;
using System.Collections;
using System.Windows.Forms;

namespace FS.HISFC.Components.Pharmacy
{
    public class Function
    {
        public Function ()
        {

        }

        /// <summary>
        /// �������
        /// </summary>
        internal const string DrugTypePriv_ConsType = "TypePriv";

        #region ���ݴ�ӡ�ӿ�

        /// <summary>
        /// ���ݴ�ӡ�ӿ�
        /// </summary>
        public static FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint IPrint = null;

        #endregion

        /// <summary>
        /// ����Xml�ļ����� ���� DataTable
        /// </summary>
        /// <param name="xmlFilePath">Xml�����ļ�·��</param>
        /// <returns>�ɹ�����DataTable �������󷵻�Null</returns>
        public static DataTable GetDataTableFromXml(string xmlFilePath)
        {
            DataTable dt = new DataTable();
            if (System.IO.File.Exists(xmlFilePath))
            {
                XmlDocument doc = new XmlDocument();
                try
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(xmlFilePath, System.Text.Encoding.Default);
                    string streamXml = sr.ReadToEnd();
                    sr.Close();
                    doc.LoadXml(streamXml);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(Language.Msg("��ȡXml�����ļ��������� ���������ļ��Ƿ���ȷ") + ex.Message);
                    return null;
                }

                try
                {

                    XmlNodeList nodes = doc.SelectNodes("//Column");

                    string tempString = "";

                    foreach (XmlNode node in nodes)
                    {
                         switch (node.Attributes["type"].Value)
                        {
                            case "TextCellType":
                            case "ComboBoxCellType":
                                tempString = "System.String";
                                break;
                            case "CheckBoxCellType":
                                tempString = "System.Boolean";
                                break;
                            case "DateTimeCellType":
                                tempString = "System.DateTime";
                                break;
                             case "NumberCellType":
                                 tempString = "System.Decimal";
                                 break;
                        }

                        dt.Columns.Add(new DataColumn(node.Attributes["displayname"].Value,
                            System.Type.GetType(tempString)));
                    }
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show(Language.Msg("Xml�ļ���ʽ����ȷ"));
                    return null;
                }
            }

            return dt;
        }

        /// <summary>
        /// ����ҩƷ���Ƶ�Ĭ�Ϲ����ֶ� ���ع����ַ���
        /// </summary>
        /// <param name="dv">����˵�DataView</param>
        /// <param name="queryCode">���������ַ���</param>
        /// <returns>�ɹ����ع����ַ��� ʧ�ܷ���null</returns>
        public static string GetFilterStr(DataView dv, string queryCode)
        {
            string filterStr = "";
            if (dv.Table.Columns.Contains("ƴ����"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("ƴ���� like '{0}'", queryCode),"or");
            if (dv.Table.Columns.Contains("�����"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("����� like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("�Զ�����"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("�Զ����� like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("��Ʒ����"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("��Ʒ���� like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("ͨ����"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("ͨ���� like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("ͨ����ƴ����"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("ͨ����ƴ���� like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("ͨ���������"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("ͨ��������� like '{0}'", queryCode) ,"or");
            if (dv.Table.Columns.Contains("ͨ�����Զ�����"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("ͨ�����Զ����� like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("Ӣ����Ʒ��"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("Ӣ����Ʒ�� like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("Ӣ��ͨ����"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("Ӣ��ͨ���� like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("Ӣ�ı���"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("Ӣ�ı��� like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("��������"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("�������� like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("ѧ��"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("ѧ�� like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("ѧ��ƴ����"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("ѧ��ƴ���� like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("����"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("���� like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("����ƴ����"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("����ƴ���� like '{0}'", queryCode), "or");
            return filterStr;
        }

        /// <summary>
        /// ���ع����ַ���
        /// </summary>
        /// <param name="dv">����˵�DataView</param>
        /// <param name="queryCode">���������ַ���</param>
        /// <param name="filterIndex">����˵�������</param>
        /// <returns>�ɹ����ع����ַ���</returns>
        public static string GetFilterStr(DataView dv, string queryCode,params int[] filterIndex)
        {
            string filterStr = "";

            for (int i = 0; i < filterIndex.Length; i++)
            {
                filterStr = Function.ConnectFilterStr(filterStr, string.Format(dv.Table.Columns[filterIndex[i]] + " like '{0}'", queryCode), "or");
            }

            return filterStr;
        }

        /// <summary>
        /// ���ӹ����ַ���
        /// </summary>
        /// <param name="filterStr">ԭʼ�����ַ���</param>
        /// <param name="newFilterStr">�����ӵĹ�������</param>
        /// <param name="logicExpression">�߼������</param>
        /// <returns>�ɹ��������Ӻ�Ĺ����ַ���</returns>
        public static string ConnectFilterStr(string filterStr, string newFilterStr,string logicExpression )
        {
            string connectStr = "";
            if (filterStr == "")
                connectStr = newFilterStr;
            else
                connectStr = filterStr + " " + logicExpression + " " + newFilterStr;

            return connectStr;
        }

        /// <summary>
        /// ���ٲ�ѯ����
        /// </summary>
        /// <param name="arrayList">��������</param>
        /// <param name="farPointLabel">FarPoint��������ʾ ���Ϊ6��</param>
        /// <param name="farPointWidth">FarPoint�ڸ��п�� �������Ϊ6</param>
        /// <param name="farPointVisible">FarPoint�ڸ����Ƿ����� �������Ϊ6</param>
        /// <param name="neuObject">neuObject����</param>
        /// <returns>1ѡ�������ݣ�0û��ѡ��</returns>
        public static int ChooseItem(ArrayList arrayList, string[] farPointLabel, float[] farPointWidth, bool[] farPointVisible, ref FS.FrameWork.Models.NeuObject neuObject)
        {
            //frmUserDefineEasyChoose form = new frmUserDefineEasyChoose(arrayList);
            //form.FarPointLabel = farPointLabel;
            //form.FarPointWidth = farPointWidth;
            //form.FarPointVisible = farPointVisible;

            ////���ò�ѯ����
            //System.Windows.Forms.DialogResult Result = form.ShowDialog();
            ////ȡ���ڷ��ص���ʼ���ں���ֹ����
            //if (Result == DialogResult.OK)
            //{
            //    neuObject = form.Object;
            //    //ȡ�������ݣ��򷵻�1
            //    return 1;
            //}

            ////���û��ѡ�����ݣ��򷵻�0
            return 0;
        }

        /// <summary>
        /// ��ʾ��ʾ��Ϣ
        /// </summary>
        /// <param name="errStr">��ʾ��Ϣ</param>
        public static void ShowMsg(string strMsg)
        {
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            System.Windows.Forms.MessageBox.Show(Language.Msg(strMsg));
        }

        /// <summary>
        /// ģ��ѡ��
        /// </summary>
        /// <param name="privDept">Ȩ�޿���</param>
        /// <param name="openType">ģ������</param>
        /// <remarks>{037D86BC-5E18-41dd-8D34-16D89C426B88}�����÷��������ڴ��뱾�ػ�ʱ���Ե���</remarks>
        /// <returns>�ɹ�����ģ����Ϣ  ʧ�ܷ���null</returns>
        public static ArrayList ChooseDrugStencil(string privDept, FS.HISFC.Models.Pharmacy.EnumDrugStencil stencilType)
        {
            FS.HISFC.BizLogic.Pharmacy.Constant consManager = new FS.HISFC.BizLogic.Pharmacy.Constant();

            ArrayList alList = consManager.QueryDrugStencilList(privDept, stencilType);
            if (alList == null)
            {
                System.Windows.Forms.MessageBox.Show(Language.Msg("��ȡ������ģ�淢������" + consManager.Err));
                return null;
            }
            if (alList.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show(Language.Msg("�޸�����ģ������"));
                return null;
            }

            ArrayList alSelect = new ArrayList();
            FS.FrameWork.Models.NeuObject selectObj = new FS.FrameWork.Models.NeuObject();
            foreach (FS.HISFC.Models.Pharmacy.DrugStencil temp in alList)
            {
                selectObj = new FS.FrameWork.Models.NeuObject();
                selectObj.ID = temp.Stencil.ID;
                selectObj.Name = temp.Stencil.Name;
                selectObj.Memo = temp.OpenType.Name;

                alSelect.Add(selectObj);
            }

            string[] label = { "ģ�����", "ģ������", "ģ������" };
            float[] width = { 60F, 100F, 120F };
            bool[] visible = { true, true, true, false, false, false };
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(alSelect, ref selectObj) == 0)
            {
                return new ArrayList();
            }
            else
            {
                ArrayList alOpenDetail = new ArrayList();

                alOpenDetail = consManager.QueryDrugStencil(selectObj.ID);
                if (alOpenDetail == null)
                {
                    System.Windows.Forms.MessageBox.Show(Language.Msg(consManager.Err));
                    return null;
                }

                return alOpenDetail;
            }
        }

        /// <summary>
        ///  �۸��жϴ���
        /// </summary>
        public static int SetPrice(string deptCode, string drugCode, ref FS.HISFC.Models.Pharmacy.Item item)
        {
            //ȡҩƷ�ֵ���Ϣ
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
            FS.HISFC.Models.Pharmacy.Storage storage = itemManager.GetStockInfoByDrugCode(deptCode, drugCode);
            if (storage == null)
            {
                MessageBox.Show(Language.Msg("��ȡ���ҿ�������Ϣ��������") + itemManager.Err);
                return -1;
            }
            if (storage.Item.ID != "")
            {
                if (item.PriceCollection.RetailPrice != storage.Item.PriceCollection.RetailPrice)
                {
                    MessageBox.Show(Language.Msg("��ע�⣡" + item.Name + " ��ҩƷ�ѽ��й����Ƶ��ۡ�"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                item.PriceCollection.RetailPrice = storage.Item.PriceCollection.RetailPrice;
            }
            
            return 1;
        }

        /// <summary>
        /// �ж����ۼ۵�һ����
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="item">ҩƷ��Ŀ��Ϣ</param>
        /// <returns></returns>
        public static bool JudgePriceConsinstency(string deptCode, FS.HISFC.Models.Pharmacy.Item item)
        {
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
            FS.HISFC.Models.Pharmacy.Storage sourceStorage = itemManager.GetStockInfoByDrugCode(deptCode, item.ID);
            if (sourceStorage == null)
            {
                MessageBox.Show(Language.Msg("��ȡԴ���ҿ�������Ϣ��������") + itemManager.Err);
                return false;
            }

            if (sourceStorage.Item.ID != "")
            {
                if (sourceStorage.Item.PriceCollection.RetailPrice != item.PriceCollection.RetailPrice)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// ��ȡԶ�������ļ�
        /// </summary>
        /// <returns>�ɹ�����Զ�������ļ���Ϣ ʧ�ܷ���null</returns>
        public System.Xml.XmlDocument GetConfig()
        {
            #region ��ȡ�����ļ�·��

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(Application.StartupPath + "\\url.xml");

            System.Xml.XmlNode node = doc.SelectSingleNode("//dir");
            if (node == null)
            {
                MessageBox.Show(Language.Msg("url����dir������"));
            }

            string serverPath = node.InnerText;
            string configPath = "//Config.xml"; //Զ�������ļ��� 

            #endregion

            try
            {
                doc.Load(serverPath + configPath);
            }
            catch (System.Net.WebException)
            {

            }
            catch (System.IO.FileNotFoundException)
            { 

            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.Msg("װ��Config.xmlʧ�ܣ�\n" + ex.Message));
            }

            return doc;
        }

        /// <summary>
        /// �Ƿ����ų���
        /// </summary>
        public static bool IsOutByBatchNO
        {
            get
            {
                //{DE934736-B2C2-44a4-A218-2DC38E1620BA}
               // return false;
                FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrmIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

                return ctrmIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Out_Choose_BatchNO, false, false);
            }
        }
    }
}
