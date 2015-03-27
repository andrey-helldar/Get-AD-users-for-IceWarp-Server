using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;

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


                // Remove shortcut
                var info = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Startup));

                foreach (FileInfo file in info.GetFiles())
                    if (file.Name.IndexOf("Psi") > -1)
                        File.Delete(file.FullName);
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
            try
            {
                Console.WriteLine("Extract Miranda IM...");
                string miranda = Environment.GetFolderPath(Environment.SpecialFolder.Templates) + @"\MirandaIM.exe";
                if (File.Exists(miranda)) File.Delete(miranda);
                File.WriteAllBytes(miranda, Properties.Resources.MirandaIM);

                Console.WriteLine("Installing Miranda IM...");
                Process.Start(miranda);

                Console.WriteLine("Installing profile for this user");
                string fileUserDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Miranda\Work\";
                string fileUser = @"\\192.168.1.5\netlogon\miranda\" + Translate(Environment.UserName) + ".dat";
                string fileUserTo = fileUserDir + "Work.dat";

                if (!Directory.Exists(fileUserDir))
                    Directory.CreateDirectory(fileUserDir);

                try { File.Copy(fileUser, fileUserTo, true); }
                catch (Exception ex)
                {
                    File.WriteAllBytes(fileUserTo, Properties.Resources.Work);
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(" ");
                    Console.WriteLine(ex.StackTrace);
                }

                Console.WriteLine("Miranda IM installed succesfully!");

                // Deleting installer
                if (File.Exists(ProgramFilesx86() + @"\MirandaIM.exe"))
                    File.Delete(ProgramFilesx86() + @"\MirandaIM.exe");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(" ");
                Console.WriteLine(ex.StackTrace);
            }

            //Console.Read();
        }

        /// <summary>
        /// Get "Program Files" for x84 architecture programms
        /// </summary>
        /// <returns>Path</returns>
        static string ProgramFilesx86()
        {
            if (8 == IntPtr.Size
                || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }

        /// <summary>
        /// Translate cyrillyc usernames into english symbols
        /// </summary>
        /// <param name="user">Username</param>
        /// <returns>Translated username</returns>
        static string Translate(string user)
        {
            try
            {
                user = user.ToLower();

                string[,] words = new string[,] {
                    {"а", "a"},
                    {"б", "b"},
                    {"в", "v"},
                    {"г", "g"},
                    {"д", "d"},
                    {"е", "e"},
                    {"ё", "e"},
                    {"ж", "zh"},
                    {"з", "z"},
                    {"и", "i"},
                    {"й", "i"},
                    {"к", "k"},
                    {"л", "l"},
                    {"м", "m"},
                    {"н", "n"},
                    {"о", "o"},
                    {"п", "p"},
                    {"р", "r"},
                    {"с", "s"},
                    {"т", "t"},
                    {"у", "u"},
                    {"ф", "f"},
                    {"х", "h"},
                    {"ц", "c"},
                    {"ч", "ch"},
                    {"ш", "sh"},
                    {"щ", "sh"},
                    {"ъ", ""},
                    {"ы", "i"},
                    {"ь", ""},
                    {"э", "e"},
                    {"ю", "yu"},
                    {"я", "ya"},
                    {" ", ""}
                };

                for (int i = 0; i < 33; i++)
                    user = user.Replace(words[i, 0], words[i, 1]);

                return user;
            }
            catch (Exception) { return "error"; }
        }
    }
}
