using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Local.Pharmacy.ShenZhen.Print
{
    #region ��ӡ�ӿڵ�ʵ��
    public class BillPrintInterfaceImplement : FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBill
    {
        #region ����
        /// <summary>
        /// ���ݽӿ�
        /// </summary>
        private FS.SOC.Local.Pharmacy.ShenZhen.Base.IPharmacyBillPrint iPrint;
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
                    if (r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.����Ȩ�޴���.ToString()].ToString() == class2Code)
                    {
                        if (r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.����Ȩ�޴���.ToString()].ToString() == class3Code)
                        {
                            try
                            {
                                pringBill.IsNeedPrint = FS.FrameWork.Function.NConvert.ToBoolean(r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.��ӡ.ToString()]);
                            }
                            catch { }

                            try
                            {
                                pringBill.IsNeedQuest = FS.FrameWork.Function.NConvert.ToBoolean(r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.��ʾ.ToString()]);
                            }
                            catch { }

                            try
                            {
                                pringBill.IsNeedPreview = FS.FrameWork.Function.NConvert.ToBoolean(r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.Ԥ��.ToString()]);
                            }
                            catch { }

                            try
                            {
                                pringBill.IsQuested = FS.FrameWork.Function.NConvert.ToBoolean(r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.�Ѿ�ѯ��.ToString()]);
                            }
                            catch { }

                            try
                            {
                                pringBill.Title = r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.��������.ToString()].ToString();
                            }
                            catch { }

                            try
                            {
                                pringBill.DLLName = r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.����.ToString()].ToString();
                            }
                            catch { }

                            try
                            {
                                pringBill.ControlName = r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.�ؼ�.ToString()].ToString();
                            }
                            catch { }

                            try
                            {
                                pringBill.RowCount = FS.FrameWork.Function.NConvert.ToInt32(r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.ÿҳ����.ToString()]);
                                ///{A513864B-B069-4604-B554-EBAD57F91A49}
                                if (pringBill.RowCount <= 0)
                                {
                                    pringBill.RowCount = 1;
                                }
                            }
                            catch { }

                            try
                            {
                                string sortTemp = r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.����ʽ.ToString()].ToString();
                                if (sortTemp == FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.SortType.��λ��.ToString())
                                {
                                    pringBill.Sort = FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.SortType.��λ��;
                                }
                                else if (sortTemp == FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.SortType.����λ��.ToString())
                                {
                                    pringBill.Sort = FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.SortType.����λ��;
                                }
                                else if (sortTemp == FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.SortType.����˳��.ToString())
                                {
                                    pringBill.Sort = FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.SortType.����˳��;
                                }
                                
                                //pringBill.Sort = (FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.SortType)FS.FrameWork.Function.NConvert.ToInt32(r[FS.SOC.Local.Pharmacy.ShenZhen.Base.PrintBill.ColSet.����ʽ.ToString()]);
                            }
                            catch(Exception ex) { }
                        }
                    }
                }
            }
            #endregion

            #region �����ݿ��ȡϵͳ����

            FS.HISFC.BizLogic.Manager.PageSize pageSizeMgr = new FS.HISFC.BizLogic.Manager.PageSize();
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
                    return FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PharmacyBillPrintDefine.xml";
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
                System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\" + printBill.DLLName);
                object myObject = assembly.CreateInstance(printBill.ControlName, true);
                this.iPrint = myObject as FS.SOC.Local.Pharmacy.ShenZhen.Base.IPharmacyBillPrint;
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
