using System;
using _EPAM_UserList.Interfases.BLL;
using System.Linq;
using _EPAM_UserList.BLL.UserListLogic;
using System.Globalization;
using System.Text.RegularExpressions;
using _EPAM_UserList.BLL.UserLogic;
using Logs;
using _EPAM_UserList.BLL;

namespace _EPAM_UserList.PL.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IUserListBLL userlogic = null;
            IAwardListBLL awardlogic = null;            
            try
            {
                userlogic = new UserListLogic();
                awardlogic = new AwardLogic();
            }
            catch (ConfigurationFileExeption e)
            {
                System.Console.WriteLine("AAAAA!!! Config is not found, or cant be read. Bye.");
                LogType.AddLog(e);
            }
            catch (Exception e)
            {             
                LogType.AddLog(e);                
            }

            int count = 0;
            do
            {
                System.Console.WriteLine("Select action:");
                System.Console.WriteLine("1 - View all users");
                System.Console.WriteLine("2 - Delete User");
                System.Console.WriteLine("3 - Add User");
                System.Console.WriteLine("4 - View all awards");
                System.Console.WriteLine("5 - Delete award");
                System.Console.WriteLine("6 - Reward the user");
                System.Console.WriteLine("7 - Add new reward");
                System.Console.WriteLine("8 - Exit and save");
                System.Console.Write("Enter number: ");
                int.TryParse(System.Console.ReadLine(), out count);
                try
                {
                    switch (count)
                    {
                        case 1:
                            {
                                System.Console.Clear();
                                int temp = 0;
                                foreach (var item in userlogic.GetAll())
                                {
                                    System.Console.WriteLine("{0} - {1}", ++temp, item);
                                }
                            }
                            break;
                        case 2:
                            {
                                System.Console.Clear();
                                Remove(userlogic);
                            }
                            break;
                        case 3:
                            {
                                System.Console.Clear();
                                UserAdd(userlogic);
                            }
                            break;
                        case 4:
                            {
                                System.Console.Clear();
                                int temp = 0;
                                foreach (var item in awardlogic.GetAll())
                                {
                                    System.Console.WriteLine("{0} - {1}", ++temp, item);
                                }
                            }
                            break;
                        case 5:
                            {
                                System.Console.Clear();
                                Remove(awardlogic, userlogic);
                            }
                            break;
                        case 6:
                            {
                                System.Console.Clear();
                                RewardUser(userlogic, awardlogic);
                            }
                            break;
                        case 7:
                            {
                                System.Console.Clear();
                                RewardAdd(awardlogic);
                            }
                            break;
                        case 8:
                            {
                                System.Console.Clear();
                                System.Console.WriteLine("Save all data");
                                userlogic.Save();
                                awardlogic.Save();
                                Environment.Exit(0);
                            }
                            break;
                    }
                }
                catch (Exception e)
                {
                    LogType.AddLog(e);
                }


                System.Console.ReadLine();
                System.Console.Clear();
            } while (count != 100);
        }

        private static void RewardUser(IUserListBLL users, IAwardListBLL awards)
        {
            int temp = 0;
            var collection = users.GetAll();
            if (awards.GetAll().Count() > 0)
            {
                foreach (var item in collection)
                {
                    System.Console.WriteLine("{0} - {1}", ++temp, item);
                }
                System.Console.Write("Enter number: ");
                temp = 0;
                int.TryParse(System.Console.ReadLine(), out temp);
                if (temp > 0 && temp < collection.Count() + 1)
                {
                    var selected_user = collection.ElementAt(temp - 1);
                    Entites.AwardDTO selected_award;
                    temp = 0;
                    foreach (var item in awards.GetAll())
                    {
                        System.Console.WriteLine("{0} - {1}", ++temp, item);
                    }
                    System.Console.Write("Enter number: ");
                    temp = 0;
                    int.TryParse(System.Console.ReadLine(), out temp);
                    if (temp > 0 && temp < awards.GetAll().Count() + 1)
                    {

                        selected_award = awards.GetAll().ElementAt(temp - 1);
                        var value = selected_user.Awards.FirstOrDefault(item => item.Name == selected_award.Name);
                        if (value == null)
                        {
                            selected_user.Awards.Add(selected_award);
                            selected_award.Users.Add(selected_user);
                            System.Console.WriteLine("{0} - awarded with - \"{1}\" !", selected_user.Name, selected_award);
                        }
                        else
                        {
                            System.Console.WriteLine("This user already rewarded with this award");
                        }

                    }
                }
            }
            else
            {
                System.Console.WriteLine("There is no awards. Please add some.. =)");
            }
            
        }

        private static void RewardAdd(IAwardListBLL logic)
        {
            System.Console.Write("Award's name: ");
            var name = System.Console.ReadLine();
            if (!logic.Exists(name))
            {
                logic.Create(new Entites.AwardDTO(name));
                System.Console.WriteLine("{0} - Created", name);
            }
            else
            {
                System.Console.WriteLine("{0} - already exists", name);
            }

        }

        private static void Remove(IAwardListBLL logic, IUserListBLL userslogic)
        {
            int temp = 0;
            var collection = logic.GetAll();
            var users = userslogic.GetAll();
            foreach (var item in collection)
            {
                System.Console.WriteLine("{0} - {1}", ++temp, item);
            }
            System.Console.Write("Enter number: ");
            temp = 0;
            int.TryParse(System.Console.ReadLine(), out temp);
            if (temp > 0 && temp < collection.Count() + 1)
            {
                var item = collection.ElementAt(temp - 1);
                foreach (var user in users)
                {
                    var value = user.Awards.FirstOrDefault(i => i.Name == item.Name);
                    if (value != null)
                    {
                        user.Awards.Remove(item);
                    }
                }
                logic.Delete(item.Id);

                System.Console.WriteLine("{0} - Deleted!", item);
            }
        }

        private static void Remove(IUserListBLL logic)
        {
            int temp = 0;
            var collection = logic.GetAll();
            foreach (var item in collection)
            {
                System.Console.WriteLine("{0} - {1}", ++temp, item);
            }
            System.Console.Write("Enter number: ");
            temp = 0;
            int.TryParse(System.Console.ReadLine(), out temp);
            if (temp > 0 && temp < collection.Count() + 1)
            {
                var item = collection.ElementAt(temp - 1);
                logic.Delete(item.Id);

                System.Console.WriteLine("{0} - Deleted!", item);
            }
        }

        private static void UserAdd(IUserListBLL logic)
        {
            System.Console.WriteLine("Enter user's data:");
            System.Console.Write("Name: ");
            var name = System.Console.ReadLine();
            System.Console.Write("Date of Birth (dd-mm-yyyy): ");
            var sdate = System.Console.ReadLine();
            if (isDate(sdate))
            {
                var date = DateTime.ParseExact(sdate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                logic.Create(new Entites.UserDTO(name, date));
                System.Console.WriteLine("{0} - Created", name);
            }
            else
            {
                System.Console.WriteLine("Wrong date of birth");
                System.Console.ReadLine();
                UserAdd(logic);
            }


        }

        private static bool isDate(string sdate)
        {
            string date_pattern = @"(0[1-9]|[12][0-9]|3[01])\-(0[1-9]|1[0-2])\-(19|20)[0-9]{2}";

            if (sdate != null && Regex.IsMatch(sdate, date_pattern))
            {
                return true;
            }
            return false;
        }
        

    }
}
