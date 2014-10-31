using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FullTextSearch.Models
{
  public class FileSearch
  {
    /// <summary>
    /// This method will find all the files in the specified directory with matching criteria for the arguments.
    /// </summary>
    /// <param name="sDirectory"></param>
    /// <param name="sFileExtensions"></param>
    /// <param name="sSearchFor"></param>
    /// <param name="fUseRegex"></param>
    /// <param name="fMatchCase"></param>
    /// <param name="bwWorker"></param>
    /// <returns></returns>
    public static List<Result> FindAllFiles(string sDirectory, string sFileExtensions, string sSearchFor, bool fUseRegex, bool fMatchCase, BackgroundWorker bwWorker)
    {
      List<Result> lstResults = new List<Result>();

      if (System.IO.Directory.Exists(sDirectory))
      {
        IEnumerable<string> lstFiles = System.IO.Directory.EnumerateFiles(sDirectory, sFileExtensions, System.IO.SearchOption.AllDirectories).AsParallel();
        int iCount = lstFiles.Count();
        double dCurrentFile = 0; //needs to be a double or the integer division won't work

        foreach (string sFile in lstFiles)
        {
          //Report progress to background worker
          bwWorker.ReportProgress((int)((dCurrentFile / iCount) * 100));

          //Read all text
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
          ++dCurrentFile;
        }
      }

      return lstResults;
    }
  }
}
