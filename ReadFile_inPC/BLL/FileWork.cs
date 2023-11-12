using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FileWork
    {
        public DirectoryInfo _maindirectory;
        private List<string> _bunWords;
        public int Progress { get; set; }
        public int MaxProgress { get; set; }

        public FileWork()
        {
            _maindirectory = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).Parent;

        }
        async public Task FindWords(List<string> words)
        {
            _bunWords = words;
            Progress = 0;
            MaxProgress += 1;
            string path = @"C:\Tetyana";
            DirectoryInfo dir = new DirectoryInfo(path);
            MaxProgress = dir.GetDirectories().Length;
            await FindCountDirection(path);
            await FindDirection(path);
            await FileToChange();
        }

        #region ReadFileBanWord1
        async public Task<List<string>> AsyncReadFileWord()
        {

            List<string> strings = new List<string>();

            using (FileStream fileStream = new FileStream($@"{_maindirectory.FullName}\Words.txt", FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fileStream, Encoding.UTF8))
                {
                    while (sr.Peek() > 0)
                    {
                        strings.Add(await sr.ReadLineAsync());

                    }
                }
            }
            return strings;
        }
        async public Task AddWordAsync(string word)
        {
            bool ok = true;
            List<string> strings = await AsyncReadFileWord();
            foreach (string s in strings)
            {
                if (s == word)
                {
                    ok = false;
                    break;
                }
            }
            using (FileStream fileStream = new FileStream($@"{_maindirectory.FullName}\Words.txt", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sr = new StreamWriter(fileStream, Encoding.UTF8))
                {

                    if (ok)
                        await sr.WriteLineAsync(word);
                }
            }
        }
        #endregion


        #region FindFile2
        async private Task FindCountDirection(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            DirectoryInfo[] subdirectories = dir.GetDirectories();
            MaxProgress += dir.GetDirectories().Length;
            foreach (DirectoryInfo subdirectory in subdirectories)
            {
                await FindCountDirection(subdirectory.FullName);
            }
        }
        async private Task FindDirection(string path)
        {
            Progress += 1;
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
             
                if (file.Extension == ".txt")
                {
                    if (await ReadFileAsync(file.FullName))
                    {
                        await Task.Run(() => file.CopyTo($@"{_maindirectory.FullName}\FindText\{file.Name}_copy.txt", true));
                    }

                }
            }

            DirectoryInfo[] subdirectories = dir.GetDirectories();
            foreach (DirectoryInfo subdirectory in subdirectories)
            {
                await FindDirection(subdirectory.FullName);
            }
        }

        async private Task<bool> ReadFileAsync(string path)
        {

            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string temp = "";
                    while (sr.Peek() > 0)
                    {
                        temp = await sr.ReadLineAsync();
                        foreach (string word in _bunWords)
                        {
                            if (temp.Contains(word))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        #endregion


        #region ChangeFile3
        async private Task FileToChange()
        {

            DirectoryInfo directoryInfo = new DirectoryInfo($@"{_maindirectory.FullName}\FindText");
            FileInfo[] files = directoryInfo.GetFiles();

            foreach (FileInfo file in files)
            {
                if (file.Extension == ".txt")
                    await ChangeWord(file);
            }
        }
        async private Task ChangeWord(FileInfo file)
        {
            string temp = "";
            using (FileStream fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fileStream, Encoding.UTF8))
                {

                    while (sr.Peek() > 0)
                    {
                        temp += await sr.ReadLineAsync() + "\n";
                    }
                }
            }

            Dictionary<string, int> rez = new Dictionary<string,int>();
            foreach (string word in _bunWords)
            {
                int count = temp.Split(new string[] { word }, StringSplitOptions.None).Length - 1;
                rez.Add(word, count);
                temp = temp.Replace(word, "*******");
            }
            using (FileStream fileStream = new FileStream($@"{_maindirectory.FullName}\FindFiles.txt", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sr = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    string write = $"Файл: {file.Name} | ";
                    foreach (var keys in rez)
                    {
                        write += $"Найдено <{keys.Key}>   раз: {keys.Value} | ";
                    }
                    await sr.WriteLineAsync(write);
                }
            }

            using (FileStream fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Write))
            {
                using (StreamWriter sr = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    await sr.WriteLineAsync(temp);

                }
            }
        }
        #endregion

        public void OpenFile(string name)
        {
            Process.Start($@"{_maindirectory.FullName}\{name}.txt");
        }
        public string getPhoto()
        {
            string str = "";
            foreach(DirectoryInfo directory in _maindirectory.GetDirectories())
            {
                if(directory.Name == "Exam_Sys")
                {
                    foreach(DirectoryInfo dir in directory.GetDirectories())
                    {
                        if (dir.Name == "images")
                        {
                            foreach(FileInfo file in dir.GetFiles())
                            {
                                if (file.Name == "car.png")
                                {
                                    return file.FullName;
                                }
                            }
                        }
                    }
                }
            }
            return "";
        }

    }
}
