using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using System.Collections;
namespace FS.HISFC.Components.Manager.Controls
{
    public partial class ucBedList : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucBedList()
        {
            InitializeComponent();
            InitSpread();
        }

        #region ����
        public string strTag = "";

        TreeNode CurrentNode = new TreeNode();//��ǰѡ�еĽڵ�

		System.Data.DataSet myDataSet = new System.Data.DataSet();

		public FS.HISFC.Models.Base.Bed GetBedInfo()
		{			
			FS.HISFC.Models.Base.Bed oBedInfo = new FS.HISFC.Models.Base.Bed();
			int iIndex = fpSpread1.Sheets[0].ActiveRow.Index;
			oBedInfo.NurseStation.ID = fpSpread1.Sheets[0].Cells[iIndex,0].Text;//��ʿվ���
			oBedInfo.SickRoom.ID = fpSpread1.Sheets[0].Cells[iIndex,1].Text;//������
            oBedInfo.ID = oBedInfo.NurseStation.ID + fpSpread1.Sheets[0].Cells[iIndex, 2].Text;//������ liuxq070924
			oBedInfo.BedGrade.Memo = fpSpread1.Sheets[0].Cells[iIndex,3].Text;//�����ȼ�
			oBedInfo.Status.Name = fpSpread1.Sheets[0].Cells[iIndex,4].Text;//����״̬
            oBedInfo.BedRankEnumService.Name = fpSpread1.Sheets[0].Cells[iIndex, 5].Text;//��������
			oBedInfo.Phone = fpSpread1.Sheets[0].Cells[iIndex,6].Text;//�绰
			oBedInfo.SortID = Convert.ToInt32(fpSpread1.Sheets[0].Cells[iIndex,9].Text);//˳���
			oBedInfo.OwnerPc = fpSpread1.Sheets[0].Cells[iIndex,7].Text;//����
			oBedInfo.BedGrade.ID = fpSpread1.Sheets[0].Cells[iIndex,10].Text;
            oBedInfo.Status.ID = fpSpread1.Sheets[0].Cells[iIndex, 11].Text;
			oBedInfo.BedRankEnumService.ID = fpSpread1.Sheets[0].Cells[iIndex,12].Text;
            oBedInfo.InpatientNO = fpSpread1_Sheet1.Cells[iIndex,13].Text;
			return oBedInfo;
		}

		private void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
		{

		}

		private void EventResultChanged(ArrayList s)
		{
		}

		/// <summary>
		/// ������������е����ݱ�����myDataSet��
		/// </summary>
		/// <param name="arrBed">��λ��Ϣ</param>
		public void dataSet_Init(ArrayList arrBed)
		{
			DataSet dts = new DataSet();
			dts.EnforceConstraints = true;//�Ƿ���ѭԼ������
			this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
 
			//��������ӵ�myDataSet��
			DataTable myDataTable = dts.Tables.Add();			
			//��յ�ǰmyDataSet�е�������
			myDataTable.Columns.Clear();			
			myDataTable.Columns.AddRange
				(new System.Data.DataColumn[] 
					{
						new System.Data.DataColumn("��ʿվ��",Type.GetType("System.String")), //0
						new System.Data.DataColumn("������",Type.GetType("System.String")),   //1
						new System.Data.DataColumn("����", Type.GetType("System.String")),    //2
						new System.Data.DataColumn("��λ�ȼ�", Type.GetType("System.String")), //3
						new System.Data.DataColumn("��λ״̬", Type.GetType("System.String")), //4
						new System.Data.DataColumn("��λ����", Type.GetType("System.String")), //5
						new System.Data.DataColumn("�����绰", Type.GetType("System.String")), //6
						new System.Data.DataColumn("����", Type.GetType("System.String")),     //7
						new System.Data.DataColumn("����", Type.GetType("System.String")),     //8
						new System.Data.DataColumn("˳���", Type.GetType("System.String")),   //9
						new System.Data.DataColumn("Levelid", Type.GetType("System.String")),  //10
						new System.Data.DataColumn("Stateid", Type.GetType("System.String")),  //11
 						new System.Data.DataColumn("Weaveid", Type.GetType("System.String")) ,  //12
                        new System.Data.DataColumn("סԺ��", Type.GetType("System.String")),
                        new System.Data.DataColumn("����۸�", Type.GetType("System.String")) //14
					}
				);
	
			DataRow dr;
			FS.HISFC.Models.Base.Bed oEBed = new FS.HISFC.Models.Base.Bed();;
			if(arrBed!=null)
			{
				//ѭ�����������Ϣ
				for( int i = 0; i < arrBed.Count; i++ )
				{	
					oEBed = (FS.HISFC.Models.Base.Bed)arrBed[i];
					dr = myDataTable.NewRow();			
					this.SetRow( dr, oEBed );
					myDataTable.Rows.Add( dr );	
				}
			}

			//����DataView��
			this.fpSpread1_Sheet1.DataSource = dts.Tables[0].DefaultView;
			InitSpread();
		}
        
		private FS.HISFC.BizLogic.Manager.Bed oCBed = new FS.HISFC.BizLogic.Manager.Bed();
		public int DelBedInfo()
		{
			int iRet = 0;
			int iIndex = fpSpread1.Sheets[0].ActiveRow.Index;		
			string strNurse = fpSpread1.Sheets[0].Cells[iIndex,0].Text;//��ʿվ���
            string strBedID = strNurse + fpSpread1.Sheets[0].Cells[iIndex, 2].Text;//������ liuxq070924
			string strWardNo = fpSpread1.Sheets[0].Cells[iIndex,1].Text;//������
			string strBedState = fpSpread1.Sheets[0].Cells[iIndex,4].Text;
			try
			{
                /*
                 * [2007/02/02] ����һ��İ�.
                 * if(strBedState!="�մ�")
                 * {
				 *   	this.Err = "�˴���ռ�ò���ɾ��!";
				 *	    iRet = -1;
				 *	    MessageBox.Show(Err);
                 * }
                 */
                if (strBedState=="ռ��" || strBedState=="���" || strBedState=="����" || strBedState=="�Ҵ�")
                {
					this.Err = "�˴���ռ�ò���ɾ��!";
					iRet = -1;
					MessageBox.Show(Err);
                }
				else
				{

                    FS.HISFC.Models.Base.Bed bed = this.oCBed.GetBedInfo(strBedID);

					if (this.oCBed.DeleteBedInfo(strBedID) == 0)
					{
						this.Err = "ɾ���ɹ���";

                        //ɾ������
                        deleteTreeNode();


                        if (bed != null)
                        {
                            //Ƕ�������ϵͳ������ҵ��ģ�����Ϣ�����ŽӴ���
                            string errInfo = "";
                            System.Collections.ArrayList alInfo = new System.Collections.ArrayList();
                            alInfo.Add(bed);
                            int param = Function.SendBizMessage(alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Delete, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Bed, ref errInfo);

                            if (param == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                                Function.ShowMessage("��λɾ��ʧ�ܣ�����ϵͳ����Ա���������Ϣ��" + errInfo, MessageBoxIcon.Error);
                                return -1;
                            }
                        }
					}
					else
					{
						this.Err = "ɾ��ʧ�ܣ�";
					}
				}
			}
			catch{}
			if(this.strTag=="0")
			{
				ReBind(strNurse,strTag,"");//��ʿվ
			}
			if(this.strTag=="1")
			{
				ReBind(strWardNo,strTag,strNurse);//����
			}

			
			return iRet;
		}

		
		private string Err;
		private void ReBind(string strID,string strTag,string strNurseID)
		{
			ArrayList arr = new ArrayList();
			if(strTag=="0")
			{
				arr = oCBed.GetBedList(strID);
			}
			if(strTag=="1")
			{
				arr = oCBed.GetBedListByRoom(strID,strNurseID);
			}
		
			this.dataSet_Init(arr);
			InitSpread();
		}


		private DataRow SetRow( DataRow dr, FS.HISFC.Models.Base.Bed objBed )
		{
			if(objBed!=null)
			{
				dr["��ʿվ��"] = objBed.NurseStation.ID ;//��ʿվ���
				//			oBedInfo.NurseStation.Name = cboNurseCell.Text.Trim();
				dr["������"] = objBed.SickRoom.ID;//.NurseStation.ID ;//������
				dr["����"] = objBed.ID.Substring(objBed.NurseStation.ID.Length,(objBed.ID.Length - objBed.NurseStation.ID.Length)) ;//������ liuxq070924
				dr["��λ�ȼ�"] = objBed.BedGrade.Name;//�����ȼ�
				dr["��λ״̬"] = objBed.Status.Name ;//����״̬
				dr["��λ����"] = objBed.BedRankEnumService.Name ;//��������
				dr["�����绰"] = objBed.Phone ;//�绰
				dr["˳���"] = objBed.SortID ;//˳���
				dr["����"] = objBed.OwnerPc;//����
				dr["����"] = objBed.User03.ToString();//����
				dr["Levelid"] = objBed.BedGrade.ID;
				dr["Stateid"] = objBed.Status.ID;
                dr["Weaveid"] = objBed.BedRankEnumService.ID;
                dr["סԺ��"] = objBed.InpatientNO;
                dr["����۸�"] = objBed.Memo;
			}
			return dr;
		}

		
		private void InitSpread()
		{
			this.fpSpread1_Sheet1.Columns[0].Width = 100;
			this.fpSpread1_Sheet1.Columns[1].Width = 60;
			this.fpSpread1_Sheet1.Columns[2].Width = 80;
			this.fpSpread1_Sheet1.Columns[3].Width = 80;
			this.fpSpread1_Sheet1.Columns[4].Width = 100;
			this.fpSpread1_Sheet1.Columns[5].Width = 100;
			this.fpSpread1_Sheet1.Columns[6].Width = 80;
			this.fpSpread1_Sheet1.Columns[7].Width = 80;
			this.fpSpread1_Sheet1.Columns[8].Width = 40;		
			this.fpSpread1_Sheet1.Columns[9].Width =50;
			this.fpSpread1_Sheet1.Columns[10].Width = 0;
			this.fpSpread1_Sheet1.Columns[11].Width = 0;
			this.fpSpread1_Sheet1.Columns[12].Width = 0;
            this.fpSpread1_Sheet1.Columns[14].Width = 80;
            if (fpSpread1_Sheet1.Rows.Count > 0)
            {
                fpSpread1.ContextMenuStrip = contextMenuStrip1;
            }
            else
            {
                fpSpread1.ContextMenuStrip = null;

            }
            for (int columnIndex = 0; columnIndex < this.fpSpread1_Sheet1.ColumnCount; columnIndex++)
            {
                fpSpread1_Sheet1.Columns[columnIndex].AllowAutoSort = true;
            }
		}

        public void SetActiveSell(string BedNo)
		{
			for(int i=0;i<fpSpread1_Sheet1.Rows.Count;i++)
			{
				if(fpSpread1_Sheet1.Cells[i,2].Text==BedNo)
				{
					fpSpread1_Sheet1.SetActiveCell(i,2);
					return ;
				}
			}
		}

		
        #endregion

        FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// �����ӵĴ���
        /// </summary>
        /// <param name="isEnabled"></param>
        protected override void OnPrintPreviewButtonChanged(bool isEnabled)
        {
            isEnabled = false;
            base.OnPrintPreviewButtonChanged(isEnabled);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Border;
                p.PrintPreview(this);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            return base.OnPrint(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            if (this.fpSpread1_Sheet1.Rows.Count == 0)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("û��Ҫ���������!"), "��Ϣ");
                return -1;
            }
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "(*.xls)|*.xls";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.fpSpread1.SaveExcel(dlg.FileName);
                this.fpSpread1.SaveExcel(dlg.FileName, FarPoint.Excel.ExcelSaveFlags.SaveBothCustomRowAndColumnHeaders);
                return 1;
            }
            else
                return 0;

           // return base.Export(sender, neuObject);
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.dataSet_Init(new ArrayList());
            toolbarService.AddToolButton("���", "��Ӵ�λ", 0, true, false, null);
            toolbarService.AddToolButton("�������", "������Ӵ�λ", 0, true, false, null);
            toolbarService.AddToolButton("����", "���ƴ�λ", 0, true, false, null);
            toolbarService.AddToolButton("ɾ��", "ɾ����λ", 0, true, false, null);
            return toolbarService;
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void  ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "���":
                    this.AddInfo();
                    break;
                case "�������":
                    this.BatchAddInfo();
                    break;
                case "����":
                    this.CopyInfo();
                    break;
                case "ɾ��":
                    this.DeleteBed();
                    break;
            }
 	        base.ToolStrip_ItemClicked(sender, e);
        }
        private void BatchAddInfo()
        {
            Forms.frmBatchAddBed f = new Manager.Forms.frmBatchAddBed(false);
            if (CurrentNode.Parent != null) // �жϽڵ�����ȡ  �������վ
            {
                if (CurrentNode.Parent.Parent == null)
                {
                    f.NurseStation = CurrentNode.Tag.ToString();
                    f.BedRoomNO = null;
                }
                else if (CurrentNode.Parent.Parent != null)
                {
                    f.NurseStation = CurrentNode.Parent.Tag.ToString();
                    f.BedRoomNO = CurrentNode.Text.ToString();
                }
            }

            if (f.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ((tvNurseList)tv).InitTree();
                }
                catch
                {
                }
            }

        }
        private void AddInfo()
		{
            Forms.frmBedManager f = new Manager.Forms.frmBedManager(false);
            if (CurrentNode.Parent != null) // �жϽڵ�����ȡ  �������վ
            {
                if ( CurrentNode.Parent.Parent == null)
                {
                    f.NurseStation = CurrentNode.Tag.ToString();
                    f.BedRoomNO = null;
                }
                else if (CurrentNode.Parent.Parent != null)
                {
                    f.NurseStation = CurrentNode.Parent.Tag.ToString();
                    f.BedRoomNO = CurrentNode.Text.ToString();
                }
            }

            if (f.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ((tvNurseList)tv).InitTree();
                }
                catch { }
            }
             
		}
        private void ModifiedInfo()
        {
             
            Forms.frmBedManager f = new Manager.Forms.frmBedManager(true);
            f.SetBedInfo( this.GetBedInfo());
           
            if (f.ShowDialog() == DialogResult.OK)
            {
                //Ӧ��дˢ�´���
                this.Refresh();
            }

        }

        /// <summary>
        /// �޸�������ݺ�ˢ������
        /// </summary>
        private void Refresh()
        {
            ArrayList arr = new ArrayList();
            if (strID == string.Empty) return;
            if (strTag == "1")
            {
                arr = oCBed.GetBedListByRoom(strID, NurseID);
            }
            else if (strTag == "0")
            {
                arr = oCBed.GetBedList(strID);
            }
            else
            {
                arr = oCBed.GetBedList(strID);
            }
            this.dataSet_Init(arr);
        }


        private void CopyInfo()
        {
            Forms.frmCopyBed f = new Manager.Forms.frmCopyBed(true);
            f.SetBedInfo(this.GetBedInfo());

            if (f.ShowDialog() == DialogResult.OK)
            {
                //Ӧ��дˢ�´���
                this.Refresh();
            }

        }

        private void DeleteBed()
        {
            DialogResult result;
            if (this.fpSpread1.Sheets[0].ActiveRowIndex < 0) return;
            string bedno = fpSpread1.Sheets[0].Cells[this.fpSpread1.Sheets[0].ActiveRowIndex, 2].Text;
            result = MessageBox.Show(string.Format("ȷ��Ҫɾ��{0}��λ��Ϣ��",bedno), "ȷ��", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (this.DelBedInfo() != -1)
                    {
                        MessageBox.Show("ɾ���ɹ���");
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
            //if (result == DialogResult.No)
            //{

            //}
        }
        string NurseID;
        string strID = string.Empty;
        protected override int  OnSetValue(object neuObject, TreeNode e)
        {
            //string strID = "";
            ArrayList arr = new ArrayList();
            FS.HISFC.BizLogic.Manager.Bed oCBed = new FS.HISFC.BizLogic.Manager.Bed();
            if (e != null)
            {
                CurrentNode = e;
                if (e.Parent != null && e.Parent.Parent != null)//������
                {
                    string strNurse = e.Parent.Tag.ToString();
                    strID = e.Text.Trim();
                    arr = oCBed.GetBedListByRoom(strID, strNurse);
                    this.strTag = "1";
                    this.dataSet_Init(arr);
                    this.NurseID = strNurse; //��ʿվ    
                }
                else if (e.Parent != null)//��ʿվ��
                {
                    if (e.Tag != null)
                    {
                        strID = e.Tag.ToString();
                        arr = oCBed.GetBedList(strID);
                        this.strTag = "0";
                        this.dataSet_Init(arr);
                        strID = "";
                    }
                }
                else
                {
                    strID = "ALL";
                    arr = oCBed.GetBedList(strID);
                    this.dataSet_Init(arr);

                }
            }
            
            
            return base.OnSetValue(neuObject, e);
        }  
        private void fpSpread1_CellDoubleClick_1(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.ModifiedInfo(); 
        }

        /// <summary>
        /// ���䴲λ��������б���ͬʱˢ��,��ĳ�����䴲λ��ȫɾ����÷�����������б����Զ�ɾ�� bug ����
        /// </summary>
        private void deleteTreeNode()
        {
            int i = 0;

            string currentNursestation = string.Empty;  // Ҫɾ���Ĳ������ڻ�ʿվ
            string currentBedroomno = string.Empty;// Ҫɾ���Ĳ������ڲ�����

            currentNursestation = fpSpread1_Sheet1.GetText(fpSpread1_Sheet1.ActiveRowIndex, 0);
            currentBedroomno = fpSpread1_Sheet1.GetText(fpSpread1_Sheet1.ActiveRowIndex, 1);

            fpSpread1.Sheets[0].ActiveRow.Remove();
            // ����iֵ �������㵱ǰѡ���еĻ���վ  �Ͳ������ڽ������Ƿ��Ǵ���
            if (fpSpread1_Sheet1.Rows.Count > 0)
            {
                for (int j = 0; j < fpSpread1_Sheet1.Rows.Count; j++)
                {
                    if (fpSpread1_Sheet1.GetText(j, 0) == currentNursestation && fpSpread1_Sheet1.GetText(j, 1) == currentBedroomno)  //��ʿվ
                    {
                        i++;
                    }

                }
            }
            // ѡ�е�һ��ڵ�ɾ����λ
            if (CurrentNode.Parent == null) 
            {
                if (i <= 0)
                {
                    foreach (TreeNode tn in this.CurrentNode.Nodes)
                    {
                        if (tn.Tag.ToString() == currentNursestation)
                        {
                            foreach (TreeNode tn1 in tn.Nodes)
                            {
                                if (tn1.Text == currentBedroomno)
                                {
                                    tn1.Remove();
                                    break;
                                }
                            }
                        }
                        
                    }
                }
            }

            // ѡ����ĩ��ڵ�ɾ����λ
            if (this.fpSpread1_Sheet1.Rows.Count == 0 && CurrentNode.Parent != null && CurrentNode.Parent.Parent != null)
            {
                this.CurrentNode.Remove();
            }
            // ѡ��ڶ���ڵ�ɾ����λ
            else if (CurrentNode.Parent != null)
            {
                if (i <= 0)
                {
                    foreach (TreeNode tn in this.CurrentNode.Nodes)
                    {
                        if (tn.Text == currentBedroomno)
                        {
                            tn.Remove();
                        }
                    }
                }
            }
        }

        private void tsmCopyToAdd_Click(object sender, EventArgs e)
        {
            if (this.fpSpread1.Sheets[0].ActiveRowIndex < 0)
                return;
            CopyInfo();

        }
    }
}
