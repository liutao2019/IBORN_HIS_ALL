using System;
using System.Collections;
using FS.HISFC.Models.SIInterface;
using FS.HISFC.Models.Fee;
using System.Data;
using System.Collections.Generic;

namespace FS.HISFC.BizLogic.Fee
{
    /// <summary>
    /// Interface ��ժҪ˵����
    /// </summary>
    public class Interface : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// ���ϴ���Ŀ
        /// </summary>
        private static Dictionary<string, string> dictionarySplitFee = new Dictionary<string, string>();

        bool isSplitFee = false;
        public bool IsSplitFee
        {
            set { isSplitFee = value; }
        }
        /// <summary>
        /// �ж��Ƿ��Ǹ��շ���Ŀ
        /// </summary>
        /// <param name="feeItemList"></param>
        /// <returns></returns>
        public static bool IsSplitFeeFlag(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            if (feeItemList.SplitFeeFlag)
            {
                return true;
            }
            else
            {
                if (dictionarySplitFee.Count == 0)
                {
                    FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
                    ArrayList alMinFee = conMgr.GetList("NOUPMINFEE");
                    foreach (FS.FrameWork.Models.NeuObject obj in alMinFee)
                    {
                        dictionarySplitFee[obj.ID] = obj.Name;
                    }
                }

                return dictionarySplitFee.ContainsKey(feeItemList.Item.MinFee.ID);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public Interface()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region ����
        /// <summary>
        /// ��ȡҽ������ҩƷ��Ϣ����
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="sysClass"></param>
        /// <returns></returns>
        public ArrayList GetCenterItem(string pactCode, string sysClass)
        {
            string centerDrugType = "";
            string centerDrugTypeSec = "";
            string strSql = "";

            switch (sysClass)
            {
                case "P":
                    centerDrugType = "X"; //��ҩ��Ŀ
                    centerDrugTypeSec = "X";
                    break;
                case "Z":
                    centerDrugType = "Z"; //��ҩ��Ŀ
                    centerDrugTypeSec = "Z";
                    break;
                case "C":
                    centerDrugType = "C"; //��ҩ��Ŀ
                    centerDrugTypeSec = "C";
                    break;
                case "Undrug":
                    centerDrugType = "L";
                    centerDrugTypeSec = "F";
                    break;
                //{8507E4F9-9C00-4571-BF32-2A6779CE4815} wbo 2010-08-30
                case "ALL":
                    centerDrugType = "ALL";
                    centerDrugTypeSec = "ALL";
                    break;
            }

            if (this.Sql.GetCommonSql("Fee.Interface.GetCenterItem.Select.1", ref strSql) == -1)
                return null;

            try
            {
                strSql = string.Format(strSql, pactCode, centerDrugType, centerDrugTypeSec);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    FS.HISFC.Models.SIInterface.Item obj = new FS.HISFC.Models.SIInterface.Item();

                    obj.PactCode = Reader[0].ToString();
                    obj.ID = Reader[1].ToString();
                    obj.SysClass = Reader[2].ToString();
                    obj.Name = Reader[3].ToString();
                    obj.EnglishName = Reader[4].ToString();
                    obj.Specs = Reader[5].ToString();
                    obj.DoseCode = Reader[6].ToString();
                    obj.SpellCode = Reader[7].ToString();
                    obj.FeeCode = Reader[8].ToString();
                    obj.ItemType = Reader[9].ToString();
                    obj.ItemGrade = Reader[10].ToString();
                    obj.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[11].ToString());
                    obj.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[12].ToString());
                    obj.Memo = Reader[13].ToString();
                    obj.OperCode = Reader[14].ToString();
                    // obj.OperDate = Convert.ToDateTime(Reader[15].ToString());
                    //{B61D848C-EDAF-4c42-B790-EE6BE986B192} ��ׯ�޸�
                    obj.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[15].ToString());
                    al.Add(obj);
                }

                Reader.Close();

                return al;

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// ��ȡҽ������������Ϣ����
        /// </summary>
        /// <param name="pactCode">��ͬ��λ</param>
        /// <returns></returns>
        public ArrayList GetCenterItem(string pactCode)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Interface.GetCenterItem.Select.3", ref strSql) == -1)
                return null;
            try
            {
                strSql = string.Format(strSql, pactCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    FS.HISFC.Models.SIInterface.Item obj = new FS.HISFC.Models.SIInterface.Item();

                    obj.PactCode = Reader[0].ToString();
                    obj.ID = Reader[1].ToString();
                    obj.SysClass = Reader[2].ToString();
                    obj.Name = Reader[3].ToString();
                    obj.EnglishName = Reader[4].ToString();
                    obj.Specs = Reader[5].ToString();
                    obj.DoseCode = Reader[6].ToString();
                    obj.SpellCode = Reader[7].ToString();
                    obj.FeeCode = Reader[8].ToString();
                    obj.ItemType = Reader[9].ToString();
                    obj.ItemGrade = Reader[10].ToString();
                    obj.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[11].ToString());
                    obj.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[12].ToString());
                    obj.Memo = Reader[13].ToString();
                    obj.OperCode = Reader[14].ToString();
                    //obj.OperDate = Convert.ToDateTime(Reader[15].ToString());

                    al.Add(obj);
                }

                Reader.Close();

                return al;

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// ��ö��պ����Ŀ��Ϣ
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="sysClass"></param>
        /// <returns></returns>
        public ArrayList GetCompareItem(string pactCode, string sysClass)
        {
            string centerDrugType = "";
            string centerDrugTypeSec = "";

            switch (sysClass)
            {
                case "P":
                    centerDrugType = "X"; //��ҩ��Ŀ
                    centerDrugTypeSec = "X";
                    break;
                case "Z":
                    centerDrugType = "Z"; //��ҩ��Ŀ
                    centerDrugTypeSec = "Z";
                    break;
                case "C":
                    //����
                    centerDrugType = "Z"; //��ҩ��Ŀ
                    centerDrugTypeSec = "Z";
                    break;
                case "Undrug":
                    centerDrugType = "L";
                    centerDrugTypeSec = "F";
                    break;
                //����������ҩ{8507E4F9-9C00-4571-BF32-2A6779CE4815} WBO 2010-08-30
                case "ALL":
                    centerDrugType = "ALL";
                    centerDrugTypeSec = "ALL";
                    break;
            }

            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Interface.GetCompareItem.Select.1", ref strSql) == -1)
                return null;

            try
            {
                strSql = string.Format(strSql, pactCode, centerDrugType, centerDrugTypeSec);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    FS.HISFC.Models.SIInterface.Compare obj = new Compare();

                    obj.CenterItem.PactCode = Reader[0].ToString();
                    obj.HisCode = Reader[1].ToString();
                    obj.CenterItem.ID = Reader[2].ToString();
                    obj.CenterItem.SysClass = Reader[3].ToString();
                    obj.CenterItem.Name = Reader[4].ToString();
                    obj.CenterItem.EnglishName = Reader[5].ToString();
                    obj.CenterItem.Specs = Reader[6].ToString();
                    obj.CenterItem.DoseCode = Reader[7].ToString();
                    obj.CenterItem.SpellCode = Reader[8].ToString();
                    obj.CenterItem.FeeCode = Reader[9].ToString();
                    obj.CenterItem.ItemType = Reader[10].ToString();
                    obj.CenterItem.ItemGrade = Reader[11].ToString();
                    obj.CenterItem.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[12].ToString());
                    obj.CenterItem.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[13].ToString());
                    obj.CenterItem.Memo = Reader[14].ToString();
                    obj.SpellCode.SpellCode = Reader[15].ToString();
                    obj.SpellCode.WBCode = Reader[16].ToString();
                    obj.SpellCode.UserCode = Reader[17].ToString();
                    obj.Specs = Reader[18].ToString();
                    obj.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[19].ToString());
                    obj.DoseCode = Reader[20].ToString();
                    obj.CenterItem.OperCode = Reader[21].ToString();
                    obj.CenterItem.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[22].ToString());
                    obj.Name = Reader[23].ToString();
                    obj.RegularName = Reader[24].ToString();
                    obj.FdaDrguCode = Reader[25].ToString();

                    al.Add(obj);
                }

                Reader.Close();

                return al;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }

        /*
        /// <summary>
        /// ��ö��պ���������ҩƷ��Ϣ
        /// </summary>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public ArrayList GetCompareDrugItem(string pactCode)
        {
            string strSql = "";
            if(this.Sql.GetCommonSql("Fee.Interface.GetCompareItem.Select.3", ref strSql) == -1) 
                return null;
			
            try
            {   				
                strSql = string.Format(strSql, pactCode);
            }
            catch(Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery(strSql);
                while(Reader.Read())
                {
                    FS.HISFC.Models.InterfaceSi.Compare obj = new FS.HISFC.Models.InterfaceSi.Compare();

                    obj.CenterItem.PactCode = Reader[0].ToString();
                    obj.HisCode = Reader[1].ToString();
                    obj.CenterItem.ID = Reader[2].ToString();
                    obj.CenterItem.SysClass = Reader[3].ToString();
                    obj.CenterItem.Name = Reader[4].ToString();
                    obj.CenterItem.EnglishName = Reader[5].ToString();
                    obj.CenterItem.Specs = Reader[6].ToString();
                    obj.CenterItem.DoseCode = Reader[7].ToString();
                    obj.CenterItem.SpellCode = Reader[8].ToString();
                    obj.CenterItem.FeeCode = Reader[9].ToString();
                    obj.CenterItem.ItemType = Reader[10].ToString();
                    obj.CenterItem.ItemGrade = Reader[11].ToString();
                    obj.CenterItem.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[12].ToString());
                    obj.CenterItem.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[13].ToString());
                    obj.CenterItem.Memo = Reader[14].ToString();
                    obj.SpellCode = Reader[15].ToString();
                    obj.SpellCode.WBCode = Reader[16].ToString();
                    obj.SpellCode.UserCode = Reader[17].ToString();
                    obj.Specs = Reader[18].ToString();
                    obj.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[19].ToString());
                    obj.DoseCode = Reader[20].ToString();
                    obj.CenterItem.OperCode = Reader[21].ToString();
                    obj.CenterItem.OperDate = Convert.ToDateTime(Reader[22].ToString());
                    obj.Name = Reader[23].ToString();
                    obj.RegularName = Reader[24].ToString();

                    al.Add(obj);
                }

                Reader.Close();

                return al;
            }
            catch(Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }
        */

        /// <summary>
        /// ����ҽ�������ȡҽ��������Ϣ
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public FS.HISFC.Models.SIInterface.Compare GetCompareDrugItemByItemCode(string pactCode, string itemCode)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Fee.Interface.GetCompareItem.Select.2", ref strSql) == -1)
                return null;

            try
            {
                strSql = string.Format(strSql, pactCode, itemCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            try
            {

                this.ExecQuery(strSql);
                FS.HISFC.Models.SIInterface.Compare obj = null;
                while (Reader.Read())
                {
                    obj = new FS.HISFC.Models.SIInterface.Compare();
                    obj.CenterItem.PactCode = Reader[0].ToString();
                    obj.HisCode = Reader[1].ToString();
                    obj.CenterItem.ID = Reader[2].ToString();
                    obj.CenterItem.SysClass = Reader[3].ToString();
                    obj.CenterItem.Name = Reader[4].ToString();
                    obj.CenterItem.EnglishName = Reader[5].ToString();
                    obj.CenterItem.Specs = Reader[6].ToString();
                    obj.CenterItem.DoseCode = Reader[7].ToString();
                    obj.CenterItem.SpellCode = Reader[8].ToString();
                    obj.CenterItem.FeeCode = Reader[9].ToString();
                    obj.CenterItem.ItemType = Reader[10].ToString();
                    obj.CenterItem.ItemGrade = Reader[11].ToString();
                    obj.CenterItem.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[12].ToString());
                    obj.CenterItem.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[13].ToString());
                    obj.CenterItem.Memo = Reader[14].ToString();
                    obj.SpellCode.SpellCode = Reader[15].ToString();
                    obj.SpellCode.WBCode = Reader[16].ToString();
                    obj.SpellCode.UserCode = Reader[17].ToString();
                    obj.Specs = Reader[18].ToString();
                    obj.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[19].ToString());
                    obj.DoseCode = Reader[20].ToString();
                    obj.CenterItem.OperCode = Reader[21].ToString();
                    obj.CenterItem.OperDate = Convert.ToDateTime(Reader[22].ToString());
                    obj.Name = Reader[23].ToString();
                    obj.RegularName = Reader[24].ToString();
                }

                Reader.Close();

                return obj;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }


        /// <summary>
        /// ��ȡ������Ŀ��Ϣ
        /// </summary>
        /// <param name="pactCode">��ͬ��λ����</param>
        /// <param name="itemCode">��Ŀ����</param>
        /// <returns>������Ŀ��Ϣ</returns>
        public FS.HISFC.Models.SIInterface.Item GetCenterItemInfo(string pactCode, string itemCode)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Fee.Interface.GetCenterItem", ref strSql) == -1)
                return null;
            try
            {
                strSql = string.Format(strSql, pactCode, itemCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            FS.HISFC.Models.SIInterface.Item obj = new FS.HISFC.Models.SIInterface.Item();
            try
            {
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    obj = new FS.HISFC.Models.SIInterface.Item();

                    obj.PactCode = Reader[0].ToString();
                    obj.ID = Reader[1].ToString();
                    obj.SysClass = Reader[2].ToString();
                    obj.Name = Reader[3].ToString();
                    obj.EnglishName = Reader[4].ToString();
                    obj.Specs = Reader[5].ToString();
                    obj.DoseCode = Reader[6].ToString();
                    obj.SpellCode = Reader[7].ToString();
                    obj.FeeCode = Reader[8].ToString();
                    obj.ItemType = Reader[9].ToString();
                    obj.ItemGrade = Reader[10].ToString();
                    obj.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[11].ToString());
                    obj.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[12].ToString());
                    obj.Memo = Reader[13].ToString();
                    obj.OperCode = Reader[14].ToString();
                    obj.OperDate = Convert.ToDateTime(Reader[15].ToString());
                }

                Reader.Close();
                if (obj == null || obj.ID == "")
                    this.Err = "δ��ȡҽ��������Ŀ��Ϣ ���ȶ����ø��³�����±���������Ŀ��Ϣ";
                return obj;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// �Ƿ�Ը���Ŀ�Ѷ���
        /// </summary>
        /// <param name="hisUserCode">his����Ŀ�Զ����� ��Ӧҽ����������ҽ��������Ϣ��his��Ŀ����</param>
        /// <param name="centerCode">������Ŀ����</param>
        /// <returns>-1 ���� 0 δ���� 1 �Ѷ���</returns>
        public int IsCompared(string hisUserCode, string centerCode)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Fee.Interface.IsCompared", ref strSql) == -1)
                return -1;
            try
            {
                strSql = string.Format(strSql, hisUserCode, centerCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
            try
            {
                this.ExecQuery(strSql);
                return 0;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// ��õ����Ѷ�����Ϣ
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="objCompare"></param>
        /// <returns></returns>
        public int GetCompareSingleItem(string pactCode, string itemCode, ref Compare objCompare)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Interface.GetCompareSingleItem.Select.1", ref strSql) == -1)
                return -1;

            try
            {
                strSql = string.Format(strSql, pactCode, itemCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    FS.HISFC.Models.SIInterface.Compare obj = new Compare();

                    obj.CenterItem.PactCode = Reader[0].ToString();//0��ͬ��λ
                    obj.HisCode = Reader[1].ToString();//1������Ŀ����
                    obj.CenterItem.ID = Reader[2].ToString();//2ҽ���շ���Ŀ����
                    obj.CenterItem.SysClass = Reader[3].ToString();//3��Ŀ��� X-��ҩ Z-��ҩ L-������Ŀ F-ҽ�Ʒ�����ʩ
                    obj.CenterItem.Name = Reader[4].ToString();//4ҽ���շ���Ŀ��������
                    obj.CenterItem.EnglishName = Reader[5].ToString();//5ҽ���շ���ĿӢ������
                    obj.CenterItem.Specs = Reader[6].ToString();//6ҽ�����
                    obj.CenterItem.DoseCode = Reader[7].ToString();//7ҽ�����ͱ���
                    obj.CenterItem.SpellCode = Reader[8].ToString();//8ҽ��ƴ������
                    obj.CenterItem.FeeCode = Reader[9].ToString();//9ҽ�����÷������ 1 ��λ�� 2��ҩ��3��ҩ��4�г�ҩ5�в�ҩ6����7���Ʒ�8�����9������10�����11��Ѫ��12������13����
                    obj.CenterItem.ItemType = Reader[10].ToString();//10ҽ��Ŀ¼���� 1 ����ҽ�Ʒ�Χ 2 �㶫ʡ������
                    obj.CenterItem.ItemGrade = Reader[11].ToString();//11ҽ��Ŀ¼�ȼ� 1 ����(ͳ��ȫ��֧��) 2 ����(׼�貿��֧��) 3 �Է�
                    obj.CenterItem.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[12].ToString());//12�Ը�����
                    obj.CenterItem.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[13].ToString());//13��׼�۸�
                    obj.CenterItem.Memo = Reader[14].ToString();//14����ʹ��˵��(ҽ����ע)
                    obj.SpellCode.SpellCode = Reader[15].ToString();//15ҽԺƴ��
                    obj.SpellCode.WBCode = Reader[16].ToString();//16ҽԺ�����
                    obj.SpellCode.UserCode = Reader[17].ToString();//17ҽԺ�Զ�����
                    obj.Specs = Reader[18].ToString();//18ҽԺ���
                    obj.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[19].ToString());//19ҽԺ�����۸�
                    obj.DoseCode = Reader[20].ToString();//20ҽԺ����
                    obj.CenterItem.OperCode = Reader[21].ToString();//21����Ա
                    obj.CenterItem.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[22].ToString());//22��������
                    obj.Name = Reader[23].ToString();//23������Ŀ����
                    obj.RegularName = Reader[24].ToString();//24������Ŀ����
                    //{8DF3D566-FA34-44cb-A2D5-919FE05D1702}
                    obj.Practicablesymptom.ID = Reader[25].ToString();//25���ޱ�־��ҩƷ��Ӧ�ȼ���
                    //obj.Ispracticablesymptom = FS.FrameWork.Function.NConvert.ToBoolean(Reader[25]);
                    obj.Practicablesymptomdepiction = Reader[26].ToString();//26������������Ӧ֢������

                    al.Add(obj);
                }

                Reader.Close();

                if (al.Count > 0)
                {
                    objCompare = (Compare)al[0];
                    return 0;
                }
                else
                {
                    return -2;
                }
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }



        /// <summary>
        /// ��ö���������Ϣ
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="objCompare"></param>
        /// <returns></returns>
        public int GetComparealItem(string pactCode, string itemCode, ref ArrayList alCompare)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Interface.GetCompareSingleItem.Select.1", ref strSql) == -1)
                return -1;

            try
            {
                strSql = string.Format(strSql, pactCode, itemCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    FS.HISFC.Models.SIInterface.Compare obj = new Compare();

                    obj.CenterItem.PactCode = Reader[0].ToString();//0��ͬ��λ
                    obj.HisCode = Reader[1].ToString();//1������Ŀ����
                    obj.CenterItem.ID = Reader[2].ToString();//2ҽ���շ���Ŀ����
                    obj.CenterItem.SysClass = Reader[3].ToString();//3��Ŀ��� X-��ҩ Z-��ҩ L-������Ŀ F-ҽ�Ʒ�����ʩ
                    obj.CenterItem.Name = Reader[4].ToString();//4ҽ���շ���Ŀ��������
                    obj.CenterItem.EnglishName = Reader[5].ToString();//5ҽ���շ���ĿӢ������
                    obj.CenterItem.Specs = Reader[6].ToString();//6ҽ�����
                    obj.CenterItem.DoseCode = Reader[7].ToString();//7ҽ�����ͱ���
                    obj.CenterItem.SpellCode = Reader[8].ToString();//8ҽ��ƴ������
                    obj.CenterItem.FeeCode = Reader[9].ToString();//9ҽ�����÷������ 1 ��λ�� 2��ҩ��3��ҩ��4�г�ҩ5�в�ҩ6����7���Ʒ�8�����9������10�����11��Ѫ��12������13����
                    obj.CenterItem.ItemType = Reader[10].ToString();//10ҽ��Ŀ¼���� 1 ����ҽ�Ʒ�Χ 2 �㶫ʡ������
                    obj.CenterItem.ItemGrade = Reader[11].ToString();//11ҽ��Ŀ¼�ȼ� 1 ����(ͳ��ȫ��֧��) 2 ����(׼�貿��֧��) 3 �Է�
                    obj.CenterItem.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[12].ToString());//12�Ը�����
                    obj.CenterItem.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[13].ToString());//13��׼�۸�
                    obj.CenterItem.Memo = Reader[14].ToString();//14����ʹ��˵��(ҽ����ע)
                    obj.SpellCode.SpellCode = Reader[15].ToString();//15ҽԺƴ��
                    obj.SpellCode.WBCode = Reader[16].ToString();//16ҽԺ�����
                    obj.SpellCode.UserCode = Reader[17].ToString();//17ҽԺ�Զ�����
                    obj.Specs = Reader[18].ToString();//18ҽԺ���
                    obj.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[19].ToString());//19ҽԺ�����۸�
                    obj.DoseCode = Reader[20].ToString();//20ҽԺ����
                    obj.CenterItem.OperCode = Reader[21].ToString();//21����Ա
                    obj.CenterItem.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[22].ToString());//22��������
                    obj.Name = Reader[23].ToString();//23������Ŀ����
                    obj.RegularName = Reader[24].ToString();//24������Ŀ����
                    //{8DF3D566-FA34-44cb-A2D5-919FE05D1702}
                    obj.Practicablesymptom.ID = Reader[25].ToString();//25���ޱ�־��ҩƷ��Ӧ�ȼ���
                    //obj.Ispracticablesymptom = FS.FrameWork.Function.NConvert.ToBoolean(Reader[25]);
                    obj.Practicablesymptomdepiction = Reader[26].ToString();//26������������Ӧ֢������

                    al.Add(obj);
                }

                Reader.Close();

                if (al.Count > 0)
                {
                    alCompare = al;
                    return 0;
                }
                else
                {
                    return -2;
                }
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// ������պ����Ŀ��Ϣ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertCompareItem(Compare obj)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Interface.InsertCompareItem.1", ref strSql) == -1)
                return -1;

            //			try
            //			{   				
            //				strSql = string.Format(strSql, obj.CenterItem.PactCode ,obj.HisCode, obj.CenterItem.ID,
            //					obj.CenterItem.SysClass, obj.CenterItem.Name, obj.CenterItem.EnglishName,
            //					obj.CenterItem.Specs, obj.CenterItem.DoseCode, obj.CenterItem.SpellCode,
            //					obj.CenterItem.FeeCode, obj.CenterItem.ItemType, obj.CenterItem.ItemGrade,
            //					obj.CenterItem.Rate, obj.CenterItem.Price, obj.CenterItem.Memo, 
            //					obj.SpellCode, obj.SpellCode.WBCode, obj.SpellCode.UserCode,
            //					obj.Specs, obj.Price, obj.DoseCode, obj.CenterItem.OperCode,
            //					obj.Name, obj.RegularName);
            //
            //			}
            //			catch(Exception ex)
            //			{
            //				this.ErrCode = ex.Message;
            //				this.Err = ex.Message;
            //				return -1;
            //			}      			

            try
            {
                return this.ExecNoQuery(strSql, obj.CenterItem.PactCode, obj.HisCode, obj.CenterItem.ID,
                    obj.CenterItem.SysClass, obj.CenterItem.Name, obj.CenterItem.EnglishName,
                    obj.CenterItem.Specs, obj.CenterItem.DoseCode, obj.CenterItem.SpellCode,
                    obj.CenterItem.FeeCode, obj.CenterItem.ItemType, obj.CenterItem.ItemGrade,
                    obj.CenterItem.Rate.ToString(), obj.CenterItem.Price.ToString(), obj.CenterItem.Memo,
                    obj.SpellCode.SpellCode, obj.SpellCode.WBCode, obj.SpellCode.UserCode,
                    obj.Specs, obj.Price.ToString(), obj.DoseCode, obj.CenterItem.OperCode,
                    obj.Name, obj.RegularName,obj.FdaDrguCode);//this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// ɾ���Ѷ�����Ϣ
        /// </summary>
        /// <param name="pactCode">��ͬ��λ����</param>
        /// <param name="hisCode">HIS���ر���</param>
        /// <returns></returns>
        public int DeleteCompareItem(string pactCode, string hisCode)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Fee.Interface.DeleteCompareItem.1", ref strSql) == -1)
                return -1;

            try
            {
                strSql = string.Format(strSql, pactCode, hisCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// ���δ���շ�ҩƷ��Ŀ��Ϣ
        /// </summary>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public ArrayList GetNoCompareUndrugItem(string pactCode)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Interface.GetNoCompareUndrugItem.Select.1", ref strSql) == -1)
                return null;

            try
            {
                strSql = string.Format(strSql, pactCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    FS.HISFC.Models.Fee.Item.Undrug obj = new FS.HISFC.Models.Fee.Item.Undrug();

                    obj.ID = Reader[0].ToString();
                    obj.Name = Reader[1].ToString();
                    obj.SpellCode = Reader[2].ToString();
                    obj.WBCode = Reader[3].ToString();
                    obj.UserCode = Reader[4].ToString();
                    obj.Specs = Reader[5].ToString();
                    obj.NationCode = Reader[6].ToString();
                    obj.GBCode = Reader[7].ToString();
                    obj.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[8].ToString());
                    obj.PriceUnit = Reader[9].ToString();

                    al.Add(obj);
                }

                Reader.Close();

                return al;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// ���ҩƷ��Ϣ
        /// </summary>
        /// <param name="pactCode">��ͬ��λ</param>
        /// <param name="drugType">ҩƷ���</param>
        /// <returns></returns>
        public ArrayList GetNoCompareDrugItem(string pactCode, string drugType)
        {
            string strSql = "";
            string centerDrugType = "";

            //������ĿP ��ҩ Z �г�ҩ C ��ҩ

            //����ϵͳ����ȡ 
            if (drugType == "P")
            {
                drugType = "P"; //��ҩ��Ŀ
                centerDrugType = "X"; //��ҩ��Ŀ
            }
            else if (drugType == "C")
            {
                drugType = "PCC"; //�в�ҩ��Ŀ
                centerDrugType = "Z"; //�в�ҩ��Ŀ
            }
            else if (drugType == "Z")
            {
                drugType = "PCZ"; //�г�ҩ��Ŀ
                centerDrugType = "X"; //�г�ҩ��Ŀ
            }
            if (drugType == "ALL")			//����ҩƷ���
            {
                centerDrugType = "ALL";
            }

            if (this.Sql.GetCommonSql("Fee.Interface.GetNoCompareDrugItem.Select.1", ref strSql) == -1)
            {
                this.Err = Sql.Err;
                return null;
            }

            try
            {
                strSql = string.Format(strSql, pactCode, drugType, centerDrugType);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            try
            {

                ArrayList al = new ArrayList();
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    FS.HISFC.Models.Pharmacy.Item obj = new FS.HISFC.Models.Pharmacy.Item();

                    obj.ID = Reader[0].ToString();
                    obj.Name = Reader[1].ToString();
                    obj.SpellCode = Reader[2].ToString();
                    obj.WBCode = Reader[3].ToString();
                    obj.UserCode = Reader[4].ToString();
                    obj.NameCollection.FormalSpell.UserCode  = Reader[5].ToString();
                    obj.Specs = Reader[6].ToString();
                    obj.NameCollection.RegularName = Reader[7].ToString();
                    obj.NameCollection.RegularSpell.SpellCode = Reader[8].ToString();
                    obj.NameCollection.RegularSpell.WBCode = Reader[9].ToString();
                    obj.NameCollection.InternationalCode = Reader[10].ToString();
                    obj.GBCode = Reader[11].ToString();
                    obj.PriceCollection.RetailPrice = FS.FrameWork.Function.NConvert.ToDecimal(Reader[12].ToString());
                    obj.Type.ID = Reader[13].ToString();
                    obj.DosageForm.ID = Reader[14].ToString();

                    al.Add(obj);
                }

                Reader.Close();

                return al;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// ��ñ���ҽ��Ŀ¼��Ϣ
        /// </summary>
        /// <returns></returns>
        public int GetLocalSIItemCounts()
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Interface.GetLocalSIItemCounts.Select", ref strSql) == -1)
                return -1;

            if (this.ExecQuery(strSql) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "��ѯҽ����ĿĿ¼ʧ��!";
                return -1;
            }

            int count = 0;
            try
            {
                while (Reader.Read())
                {
                    count = FS.FrameWork.Function.NConvert.ToInt32(Reader[0].ToString());
                }

                Reader.Close();

                return count;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                return -1;
            }
        }
        /// <summary>
        /// ɾ������ҽ����Ϣ
        /// </summary>
        /// <returns></returns>
        public int DeleteSIItem()
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Interface.DeleteSIItem.Delete", ref strSql) == -1)
                return -1;

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

        }
        /// <summary>
        /// ����ҽ����Ŀ��Ϣ
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int InsertSIItem(FS.HISFC.Models.SIInterface.Item item)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Interface.InsertSIItem.Insert", ref strSql) == -1)
                return -1;

            //			try
            //			{   				
            //				strSql = string.Format(strSql, item.ID, item.SysClass, item.Name, item.EnglishName,
            //					item.Specs, item.DoseCode, item.SpellCode, item.FeeCode, item.ItemType, 
            //					item.ItemGrade, item.Rate.ToString(),this.Operator.ID);
            //
            //			}
            //			catch(Exception ex)
            //			{
            //				this.ErrCode = ex.Message;
            //				this.Err = ex.Message;
            //				return -1;
            //			}      			

            try
            {
                //{E5267BE7-0707-4780-AB86-266650C476C2} �޸�sql��䣬��pactCode����
                return this.ExecNoQuery(strSql, item.ID, item.SysClass, item.Name, item.EnglishName,
                    item.Specs, item.DoseCode, item.SpellCode, item.FeeCode, item.ItemType,
                    item.ItemGrade, item.Rate.ToString(), this.Operator.ID, item.PactCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        #endregion

        #region ҽ������ά��
        /// <summary>
        /// �������ά�������к�ͬ��λ��Ϣ;
        /// </summary>
        /// <returns></returns>
        public ArrayList GetAllPactInfo()
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Interface.Ruls.GetAllPactInfo.Select.1", ref strSql) == -1)
                return null;

            try
            {
                strSql = string.Format(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    Insurance obj = new Insurance();

                    obj.PactInfo.ID = Reader[0].ToString();
                    obj.PactInfo.Name = Reader[1].ToString();
                    obj.Kind.ID = Reader[2].ToString();

                    al.Add(obj);
                }

                Reader.Close();

                return al;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

        }
        /// <summary>
        /// ����ҽ��������Ϣ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertInsuranceDeal(Insurance obj)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Interface.InsertInsuranceDeal.1", ref strSql) == -1)
                return -1;

            try
            {
                strSql = string.Format(strSql, obj.PactInfo.ID, obj.Kind.ID, obj.PartId, obj.Rate,
                    obj.BeginCost, obj.EndCost, obj.Memo, obj.OperCode.ID);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// ����ҽ��������Ϣ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateInsuranceDeal(Insurance obj)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Interface.UpdateInsuranceDeal.1", ref strSql) == -1)
                return -1;

            try
            {
                strSql = string.Format(strSql, obj.PactInfo.ID, obj.Kind.ID, obj.PartId, obj.Rate,
                    obj.BeginCost, obj.EndCost, obj.Memo, obj.OperCode.ID);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// ɾ��ҽ��������Ϣ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int DeleteInsuranceDeal(Insurance obj)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Interface.DeleteInsuranceDeal.1", ref strSql) == -1)
                return -1;

            try
            {
                strSql = string.Format(strSql, obj.PactInfo.ID, obj.Kind.ID, obj.PartId);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// �õ�ָ����ͬ��λ,����µĹ�����Ϣ
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="kind"></param>
        /// <returns></returns>
        public ArrayList GetAllInsuranceInfo(string pactCode, string kind)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Interface.Ruls.GetAllInsuranceInfo.Select.1", ref strSql) == -1)
                return null;

            try
            {
                strSql = string.Format(strSql, pactCode, kind);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    Insurance obj = new Insurance();

                    //					obj.PactInfo.ID = Reader[0].ToString();
                    //obj.PactInfo.Name = Reader[1].ToString();
                    //obj.Kind.ID = Reader[2].ToString();
                    obj.PartId = Reader[0].ToString();
                    obj.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[1].ToString());
                    obj.BeginCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[2].ToString());
                    obj.EndCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[3].ToString());
                    obj.Memo = Reader[4].ToString();
                    obj.OperCode.ID = Reader[5].ToString();
                    obj.OperDate = Convert.ToDateTime(Reader[6].ToString());

                    al.Add(obj);
                }

                Reader.Close();

                return al;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

        }

        #endregion

        #region ������ά��
        /// <summary>
        /// �����������Ϣ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertBlackList(BlackList obj)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Interface.InsertBlackList.1", ref strSql) == -1)
                return -1;

            try
            {
                strSql = string.Format(strSql, obj.MCardNo, obj.Kind, obj.Name, obj.ValidState,
                    obj.OperInfo.ID);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// ���º�������Ϣ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateBlackList(BlackList obj)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Interface.UpdateBlackList.1", ref strSql) == -1)
                return -1;

            try
            {
                strSql = string.Format(strSql, obj.ID, obj.MCardNo, obj.Kind, obj.Name, obj.ValidState,
                    obj.OperInfo.ID, obj.OperDate);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// ɾ����������Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mCardNo"></param>
        /// <returns></returns>
        public int DeleteBlackList(string id, string mCardNo)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Interface.DeleteBlackList.1", ref strSql) == -1)
                return -1;

            try
            {
                strSql = string.Format(strSql, id, mCardNo);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// ͨ����������������к������б�
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="validState"></param>
        /// <returns></returns>
        public ArrayList GetBlackListFromKind(string kind, string validState)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Interface.GetBlackListFromKind.Select.1", ref strSql) == -1)
                return null;

            try
            {
                strSql = string.Format(strSql, kind, validState);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    BlackList obj = new BlackList();

                    obj.ID = Reader[0].ToString();
                    obj.MCardNo = Reader[1].ToString();
                    obj.Kind = Reader[2].ToString();
                    obj.Name = Reader[3].ToString();
                    obj.ValidState = Reader[4].ToString();
                    obj.OperInfo.ID = Reader[5].ToString();
                    obj.OperDate = Convert.ToDateTime(Reader[6].ToString());

                    al.Add(obj);
                }

                Reader.Close();

                return al;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// �жϻ��ߵõ�λ������Ƿ���ں�������
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="mCardNo"></param>
        /// <returns></returns>
        public bool ExistBlackList(string pactCode, string mCardNo)
        {
            string strSql = "";
            string strSql2 = "";
            int pactCount = 0;
            int personCount = 0;

            if (this.Sql.GetCommonSql("Fee.Interface.ExistBlackList.Select.1", ref strSql) == -1)
                return false;

            try
            {
                strSql = string.Format(strSql, pactCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return false;
            }
            try
            {

                pactCount = FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strSql));

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return false;
            }

            if (this.Sql.GetCommonSql("Fee.Interface.ExistBlackList.Select.2", ref strSql2) == -1)
                return false;

            try
            {
                strSql = string.Format(strSql2, pactCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return false;
            }
            try
            {

                personCount = FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strSql2));

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return false;
            }

            if (pactCount + personCount > 0)
                return true;
            else
                return false;

        }


        #endregion

        #region ҽ���ӿڼ���
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="pactCode">��ͬ��λ����</param>
        /// <param name="item">����ʵ��</param>
        /// <returns></returns>
        public int ComputeRate(string pactCode, ref FS.HISFC.Models.Fee.Inpatient.FeeItemList item)
        {
            int returnValue = 0;

            if (IsSplitFeeFlag(item))
            {
                item.FT.OwnCost = item.FT.TotCost;
                item.FT.PubCost = 0;
                item.FT.PayCost = 0;
                item.FT.DerateCost = 0;
            }
            else
            {


                Compare objCompare = new Compare();
                objCompare.CenterItem.Rate = 1;

                returnValue = GetCompareSingleItem(pactCode, item.Item.ID, ref objCompare);

                if (returnValue == -1)
                    return returnValue;
                if (returnValue == -2)
                    objCompare.CenterItem.Rate = 1;

                decimal decDefCost = 0;
                if (item.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    decDefCost = item.FT.TotCost;
                }
                else
                {
                    decDefCost = item.FT.DefTotCost;
                }
                if (isSplitFee && item.Item.DefPrice > 0 && decDefCost > 0 && decDefCost != item.FT.TotCost)
                {
                    //�����������
                    item.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(decDefCost * objCompare.CenterItem.Rate, 2);
                    item.FT.PubCost = decDefCost - item.FT.OwnCost;
                    item.FT.OwnCost = item.FT.TotCost - item.FT.PubCost;
                    item.FT.PayCost = 0;
                    item.FT.DerateCost = 0;//���㲻���Ǽ�����

                }
                else
                {
                    item.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber(item.FT.TotCost * objCompare.CenterItem.Rate, 2);
                    item.FT.PubCost = item.FT.TotCost - item.FT.OwnCost;
                    item.FT.PayCost = 0;
                    item.FT.DerateCost = 0;//���㲻���Ǽ�����
                }
            }

            return 0;
        }
        /// <summary>
        /// �õ�������Ϣ
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetItemCompareInfo(string pactCode, FS.HISFC.Models.Fee.Inpatient.FeeItemList obj)
        {
            return 0;
        }

        #endregion

        #region ҽ���޶�ά��
        /// <summary>
        /// ��������·ݵĿ����޶���Ϣ
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public ArrayList GetDeptSICostFromMonth(string month)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Interface.GetDeptSICostFromMonth.Select.1", ref strSql) == -1)
                return null;

            try
            {
                strSql = string.Format(strSql, month);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    DeptSICost obj = new DeptSICost();

                    obj.ID = Reader[0].ToString();
                    obj.Month = Reader[1].ToString();
                    obj.Name = Reader[2].ToString();
                    obj.AlertMoney = FS.FrameWork.Function.NConvert.ToDecimal(Reader[3].ToString());
                    obj.ValidStateId = Reader[4].ToString();
                    obj.SortId = FS.FrameWork.Function.NConvert.ToInt32(Reader[5].ToString());
                    obj.OperInfo.ID = Reader[6].ToString();
                    obj.OperDate = Convert.ToDateTime(Reader[7].ToString());
                    obj.SpellCode.SpellCode = Reader[8].ToString();

                    al.Add(obj);
                }

                Reader.Close();

                return al;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// ����δά����ҽ���޶�Ŀ�����Ϣ,������Ĭ��Ϊ0; sortIdĬ��Ϊ10000
        /// </summary>
        /// <param name="month"></param>
        /// <param name="operCode"></param>
        /// <returns></returns>
        public int InsertNoExistDeptInfo(string month, string operCode)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Interface.InsertNoExistDeptInfo.1", ref strSql) == -1)
                return -1;

            try
            {
                strSql = string.Format(strSql, month, operCode);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// ����ҽ���޶�
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateDeptSICost(DeptSICost obj)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Interface.UpdateDeptSICost.1", ref strSql) == -1)
                return -1;

            try
            {
                strSql = string.Format(strSql, obj.ID, obj.Month, obj.Name, obj.AlertMoney, obj.ValidStateId,
                    obj.SortId, obj.OperInfo.ID);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// ���ĳ����ĳ�µ��޶�
        /// </summary>
        /// <param name="month"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public DeptSICost GetSingleDeptSICost(string month, string deptCode)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Interface.GetSingleDeptSICost.Select.1", ref strSql) == -1)
                return null;

            try
            {
                strSql = string.Format(strSql, month, deptCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }

            try
            {
                ArrayList al = new ArrayList();
                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    DeptSICost obj = new DeptSICost();

                    obj.ID = Reader[2].ToString();
                    obj.Month = Reader[3].ToString();
                    obj.Name = Reader[4].ToString();
                    obj.AlertMoney = FS.FrameWork.Function.NConvert.ToDecimal(Reader[5].ToString());
                    obj.ValidStateId = Reader[6].ToString();
                    obj.SortId = FS.FrameWork.Function.NConvert.ToInt32(Reader[7].ToString());
                    obj.OperInfo.ID = Reader[8].ToString();
                    obj.OperDate = Convert.ToDateTime(Reader[9].ToString());
                    al.Add(obj);
                }

                Reader.Close();

                if (al.Count > 0)
                    return (DeptSICost)al[0];
                else
                {
                    this.Err = month + "�Ŀ����޶�û��ά��,��֪ͨ��Ϣ�ƣ�";
                    return null;
                }
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
        }
        #endregion

        #region ҽ�������

        /// <summary>
        /// �õ��������
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public string GetBalNo(string inpatientNo)
        {
            string strSql = "";
            string balNo = "";
            if (this.Sql.GetCommonSql("Fee.Interface.GetBalNo.1", ref strSql) == -1)
                return "";
            try
            {
                strSql = string.Format(strSql, inpatientNo);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
            this.ExecQuery(strSql);
            try
            {
                while (Reader.Read())
                {
                    balNo = Reader[0].ToString();
                }
                Reader.Close();
                return balNo;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return "";
            }
        }

        /// <summary>
        /// ����ҽ��������Ϣ��
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertSIMainInfo(FS.HISFC.Models.RADT.PatientInfo obj)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Fee.Interface.InsertSIMainInfo.1", ref strSql) == -1)
                return -1;

            try
            {
                strSql = string.Format(strSql, 
                            obj.ID, //0
                            obj.SIMainInfo.BalNo, 
                            obj.SIMainInfo.InvoiceNo, 
                            obj.PVisit.MedicalType.ID, 
                            obj.PID.PatientNO,
                            obj.PID.CardNO, 
                            obj.SSN, 
                            obj.SIMainInfo.AppNo, 
                            obj.SIMainInfo.ProceatePcNo,
                            obj.SIMainInfo.SiBegionDate.ToString(), 
                            obj.SIMainInfo.SiState, 
                            obj.Name, 
                            obj.Sex.ID.ToString(),
                            obj.IDCard, 
                            "", //14
                            obj.Birthday.ToString(), 
                            obj.SIMainInfo.EmplType, 
                            obj.CompanyName,
                            obj.SIMainInfo.InDiagnose.Name, 
                            obj.PVisit.PatientLocation.Dept.ID, 
                            obj.PVisit.PatientLocation.Dept.Name,
                            obj.Pact.PayKind.ID,
                            obj.Pact.ID, 
                            obj.Pact.Name, 
                            obj.PVisit.PatientLocation.Bed.ID,
                            obj.PVisit.InTime.ToString(), 
                            obj.PVisit.InTime.ToString(), 
                            obj.SIMainInfo.InDiagnose.ID,
                            obj.SIMainInfo.InDiagnose.Name, 
                            this.Operator.ID, 
                            obj.SIMainInfo.HosNo, //30
                            obj.SIMainInfo.RegNo,
                            obj.SIMainInfo.FeeTimes, 
                            obj.SIMainInfo.HosCost, 
                            obj.SIMainInfo.YearCost,
                            obj.PVisit.OutTime.ToString(),
                            obj.SIMainInfo.OutDiagnose.ID, 
                            obj.SIMainInfo.OutDiagnose.Name, 
                            obj.SIMainInfo.BalanceDate.ToString(),
                            obj.SIMainInfo.TotCost, 
                            obj.SIMainInfo.PayCost, 
                            obj.SIMainInfo.PubCost, 
                            obj.SIMainInfo.ItemPayCost,
                            obj.SIMainInfo.BaseCost, 
                            obj.SIMainInfo.ItemYLCost, 
                            obj.SIMainInfo.PubOwnCost,//45 
                            obj.SIMainInfo.OwnCost,
                            obj.SIMainInfo.OverTakeOwnCost, 
                            FS.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsValid),
                            FS.FrameWork.Function.NConvert.ToInt32(obj.SIMainInfo.IsBalanced), 
                            obj.SIMainInfo.TypeCode//50
                            );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            try
            {
                return this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// �õ�ҽ�����߻�����Ϣ;
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.RADT.PatientInfo GetSIPersonInfo(string inpatientNo)
        {
            FS.HISFC.Models.RADT.PatientInfo obj = new FS.HISFC.Models.RADT.PatientInfo();
            string strSql = "";
            string balNo = this.GetBalNo(inpatientNo);
            if (balNo == "")
                return null;
            if (this.Sql.GetCommonSql("Fee.Interface.GetSIPersonInfo.Select.1", ref strSql) == -1)
                return null;
            try
            {
                strSql = string.Format(strSql, inpatientNo, balNo);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return null;
            }
            this.ExecQuery(strSql);
            try
            {
                while (Reader.Read())
                {

                    obj.SIMainInfo.HosNo = Reader[0].ToString();
                    obj.ID = Reader[1].ToString();
                    obj.SIMainInfo.BalNo = Reader[2].ToString();
                    obj.SIMainInfo.InvoiceNo = Reader[3].ToString();
                    obj.SIMainInfo.MedicalType.ID = Reader[4].ToString();
                    if (obj.SIMainInfo.MedicalType.ID == "1")
                        obj.SIMainInfo.MedicalType.Name = "סԺ";
                    else
                        obj.SIMainInfo.MedicalType.Name = "�����ض���Ŀ";
                    obj.PID.PatientNO = Reader[5].ToString();
                    obj.PID.CardNO = Reader[6].ToString();
                    obj.SSN = Reader[7].ToString();
                    obj.SIMainInfo.AppNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[8].ToString());
                    obj.SIMainInfo.ProceatePcNo = Reader[9].ToString();
                    obj.SIMainInfo.SiBegionDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[10].ToString());
                    obj.SIMainInfo.SiState = Reader[11].ToString();
                    obj.Name = Reader[12].ToString();
                    obj.Sex.ID = Reader[13].ToString();
                    obj.IDCard = Reader[14].ToString();
                    obj.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(Reader[15].ToString());
                    obj.SIMainInfo.EmplType = Reader[16].ToString();
                    obj.CompanyName = Reader[17].ToString();
                    obj.SIMainInfo.InDiagnose.Name = Reader[18].ToString();
                    obj.PVisit.PatientLocation.Dept.ID = Reader[19].ToString();
                    obj.PVisit.PatientLocation.Dept.Name = Reader[20].ToString();
                    obj.Pact.PayKind.ID = Reader[21].ToString();
                    obj.Pact.ID = Reader[22].ToString();
                    obj.Pact.Name = Reader[23].ToString();
                    obj.PVisit.PatientLocation.Bed.ID = Reader[24].ToString();
                    obj.PVisit.InTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[25].ToString());
                    obj.SIMainInfo.InDiagnoseDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[25].ToString());
                    obj.SIMainInfo.InDiagnose.ID = Reader[26].ToString();
                    obj.SIMainInfo.InDiagnose.Name = Reader[27].ToString();
                    if (!Reader.IsDBNull(28))
                        obj.PVisit.OutTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[28].ToString());
                    obj.SIMainInfo.OutDiagnose.ID = Reader[29].ToString();
                    obj.SIMainInfo.OutDiagnose.Name = Reader[30].ToString();
                    if (!Reader.IsDBNull(31))
                        obj.SIMainInfo.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[31].ToString());

                    obj.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[32].ToString());
                    obj.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[33].ToString());
                    obj.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[34].ToString());
                    obj.SIMainInfo.ItemPayCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[35].ToString());
                    obj.SIMainInfo.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[36].ToString());
                    obj.SIMainInfo.PubOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[37].ToString());
                    obj.SIMainInfo.ItemYLCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[38].ToString());
                    obj.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[39].ToString());
                    obj.SIMainInfo.OverTakeOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[40].ToString());
                    obj.SIMainInfo.Memo = Reader[41].ToString();
                    obj.SIMainInfo.OperInfo.ID = Reader[42].ToString();
                    obj.SIMainInfo.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[43].ToString());
                    obj.SIMainInfo.RegNo = Reader[44].ToString();
                    obj.SIMainInfo.FeeTimes = FS.FrameWork.Function.NConvert.ToInt32(Reader[45].ToString());
                    obj.SIMainInfo.HosCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[46].ToString());
                    obj.SIMainInfo.YearCost = FS.FrameWork.Function.NConvert.ToDecimal(Reader[47].ToString());
                    obj.SIMainInfo.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(Reader[48].ToString());
                    obj.SIMainInfo.IsBalanced = FS.FrameWork.Function.NConvert.ToBoolean(Reader[49].ToString());
                }
                Reader.Close();
                return obj;
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                Reader.Close();
                return null;
            }
        }
        /// <summary>
        /// ����ҽ������������Ϣ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateSiMainInfo(FS.HISFC.Models.RADT.PatientInfo pInfo)
        {
            string strSql = "";
            string balNo = this.GetBalNo(pInfo.ID);
            if (this.Sql.GetCommonSql("Fee.Interface.UpdateSiMainInfo.Update.1", ref strSql) == -1)
                return -1;
            try
            {
                strSql = string.Format(strSql, 
                                pInfo.ID, 
                                pInfo.SIMainInfo.BalNo, 
                                pInfo.SIMainInfo.InvoiceNo,  //2
                                pInfo.PVisit.MedicalType.ID, 
                                pInfo.PID.PatientNO,
                                pInfo.PID.CardNO, 
                                pInfo.SSN, 
                                pInfo.SIMainInfo.AppNo,
                                pInfo.SIMainInfo.ProceatePcNo,
                                pInfo.SIMainInfo.SiBegionDate.ToString(), 
                                pInfo.SIMainInfo.SiState, //10
                                pInfo.Name, 
                                pInfo.Sex.ID.ToString(),
                                pInfo.IDCard, 
                                "", //14 ƴ��
                                pInfo.Birthday.ToString(), 
                                pInfo.SIMainInfo.EmplType, 
                                pInfo.CompanyName,
                                pInfo.SIMainInfo.InDiagnose.Name, 
                                pInfo.PVisit.PatientLocation.Dept.ID, 
                                pInfo.PVisit.PatientLocation.Dept.Name,
                                pInfo.Pact.PayKind.ID, 
                                pInfo.Pact.ID, 
                                pInfo.Pact.Name, //23
                                pInfo.PVisit.PatientLocation.Bed.ID,
                                pInfo.PVisit.InTime.ToString(), 
                                pInfo.PVisit.InTime.ToString(), 
                                pInfo.SIMainInfo.InDiagnose.ID,
                                pInfo.SIMainInfo.InDiagnose.Name, 
                                pInfo.PVisit.OutTime, 
                                pInfo.SIMainInfo.OutDiagnose.ID, //30
                                pInfo.SIMainInfo.OutDiagnose.Name,
                                pInfo.SIMainInfo.BalanceDate.ToString(),
                                pInfo.SIMainInfo.TotCost, 
                                pInfo.SIMainInfo.PayCost, 
                                pInfo.SIMainInfo.PubCost,
                                pInfo.SIMainInfo.ItemPayCost, 
                                pInfo.SIMainInfo.BaseCost, 
                                pInfo.SIMainInfo.PubOwnCost, 
                                pInfo.SIMainInfo.ItemYLCost,
                                pInfo.SIMainInfo.OwnCost, //40
                                pInfo.SIMainInfo.OverTakeOwnCost, 
                                pInfo.SIMainInfo.Memo, 
                                this.Operator.ID,
                                pInfo.SIMainInfo.RegNo, 
                                pInfo.SIMainInfo.FeeTimes, 
                                pInfo.SIMainInfo.HosCost, 
                                pInfo.SIMainInfo.YearCost,
                                FS.FrameWork.Function.NConvert.ToInt32(pInfo.SIMainInfo.IsValid), 
                                FS.FrameWork.Function.NConvert.ToInt32(pInfo.SIMainInfo.IsBalanced),//49�Ƿ����
                                pInfo.SIMainInfo.OverCost,//����
                                pInfo.SIMainInfo.OfficalCost //����Ա����
                                );
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);

        }
        #endregion

        #region ��ʱ

        #endregion

        #region ҽ��Ԥ����
        /// <summary>
        /// ����ҽ������Ԥ���������Ϣ
        /// </summary>
        /// <param name="InpatientNo">סԺ��ˮ��</param>
        /// <returns>-1ʧ�ܣ�0�ɹ�</returns>
        public int UpdateSICostForPreBalance(string InpatientNo)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Fee.Interface.UpdateSICostForPreBalance", ref strSql) == -1)
            {
                this.Err = "û���ҵ� Fee.Interface.UpdateSICostForPreBalance �ֶ�!";
                this.ErrCode = "-1";
                return -1;
            }
            decimal owncost = 0m;
            decimal pubcost = 0m;
            decimal realOwncost = 0m;
            decimal realPubcost = 0m;
            string Error = "";
            if (this.ExecutePackage(InpatientNo, ref owncost, ref pubcost, ref realOwncost, ref realPubcost, ref Error) == -1)
            {
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, InpatientNo, owncost.ToString(), pubcost.ToString(),
                    realOwncost.ToString());
                this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// ҽ��Ԥ����
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="myFee"></param>
        /// <param name="refFT"></param>
        /// <returns></returns>
        public bool CalculateSiFee(FS.HISFC.Models.RADT.PatientInfo obj, FS.HISFC.Models.Fee.Inpatient.FeeItemList myFee, FS.HISFC.Models.Base.FT refFT)
        {
            FS.HISFC.Models.RADT.PatientInfo myObj = GetSIPersonInfo(obj.ID);
            if (myObj == null)
            {
                this.Err = "��û�����Ϣ����!";
                return false;
            }
            if (!this.Calculate(myObj, myFee, refFT))
            {
                this.Err = "Ԥ�������ʧ��!";
                return false;
            }
            if (UpdateSiMainInfo(myObj) == -1)
            {
                this.Err = "����ҽ�����߽����������!";
                return false;
            }

            obj.SIMainInfo = myObj.SIMainInfo;

            return true;
        }

        #region ִ�д洢����
        /// <summary>
        /// 1.���ҽ�����߼�����Ŀ��������Է��ܶ��ͳ���ܶ� 2.����ҽ�������㷨�����������ԷѺ�ͳ�����   
        /// </summary>
        /// <param name="inpatient_no">����סԺ��ˮ��</param>
        /// <param name="ownCost">�Էѽ��</param>
        /// <param name="pubCost">���ѽ��</param>
        /// <param name="realOwnCost">����������Էѽ��</param>
        /// <param name="realPubCost">��������󹫷ѽ��</param>
        /// <param name="Error">������Ϣ</param>
        /// <returns>-1 �������ݿ�ʧ�� 0 �ɹ� </returns>
        public int ExecutePackage(string inpatient_no, ref decimal ownCost, ref decimal pubCost, ref decimal realOwnCost, ref decimal realPubCost, ref string Error)
        {
            //�����ַ��� �洢SQL���
            string strSql = "";
            string strReturn = "";
            int iReturn = 0;
            //��ȡSQL���
            if (this.Sql.GetCommonSql("RADT.Inpatient.ExecutePackage.ComputeSICost", ref strSql) == -1)
            {
                this.Err = "û���ҵ� RADT.Inpatient.ExecutePackage �ֶ�!";
                this.ErrCode = "-1";
                return -1;
            }
            //��ʽ���ַ���
            strSql = string.Format(strSql, inpatient_no, "1", "1", "1", "1", "1", "1");

            if (this.ExecEvent(strSql, ref strReturn) == -1)
            {
                this.Err = "ִ�д洢���̳���!PRC_PRE_BALANCE";
                this.ErrCode = "PRC_PRE_BALANCE";
                this.WriteErr();
                return -1;
            }

            string[] s = strReturn.Split(',');

            try
            {
                ownCost = FS.FrameWork.Public.String.FormatNumber(FS.FrameWork.Function.NConvert.ToDecimal(s[0]), 2);
                pubCost = FS.FrameWork.Public.String.FormatNumber(FS.FrameWork.Function.NConvert.ToDecimal(s[1]), 2);
                realOwnCost = FS.FrameWork.Public.String.FormatNumber(FS.FrameWork.Function.NConvert.ToDecimal(s[2]), 2);
                realPubCost = FS.FrameWork.Public.String.FormatNumber(FS.FrameWork.Function.NConvert.ToDecimal(s[3]), 2);
                iReturn = FS.FrameWork.Function.NConvert.ToInt32(s[4]);
                Error = s[5];
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err += ex.Message;
                return -1;
            }
            return iReturn;
        }
        /// <summary>
        /// �������ã�1.���ҽ�����߼�����Ŀ��������Է��ܶ��ͳ���ܶ� 2.����ҽ�������㷨�����������ԷѺ�ͳ�����   
        /// </summary>
        /// <param name="inpatient_no">����סԺ��ˮ��</param>
        /// <param name="ownCost">�Էѽ��</param>
        /// <param name="pubCost">���ѽ��</param>
        /// <param name="realOwnCost">����������Էѽ��</param>
        /// <param name="realPubCost">��������󹫷ѽ��</param>
        /// <param name="drugOwnCost">�Է�ҩƷ</param>
        /// <param name="drugPubCost">ͳ��ҩƷ</param>
        /// <param name="Error">������Ϣ</param>
        /// <returns>-1 �������ݿ�ʧ�� 0 �ɹ�</returns>
        public int ExecutePackage(string inpatient_no, ref decimal ownCost, ref decimal pubCost, ref decimal realOwnCost, ref decimal realPubCost, ref decimal drugOwnCost, ref decimal drugPubCost, ref string Error)
        {
            //�����ַ��� �洢SQL���
            string strSql = "";
            string strReturn = "";
            int iReturn = 0;
            //��ȡSQL���
            if (this.Sql.GetCommonSql("RADT.Inpatient.ExecutePackage.ComputeSICost.2", ref strSql) == -1)
            {
                this.Err = "û���ҵ� RADT.Inpatient.ExecutePackage �ֶ�!";
                this.ErrCode = "-1";
                return -1;
            }
            //��ʽ���ַ���
            strSql = string.Format(strSql, inpatient_no, "1", "1", "1", "1", "1", "1", "1", "1");

            if (this.ExecEvent(strSql, ref strReturn) == -1)
            {
                this.Err = "ִ�д洢���̳���!PRC_PRE_BALANCE";
                this.ErrCode = "PRC_PRE_BALANCE";
                this.WriteErr();
                return -1;
            }

            string[] s = strReturn.Split(',');

            try
            {
                ownCost = FS.FrameWork.Public.String.FormatNumber(FS.FrameWork.Function.NConvert.ToDecimal(s[0]), 2);
                pubCost = FS.FrameWork.Public.String.FormatNumber(FS.FrameWork.Function.NConvert.ToDecimal(s[1]), 2);
                realOwnCost = FS.FrameWork.Public.String.FormatNumber(FS.FrameWork.Function.NConvert.ToDecimal(s[2]), 2);
                realPubCost = FS.FrameWork.Public.String.FormatNumber(FS.FrameWork.Function.NConvert.ToDecimal(s[3]), 2);
                drugOwnCost = FS.FrameWork.Public.String.FormatNumber(FS.FrameWork.Function.NConvert.ToDecimal(s[4]), 2);
                drugPubCost = FS.FrameWork.Public.String.FormatNumber(FS.FrameWork.Function.NConvert.ToDecimal(s[5]), 2);

                iReturn = FS.FrameWork.Function.NConvert.ToInt32(s[6]);
                Error = s[7];
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err += ex.Message;
                return -1;
            }
            return iReturn;
        }

        #endregion

        /// <summary>
        /// ҽ��Ԥ����
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="myFeeItemList"></param>
        /// <param name="refFT"></param>
        /// <returns></returns>
        public bool Calculate(FS.HISFC.Models.RADT.PatientInfo obj, FS.HISFC.Models.Fee.Inpatient.FeeItemList myFeeItemList, FS.HISFC.Models.Base.FT refFT)
        {
            if (myFeeItemList == null || myFeeItemList.FT == null)
            {
                return true;
            }

            if (obj.SIMainInfo == null)
            {
                //m_error = "������Ϣ��Ч��";
                return false;
            }

            //m_error = "";


            //�ۼ�ҽ�Ʒ���

            obj.SIMainInfo.TotCost += myFeeItemList.FT.TotCost;	//�ϴ��ܽ��


            if (myFeeItemList.FT.TotCost == myFeeItemList.FT.OwnCost)
            {
                obj.SIMainInfo.OwnCost += myFeeItemList.FT.OwnCost;//�����Է���Ŀ���
            }
            else
            {
                obj.SIMainInfo.ItemYLCost += myFeeItemList.FT.OwnCost;//�����Ը��������Ը����֣�
            }



            //��ȡ������Ա��ҽ��֧�������б�
            string pactCode = obj.Pact.ID;		//��ͬ��λ����
            string emplType = obj.SIMainInfo.EmplType;	//��Ա���,1��ְ..

            // �ֶ�֧�������б�

            ArrayList rateList = this.GetAllInsuranceInfo(pactCode, obj.SIMainInfo.EmplType);

            if (rateList.Count == 0)
            {
                //m_error = "ҽ��֧�������б���Ч��";
                return false;
            }


            // ����ҽ��ͳ���ܷ���
            decimal nrzfy = obj.SIMainInfo.TotCost - obj.SIMainInfo.OwnCost - obj.SIMainInfo.ItemYLCost;

            decimal zje1 = 0;
            decimal zje2 = 0;
            decimal bfb = 0;
            decimal gffy1 = 0;
            decimal gffy2 = 0;

            decimal[] zf_fy = new decimal[rateList.Count];	//��Ÿ������Ը����

            for (int i = 0; i < rateList.Count; i++)
            {
                zf_fy[i] = 0;
            }


            // ��������ҽ��ͳ���ܷ��ü��������ͳ��/����֧������
            for (int i = 0; i < rateList.Count; i++)
            {
                FS.HISFC.Models.SIInterface.Insurance rate = (FS.HISFC.Models.SIInterface.Insurance)rateList[i];

                zje1 = zje2;
                gffy1 = gffy2;

                zje2 = rate.EndCost;	//�޶�	(decimal)DbAssist.GetItemNumber(drv,"zje",0);
                bfb = rate.Rate * 100;		//����	(decimal)DbAssist.GetItemNumber(drv,"zfbl",100);

                if (nrzfy <= gffy1) break;

                if (bfb == 100)
                {
                    if (zje2 == 0)
                    {
                        zf_fy[i] += (nrzfy - gffy1);
                    }
                    else
                    {
                        if (nrzfy >= zje2)
                        {
                            zf_fy[i] += zje2;
                        }
                        else
                        {
                            zf_fy[i] += nrzfy;
                        }
                    }

                    gffy2 = zf_fy[i]; // needed
                }
                else
                {
                    gffy2 = zje2 / (100 - bfb) * 100 + gffy1;
                    if (nrzfy < gffy2) gffy2 = nrzfy;

                    zf_fy[i] += (gffy2 - gffy1) * bfb / 100;
                }
            }

            ////////////////////////////////////////////////////////////////

            decimal ybzf = 0;	//ҽ���Ը���� = �𸶲��� + �����Ը����� + ���޶��Ը�����

            for (int i = 0; i < rateList.Count; i++)
            {
                ybzf += zf_fy[i];
            }

            //ҽ������֧����� = ҽ�Ʒ��� - �ԷѲ��� - �Ը�����
            obj.SIMainInfo.PubCost = obj.SIMainInfo.TotCost - obj.SIMainInfo.OwnCost - obj.SIMainInfo.ItemYLCost - ybzf;


            //obj.SIMainInfo.ItemPayCost	= 0;//������Ŀ�Ը����

            obj.SIMainInfo.BaseCost = (rateList.Count > 0) ? zf_fy[0] : 0;	//�����𸶽��
            obj.SIMainInfo.PubOwnCost = (rateList.Count > 1) ? zf_fy[1] : 0;	//�����Ը����

            obj.SIMainInfo.OverTakeOwnCost = 0;				//��ͳ��֧���޶�����Ը����

            for (int i = 2; i < rateList.Count; i++)
            {
                obj.SIMainInfo.OverTakeOwnCost += zf_fy[i];	//��ͳ��֧���޶�����Ը����
            }



            refFT.TotCost = obj.SIMainInfo.TotCost;
            refFT.OwnCost = obj.SIMainInfo.TotCost - obj.SIMainInfo.PubCost;
            refFT.PubCost = obj.SIMainInfo.PubCost;

            return true;

        }

        #endregion

        #region ҽ������ϸ
        /// <summary>
        /// ���ҽ������Ҫ���ݵ���ϸ��Ϣ
        /// </summary>
        /// <param name="pactCode"></param>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public ArrayList GetSIPersonDetail(string pactCode, string inpatientNo)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Fee.Interface.GetSIPersonDetail.Select.1", ref strSql) == -1)
                return null;
            try
            {
                strSql = string.Format(strSql, pactCode, inpatientNo);
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }
            this.ExecQuery(strSql);
            try
            {
                ArrayList al = new ArrayList();

                while (Reader.Read())
                {
                    Compare obj = new Compare();
                    if (!Reader.IsDBNull(0))
                        obj.CenterItem.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[0].ToString());
                    obj.ID = Reader[1].ToString();
                    obj.Name = Reader[2].ToString();
                    obj.CenterItem.FeeCode = Reader[3].ToString();
                    obj.Specs = Reader[4].ToString();
                    obj.DoseCode = Reader[5].ToString();
                    //����
                    obj.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[6].ToString());
                    //����
                    obj.CenterItem.Rate = FS.FrameWork.Function.NConvert.ToDecimal(Reader[7].ToString());
                    //���
                    obj.CenterItem.Price = FS.FrameWork.Function.NConvert.ToDecimal(Reader[8].ToString());

                    al.Add(obj);
                }

                Reader.Close();

                return al;
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = ex.Message;
                this.WriteErr();
                Reader.Close();
                return null;
            }
        }
        /// <summary>
        /// ��д���ϴ���־
        /// </summary>
        /// <param name="noteNo">������</param>
        /// <param name="seqNo">������ˮ��</param>
        /// <param name="flag">��Ŀ���, 1 ҩƷ 2 ��ҩƷ</param>
        /// <returns>-1���ݿ����ʧ��, 0 û���ҵ���(����), 1 �ɹ�</returns>
        public int UpdateUploadedDetailFlag(string noteNo, int seqNo, string flag)
        {
            string strSQL = "";

            if (flag == "1") //ҩƷ
            {
                if (this.Sql.GetCommonSql("Fee.Interface.UpdateUploadedDetailFlag.Drug", ref strSQL) == -1)
                {
                    this.ErrCode = "-1";
                    this.Err = "��ø���ҩƷ�ϴ�SQL������!";
                    return -1;
                }

            }
            else if (flag == "2")//��ҩƷ
            {
                if (this.Sql.GetCommonSql("Fee.Interface.UpdateUploadedDetailFlag.Undrug", ref strSQL) == -1)
                {
                    this.ErrCode = "-1";
                    this.Err = "��ø��·�ҩƷ�ϴ�SQL������!";
                    return -1;
                }
            }
            else
            {
                return 0;
            }

            try
            {
                strSQL = string.Format(strSQL, noteNo, seqNo);
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }
        /// <summary>
        /// ����ҽ��Ҫ�ϴ�����ϸ��Ϣ
        /// </summary>
        /// <param name="inpatientNo">סԺ��ˮ��</param>
        /// <param name="flag">���±�� 0 û���ϴ� 1 �Ѿ��ϴ�</param>
        /// <returns>-1����ʧ�� 0 û�м�¼ >=1 ���³ɹ�</returns>
        public int UpdateAllDetailFlag(string inpatientNo, string flag)
        {
            string strSqlDrug = "", strSqlItem = "";
            int iReturnDrug = 0, iReturnItem = 0;
            if (this.Sql.GetCommonSql("Fee.Interface.UpdateAllDetailFlag.Drug", ref strSqlDrug) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "��ø���ҩƷ�ϴ�SQL������!";
                return -1;
            }
            if (this.Sql.GetCommonSql("Fee.Interface.UpdateAllDetailFlag.UnDrug", ref strSqlItem) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "��ø��·�ҩƷ�ϴ�SQL������!";
                return -1;
            }
            if (FS.FrameWork.Public.String.FormatString(strSqlDrug, out strSqlDrug, inpatientNo, flag) == -1)
            {
                this.Err += "ҩƷSQL��为ֵ����!";
                return -1;
            }
            if (FS.FrameWork.Public.String.FormatString(strSqlItem, out strSqlItem, inpatientNo, flag) == -1)
            {
                this.Err += "��ҩƷSQL��为ֵ����!";
                return -1;
            }
            iReturnDrug = this.ExecNoQuery(strSqlDrug);
            if (iReturnDrug < 0)
            {
                return iReturnDrug;
            }
            iReturnItem = this.ExecNoQuery(strSqlItem);
            if (iReturnItem < 0)
            {
                return iReturnItem;
            }

            return iReturnItem + iReturnDrug;
        }
        /// <summary>
        /// ���²��ϴ�����Ŀ���ϴ����
        /// </summary>
        /// <param name="inpatientNo">סԺ��ˮ��</param>
        /// <param name="condition">���ϴ���Ŀ�б�</param>
        /// <param name="flag">0 ��Ҫ�ϴ� 3 ���ϴ�</param>
        /// <returns>-1����ʧ�� 0 û�м�¼ >=1 ���³ɹ�</returns>
        public int UpdateFlagForNotUpload(string inpatientNo, string condition, string flag)
        {
            string strSqlDrug = "", strSqlItem = "";
            int iReturnDrug = 0, iReturnItem = 0;
            if (this.Sql.GetCommonSql("Fee.Interface.UpdateFlagForNotUpload.Drug", ref strSqlDrug) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "��ø��²��ϴ�ҩƷSQL������!";
                return -1;
            }
            if (this.Sql.GetCommonSql("Fee.Interface.UpdateFlagForNotUpload.UnDrug", ref strSqlItem) == -1)
            {
                this.ErrCode = "-1";
                this.Err = "��ø��²��ϴ���ҩƷSQL������!";
                return -1;
            }
            //			if(FS.FrameWork.Public.String.FormatString(strSqlDrug, out strSqlDrug, inpatientNo, flag) == -1)
            //			{
            //				this.Err += "ҩƷSQL��丳ֵ����!";
            //				return -1;
            //			}
            //			if(FS.FrameWork.Public.String.FormatString(strSqlItem, out strSqlItem, inpatientNo, flag) == -1)
            //			{
            //				this.Err += "��ҩƷSQL��丳ֵ����!";
            //				return -1;
            //			}
            try
            {
                strSqlDrug = string.Format(strSqlDrug, inpatientNo, flag, condition);
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = ex.Message;
                return -1;
            }
            try
            {
                strSqlItem = string.Format(strSqlItem, inpatientNo, flag, condition);
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = ex.Message;
                return -1;
            }
            iReturnDrug = this.ExecNoQuery(strSqlDrug);
            if (iReturnDrug < 0)
            {
                return iReturnDrug;
            }
            iReturnItem = this.ExecNoQuery(strSqlItem);
            if (iReturnItem < 0)
            {
                return iReturnItem;
            }

            return iReturnItem + iReturnDrug;
        }


        /// <summary>
        /// ���²��ϴ�����Ŀ���ϴ���ǣ����շ���Ŀ��ϸ��
        /// </summary>
        /// <param name="inpatientNo">סԺ��ˮ��</param>
        /// <param name="splitFeeFlag">���ϴ���Ŀ��־SplitFeeFlag true:���ϴ� </param>
        /// <param name="flag">0 ��Ҫ�ϴ� 3 ���ϴ�</param>
        /// <returns>-1����ʧ�� 0 û�м�¼ >=1 ���³ɹ�</returns>
        public int UpdateFlagForNotUploadForSplitFeeFlag(string inpatientNo, string splitFeeFlag, string flag)
        {
            string strSqlItem = "";
            int iReturnDrug = 0, iReturnItem = 0;

            if (this.Sql.GetCommonSql("Fee.Interface.UpdateFlagForNotUpload.UnDrug.SplitFeeFlag", ref strSqlItem) == -1)
            {
                strSqlItem = @"update fin_ipb_itemlist a
                        set a.upload_flag = '{1}'
                        where   a.inpatient_no = '{0}'
                        and a.SPLIT_FEE_FLAG ='{2}'
                        and a.UPLOAD_FLAG = '0'";
            }
            //			if(FS.FrameWork.Public.String.FormatString(strSqlDrug, out strSqlDrug, inpatientNo, flag) == -1)
            //			{
            //				this.Err += "ҩƷSQL��丳ֵ����!";
            //				return -1;
            //			}
            //			if(FS.FrameWork.Public.String.FormatString(strSqlItem, out strSqlItem, inpatientNo, flag) == -1)
            //			{
            //				this.Err += "��ҩƷSQL��丳ֵ����!";
            //				return -1;
            //			}

            try
            {
                strSqlItem = string.Format(strSqlItem, inpatientNo, flag, splitFeeFlag);
            }
            catch (Exception ex)
            {
                this.ErrCode = "-1";
                this.Err = ex.Message;
                return -1;
            }

            iReturnItem = this.ExecNoQuery(strSqlItem);
            if (iReturnItem < 0)
            {
                return iReturnItem;
            }

            return iReturnItem;
        }
        #endregion

        #region ҽ������
        /// <summary>
        /// ���ָ���·�֮��Ŀ���ҽ���޶�
        /// </summary>
        /// <param name="dtBegin">��ʼʱ��</param>
        /// <param name="dtEnd">����ʱ��</param>
        /// <param name="inState">��Ժ״̬</param>
        /// <param name="pactCode">��ͬ��λ</param>
        /// <param name="ds">���ص���ʾ��Ϣ</param>
        /// <returns>-1 ʧ�� 0 �ɹ�</returns>
        public int QueryDeptSiCost(DateTime dtBegin, DateTime dtEnd, string inState, string pactCode, ref DataSet ds)
        {
            string strSql = "";
            if (inState == "B")
            {
                if (this.Sql.GetCommonSql("Fee.Interface.QueryDeptSiCost.DataSet.OUT", ref strSql) == -1)
                {
                    this.Err += "���SQL������!";
                    return -1;
                }
                if (FS.FrameWork.Public.String.FormatString(strSql, out strSql, inState, dtBegin.ToString(), dtEnd.ToString(), pactCode) == -1)
                {
                    this.Err += "SQL��为ֵ����!";
                    return -1;
                }
            }
            if (inState == "I")
            {
                if (this.Sql.GetCommonSql("Fee.Interface.QueryDeptSiCost.DataSet.IN", ref strSql) == -1)
                {
                    this.Err += "���SQL������!";
                    return -1;
                }
                if (FS.FrameWork.Public.String.FormatString(strSql, out strSql, pactCode) == -1)
                {
                    this.Err += "SQL��为ֵ����!";
                    return -1;
                }
            }

            return this.ExecQuery(strSql, ref ds);
        }
        /// <summary>
        /// ���һ��ʱ���ڵĲ���ҽ��������ҩ���
        /// </summary>
        /// <param name="dtBegin">��ʼʱ��</param>
        /// <param name="dtEnd">����ʱ��</param>
        /// <param name="inState">��Ժ״̬</param>
        /// <param name="pactCode">��ͬ��λ</param>
        /// <param name="ds">���ص���ʾ��Ϣ</param>
        /// <returns>-1 ʧ�� 0 �ɹ�</returns>
        public int QuerySIDeptDrug(DateTime dtBegin, DateTime dtEnd, string inState, string pactCode, ref DataSet ds)
        {
            string strSql = "";
            if (inState == "B")
            {
                if (this.Sql.GetCommonSql("Fee.Interface.QuerySIDeptDrug.DataSet.OUT", ref strSql) == -1)
                {
                    this.Err += "���SQL������!";
                    return -1;
                }
                if (FS.FrameWork.Public.String.FormatString(strSql, out strSql, dtBegin.ToString(), dtEnd.ToString(), pactCode) == -1)
                {
                    this.Err += "SQL��为ֵ����!";
                    return -1;
                }
            }
            if (inState == "I")
            {
                if (this.Sql.GetCommonSql("Fee.Interface.QuerySIDeptDrug.DataSet.IN", ref strSql) == -1)
                {
                    this.Err += "���SQL������!";
                    return -1;
                }
                if (FS.FrameWork.Public.String.FormatString(strSql, out strSql, pactCode) == -1)
                {
                    this.Err += "SQL��为ֵ����!";
                    return -1;
                }
            }

            return this.ExecQuery(strSql, ref ds);
        }
        /// <summary>
        /// ��ѯָ��������ҽ�����ߵ�ҩƷ��Ϣ
        /// </summary>
        /// <param name="dtBegin">��ʼʱ��</param>
        /// <param name="dtEnd">����ʱ��</param>
        /// <param name="deptCode">��������</param>
        /// <param name="inState">��Ժ״̬</param>
        /// <param name="pactCode">��ͬ��λ</param>
        /// <param name="ds">��ʾ��ϢDataSet</param>
        /// <returns>-1 ʧ�� 0 �ɹ�</returns>
        public int QuerySIPateintDrugForDept(DateTime dtBegin, DateTime dtEnd, string deptCode, string inState, string pactCode, ref DataSet ds)
        {
            string strSql = "";
            if (inState == "O")
            {
                if (this.Sql.GetCommonSql("Fee.Interface.QuerySIPateintDrugForDept.DataSet.OUT", ref strSql) == -1)
                {
                    this.Err += "���SQL������!";
                    return -1;
                }
                if (FS.FrameWork.Public.String.FormatString(strSql, out strSql, deptCode, dtBegin.ToString(), dtEnd.ToString(), pactCode) == -1)
                {
                    this.Err += "SQL��为ֵ����!";
                    return -1;
                }
            }
            if (inState == "I")
            {
                if (this.Sql.GetCommonSql("Fee.Interface.QuerySIPateintDrugForDept.DataSet.IN", ref strSql) == -1)
                {
                    this.Err += "���SQL������!";
                    return -1;
                }
                if (FS.FrameWork.Public.String.FormatString(strSql, out strSql, deptCode, pactCode, dtBegin.ToString(), dtEnd.ToString()) == -1)
                {
                    this.Err += "SQL��为ֵ����!";
                    return -1;
                }
            }

            return this.ExecQuery(strSql, ref ds);
        }
        /// <summary>
        /// ��ѯָ��������ҽ�����ߵ�ҩƷ��Ϣ, ����2
        /// </summary>
        /// <param name="dtBegin">��ʼʱ��</param>
        /// <param name="dtEnd">����ʱ��</param>
        /// <param name="deptCode">��������</param>
        /// <param name="inState">��Ժ״̬</param>
        /// <param name="pactCode">��ͬ��λ</param>
        /// <param name="ds">��ʾ��ϢDataSet</param>
        /// <returns>-1 ʧ�� 0 �ɹ�</returns>
        public int QuerySIPateintDrugForDeptSec(DateTime dtBegin, DateTime dtEnd, string deptCode, string inState, string pactCode, ref DataSet ds)
        {
            string strSql = "";
            if (inState == "O")
            {
                if (this.Sql.GetCommonSql("Fee.Interface.QuerySIPateintDrugForDeptSec.DataSet.OUT", ref strSql) == -1)
                {
                    this.Err += "���SQL������!";
                    return -1;
                }
                if (FS.FrameWork.Public.String.FormatString(strSql, out strSql, deptCode, dtBegin.ToString(), dtEnd.ToString(), pactCode) == -1)
                {
                    this.Err += "SQL��为ֵ����!";
                    return -1;
                }
            }
            if (inState == "I")
            {
                if (this.Sql.GetCommonSql("Fee.Interface.QuerySIPateintDrugForDeptSec.DataSet.IN", ref strSql) == -1)
                {
                    this.Err += "���SQL������!";
                    return -1;
                }
                if (FS.FrameWork.Public.String.FormatString(strSql, out strSql, deptCode, pactCode, dtBegin.ToString(), dtEnd.ToString()) == -1)
                {
                    this.Err += "SQL��为ֵ����!";
                    return -1;
                }
            }

            return this.ExecQuery(strSql, ref ds);
        }
        /// <summary>
        ///	��ó�Ժ�����ҽ��������Ϣ
        /// </summary>
        /// <param name="dtBegin">סԺ�Ǽǿ�ʼʱ��</param>
        /// <param name="dtEnd">סԺ�Ǽǽ���ʱ��</param>
        /// <param name="pactCode">��ͬ��λ����</param>
        /// <returns></returns>
        public ArrayList QueryOutHosPatients(DateTime dtBegin, DateTime dtEnd, string pactCode)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Fee.Interface.QueryOutHosPatients.Select.1", ref strSql) == -1)
            {
                this.Err += "���SQL������!";
                return null;
            }
            if (FS.FrameWork.Public.String.FormatString(strSql, out strSql, dtBegin.ToString(), dtEnd.ToString(), pactCode) == -1)
            {
                this.Err += "SQL��为ֵ����!";
                return null;
            }
            this.ExecQuery(strSql);
            ArrayList al = new ArrayList();
            string temp = "";
            try
            {
                while (Reader.Read())
                {
                    temp = Reader[0].ToString();
                    al.Add(temp);
                }

                this.Reader.Close();
                return al;
            }
            catch (Exception ex)
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                this.Err += ex.Message;
                return null;
            }
        }
        /// <summary>
        /// ��õ�ǰ������Ժҽ������
        /// </summary>
        /// <param name="dtBegin">��ʼʱ��</param>
        /// <param name="dtEnd">����ʱ��</param>
        /// <param name="pactCode">��ͬ��λ����</param>
        /// <returns>null ʧ�� ArrayList.Count > 1 �ɹ�</returns>
        public ArrayList QueryInHosPatients(DateTime dtBegin, DateTime dtEnd, string pactCode)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Fee.Interface.QueryInHosPatient.Select.1", ref strSql) == -1)
            {
                this.Err += "���SQL������!";
                return null;
            }
            if (FS.FrameWork.Public.String.FormatString(strSql, out strSql, dtBegin.ToString(), dtEnd.ToString(), pactCode) == -1)
            {
                this.Err += "SQL��为ֵ����!";
                return null;
            }
            this.ExecQuery(strSql);
            ArrayList al = new ArrayList();
            string temp = "";
            try
            {
                while (Reader.Read())
                {
                    temp = Reader[0].ToString();
                    al.Add(temp);
                }

                this.Reader.Close();
                return al;
            }
            catch (Exception ex)
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                this.Err += ex.Message;
                return null;
            }
        }

        /// <summary>
        ///	��ó�Ժ�����ҽ��������Ϣ
        /// </summary>
        /// <param name="dtBegin">סԺ�Ǽǿ�ʼʱ��</param>
        /// <param name="dtEnd">סԺ�Ǽǽ���ʱ��</param>
        /// <param name="pactCode">��ͬ��λ����</param>
        /// <param name="sqlWhere">���Ҵ���</param>
        /// <returns></returns>
        public ArrayList QueryOutHosPatients(DateTime dtBegin, DateTime dtEnd, string pactCode, string sqlWhere)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Fee.Interface.QueryOutHosPatients.Select.1", ref strSql) == -1)
            {
                this.Err += "���SQL������!";
                return null;
            }
            if (FS.FrameWork.Public.String.FormatString(strSql, out strSql, dtBegin.ToString(), dtEnd.ToString(), pactCode) == -1)
            {
                this.Err += "SQL��为ֵ����!";
                return null;
            }
            strSql = strSql + sqlWhere;
            this.ExecQuery(strSql);
            ArrayList al = new ArrayList();
            string temp = "";
            try
            {
                while (Reader.Read())
                {
                    temp = Reader[0].ToString();
                    al.Add(temp);
                }

                this.Reader.Close();
                return al;
            }
            catch (Exception ex)
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                this.Err += ex.Message;
                return null;
            }
        }
        /// <summary>
        /// ��õ�ǰ������Ժҽ������
        /// </summary>
        /// <param name="dtBegin">��ʼʱ��</param>
        /// <param name="dtEnd">����ʱ��</param>
        /// <param name="pactCode">��ͬ��λ����</param>
        /// <param name="sqlWhere">���Ҵ���</param>
        /// <returns>null ʧ�� ArrayList.Count > 1 �ɹ�</returns>
        public ArrayList QueryInHosPatients(DateTime dtBegin, DateTime dtEnd, string pactCode, string sqlWhere)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Fee.Interface.QueryInHosPatient.Select.1", ref strSql) == -1)
            {
                this.Err += "���SQL������!";
                return null;
            }
            if (FS.FrameWork.Public.String.FormatString(strSql, out strSql, dtBegin.ToString(), dtEnd.ToString(), pactCode) == -1)
            {
                this.Err += "SQL��为ֵ����!";
                return null;
            }
            strSql = strSql + sqlWhere;
            this.ExecQuery(strSql);
            ArrayList al = new ArrayList();
            string temp = "";
            try
            {
                while (Reader.Read())
                {
                    temp = Reader[0].ToString();
                    al.Add(temp);
                }

                this.Reader.Close();
                return al;
            }
            catch (Exception ex)
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
                this.Err += ex.Message;
                return null;
            }
        }

        /// <summary>
        /// ��ѯҽ�����˷��ù���
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="state"></param>
        /// <param name="reportName"></param>
        /// <returns></returns>
        public DataSet QueryFeeStruct(string dtBegin, string dtEnd, string[] state, string reportName)
        {
            string strSql = "";
            DataSet dsFee = new DataSet();
            if (this.Sql.GetCommonSql("Fee.Interface.QueryFeeStruct", ref strSql) == -1)
            {
                this.Err = "Can't Find The SqlExpression";
                return null;
            }
            strSql = System.String.Format(strSql, reportName, dtBegin, dtEnd, state[0] + "','" + state[1]);
            this.ExecQuery(strSql, ref dsFee);
            return dsFee;
        }
        /// <summary>
        /// ���տ��һ���ȫԺ��ѯ���ҽ�Ʊ��ն������ҽ�Ʒ��ò�����ϸ
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="DeptCode"></param>
        /// <returns></returns>
        public DataSet QuerySIDeptFee(string dtBegin, string dtEnd, string DeptCode)
        {
            string strSql = "";
            DataSet dsFee = new DataSet();
            FS.HISFC.BizLogic.Manager.Department manager = new FS.HISFC.BizLogic.Manager.Department();
            if (this.Sql.GetCommonSql("Fee.Interface.QuerySIDeptFee", ref strSql) == -1)
            {
                this.Err = "Can't Find the Sql";
                return null;
            }
            if (DeptCode == "")
            {
                strSql = System.String.Format(strSql, dtBegin, dtEnd);
                strSql += " ORDER BY A.DEPT_NAME";
            }
            else
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = DeptCode;
                ArrayList alDept = manager.GetDeptFromNurseStation(obj);
                DeptCode = "";
                for (int i = 0; i < alDept.Count; i++)
                {
                    DeptCode += (alDept[i] as FrameWork.Models.NeuObject).ID + "','";
                }
                DeptCode = DeptCode.Substring(0, DeptCode.Length - 2);
                strSql += " AND A.DEPT_CODE IN( '" + DeptCode + ")";
                strSql += " ORDER BY A.DEPT_NAME";
                strSql = System.String.Format(strSql, dtBegin, dtEnd);
            }
            this.ExecQuery(strSql, ref dsFee);
            return dsFee;
        }
        /// <summary>
        /// ��ѯҽ�����ö�����ϸ
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="reportName"></param>
        /// <returns></returns>
        public DataSet QueryFeeCollateDetail(string dtBegin, string dtEnd, string reportName)
        {
            string strSql = "";
            DataSet dsFee = new DataSet();
            if (this.Sql.GetCommonSql("Fee.Interface.QueryFeeCollateDetail", ref strSql) == -1)
            {
                this.Err = "Can't Find The Sql";
                return null;
            }
            strSql = System.String.Format(strSql, dtBegin, dtEnd, reportName);
            this.ExecQuery(strSql, ref dsFee);
            return dsFee;
        }
        /// <summary>
        /// ��ѯҽ�����ý���������Ϣ
        /// </summary>
        /// <param name="dtBegin">��ʼ����</param>
        /// <param name="dtEnd">��ֹ����</param>
        /// <param name="state">סԺor�����ض���Ŀ</param>
        /// <param name="empl_type">��ְor����</param>
        /// <returns></returns>
        public DataSet QueryStatFeePactByInsure(string dtBegin, string dtEnd, string[] state, string[] empl_type)
        {
            string strSql = "";
            DataSet ds = new DataSet();
            if (this.Sql.GetCommonSql("Fee.Interface.QueryStatFeePactByInsure", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql";
                return null;
            }
            strSql = System.String.Format(strSql, dtBegin, dtEnd, state[0] + "','" + state[1], empl_type[0] + "','" + empl_type[1]);
            this.ExecQuery(strSql, ref ds);
            return ds;
        }
        #endregion

        #region ҽ����Ŀ��ʾ
        /// <summary>
        /// �����Ŀ���
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string ShowItemFlag(FS.HISFC.Models.Base.Item item)
        {
            bool b1, b2, b3, b4;
            //if (item.IsPharmacy)
            if (item.ItemType == HISFC.Models.Base.EnumItemType.Drug)
            {
                FS.HISFC.Models.Pharmacy.Item o = item as FS.HISFC.Models.Pharmacy.Item;
                b1 = FS.FrameWork.Function.NConvert.ToBoolean(o.SpecialFlag);
                b2 = FS.FrameWork.Function.NConvert.ToBoolean(o.SpecialFlag1);
                b3 = FS.FrameWork.Function.NConvert.ToBoolean(o.SpecialFlag2);
                b4 = FS.FrameWork.Function.NConvert.ToBoolean(o.SpecialFlag3);
            }
            #region {8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} ҽ�����İ����� by gengxl
            else if (item.ItemType == HISFC.Models.Base.EnumItemType.MatItem)
            {
                FS.HISFC.Models.FeeStuff.MaterialItem o = item as FS.HISFC.Models.FeeStuff.MaterialItem;

                b1 = FS.FrameWork.Function.NConvert.ToBoolean(o.SpecialFlag);
                b2 = FS.FrameWork.Function.NConvert.ToBoolean(o.SpecialFlag1);
                b3 = FS.FrameWork.Function.NConvert.ToBoolean(o.SpecialFlag2);
                b4 = FS.FrameWork.Function.NConvert.ToBoolean(o.SpecialFlag3);
            }
            #endregion
            else
            {
                FS.HISFC.Models.Fee.Item.Undrug o = item as FS.HISFC.Models.Fee.Item.Undrug;

                b1 = FS.FrameWork.Function.NConvert.ToBoolean(o.SpecialFlag);
                b2 = FS.FrameWork.Function.NConvert.ToBoolean(o.SpecialFlag1);
                b3 = FS.FrameWork.Function.NConvert.ToBoolean(o.SpecialFlag2);
                b4 = FS.FrameWork.Function.NConvert.ToBoolean(o.SpecialFlag3);
            }
            string s = "";
            if (b1) s = s + "X";
            if (b2) s = s + "S";
            if (b3) s = s + "Z";
            if (b4) s = s + "T";
            if (s != "") s = "[" + s + "]";
            return s;
        }
        /// <summary>
        /// �����Ŀ��� ��,..
        /// </summary>
        /// <param name="ItemCode"></param>
        /// <returns></returns>
        public string ShowItemGrade(string ItemCode)
        {
            FS.HISFC.Models.SIInterface.Compare obj = null;
            int iReturn = this.GetCompareSingleItem("2", ItemCode, ref obj);
            if (iReturn == -1)
            {
                return "���ҽ����������";
            }
            else if (iReturn == -2)//û����
            {
                return "�Էѡ�100%��";
            }
            else
            {
                switch (FS.FrameWork.Function.NConvert.ToInt32(obj.CenterItem.ItemGrade))
                {
                    case 1:
                        return "���ࡾ" + (obj.CenterItem.Rate * 100).ToString() + "%��";
                    case 2:
                        return "���ࡾ" + (obj.CenterItem.Rate * 100).ToString() + "%��";
                    case 3:
                        return "�Էѡ�" + (obj.CenterItem.Rate * 100).ToString() + "%��";
                    default:
                        break;
                }
            }
            return "";

        }

        /// <summary>
        /// ����ItemGrade���룬���ؼף��ң�����
        /// wolf ���,��̬�Ĵ�Ҷ�������
        /// </summary>
        /// <param name="itemGrade"></param>
        /// <returns></returns>
        public static string ShowItemGradeByCode(string itemGrade)
        {
            if (itemGrade == "1")
            {
                return "����";
            }
            else if (itemGrade == "2")
            {
                return "����";
            }
            else if (itemGrade == "3")
            {
                return "";
                //return "�Է�";
            }
            if (string.IsNullOrEmpty(itemGrade))
            {
                return "";
                //return "�Է�";
            }
            else
            {
                return "ҽ��";
            }
        }
        #endregion
    }
}