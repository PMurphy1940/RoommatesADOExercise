using System;
using System.Collections.Generic;
using System.Linq;
using Roommates.Models;
using Roommates.Repositories;

namespace Roommates
{
    class Program
    {
        /// <summary>
        ///  This is the address of the database.
        ///  We define it here as a constant since it will never change.
        /// </summary>
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);
            Console.Clear();

            

            Console.WriteLine();
            MainPage(roomRepo, roommateRepo);        

        }

        private static void MainPage(RoomRepository roomRepo, RoommateRepository roommateRepo)
        {
            int choice;
            HouseBot();
            Console.WriteLine("Choose from the following:");
            Console.WriteLine("\t 1) View Roommates");
            Console.WriteLine("\t 2) View Rooms");
            Console.WriteLine("\t 3) View Chores");
            Console.WriteLine("\t 4) Exit");
            do
            {
                choice = getChoice();
            } while (choice < 1 || choice > 4);

            if (choice == 1)
            {
                Console.Clear();
                RoommatePage(roomRepo, roommateRepo);
            }
            else if (choice == 2)
            {
                string message = "Sorry Folks, the park's closed.";
                MooseSaysGoodbye(message);
            }
            else if (choice == 3)
            {
                string message = "Sorry Folks, the park's closed.";
                MooseSaysGoodbye(message);
            }
            else if (choice == 4)
            {
                string message = "Sorry Folks, the park's closed.";
                MooseSaysGoodbye(message);
            }



        }

        private static void HouseBot()
        {
            Console.WriteLine(@"    
     
╔╗─╔╗───────────╔╗────╔╗─╔═══╦═══╦═══╦═══╗
║║─║║───────────║║───╔╝╚╗║╔═╗║╔═╗║╔═╗║╔═╗║
║╚═╝╠══╦╗╔╦══╦══╣╚═╦═╩╗╔╝╚╝╔╝║║║║║║║║║║║║║
║╔═╗║╔╗║║║║══╣║═╣╔╗║╔╗║║─╔╗╚╗║║║║║║║║║║║║║
║║─║║╚╝║╚╝╠══║║═╣╚╝║╚╝║╚╗║╚═╝║╚═╝║╚═╝║╚═╝║
╚╝─╚╩══╩══╩══╩══╩══╩══╩═╝╚═══╩═══╩═══╩═══╝
");
        }

        private static void RoommatePage(RoomRepository roomRepo, RoommateRepository roommateRepo)
        {
            int choice;
            HouseBot();
            Console.WriteLine("Roommate management");
            Console.WriteLine("-------------------");
            Console.WriteLine("\t 1) View all Roommates");
            Console.WriteLine("\t 2) View a single Roommate");
            Console.WriteLine("\t 3) Add a new Roommate");
            Console.WriteLine("\t 4) Update a Roommate file");
            Console.WriteLine("\t 5) Remove a roommate");
            Console.WriteLine("\t 6) Return to main page");
            Console.WriteLine("\t 7) Exit");
            do
            {
                choice = getChoice();
            } while (choice < 1 || choice > 7);

            if (choice == 1)
            {
                ViewAllRoommates(roomRepo, roommateRepo);
            }
            else if (choice == 2)
            {
                ViewSingleRoommate(roomRepo, roommateRepo);
            }
            else if (choice == 3)
            {
                AddARoommate(roomRepo, roommateRepo);
            }
            else if (choice == 4)
            {
                UpdateARoommate(roomRepo, roommateRepo);
            }
            else if (choice == 5)
            {
                DeleteARoommate(roomRepo, roommateRepo);
            }
            else if (choice == 6)
            {
                Console.Clear();
                MainPage(roomRepo, roommateRepo);
            }
            else if (choice == 7)
            {
                string message = "Sorry Folks, the park's closed.";
                MooseSaysGoodbye(message);
            }
        }

        private static void UpdateARoommate(RoomRepository roomRepo, RoommateRepository roommateRepo)
        {
            string message = "Which roommate would you like to update?";
            List<Roommate> allRoommates = roommateRepo.GetAll();

            int id;
            do {
                id = intMooseSays(message, allRoommates);
            }
            while (!allRoommates.Any(rm => rm.Id == id));

            Console.Clear();
            Roommate roommateToUpdate = RoommateValueUpdater(id, roommateRepo);

            roommateRepo.Update(roommateToUpdate);
            Console.Clear();
            RoommatePage(roomRepo, roommateRepo);
        }

        private static Roommate RoommateValueUpdater(int id, RoommateRepository roommateRepo)
        {
            Roommate singleRoommate = roommateRepo.GetById(id);
            int choice;
            do
            {
                Console.Clear();
                HouseBot();
                Console.WriteLine("What would you like to change?");
                Console.WriteLine($"1) First Name \t{singleRoommate.Firstname}");
                Console.WriteLine($"2) Last Name \t{singleRoommate.Lastname}");
                Console.WriteLine($"3) Rent% \t{singleRoommate.RentPortion}");
                Console.WriteLine($"4) Move In \t{singleRoommate.MoveInDate}");
                Console.WriteLine($"5) Room Id \t{singleRoommate.RoomId}");
                Console.WriteLine($"6) I'm finished");
                choice = getChoice();
                if (choice == 1)
                {
                    Console.WriteLine("Enter a new first name");
                    singleRoommate.Firstname = Console.ReadLine();
                }
                else if (choice == 2)
                {
                    Console.WriteLine("Enter a new last name");
                    singleRoommate.Lastname = Console.ReadLine();
                }
                else if (choice == 3)
                {
                    Console.WriteLine("Enter the new rent protion");
                    do
                    {
                        singleRoommate.RentPortion = getChoice();
                    } while (singleRoommate.RentPortion < 1 || singleRoommate.RentPortion > 100);
                }
                else if (choice == 4)
                {
                    Console.WriteLine("Enter the correct move in date");
                    
                    
                        singleRoommate.MoveInDate = UpdateDate();
                    
                }
                else if (choice == 5)
                {
                    Console.WriteLine("Enter their new room assignment");
                    singleRoommate.RoomId = getChoice();                   
                }

            } while (choice != 6);

            return singleRoommate;

        }

        private static DateTime UpdateDate()
        {
            string response;
            DateTime dateTime;
            do
            {
                response = Console.ReadLine();
            } while (!DateTime.TryParse(response, out dateTime));

            return dateTime;
        }

        private static void DeleteARoommate(RoomRepository roomRepo, RoommateRepository roommateRepo)
        {
            string message = "Who's been voted out?";
            List<Roommate> allRoommates = roommateRepo.GetAll();


            int id;
                do
            {
                id = intMooseSays(message, allRoommates);
            }
                while (!allRoommates.Any(rm => rm.Id == id)) ;

        
                roommateRepo.Delete(id); 
 
                Console.Clear();
                RoommatePage(roomRepo, roommateRepo);
        }

        private static void AddARoommate(RoomRepository roomRepo, RoommateRepository roommateRepo)
        {
            Console.Clear();
            string message = "Enter your new roommate's first name";
            string firstName = MooseSays(message);
            Console.Clear();
            message = "Thanks! Now enter your new roommate's last name";
            string lastName = MooseSays(message);
            Console.Clear();
            message = "Sweet! What percentage of the rent will your roommate pay?";
            int RentPortion;
            do
            {
                RentPortion = intMooseSays(message);
            } while (RentPortion < 1 || RentPortion > 100);
            Console.Clear();
            if (RentPortion <= 10)
            {
                message = "Cheapskate, huh?";
            }
            else if (RentPortion > 10  && RentPortion <= 70)
            {
                message = "Sounds reasonable.";
            }
            else if (RentPortion > 70)
            {
                message = "Hey moneybags!";
            }
            message += " Which room do they get?";
            int RoomId = intMooseSays(message);

            message = "Still wanna have them move in?";
            Console.Clear();
            string save = SaveMooseSays(message);
            if (save == "y")
            {
                Roommate newRoomate = new Roommate()
                {
                    Firstname = firstName,
                    Lastname = lastName,
                    RentPortion = RentPortion,
                    MoveInDate = DateTime.Now,
                    RoomId = RoomId
                };

                roommateRepo.Insert(newRoomate);
                Console.Clear();
                RoommatePage(roomRepo, roommateRepo);
            }
            else
            {
                RoommatePage(roomRepo, roommateRepo);
            }

        }

        private static void ViewSingleRoommate(RoomRepository roomRepo, RoommateRepository roommateRepo)
        {
            int choice;

            Console.Clear();
            HouseBot();
            Console.WriteLine("Enter the index of the Roommate you'd like to view");
            List<Roommate> allRoommates = roommateRepo.GetAll();

            foreach (Roommate roommate in allRoommates)
            {
                Console.WriteLine($"{roommate.Id} {roommate.Firstname} {roommate.Lastname}");
            }

            do
            {
                choice = getChoice();
            } while (!allRoommates.Any(rm => rm.Id == choice));


            Roommate singleRoommate = roommateRepo.GetById(choice);

            Console.WriteLine($"{singleRoommate.Firstname} {singleRoommate.Lastname} {singleRoommate.RentPortion} {singleRoommate.MoveInDate}");

            Console.WriteLine("Would you like to view another?");
            string YesNo = getYesNo();

            if (YesNo == "y")
            {
                Console.Clear();
                ViewSingleRoommate(roomRepo, roommateRepo);
            }
            else
            {
                Console.Clear();
                RoommatePage(roomRepo, roommateRepo);
            }          
        }

        private static string getYesNo()
        {
            string userChoice;

            do
            {
                userChoice = Console.ReadLine().ToLower();
            } while (userChoice != "y" && userChoice != "n");

            return userChoice;
        }

        private static void ViewAllRoommates(RoomRepository roomRepo, RoommateRepository roommateRepo)
        {
            Console.Clear();
            HouseBot();
            List<Roommate> allRoommates = roommateRepo.GetAll();

            foreach (Roommate roommate in allRoommates)
            {
                Console.WriteLine($"{roommate.Id} {roommate.Firstname} {roommate.Lastname} {roommate.MoveInDate} {roommate.RentPortion}");
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.Clear();
            RoommatePage(roomRepo, roommateRepo);
            
        }

        private static int getChoice()
        {
            string userChoice;
            int choice;
            do
            {
                userChoice = Console.ReadLine();
            } 
            while (!int.TryParse(userChoice, out choice));

            return choice;

            
        }

        static string MooseSays(string message)
        {
            Console.Clear();
            Console.WriteLine($@"
                                      _.--^^^--,
                                    .'          `\
  .-^^^^^^-.                      .'              |
 /          '.                   /            .-._/
|             `.                |             |
 \              \          .-._ |          _   \
  `^^'-.         \_.-.     \   `          ( \__/
        |             )     '=.       .,   \
       /             (         \     /  \  /
     /`               `\        |   /    `'
     '..-`\        _.-. `\ _.__/   .=.
          |  _    / \  '.-`    `-.'  /
          \_/ |  |   './ _     _  \.'
               '-'    | /       \ |
                      |  .-. .-.  |
                      \ / o| |o \ /
                       |   / \   |    {message}
                      / `^`   `^` \
                     /             \
                    | '._.'         \
                    |  /             |
                     \ |             |
                      ||    _    _   /
                      /|\  (_\  /_) /
                      \ \'._  ` '_.'
                       `^^` `^^^`

   ");
            string response;
            do
            {
                response = Console.ReadLine();
            } while (response.Length < 1);

            return response;

        }
        static string SaveMooseSays(string message)
        {
            Console.Clear();
            Console.WriteLine($@"
                                      _.--^^^--,
                                    .'          `\
  .-^^^^^^-.                      .'              |
 /          '.                   /            .-._/
|             `.                |             |
 \              \          .-._ |          _   \
  `^^'-.         \_.-.     \   `          ( \__/
        |             )     '=.       .,   \
       /             (         \     /  \  /
     /`               `\        |   /    `'
     '..-`\        _.-. `\ _.__/   .=.
          |  _    / \  '.-`    `-.'  /
          \_/ |  |   './ _     _  \.'
               '-'    | /       \ |
                      |  .-. .-.  |
                      \ / o| |o \ /
                       |   / \   |    {message}
                      / `^`   `^` \
                     /             \
                    | '._.'         \
                    |  /             |
                     \ |             |
                      ||    _    _   /
                      /|\  (_\  /_) /
                      \ \'._  ` '_.'
                       `^^` `^^^`

   ");
            
            

            return getYesNo();

        }

        static int intMooseSays(string message)
        {
            Console.Clear();
            Console.WriteLine($@"
                                      _.--^^^--,
                                    .'          `\
  .-^^^^^^-.                      .'              |
 /          '.                   /            .-._/
|             `.                |             |
 \              \          .-._ |          _   \
  `^^'-.         \_.-.     \   `          ( \__/
        |             )     '=.       .,   \
       /             (         \     /  \  /
     /`               `\        |   /    `'
     '..-`\        _.-. `\ _.__/   .=.
          |  _    / \  '.-`    `-.'  /
          \_/ |  |   './ _     _  \.'
               '-'    | /       \ |
                      |  .-. .-.  |
                      \ / o| |o \ /
                       |   / \   |    {message}
                      / `^`   `^` \
                     /             \
                    | '._.'         \
                    |  /             |
                     \ |             |
                      ||    _    _   /
                      /|\  (_\  /_) /
                      \ \'._  ` '_.'
                       `^^` `^^^`

   ");
            string response;
            int number;
            do
            {
                response = Console.ReadLine();
            } while (!int.TryParse(response, out number));

            return number;

        }

        static int intMooseSays(string message, List<Roommate> allRoommates)
        {
            Console.Clear();
            Console.WriteLine($@"
                                      _.--^^^--,
                                    .'          `\
  .-^^^^^^-.                      .'              |
 /          '.                   /            .-._/
|             `.                |             |
 \              \          .-._ |          _   \
  `^^'-.         \_.-.     \   `          ( \__/
        |             )     '=.       .,   \
       /             (         \     /  \  /
     /`               `\        |   /    `'
     '..-`\        _.-. `\ _.__/   .=.
          |  _    / \  '.-`    `-.'  /
          \_/ |  |   './ _     _  \.'
               '-'    | /       \ |
                      |  .-. .-.  |
                      \ / o| |o \ /
                       |   / \   |    {message}
                      / `^`   `^` \
                     /             \
                    | '._.'         \
                    |  /             |
                     \ |             |
                      ||    _    _   /
                      /|\  (_\  /_) /
                      \ \'._  ` '_.'
                       `^^` `^^^`

   ");
            string response;
            int number;
            foreach (Roommate roommate in allRoommates)
            {
                Console.WriteLine($"{roommate.Id} {roommate.Firstname} {roommate.Lastname}");
            }
            do
            {
                response = Console.ReadLine();
            } while (!int.TryParse(response, out number));

            return number;

        }

        static void MooseSaysGoodbye(string message)
        {
            Console.Clear();
            Console.WriteLine($@"
                                      _.--^^^--,
                                    .'          `\
  .-^^^^^^-.                      .'              |
 /          '.                   /            .-._/
|             `.                |             |
 \              \          .-._ |          _   \
  `^^'-.         \_.-.     \   `          ( \__/
        |             )     '=.       .,   \
       /             (         \     /  \  /
     /`               `\        |   /    `'
     '..-`\        _.-. `\ _.__/   .=.
          |  _    / \  '.-`    `-.'  /
          \_/ |  |   './ _     _  \.'
               '-'    | /       \ |
                      |  .-. .-.  |
                      \ / o| |o \ /
                       |   / \   |    {message}
                      / `^`   `^` \
                     /             \
                    | '._.'         \
                    |  /             |
                     \ |             |
                      ||    _    _   /
                      /|\  (_\  /_) /
                      \ \'._  ` '_.'
                       `^^` `^^^`

   ");
        }
        }
}

