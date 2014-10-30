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

    #endregion

    #region Constructors

    public ResultDetailViewModel(ResultViewModel vmResult)
    {
      Text = vmResult.Text;
      PathFileName = vmResult.PathFileName;
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
          NotifyPropertyChanged();
        }
      }
    }

    #endregion


  }
}
