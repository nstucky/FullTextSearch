using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FullTextSearch.Commands
{
  public abstract class CommandBase : ICommand
  {
    public event EventHandler CanExecuteChanged;
    public event EventHandler Executed;

    public bool CanExecute(object obj)
    {
      return true;
    }

    protected virtual void OnCanExecuteChanged(EventArgs e)
    {
      CanExecuteChanged(this, e);
    }

    protected virtual void OnExecuted(object oParameter, EventArgs e)
    {
      Executed(this, e);
    }

    public void Execute(object obj)
    {
      OnExecuted(obj, new EventArgs());
    }
  }
}
