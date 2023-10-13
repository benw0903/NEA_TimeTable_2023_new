using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
            FileName();

        }
        static void ViewOrCreate()
        {

            string option = "";

            while (option != "view" || option != "create")
            {
                Console.Clear();
                Console.WriteLine("Would you like to view or create a table today(View/Create)");
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


            int numRows = int.Parse(Console.ReadLine());
            int numColumns = 7;


            Console.WriteLine("Are you happy with the amount of slots you chave chosen for you activities? " + "(" + numRows + ")");
            string option = Console.ReadLine();

            while (option != "yes")
            {
                Console.Clear();
                Console.WriteLine("Hello user. How many slots for activities will you want?");

                numRows = int.Parse(Console.ReadLine());
                Console.WriteLine("Are you happy with the amount of slots you chave chosen for you activities? " + "(" + numRows + ")");

                Console.WriteLine("Are you happy with the amount of slots you chave chosen?");
                option = Console.ReadLine();
            }

            if (option == "yes")
            {
                CreateTable Table = new CreateTable(numColumns, numRows);
                Console.Clear();
                ActivitiesAndTime(numColumns, numRows);
            }
        }

        static void block()
        {
            string fileName = FileName();
            CreateFile createFile = new CreateFile();
            createFile.Create(fileName);
            // Name for the files sent to CreateFile
        }

        static void ActivitiesAndTime(int numColumns, int numRows)
        {
            Console.Clear();


            ActivitiesAndTimes[,] activities = new ActivitiesAndTimes[numColumns, numRows];
            int[,] minutes = new int[numColumns, numRows];
            int[,] hours = new int[numColumns, numRows];
            int count1 = 0, count2 = 0;

            for (int i = 0; i < numColumns * numRows; i++)
            {
                Console.Clear();

                Console.WriteLine("Enter the hour the activity will take place. 0-23");
                hours[count1, count2] = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the minute of the hour the activity will take place. 0-59");
                minutes[count1, count2] = int.Parse(Console.ReadLine());

                Console.WriteLine("Are you happy with the times your picked? (Yes/No)");
                string option = Console.ReadLine();


                while (option != "Yes" || minutes[count1, count2] > 59 && hours[count1, count2] < 0 || hours[count1, count2] > 23 && hours[count1, count2] < 0)
                {
                    Console.Clear();
                    if (option != "Yes")
                    {
                        Console.WriteLine("Please enter an Hour between 0-23.");
                    }
                    if (minutes[count1, count2] > 59 && hours[count1, count2] < 0 || hours[count1, count2] > 23 && hours[count1, count2] < 0)
                    {
                        Console.WriteLine("Invalid input. Please enter an Hour between 0-23.");
                    }

                    Console.WriteLine("Enter the hour the activity will take place. 0-23");
                    hours[count1, count2] = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter the minute of the hour the activity will take place. 0-59");
                    minutes[count1, count2] = int.Parse(Console.ReadLine());

                    Console.WriteLine("Are you happy with the times your picked? (Yes/No)");
                    option = Console.ReadLine();
                }


                if (hours[count1, count2] < 24 || minutes[count1, count2] < 60 && hours[count1, count2] >= 0 || minutes[count1, count2] >= 0)
                {
                    activities.HoursValue(count2, count1, hours[count1, count2]);
                }

                for (int f = 0; f < numColumns * numRows; f++)
                {
                    Console.Clear();
                    Console.WriteLine("Enter the activity for the time your have chosen.");


                    while (option != "Yes")
                    {
                        Console.Clear();
                        if (option != "Yes")
                        {
                            Console.WriteLine("Please enter an Hour between 0-23.");
                        }
                    }
                }
            }

        }

        static string FileName()
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

            while (dayInt > 31 && monthInt == 1 || dayInt > 28 && monthInt == 2 || dayInt > 31 && monthInt == 3)
            {
                Console.Clear();
                Console.WriteLine(@"The current Date and Time is: " + currentTime +
". Enter in a valid date (dd/mm/yyyy).");
                filename = Console.ReadLine();

                dayString = filename.Substring(0, 2); monthString = filename.Substring(3, 2); YearString = filename.Substring(6, 4);
                dayInt = int.Parse(dayString); monthInt = int.Parse(monthString); yearInt = int.Parse(YearString);

            }
            //Days for jan,feb,march 

            while (dayInt > 30 && monthInt == 4 || dayInt > 31 && monthInt == 5 || dayInt > 30 && monthInt == 6)
            {
                Console.Clear();
                Console.WriteLine(@"The current Date and Time is: " + currentTime +
". Enter in a valid date (dd/mm/yyyy).");
                filename = Console.ReadLine();

                dayString = filename.Substring(0, 2); monthString = filename.Substring(3, 2); YearString = filename.Substring(6, 4);
                dayInt = int.Parse(dayString); monthInt = int.Parse(monthString); yearInt = int.Parse(YearString);
            }
            //Days for april,may,june

            while (dayInt > 31 && monthInt == 7 || dayInt > 31 && monthInt == 8 || dayInt > 30 && monthInt == 9)
            {
                Console.Clear();
                Console.WriteLine(@"The current Date and Time is: " + currentTime +
". Enter in a valid date (dd/mm/yyyy).");
                filename = Console.ReadLine();

                dayString = filename.Substring(0, 2); monthString = filename.Substring(3, 2); YearString = filename.Substring(6, 4);
                dayInt = int.Parse(dayString); monthInt = int.Parse(monthString); yearInt = int.Parse(YearString);
            }
            //Days for july,Aug,Sep

            while (dayInt > 31 && monthInt == 10 || dayInt > 30 && monthInt == 11 || dayInt > 31 && monthInt == 12)
            {
                Console.Clear();
                Console.WriteLine(@"The current Date and Time is: " + currentTime +
". Enter in a valid date (dd/mm/yyyy).");
                filename = Console.ReadLine();

                dayString = filename.Substring(0, 2); monthString = filename.Substring(3, 2); YearString = filename.Substring(6, 4);
                dayInt = int.Parse(dayString); monthInt = int.Parse(monthString); yearInt = int.Parse(YearString);
            }
            //Days for oct,nov,dec

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
            return filename;

        }


        static void ViewTable()
        {
            Console.WriteLine("Enter the name of the file you would like to view.");
            string fileName = Console.ReadLine();
        }




    }
}
