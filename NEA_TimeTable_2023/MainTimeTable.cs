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
            SlotAmount();

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

            
            Console.WriteLine("Are you happy with the amount of slots you chave chosen for you activities? " + "(Slot amount: " + numRows + ")");
            string option = Console.ReadLine();
            option = option.ToLower();

            while (option != "yes")
            {
                Console.Clear();
                Console.WriteLine("Hello user. How many slots for activities will you want?");

                numRows = int.Parse(Console.ReadLine());
                Console.WriteLine("Are you happy with the amount of slots you chave chosen for you activities? " + "(" + numRows + ")");

                Console.WriteLine("");
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

        static void ActivitiesAndTime(int numColumns, int numRows)
        {
            Console.Clear();


            ActivitiesAndTimes[,] activities = new ActivitiesAndTimes[numColumns, numRows];
            int[,] minutes = new int[numColumns, numRows];
            int[,] hours = new int[numColumns, numRows];
            string[]days = new string[7] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            int count1 = 0, count2 = 0;

            for (int i = 0; i < numColumns * numRows; i++)
            {
                Console.Clear();

                Console.WriteLine(days[count1]);
                Console.WriteLine("");
                Console.WriteLine("Enter the hour the activity will take place. 0-23");
                hours[count1, count2] = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the minute of the hour the activity will take place. 0-59");
                minutes[count1, count2] = int.Parse(Console.ReadLine());

                Console.WriteLine("Are you happy with the times your picked? (Yes/No)");
                string option = Console.ReadLine();
                option = option.ToLower();

                while (option != "yes" || minutes[count1, count2] > 59 && hours[count1, count2] < 0 || hours[count1, count2] > 23 && hours[count1, count2] < 0)
                {
                    Console.Clear();

                    Console.WriteLine("Enter the hour the time will take place. 0-23");
                    hours[count1, count2] = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter the minute of the hour the activity will take place. 0-59");
                    minutes[count1, count2] = int.Parse(Console.ReadLine());

                    Console.WriteLine("Are you happy with the times your picked? (Yes/No)");
                    option = Console.ReadLine();
                }
                // Makes sure the Hours and Minutes are valid


                if (hours[count1, count2] < 24 || minutes[count1, count2] < 60 && hours[count1, count2] >= 0 || minutes[count1, count2] >= 0)
                {
                    
                }

                for (int f = 0; f < numColumns * numRows; f++)
                {
                    Console.Clear();
                    Console.WriteLine("Enter the activity for the time your have chosen.");


                    while (option != "Yes")
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter an Hour between 0-23.");
                        
                    }
                }
            }

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
