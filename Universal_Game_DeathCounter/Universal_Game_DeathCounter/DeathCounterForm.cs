using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;


namespace Universal_Game_DeathCounter
{
    public partial class DeathCounterForm : Form
    {
        IntPtr Base;
        VAMemory vam;
        int currentDeathCount = 0;

        const short max_games = 5;
        const short max_offsets = 3;
        string[] currentGame = { "DarkSoulsRemastered", "DarkSouls", "DarkSoulsIII", "DarkSoulsII", "DarkSoulsII" };
        int[] memoryLocationOfDeathCounter = { 0x1D278F0, 0xF78700, 0x4740178, 0x11493F4, 0x160B8D0 };
        int[,] currentGameOffset = new int[max_games, max_offsets] { { 0x98, 0, 0 },
                                                   { 0x5C, 0, 0 },
                                                   { 0x98, 0, 0 },
                                                   { 0x74, 0x378, 0x1A0 }, //OG DS2
                                                   { 0xD0, 0x490, 0x1A4 } }; //SOTFS

        string OriginalDarkSouls2_Description = "DARK SOULS Ⅱ";
        short OriginalDarkSouls2_Index = 3;
         
        short currentGameID = -1;

        public DeathCounterForm()
        {
            InitializeComponent();
        }

        private void DeathCounterForm_Load(object sender, EventArgs e)
        {
     
            DetectGame();
            InitialiseSettings();
            InitialiseDeathCounterTimer();
        }

        private void DetectGame()
        {
            for (short i = 0; i < max_games; i++)
            {
                if (Process.GetProcessesByName(currentGame[i]).FirstOrDefault() != null)
                {
                    bool passed = true;
                    if (OriginalDarkSouls2_Index == i) //Dark souls 2 and Darks Souls 2 SOTFS have the same names, therefor I am reading the FileDescription for OG Dark Souls to make sure we are setting up correct DS2
                    {
                        string ds2_FileDescription = Process.GetProcessesByName(currentGame[i]).FirstOrDefault().MainModule.FileVersionInfo.FileDescription;

                        if (ds2_FileDescription != OriginalDarkSouls2_Description)
                            passed = false;
                    }

                    if (passed)
                    {
                        currentGameID = i;
                        this.Text = currentGame[i];
                        this.DeathCounter_Label.BeginInvoke((MethodInvoker)delegate () { this.DeathCounter_Label.Text = "0"; });

                        Base = Process.GetProcessesByName(currentGame[currentGameID]).FirstOrDefault().MainModule.BaseAddress + memoryLocationOfDeathCounter[currentGameID];
                        vam = new VAMemory(currentGame[currentGameID]);
                        return;
                    }

                }
            }
            this.Text = "Searching...";
            currentGameID = -1;
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
            if (currentGameID != -1 && Process.GetProcessesByName(currentGame[currentGameID]).FirstOrDefault() != null)
            {
                IntPtr BaseOffseted = Base;
                for (short i = 0; i < max_offsets; i++)
                {
                    if (currentGameOffset[currentGameID, i] != 0)
                    {
                        if (!NativeMethods.IsWin64Emulator(Process.GetProcessesByName(currentGame[currentGameID]).FirstOrDefault()))
                        {
                            BaseOffseted = (IntPtr)vam.ReadInt64(BaseOffseted) + currentGameOffset[currentGameID, i];
                        }
                        else
                            BaseOffseted = (IntPtr)vam.ReadInt32(BaseOffseted) + currentGameOffset[currentGameID, i];
                    }
                    else
                        break;
                }

                var deathCount = ((IntPtr)vam.ReadInt32(BaseOffseted)).ToString();

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
            else
            {
                currentDeathCount = 0;
                this.DeathCounter_Label.BeginInvoke((MethodInvoker)delegate () { this.DeathCounter_Label.Text = "..."; });
                File.WriteAllText(Directory.GetCurrentDirectory() + "\\deaths.txt", "0");
                DetectGame();
            }

            UpdateDeathCounter_Timer.Start();
        }
    }
}
