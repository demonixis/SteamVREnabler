namespace SteamVRSwitcher
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.LabelToggleStatus = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.LabelSteamVRStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Toggle";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnToggleButtonClick);
            // 
            // LabelToggleStatus
            // 
            this.LabelToggleStatus.AutoSize = true;
            this.LabelToggleStatus.Location = new System.Drawing.Point(106, 17);
            this.LabelToggleStatus.Name = "LabelToggleStatus";
            this.LabelToggleStatus.Size = new System.Drawing.Size(46, 13);
            this.LabelToggleStatus.TabIndex = 1;
            this.LabelToggleStatus.Text = "Enabled";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Kill";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OnKillButtonClick);
            // 
            // LabelSteamVRStatus
            // 
            this.LabelSteamVRStatus.AutoSize = true;
            this.LabelSteamVRStatus.Location = new System.Drawing.Point(106, 46);
            this.LabelSteamVRStatus.Name = "LabelSteamVRStatus";
            this.LabelSteamVRStatus.Size = new System.Drawing.Size(46, 13);
            this.LabelSteamVRStatus.TabIndex = 3;
            this.LabelSteamVRStatus.Text = "Enabled";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(164, 76);
            this.Controls.Add(this.LabelSteamVRStatus);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.LabelToggleStatus);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "SteamVR Enabler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label LabelToggleStatus;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label LabelSteamVRStatus;
    }
}

