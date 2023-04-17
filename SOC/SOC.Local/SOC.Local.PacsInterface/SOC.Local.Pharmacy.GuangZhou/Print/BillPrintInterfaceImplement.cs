using System;
using System.Collections.Generic;
using System.Text;

namespace Neusoft.SOC.Local.Pharmacy.Print
{
    #region ��ӡ�ӿڵ�ʵ��
    public class BillPrintInterfaceImplement : Neusoft.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBill
    {
        #region ����
        /// <summary>
        /// ���ݽӿ�
        /// </summary>
        private Base.IPharmacyBillPrint iPrint;
        #endregion

        #region ��ȡ�û��Զ���Ĵ�ӡ����

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="class2Code">����Ȩ�ޱ���</param>
        /// <param name="class3Code">�������ı���[�Զ��������Ȩ��]</param>
        /// <returns></returns>
        private Base.PrintBill GetPrintBill(string class2Code, string class3Code)
        {
            Base.PrintBill pringBill = new Base.PrintBill();
            
            #region ��ȡ���������ļ�
            if (System.IO.File.Exists(this.SettingFile))
            {
                System.Data.DataSet dsTmp = new System.Data.DataSet();
                dsTmp.ReadXml(this.SettingFile);
                if (dsTmp == null)
                {
                    System.Windows.Forms.MessageBox.Show("���������ļ������Ѿ���!");
                    return null;
                }
                foreach (System.Data.DataRow r in dsTmp.Tables[0].Rows)
                {
                    if (r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.����Ȩ�޴���.ToString()].ToString() == class2Code)
                    {
                        if (r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.����Ȩ�޴���.ToString()].ToString() == class3Code)
                        {
                            try
                            {
                                pringBill.IsNeedPrint = Neusoft.FrameWork.Function.NConvert.ToBoolean(r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.��ӡ.ToString()]);
                            }
                            catch { }

                            try
                            {
                                pringBill.IsNeedQuest = Neusoft.FrameWork.Function.NConvert.ToBoolean(r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.��ʾ.ToString()]);
                            }
                            catch { }

                            try
                            {
                                pringBill.IsNeedPreview = Neusoft.FrameWork.Function.NConvert.ToBoolean(r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.Ԥ��.ToString()]);
                            }
                            catch { }

                            try
                            {
                                pringBill.IsQuested = Neusoft.FrameWork.Function.NConvert.ToBoolean(r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.�Ѿ�ѯ��.ToString()]);
                            }
                            catch { }

                            try
                            {
                                pringBill.Title = r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.��������.ToString()].ToString();
                            }
                            catch { }

                            try
                            {
                                pringBill.DLLName = r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.����.ToString()].ToString();
                            }
                            catch { }

                            try
                            {
                                pringBill.ControlName = r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.�ؼ�.ToString()].ToString();
                            }
                            catch { }

                            try
                            {
                                pringBill.RowCount = Neusoft.FrameWork.Function.NConvert.ToInt32(r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.ÿҳ����.ToString()]);
                                ///{A513864B-B069-4604-B554-EBAD57F91A49}
                                if (pringBill.RowCount <= 0)
                                {
                                    pringBill.RowCount = 1;
                                }
                            }
                            catch { }

                            try
                            {
                                string sortTemp = r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.����ʽ.ToString()].ToString();
                                if (sortTemp == Neusoft.SOC.Local.Pharmacy.Base.PrintBill.SortType.��λ��.ToString())
                                {
                                    pringBill.Sort = Neusoft.SOC.Local.Pharmacy.Base.PrintBill.SortType.��λ��;
                                }
                                else if (sortTemp == Neusoft.SOC.Local.Pharmacy.Base.PrintBill.SortType.����λ��.ToString())
                                {
                                    pringBill.Sort = Neusoft.SOC.Local.Pharmacy.Base.PrintBill.SortType.����λ��;
                                }
                                else if (sortTemp == Neusoft.SOC.Local.Pharmacy.Base.PrintBill.SortType.����˳��.ToString())
                                {
                                    pringBill.Sort = Neusoft.SOC.Local.Pharmacy.Base.PrintBill.SortType.����˳��;
                                }
                                
                                //pringBill.Sort = (Neusoft.SOC.Local.Pharmacy.Base.PrintBill.SortType)Neusoft.FrameWork.Function.NConvert.ToInt32(r[Neusoft.SOC.Local.Pharmacy.Base.PrintBill.ColSet.����ʽ.ToString()]);
                            }
                            catch(Exception ex) { }
                        }
                    }
                }
            }
            #endregion

            #region �����ݿ��ȡϵͳ����

            Neusoft.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new Neusoft.HISFC.BizLogic.Manager.PageSize();
            pringBill.PageSize = pageSizeMgr.GetPageSize(class2Code + class3Code);
            if (pringBill.PageSize != null && pringBill.PageSize.Printer.ToLower() == "default")
            {
                pringBill.PageSize.Printer = "";
            }
            
            #endregion

            return pringBill;
        }

        /// <summary>
        /// ��ȡֽ�����õı����ļ�
        /// </summary>
        private string settingFile = "";

        /// <summary>
        /// ��ȡֽ�����õı����ļ�
        /// </summary>
        public string SettingFile
        {
            get
            {
                if (string.IsNullOrEmpty(this.settingFile))
                {
                    return Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PharmacyBillPrintDefine.xml";
                }
                return this.settingFile;
            }
        }

        #endregion            


        #region IPharmacyBill ��Ա

        public int PrintBill(string class2Code, string class3code, System.Collections.ArrayList alPrintData)
        {
            Base.PrintBill printBill = this.GetPrintBill(class2Code, class3code);
            if (printBill == null || !printBill.IsNeedPrint)
            {
                return -1;
            }
            if (printBill.IsNeedQuest)
            {
                System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show("�Ƿ��ӡ����", "��ʾ>>", System.Windows.Forms.MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.No)
                {
                    return -1;
                }
            }
            if (string.IsNullOrEmpty(printBill.DLLName) || string.IsNullOrEmpty(printBill.ControlName))
            {
                System.Windows.Forms.MessageBox.Show("û��ָ���ؼ���");
                return -1;
            }
            try
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(Neusoft.FrameWork.WinForms.Classes.Function.CurrentPath + "\\" + printBill.DLLName);
                object myObject = assembly.CreateInstance(printBill.ControlName, true);
                this.iPrint = myObject as Neusoft.SOC.Local.Pharmacy.Base.IPharmacyBillPrint;
                this.iPrint.SetPrintData(alPrintData, printBill);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("����" + ex.Message);
                return -1;
            }
            return 1;
        }

        #endregion

    }
    #endregion         

}
