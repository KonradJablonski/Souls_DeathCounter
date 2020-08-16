using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Universal_Game_DeathCounter
{
    public partial class DeathCounterForm : Form
    {
        public DeathCounterForm()
        {
            InitializeComponent();
        }

        private void DeathCounterForm_Load(object sender, EventArgs e)
        {
            InitialiseSettings();
            InitialiseDeathCounterTimer();
        }

        private void InitialiseSettings()
        {
            System.Collections.Generic.IEnumerable<string> fileLines;
            string filePath = System.IO.Directory.GetCurrentDirectory() + "\\settings.txt";
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

    }
}
