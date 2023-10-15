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
                string filePath = fileName + fileExtension;


                using (FileStream fileStream = File.Open(filePath, FileMode.OpenOrCreate))
                {

                }

                Console.Clear();
                Console.WriteLine("File " + filePath + " created or opened successfully.");
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
        public string[] days;
        public string[,] activities;
        public int[,] hours;
        public int[,] minutes;

        
        public ActivitiesAndTimes(int numRows, int numColumns)
        {

            activities = new string[numColumns, numRows];
            hours = new int[numColumns, numRows];
            minutes = new int[numColumns, numRows];
            days = new string[7] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

            for (int i = 0; i < numRows* numColumns; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    
                    hours[i, j] = i * numColumns + j;
                    minutes[i, j] = i * numColumns + j;
                    activities[i, j] = $"Row {i}, Col {j}";
                }
            }
        }
        public void HoursValue(int numRows, int columns, int value)
        {
            if (numRows >= 0 && numRows < hours.GetLength(0) && columns >= 0 && columns < hours.GetLength(1))
            {
                hours[numRows, columns] = value;
            }
            else
            {
                Console.WriteLine("Invalid row or column.");
            }
        }

        public void MinutesValue(int numRows, int columns, int value)
        {
            if (numRows >= 0 && numRows < minutes.GetLength(0) && columns >= 0 && columns < minutes.GetLength(1))
            {
                minutes[numRows, columns] = value;
            }
            else
            {
                Console.WriteLine("Invalid row or column.");
            }
        }


        public void ActivitiesInput(int numRows, int columns, string value)
        {
            if (numRows >= 0 && numRows < activities.GetLength(0) && columns >= 0 && columns < activities.GetLength(1))
            {
                activities[numRows, columns] = value;
            }
            else
            {
                Console.WriteLine("Invalid row or column.");
            }
        }
    }


}