using AbsenMVC.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace absenxaml.Manager
{
    public class Utils
    {
        public static List<String> GetListHari()
        {
            return new List<String>
            {
                "Senin", 
                "Selasa",
                "Rabu",
                "Kamis",
                "Jumat"
            };  
        }

        public static void ShowMBWarning(string message)
        {
            MessageBox.Show(message, "Warning !", MessageBoxButton.OK, MessageBoxImage.Exclamation);

        }
        public static void ShowMBInfo(string message)
        {
            MessageBox.Show(message, "Warning !", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static List<String> GetListMatkulName()
        {
            MatkulManager matkulManager = new MatkulManager();
            var list = new List<String>();
            List<Matkul> listMatkul = matkulManager.getMatkul().AsQueryable().ToList();
            foreach (var item in listMatkul)
            {
                list.Add(item.Nama);
            }
            return list;
        }

        public static string GetCurrentLocalDay()
        {
            DateTime currentDate = DateTime.Now;
            string day = currentDate.DayOfWeek.ToString();
            switch (day.ToLower())
            {
                case "monday":
                    return "Senin";
                case "tuesday":
                    return "Selasa";
                case "wednesday":
                    return "Rabu";
                case "thursday":
                    return "Kamis";
                case "friday":
                    return "Jumat";
                case "saturday":
                    return "Jumat";
                default:
                    return "Hari tidak valid";
            }
        }

        public static TimeSpan StringToTimeSpan(string str)
        {
            string[] parts = str.Split(':');
            if (parts.Length == 2 && int.TryParse(parts[0], out int hours) && int.TryParse(parts[1], out int minutes))
            {
                return new TimeSpan(hours, minutes, 0);
            }

            // Handle case when parsing fails (return default TimeSpan or throw exception)
            return TimeSpan.Zero;
        }    
    }
}
