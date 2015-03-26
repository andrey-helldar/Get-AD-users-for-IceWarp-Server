using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Install_Miranda_IM_client
{
    class Program
    {
        static void Main(string[] args)
        {
            /* ==========================
             *  REMOVE PSI
             * ========================== */
            try
            {
                /*
                 * PsiService_2.exe
                 * Psi.exe
                 */
                Process[] processPsi = Process.GetProcessesByName("Psi");
                foreach (var process in processPsi)
                {
                    process.Kill();
                    Console.WriteLine("Process Psy.exe killed!");
                }


                Process[] processPsiService = Process.GetProcessesByName("PsiService_2");
                foreach (var process in processPsiService)
                {
                    process.Kill();
                    Console.WriteLine("Process PsiService_2.exe killed!");
                }


                // Remove profile of "PSI" IM client from user PC
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Psi";
                Console.WriteLine("Deleting " + appData);

                if (Directory.Exists(appData))
                {
                    Directory.Delete(appData, true);
                    Console.WriteLine("Profile deleted");
                }
                else
                    Console.WriteLine("Profile not found");


                // Remove "PSI" programm files
                string psyData = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Psi";
                Console.WriteLine("Deleting " + psyData);

                if (Directory.Exists(psyData))
                {
                    Directory.Delete(psyData, true);
                    Console.WriteLine("PSI deleted");
                }
                else
                    Console.WriteLine("PSI not found");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(" ");
                Console.WriteLine(ex.StackTrace);
            }



            /* ==========================
             *  INSTALLING MIRANDA IM
             * ========================== */
        }
    }
}
