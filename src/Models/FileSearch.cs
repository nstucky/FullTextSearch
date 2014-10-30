using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FullTextSearch.Models
{
  public class FileSearch
  {
    public static List<Result> FindAllFiles(string sDirectory, string sFileExtensions, string sSearchFor, bool fUseRegex, bool fMatchCase)
    {
      List<Result> lstResults = new List<Result>();

      if (System.IO.Directory.Exists(sDirectory))
      {
        IEnumerable<string> lstFiles = System.IO.Directory.EnumerateFiles(sDirectory, sFileExtensions, System.IO.SearchOption.AllDirectories).AsParallel();
        
        //TODO: make this work asynchronously
        foreach (string sFile in lstFiles)
        {
          string sFileText = System.IO.File.ReadAllText(sFile);
          Result oResult = null;
          
          //TODO: refactor this code to make it prettier.  It should work for the time being though
          if (fUseRegex)
          {
            var eOptions = RegexOptions.Multiline;
            if (!fMatchCase) eOptions |= RegexOptions.IgnoreCase;
            foreach (Match oMatch in Regex.Matches(sFileText, sSearchFor, eOptions))
            {
              oResult = new Result()
              {
                File = sFile,
                Text = oMatch.Value
              };
              lstResults.Add(oResult);
            }
          }
          else
          {
            if (sFileText.Contains(sSearchFor) || (!fMatchCase && sFileText.ToLower().Contains(sSearchFor.ToLower())))
            {
              string[] asFileText = sFileText.Split(Environment.NewLine.ToArray());
              foreach (string sResult in asFileText.Where(s => (fMatchCase ? s : s.ToLower()).Contains((fMatchCase ? sSearchFor : sSearchFor.ToLower()))))
              {
                oResult = new Result()
                {
                  File = sFile,
                  Text = sResult
                };
                lstResults.Add(oResult);
              }              
            }
          }
        }
      }

      return lstResults;
    }
  }
}
