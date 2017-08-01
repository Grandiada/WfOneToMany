using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
namespace WfOneToMany
{
    public partial class Form2 : Form
    {
        DbConnected db;
        public Form2()
        {
            InitializeComponent();
            db = new DbConnected();
            db.Teams.Load();
            dataGridView1.DataSource = db.Teams.Local.ToBindingList();
        }


        //add
        private void button1_Click(object sender, EventArgs e)
        {
            TeamForm tf = new TeamForm();
            DialogResult result = tf.ShowDialog(this);


            if (tf.DialogResult == DialogResult.Cancel)
                return;
            Team team = new Team();

            team.Name = tf.textBox1.Text;
            team.Coach = tf.textBox2.Text;
            db.Teams.Add(team);
            db.SaveChanges();
            MessageBox.Show("Team addded");
       }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {

                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Team team= db.Teams.Find(id);

                TeamForm tf = new TeamForm();
                tf.textBox1.Text = team.Name;
                tf.textBox2.Text = team.Coach;

                DialogResult result = tf.ShowDialog(this);


                if (tf.DialogResult == DialogResult.Cancel)
                    return;

                team.Name = tf.textBox1.Text;
                team.Coach = tf.textBox2.Text;

                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                MessageBox.Show("Объект обновлен");

            }


        }
        //delete
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {

                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Team team = db.Teams.Find(id);
                team.Players.Clear();
                db.Teams.Remove(team);
                db.SaveChanges();

                MessageBox.Show("Объект удален");



            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                Team team = db.Teams.Find(id);
                listBox1.DataSource = team.Players.ToList();
                listBox1.DisplayMember = "Name";
            }
        }
    }
}
