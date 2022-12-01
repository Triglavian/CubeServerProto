using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using CubeServerTest.ServerLogic.DataIO;

namespace CubeServerTest.ServerLogic
{
    class FileManager
    {
        static FileManager instance = null;
        public static FileManager GetInstance()
        {
            if (instance is null) 
            {
                instance = new FileManager();
            }
            return instance;
        }
        Dictionary<int, string> stageData = null;
        public byte[] GetStageListData()
        {
            List<int> idList = new List<int>();
            foreach (var file in stageData)
            {
                idList.Add(file.Key);
            }
            return idList.SelectMany(BitConverter.GetBytes).ToArray();
        }
        public byte[] GetStageData(int id)
        {
            string fileDirectory = stageData[id];
            if (fileDirectory == null) 
            {
                return null;
            }
            //JsonReader reader = new JsonReader();
            //reader.Convert(fileDirectory);
            return JsonReader.Convert(fileDirectory);
        }
        private FileManager()
        {
            stageData = new Dictionary<int, string>();
            stageData.Clear();
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            RegisterFileDirectory();
        }
        private FileManager(FileManager inst) 
        {

        }
        void RegisterFileDirectory()
        {
            DirectoryInfo direc = new DirectoryInfo(@"..\..\Jsons");
            foreach (var file in (direc.GetFiles()))
            {
                string[] token = file.Name.Split('.');
                stageData.Add(int.Parse(token[0]), file.DirectoryName + "\\" + file.Name);
                Console.WriteLine("Add {0}, {1}", file.Name, file.DirectoryName);
            }
            foreach (var a in stageData)
            {
                Console.WriteLine($"{a.Key}, {a.Value}");
            }
        }
    }
}
