using System;
using System.Collections;
namespace FS.HISFC.BizLogic.Manager
{

	/// <summary>
    /// 层级医嘱
    /// {1EB2DEC4-C309-441f-BCCE-516DB219FD0E} 
	/// </summary>
	public class ItemLevel : DataBase
	{

		/// <summary>
		///医嘱 
		/// </summary>
        public ItemLevel()
		{
        }

        #region
        /// <summary>
        /// 获得新的组套文件夹ID
        /// </summary>
        /// <returns></returns>
        public string GetNewFolderID()
        {
            string strSql = "";
            if (this.GetSQL("Manager.ItemLevel.GetNewFolderID", ref strSql) == -1)
            {
                return "";
            }
            return this.ExecSqlReturnOne(strSql);
        }
        /// <summary>
        /// 插入或更新组套文件夹
        /// </summary>
        /// <param name="itemLevelFolder"></param>
        /// <returns></returns>
        public int SetNewFolder(FS.HISFC.Models.Fee.Item.ItemLevel itemLevelFolder)
        {
            //如果为空--获得
            if (itemLevelFolder.ID == "")
            {
                itemLevelFolder.ID = this.GetNewFolderID();
                if (itemLevelFolder.ID == "")
                {
                    return -1;
                }
            }
            //先更新
            int iRet = this.updateFolder(itemLevelFolder);
            if (iRet < 0)//出错
            {
                return -1;
            }
            else if (iRet == 0)//没有更新到
            {
                //插入
                int iReturn = this.insertFolder(itemLevelFolder);
                if (iReturn < 0)//出错
                {
                    return -1;
                }
                return iReturn;
            }
            //返回
            return iRet;
        }
        /// <summary>
        /// 更新组套文件夹
        /// </summary>
        /// <param name="itemLevelFolder"></param>
        /// <returns></returns>
        public int updateFolder(FS.HISFC.Models.Fee.Item.ItemLevel itemLevelFolder)
        {
            string strSql = "";
            if (this.GetSQL("Manager.ItemLevel.UpdateFolder", ref strSql) == -1)
            {
                return -1;
            }

            strSql = System.String.Format(strSql, itemLevelFolder.ID, itemLevelFolder.Name, itemLevelFolder.SpellCode, itemLevelFolder.WBCode,
                itemLevelFolder.InOutType, 
                itemLevelFolder.Dept.ID, itemLevelFolder.Owner.ID, FS.FrameWork.Function.NConvert.ToInt32(itemLevelFolder.IsShared),
                itemLevelFolder.Memo, this.Operator.ID,
                itemLevelFolder.ParentID, //上级目录ID  {C2922531-DEE7-43a0-AB7A-CDD7C58691BD}
                itemLevelFolder.LevelClass.ID,
                itemLevelFolder.SortID
                );
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// 插入一个目录
        /// </summary>
        /// <param name="itemLevelFolder"></param>
        /// <returns></returns>
        private int insertFolder(FS.HISFC.Models.Fee.Item.ItemLevel itemLevelFolder)
        {
            string strSql = "";
            if (this.GetSQL("Manager.ItemLevel.InsertFolder", ref strSql) == -1)
            {
                return -1;
            }
            strSql = System.String.Format(strSql, itemLevelFolder.ID, itemLevelFolder.Name, itemLevelFolder.SpellCode, itemLevelFolder.WBCode,
               itemLevelFolder.InOutType,
               itemLevelFolder.Dept.ID, itemLevelFolder.Owner.ID, FS.FrameWork.Function.NConvert.ToInt32(itemLevelFolder.IsShared),
               itemLevelFolder.Memo, this.Operator.ID,
               itemLevelFolder.ParentID, //上级目录ID  {C2922531-DEE7-43a0-AB7A-CDD7C58691BD}
               itemLevelFolder.LevelClass.ID,
               itemLevelFolder.SortID
               );
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// 删除文件夹，同时更新其所属组套为没有文件夹
        /// </summary>
        /// <param name="groupFolder"></param>
        /// <returns></returns>
        public int deleteFolder(FS.HISFC.Models.Fee.Item.ItemLevel itemLevelFolder)
        {
            string strSql = "";
            string strSql1 = "";
            if (this.GetSQL("Manager.ItemLevel.deleteFolder", ref strSql) == -1)
            {
                return -1;
            }
            if (this.GetSQL("Manager.ItemLevel.updateFolderIDNull", ref strSql1) == -1)
            {
                return -1;
            }
            strSql = System.String.Format(strSql, itemLevelFolder.ID);

            if (this.ExecNoQuery(strSql) < 0)
            {
                return -1;
            }
            strSql1 = System.String.Format(strSql1, itemLevelFolder.ID);

            //if (this.ExecNoQuery(strSql1) < 0)
            //{
            //    return -1;
            //}

            return 1;
        }


        /// <summary>
        /// 获得所有一级目录
        /// </summary>
        /// <param name="inouType"></param>
        /// <param name="levelClass"></param>
        /// <returns></returns>
        public ArrayList GetAllFirstLVFolder(int inouType, string levelClass)
        {
            string strSql = "";
            if (this.GetSQL("Manager.ItemLevel.GetAllFirstLVFolder", ref strSql) == -1)
            {
                return null;
            }
            strSql = System.String.Format(strSql, inouType, levelClass);
            return GetFolder(strSql);
        }

        /// <summary>
        /// 按照目录ID取下面的所有目录
        /// </summary>
        /// <param name="folderID"></param>
        /// <returns></returns>
        public ArrayList GetAllFolderByFolderID(string folderID)
        {
            string strSql = "";
            if (this.GetSQL("Manager.ItemLevel.GetAllFolderByFolderID", ref strSql) == -1)
            {
                return null;
            }
            strSql = System.String.Format(strSql, folderID);
            return GetFolder(strSql);
        }

        private ArrayList GetFolder(string strSql)
        {
            if (this.ExecQuery(strSql) < 0)
            {
                return null;
            }
            ArrayList al = new ArrayList();
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Fee.Item.ItemLevel itemLevel = new FS.HISFC.Models.Fee.Item.ItemLevel();
                itemLevel.ID = this.Reader[0].ToString();//编码
                itemLevel.Name = this.Reader[1].ToString();//名称
                itemLevel.InOutType = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());
                itemLevel.Dept.ID = this.Reader[3].ToString();//科室代码
                itemLevel.Owner.ID = this.Reader[4].ToString();//所属人代码
                itemLevel.SpellCode = this.Reader[5].ToString();
                itemLevel.WBCode = this.Reader[6].ToString();
                itemLevel.IsShared = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[7].ToString());
                itemLevel.Memo = this.Reader[8].ToString();//备注
                itemLevel.UserCode = "F";//代表是文件夹
                itemLevel.ParentID = this.Reader[9].ToString();//上级文件夹编码
                itemLevel.LevelClass.ID = this.Reader[10].ToString();
                itemLevel.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[11]);

                al.Add(itemLevel);
            }
            this.Reader.Close();
            return al;
        }


        /// <summary>
        /// 插入一个项目
        /// </summary>
        /// <param name="itemLevel"></param>
        /// <returns></returns>
        public int insertItem(FS.HISFC.Models.Fee.Item.ItemLevel itemLevel)
        {
            string strSql = "";
            if (this.GetSQL("Manager.ItemLevel.InsertItem", ref strSql) == -1)
            {
                return -1;
            }
            strSql = System.String.Format(strSql, itemLevel.ID, itemLevel.Name, itemLevel.SpellCode, itemLevel.WBCode,
               itemLevel.InOutType,
               itemLevel.Dept.ID, itemLevel.Owner.ID, FS.FrameWork.Function.NConvert.ToInt32(itemLevel.IsShared),
               itemLevel.Memo, this.Operator.ID,
               itemLevel.ParentID, //上级目录ID  {C2922531-DEE7-43a0-AB7A-CDD7C58691BD}
               itemLevel.LevelClass.ID,
               itemLevel.SortID
               );
            return this.ExecNoQuery(strSql);
        }


        public int deleteItem(string itemID, string parentID)
        {
            string strSql = "";
            if (this.GetSQL("Manager.ItemLevel.deleteItem", ref strSql) == -1)
            {
                return -1;
            }

            strSql = System.String.Format(strSql, itemID, parentID);

            if (this.ExecNoQuery(strSql) < 0)
            {
                return -1;
            }

            return 1;
        }

        public int UpdateItemSortID(string SortID, string itemID, string parentID)
        {
            string strSql = "";
            if (this.GetSQL("Manager.ItemLevel.UpdateItemSortID", ref strSql) == -1)
            {
                return -1;
            }

            strSql = System.String.Format(strSql, SortID, itemID, parentID);

            if (this.ExecNoQuery(strSql) < 0)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 按照目录ID取下面的所有项目
        /// </summary>
        /// <param name="folderID"></param>
        /// <returns></returns>
        public ArrayList GetAllItemByFolderID(string folderID)
        {
            string strSql = "";
            if (this.GetSQL("Manager.ItemLevel.GetAllItemByFolderID", ref strSql) == -1)
            {
                return null;
            }
            strSql = System.String.Format(strSql, folderID);
            return GetFolder(strSql);
        }

        #endregion
    }
	
}