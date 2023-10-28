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

    class block
    {
        public string[,] table;

        block(int[,]hours, int[,] minutes,string[] days, string[,] activities, int numRows, int numColumns)
        {
            table = new string[numColumns, numRows];
            string printTable;
            int count1 = 0;
            int count2 = 0;

            for(int i = 0; i < numColumns*numRows; i++)
            {
                if(count2 == 0)
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

                if (hours[count1,count2] > 9)
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
/             \
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
            printTable = ""+ table[1,1] + "" + "" + table[1, 2] + "" + "" + table[1, 3] + "" + "" + table[1, 4] + "" + "" + table[1, 5] + "" + "" + table[1, 6] + "" + "" + table[1, 7] + @"" +
"" + table[2, 1] + "" + "" + table[2, 2] + "" + "" + table[2, 3] + "" + "" + table[2, 4] + "" + "" + table[2, 5] + "" + "" + table[2, 6] + "" + "" + table[2, 7] + @""+ 
"" + table[3, 1] + "" + "" + table[3, 2] + "" + "" + table[3, 3] + "" + "" + table[3, 4] + "" + "" + table[3, 5] + "" + "" + table[3, 6] + "" + "" + table[3, 7] + @""+
"" + table[4, 1] + "" + "" + table[4, 2] + "" + "" + table[4, 3] + "" + "" + table[4, 4] + "" + "" + table[4, 5] + "" + "" + table[4, 6] + "" + "" + table[4, 7] + @""+
"" + table[5, 1] + "" + "" + table[5, 2] + "" + "" + table[5, 3] + "" + "" + table[5, 4] + "" + "" + table[5, 5] + "" + "" + table[5, 6] + "" + "" + table[5, 7] + @""+
"" + table[6, 1] + "" + "" + table[6, 2] + "" + "" + table[6, 3] + "" + "" + table[6, 4] + "" + "" + table[6, 5] + "" + "" + table[6, 6] + "" + "" + table[6, 7] + @""+
"" + table[7, 1] + "" + "" + table[7, 2] + "" + "" + table[7, 3] + "" + "" + table[7, 4] + "" + "" + table[7, 5] + "" + "" + table[7, 6] + "" + "" + table[7, 7] + @"";
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
    }

    
}