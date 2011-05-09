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
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TradArmory
{
    public partial class Parser : Form
    {
        private string _locale;
        public Parser()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Vérifie si une chaîne est un entier
        /// </summary>
        /// <param name="str">La chaîne à vérifier</param>
        /// <returns>Vrai (true) si la chaîne est un entier ou Faux (false) dans le cas échéant</returns>
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
        /// Récupère le numéro de la locale 
        /// </summary>
        /// <returns></returns>
        private string getLocale()
        {
            switch (comboBoxLangue.SelectedIndex)
            {
                case 0:
                    return "fr";
                case 1:
                    return "de";
                case 2:
                    return "es";
                case 3:
                    return "ru";
                default:
                    return "fr";
            }
        }

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
            else if (comboBoxLangue.Text == string.Empty)
            {
                MessageBox.Show("Vous n'avez pas indiqué de langue !", "Erreur !", MessageBoxButtons.OK);
                return false;
            }
            else if (!comboBoxLangue.Items.Contains(comboBoxLangue.Text))
            {
                MessageBox.Show("Vous n'avez pas indiqué une langue valide !", "Erreur !", MessageBoxButtons.OK);
                return false;
            }
            else if (comboBoxType.Text == string.Empty)
            {
                MessageBox.Show("Vous n'avez pas indiqué un type à traduire !", "Erreur !", MessageBoxButtons.OK);
                return false;
            }
            else if (!comboBoxType.Items.Contains(comboBoxType.Text))
            {
                MessageBox.Show("Vous n'avez pas indiqué un type à traduire valide !", "Erreur !", MessageBoxButtons.OK);
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
                AbstractParser parser;
                switch (comboBoxType.SelectedIndex)
                {
                    case 0:
                        parser = new ItemParser(this, textBoxIdDebut.Text, textBoxIdFin.Text, getLocale());
                        break;
                    case 1:
                        parser = new NPCParser(this, textBoxIdDebut.Text, textBoxIdFin.Text, getLocale());
                        break;
                }
                
            }
        }

        private void buttonLancerDB_Click(object sender, EventArgs e)
        {
            if (VerifierChamps())
            {
                if (VerifierChampsDB())
                {
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
                        return;
                    }

                    AbstractParser parser;
                    switch (comboBoxType.SelectedIndex)
                    {
                        case 0:
                            parser = new ItemParser(this, textBoxIdDebut.Text, textBoxIdFin.Text, getLocale(), connection);
                            break;
                        case 1:
                            parser = new NPCParser(this, textBoxIdDebut.Text, textBoxIdFin.Text, getLocale(), connection);
                            break;
                    }
                }
            }
        }

    }
}
