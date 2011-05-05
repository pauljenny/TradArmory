//TradArmory est un parser écrit en C# avec le .NET Framework 4 Client Profile pour récupérer des informations de traduction depuis l'armurerie officielle ou depuis WoWHead. 
//Il est distribué sous licence GNU GPL    
//Copyright (C) 2011  Shyax

//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.

//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.

//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace ParserArmory
{
    public partial class Parser : Form
    {
        private string _locale;
        public Parser()
        {
            InitializeComponent();
        }

        private bool IsInt(string str)
        {
            try 
            {
                Convert.ToInt32(str);
                return true;
            }
            catch (FormatException e)
            {
                return false;
            }
        }

        /// <summary>
        /// Récupère la locale 
        /// </summary>
        /// <returns></returns>
        private int getLocale()
        {
            switch (_locale)
            {
                case "fr":
                    return (int)ClientLocale.French;
                case "de":
                    return (int)ClientLocale.German;
                case "es":
                    return (int)ClientLocale.Spanish;
                case "ru":
                    return (int)ClientLocale.Russian;
                default:
                    return (int)ClientLocale.French;
            }
        }
    }
}
