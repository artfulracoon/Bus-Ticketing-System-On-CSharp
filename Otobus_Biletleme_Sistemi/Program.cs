using System;
using System.Windows.Forms;

namespace Otobus_Biletleme_Sistemi
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool KeepRunning = true;

            while (KeepRunning)
            {
                Application.Run(new Form3());
                var result = MessageBox.Show("Programı tamamen kapatmak için evet, ana menüye dönmek için hayırı seçiniz.", "UYARI!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    KeepRunning = false;
            }
        }
    }
}
