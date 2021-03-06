﻿namespace UnitTestTool.EncryptTeamsInfo
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.labelToken = new System.Windows.Forms.Label();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.labelPath = new System.Windows.Forms.Label();
            this.buttonExportToken = new System.Windows.Forms.Button();
            this.textBoxToken = new System.Windows.Forms.TextBox();
            this.textBoxGuestIssuerId = new System.Windows.Forms.TextBox();
            this.labelGuestIssuerId = new System.Windows.Forms.Label();
            this.textBoxGuestIssuerSecret = new System.Windows.Forms.TextBox();
            this.labelGuestIssuerSecret = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelToken
            // 
            resources.ApplyResources(this.labelToken, "labelToken");
            this.labelToken.Name = "labelToken";
            // 
            // textBoxPath
            // 
            resources.ApplyResources(this.textBoxPath, "textBoxPath");
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.ReadOnly = true;
            // 
            // labelPath
            // 
            resources.ApplyResources(this.labelPath, "labelPath");
            this.labelPath.Name = "labelPath";
            // 
            // buttonExportToken
            // 
            resources.ApplyResources(this.buttonExportToken, "buttonExportToken");
            this.buttonExportToken.Name = "buttonExportToken";
            this.buttonExportToken.UseVisualStyleBackColor = true;
            this.buttonExportToken.Click += new System.EventHandler(this.buttonExportToken_Click);
            // 
            // textBoxToken
            // 
            resources.ApplyResources(this.textBoxToken, "textBoxToken");
            this.textBoxToken.Name = "textBoxToken";
            this.textBoxToken.UseSystemPasswordChar = true;
            // 
            // textBoxGuestIssuerId
            // 
            resources.ApplyResources(this.textBoxGuestIssuerId, "textBoxGuestIssuerId");
            this.textBoxGuestIssuerId.Name = "textBoxGuestIssuerId";
            // 
            // labelGuestIssuerId
            // 
            resources.ApplyResources(this.labelGuestIssuerId, "labelGuestIssuerId");
            this.labelGuestIssuerId.Name = "labelGuestIssuerId";
            // 
            // textBoxGuestIssuerSecret
            // 
            resources.ApplyResources(this.textBoxGuestIssuerSecret, "textBoxGuestIssuerSecret");
            this.textBoxGuestIssuerSecret.Name = "textBoxGuestIssuerSecret";
            this.textBoxGuestIssuerSecret.UseSystemPasswordChar = true;
            // 
            // labelGuestIssuerSecret
            // 
            resources.ApplyResources(this.labelGuestIssuerSecret, "labelGuestIssuerSecret");
            this.labelGuestIssuerSecret.Name = "labelGuestIssuerSecret";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxGuestIssuerSecret);
            this.Controls.Add(this.labelGuestIssuerSecret);
            this.Controls.Add(this.textBoxGuestIssuerId);
            this.Controls.Add(this.labelGuestIssuerId);
            this.Controls.Add(this.textBoxToken);
            this.Controls.Add(this.buttonExportToken);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.labelToken);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelToken;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Button buttonExportToken;
        private System.Windows.Forms.TextBox textBoxToken;
        private System.Windows.Forms.TextBox textBoxGuestIssuerId;
        private System.Windows.Forms.Label labelGuestIssuerId;
        private System.Windows.Forms.TextBox textBoxGuestIssuerSecret;
        private System.Windows.Forms.Label labelGuestIssuerSecret;
    }
}

