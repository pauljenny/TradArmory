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
