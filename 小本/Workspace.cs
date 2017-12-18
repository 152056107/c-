using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 小本
{
    class Workspace
    {
        private int id;

        public int Id
        {
            get { return id; }
           
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private DateTime time;

        public DateTime Time
        {
            get { return time; }
            
        }
        private int noteNum;

        public int NoteNum
        {
            get { return noteNum; }
           
        }
        public Workspace(string name) {
            this.name = name;
            this.time = DateTime.Now;
            this.noteNum = 0;
        }

        public Workspace(int id,string name,string time,int num) {
            this.id = id;
            this.name = name;
            this.time = DateTime.Parse(time);
            this.noteNum = num;
        }
    }
}
