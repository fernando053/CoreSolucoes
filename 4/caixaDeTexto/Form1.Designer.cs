using DevExpress.LookAndFeel;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Controls;
using System.Drawing;
using System.Windows.Forms;

namespace caixaDeTexto
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.simpleButtonInserir = new DevExpress.XtraEditors.SimpleButton();
            this.TextBoxView01 = new System.Windows.Forms.TextBox();
            this.labelView = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // simpleButtonInserir
            // 
            this.simpleButtonInserir.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.simpleButtonInserir.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.simpleButtonInserir.Appearance.ForeColor = System.Drawing.Color.White;
            this.simpleButtonInserir.Appearance.Options.UseBackColor = true;
            this.simpleButtonInserir.Appearance.Options.UseBorderColor = true;
            this.simpleButtonInserir.Appearance.Options.UseFont = true;
            this.simpleButtonInserir.Appearance.Options.UseForeColor = true;
            this.simpleButtonInserir.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(165)))));
            this.simpleButtonInserir.AppearanceHovered.Options.UseBackColor = true;
            this.simpleButtonInserir.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(77)))), ((int)(((byte)(128)))));
            this.simpleButtonInserir.AppearancePressed.Options.UseBackColor = true;
            this.simpleButtonInserir.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.simpleButtonInserir.Location = new System.Drawing.Point(651, 10);
            this.simpleButtonInserir.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.simpleButtonInserir.LookAndFeel.UseDefaultLookAndFeel = false;
            this.simpleButtonInserir.Name = "simpleButtonInserir";
            this.simpleButtonInserir.Size = new System.Drawing.Size(90, 26);
            this.simpleButtonInserir.TabIndex = 3;
            this.simpleButtonInserir.Text = "Inserir";
            this.simpleButtonInserir.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // TextBoxView01
            // 
            this.TextBoxView01.BackColor = System.Drawing.Color.White;
            this.TextBoxView01.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextBoxView01.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.TextBoxView01.ForeColor = System.Drawing.Color.Black;
            this.TextBoxView01.Location = new System.Drawing.Point(492, 12);
            this.TextBoxView01.MaxLength = 50;
            this.TextBoxView01.Name = "TextBoxView01";
            this.TextBoxView01.Size = new System.Drawing.Size(144, 22);
            this.TextBoxView01.TabIndex = 2;
            // 
            // labelView
            // 
            this.labelView.AutoSize = true;
            this.labelView.BackColor = System.Drawing.Color.Transparent;
            this.labelView.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.labelView.Location = new System.Drawing.Point(437, 15);
            this.labelView.Name = "labelView";
            this.labelView.Size = new System.Drawing.Size(50, 15);
            this.labelView.TabIndex = 0;
            this.labelView.Text = "| View 1:";
            this.labelView.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 60);
            this.Controls.Add(this.labelView);
            this.Controls.Add(this.TextBoxView01);
            this.Controls.Add(this.simpleButtonInserir);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("Form1.IconOptions.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MirrorBase - Class Creator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton simpleButtonInserir;
        private TextBox TextBoxView01;
        private Label labelView;
    }
}

