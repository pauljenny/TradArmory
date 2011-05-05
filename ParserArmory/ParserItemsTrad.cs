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
using MySql.Data.MySqlClient;

namespace ParserArmory
{
    public struct Item
    {
        public int Id;
        public string Name;
        public string Description;
    }

    /// <summary>
    /// Gère le parsage des items depuis l'armurerie officielle 
    /// </summary>
    public partial class Parser
    {
        private List<Item> _items;
        private List<int> _idsToExclude;

        /// <summary>
        /// Vérifie les champs d'ID
        /// </summary>
        /// <returns>True lorsque les champs sont corrects ou False le cas échéant</returns>
        private bool VerifierChamps()
        {
            if (textBoxIdDebut.Text == string.Empty || textBoxIdFin.Text == string.Empty) // On vérifie que les champs ne sont pas vides
            {
                MessageBox.Show("Vous n'avez pas indiqué d'ID de début et/ou de fin !", "Erreur !", MessageBoxButtons.OK);
                return false;
            }
            else if (!IsInt(textBoxIdDebut.Text) || !IsInt(textBoxIdFin.Text)) // On vérifie que c'est bien des entiers 
            {
                MessageBox.Show("Vous n'avez pas entré des chiffres pour les ID !", "Erreur !", MessageBoxButtons.OK);
                return false;
            }
            else if (Convert.ToInt32(textBoxIdDebut.Text) > Convert.ToInt32(textBoxIdFin.Text)) // On vérifie que l'id de début est inférieur à l'id de fin
            {
                MessageBox.Show("L'ID de début est supérieure à l'ID de fin !", "Erreur !", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Vérifie les champs de connexion à la base de données
        /// </summary>
        /// <returns>True lorsque les champs sont corrects ou False le cas échéant</returns>
        private bool VerifierChampsDB()
        {
            if (textBoxDBHote.Text == string.Empty || textBoxDBMonde.Text == string.Empty || textBoxDBPass.Text == string.Empty
                || textBoxDBPort.Text == string.Empty || textBoxDBUsername.Text == string.Empty) // On vérifie chaque champ...
            {
                MessageBox.Show("Vous n'avez pas rempli tous les champs demandés pour la connexion à la base de données !", "Erreur !", MessageBoxButtons.OK);
                return false;
            }
            else if (!IsInt(textBoxDBPort.Text)) // On vérifie que le numéro de port est bien un entier
            {
                MessageBox.Show("Vous n'avez pas entré un numéro de port valide !", "Erreur !", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void buttonLancer_Click(object sender, EventArgs e)
        {
            if (VerifierChamps())
            {
                buttonLancer.Enabled = false;
                buttonLancerDB.Enabled = false;
                if (radioButtonFR.Checked)
                    _locale = Locale.localeDictionary[ClientLocale.French];
                else if (radioButtonDE.Checked)
                    _locale = Locale.localeDictionary[ClientLocale.German];
                else if (radioButtonES.Checked)
                    _locale = Locale.localeDictionary[ClientLocale.Spanish];
                else
                    _locale = Locale.localeDictionary[ClientLocale.Russian];

                LancerParser(false);
            }
        }

        private void buttonLancerDB_Click(object sender, EventArgs e)
        {
            if (VerifierChamps())
            {
                if (VerifierChampsDB())
                {
                    buttonLancerDB.Enabled = false;
                    buttonLancer.Enabled = false;
                    if (radioButtonFR.Checked)
                        _locale = Locale.localeDictionary[ClientLocale.French];
                    else if (radioButtonDE.Checked)
                        _locale = Locale.localeDictionary[ClientLocale.German];
                    else if (radioButtonES.Checked)
                        _locale = Locale.localeDictionary[ClientLocale.Spanish];
                    else
                        _locale = Locale.localeDictionary[ClientLocale.Russian];

                    string connexionString = "Server = " + textBoxDBHote.Text + "; Port = " + textBoxDBPort.Text + "; Database = " + textBoxDBMonde.Text + "; Uid = " + textBoxDBUsername.Text + "; Pwd = " + textBoxDBPass.Text;
                    // On lance la connexion au serveur MySQL
                    MySqlConnection connection = new MySqlConnection(connexionString);
                    try
                    { 
                        connection.Open();
                    }
                    catch (Exception mysqlEx)
                    {
                        MessageBox.Show("Erreur lors de la connexion à la base de données " + Environment.NewLine + mysqlEx.Message, "Erreur !");
                        buttonLancerDB.Enabled = true;
                        return;
                    }
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT `entry` FROM `locales_item` WHERE `entry` IN (SELECT `entry` FROM `item_template` WHERE `entry` >= "+textBoxIdDebut.Text+" AND `entry` <= "+textBoxIdFin.Text+") AND `name_loc"+getLocale().ToString()+"` <> '';";
                    try
                    {
                        MySqlDataReader reader = command.ExecuteReader();
                        _idsToExclude = new List<int>();
                        while (reader.Read())
                        {
                            _idsToExclude.Add(reader.GetInt32(0));
                        }
                    }
                    catch (Exception ex)
                    {
                        return;
                    }

                    LancerParser(true);
                    buttonLancerDB.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Lance le parser
        /// </summary>
        /// <param name="exclude">Vérifie ou non si il y a des IDs à exclure</param>
        private void LancerParser(bool exclude)
        {
            DateTime debut = DateTime.Now;
            textBoxAvancement.ResetText();
            textBoxAvancement.Text = Environment.NewLine + "Lancement du parser de traduction des créatures depuis l'armurerie officielle de l'ID " + textBoxIdDebut.Text + " à " + textBoxIdFin.Text;
            progressBar1.Minimum = Convert.ToInt32(textBoxIdDebut.Text);
            progressBar1.Maximum = Convert.ToInt32(textBoxIdFin.Text);
            _items = new List<Item>();
            for (int id = Convert.ToInt32(textBoxIdDebut.Text); id <= Convert.ToInt32(textBoxIdFin.Text); id++)
            {
                if (exclude && _idsToExclude != null && _idsToExclude.Contains(id))
                {
                    textBoxAvancement.AppendText(Environment.NewLine + "Objet " + id + " déjà traduit...");
                    continue;
                }
                else
                {
                    progressBar1.Value = id;
                    try
                    {
                        XmlReader reader = XmlReader.Create("http://eu.battle.net/wow/" + _locale + "/item/" + id.ToString());
                        while (!reader.Value.Contains("subheader color-q"))
                        {
                            reader.ReadToFollowing("h3");
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
                            textBoxAvancement.AppendText(Environment.NewLine + "Objet " + id.ToString() + " non traduit !");
                        }
                        else
                        {
                            Item obj = new Item();
                            obj.Id = id;
                            obj.Name = nameItem;
                            textBoxAvancement.AppendText(Environment.NewLine + "Objet :" + id.ToString() + " Nom : " + nameItem);
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
                                textBoxAvancement.AppendText(" Description : " + descItem);
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
                }
            }
            DateTime fin = DateTime.Now;
            TimeSpan span = fin - debut;
            textBoxAvancement.AppendText(Environment.NewLine + "Récupération de la traduction de " + _items.Count().ToString() + " objet(s) terminée en " + span.Hours.ToString() + ":" + span.Minutes.ToString() + ":" + span.Seconds.ToString() + " !");
            EcrireSQL();
        }

        /// <summary>
        /// Ecrit le fichier SQL en récupérant les objets situés dans le tableau _items
        /// </summary>
        private void EcrireSQL()
        {
            string filePath = Path.Combine(Application.StartupPath, "tradItems" + _locale + ".sql");
            using (StreamWriter stream = new StreamWriter(filePath, true, Encoding.UTF8))
            {
                string utf8 = "SET NAMES `utf8`;";
                stream.WriteLine(utf8);
                string insert = "INSERT INTO `locales_item` (`entry`,`name_loc" + getLocale() + "`,`description_loc" + getLocale() + "`) VALUES";
                stream.WriteLine(insert);
                foreach (Item item in _items)
                {
                    if (item.Id != 0)
                    {
                        if (item.Id == _items.Last().Id)
                            stream.WriteLine("('" + item.Id.ToString() + "', '" + item.Name + "', '" + item.Description + "');");
                        else
                            stream.WriteLine("('" + item.Id.ToString() + "', '" + item.Name + "', '" + item.Description + "'),");
                    }
                }
                stream.Close();
                buttonLancer.Enabled = true;
                MessageBox.Show("Votre récupération des traductions s'est bien effectuée ! Un fichier nommé tradItems" + _locale + ".sql a été crée !", "Récupération terminée !");
            }
        }
    }
}
