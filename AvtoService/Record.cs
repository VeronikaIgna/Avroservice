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
    public partial class Record : Form
    {
        public static string ConnectionString = $"Server=localhost;Database={Settings.DataBaseName};" +
            $"Uid={Settings.DataBaseUsername};" +
            $"pwd={Settings.DataBasePassword};charset=koi8r";
        Form f3;

       private MySqlConnection Connection = new MySqlConnection(ConnectionString);
        public Record()
        {
            InitializeComponent();
            Connection.Open();
    
        }
        public Record(Form f3)
        {
            InitializeComponent();
            LoadDataFromTableRecord();
            this.f3 = f3;
            dataGridView1.DefaultCellStyle.Font = new Font("Times New Roman", 12);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 14);
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Red;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 170;
            dataGridView1.Columns[5].Width = 120;
            dataGridView1.Columns[6].Width = 120;
            dataGridView1.Columns[7].Width = 120;
            dataGridView1.Columns[8].Width = 180;
            dataGridView1.Columns[9].Width = 140;

        }
        public void LoadDataFromTableRecord()// Метод для загрузки данных с таблицы ЗАПИСЬ
        {
            MySqlCommand obd3 = new MySqlCommand("SELECT id_Record, Surname_Owner AS 'Фамилия', Name_Owner AS 'Имя', MiddleName_Owner AS 'Отчество', StateNumber AS 'Гос.номер', Date AS 'Дата', Time_work AS 'Время', Name_Status AS 'Статус', Name_Services AS 'Наименование услуги', Surname_Work AS 'Мастер' from record LEFT JOIN `Car` ON Car.id_Owner = record.id_Owner LEFT JOIN `Owner` ON Owner.id_Owner = record.id_Owner LEFT JOIN `status` ON status.id_status = record.id_status LEFT JOIN `worker` ON worker.id_Worker = record.id_Worker LEFT JOIN `Services` ON Services.id_Services = record.id_Services", Connection);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(obd3);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);// Загрузка данных
            dataGridView1.DataSource = dt; // Вывод данных  
            dataGridView1.Columns[0].Visible = false;
        }
        private void Record_FormClosed(object sender, FormClosedEventArgs e)
        {
            Connection.Close();
        }
         private void календарьЗаписейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Record form3 = new Record(this);
            form3.Show();
        }
     
        private void button1_Click(object sender, EventArgs e)//Добавление записи
        {
            AddRecord form4 = new AddRecord(this);
                form4.Show();
            }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                //Удаляет записи из таблицы БД
                int id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString());

                string command1 = "delete from car where id_Record = '" + id.ToString() + "'";
                MySqlCommand obd1 = new MySqlCommand(command1, Connection);
                obd1.ExecuteNonQuery();
                string command2 = "delete from owner where id_Record = '" + id.ToString() + "'";
                MySqlCommand obd2 = new MySqlCommand(command2, Connection);
                if (obd2.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Вы успешно удалили!");
                }
                this.dataGridView1.Rows.Remove(this.dataGridView1.CurrentRow);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChangeStatus form6 = new ChangeStatus(this);
            form6.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChangeRecord form10 = new ChangeRecord(this);
            form10.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            {

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Selected = false;
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        if (dataGridView1.Rows[i].Cells[9].Value != null)
                            if (dataGridView1.Rows[i].Cells[9].Value.ToString().Contains(textBox1.Text))
                            {
                                dataGridView1.Rows[i].Selected = true;
                                break;
                            }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            {

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Selected = false;
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        if (dataGridView1.Rows[i].Cells[5].Value != null)
                            if (dataGridView1.Rows[i].Cells[5].Value.ToString().Contains(textBox1.Text))
                            {
                                dataGridView1.Rows[i].Selected = true;
                                break;
                            }
                }
            }
        }
    }
    }


