using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.WinForms.Report.DrugStore
{
    public partial class ucCompoundList : UserControl
    {
        public ucCompoundList()
        {
            InitializeComponent();

            this.Init();
        }

        private static FS.FrameWork.Public.ObjectHelper usageHelper = new FS.FrameWork.Public.ObjectHelper();

        private static FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 出生日期
        /// </summary>
        private static System.Collections.Hashtable hsBirth = new Hashtable();

        private void Init()
        {
            FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList alUsage = consManager.GetList(FS.HISFC.Models.Base.EnumConstant.USAGE);

            usageHelper = new FS.FrameWork.Public.ObjectHelper(alUsage);

            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList alDept = deptManager.GetDeptmentAll();

            deptHelper = new FS.FrameWork.Public.ObjectHelper(alDept);
        }

        public int ShowData(ArrayList alData,bool isPreview)
        {
            #region 按患者进行单独分组

            List<FS.HISFC.Models.Pharmacy.ApplyOut> detailApplyOut = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
            System.Collections.Hashtable hsPatientApply = new Hashtable();

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut temp in alData)
            {
                if (hsPatientApply.ContainsKey(temp.PatientNO + temp.UseTime.ToString()))
                {
                    (hsPatientApply[temp.PatientNO + temp.UseTime.ToString()] as List<FS.HISFC.Models.Pharmacy.ApplyOut>).Add(temp.Clone());
                }
                else
                {
                    List<FS.HISFC.Models.Pharmacy.ApplyOut> tempList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
                    tempList.Add(temp.Clone());

                    hsPatientApply.Add(temp.PatientNO + temp.UseTime.ToString(), tempList);
                }
            }

            #endregion

            #region 按批次流水对患者药品进行排序 完成打印

            CompareApplyOutByCompoundGroup compare = new CompareApplyOutByCompoundGroup();
            foreach (List<FS.HISFC.Models.Pharmacy.ApplyOut> list in hsPatientApply.Values)
            {
                if (list.Count <= 0)
                {
                    continue;
                }

                list.Sort(compare);

                //患者头信息赋值
                string information = "";
                this.neuSpread1_Sheet1.Rows.Count = 0;                
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in list)
                {
                    int iRowIndex = this.neuSpread1_Sheet1.Rows.Count;
                    this.neuSpread1_Sheet1.Rows.Add(iRowIndex, 1);
                    if (info.CompoundGroup != string.Empty)
                    {
                        this.neuSpread1_Sheet1.Cells[iRowIndex, 0].Text = info.CompoundGroup.Substring(0, 1);
                    }
                    this.neuSpread1_Sheet1.Cells[iRowIndex, 1].Text = info.CompoundGroup;
                    this.neuSpread1_Sheet1.Cells[iRowIndex, 2].Text = info.Item.Name + "－" + Function.DrugDosage.GetStaticDosage(info.Item.ID) + "[" + info.Item.Specs + "]";
                    this.neuSpread1_Sheet1.Cells[iRowIndex, 3].Text = info.Operation.ApplyQty.ToString();      //总量
                    this.neuSpread1_Sheet1.Cells[iRowIndex, 4].Text = info.DoseOnce.ToString() + info.Item.DoseUnit;       //用量
                    this.neuSpread1_Sheet1.Cells[iRowIndex, 5].Text = usageHelper.GetName(info.Usage.ID);

                    string birth = "";
                    if (hsBirth.ContainsKey(info.PatientNO))
                    {
                        birth = hsBirth[info.PatientNO] as string;
                    }
                    else
                    {
                        FS.HISFC.BizLogic.RADT.InPatient patientManager = new FS.HISFC.BizLogic.RADT.InPatient();
                        FS.HISFC.Models.RADT.PatientInfo patient = patientManager.QueryPatientInfoByInpatientNO(info.PatientNO);
                        birth = patient.Birthday.ToString("yyyy.MM.dd");
                        hsBirth.Add(info.PatientNO, birth);
                    }

                    if (iRowIndex == 0)
                    {
                        if (info.User01.Length <= 4)
                        {
                            information = string.Format("{0}    床号：{1}   {2}   {3}    出生日期：{4}   用药时间：{5}", deptHelper.GetName(info.ApplyDept.ID),
                                                            info.User01, info.User02, info.User02.Substring(2), birth, info.UseTime.ToString("yyyy.MM.dd"));
                        }
                        else
                        {
                            information = string.Format("{0}    床号：{1}   {2}   {3}    出生日期：{4}   用药时间：{5}", deptHelper.GetName(info.ApplyDept.ID),
                                                                                      info.User01.Substring(4), info.User02, info.PatientNO.Substring(4), birth, info.UseTime.ToString("yyyy.MM.dd"));
                        }
                    }
                }

                this.lbInfo.Text = information;

                this.Print(isPreview);
            }

            #endregion

            return 1;
        }

        public void Print(bool isPreview)
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.IsResetPage = true;
            System.Drawing.Printing.PaperSize pageSize = this.getPaperSizeForInput();
            p.SetPageSize(pageSize);
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            if (isPreview)
            {
                p.PrintPreview(15, 10, this);
            }
            else
            {
                p.PrintPage(15, 10, this);
            }
            //p.PrintPreview(15, 10, this.neuPanel1);
        }

        public void Clear()
        {
            this.lbInfo.Text = "";

            this.neuSpread1_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// 入库单的纸张高度设置
        /// 默认情况下是三行入库数据的高度
        /// </summary>
        private System.Drawing.Printing.PaperSize getPaperSizeForInput()
        {
            System.Drawing.Printing.PaperSize paperSize = new System.Drawing.Printing.PaperSize();
            paperSize.PaperName = "cli" + System.DateTime.Now.ToString();
            try
            {
                int width = 820;
                //int width = this.Width;
                int curHeight = this.Height;
                int addHeight = (this.neuSpread1_Sheet1.RowCount - 1) * (int)this.neuSpread1_Sheet1.Rows[0].Height;

                paperSize.Width = width;
                paperSize.Height = (addHeight + curHeight);
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置入库打印纸张出错>>" + ex.Message);
            }
            return paperSize;
        }


        public class CompareApplyOutByCompoundGroup : IComparer<FS.HISFC.Models.Pharmacy.ApplyOut>
        {
            public int Compare(FS.HISFC.Models.Pharmacy.ApplyOut o1, FS.HISFC.Models.Pharmacy.ApplyOut o2)
            {             
                string oX = o1.PatientNO;          //患者姓名
                string oY = o2.PatientNO;          //患者姓名

                int nComp;

                if (oX == null)
                {
                    nComp = (oY != null) ? -1 : 0;
                }
                else if (oY == null)
                {
                    nComp = 1;
                }
                else
                {
                    nComp = string.Compare(oX.ToString(), oY.ToString());
                }

                return nComp;
            }

        }
    }
}
