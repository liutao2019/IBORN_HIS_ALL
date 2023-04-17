using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace GZSI.Controls
{
    class LoadSIDataManager
    {
        /// <summary>
        /// 默认广州医保合同单位编码
        /// </summary>
        private static string gzybParamCode = string.Empty;


        /// <summary>
        /// 默认异地医保合同单位编码
        /// </summary>
        private static string ydybParamCode = string.Empty;

        /// <summary>
        /// 常数管理业务层
        /// </summary>
        private static FS.HISFC.BizLogic.Manager.Constant conManager = null;

        /// <summary>
        /// 常数管理业务层
        /// </summary>
        public static FS.HISFC.BizLogic.Manager.Constant ConManager
        {
            get
            {
                if (conManager == null)
                {
                    conManager = new FS.HISFC.BizLogic.Manager.Constant();
                }

                return conManager;
            }
        }

        /// <summary>
        /// 常数缓存列表
        /// </summary>
        private static Dictionary<string, ArrayList> dicConList = null;

        /// <summary>
        /// 获取常数列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ArrayList GetConList(string type)
        {
            if (dicConList == null)
            {
                dicConList = new Dictionary<string, ArrayList>();
            }

            if (dicConList.ContainsKey(type))
            {
                return dicConList[type];
            }

            ArrayList alCon = ConManager.GetList(type);

            if (alCon != null)
            {
                dicConList.Add(type, alCon);
            }
            return alCon;
        }

        public LoadSIDataManager()
        {
            connect();
        }
        #region 变量
        private FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();
        private FS.HISFC.BizLogic.Manager.Spell mySpell = new FS.HISFC.BizLogic.Manager.Spell();
        private FS.HISFC.BizLogic.Pharmacy.Item myPhaItem = new FS.HISFC.BizLogic.Pharmacy.Item();
        private FS.HISFC.BizLogic.Fee.Item myFeeItem = new FS.HISFC.BizLogic.Fee.Item();


        DownloadManager downMgr = new DownloadManager();
        private Management.SIConnect myConn = null;
        private int loadProgress;       //只被属性调用

        private FS.FrameWork.Models.NeuLog errLog;

        private int progressCount;      //进度总量

        private ArrayList alPact = new ArrayList();

        private ArrayList alYDPact = new ArrayList(); //{1AD4F611-F1AA-45b1-B05F-F314F32426D5} zhang-wx 2017-01-11 增加异地医保的自动对照功能


        public bool isAddCompare = false;//是否增量对照

        public string PactID
        {
            get
            {
                //FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                //System.Collections.ArrayList al = conMgr.QueryConstantList("gzyb");
                //alPact = al;
                //foreach (FS.HISFC.Models.Base.Const con in al)
                //{
                //    if (con.Memo == "param")
                //    {
                //        return con.ID;
                //    }
                //}
                //return "";

                if (!string.IsNullOrEmpty(LoadSIDataManager.gzybParamCode))
                {
                    return LoadSIDataManager.gzybParamCode;
                }
                else
                {
                    System.Collections.ArrayList al = DownloadManager.GetConList("gzyb");

                    foreach (FS.HISFC.Models.Base.Const con in al)
                    {
                        if (con.Memo == "param")
                        {
                            LoadSIDataManager.gzybParamCode = con.ID;
                            return con.ID;
                        }
                    }
                }
                return "";
            }

        }
        //{1AD4F611-F1AA-45b1-B05F-F314F32426D5} zhang-wx 2017-01-11 增加异地医保的自动对照功能
        public string YDPactID
        {
            get
            {
                FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                System.Collections.ArrayList al = conMgr.QueryConstantList("ydyb");
                //alYDPact = al;
                foreach (FS.HISFC.Models.Base.Const con in al)
                {
                    if (con.Memo == "param")
                    {
                        alYDPact.Add(con);
                        return con.ID;
                    }
                }
                return "";
            }

        }

        string pactCode = string.Empty;

        /// <summary>
        /// 是否对非药品进行对照
        /// </summary>
        private bool isCompareUnDrug = true;
        /// <summary>
        /// 是否对非药品进行对照
        /// </summary>
        public bool IsCompareUnDrug
        {
            set
            {
                this.isCompareUnDrug = value;
            }
        }

        /// <summary>
        /// 下载进度
        /// </summary>
        public int LoadProgress
        {
            get
            {
                return this.loadProgress;
            }
        }

        private void SetProgress(int i)
        {
            this.loadProgress = i;
            evLoadProgressChanged();
        }

        /// <summary>
        /// 下载的数据总量
        /// </summary>
        public int ProgressCount
        {
            get { return progressCount; }
        }
        private void SetProgressCount(int i)
        {
            this.progressCount = i;
            SetProgress(0);
            evProgressCountChanged();
        }

        public delegate void LoadProgressChanged();


        #endregion

        #region  内部函数
        /// <summary>
        /// 连接到医保

        /// </summary>
        /// <returns></returns>
        private int connect()
        {
            try
            {
                myConn = new Management.SIConnect();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("连接医保服务器失败!请设置!" + ex.Message);

                FS.FrameWork.WinForms.Classes.Function.PopShowControl(new usSetConnectSqlServer());
                return -1;
            }
            return 0;
        }

        //{1AD4F611-F1AA-45b1-B05F-F314F32426D5} zhang-wx 2017-01-11 增加异地医保的自动对照功能
        private int UpdateLocalDrugYD(ArrayList alDrug, out string errMsg)
        {
            errMsg = "";
            //插入药品记录
            System.Windows.Forms.Application.DoEvents();

            FS.HISFC.Models.SIInterface.Item obj = null;
            for (int i = 0; i < alDrug.Count; i++)
            {
                //开始事务

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                myInterface.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                obj = alDrug[i] as FS.HISFC.Models.SIInterface.Item;
                Application.DoEvents();
                SetProgress(LoadProgress + 1);
                Application.DoEvents();
                if (obj.Name == "尼卡地平")
                {
                    string s = "";
                }
                if (downMgr.LoadYDSIItem(obj) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errMsg = "更新异地医保药品信息失败!  项目名称：" + obj.Name + myInterface.Err;
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            return 0;
        }
        //{1AD4F611-F1AA-45b1-B05F-F314F32426D5} zhang-wx 2017-01-11 增加异地医保的自动对照功能
        private int UpdateLocalUndrugYD(ArrayList alUndrug, out string errMsg)
        {
            errMsg = "";
            Application.DoEvents();
            //插入非药品记录

            for (int i = 0; i < alUndrug.Count; i++)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                myInterface.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                mySpell.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                FS.HISFC.Models.SIInterface.Item obj = alUndrug[i] as FS.HISFC.Models.SIInterface.Item;
                SetProgress(this.loadProgress + 1);//((i + alDrug.Count + 1) /(alDrug.Count + alUndrug.Count) * 100);
                Application.DoEvents();

                FS.HISFC.Models.Base.Spell sp = (FS.HISFC.Models.Base.Spell)mySpell.Get(obj.Name);

                if (sp != null)
                {
                    if (sp.SpellCode.Length > 9)
                    {
                        sp.SpellCode = sp.SpellCode.Substring(0, 10);
                    }
                    obj.SpellCode = sp.SpellCode;
                }
                if (this.downMgr.LoadYDSIItem(obj) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errMsg = "更新异地医保非药品信息失败!  项目名称：" + obj.Name + myInterface.Err;
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            return 0;
        }

        private int UpdateLocalDrug(ArrayList alDrug, out string errMsg)
        {
            errMsg = "";
            //插入药品记录
            System.Windows.Forms.Application.DoEvents();

            FS.HISFC.Models.SIInterface.Item obj = null;
            for (int i = 0; i < alDrug.Count; i++)
            {
                //开始事务

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                myInterface.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                obj = alDrug[i] as FS.HISFC.Models.SIInterface.Item;
                Application.DoEvents();
                SetProgress(LoadProgress + 1);
                Application.DoEvents();
                if (downMgr.LoadSIItem(obj) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errMsg = "更新医保药品信息失败!  项目名称：" + obj.Name + downMgr.Err;
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            return 0;
        }

        private int UpdateLocalUndrug(ArrayList alUndrug, out string errMsg)
        {
            errMsg = "";
            Application.DoEvents();
            //插入非药品记录

            for (int i = 0; i < alUndrug.Count; i++)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                myInterface.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                mySpell.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                FS.HISFC.Models.SIInterface.Item obj = alUndrug[i] as FS.HISFC.Models.SIInterface.Item;
                SetProgress(this.loadProgress + 1);//((i + alDrug.Count + 1) /(alDrug.Count + alUndrug.Count) * 100);
                Application.DoEvents();

                FS.HISFC.Models.Base.Spell sp = (FS.HISFC.Models.Base.Spell)mySpell.Get(obj.Name);

                if (sp != null)
                {
                    if (sp.SpellCode.Length > 9)
                    {
                        sp.SpellCode = sp.SpellCode.Substring(0, 10);
                    }
                    obj.SpellCode = sp.SpellCode;
                }
                if (this.downMgr.LoadSIItem(obj) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    errMsg = "更新医保非药品信息失败!  项目名称：" + obj.Name + downMgr.Err;
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            return 0;
        }


        /// <summary>
        /// 药品 由本地项目和医保项目生产对照信息
        /// </summary>
        /// <param name="hisObj"></param>
        /// <param name="centerItem"></param>
        /// <returns></returns>
        private FS.HISFC.Models.SIInterface.Compare GetDrugCompare(object hisObj, FS.HISFC.Models.SIInterface.Item centerItem)
        {
            FS.HISFC.Models.SIInterface.Compare objCom = new FS.HISFC.Models.SIInterface.Compare();
            FS.HISFC.Models.Pharmacy.Item phaItem = hisObj as FS.HISFC.Models.Pharmacy.Item;
            if (phaItem == null)
            {
                return null;
            }

            objCom.CenterItem.PactCode = !isAddCompare ? this.PactID : this.pactCode;
            objCom.HisCode = phaItem.ID;                   //本地药品编码
            objCom.CenterItem.ID = centerItem.ID;          //医保中心的编码

            objCom.CenterItem.SysClass = centerItem.SysClass;
            objCom.CenterItem.Name = centerItem.Name;
            objCom.CenterItem.EnglishName = centerItem.EnglishName;

            objCom.Name = phaItem.Name;
            objCom.RegularName = phaItem.NameCollection.RegularName;
            objCom.CenterItem.DoseCode = centerItem.DoseCode;
            objCom.CenterItem.Specs = centerItem.Specs;
            objCom.CenterItem.FeeCode = centerItem.FeeCode;
            objCom.CenterItem.ItemType = centerItem.ItemType;
            objCom.CenterItem.ItemGrade = centerItem.ItemGrade;
            objCom.CenterItem.Rate = centerItem.Rate;
            objCom.CenterItem.Price = centerItem.Price;
            objCom.CenterItem.Memo = centerItem.Memo;
            objCom.SpellCode.SpellCode = phaItem.SpellCode;
            objCom.SpellCode.WBCode = phaItem.WBCode;
            objCom.SpellCode.UserCode = phaItem.UserCode;        //本地自定义码
            objCom.Specs = phaItem.Specs;
            objCom.Price = phaItem.PriceCollection.PriceRate;
            objCom.DoseCode = phaItem.DosageForm.ID;
            objCom.CenterItem.OperCode = this.myInterface.Operator.ID;


            return objCom;
        }

        /// <summary>
        /// 非药品 由本地项目和医保项目生产对照信息
        /// </summary>
        /// <param name="hisObj"></param>
        /// <param name="centerItem"></param>
        /// <returns></returns>
        private FS.HISFC.Models.SIInterface.Compare GetUndrugCompare(object hisObj, FS.HISFC.Models.SIInterface.Item centerItem)
        {
            FS.HISFC.Models.SIInterface.Compare objCom = new FS.HISFC.Models.SIInterface.Compare();
            FS.HISFC.Models.Fee.Item.Undrug feeItem = hisObj as FS.HISFC.Models.Fee.Item.Undrug;
            if (feeItem == null)
            {
                return null;
            }
            objCom.CenterItem.PactCode = !isAddCompare ? this.PactID : this.pactCode;						//合同单位
            objCom.CenterItem.ID = centerItem.ID;							//中心项目代码
            objCom.CenterItem.SysClass = centerItem.SysClass;				//项目类别
            objCom.CenterItem.Name = centerItem.Name;						//中心项目名称
            objCom.CenterItem.EnglishName = centerItem.EnglishName;
            objCom.HisCode = feeItem.ID;									//本地项目代码
            objCom.Name = feeItem.Name;										//本地项目名称
            objCom.RegularName = "";

            objCom.CenterItem.DoseCode = centerItem.DoseCode;
            objCom.CenterItem.Specs = centerItem.Specs;
            objCom.CenterItem.FeeCode = centerItem.FeeCode;
            objCom.CenterItem.ItemType = centerItem.ItemType;
            objCom.CenterItem.ItemGrade = centerItem.ItemGrade;
            objCom.CenterItem.Rate = centerItem.Rate;
            objCom.CenterItem.Price = centerItem.Price;
            objCom.CenterItem.Memo = centerItem.Memo;
            objCom.SpellCode.SpellCode = feeItem.SpellCode;
            objCom.SpellCode.WBCode = feeItem.WBCode;
            objCom.SpellCode.UserCode = feeItem.UserCode;
            objCom.Specs = feeItem.Specs;
            objCom.Price = feeItem.Price;
            objCom.DoseCode = "";
            objCom.CenterItem.OperCode = this.myInterface.Operator.ID;


            return objCom;
        }

        /// <summary>
        /// 根据一条对照信息更新对照表
        /// </summary>
        /// <param name="objCom"></param>
        /// <returns></returns>
        private int UpdateCompanre(FS.HISFC.Models.SIInterface.Compare objCom)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            myInterface.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            int returnValue = 0;
            if (objCom.SpellCode.SpellCode.Length > 16)
            {
                objCom.SpellCode.SpellCode = objCom.SpellCode.SpellCode.Substring(0, 16);
            }
            if (objCom.SpellCode.WBCode.Length > 16)
            {
                objCom.SpellCode.WBCode = objCom.SpellCode.WBCode.Substring(0, 16);
            }
            //更新比例，如果更新不到，插入
            returnValue = this.downMgr.UpdateCompareItem(objCom);
            if (returnValue == 0)
            {
                returnValue = this.myInterface.InsertCompareItem(objCom);
                if (returnValue == -1)
                {
                    if (myInterface.DBErrCode == 1)
                    {
                        returnValue = myInterface.DeleteCompareItem(this.PactID, objCom.HisCode);
                        if (returnValue == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("删除对照失败!" + myInterface.Err);
                            return -1;
                        }
                        returnValue = this.myInterface.InsertCompareItem(objCom);
                        if (returnValue == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("插入对照失败!" + myInterface.Err);
                            return -1;
                        }

                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("插入对照失败!" + myInterface.Err);
                        return -1;
                    }
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }

        #endregion

        /// <summary>
        /// 下载进度改变时触发此事件
        /// </summary>
        public event LoadProgressChanged evLoadProgressChanged;
        /// <summary>
        /// 下载的总量改变时触发此事件
        /// </summary>
        public event LoadProgressChanged evProgressCountChanged;

        /// <summary>
        /// 将医保的数据下载到本地
        /// </summary>
        /// <returns></returns>
        public int LoadSIData()
        {
            //获得医保药品项目列表;
            ArrayList alDrug = new ArrayList();
            Application.DoEvents();
            alDrug = myConn.GetSIDrugList();
            if (alDrug == null)
            {
                MessageBox.Show("获得医保项目药品目录失败!" + myConn.Err);
                return -1;
            }
            //获得医保非药品项目列表;
            ArrayList alUndrug = new ArrayList();
            Application.DoEvents();
            alUndrug = myConn.GetSIUndrugList();
            this.SetProgressCount(alUndrug.Count + alDrug.Count);
            if (alUndrug == null)
            {
                MessageBox.Show("获得医保项目非药品目录失败!" + myConn.Err);
                return -1;
            }
            string errMsg;
            if (UpdateLocalDrug(alDrug, out errMsg) == -1)
            {
                MessageBox.Show(errMsg);
                return -1;
            }

            if (UpdateLocalUndrug(alUndrug, out errMsg) == -1)
            {
                MessageBox.Show(errMsg);
                return -1;
            }

            MessageBox.Show("下载完毕!");
            return 0;
        }


        /// <summary>
        /// {1AD4F611-F1AA-45b1-B05F-F314F32426D5} zhang-wx 2017-01-11 增加异地医保的自动对照功能
        /// 将异地医保的数据下载到本地
        /// </summary>
        /// <returns></returns>
        public int LoadYDSIData()
        {
            //获得医保药品项目列表;
            ArrayList alDrug = new ArrayList();
            Application.DoEvents();
            alDrug = myConn.GetYDSIDrugList();
            if (alDrug == null)
            {
                MessageBox.Show("获得医保项目药品目录失败!" + myConn.Err);
                return -1;
            }
            //获得医保非药品项目列表;
            ArrayList alUndrug = new ArrayList();
            Application.DoEvents();
            alUndrug = myConn.GetYDSIUndrugList();
            this.SetProgressCount(alUndrug.Count + alDrug.Count);
            if (alUndrug == null)
            {
                MessageBox.Show("获得医保项目非药品目录失败!" + myConn.Err);
                return -1;
            }
            string errMsg;
            if (UpdateLocalDrugYD(alDrug, out errMsg) == -1)
            {
                MessageBox.Show(errMsg);
                return -1;
            }

            if (UpdateLocalUndrugYD(alUndrug, out errMsg) == -1)
            {
                MessageBox.Show(errMsg);
                return -1;
            }

            MessageBox.Show("下载完毕!");
            return 0;
        }

        /// <summary>
        /// 根据医保服务器已对照信息自动进行医保项目对照
        /// </summary>
        /// <returns></returns>
        public int AutoCompare()
        {
            //由医保服务器获取已对照项目信息

            this.isAddCompare = false;
            ArrayList alCenterCompare = this.myConn.GetSICompareList();

            if (alCenterCompare == null)
            {
                MessageBox.Show(this.myConn.Err);
                return -1;
            }
            SetProgressCount(alCenterCompare.Count);
            int iErrCompareCount = 0;
            int iCompareCount = 0;
            Application.DoEvents();
            int iIndex = 0;
            foreach (FS.FrameWork.Models.NeuObject info in alCenterCompare)
            {
                iIndex++;
                SetProgress(iIndex);
                Application.DoEvents();
                if (info == null)
                {
                    iErrCompareCount++;
                    continue;
                }
                #region 对未对照项目进行对照

                if (info.Memo == "0")
                {
                    //非药品---------
                    ArrayList alUndrug = this.downMgr.GetItemsByUserCode(info.User01.Trim());
                    if (alUndrug == null || alUndrug.Count == 0)
                    {
                        this.WriteErr(info.Name + "  " + info.User01 + "未从本地获取到");
                        continue;
                    }
                    foreach (FS.HISFC.Models.Fee.Item.Undrug obj in alUndrug)
                    {
                        FS.HISFC.Models.SIInterface.Item objCenter = this.myInterface.GetCenterItemInfo(PactID, info.ID);
                        if (objCenter == null || objCenter.ID == "")
                        {
                            iErrCompareCount++;
                            continue;
                        }
                        if (this.CompareUndrug((object)obj, objCenter) == -1)
                        {
                            iErrCompareCount++;
                            continue;
                        }
                        //更新项目信息表的医保等级（拓展表）
                        if (-1 == this.downMgr.UpdateCenterGrade(obj.ID, objCenter.ItemGrade))
                        {
                            this.WriteErr("更新非药品信息表的医保等级（拓展表）！" + obj.ID + "医保等级：" + objCenter.ItemGrade);
                            continue;
                        }
                        iCompareCount++;
                    }
                }
                else
                {
                    //药品-----------
                    List<FS.HISFC.Models.Pharmacy.Item> alDrugItems = this.downMgr.GetArrayItemByUserCode(info.User01.Trim());
                    if (alDrugItems == null || alDrugItems.Count == 0)
                    {
                        this.WriteErr(info.Name + "  " + info.User01 + "未从本地获取到");
                        continue;
                    }
                    foreach (FS.HISFC.Models.Pharmacy.Item obj1 in alDrugItems)
                    {
                        // FS.HISFC.Models.Pharmacy.Item obj1 = this.downMgr.GetItemByFormalName(info.User01.Trim());
                        if (obj1 == null)
                        {
                            this.WriteErr(info.Name + "  " + info.User01 + "未从本地获取到");
                            continue;
                        }
                        FS.HISFC.Models.SIInterface.Item objCenter1 = this.myInterface.GetCenterItemInfo(PactID, info.ID);
                        if (objCenter1 == null || objCenter1.ID == "")
                        {
                            iErrCompareCount++;
                            this.WriteErr(info.Name + info.User01 + "未从医保获取到");
                            continue;
                        }
                        if (this.CompareDrug((object)obj1, objCenter1) == -1)
                        {
                            iErrCompareCount++;
                            this.WriteErr(obj1.Name + " 与" + objCenter1.Name + "对照失败");
                            continue;
                        }

                        //更新项目信息表的医保等级（拓展表）
                        if (-1 == this.downMgr.UpdateCenterGrade(obj1.ID, objCenter1.ItemGrade))
                        {
                            this.WriteErr("更新非药品信息表的医保等级（拓展表）！" + obj1.ID + "医保等级：" + objCenter1.ItemGrade);
                            continue;
                        }
                        if (-1 == this.downMgr.UpdateUndrugCenterGrade(obj1.ID, objCenter1.ItemGrade))
                        {
                            this.WriteErr("更新非药品信息表的医保等级！" + obj1.ID + "医保等级：" + objCenter1.ItemGrade);
                            continue;
                        }
                        iCompareCount++;
                        //  }
                    }
                }
                #endregion
            }
            MessageBox.Show("本次成功对照" + iCompareCount.ToString() + "条项目\n" + iErrCompareCount.ToString() + "条项目对照失败");
            return 1;
        }

        /// <summary>
        /// 根据医保服务器已对照信息自动进行增量医保项目对照
        /// </summary>
        /// <returns></returns>
        public int AutoAddCompare()
        {
            int rtnFlag = 0;

            try
            {
                this.isAddCompare = true;
                //由医保服务器获取已对照项目信息
                Hashtable htCenterCompare = this.myConn.GetSICompareHashTable();
                if (htCenterCompare == null)
                {
                    MessageBox.Show(this.myConn.Err);
                    return -1;
                }

                int iErrCompareCount = 0;
                int iCompareCount = 0;
                Application.DoEvents();
                int iIndex = 0;


                //获取本地未对照非药品项目
                ArrayList alUnDrug = this.downMgr.GetItemsForAddCompare(PactID);

                //获取本地未对照的药品项目
                List<FS.HISFC.Models.Pharmacy.Item> alDrug = this.downMgr.GetMedItemByForAddCompare(PactID);

                SetProgressCount(alUnDrug.Count + alDrug.Count);


                if (alUnDrug.Count > 0)
                {
                    foreach (FS.HISFC.Models.Fee.Item.Undrug item in alUnDrug)
                    {
                        iIndex++;
                        SetProgress(iIndex);
                        Application.DoEvents();
                        if (htCenterCompare.Contains(item.UserCode.Trim()))
                        {
                            FS.FrameWork.Models.NeuObject cenObj = htCenterCompare[item.UserCode.Trim()] as FS.FrameWork.Models.NeuObject;
                            if (cenObj == null || string.IsNullOrEmpty(cenObj.ID))
                            {
                                iErrCompareCount++;
                                continue;
                            }
                            //对照
                            FS.HISFC.Models.SIInterface.Item objCenter = null;
                            //foreach (FS.HISFC.Models.Base.Const con in alPact)
                            //{
                                objCenter = this.myInterface.GetCenterItemInfo(this.PactID, cenObj.ID);
                                if (objCenter == null || objCenter.ID == "")
                                {
                                    //本地医保项目没有，获取医保服务器项目，插入本地医保项目表
                                    #region 处理本地医保项目
                                    objCenter = myConn.GetSIUndrugItem(cenObj.ID);
                                    if (objCenter == null)
                                    {
                                        continue;
                                    }
                                    FS.HISFC.Models.Base.Spell sp = (FS.HISFC.Models.Base.Spell)mySpell.Get(objCenter.Name);

                                    if (sp != null)
                                    {
                                        if (sp.SpellCode.Length > 9)
                                        {
                                            sp.SpellCode = sp.SpellCode.Substring(0, 10);
                                        }
                                        objCenter.SpellCode = sp.SpellCode;
                                    }
                                    if (this.downMgr.LoadSIItem(objCenter, alPact) == -1)
                                    {
                                        this.WriteErr("更新医保非药品信息失败!  项目名称：" + objCenter.Name + downMgr.Err);
                                        iErrCompareCount++;
                                        continue;
                                    }

                                    rtnFlag++;

                                    #endregion
                                }
                                this.pactCode = PactID;
                                if (this.CompareUndrug((object)item, objCenter) == -1)
                                {
                                    iErrCompareCount++;
                                    continue;
                                }
                            //}

                            if (objCenter != null)
                            {
                                //更新项目信息表的医保等级（拓展表）
                                if (-1 == this.downMgr.UpdateCenterGrade(item.ID, objCenter.ItemGrade))
                                {
                                    this.WriteErr("更新非药品信息表的医保等级（拓展表）！" + item.ID + "医保等级：" + objCenter.ItemGrade);
                                    continue;
                                }
                                if (-1 == this.downMgr.UpdateUndrugCenterGrade(item.ID, objCenter.ItemGrade))
                                {
                                    this.WriteErr("更新非药品信息表的医保等级！" + item.ID + "医保等级：" + objCenter.ItemGrade);
                                    continue;
                                }
                            }
                            iCompareCount++;
                        }
                    }
                }

                if (alDrug.Count > 0)
                {
                    foreach (FS.HISFC.Models.Pharmacy.Item item in alDrug)
                    {
                        iIndex++;
                        SetProgress(iIndex);
                        Application.DoEvents();
                        //自定义码
                        string userCode = item.UserCode;
                        if (htCenterCompare.Contains(userCode.Trim()))
                        {
                            FS.FrameWork.Models.NeuObject cenObj = htCenterCompare[userCode.Trim()] as FS.FrameWork.Models.NeuObject;
                            if (cenObj == null || string.IsNullOrEmpty(cenObj.ID))
                            {
                                iErrCompareCount++;
                                continue;
                            }
                            //对照
                            FS.HISFC.Models.SIInterface.Item objCenter1 = null;
                            //foreach (FS.HISFC.Models.Base.Const con in alPact)
                            //{
                            objCenter1 = this.myInterface.GetCenterItemInfo(this.PactID, cenObj.ID);
                                if (objCenter1 == null || objCenter1.ID == "")
                                {
                                    //本地医保项目没有，获取医保服务器项目，插入本地医保项目表
                                    #region 处理医保本地项目
                                    objCenter1 = myConn.GetSIDrugItem(cenObj.ID);
                                    if (objCenter1 == null)
                                    {
                                        continue;
                                    }
                                    FS.HISFC.Models.Base.Spell sp = (FS.HISFC.Models.Base.Spell)mySpell.Get(objCenter1.Name);

                                    if (sp != null)
                                    {
                                        if (sp.SpellCode.Length > 9)
                                        {
                                            sp.SpellCode = sp.SpellCode.Substring(0, 10);
                                        }
                                        objCenter1.SpellCode = sp.SpellCode;
                                    }
                                    if (this.downMgr.LoadSIItem(objCenter1, alPact) == -1)
                                    {
                                        this.WriteErr("更新医保非药品信息失败!  项目名称：" + objCenter1.Name + downMgr.Err);
                                        iErrCompareCount++;
                                        continue;
                                    }


                                    rtnFlag++;

                                    #endregion
                                }
                                this.pactCode = this.PactID;
                                if (this.CompareDrug((object)item, objCenter1) == -1)
                                {
                                    iErrCompareCount++;
                                    continue;
                                }

                            //}
                            if (objCenter1 == null)
                            {
                                continue;
                            }
                            //更新项目信息表的医保等级
                            if (-1 == this.downMgr.UpdateCenterGrade(item.ID, objCenter1.ItemGrade))
                            {
                                this.WriteErr("更新药品信息表的医保等级！" + item.ID + "医保等级：" + objCenter1.ItemGrade);
                                continue;
                            }
                            iCompareCount++;
                        }
                    }
                }
                MessageBox.Show("本次成功对照" + iCompareCount.ToString() + "条项目\n" + iErrCompareCount.ToString() + "条项目对照失败");
            }
            catch (Exception exe)
            {
                this.WriteErr("错误：" + exe.Message);
            }

            if (rtnFlag > 0)
            {
                return 2;
            }

            return 1;
        }

        /// <summary>
        /// {1AD4F611-F1AA-45b1-B05F-F314F32426D5} zhang-wx 2017-01-11 增加异地医保的自动对照功能
        /// 根据异地医保服务器已对照信息自动进行增量医保项目对照
        /// </summary>
        /// <returns></returns>
        public int AutoAddCompareYD()
        {
            try
            {
                this.isAddCompare = true;
                //由医保服务器获取已对照项目信息
                Hashtable htCenterCompare = this.myConn.GetYDSICompareHashTable();
                if (htCenterCompare == null)
                {
                    MessageBox.Show(this.myConn.Err);
                    return -1;
                }

                int iErrCompareCount = 0;
                int iCompareCount = 0;
                Application.DoEvents();
                int iIndex = 0;


                //获取本地未对照非药品项目
                ArrayList alUnDrug = this.downMgr.GetItemsForAddCompare(YDPactID);

                //获取本地未对照的药品项目
                List<FS.HISFC.Models.Pharmacy.Item> alDrug = this.downMgr.GetMedItemByForAddCompare(YDPactID);

                SetProgressCount(alUnDrug.Count + alDrug.Count);


                if (alUnDrug.Count > 0)
                {
                    foreach (FS.HISFC.Models.Fee.Item.Undrug item in alUnDrug)
                    {
                        iIndex++;
                        SetProgress(iIndex);
                        if (htCenterCompare.Contains(item.UserCode.Trim()))
                        {
                            FS.FrameWork.Models.NeuObject cenObj = htCenterCompare[item.UserCode.Trim()] as FS.FrameWork.Models.NeuObject;
                            if (cenObj == null || string.IsNullOrEmpty(cenObj.ID))
                            {
                                iErrCompareCount++;
                                continue;
                            }
                            //对照
                            FS.HISFC.Models.SIInterface.Item objCenter = null;
                            foreach (FS.HISFC.Models.Base.Const con in alYDPact)
                            {
                                objCenter = this.myInterface.GetCenterItemInfo(con.ID.Trim(), cenObj.ID);
                                if (objCenter == null || objCenter.ID == "")
                                {

                                    //本地医保项目没有，获取医保服务器项目，插入本地医保项目表
                                    #region 处理本地医保项目
                                    objCenter = myConn.GetYDSIUndrugItem(cenObj.ID);
                                    if (objCenter == null)
                                    {
                                        continue;
                                    }
                                    FS.HISFC.Models.Base.Spell sp = (FS.HISFC.Models.Base.Spell)mySpell.Get(objCenter.Name);

                                    if (sp != null)
                                    {
                                        if (sp.SpellCode.Length > 9)
                                        {
                                            sp.SpellCode = sp.SpellCode.Substring(0, 10);
                                        }
                                        objCenter.SpellCode = sp.SpellCode;
                                    }
                                    if (this.downMgr.LoadYDSIItem(objCenter, alYDPact) == -1)
                                    {
                                        this.WriteErr("更新医保非药品信息失败!  项目名称：" + objCenter.Name + downMgr.Err);
                                        iErrCompareCount++;
                                        continue;
                                    }
                                    #endregion


                                }
                                this.pactCode = con.ID.Trim();
                                if (this.CompareUndrug((object)item, objCenter) == -1)
                                {
                                    iErrCompareCount++;
                                    continue;
                                }
                            }

                            //if (objCenter != null)
                            //{
                            //    //更新项目信息表的医保等级（拓展表）
                            //    if (-1 == this.downMgr.UpdateCenterGrade(item.ID, objCenter.ItemGrade))  
                            //    {
                            //        this.WriteErr("更新非药品信息表的医保等级（拓展表）！" + item.ID + "医保等级：" + objCenter.ItemGrade);
                            //        continue;
                            //    }
                            //    if (-1 == this.downMgr.UpdateUndrugCenterGrade(item.ID, objCenter.ItemGrade))
                            //    {
                            //        this.WriteErr("更新非药品信息表的医保等级！" + item.ID + "医保等级：" + objCenter.ItemGrade);
                            //        continue;
                            //    }
                            //}
                            iCompareCount++;
                        }
                    }
                }

                if (alDrug.Count > 0)
                {
                    foreach (FS.HISFC.Models.Pharmacy.Item item in alDrug)
                    {
                        iIndex++;
                        SetProgress(iIndex);
                        //自定义码
                        string userCode = item.UserCode;
                        if (htCenterCompare.Contains(userCode.Trim()))
                        {
                            FS.FrameWork.Models.NeuObject cenObj = htCenterCompare[userCode.Trim()] as FS.FrameWork.Models.NeuObject;
                            if (cenObj == null || string.IsNullOrEmpty(cenObj.ID))
                            {
                                iErrCompareCount++;
                                continue;
                            }
                            //对照
                            FS.HISFC.Models.SIInterface.Item objCenter1 = null;
                            foreach (FS.HISFC.Models.Base.Const con in alYDPact)
                            {
                                objCenter1 = this.myInterface.GetCenterItemInfo(con.ID.Trim(), cenObj.ID);
                                if (objCenter1 == null || objCenter1.ID == "")
                                {
                                    //本地医保项目没有，获取医保服务器项目，插入本地医保项目表
                                    #region 处理医保本地项目
                                    objCenter1 = myConn.GetYDSIDrugItem(cenObj.ID);
                                    if (objCenter1 == null)
                                    {
                                        continue;
                                    }
                                    FS.HISFC.Models.Base.Spell sp = (FS.HISFC.Models.Base.Spell)mySpell.Get(objCenter1.Name);

                                    if (sp != null)
                                    {
                                        if (sp.SpellCode.Length > 9)
                                        {
                                            sp.SpellCode = sp.SpellCode.Substring(0, 10);
                                        }
                                        objCenter1.SpellCode = sp.SpellCode;
                                    }
                                    if (this.downMgr.LoadYDSIItem(objCenter1, alYDPact) == -1) // 2017-01-11 修改到此处，明天继续
                                    {
                                        this.WriteErr("更新医保非药品信息失败!  项目名称：" + objCenter1.Name + downMgr.Err);
                                        iErrCompareCount++;
                                        continue;
                                    }
                                    #endregion
                                }
                                this.pactCode = con.ID.Trim();
                                if (this.CompareDrug((object)item, objCenter1) == -1)
                                {
                                    iErrCompareCount++;
                                    continue;
                                }

                            }
                            if (objCenter1 == null)
                            {
                                continue;
                            }
                            ////更新项目信息表的医保等级
                            //if (-1 == this.downMgr.UpdateCenterGrade(item.ID, objCenter1.ItemGrade))
                            //{
                            //    this.WriteErr("更新药品信息表的医保等级！" + item.ID + "医保等级：" + objCenter1.ItemGrade);
                            //    continue;
                            //}
                            iCompareCount++;
                        }
                    }
                }
                MessageBox.Show("本次成功对照" + iCompareCount.ToString() + "条项目\n" + iErrCompareCount.ToString() + "条项目对照失败");

            }
            catch (Exception exe)
            {
                this.WriteErr("错误：" + exe.Message);

            }
            return 1;
        }

        /// <summary>
        ///对照一条药品项目
        /// </summary>
        /// <param name="hisObj"></param>
        /// <param name="centerItem"></param>
        /// <returns></returns>
        public int CompareDrug(object hisObj, FS.HISFC.Models.SIInterface.Item centerItem)
        {
            try
            {
                FS.HISFC.Models.SIInterface.Compare objCom = new FS.HISFC.Models.SIInterface.Compare();
                objCom = GetDrugCompare(hisObj, centerItem);
                return UpdateCompanre(objCom);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 对照一条非药品项目
        /// </summary>
        /// <param name="hisObj"></param>
        /// <param name="centerItem"></param>
        /// <returns></returns>
        public int CompareUndrug(object hisObj, FS.HISFC.Models.SIInterface.Item centerItem)
        {
            try
            {
                FS.HISFC.Models.SIInterface.Compare objCom = new FS.HISFC.Models.SIInterface.Compare();
                objCom = GetUndrugCompare(hisObj, centerItem);
                return UpdateCompanre(objCom);
            }
            catch
            {
                return 0;
            }
        }

        private void WriteErr(string errMsg)
        {
            if (this.errLog == null)
            {
                this.errLog = new FS.FrameWork.Models.NeuLog("loadCompare.txt");
            }
            this.errLog.WriteLog(errMsg);
        }

        public int ExecGZYBCompare()
        {
            string message = "No Return";

            int ret = this.downMgr.ExecEvent("prc_gzyb_compare", ref message);
            return ret;
        }

        /// <summary>
        /// {1AD4F611-F1AA-45b1-B05F-F314F32426D5} zhang-wx 2017-01-11 增加异地医保的自动对照功能
        /// 执行异地医保增量对照fin_com_compare表
        /// </summary>
        /// <returns></returns>
        public int ExecYDYBCompare()
        {
            string message = "No Return";

            int ret = this.downMgr.ExecEvent("prc_ydyb_compare", ref message);
            return ret;
        }

        public int ExecGZYBSiitem()
        {
            string message = "No Return";

            int ret = this.downMgr.ExecEvent("prc_gzyb_siitem", ref message);
            return ret;
        }

        /// <summary>
        /// {1AD4F611-F1AA-45b1-B05F-F314F32426D5} zhang-wx 2017-01-11 增加异地医保的自动对照功能
        /// 增量对照插入对照表
        /// </summary>
        /// <returns></returns>
        public int ExecYDYBSiitem()
        {
            string message = "No Return";

            int ret = this.downMgr.ExecEvent("prc_ydyb_siitem", ref message);
            return ret;
        }
    }
}
