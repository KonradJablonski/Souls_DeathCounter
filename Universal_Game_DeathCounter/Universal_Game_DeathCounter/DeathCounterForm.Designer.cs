using System;
using System.Windows.Forms;

namespace Universal_Game_DeathCounter
{
    partial class DeathCounterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        Timer UpdateDeathCounter_Timer;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            UpdateDeathCounter_Timer.Stop();
            UpdateDeathCounter_Timer.Dispose();

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.DeathCounter_Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DeathCounter_Label
            // 
            this.DeathCounter_Label.BackColor = System.Drawing.Color.Transparent;
            this.DeathCounter_Label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeathCounter_Label.Font = new System.Drawing.Font("Arial", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeathCounter_Label.ForeColor = System.Drawing.Color.White;
            this.DeathCounter_Label.Location = new System.Drawing.Point(0, 0);
            this.DeathCounter_Label.Margin = new System.Windows.Forms.Padding(0);
            this.DeathCounter_Label.MaximumSize = new System.Drawing.Size(266, 215);
            this.DeathCounter_Label.Name = "DeathCounter_Label";
            this.DeathCounter_Label.Size = new System.Drawing.Size(250, 176);
            this.DeathCounter_Label.TabIndex = 0;
            this.DeathCounter_Label.Text = "...";
            this.DeathCounter_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DeathCounterForm
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(250, 176);
            this.Controls.Add(this.DeathCounter_Label);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "DeathCounterForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Death Counter";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Load += new System.EventHandler(this.DeathCounterForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label DeathCounter_Label;
    }
}

