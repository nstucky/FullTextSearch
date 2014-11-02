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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FullTextSearch.ViewModels
{
  public class AboutViewModel : ViewModelBase
  {
    #region Constants

    private const string LICENSE_INFO = @"This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>";

    #endregion

    #region Private Variables

    string p_sTitle;
    string p_sVersion;
    string p_sCopyright;
    string p_sCompanyName;
    string p_sDescription;
    string p_sLicense;
    
    #endregion

    #region Properties

    public string Title
    {
      get
      {
        return p_sTitle;
      }
      set
      {
        p_sTitle = value;
        NotifyPropertyChanged();
      }
    }

    public string Version
    {
      get
      {
        return p_sVersion;
      }
      set
      {
        p_sVersion = value;
        NotifyPropertyChanged();
      }
    }

    public string Copyright
    {
      get
      {
        return p_sCopyright;
      }
      set
      {
        p_sCopyright = value;
        NotifyPropertyChanged();
      }
    }

    public string CompanyName
    {
      get
      {
        return p_sCompanyName;
      }
      set
      {
        p_sCompanyName = value;
        NotifyPropertyChanged();
      }
    }

    public string Description
    {
      get
      {
        return p_sDescription;
      }
      set
      {
        p_sDescription = value;
        NotifyPropertyChanged();
      }
    }

    public string License
    {
      get
      {
        return p_sLicense;
      }
      set
      {
        p_sLicense = value;
        NotifyPropertyChanged();
      }
    }

    #endregion

    #region Constructors

    public AboutViewModel()
    {
      foreach (Attribute oAttribute in Assembly.GetExecutingAssembly().GetCustomAttributes())
      {
        if (oAttribute is AssemblyTitleAttribute)
          Title = ((AssemblyTitleAttribute)oAttribute).Title;
        if (oAttribute is AssemblyCopyrightAttribute)
          Copyright = ((AssemblyCopyrightAttribute)oAttribute).Copyright;
        if (oAttribute is AssemblyCompanyAttribute)
          CompanyName = ((AssemblyCompanyAttribute)oAttribute).Company;
        if (oAttribute is AssemblyDescriptionAttribute)
          Description = ((AssemblyDescriptionAttribute)oAttribute).Description;
      }
      Version = string.Format("Version {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
      License = LICENSE_INFO;
    }

    #endregion
  }
}
