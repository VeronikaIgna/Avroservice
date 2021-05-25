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
    public partial class AddContract : Form
    {
        public static string ConnectionString = $"Server=localhost;Database=avtoservice;Uid=root;pwd={Settings.DataBasePassword};";

        MainForm f1;
        public int id_Owner;

        private MySqlConnection Connection = new MySqlConnection(ConnectionString);

        int totalDetailCost = 0, finalCost = 0;


        public AddContract(MainForm f1, int id_owner)
        {
            InitializeComponent();
            this.f1 = f1;
            this.id_Owner = id_owner;
        }

        private void AddContract_Load(object sender, EventArgs e)
        {
            Connection.Open();
            AddMaster();
            AddDetails();
            loadServices();
        }

        private void AddMaster()
        {
            MySqlCommand obd1 = new MySqlCommand("SELECT Surname_Work, id_Worker FROM worker", Connection);// SQL запрос для загрузки данных
            using (var reader = obd1.ExecuteReader())
            {
                while (reader.Read())
                {
                    masterBox.Items.Add(reader.GetString("Surname_Work"));
                    masArr.Add(new master(Convert.ToInt32(reader.GetString("id_Worker")), reader.GetString("Surname_Work")));
                }
            }
        }

        List<master> masArr = new List<master>();

        class master
        {
            public int id;
            public string sur;
            public master(int id,string sur)
            {
                this.id = id;
                this.sur = sur;
            }
        }

        private void AddDetails()
        {
            detailBox.Items.Clear();
            MySqlCommand obd1 = new MySqlCommand("SELECT id_detail, name_detail, price_detail FROM detail", Connection);// SQL запрос для загрузки данных
            using (var reader = obd1.ExecuteReader())
            {
                while (reader.Read())
                {
                    detailBox.Items.Add(reader.GetString("name_detail"));
                    detArr.Add(new detail(Convert.ToInt32(reader.GetString("id_detail")), reader.GetString("name_detail"), Convert.ToInt32(reader.GetString("price_detail"))));
                }
            }

        }

        List<detail> detArr = new List<detail>();

        class detail
        {
            public int id, price_detail;
            public string name;
            public detail(int id, string name, int price_detail)
            {
                this.id = id;
                this.name = name;
                this.price_detail = price_detail;
            }
        }

        private void AddContract_FormClosed(object sender, FormClosedEventArgs e)
        {
            Connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void loadServices()
        {
            serviceBox.Items.Clear();
            MySqlCommand obd1 = new MySqlCommand("SELECT id_services, Name_Services, Price_Services FROM services", Connection);// SQL запрос для загрузки данных
            using (var reader = obd1.ExecuteReader())
            {
                while (reader.Read())
                {
                    serviceBox.Items.Add(reader.GetString("Name_Services"));
                    servArr.Add(new Services(Convert.ToInt32(reader.GetString("Price_Services")), reader.GetString("Name_Services"), Convert.ToInt32(reader.GetString("id_services"))));
                }
            }
        }

        private void butAddContract_Click(object sender, EventArgs e)// добавление данных о договоре
        {
            {
                if ((dateTimePicker1.Text != "" ) && (dateTimePicker2.Text != "") && (serviceBox.Text != "") && (serviceCostEdit.Text != "") && (detailCountEdut.Text != ""))
                {
                    if (totalDetailCost == 0)
                    {
                        MessageBox.Show("Сумма деталей не подсчитана!");
                    }
                    else if (finalCost == 0)
                    {
                        MessageBox.Show("Общая сумма не подсчитана!");
                    }
                    else
                    {
                        MySqlCommand com = new MySqlCommand();
                        com.Connection = Connection;

                        string detValues = "('"+detailCountEdut.Text+"','"+totalDetailCost+"','"+ detArr[detailBox.SelectedIndex].id+"')";
                        com.CommandText = "insert into list_details(countdetails, costdetails, id_detail) values " + detValues + "; SELECT LAST_INSERT_ID();";
                        int idDet = Convert.ToInt32(com.ExecuteScalar());

                        int masterId = masArr[masterBox.SelectedIndex].id;
                        int serviceId = servArr[serviceBox.SelectedIndex].id;
                        string values = "('" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "', '" + dateTimePicker2.Value.Date.ToString("yyyy-MM-dd") + "', '" + finalCost.ToString() +"', '" + id_Owner.ToString() + "','"+masterId+"','"+serviceId+"','" + idDet +"')";
                        com.CommandText = "insert into `contract` (startdate, enddate, totalcost, id_owner, id_Worker, id_services, id_listDetails) values " + values;
                        com.ExecuteNonQuery();
                        f1.LoadDataFromTableContract(id_Owner);
                        Close();
                    }
                    }
                

                
                else MessageBox.Show("Договор не добавлен! Пропущенны поля!");
            }

        }

        private string NewMethod(int v)
        {
            throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)//Подсчет суммы
        {
            if (serviceBox.Text == "")
            {
                MessageBox.Show("Выберите услугу!");
            }
            else
            {
                if (detailCountEdut.Text == "")
                {
                    MessageBox.Show("Введите количество деталей");
                }
                else
                {
                    
                    int detailCost = detArr[detailBox.SelectedIndex].price_detail;
                    totalDetailCost = detailCost * Int32.Parse(detailCountEdut.Text);
                    finalJobCostLabel.Text = "Стоимость деталей: " + totalDetailCost;
                    finalCost = totalDetailCost + Convert.ToInt32(serviceCostEdit.Text);
                    finalCostLabel.Text = "Общая стоимость работы: " + finalCost;
                }
            }
        }

        List<Services> servArr = new List<Services>();

        class Services
        {
            public int price, id;
            public string name;
            public Services(int price, string name, int id)
            {
                this.price = price;
                this.name = name;
                this.id = id;
            }
        }

      

        private void serviceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Services serv = servArr[serviceBox.SelectedIndex];
            serviceCostEdit.Text = serv.price.ToString();
        }
    }
}
