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
        private Item[] _items;
        private void buttonLancer_Click(object sender, EventArgs e)
        {
            if (textBoxIdDebut.Text == string.Empty || textBoxIdFin.Text == string.Empty) // On vérifie que les champs ne sont pas vides
            {
                MessageBox.Show("Vous n'avez pas indiqué d'ID de début et/ou de fin !", "Erreur !", MessageBoxButtons.OK);
                return;
            }
            else if (!IsInt(textBoxIdDebut.Text) || !IsInt(textBoxIdFin.Text)) // On vérifie que c'est bien des entiers 
            {
                MessageBox.Show("Vous n'avez pas entré des chiffres pour les ID !", "Erreur !", MessageBoxButtons.OK);
                return;
            }
            else if (Convert.ToInt32(textBoxIdDebut.Text) > Convert.ToInt32(textBoxIdFin.Text)) // On vérifie que l'id de début est inférieur à l'id de fin
            {
                MessageBox.Show("L'ID de début est supérieure à l'ID de fin !", "Erreur !", MessageBoxButtons.OK);
                return;
            }
            else
            {
                DateTime debut = DateTime.Now;
                buttonLancer.Enabled = false;
                if (radioButtonFR.Checked)
                    _locale = Locale.localeDictionary[ClientLocale.French];
                else if (radioButtonDE.Checked)
                    _locale = Locale.localeDictionary[ClientLocale.German];
                else if (radioButtonES.Checked)
                    _locale = Locale.localeDictionary[ClientLocale.Spanish];
                else
                    _locale = Locale.localeDictionary[ClientLocale.Russian];

                textBoxAvancement.ResetText();
                textBoxAvancement.Text = Environment.NewLine + "Lancement du parser de traduction des créatures depuis l'armurerie officielle de l'ID " + textBoxIdDebut.Text + " à " + textBoxIdFin.Text;
                progressBar1.Minimum = Convert.ToInt32(textBoxIdDebut.Text);
                progressBar1.Maximum = Convert.ToInt32(textBoxIdFin.Text);
                _items = new Item[Convert.ToInt32(textBoxIdFin.Text) + 1];
                for (int id = Convert.ToInt32(textBoxIdDebut.Text); id <= Convert.ToInt32(textBoxIdFin.Text); id++)
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
                            _items[id].Id = id;
                            _items[id].Name = nameItem;
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
                                _items[id].Description = descItem;
                            }
                        }
                        reader.Close();
                    }
                    catch (WebException webEx)
                    {
                        // On a une ID inconnue
                        continue;
                    }
                }
                DateTime fin = DateTime.Now;
                TimeSpan span = fin - debut;
                textBoxAvancement.AppendText(Environment.NewLine + "Récupération de la traduction de "+_items.Count(obj => obj.Id != 0).ToString()+" objet(s) terminée en "+ span.Hours.ToString()+":"+span.Minutes.ToString()+":"+ span.Seconds.ToString()+" !");
                EcrireSQL();
            }
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
                        if (item.Id == _items.Last(obj => obj.Id != 0).Id)
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
