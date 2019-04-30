using System;
using System.Windows.Forms;

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