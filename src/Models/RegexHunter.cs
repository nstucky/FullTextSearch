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

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FullTextSearch.Models
{
  public class RegexHunter : Interface.IHunter
  {
    #region Private Variables

    private System.Text.RegularExpressions.RegexOptions p_eSearchOptions;

    #endregion

    #region Constructors

    public RegexHunter(bool fMatchCase)
    {
      p_eSearchOptions = RegexOptions.Multiline;
      if (!fMatchCase) p_eSearchOptions |= RegexOptions.IgnoreCase;
    }

    #endregion

    #region Methods

    public IEnumerable<Result> SearchFile(string sSearchFor, string sFileText, string sFileName)
    {
      var lstResults = new List<Result>();
      foreach (Match oMatch in Regex.Matches(sFileText, sSearchFor, p_eSearchOptions))
      {
        lstResults.Add(new Result(oMatch.Value, sFileName));
      }
      return lstResults;
    }

    #endregion
  }
}
