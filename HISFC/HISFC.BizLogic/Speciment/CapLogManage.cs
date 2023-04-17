using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Speciment;

namespace FS.HISFC.BizLogic.Speciment
{
    public class CapLogManage : FS.FrameWork.Management.Database
    {
        #region ˽�з���

        #region ʵ��������Է���������
        /// <summary>
        /// ʵ��������Է���������
        /// </summary>
        /// <param name="capLog"></param>
        /// <returns></returns>
        private string[] GetParam(CapLog capLog)
        {
            string[] str = new string[]
                          {
                              capLog.OperId.ToString(),
                              capLog.CapId.ToString(),
                              capLog.BarCode,
                              capLog.OldDesCapID.ToString(),
                              capLog.OldDesCapRow.ToString(),
                              capLog.OldDesCapCol.ToString(),
                              capLog.OldDesCapHeight.ToString(),
                              capLog.OldInType.ToString(),
                              capLog.NewDesCapID.ToString(),
                              capLog.NewDesCapRow.ToString(),
                              capLog.OldDesCapCol.ToString(),
                              capLog.OldDesCapHeight.ToString(),
                              capLog.NewInType.ToString(),
                              capLog.OperName,
                              capLog.OperTime.ToString(),
                              capLog.OperDes,
                              capLog.Comment,
                              capLog.OldDisType.ToString(),
                              capLog.NewDisType.ToString(),
                              capLog.OldSpecType.ToString(),
                              capLog.NewSpecType.ToString()
                          };
            return str;
        }       

        #region ���ô�����Ϣ
        /// <summary>
        /// ���ô�����Ϣ
        /// </summary>
        /// <param name="errorCode">������뷢������</param>
        /// <param name="errorText">������Ϣ</param>
        private void SetError(string errorCode, string errorText)
        {
            this.ErrCode = errorCode;
            this.Err = errorText + "[" + this.Err + "]"; // + "��ShelfSpecManage.cs�ĵ�" + argErrorCode + "�д���";
            this.WriteErr();
        }
        #endregion
        #endregion

        #region ���±걾������־����
        /// <summary>
        /// ���±걾������־����
        /// </summary>
        /// <param name="sqlIndex">sql�������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: >= 1 ʧ�� -1 û�и��µ����� 0</returns>
        private int UpdateLog(string sqlIndex, params string[] args)
        {
            string sql = string.Empty;//Update���

            //���Where���
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + sqlIndex + "��SQL���";

                return -1;
            }

            return this.ExecNoQuery(sql, args);
        }

        /// <summary>
        /// ��ȡ�걾������־��Ϣ
        /// </summary>
        /// <returns></returns>
        private CapLog SetCapLog()
        {
            CapLog capLog = new CapLog();
            try
            {
                capLog.OperId = Convert.ToInt32(this.Reader["OPERID"].ToString());
                capLog.CapId = Convert.ToInt32(this.Reader["CAPID"].ToString());
                capLog.BarCode = this.Reader["BARCODE"].ToString();
                capLog.OldDesCapID = Convert.ToInt32(this.Reader["OLDDESCAPID"].ToString());
                capLog.OldDesCapRow = Convert.ToInt32(this.Reader["OLDDESCAPROW"].ToString());
                capLog.OldDesCapCol = Convert.ToInt32(this.Reader["OLDDESCAPCOL"].ToString());
                capLog.OldDesCapHeight = Convert.ToInt32(this.Reader["OLDDESCAPHEIGHT"].ToString());
                capLog.OldInType = Convert.ToChar(this.Reader["OLDINTYPE"].ToString());
                capLog.NewDesCapID = Convert.ToInt32(this.Reader["NEWDESCAPID"].ToString());
                capLog.NewDesCapRow = Convert.ToInt32(this.Reader["NEWDESCAPROW"].ToString());
                capLog.NewDesCapCol = Convert.ToInt32(this.Reader["NEWDESCAPCOL"].ToString());
                capLog.NewDesCapHeight = Convert.ToInt32(this.Reader["NEWDESCAPHEIGHT"].ToString());
                capLog.NewInType = Convert.ToChar(this.Reader["NEWINTYPE"].ToString());
                capLog.OperName = this.Reader["OPERNAME"].ToString();
                capLog.OperTime = Convert.ToDateTime(this.Reader["OPERDATE"].ToString());
                capLog.OperDes = this.Reader["OPERDES"].ToString();
                capLog.Comment = this.Reader["MARK"].ToString();
                capLog.OldDisType = Convert.ToInt32(this.Reader["OLDDISTYPE"].ToString());
                capLog.NewDisType = Convert.ToInt32(this.Reader["NEWDISTYPE"].ToString());
                capLog.OldSpecType = Convert.ToInt32(this.Reader["OLDSPECTYPE"].ToString());
                capLog.NewSpecType = Convert.ToInt32(this.Reader["NEWSPECTYPE"].ToString());
            }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
                this.Err = e.Message;
                return null;
            }
            return capLog;
        }
        #endregion

        #endregion

        /// <summary>
        /// ��ȡ�µ�Sequence(1���ɹ�/-1��ʧ��)
        /// </summary>
        /// <param name="sequence">��ȡ���µ�Sequence</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int GetNextSequence(ref string sequence)
        {
            //
            // ִ��SQL���
            //
            sequence = this.GetSequence("Speciment.BizLogic.CapLogManage.GetNextSequence");
            //
            // �������NULL�����ȡʧ��
            //
            if (sequence == null)
            {
                this.SetError("", "��ȡSequenceʧ��");
                return -1;
            }
            //
            // �ɹ�����
            //
            return 1;
        }

        /// <summary>
        /// ������־��Ϣ
        /// </summary>
        /// <param name="capLog"></param>
        /// <returns></returns>
        public int InsertCapLog(CapLog capLog)
        {
            return this.UpdateLog("Speciment.BizLogic.CapLogManage.Insert", GetParam(capLog));
        }

        /// <summary>
        /// �����걾��
        /// </summary>
        /// <param name="shelf"></param>
        /// <returns></returns>
        public int DisuseShelf(Shelf shelf,string operName,string operDes,string comment)
        {
            CapLog capLog = new CapLog();
            string sequence = "";
            GetNextSequence(ref sequence);
            capLog.OperId = Convert.ToInt32(sequence);
            capLog.BarCode = shelf.SpecBarCode;
            capLog.CapId = shelf.ShelfID;
            capLog.NewDesCapCol = 0;
            capLog.NewDesCapHeight = 0;
            capLog.NewDesCapID = 0;
            capLog.NewDesCapRow = 0;
            capLog.NewInType = 'E';
            capLog.OldDesCapCol = shelf.IceBoxLayer.Col;
            capLog.OldDesCapHeight = shelf.IceBoxLayer.Height;
            capLog.OldDesCapID = shelf.IceBoxLayer.LayerId;
            capLog.OldDesCapRow = shelf.IceBoxLayer.Row;
            capLog.OldInType = 'L';
            capLog.OperDes = operDes;
            capLog.OperName = operName;
            capLog.OperTime = DateTime.Now;
            capLog.Comment = comment;
            return this.InsertCapLog(capLog);
        }

        /// <summary>
        /// �����걾��
        /// </summary>
        /// <param name="box"></param>
        /// <param name="operName"></param>
        /// <returns></returns>
        public int DisuseSpecBox(SpecBox box, string operName,string operDes)
        {
            CapLog capLog = new CapLog();
            string sequence = "";
            GetNextSequence(ref sequence);
            capLog.OperId = Convert.ToInt32(sequence);
            capLog.BarCode = box.BoxBarCode;
            capLog.CapId = box.BoxId;
            capLog.NewDesCapCol = 0;
            capLog.NewDesCapHeight = 0;
            capLog.NewDesCapID = 0;
            capLog.NewDesCapRow = 0;
            capLog.NewInType = 'E';
            capLog.OldDesCapCol = box.DesCapCol;
            capLog.OldDesCapHeight = box.DesCapSubLayer;
            capLog.OldDesCapID = box.DesCapID;
            capLog.OldDesCapRow = box.DesCapRow;
            capLog.OldInType = box.DesCapType;
            capLog.OperDes = operDes;
            capLog.OperName = box.Name;
            capLog.OperTime = DateTime.Now;
            capLog.Comment = "�걾�з���";
            return InsertCapLog(capLog);
        }

        /// <summary>
        /// �걾�е��޸���־��Ϣ
        /// </summary>
        /// <param name="box"></param>
        /// <param name="operName"></param>
        /// <param name="operDes"></param>
        /// <returns></returns>
        public int ModifyBoxLog(SpecBox box, string operName, string operDes,SpecBox newBox,string comment)
        {
            CapLog capLog = new CapLog();
            string sequence = "";
            GetNextSequence(ref sequence);
            capLog.OperId = Convert.ToInt32(sequence);
            capLog.BarCode = newBox.BoxBarCode == "" ? box.BoxBarCode : newBox.BoxBarCode;
            capLog.CapId = box.BoxId;
            capLog.NewDesCapCol = newBox.DesCapCol;
            capLog.NewDesCapHeight = newBox.DesCapSubLayer;
            capLog.NewDesCapID = newBox.DesCapID;
            capLog.NewDesCapRow = newBox.DesCapRow;
            capLog.NewInType = newBox.DesCapType;
            capLog.OldDesCapCol = box.DesCapCol;
            capLog.OldDesCapHeight = box.DesCapSubLayer;
            capLog.OldDesCapID = box.DesCapID;
            capLog.OldDesCapRow = box.DesCapRow;
            capLog.OldInType = box.DesCapType;
            capLog.OperDes = operDes;
            capLog.OperName = operName;
            capLog.OperTime = DateTime.Now;
            capLog.Comment = comment;
            capLog.OldSpecType = box.SpecTypeID;
            capLog.OldDisType = box.DiseaseType.DisTypeID;
            capLog.NewSpecType = newBox.SpecTypeID;
            capLog.NewDisType = newBox.DiseaseType.DisTypeID;
            return InsertCapLog(capLog);
        }

        /// <summary>
        /// ���ӵ��޸���־
        /// </summary>
        /// <param name="shelf"></param>
        /// <param name="operName"></param>
        /// <param name="operDes"></param>
        /// <param name="newShelf"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public int ModifyShelf(Shelf shelf, string operName, string operDes,Shelf newShelf, string comment)
        {
            CapLog capLog = new CapLog();
            string sequence = "";
            GetNextSequence(ref sequence);
            capLog.OperId = Convert.ToInt32(sequence);
            capLog.BarCode = shelf.SpecBarCode == "" ? shelf.SpecBarCode : newShelf.SpecBarCode;
            capLog.CapId = shelf.ShelfID;
            capLog.NewDesCapCol = shelf.IceBoxLayer.Col;
            capLog.NewDesCapHeight = shelf.IceBoxLayer.Height;
            capLog.NewDesCapID = shelf.IceBoxLayer.LayerId;
            capLog.NewDesCapRow = shelf.IceBoxLayer.Row;            
            capLog.OldDesCapCol = shelf.IceBoxLayer.Col;
            capLog.OldDesCapHeight = shelf.IceBoxLayer.Height;
            capLog.OldDesCapID = shelf.IceBoxLayer.LayerId;
            capLog.OldDesCapRow = shelf.IceBoxLayer.Row;
            capLog.OldInType = 'L';
            capLog.OperDes = operDes;
            capLog.OperName = operName;
            capLog.OperTime = DateTime.Now;
            capLog.Comment = comment;
            capLog.OldSpecType = shelf.SpecTypeId;
            capLog.OldDisType = shelf.DisTypeId;
            capLog.NewSpecType = newShelf.SpecTypeId;
            capLog.NewDisType = newShelf.DisTypeId;
            return this.InsertCapLog(capLog);
        }

        /// <summary>
        /// ������޸���־
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="operName"></param>
        /// <param name="operDes"></param>
        /// <param name="newLayer"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public int ModifyIceBoxLayer(IceBoxLayer layer, string operName, string operDes, IceBoxLayer newLayer, string comment)
        {
            CapLog capLog = new CapLog();
            string sequence = "";
            GetNextSequence(ref sequence);
            capLog.OperId = Convert.ToInt32(sequence);
            capLog.CapId = layer.LayerId;
            capLog.NewDesCapCol = 1;
            capLog.NewDesCapHeight = newLayer.LayerNum;
            capLog.NewDesCapID = newLayer.IceBox.IceBoxId;
            capLog.NewDesCapRow = 1;
            capLog.OldDesCapCol = 1;
            capLog.OldDesCapHeight = layer.LayerNum;
            capLog.OldDesCapID = layer.IceBox.IceBoxId;
            capLog.OldDesCapRow = 1;
            capLog.OldInType = 'B';
            capLog.OperDes = operDes;
            capLog.OperName = operName;
            capLog.OperTime = DateTime.Now;
            capLog.Comment = comment;
            capLog.OldSpecType = layer.SpecTypeID;
            capLog.OldDisType = layer.DiseaseType.DisTypeID;
            capLog.NewSpecType = newLayer.SpecTypeID;
            capLog.NewDisType = newLayer.DiseaseType.DisTypeID;
            return this.InsertCapLog(capLog);
        }
    }
}
