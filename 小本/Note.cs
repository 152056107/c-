using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 小本
{
    class Note
    {
        private int id;

        public int Id
        {
            get { return id; }
            
        }
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private string context;

        public string Context
        {
            get { return context; }
            set { context = value; }
        }
        private DateTime time;

        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }
        public Note(string title)
        {
            this.title = title;
            this.context = "";
            this.time = DateTime.Now;
        }
        private int workspace;

        public int Workspace
        {
            get { return workspace; }
            set { workspace = value; }
        }
       public Note(int id,string name,string context,string time)
        {
            this.id = id;
            this.title = name;
            this.context = context;
            Console.WriteLine(time);
            Console.WriteLine(DateTime.Now.ToString());
            this.time = DateTime.Parse(time);
        }

    }
}
