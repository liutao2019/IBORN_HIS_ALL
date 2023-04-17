using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment
{
    public class SpecInOper
    {
        /// <summary>
        /// 操作人
        /// </summary>
        private FS.HISFC.Models.Base.Employee loginPerson;

        /// <summary>
        /// 入库管理对象
        /// </summary>
        private SpecInManage specInManage;

        /// <summary>
        /// 分装标本管理对象
        /// </summary>
        private SubSpecManage subSpecManage;

        /// <summary>
        /// 库存管理对象
        /// </summary>
        private SpecSourcePlanManage specPlanManage;

        /// <summary>
        /// 标本盒管理对象
        /// </summary>
        private SpecBoxManage specBoxManage;

        /// <summary>
        /// 标本盒规格管理对象
        /// </summary>
        private BoxSpecManage boxSpecManage;

        private SpecTypeManage specTypeManage;

        /// <summary>
        /// 需要入库的标本
        /// </summary>
        private SubSpec subSpec;

        /// <summary>
        /// 操作的事务
        /// </summary>
        private System.Data.IDbTransaction trans;

        /// <summary>
        /// 当前的标本类型
        /// </summary>
        private string specTypeId;

        /// <summary>
        /// 病种类型
        /// </summary>
        private string disTypeId;

        /// <summary>
        /// 当前类型是血还是组织，O 组织
        /// </summary>
        private string orgOrBld;
        //863的支数，只有组织标本才有
        private int s863;
        //为了不改变LocateSubSpec 函数，当前的863支数设为全局变量
        int tmp863 = 0;
        //115的支数，只有组织标本才有
        private int s115;
        int tmp115 = 0;
        private SpecBox fullSpecBox;

        /// <summary>
        /// 已满标本盒
        /// </summary>
        private List<SpecBox> useFullBoxList;

        #region 属性
        public SubSpec SubSpec
        {
            get
            {
                return subSpec;
            }
            set
            {
                subSpec = value;
            }
        }

        public FS.HISFC.Models.Base.Employee LoginPerson
        {
            set
            {
                loginPerson = value;
            }
        }

        public System.Data.IDbTransaction Trans
        {
            set
            {
                trans = value;
            }
        }

        public string SpecTypeId
        {
            set
            {
                specTypeId = value;
            }
        }

        public string DisTypeId
        {
            set
            {
                disTypeId = value;
            }
        }

        public List<SpecBox> UseFullBoxList
        {
            get
            {
                return useFullBoxList;
            }
            set
            {
                useFullBoxList = value;
            }
        }

        public SpecBox FullSpecBox
        {
            get
            {
                return fullSpecBox;
            }
            set
            {
                fullSpecBox = value;
            }
        }

        /// <summary>
        /// 是血标本还是组织标本 O 组织 B 血
        /// </summary>
        public string OrgOrBld
        {
            get
            {
                return orgOrBld;
            }
            set
            {
                orgOrBld = value;
            }
        }

        /// <summary>
        /// 863的支数，只有组织标本才有
        /// </summary>
        public int S863
        {
            set
            {
                s863 = value;
            }
        }

        /// <summary>
        /// 115的支数，只有组织标本才有
        /// </summary>
        public int S115
        {
            set
            {
                s115 = value;
            }
        }
        #endregion

        public SpecInOper()
        {
            specInManage = new SpecInManage();
            subSpecManage = new SubSpecManage();
            specPlanManage = new SpecSourcePlanManage();
            specBoxManage = new SpecBoxManage();
            boxSpecManage = new BoxSpecManage();
            specTypeManage = new SpecTypeManage();
            useFullBoxList = new List<SpecBox>();
            fullSpecBox = new SpecBox();
            orgOrBld = "";
        }

        /// <summary>
        ///  设置事务
        /// </summary>
        public void SetTrans()
        {
            specInManage.SetTrans(trans);
            subSpecManage.SetTrans(trans);
            specPlanManage.SetTrans(trans);
            specBoxManage.SetTrans(trans);
            specTypeManage.SetTrans(trans);
            boxSpecManage.SetTrans(trans);
        }

        /// <summary>
        /// 保存当前入库的标本信息
        /// </summary>
        /// <param name="count">容量</param>
        /// <returns></returns>
        private int SaveSpecInInfo(decimal count)
        {
            SpecIn specIn = new SpecIn();
            string squence = "";
            specInManage.GetNextSequence(ref squence);
            specIn.InId = Convert.ToInt32(squence);
            specIn.InTime = DateTime.Now;
            specIn.OperId = loginPerson.ID;
            specIn.OperName = loginPerson.Name;
            specIn.Count = count;
            specIn.BoxId = subSpec.BoxId;
            specIn.Row = subSpec.BoxEndRow;
            specIn.Col = subSpec.BoxEndCol;
            specIn.Comment = subSpec.Comment;
            specIn.SubSpecBarCode = subSpec.SubBarCode;
            //specOut.SpecTypeId = specTypeId;
            specIn.SubSpecId = subSpec.SubSpecId;
            specIn.Unit = "";
            return specInManage.InsertSubSpecIn(specIn);
        }

        /// <summary>
        /// 更新库存
        /// </summary>
        /// <param name="subSpecId">入库标本ID</param>
        /// <returns></returns>
        private int UpdateSpecStore(string subSpecId)
        {
            //取出标本对应的SotreID
            SpecSourcePlan plan = specPlanManage.GetPlanById("", subSpecId);
            if (plan == null || plan.PlanID <= 0)
            {
                return -1;
            }
            plan.StoreCount = plan.StoreCount + 1;
            if (specPlanManage.UpdateSpecPlan(plan) <= 0)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 标本返回入库时使用
        /// </summary>
        /// <returns></returns>
        public int InOper()
        {
            if (UpdateSpecStore(subSpec.SubSpecId.ToString()) <= 0)
            {
                return -1;
            }
            return SaveSpecInInfo(1.0M);
        }

        /// <summary>
        /// 标本入库时使用
        /// </summary>
        /// <returns></returns>
        public int InOperInit()
        {
            return SaveSpecInInfo(1.0M);
        }

        /// <summary>
        /// 分配标本位置
        /// </summary>
        /// <returns>1：成功，-1 失败 -2 没有位置</returns>
        public int LocateSubSpec()
        {
            ArrayList arrBox = new ArrayList();
            SpecBox currentBox = new SpecBox();
            BoxSpec boxSpec = new BoxSpec();
            SubSpec lastSubSpec = new SubSpec();

            arrBox = specBoxManage.GetLastLocation(disTypeId, specTypeId);
            foreach (SpecBox box in arrBox)
            {
                boxSpec = boxSpecManage.GetSpecByBoxId(box.BoxId.ToString());
                SubSpec sub = new SubSpec();
                sub = subSpecManage.GetSubSpecByLocate(box.BoxId.ToString(), boxSpec.Row.ToString(), boxSpec.Col.ToString());
                if (sub != null && sub.SubSpecId > 0)
                {
                    continue;
                }
                if (specTypeId == "7")
                {
                    currentBox = box;
                    break;
                }
                //如果需要放入863的盒子
                if (orgOrBld == "O" && s863 > 0 && tmp863 < s863)
                {
                    if (box.SpecialUse != "8")
                    {
                        continue;
                    }
                    else
                    {
                        currentBox = box;
                        tmp863++;
                        break;
                    }
                }
                else
                {
                    //如果需要放入115的盒子
                    if (orgOrBld == "O" && s115 > 0 && tmp115 < s115)
                    {
                        if (box.SpecialUse != "1")
                        {
                            continue;
                        }
                        else
                        {
                            currentBox = box;
                            tmp115++;
                            break;
                        }
                    }
                    else
                    {
                        if (box.SpecialUse == "8" || box.SpecialUse == "1")
                        {
                            continue;
                        }
                        else
                        {
                            currentBox = box;
                            break;
                        }
                    }
                }
            }
            if (currentBox.BoxId <= 0)
            {
                string specName = specTypeManage.GetSpecTypeById(specTypeId).SpecTypeName;
                System.Windows.Forms.MessageBox.Show(specName + "类型，查找标本盒失败！请添加!");
                return -2;
            }
            subSpec.BoxId = currentBox.BoxId;
            if (currentBox.DesCapType == 'B')
            {
                lastSubSpec = subSpecManage.GetLastSpecForTmp(currentBox.BoxId.ToString());
            }
            else
            {
                lastSubSpec = subSpecManage.GetLastSpecInBox(currentBox.BoxId.ToString());
            }
            //boxSpec = boxSpecManage.GetSpecByBoxId(currentBox.BoxId.ToString());
            int maxRow = boxSpec.Row;
            int maxCol = boxSpec.Col;
            int currentEndRow = lastSubSpec.BoxEndRow;
            int currentEndCol = lastSubSpec.BoxEndCol;

            //如果是放在存储柜中,先列后行
            if (currentBox.DesCapType == 'B')
            {
                currentEndCol = currentEndCol == 0 ? 1 : currentEndCol;
                if (currentEndRow < maxRow)
                {
                    subSpec.BoxEndRow = currentEndRow + 1;
                    subSpec.BoxStartRow = currentEndRow + 1;
                    subSpec.BoxStartCol = currentEndCol;
                    subSpec.BoxEndCol = currentEndCol;
                }
                if (currentEndCol < maxCol && currentEndRow == maxRow)
                {
                    subSpec.BoxEndCol = currentEndCol + 1;
                    subSpec.BoxStartCol = currentEndCol + 1;
                    subSpec.BoxStartRow = 1;
                    subSpec.BoxEndRow = 1;
                }
            }
            //先行后列
            else
            {
                if (currentEndCol < maxCol)
                {
                    subSpec.BoxStartCol = currentEndCol + 1;
                    subSpec.BoxEndCol = currentEndCol + 1;
                    subSpec.BoxEndRow = currentEndRow;
                    subSpec.BoxStartRow = subSpec.BoxEndRow;
                }
                if (currentEndCol == maxCol && currentEndRow < maxRow)
                {
                    subSpec.BoxEndCol = 1;
                    subSpec.BoxStartCol = 1;
                    subSpec.BoxStartRow = currentEndRow + 1;
                    subSpec.BoxEndRow = currentEndRow + 1;
                }
            }
            int occupyCount = currentBox.OccupyCount + 1;
            if (specBoxManage.UpdateOccupyCount(occupyCount.ToString(), currentBox.BoxId.ToString()) == -1)
                return -1;
            bool isFull = false;
            if (occupyCount == currentBox.Capacity)
            {
                isFull = true;
            }
            if (subSpec.BoxEndCol >= maxCol && subSpec.BoxEndRow >= maxRow)
            {
                isFull = true;
            }

            //如果是组织标本 放到最后一行的时候提示用户添加标本盒
            if (orgOrBld != null && orgOrBld == "O" && currentBox.DesCapType == 'J')
            {
                //组织标本：只要加入了1个标本 就添加一个新的盒子
                if (subSpec.BoxEndRow == maxCol && subSpec.BoxEndCol == maxRow)
                {
                    useFullBoxList.Add(currentBox);
                }
            }

            if (isFull)
            {
                if (specBoxManage.UpdateOccupy(currentBox.BoxId.ToString(), "1") == -1)
                {
                    return -1;
                }
                if (specBoxManage.UpdateSotreFlag("1", currentBox.BoxId.ToString()) == -1)
                {
                    return -1;
                }
                fullSpecBox = currentBox;
                if (orgOrBld == null || orgOrBld != "O")
                {
                    if (currentBox.DesCapType == 'J')
                    {
                        useFullBoxList.Add(currentBox);
                    }
                }
            }
            //如果放了第一个标本 自动打印标本盒标签
            if (subSpec.BoxEndRow == 1 && subSpec.BoxEndCol == 1)
            {
                PrintLabel.PrintBoxBarCode(currentBox.BoxId.ToString(), currentBox.BoxBarCode, trans, 3);
            }
            return 1;
        }

        /// <summary>
        /// 保存分装标本
        /// </summary>       
        /// <returns></returns>
        public int SaveSubSpec(SpecSourcePlan sp, ref List<SubSpec> subList)
        {
            subSpec = new SubSpec();
            for (int k = 1; k <= sp.Count; k++)
            {
                //依次读取条形码
                if (sp.SubSpecCodeList.Count >= k)
                {
                    subSpec.SubBarCode = sp.SubSpecCodeList[k - 1] == null ? "" : sp.SubSpecCodeList[k - 1];
                }
                subSpec.StoreID = sp.PlanID;
                subSpec.SpecId = sp.SpecID;
                subSpec.SpecCount = 1;
                subSpec.SpecCap = sp.Capacity;
                subSpec.Status = "1";
                subSpec.SpecTypeId = sp.SpecType.SpecTypeID;
                subSpec.InStore = "S";
                subSpec.StoreTime = DateTime.Now;
                int tmpResult = LocateSubSpec();
                if (tmpResult <= -1)
                {
                    return tmpResult;
                }
                string sequence = "";
                subSpecManage.GetNextSequence(ref sequence);
                subSpec.SubSpecId = 400 + Convert.ToInt32(sequence);
                subSpec.Comment = "标本初始入库";
                if (subSpecManage.InsertSubSpec(subSpec) <= 0)
                {
                    return -1;
                }
                if (InOperInit() == -1)
                {
                    return -1;
                }
                subList.Add(subSpec);
                subSpec = new SubSpec();
            }
            tmp863 = 0;
            s863 = 0;
            tmp115 = 0;
            s115 = 0;
            return 1;
        }

        public int TransferSpec(SpecBox sourceBox, SpecBox desBox, SubSpec updatedSub)
        {

            updatedSub.BoxId = desBox.BoxId;
            BoxSpec boxSpec = boxSpecManage.GetSpecByBoxId(desBox.BoxId.ToString());
            int maxRow = boxSpec.Row;
            int maxCol = boxSpec.Col;
            //查找标本位置
            SubSpec lastSubSpec = subSpecManage.ScanSpecBox(desBox.BoxId.ToString(), boxSpec);
            int currentEndRow = lastSubSpec.BoxEndRow;
            int currentEndCol = lastSubSpec.BoxEndCol;
            if (currentEndCol < maxCol)
            {
                updatedSub.BoxStartCol = currentEndCol + 1;
                updatedSub.BoxEndCol = currentEndCol + 1;
                updatedSub.BoxEndRow = currentEndRow;
                updatedSub.BoxStartRow = currentEndRow;
            }
            if (currentEndCol == maxCol && currentEndRow < maxRow)
            {
                updatedSub.BoxEndCol = 1;
                updatedSub.BoxStartCol = 1;
                updatedSub.BoxStartRow = currentEndRow + 1;
                updatedSub.BoxEndRow = currentEndRow + 1;
            }

            int result = -1;

            if (desBox.BoxId != sourceBox.BoxId)
            {
                //更新当前盒子的占用量
                result = specBoxManage.UpdateOccupyCount((desBox.OccupyCount + 1).ToString(), desBox.BoxId.ToString());
                if (result <= 0)
                {
                    MessageBox.Show("更新标本盒失败！");
                    return -1;
                }

                bool isFull = false;
                if (updatedSub.BoxEndCol >= maxCol && updatedSub.BoxEndRow >= maxRow)
                {
                    isFull = true;
                }
                if (desBox.OccupyCount == desBox.Capacity)
                {
                    isFull = true;
                }
                //如果当前盒子满了提示入库,并更新
                if (isFull)
                {
                    if (specBoxManage.UpdateOccupy(desBox.BoxId.ToString(), "1") <= 0)
                    {
                        return -1;
                    }
                    if (specBoxManage.UpdateSotreFlag("1", desBox.BoxId.ToString()) <= 0)
                    {
                        return -1;
                    }
                }

                if (specBoxManage.UpdateOccupyCount((sourceBox.OccupyCount - 1).ToString(), sourceBox.BoxId.ToString()) <= 0)
                {
                    MessageBox.Show("更新标本盒失败！");
                    return -1;
                }
            }
            if (subSpecManage.UpdateSubSpec(updatedSub) <= 0)
            {
                MessageBox.Show("更新标本位置失败！");
                return -1;
            }

            return 1;

        }
    }
}
