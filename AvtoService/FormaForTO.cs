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
    public partial class FormaForTO : Form
    {
        public static string ConnectionString = $"Server=localhost;Database={Settings.DataBaseName};" +
            $"Uid={Settings.DataBaseUsername};" +
            $"pwd={Settings.DataBasePassword};";
        Entrance entrance;
        int id_worker;

        private MySqlConnection Connection = new MySqlConnection(ConnectionString);
        public FormaForTO(Entrance entrance, int id_worker)
        {
            InitializeComponent();
            this.entrance = entrance;
            Connection.Open();
            LoadRecordData();
            this.id_worker = id_worker;
        }

        public void LoadRecordData()
        {
            MySqlCommand obd3 = new MySqlCommand("SELECT id_Record, " +
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
                $"LEFT JOIN `Worker` on worker.id_worker = {id_worker} ", Connection);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(obd3);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);// Загрузка данных
            dataGridView1.DataSource = dt; // Вывод данных  
            dataGridView1.Columns[0].Visible = false;
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
