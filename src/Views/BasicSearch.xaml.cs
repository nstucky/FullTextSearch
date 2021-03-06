﻿#region License

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

    private void mnuAbout_Click(object sender, RoutedEventArgs e)
    {
      var oAbout = new About();
      oAbout.Show();
    }

    private void mnuExit_Click(object sender, RoutedEventArgs e)
    {
      Application.Current.Shutdown();
    }
  }
}
