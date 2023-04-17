using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Xml;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Material
{
    public class Function
    {
        public Function()
        {

        }

        #region ���ݴ�ӡ�ӿ�

        /// <summary>
        /// ���ݴ�ӡ�ӿ�
        /// </summary>
        public static FS.HISFC.BizProcess.Interface.Material.IBillPrint IPrint = null;

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
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("ƴ���� like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("�����"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("����� like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("�Զ�����"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("�Զ����� like '{0}'", queryCode), "or");
            if (dv.Table.Columns.Contains("��Ʒ����"))
                filterStr = Function.ConnectFilterStr(filterStr, string.Format("��Ʒ���� like '{0}'", queryCode), "or");
            return filterStr;
        }

        /// <summary>
        /// ���ع����ַ���
        /// </summary>
        /// <param name="dv">����˵�DataView</param>
        /// <param name="queryCode">���������ַ���</param>
        /// <param name="filterIndex">����˵�������</param>
        /// <returns>�ɹ����ع����ַ���</returns>
        public static string GetFilterStr(DataView dv, string queryCode, params int[] filterIndex)
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
        public static string ConnectFilterStr(string filterStr, string newFilterStr, string logicExpression)
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
        /// <param name="NeuObject">NeuObject����</param>
        /// <returns>1ѡ�������ݣ�0û��ѡ��</returns>
        public static int ChooseItem(ArrayList arrayList, string[] farPointLabel, float[] farPointWidth, bool[] farPointVisible, ref FS.FrameWork.Models.NeuObject NeuObject)
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
            //    NeuObject = form.Object;
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
        /// <returns>�ɹ�����ģ����Ϣ  ʧ�ܷ���null</returns>
        /*
        internal static ArrayList ChooseDrugStencil(string privDept, FS.HISFC.Models.Pharmacy.EnumDrugStencil stencilType)
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
        */
        /// <summary>
        /// ��ȡ��ʾ��Ϣ
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        public static string GetNote(string itemCode)
        {
            FS.HISFC.BizLogic.Material.MetItem myItem = new FS.HISFC.BizLogic.Material.MetItem();

            FS.HISFC.BizLogic.Material.ComCompany myCom = new FS.HISFC.BizLogic.Material.ComCompany();

            FS.HISFC.Models.Material.MaterialItem item = myItem.GetMetItemByMetID(itemCode);

            FS.HISFC.Models.Material.MaterialCompany company = myCom.QueryCompanyByCompanyID(item.Company.ID,"A","A");

            DateTime dtNow = myItem.GetDateTimeFromSysDateTime();

            string note = "";

            if (company != null)
            {
                if (company.BusinessDate < dtNow)
                {
                    note += "Ӫҵִ���Ѿ�����!" + "\n";
                }
                if (company.DutyDate < dtNow)
                {
                    note += "˰��Ǽ�֤�Ѿ�����!" + "\n";
                }
                if (company.OrgDate < dtNow)
                {
                    note += "��֯��������֤�Ѿ�����!" + "\n";
                }
            }

            return note;
        }

        internal static FarPoint.Win.Spread.CellType.ICellType GetReadOnlyCellType()
        {
            FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
            t.ReadOnly = true;

            return t;
        }

    }
    
}
