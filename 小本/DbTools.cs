using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace 小本
{
    class DbTools
    {
        public static MySqlConnection GetConnection() {
            string sql = "Server=localhost;Database=notebook;Uid=root;Pwd=123456;pooling=false;CharSet=utf8;port=3306";
            MySqlConnection conn = new MySqlConnection(sql);
            conn.Open();
            return conn;
        }
        //protected MySqlDataReader Query(string sql)
        //{
        //    MySqlConnection conn=GetConnection();
        //    conn.Open();
        //    MySqlCommand cmd = new MySqlCommand(sql, conn);
        //    return cmd.ExecuteReader();
        //}
        //protected void Execute(string sql) {
        //    MySqlConnection conn = GetConnection();
        //    conn.Open();
        //    MySqlCommand cmd = new MySqlCommand(sql, conn);
        //    cmd.ExecuteNonQuery();
        //}
    }
}
