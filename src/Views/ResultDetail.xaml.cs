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
      InitializeComponent();
    }

    public ResultDetail(ResultViewModel vmResult)
    {
      InitializeComponent();
      if (vmResult == null)
      {
        MessageBox.Show("Invalid argument passed to detail screen!");
        Close();
        return;
      }
      p_vmDetail = new ResultDetailViewModel(vmResult);
      DataContext = p_vmDetail;
    }

  }
}
