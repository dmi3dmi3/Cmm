using Microsoft.Win32;
using Parser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Path = System.IO.Path;

namespace CmmCompiler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string PathToFile = "";
        private string FileName = "";
        private string FullNameIL = "";
        private string FullName => Path.Combine(PathToFile, FileName);


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string data = InputTB.Text;
                var result = Translator.Translate(data);
                if (FileName == "")
                {
                    var fd = new SaveFileDialog();
                    fd.AddExtension = true;
                    fd.DefaultExt = "cmm";
                    fd.Filter = "Файлы cmm(*.cmm)|*.cmm";
                    fd.ShowDialog();
                    var full = fd.FileName;
                    File.WriteAllText(full, data);
                    FileName = Path.GetFileName(full);
                    PathToFile = full.Substring(0, full.Length - FileName.Length);
                }

                FullNameIL = Path.Combine(PathToFile, Path.GetFileNameWithoutExtension(FullName)) + ".il";
                File.WriteAllText(FullNameIL, result);


                var ch = new CommandHelpers();
                var res = ch.GetResult(Path.Combine(Environment.CurrentDirectory, "Ilasm\\ilasm.exe"),
                    new List<string>() {FullNameIL});
                File.Delete(FullNameIL);
                if (res.Contains("successfully"))
                {
                    MessageBox.Show("Success");
                }
                else
                {
                    MessageBox.Show("Failure");
                }

                return;
                Process proc = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = Path.Combine(Environment.CurrentDirectory, "Ilasm\\ilasm.exe"),
                        Arguments = FullNameIL,
                        UseShellExecute = false,
                        RedirectStandardOutput = false,
                    }
                };
                var bufer = new StringBuilder();
                proc.OutputDataReceived += (o, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        bufer.AppendLine(args.Data);
                    }
                };
                proc.EnableRaisingEvents = true;
                proc.Start();
                proc.BeginOutputReadLine();
                //var b = proc.Start();
                //if (!b)
                //{
                //    throw new Exception("ilasm not started");
                //}
                var str = bufer.ToString();
                MessageBox.Show(str);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }


    public class CommandHelpers
    {
        public CommandHelpers()
        {
            Invisible = true;
        }

        /// <summary>
        ///     Not show CMD window
        /// </summary>
        public bool Invisible { get; set; }

        /// <summary>
        ///     Execete CMD command
        /// </summary>
        /// <param name="commandName">Command name only</param>
        /// <param name="paramsList">Params and keys for command</param>
        public bool Execute(string commandName, IEnumerable<string> paramsList)
        {
            return CreateProcess(commandName, paramsList).Start();
        }

        /// <summary>
        ///     Async execete CMD command
        /// </summary>
        /// <param name="commandName">Command name only</param>
        /// <param name="paramsList">Params and keys for command</param>
        public Task<bool> ExecuteAsync(string commandName, IEnumerable<string> paramsList)
        {
            return Task<bool>.Factory.StartNew(() => CreateProcess(commandName, paramsList).Start());
        }

        /// <summary>
        ///     Returns result of command execution
        /// </summary>
        /// <param name="commandName">Command name only</param>
        /// <param name="paramsList">Params and keys for command</param>
        /// <returns></returns>
        public string GetResult(string commandName, IEnumerable<string> paramsList)
        {
            var bufer = new StringBuilder();
            using (var proc = CreateProcess(commandName, paramsList, true))
            {
                proc.Start();
                while (!proc.StandardOutput.EndOfStream)
                {
                    bufer.AppendLine(proc.StandardOutput.ReadLine());
                }
            }
            return bufer.ToString();
        }

        /// <summary>
        ///     Returns result of command execution
        ///     Experemental. Not Tested.
        /// </summary>
        /// <param name="commandName">Command name only</param>
        /// <param name="paramsList">Params and keys for command</param>
        /// <returns></returns>
        public string GetResultAsync(string commandName, IEnumerable<string> paramsList)
        {
            var bufer = new StringBuilder();
            using (var proc = CreateProcess(commandName, paramsList, true))
            {
                proc.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        bufer.AppendLine(e.Data);
                    }
                };
                proc.BeginOutputReadLine();
                proc.EnableRaisingEvents = true;
                proc.WaitForExit();
            }
            return bufer.ToString();
        }


        private Process CreateProcess(string commandName, IEnumerable<string> paramsList, bool output = false)
        {
            var paramString = paramsList.Aggregate<string, string>(null,
                (current, param) => current + " " + param);
            return new Process
            {
                StartInfo =
                {
                    FileName = commandName,
                    Arguments = paramString,
                    UseShellExecute = output ? !output : !Invisible,
                    RedirectStandardOutput = output
                }
            };
        }
    }
}

