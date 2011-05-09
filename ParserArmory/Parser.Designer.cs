namespace TradArmory
{
    partial class Parser
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Parser));
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAvancement = new System.Windows.Forms.TextBox();
            this.groupBoxAvancement = new System.Windows.Forms.GroupBox();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.buttonLancerDB = new System.Windows.Forms.Button();
            this.buttonLancer = new System.Windows.Forms.Button();
            this.groupBoxTop = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBoxLangue = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxDBMonde = new System.Windows.Forms.TextBox();
            this.textBoxDBPass = new System.Windows.Forms.TextBox();
            this.textBoxDBUsername = new System.Windows.Forms.TextBox();
            this.textBoxDBPort = new System.Windows.Forms.TextBox();
            this.textBoxDBHote = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxIdFin = new System.Windows.Forms.TextBox();
            this.textBoxIdDebut = new System.Windows.Forms.TextBox();
            this.groupBoxAvancement.SuspendLayout();
            this.groupBoxTop.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(385, 451);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Développé par Shyax © 2011";
            // 
            // textBoxAvancement
            // 
            this.textBoxAvancement.AcceptsReturn = true;
            this.textBoxAvancement.Location = new System.Drawing.Point(7, 20);
            this.textBoxAvancement.Multiline = true;
            this.textBoxAvancement.Name = "textBoxAvancement";
            this.textBoxAvancement.ReadOnly = true;
            this.textBoxAvancement.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxAvancement.Size = new System.Drawing.Size(380, 157);
            this.textBoxAvancement.TabIndex = 5;
            // 
            // groupBoxAvancement
            // 
            this.groupBoxAvancement.Controls.Add(this.comboBoxType);
            this.groupBoxAvancement.Controls.Add(this.label9);
            this.groupBoxAvancement.Controls.Add(this.textBoxAvancement);
            this.groupBoxAvancement.Controls.Add(this.progressBar1);
            this.groupBoxAvancement.Controls.Add(this.buttonLancerDB);
            this.groupBoxAvancement.Controls.Add(this.buttonLancer);
            this.groupBoxAvancement.Location = new System.Drawing.Point(18, 212);
            this.groupBoxAvancement.Name = "groupBoxAvancement";
            this.groupBoxAvancement.Size = new System.Drawing.Size(590, 215);
            this.groupBoxAvancement.TabIndex = 4;
            this.groupBoxAvancement.TabStop = false;
            this.groupBoxAvancement.Text = "Avancement";
            // 
            // comboBoxType
            // 
            this.comboBoxType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "Objets",
            "Créatures"});
            this.comboBoxType.Location = new System.Drawing.Point(445, 36);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(72, 21);
            this.comboBoxType.TabIndex = 11;
            this.comboBoxType.Text = "Objets";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(393, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(180, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Choisissez votre type de traduction : ";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 183);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(381, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 4;
            // 
            // buttonLancerDB
            // 
            this.buttonLancerDB.Location = new System.Drawing.Point(397, 182);
            this.buttonLancerDB.Name = "buttonLancerDB";
            this.buttonLancerDB.Size = new System.Drawing.Size(167, 24);
            this.buttonLancerDB.TabIndex = 9;
            this.buttonLancerDB.Text = "Lancer avec la DB";
            this.buttonLancerDB.UseVisualStyleBackColor = true;
            // 
            // buttonLancer
            // 
            this.buttonLancer.Location = new System.Drawing.Point(396, 137);
            this.buttonLancer.Name = "buttonLancer";
            this.buttonLancer.Size = new System.Drawing.Size(167, 24);
            this.buttonLancer.TabIndex = 4;
            this.buttonLancer.Text = "Lancer !";
            this.buttonLancer.UseVisualStyleBackColor = true;
            this.buttonLancer.Click += new System.EventHandler(this.buttonLancer_Click);
            // 
            // groupBoxTop
            // 
            this.groupBoxTop.Controls.Add(this.label12);
            this.groupBoxTop.Controls.Add(this.comboBoxLangue);
            this.groupBoxTop.Controls.Add(this.label10);
            this.groupBoxTop.Controls.Add(this.groupBox1);
            this.groupBoxTop.Controls.Add(this.label2);
            this.groupBoxTop.Controls.Add(this.label1);
            this.groupBoxTop.Controls.Add(this.textBoxIdFin);
            this.groupBoxTop.Controls.Add(this.textBoxIdDebut);
            this.groupBoxTop.Location = new System.Drawing.Point(18, 6);
            this.groupBoxTop.Name = "groupBoxTop";
            this.groupBoxTop.Size = new System.Drawing.Size(590, 196);
            this.groupBoxTop.TabIndex = 3;
            this.groupBoxTop.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoEllipsis = true;
            this.label12.Location = new System.Drawing.Point(9, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(183, 40);
            this.label12.TabIndex = 13;
            this.label12.Text = "Veuillez rentrer l\'ID de début et l\'ID de fin pour pouvoir récupérer les traducti" +
                "ons";
            // 
            // comboBoxLangue
            // 
            this.comboBoxLangue.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxLangue.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxLangue.FormattingEnabled = true;
            this.comboBoxLangue.Items.AddRange(new object[] {
            "Français",
            "Allemand",
            "Espagnol",
            "Russe"});
            this.comboBoxLangue.Location = new System.Drawing.Point(92, 127);
            this.comboBoxLangue.Name = "comboBoxLangue";
            this.comboBoxLangue.Size = new System.Drawing.Size(100, 21);
            this.comboBoxLangue.TabIndex = 12;
            this.comboBoxLangue.Text = "Français";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 130);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Langue :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.textBoxDBMonde);
            this.groupBox1.Controls.Add(this.textBoxDBPass);
            this.groupBox1.Controls.Add(this.textBoxDBUsername);
            this.groupBox1.Controls.Add(this.textBoxDBPort);
            this.groupBox1.Controls.Add(this.textBoxDBHote);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(305, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 172);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informations de connexion MySQL";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(2, 153);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(271, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Ces informations de connexion ne sont pas obligatoires !";
            // 
            // textBoxDBMonde
            // 
            this.textBoxDBMonde.Location = new System.Drawing.Point(140, 121);
            this.textBoxDBMonde.Name = "textBoxDBMonde";
            this.textBoxDBMonde.Size = new System.Drawing.Size(100, 20);
            this.textBoxDBMonde.TabIndex = 19;
            this.textBoxDBMonde.Text = "mangos";
            // 
            // textBoxDBPass
            // 
            this.textBoxDBPass.Location = new System.Drawing.Point(97, 96);
            this.textBoxDBPass.Name = "textBoxDBPass";
            this.textBoxDBPass.Size = new System.Drawing.Size(100, 20);
            this.textBoxDBPass.TabIndex = 18;
            this.textBoxDBPass.Text = "mangos";
            this.textBoxDBPass.UseSystemPasswordChar = true;
            // 
            // textBoxDBUsername
            // 
            this.textBoxDBUsername.Location = new System.Drawing.Point(97, 73);
            this.textBoxDBUsername.Name = "textBoxDBUsername";
            this.textBoxDBUsername.Size = new System.Drawing.Size(100, 20);
            this.textBoxDBUsername.TabIndex = 17;
            this.textBoxDBUsername.Text = "mangos";
            // 
            // textBoxDBPort
            // 
            this.textBoxDBPort.Location = new System.Drawing.Point(100, 48);
            this.textBoxDBPort.Name = "textBoxDBPort";
            this.textBoxDBPort.Size = new System.Drawing.Size(63, 20);
            this.textBoxDBPort.TabIndex = 16;
            this.textBoxDBPort.Text = "3306";
            // 
            // textBoxDBHote
            // 
            this.textBoxDBHote.Location = new System.Drawing.Point(46, 27);
            this.textBoxDBHote.Name = "textBoxDBHote";
            this.textBoxDBHote.Size = new System.Drawing.Size(213, 20);
            this.textBoxDBHote.TabIndex = 15;
            this.textBoxDBHote.Text = "127.0.0.1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(131, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Base de données monde :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Mot de passe :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Nom d\'utilisateur : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Port :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Hôte :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "ID de fin :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "ID de début :";
            // 
            // textBoxIdFin
            // 
            this.textBoxIdFin.Location = new System.Drawing.Point(92, 93);
            this.textBoxIdFin.Name = "textBoxIdFin";
            this.textBoxIdFin.Size = new System.Drawing.Size(100, 20);
            this.textBoxIdFin.TabIndex = 1;
            // 
            // textBoxIdDebut
            // 
            this.textBoxIdDebut.Location = new System.Drawing.Point(92, 65);
            this.textBoxIdDebut.Name = "textBoxIdDebut";
            this.textBoxIdDebut.Size = new System.Drawing.Size(100, 20);
            this.textBoxIdDebut.TabIndex = 0;
            // 
            // Parser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 473);
            this.Controls.Add(this.groupBoxAvancement);
            this.Controls.Add(this.groupBoxTop);
            this.Controls.Add(this.label3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Parser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TradArmory";
            this.groupBoxAvancement.ResumeLayout(false);
            this.groupBoxAvancement.PerformLayout();
            this.groupBoxTop.ResumeLayout(false);
            this.groupBoxTop.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBoxAvancement;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBoxTop;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxDBMonde;
        private System.Windows.Forms.TextBox textBoxDBPass;
        private System.Windows.Forms.TextBox textBoxDBUsername;
        private System.Windows.Forms.TextBox textBoxDBPort;
        private System.Windows.Forms.TextBox textBoxDBHote;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxIdFin;
        private System.Windows.Forms.TextBox textBoxIdDebut;
        private System.Windows.Forms.ComboBox comboBoxLangue;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox textBoxAvancement;
        public System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.Button buttonLancerDB;
        public System.Windows.Forms.Button buttonLancer;
    }
}

