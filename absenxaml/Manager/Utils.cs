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
    }
}
