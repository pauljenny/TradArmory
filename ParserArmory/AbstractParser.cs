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
using MySql.Data.MySqlClient;

namespace TradArmory
{
    public abstract class AbstractParser
    {
        /// <summary>
        /// Liste contenant toutes les ID des items déjà traduits existants dans la DB
        /// </summary>
        protected List<int> _idsToExclude;

        protected Parser _formulaire;
        protected int _idDebut, _idFin;

        protected string _locale;

        protected MySqlConnection _connexion;
        protected bool _exclure;

        public AbstractParser(Parser form, string idDeb, string idFin, string locale)
        {
            _formulaire = form;
            _idDebut = Convert.ToInt32(idDeb);
            _idFin = Convert.ToInt32(idFin);
            _locale = locale;
            _idsToExclude = new List<int>();
            _exclure = false;
            LancerParser();
        }

        public AbstractParser(Parser form, string idDeb, string idFin, string locale, MySqlConnection connect)
        {
            _formulaire = form;
            _idDebut = Convert.ToInt32(idDeb);
            _idFin = Convert.ToInt32(idFin);
            _locale = locale;
            _idsToExclude = new List<int>();
            _connexion = connect;
            _exclure = true;
            InitialiserDB();
            LancerParser();
        }

        protected abstract void EcrireSQL();

        protected abstract void InitialiserDB();

        public abstract void LancerParser();

        protected int getNumLocale(string loc)
        {
            switch (loc)
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
