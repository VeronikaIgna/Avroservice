using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AvtoService
{
    public partial class Worker : Form
    {
        public static string ConnectionString = $"Server=localhost;Database={Settings.DataBaseName};" +
            $"Uid={Settings.DataBaseUsername};" +
            $"pwd={Settings.DataBasePassword};";
        Form f7;

        private MySqlConnection Connection = new MySqlConnection(ConnectionString);
        public Worker()
        {
            InitializeComponent();
            Connection.Open();
        }
        public Worker(Form f7)
        {
            InitializeComponent();
            LoadDataFromTableWorker();
            this.f7 = f7;
            dataGridView1.DefaultCellStyle.Font = new Font("Times New Roman", 12);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 14);
            dataGridView1.Columns[1].Width = 180;
            dataGridView1.Columns[2].Width = 180;
            dataGridView1.Columns[3].Width = 180;
            dataGridView1.Columns[4].Width = 180;
            dataGridView1.Columns[5].Width = 223;
            dataGridView1.Columns[6].Width = 180;
            Connection.Open();
        }
        public void LoadDataFromTableWorker()// Метод для загрузки данных с таблицы СОТРУДНИКИ
        {
            MySqlCommand obd3 = new MySqlCommand("SELECT worker.id_Worker,Surname_Work AS 'Фамилия', Name_Work AS 'Имя' , MiddleName_Work AS 'Отчество', PhoneNumber_Work AS 'Номер телефона', Name_position AS 'Должность', Salary AS 'Заработная плата' FROM worker LEFT JOIN `position` ON position.id_position = worker.id_position", Connection);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(obd3);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);// Загрузка данных
            dataGridView1.DataSource = dt; // Вывод данных  
            dataGridView1.Columns[0].Visible = false;
        }
        private void Worker_FormClosed(object sender, FormClosedEventArgs e)
        {
            Connection.Close();
        }
        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Worker form7 = new Worker(this);
            form7.Show();

        }

        private void button1_Click(object sender, EventArgs e)//Добавление Сотрудника
        {
            AddWorker form8 = new AddWorker(this);
            form8.Show();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            {

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Selected = false;
                    if (dataGridView1.Rows[i].Cells[1].Value != null)
                        if (dataGridView1.Rows[i].Cells[1].Value.ToString().Contains(textBox1.Text))
                            dataGridView1.Rows[i].Selected = true;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index != -1)
            {
                int id_Worker = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);

                string login = "";
                string password = "";

                string query = $"select login, password from worker where id_worker = '{id_Worker}'";
                MySqlCommand cmd = new MySqlCommand(query, Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    login = reader.GetString("login");
                    password = reader.GetString("password");
                    
                }
                reader.Close();

                string surname = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
                string name = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();
                string lastname = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString();
                string phoneNumber = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value.ToString();
                string position = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value.ToString();
                new ChangeWorker(this, id_Worker, surname, name, lastname, phoneNumber, position, login, password).Show();
            }
        }

        private void Worker_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString());

            string command1 = "delete from worker where id_worker = '" + id.ToString() + "'";
            MySqlCommand obd1 = new MySqlCommand(command1, Connection);
            obd1.ExecuteNonQuery();
            MessageBox.Show("Вы успешно удалили!");
            this.dataGridView1.Rows.Remove(this.dataGridView1.CurrentRow);
        }
    }
}
