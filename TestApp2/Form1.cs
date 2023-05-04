using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wercs.Core.Utils;
using Ionic.Zlib.Zip;
using ZipFile = Ionic.Zlib.Zip.ZipFile;
using System.Collections;
using Newtonsoft.Json;
using System.Data.SqlTypes;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using static System.Net.WebRequestMethods;
using System.Xml.Linq;
using System.Xml;
using System.Security.AccessControl;

namespace TestApp2
{
	public partial class Form1 : Form
	{
		public DataType JSONDataTypeObject = new DataType();

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			ArrayList filelist = new ArrayList();
			string casData = "$STEL (15 min): $104 $mg/m3 $(20°C) $|par fume $TWA (8hr): $52 $mg/m3 $(20°C) $|par droplets $TWA (8hr): $10 $mg/m3 $(20°C) $ |par $H*";
			string[] data = casData.Split('$');
			string ExportPath = @"D:\Program Files\TheWercs\DTEInterfacesForPhilips\ConfigFiles\CXE\Output";
			if (Directory.Exists(ExportPath))
			{
				string resolvedFileName = "test.zip";
				string zipFilename = Path.Combine(ExportPath, resolvedFileName);



				DirectoryInfo dirInfo = new DirectoryInfo(ExportPath);
				FileInfo[] files = dirInfo.GetFiles();

				//DateTime olderDate = DateTime.Now.AddDays(-30);

				foreach (FileInfo file in files)
				{
					string extension = Path.GetExtension(file.Name);
					if (extension.ToLower() == ".xml" || extension.ToLower() == ".pdf")
					{
						//filelist.Add(file.FullName);
					}
					//if (file.LastAccessTime < olderDate)
					//{
					//	Zip.AddFileToZip()
					//}
				}
				string[] myArray = (string[])filelist.ToArray(typeof(string));
				AddFileToZip(zipFilename, myArray, ExportPath);
			}



		}
		private void AddFileToZip(string zipFilename, string[] filesToAdd, string directory)
		{
			zipFilename = Path.Combine(directory, zipFilename);
			using (ZipFile zip = new ZipFile())
			{
				zip.UseZip64WhenSaving = Zip64Option.AsNecessary;
				foreach (string file in filesToAdd)
				{
					string extension = Path.GetExtension(file);
					if (extension != ".zip")
					{
						zip.AddFile(file, "");
					}
				}

				zip.Save(zipFilename);
			}
		}
		private string GetLanguageLookup()
		{
			try
			{

				StringBuilder language = new StringBuilder();
				StringBuilder defaltlanguage = new StringBuilder();

				string d1 = "EN,RU";
				string look = "EN,MS,RU";
				string default1 = "en,ms,ru";

				string[] dataList = d1.Split(',');
				string[] Values = look.Split(',');
				string[] DefaultValues = default1.Split(',');

				foreach (string data in dataList)
				{
					if (!string.IsNullOrEmpty(data))
					{
						int i = 0;
						foreach (string value in Values)
						{
							if (data.ToLower() == value.ToLower())
							{
								language.Append(Values[i]);
								language.Append(",");
								defaltlanguage.Append(DefaultValues[i]);
								defaltlanguage.Append(",");
							}
							i = i + 1;
						}
					}
				}


				string l1 = language.ToString().TrimEnd(',');
				string l2 = defaltlanguage.ToString().TrimEnd(',');

			}
			catch (Exception ex)
			{
				return ex.Message;
			}

			return "";

		}



        private void writefile(string file)
        {

            StringBuilder sb = new StringBuilder();
            
            sb.Append("log something" + Environment.NewLine); 
            sb.Append("log2 something" + Environment.NewLine);
            sb.Append("log3 something" + Environment.NewLine);


            // flush every 20 seconds as you do it
            System.IO.File.AppendAllText(file, sb.ToString());
            sb.Clear();

        }

        private void button2_Click(object sender, EventArgs e)
		{
			string chem = "Nitric acid |vC <= 70%|v0";
			string reschem = chem.Replace("|vC <= 70%|v0", "").TrimEnd();

            string[] reschem2 = chem.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
			string reschem3 = reschem2[0].TrimEnd();



            string filename = @"D:\test.txt";

			writefile(filename);
			GetLanguageLookup();
			string val=string.Empty;
			string[] test4 = val.Split(',');
			int cnt=test4.Length;


			string[] data = new string[3];
			data[0] = "t1";
			data[1] = "t2";
			data[2] = "t3";

			string test = string.Join(",", data);

			DataType dbtype = new DataType();
			dbtype.EUHZinDataCode = "t4";
			dbtype.PZinDataCode = "t5";
			
			string s = JsonConvert.SerializeObject(dbtype);

			//string DataCodeJSON = "{\"DataType\":{\r\n\"HZinDataCode\":\"EHSHP,EPHHH,EHY2H,EHY3H\",\r\n\"PZinDataCode\":\"EHSPO,EWPS\",\r\n\"EUHZinDataCode\":\"EGHEU\",\r\n\"SignalDataCode\":\"EGHS\",\r\n\"SymbolDataCode\":\"ESGHG,ESGG2,ESGG3,ES11G,ESPHG,ESPHG2,ESPG1,ESPG2,ESPG4,ESPG5\",\r\n\"FaseDataCode\":\"PYST\"\r\n}\r\n}";
			//JSONDataTypeObject = JsonConvert.DeserializeObject<DataType>(DataCodeJSON);

			string value = "dot0001,dot0002";
			value.Split(',').ForEach(x =>
			{
				if (!string.IsNullOrEmpty(x))
				{
					return;
				}

			});

		}

		public class DataType
		{
			public string HZinDataCode { get; set; }
			public string PZinDataCode { get; set; }
			public string EUHZinDataCode { get; set; }
			public string SignalDataCode { get; set; }
			public string SymbolDataCode { get; set; }
			public string FaseDataCode { get; set; }
		}
		public class Root
		{
			public DataType DataType { get; set; }
		}

		private void button3_Click(object sender, EventArgs e)
		{
			string sourcepath = "";
			string passPhrase = "wercs@123";
			string gpgBatPath = "E:\\Sample\\TestApp2\\GPGBatch";
			string[] sourceFiles = Directory.GetFiles(sourcepath, "*.gpg");			
			foreach (string sourceFile in sourceFiles)
			{				
				string targetFile = Path.ChangeExtension(sourceFile, ".xml");
				//string destinationFile = string.Format("{0}{1}{2}", Path.GetFileNameWithoutExtension(sourceFile), Path.GetExtension(sourceFile));

				string msg = DecryptFileBatch(sourceFile, targetFile, passPhrase, gpgBatPath);
				//Move Decrypted Files to SourcePath
			
				//Original file moved to Error path
				if (msg.Contains("Error occured") || msg.Contains("decryption failed") || msg.Contains("secret key not available"))
				{
					MessageBox.Show(msg.ToString());
				}

			}
			MessageBox.Show("Decrypt Process Completed");



		}
		private string DecryptFileBatch(string inputName, string outputName, string passPhrase, string gpgBatPath)
		{
			string decryptFile = Path.Combine(gpgBatPath, "GPGDecryption.bat");
			if (System.IO.File.Exists(decryptFile))
			{
				string Command = System.IO.File.ReadAllText(decryptFile);
				Command = Command.Replace("@sourceFile", inputName);
				Command = Command.Replace("@destinationFile", outputName);
				System.IO.File.WriteAllText(Path.Combine(gpgBatPath, "Decrypt.bat"), Command);
			}
			else
			{
				return "GPGDecryption.bat file dose not exists";
			}
			return ExecuteCommandSync(Path.Combine(gpgBatPath, "Decrypt.bat"), passPhrase);
		}
		private string ExecuteCommandSync(string GpgBatFile, string password)
		{
			try
			{
				var procStartInfo = new ProcessStartInfo(GpgBatFile)
				{
					WorkingDirectory = Path.GetDirectoryName(GpgBatFile),
					CreateNoWindow = true,
					UseShellExecute = false,
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					RedirectStandardInput = true
				};

				var proc = new Process { StartInfo = procStartInfo };
				proc.Start();

				if (!string.IsNullOrEmpty(password))
				{
					proc.StandardInput.WriteLine(password);
				}

				proc.StandardInput.Flush();

				// Get the output into a string
				string result = proc.StandardOutput.ReadToEnd();
				string error = proc.StandardError.ReadToEnd();
				return "\n" + error + "\n" + "" + result;
			}
			catch (Exception ex)
			{
				string message = "Error occured at ExecuteCommandSync " + ex.ToString();				
				return message;
			}
		}
	}
}
