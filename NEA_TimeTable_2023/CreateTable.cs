using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TimeTableApp_NEA
{
    class CreateTable
    {
        public string[,] TableRowAndColumns { get; }

        public CreateTable(int rows, int columns)
        {
            TableRowAndColumns = new string[columns, rows];
        }


    }

    class CreateFile
    {
        public void Create(string fileName)
        {
            try
            {
                string fileExtension = ".txt";
                string fullFilePath = fileName + fileExtension;


                using (FileStream fileStream = File.Open(fullFilePath, FileMode.OpenOrCreate))
                {

                }

                Console.Clear();
                Console.WriteLine("File " + fullFilePath + " created or opened successfully.");
            }
            catch (Exception error)
            {
                Console.Clear();
                Console.WriteLine("Error creating or opening the file: " + error.Message);
            }
        }
    }

    class block
    {
        block()
        {

        }
    }

    class ActivitiesAndTimes
    {
        public string[] days { get; set; }
        public string[,] activities { get; }
        public int[,] hours { get; }
        public int[,] minutes { get; }

        //This conctuctor initializes the Arrays
        ActivitiesAndTimes(int rows, int columns)
        {

            activities = new string[columns, rows];
            hours = new int[columns, rows];
            minutes = new int[columns, rows];
            days = new string[7] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    hours[i, j] = i * columns + j;
                    minutes[i, j] = i * columns + j;
                    activities[i, j] = $"Row {i}, Col {j}";
                }
            }
        }
        public void HoursValue(int rows, int columns, int value)
        {
            if (rows >= 0 && rows < hours.GetLength(0) && columns >= 0 && columns < hours.GetLength(1))
            {
                hours[rows, columns] = value;
            }
            else
            {
                Console.WriteLine("Invalid row or column.");
            }
        }

        public void MinutesValue(int rows, int columns, int value)
        {
            if (rows >= 0 && rows < minutes.GetLength(0) && columns >= 0 && columns < minutes.GetLength(1))
            {
                minutes[rows, columns] = value;
            }
            else
            {
                Console.WriteLine("Invalid row or column.");
            }
        }


        public void ActivitiesInput(int rows, int columns, string value)
        {
            if (rows >= 0 && rows < activities.GetLength(0) && columns >= 0 && columns < activities.GetLength(1))
            {
                activities[rows, columns] = value;
            }
            else
            {
                Console.WriteLine("Invalid row or column.");
            }
        }
    }

    class activities
    {
        activities()
        {

        }
    }
}