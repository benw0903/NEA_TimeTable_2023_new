using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace TimeTableApp_NEA
{
    class ActivitiesAndTimes
    {
        public string filePath;
        public string[] days;
        public string[,] activities;
        public int[,] hours;
        public int[,] minutes;
        public string []table;


        public ActivitiesAndTimes(int numRows, int numColumns)
        {

            activities = new string[numColumns, numRows];
            hours = new int[numColumns, numRows];
            minutes = new int[numColumns, numRows];
            days = new string[7] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            table = new string[100*5];

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
            numColumns = 7;
            
            int count2 = 0;
            int newRow = 0;
            int count1 = 0;
            int activitiesAndTimeRow = 0;
            

            StringBuilder repeatLines = new StringBuilder();
            //puts string together
            string line = ""; string line1 = ""; string line2 = ""; string line3 = ""; string line4 = "";
            for (int i = 0; i < numRows-1; i++)
            {
                
                if(numRows == 0)
                {
                    activitiesAndTimeRow = 0;
                }
                if(numRows >= 1)
                {
                    activitiesAndTimeRow += 1;
                }
                


                //puts string together

                for (int a = 0; a < 7; a++)
                {
                    line = $@"_______________ ";
                    repeatLines.Append(line);
                }
                line = repeatLines.ToString();
                repeatLines.Clear();

                for (int b = 0; b < 7; b++)
                {
                    line1 = $@"| {days[b],-11} | ";
                    repeatLines.Append(line1);
                    count2++;
                    if (count2 % numRows == 0)
                    {
                        count2 = 0;
                    }
                }
                line1 = repeatLines.ToString();
                repeatLines.Clear();

                for (int c = 0; c < 7; c++)
                {
                    line2 = $@"| {hours[c, activitiesAndTimeRow].ToString().PadLeft(2, '0')}:{minutes[c, activitiesAndTimeRow].ToString().PadLeft(2, '0')}       | ";
                    repeatLines.Append(line2);
                }
                line2 = repeatLines.ToString();
                repeatLines.Clear();

                for (int d = 0; d < 7; d++)
                {

                    line3 = $@"| {activities[d, activitiesAndTimeRow],-11} | ";
                    repeatLines.Append(line3);
                    
                }
                line3 = repeatLines.ToString();
                repeatLines.Clear();

                for (int f = 0; f < 7; f++)
                {
                    line4 = $@"|_____________| ";
                    repeatLines.Append(line4);
                }
                line4 = repeatLines.ToString();
                repeatLines.Clear();
                //prints the boxes horizontal instead of vertical

                    

                table[newRow + 0] = line;
                table[newRow + 1] = line1;
                table[newRow + 2] = line2;
                table[newRow + 3] = line3;
                table[newRow + 4] = line4;


                if (numRows == 0)
                {
                    newRow = 0;
                }
                if (numRows >= 1)
                {
                    newRow += 5;
                }
                newRow += 5;
                
            }
            
        }

        public void PrintTable(int numRows)
        {
            int newRow = 0;
            for (int i = 0; i < numRows; i++)
            {
                Console.WriteLine(table[newRow + 0]);
                Console.WriteLine(table[newRow + 1]);
                Console.WriteLine(table[newRow + 2]);
                Console.WriteLine(table[newRow + 3]);
                Console.WriteLine(table[newRow + 4]);
                Console.WriteLine("");
               

                newRow += 5;
            }
            // Print the table
        }
        public void CreateFile(string fileName)
        {
            try
            {
                string fileExtension = ".txt";
                filePath = fileName + fileExtension;
                FileStream fs = File.Create(filePath);

                Console.Clear();
                Console.WriteLine("File " + filePath + " created or opened successfully.");

                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine(table[0]);
                    sw.WriteLine(table[1]);
                    sw.WriteLine(table[2]);
                    sw.WriteLine(table[3]);
                    sw.WriteLine(table[4]);
                    sw.WriteLine("");
                }
            }
            catch (Exception error)
            {
                Console.Clear();
                Console.WriteLine("Error creating the file: " + error.Message);
            }


        }

    }
}