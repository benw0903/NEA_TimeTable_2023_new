using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices.ComTypes;
using System.ComponentModel;

namespace TimeTableApp_NEA
{
    internal class Program
    {
        static List<string> comments;

        static void Main(string[] args)
        {

            Console.WriteLine(@"
███╗   ███╗██╗   ██╗████████╗██╗███╗   ███╗███████╗
████╗ ████║╚██╗ ██╔╝╚══██╔══╝██║████╗ ████║██╔════╝
██╔████╔██║ ╚████╔╝    ██║   ██║██╔████╔██║█████╗  
██║╚██╔╝██║  ╚██╔╝     ██║   ██║██║╚██╔╝██║██╔══╝  
██║ ╚═╝ ██║   ██║      ██║   ██║██║ ╚═╝ ██║███████╗
╚═╝     ╚═╝   ╚═╝      ╚═╝   ╚═╝╚═╝     ╚═╝╚══════╝");

            Console.WriteLine("Press any key to enter.");

            Console.ReadKey();
            Console.Clear();
            ViewOrCreate();

        }
        public static void ViewOrCreate()
        {
            string option = "";

            while (option != "view" || option != "create")
            {
                Console.Clear();
                Console.WriteLine("Would you like to view or create a table today?");
                Console.WriteLine("View");
                Console.WriteLine("Create");
                option = Console.ReadLine().ToLower();

                if (option == "view") { ViewTable(); }
                if (option == "create") { FileName(); }
            }
        }
        public static void SlotAmount(string fileName)
        {
            Console.Clear();

            Console.WriteLine("Hello user. How many slots for activities will you want?");

            int numRows = 0;
            int numColumns = 7;
            bool hasEnteredValidInt = false;

            while (!hasEnteredValidInt)
            {
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out numRows)) { hasEnteredValidInt = true; }
                else { Console.WriteLine("Please enter a valid integer."); }
            };

            Console.WriteLine("Are you happy with the amount of slots you have chosen for you activities?");
            Console.WriteLine("Yes");
            Console.WriteLine("No");
            Console.WriteLine($"(Slot amount: {numRows})");
            string option = Console.ReadLine().ToLower();

            while (option != "yes")
            {
                Console.Clear();

                Console.WriteLine("Hello user. How many slots for activities will you want?");

                numRows = int.Parse(Console.ReadLine());
                Console.WriteLine("Are you happy with the amount of slots you have chosen for you activities?");
                Console.WriteLine("Yes");
                Console.WriteLine("No");
                Console.WriteLine($"(Slot amount: {numRows})");

                option = Console.ReadLine();
            }

            if (option == "yes")
            {
                Console.Clear();
                ActivitiesAndTime(numColumns, numRows, fileName);
            }
        }
        static void GetInputtedTime(out int hoursValue, out int minutesValue)
        {
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
        }

        static void GetInputtedActivity(out string activity,int count2)
        {
            Console.WriteLine("Enter the activity for the time you have chosen.");
            Console.WriteLine("Slot(" + (count2 + 1) + ")");
            activity = Console.ReadLine();
        }

        static bool ConfirmActivity()
        {
            Console.WriteLine("Are you happy with the activity you have picked?");
            Console.WriteLine("Yes");
            Console.WriteLine("No");
            string option2 = Console.ReadLine().ToLower();

            return option2 == "yes";
        }

        static void PrintTime(int hoursValue, int minutesValue, string[] days,int count1)
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
        }

        static void ChangeOption(out string option3)
        {
            Console.WriteLine(@"Would you like to change any part of this timetable or would you like to make some comments on your table.");
            Console.WriteLine("Change");
            Console.WriteLine("Finish");
            option3 = Console.ReadLine().ToLower();
        }

        static void LoopForOption3(out string option3)
        {
            Console.WriteLine(@"Would you like to make more changes or are you happy with the timetable.");
            Console.WriteLine("Change");
            Console.WriteLine("Finish");
            option3 = Console.ReadLine().ToLower();
            Console.Clear();
        }

        static void ActivitiesAndTime(int numColumns, int numRows, string fileName)
        {
            
            Console.Clear();

            ActivitiesAndTimes schedule = new ActivitiesAndTimes(numRows, numColumns);
            int hoursValue, minutesValue;
            string activity;
            int[,] hours = new int[numColumns, numRows];
            int[,] minutes = new int[numColumns, numRows];
            string[,] activities = new string[numColumns, numRows];
            List<string> comments;
            comments = new List<string>();
            bool hasFinishedCommenting = false;
            bool hasFinisheddeleting = false;

            string[] days = new string[7] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            int count1 = 0, count2 = 0;

            for (int i = 0; i < numColumns * numRows; i++)
            {
                Console.Clear();

                Console.WriteLine(days[count1]);
                Console.WriteLine("");
                Console.WriteLine("Enter the hour the activity will take place. 00-23");
                Console.WriteLine("Slot(" + (count2 + 1) + ")");

                GetInputtedTime(out hoursValue, out minutesValue);

                PrintTime(hoursValue, minutesValue, days,count1);

                while (!ConfirmActivity())
                {
                    Console.Clear();
                    PrintTime(hoursValue, minutesValue,days,count1);
                    GetInputtedActivity(out activity,count2);
                }

                schedule.HoursValue(count1, count2, hoursValue);
                schedule.MinutesValue(count1, count2, minutesValue);
                hours[count1, count2] = hoursValue;
                minutes[count1, count2] = minutesValue;

                Console.Clear();
                PrintTime(hoursValue, minutesValue,days,count1);

                GetInputtedActivity(out activity,count2);

                while (!ConfirmActivity())
                {
                    Console.Clear();
                    PrintTime(hoursValue, minutesValue, days,count1);
                    GetInputtedActivity(out activity,count2);
                }

                schedule.ActivitiesInput(count1, count2, activity);
                activities[count1, count2] = activity;

                count2++;
                if (count2 == numRows)
                {
                    count2 = 0;
                    count1++;
                }
            }

            while (!hasFinishedCommenting)
            {
                Console.Write("Enter a comment or type stop to finish commenting. ");
                string comment = Console.ReadLine().ToLower();

                if (comment == "stop")
                {
                    hasFinishedCommenting = true;
                }
                comments.Add(comment);
            } 

            Console.WriteLine("Comments List:");
            for (int i = 0; i < comments.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + comments[i]);
            }
            

            while (hasFinisheddeleting == false)
            {
                Console.Write("Enter the number of the comment to remove.");
                Console.WriteLine("You can exit by entering (0)");
                if (int.TryParse(Console.ReadLine(), out int selection))
                {
                    if (selection == 0)
                    {
                        hasFinisheddeleting = true;
                    }

                    if (selection >= 1 && selection <= comments.Count)
                    {
                        comments.RemoveAt(selection - 1);
                        Console.WriteLine("Comment removed.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input please enter a number or '0' to exit.");
                }
            }

            Console.WriteLine("Updated Comments List:");
            for (int i = 0; i < comments.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {comments[i]}");
            }

            
            Console.Clear();
            
            schedule.block2(schedule.hours, schedule.minutes, schedule.days, schedule.activities, numRows, numColumns,comments);
            schedule.PrintTable(numRows,comments);
            schedule.CreateFile(fileName, numColumns, numRows, comments);
            string option3;
            ChangeOption(out option3);

            while (option3 != "change" && option3 != "finish")
            {
                LoopForOption3(out option3);
                ChangeOption(out option3);
            }
            if (option3 == "change")
            {
                BlockSelect(numRows, numColumns, fileName, hours, minutes, activities);
            }
            if (option3 == "finish")
            {
                FinishTable(fileName);
            }
        }

        public static List<string> Comments(List<string> comments)
        {
            comments = new List<string>();
            bool hasFinishedCommenting = false;


            while (!hasFinishedCommenting)
            {
                Console.Write("Enter a comment or type stop to finish commenting. ");
                string comment = Console.ReadLine().ToLower();

                if (comment == "stop")
                {
                    hasFinishedCommenting = true;
                }
                comments.Add(comment);
            }
            


            Console.WriteLine("Comments List:");
            for (int i = 0; i < comments.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + comments[i]);
            }
            bool hasFinisheddeleting = false;

            while(hasFinisheddeleting== false)
            {
                Console.Write("Enter the number of the comment to remove.");
                Console.WriteLine("You can exit by entering (0)");
                if (int.TryParse(Console.ReadLine(), out int selection))
                {
                    if (selection == 0)
                    {
                        hasFinisheddeleting = true;
                    }

                    if (selection >= 1 && selection <= comments.Count)
                    {
                        Console.Clear();
                        comments.RemoveAt(selection - 1);
                        Console.WriteLine("Comment removed.");
                        Console.WriteLine("Comments List:");
                    }
                    else
                    {
                        Console.WriteLine("Invalid please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input please enter a number or '0' to exit.");
                }
            }

            Console.WriteLine("Updated Comments List:");
            for (int i = 0; i < comments.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {comments[i]}");
            }
            return comments;
        }

        public static void BlockSelect(int numRows, int numColumns, string fileName, int[,] hours, int[,] minutes, string[,] activities)
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
                Console.WriteLine("column:" + columns + "" + @"rows:" + rows + "Are valid");
            }

            BlockDeleteAndReform(numRows, numColumns, rows, columns, fileName, hours, minutes, activities);
        }
        public static void BlockDeleteAndReform(int numRows, int numColumns, int rows, int columns, string fileName, int[,] hours, int[,] minutes, string[,] activities)
        {
            ActivitiesAndTimes schedule = new ActivitiesAndTimes(rows, columns);
            string[] days = new string[7] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            numRows = 0; numColumns = 0;
            int hoursValue, minutesValue;

            Console.WriteLine("Day:" + days[columns - 1]);
            Console.WriteLine("Enter the hour the activity will take place. 00-23");

            while (true)
            {
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out hoursValue) && hoursValue >= 0 && hoursValue <= 23)
                {
                    schedule.HoursValue(columns, rows, hoursValue);
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid integer. 00-23");
                }
            }

            Console.WriteLine("Enter the minute of the hour the activity will take place. 00-59");
            while (true)
            {
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out minutesValue) && minutesValue >= 0 && minutesValue <= 59)
                {
                    schedule.MinutesValue(columns, rows, minutesValue);
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valid integer. 00-59");
                }
            }

            Console.WriteLine("Are you happy with the times you have picked?");
            Console.WriteLine("Yes");
            Console.WriteLine("No");
            string option = Console.ReadLine().ToLower();

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

                Console.WriteLine("Are you happy with the times you have picked?");
                Console.WriteLine("Yes");
                Console.WriteLine("No");
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
            string activity = Console.ReadLine();

            Console.WriteLine("Are you happy with the activity you have picked?");
            Console.WriteLine("Yes");
            Console.WriteLine("No");
            string option2 = Console.ReadLine().ToLower();

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

                Console.WriteLine("Are you happy with the times you have picked?");
                Console.WriteLine("Yes");
                Console.WriteLine("No");
                option2 = Console.ReadLine();
            }
            //Checks to see if the user is happy with the activity they choosen

            Console.Clear();


            schedule.block1(schedule.hours, schedule.minutes, schedule.days, schedule.activities, numRows, numColumns);
            schedule.block2(schedule.hours, schedule.minutes, schedule.days, schedule.activities, numRows, numColumns,comments);
            schedule.PrintTable(numRows,comments);

            //prints table

            Console.WriteLine("Would you like to make more changes or are the you happy with the timetable.");
            Console.WriteLine("Change");
            Console.WriteLine("Comment");
            Console.WriteLine("Finish");

            string option3 = Console.ReadLine().ToLower();
            Console.Clear();

            while (option3 != "change" || option3 != "comment" || option3 != "finish")
            {
                Console.WriteLine("Would you like to make more changes or are the you happy with the timetable. 1.Change 2.Comment 3.Finish");
                option3 = Console.ReadLine().ToLower();
                Console.Clear();
            }
            if (option3 == "change")
            {
                BlockDeleteAndReform(numRows, numColumns, rows, columns, fileName, hours, minutes, activities);
            }
            if (option3 != "finish")
            {
                FinishTable(fileName);
            }
        }
        
        public static void FileName()
        {
            Console.Clear();
            DateTime currentTime = DateTime.Now;

            Console.WriteLine($"The current Date and Time is: {currentTime}.");
            Console.WriteLine("Enter in the starting day of your week. (dd.mm.yyyy).");
            string fileName = Console.ReadLine();
            List<int> incorrectMaxDays = new List<int> { 1, 3, 5, 7, 8, 10, 12 };
            List<int> incorrectDays = new List<int> { 4, 6, 9, 11 };

            while (CheckDateFormat(fileName) != true)
            {
                Console.WriteLine("Make sure the enter the date in the correct format. (dd/mm/yyyy)");
                fileName = Console.ReadLine();
            }

            string currentTimeString = currentTime.ToString();
            // Make the current time into a string

            string currentDayString = currentTimeString.Substring(0, 2);
            string currentMonthString = currentTimeString.Substring(3, 2);
            string currentYearString = currentTimeString.Substring(6, 4);
            int currentDayInt = int.Parse(currentDayString);
            int currentMonthInt = int.Parse(currentMonthString);
            int currentYearInt = int.Parse(currentYearString);
            // Converting the current date into induvidual days months and years

            string dayString = fileName.Substring(0, 2);
            string monthString = fileName.Substring(3, 2);
            string YearString = fileName.Substring(6, 4);

            int dayInt = int.Parse(dayString);
            int monthInt = int.Parse(monthString);
            int yearInt = int.Parse(YearString);
            // Converting the inputed date into induvidual days months and years

            if ((dayInt > 31 && incorrectMaxDays.Contains(monthInt)) || (dayInt > 30 && incorrectDays.Contains(monthInt)) || (dayInt > 28 && monthInt == 2)) { FileName(); }
            //Checks to see if the date enterd is correct from january-december

            if (dayInt > 31 && monthInt > 12 || dayInt < 1 && monthInt < 1) { FileName(); }
            //Checks the max month and max days and lowest days and lowest month int

            while (currentDayInt - dayInt >= 0 && currentMonthInt - monthInt >= 0 && currentYearInt - yearInt >= 0)
            {
                Console.Clear();
                Console.WriteLine(@"The current Date and Time is: " + currentTime + ". Enter in a future or current date (dd/mm/yyyy).");
                fileName = Console.ReadLine();

                dayString = fileName.Substring(0, 2); monthString = fileName.Substring(3, 2); YearString = fileName.Substring(6, 4);
                dayInt = int.Parse(dayString); monthInt = int.Parse(monthString); yearInt = int.Parse(YearString);
            }

            Console.WriteLine();
            Console.WriteLine(fileName + "  is a valid date.");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            SlotAmount(fileName);
        }
        public static bool CheckDateFormat(string fileName)
        {
            string regexDate = @"\d{2}.\d{2}.\d{4}";
            //regex for the date as dd/mm/yyyy  2 digits 0-9 /2 digitis 0-9 /4 digits 0-9
            if (Regex.IsMatch(fileName, regexDate))
            {
                return true;
            }
            return false;
        }
        public static void FinishTable(string filename)
        {
            Console.WriteLine("Here is your finished Timetable for the date " + filename);
            Console.WriteLine("");
            Console.WriteLine("Would you like view or create another table or exit the application?");
            Console.WriteLine("restart");
            Console.WriteLine("exit");

            string option = Console.ReadLine();
            while(option != "restart" || option != "exit")
            {
                Console.WriteLine("Would you like view or create another table or exit the application?");
                Console.WriteLine("restart");
                Console.WriteLine("exit");

                option = Console.ReadLine();
            }
            if(option == "restart")
            {
                
            }
        }
        public static void ViewTable()
        {
            Console.WriteLine("Enter the name of the file you would like to view.");
            string fileName = Console.ReadLine();
        }
    }
}
 