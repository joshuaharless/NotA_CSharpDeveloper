using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Program
    {
        public static void Main(string[] args)
        {
            CreateSQLConnection();
            Console.ReadLine();
            Console.ReadKey();
        }
        static async void LoadData()
        {
            string page = "https://www.reddit.com";
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(page))
            using (HttpContent content = response.Content)
            {
                string data = await content.ReadAsStringAsync();
                if (data != null)
                {
                    Console.WriteLine(data);
                }
                else Console.WriteLine("waiting");
            }
        }

        public static void CreateSQLConnection()
        {
            SqlConnection conn = new SqlConnection("Server=.;Database=Aware_DVR; Integrated Security = true");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT TOP 10 DB_Change_ID,DB_Change_Detail_ID FROM dbo.aeDB_Change_Log WHERE DB_Change_Detail_ID IS NOT NULL;", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("{1},{0}", reader.GetInt32(0), reader.GetInt32(1));
            }
            reader.Close();
            conn.Close();
            if(Debugger.IsAttached)
            { Console.ReadLine(); }
        }
        }
    }