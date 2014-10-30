using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextSearch.Models
{
  public class Result
  {
    public string Text { get; set; }
    public string File { get; set; }

    public override string ToString()
    {
      return Text + Environment.NewLine + File;
    }
  }
}
