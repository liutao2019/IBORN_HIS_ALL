using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace API.GZSI.Class
{
    public class ZipFloClass
    {
        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="TargetFile">需要解压的文件</param>
        /// <param name="fileDir">解压路径</param>
        /// <param name="fileNames">文件名称</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static int unZipFile(byte[] TargetFile, string fileDir, ref string fileNames,ref string msg)
        {
            string rootFile = "";
            msg = "";
            try
            {
                MemoryStream originalStream = new MemoryStream(TargetFile);
                ZipInputStream inputstream = new ZipInputStream(originalStream);
                ZipEntry entry;
                string path = fileDir;
                string entryDir = ""; //解压出来的文件保存路径

                //根目录下的下一个子文件夹的名称
                while ((entry = inputstream.GetNextEntry()) != null)
                {
                    entryDir = Path.GetDirectoryName(entry.Name);
                    //得到根目录下的第一级子文件夹的名称
                    if (entryDir.IndexOf("\\") >= 0)
                    {
                        entryDir = entryDir.Substring(0, entryDir.IndexOf("\\") + 1);
                    }
                    string dir = Path.GetDirectoryName(entry.Name);
                    string fileName = Path.GetFileName(entry.Name); //得到根目录下的第一级子文件夹下的子文件夹名称
                    //根目录下的文件名称
                    if (dir != "")
                    {
                        //创建根目录下的子文件夹，不限制级别
                        if (!Directory.Exists(fileDir + "\\" + dir))
                        {
                            path = fileDir + "\\" + dir;
                            //在指定的路径创建文件夹
                            Directory.CreateDirectory(path);
                        }
                    }
                    else if (dir == "" && fileName != "")
                    {
                        //根目录下的文件
                        path = fileDir;
                        rootFile = fileName;
                    }
                    else if (dir != "" && fileName != "")
                    {
                        //根目录下的第一级子文件夹下的文件
                        if (dir.IndexOf("\\") > 0)
                        {
                            //指定文件保存路径
                            path = fileDir + "\\" + dir;
                        }
                    }
                    if (dir == entryDir)
                    {
                        //判断是不是需要保存在根目录下的文件
                        path = fileDir + "\\" + entryDir;
                    }

                    //以下为解压zip文件的基本步骤
                    //基本思路：遍历压缩文件里的所有文件，创建一个相同的文件
                    if (fileName != String.Empty)
                    {
                        FileStream fs = File.Create(path + "\\" + fileName);
                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = inputstream.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                fs.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                        fs.Close();
                        fileNames += fileName + ",";
                    }
                }
                inputstream.Close();
                msg = "解压成功！";
                return 1;
            }
            catch (Exception ex)
            {
                msg = "解压失败，原因：" + ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 功能：压缩文件（暂时只压缩文件夹下一级目录中的文件，文件夹及其子级被忽略）
        /// </summary>
        /// <param name="dirPath"> 被压缩的文件夹夹路径 </param>
        /// <param name="zipFilePath"> 生成压缩文件的路径，为空则默认与被压缩文件夹同一级目录，名称为：文件夹名 +.zip</param>
        /// <param name="err"> 出错信息</param>
        /// <returns> 是否压缩成功 </returns>
        public static bool ZipFile(string dirPath, string zipFilePath, out string err)
        {
            err = "";
            if (dirPath == string.Empty)
            {
                err = "要压缩的文件夹不能为空！ ";
                return false;
            }
            if (!Directory.Exists(dirPath))
            {
                err = "要压缩的文件夹不存在！ ";
                return false;
            }
            //压缩文件名为空时使用文件夹名＋ zip
            if (zipFilePath == string.Empty)
            {
                if (dirPath.EndsWith("\\"))
                {
                    dirPath = dirPath.Substring(0, dirPath.Length - 1);
                }
                zipFilePath = dirPath + ".zip";
            }

            try
            {
                string[] filenames = Directory.GetFiles(dirPath);
                using (ICSharpCode.SharpZipLib.Zip.ZipOutputStream s = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(File.Create(zipFilePath)))
                {
                    s.SetLevel(9);
                    byte[] buffer = new byte[4096];
                    foreach (string file in filenames)
                    {
                        ICSharpCode.SharpZipLib.Zip.ZipEntry entry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(Path.GetFileName(file));
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);
                        using (FileStream fs = File.OpenRead(file))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                    s.Finish();
                    s.Close();
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
            return true;
        }

    }
}
