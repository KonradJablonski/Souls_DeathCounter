using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarkSouls_Remastered_DeathCounter
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
    }
}
