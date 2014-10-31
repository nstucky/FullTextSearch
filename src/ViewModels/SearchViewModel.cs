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

    public Commands.SearchCommand BeginSearchCommand { get; private set; }

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
      BeginSearchCommand = new Commands.SearchCommand();
      BeginSearchCommand.Executed += BeginSearchCommand_Executed;
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
      var lstResults = FullTextSearch.Models.FileSearch.FindAllFiles(Directory, FileExtensions, SearchText, Regex, MatchCase, (BackgroundWorker)sender);

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

    #endregion
  }
}