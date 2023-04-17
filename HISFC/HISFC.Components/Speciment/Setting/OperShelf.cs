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
        /// �Ƴ������
        /// </summary>
        /// <param name="curShelf">Ҫ�Ƴ��Ķ����</param>
        public void RemoveShelf(ref Shelf curShelf)
        {
            ArrayList arrSubSpec = new ArrayList();
            arrSubSpec = subSpecManage.GetSubSpecInLayerOrShelf(curShelf.ShelfID.ToString(), "IJ");
            if (arrSubSpec.Count > 0 && arrSubSpec != null)
            {
                MessageBox.Show("�б걾��ţ������Ƴ���");
                return;
            }
            DialogResult dialog = MessageBox.Show("�˲������Ƴ�����������б걾�е���Ϣ��������", "�Ƴ�", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.No)
                return;
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                capLogManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                shelfManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                layerManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                specBoxManage.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                
                //д����־
                if (capLogManage.DisuseShelf(curShelf, loginPerson.Name, "D", "�Ƴ������") == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ʧ�ܣ�");
                    return;
                }
               
                IceBoxLayer tmpLayer = layerManage.GetLayerById(curShelf.IceBoxLayer.LayerId.ToString());
                if (tmpLayer == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ʧ�ܣ�");
                    return;
                }
                int occupyCount = tmpLayer.OccupyCount - 1;
                if (occupyCount <= 0)
                    occupyCount = 0;
                if (layerManage.UpdateOccupy("0", tmpLayer.LayerId.ToString()) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ʧ�ܣ�");
                    return;
                }
                if (layerManage.UpdateOccupyCount(occupyCount.ToString(), tmpLayer.LayerId.ToString()) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ʧ�ܣ�");
                    return;
                }

                curShelf.Comment = "�Ƴ�";
                curShelf.IceBoxLayer.Row = 0;
                curShelf.IceBoxLayer.Col = 0;
                curShelf.IceBoxLayer.LayerId = 0;
                curShelf.IceBoxLayer.Height = 0;
                curShelf.IsOccupy = '0';
                curShelf.OccupyCount = 0;
                //���¼���
                if (shelfManage.UpdateShelf(curShelf) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    System.Windows.Forms.MessageBox.Show("����ʧ�ܣ�");
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
                        MessageBox.Show("����ʧ��!");
                        return;
                    }
                    capLogManage.ModifyBoxLog(sb, loginPerson.Name, "M", tmp, "�Ƴ��걾��");
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("�����ɹ�!");
            }
            catch
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("����ʧ�ܣ�");
            }
        }

        /// <summary>
        /// �޸Ķ����
        /// </summary>
        /// <param name="shelf"></param>
        public void ModifyShelf(ref Shelf curShelf)
        {
            ArrayList arrSubSpec = new ArrayList();
            arrSubSpec = subSpecManage.GetSubSpecInLayerOrShelf(curShelf.ShelfID.ToString(), "IJ");
            if (arrSubSpec.Count > 0 && arrSubSpec != null)
            {
                MessageBox.Show("�б걾��ţ������޸ģ�");
                return;
            }
            layerManage = new IceBoxLayerManage();
            IceBoxLayer layer = new IceBoxLayer();
            layer = layerManage.GetLayerById(curShelf.IceBoxLayer.LayerId.ToString());

            int specTypeId = layer.SpecTypeID;
            int disTypeId = layer.DiseaseType.DisTypeID;
            if (specTypeId != 9 && disTypeId != 16)
            {
                MessageBox.Show("�����޸ģ�");
                return;
            }
            frmSpecShelf frmModifyShelf = new frmSpecShelf();
            frmModifyShelf.ShelfFromModify = curShelf;
            frmModifyShelf.Show();
        }
    }
}
