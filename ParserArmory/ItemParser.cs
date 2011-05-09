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
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using MySql.Data.MySqlClient;

namespace TradArmory
{
    public struct Item
    {
        public int Id;
        public string Nom;
        public string Description;
    };

    public class ItemParser : AbstractParser
    {
        private List<Item> _items;

        public ItemParser(Parser form, string idDeb, string idFin, string locale)
            : base (form, idDeb, idFin, locale)
        {
        }

        public ItemParser(Parser form, string idDeb, string idFin, string locale, MySqlConnection connect)
            : base(form, idDeb, idFin, locale, connect)
        {
        }

        protected override void InitialiserDB()
        {
            MySqlCommand command = _connexion.CreateCommand();
            command.CommandText = "SELECT `entry` FROM `locales_item` WHERE `entry` IN (SELECT `entry` FROM `item_template` WHERE `entry` >= "+_idDebut.ToString()+" AND `entry` <= "+_idFin.ToString()+") AND `name_loc"+getNumLocale(_locale).ToString()+"` <> '';";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                _idsToExclude.Add(reader.GetInt32(0));
            }
        }

        public override void LancerParser()
        {
            _items = new List<Item>();
            _formulaire.progressBar1.Minimum = _idDebut;
            _formulaire.progressBar1.Maximum = _idFin;
            _formulaire.buttonLancer.Enabled = false;
            _formulaire.buttonLancerDB.Enabled = false;

            DateTime debut = DateTime.Now;
            _formulaire.textBoxAvancement.AppendText("Lancement du parser de traduction des objets");
            for (int id = _idDebut; id <= _idFin; id++)
            {
                _formulaire.progressBar1.Value = id;
                if (_exclure && _items != null && _idsToExclude.Contains(id))
                {
                    _formulaire.textBoxAvancement.AppendText(Environment.NewLine + "Objet " + id.ToString() + " déjà traduit...");
                }
                else
                {
                    try
                    {
                        XmlReader reader = XmlReader.Create("http://eu.battle.net/wow/" + _locale + "/item/" + id.ToString());
                        while (!reader.Value.Contains("color-q"))
                        {
                            reader.ReadToFollowing("h2");
                            reader.MoveToAttribute("class");
                            if (reader.EOF)
                            {
                                break;
                            }
                        }
                        reader.MoveToElement();
                        string nameItem = reader.ReadElementContentAsString().Trim().Replace("\u0027", "\u005c\u0027");
                        if (nameItem.Contains("[Monster]")) // Les noms d'objets placés entre crochets ne sont pas traduits
                        {
                            _formulaire.textBoxAvancement.AppendText(Environment.NewLine + "Objet " + id.ToString() + " non traduit !");
                        }
                        else
                        {
                            Item obj = new Item();
                            obj.Id = id;
                            obj.Nom = nameItem;
                            _formulaire.textBoxAvancement.AppendText(Environment.NewLine + "Objet :" + id.ToString() + " Nom : " + nameItem);
                            reader.ReadToFollowing("li");
                            while (!reader.Value.Contains("color-tooltip-yellow"))
                            {
                                reader.ReadToFollowing("li");
                                reader.MoveToAttribute("class");
                                if (reader.EOF)
                                    break;
                            }
                            if (!reader.EOF)
                            {
                                reader.MoveToElement();
                                string descItem = reader.ReadElementContentAsString().Trim().Replace("\u0027", "\u005c\u0027").Replace("\u0022", string.Empty);
                                _formulaire.textBoxAvancement.AppendText(" Description : " + descItem);
                                obj.Description = descItem;
                            }
                            _items.Add(obj);
                        }

                        reader.Close();
                    }
                    catch (WebException webEx)
                    {
                        // On a une ID inconnue
                        continue;
                    }
                    catch (Exception readEx)
                    {
                        throw readEx;
                    }
                }
            }
            DateTime fin = DateTime.Now;
            TimeSpan span = fin - debut;
            _formulaire.textBoxAvancement.AppendText(Environment.NewLine + "Traduction terminée de " + _items.Count + " objets en "+ span.Hours+":"+span.Minutes+":"+span.Seconds);
            EcrireSQL();
        }

        protected override void EcrireSQL()
        {
            string filePath = Path.Combine(Application.StartupPath, "tradItems" + _locale + ".sql");
            using (StreamWriter stream = new StreamWriter(filePath, true, Encoding.UTF8))
            {
                string utf8 = "SET NAMES `utf8`;";
                stream.WriteLine(utf8);
                string insert = "INSERT INTO `locales_item` (`entry`,`name_loc" + getNumLocale(_locale) + "`,`description_loc" + getNumLocale(_locale) + "`) VALUES";
                stream.WriteLine(insert);
                foreach (Item item in _items)
                {
                    if (item.Id != 0)
                    {
                        if (item.Id == _items.Last().Id)
                            stream.WriteLine("('" + item.Id.ToString() + "', '" + item.Nom + "', '" + item.Description + "');");
                        else
                            stream.WriteLine("('" + item.Id.ToString() + "', '" + item.Nom + "', '" + item.Description + "'),");
                    }
                }
                stream.Close();
                _formulaire.buttonLancer.Enabled = true;
                _formulaire.buttonLancerDB.Enabled = true;
                MessageBox.Show("Votre récupération des traductions s'est bien effectuée ! Un fichier nommé tradItems" + _locale + ".sql a été crée !", "Récupération terminée !");
            }
        }

    }
}
