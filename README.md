# FullTextSearch: A tool for searching through the text of large amounts of files
==============

* **Overview**
  - FullTextSearch can be used to find specific files that have certain lines of text in them
  - It is useful when you are working on large projects
    - You might know something you need to change
    - You might not know where it is
  - You can use both normal text, or regular expressions to find what you are looking for
  - It might not be very useful if you don't have very many files to search through
  
* **Example usage**
  - You find a bug in a project where a message is misspelled
    - Browse for the project's base folder
    - Fill in the file type (i.e. *.cs)
    - Type in the misspelling in the search text
    - The tool will do the rest
    - You can make basic edits to the file in the tool itself, or have it open the file in the default program for it's file type

* **Getting Started**
  - There currently is no installation, but that should be added shortly
  - Just run the program and you should be good to go
  
* **Detailed Usage**
  - The main goal of this project is to allow intelligent searching of text using multiline regular expressions
    - This can allow you to find very specific pieces of information in a vast amount of files
    - It can take quite a while to find all of the files with very complicated expressions though
  - The file extensions must fit the criteria for the function `System.IO.Directory.GetFiles(String, String, SearchOption)`
    - This function is documented [here](http://msdn.microsoft.com/en-us/library/ms143316(v=vs.110).aspx)
  - The regular expressions accepted are from on the .NET framework's regular expressions
    - There is documentation for them [here](http://msdn.microsoft.com/en-us/library/az24scfc(v=vs.110).aspx)

* **Developer info**
  - The tool uses the MVVM model using C#
  - There is an MSTest project for the primary search function, though I'd like to get tests setup for everything other than the Views
  - If you would like to contribute to this project, pull requests are accepted
  
* **License**
FullTextSearch uses the GNU GPLv3 license.  You can get a copy of the license [here](http://www.gnu.org/licenses/gpl.txt)
