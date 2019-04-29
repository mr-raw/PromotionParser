using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PromotionParser;
using PromotionParser.Workers;

namespace PromotionParser.UI.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void TestButton_Click(object sender, EventArgs e)
        {            
            try
            {
                var p = new PromotionParser();
                p.Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Det oppstod en feil", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
    }
}