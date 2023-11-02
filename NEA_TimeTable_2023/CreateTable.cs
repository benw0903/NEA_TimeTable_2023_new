using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TimeTableApp_NEA
{

    class CreateFile
    {
        public void Create(string fileName)
        {
            try
            {
                string fileExtension = ".txt";
                string filePath = fileName + fileExtension;
                FileStream fileStream = File.Open(filePath, FileMode.Create);


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

    class CreateTable
    {
        public string[,] TableRowAndColumns { get; }

        public CreateTable(int rows, int columns)
        {
            TableRowAndColumns = new string[columns, rows];
        }


    }

    class ActivitiesAndTimes
    {
        public string[] days;
        public string[,] activities;
        public int[,] hours;
        public int[,] minutes;
        public string[,] table;


        public ActivitiesAndTimes(int numRows, int numColumns)
        {

            activities = new string[numColumns, numRows];
            hours = new int[numColumns, numRows];
            minutes = new int[numColumns, numRows];
            days = new string[7] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

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

        public void block(int[,] hours, int[,] minutes, string[] days, string[,] activities, int numRows, int numColumns)
        {
            table = new string[numColumns, numRows];
            int count1 = 0;
            int count2 = 0;

            for (int i = 0; i < numColumns * numRows; i++)
            {
                if (count2 == 0)
                {
                    if (hours[count1, count2] < 10)
                    {
                        table[count1, count2] = @"____________
/   " + days[count1] + @"          \
|   " + "0" + hours + ":" + minutes + @"     |
|   " + activities + @"          |
|             |
|             |
|             | 
\_____________/";
                    }

                    if (hours[count1, count2] > 9)
                    {
                        table[count1, count2] = @"____________
/   " + days[count1] + @"          \
|   " + hours + ":" + minutes + @"      |
|   " + activities + @"          |
|             |
|             |
|             | 
\_____________/";
                    }
                }

                if (hours[count1, count2] > 9)
                {
                    table[count1, count2] = @"____________
/   " + days[count1] + @"          \
|   " + hours + ":" + minutes + @"      |
|   " + activities + @"          |
|             |
|             |
|             | 
\_____________/";
                }

                else
                {
                    table[count1, count2] = @"____________
/   " + days[count1] + @"          \
|   " + "0" + hours + ":" + minutes + @"     |
|   " + activities + @"          |
|             |
|             |
|             | 
\_____________/";
                }

                count2++;
                if (count2 % numRows == 0)
                {
                    count2 = 0;
                    count1++;
                }

            }
        }
        public void PrintTable()
        {
            for (int numRows = 0; numRows < table.GetLength(0); numRows++)
            {
                for (int numColumns = 0; numColumns < table.GetLength(1); numColumns++)
                {
                    Console.Write(table[numRows, numColumns]);
                    Console.Write("  ");
                }
                Console.WriteLine();
            }
        }
    }

    
}