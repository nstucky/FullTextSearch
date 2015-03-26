#region License

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
    protected static HunterFactory Factory
    {
      get
      {
        return new HunterFactory();
      }
    }

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
    /// <exception cref="System.ArgumentException">
    ///    path is a zero-length string, contains only white space, or contains one
    ///    or more invalid characters as defined by System.IO.Path.InvalidPathChars.
    ///    -or- searchPattern does not contain a valid pattern.
    /// </exception>
    /// <exception cref="System.UnauthorizedAccessException">
    ///   The caller does not have the required permission.
    /// </exception>
    /// <exception cref="System.IO.DirectoryNotFoundException">
    ///   The specified path is invalid (for example, it is on an unmapped drive).
    /// </exception>
    /// <exception cref="System.IO.PathTooLongException">
    ///    The specified path, file name, or both exceed the system-defined maximum
    ///    length. For example, on Windows-based platforms, paths must be less than
    ///    248 characters and file names must be less than 260 characters.
    /// </exception>
    /// <exception cref="System.IO.IOException">
    ///   path is a file name.-or-A network error has occurred.
    /// </exception>
    /// <exception cref="System.NotSupportedException">
    ///   path is in an invalid format.
    /// </exception>
    /// <exception cref="System.Security.SecurityException">
    ///   The caller does not have the required permission.
    /// </exception>  
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
        var oHunter = Factory.GetHunter(fUseRegex, fMatchCase);
        
        foreach (string sFile in lstFiles)
        {
          //Report progress to background worker
          bwWorker.ReportProgress((int)((dCurrentFile / iCount) * 100));

          //Read all text
          string sFileText = System.IO.File.ReadAllText(sFile);

          //Search for any matches, and if any are found add them to the list
          var ieFileResults = oHunter.SearchFile(sSearchFor, sFileText, sFile);
          if (ieFileResults.Any())
          {
            lstResults.AddRange(ieFileResults);
          }

          ++dCurrentFile;
        }
      }

      return lstResults;
    }
  }
}
