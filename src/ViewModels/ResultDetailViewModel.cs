using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextSearch.ViewModels
{
  class ResultDetailViewModel : ResultViewModel
  {
    #region Private Variables

    private string p_sFileText;
    private bool p_fReadOnly;
    private bool p_fChanged;

    #endregion

    #region Constructors

    public ResultDetailViewModel(ResultViewModel vmResult)
    {
      Text = vmResult.Text;
      PathFileName = vmResult.PathFileName;
      OpenFileCommand = new Commands.DefaultCommand();
      OpenFileCommand.Executed += OpenFileCommand_Executed;
      SaveFileCommand = new Commands.DefaultCommand();
      SaveFileCommand.Executed += SaveFileCommand_Executed;
      p_fReadOnly = true;
      p_fChanged = false;
      if (!string.IsNullOrWhiteSpace(PathFileName))
      {
        p_sFileText = System.IO.File.ReadAllText(PathFileName);
      }
    }

    #endregion

    #region Properties

    public string FileText
    {
      get
      {
        return p_sFileText;
      }
      set
      {
        if (p_sFileText != value)
        {
          p_sFileText = value;
          Changed = true;
          NotifyPropertyChanged();
        }
      }
    }

    public bool ReadOnly
    {
      get
      {
        return p_fReadOnly;
      }
      set
      {
        p_fReadOnly = value;
        NotifyPropertyChanged();
        NotifyPropertyChanged("CanSave");
      }
    }

    public bool WriteEnabled
    {
      get
      {
        return !p_fReadOnly;
      }
      set
      {
        ReadOnly = !value;
        NotifyPropertyChanged();
      }
    }

    public bool CanSave
    {
      get
      {
        return !p_fReadOnly && p_fChanged;
      }
    }

    public bool Changed
    {
      get
      {
        return p_fChanged;
      }
      set
      {
        p_fChanged = value;
        NotifyPropertyChanged();
        NotifyPropertyChanged("CanSave");
      }
    }

    public Commands.DefaultCommand OpenFileCommand { get; private set; }
    public Commands.DefaultCommand SaveFileCommand { get; private set; }

    #endregion

    #region Event Handlers

    void SaveFileCommand_Executed(object sender, EventArgs e)
    {
      if (p_fChanged)
      {
        System.IO.File.WriteAllText(PathFileName, FileText);
        Changed = false;
      }
    }

    void OpenFileCommand_Executed(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start(PathFileName);
    }

    #endregion

  }
}
