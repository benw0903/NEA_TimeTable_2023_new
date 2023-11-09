using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;


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
                Console.WriteLine("Would you like to view or create a table today?");
                Console.WriteLine("1.View");
                Console.WriteLine("2.Create");

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

            Console.WriteLine(@"Are you happy with the amount of slots you have chosen for you activities?");
            Console.WriteLine("1.Yes");
            Console.WriteLine("2.No");

            Console.WriteLine("(Slot amount: " + numRows + ")");
            Console.WriteLine("");
            string option = Console.ReadLine();
            option = option.ToLower();

            while (option != "yes")
            {
                Console.Clear();

                Console.WriteLine("Hello user. How many slots for activities will you want?");

                numRows = int.Parse(Console.ReadLine());
                Console.WriteLine(@"Are you happy with the amount of slots you have chosen for you activities?");
                Console.WriteLine("1.Yes");
                Console.WriteLine("2.No");

                Console.WriteLine("(Slot amount: " + numRows + ")");
                Console.WriteLine("");

                option = Console.ReadLine();
            }

            if (option == "yes")
            {
                Console.Clear();
                ActivitiesAndTime(numColumns, numRows, filename);
            }
        }

        static void ActivitiesAndTime(int numColumns, int numRows, string filename)
        {
            Console.Clear();


            ActivitiesAndTimes schedule = new ActivitiesAndTimes(numRows, numColumns);
            int hoursValue, minutesValue;
            string activity;
            int[,] hours = new int[numColumns, numRows];
            int[,] minutes = new int[numColumns, numRows];
            string[,] activities = new string[numColumns, numRows];
            

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
                        schedule.HoursValue(count1, count2, hoursValue);
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
                        schedule.MinutesValue(count1, count2, minutesValue);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid integer.");
                    }
                } while (true);



                Console.WriteLine(@"Are you happy with the times you have picked?");
                Console.WriteLine("1.Yes");
                Console.WriteLine("2.No");

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

                    Console.WriteLine(@"Are you happy with the times you have picked?");
                    Console.WriteLine("1.Yes");
                    Console.WriteLine("2.No");

                    option = Console.ReadLine();
                }
                // Makes sure the Hours and Minutes are valid


                if (hoursValue <= 24 || minutesValue < 60 && hoursValue >= 0 || minutesValue > 0)
                {
                    schedule.HoursValue(count1, count2, hoursValue);
                    schedule.MinutesValue(count1, count2, minutesValue);
                    hours[count1, count2]=hoursValue;
                    minutes[count1, count2]=minutesValue;
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

                Console.WriteLine(@"Are you happy with the activity you have picked?");
                Console.WriteLine("1.Yes");
                Console.WriteLine("2.No");

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

                    Console.WriteLine(@"Are you happy with the times you have picked?");
                    Console.WriteLine("1.Yes");
                    Console.WriteLine("2.No");

                    option2 = Console.ReadLine();
                }
                //Checks to see if the user is happy with the activity they chosen

                schedule.ActivitiesInput(count1, count2, activity);
                activities[count1, count2] = activity;

                count2++;
                if (count2 % numRows == 0)
                {
                    count2 = 0;
                    count1++;
                }

            }
            //loops for time and activities 

            Console.Clear();
            schedule.block(schedule.hours, schedule.minutes, schedule.days, schedule.activities, numRows, numColumns);
            schedule.PrintTable();
            //prints table

            Console.WriteLine("");
            Console.WriteLine(@"Would you like to change any part of this timetable or would you like to make some comments on your table.");
            Console.WriteLine("1.Change");
            Console.WriteLine("2.Comment");
            Console.WriteLine("3.Finish");

            string option3 = Console.ReadLine();
            option3 = option3.ToLower();

            while (option3 != "change" && option3 != "comment" && option3 != "finish")
            {
                Console.WriteLine(@"Would you like to make more changes or are the you happy with the timetable.
1.Change
2.Comment
3.Finish");
                option3 = Console.ReadLine();
                option3 = option3.ToLower();
                Console.Clear();
            }

            if (option3 == "change")
            {
                BlockSelect(numRows, numColumns, filename,hours,minutes,activities);
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

        static void BlockSelect(int numRows, int numColumns, string filename, int[,] hours,int[,] minutes, string[,] activities)
        {
            Console.Clear();
            Console.WriteLine("Enter the number of which day you want to enter (Monday = 1 - Sunday = 7).");
            int columns = int.Parse(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Enter the number of which day you want to enter (slot = 1 - slot = " + numRows + ").");
            int rows = int.Parse(Console.ReadLine());
            Console.Clear();

            while (columns < 0 || columns > numColumns || rows < 0 || columns > numColumns)
            {
                Console.WriteLine("Enter the number of which day you want to enter (Monday = 1 - Sunday = 7).");
                columns = int.Parse(Console.ReadLine());
                Console.Clear();

                Console.WriteLine("Enter the number of which day you want to enter (slot = 1 - slot = " + numRows + ").");
                rows = int.Parse(Console.ReadLine());
                Console.Clear();
            }

            if (columns < 0 || columns > numColumns || rows < 0 || columns > numColumns)
            {
                Console.WriteLine(@"column:" + columns + "" +
@"rows:" + rows
+ "Are valid");
            }

            BlockDeleteAndReform(numRows,numColumns,rows, columns, filename,hours,minutes,activities);
        }

        static void BlockDeleteAndReform(int numRows, int numColumns,int rows, int columns,string filename, int[,] hours, int[,] minutes, string[,] activities)
        {
            ActivitiesAndTimes schedule = new ActivitiesAndTimes(rows, columns);
            string[] days = new string[7] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            numRows = 0; numColumns = 0;
            int hoursValue, minutesValue;
            string activity;
            
            Console.WriteLine("Day:"+ days[columns-1]);
            Console.WriteLine("Enter the hour the activity will take place. 00-23");

            do
            {
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out hoursValue))
                {
                    schedule.HoursValue(numColumns, numRows, hoursValue);
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
                    schedule.MinutesValue(numColumns, numRows, minutesValue);
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid integer.");
                }
            } while (true);



            Console.WriteLine(@"Are you happy with the times you have picked?");
            Console.WriteLine("1.Yes");
            Console.WriteLine("2.No");


            string option = Console.ReadLine();
            option = option.ToLower();

            while (option != "yes" || minutesValue >= 60 || minutesValue < 0 || hoursValue >= 24 || hoursValue < 0)
            {
                Console.Clear();

                Console.WriteLine(days[columns]);
                Console.WriteLine("Enter the hour the time will take place. 00-23");

                bool validInput = false;
                while (!validInput)
                {
                    string userInput = Console.ReadLine();
                    if (int.TryParse(userInput, out hoursValue) && hoursValue >= 0 && hoursValue <= 23)
                    {
                        schedule.HoursValue(columns, rows, hoursValue);
                        validInput = true; 
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid integer. 00-23");
                    }
                }

                Console.WriteLine("Enter the minute of the hour the activity will take place. 00-59");

                bool validInput2 = false;
                while (!validInput2)
                {
                    string userInput = Console.ReadLine();
                    if (int.TryParse(userInput, out minutesValue))
                    {
                        schedule.HoursValue(columns, rows, hoursValue);
                        validInput2 = true;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid integer.00-59");
                    }
                } 

                Console.Clear();

                Console.WriteLine(@"Are you happy with the times you have picked? 
1.Yes
2.No");
                option = Console.ReadLine();
            }
            // Makes sure the Hours and Minutes are valid


            if (hoursValue < 24 || minutesValue < 60 && hoursValue >= 0 || minutesValue >= 0)
            {
                schedule.HoursValue(columns, rows, hoursValue);
                schedule.MinutesValue(columns, rows, minutesValue);
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

            Console.Clear();
            schedule.block(schedule.hours, schedule.minutes, schedule.days, schedule.activities, rows-1, columns-1);
            schedule.PrintTable();
            //prints table

            Console.WriteLine(@"Would you like to make more changes or are the you happy with the timetable.
1.Change
2.Comment
3.Finish");

            string option3 = Console.ReadLine();
            option3 = option3.ToLower();
            Console.Clear();

            while (option3 != "change" || option3 != "comment" || option3 != "finish")
            {
                Console.WriteLine(@"Would you like to make more changes or are the you happy with the timetable.
1.Change
2.Comment
3.Finish");
                option3 = Console.ReadLine();
                option3 = option3.ToLower();
                Console.Clear();
            }
            if (option3 == "change")
            {
                BlockDeleteAndReform(numRows,numColumns,rows,columns,filename,hours,minutes,activities);
            }
            if(option3 == "comment")
            {
                Comments();
            }
            if (option3 != "finish")
            {
                FinishTable(filename);
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
                Console.Write(@"Enter the number of the comment to remove.  
If you are finished then you will be able to exit by entering (0) ");
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
            string fileName = Console.ReadLine();

            CheckDateFormat(fileName);
            while (CheckDateFormat(fileName) != true)
            {
                Console.WriteLine("Make sure the enter the date in the correct format. (dd/mm/yyyy)");
                fileName = Console.ReadLine();
            }

            string currentTimeString = currentTime.ToString();
            // Make the current time into a string

            string currentDayString = currentTimeString.Substring(0, 2), currentMonthString = currentTimeString.Substring(3, 2), currentYearString = currentTimeString.Substring(6, 5);
            int currentDayInt = int.Parse(currentDayString), currentMonthInt = int.Parse(currentMonthString), currentYearInt = int.Parse(currentYearString);
            // Converting the current date into induvidual days months and years


            string dayString = fileName.Substring(0, 2), monthString = fileName.Substring(3, 2), YearString = fileName.Substring(6, 4);

            int dayInt = int.Parse(dayString), monthInt = int.Parse(monthString), yearInt = int.Parse(YearString);
            // Converting the inputed date into induvidual days months and years

            while ((dayInt > 31 && (monthInt == 1 || monthInt == 3 || monthInt == 5 || monthInt == 7 || monthInt == 8 || monthInt == 10 || monthInt == 12)) || (dayInt > 30 && (monthInt == 4 || monthInt == 6 || monthInt == 9 || monthInt == 11)) || (dayInt > 28 && monthInt == 2))
            {
                Console.Clear();
                Console.WriteLine("The current Date and Time is: " + currentTime + ". Enter a valid date (dd/mm/yyyy):");
                fileName = Console.ReadLine();

                dayString = fileName.Substring(0, 2);
                monthString = fileName.Substring(3, 2);
                YearString = fileName.Substring(6, 4);

                dayInt = int.Parse(dayString);
                monthInt = int.Parse(monthString);
                yearInt = int.Parse(YearString);
            }
            //Checks to see if the date enterd is correct from january-december

            while (dayInt > 31 && monthInt > 12 || dayInt < 1 && monthInt < 1)
            {
                Console.Clear();
                Console.WriteLine(@"The current Date and Time is: " + currentTime +
". Enter in a valid date (dd/mm/yyyy)."); ;
                fileName = Console.ReadLine();

                dayString = fileName.Substring(0, 2); monthString = fileName.Substring(3, 2); YearString = fileName.Substring(6, 4);
                dayInt = int.Parse(dayString); monthInt = int.Parse(monthString); yearInt = int.Parse(YearString);

            }
            //Checks the max month and max days and lowest days and lowest month int


            while (currentDayInt - dayInt >= 0 && currentMonthInt - monthInt >= 0 && currentYearInt - yearInt >= 0)
            {
                Console.Clear();
                Console.WriteLine(@"The current Date and Time is: " + currentTime +
". Enter in a future or current date (dd/mm/yyyy).");
                fileName = Console.ReadLine();

                dayString = fileName.Substring(0, 2); monthString = fileName.Substring(3, 2); YearString = fileName.Substring(6, 4);
                dayInt = int.Parse(dayString); monthInt = int.Parse(monthString); yearInt = int.Parse(YearString);
            }

            Console.WriteLine(fileName + "  is a valid date.");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            CreateFile createFile = new CreateFile();
            createFile.Create(fileName);

            SlotAmount(fileName);
        }

        static bool CheckDateFormat(string fileName)
        { 
            string regexDate = @"^\d{2}/\d{2}/\d{4}$";
            if (Regex.IsMatch(fileName, regexDate))
            {
                return true;
            }
            return false;
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
