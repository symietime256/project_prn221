using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Schedule_Project.DTOs;
using static Schedule_Project.SharingContent.EnumSource;

namespace Schedule_Project.SharingContent
{
    public class HandleFileUpload
    {
        public HandleFileUpload() { }

        //private static void Main(string[] args)
        //{
        //    string json = System.IO.File.ReadAllText(@"D:\workspace\self-learning\cs\SE1702_Solution\TestExcel\data.json");

        //    Youtuber deserialized = JsonConvert.DeserializeObject<Youtuber>(json);
        //    foreach (var item in deserialized.Members)
        //    {
        //        Console.WriteLine(item.ToString());
        //    }
        //}
        public List<ScheduleDTO> DeserializeListOfScheduleInformation(string filePath, FileType fileType)
        {
            List<ScheduleDTO> scheduleDTOs;
            switch (fileType) {
                case FileType.JSON:
                    scheduleDTOs = DeserializeToJSON(filePath);
                    break;
                case FileType.XML:
                    scheduleDTOs = new List<ScheduleDTO>();
                    break;
                default:
                    scheduleDTOs = new List<ScheduleDTO>(); 
                    break;
            }
          
            return scheduleDTOs;
        }

        // StreamReader.ReadToEnd();

        private List<ScheduleDTO> DeserializeToJSON(string filePath)
        {
            string json = System.IO.File.ReadAllText(filePath);
            ScheduleListDTO listScheduleDTOs = JsonConvert.DeserializeObject<ScheduleListDTO>(json);
            List<ScheduleDTO> scheduleDTOs = listScheduleDTOs.ListOfScheduleInformation;
            return scheduleDTOs;
            
        }
    }
}
