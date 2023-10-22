using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.Common;

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
            SlotAmount();

        }
        static void ViewOrCreate()
        {

            string option = "";

            while (option != "view" || option != "create")
            {
                Console.Clear();
                Console.WriteLine(@"Would you like to view or create a table today?
1.view
2.create");
                option = Console.ReadLine();
                option = option.ToLower();


                if (option == "view")
                {
                    ViewTable();
                }
                if (option == "create")
                {
                    SlotAmount();
                }
            }
        }
        static void SlotAmount()
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
"1.Yes"+
"2.NO");
            string option = Console.ReadLine();
            option = option.ToLower();

            while (option != "yes")
            {
                Console.Clear();

                Console.WriteLine("Hello user. How many slots for activities will you want?");

                numRows = int.Parse(Console.ReadLine());
                Console.WriteLine(@"Are you happy with the amount of slots you have chosen for you activities? " + "(Slot amount: " + numRows + ") " +
"1.Yes" +
"2.NO");

                Console.WriteLine("");
                Console.WriteLine(@"Are you happy with the amount of slots you have chosen?
1.Yes
2.NO");
                option = Console.ReadLine();
            }

            if (option == "yes")
            {
                Console.Clear();
                ActivitiesAndTime(numColumns, numRows);
            }
        }

        static void ActivitiesAndTime(int numColumns, int numRows)
        {
            Console.Clear();

            
            ActivitiesAndTimes activitiesAndTimes = new ActivitiesAndTimes(numRows, numColumns);
            int hoursValue, minutesValue;
            string activity;

            string[]days = new string[7] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            int count1 = 0, count2 = 0;

            for (int i = 0; i < numColumns * numRows; i++)
            {
                Console.Clear();

                Console.WriteLine(days[count1]);
                Console.WriteLine("");
                Console.WriteLine("Enter the hour the activity will take place. 0-23");

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

                Console.WriteLine("Enter the minute of the hour the activity will take place. 0-59");
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

                while (option != "yes" || minutesValue > 59 && minutesValue < 0 || hoursValue > 23 && hoursValue < 0)
                {
                    Console.Clear();

                    Console.WriteLine("Enter the hour the time will take place. 0-23");
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

                    Console.WriteLine("Enter the minute of the hour the activity will take place. 0-59");
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

                    Console.WriteLine(@"Are you happy with the times you have picked? 
1.Yes
2.No");
                    option = Console.ReadLine();
                }
                // Makes sure the Hours and Minutes are valid


                if (hoursValue < 24 || minutesValue < 60 && hoursValue >= 0 || minutesValue >= 0)
                {
                    activitiesAndTimes.HoursValue(count1,count2,hoursValue);
                    activitiesAndTimes.MinutesValue(count1,count2,minutesValue);
                }

                
                
                Console.Clear();
                Console.WriteLine(days[count1]+" "+ hoursValue + ":" + minutesValue);

                Console.WriteLine("Enter the activity for the time you have chosen.");
                activity = Console.ReadLine();

                Console.WriteLine(@"Are you happy with the activity you have picked?
1.Yes
2.No");
                string option2 = Console.ReadLine();

                while (option2 != "Yes")
                {
                    Console.Clear();
                    Console.WriteLine(days[count1] + " " + hoursValue + ":" + minutesValue);

                    Console.WriteLine("Enter the activity for the time you have chosen.");
                    activity = Console.ReadLine();

                    Console.WriteLine(@"Are you happy with the times you have picked?
1.Yes
2.No");
                    option2 = Console.ReadLine();
                }
                //Checks to see if the user is happy with the activity they choosen
                    
                

            }
            //loops for time and activities 

        }

        static void FileName()
        {
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
        }


        static void ViewTable()
        {
            Console.WriteLine("Enter the name of the file you would like to view.");
            string fileName = Console.ReadLine();
        }




    }
}
