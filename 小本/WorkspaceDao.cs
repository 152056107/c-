using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace 小本
{
    class WorkspaceDao
    {
        private MySqlConnection conn = DbTools.GetConnection();
        public void Save(Workspace workspace)
        {
            Console.WriteLine(workspace.Name + "  " + workspace.Time.ToString());
            string sql = "INSERT INTO `workspace` (`name`, `time`, `note_count`) VALUES ('" + workspace.Name + "', '" + workspace.Time + "', '" + workspace.NoteNum + "');";
            Console.WriteLine(sql);
            new MySqlCommand(sql, conn).ExecuteNonQuery();

        }
        public void Delete(int id)
        {
            string sql = "delete from `workspace` where `id`='" + id + "';";
            new MySqlCommand(sql, conn).ExecuteNonQuery();
        }
        public void Rename(int id, string name)
        {
            string sql = "update `workspace` set `name`='" + name + "' where `id`='" + id + "';";
            new MySqlCommand(sql, conn).ExecuteNonQuery();

        }
        public MySqlDataReader FindAll()
        {
            string sql = "select * from workspace;";
            MySqlDataReader reader = new MySqlCommand(sql, conn).ExecuteReader();
            return reader;
        }
    }
}
