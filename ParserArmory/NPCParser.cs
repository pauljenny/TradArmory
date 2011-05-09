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
using HtmlAgilityPack;
using MySql.Data.MySqlClient;

namespace TradArmory
{
    public struct NPC
    {
        public int Id;
        public string Nom;
        public string Subname;
    };

    public class NPCParser : AbstractParser
    {
        private List<NPC> _npcs;

        public NPCParser(Parser form, string idDeb, string idFin, string locale)
            : base (form, idDeb, idFin, locale)
        {
        }

        public NPCParser(Parser form, string idDeb, string idFin, string locale, MySqlConnection connect)
            : base(form, idDeb, idFin, locale, connect)
        {
        }

        protected override void InitialiserDB()
        {
            MySqlCommand command = _connexion.CreateCommand();
            command.CommandText = "SELECT `entry` FROM `locales_creature` WHERE `entry` IN (SELECT `entry` FROM `creature_template` WHERE `entry` >= " + _idDebut.ToString() + " AND `entry` <= " + _idFin.ToString() + ") AND `name_loc" + getNumLocale(_locale).ToString() + "` <> '';";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                _idsToExclude.Add(reader.GetInt32(0));
            }
        }
        protected override void EcrireSQL()
        {
            string filePath = Path.Combine(Application.StartupPath, "tradNPCs" + _locale + ".sql");
            using (StreamWriter stream = new StreamWriter(filePath, true, Encoding.UTF8))
            {
                string utf8 = "SET NAMES `utf8`;";
                stream.WriteLine(utf8);
                string insert = "INSERT INTO `locales_creature` (`entry`,`name_loc" + getNumLocale(_locale) + "`,`subname_loc" + getNumLocale(_locale) + "`) VALUES";
                stream.WriteLine(insert);
                foreach (NPC npc in _npcs)
                {
                    if (npc.Id != 0)
                    {
                        if (npc.Id == _npcs.Last().Id)
                            stream.WriteLine("('" + npc.Id.ToString() + "', '" + npc.Nom + "', '" + npc.Subname + "');");
                        else
                            stream.WriteLine("('" + npc.Id.ToString() + "', '" + npc.Nom + "', '" + npc.Subname + "'),");
                    }
                }
                stream.Close();
                _formulaire.buttonLancer.Enabled = true;
                _formulaire.buttonLancerDB.Enabled = true;
                MessageBox.Show("Votre récupération des traductions s'est bien effectuée ! Un fichier nommé tradNPCs" + _locale + ".sql a été crée !", "Récupération terminée !");
            }
        }

        public override void LancerParser()
        {
             _npcs = new List<NPC>();
            _formulaire.progressBar1.Minimum = _idDebut;
            _formulaire.progressBar1.Maximum = _idFin;
            _formulaire.buttonLancer.Enabled = false;
            _formulaire.buttonLancerDB.Enabled = false;

            DateTime debut = DateTime.Now;
            _formulaire.textBoxAvancement.AppendText("Lancement du parser de traduction des créatures");
            for (int id = _idDebut; id <= _idFin; id++)
            {
                _formulaire.progressBar1.Value = id;
                if (_exclure && _npcs != null && _idsToExclude.Contains(id))
                {
                    _formulaire.textBoxAvancement.AppendText(Environment.NewLine + "Créature " + id.ToString() + " déjà traduite...");
                }
                else
                {
                    try
                    {
                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                        doc.Load(WebRequest.Create("http://" + _locale + ".wowhead.com/npc=" + id.ToString()).GetResponse().GetResponseStream(), Encoding.UTF8);
                        HtmlNode node = doc.DocumentNode.SelectSingleNode("//div[@class='text']/descendant::h1");
                        string text = node.InnerText;
                        if (text.Contains("["))
                        {
                            _formulaire.textBoxAvancement.AppendText(Environment.NewLine + " Créature " + id.ToString() + " non traduite");
                        }
                        else
                        {
                            string[] separateur = new string[1] { "&lt;" };
                            string[] splits = text.Split(separateur, StringSplitOptions.RemoveEmptyEntries);
                            NPC obj = new NPC();
                            obj.Id = id;
                            obj.Nom = splits[0].Replace("\u0027", "\u005c\u0027");
                            _formulaire.textBoxAvancement.AppendText(Environment.NewLine + "Créature : " + id.ToString() + " Nom : " + splits[0]);
                            if (splits.Length > 1)
                            {
                                string subname = splits[1].Replace("/u003c", string.Empty).Replace("/u003e", string.Empty).Replace("&nbsp;", " ").Replace("&gt;", string.Empty).Replace("\u0027", "\u005c\u0027").Trim();
                                obj.Subname = subname;
                                _formulaire.textBoxAvancement.AppendText(" Description : " + subname);
                            }
                            _npcs.Add(obj);
                        }
                    }
                    catch (WebException webEx)
                    {
                        continue;
                    }
                }
            }
            DateTime fin = DateTime.Now;
            TimeSpan span = fin - debut;
            _formulaire.textBoxAvancement.AppendText(Environment.NewLine + "Traduction de " + _npcs.Count + " créatures en " + span.Hours + ":" + span.Minutes + ":" + span.Seconds);
            EcrireSQL();
        }
    }
}
