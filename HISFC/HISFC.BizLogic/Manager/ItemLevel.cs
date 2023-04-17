using System;
using System.Collections;
namespace FS.HISFC.BizLogic.Manager
{

	/// <summary>
    /// �㼶ҽ��
    /// {1EB2DEC4-C309-441f-BCCE-516DB219FD0E} 
	/// </summary>
	public class ItemLevel : DataBase
	{

		/// <summary>
		///ҽ�� 
		/// </summary>
        public ItemLevel()
		{
        }

        #region
        /// <summary>
        /// ����µ������ļ���ID
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
        /// �������������ļ���
        /// </summary>
        /// <param name="itemLevelFolder"></param>
        /// <returns></returns>
        public int SetNewFolder(FS.HISFC.Models.Fee.Item.ItemLevel itemLevelFolder)
        {
            //���Ϊ��--���
            if (itemLevelFolder.ID == "")
            {
                itemLevelFolder.ID = this.GetNewFolderID();
                if (itemLevelFolder.ID == "")
                {
                    return -1;
                }
            }
            //�ȸ���
            int iRet = this.updateFolder(itemLevelFolder);
            if (iRet < 0)//����
            {
                return -1;
            }
            else if (iRet == 0)//û�и��µ�
            {
                //����
                int iReturn = this.insertFolder(itemLevelFolder);
                if (iReturn < 0)//����
                {
                    return -1;
                }
                return iReturn;
            }
            //����
            return iRet;
        }
        /// <summary>
        /// ���������ļ���
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
                itemLevelFolder.ParentID, //�ϼ�Ŀ¼ID  {C2922531-DEE7-43a0-AB7A-CDD7C58691BD}
                itemLevelFolder.LevelClass.ID,
                itemLevelFolder.SortID
                );
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ����һ��Ŀ¼
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
               itemLevelFolder.ParentID, //�ϼ�Ŀ¼ID  {C2922531-DEE7-43a0-AB7A-CDD7C58691BD}
               itemLevelFolder.LevelClass.ID,
               itemLevelFolder.SortID
               );
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ɾ���ļ��У�ͬʱ��������������Ϊû���ļ���
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
        /// �������һ��Ŀ¼
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
        /// ����Ŀ¼IDȡ���������Ŀ¼
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
                itemLevel.ID = this.Reader[0].ToString();//����
                itemLevel.Name = this.Reader[1].ToString();//����
                itemLevel.InOutType = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());
                itemLevel.Dept.ID = this.Reader[3].ToString();//���Ҵ���
                itemLevel.Owner.ID = this.Reader[4].ToString();//�����˴���
                itemLevel.SpellCode = this.Reader[5].ToString();
                itemLevel.WBCode = this.Reader[6].ToString();
                itemLevel.IsShared = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[7].ToString());
                itemLevel.Memo = this.Reader[8].ToString();//��ע
                itemLevel.UserCode = "F";//�������ļ���
                itemLevel.ParentID = this.Reader[9].ToString();//�ϼ��ļ��б���
                itemLevel.LevelClass.ID = this.Reader[10].ToString();
                itemLevel.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[11]);

                al.Add(itemLevel);
            }
            this.Reader.Close();
            return al;
        }


        /// <summary>
        /// ����һ����Ŀ
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
               itemLevel.ParentID, //�ϼ�Ŀ¼ID  {C2922531-DEE7-43a0-AB7A-CDD7C58691BD}
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
        /// ����Ŀ¼IDȡ�����������Ŀ
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