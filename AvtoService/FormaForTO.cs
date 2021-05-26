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
    public partial class FormaForTO : Form //Форма для сотрудников ТОО
    {
        public static string ConnectionString = $"Server=localhost;Database={Settings.DataBaseName};" +
            $"Uid={Settings.DataBaseUsername};" +
            $"pwd={Settings.DataBasePassword};";
        Entrance entrance;
        int id_worker;
        string name = "", surname = "", lastname = "";

        private MySqlConnection Connection = new MySqlConnection(ConnectionString);
        public FormaForTO( int id_worker, string name, string surname, string lastname, Entrance entrance )
        {
            InitializeComponent();
            this.entrance = entrance;
            Connection.Open();
            this.name = name;
            this.surname = surname;
            this.lastname = lastname;
            label5.Text = $"{surname} {name} {lastname}";
            this.id_worker = id_worker;
            this.entrance = entrance;
            LoadRecordData();
            this.id_worker = id_worker;
            dataGridView1.DefaultCellStyle.Font = new Font("Times New Roman", 12);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 14);
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 170;
            dataGridView1.Columns[5].Width = 120;
            dataGridView1.Columns[6].Width = 120;
            dataGridView1.Columns[7].Width = 120;
            dataGridView1.Columns[8].Width = 316;
        }

        public void LoadRecordData()
        {
            MySqlCommand obd3 = new MySqlCommand("SELECT id_Record," +
                "Surname_Owner AS 'Фамилия', " +
                "Name_Owner AS 'Имя', " +
                "MiddleName_Owner AS 'Отчество', " +
                "StateNumber AS 'Гос.номер', " +
                "Date AS 'Дата', " +
                "Time_work AS 'Время', " +
                "Name_Status AS 'Статус', " +
                "Name_Services AS 'Наименование услуги' " +
                "from record " +
                "LEFT JOIN `Car` ON Car.id_Owner = record.id_Owner " +
                "LEFT JOIN `Owner` ON Owner.id_Owner = record.id_Owner " +
                "LEFT JOIN `status` ON status.id_status = record.id_status " +
                "LEFT JOIN `Services` ON Services.id_Services = record.id_Services " +
                $"where record.id_worker = {id_worker}", Connection);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(obd3);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);// Загрузка данных
            dataGridView1.DataSource = dt; // Вывод данных  
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
        }

        public void LoadCarData(int owner_id)
        {
            string q = $"SELECT Owner.id_Owner, " +
                $"Name_marka AS 'Марка', " +
                $"Name_model AS 'Модель', " +
                $"StateNumber AS 'Гос. номер', " +
                $"YearOfManufacture AS 'Год выпуска', " +
                $"Run AS 'Пробег', " +
                $"StartDate AS 'Дата начала работы' , " +
                $"EndDate AS 'Дата окончания работы', " +
                $"Name_Services AS 'Наименование услуги' " +
                $"FROM Owner " +
                $"LEFT JOIN `Car` ON Car.id_Owner = Owner.id_Owner " +
                $"LEFT JOIN `contract` ON contract.id_Owner = Owner.id_Owner " +
                $"LEFT JOIN `services` ON services.id_Services = contract.id_Services " +
                $"LEFT JOIN `Marka` ON Marka.id_marka = Car.id_marka " +
                $"LEFT JOIN `Model` ON Model.id_model = Car.id_model WHERE Owner.id_Owner = {owner_id};";
            MySqlCommand dowContr = new MySqlCommand(q, Connection);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(dowContr);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);// Загрузка данных
            dataGridView2.DataSource = dt; // Вывод данных
            dataGridView2.DefaultCellStyle.Font = new Font("Times New Roman", 12);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 14);
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[1].Width = 130;
            dataGridView2.Columns[2].Width = 130;
            dataGridView2.Columns[3].Width = 170;
            dataGridView2.Columns[4].Width = 130;//Стоимость услуги
            dataGridView2.Columns[5].Width = 148;
            dataGridView2.Columns[6].Width = 178;
            dataGridView2.Columns[7].Width = 178;
            dataGridView2.Columns[8].Width = 210;
            // MessageBox.Show("Загрузка данных из id =" + id_Owner.ToString());

        }
        private void button2_Click(object sender, EventArgs e)
        {
            entrance.Show();
            Close();
        }

        private void FormaForTO_FormClosed(object sender, FormClosedEventArgs e)
        {
            Connection.Close();
            entrance.Show();
        }

        private void FormaForTO_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            Connection.Close();
            entrance.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.RowIndex != dataGridView1.RowCount - 1)
            {
                DataGridViewRow d = dataGridView1.Rows[e.RowIndex];
                LoadCarData(int.Parse(d.Cells[0].Value.ToString()));
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            foreach (DataGridViewRow row in dataGridView1.Rows)
                if (row.Cells[7].Value != null && row.Cells[7].Value.ToString().Equals("Выполнен"))
                    row.DefaultCellStyle.BackColor = Color.FromArgb(201, 242, 201);
                else if (row.Cells[7].Value != null && row.Cells[7].Value.ToString().Equals("Активный"))
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 140, 150);
                else if (row.Cells[7].Value != null && row.Cells[7].Value.ToString().Equals("Ожидает"))
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 228, 150);
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            {

                for (int i = 0; i < dataGridView2.RowCount; i++)
                {
                    dataGridView2.Rows[i].Selected = false;
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        if (dataGridView2.Rows[i].Cells[3].Value != null)
                            if (dataGridView2.Rows[i].Cells[3].Value.ToString().Contains(textBox1.Text))
                            {
                                dataGridView2.Rows[i].Selected = true;
                                break;
                            }
                }
            }
        }
    }
}
