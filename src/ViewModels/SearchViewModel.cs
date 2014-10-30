using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FullTextSearch.ViewModels
{
  public class SearchViewModel : ViewModels.ViewModelBase
  {
    #region Private Variables

    string p_sDirectory;
    string p_sFileExtensions;
    string p_sSearchText;
    bool p_fRegex;
    bool p_fMatchCase;
    ObservableCollection<ResultViewModel> p_vmResults;
    
    #endregion

    #region Properties

    public ICommand BeginSearchCommand { get; private set; }

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

    #endregion

    #region Constructors

    public SearchViewModel()
    {
      BeginSearchCommand = new Commands.SearchCommand(BeginSearch);
      p_vmResults = new ObservableCollection<ResultViewModel>();
    }

    #endregion

    #region Methods

    public void BeginSearch()
    {
      var lstResults = FullTextSearch.Models.FileSearch.FindAllFiles(Directory, FileExtensions, SearchText, Regex, MatchCase);

      foreach (var oResult in lstResults)
      {
        ResultViewModel vmResult = new ResultViewModel()
        {
          PathFileName = oResult.File,
          Text = oResult.Text
        };
        Results.Add(vmResult);
      }
    }

    #endregion
  }
}