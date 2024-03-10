using Newtonsoft.Json;
using Schedule_Project.DTOs;

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
        public List<ScheduleDTO> ListOfScheduleInformation(string filePath)
        {
            string json = System.IO.File.ReadAllText(filePath);

            List<ScheduleDTO> scheduleDTOs = JsonConvert.DeserializeObject<List<ScheduleDTO>>(json);
            return scheduleDTOs;
        }
    }
}
