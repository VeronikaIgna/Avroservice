using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection; 
 
namespace AvtoService
{
    public partial class Print : Form
    {
        public Print()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // Excel.Application PrXL;  // Приложение Excel
            //Excel._Workbook oWB;     // Рабочая книга которую нужно открыть
           // Excel._Worksheet oSheet; // Рабочая вкладка книги
            try
            {
                //Стартуем Excel с указанием пути к файлу с шаблоном (У меня офис 2010 все ок работает)
               // PrXL = new Excel.Application();
               // oWB = (Excel._Workbook)(PrXL.Workbooks.Open(@"c:\Forma.xlsx"));
               // PrXL.Visible = true;
               // oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                //Заполняем все что нужно данными
               // oSheet.Cells[9, 1] = Data1.Text;
               // oSheet.Cells[2, 12] = Data2.Text;
              //  oSheet.Cells[4, 12] = dateTimePicker1.Text;


                // Открываем MS Excel.
                //PrXL.Visible = true;
              //  PrXL.UserControl = true;
            }
            //Обязательно отработаем ошибки
            catch (Exception theException)
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, theException.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, theException.Source);

                MessageBox.Show(errorMessage, "Error");
            }
        }
    }
}