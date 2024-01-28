using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using System.Management.Instrumentation;
using System.Diagnostics;

class ActivitiesAndTimes
{
    public string filePath;
    public string[] days;
    public string[,] activities;
    public int[,] hours;
    public int[,] minutes;
    public string[] table1;
    public string[,] table2;
    public List<string> comments;
    
    public ActivitiesAndTimes(int numRows, int numColumns)
    {

        activities = new string[numColumns, numRows];
        hours = new int[numColumns, numRows];
        minutes = new int[numColumns, numRows];
        days = new string[7] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        table1 = new string[numRows+1 * 5];
        table2 = new string[numRows,numRows* 5];

    }
    public void HoursValue(int numRows, int columns, int value)
    {
        if (numRows >= 0 && numRows < hours.GetLength(0) && columns >= 0 && columns < hours.GetLength(1))
        {
            hours[numRows, columns] = value;
        }
        else
        {
            Console.WriteLine("The inputted row or column is invalid");
        }
        //Gets the input for hours then stores in inside the 2d array hours
    }
    public void MinutesValue(int numRows, int columns, int value)
    {
        if (numRows >= 0 && numRows < minutes.GetLength(0) && columns >= 0 && columns < minutes.GetLength(1))
        {
            minutes[numRows, columns] = value;
        }
        else
        {
            Console.WriteLine("The inputted row or column is invalid.");
        }
        //Gets the input for minutes then stores in inside the 2d array minutes
    }
    public void ActivitiesInput(int numRows, int columns, string value)
    {
        if (numRows >= 0 && numRows < activities.GetLength(0) && columns >= 0 && columns < activities.GetLength(1))
        {
            activities[numRows, columns] = value;
        }
        else
        {
            Console.WriteLine("The inputted row or column is invalid.");
        }
        //Gets the activities then stores in inside the 2d array activities 
    }
    
    public void block(int[,] hours, int[,] minutes, string[] days, string[,] activities, int numRows, int numColumns,List<string> comment)
    {
        numColumns = 7;
        int count1 = 0;
        int count2 = 0;
        int newRow = 0;
        int activitiesAndTimeRow = 0;

        StringBuilder repeatLines = new StringBuilder();
        //string manipulator
        string line = ""; string line1 = ""; string line2 = ""; string line3 = ""; string line4 = "";
        for (int i = 0; i < numRows - 1; i++)
        {

            if (i % 2 == 0)
            {
                activitiesAndTimeRow = 0;
            }
            if (i % 2 == 1)
            {
                activitiesAndTimeRow += 1;
            }
            

            for (int a = 0; a < 7; a++)
            {
                line = $@"_______________ ";
                repeatLines.Append(line);
                //top of box
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
                    count1++;
                    count2 = 0;
                }
            }
            line1 = repeatLines.ToString();
            repeatLines.Clear();

            for (int c = 0; c < 7; c++)
            {
                line2 = $@"| {hours[c, activitiesAndTimeRow].ToString().PadLeft(2, '0')}:{minutes[c, activitiesAndTimeRow].ToString().PadLeft(2, '0')}       | ";
                //uses pad left to make sure that the time appears correctly instead of 1:2 its may show 01:02
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
                //bottom of box
                repeatLines.Append(line4);
            }
            line4 = repeatLines.ToString();
            repeatLines.Clear();
            
            //prints the boxes horizontal instead of vertical



            table2[i,newRow] = line;
            table2[i,newRow+1] = line1;
            table2[i,newRow+2] = line2;
            table2[i,newRow+3] = line3;
            table2[i,newRow+4] = line4;
            //makes sure to store each rown in a new place so it doesnt overwrite itself

            newRow += 5;
            comments = comment;

        }

    }
    public void PrintTable(int numRows, List<string> comment)
    {
        comments = comment;
        int newRow = 0;
        for (int i = 0; i < numRows; i++)
        {
 

            Console.WriteLine("");
            Console.WriteLine(table2[i, newRow]);
            Console.WriteLine(table2[i,newRow + 1]);
            Console.WriteLine(table2[i,newRow + 2]);
            Console.WriteLine(table2[i,newRow + 3]);
            Console.WriteLine(table2[i,newRow + 4]);
            newRow += 5;
            // Prints the table
        }

        Console.WriteLine("\nComments List:");
        if (comments.Count == 0)
        {
            Console.WriteLine("No comments available.");
        }
        else
        {
            for (int i = 0; i < comments.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + comments[i]);
            }
            //prints the comments line by line untill there arent anymore to print
        }
    }
   


    public void CreateFile(string fileName, int numColumns, int numRows, List<string> comment)
    {
        comments = comment;
        string filePath;
        string fileExtension = ".txt";
      
                        
        string path = @"C:\Users\Ben09\Source\Repos\benw0903\NEA_TimeTable_2023_new\NEA_TimeTable_2023\bin\Debug\";
        filePath = Path.Combine(path, fileName + fileExtension);


        string directoryPath = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        //if the directory does not exitst it will make one

        using (FileStream fs = File.Create(filePath))
        {
                
            Console.WriteLine("File " + filePath + " created or opened successfully.");

            using (StreamWriter sw = new StreamWriter(fs))
            {
                int newRow = 0;
                for (int i = 0; i < numRows; i++)
                {


                    sw.WriteLine("");
                    sw.WriteLine(table2[i, newRow]);
                    sw.WriteLine(table2[i, newRow + 1]);
                    sw.WriteLine(table2[i, newRow + 2]);
                    sw.WriteLine(table2[i, newRow + 3]);
                    sw.WriteLine(table2[i, newRow + 4]);
                    newRow += 5;
                    // Prints the table
                }
                sw.WriteLine("");
                sw.WriteLine("Comments List:");
                if (comments.Count == 0)
                {
                    sw.WriteLine("No comments available.");
                }
                else
                {
                    for (int i = 0; i < comments.Count; i++)
                    {
                        sw.WriteLine((i + 1) + ". " + comments[i]);
                    }
                    //prints comments line by line untill there isnt anymore
                }
            }
        }
        
        


    }
}
