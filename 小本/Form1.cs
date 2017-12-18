using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql;
using MySql.Data.MySqlClient;

namespace 小本
{
    public partial class Form1 : Form
    {
        private WorkspaceDao workspaceDao = new WorkspaceDao();
        private NoteDao noteDao = new NoteDao();
        private Note current_note;
        private bool saveOrUpdate = true;
        public Form1()
        {
           
            InitializeComponent();
            this.splitContainer1.Panel2.Hide();
            showCategory();
            
        }
        private void title_TextChanged(object sender, EventArgs e)
        {
            this.保存当前ToolStripMenuItem.Enabled = true;
            this.保存并关闭ToolStripMenuItem.Enabled = true;
        }
        private void context_TextChanged(object sender, EventArgs e)
        {
            this.保存当前ToolStripMenuItem.Enabled = true;
            this.保存并关闭ToolStripMenuItem.Enabled = true;
        }
        private void 新建工作区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateWorkspaceForm form = new CreateWorkspaceForm();
            form.Show();
            form.FormClosed += form_FormClosed;
           
        }

        private void form_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            this.showCategory();
        }
        private void showCategory()
        {
            this.treeView1.Nodes.Clear();
            MySqlDataReader reader = workspaceDao.FindAll();
            List<Workspace> spaceList = new List<Workspace>();
            while (reader.Read())
            {
                Console.WriteLine(reader[0]+"  "+reader[1]+"  "+reader[2]+"  "+reader[3]);
                spaceList.Add(new Workspace(reader.GetInt32("id"),reader.GetString("name"),reader.GetString("time"),reader.GetInt32("note_count")));
            }
            reader.Close();
            
            foreach(Workspace space in spaceList){
                TreeNode rootNode = new TreeNode(space.Name);
                rootNode.Name = space.Id+"";
                this.treeView1.Nodes.Add(rootNode);
                MySqlDataReader noteReader = noteDao.FindAll(space.Id);
                while (noteReader.Read())
                {
                    TreeNode childNode = new TreeNode(noteReader[1] + "");
                    childNode.Name = noteReader[0] + "";
                    rootNode.Nodes.Add(childNode);
                }
                noteReader.Close();
            }
            
        }


      

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

       

        private void 新建笔记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Note note = new Note("新建笔记本");
            note.Workspace = int.Parse(this.treeView1.SelectedNode.Name);
            //noteDao.Save(note);
            this.treeView1.SelectedNode.Nodes.Add(new TreeNode(note.Title));
            this.splitContainer1.Panel2.Show();
            this.current_note = note;
            this.saveButton.Enabled = true;
            this.放弃编辑ToolStripMenuItem.Enabled = true;
            this.title.Text = note.Title;
            this.context .Text= note.Context;
            Console.WriteLine(current_note.Time.ToString());
            saveOrUpdate = true;


        }
       
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //current_note.Title = this.title.Text;
            //current_note.Context = this.context.Text;
            //noteDao.Save(current_note);
        }

        private void 保存当前ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            current_note.Title = this.title.Text;
            current_note.Context = this.context.Text;
            Console.WriteLine(this.title.Text+"   "+this.context.Text+"    "+current_note.Time.ToString());
            if (this.saveOrUpdate)
            {
                noteDao.Save(current_note);
                
            }
            else
            {
                noteDao.UpdateByTime(current_note);
            }

            
            
            this.保存当前ToolStripMenuItem.Enabled = false;
            this.saveOrUpdate = false;
        }

        private void 保存并关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            current_note.Title = this.title.Text;
            current_note.Context = this.context.Text;
            Console.WriteLine(this.title.Text + "   " + this.context.Text + "    " + current_note.Time.ToString());
            if (this.saveOrUpdate)
            {
                noteDao.Save(current_note);

            }
            else
            {
                noteDao.UpdateByTime(current_note);
            }



            this.保存当前ToolStripMenuItem.Enabled = false;
            this.saveOrUpdate = false;
            this.splitContainer1.Panel2.Hide();
            current_note = null;
            this.showCategory();
            this.保存并关闭ToolStripMenuItem.Enabled = false;
            this.saveButton.Enabled = false;
        }

        private void 放弃编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定放弃编辑？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {

            }
            else
            {
                this.splitContainer1.Panel2.Hide();
                current_note = null;
                this.保存并关闭ToolStripMenuItem.Enabled = false;
                this.saveButton.Enabled = false;
                this.放弃编辑ToolStripMenuItem.Enabled = false;
                this.showCategory();
            }
        }
        void treeView1_DoubleClick(object sender, System.EventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Parent != null)
            {

                current_note = noteDao.FindById(treeView1.SelectedNode.Name);
                Console.WriteLine(treeView1.SelectedNode.Name);
                this.saveButton.Enabled = true;
                this.放弃编辑ToolStripMenuItem.Enabled = true;
                this.splitContainer1.Panel2.Show();
                this.title.Text = current_note.Title;
                this.context.Text = current_note.Context;
                saveOrUpdate = false;

            }

        }
        private void treeView1_MouseDown(object sender, MouseEventArgs  e)
        {
            if (e.Button == MouseButtons.Right && treeView1.SelectedNode.Parent != null)
            {
                Point ClickPoint = new Point(e.X, e.Y);
                TreeNode CurrentNode = treeView1.GetNodeAt(ClickPoint);
                CurrentNode.ContextMenuStrip = this.contextMenuStrip1;
                
                

            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            current_note = noteDao.FindById(treeView1.SelectedNode.Name);
            Console.WriteLine(treeView1.SelectedNode.Name);
            this.saveButton.Enabled = true;
            this.放弃编辑ToolStripMenuItem.Enabled = true;
            this.splitContainer1.Panel2.Show();
            this.title.Text = current_note.Title;
            this.context.Text = current_note.Context;
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectNode = treeView1.SelectedNode;
            
            noteDao.Delete(int.Parse(selectNode.Name));
            this.showCategory();
        }

        private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

     
    }
}
