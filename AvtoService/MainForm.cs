﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AvtoService
{

    public partial class MainForm : Form
    {
        public static string ConnectionString = "Server=localhost;Database=avtoservice;Uid=root;pwd=MemoriesInHeart2020;";
        public static int countOwners = 0;

        Entrance sf;
        public int idManager;

        private MySqlConnection Connection = new MySqlConnection(ConnectionString);
        public MainForm(int idManager, Entrance sf)
        {
            InitializeComponent();
            this.idManager = idManager;
            this.sf = sf;
            LoadDataFromTableOwner();
            Connection.Open();
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
            dataGridView1.Columns[8].Width = 140;
            dataGridView1.Columns[9].Width = 140;
            dataGridView2.DefaultCellStyle.Font = new Font("Times New Roman", 12);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 14);
           // this.dataGridView1.Columns[3].HeaderCell.Style.BackColor = Color.Red;

        }
        public void LoadDataFromTableContract(int id_Owner)// Метод для загрузки данных с таблицы владельцев
        {
            //if (id_Owner!= -1)
            // MySqlCommand dowContr = new MySqlCommand("SELECT * from Contract where id_Owner = '" + id_Owner.ToString() + "'", Connection);
            MySqlCommand dowContr = new MySqlCommand("SELECT id_Contract, StartDate AS 'Дата начала работы', EndDate AS 'Дата окончания работы', Name_Services AS 'Наименование услуги', Price_Services AS 'Стоимость услуги', Surname_Work AS 'Фамилия мастера', Name_detail AS 'Наименование детали' , CountDetails AS 'Кол-во деталей', CostDetails AS 'Стоимость детали', TotalCost AS 'ОБЩАЯ стоимость'  from Contract  LEFT JOIN `Services` ON services.id_services = contract.id_services LEFT JOIN `worker` ON worker.id_Worker = contract.id_worker LEFT JOIN `list_details` ON list_details.id_listdetails = contract.id_listdetails LEFT JOIN `detail` ON detail.id_detail = list_details.id_detail where id_owner = '" + id_Owner + "'", Connection);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(dowContr);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);// Загрузка данных
            dataGridView2.DataSource = dt; // Вывод данных
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[1].Width = 130;
            dataGridView2.Columns[2].Width = 130;
            dataGridView2.Columns[3].Width = 170;
            dataGridView2.Columns[4].Width = 130;//Стоимость услуги
            dataGridView2.Columns[5].Width = 148;
            dataGridView2.Columns[6].Width = 190;
            dataGridView2.Columns[7].Width = 120;
            dataGridView2.Columns[8].Width = 130;
            dataGridView2.Columns[9].Width = 130;
            // MessageBox.Show("Загрузка данных из id =" + id_Owner.ToString());


        }
        public void LoadDataFromTableOwner()// Метод для загрузки данных с таблицы Владелец + АВТО
        {
            MySqlCommand obd1 = new MySqlCommand("SELECT Owner.id_Owner, Surname_Owner AS 'Фамилия', Name_Owner AS 'Имя', MiddleName_Owner AS 'Отчество', PhoneNumber_Owner AS 'Номер телефона', Name_marka AS 'Марка', Name_model AS 'Модель', StateNumber AS 'Гос. номер', YearOfManufacture AS 'Год выпуска', Run AS 'Пробег' FROM Owner LEFT JOIN `Car` ON Car.id_Owner = Owner.id_Owner LEFT JOIN `Marka` ON Marka.id_marka = Car.id_marka LEFT JOIN `Model` ON Model.id_model = Car.id_model", Connection);
            // MySqlCommand obd1 = new MySqlCommand("SELECT * from Owner", Connection);
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(obd1);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);// Загрузка данных
            dataGridView1.DataSource = dt; // Вывод данных  
            dataGridView1.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)//Открытие формы 2
        {
            AddCar form2 = new AddCar(this);
            form2.Show();
        }
    
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Connection.Close();
            sf.Close();
        }

        private void button3_Click(object sender, EventArgs e)//добавление договора
        {
            string s = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
            int id = Convert.ToInt32(s);
            AddContract addContract = new AddContract(this, id);
            addContract.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Удаляет запись клиента из таблицы БД
            int id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString());

            string command1 = "delete from car where id_Owner = '" + id.ToString() + "'";
            MySqlCommand obd1 = new MySqlCommand(command1, Connection);
            obd1.ExecuteNonQuery();
            string command2 = "delete from owner where id_Owner = '" + id.ToString() + "'";
            MySqlCommand obd2 = new MySqlCommand(command2, Connection);
            if (obd2.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Вы успешно удалили!");
            }
            this.dataGridView1.Rows.Remove(this.dataGridView1.CurrentRow);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.RowIndex != dataGridView1.RowCount - 1)
            {
                DataGridViewRow d = dataGridView1.Rows[e.RowIndex];
                LoadDataFromTableContract(int.Parse(d.Cells[0].Value.ToString()));
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ChangeOwner changeOwner = new ChangeOwner(this);
            changeOwner.Show();
        }

        private void button7_Click(object sender, EventArgs e)//search
        {
            {

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Selected = false;
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        if (dataGridView1.Rows[i].Cells[7].Value != null)
                            if (dataGridView1.Rows[i].Cells[7].Value.ToString().Contains(textBox1.Text))
                            {
                                dataGridView1.Rows[i].Selected = true;
                                break;
                            }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Удаляет запись из таблицы и из БД
            int id = Convert.ToInt32(dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
            MySqlCommand cmd = new MySqlCommand("delete from contract where id_contract = '" + id +"'", Connection);
            cmd.ExecuteNonQuery();
            this.dataGridView2.Rows.Remove(this.dataGridView2.CurrentRow);
        }

  

        private void button2_Click_1(object sender, EventArgs e)
        {
            
            Entrance entrance = new Entrance();
            entrance.Show();
            this.Hide();
          
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCar form2 = new AddCar(this);
            form2.Show();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeOwner changeOwner = new ChangeOwner(this);
            changeOwner.Show();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Удаляет запись клиента из таблицы БД
            int id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString());

            string command1 = "delete from car where id_Owner = '" + id.ToString() + "'";
            MySqlCommand obd1 = new MySqlCommand(command1, Connection);
            obd1.ExecuteNonQuery();
            string command2 = "delete from owner where id_Owner = '" + id.ToString() + "'";
            MySqlCommand obd2 = new MySqlCommand(command2, Connection);
            if (obd2.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Вы успешно удалили!");
            }
            this.dataGridView1.Rows.Remove(this.dataGridView1.CurrentRow);
        }

        private void календарьЗаписейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Record form3 = new Record(this);
            form3.Show();
        }
        private string text = "";
        private void button1_Click_2(object sender, EventArgs e)
        {
            text = "Строка 1\n\n";
            text += "Строка 2\n\n";
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintPageHandler;
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;
            if (printDialog.ShowDialog() == DialogResult.OK)
                printDialog.Document.Print();

        }
        void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(text, new Font("Times New Roman", 14), Brushes.Black, 0, 0);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            var helper = new WordHelper("AAAContractPr.doc");

            var items = new Dictionary<string, string>
            {
               {"<id_Contract>", textBox2.Text }, 
            };
            helper.Process(items);
        }

        private void добавитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string s = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
            int id = Convert.ToInt32(s);
            AddContract addContract = new AddContract(this, id);
            addContract.Show();
        }

        private void удалитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
            MySqlCommand cmd = new MySqlCommand("delete from contract where id_contract = '" + id + "'", Connection);
            cmd.ExecuteNonQuery();
            this.dataGridView2.Rows.Remove(this.dataGridView2.CurrentRow);
        }

        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Worker form7 = new Worker(this);
            form7.Show();
          
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            {

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Selected = false;
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        if (dataGridView1.Rows[i].Cells[3].Value != null)
                            if (dataGridView1.Rows[i].Cells[2].Value.ToString().Contains(textBox3.Text))
                            {
                                dataGridView1.Rows[i].Selected = true;
                                break;
                            }
                }
            }
        }
    }
 
}
