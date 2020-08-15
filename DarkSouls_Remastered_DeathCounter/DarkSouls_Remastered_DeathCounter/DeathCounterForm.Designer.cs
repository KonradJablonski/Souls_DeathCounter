using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DarkSouls_Remastered_DeathCounter
{
    partial class DeathCounterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        Timer UpdateDeathCounter_Timer;
        IntPtr Base;
        VAMemory vam;
        int currentDeathCount = 0;

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

        private void InitialiseSettings()
        {
            System.Collections.Generic.IEnumerable<string> fileLines;
            string filePath = Directory.GetCurrentDirectory() + "\\settings.txt";
            if (File.Exists(filePath))
            {
                fileLines = File.ReadLines(filePath);

                using (var sequenceEnum = fileLines.GetEnumerator())
                {
                    sequenceEnum.MoveNext();
                    string fontColor = sequenceEnum.Current;
                    fontColor = fontColor.Replace("Font_Color:", "");
                    fontColor = fontColor.Trim();

                    sequenceEnum.MoveNext();
                    string fontType = sequenceEnum.Current;
                    fontType = fontType.Replace("Font_Type:", "");
                    fontType = fontType.Trim();

                    sequenceEnum.MoveNext();
                    string fontSize = sequenceEnum.Current;
                    fontSize = fontSize.Replace("Font_Size:", "");
                    fontSize = fontSize.Trim();

                    sequenceEnum.MoveNext();
                    string fontStyle = sequenceEnum.Current;
                    fontStyle = fontStyle.Replace("Font_Style:", "");
                    fontStyle = fontStyle.Trim();
                    
                    DeathCounter_Label.ForeColor = System.Drawing.ColorTranslator.FromHtml(fontColor);
                    DeathCounter_Label.Font = new System.Drawing.Font(fontType, float.Parse(fontSize), (System.Drawing.FontStyle)int.Parse(fontStyle));
                }
            }
        }

        private void InitialiseDeathCounterTimer()
        {
            UpdateDeathCounter_Timer = new Timer();
            UpdateDeathCounter_Timer.Tick += new EventHandler(UpdateDeathCounterLabel);
            UpdateDeathCounter_Timer.Interval = 1;
            UpdateDeathCounter_Timer.Start();
        }

        private void UpdateDeathCounterLabel(object myObject, EventArgs myEventArgs)
        {
            UpdateDeathCounter_Timer.Stop();
            UpdateDeathCounter_Timer.Interval = 2000;
            if (Process.GetProcessesByName("DarkSoulsRemastered").FirstOrDefault() != null)
            {
                Base = Process.GetProcessesByName("DarkSoulsRemastered").FirstOrDefault().MainModule.BaseAddress + 0x1D278F0;
                vam = new VAMemory("DarkSoulsRemastered");

                IntPtr Basefirst = IntPtr.Add((IntPtr)vam.ReadInt32(Base), 0x98);

                var deathCount = ((IntPtr)vam.ReadInt32(Basefirst)).ToString();

                if (currentDeathCount != int.Parse(deathCount))
                {
                    this.DeathCounter_Label.BeginInvoke((MethodInvoker)delegate () { this.DeathCounter_Label.Text = deathCount; });
                    File.WriteAllText(Directory.GetCurrentDirectory() + "\\deaths.txt", deathCount);
                    currentDeathCount = int.Parse(deathCount);
                    Focus();
                    Refresh();
                    Update();

                }
            }
            UpdateDeathCounter_Timer.Start();
        }

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
            this.DeathCounter_Label.Text = "0";
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
            this.Text = "DSR - Death Counter";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Load += new System.EventHandler(this.DeathCounterForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label DeathCounter_Label;
    }
}

