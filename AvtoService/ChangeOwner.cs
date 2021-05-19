using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvtoService
{
    public partial class ChangeOwner : Form
    {
        public static string ConnectionString = $"Server=localhost;Database={Settings.DataBaseName};" +
            $"Uid={Settings.DataBaseUsername};" +
            $"pwd={Settings.DataBasePassword};charset=koi8r";
        MainForm f1;

        private MySqlConnection Connection = new MySqlConnection(ConnectionString);
        public ChangeOwner()
        {
            InitializeComponent();
            Connection.Open();
        }

        public ChangeOwner(MainForm f1)
        {
            InitializeComponent();
            this.f1 = f1;
            Connection.Open();
        }

        private void button1_Click(object sender, EventArgs e) // редактирование данных клиента
        {
            int ownerID = int.Parse(this.f1.dataGridView1.Rows[f1.dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString());

            MySqlCommand com = new MySqlCommand("update owner set surname_owner = '" + surnameEdit.Text.ToString() + "', name_owner = '" + nameEdit.Text.ToString() + "', middlename_owner = '" + lastnameEdit.Text.ToString() + "', phonenumber_owner = '" + phoneEdit.Text.ToString() + "' where id_owner = '" + ownerID + "'", Connection);
            com.ExecuteNonQuery();

            String mark = markBox.Text;
            com = new MySqlCommand("select id_marka from marka where name_marka = '" + mark + "'", Connection);
            int idMark = Convert.ToInt32(com.ExecuteScalar());

            String model = modelBox.Text;
            com = new MySqlCommand("select id_model from model where name_model = '" + model + "'", Connection);
            int idModel = Convert.ToInt32(com.ExecuteScalar());

            com = new MySqlCommand("update car set statenumber = '" + numberEdit.Text.ToString() + "', run = '" + runEdit.Text.ToString() + "', yearOfManufacture = '" + yearEdit.Text + "', id_Model = '" + idModel.ToString() + "', id_Marka = '" + idMark + "' where id_owner = '" + ownerID + "'", Connection);
            com.ExecuteNonQuery();
            this.f1.LoadDataFromTableOwner();
            this.Close();

        }

        private void ChangeOwner_Load(object sender, EventArgs e)
        {
            nameEdit.Text = this.f1.dataGridView1.Rows[f1.dataGridView1.SelectedCells[0].RowIndex].Cells[2].Value.ToString(); 
            surnameEdit.Text = this.f1.dataGridView1.Rows[f1.dataGridView1.SelectedCells[0].RowIndex].Cells[1].Value.ToString(); 
            lastnameEdit.Text = this.f1.dataGridView1.Rows[f1.dataGridView1.SelectedCells[0].RowIndex].Cells[3].Value.ToString(); 
            phoneEdit.Text = this.f1.dataGridView1.Rows[f1.dataGridView1.SelectedCells[0].RowIndex].Cells[4].Value.ToString(); 
            runEdit.Text = this.f1.dataGridView1.Rows[f1.dataGridView1.SelectedCells[0].RowIndex].Cells[9].Value.ToString(); 
            numberEdit.Text = this.f1.dataGridView1.Rows[f1.dataGridView1.SelectedCells[0].RowIndex].Cells[7].Value.ToString(); 
            yearEdit.Text = this.f1.dataGridView1.Rows[f1.dataGridView1.SelectedCells[0].RowIndex].Cells[8].Value.ToString();
            
            //заполнение марки
            MySqlCommand obd1 = new MySqlCommand("SELECT `Name_Marka` FROM Marka", Connection);// SQL запрос для загрузки данных
            using (var reader = obd1.ExecuteReader())
            {
                while (reader.Read())
                {
                    markBox.Items.Add(reader.GetString("Name_Marka"));
                }
            }
            String mark = this.f1.dataGridView1.Rows[f1.dataGridView1.SelectedCells[0].RowIndex].Cells[5].Value.ToString();
            markBox.SelectedText = mark;


            //заполнение модели
            obd1 = new MySqlCommand("SELECT `id_Marka` FROM marka where Name_Marka = '" + mark + "'", Connection);
            int idMarka = Convert.ToInt32(obd1.ExecuteScalar());
            AddModelFirst(idMarka);
            String model = this.f1.dataGridView1.Rows[f1.dataGridView1.SelectedCells[0].RowIndex].Cells[6].Value.ToString();
            modelBox.SelectedText = model;
        }

        private void markBox_SelectedIndexChanged(object sender, EventArgs e)//Отношение макри с моделью
        {
            MySqlCommand cmd1 = new MySqlCommand("SELECT id_Marka FROM marka  where Name_marka = '" + markBox.GetItemText(markBox.SelectedItem) + "'", Connection);
            int countMarka = Convert.ToInt32(cmd1.ExecuteScalar());
            AddModel(countMarka);
        }

        private void AddModelFirst(int idMarka)
        {
            modelBox.Items.Clear();
            MySqlCommand obd1 = new MySqlCommand("SELECT `Name_Model` FROM Model where id_marka = '" + idMarka + "'", Connection);// SQL запрос для загрузки данных
            obd1 = new MySqlCommand("SELECT `Name_Model` FROM Model where id_marka = '" + idMarka + "'", Connection);// SQL запрос для загрузки данных
            using (var reader = obd1.ExecuteReader())
            {
                while (reader.Read())
                {
                    modelBox.Items.Add(reader.GetString("Name_Model"));
                }
            }

        }

        private void AddModel(int idMarka)
        {
            modelBox.Items.Clear();
            MySqlCommand obd1 = new MySqlCommand("SELECT `Name_Model` FROM Model where id_marka = '" + idMarka + "'", Connection);// SQL запрос для загрузки данных
            obd1 = new MySqlCommand("SELECT `Name_Model` FROM Model where id_marka = '" + idMarka + "'", Connection);// SQL запрос для загрузки данных
            using (var reader = obd1.ExecuteReader())
            {
                while (reader.Read())
                {
                    int k = 0;
                    modelBox.Items.Add(reader.GetString("Name_Model"));
                    if (k == 0)
                    {
                        k++;
                        modelBox.SelectedIndex = 0;
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
