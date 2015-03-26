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
using System.Linq;

namespace FullTextSearch.Models
{
  public class LiteralHunter : Interface.IHunter
  {
    #region Methods

    public IEnumerable<Result> SearchFile(string sSearchFor, string sFileText, string sFileName)
    {
      if (sFileText.Contains(sSearchFor))
      {
        string[] asFileText = sFileText.Split('\n');

        //Finds all matches and adds them to the results list
        return asFileText.Where(s => (s.Contains(sSearchFor)))
                         .Select((sResult) => new Result(sResult.Trim(), sFileName));
      }
      return new List<Result>();
    }

    #endregion
  }
}
