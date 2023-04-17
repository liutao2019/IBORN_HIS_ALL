using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.WinForms.Report.InpatientFee
{
    /// <summary>
    /// 住院费用结算表（减免）
    /// 2011-6-17 顺德妇幼
    /// </summary>
    public partial class ucFinIpbDerate : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucFinIpbDerate()
        {
            InitializeComponent();
        }

        private int colunmNum = 4;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            FarPoint.Win.BevelBorder bevelBorder1 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered, System.Drawing.SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlText, 1, true, true, false, true);
            FarPoint.Win.BevelBorder bevelBorder2 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlText, 1, true, true, false, true);
            FarPoint.Win.BevelBorder bevelBorder3 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Lowered, System.Drawing.SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlText, 1, false, true, true, true);
            FarPoint.Win.BevelBorder bevelBorder4 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.SystemColors.ControlText, 1, false, true, true, true);

            FarPoint.Win.CompoundBorder compoundBorder1 = new FarPoint.Win.CompoundBorder(bevelBorder1, bevelBorder2);
            FarPoint.Win.CompoundBorder compoundBorder2 = new FarPoint.Win.CompoundBorder(bevelBorder3, bevelBorder4);
            //加载最小费用信息

            FS.HISFC.BizProcess.Integrate.Manager constManager = new FS.HISFC.BizProcess.Integrate.Manager();
            ArrayList alMinFee= constManager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.MINFEE);
            if (alMinFee != null)
            {
                this.neuSpread1_Sheet1.RowCount=0;
                this.neuSpread1_Sheet1.ColumnCount = colunmNum * 2;
                for (int i = 0; i < alMinFee.Count; i++)
                {
                    if (i % colunmNum== 0)
                    {
                        this.neuSpread1_Sheet1.RowCount++;
                    }

                    FS.FrameWork.Models.NeuObject obj=alMinFee[i] as FS.FrameWork.Models.NeuObject;

                    this.neuSpread1_Sheet1.Cells[i / colunmNum, i % colunmNum * 2].Text = obj.Name;
                    this.neuSpread1_Sheet1.Cells[i / colunmNum, i % colunmNum * 2].Tag = obj.ID;

                    this.neuSpread1_Sheet1.Cells[i / colunmNum, i % colunmNum * 2].Border = compoundBorder1;
                    this.neuSpread1_Sheet1.Cells[i / colunmNum, i % colunmNum * 2 + 1].Border = compoundBorder2;
                }
            }
            //重新结算Fp的高度
            this.neuSpread1.Height = (int)(this.neuSpread1_Sheet1.RowCount * this.neuSpread1_Sheet1.Rows.Default.Height + 1);
            this.neuPanel2.Height = this.neuSpread1.Height;

            this.Clear();
        }

        /// <summary>
        /// 清空界面信息
        /// </summary>
        private void Clear()
        {
            this.ClearPatientInfo();
            this.tvBalanceInfo.Nodes.Clear();
            this.tvBalanceInfo.Tag = null;
        }

        private void ClearPatientInfo()
        {
            this.txtAge.Text = string.Empty;
            this.txtBalanceDate.Text = string.Empty;
            this.txtBalanceKind.Text = string.Empty;
            this.txtBalanceTimes.Text = string.Empty;
            this.txtBalanceType.Text = string.Empty;
            this.txtBedNO.Text = string.Empty;
            this.txtDept.Text = string.Empty;
            this.txtHome.Text = string.Empty;
            this.txtIddNO.Text = string.Empty;
            this.txtInDate.Text = string.Empty;
            this.txtInDiagnose.Text = string.Empty;
            this.txtJzDanwei.Text = string.Empty;
            this.txtSbKind.Text = string.Empty;
            this.txtIntimes.Text = string.Empty;
            this.txtLinkManAddress.Text = string.Empty;
            this.txtLinkManName.Text = string.Empty;
            this.txtLinkManPhone.Text = string.Empty;
            this.txtLinkManRelation.Text = string.Empty;
            this.txtMedicalNO.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.txtNurse.Text = string.Empty;
            this.txtOutDate.Text = string.Empty;
            this.txtOutDiagnose.Text = string.Empty;
            this.txtOwnCost.Text = string.Empty;
            this.txtPatientNO.Text = string.Empty;
            this.txtPayCost.Text = string.Empty;
            this.txtPhone.Text = string.Empty;
            this.txtPubCost.Text = string.Empty;
            this.txtSex.Text = string.Empty;
            this.txtTotCost.Text = string.Empty;
            this.txtWorkName.Text = string.Empty;

            //清空费用信息
            for (int i = 0; i < this.neuSpread1_Sheet1.ColumnCount; i++)
            {
                if(i%2==1)
                {
                    this.neuSpread1_Sheet1.Cells[0, i, this.neuSpread1_Sheet1.RowCount - 1, i].Text = "   ";
                }
            }
        }

        /// <summary>
        /// 设置界面信息
        /// </summary>
        private void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo,FS.HISFC.Models.Fee.Inpatient.Balance balanceInfo)
        {
            if (patientInfo==null||balanceInfo == null)
            {
                return;
            }
            this.ClearPatientInfo();

            this.txtAge.Text = patientInfo.Age;
            this.txtBalanceDate.Text = balanceInfo.BalanceOper.OperTime.ToString("yyyy.MM.dd");
            //获取PactName
            FS.HISFC.BizProcess.Integrate.Manager constManager = new FS.HISFC.BizProcess.Integrate.Manager();
            FS.FrameWork.Models.NeuObject obj = constManager.GetConstant("PACTUNIT", balanceInfo.Patient.Pact.ID);
            if (obj == null)
            {
                this.txtBalanceKind.Text = balanceInfo.Patient.Pact.Name;
            }
            else
            {
                this.txtBalanceKind.Text = obj.Name;
            }
            //
            this.txtBalanceType.Text = balanceInfo.BalanceType.Name;
            this.txtBedNO.Text = patientInfo.PVisit.PatientLocation.Bed.ID;
            this.txtDept.Text = patientInfo.PVisit.PatientLocation.Dept.Name;
            this.txtHome.Text = patientInfo.AddressHome;
            this.txtIddNO.Text = patientInfo.IDCard;
            this.txtInDate.Text = patientInfo.PVisit.InTime.ToString("yyyy.MM.dd");
            //this.txtInDiagnose.Text = 
            this.txtBalanceTimes.Text = balanceInfo.Memo.PadLeft(4, '0');
            //不知道从哪取
            this.txtJzDanwei.Text = "";
            this.txtSbKind.Text = "";

            this.txtIntimes.Text = patientInfo.InTimes.ToString().PadLeft(2,'0');
            this.txtLinkManAddress.Text = patientInfo.Kin.RelationAddress;
            this.txtLinkManName.Text = patientInfo.Kin.Name;
            this.txtLinkManPhone.Text = patientInfo.Kin.RelationPhone;
            obj = constManager.GetConstant(FS.HISFC.Models.Base.EnumConstant.RELATIVE.ToString(), patientInfo.Kin.Relation.ID);
            if (obj != null)
            {
                this.txtLinkManRelation.Text = obj.Name;
            }
            else
            {
                this.txtLinkManRelation.Text = patientInfo.Kin.Relation.Name;
            }
            this.txtMedicalNO.Text = patientInfo.PID.HealthNO;
            this.txtName.Text = patientInfo.Name;
            this.txtNurse.Text = patientInfo.PVisit.PatientLocation.NurseCell.Name;
            this.txtOutDate.Text = patientInfo.PVisit.OutTime.ToString("yyyy.MM.dd");
            this.txtOutDiagnose.Text = string.Empty;
            this.txtOwnCost.Text = (balanceInfo.FT.OwnCost - balanceInfo.FT.DerateCost).ToString("F2");
            this.txtPatientNO.Text = patientInfo.PID.PatientNO;
            this.txtPayCost.Text = balanceInfo.FT.PayCost.ToString("F2");
            this.txtPhone.Text = patientInfo.PhoneHome;
            this.txtPubCost.Text = balanceInfo.FT.DerateCost.ToString("F2");//balanceInfo.FT.PubCost.ToString("F2");
            this.txtSex.Text = patientInfo.Sex.Name;
            this.txtTotCost.Text = balanceInfo.FT.TotCost.ToString("F2");
            this.txtWorkName.Text = patientInfo.AddressBusiness;

            FS.HISFC.BizLogic.HealthRecord.Diagnose diagnoseManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            FS.HISFC.Models.HealthRecord.Diagnose inDiagnose= diagnoseManager.GetFirstDiagnose(patientInfo.ID, FS.HISFC.Models.HealthRecord.DiagnoseType.enuDiagnoseType.IN, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (inDiagnose != null)
            {
                this.txtInDiagnose.Text = inDiagnose.Name;
            }

            FS.HISFC.Models.HealthRecord.Diagnose outDiagnose = diagnoseManager.GetFirstDiagnose(patientInfo.ID, FS.HISFC.Models.HealthRecord.DiagnoseType.enuDiagnoseType.OUT, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            if (inDiagnose != null)
            {
                this.txtOutDiagnose.Text = outDiagnose.Name;
            }

            //获取减免信息

            FS.HISFC.BizLogic.Fee.Derate derateManager = new FS.HISFC.BizLogic.Fee.Derate();
            ArrayList alDreateInfo = derateManager.QueryDerateDetailByClinicNOAndInvoiceNO(patientInfo.ID, balanceInfo.Invoice.ID);
            if (alDreateInfo != null)
            {
                foreach (FS.HISFC.Models.Fee.DerateFee derate in alDreateInfo)
                {
                    for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                    {
                        for (int j = 0; j < colunmNum; j++)
                        {
                            object minfeeID=this.neuSpread1_Sheet1.Cells[i,j*2].Tag;
                            if (minfeeID != null && minfeeID.ToString() == derate.FeeCode)
                            {
                                this.neuSpread1_Sheet1.Cells[i, j * 2 + 1].Text = derate.DerateCost.ToString("F2");
                            }
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 回车住院号
        /// </summary>
        private void ucQueryInpatientNo1_myEvent()
        {
            if (string.IsNullOrEmpty(this.ucQueryInpatientNo1.InpatientNo))
            {
                MessageBox.Show(this, "提示", "请输入住院号!");
                return;
            }

            this.Clear();
            //先查询患者信息
            FS.HISFC.BizProcess.Integrate.RADT radtIntegrate=new FS.HISFC.BizProcess.Integrate.RADT();
            FS.HISFC.Models.RADT.PatientInfo patientInfo=radtIntegrate.GetPatientInfomation(this.ucQueryInpatientNo1.InpatientNo);
            if(patientInfo==null||string.IsNullOrEmpty(patientInfo.ID))
            {
                MessageBox.Show(this, "提示", "不存在住院号为："+this.ucQueryInpatientNo1.InpatientNo+"的患者住院信息");
                return;
            }

            this.tvBalanceInfo.Tag = patientInfo;

            FS.HISFC.BizLogic.Fee.InPatient inpatientFeeManager = new FS.HISFC.BizLogic.Fee.InPatient();
            ArrayList alBalanceInfo = inpatientFeeManager.QueryBalancesByInpatientNO(this.ucQueryInpatientNo1.InpatientNo);
            
            if (alBalanceInfo != null)
            {
                if (alBalanceInfo.Count == 0)
                {
                    MessageBox.Show(this, "提示", patientInfo.Name+"没有结算信息");
                    return;
                }
                else
                {
                    alBalanceInfo.Sort(new BalanceComparer());
                    int i = 0;
                    foreach (FS.HISFC.Models.Fee.Inpatient.Balance balanaceInfo in alBalanceInfo)
                    {
                        if (balanaceInfo.CancelType != FS.HISFC.Models.Base.CancelTypes.Valid)
                        {
                            continue;
                        }
                        i++;
                        //加载结算信息
                        TreeNode node = new TreeNode();
                        balanaceInfo.Memo = i.ToString();
                        node.Name = balanaceInfo.Invoice.ID + ((int)balanaceInfo.TransType).ToString();
                        node.Text = balanaceInfo.Invoice.ID + "【" + balanaceInfo.BalanceType.Name + "】"+"【"+balanaceInfo.ID+"】";
                        node.Tag = balanaceInfo;

                        this.tvBalanceInfo.Nodes.Add(node);
                    }

                    if (this.tvBalanceInfo.Nodes.Count > 0)
                    {
                        this.tvBalanceInfo.SelectedNode = this.tvBalanceInfo.Nodes[0];
                    }
                    else
                    {
                        MessageBox.Show(this, "提示", patientInfo.Name + "没有有效的结算信息");
                        return;
                    }
                }
            }

        }

        private void tvBalanceInfo_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                if (this.tvBalanceInfo.Tag is FS.HISFC.Models.RADT.PatientInfo &&
                    e.Node.Tag is FS.HISFC.Models.Fee.Inpatient.Balance)
                {
                    this.SetPatientInfo(this.tvBalanceInfo.Tag as FS.HISFC.Models.RADT.PatientInfo, e.Node.Tag as FS.HISFC.Models.Fee.Inpatient.Balance);
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

        protected override int OnPrintPreview(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            return print.PrintPreview(this.gbFee);

        }

    }

    public class BalanceComparer : IComparer
    {

        #region IComparer 成员

        public int Compare(object x, object y)
        {
            FS.HISFC.Models.Fee.Inpatient.Balance bx=x as FS.HISFC.Models.Fee.Inpatient.Balance;
            FS.HISFC.Models.Fee.Inpatient.Balance  by=y as FS.HISFC.Models.Fee.Inpatient.Balance ;

            if (bx == null)
            {
                return by == null ? 0 : -1;
            }
            else if (by == null)
            {
                return bx == null ? 0 : 1;
            }
            else
            {
                return string.Compare(bx.Invoice.ID + ((int)bx.TransType).ToString() + bx.ID, by.Invoice.ID + ((int)by.TransType).ToString() + by.ID);
            }

            return 0;
        }

        #endregion
    }


    
}
