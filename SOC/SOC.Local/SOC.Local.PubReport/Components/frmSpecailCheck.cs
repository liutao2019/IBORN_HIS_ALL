using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.PubReport.Components
{
    public partial class frmSpecailCheck : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmSpecailCheck()
        {
            InitializeComponent();
            InitControl();
        }

        SOC.Local.PubReport.BizLogic.PubReport pubMgr = new SOC.Local.PubReport.BizLogic.PubReport();

        DateTime staticMonth = new DateTime();

        public DateTime StaticMonth
        {
            get
            {
                return staticMonth;
            }
            set
            {
                staticMonth = value;
            }
        }

        private void InitControl()
        {
            FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList alCheck = conMgr.GetList("SpecialCheck");
            this.cmbItemName.AddItems(alCheck);

            ArrayList alCheckResult = conMgr.GetList("CheckResult");
            this.cmbCheckResult.AddItems(alCheckResult);

            ArrayList alDiagnoze = conMgr.GetList("Diagnose");
            this.cmbDiagnoze.AddItems(alDiagnoze);

            InitFp();
        }

        private void InitFp()
        {
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkCT = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCT = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCT = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.DateTimeCellType dtCT = new FarPoint.Win.Spread.CellType.DateTimeCellType();


            this.fpSpread1_Sheet1.Columns[(int)Col.单位名称].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.姓名].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.医疗证号].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.住院号].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.检查治疗日期].CellType = dtCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.检查及治疗项目及部位].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.临床诊断].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.检查诊断].CellType = textCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.检查相机费].CellType = numberCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.CT螺旋费].CellType = numberCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.高分辨].CellType = numberCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.片费].CellType = numberCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.注射器费].CellType = numberCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.显影剂费].CellType = numberCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.合计].CellType = numberCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.自付比例].CellType = numberCT;
            this.fpSpread1_Sheet1.Columns[(int)Col.实报金额].CellType = numberCT;

            this.fpSpread1_Sheet1.Columns[(int)Col.单位名称].Label = Col.单位名称.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.姓名].Label  = Col.姓名.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.医疗证号].Label= Col.医疗证号.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.住院号].Label = Col.住院号.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.检查治疗日期].Label = Col.检查治疗日期.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.检查及治疗项目及部位].Label = Col.检查及治疗项目及部位.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.临床诊断].Label = Col.临床诊断.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.检查诊断].Label = Col.检查诊断.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.检查相机费].Label = Col.检查相机费.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.CT螺旋费].Label = Col.CT螺旋费.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.高分辨].Label = Col.高分辨.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.片费].Label = Col.片费.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.注射器费].Label = Col.注射器费.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.显影剂费].Label = Col.显影剂费.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.合计].Label = Col.合计.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.自付比例].Label = Col.自付比例.ToString();
            this.fpSpread1_Sheet1.Columns[(int)Col.实报金额].Label = Col.实报金额.ToString();
            this.fpSpread1_Sheet1.Columns.Count = (int)Col.实报金额 + 1;

            this.fpSpread1_Sheet1.Columns[(int)Col.单位名称].Width = 120;
            this.fpSpread1_Sheet1.Columns[(int)Col.姓名].Width = 45;
            this.fpSpread1_Sheet1.Columns[(int)Col.医疗证号].Width = 60;
            this.fpSpread1_Sheet1.Columns[(int)Col.住院号].Width = 60;
            this.fpSpread1_Sheet1.Columns[(int)Col.检查治疗日期].Width = 75;
            this.fpSpread1_Sheet1.Columns[(int)Col.检查及治疗项目及部位].Width = 120;
            this.fpSpread1_Sheet1.Columns[(int)Col.临床诊断].Width = 120;
            this.fpSpread1_Sheet1.Columns[(int)Col.检查诊断].Width = 120;

            this.fpSpread1_Sheet1.Rows.Count = 0;

        }

        FS.HISFC.Models.RADT.Patient patient = new FS.HISFC.Models.RADT.Patient();
        SOC.Local.PubReport.Models.PubReport pubObj = new SOC.Local.PubReport.Models.PubReport();
        SOC.Local.PubReport.Models.SpecialCheck specialCheck = new SOC.Local.PubReport.Models.SpecialCheck();

        /// <summary>
        /// 设置记账单信息
        /// </summary>
        /// <returns>1设置成功 -1设置失败</returns>
        public int SetPubObj(SOC.Local.PubReport.Models.PubReport pubObj)
        {
            this.pubObj = pubObj.Clone();

            this.WritePubObjToPanel(pubObj);
            this.AlDelete = new ArrayList();
            QuerySpecialCheck(pubObj.InpatientNo,pubObj.ID);
            return 1;

        }

        void WritePubObjToPanel(SOC.Local.PubReport.Models.PubReport pubObj)
        {
            IsEditMode = false;
            this.txtWorkPlace.Text = pubObj.WorkName;
            this.txtName.Text = pubObj.Name;
            this.txtMcardNo.Text = pubObj.MCardNo;
            this.cmbDiagnoze.Text = "";
            this.txtPatientNo.Text = pubObj.PatientNO;
            this.patient.ID = pubObj.InpatientNo;
            IsEditMode = true;
        }

        private SOC.Local.PubReport.Models.SpecialCheck ReadFromFp(int i)
        {
            SOC.Local.PubReport.Models.SpecialCheck obj = this.fpSpread1_Sheet1.Rows[i].Tag as SOC.Local.PubReport.Models.SpecialCheck;
            if (obj == null)
            {
                obj = new SOC.Local.PubReport.Models.SpecialCheck();
            }
            try
            {
            obj.WorkPlace = this.fpSpread1_Sheet1.Cells[i,(int)Col.单位名称].Text;
            obj.Name = this.fpSpread1_Sheet1.Cells[i, (int)Col.姓名].Text;
            obj.McardNo = this.fpSpread1_Sheet1.Cells[i, (int)Col.医疗证号].Text;
            obj.PatientNo = this.fpSpread1_Sheet1.Cells[i, (int)Col.住院号].Text;
            obj.CheckDate = (DateTime)this.fpSpread1_Sheet1.Cells[i, (int)Col.检查治疗日期].Value;
            obj.ItemName = this.fpSpread1_Sheet1.Cells[i, (int)Col.检查及治疗项目及部位].Text;
            obj.Diagnoze = this.fpSpread1_Sheet1.Cells[i, (int)Col.临床诊断].Text;
            obj.CheckResult = this.fpSpread1_Sheet1.Cells[i, (int)Col.检查诊断].Text;
            obj.Xiangji = (Decimal)this.fpSpread1_Sheet1.Cells[i, (int)Col.检查相机费].Value;
            obj.CT = (Decimal)this.fpSpread1_Sheet1.Cells[i, (int)Col.CT螺旋费].Value;
            obj.Gaofen = (Decimal)this.fpSpread1_Sheet1.Cells[i, (int)Col.高分辨].Value;
            obj.Pianfei = (Decimal)this.fpSpread1_Sheet1.Cells[i, (int)Col.片费].Value;
            obj.Zhushi = (Decimal)this.fpSpread1_Sheet1.Cells[i, (int)Col.注射器费].Value;
            obj.Xianyin = (Decimal)this.fpSpread1_Sheet1.Cells[i, (int)Col.显影剂费].Value;
            obj.TotCost = (Decimal)this.fpSpread1_Sheet1.Cells[i, (int)Col.合计].Value;
            obj.PayRate = (Decimal)this.fpSpread1_Sheet1.Cells[i, (int)Col.自付比例].Value;
            obj.PubCost = (Decimal)this.fpSpread1_Sheet1.Cells[i, (int)Col.实报金额].Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取特殊检查信息失败" + ex.Message);
                return null;
            }
            return obj;
        }

        private void WriteToFp(int i, SOC.Local.PubReport.Models.SpecialCheck obj)
        {
            if (this.fpSpread1_Sheet1.Rows.Count <= i)
            {
                this.fpSpread1_Sheet1.Rows.Add(i, 1);
            }
            try
            {
                this.fpSpread1_Sheet1.Cells[i, (int)Col.单位名称].Text = obj.WorkPlace;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.姓名].Text = obj.Name;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.医疗证号].Text = obj.McardNo;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.住院号].Text = obj.PatientNo;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.检查治疗日期].Value = obj.CheckDate;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.检查及治疗项目及部位].Text = obj.ItemName;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.临床诊断].Text = obj.Diagnoze;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.检查诊断].Text = obj.CheckResult;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.检查相机费].Value = obj.Xiangji;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.CT螺旋费].Value = obj.CT;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.高分辨].Value = obj.Gaofen;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.片费].Value = obj.Pianfei;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.注射器费].Value = obj.Zhushi;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.显影剂费].Value = obj.Xianyin;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.合计].Value = obj.TotCost;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.自付比例].Value = obj.PayRate;
                this.fpSpread1_Sheet1.Cells[i, (int)Col.实报金额].Value = obj.PubCost;
                this.fpSpread1_Sheet1.Rows[i].Tag = obj;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private SOC.Local.PubReport.Models.SpecialCheck ReadFromPanel()
        {
            SOC.Local.PubReport.Models.SpecialCheck obj = specialCheck.Clone();
            try
            {
                obj.InpatientNo = patient.ID;
                obj.WorkPlace = this.txtWorkPlace.Text;
                obj.Name = this.txtName.Text;
                obj.McardNo = this.txtMcardNo.Text;
                obj.PatientNo = this.txtPatientNo.Text;
                obj.CheckDate = this.dtCheckDate.Value;
                obj.ItemName = this.cmbItemName.Text;
                obj.Diagnoze = this.cmbDiagnoze.Text;
                obj.CheckResult = this.cmbCheckResult.Text;
                obj.Xiangji =  FS.FrameWork.Function.NConvert.ToDecimal(this.txtXiangji.Text);
                obj.CT = FS.FrameWork.Function.NConvert.ToDecimal(this.txtCTLX.Text);
                obj.Gaofen = FS.FrameWork.Function.NConvert.ToDecimal(this.txtGaoFen.Text);
                obj.Pianfei = FS.FrameWork.Function.NConvert.ToDecimal(this.txtPianFei.Text);
                obj.Zhushi = FS.FrameWork.Function.NConvert.ToDecimal(this.txtZhushe.Text);
                obj.Xianyin = FS.FrameWork.Function.NConvert.ToDecimal(this.txtXianyin.Text);
                obj.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.txtTotCost.Text);
                obj.PayRate = FS.FrameWork.Function.NConvert.ToDecimal(this.txtPayRate.Text);
                obj.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(this.txtPubCost.Text);
                obj.StaticMonth = this.StaticMonth;
                obj.InvoiceNo = this.pubObj.ID;
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取特殊检查信息失败" + ex.Message);
                return null;
            }
            return obj;
        }

        private void WriteToPanel(SOC.Local.PubReport.Models.SpecialCheck obj)
        {
            try
            {
                IsEditMode = false;
                this.txtWorkPlace.Text = obj.WorkPlace;
                this.txtName.Text = obj.Name;
                this.txtMcardNo.Text = obj.McardNo;
                this.txtPatientNo.Text = obj.PatientNo;
                this.dtCheckDate.Value = obj.CheckDate;
                this.cmbItemName.Text = obj.ItemName;
                this.cmbDiagnoze.Text = obj.Diagnoze;
                this.cmbCheckResult.Text = obj.CheckResult;
                this.txtXiangji.Text = obj.Xiangji.ToString();
                this.txtCTLX.Text = obj.CT.ToString();
                this.txtGaoFen.Text = obj.Gaofen.ToString();
                this.txtPianFei.Text = obj.Pianfei.ToString();
                this.txtZhushe.Text = obj.Zhushi.ToString();
                this.txtXianyin.Text = obj.Xianyin.ToString();
                this.txtTotCost.Text = obj.TotCost.ToString();
                this.txtPayRate.Text = obj.PayRate.ToString();
                this.txtPubCost.Text = obj.PubCost.ToString();
                this.specialCheck = obj.Clone();
                IsEditMode = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void Save()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            pubMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            for (int i = 0; i < this.fpSpread1_Sheet1.Rows.Count; i++)
            {
                SOC.Local.PubReport.Models.SpecialCheck obj = this.ReadFromFp(i);
                if (obj.Seq == 0)
                {
                    obj.Seq = pubMgr.GetSpecialCheckSeq(this.patient.ID);
                }
                int iReturn;
                
                iReturn = pubMgr.UpdateSpecialCheck(obj);
                if (iReturn == 0)
                {
                    iReturn = this.pubMgr.InsertSpecialCheck(obj);
                }
                if (iReturn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("特殊检查保存失败" + this.pubMgr.Err);
                    return;
                }
            }
            foreach (SOC.Local.PubReport.Models.SpecialCheck obj in AlDelete)
            {
                if (pubMgr.DeleteSpecialCheck(obj) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("特殊检查删除失败" + this.pubMgr.Err);
                    return;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("特殊检查保存成功");

        }

        public void QuerySpecialCheck(string patientNo,string invNo)
        {
            ArrayList al = pubMgr.QuerySpecailCheck(patientNo, invNo);
            foreach (SOC.Local.PubReport.Models.SpecialCheck obj in al)
            {
                WriteToFp(this.fpSpread1_Sheet1.Rows.Count, obj);
            }
        }

        enum Col
        {
            单位名称,
            姓名,
            医疗证号,
            住院号,
            检查治疗日期,
            检查及治疗项目及部位,
            临床诊断,
            检查诊断,
            检查相机费,
            CT螺旋费,
            高分辨,
            片费,
            注射器费,
            显影剂费,
            合计,
            自付比例,
            实报金额
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SOC.Local.PubReport.Models.SpecialCheck obj = ReadFromPanel();
            WriteToFp(this.fpSpread1_Sheet1.Rows.Count, obj);
            SumCost();
            
        }

        ArrayList AlDelete = new ArrayList();
        private void btnDelete_Click(object sender, EventArgs e)
        {
            SOC.Local.PubReport.Models.SpecialCheck obj = ReadFromFp(this.fpSpread1_Sheet1.ActiveRowIndex);
            if (obj.Seq != 0)
            {
                AlDelete.Add(obj);
            }
            this.fpSpread1_Sheet1.Rows.Remove(this.fpSpread1_Sheet1.ActiveRowIndex, 1);

        }

        int ModifyRow = 0;
        void neuFpEnter1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            SOC.Local.PubReport.Models.SpecialCheck obj = ReadFromFp(this.fpSpread1_Sheet1.ActiveRowIndex);
            WriteToPanel(obj);
            this.btnAdd.Enabled = false;
            this.btnModify.Enabled = true;
            this.btnCancelModify.Enabled = true;
            ModifyRow = this.fpSpread1_Sheet1.ActiveRowIndex;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            SOC.Local.PubReport.Models.SpecialCheck obj = ReadFromPanel();
            WriteToFp(ModifyRow, obj);
            btnModify.Enabled = false;
            btnCancelModify.Enabled = false;
            btnAdd.Enabled = true;
        }

        public void Clear()
        {
            this.cmbItemName.Text = "";
            this.cmbDiagnoze.Text = "";
            this.cmbCheckResult.Text = "";
            this.txtXiangji.Text = "0";
            this.txtCTLX.Text = "0";
            this.txtGaoFen.Text = "0";
            this.txtPianFei.Text = "0";
            this.txtZhushe.Text = "0";
            this.txtXianyin.Text = "0";
            this.txtTotCost.Text = "0";
            this.txtPayRate.Text = "0";
            this.txtPubCost.Text = "0";
        }

        private void btnCancelModify_Click(object sender, EventArgs e)
        {
            Clear();
            btnModify.Enabled = false;
            btnCancelModify.Enabled = false;
            btnAdd.Enabled = true;
        }

        private decimal totCost;
        private decimal pubCost;
        private SOC.Local.PubReport.Models.SpecialCheck specialCheckobj = new SOC.Local.PubReport.Models.SpecialCheck();

        public SOC.Local.PubReport.Models.SpecialCheck SpecialCheck
        {
            get
            {
                return specialCheckobj;
            }
            set
            {
                this.specialCheckobj = value;
            }
        }

        private void SumCost()
        {
            SpecialCheck.TotCost = 0;
            SpecialCheck.PubCost = 0;
            for (int i = 0; i < this.fpSpread1_Sheet1.Rows.Count; i++)
            {
                SOC.Local.PubReport.Models.SpecialCheck obj = this.ReadFromFp(i);
                SpecialCheck.TotCost += obj.TotCost;
                SpecialCheck.PubCost += obj.PubCost;
                SpecialCheck.Xiangji += obj.Xiangji;
                SpecialCheck.Xianyin += obj.Xianyin;
            }
            lbTotCost.Text = "总金额:" + SpecialCheck.TotCost.ToString();
            lbPubCost.Text = "总记账:" + SpecialCheck.PubCost.ToString();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Save();
            SumCost();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SumCost();
            Close();
        }

        bool IsEditMode = true;

        private void cmbItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsEditMode)
            {
                FS.HISFC.Models.Base.Const con = this.cmbItemName.SelectedItem as FS.HISFC.Models.Base.Const;
                this.txtXiangji.Text = con.Memo;
                this.txtXianyin.Text = con.UserCode;
            }
        }

        private void txtXiangji_Leave(object sender, EventArgs e)
        {
            IsEditMode = false;
            SOC.Local.PubReport.Models.SpecialCheck obj = ReadFromPanel();
            obj.TotCost = obj.CT + obj.Gaofen + obj.Pianfei + obj.Xiangji + obj.Xianyin + obj.Zhushi;
            obj.PubCost = obj.TotCost * (1 - obj.PayRate);
            this.txtTotCost.Text = obj.TotCost.ToString();
            this.txtPubCost.Text = obj.PubCost.ToString();
            IsEditMode = true;
            
        }

    }
}
