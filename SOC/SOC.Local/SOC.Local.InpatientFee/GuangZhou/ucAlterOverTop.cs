using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.RADT;

namespace FS.SOC.Local.InpatientFee.GuangZhou
{
    public partial class ucAlterOverTop : UserControl
    {
        public ucAlterOverTop()
        {
            InitializeComponent();
        }

        FS.HISFC.Models.RADT.PatientInfo myPatient = null;
        FS.HISFC.BizLogic.RADT.InPatient myRadt = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.BizLogic.Fee.InPatient myFee = new FS.HISFC.BizLogic.Fee.InPatient();

		
		private void ucQueryInpatientNo1_myEvent()
		{
			this.txtLimitTot.Text = "";
			this.txtPubCost.Text = "";
			if(this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo == "")
			{
				MessageBox.Show("没有该住院号患者！");
				this.ucQueryInpatientNo1.Focus();
				return;
			}
															
			FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();
			myPatient = myRadt.QueryPatientInfoByInpatientNO(this.ucQueryInpatientNo1.InpatientNo);
			if(myPatient == null)
			{
				MessageBox.Show("查找患者信息出错!");
				this.ucQueryInpatientNo1.Focus();
				return;
			}
		
			this.lblName.Text = myPatient.Name;
            this.lblPatientNO.Text = myPatient.PID.PatientNO.ToString();
		}

		private void btnQuery_Click(object sender, System.EventArgs e)
		{
			if(this.myPatient == null)
			{
				MessageBox.Show("请回车确认住院号！");
				return;
			}
			ArrayList al = this.myFee.QueryFeeInfosGroupByMinFeeByInpatientNO(this.myPatient.ID,this.dtBegin.Value,
				this.dtEnd.Value,"0");
			if(al == null)
			{
				MessageBox.Show("查询患者费用出错！");
				return;
			}
			decimal pub_cost = 0m;
			foreach(FS.HISFC.Models.Fee.Inpatient.FeeInfo item in al)
			{
				if(item.Item.MinFee.ID == "001" || item.Item.MinFee.ID == "002" ||
					item.Item.MinFee.ID == "003")
				{
                    pub_cost += item.FT.PubCost + item.FT.PayCost;
				}
			}
			this.txtPubCost.Text = pub_cost.ToString();
		
		}

		private void ucAlterOverTop_Load(object sender, System.EventArgs e)
		{
			try
			{
				//Report.Management.PubReport report = new Local.Report.Management.PubReport();
                SOC.Local.PubReport.BizLogic.PubReport report = new FS.SOC.Local.PubReport.BizLogic.PubReport();

				FS.FrameWork.Models.NeuObject obj = report.GetStaticTime();

				if(obj == null)
				{
					return;
				}
				this.dtBegin.Value = FS.FrameWork.Function.NConvert.ToDateTime(obj.User01);
				this.dtEnd.Value = FS.FrameWork.Function.NConvert.ToDateTime(obj.User02);
			}
			catch
			{}
		}

		private void btnChange_Click(object sender, System.EventArgs e)
		{
			if(this.myPatient == null)
			{
				MessageBox.Show("请回车确认住院号！");
				return;
			}
			decimal LimitCost = 0m;
			if(this.txtLimitTot.Text == "")
			{
				MessageBox.Show("请输入药品限额！");
				return;
			}
			if(this.txtPubCost.Text == "")
			{
				MessageBox.Show("请查询患者药品记账金额");
				return;
			}
			LimitCost = FS.FrameWork.Function.NConvert.ToDecimal(this.txtLimitTot.Text);
			if(LimitCost == 0 && this.txtLimitTot.Text.Trim() != "0")
			{
				MessageBox.Show("你输入的金额不合法");
				return;
			}
			if(this.myPatient == null)
			{
				MessageBox.Show("请回车确认住院号！");
				return;
			}
			decimal PubCost = 0m;
			PubCost = FS.FrameWork.Function.NConvert.ToDecimal(this.txtPubCost.Text);
			if(PubCost == 0)
			{
				DialogResult dr =  MessageBox.Show("当前记帐金额为0，是否继续?","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Information
					,MessageBoxDefaultButton.Button1);
				if(dr == DialogResult.No)
				{
					return;
				}
			}
			#region 插入调整记录			
            FS.HISFC.Models.Base.FT ft = new FS.HISFC.Models.Base.FT();
			
			//调整金额=已记帐金额 - 超标金额
			ft.OwnCost = PubCost - LimitCost;
            if (ft.OwnCost < 0)
			{
				DialogResult dr =  MessageBox.Show("当前记帐金额小于应记帐金额，是否继续?","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Information
					,MessageBoxDefaultButton.Button1);
				if(dr == DialogResult.No)
				{
					return;
				}
			}
			//取患者费用比例					
            FS.HISFC.Models.Base.PactInfo PactUnitInfo = new PactInfo();
            FS.HISFC.BizLogic.Fee.PactUnitInfo PactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();			
			PactUnitInfo = PactManagment.GetPactUnitInfoByPactCode(myPatient.Pact.ID.ToString());
			if(PactUnitInfo == null)
			{
				MessageBox.Show("获取患者费用比例出错！");
				return;
			}
            FS.HISFC.Models.Fee.Inpatient.FeeItemList ItemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
			FS.HISFC.Models.Pharmacy.Item PhaItem = new FS.HISFC.Models.Pharmacy.Item();
			ItemList.Item = PhaItem;
			// 赋值			
            ItemList.TransType = TransTypes.Positive; //交易类型-3为调整
			//ItemList.InDept.ID=myPatient.PVisit.PatientLocation.Dept.ID;//在院科室

            ((PatientInfo)ItemList.Patient).PVisit.PatientLocation.Dept.ID = myPatient.PVisit.PatientLocation.Dept.ID;//在院科室
			//ItemList.NurseStation.ID=myPatient.PVisit.PatientLocation.NurseCell.ID; //护士站
            ItemList.ExecOper.Dept.ID = myPatient.PVisit.PatientLocation.Dept.ID;
            ItemList.StockOper.Dept.ID = myPatient.PVisit.PatientLocation.Dept.ID;
            ItemList.RecipeOper.Dept.ID = myPatient.PVisit.PatientLocation.Dept.ID;
            ItemList.RecipeOper.ID = myPatient.PVisit.AdmittingDoctor.ID; //医生
			
			ItemList.Item.Qty=1;//数量			
			ItemList.Item.PriceUnit="次";

            ItemList.Item.ItemType = EnumItemType.Drug;
            ItemList.PayType = PayTypes.SendDruged;
			ItemList.IsBaby=false;
			//ItemList.Order.OrderType=
            ItemList.BalanceNO = 0;
            ItemList.BalanceState = "0";
            ItemList.NoBackQty = 1;
			ItemList.ChargeOper.ID=this.myFee.Operator.ID;
			ItemList.ChargeOper.OperTime=this.dtEnd.Value.AddMinutes(-29); //划价时间
			ItemList.FeeOper.ID=this.myFee.Operator.ID;
			ItemList.FeeOper.OperTime=this.dtEnd.Value.AddMinutes(-29);
			ItemList.Item.PackQty = 1;
            ItemList.FTSource.SourceType1 = "C";//日限额调整标志

			ItemList.FT.OwnCost = ft.OwnCost;
            ItemList.FT.PayCost = -FS.FrameWork.Public.String.FormatNumber(ft.OwnCost * PactUnitInfo.Rate.PayRate, 2);
            ItemList.FT.TotCost = 0;
            ItemList.FT.PubCost = -(ft.OwnCost + ItemList.FT.PayCost);
			
			ItemList.Item.Price=0;//
			ItemList.Item.ID="Y001";
            if (ft.OwnCost > 0)
			{
				ItemList.Item.Name="西药费(公费到自费)";
			}
			else
			{
				ItemList.Item.Name="西药费(自费到公费)";
			}
			//最小费用
			ItemList.Item.MinFee.ID="001";
			ItemList.RecipeNO=this.myFee.GetDrugRecipeNO();
			ItemList.SequenceNO=1;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.myFee.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

			//费用明细表
			//if(this.myFee.AddPatientMedAccount(myPatient,ItemList)==-1)
            if(this.myFee.InsertMedItemList(myPatient,ItemList)==-1)
			{
                FS.FrameWork.Management.PublicTrans.RollBack();
				MessageBox.Show(this.myFee.Err);
				return ;
			}
			//费用汇总表
			//if(this.myFee.UpdateAccount(myPatient,ItemList.FeeInfo)==-1)
            if (this.myFee.InsertFeeInfo(myPatient, ItemList) == -1)
			{
                FS.FrameWork.Management.PublicTrans.RollBack();
				MessageBox.Show(this.myFee.Err);
				return ;
			}
			//int parm = this.myFee.UpdateAccount(myPatient.ID,ItemList.FeeInfo.Fee);
            int parm = this.myFee.UpdateInMainInfoFee(myPatient.ID, ItemList.FT);
			if(parm == -1)
			{
                FS.FrameWork.Management.PublicTrans.RollBack();
				MessageBox.Show("更新住院主表失败!");
				return ;
			}
			if(parm == 0)
			{
                FS.FrameWork.Management.PublicTrans.RollBack();
				MessageBox.Show("患者已结算或者处于封账状态，不能收费，请先开帐再收费!");
				return ;
			}
			//更新公费日限额累计和超标金额
			if(this.myFee.UpdateLimitOverTop(this.myPatient.ID,-this.myPatient.FT.OvertopCost)<1)
			{
				return ;
			}
            FS.FrameWork.Management.PublicTrans.Commit();
			MessageBox.Show("调整患者药品记帐金额成功！");
			#endregion
		}
    }
}
