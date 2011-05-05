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
using System.Linq;
using System.Text;

namespace ParserArmory
{
    public enum ClientLocale
    {
        /// <summary>
        /// Any english locale
        /// </summary>
        English = 0,

        /// <summary>
        /// Korean
        /// </summary>
        Korean = 1,

        /// <summary>
        /// French
        /// </summary>
        French = 2,

        /// <summary>
        /// German
        /// </summary>
        German = 3,

        /// <summary>
        /// Simplified Chinese
        /// </summary>
        ChineseSimplified = 4,

        /// <summary>
        /// Traditional Chinese
        /// </summary>
        ChineseTraditional = 5,

        /// <summary>
        /// Spanish.
        /// </summary>
        Spanish = 6,

        /// <summary>
        /// Mexico
        /// </summary>
        Mexico = 7,

        /// <summary>
        /// Russian
        /// </summary>
        Russian = 8,

        End
    }
}
