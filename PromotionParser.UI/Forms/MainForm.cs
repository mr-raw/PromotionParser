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
            var p = new PromotionParser();
            p.Run();
        }
    }
}