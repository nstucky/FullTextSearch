using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FullTextSearch.ViewModels;

namespace FullTextSearch.Views
{
  /// <summary>
  /// Interaction logic for ResultDetail.xaml
  /// </summary>
  public partial class ResultDetail : Window
  {
    private ResultDetailViewModel p_vmDetail;

    public ResultDetail()
    {
      Initialize();
    }

    public ResultDetail(ResultViewModel vmResult)
    {
      Initialize();
      if (vmResult == null)
      {
        MessageBox.Show("Invalid argument passed to detail screen!");
        Close();
        return;
      }
      p_vmDetail = new ResultDetailViewModel(vmResult);
      DataContext = p_vmDetail;
    }

    private void Initialize()
    {
      InitializeComponent();
      this.Closing += ResultDetail_Closing;
    }

    private void ResultDetail_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if (p_vmDetail.Changed)
      {
        e.Cancel = MessageBox.Show("Are you sure you want to close without saving?", 
                                   "Result Detail", 
                                   MessageBoxButton.YesNo, 
                                   MessageBoxImage.Question, 
                                   MessageBoxResult.Yes) == MessageBoxResult.No;
      }
    }


  }
}
