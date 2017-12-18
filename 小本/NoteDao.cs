using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace 小本
{
    class NoteDao
    {
        private MySqlConnection conn = DbTools.GetConnection();
        public void Save(Note note)
        {
            string sql = "insert into note(`title`,`context`,`time`,`workspace`) values('" + note.Title + "','" + note.Context + "','" + note.Time.ToString() + "','" + note.Workspace + "');";
            new MySqlCommand(sql, conn).ExecuteNonQuery();
        }
        public void Delete(int id)
        {
            string sql = "delete from `note` where `id`=" + id + ";";
            new MySqlCommand(sql, conn).ExecuteNonQuery();
        }
        public void Update(Note note)
        {

            string sql = "update note set `title`='" + note.Title + "',context='" + note.Context + "' where `id`='" + note.Id + "';";
            new MySqlCommand(sql, conn).ExecuteNonQuery();
        }
        public MySqlDataReader FindAll(int workspaceId)
        {
            string sql = "select * from note where workspace=" + workspaceId + ";";
            MySqlDataReader reader = new MySqlCommand(sql, conn).ExecuteReader();

            return reader;
        }
        public Note FindByTime(string time)
        {
            Note note = null;
            try
            {
                string sql = "select * from `note` where `time`='" + time + "';";
                MySqlDataReader reader = new MySqlCommand(sql, conn).ExecuteReader();
                int id = reader.GetInt32("id");
                string title = reader.GetString("title");
                string context = reader.GetString("context");
                string time1 = reader.GetString("time");
                note = new Note(id, title, context, time);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return note;

        }
        public Note FindById(string Id)
        {
            Note note = null;
            try
            {
                string sql = "select * from `note` where `id`=" + Id + ";";
                MySqlDataReader reader = new MySqlCommand(sql, conn).ExecuteReader();

                reader.Read();
                // int id = reader.GetInt32("id");
                int id = int.Parse(reader[0].ToString());
                Console.WriteLine(int.Parse(reader[0].ToString()));
                string title = reader.GetString("title");
                Console.WriteLine(title);
                string context = reader.GetString("context");
                Console.WriteLine(context);
                string time1 = reader[4].ToString();
                note = new Note(id, title, context, time1);
                reader.Close();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return note;
        }
        public void UpdateByTime(Note note)
        {

            string sql = "update `note` set `title`='" + note.Title + "',`context`='" + note.Context + "' where `time`='" + note.Time.ToString() + "';";
            Console.WriteLine(note.Title + "   " + note.Context);
            new MySqlCommand(sql, conn).ExecuteNonQuery();
        }

    }
}
