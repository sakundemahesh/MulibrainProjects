using System.IO;
using System.Threading;
using System;
using System.Configuration;

namespace Hindalco
{
    internal class DataLog
    {

        static string LogDriveName = ConfigurationManager.AppSettings["LogDrive"] ?? "C:\\";
        static int count = 0;
        public static void WriteReciptDataLog(string Data)
        {
            try
            {
                string path = @"\" + System.DateTime.Now.ToString("yyyy") + @"\" + System.DateTime.Now.ToString("MMM") + @"\" + System.DateTime.Now.ToString("dd") + @"\" + "Recipt";
                Directory.CreateDirectory(path);
                string str = System.DateTime.Now.ToString("ddyyyy");
                string fileName = path + @"\" + str + /*GlobalObject.gSalesOrderNumber */+(count + 1) + ".log";
                using (StreamWriter file2 = new StreamWriter(fileName, true))
                {
                    string str2 = DateTime.Now.ToString("HH:mm:ss") + "; " + Data + "\r\n";
                    file2.Write(str2);
                    file2.Flush();
                    file2.Close();
                    file2.Dispose();
                }
            }
            catch
            {

            }
        }



        public static void WriteDataLog(string sData)
        {
            try
            {
                string path = LogDriveName + System.DateTime.Now.ToString("yyyy") + @"\" + System.DateTime.Now.ToString("MMM") + @"\" + System.DateTime.Now.ToString("dd") + @"\" + "D_Log";
                Directory.CreateDirectory(path);
                string str = System.DateTime.Now.ToString("dd-MMM-yyyy");
                string fileName = path + @"\" + str + "SafetyTrainingKiosk.log";

                using (StreamWriter file2 = new StreamWriter(fileName, true))
                {
                    string str2 = Convert.ToString(DateTime.Now) + "; " + sData + "\r\n";
                    file2.Write(str2);
                    file2.Flush();
                    file2.Close();
                    file2.Dispose();
                }
            }
            catch
            {

            }
        }

        public static void WriteAdminLog(string sData)
        {
            try
            {
                string path = "C:" + @"\" + "Admin" + System.DateTime.Now.ToString("yyyy") + @"\" + System.DateTime.Now.ToString("MMM") + @"\" + System.DateTime.Now.ToString("dd") + @"\" + "Admin_Log";
                Directory.CreateDirectory(path);
                string str = System.DateTime.Now.ToString("dd-MMM-yyyy");
                string fileName = path + @"\" + str + "AdminLog.log";

                using (StreamWriter file2 = new StreamWriter(fileName, true))
                {
                    string str2 = Convert.ToString(DateTime.Now) + "; " + sData + "\r\n";
                    file2.Write(str2);
                    file2.Flush();
                    file2.Close();
                    file2.Dispose();
                }
            }
            catch
            {

            }
        }

        public static void WriteErrorLog(string sData)
        {
            try
            {
                string path = LogDriveName + System.DateTime.Now.ToString("yyyy") + @"\" + System.DateTime.Now.ToString("MMM") + @"\" + System.DateTime.Now.ToString("dd") + @"\" + "E_Log";
                Directory.CreateDirectory(path);
                string str = System.DateTime.Now.ToString("dd-MMM-yyyy");
                string fileName = path + @"\" + str + "HondaTrainingKiosk.log";
                using (StreamWriter file2 = new StreamWriter(fileName, true))
                {
                    string str2 = Convert.ToString(DateTime.Now) + "; " + sData + "\r\n\r\n";
                    file2.Write(str2);
                    file2.Flush();
                    file2.Close();
                    file2.Dispose();
                }
            }
            catch
            {

            }
        }

        public static string WriteReceipt(string sData)
        {
            string fileName = string.Empty;
            try
            {
                string path = LogDriveName + System.DateTime.Now.ToString("yyyy") + @"\" + System.DateTime.Now.ToString("MMM") + @"\" + System.DateTime.Now.ToString("dd") + @"\" + "Receipt";
                Directory.CreateDirectory(path);
                string str = System.DateTime.Now.ToString("ddMMMyyyyhhmmss");
                fileName = path + @"\" + str + "Rpt.log";

                using (StreamWriter file2 = new StreamWriter(fileName, true))
                {
                    string str2 = Convert.ToString(DateTime.Now) + "; " + sData + "\r\n";
                    file2.Write(str2);
                    file2.Flush();
                    file2.Close();
                    file2.Dispose();
                }
                return fileName;
            }
            catch
            {
                return fileName;
            }
        }

        public static void CreateFile(string data)
        {
            try
            {
                try
                {
                    if (File.Exists(@"D:\Request\BReq.txt"))
                    {
                        File.Delete(@"D:\Request\BReq.txt");
                        Thread.Sleep(500);
                    }

                    if (File.Exists(@"D:\Response\BResp.txt"))
                    {
                        File.Delete(@"D:\Response\BResp.txt");
                        Thread.Sleep(500);
                    }
                }
                catch (Exception ex)
                {
                }
                using (StreamWriter file2 = new StreamWriter(@"D:\Request\BReq.txt", true))
                {
                    string str2 = data + "\r\n";
                    file2.Write(str2);
                    file2.Flush();
                    file2.Close();
                    file2.Dispose();
                }
            }
            catch (Exception ex)
            {

                WriteErrorLog("InsertCard : CreateFile " + ex.Message);

            }
        }

        public static void CreateFileW(string data, string Amount, string txnId)
        {
            try
            {
                try
                {
                    if (File.Exists(@"D:\Request\WReq.txt"))
                    {
                        File.Delete(@"D:\Request\WReq.txt");
                        Thread.Sleep(500);
                    }

                    if (File.Exists(@"D:\Response\WResp.txt"))
                    {
                        File.Delete(@"D:\Response\WResp.txt");
                        Thread.Sleep(500);
                    }
                }
                catch (Exception ex)
                {
                }
                using (StreamWriter file2 = new StreamWriter(@"D:\Request\WReq.txt", true))
                {
                    string str2 = data + "\r\n";
                    string str3 = Amount + "\r\n";
                    string str4 = txnId + "\r\n";

                    file2.Write(str2);
                    file2.Write(str3);
                    file2.Write(str4);

                    file2.Flush();
                    file2.Close();
                    file2.Dispose();
                }
            }
            catch (Exception ex)
            {
                WriteErrorLog("InsertCard : CreateFile " + ex.Message);
            }
        }

        internal static void WriteDataLog(object p)
        {
            throw new NotImplementedException();
        }

        //internal class WriteErrorLog
        //{
        //}
    
}
}