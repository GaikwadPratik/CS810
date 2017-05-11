using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Stopwatch sw = new Stopwatch();
        Timer t1 = new Timer();
        public Form1()
        {
            InitializeComponent();
            txtPassword.Focus();
            File.WriteAllText("profile.arff", $"C,T{Environment.NewLine}");
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            //sw.Start();
            //DisplayInfo("Key Pressed:", e, true);
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            sw.Start();
            //DisplayInfo("Key Release:", e, false);
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            //DisplayInfo("Key pressed", null, e);
            sw.Stop();
            string strText = $"{e.KeyChar},{sw.ElapsedMilliseconds.ToString()}{Environment.NewLine}";
            txtSummary.Text += string.Format($"{strText}");
            File.AppendAllText("profile.arff", strText);
            sw.Restart();
        }

        private void DisplayInfo(String strStatus, KeyEventArgs keyEvent = null, bool bStopTimer = false)
        {
            StringBuilder sbKeyString = new StringBuilder();
            string strKeyString = string.Empty;

            sbKeyString.Append($"Key code = {keyEvent.KeyCode} ({keyEvent.KeyData}");
            sbKeyString.Append($"Extended modifiers = {keyEvent.Modifiers}");
            //if(keyEvent.Modifiers)
            //    sbKeyString.Append()
            //if (keyEvent.Shift || keyEvent.Alt || keyEvent.Control)
            //    sbKeyString.AppendFormat($"({keyEvent.KeyData})");
            if (!bStopTimer)
            {
                sw.Reset();

            }
            else
            {
                sw.Stop();
                string strText = $" {keyEvent.KeyData} {sw.ElapsedMilliseconds.ToString()}";
                txtSummary.Text += strText;
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPassword.Text = string.Empty;
            txtSummary.Text = string.Empty;
        }


    }
}
