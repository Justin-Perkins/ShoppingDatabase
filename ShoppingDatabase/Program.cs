using System;
using MySql.Data.MySqlClient;

namespace CSharpToMySQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Four variables to create the connection string
            string server = "localhost"; // If not using a cloud database, use localhost. If using cloud database, use the IP to the server
            string database = "ShoppingDatabase"; // Whatever database you want to use for the application
            string username = "root"; // The username of your MySQL client
            string password = Environment.GetEnvironmentVariable("SNHU_PASS"); // The password of your MySQL client

            // Throw all the variables into this connection string
            // MUST USE THIS FORMAT! (server, database, username, then password)
            string connstring = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";";

            string userSelectionString;
            int userSelection = 0;

            string userName;
            string userPassword;

            // Initializes the connection object called "conn", using the connection string
            MySqlConnection conn = new MySqlConnection(connstring);


            while (userSelection != 3)
            {
                Console.WriteLine("Choose an option...");
                Console.WriteLine("1) Sign in");
                Console.WriteLine("2) Sign up");
                Console.WriteLine("3) Exit from interface");

                userSelectionString = Console.ReadLine();
                userSelection = Convert.ToInt32(userSelectionString);

                switch (userSelection)
                {
                    case 1:
                        Console.WriteLine("Username:");
                        userName = Console.ReadLine();
                        Console.WriteLine("Password:");
                        userPassword = Console.ReadLine();

                        if (doesUserExist(conn, userName, userPassword))
                        {
                            userSelection = 3;
                            Console.WriteLine("User does exist, start application here");
                        }
                        else
                        {
                            Console.WriteLine("This user does not exist");
                        }
                        break;
                    case 2:
                        Console.WriteLine("Username:");
                        userName = Console.ReadLine();
                        Console.WriteLine("Password:");
                        userPassword = Console.ReadLine();

                        if (insertNewUser(conn, userName, userPassword))
                        {
                            Console.WriteLine("Created a new account");
                        }
                        else
                        {
                            Console.WriteLine("This user already exists");
                        }
                        break;
                    case 3:
                      
                        break;
                    default:
                        Console.WriteLine("Invalid Selection. Choose again.");
                        break;
                }
            }
        }


        // Check if the given username is in the database
        static bool doesUsernameExist(MySqlConnection connection, string username)
        {
            connection.Open();

            MySqlCommand user_cmd = new MySqlCommand("select userId from users where users.userName = @user;", connection);
            user_cmd.Parameters.AddWithValue("@user", username);
            var nId = user_cmd.ExecuteScalar();

            if (nId != null)
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }


        // Check if a user exists in the database given their username and password
        static bool doesUserExist(MySqlConnection connection, string username, string userPassword)
        {
            connection.Open();

            MySqlCommand user_cmd = new MySqlCommand("select userId from users where users.userName = @user and users.password = @password;", connection);
            user_cmd.Parameters.AddWithValue("@user", username);
            user_cmd.Parameters.AddWithValue("@password", userPassword);
            var nId = user_cmd.ExecuteScalar();

            if (nId != null)
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }

        
        // Put a new user into the database if they do not exist already
        static bool insertNewUser(MySqlConnection connection, string username, string password)
        {
            if (doesUsernameExist(connection, username))
            {
                return false;
            }
            connection.Open();

            MySqlCommand cmd = new MySqlCommand("INSERT INTO users(userName, password) VALUES (@userName, @password)", connection);
            cmd.Parameters.AddWithValue("@userName", username);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.ExecuteNonQuery();

            connection.Close();
            return true;
        }


        // Start the main application
        void startShoppingInterface()
        {
            int userInput = 0;
            while (userInput != 3)
            {

            }

        }
    }
}