﻿#region License

/*
    Copyright (C) 2014  Nathan Stucky

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>. 
*/

#endregion

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
        //Get all files at once so we can get the full count and report progress back to the user.  It will probably take slightly longer than 
        //if we we're reporting progress and just enumerating the files, but then we would have no way of telling how many files are returned until
        //we go through the whole list.
        List<string> lstFiles = System.IO.Directory.GetFiles(sDirectory, sFileExtensions, System.IO.SearchOption.AllDirectories).ToList();
        int iCount = lstFiles.Count();
        double dCurrentFile = 0; //needs to be a double or the integer division won't work

        foreach (string sFile in lstFiles)
        {
          //Report progress to background worker
          bwWorker.ReportProgress((int)((dCurrentFile / iCount) * 100));

          //Read all text
          string sFileText = System.IO.File.ReadAllText(sFile);

          //TODO: refactor this code to make it prettier.  It should work for the time being though
          if (fUseRegex)
          {
            var eOptions = RegexOptions.Multiline;
            if (!fMatchCase) eOptions |= RegexOptions.IgnoreCase;
            foreach (Match oMatch in Regex.Matches(sFileText, sSearchFor, eOptions))
            {
              lstResults.Add(new Result(oMatch.Value, sFile));
            }
          }
          else
          {
            if (fMatchCase ? sFileText.Contains(sSearchFor) : sFileText.ToLower().Contains(sSearchFor.ToLower()))
            {
              string[] asFileText = sFileText.Split(Environment.NewLine.ToArray());

              //Finds all matches and adds them to the results list
              lstResults.AddRange(asFileText.Where(s => (fMatchCase ? s : s.ToLower()).Contains((fMatchCase ? sSearchFor : sSearchFor.ToLower())))
                                            .Select((sResult) => new Result(sResult, sFile))); 
            }
          }
          ++dCurrentFile;
        }
      }

      return lstResults;
    }
  }
}
