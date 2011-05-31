//This source code is property of Nsoftware systems 2005-2011
//It cant be totally reproduced, maximum of 30% of code amount is allowed to use
//Cant be reproduced without referance to the Nsoft
//Cant be used in commercial projects
//All liciensing questions can be asked at http://nsoft.ucoz.ru


using System;
using System.Windows.Forms;

namespace Graph
{
    static class Program
    {
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
