using System;
using System.Collections.Generic;
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
            Console.WriteLine("Welcome to housebot 3000");

            Console.WriteLine();
            MainPage(roomRepo, roommateRepo);

            
            
            /*  
            Console.WriteLine("Getting All Rooms:");
            Console.WriteLine();

            List<Room> allRooms = roomRepo.GetAll();

            foreach (Room room in allRooms)
            {
                Console.WriteLine($"{room.Id} {room.Name} {room.MaxOccupancy}");
            }

            Console.WriteLine("----------------------------");
            Console.WriteLine("Getting Room with Id 1");

            Room singleRoom = roomRepo.GetById(1);

            Console.WriteLine($"{singleRoom.Id} {singleRoom.Name} {singleRoom.MaxOccupancy}");

             *  Room bathroom = new Room
                        {
                            Name = "Bathroom",
                            MaxOccupancy = 1
                        };

                        //roomRepo.Insert(bathroom);


                        Console.WriteLine("-------------------------------");
                        Console.WriteLine($"Added the new Room with id {bathroom.Id}");

                        bathroom.MaxOccupancy = 3;

                        roomRepo.Update(bathroom);

                        roomRepo.Delete(7);

                        foreach (Room room in allRooms)
                        {
                            Console.WriteLine($"{room.Id} {room.Name} {room.MaxOccupancy}");
                        }

            List<Roommate> allRoommates = roommateRepo.GetAll();

            foreach (Roommate roommate in allRoommates)
            {
                Console.WriteLine($"{roommate.Id} {roommate.Firstname} {roommate.Lastname} {roommate.MoveInDate} {roommate.RentPortion}");
            }

            Console.WriteLine("----------------------------");
            Console.WriteLine("Getting Roommate with Id 1");

            Roommate singleRoommate = roommateRepo.GetById(1);

            Console.WriteLine($"{singleRoommate.Firstname} {singleRoommate.Lastname} {singleRoommate.RentPortion} {singleRoommate.MoveInDate}");

            List<Roommate> roommatesByRoom = roommateRepo.GetRoommatesByRoomId(1);

            Console.WriteLine("----------------------------");
            Console.WriteLine("Getting Roommate in room 1");
            foreach (Roommate roommate in roommatesByRoom)
            {
                Console.WriteLine($"{roommate.Id} {roommate.Firstname} {roommate.Lastname} {roommate.MoveInDate} {roommate.RentPortion} {roommate.Room.Name} {roommate.Room.MaxOccupancy}");
            }
            */

        }

        private static void MainPage(RoomRepository roomRepo, RoommateRepository roommateRepo)
        {
            int choice;
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
                RoommatePage(roommateRepo);
            }

            
        }

        private static void RoommatePage(RoommateRepository roommateRepo)
        {
            int choice;
            
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
                ViewAllRoommates(roommateRepo);
            }
            else if (choice == 2)
            {
                ViewSingleRoommate(roommateRepo);
            }
            else if (choice == 3)
            {
                AddARoommate(roommateRepo);
            }

        }

        private static void AddARoommate(RoommateRepository roommateRepo)
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
            else if (RentPortion > 10  || RentPortion <= 70)
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
            }
            else
            {
                RoommatePage(roommateRepo);
            }

        }

        private static void ViewSingleRoommate(RoommateRepository roommateRepo)
        {
            int choice;

            Console.Clear();
            Console.WriteLine("Enter the index of the Roommate you'd like to view");
            List<Roommate> allRoommates = roommateRepo.GetAll();
            int numberOfRoommates = allRoommates.Count;

            foreach (Roommate roommate in allRoommates)
            {
                Console.WriteLine($"{roommate.Id} {roommate.Firstname} {roommate.Lastname}");
            }

            do
            {
                choice = getChoice();
            } while (choice < 1 || choice > numberOfRoommates);


            Roommate singleRoommate = roommateRepo.GetById(choice);

            Console.WriteLine($"{singleRoommate.Firstname} {singleRoommate.Lastname} {singleRoommate.RentPortion} {singleRoommate.MoveInDate}");

            Console.WriteLine("Would you like to view another?");
            string YesNo = getYesNo();

            if (YesNo == "y")
            {
                ViewSingleRoommate(roommateRepo);
            }
            else
            {
                Console.Clear();
                RoommatePage(roommateRepo);
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

        private static void ViewAllRoommates(RoommateRepository roommateRepo)
        {

            List<Roommate> allRoommates = roommateRepo.GetAll();

            foreach (Roommate roommate in allRoommates)
            {
                Console.WriteLine($"{roommate.Id} {roommate.Firstname} {roommate.Lastname} {roommate.MoveInDate} {roommate.RentPortion}");
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            RoommatePage(roommateRepo);
            
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
            int percent;
            do
            {
                response = Console.ReadLine();
            } while (!int.TryParse(response, out percent));

            return percent;

        }
    }
}