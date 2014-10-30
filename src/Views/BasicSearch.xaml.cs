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

namespace FullTextSearch.Views
{
  /// <summary>
  /// Interaction logic for BasicSearch.xaml
  /// </summary>
  public partial class BasicSearch : Page
  {
    private ViewModels.SearchViewModel p_vmSearch;

    public BasicSearch()
    {
      InitializeComponent();
      p_vmSearch = (ViewModels.SearchViewModel)DataContext;
    }

    private void Hyperlink_Click(object sender, RoutedEventArgs e)
    {
      ResultDetail viewDetail = new ResultDetail(p_vmSearch.SelectedResult);
      viewDetail.Show();
      viewDetail.Activate();
    }

  }
}
