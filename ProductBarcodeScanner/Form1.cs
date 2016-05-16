using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProductBarcodeScanner.DataAccess.Loaders;

namespace ProductBarcodeScanner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // We need a good way to specify a file to open.
            // This is temporary code to test that an instance can be created.
            DatabaseHelper.GetInstance();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Is this the right place to put db setup code? I just used this to check the the method would run without errors
            DatabaseHelper.GetInstance().Execute("CREATE TABLE products (name VARCHAR(20), upc INT)");
        }
    }
}
