﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace TimeTableApp_NEA
{
    class MainTimeTable
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"
███╗   ███╗██╗   ██╗████████╗██╗███╗   ███╗███████╗
████╗ ████║╚██╗ ██╔╝╚══██╔══╝██║████╗ ████║██╔════╝
██╔████╔██║ ╚████╔╝    ██║   ██║██╔████╔██║█████╗  
██║╚██╔╝██║  ╚██╔╝     ██║   ██║██║╚██╔╝██║██╔══╝  
██║ ╚═╝ ██║   ██║      ██║   ██║██║ ╚═╝ ██║███████╗
╚═╝     ╚═╝   ╚═╝      ╚═╝   ╚═╝╚═╝     ╚═╝╚══════╝
                                                   


Press any key to enter.");

            Console.WriteLine("");

            Console.ReadKey();
            Console.Clear();
            ViewOrCreate();

        }
        static void ViewOrCreate()
        {

            string option = "";

            while (option != "view" || option != "create")
            {
                Console.Clear();
                Console.WriteLine(@"Would you like to view or create a table today?
1.View
2.Create");
                option = Console.ReadLine();
                option = option.ToLower();


                if (option == "view")
                {
                    ViewTable();
                }
                if (option == "create")
                {
                    FileName();
                }
            }
        }
        static void SlotAmount(string filename)
        {
            Console.Clear();

            Console.WriteLine("Hello user. How many slots for activities will you want?");

            int numRows;
            int numColumns = 7;

            do
            {
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out numRows))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid integer.");
                }
            } while (true);

            Console.WriteLine(@"Are you happy with the amount of slots you have chosen for you activities? " + "(Slot amount: " + numRows + ") " +
"1.Yes" +
"2.No");
            string option = Console.ReadLine();
            option = option.ToLower();

            while (option != "yes")
            {
                Console.Clear();

                Console.WriteLine("Hello user. How many slots for activities will you want?");

                numRows = int.Parse(Console.ReadLine());
                Console.WriteLine(@"Are you happy with the amount of slots you have chosen for you activities? " + "(Slot amount: " + numRows + ") " +
"1.Yes" +
"2.No");

                Console.WriteLine("");

                option = Console.ReadLine();
            }

            if (option == "yes")
            {
                Console.Clear();
                ActivitiesAndTime(numColumns, numRows,filename);
            }
        }

        static void ActivitiesAndTime(int numColumns, int numRows,string filename)
        {
            Console.Clear();


            ActivitiesAndTimes activitiesAndTimes = new ActivitiesAndTimes(numRows, numColumns);
            int hoursValue, minutesValue;
            string activity;

            string[] days = new string[7] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            int count1 = 0, count2 = 0;

            for (int i = 0; i < numColumns * numRows; i++)
            {
                Console.Clear();

                Console.WriteLine(days[count1]);
                Console.WriteLine("");
                Console.WriteLine("Enter the hour the activity will take place. 00-23");

                do
                {
                    string userInput = Console.ReadLine();
                    if (int.TryParse(userInput, out hoursValue))
                    {
                        activitiesAndTimes.HoursValue(count1, count2, hoursValue);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid integer.");
                    }
                } while (true);

                Console.WriteLine("Enter the minute of the hour the activity will take place. 00-59");
                do
                {
                    string userInput = Console.ReadLine();
                    if (int.TryParse(userInput, out minutesValue))
                    {
                        activitiesAndTimes.MinutesValue(count1, count2, minutesValue);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid integer.");
                    }
                } while (true);



                Console.WriteLine(@"Are you happy with the times you have picked? 
1.Yes
2.No");
                string option = Console.ReadLine();
                option = option.ToLower();

                while (option != "yes" || minutesValue > 59 || minutesValue < 0 || hoursValue > 23 || hoursValue < 0)
                {
                    Console.Clear();

                    Console.WriteLine(days[count1]);
                    Console.WriteLine("Enter the hour the time will take place. 00-23");
                    do
                    {
                        string userInput = Console.ReadLine();
                        if (int.TryParse(userInput, out hoursValue))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid integer.");
                        }
                    } while (true);

                    Console.WriteLine("Enter the minute of the hour the activity will take place. 00-59");
                    do
                    {
                        string userInput = Console.ReadLine();
                        if (int.TryParse(userInput, out minutesValue))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid integer.");
                        }
                    } while (true);
                    Console.Clear();

                    Console.WriteLine(@"Are you happy with the times you have picked? 
1.Yes
2.No");
                    option = Console.ReadLine();
                }
                // Makes sure the Hours and Minutes are valid


                if (hoursValue <= 24 || minutesValue < 60 && hoursValue >= 0 || minutesValue > 0)
                {
                    activitiesAndTimes.HoursValue(count1, count2, hoursValue);
                    activitiesAndTimes.MinutesValue(count1, count2, minutesValue);
                }



                Console.Clear();
                if (minutesValue > 9)
                {
                    Console.WriteLine(days[count1] + " " + hoursValue + ":" + minutesValue);
                }
                if (minutesValue < 10)
                {
                    Console.WriteLine(days[count1] + " " + hoursValue + ":0" + minutesValue);
                }
                // makes sure the time is 1:02 instead of 1:2

                Console.WriteLine("Enter the activity for the time you have chosen.");
                activity = Console.ReadLine();

                Console.WriteLine(@"Are you happy with the activity you have picked?
1.Yes
2.No");
                string option2 = Console.ReadLine();
                option2 = option2.ToLower();

                while (option2 != "yes")
                {
                    Console.Clear();

                    if (minutesValue > 9)
                    {
                        Console.WriteLine(days[count1] + " " + hoursValue + ":" + minutesValue);
                    }
                    if (minutesValue < 10)
                    {
                        Console.WriteLine(days[count1] + " " + hoursValue + ":0" + minutesValue);
                    }
                    // makes sure the time is 1:02 instead of 1:2

                    Console.WriteLine("Enter the activity for the time you have chosen.");
                    activity = Console.ReadLine();
                    Console.Clear();

                    Console.WriteLine(@"Are you happy with the times you have picked?
1.Yes
2.No");
                    option2 = Console.ReadLine();
                }
                //Checks to see if the user is happy with the activity they choosen

                activitiesAndTimes.ActivitiesInput(count1, count2, activity);

                count2++;
                if (count2 % numRows == 0)
                {
                    count2 = 0;
                    count1++;
                }

            }
            //loops for time and activities 

            Console.Clear();
            Console.WriteLine();
            //prints table

            Console.WriteLine(@"Would you like to change any part of this timetable or would you like to make some comments on your table.
1.Change
2.Comment
3.Finish");
            string option3 = Console.ReadLine();
            option3 = option3.ToLower();

            if (option3 == "change")
            {
                BlockSelect(numRows);
            }
            if (option3 == "comment")
            {
                Comments();
            }
            if (option3 == "finish")
            {
                FinishTable(filename);
            }
        }

        static void BlockSelect(int numRows)
        {
            Console.WriteLine("Enter the number of which day you want to enter (Monday = 1 - Sunday = 7).");
            int Columns = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the number of which day you want to enter (slot = 1 - slot = " + numRows + ").");
            int Rows = int.Parse(Console.ReadLine());
            BlockDeleteAndReform(Rows, Columns);
        }

        static void BlockDeleteAndReform(int Rows, int Columns)
        {
            string[] days = new string[7] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            int numRows = 0, numColumns = 0;
            int hoursValue, minutesValue;
            ActivitiesAndTimes activitiesAndTimes = new ActivitiesAndTimes(numRows, numColumns);
            string activity;

            Console.WriteLine("Enter the hour the activity will take place. 00-23");
            do
            {
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out hoursValue))
                {
                    activitiesAndTimes.HoursValue(numColumns, numRows, hoursValue);
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid integer.");
                }
            } while (true);

            Console.WriteLine("Enter the minute of the hour the activity will take place. 00-59");
            do
            {
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out minutesValue))
                {
                    activitiesAndTimes.MinutesValue(numColumns, numRows, minutesValue);
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid integer.");
                }
            } while (true);



            Console.WriteLine(@"Are you happy with the times you have picked? 
1.Yes
2.No");
            string option = Console.ReadLine();
            option = option.ToLower();

            while (option != "yes" || minutesValue >= 60 || minutesValue > 0 || hoursValue >= 24 || hoursValue < 0)
            {
                Console.Clear();

                Console.WriteLine(days[numColumns]);
                Console.WriteLine("Enter the hour the time will take place. 00-23");
                do
                {
                    string userInput = Console.ReadLine();
                    if (int.TryParse(userInput, out hoursValue))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid integer.");
                    }
                } while (true);

                Console.WriteLine("Enter the minute of the hour the activity will take place. 00-59");
                do
                {
                    string userInput = Console.ReadLine();
                    if (int.TryParse(userInput, out minutesValue))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid integer.");
                    }
                } while (true);
                Console.Clear();

                Console.WriteLine(@"Are you happy with the times you have picked? 
1.Yes
2.No");
                option = Console.ReadLine();
            }
            // Makes sure the Hours and Minutes are valid


            if (hoursValue < 24 || minutesValue < 60 && hoursValue >= 0 || minutesValue >= 0)
            {
                activitiesAndTimes.HoursValue(numColumns, numRows, hoursValue);
                activitiesAndTimes.MinutesValue(numColumns, numRows, minutesValue);
            }



            Console.Clear();
            if (minutesValue > 9)
            {
                Console.WriteLine(days[numColumns] + " " + hoursValue + ":" + minutesValue);
            }
            if (minutesValue < 10)
            {
                Console.WriteLine(days[numColumns] + " " + hoursValue + ":0" + minutesValue);
            }
            // makes sure the time is 1:02 instead of 1:2

            Console.WriteLine("Enter the activity for the time you have chosen.");
            activity = Console.ReadLine();

            Console.WriteLine(@"Are you happy with the activity you have picked?
1.Yes
2.No");
            string option2 = Console.ReadLine();
            option2 = option2.ToLower();

            while (option2 != "yes")
            {
                Console.Clear();

                if (minutesValue > 9)
                {
                    Console.WriteLine(days[numColumns] + " " + hoursValue + ":" + minutesValue);
                }
                if (minutesValue < 10)
                {
                    Console.WriteLine(days[numColumns] + " " + hoursValue + ":0" + minutesValue);
                }
                // makes sure the time is 1:02 instead of 1:2

                Console.WriteLine("Enter the activity for the time you have chosen.");
                activity = Console.ReadLine();
                Console.Clear();

                Console.WriteLine(@"Are you happy with the times you have picked?
1.Yes
2.No");
                option2 = Console.ReadLine();
            }
            //Checks to see if the user is happy with the activity they choosen

            Console.WriteLine(@"Would you like to make more changes or are the you happy with the timetable.
1.Change
2.Complete");

            string option3 = Console.ReadLine();
            option3 = option3.ToLower();
            Console.Clear();

            while (option3 != "change" || option3 != "complete")
            {
                Console.WriteLine(@"Would you like to make more changes or are the you happy with the timetable.
1.Change
2.Complete");
                option3 = Console.ReadLine();
                option3 = option3.ToLower();
                Console.Clear();
            }
            if (option3 == "change")
            {
                BlockDeleteAndReform(Rows, Columns);
            }
            if (option3 != "complete")
            {
                Comments();
            }
        }

        static void Comments()
        {
            List<string> comments = new List<string>();

            do
            {
                Console.Write("Enter a comment or type stop to finish commenting. ");
                string comment = Console.ReadLine();

                if (comment.ToLower() == "stop")
                {
                    break;
                }

                comments.Add(comment);
            } while (true);

            Console.WriteLine("Comments List:");
            for (int i = 0; i < comments.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + comments[i]);
            }

            while (true)
            {
                Console.Write("Enter the number of the comment to remove (0 to exit): ");
                if (int.TryParse(Console.ReadLine(), out int selection))
                {
                    if (selection == 0)
                    {
                        break;
                    }

                    if (selection >= 1 && selection <= comments.Count)
                    {
                        comments.RemoveAt(selection - 1);
                        Console.WriteLine("Comment removed.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number or '0' to exit.");
                }
            }

            Console.WriteLine("Updated Comments List:");
            for (int i = 0; i < comments.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {comments[i]}");
            }
        }

        static void FileName()
        {
            Console.Clear();
            DateTime currentTime = DateTime.Now;

            Console.WriteLine(@"The current Date and Time is: " + currentTime +
". Enter in the starting day of your week. (dd/mm/yyyy).");
            string filename = Console.ReadLine();

            string currentTimeString = currentTime.ToString();
            // Make the current time into a string

            string currentDayString = currentTimeString.Substring(0, 2), currentMonthString = currentTimeString.Substring(3, 2), currentYearString = currentTimeString.Substring(6, 5);
            int currentDayInt = int.Parse(currentDayString), currentMonthInt = int.Parse(currentMonthString), currentYearInt = int.Parse(currentYearString);
            // Converting the current date into induvidual days months and years


            string dayString = filename.Substring(0, 2), monthString = filename.Substring(3, 2), YearString = filename.Substring(6, 4);

            int dayInt = int.Parse(dayString), monthInt = int.Parse(monthString), yearInt = int.Parse(YearString);
            // Converting the inputed date into induvidual days months and years

            while (dayInt > 31 && monthInt > 12 || dayInt < 1 && monthInt < 1)
            {
                Console.Clear();
                Console.WriteLine(@"The current Date and Time is: " + currentTime +
". Enter in a valid date (dd/mm/yyyy)."); ;
                filename = Console.ReadLine();

                dayString = filename.Substring(0, 2); monthString = filename.Substring(3, 2); YearString = filename.Substring(6, 4);
                dayInt = int.Parse(dayString); monthInt = int.Parse(monthString); yearInt = int.Parse(YearString);

            }
            //Checks the max month and max days and lowest days and lowest month int

            while ((dayInt > 31 && (monthInt == 1 || monthInt == 3 || monthInt == 5 || monthInt == 7 || monthInt == 8 || monthInt == 10 || monthInt == 12)) || (dayInt > 30 && (monthInt == 4 || monthInt == 6 || monthInt == 9 || monthInt == 11)) || (dayInt > 28 && monthInt == 2))
            {
                Console.Clear();
                Console.WriteLine("The current Date and Time is: " + currentTime + ". Enter a valid date (dd/mm/yyyy):");
                filename = Console.ReadLine();

                dayString = filename.Substring(0, 2);
                monthString = filename.Substring(3, 2);
                YearString = filename.Substring(6, 4);

                dayInt = int.Parse(dayString);
                monthInt = int.Parse(monthString);
                yearInt = int.Parse(YearString);
            }
            //Checks to see if the date enterd is correct from january-december


            while (currentDayInt - dayInt >= 0 && currentMonthInt - monthInt >= 0 && currentYearInt - yearInt >= 0)
            {
                Console.Clear();
                Console.WriteLine(@"The current Date and Time is: " + currentTime +
". Enter in a future or current date (dd/mm/yyyy).");
                filename = Console.ReadLine();

                dayString = filename.Substring(0, 2); monthString = filename.Substring(3, 2); YearString = filename.Substring(6, 4);
                dayInt = int.Parse(dayString); monthInt = int.Parse(monthString); yearInt = int.Parse(YearString);
            }
            Console.WriteLine(filename + " is a valid date");

            CreateFile createFile = new CreateFile();
            createFile.Create(filename);

            SlotAmount(filename);
        }

        static void FinishTable(string filename)
        {
            Console.WriteLine("Here is your finished Timetable for the date " + filename);
        }

        static void ViewTable()
        {
            Console.WriteLine("Enter the name of the file you would like to view.");
            string fileName = Console.ReadLine();
        }




    }
}
