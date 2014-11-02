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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace FullTextSearch.MSTest
{
  [TestClass]
  public class FileSearchTests
  {
    [TestMethod]
    public void FindAllFiles_NoRegexMatchCase_FindsSpecifiedTextAndReturnFullLine()
    {
      //arrange
      string sDirectory = Environment.CurrentDirectory + "\\TestFiles";
      const string sFileExtensions = "*.txt";
      const string sTextToFind = "Awesome Text";
      List<FullTextSearch.Models.Result> oExpectedResults = new List<FullTextSearch.Models.Result>
      {
        new FullTextSearch.Models.Result(){
          Text = "FIND THIS TEXT HERE -> Awesome Text <- END OF TEXT TO FIND",
          File = sDirectory + "\\BasicTXT.txt"
        },
        new FullTextSearch.Models.Result(){
          Text = "FIND ME TOO -> Awesome Text <- END OF TEXT TO FIND 2",
          File = sDirectory + "\\BasicTXT.txt"
        },
        new FullTextSearch.Models.Result(){
          Text = "The test should be able to find the Awesome Text that's in this sentence.",
          File = sDirectory + "\\NestedFolder\\BasicTXT2.txt"
        }  
      };

      //act
      List<FullTextSearch.Models.Result> oActualResults = FullTextSearch.Models.FileSearch.FindAllFiles(sDirectory, sFileExtensions, sTextToFind, false, true, new BackgroundWorker() { WorkerReportsProgress = true });

      //assert
      Assert.AreEqual(string.Join(",", oExpectedResults.Select(r => r.ToString())), 
                      string.Join(",", oActualResults.Select(r => r.ToString())));
    }

    [TestMethod]
    public void FindAllFiles_NoRegexIgnoreCase_FindsSpecifiedTextAndReturnFullLine()
    {
      //arrange
      string sDirectory = Environment.CurrentDirectory + "\\TestFiles";
      const string sFileExtensions = "*.txt";
      const string sTextToFind = "Awesome Text";
      List<FullTextSearch.Models.Result> oExpectedResults = new List<FullTextSearch.Models.Result>
      {
        new FullTextSearch.Models.Result(){
          Text = "FIND THIS TEXT HERE -> Awesome Text <- END OF TEXT TO FIND",
          File = sDirectory + "\\BasicTXT.txt"
        },
        new FullTextSearch.Models.Result(){
          Text = "FIND ME TOO -> Awesome Text <- END OF TEXT TO FIND 2",
          File = sDirectory + "\\BasicTXT.txt"
        },
        new FullTextSearch.Models.Result(){
          Text = "The test should be able to find the Awesome Text that's in this sentence.",
          File = sDirectory + "\\NestedFolder\\BasicTXT2.txt"
        },
        new FullTextSearch.Models.Result(){
          Text = "Only case-insensitive tests will find this awesome text as well.",
          File = sDirectory + "\\NestedFolder\\BasicTXT2.txt"
        }
      };

      //act
      List<FullTextSearch.Models.Result> oActualResults = FullTextSearch.Models.FileSearch.FindAllFiles(sDirectory, sFileExtensions, sTextToFind, false, false, new BackgroundWorker() { WorkerReportsProgress = true });

      //assert
      Assert.AreEqual(string.Join(",", oExpectedResults.Select(r => r.ToString())),
                      string.Join(",", oActualResults.Select(r => r.ToString())));
    }

    [TestMethod]
    public void FindAllFiles_UseRegex_FindsMatchingPattern()
    {
      //arrange
      string sDirectory = Environment.CurrentDirectory + "\\TestFiles";
      const string sFileExtensions = "*.txt";
      const string sTextToFind = @"->\s.+\s<-";
      List<FullTextSearch.Models.Result> oExpectedResults = new List<FullTextSearch.Models.Result>
      {
        new FullTextSearch.Models.Result(){
          Text = "-> Awesome Text <-",
          File = sDirectory + "\\BasicTXT.txt"
        },
        new FullTextSearch.Models.Result(){
          Text = "-> Awesome Text <-",
          File = sDirectory + "\\BasicTXT.txt"
        }
      };

      //act
      List<FullTextSearch.Models.Result> oActualResults = FullTextSearch.Models.FileSearch.FindAllFiles(sDirectory, sFileExtensions, sTextToFind, true, true, new BackgroundWorker() { WorkerReportsProgress = true });

      //assert
      Assert.AreEqual(string.Join(",", oExpectedResults.Select(r => r.ToString())),
                      string.Join(",", oActualResults.Select(r => r.ToString())));
    }

    [TestMethod]
    public void FindAllFiles_UseRegexMatchCase_FindsMatchingPattern()
    {
      //arrange
      string sDirectory = Environment.CurrentDirectory + "\\TestFiles";
      const string sFileExtensions = "*.txt";
      const string sTextToFind = @"Awesome\s.+?\s";
      List<FullTextSearch.Models.Result> oExpectedResults = new List<FullTextSearch.Models.Result>
      {
        new FullTextSearch.Models.Result(){
          Text = "Awesome Text ",
          File = sDirectory + "\\BasicTXT.txt"
        },
        new FullTextSearch.Models.Result(){
          Text = "Awesome Text ",
          File = sDirectory + "\\BasicTXT.txt"
        },
        new FullTextSearch.Models.Result(){
          Text = "Awesome Text ",
          File = sDirectory + "\\NestedFolder\\BasicTXT2.txt"
        }
      };

      //act
      List<FullTextSearch.Models.Result> oActualResults = FullTextSearch.Models.FileSearch.FindAllFiles(sDirectory, sFileExtensions, sTextToFind, true, true, new BackgroundWorker() { WorkerReportsProgress = true});

      //assert
      Assert.AreEqual(string.Join(",", oExpectedResults.Select(r => r.ToString())),
                      string.Join(",", oActualResults.Select(r => r.ToString())));
    }

    [TestMethod]
    public void FindAllFiles_UseRegexIgnoreCase_FindsMatchingPattern()
    {
      //arrange
      string sDirectory = Environment.CurrentDirectory + "\\TestFiles";
      const string sFileExtensions = "*.txt";
      const string sTextToFind = @"awesome\s.+?\s";
      List<FullTextSearch.Models.Result> oExpectedResults = new List<FullTextSearch.Models.Result>
      {
        new FullTextSearch.Models.Result(){
          Text = "Awesome Text ",
          File = sDirectory + "\\BasicTXT.txt"
        },
        new FullTextSearch.Models.Result(){
          Text = "Awesome Text ",
          File = sDirectory + "\\BasicTXT.txt"
        },
        new FullTextSearch.Models.Result(){
          Text = "Awesome Text ",
          File = sDirectory + "\\NestedFolder\\BasicTXT2.txt"
        },
        new FullTextSearch.Models.Result(){
          Text = "awesome text ",
          File = sDirectory + "\\NestedFolder\\BasicTXT2.txt"
        }
      };

      //act
      List<FullTextSearch.Models.Result> oActualResults = FullTextSearch.Models.FileSearch.FindAllFiles(sDirectory, sFileExtensions, sTextToFind, true, false, new BackgroundWorker() { WorkerReportsProgress = true });

      //assert
      Assert.AreEqual(string.Join(",", oExpectedResults.Select(r => r.ToString())),
                      string.Join(",", oActualResults.Select(r => r.ToString())));
    }
  }
}
