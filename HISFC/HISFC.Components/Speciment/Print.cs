using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace FS.HISFC.Components.Speciment
{
    public class PrintLabel
    {
        private static IPAddress ip;
        private static IPEndPoint ipe;
        private static Socket c;
        //private static MW6DataMatrix.Font DMFontObj = new MW6DataMatrix.Font();
        public static string bradyIP = "";
        /// <summary>
        /// 连接打印机
        /// </summary>
        /// <param name="host">打印机IP</param>
        private static int Connect()
        {
            try
            {
                FS.FrameWork.Management.ControlParam controlMgr = new FS.FrameWork.Management.ControlParam();
                //string bradyIP = controlMgr.QueryControlInfoByCode("SPECIP").ControlerValue;
                if (bradyIP == "")
                {
                    bradyIP = "172.16.135.45";
                }
                ip = IPAddress.Parse(bradyIP);
                ipe = new IPEndPoint(ip, 9100);
                c = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//
                c.Connect(ipe);
                return 1;
            }
            catch
            {
                return -2;
            }

        }

        private static void Close()
        {
            c.Close();
            bradyIP = "";
        }

        /// <summary>
        /// 打印二维码
        /// </summary>
        /// <param name="barCode">需要打印的条形码</param>
        /// <param name="sequence">序号</param>
        /// <param name="host">打印机IP</param>
        /// <param name="disType">病种</param>
        /// <param name="num"></param>
        /// <returns>-1，打印失败，1 成功</returns>
        public static int Print2DBarCodeOrg(List<string> barCode, List<string> sequence, List<string> tumorPos, List<string> disType, List<string> num)
        {
            try
            {
                FS.HISFC.BizLogic.Speciment.SubSpecManage subManage = new FS.HISFC.BizLogic.Speciment.SubSpecManage();
                FS.HISFC.BizLogic.Speciment.BoxSpecManage specManage = new FS.HISFC.BizLogic.Speciment.BoxSpecManage();
                FS.HISFC.BizLogic.Speciment.SpecBoxManage specBoxMgr = new FS.HISFC.BizLogic.Speciment.SpecBoxManage();
                //FS.HISFC.Management.Speciment.SpecSourceManage s = new FS.HISFC.Management.Speciment.SpecSourceManage();
                //s.GetSource
                //连接主机
                if (Connect() == -2)
                {
                    return -2;
                }
                UTF8Encoding utf = new UTF8Encoding();
                string sendStr = "";
                for (int i = 0; i < barCode.Count; i++)
                {
                    string date = "";

                    FS.HISFC.Models.Speciment.SubSpec spec = subManage.GetSubSpecById("", barCode[i]);
                    if (spec.SubSpecId == 0)
                    {
                        continue;
                    }
                    FS.HISFC.Models.Speciment.BoxSpec bS = specManage.GetSpecByBoxId(spec.BoxId.ToString());
                    FS.HISFC.Models.Speciment.SpecBox box = specBoxMgr.GetBoxById(spec.BoxId.ToString());
                    string specUse = "";
                    if (box.SpecialUse == "1")
                    {
                        specUse = "F";
                    }
                    if (box.SpecialUse == "8")
                    {
                        specUse = "E";
                    }

                    if (spec == null || spec.SubSpecId <= 0)
                    {
                        date = DateTime.Now.ToShortDateString();
                    }
                    else
                    {
                        date = spec.StoreTime.ToShortDateString();
                    }
                    string seqence = Convert.ToInt32(sequence[i]).ToString().PadLeft(4, '0');
                    sendStr += "M l FNT;/card/simfang" + Convert.ToChar(13);
                    //sendStr += "M l IMG;/card/sysucc" + Convert.ToChar(13);
                    sendStr += "mm" + Convert.ToChar(13);
                    sendStr += "zO" + Convert.ToChar(13);
                    sendStr += "J" + Convert.ToChar(13);
                    sendStr += "F90;simfang" + Convert.ToChar(13);
                    sendStr += "S l1;0,0,12,14,35" + Convert.ToChar(13);
                    sendStr += "T 16,2.7,1,90,2;" + date + Convert.ToChar(13);
                    sendStr += "T 26,2.7,1,90,2; 中大肿瘤" + Convert.ToChar(13);
                    sendStr += "B 13,3,0,DATAMATRIX,0.29;" + barCode[i] + Convert.ToChar(13);
                    sendStr += "T 14,9,0,3,2.8;" + barCode[i] + Convert.ToChar(13);
                    string tumor = barCode[i].Substring(barCode[i].Length - 3, 1);
                    switch (tumor)
                    {
                        case "T":
                            tumor = "肿物";
                            break;
                        case "S":
                            tumor = "子瘤";
                            break;
                        case "P":
                            tumor = "癌旁";
                            break;
                        case "N":
                            tumor = "正常";
                            break;
                        case "E":
                            tumor = "癌栓";
                            break;
                        case "L":
                            tumor = "淋巴结";
                            break;
                        default:
                            break;
                    }
                    sendStr += "T 18.5,6,1,90,3;" + disType[i] + "  " + tumor + Convert.ToChar(13);
                    sendStr += "T 0.8,8.2,0,90,2.5;" + disType[i].Substring(0, 1) + Convert.ToChar(13);

                    sendStr += "T 1.2,3.5,0,3,2.5;" + spec.BoxEndRow.ToString() + "-" + spec.BoxEndCol.ToString() + specUse + Convert.ToChar(13);

                    sendStr += "T 0,5.8,0,3,3;" + seqence + Convert.ToChar(13);
                    sendStr += "T 3,8.2,0,3,2.5;" + barCode[i].Substring(barCode[i].Length - 3, 2) + num[i] + Convert.ToChar(13);

                    sendStr += "A 1" + Convert.ToChar(13);

                }
                if (bradyIP != "172.16.135.45")
                {
                    sendStr = sendStr.Replace("/card", "/iffs");
                }
                byte[] bs = utf.GetBytes(sendStr);
                //给打印机发送命令
                c.Send(bs, bs.Length, 0);
                Close();
                return 1;
            }
            catch
            {
                Close();
                return -1;
            }
        }


        /// <summary>
        /// 打印二维码
        /// </summary>
        /// <param name="barCode">需要打印的条形码</param>
        /// <param name="sequence">序号</param>
        /// <param name="host">打印机IP</param>
        /// <param name="disType">病种</param>
        /// <param name="num"></param>
        /// <returns>-1，打印失败，1 成功</returns>
        public static int Print2DBarCode(List<string> barCode, List<string> sequence, List<string> specType, List<string> disType, List<string> num)
        {
            try
            {
                FS.HISFC.BizLogic.Speciment.SubSpecManage subManage = new FS.HISFC.BizLogic.Speciment.SubSpecManage();
                FS.HISFC.BizLogic.Speciment.BoxSpecManage specManage = new FS.HISFC.BizLogic.Speciment.BoxSpecManage();
                FS.HISFC.BizLogic.Speciment.SpecBoxManage specBoxMgr = new FS.HISFC.BizLogic.Speciment.SpecBoxManage();
                //FS.HISFC.Management.Speciment.SpecSourceManage s = new FS.HISFC.Management.Speciment.SpecSourceManage();
                //s.GetSource
                //连接主机
                if (Connect() == -2)
                {
                    return -2;
                }
                UTF8Encoding utf = new UTF8Encoding();
                string sendStr = "";
                for (int i = 0; i < barCode.Count; i++)
                {
                    string date = "";

                    FS.HISFC.Models.Speciment.SubSpec spec = subManage.GetSubSpecById("", barCode[i]);
                    FS.HISFC.Models.Speciment.BoxSpec bS = specManage.GetSpecByBoxId(spec.BoxId.ToString());
                    FS.HISFC.Models.Speciment.SpecBox box = specBoxMgr.GetBoxById(spec.BoxId.ToString());

                    if (spec == null || spec.SubSpecId <= 0)
                    {
                        date = DateTime.Now.ToShortDateString();
                    }
                    else
                    {
                        date = spec.StoreTime.ToShortDateString();
                    }
                    string seqence = sequence[i].PadLeft(6, '0');
                    sendStr += "M l FNT;/card/simfang" + Convert.ToChar(13);
                    sendStr += "mm" + Convert.ToChar(13);
                    sendStr += "zO" + Convert.ToChar(13);
                    sendStr += "J" + Convert.ToChar(13);
                    sendStr += "F90;simfang" + Convert.ToChar(13);
                    sendStr += "S l1;0,0,12,14,35" + Convert.ToChar(13);
                    sendStr += "T 16,3,1,90,2;" + date + Convert.ToChar(13);
                    sendStr += "T 26,3,1,90,2; 中大肿瘤" + Convert.ToChar(13);
                    sendStr += "B 12,3.5,0,DATAMATRIX,0.29;" + barCode[i] + Convert.ToChar(13);
                    sendStr += "T 17,9,0,3,2.8;" + barCode[i] + Convert.ToChar(13);
                    sendStr += "T 17,6,1,90,3;" + disType[i] + "  " + specType[i] + Convert.ToChar(13);
                    sendStr += "T 1,9,0,90,2.8;" + disType[i].Substring(0, 1) + Convert.ToChar(13);


                    if (box.DesCapType == 'B')
                    {
                        //一列的结束
                        if (spec.BoxEndRow == bS.Row)
                        {
                            if (spec.BoxEndCol.ToString().Length == 2)
                            {
                                sendStr += "T 0,2.8,0,3,2;" + spec.BoxEndCol.ToString() + "■" + Convert.ToChar(13);
                                sendStr += "T 4,3.5,0,3,3.3;" + seqence.Substring(0, 2) + Convert.ToChar(13);

                            }
                            else
                            {
                                sendStr += "T 0,3.5,0,3,3;" + spec.BoxEndCol.ToString() + "■" + seqence.Substring(0, 2) + Convert.ToChar(13);
                            }
                        }
                        //一列的开始
                        if (spec.BoxEndRow == 1)
                        {
                            if (spec.BoxEndCol.ToString().Length == 2)
                            {
                                sendStr += "T 0,2.8,0,3,2;" + spec.BoxEndCol.ToString() + "●" + Convert.ToChar(13);
                                sendStr += "T 4,3.5,0,3,3.3;" + seqence.Substring(0, 2) + Convert.ToChar(13);

                            }
                            else
                            {
                                sendStr += "T 0,3.5,0,3,3;" + spec.BoxEndCol.ToString() + "●" + seqence.Substring(0, 2) + Convert.ToChar(13);

                            }
                        }
                        if (spec.BoxEndRow != 1 && spec.BoxEndRow != bS.Row)
                        {
                            sendStr += "T 1.2,3.5,0,3,3.6;" + seqence.Substring(0, 2) + Convert.ToChar(13);
                        }
                    }

                    else
                    {
                        //一行的结束
                        if (spec.BoxEndCol == bS.Col)
                        {
                            if (spec.BoxEndRow.ToString().Length == 2)
                            {
                                sendStr += "T 0,2.8,0,3,2;" + spec.BoxEndRow.ToString() + "■" + Convert.ToChar(13);
                                sendStr += "T 4,3.5,0,3,3.3;" + seqence.Substring(0, 2) + Convert.ToChar(13);

                            }
                            else
                            {
                                sendStr += "T 0,3.5,0,3,3;" + spec.BoxEndRow.ToString() + "■" + seqence.Substring(0, 2) + Convert.ToChar(13);
                            }
                        }
                        //一行的开始
                        if (spec.BoxEndCol == 1)
                        {
                            if (spec.BoxEndRow.ToString().Length == 2)
                            {
                                sendStr += "T 0,2.8,0,3,2;" + spec.BoxEndRow.ToString() + "●" + Convert.ToChar(13);
                                sendStr += "T 4,3.5,0,3,3.3;" + seqence.Substring(0, 2) + Convert.ToChar(13);

                            }
                            else
                            {
                                sendStr += "T 0,3.5,0,3,3;" + spec.BoxEndRow.ToString() + "●" + seqence.Substring(0, 2) + Convert.ToChar(13);

                            }

                        }
                        if (spec.BoxEndCol != 1 && spec.BoxEndCol != bS.Col)
                        {
                            sendStr += "T 1.2,3.5,0,3,3.6;" + seqence.Substring(0, 2) + Convert.ToChar(13);
                        }
                    }
                    sendStr += "T 0,6.3,0,3,3.6;" + seqence.Substring(2, 4) + Convert.ToChar(13);
                    sendStr += "T 4,9,0,3,2.8;" + barCode[i].Substring(barCode[i].Length - 2, 1) + num[i] + Convert.ToChar(13);

                    sendStr += "A 1" + Convert.ToChar(13);

                }
                if (bradyIP != "172.16.135.45")
                {
                    sendStr.Replace("/card", "/iffs");
                }
                byte[] bs = utf.GetBytes(sendStr);
                //给打印机发送命令
                c.Send(bs, bs.Length, 0);
                Close();
                return 1;
            }
            catch
            {
                Close();
                return -1;
            }
        }

        public static int PrintBarCode(List<string> barCode)
        {
            //组织标本打印列表
            List<string> orgBarCode = new List<string>();
            List<string> orgSequence = new List<string>();
            List<string> orgtumorPos = new List<string>();
            List<string> orgDisType = new List<string>();
            List<string> orgNum = new List<string>();

            //血标本打印列表
            List<string> bldBarCode = new List<string>();
            List<string> bldSequence = new List<string>();
            List<string> bldSpecType = new List<string>();
            List<string> bldDisType = new List<string>();
            List<string> bldNum = new List<string>();

            FS.HISFC.BizLogic.Speciment.SubSpecManage subManage = new FS.HISFC.BizLogic.Speciment.SubSpecManage();
            FS.HISFC.BizLogic.Speciment.SpecSourceManage sourceManage = new FS.HISFC.BizLogic.Speciment.SpecSourceManage();
            FS.HISFC.BizLogic.Speciment.SpecSourcePlanManage planManage = new FS.HISFC.BizLogic.Speciment.SpecSourcePlanManage();
            FS.HISFC.BizLogic.Speciment.DisTypeManage disTypeManage = new FS.HISFC.BizLogic.Speciment.DisTypeManage();
            FS.HISFC.BizLogic.Speciment.SpecTypeManage typeManage = new FS.HISFC.BizLogic.Speciment.SpecTypeManage();

            try
            {
                foreach (string s in barCode)
                {
                    FS.HISFC.Models.Speciment.SpecSource source = sourceManage.GetBySubBarCode(s, "0");
                    FS.HISFC.Models.Speciment.SpecSourcePlan p = planManage.GetPlanBySubBarCode(s);
                    if (source == null)
                        continue;
                    FS.HISFC.Models.Speciment.DiseaseType dis = disTypeManage.SelectDisByID(source.DiseaseType.DisTypeID.ToString());
                    if (dis == null)
                        continue;
                    if (p == null)
                        continue;
                    FS.HISFC.Models.Speciment.SpecType t = typeManage.GetSpecTypeById(p.SpecType.SpecTypeID.ToString());
                    if (t == null)
                        continue;
                    if (source.OrgOrBoold == "O")
                    {
                        //组织标本打印列表
                        orgBarCode.Add(s);
                        orgSequence.Add(s.Substring(0, 6));
                        orgtumorPos.Add(s.Substring(s.Length - 3, 1));
                        orgDisType.Add(dis.DiseaseName);
                        orgNum.Add(s.Substring(s.Length - 1, 1));
                    }
                    else
                    {
                        string firstLabel = s.Substring(0, 1);
                        if (System.Text.RegularExpressions.Regex.Match(firstLabel, "^\\d+$").Success)
                        {
                            bldSequence.Add(s.Substring(0, 6));
                        }

                        if (System.Text.RegularExpressions.Regex.Match(firstLabel, "^[a-zA-Z]+$").Success)
                        {
                            bldSequence.Add(s.Substring(1, 6));
                        }
                        bldBarCode.Add(s);
                        bldSpecType.Add(t.SpecTypeName);
                        bldDisType.Add(dis.DiseaseName);
                        bldNum.Add(s.Substring(s.Length - 1, 1));
                    }
                }
                if (bldBarCode.Count > 0)
                {
                    return Print2DBarCode(bldBarCode, bldSequence, bldSpecType, bldDisType, bldNum);
                }
                if (orgBarCode.Count > 0)
                {
                    return Print2DBarCodeOrg(orgBarCode, orgSequence, orgtumorPos, orgDisType, orgNum);
                }
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 转为二维码
        /// </summary>
        /// <param name="barCode">条码内容</param>
        /// <returns>解析的字符串</returns>
        public static string Generate2DBarCode(string barCode)
        {

            // Encode data using DataMatrix
            //DMFontObj.Encode(barCode, -1, -1, true);

            // How many rows?
            //int RowCount = DMFontObj.GetRows();

            // Produce string for DataMatrix font
            string EncodedMsg = "" + System.Convert.ToChar(13) + System.Convert.ToChar(10);
            //for (int i = 0; i < RowCount; i++)
            //{
            //    EncodedMsg = EncodedMsg + DMFontObj.GetRowStringAt(i);
            //    EncodedMsg = EncodedMsg + System.Convert.ToChar(13) + System.Convert.ToChar(10);
            //}
            return EncodedMsg;
        }

        public static int PrintBoxBarCode(string boxId, string code, System.Data.IDbTransaction trans, int cnt)
        {
            try
            {
                bradyIP = "172.16.135.32";

                FS.HISFC.BizLogic.Speciment.SubSpecManage subManage = new FS.HISFC.BizLogic.Speciment.SubSpecManage();
                FS.HISFC.BizLogic.Speciment.SpecBoxManage specBoxMgr = new FS.HISFC.BizLogic.Speciment.SpecBoxManage();
                FS.HISFC.BizLogic.Speciment.DisTypeManage disMgr = new FS.HISFC.BizLogic.Speciment.DisTypeManage();
                FS.HISFC.BizLogic.Speciment.SpecTypeManage typeMgr = new FS.HISFC.BizLogic.Speciment.SpecTypeManage();
                //连接主机
                if (trans != null)
                {
                    subManage.SetTrans(trans);
                    specBoxMgr.SetTrans(trans);
                    disMgr.SetTrans(trans);
                    typeMgr.SetTrans(trans);

                }
                if (Connect() == -2)
                {
                    return -2;
                }
                UTF8Encoding utf = new UTF8Encoding();

                FS.HISFC.Models.Speciment.SubSpec sub = subManage.GetSubSpecInOneBox(boxId)[0] as FS.HISFC.Models.Speciment.SubSpec;

                string specType = typeMgr.GetSpecTypeByBoxId(boxId).SpecTypeName;
                string disType = disMgr.GetDisTypeByBoxId(boxId).DiseaseName;
                string specNum = "";
                string firstLabel = sub.SubBarCode.Substring(0, 1);
                string date = sub.StoreTime.ToShortDateString();
                string sendStr = "";
                string loc = ParseLocation.ParseSpecBox(code);
                int index = loc.IndexOf('-');
                string loc2 = "";
                string loc1 = "";
                if (loc.Length > 10)
                {
                    loc1 = loc.Substring(0, index);
                    loc2 = loc.Substring(index + 1);
                }
                else
                {
                    loc1 = loc;
                }

                //以数字开头
                if (System.Text.RegularExpressions.Regex.Match(firstLabel, "^\\d+$").Success)
                {
                    specNum = sub.SubBarCode.Substring(0, 6);
                }
                //条码以字母开头
                if (System.Text.RegularExpressions.Regex.Match(firstLabel, "^[a-zA-Z]+$").Success)
                {
                    specNum = sub.SubBarCode.Substring(1, 6);
                }

                for (int i = 0; i < cnt; i++)
                {
                    sendStr += "M l FNT;/iffs/simfang" + Convert.ToChar(13);
                    sendStr += "mm" + Convert.ToChar(13);
                    sendStr += "zO" + Convert.ToChar(13);
                    sendStr += "J" + Convert.ToChar(13);
                    sendStr += "F90;simfang" + Convert.ToChar(13);
                    sendStr += "S l1;0,0,25.4,27,52.8" + Convert.ToChar(13);
                    sendStr += "T 33,5,1,90,3;" + date + Convert.ToChar(13);

                    sendStr += "B 4,1,0,DATAMATRIX,0.3;" + code + Convert.ToChar(13);
                    sendStr += "T 10,5,0,3,3;" + code + Convert.ToChar(13);

                    sendStr += "T 5,12,1,90,4;" + loc1 + Convert.ToChar(13);
                    sendStr += "T 5,17,1,90,4;" + loc2 + Convert.ToChar(13);

                    sendStr += "T 4,22,0,3,5.5;" + specNum + Convert.ToChar(13);
                    sendStr += "T 22,22,1,90,5.5;" + disType + " " + specType + Convert.ToChar(13);



                    sendStr += "A 1" + Convert.ToChar(13);
                }
                byte[] bs = utf.GetBytes(sendStr);

                //给打印机发送命令
                c.Send(bs, bs.Length, 0);
                Close();
                return 1;
            }
            catch
            {
                Close();
                return -1;
            }
        }


        public static int PrintShelfBarCode(string shefId)
        {
            //try
            //{

            //    FS.HISFC.Management.Speciment.SpecBoxManage specBoxMgr = new FS.HISFC.Management.Speciment.SpecBoxManage();
            //    FS.HISFC.Management.Speciment.DisTypeManage disMgr = new FS.HISFC.Management.Speciment.DisTypeManage();
            //    FS.HISFC.Management.Speciment.SpecTypeManage typeMgr = new FS.HISFC.Management.Speciment.SpecTypeManage();
            //    //连接主机
            //    if (Connect() == -2)
            //    {
            //        return -2;
            //    }
            //    UTF8Encoding utf = new UTF8Encoding();


            //    string specType = typeMgr.gets(boxId).SpecTypeName;
            //    string disType = disMgr.GetDisTypeByBoxId(boxId).DiseaseName;
            //    string specNum = "";
            //    string firstLabel = sub.SubBarCode.Substring(0, 1);
            //    string date = sub.StoreTime.ToShortDateString();
            //    string sendStr = "";
            //    string loc = ParseLocation.ParseSpecBox(code);
            //    int index = loc.IndexOf('-');
            //    string loc2 = "";
            //    string loc1 = "";
            //    if (loc.Length > 10)
            //    {
            //        loc1 = loc.Substring(0, index);
            //        loc2 = loc.Substring(index + 1);
            //    }
            //    else
            //    {
            //        loc1 = loc;
            //    }

            //    //以数字开头
            //    if (System.Text.RegularExpressions.Regex.Match(firstLabel, "^\\d+$").Success)
            //    {
            //        specNum = sub.SubBarCode.Substring(0, 6);
            //    }
            //    //条码以字母开头
            //    if (System.Text.RegularExpressions.Regex.Match(firstLabel, "^[a-zA-Z]+$").Success)
            //    {
            //        specNum = sub.SubBarCode.Substring(1, 6);
            //    }

            //    for (int i = 0; i < cnt; i++)
            //    {
            //        sendStr += "M l FNT;/iffs/simfang" + Convert.ToChar(13);
            //        sendStr += "mm" + Convert.ToChar(13);
            //        sendStr += "zO" + Convert.ToChar(13);
            //        sendStr += "J" + Convert.ToChar(13);
            //        sendStr += "F90;simfang" + Convert.ToChar(13);
            //        sendStr += "S l1;0,0,25.4,27,52.8" + Convert.ToChar(13);
            //        sendStr += "T 33,5,1,90,3;" + date + Convert.ToChar(13);

            //        sendStr += "B 4,1,0,DATAMATRIX,0.3;" + code + Convert.ToChar(13);
            //        sendStr += "T 10,5,0,3,3;" + code + Convert.ToChar(13);

            //        sendStr += "T 5,12,1,90,4;" + loc1 + Convert.ToChar(13);
            //        sendStr += "T 5,17,1,90,4;" + loc2 + Convert.ToChar(13);

            //        sendStr += "T 4,22,0,3,5.5;" + specNum + Convert.ToChar(13);
            //        sendStr += "T 20,22,1,90,5.5;" + disType + " " + specType + Convert.ToChar(13);



            //        sendStr += "A 1" + Convert.ToChar(13);
            //    }
            //    byte[] bs = utf.GetBytes(sendStr);

            //    //给打印机发送命令
            //    c.Send(bs, bs.Length, 0);
            //    Close();
            //    return 1;
            //}
            //catch
            //{
            //    Close();
            //    return -1;
            //}
            return 1;
        }
    }
}
