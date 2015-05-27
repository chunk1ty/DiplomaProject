using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Ankk.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        const int numberOfTests = 9;

        public ActionResult Index()
        {
            //if (User.IsInRole("Administrator"))
            //{
            //    RedirectToAction("Index", "Administration");   
            //}

            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // extract only the fielname
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
            }
            // redirect back to the index action to show the form once again
            return RedirectToAction("Result");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Result()
        {
            int score = Compare() * 10;

            return View(score);
        }

        public int Compare()
        {
            OpenCMD();

            int counter = 0;
            for (int i = 1; i <= numberOfTests; i++)
            {
                string output = Path.Combine(Server.MapPath("~/App_Data/output"), "test.00" + i + ".out.txt");
                FileInfo outputFile = new FileInfo(output);

                string actual = Path.Combine(Server.MapPath("~/App_Data/actualOutput"), "actualOutput" + i + ".txt");
                FileInfo actualFile = new FileInfo(actual);


                var areSameByHash = FilesAreEqual_Hash(outputFile, actualFile);
                var areSameByOneByte = FilesAreEqual_OneByte(outputFile, actualFile);

                if (areSameByHash && areSameByOneByte)
                {
                    counter++;
                }
            }

            return counter;
        }

        public void OpenCMD()
        {
            string pathToExe = Path.Combine(Server.MapPath("~/App_Data/uploads"), "CalculateTwoNumbers.exe");

            Process cmd = new Process();

            ProcessStartInfo info = new ProcessStartInfo(pathToExe, "");
            //info.WorkingDirectory = @"D:\";
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            info.UseShellExecute = false;
            cmd.StartInfo = info;

            for (int i = 1; i <= numberOfTests; i++)
            {
                cmd.Start();
                string currentInput = "test.00" + i + ".in.txt";

                //@"D:\Ankk\Tests\Input\

                var pathInput = Path.Combine(Server.MapPath("~/App_Data/input"), currentInput);

                using (StreamReader reader = new StreamReader(pathInput))
                {
                    string line = reader.ReadLine();
                    cmd.StandardInput.WriteLine(line);

                    while (line != null)
                    {
                        line = reader.ReadLine();
                        cmd.StandardInput.WriteLine(line);
                    }
                }

                String output = cmd.StandardOutput.ReadToEnd();

                string currentOutput = "actualOutput" + i + ".txt";

                var pathOutput = Path.Combine(Server.MapPath("~/App_Data/actualOutput"), currentOutput);

                using (StreamWriter writer = new StreamWriter(pathOutput))
                {
                    writer.Write(output);
                }

                cmd.Close();
            }
        }

        public static bool FilesAreEqual_OneByte(FileInfo first, FileInfo second)
        {
            if (first.Length != second.Length)
                return false;

            using (FileStream fs1 = first.OpenRead())
            using (FileStream fs2 = second.OpenRead())
            {
                for (int i = 0; i < first.Length; i++)
                {
                    if (fs1.ReadByte() != fs2.ReadByte())
                        return false;
                }
            }

            return true;
        }

        public static bool FilesAreEqual_Hash(FileInfo first, FileInfo second)
        {
            byte[] firstHash = MD5.Create().ComputeHash(first.OpenRead());
            byte[] secondHash = MD5.Create().ComputeHash(second.OpenRead());

            for (int i = 0; i < firstHash.Length; i++)
            {
                if (firstHash[i] != secondHash[i])
                    return false;
            }
            return true;
        }
    }
}