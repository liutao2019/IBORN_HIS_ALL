using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Speciment.OutStore
{
    public partial class ucOut : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        private FarPoint.Win.Spread.SheetView sheetView = new FarPoint.Win.Spread.SheetView();
        private DataTable curDt = new DataTable();
        private List<OutInfo> outList = new List<OutInfo>();
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        private FS.HISFC.BizLogic.Speciment.SpecApplyOutManage appMgr = new FS.HISFC.BizLogic.Speciment.SpecApplyOutManage();
        private FS.HISFC.BizLogic.Speciment.SubSpecManage subMgr = new FS.HISFC.BizLogic.Speciment.SubSpecManage();
        private FS.HISFC.BizLogic.Speciment.ApplyTableManage applyTableManage = new FS.HISFC.BizLogic.Speciment.ApplyTableManage();
        //�걾�������ڵ���
        private int barCodeCol = -1;

        private string specBar = string.Empty;

        private ArrayList appTab = new ArrayList();
        private FS.HISFC.Models.Speciment.ApplyTable curApplyTable = new FS.HISFC.Models.Speciment.ApplyTable();

        /// <summary>
        /// ��ǰ����Ա
        /// </summary>
        private FS.HISFC.Models.Base.Employee loginPerson = new FS.HISFC.Models.Base.Employee();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        // <summary>
        /// ��ǰ������Ա�б�
        /// </summary>
        private ArrayList alCurDeptEmpl = new ArrayList();

        public ucOut()
        {
            InitializeComponent();
        }

        private void OpenFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            txtFileName.Text = ofd.FileName;
        }

        private void OpenExcel()
        {
            FS.HISFC.BizLogic.Speciment.ExlToDb2Manage exlMgr = new FS.HISFC.BizLogic.Speciment.ExlToDb2Manage();
            DataSet ds = new DataSet();
            try
            {
                exlMgr.ExlConnect(txtFileName.Text, ref ds);
                if (ds == null || ds.Tables.Count <= 0)
                {
                    return;
                }
                curDt = ds.Tables[0];
            }
            catch
            {
                MessageBox.Show("���ļ�ʧ�ܣ�");
            }
        }

        private void ReadData()
        {
            try
            {
                sheetView.RowCount=0;
                sheetView.ColumnCount=0;
                this.sheetView.ColumnCount = curDt.Columns.Count + 4;
                this.sheetView.ColumnHeader.Rows[0].Height = 40;

                FarPoint.Win.Spread.CellType.CheckBoxCellType chkType0 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                FarPoint.Win.Spread.CellType.CheckBoxCellType chkType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                chkType1.TextFalse = "���黹";
                chkType1.TextIndeterminate = "��γ���";
                chkType1.TextTrue = "�黹";
                chkType1.ThreeState = true;

                sheetView.Columns[0].CellType = chkType0;
                sheetView.Columns[1].CellType = chkType1;

                sheetView.Columns[0].Label = "ѡ��";
                sheetView.Columns[1].Label = "�黹";
                sheetView.Columns[2].Label = "����";
                sheetView.Columns[3].Label = "����";

                for (int i = 0; i < curDt.Columns.Count; i++)
                {
                    sheetView.Columns[i + 4].Label = curDt.Columns[i].ColumnName;
                    if (sheetView.Columns[i + 4].Label == "�걾����")
                    {
                        barCodeCol = i + 4;
                    }
                }
                for (int jk = 0; jk < this.sheetView.Columns.Count; jk++)
                {
                    sheetView.Columns[jk].Width = 65;
                    if ((jk == 0) || (jk == 3) || (jk == 10) || (jk == 15) || (jk == 16))
                    {
                        sheetView.Columns[jk].Width = 50;
                    }
                    if (sheetView.Columns[jk].Label == "�걾��λ��")
                    {
                        sheetView.Columns[jk].Width = 255;
                    }
                    if (sheetView.Columns[jk].Label == "�걾����")
                    {
                        sheetView.Columns[jk].Width = 110;
                    }
                }
                for (int i = 0; i < curDt.Rows.Count; i++)
                {
                    sheetView.Rows.Add(i, 1);
                    for (int j = 0; j < curDt.Columns.Count; j++)
                    {
                        sheetView.Cells[i, j + 4].Value = curDt.Rows[i][j];
                        if (j == 10)
                        {
                            sheetView.Cells[i, j + 4].Tag = curDt.Rows[i][j];
                        }
                    }
                }
                sheetView.Columns[2].Visible = false;
                this.SetEditMode();
            }
            catch
            {
                MessageBox.Show("��ȡ�ļ�ʧ�ܣ�");
            }
        }

        /// <summary>
        /// ��ȡҪ����ı걾
        /// </summary>
        private int GetOutData()
        {
            if (barCodeCol == -1)
            {
                MessageBox.Show("�뱣֤Excel���� '�걾����' ��");
                return -1;
            }

            for (int i = 0; i < sheetView.RowCount; i++)
            {
                string selected = "";
                try
                {
                    selected = sheetView.Cells[i, 0].Value.ToString();
                }
                catch
                {
                    continue;
                }
                if (selected == "1" || selected.ToUpper() == "TRUE")
                {
                    OutInfo o = new OutInfo();

                    decimal cnt = 0.0m;

                    try
                    {
                        cnt = Convert.ToDecimal( sheetView.Cells[i, 3].Value);
                    }
                    catch 
                    { }
                    o.Count = cnt;
                    o.SpecBarCode = sheetView.Cells[i, barCodeCol].Value.ToString();
                    #region �������ڴ�ӡ
                    o.SpecId = sheetView.Cells[i, 5].Value.ToString();
                    if (o.SpecBarCode.Length == 13)
                    {
                        o.SubQly = o.SpecBarCode.Substring(11, 1);
                    }
                    o.SubType = sheetView.Cells[i, 9].Value.ToString();
                    o.SubDis = sheetView.Cells[i, 10].Value.ToString();
                    o.SubCol = sheetView.Cells[i, 16].Value.ToString();
                    o.SubRow = sheetView.Cells[i, 15].Value.ToString();
                    o.Position = sheetView.Cells[i, 14].Value.ToString();
                    #endregion
                    string isReturn = "0";
                    try
                    {
                        if (sheetView.Cells[i, 1].Value == null)
                        {
                            isReturn = "0";
                        }
                        else
                        {
                            isReturn = sheetView.Cells[i, 1].Value.ToString();
                        } 
                    }
                    catch
                    { }
                    o.ReturnAble = isReturn;
                    outList.Add(o);
                }
            }
            return 0;
        }

        private void SetEditMode()
        {
            if (this.sheetView.RowCount == 0)
            {
                return;
            }
            else
            {
                sheetView.Columns.Get(barCodeCol).Locked = true;
                sheetView.Columns.Get(4).Locked = true;
                sheetView.Columns.Get(5).Locked = true;
                sheetView.Columns.Get(6).Locked = true;
                sheetView.Columns.Get(7).Locked = true;
                sheetView.Columns.Get(8).Locked = true;
                sheetView.Columns.Get(10).Locked = true;
                sheetView.Columns.Get(11).Locked = true;
                sheetView.Columns.Get(12).Locked = true;
                sheetView.Columns.Get(13).Locked = true;
                sheetView.Columns.Get(14).Locked = true;
                sheetView.Columns.Get(15).Locked = true;
                sheetView.Columns.Get(16).Locked = true;
                //sheetView.Columns.Get(17).Locked = true;
            }
            for (int k = 0; k <= sheetView.Columns.Count - 1; k++)
            {
                if (k != 0)
                {
                    sheetView.Columns[k].AllowAutoFilter = true;
                    sheetView.Columns[k].AllowAutoSort = true;
                }
            }
        }

        private void Out(bool direct)
        {
            //��ֱ�ӳ��ⲽ�裬���뵥��һ��Ҫ����
            //1. ����ApplyOut���У����뵥��
            //�����������һ�����뵥��

            try
            {
                outList = new List<OutInfo>();
                GetOutData();
            }
            catch
            {
                MessageBox.Show("��ȡ����ʧ��!");
                return;
            }
            if (txtApplyNum.Text.Trim() == "")
            {
               DialogResult dr = MessageBox.Show("�����Ϊ�գ�ȷ������д�������?", "�����", MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2);
               if (dr == DialogResult.No)
               {
                   return;
               }
            }

            int tmp = 0;
            if(txtApplyNum.Text.Trim()!="")
            {
                try
                {
                    tmp = Convert.ToInt32(txtApplyNum.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("���뵥�ű���������,��������д");
                    return;
                }
            }

            SpecOutOper outOper = new SpecOutOper(loginPerson);
            outOper.IsDirect = direct;
            FS.HISFC.BizLogic.Speciment.UserApplyManage userApplyManage = new FS.HISFC.BizLogic.Speciment.UserApplyManage();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                outOper.ApplyNum = tmp.ToString();
                outOper.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                userApplyManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (txtApplyNum.Text.Trim() != "")
                {
                    outOper.ApplyNum = txtApplyNum.Text.Trim();
                }
                else
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���뵥�Ų���Ϊ�գ�");
                    return;
                }

                #region �������뵥״̬������״̬�ж�
                if (this.curApplyTable == null)
                {
                    this.curApplyTable = applyTableManage.QueryApplyByID(outOper.ApplyNum);
                }
                else
                {
                    //�������ͬ���϶��޸Ĺ�����ţ�����ȡ����
                    if ((this.curApplyTable.ApplyId == 0) || (this.curApplyTable.ApplyId.ToString() != outOper.ApplyNum))
                    {
                        this.curApplyTable = applyTableManage.QueryApplyByID(outOper.ApplyNum);
                    }
                }
                if (this.curApplyTable == null)
                {
                    MessageBox.Show("û�ж�Ӧ�����뵥�ţ����ѯ��");
                    return;
                }
                else
                {
                    if (this.curApplyTable.ApplyId == 0)
                    {
                        MessageBox.Show("û�ж�Ӧ�����뵥�ţ����ѯ��");
                        return;
                    }
                    else
                    {
                        if (this.curApplyTable.User03 == "�ѳ���")
                        {
                            MessageBox.Show("�����뵥�걾�ѳ��⣬����ϸ�˶ԣ�");
                            return;
                        }

                    }
                }
                #endregion

                #region ���ӳ���˵��
                //�������˵����Ϊ���ڱ���ʱ�򼴿��Ա����ڶ�Ӧ�����뵥��Ϣ�ı�ע��SPEC_APPLICATIONTABLE.COMMENT
                string strCmt = "��";
                FS.FrameWork.Models.NeuObject strOutInfo = new FS.FrameWork.Models.NeuObject();
                if (!string.IsNullOrEmpty(this.txtOtherDemand.Text.Trim()))
                {
                    strCmt = this.txtOtherDemand.Text.Trim();
                }
                if (!string.IsNullOrEmpty(this.tbOutInfo.Text.Trim()))
                {
                    strOutInfo.Memo = this.tbOutInfo.Text.Trim();
                }
                if (!string.IsNullOrEmpty(this.cmbOutputOperName.Text.Trim()))
                {
                    strOutInfo.Name = this.cmbOutputOperName.Text.Trim();
                }
                string tmpSql = @"update SPEC_APPLICATIONTABLE set COMMENT = '{0}',OUTPUTRESULT = '{1}',IMPLEMENTNAME = '{2}'
                                        where APPLICATIONID = {3}";
                tmpSql = string.Format(tmpSql, strCmt, strOutInfo.Memo, strOutInfo.Name, tmp.ToString());
                if (outOper.UpdateSpecOut(tmpSql) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���³���˵��ʧ�ܣ�");
                    return;
                }
                #endregion

                #region �����������
                FS.HISFC.Models.Speciment.UserApply userApply = new FS.HISFC.Models.Speciment.UserApply();
                userApply.ApplyId = tmp;
                userApply.UserId = loginPerson.ID.ToString();
                string rbtChd = "�ѳ���";
                userApply.Schedule = rbtChd;
                userApply.ScheduleId = "OT";
                userApply.CurDate = System.DateTime.Now;
                userApply.OperId = loginPerson.ID;
                userApply.OperName = loginPerson.Name;
                int result = -1;
                string sequence = "";
                userApplyManage.GetNextSequence(ref sequence);
                userApply.UserAppId = Convert.ToInt32(sequence);
                result = userApplyManage.InsertUserApply(userApply);

                if (result == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("������ȱ�ʧ��!", userApply.Schedule);
                    return;
                }
                #endregion

                if (outOper.SpecOut(outList) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ʧ�ܣ�");
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                this.PrintPreOut("OUT");
                this.sheetView.RowCount = 0;
                MessageBox.Show("����ɹ���");
                //outOper.PrintOutSpec();
                return;
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("����ʧ�ܣ�");
                return;
            }
        } 

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                sheetView.RowCount = 0;
                this.OpenFile();
                this.OpenExcel();
                this.ReadData();
                if (barCodeCol == -1)
                {
                    MessageBox.Show("�뱣֤Excel���� '�걾����' ��");
                    
                }
            }
            catch
            { }
        }

        protected override void OnLoad(EventArgs e)
        {
            FarPoint.Win.Spread.FpSpread spread = new FarPoint.Win.Spread.FpSpread();
            spread.Sheets.Add(sheetView);
            panel3.Controls.Add(spread);
            spread.Dock = DockStyle.Fill;

            loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

            this.alCurDeptEmpl = this.managerIntegrate.QueryEmployeeByDeptID(loginPerson.Dept.ID);
            if (this.alCurDeptEmpl != null)
            {
                if (this.alCurDeptEmpl.Count > 0)
                {
                    this.cmbOutputOperName.AddItems(this.alCurDeptEmpl);
                }
            }
            
            base.OnLoad(e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryByApplyID();
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveChoosedSub();
            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// ���ⵥ��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            this.PrintPreOut("OUT");
            return base.OnPrint(sender, neuObject);
        }

        /// <summary>
        /// ��ӡ���ⵥ��
        /// </summary>
        /// <param name="type">��������</param>
        private void PrintPreOut(string type)
        {
            //�ȱ�Ϊ�գ���ֹ�ظ�����
            try
            {
                outList = new List<OutInfo>();
                GetOutData();
            }
            catch
            {
                MessageBox.Show("��ȡ����ʧ��!");
                return;
            }
            string applyID = this.txtApplyNum.Text.Trim();
            if (string.IsNullOrEmpty(applyID))
            {
                MessageBox.Show("����������ź��ѯ��");
                return;
            }

            if (this.curApplyTable == null)
            {
                this.curApplyTable = applyTableManage.QueryApplyByID(applyID);
            }
            else
            {
                //�������ͬ���϶��޸Ĺ�����ţ�����ȡ����
                if ((this.curApplyTable.ApplyId == 0) || (this.curApplyTable.ApplyId.ToString() != applyID))
                {
                    this.curApplyTable = applyTableManage.QueryApplyByID(applyID);
                }
            }
            FS.HISFC.Components.Speciment.Print.ucPreOutBillPrint ucPOBill = new FS.HISFC.Components.Speciment.Print.ucPreOutBillPrint();
            if (!string.IsNullOrEmpty(this.txtOtherDemand.Text.Trim()))
            {
                this.curApplyTable.Comment = this.txtOtherDemand.Text.Trim();
            }
            if (this.curApplyTable == null)
            {
                MessageBox.Show("��ǰ����Ų����ڣ�");
                return;
            }
            else
            {
                ucPOBill.AppTab = curApplyTable;
            }
            if (this.outList.Count <= 0)
            {
                MessageBox.Show("��ѡ����Ҫ��ӡ����[��ѡȫѡ��ѡ�ж�Ӧ��]��");
                return;
            }
            ucPOBill.OutType = type;
            ucPOBill.AlPreOutList = this.outList;
            ucPOBill.AddData();
            ucPOBill.Print();
        }

        /// <summary>
        /// �����Ѵ�걾
        /// </summary>
        private void SaveChoosedSub()
        {
            if (sheetView.RowCount == 0)
            {
                MessageBox.Show("û���κα걾�����ѯ���ݻ��ߵ���걾��","����ʧ��");
                return;
            }
            else
            {
                //FS.HISFC.Object.Base.Employee loginPerson = FS.NFC.Management.Connection.Operator as FS.HISFC.Object.Base.Employee;
                int tmp = 0;
                if (txtApplyNum.Text.Trim() != "")
                {
                    try
                    {
                        tmp = Convert.ToInt32(txtApplyNum.Text.Trim());
                    }
                    catch
                    {
                        MessageBox.Show("���뵥�ű���������,��������д", "����ʧ��");
                        return;
                    }
                }
                bool isDifferent = false;
                if (this.curApplyTable == null)
                {
                    this.curApplyTable = applyTableManage.QueryApplyByID(tmp.ToString());
                }
                else
                {
                    //�������ͬ���϶��޸Ĺ�����ţ�����ȡ����
                    if (this.curApplyTable.ApplyId == 0)
                    {
                        this.curApplyTable = applyTableManage.QueryApplyByID(tmp.ToString());
                        isDifferent = false;
                    }
                    else if (this.curApplyTable.ApplyId != tmp)
                    {
                        this.curApplyTable = applyTableManage.QueryApplyByID(tmp.ToString());
                        isDifferent = true;
                    }
                    else
                    {

                    }
                }
                SpecOutOper outOper = new SpecOutOper(loginPerson);
                outOper.IsDirect = false;
                FS.HISFC.BizLogic.Speciment.UserApplyManage userApplyManage = new FS.HISFC.BizLogic.Speciment.UserApplyManage();
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                try
                {
                    outOper.ApplyNum = tmp.ToString();
                    outOper.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    userApplyManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    string strCmt = "��";
                    FS.FrameWork.Models.NeuObject strOutInfo = new FS.FrameWork.Models.NeuObject();
                    string tmpSql = string.Empty;

                    if (this.curApplyTable != null)
                    {
                        if (this.curApplyTable.ImpProcess.StartsWith("O"))
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("���뵥��Ϊ[" + tmp.ToString() + "]�����뵥�Ѿ����������ٴ��޸ģ�");
                            return;
                        }
                        if (this.curApplyTable.User03 == "�ѳ���")
                        {
                            #region ���ӳ���˵��
                            //�������˵����Ϊ���ڱ���ʱ�򼴿��Ա����ڶ�Ӧ�����뵥��Ϣ�ı�ע��SPEC_APPLICATIONTABLE.COMMENT
                            
                            if (!string.IsNullOrEmpty(this.txtOtherDemand.Text.Trim()))
                            {
                                strCmt = this.txtOtherDemand.Text.Trim();
                            }
                            if (!string.IsNullOrEmpty(this.tbOutInfo.Text.Trim()))
                            {
                                strOutInfo.Memo = this.tbOutInfo.Text.Trim();
                            }
                            if (!string.IsNullOrEmpty(this.cmbOutputOperName.Text.Trim()))
                            {
                                strOutInfo.Name = this.cmbOutputOperName.Text.Trim();
                            }
                            if (!string.IsNullOrEmpty(strCmt) || !string.IsNullOrEmpty(strOutInfo.Memo) || !string.IsNullOrEmpty(strOutInfo.Name))
                            {
                                tmpSql = @"update SPEC_APPLICATIONTABLE set COMMENT = '{0}',OUTPUTRESULT = '{1}',IMPLEMENTNAME = '{2}'
                                        where APPLICATIONID = {3}";
                                tmpSql = string.Format(tmpSql, strCmt, strOutInfo.Memo, strOutInfo.Name, tmp.ToString());
                                if (outOper.UpdateSpecOut(tmpSql) < 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("���³���˵��ʧ�ܣ�");
                                    return;
                                }
                                else
                                {
                                    FS.FrameWork.Management.PublicTrans.Commit();
                                    MessageBox.Show("���³�������ɹ���");
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("���뵥��Ϊ[" + tmp.ToString() + "]�����뵥�Ѿ����ⲻ���ٴ��޸�������Ϣ��");
                                return;
                            }
                            #endregion
                        }
                    }

                    #region ���ӳ���˵��
                    //�������˵����Ϊ���ڱ���ʱ�򼴿��Ա����ڶ�Ӧ�����뵥��Ϣ�ı�ע��SPEC_APPLICATIONTABLE.COMMENT
                    //string strCmt = "��";
                    //FS.NFC.Object.NeuObject strOutInfo = new FS.NFC.Object.NeuObject();
                    if (!string.IsNullOrEmpty(this.txtOtherDemand.Text.Trim()))
                    {
                        strCmt = this.txtOtherDemand.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(this.tbOutInfo.Text.Trim()))
                    {
                        strOutInfo.Memo = this.tbOutInfo.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(this.cmbOutputOperName.Text.Trim()))
                    {
                        strOutInfo.Name = this.cmbOutputOperName.Text.Trim();
                    }
                    tmpSql = @"update SPEC_APPLICATIONTABLE set COMMENT = '{0}',OUTPUTRESULT = '{1}',IMPLEMENTNAME = '{2}'
                                        where APPLICATIONID = {3}";
                    tmpSql = string.Format(tmpSql, strCmt, strOutInfo.Memo, strOutInfo.Name, tmp.ToString());
                    if (outOper.UpdateSpecOut(tmpSql) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���³���˵��ʧ�ܣ�");
                        return;
                    }
                    #endregion

                    if ((this.appTab == null) || (this.appTab.Count == 0))
                    {
                        appTab = outOper.GetSubSpecOut(tmp.ToString());
                    }
                    else
                    {
                        if (((FS.HISFC.Models.Speciment.SpecOut)appTab[0]).RelateId != tmp)
                        {
                            appTab = outOper.GetSubSpecOut(tmp.ToString());
                        }
                    }
                    string rbtChd = "";
                    if ((appTab != null) && (appTab.Count > 0))
                    {
                        //�ж��Ƿ�Ϊ׷�����ݣ�specOut.Oper="Imp"����״̬��,���Ǹ������ݣ�ԭspecOut.Oper="Del"ɾ��״̬,������specOut.Oper="Merge"�ϲ�״̬��
                        if (rbtEnd.Checked)
                        {
                            string updSql = @"update SPEC_APPLY_OUT set OPER = 'Del' 
                                          where RELATEID = {0}";
                            updSql = string.Format(updSql, tmp.ToString());
                            if (outOper.UpdateSpecOut(updSql) < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("����ԭ����������ʧ�ܣ�");
                                return;
                            }
                            rbtChd = "����ɸѡ";
                            outOper.Oper = "Merge";
                        }
                        if (rbtFirst.Checked)
                        {
                            rbtChd = "׷��ɸѡ";
                            outOper.Oper = "Imp";
                        }

//                        outOper.Oper = "Merge";
//                        string updSql = @"update SPEC_APPLY_OUT set OPER = 'Del' 
//                                          where RELATEID = {0}";
//                        updSql = string.Format(updSql, tmp.ToString());
//                        if (outOper.UpdateSpecOut(updSql) < 0)
//                        {
//                            FS.FrameWork.Management.PublicTrans.RollBack();
//                            MessageBox.Show("����ԭ����������ʧ�ܣ�");
//                            return;
//                        }
                    }
                    else
                    {
                        outOper.Oper = "Imp";
                    }
                    //ArrayList outSpec = this.appMgr.GetSubSpecOut(tmp.ToString());
                    for (int i = 0; i < curDt.Rows.Count; i++)
                    {
                        string specBarCode = sheetView.Cells[i, barCodeCol].Text.Trim();
                        if (!string.IsNullOrEmpty(specBarCode))
                        {
                            if (rbtFirst.Checked)
                            {
                                bool sign = false;
                                foreach (FS.HISFC.Models.Speciment.SpecOut outTmp in appTab)
                                {
                                    if (specBarCode == outTmp.SubSpecBarCode)
                                    {

                                        //FS.FrameWork.Management.PublicTrans.RollBack();
                                        //MessageBox.Show("׷�ӵı걾,�걾��Ϊ"+specBarCode+"�ı걾�Ѿ����ڣ�׷��ʧ�ܣ�");
                                        //return;
                                        sign = true;
                                        break;
                                    }
                                }
                                if (sign)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("׷�ӵı걾,�걾��Ϊ[" + specBarCode + "]�ı걾�Ѿ����ڣ�׷��ʧ�ܣ�");
                                    return;
                                }
                            }
                            FS.HISFC.Models.Speciment.SubSpec tmpSpec = new FS.HISFC.Models.Speciment.SubSpec();
                            if (outOper.GetSubSpecById(specBarCode, ref tmpSpec) < 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("���ݱ걾�����ȡ�걾����");
                                return;
                            }
                            tmpSpec.BoxBarCode = sheetView.Cells[i, 14].Tag.ToString();
                            if (tmpSpec != null)
                            {
                                //�ж��Ƿ�Ϊ�¼����ݣ�specOut.Oper="Imp"����״̬��,���Ǹ������ݣ�ԭspecOut.Oper="Del"ɾ��״̬,������specOut.Oper="Merge"�ϲ�״̬��

                                if (outOper.SaveApplyOutInfo(tmpSpec, 1, "2") <= 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("������ѡ�걾����");
                                    return;
                                }
                            }
                            else
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("���ݱ걾�����ȡ�걾���������Ϊ[" + specBarCode + "]");
                                return;
                            }
                        }
                    }

                    FS.HISFC.Models.Speciment.UserApply userApply = new FS.HISFC.Models.Speciment.UserApply();
                    userApply.ApplyId = tmp;
                    userApply.UserId = loginPerson.ID.ToString();
                    //string rbtChd = "��ɸ";
                    userApply.Schedule = rbtChd;
                    if (rbtFirst.Checked)
                    {
                        userApply.ScheduleId = "Q4";
                    }
                    else
                    {
                        userApply.ScheduleId = "Q5";
                    }
                    userApply.CurDate = System.DateTime.Now;
                    userApply.OperId = loginPerson.ID;
                    userApply.OperName = loginPerson.Name;
                    int result = -1;
                    string sequence = "";
                    userApplyManage.GetNextSequence(ref sequence);
                    userApply.UserAppId = Convert.ToInt32(sequence);
                    result = userApplyManage.InsertUserApply(userApply);

                    if (result == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("������ȱ�ʧ��!", userApply.Schedule);
                        return;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show("�ɹ�������ѡ�걾��");
                }
                catch
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("������ѡ�걾�쳣����");
                    return;
                }
            }
        }

        /// <summary>
        /// ��������ID��ѯ�����������
        /// </summary>
        private void QueryByApplyID()
        {
            string applyID = this.txtApplyNum.Text.Trim();
            if (string.IsNullOrEmpty(applyID))
            {
                MessageBox.Show("����������ź��ѯ��");
                return;
            }
            else
            {
                if (this.sheetView.RowCount > 0)
                {
                    DialogResult result = MessageBox.Show("��ѯ���ݽ������ǰ�������Ƿ������", "��ʾ", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }
                if (this.curApplyTable == null)
                {
                    this.curApplyTable = applyTableManage.QueryApplyByID(applyID);
                }
                else
                {
                    if (this.curApplyTable.ApplyId == 0)
                    {
                        this.curApplyTable = applyTableManage.QueryApplyByID(applyID);
                    }
                    else if (this.curApplyTable.ApplyId.ToString() != applyID) //�µ������
                    {
                        this.curApplyTable = applyTableManage.QueryApplyByID(applyID);
                    }
                }
                if (this.curApplyTable == null)
                {
                    MessageBox.Show("δ�ҵ������뵥�Ķ�Ӧ�걾��");
                    return;
                }
                else
                {
                    if (this.curApplyTable.ApplyId == 0)
                    {
                        MessageBox.Show("δ�ҵ������뵥�Ķ�Ӧ�걾��");
                        return;
                    }
                    else
                    {
                        this.lbStatus.Text = this.curApplyTable.User03;
                    }
                }
                //����������
                if (!string.IsNullOrEmpty(this.curApplyTable.OutPutResult))
                {
                    this.tbOutInfo.Text = this.curApplyTable.OutPutResult;
                }
                //����ִ����
                if (!string.IsNullOrEmpty(this.curApplyTable.ImpDocId))
                {
                    this.cmbOutputOperName.Text = this.curApplyTable.ImpDocId;
                }
                //����˵��
                if (!string.IsNullOrEmpty(this.curApplyTable.Comment))
                {
                    this.txtOtherDemand.Text = this.curApplyTable.Comment;
                }
                Hashtable hsOutNums = new Hashtable();
                bool isAddNums = false;
                if (this.curApplyTable.User03 == "�ѳ���")
                {
                    isAddNums = true;
                }
                this.appTab = this.appMgr.GetSubSpecOut(applyID);
                if ((appTab != null) && (appTab.Count > 0))
                {
                    string alBar = string.Empty;
                    foreach (FS.HISFC.Models.Speciment.SpecOut spOut in appTab)
                    {
                        if ((isAddNums) && (!hsOutNums.Contains(spOut.SubSpecBarCode)))
                        {
                            hsOutNums.Add(spOut.SubSpecBarCode, spOut);
                        }
                        if (string.IsNullOrEmpty(alBar))
                        {
                            alBar = "'" + spOut.SubSpecBarCode + "'";
                        }
                        else
                        {
                            alBar = alBar + "," + "'" + spOut.SubSpecBarCode + "'";
                        }
                    }
                    if (!string.IsNullOrEmpty(alBar))
                    {
                        this.specBar = alBar;
                    }
                    string curSql = string.Empty;
                    if (this.curApplyTable.User03 == "�ѳ���")
                    {
                        curSql = this.GetSql() + " where ss.SUBBARCODE in ({0})" + " \n ) select * from t";
                    }
                    else
                    {
                        curSql = this.GetSql() + " where ss.SUBBARCODE in ({0})" + " and ss.STATUS in ('1','3')\n ) select * from t";
                    }
                    curSql = string.Format(curSql, specBar);
                    DataSet ds = new DataSet();
                    if (this.applyTableManage.appExecQuery(curSql, ref ds) >= 0)
                    {
                        curDt = ds.Tables[0];
                        this.ReadData();
                        for (int j = 0; j < curDt.Rows.Count; j++)
                        {
                            string boxBarCode = sheetView.Cells[j, 14].Text;
                            string strBar = sheetView.Cells[j, barCodeCol].Value.ToString();
                            if (hsOutNums.ContainsKey(strBar))
                            {
                                sheetView.Cells[j, 3].Text = ((FS.HISFC.Models.Speciment.SpecOut)hsOutNums[strBar]).Count.ToString();
                                if (string.IsNullOrEmpty(boxBarCode))
                                {
                                    boxBarCode = ((FS.HISFC.Models.Speciment.SpecOut)hsOutNums[strBar]).BoxBarCode;
                                }
                                sheetView.Cells[j, 15].Text = ((FS.HISFC.Models.Speciment.SpecOut)hsOutNums[strBar]).BoxRow.ToString();
                                sheetView.Cells[j, 16].Text = ((FS.HISFC.Models.Speciment.SpecOut)hsOutNums[strBar]).BoxCol.ToString();
                                sheetView.Cells[j, 1].Value = (object)(Convert.ToInt32(((FS.HISFC.Models.Speciment.SpecOut)hsOutNums[strBar]).IsRetuanAble));
                            }
                            try
                            {
                                string loc = ParseLocation.ParseSpecBox(boxBarCode);
                                sheetView.Cells[j, 14].Text = loc;
                                sheetView.Cells[j, 14].Tag = boxBarCode;
                            }
                            catch
                            {
                            }
                        }
                        lbStatus.Text += lbStatus.Text + "  ����" + curDt.Rows.Count.ToString() + "����¼";
                    }
                }
            }
        }

        /// <summary>
        /// ��ϸ�Ĳ�ѯ����
        /// </summary>
        /// <returns></returns>
        private string GetSql()
        {
            #region
            /*return @"
                         with t as
                         (
                         SELECT DISTINCT 
                         s.SPECID �걾ԴID,
                         s.HISBARCODE Դ����, 
                         s.OPEREMP ������,
                         s.COMMENT ��ע,
                         ( case s.MATCHFLAG when '1' then '��' else '��'end) ���, p.name ����, 
                         s.SPEC_NO �걾��,
  						 p.CARD_NO ������,
                         ss.SUBBARCODE �걾����,
                         d.DISEASENAME ����, 
                         s.OPERTIME ����ʱ��, 
						(case s.ISHIS when 'O' then s.SENDDOCID else	(SELECT e.EMPL_NAME FROM COM_EMPLOYEE e WHERE e.EMPL_CODE =  s.SENDDOCID) end) �ʹ�ҽ��,
                        (case s.ISHIS when 'O' then s.DEPTNO else (SELECT cd.DEPT_NAME FROM COM_DEPARTMENT cd WHERE cd.DEPT_CODE = s.DEPTNO FETCH FIRST 1 ROWS ONLY) end)����,
                        (case s.ORGORBLOOD when 'O' then st.TUMORPOS else '' end) ȡ������,
						(case st.TUMORTYPE when '1' then '����' when '2' then '����' when '3' then '����' when '4' then '����' when '5' then '��˨' when '8' then '�ܰͽ�' else '' end) ��������,
						(case ss.STATUS when '1' then '�ڿ�' when '2' then '���' when '3' then '�ѻ�' when '4' then '����' else '' end) �ڿ�״̬,
						(case st.TUMORPOR when '1' then 'ԭ��' when '2' then '����' when '3' then 'ת��' when '13' then 'ԭ����ת��' when '23' then '������ת��' else '' end) �걾����,
						ss.LASTRETURNTIME �ϴη���ʱ��, 
                         ss.DATERETURNTIME Լ������ʱ��, 
                         st.TRANSPOS ת�Ʋ�λ, 
                         st.CAPACITY �걾����,
						st.BAOMOENTIRE ��������, 
                         st.COMMENT �걾��ϸ����, 
                         p.IC_CARDNO ���ƿ���,
                         s.INPATIENT_NO סԺ��ˮ��,
						s.GETPEORID ��ǰ���ƽ׶�, 
                         s.OPERPOSNAME ��������, 
                         sd.MAIN_DIANAME �����,
                         sd.MAIN_DIACODE �������̬��,
						sd.MAIN_DIANAME1 ���1, 
                         sd.MAIN_DIACODE1 ���1��̬��,
                         sd.MAIN_DIANAME2 ���2, 
                         sd.MAIN_DIACODE2 ���2��̬��,
						sd.COMMENT ��ϱ�ע, 
                         (case p.GENDER when 'F' then 'Ů' else '��' end) �Ա�,
						(select name  from COM_DICTIONARY where type = 'COUNTRY' and code = p.NATIONALITY fetch first 1 rows only)����,
            			(select name  from COM_DICTIONARY where type = 'NATION' and code = p.NATION fetch first 1 rows only) ����,
            			p.ADDRESS סַ,
                         (select name from COM_DICTIONARY where type = 'MaritalStatus' and code = p.ISMARR fetch first 1 rows only)����״��,
           				p.BIRTHDAY ����, 
                         p.CONTACTNUM ��ϵ�绰, 
                         p.HOMEPHONENUM ��ͥ�绰, 
                         b.BOXBARCODE �걾��λ��, 
                         ss.BOXENDROW ��,ss.BOXENDCOL ��,
						 t.SPECIMENTNAME �걾����,
                         st.SPECTYPEID
                       from SPEC_SOURCE s join SPEC_SOURCE_STORE st on s.SPECID = st.SPECID 
                       join SPEC_DISEASETYPE d on d.DISEASETYPEID = s.DISEASETYPEID
                       join SPEC_TYPE t ON st.SPECTYPEID = t.SPECIMENTTYPEID
                       join SPEC_PATIENT p on p.PATIENTID = s.PATIENTID
					   join SPEC_SUBSPEC ss on s.SPECID = ss.SPECID and st.SOTREID = ss.STOREID
                       left join SPEC_DIAGNOSE sd on sd.SPECID = s.SPECID
					   left join SPEC_BOX b on ss.BOXID = b.BOXID";*/
            return @"
                         with t as
                         (
                         SELECT DISTINCT
                         s.HISBARCODE Դ����,
                         s.SPEC_NO �걾��,
                         p.name ����,
  						 p.CARD_NO ������,
                         ss.SUBBARCODE �걾����,
                         t.SPECIMENTNAME �걾����,
                         d.DISEASENAME ����, 
						(case s.ISHIS when 'O' then s.SENDDOCID else	(SELECT e.EMPL_NAME FROM COM_EMPLOYEE e WHERE e.EMPL_CODE =  s.SENDDOCID) end) �ʹ�ҽ��, 
                         sd.MAIN_DIANAME �����,
                         sd.MAIN_DIACODE �������̬��, 
                         b.BOXBARCODE �걾��λ��, 
                         ss.BOXENDROW ��,
                        ss.BOXENDCOL ��
                       from SPEC_SOURCE s join SPEC_SOURCE_STORE st on s.SPECID = st.SPECID 
                       join SPEC_DISEASETYPE d on d.DISEASETYPEID = s.DISEASETYPEID
                       join SPEC_TYPE t ON st.SPECTYPEID = t.SPECIMENTTYPEID
                       join SPEC_PATIENT p on p.PATIENTID = s.PATIENTID
					   join SPEC_SUBSPEC ss on s.SPECID = ss.SPECID and st.SOTREID = ss.STOREID
                       left join SPEC_DIAGNOSE sd on sd.SPECID = s.SPECID
					   left join SPEC_BOX b on ss.BOXID = b.BOXID";
            #endregion
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("ֱ�ӳ���", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Zִ��, true, false, null);
            this.toolBarService.AddToolButton("����", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C��Ժ�Ǽ�, true, false, null);
            this.toolBarService.AddToolButton("��ӡԤ���ⵥ", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡԤ��, true, false, null);
            return this.toolBarService;
           // return base.OnInit(sender, neuObject, param);
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //ֱ�ӳ��ⲻ��Ҫ�ٴ�ȷ�ϣ���SPPEC_OUT �в����˼�¼�������⻹��Ҫȷ�ϳ���
            switch (e.ClickedItem.Text.Trim())
            {
                case "����":
                    DialogResult result = MessageBox.Show("ȷ������?", "����", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                    this.Out(false);
                    break;
                case "ֱ�ӳ���":
                    result = MessageBox.Show("ȷ������Ҫȷ�ϣ�ֱ�ӳ���?", "ֱ�ӳ���", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                    this.Out(true);
                    break;
                case "��ӡԤ���ⵥ":
                    this.PrintPreOut("PREOUT");
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked)
            {
                for (int i = 0; i < sheetView.Rows.Count; i++)
                {
                    
                        sheetView.Cells[i, 0].Value = (object)1;
                        //rowIndexList.Add(i);
                    
                }
            }
            else
            {
                for (int i = 0; i < sheetView.Rows.Count; i++)
                {
                    sheetView.Cells[i, 0].Value = (object)0;
                }
            }
        }

        /// <summary>
        /// ����Żس��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtApplyNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.QueryByApplyID();
            }
        }

        private void chkBack_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkBack.CheckState == CheckState.Checked)
            {
                chkBack.Text = "�黹";
                for (int i = 0; i < sheetView.Rows.Count; i++)
                {

                    sheetView.Cells[i, 1].Value = (object)(1);
                    //rowIndexList.Add(i);
                }
            }
            else if (chkBack.CheckState == CheckState.Unchecked)
            {
                chkBack.Text = "����";
                for (int i = 0; i < sheetView.Rows.Count; i++)
                {

                    sheetView.Cells[i, 1].Value = (object)(0);
                    //rowIndexList.Add(i);

                }
            }
            else
            {
                chkBack.Text = "��γ���";
                for (int i = 0; i < sheetView.Rows.Count; i++)
                {

                    sheetView.Cells[i, 1].Value = (object)(2);
                    //rowIndexList.Add(i);

                }
            }
        }

        private void nudCount_ValueChanged(object sender, EventArgs e)
        {
            decimal count = Convert.ToDecimal(nudCount.Value.ToString());
            if (count > 0.0M)
            {
                for (int i = 0; i < sheetView.Rows.Count; i++)
                {
                    string check = "";
                    check = sheetView.Cells[i, 0].Value == null ? "" : sheetView.Cells[i, 0].Value.ToString();
                    if (check == "1" || check.ToUpper() == "TRUE")
                        sheetView.Cells[i, 3].Text = count.ToString();
                }
            }
        }

        private void rbURt_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < sheetView.Rows.Count; i++)
            {
                sheetView.Cells[i, 1].Value = (object)(0);
            }
        }

        private void rbRt_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < sheetView.Rows.Count; i++)
            {
                sheetView.Cells[i, 1].Value = (object)(1);
            }
        }

        private void rbMt_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < sheetView.Rows.Count; i++)
            {
                sheetView.Cells[i, 1].Value = (object)(2);
            }
        }

        public override int Export(object sender, object neuObject)
        {
            if (this.sheetView.Rows.Count > 0)
            {
                FS.FrameWork.WinForms.Controls.NeuSpread ns = new FS.FrameWork.WinForms.Controls.NeuSpread();
               
                string path = string.Empty;
                SaveFileDialog saveFileDiaglog = new SaveFileDialog();

                saveFileDiaglog.Title = "��ѯ���������ѡ��Excel�ļ�����λ��";
                saveFileDiaglog.RestoreDirectory = true;
                saveFileDiaglog.InitialDirectory = Application.StartupPath;
                saveFileDiaglog.Filter = "Excel files (*.xls)|*.xls";
                saveFileDiaglog.FileName = DateTime.Now.ToShortDateString() + "�걾";
                DialogResult dr = saveFileDiaglog.ShowDialog();
                SpreadToExlHelp.ExportExl(sheetView, 16, new int[] { }, saveFileDiaglog.FileName, false);
            }
            return base.Export(sender, neuObject);
        }
    }
}
