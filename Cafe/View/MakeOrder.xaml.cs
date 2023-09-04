using Cafe.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Word = Microsoft.Office.Interop.Word;

namespace Pariks.View
{
    /// <summary>
    /// Логика взаимодействия для MakeOrder.xaml
    /// </summary>
    public partial class MakeOrder : Window
    {
        double amount = App.amount;
        double onCard = App.onCard;
        public List<ProductInOrder> listProductsInOrders;

        public MakeOrder(List<ProductInOrder> list)
        {
            listProductsInOrders = list;
            InitializeComponent();           
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            InsertData();
            txtOnCard.Text = $"На карте\n{onCard} рублей";
        }
        
        private void InsertData()
        {
            tableOrder.ItemsSource = null;
            amount = 0;
            for (int i = 0; i < listProductsInOrders.Count; i++)
            {
                listProductsInOrders[i].Sum = listProductsInOrders[i].Cost * listProductsInOrders[i].Count;
                amount += listProductsInOrders[i].Sum;
            }
            txtAmount.Text = $"Сумма заказа\n{amount} рублей";
            tableOrder.ItemsSource = listProductsInOrders;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ProductInOrder productInOrder = (ProductInOrder)tableOrder.SelectedItem;

            int id = listProductsInOrders.IndexOf(productInOrder);
            listProductsInOrders[id].Count++;
            InsertData();

            if (amount > onCard)
            {
                listProductsInOrders[listProductsInOrders.IndexOf(productInOrder)].Count--;
                InsertData();
                MessageBox.Show("У Вас уже не хватает денег");
            }
        }

        private void btnReduce_Click(object sender, RoutedEventArgs e)
        {
            ProductInOrder productInOrder = (ProductInOrder)tableOrder.SelectedItem;
            listProductsInOrders[listProductsInOrders.IndexOf(productInOrder)].Count--;
            if (listProductsInOrders[listProductsInOrders.IndexOf(productInOrder)].Count == 0) listProductsInOrders.Remove(productInOrder);
            InsertData();
        }


        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            ProductInOrder productInOrder = (ProductInOrder)tableOrder.SelectedItem;
            listProductsInOrders.Remove(productInOrder);
            InsertData();
        }

        public void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Не могу освободить объект " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void butCheck_Click(object sender, RoutedEventArgs e)
        {
            //Создание чека заказа
            //Объявление необходимых величин
            Word.Application wordApp;           //Сервер Word
            Word.Document wordDoc;          //Документ Word
            Word.Paragraph wordPar;         //Абзац документа
            Word.Range wordRange;           //Текст абзаца
            Word.Table wordTable;           //Таблица 
            Word.InlineShape wordShape;     //Рисунок
                                            //Создание сервера Word
            try
            {
                wordApp = new Word.Application();
                wordApp.Visible = false;
            }
            catch
            {
                MessageBox.Show("Товарный чек в Word создать не удалось");
                return;
            }
            //Создание документа Word
            wordDoc = wordApp.Documents.Add();      //Добавить новый пустой документ
            wordDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait; // Книжная

            //***Первый параграф – логотип
            wordPar = wordDoc.Paragraphs.Add();
            wordRange = wordPar.Range;
            wordShape = wordDoc.InlineShapes.AddPicture(Environment.CurrentDirectory + "/../../Resourse/Logo.png", Type.Missing, Type.Missing, wordRange);
            wordShape.Width = 100;
            wordShape.Height = 100;

            //***Второй параграф - дата и время заказа
            wordPar = wordDoc.Paragraphs.Add();
            wordRange = wordPar.Range;
            wordRange.Font.Color = Word.WdColor.wdColorBlack;
            wordRange.Font.Size = 16;
            wordRange.Font.Name = "Times New Roman"; //Текст первого абзаца – заголовка документа
            wordRange.Text = "Дата и время заказа: " + DateTime.Now.ToString();
            wordRange.InsertParagraphAfter();

            //***Третий параграф - заголовок таблицы
            wordPar = wordDoc.Paragraphs.Add();
            wordPar.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordRange = wordPar.Range;
            wordRange.Font.Color = Word.WdColor.wdColorBlack;
            wordRange.Font.Size = 16;
            wordRange.Font.Name = "Times New Roman";
            wordRange.Text = "Список заказанных блюд";
            wordRange.InsertParagraphAfter();

            
            //***Четверный параграф - таблица
            //wordPar = wordDoc.Paragraphs.Add();
            wordRange = wordPar.Range;
            //Число строк в таблицы совпадает с число строк в таблице заказов формы
            wordTable = wordDoc.Tables.Add(wordRange, listProductsInOrders.Count + 1, 4);
            wordTable.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            wordTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingleWavy;
            //Заголовков таблицы из ЭУ DataGrid
            Word.Range cellRange;
            for (int col = 1; col <= 4; col++)
            {
                cellRange = wordTable.Cell(1, col).Range;
                cellRange.Text = tableOrder.Columns[col - 1].Header.ToString();
            }
            //Можно выполнить заливку заголовка таблицы
            wordTable.Rows[1].Shading.ForegroundPatternColor = Word.WdColor.wdColorLightYellow;
            wordTable.Rows[1].Shading.BackgroundPatternColorIndex = Word.WdColorIndex.wdBlue;
            wordTable.Rows[1].Range.Bold = 1;
            wordTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            //wordRange.Font.Italic = 1;
            //Заполнение ячеек таблицы из списка заказов
            wordRange.Font.Size = 14;
            wordRange.Font.Color = Word.WdColor.wdColorBlack;
            wordRange.Font.Name = "Times New Roman";
            wordPar.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            for (int row = 2; row <= listProductsInOrders.Count + 1; row++)
            {
                cellRange = wordTable.Cell(row, 1).Range;
                cellRange.Font.Size = 12;
                cellRange.Font.Color = Word.WdColor.wdColorGray80;
                cellRange.Font.Italic = 1;
                cellRange.Text = listProductsInOrders[row - 2].Name;
                //wordRange.Font.Italic = 0;
                cellRange = wordTable.Cell(row, 2).Range;
                cellRange.Font.Size = 12;
                cellRange.Font.Color = Word.WdColor.wdColorGray60;
                cellRange.Font.Italic = 1;
                cellRange.Text = listProductsInOrders[row - 2].Cost.ToString();
                cellRange = wordTable.Cell(row, 3).Range;
                cellRange.Font.Size = 12;
                cellRange.Font.Color = Word.WdColor.wdColorGray60;
                cellRange.Font.Italic = 1;
                cellRange.Text = listProductsInOrders[row - 2].Count.ToString();
                cellRange = wordTable.Cell(row, 4).Range;
                cellRange.Font.Size = 12;
                cellRange.Font.Color = Word.WdColor.wdColorGray60;
                cellRange.Font.Italic = 1;
                cellRange.Text = listProductsInOrders[row - 2].Costing.ToString();
            }
            //wordRange.InsertParagraphAfter();   

            //*************Пятый параграф - итоги
            wordPar = wordDoc.Paragraphs.Add();
            wordPar.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordRange = wordPar.Range;
            wordRange.Font.Color = Word.WdColor.wdColorRed;
            wordRange.Font.Size = 16;
            wordRange.Font.Name = "Times New Roman";
            wordRange.Bold = 2;
            wordRange.Text = "Стоимость заказа: " + amount.ToString() + " рублей";
            wordRange.InsertParagraphAfter();

            //***Шестой параграф - печать
            wordPar = wordDoc.Paragraphs.Add();
            wordRange = wordPar.Range;
            wordShape = wordDoc.InlineShapes.AddPicture(Environment.CurrentDirectory + "/../../Resourse/stamp.jpg", Type.Missing, Type.Missing, wordRange);
            wordShape.Width = 150;
            wordShape.Height = 150;

            //***Седьмой параграф - подписьТекст
            wordPar = wordDoc.Paragraphs.Add();
            wordPar.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            wordRange = wordPar.Range;
            wordRange.Font.Color = Word.WdColor.wdColorBlack;
            wordRange.Font.Size = 16;
            wordRange.Font.Name = "Times New Roman";
            wordRange.Text = "Подпись:";
            wordRange.InsertParagraphAfter();

            //***Восьмой параграф - подпись
            wordPar = wordDoc.Paragraphs.Add();
            wordRange = wordPar.Range;
            wordShape = wordDoc.InlineShapes.AddPicture(Environment.CurrentDirectory + "/../../Resourse/writing.jpg", Type.Missing, Type.Missing, wordRange);
            wordShape.Width = 50;
            wordShape.Height = 25;

            //Сохранение документа
            string fileName = Environment.CurrentDirectory + "/../../Checks/Чек";
            wordDoc.SaveAs(fileName + ".docx");
            wordDoc.SaveAs(fileName + ".pdf", Word.WdExportFormat.wdExportFormatPDF);
            //Завершение работы с Word
            wordDoc.Close(true, null, null);                //Сначала закрыть документ
            wordApp.Quit();                     //Выход из Word
                                                //Вызвать свою подпрограмму убивания процессов
            releaseObject(wordPar);                 //Уничтожить абзац
            releaseObject(wordDoc);                 //Уничтожить документ
            releaseObject(wordApp);                 //Удалить из Диспетчера задач

            MessageBox.Show("Чек успешно создан!");
        }

    }
}
