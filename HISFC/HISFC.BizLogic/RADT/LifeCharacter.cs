using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FS.FrameWork.Management;
using FS.HISFC.Models;

namespace FS.HISFC.BizLogic.RADT
{
    /// <summary>
    /// [��������: ������������������]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-05-02]<br></br>
    /// <�޸ļ�¼/>
    /// </summary> 
    public class LifeCharacter : Database
    {
        public LifeCharacter()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ��������

        /// <summary>
        /// ����һ����¼
        /// </summary>
        /// <param name="lifeCharacter"></param>
        /// <returns></returns>
        public int InsertLifeCharacter(FS.HISFC.Models.RADT.LifeCharacter lifeCharacter)
        {
            string strSql = "";

			if(this.Sql.GetCommonSql("RADT.InPatient.LifeCharacter.Insert.1",ref strSql) == -1) 
			{
				this.Err = this.Sql.Err;
				return -1;
			}
            strSql = this.GetLifeCharacterSql(strSql, lifeCharacter);
            if (strSql == null) return -1;
            if (this.ExecNoQuery(strSql) <= 0) return -1;
            return 1;
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="measureDate"></param>
        /// <returns></returns>
        public int DeleteLifeCharacter(string inpatientNO, DateTime measureDate)
        {
            string sql = "RADT.InPatient.LifeCharacter.Delete.1";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                sql = string.Format(sql, inpatientNO, measureDate);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ����һ��
        /// </summary>
        /// <param name="lifeCharacter"></param>
        /// <returns></returns>
        public int UpdateLifeCharacter(FS.HISFC.Models.RADT.LifeCharacter lifeCharacter)
        {
            if (this.DeleteLifeCharacter(lifeCharacter.ID, lifeCharacter.MeasureDate) < 0)
            {
                return -1;
            }
            return this.InsertLifeCharacter(lifeCharacter);
        }

        #endregion

        #region ��ѯ

        /// <summary>
        /// ��ѯһ��
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="measureDate"></param>
        /// <returns></returns>
        public ArrayList QueryLifeCharacter(string inpatientNO, DateTime measureDate)
        {
            string sql = "", sqlSelect = "", sqlWhere = "RADT.InPatient.LifeCharacter.Select.Where.1";
            if (this.GetSelectSql(ref sqlSelect) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            if (this.Sql.GetCommonSql(sqlWhere, ref sqlWhere) == -1) return null;
            sql = sqlSelect + " " + sqlWhere;
            sql = string.Format(sql, inpatientNO, measureDate);
            return this.GetLifeCharacterAL(sql);
        }

        #endregion

        #region ˽�з���

        private int GetSelectSql(ref string sql)
        {
            return this.Sql.GetCommonSql("RADT.InPatient.LifeCharacter.Select.1", ref sql);
        }

        /// <summary>
        /// ��ȡ��������ArrayList
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private ArrayList GetLifeCharacterAL(string sql)
        {
            if (this.ExecQuery(sql) == -1) return null;
            ArrayList al = new ArrayList();
            while (this.Reader.Read())
            {
                FS.HISFC.Models.RADT.LifeCharacter lfch = new FS.HISFC.Models.RADT.LifeCharacter();
                lfch.ID = this.Reader[0].ToString();
                lfch.Name = this.Reader[1].ToString();
                lfch.Dept.ID = this.Reader[2].ToString();
                lfch.Dept.Name = this.Reader[3].ToString();
                lfch.NurseStation.ID = this.Reader[4].ToString();
                lfch.NurseStation.Name = this.Reader[5].ToString();
                lfch.BedNO = this.Reader[6].ToString();
                lfch.PID.PatientNO = this.Reader[7].ToString();
                lfch.InDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8]);
                lfch.MeasureDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[9]);
                lfch.Breath = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[10]);
                lfch.Pulse = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[11]);
                lfch.Temperature = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[12]);
                lfch.HighBloodPressure = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[13]);
                lfch.LowBloodPressure = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[14]);
                lfch.Time = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[15]);
                lfch.IsForceHypothermia = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[16].ToString());
                lfch.ForceHypothermiaInt = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[16]);
                lfch.TargetTemperature = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[17]);
                lfch.TemperatureType = this.Reader[18].ToString();
                lfch.Memo = this.Reader[19].ToString();
                lfch.Oper.ID = this.Reader[20].ToString();

                al.Add(lfch);
            }
            this.Reader.Close();
            return al;
        }

        /// <summary>
        /// ���SQL���������
        /// </summary>
        /// <param name="sqlLifeCharacter"></param>
        /// <param name="lifeCharacter"></param>
        /// <returns></returns>
        private string GetLifeCharacterSql(string sqlLifeCharacter, FS.HISFC.Models.RADT.LifeCharacter lifeCharacter)
        {
            try
            {
                System.Object[] s ={lifeCharacter.ID,
                                    lifeCharacter.Name,
                                    lifeCharacter.Dept.ID,
                                    lifeCharacter.Dept.Name,
                                    lifeCharacter.NurseStation.ID,
                                    lifeCharacter.NurseStation.Name,
                                    lifeCharacter.BedNO,
                                    lifeCharacter.PID.PatientNO,
                                    lifeCharacter.InDate.ToString("yyyyMMdd"),
                                    lifeCharacter.MeasureDate.ToString("yyyyMMddHHmmss"),
                                    lifeCharacter.Breath,
                                    lifeCharacter.Temperature,
                                    lifeCharacter.Pulse,
                                    lifeCharacter.HighBloodPressure,
                                    lifeCharacter.LowBloodPressure,
                                    lifeCharacter.Time,
                                    lifeCharacter.IsForceHypothermia == true ? 1 : 0,
                                    lifeCharacter.TargetTemperature,
                                    lifeCharacter.TemperatureType,
                                    lifeCharacter.Memo,
                                    lifeCharacter.Oper.ID};
                sqlLifeCharacter = string.Format(sqlLifeCharacter, s);
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����" + ex.Message;
                this.WriteErr();
                return null;
            }
            return sqlLifeCharacter;
        }

        #endregion
    }
}
