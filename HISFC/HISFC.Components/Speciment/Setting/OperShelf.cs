using System;
using System.Collections;
using System.Text;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Windows.Forms;

namespace FS.HISFC.Components.Speciment.Setting
{
    public  class OperShelf
    {
        private IceBoxLayerManage layerManage=new IceBoxLayerManage();
        private IceBoxManage iceBoxManage=new IceBoxManage();
        private ShelfManage shelfManage = new ShelfManage();
        private SubSpecManage subSpecManage = new SubSpecManage();
        private CapLogManage capLogManage = new CapLogManage(); 
        private SpecBoxManage specBoxManage = new SpecBoxManage();
        private FS.HISFC.Models.Base.Employee loginPerson = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;


        /// <summary>
        /// 移除冻存架
        /// </summary>
        /// <param name="curShelf">要移除的冻存架</param>
        public void RemoveShelf(ref Shelf curShelf)
        {
            ArrayList arrSubSpec = new ArrayList();
            arrSubSpec = subSpecManage.GetSubSpecInLayerOrShelf(curShelf.ShelfID.ToString(), "IJ");
            if (arrSubSpec.Count > 0 && arrSubSpec != null)
            {
                MessageBox.Show("有标本存放，不能移除！");
                return;
            }
            DialogResult dialog = MessageBox.Show("此操作将移除冻存架中所有标本盒的信息，继续？", "移除", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.No)
                return;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                capLogManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                shelfManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                layerManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                specBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                
                //写入日志
                if (capLogManage.DisuseShelf(curShelf, loginPerson.Name, "D", "移除冻存架") == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("操作失败！");
                    return;
                }
               
                IceBoxLayer tmpLayer = layerManage.GetLayerById(curShelf.IceBoxLayer.LayerId.ToString());
                if (tmpLayer == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("操作失败！");
                    return;
                }
                int occupyCount = tmpLayer.OccupyCount - 1;
                if (occupyCount <= 0)
                    occupyCount = 0;
                if (layerManage.UpdateOccupy("0", tmpLayer.LayerId.ToString()) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("操作失败！");
                    return;
                }
                if (layerManage.UpdateOccupyCount(occupyCount.ToString(), tmpLayer.LayerId.ToString()) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("操作失败！");
                    return;
                }

                curShelf.Comment = "移除";
                curShelf.IceBoxLayer.Row = 0;
                curShelf.IceBoxLayer.Col = 0;
                curShelf.IceBoxLayer.LayerId = 0;
                curShelf.IceBoxLayer.Height = 0;
                curShelf.IsOccupy = '0';
                curShelf.OccupyCount = 0;
                //更新架子
                if (shelfManage.UpdateShelf(curShelf) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    System.Windows.Forms.MessageBox.Show("操作失败！");
                    return;
                }

                ArrayList arrBox = new ArrayList();
                arrBox = specBoxManage.GetBoxByCap(curShelf.ShelfID.ToString(), 'J');
                foreach (SpecBox sb in arrBox)
                {
                    SpecBox tmp = new SpecBox();
                    tmp = sb;
                    tmp.Capacity = 0;
                    tmp.DesCapCol = 0;
                    tmp.DesCapID = 0;
                    tmp.DesCapRow = 0;
                    tmp.DesCapSubLayer = 0;
                    tmp.DesCapType = '0';
                    tmp.IsOccupy = '1';
                    tmp.OccupyCount = 0;

                    if (specBoxManage.UpdateSpecBox(tmp) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存失败!");
                        return;
                    }
                    capLogManage.ModifyBoxLog(sb, loginPerson.Name, "M", tmp, "移除标本盒");
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("操作成功!");
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("操作失败！");
            }
        }

        /// <summary>
        /// 修改冻存架
        /// </summary>
        /// <param name="shelf"></param>
        public void ModifyShelf(ref Shelf curShelf)
        {
            ArrayList arrSubSpec = new ArrayList();
            arrSubSpec = subSpecManage.GetSubSpecInLayerOrShelf(curShelf.ShelfID.ToString(), "IJ");
            if (arrSubSpec.Count > 0 && arrSubSpec != null)
            {
                MessageBox.Show("有标本存放，不能修改！");
                return;
            }
            layerManage = new IceBoxLayerManage();
            IceBoxLayer layer = new IceBoxLayer();
            layer = layerManage.GetLayerById(curShelf.IceBoxLayer.LayerId.ToString());

            int specTypeId = layer.SpecTypeID;
            int disTypeId = layer.DiseaseType.DisTypeID;
            if (specTypeId != 9 && disTypeId != 16)
            {
                MessageBox.Show("不能修改！");
                return;
            }
            frmSpecShelf frmModifyShelf = new frmSpecShelf();
            frmModifyShelf.ShelfFromModify = curShelf;
            frmModifyShelf.Show();
        }
    }
}
