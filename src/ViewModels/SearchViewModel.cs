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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace FullTextSearch.ViewModels
{
  public class SearchViewModel : ViewModels.ViewModelBase
  {
    #region Private Variables

    private string p_sDirectory;
    private string p_sFileExtensions;
    private string p_sSearchText;
    private bool p_fRegex;
    private bool p_fMatchCase;
    private bool p_fSearching;
    private ObservableCollection<ResultViewModel> p_vmResults;
    private bool p_fProgressIndeterminite;
    private int p_iProgress;
    
    #endregion

    #region Properties

    public Commands.DefaultCommand SearchCommand { get; private set; }
    public Commands.DefaultCommand BrowseCommand { get; private set; }

    public string Directory
    {
      get
      {
        return p_sDirectory;
      }
      set
      {
        p_sDirectory = value;
        NotifyPropertyChanged();
      }
    }

    public string FileExtensions
    {
      get
      {
        return p_sFileExtensions;
      }
      set
      {
        p_sFileExtensions = value;
        NotifyPropertyChanged();
      }
    }

    public string SearchText
    {
      get
      {
        return p_sSearchText;
      }
      set
      {
        p_sSearchText = value;
        NotifyPropertyChanged();
      }
    }

    public bool Regex
    {
      get
      {
        return p_fRegex;
      }
      set
      {
        p_fRegex = value;
        NotifyPropertyChanged();
      }
    }

    public bool MatchCase
    {
      get
      {
        return p_fMatchCase;
      }
      set
      {
        p_fMatchCase = value;
        NotifyPropertyChanged();
      }
    }

    public ObservableCollection<ResultViewModel> Results
    {
      get
      {
        return p_vmResults;
      }
    }

    public ResultViewModel SelectedResult
    {
      get
      {
        return p_vmResults.FirstOrDefault(vm => vm.IsSelected);
      }
    }

    public bool Searching
    {
      get
      {
        return p_fSearching;
      }
      set
      {
        p_fSearching = value;
        NotifyPropertyChanged();
      }
    }

    public int Progress
    {
      get
      {
        return p_iProgress;
      }
      set
      {
        p_iProgress = value;
        NotifyPropertyChanged();
        if (p_iProgress > 0 && p_fProgressIndeterminite) ProgressIndeterminite = false;
        if (p_iProgress == 0 && !p_fProgressIndeterminite) ProgressIndeterminite = true;
      }

    }

    public bool ProgressIndeterminite
    {
      get 
      {
        return p_fProgressIndeterminite;
      }
      set
      {
        p_fProgressIndeterminite = value;
        NotifyPropertyChanged();
      }
    }

    #endregion

    #region Constructors

    public SearchViewModel()
    {
      SearchCommand = new Commands.DefaultCommand();
      SearchCommand.Executed += BeginSearchCommand_Executed;
      BrowseCommand = new Commands.DefaultCommand();
      BrowseCommand.Executed += BrowseDirectoryCommand_Executed;
      p_vmResults = new ObservableCollection<ResultViewModel>();
      p_sDirectory = string.Empty;
      p_sFileExtensions = string.Empty;
      p_sSearchText = string.Empty;
      p_fRegex = false;
      p_fMatchCase = false;
      p_fSearching = false;
    }

    #endregion

    #region Event Handlers

    private void BeginSearchCommand_Executed(object sender, EventArgs e)
    {
      Results.Clear();
      Searching = true;
      BackgroundWorker bwWorker = new BackgroundWorker();
      bwWorker.WorkerReportsProgress = true;
      bwWorker.DoWork += bwWorker_DoWork;
      bwWorker.ProgressChanged += bwWorker_ProgressChanged;
      bwWorker.RunWorkerAsync(Dispatcher.CurrentDispatcher);
    }

    private void bwWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      Progress = e.ProgressPercentage;
    }

    private void bwWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      List<Models.Result> lstResults;

      //Try to find the files with the specified criteria, but if an exception occurs give a message to the user
      try
      {
        lstResults = FullTextSearch.Models.FileSearch.FindAllFiles(Directory, FileExtensions, SearchText, Regex, MatchCase, (BackgroundWorker)sender);
      }
      catch (Exception ex)
      {
        //Only show the message of the exception if the exception is one that we expect to receive.  For anything else re-throw the exception
        //to preserve the stack trace
        if (ex is ArgumentException || ex is UnauthorizedAccessException || ex is System.IO.DirectoryNotFoundException || 
            ex is System.IO.PathTooLongException || ex is System.IO.IOException || ex is NotSupportedException ||
            ex is System.Security.SecurityException)
        {
          System.Windows.MessageBox.Show(ex.Message);
          Searching = false;
          return;
        }
        else
        {
          throw;
        }
      }
      
      if (e.Argument is Dispatcher){
        ((Dispatcher)e.Argument).BeginInvoke(new System.Action(() =>
        {
          foreach (var oResult in lstResults)
          {
            ResultViewModel vmResult = new ResultViewModel()
            {
              PathFileName = oResult.File,
              Text = oResult.Text
            };
            Results.Add(vmResult);
          }
          Searching = false;
          Progress = 0;
        }));
      }
    }

    private void BrowseDirectoryCommand_Executed(object sender, EventArgs e)
    {
      var oBrowse = new System.Windows.Forms.FolderBrowserDialog()
      {
        Description = "Search Directory",
        ShowNewFolderButton = false
      };
      if (oBrowse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        Directory = oBrowse.SelectedPath;
    }

    #endregion
  }
}