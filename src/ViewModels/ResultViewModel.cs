using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextSearch.ViewModels
{
  public class ResultViewModel : ViewModelBase
  {
    #region Constructors

    public ResultViewModel()
    {
      p_sFileName = string.Empty;
      p_sPathFileName = string.Empty;
      p_sText = string.Empty;
      p_fIsSelected = false;
    }

    #endregion

    #region Private Variables

    private string p_sFileName;
    private string p_sPathFileName;
    private string p_sText;
    private bool p_fIsSelected;

    #endregion

    #region Properties

    public string FileName
    {
      get
      {
        return p_sFileName;
      }
      set
      {
        p_sFileName = value;
        NotifyPropertyChanged();
      }
    }

    public string PathFileName
    {
      get
      {
        return p_sPathFileName;
      }
      set
      {
        if (p_sPathFileName != value)
        {
          p_sPathFileName = value;
          NotifyPropertyChanged();
          FileName = System.IO.Path.GetFileName(p_sPathFileName);
        }
      }
    }

    public string Text
    {
      get
      {
        return p_sText;
      }
      set
      {
        p_sText = value;
        NotifyPropertyChanged();
      }
    }

    public bool IsSelected
    {
      get
      {
        return p_fIsSelected;
      }
      set
      {
        if (value != p_fIsSelected)
        {
          p_fIsSelected = value;
          NotifyPropertyChanged();
        }
      }
    }

    #endregion

  }
}
