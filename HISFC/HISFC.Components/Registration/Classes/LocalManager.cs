using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.Components.Registration.Classes
{
    /// <summary>
    /// 本地业务处理
    /// </summary>
    public class LocalManager : FS.HISFC.BizLogic.Manager.DataBase
    {
        //{0FBEA522-F50E-4fd2-9108-9A8FA8712890} 添加B超排班 类型为2
        public ArrayList GetAllRoom(FS.HISFC.Models.Base.EnumSchemaType schemaType)
        {
            string sql = null;
            if (schemaType == FS.HISFC.Models.Base.EnumSchemaType.BDoct)
            {
                sql = @"select f.room_id,f.room_name from met_nuo_room f
                            where f.valid_flag='1' and f.dept_code='6003'
                            order by lpad(replace(f.room_name,'诊室',''),6,'0')";
            }
            else
            {
                sql = @"select f.room_id,f.room_name from met_nuo_room f
                            where f.valid_flag='1' and f.dept_code!='6003'
                            order by lpad(replace(f.room_name,'诊室',''),6,'0')";
            }
            

            ArrayList alRoom = new ArrayList();
            this.ExecQuery(sql);

            try
            {
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject roomObj = new FS.FrameWork.Models.NeuObject();
                    roomObj.ID = Reader[0].ToString();
                    roomObj.Name = Reader[1].ToString();

                    alRoom.Add(roomObj);
                }

                return alRoom;
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return null;
            }
            finally
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
            }
        }

        public ArrayList GetConsole(string roomID)
        {
            string sql = @"select f.console_code,
                                   f.console_name
                        from met_nuo_console f 
                        where f.valid_flag = '1'
                        and f.room_code='{0}'";

            ArrayList alRoom = new ArrayList();

            try
            {
                sql = string.Format(sql, roomID);

                this.ExecQuery(sql);
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject roomObj = new FS.FrameWork.Models.NeuObject();
                    roomObj.ID = Reader[0].ToString();
                    roomObj.Name = Reader[1].ToString();

                    alRoom.Add(roomObj);
                }

                return alRoom;
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return null;
            }
            finally
            {
                if (!Reader.IsClosed)
                {
                    Reader.Close();
                }
            }
        }
    }
}
