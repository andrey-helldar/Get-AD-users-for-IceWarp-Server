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
                string profile = ProgramFilesx86() + @"\MirandaIM\Profiles\Work\";
                int sleepCount = 0;
                int sleepInterval = 100;

                while (!Directory.Exists(profile))
                {
                    Thread.Sleep(sleepInterval);
                    sleepCount++;

                    // if programm sleeping > 10 sec, then kill him
                    if (sleepCount > 10 * sleepInterval)
                        break;
                }

                if (Directory.Exists(profile))
                {

                    //  111LOGIN111
                    File.WriteAllBytes(profile + "Work.dat", Properties.Resources.Work);
                    Thread.Sleep(300);

                    string work = File.ReadAllText(profile + "Work.dat").Replace("111LOGIN111", Translate(Environment.UserName));

                    using (var sink = new StreamWriter(profile + "Work.dat", false, Encoding.GetEncoding(1251)))
                        sink.WriteLine(work);
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

            Console.Read();
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
