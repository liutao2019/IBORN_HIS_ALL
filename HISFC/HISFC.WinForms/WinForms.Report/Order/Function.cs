using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace FS.WinForms.Report.Order
{
    class Function:FS.FrameWork.Management.Database 
    {
        #region ���ҽ�� ����Ķ���column �����Ŀ��
        public static void DrawCombo(object sender, int column, int DrawColumn, int ChildViewLevel)
        {
            switch (sender.GetType().ToString().Substring(sender.GetType().ToString().LastIndexOf(".") + 1))
            {
                case "SheetView":
                    FarPoint.Win.Spread.SheetView o = sender as FarPoint.Win.Spread.SheetView;
                    int i = 0;
                    string tmp = "", curComboNo = "";
                    if (ChildViewLevel == 0)
                    {
                        for (i = 0; i < o.RowCount; i++)
                        {
                            #region "��"
                            if (o.Cells[i, column].Text == "0") o.Cells[i, column].Text = "";
                            tmp = o.Cells[i, column].Text + "";
                            o.Cells[i, column].Tag = tmp;
                            if (curComboNo != tmp && tmp != "") //��ͷ
                            {
                                curComboNo = tmp;
                                o.Cells[i, DrawColumn].Text = "��";
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "��";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "";
                                    }
                                }
                                catch { }
                            }
                            else if (curComboNo == tmp && tmp != "")
                            {
                                o.Cells[i, DrawColumn].Text = "��";
                            }
                            else if (curComboNo != tmp && tmp == "")
                            {
                                try
                                {
                                    if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "��";
                                    }
                                    else if (o.Cells[i - 1, DrawColumn].Text == "��")
                                    {
                                        o.Cells[i - 1, DrawColumn].Text = "";
                                    }
                                }
                                catch { }
                                o.Cells[i, DrawColumn].Text = "";
                                curComboNo = "";
                            }
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "��") o.Cells[i, DrawColumn].Text = "��";
                            if (i == o.RowCount - 1 && o.Cells[i, DrawColumn].Text == "��") o.Cells[i, DrawColumn].Text = "";
                            //o.Cells[i, DrawColumn].ForeColor = System.Drawing.Color.Red;
                            #endregion
                        }
                    }
                    else if (ChildViewLevel == 1)
                    {
                        for (int m = 0; m < o.RowCount; m++)
                        {
                            FarPoint.Win.Spread.SheetView c = o.GetChildView(m, 0);
                            for (int j = 0; j < c.RowCount; j++)
                            {
                                #region "��"
                                if (c.Cells[j, column].Text == "0") c.Cells[j, column].Text = "";
                                tmp = c.Cells[j, column].Text + "";

                                c.Cells[j, column].Tag = tmp;
                                if (curComboNo != tmp && tmp != "") //��ͷ
                                {
                                    curComboNo = tmp;
                                    c.Cells[j, DrawColumn].Text = "��";
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "��";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "";
                                        }
                                    }
                                    catch { }
                                }
                                else if (curComboNo == tmp && tmp != "")
                                {
                                    c.Cells[j, DrawColumn].Text = "��";
                                }
                                else if (curComboNo != tmp && tmp == "")
                                {
                                    try
                                    {
                                        if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "��";
                                        }
                                        else if (c.Cells[j - 1, DrawColumn].Text == "��")
                                        {
                                            c.Cells[j - 1, DrawColumn].Text = "";
                                        }
                                    }
                                    catch { }
                                    c.Cells[j, DrawColumn].Text = "";
                                    curComboNo = "";
                                }
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "��") c.Cells[j, DrawColumn].Text = "��";
                                if (j == c.RowCount - 1 && c.Cells[j, DrawColumn].Text == "��") c.Cells[j, DrawColumn].Text = "";
                                //c.Cells[j, DrawColumn].ForeColor = System.Drawing.Color.Red;
                                #endregion

                            }
                        }
                    }
                    break;
                case "FS":
                    FSDataWindow.Controls.FSDataWindow obj = sender as FSDataWindow.Controls.FSDataWindow;
                    if (obj.RowCount < 1)//���û��ҽ������
                        return;
                    string curComboID = "";
                    string tmpComboID = obj.GetItemString(1, (short)column);
                    for (i = 2; i <= obj.RowCount; i++)
                    {
                        curComboID = obj.GetItemString(i, (short)column);
                        if (tmpComboID == curComboID)
                        {
                            //��Ϻ���ȣ������һ��û�б�־˵������ϵĵ�һ��
                            if (obj.IsItemNull(i - 1, (short)DrawColumn))
                            {
                                //��ϵ�һ����ֵ
                                obj.SetItemSqlString(i - 1, (short)DrawColumn, "��");
                                //��������һ��
                                if (i == obj.RowCount)
                                    obj.SetItemString(i, (short)DrawColumn, "��");
                                else
                                    obj.SetItemString(i, (short)DrawColumn, "��");//���ﲻ���Ƿ���һ�����һ�������һ������ϺŲ���ʱ������
                            }
                            else
                            {
                                //��������һ��
                                if (i == obj.RowCount)
                                    obj.SetItemString(i, (short)DrawColumn, "��");
                                else
                                    obj.SetItemString(i, (short)DrawColumn, "��");
                            }
                        }
                        else
                        {
                            //��ϺŲ��ȣ���ʱ��ı�����Ϻ����ʱ���õ�"��"����"��"��Ϊ"��"
                            if (!obj.IsItemNull(i - 1, (short)DrawColumn))
                            {
                                //����һ������һ������
                                if (obj.GetItemString(i - 1, (short)DrawColumn) == "��" || obj.GetItemString(i - 1, (short)DrawColumn) == "��")
                                    obj.SetItemString(i - 1, (short)DrawColumn, "��");
                            }
                        }
                        tmpComboID = curComboID;
                    }
                    break;
            }

        }
        /// <summary>
        /// ����Ϻ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="column"></param>
        /// 		/// <param name="DrawColumn"></param>
        public static void DrawCombo(object sender, int column, int DrawColumn)
        {
            DrawCombo(sender, column, DrawColumn, 0);
        }
       
        #endregion
        #region ҽ������ӡ
        #region ҽ������ӡ��ѯ

        /// <summary>
        /// ��ѯҽ������ӡ��ҽ��
        /// </summary>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <returns></returns>
        public ArrayList QueryPrnOrder(string inpatientNO)
        {

            string sql = "";
            ArrayList al = new ArrayList();
            //sql = OrderQuerySelect();
            //if (sql == null) return null;
            if (this.Sql.GetSql("Order.OrderPrn.QueryOrderByPatient", ref sql) == -1)
            {
                this.Err = "û���ҵ�Order.OrderPrn.QueryOrderByPatient�ֶ�!";
                return null;
            }
            sql = string.Format(sql, inpatientNO);
            return this.myOrderQuery(sql);
        }
        #endregion
        /// <summary>
        /// ��ѯ����ҽ��
        /// </summary>
        /// <param name="InPatientNO"></param>
        /// <returns></returns>
        public ArrayList QueryDcOrder(string InPatientNO)
        {
            #region ��ѯ����ҽ��
            //��ѯ����ҽ��
            //Order.Order.QueryOrder.1
            //���룺0 inpatientno
            //������ArrayList
            #endregion
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null) return null;
            if (this.Sql.GetSql("Order.Order.QueryOrder.OrderPrint", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrder.1�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            sql = sql + " " + string.Format(sql1, InPatientNO);
            return this.myOrderQuery(sql);
        }

        /// <summary>
        /// ҩƷ������Ϣ
        /// </summary>
        static FS.FrameWork.Public.ObjectHelper phaItemHelper;

        static FS.HISFC.Models.Pharmacy.Item itemObj = null;

        /// <summary>
        /// ��ȡҩƷ��Ϣ
        /// </summary>
        /// <returns></returns>
        public static FS.HISFC.Models.Pharmacy.Item GetPhaItem(string itemCode)
        {
            if (phaItemHelper == null || phaItemHelper.ArrayObject.Count == 0)
            {
                phaItemHelper = new FS.FrameWork.Public.ObjectHelper();
                FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();
                List<FS.HISFC.Models.Pharmacy.Item> alphaItems = itemMgr.QueryItemList();
                if (alphaItems != null)
                {
                    phaItemHelper.ArrayObject = new ArrayList(alphaItems);
                }
            }

            if (phaItemHelper == null)
            {
                return null;
            }

            try
            {
                itemObj = phaItemHelper.GetObjectFromID(itemCode) as FS.HISFC.Models.Pharmacy.Item;
            }
            catch
            {
                return null;
            }
            return itemObj;
        }

        /// <summary>
        /// ����״̬��ѯ
        /// </summary>
        /// <param name="InPatientNO"></param>
        /// <returns></returns>
        public ArrayList QueryDcOrder(string InPatientNO, string Status)
        {
            #region ��ѯ����ҽ��
            //��ѯ����ҽ��
            //Order.Order.QueryOrder.1
            //���룺0 inpatientno
            //������ArrayList
            #endregion
            string sql = "", sql1 = "";
            ArrayList al = new ArrayList();
            sql = OrderQuerySelect();
            if (sql == null) return null;
            if (this.Sql.GetSql("Order.Order.QueryOrder.OrderPrintGoOn", ref sql1) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrder.OrderPrintGoOn�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            sql = sql + " " + string.Format(sql1, InPatientNO, Status);
            return this.myOrderQuery(sql);
        }
        /// ��ѯ������Ϣ��select��䣨��where������
        private string OrderQuerySelect()
        {
            #region �ӿ�˵��
            //Order.Order.QueryOrder.Select.1
            //���룺0
            //������sql.select
            #endregion
            string sql = "";
            if (this.Sql.GetSql("Order.Order.QueryOrder.Select.New", ref sql) == -1)
            {
                this.Err = "û���ҵ�Order.Order.QueryOrder.Select.New�ֶ�!";
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            return sql;
        }

        /// <summary>
        /// ����ҽ��״̬
        /// Ϊ�Ѿ�ִ��
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateOrderStatus(string orderNo, string status)
        {
            string strSql = "";

            if (this.Sql.GetSql("Order.Update.OrderTerm.OrderPrintStatus", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, orderNo, status.ToString());
            }
            catch (Exception ex)
            {
                this.Err = "����������ԣ�Order.Update.OrderTerm.OrderPrintStatus" + ex.Message;
                this.WriteErr();
                return -1;
            }
            if (this.ExecNoQuery(strSql) <= 0) return -1;
            return 0;
        }
        //˽�к�������ѯҽ����Ϣ
        private ArrayList myOrderQuery(string SQLPatient)
        {

            ArrayList al = new ArrayList();

            if (this.ExecQuery(SQLPatient) == -1) return null;
            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Order.Inpatient.Order objOrder = new FS.HISFC.Models.Order.Inpatient.Order();
                    #region "������Ϣ"
                    //������Ϣ����  
                    //			1 סԺ��ˮ��   2סԺ������     3���߿���id      4���߻���id
                    try
                    {
                        objOrder.Patient.ID = this.Reader[1].ToString();
                        objOrder.Patient.PID.PatientNO = this.Reader[2].ToString();
                        objOrder.Patient.PVisit.PatientLocation.Dept.ID = this.Reader[3].ToString();
                        objOrder.InDept.ID = this.Reader[3].ToString();
                        objOrder.Patient.PVisit.PatientLocation.NurseCell.ID = this.Reader[4].ToString();

                    }
                    catch (Exception ex)
                    {
                        this.Err = "��û��߻�����Ϣ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    #endregion
                    #region "��Ŀ��Ϣ"
                    //ҽ��������Ϣ
                    // ������Ŀ��Ϣ
                    //	       5��Ŀ���      6��Ŀ����       7��Ŀ����      8��Ŀ������,    9��Ŀƴ���� 
                    //	       10��Ŀ������ 11��Ŀ�������  12ҩƷ���     13ҩƷ��������  14������λ       
                    //         15��С��λ     16��װ����,     17���ʹ���     18ҩƷ���  ,   19ҩƷ����
                    //         20���ۼ�       21�÷�����      22�÷�����     23�÷�Ӣ����д  24Ƶ�δ���  
                    //         25Ƶ������     26ÿ�μ���      27��Ŀ����     28�Ƽ۵�λ      29ʹ������			  
                    // �ж�ҩƷ/��ҩƷ
                    //         25Ƶ������     26ÿ�μ���      27��Ŀ����     28�Ƽ۵�λ      29ʹ������			  
                    // 73 �������� ����
                    if (this.Reader[5].ToString() == "1")//ҩƷ
                    {
                        FS.HISFC.Models.Pharmacy.Item objPharmacy = new FS.HISFC.Models.Pharmacy.Item();
                        try
                        {
                            objPharmacy.ID = this.Reader[6].ToString();
                            objPharmacy.Name = this.Reader[7].ToString();
                            objPharmacy.UserCode = this.Reader[8].ToString();
                            objPharmacy.SpellCode = this.Reader[9].ToString();
                            objPharmacy.SysClass.ID = this.Reader[10].ToString();
                            //objPharmacy.SysClass.Name = this.Reader[11].ToString();
                            objPharmacy.Specs = this.Reader[12].ToString();
                            //							try
                            //							{
                            if (!this.Reader.IsDBNull(13)) objPharmacy.BaseDose = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[13].ToString());
                            //}
                            //							catch{}
                            objPharmacy.DoseUnit = this.Reader[14].ToString();
                            objPharmacy.MinUnit = this.Reader[15].ToString();
                            //try
                            //{
                            if (!this.Reader.IsDBNull(16)) objPharmacy.PackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[16].ToString());
                            //}
                            //catch{}
                            objPharmacy.DosageForm.ID = this.Reader[17].ToString();
                            objPharmacy.Type.ID = this.Reader[18].ToString();
                            objPharmacy.Quality.ID = this.Reader[19].ToString();
                            //try
                            //{
                            if (!this.Reader.IsDBNull(20)) objPharmacy.PriceCollection.RetailPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20].ToString());
                            //}
                            //catch{}					
                            objOrder.Usage.ID = this.Reader[21].ToString();
                            objOrder.Usage.Name = this.Reader[22].ToString();
                            objOrder.Usage.Memo = this.Reader[23].ToString();
                        }
                        catch (Exception ex)
                        {
                            this.Err = "���ҽ����Ŀ��Ϣ����" + ex.Message;
                            this.WriteErr();
                            return null;
                        }
                        objOrder.Item = objPharmacy;
                    }
                    else if (this.Reader[5].ToString() == "2")//��ҩƷ
                    {
                        FS.HISFC.Models.Fee.Item.Undrug objAssets = new FS.HISFC.Models.Fee.Item.Undrug();
                        try
                        {
                            objAssets.ID = this.Reader[6].ToString();
                            objAssets.Name = this.Reader[7].ToString();
                            objAssets.UserCode = this.Reader[8].ToString();
                            objAssets.SpellCode = this.Reader[9].ToString();
                            objAssets.SysClass.ID = this.Reader[10].ToString();
                            //objAssets.SysClass.Name = this.Reader[11].ToString();
                            objAssets.Specs = this.Reader[12].ToString();
                            //							try
                            //							{
                            if (!this.Reader.IsDBNull(20)) objAssets.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20].ToString());
                            //}
                            //							catch{}	
                            objAssets.PriceUnit = this.Reader[28].ToString();
                            //������������
                            objOrder.Sample.Name = this.Reader[73].ToString();
                        }
                        catch (Exception ex)
                        {
                            this.Err = "���ҽ����Ŀ��Ϣ����" + ex.Message;
                            this.WriteErr();
                            return null;
                        }
                        objOrder.Item = objAssets;
                    }


                    objOrder.Frequency.ID = this.Reader[24].ToString();
                    objOrder.Frequency.Name = this.Reader[25].ToString();
                    //try
                    //{
                    if (!this.Reader.IsDBNull(26)) objOrder.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[26].ToString());//}
                    //catch{}
                    //try
                    //{
                    if (!this.Reader.IsDBNull(27)) objOrder.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[27].ToString());//}
                    //catch{}
                    objOrder.Unit = this.Reader[28].ToString();
                    //try
                    //{
                    if (!this.Reader.IsDBNull(29)) objOrder.HerbalQty = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[29].ToString());//}
                    //catch{}

                    #endregion
                    #region "ҽ������"
                    // ����ҽ������
                    //		   30ҽ�������� 31ҽ���������  32ҽ���Ƿ�ֽ�:1����/2��ʱ     33�Ƿ�Ʒ� 
                    //		   34ҩ���Ƿ���ҩ 35��ӡִ�е�    36�Ƿ���Ҫȷ��   
                    try
                    {
                        objOrder.ID = this.Reader[0].ToString();
                        objOrder.ExtendFlag1 = this.Reader[78].ToString();//��ʱҽ��ִ��ʱ�䣭���Զ���
                        objOrder.OrderType.ID = this.Reader[30].ToString();
                        objOrder.OrderType.Name = this.Reader[31].ToString();
                        //try
                        //{
                        if (!this.Reader.IsDBNull(32)) objOrder.OrderType.IsDecompose = System.Convert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[32].ToString()));//}
                        //catch{}
                        //try
                        //{
                        if (!this.Reader.IsDBNull(33)) objOrder.OrderType.IsCharge = System.Convert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[33].ToString()));//}
                        //catch{}
                        //try
                        //{
                        if (!this.Reader.IsDBNull(34)) objOrder.OrderType.IsNeedPharmacy = System.Convert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[34].ToString()));//}
                        //catch{}
                        //try
                        //{
                        if (!this.Reader.IsDBNull(35)) objOrder.OrderType.IsPrint = System.Convert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[35].ToString()));//}

                        //�Ƿ��ӡ��ҽ����
                        if (!this.Reader.IsDBNull(84)) objOrder.User03 = this.Reader[84].ToString();//}
                        //catch{}
                        //try
                        //{
                        if (!this.Reader.IsDBNull(36)) objOrder.OrderType.IsConfirm = System.Convert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[36].ToString()));//}


                        //catch{}
                    }
                    catch (Exception ex)
                    {
                        this.Err = "���ҽ��������Ϣ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    #endregion
                    #region "ִ�����"
                    // ����ִ�����
                    //		   37����ҽʦId   38����ҽʦname  39��ʼʱ��      40����ʱ��     41��������
                    //		   42����ʱ��     43¼����Ա����  44¼����Ա����  45�����ID     46���ʱ��       
                    //		   47DCԭ�����   48DCԭ������    49DCҽʦ����    50DCҽʦ����   51Dcʱ��
                    //         52ִ����Ա���� 53ִ��ʱ��      54ִ�п��Ҵ���  55ִ�п������� 
                    //		   56���ηֽ�ʱ�� 57�´ηֽ�ʱ��
                    try
                    {
                        objOrder.ReciptDoctor.ID = this.Reader[37].ToString();
                        objOrder.ReciptDoctor.Name = this.Reader[38].ToString();
                        //try{
                        if (!this.Reader.IsDBNull(39)) objOrder.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[39].ToString());
                        //}
                        //catch{}
                        //try{
                        if (!this.Reader.IsDBNull(40)) objOrder.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[40].ToString());
                        //}
                        //catch{}
                        objOrder.ReciptDept.ID = this.Reader[41].ToString();//InDept.ID
                        //try{
                        if (!this.Reader.IsDBNull(42)) objOrder.MOTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[42].ToString());
                        //}
                        //catch{}
                        objOrder.Oper.ID = this.Reader[43].ToString();
                        objOrder.Oper.Name = this.Reader[44].ToString();
                        objOrder.Nurse.ID = this.Reader[45].ToString();
                        //try{
                        if (!this.Reader.IsDBNull(46)) objOrder.ConfirmTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[46].ToString());
                        //}
                        //catch{}
                        objOrder.DcReason.ID = this.Reader[47].ToString();
                        objOrder.DcReason.Name = this.Reader[48].ToString();
                        objOrder.DCOper.ID = this.Reader[49].ToString();
                        objOrder.DCOper.Name = this.Reader[50].ToString();
                        //try{
                        if (!this.Reader.IsDBNull(51)) objOrder.DCOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[51].ToString());
                        //}
                        //catch{}
                        objOrder.ExecOper.ID = this.Reader[52].ToString();
                        //try{
                        if (!this.Reader.IsDBNull(53)) objOrder.ExecOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[53].ToString());

                        objOrder.ExeDept.ID = this.Reader[54].ToString();
                        objOrder.ExeDept.Name = this.Reader[55].ToString();

                        objOrder.ExecOper.Dept.ID = this.Reader[54].ToString();
                        objOrder.ExecOper.Dept.Name = this.Reader[55].ToString();

                        if (!this.Reader.IsDBNull(56)) objOrder.CurMOTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[56].ToString());

                        if (!this.Reader.IsDBNull(57)) objOrder.NextMOTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[57].ToString());

                    }
                    catch (Exception ex)
                    {
                        this.Err = "���ҽ��ִ�������Ϣ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    #endregion
                    #region "ҽ������"
                    // ����ҽ������
                    //		   58�Ƿ�Ӥ����1��/2��          59�������  	  60������     61��ҩ��� 
                    //		   62�Ƿ񸽲�'1'  63�Ƿ��������  64ҽ��״̬      65�ۿ���     66ִ�б��1δִ��/2��ִ��/3DCִ�� 
                    //		   67ҽ��˵��                     68�Ӽ����:1��ͨ/2�Ӽ�         69�������
                    //         70 ��ע       ,       71��鲿λ��ע    ,72 �������,74 ȡҩҩ������ 81 �Ƿ�Ƥ��
                    try
                    {

                        if (!this.Reader.IsDBNull(58)) objOrder.IsBaby = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[58].ToString()));

                        if (!this.Reader.IsDBNull(59)) objOrder.BabyNO = this.Reader[59].ToString();

                        objOrder.Combo.ID = this.Reader[60].ToString();

                        if (!this.Reader.IsDBNull(61)) objOrder.Combo.IsMainDrug = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[61].ToString()));

                        if (!this.Reader.IsDBNull(62)) objOrder.IsSubtbl = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[62].ToString()));

                        if (!this.Reader.IsDBNull(63)) objOrder.IsHaveSubtbl = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[63].ToString()));

                        if (!this.Reader.IsDBNull(64)) objOrder.Status = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[64].ToString());

                        if (!this.Reader.IsDBNull(65)) objOrder.IsStock = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[65].ToString()));


                        if (!this.Reader.IsDBNull(66)) objOrder.ExecStatus = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[66].ToString());

                        objOrder.Note = this.Reader[67].ToString();

                        if (!this.Reader.IsDBNull(68)) objOrder.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[68].ToString()));

                        if (!this.Reader.IsDBNull(69)) objOrder.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[69]);

                        objOrder.Memo = this.Reader[70].ToString();
                        objOrder.CheckPartRecord = this.Reader[71].ToString();
                        objOrder.Reorder = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[72].ToString());
                        objOrder.StockDept.ID = this.Reader[74].ToString();//ȡҩҩ������
                        try
                        {
                            if (!this.Reader.IsDBNull(75)) objOrder.IsPermission = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[75]);//������ҩ֪��
                        }
                        catch { }
                        objOrder.Package.ID = this.Reader[76].ToString();
                        objOrder.Package.Name = this.Reader[77].ToString();
                        objOrder.ExtendFlag2 = this.Reader[79].ToString();
                        objOrder.ReTidyInfo = this.Reader[80].ToString();
                        try
                        {
                            if (!this.Reader.IsDBNull(81))
                                objOrder.HypoTest = (FS.HISFC.Models.Order.EnumHypoTest)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[81].ToString());//1 ����Ƥ�� 2 ��Ƥ�� 3 �� 4 ��
                        }
                        catch
                        {
                            objOrder.HypoTest = FS.HISFC.Models.Order.EnumHypoTest.FreeHypoTest;
                        }

                        objOrder.Frequency.Time = this.Reader[82].ToString(); //ִ��ʱ��
                        objOrder.ExecDose = this.Reader[83].ToString();//����Ƶ�μ���
                    }
                    catch (Exception ex)
                    {
                        this.Err = "���ҽ��������Ϣ����" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    #endregion
                    al.Add(objOrder);
                }
            }
            catch (Exception ex)
            {
                this.Err = "���ҽ����Ϣ����" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            this.Reader.Close();
            return al;
        }
          #endregion
    }
}
