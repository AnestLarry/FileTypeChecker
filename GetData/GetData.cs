using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace getdata
{
    public class GetData
    {
        public void Input(byte[] data)
        {
            if (data.Length > 0)
            {
                sqlite3handle sql = new sqlite3handle();
                List<select_result> result = sql.Select();
                
                foreach (select_result i in result)
                {
                    bool isright = true;
                    for (int j = 0; j < i.sign.Length; j++)
                    {
                        if (data[j] != i.sign[j])
                        {
                            isright = false;
                            break;
                        }

                    }
                    
                    if (isright)
                    {
                        Console.WriteLine(string.Format("File Ext:{0}\nDes:\n{1}\n\n", i.ext, i.des));
                        isright = false;
                    }

                }
            }
            else
            {
                Console.WriteLine("nothings is give me !");
            }
        }

    }
    public class sqlite3handle
    {
        private SQLiteConnection conn;
        public sqlite3handle()
        {

            conn = new SQLiteConnection("data source="+ AppDomain.CurrentDomain.BaseDirectory + "file.db");
            conn.Open();

        }
        public List<select_result> Select()
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Select * from filetype;";
            SQLiteDataReader sr = cmd.ExecuteReader();
            List<select_result> result = new List<select_result> { };
            while (sr.Read())
            {
                //result.Add(new select_result{ext = sr.GetString(0),des = sr.GetString(1),sign = (byte[])(sr.GetValue(2))});
                result.Add(new select_result { ext = (string)(sr.GetValue(0)), des = (string)(sr.GetValue(1)), sign = (byte[])(sr.GetValue(2)) });
            }
            return result;
        }
        public static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }


    }
    public struct select_result
    {
        public string ext;
        public string des;
        public byte[] sign;
    }
}