using Cafe.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;

namespace Pariks
{

    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Excel.Application excelApp;
        public static Excel.Workbook excelBook;
        public static Excel.Worksheet excelSheet;
        public static Excel.Range excelCells;

        public static Entity.PariksDB DB;
        public static List<Entity.Categories> categoriesDB;
        public static List<Entity.Products> productsDB;


        public static string fileMenu = Environment.CurrentDirectory + "/../../PriceList/Pricelist.xlsx";	//К файлу Excel

        public static double amount;
        public static double onCard;

        public static string login = "admin";
        public static string password = "1234";
    }
}
