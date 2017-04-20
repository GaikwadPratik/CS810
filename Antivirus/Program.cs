using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string _folderPath = "Malware";
            string _hashFile = "Hash";

            List<string> _hasFileList = File.ReadAllText(_hashFile).Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            List<string> _lstFiles = new List<string>();
            int inm = 1;
            foreach (string file in Directory.EnumerateFiles(_folderPath, "*.apk", SearchOption.AllDirectories))
            {
                _lstFiles.Add(file);
                using (Stream _fileSteram = File.OpenRead(file))
                {
                    using (MD5CryptoServiceProvider _md5 = new MD5CryptoServiceProvider())
                    {
                        byte[] _hashByte = _md5.ComputeHash(_fileSteram);
                        StringBuilder result = new StringBuilder(_hashByte.Length * 2);

                        for (int i = 0; i < _hashByte.Length; i++)
                            result.Append(_hashByte[i].ToString("x2"));
                        string _finalHash = result.ToString();

                        if (result != null)
                            result = null;

                        bool _bFound = false;
                        foreach (string hash in _hasFileList)
                        {
                            if (hash.Equals(_finalHash, StringComparison.OrdinalIgnoreCase))
                            {
                                _bFound = true;
                                break;
                            }
                        }

                        string _fileName = file.Split(new char[] { '\\' }).Last();

                        if (_bFound)
                            Console.WriteLine($"{inm}>{_fileName} is Virus");
                        else
                            Console.WriteLine($"{inm}>{_fileName} is not virus");
                    }
                }
                inm += 1;
            }
            Console.ReadLine();
        }
    }
}
