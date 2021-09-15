using System;
using System.Data.SqlClient;

namespace Problem {
    class Program {
        static void Main(string[] args) {
            string connectionString = "Server=DESKTOP-CT0BSVJ\\DEV;" +
                                      "Database=Person;" +
                                      "Trusted_Connection=True;";

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            if(sqlConnection.State != System.Data.ConnectionState.Open) {
                Console.WriteLine("couldn't open the connection");

                return;
            }

            Console.Write(
                "1) Insert a new person in to the database\n" +
                "2) Print all people data from database\n" +
                "3) Print person data by his ID\n" +
                "4) Update person data\n" +
                "5) Delete person data from database\n" +
                "Enter command index:" 
                );

            int choice = int.Parse(Console.ReadLine());

            switch(choice) {
            case 1: {
                Person person = new Person();
                Console.Write("Enter Person First Name: ");
                person.FirstName = Console.ReadLine();
                Console.Write("Enter Person Middle Name: ");
                person.MiddleName = Console.ReadLine();
                Console.Write("Enter Person Last Name: ");
                person.LastName = Console.ReadLine();
                Console.Write("Enter Person Birth Date(format: yyyy-mm-dd): ");
                person.BirthDate = Console.ReadLine();

                Insert(sqlConnection, person);
            } break;
            case 2: {
                SelectAll(sqlConnection);
            } break;
            case 3:{
                Console.Write("Enter Person ID: ");
                int id = int.Parse(Console.ReadLine());
                SelcetByID(sqlConnection, id);
            } break;
            case 4: {
                Person person = new Person();
                Console.Write("enter person ID: ");
                person.ID = int.Parse(Console.ReadLine());
                Console.Write("Enter Persons new First Name: ");
                person.FirstName = Console.ReadLine();
                Console.Write("Enter Persons new Middle Name: ");
                person.MiddleName = Console.ReadLine();
                Console.Write("Enter Persons new Last Name: ");
                person.LastName = Console.ReadLine();
                Console.Write("Enter Persons new Birth Date(format: yyyy-mm-dd): ");
                person.BirthDate = Console.ReadLine();

                Update(sqlConnection, person);
            } break;
            case 5: {
                Person person = new Person();
                Console.Write("Enter Person ID: ");
                int id = int.Parse(Console.ReadLine());
                Delete(sqlConnection, id);
            } break;
            default:
                System.Console.WriteLine("Invalid command index");
                break;
            }

            sqlConnection.Close();
        }

        static void Insert(SqlConnection sqlConnection, in Person person) {
            string sqlQuery = "insert into People(LastName, MiddleName, FirstName, BirthDate)" +
                              $"values('{person.FirstName}', '{person.MiddleName}', '{person.LastName}', '{person.BirthDate}')";

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            int result = sqlCommand.ExecuteNonQuery();

            if (result > 0) 
                Console.WriteLine("Person added successfully");
        }

        static void SelectAll(SqlConnection sqlConnection) {
            string sqlQuery = "" +
                              "select " +
                              "People.ID, " +
                              "People.LastName, " +
                              "People.FirstName, " +
                              "People.MiddleName, " +
                              "People.BirthDate " +
                              "from People";

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            SqlDataReader sqlReader = sqlCommand.ExecuteReader();

            while(sqlReader.Read()) {
                string text = $"ID: {sqlReader.GetValue(0)}, LastName: {sqlReader.GetValue(1)}, " +
                              $"FirstName: {sqlReader.GetValue(2)}, MiddleName: {sqlReader.GetValue(3)}, " +
                              $"BirthDate: {sqlReader.GetValue(4)}";

                Console.WriteLine(text);
            }

            sqlReader.Close();
        }

        static void SelcetByID(SqlConnection sqlConnection, in int ID) {
            string sqlQuery =  "" +
                              "select " +
                              "People.ID, " +
                              "People.LastName, " +
                              "People.FirstName, " +
                              "People.MiddleName, " +
                              "People.BirthDate " +
                              $"from People where ID = {ID}";

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            SqlDataReader sqlReader = sqlCommand.ExecuteReader();
            bool empty = true;
            while(sqlReader.Read()) {
                empty = false;
                string text = $"LastName: {sqlReader.GetValue(1)}, " +
                              $"FirstName: {sqlReader.GetValue(2)}, MiddleName: {sqlReader.GetValue(3)}, " +
                              $"BirthDate: {sqlReader.GetValue(4)}";

                Console.WriteLine(text);
            }

            if(empty)
                Console.WriteLine($"Person with ID = {ID} not found");

            sqlReader.Close();
        }

        static void Update(SqlConnection sqlConnection, Person person) {
            string sqlQuery = $"update People " +
                              $"set " +
                              $"LastName = '{person.LastName}', " +
                              $"FirstName = '{person.FirstName}', " +
                              $"MiddleName = '{person.MiddleName}', " +
                              $"BirthDate = '{person.BirthDate}' " +
                              $"where ID = {person.ID};";

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            int result = sqlCommand.ExecuteNonQuery();

            if(result > 0) {
                Console.WriteLine($"Student with ID = {person.ID} was updated successfully");
            } else {
                Console.WriteLine($"Student with ID = {person.ID} not found");
            }
        }

        static void Delete(SqlConnection sqlConnection, in int ID) {
            string sqlQuery = $"delete People where ID = {ID}";

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            int result = sqlCommand.ExecuteNonQuery();

            if(result > 0) {
                Console.WriteLine($"student with ID = {ID} was deleted successfully");
            } else{
                Console.WriteLine($"student with ID = {ID} not found");
            }
        }
    }


    class Person {
        public int ID {get; set;}
        public string LastName {get; set;}
        public string FirstName {get; set;}
        public string MiddleName {get; set;}
        public string BirthDate {get; set;}
    }
}
