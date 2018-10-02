using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace VacationPlus.Windows.AdminWindow.Pages
{
    public partial class DocumentsPage : Page
    {
        public DocumentsPage()
        {
            InitializeComponent();
            DocListHire.ItemsSource = AdminWindow.logic.GetDocHireList();
            DocListFire.ItemsSource = AdminWindow.logic.GetDocFireList();
        }

        private void DocList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListView).SelectedItem as VP.BAL.Classes.VPFWDocument != null)
            {
                FileStream fileStream = File.Open(((sender as ListView).SelectedItem as VP.BAL.Classes.VPFWDocument).FullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                FlowDocument flowDocument = new FlowDocument();
                TextRange textRange = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);
                if (textRange.CanLoad(DataFormats.Rtf))
                {
                    textRange.Load(fileStream, DataFormats.Rtf);
                }
                FWDocument.Document = flowDocument;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DocListFire.SelectedItem != null || DocListHire.SelectedItem != null)
            {
                PrintDialog pr = new PrintDialog();
                if ((pr.ShowDialog() == true))
                {
                    pr.PrintDocument((((IDocumentPaginatorSource)FWDocument.Document).DocumentPaginator), "Печать документа");
                    AdminWindow.SetSettingLabel("Документ был успешно распечатан!");
                }
                else
                    AdminWindow.SetSettingLabel("Отмена печати");
            }
            else
                AdminWindow.SetSettingLabel("Выберите документ!");
        }
    }
}