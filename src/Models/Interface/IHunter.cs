using System.Collections.Generic;

namespace FullTextSearch.Models.Interface
{
  public interface IHunter
  {
    IEnumerable<Result> SearchFile(string sSearchFor, string sFileText, string sFileName);
  }
}
