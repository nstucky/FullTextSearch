using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FullTextSearch.Commands
{
  public class SearchCommand : ICommand
  {
    public event EventHandler CanExecuteChanged;
    private System.Action p_actBeginSearch;

    public SearchCommand(System.Action actBeginSearch)
    {
      p_actBeginSearch = actBeginSearch;
    }

    public bool CanExecute(object obj) 
    {
      return true;
    }

    protected virtual void OnCanExecuteChanged(EventArgs e)
    {
      CanExecuteChanged(this, e);
    }

    public void Execute(object obj)
    {
      p_actBeginSearch();
    }
  }
}
