#region License

/*
    Copyright (C) 2014  Nathan Stucky

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>. 
*/

#endregion

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
    private bool p_fLoading;

    public ResultDetail()
    {
      Initialize();
    }

    public ResultDetail(ResultViewModel vmResult)
    {
      p_fLoading = true;
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

    private void txtFileText_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (p_fLoading)
      {
        txtFileText.Focus();
        txtFileText.SelectionStart = txtFileText.Text.IndexOf(p_vmDetail.Text);
        txtFileText.SelectionLength = p_vmDetail.Text.Length;
        p_fLoading = false;
      }
    }

  }
}
