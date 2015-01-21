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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextSearch.ViewModels
{
  public class ResultViewModel : ViewModelBase
  {
    #region Constructors

    public ResultViewModel()
    {
      p_sFileName = string.Empty;
      p_sPathFileName = string.Empty;
      p_sText = string.Empty;
      p_fIsSelected = false;
      p_sFilePath = string.Empty;
      OpenFolderCommand = new Commands.DefaultCommand();
      OpenFolderCommand.Executed += OpenFolderCommand_Executed;
    }

    #endregion

    #region Private Variables

    private string p_sFileName;
    private string p_sPathFileName;
    private string p_sText;
    private bool p_fIsSelected;
    private string p_sFilePath;

    #endregion

    #region Properties

    public string FileName
    {
      get
      {
        return p_sFileName;
      }
      set
      {
        p_sFileName = value;
        NotifyPropertyChanged();
      }
    }

    public string PathFileName
    {
      get
      {
        return p_sPathFileName;
      }
      set
      {
        if (p_sPathFileName != value)
        {
          p_sPathFileName = value;
          NotifyPropertyChanged();
          FilePath = System.IO.Path.GetDirectoryName(p_sPathFileName);
          FileName = System.IO.Path.GetFileName(p_sPathFileName);
        }
      }
    }

    public string Text
    {
      get
      {
        return p_sText;
      }
      set
      {
        p_sText = value;
        NotifyPropertyChanged();
      }
    }

    public bool IsSelected
    {
      get
      {
        return p_fIsSelected;
      }
      set
      {
        if (value != p_fIsSelected)
        {
          p_fIsSelected = value;
          NotifyPropertyChanged();
        }
      }
    }

    public string FilePath
    {
      get
      {
        return p_sFilePath;
      }
      set
      {
        if (value != p_sFilePath)
        {
          p_sFilePath = value;
          NotifyPropertyChanged();
        }
      }
    }

    public Commands.DefaultCommand OpenFolderCommand { get; private set; }

    #endregion

    #region "Event Handlers"

    void OpenFolderCommand_Executed(object sender, EventArgs e)
    {
      System.Diagnostics.Process.Start(FilePath);
    }

    #endregion

  }
}
