using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Collections;
namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// [��������: ҽ���ؼ�]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class Order
    {

        //public string LONGSETTINGFILENAME = FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + "longordersetting.xml";
        //public string SHORTSETTINGFILENAME = FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.FrameWork.WinForms.Classes.Function.SettingPath + "shortordersetting.xml";

        public DataSet dsAllLong = null;

        /// <summary>
        /// ����ҩƷ������Ϣ
        /// </summary>
        static Hashtable hsPhaItem = new Hashtable();

        /// <summary>
        /// ��ȡҩƷ������Ϣ������֤��������Ϣ
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public static FS.HISFC.Models.Pharmacy.Item GetPHAItem(string itemCode)
        {
            FS.HISFC.Models.Pharmacy.Item item = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(itemCode);
            if (item == null)
            {
                if (hsPhaItem.Contains(itemCode))
                {
                    item = hsPhaItem[itemCode] as FS.HISFC.Models.Pharmacy.Item;
                }
                else
                {
                    item = CacheManager.PhaIntegrate.GetItem(itemCode);
                    hsPhaItem.Add(itemCode, item);
                }
            }
            return item;
        }


        #region ��ʼ��
        /// <summary>
        /// ����DataSet
        /// </summary>
        /// <param name="dataSet"></param>
        [Obsolete("�����ٴ��������", false)]
        public void SetDataSet(ref System.Data.DataSet dataSet)
        {
            try
            {

                Type dtStr = System.Type.GetType("System.String");
                Type dtDbl = typeof(System.Double);
                Type dtInt = typeof(System.Int32);
                Type dtBoolean = typeof(System.Boolean);
                Type dtDate = typeof(System.DateTime);

                DataTable table = new DataTable("Table");
                table.Columns.AddRange(new DataColumn[] {
															new DataColumn("!",dtStr),     //0
															new DataColumn("��Ч",dtStr),     //0
															new DataColumn("ҽ������",dtStr),//1
															new DataColumn("ҽ����ˮ��",dtStr),//2
															new DataColumn("ҽ��״̬",dtStr),//�¿�������ˣ�ִ��
															new DataColumn("��Ϻ�",dtStr),//4
															new DataColumn("��ҩ",dtStr),//5
                                                            
                                                            new DataColumn("���",dtStr),
                                                            new DataColumn("����ʱ��",dtStr),
															new DataColumn("����ҽ��",dtStr),
															new DataColumn("˳���",dtInt),//28
                                                            new DataColumn("ҽ������",dtStr),//6
															new DataColumn("���",dtStr),     //0
															new DataColumn("������",dtInt),//7
                                                            new DataColumn("ÿ������",dtStr),//9
															new DataColumn("��λ",dtStr),//10

                                                            new DataColumn("Ƶ�α���",dtStr),
															new DataColumn("Ƶ������",dtStr),
															new DataColumn("�÷�����",dtStr),
															new DataColumn("�÷�����",dtStr),//15

                                                            new DataColumn("����",dtStr),//7
															new DataColumn("������λ",dtStr),//8
															
															new DataColumn("����",dtStr),//11

															new DataColumn("ϵͳ���",dtStr),//5															
															
															new DataColumn("��ʼʱ��",dtStr),
                                                            new DataColumn("����ʱ��",dtStr),//25
															new DataColumn("ֹͣʱ��",dtStr),//25
                                                            
															new DataColumn("ִ�п��ұ���",dtStr),
															new DataColumn("ִ�п���",dtStr),
															new DataColumn("�Ӽ�",dtBoolean),
															new DataColumn("��鲿λ",dtStr),//31
															new DataColumn("��������",dtStr),//32
															new DataColumn("�ۿ���ұ���",dtStr),//33
															new DataColumn("�ۿ����",dtStr),//34
															new DataColumn("��ע",dtStr),//20
															new DataColumn("¼���˱���",dtStr),
															new DataColumn("¼����",dtStr),
															new DataColumn("��������",dtStr),

                                                            //new DataColumn("����ʱ��",dtStr),

															new DataColumn("ֹͣ�˱���",dtStr),
															new DataColumn("ֹͣ��",dtStr),
                                                            new DataColumn("����",dtStr),
                                                            new DataColumn("����ҽ������",dtStr)
														});


                dataSet.Tables.Add(table);

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("SetDataSet" + ex.Message);
                return;
            }
        }


        public FarPoint.Win.Spread.FpSpread fpSpread1 = null;

        #endregion

        #region "��Ӧ"

        [Obsolete("", true)]
        public int[] iColumns;

        /// <summary>
        /// �洢�п�
        /// </summary>
        //[Obsolete("", true)]
        //public int[] iColumnWidth;

        /// <summary>
        /// ����������
        /// </summary>
        [Obsolete("", true)]
        public void SetColumnProperty()
        {
            //if (System.IO.File.Exists(LONGSETTINGFILENAME))
            //{
            //    //if (iColumnWidth == null || iColumnWidth.Length <= 0)
            //    //{
            //        FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1.Sheets[0], LONGSETTINGFILENAME);
            //        FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1.Sheets[1], SHORTSETTINGFILENAME);

            //        //iColumnWidth = new int[40];
            //        //for (int i = 0; i < this.fpSpread1.Sheets[0].Columns.Count; i++)
            //        //{
            //        //    iColumnWidth[i] = (int)this.fpSpread1.Sheets[0].Columns[i].Width;
            //        //}
            //    //}
            //    //else
            //    //{
            //    //    for (int i = 0; i < this.fpSpread1.Sheets[0].Columns.Count; i++)
            //    //    {
            //    //        this.fpSpread1.Sheets[0].Columns[i].Width = iColumnWidth[i];
            //    //        this.fpSpread1.Sheets[1].Columns[i].Width = iColumnWidth[i];
            //    //    }
            //    //}
            //}
            //else
            //{
            //    FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1.Sheets[0], LONGSETTINGFILENAME);
            //    FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1.Sheets[1], SHORTSETTINGFILENAME);
            //}
        }
        [Obsolete("", true)]
        public void SetColumnWidth()
        {
            //this.iColumnWidth = new int[40];
            //this.iColumnWidth[0] = 56;
            //this.iColumnWidth[1] = 10;
            //this.iColumnWidth[2] = 56;
            //this.iColumnWidth[3] = 10;
            //this.iColumnWidth[4] = 10;
            //this.iColumnWidth[5] = 10;
            //this.iColumnWidth[6] = 10;
            //this.iColumnWidth[7] = 185;
            //this.iColumnWidth[8] = 15;
            //this.iColumnWidth[9] = 31;
            //this.iColumnWidth[10] = 31;
            //this.iColumnWidth[11] = 46;
            //this.iColumnWidth[12] = 31;
            //this.iColumnWidth[13] = 33;
            //this.iColumnWidth[14] = 33;
            //this.iColumnWidth[15] = 10;
            //this.iColumnWidth[16] = 10;
            //this.iColumnWidth[17] = 31;
            //this.iColumnWidth[18] = 76;//��ʼʱ��
            //this.iColumnWidth[19] = 76;//ֹͣʱ��
            //this.iColumnWidth[20] = 56;//����ҽ��
            //this.iColumnWidth[21] = 10;//ִ�п��ұ���
            //this.iColumnWidth[22] = 56;//ִ�п���
            //this.iColumnWidth[23] = 19;//�Ӽ�
            //this.iColumnWidth[24] = 56;//��鲿λ
            //this.iColumnWidth[26] = 56;//��������
            //this.iColumnWidth[27] = 10;//�ۿ���ұ���
            //this.iColumnWidth[28] = 56;//�ۿ����
            //this.iColumnWidth[29] = 56;
            //this.iColumnWidth[30] = 56;
            //this.iColumnWidth[31] = 56;
            //this.iColumnWidth[32] = 56;
            //this.iColumnWidth[33] = 56;
            //this.iColumnWidth[34] = 56;
            //this.iColumnWidth[35] = 56;
            //this.iColumnWidth[36] = 56;
            //this.iColumnWidth[37] = 10;
            //this.iColumnWidth[38] = 10;
            //this.iColumnWidth[39] = 10;
        }

        /// <summary>
        /// ͨ���������������
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        [Obsolete("", true)]
        public int GetColumnIndexFromName(string Name)
        {
            for (int i = 0; i < dsAllLong.Tables[0].Columns.Count; i++)
            {
                if (dsAllLong.Tables[0].Columns[i].ColumnName == Name)
                    return i;
            }
            MessageBox.Show("ȱ����" + Name);
            return -1;
        }
        [Obsolete("", true)]
        public void ColumnSet()
        {
            //iColumns = new int[40];
            //iColumns[0] = this.GetColumnIndexFromName("��Ч");     //Type
            //iColumns[1] = this.GetColumnIndexFromName("ҽ������");//OrderType
            //iColumns[2] = this.GetColumnIndexFromName("ҽ����ˮ��");//ID
            //iColumns[3] = this.GetColumnIndexFromName("ҽ��״̬");//�¿�������ˣ�ִ��State
            //iColumns[4] = this.GetColumnIndexFromName("��Ϻ�");//4 ComboNo
            //iColumns[5] = this.GetColumnIndexFromName("��ҩ");//5 MainDrug
            //iColumns[6] = this.GetColumnIndexFromName("ҽ������");//6 Nameer	
            //iColumns[7] = this.GetColumnIndexFromName("����");//7	Qty
            //iColumns[8] = this.GetColumnIndexFromName("������λ");//8 PackUnit
            //iColumns[9] = this.GetColumnIndexFromName("ÿ������");//9 DoseOnce
            //iColumns[10] = this.GetColumnIndexFromName("��λ");//10 doseUnit
            //iColumns[11] = this.GetColumnIndexFromName("����");//11 Fu
            //iColumns[12] = this.GetColumnIndexFromName("Ƶ�α���"); //FrequencyCode
            //iColumns[13] = this.GetColumnIndexFromName("Ƶ������"); //FrequecyName
            //iColumns[14] = this.GetColumnIndexFromName("�÷�����"); //UsageCode
            //iColumns[15] = this.GetColumnIndexFromName("�÷�����");//15
            //iColumns[16] = this.GetColumnIndexFromName("��ʼʱ��");
            //iColumns[17] = this.GetColumnIndexFromName("ִ�п��ұ���");
            //iColumns[18] = this.GetColumnIndexFromName("ִ�п���");
            //iColumns[19] = this.GetColumnIndexFromName("�Ӽ�");
            //iColumns[20] = this.GetColumnIndexFromName("��ע");//20
            //iColumns[21] = this.GetColumnIndexFromName("¼���˱���");
            //iColumns[22] = this.GetColumnIndexFromName("¼����");
            //iColumns[23] = this.GetColumnIndexFromName("��������");
            //iColumns[25] = this.GetColumnIndexFromName("ֹͣʱ��");//25
            //iColumns[26] = this.GetColumnIndexFromName("ֹͣ�˱���");
            //iColumns[27] = this.GetColumnIndexFromName("ֹͣ��");
            //iColumns[28] = this.GetColumnIndexFromName("˳���");//28
            //iColumns[24] = this.GetColumnIndexFromName("����ʱ��");
            //iColumns[29] = this.GetColumnIndexFromName("����ҽ��");
            //iColumns[30] = this.GetColumnIndexFromName("���");
            //iColumns[31] = this.GetColumnIndexFromName("��鲿λ");
            //iColumns[32] = this.GetColumnIndexFromName("��������");
            //iColumns[33] = this.GetColumnIndexFromName("�ۿ���ұ���");34
            //iColumns[34] = this.GetColumnIndexFromName("�ۿ����");
            //iColumns[35] = this.GetColumnIndexFromName("!");

            //iColumns[36] = this.GetColumnIndexFromName("ϵͳ���");
            //iColumns[37] = this.GetColumnIndexFromName("������");
            //iColumns[38] = this.GetColumnIndexFromName("���");
            //iColumns[39] = this.GetColumnIndexFromName("���");

        }

        [Obsolete("����SetColumnNameNew", true)]
        public void SetColumnName(int k)
        {
            this.fpSpread1.Sheets[k].Columns.Count = 100;
            int i = 0;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("!");    //0
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("��Ч");     //0
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("ҽ������");//1
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("ҽ����ˮ��");//2
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("ҽ��״̬");//�¿�������ˣ�ִ��
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("��Ϻ�");//4
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("��ҩ");//5
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;


            /*ҽ��˳����ʾ����*/

            i++;

            this.fpSpread1.Sheets[k].Columns[i].Label = ("���");

            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("����ʱ��");
            this.fpSpread1.Sheets[k].Columns[i].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            i++;

            this.fpSpread1.Sheets[k].Columns[i].Label = ("����ҽ��");

            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("˳���");//28

            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("ҽ������");//6
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("��");    //0
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("������");//7

            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("ÿ����");//9
            this.fpSpread1.Sheets[k].Columns[i].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.fpSpread1.Sheets[k].Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;

            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("��λ");//10

            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("Ƶ��");
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("Ƶ������");
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("�÷�����");
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("�÷�");//15


            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("����");//7
            this.fpSpread1.Sheets[k].Columns[i].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            this.fpSpread1.Sheets[k].Columns[i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("��λ");//8

            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("����");//11

            /*������ֹ*/

            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("ϵͳ���");//6

            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("��ʼʱ��");
            this.fpSpread1.Sheets[k].Columns[i].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("ֹͣʱ��");//25
            this.fpSpread1.Sheets[k].Columns[i].CellType = new FarPoint.Win.Spread.CellType.TextCellType();


            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("ִ�п��ұ���");
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("ִ�п���");
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("��");
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("��鲿λ");
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("��������");
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("�ۿ���ұ���");
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("�ۿ����");
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("��ע");//20
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("¼���˱���");
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("¼����");
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("��������");
            i++;

            this.fpSpread1.Sheets[k].Columns[i].Label = ("ֹͣ�˱���");
            this.fpSpread1.Sheets[k].Columns[i].Visible = false;
            i++;
            this.fpSpread1.Sheets[k].Columns[i].Label = ("ֹͣ��");

            i++;
            this.fpSpread1.Sheets[k].Columns.Count = i;
        }

        #endregion

        #region ����
        /// <summary>
        /// �����ʽ
        /// </summary>
        //public void SaveGrid()
        //{
        //    try
        //    {
        //        FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1.Sheets[0], this.LONGSETTINGFILENAME);
        //        FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1.Sheets[1], this.SHORTSETTINGFILENAME);
        //        MessageBox.Show("��ʾ��ʽ����ɹ��������µ�¼����Ч��");
        //    }
        //    catch { }
        //}

        #endregion

    }
}
