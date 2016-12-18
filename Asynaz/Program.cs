using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using CsvHelper;

namespace Asynaz { 

    class Program
    {
        static int countingofworkingdays()
        {
            int counter = 0;
            using (StreamReader reader = File.OpenText("calendar.csv"))
            {
                var csvr = new CsvReader(reader);
                while (csvr.Read())
                {
                    var isworkingday = csvr.GetField<int>(1);
                    if (isworkingday == 1)
                    {
                        counter += 1;
                    }
                }
            }
            return counter;
        }
        static int countingofworkers()
        {
            
            int wcount = System.IO.File.ReadAllLines("workers.txt").Length;
            return wcount;
        }
       static int countingbmoneyforone()
        {
            int forone = 0;
            using (StreamReader reader = File.OpenText("bmoney.txt"))
            {
                forone = int.Parse(reader.ReadLine())/countingofworkers();
            }
            return forone;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Приветствуем вас в программе АСиНаЗ, вы хотите начать процесс зачисления? (yes/no)");
            string yesno = Console.ReadLine();
            if (yesno == "no")
            {
                Environment.Exit(0);
            }
            Console.WriteLine("Считаем количество работников...");
            var workercount = countingofworkers();
            Console.WriteLine("Вычисляем количество рабочих дней...");
            var workingdays = countingofworkingdays();
            Console.WriteLine("Считаем черную зарплату...");
            var bmoneyforone = countingbmoneyforone();

            using (StreamReader reader = File.OpenText("workers.txt"))
            {
                var csvr = new CsvReader(reader);
                using (StreamWriter writer = File.CreateText("output.txt"))
                {
                    
                    var csvw = new CsvWriter(writer);
                    for (int ii = 1; ii < countingofworkers(); ii++)
                    {
                        csvr.Read();
                        var money = csvr.GetField<int>(1);
                        var id = csvr.GetField<string>(0);
                        Console.WriteLine("Считаем зарплату работника с id " + id);
                        money = money * workingdays;
                        double salary = money - 0.13 * money - 0.22 * money - 0.029 * money - 0.051 * money - 0.002 * money;//
                        writer.WriteLine(id + "," + Convert.ToString(salary));
                        Console.WriteLine("Есть");
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
