using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AddressBookUsing_ADO.NET
{
    public class AddressBookRepo
    {
        public static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=addressbook_service;Integrated Security=True;";
        SqlConnection connection = new SqlConnection(connectionString);

        public void CheckConnection()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Database_Connected_Successfully....");
                }
            }
            catch
            {
                Console.WriteLine("Database_NOT_Connected!!!");
            }
            finally
            {
                this.connection.Close();
            }
        }
        public bool AddContact(AddressBookModel model)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("addressProcedure", this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@First_Name", model.First_Name);
                    command.Parameters.AddWithValue("@Last_Name", model.Last_Name);
                    command.Parameters.AddWithValue("@Address", model.Address);
                    command.Parameters.AddWithValue("@City", model.City);
                    command.Parameters.AddWithValue("@State", model.State);
                    command.Parameters.AddWithValue("@Zip", model.Zip);
                    command.Parameters.AddWithValue("@Phone_Number", model.Phone_Number);
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@BookName", model.BookName);
                    command.Parameters.AddWithValue("@AddressbookType", model.AddressbookType);
                    this.connection.Open();
                    var result = command.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
        public void RetrieveContact()//display
        {
            try
            {
                AddressBookModel model = new AddressBookModel();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(
                        @"SELECT * FROM address_book;"
                            , connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                model.First_Name = reader.GetString(0);
                                model.Last_Name = reader.GetString(1);
                                model.Address = reader.GetString(2);
                                model.City = reader.GetString(3);
                                model.State = reader.GetString(4);
                                model.Zip = reader.GetString(5);
                                model.Phone_Number = reader.GetString(6);
                                model.Email = reader.GetString(7);

                                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", model.First_Name, model.Last_Name, model.Address, model.City,
                                    model.State, model.Zip, model.Phone_Number, model.Email);
                                Console.WriteLine("\n");
                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void EditContactUsingFirstName(AddressBookModel model)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string updateQuery = @"UPDATE address_book SET last_name = @Last_Name, city = @City, state = @State, email = @Email, bookname = @BookName, addressbooktype = @AddressbookType WHERE first_name = @First_Name;";
                    SqlCommand command = new SqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@First_Name", model.First_Name);
                    command.Parameters.AddWithValue("@Last_Name", model.Last_Name);
                    command.Parameters.AddWithValue("@City", model.City);
                    command.Parameters.AddWithValue("@State", model.State);
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@BookName", model.BookName);
                    command.Parameters.AddWithValue("@AddressbookType", model.AddressbookType);

                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Contact Updated successfully...");
                    this.connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
        public void DeleteContactUsingName(AddressBookModel model)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //SqlCommand command = new SqlCommand("deletcontactProcedure", connection);
                    //command.CommandType = CommandType.StoredProcedure;
                    string deleteQuery = @"Delete from address_book WHERE first_name = @First_Name;";
                    SqlCommand command = new SqlCommand(deleteQuery, connection);
                    command.Parameters.AddWithValue("@First_Name", model.First_Name);
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Contact Deleted successfully...");
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
        public void RetrieveContactFromPerticularCityOrState()
        {
            try
            {
                AddressBookModel model = new AddressBookModel();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(
                        @"SELECT * FROM address_book WHERE city = 'Parbhani' OR state = 'Kerala'; 
                            SELECT * FROM address_book WHERE city = 'Hyderabad' OR state = 'Telengana';", connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                model.First_Name = reader.GetString(0);
                                model.Last_Name = reader.GetString(1);
                                model.Address = reader.GetString(2);
                                model.City = reader.GetString(3);
                                model.State = reader.GetString(4);
                                model.Zip = reader.GetString(5);
                                model.Phone_Number = reader.GetString(6);
                                model.Email = reader.GetString(7);

                                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", model.First_Name, model.Last_Name, model.Address, model.City,
                                    model.State, model.Zip, model.Phone_Number, model.Email);
                                Console.WriteLine("\n");
                            }
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    model.First_Name = reader.GetString(0);
                                    model.Last_Name = reader.GetString(1);
                                    model.Address = reader.GetString(2);
                                    model.City = reader.GetString(3);
                                    model.State = reader.GetString(4);
                                    model.Zip = reader.GetString(5);
                                    model.Phone_Number = reader.GetString(6);
                                    model.Email = reader.GetString(7);

                                    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", model.First_Name, model.Last_Name, model.Address, model.City,
                                        model.State, model.Zip, model.Phone_Number, model.Email);
                                    Console.WriteLine("\n");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void AddressBookSizeByCityANDState()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(
                        @"select count(first_name) from address_book WHERE city = 'Bengaluru' AND state = 'karnataka'; 
                        select count(first_name) from address_book WHERE city = 'england' AND state = 'UK';", connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int count = reader.GetInt32(0);
                                Console.WriteLine("Total Contacts From City Bengaluru And State karnataka: {0}", +count);
                            }
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    int count = reader.GetInt32(0);
                                    Console.WriteLine("Total Contacts From City england And State UK: {0}", +count);
                                }
                            }
                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void SortPersonNameByCity()
        {
            try
            {
                AddressBookModel model = new AddressBookModel();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(
                        @"SELECT * FROM address_book WHERE city = 'Bengaluru' order by first_name; 
                        SELECT * FROM address_book WHERE city = 'Mysore' order by first_name, last_name;", connection))
                    {
                        connection.Open();//it will open the specified string connection using sql connection
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("Sorted Contact Using first name from Bengaluru");

                            while (reader.Read())
                            {
                                model.First_Name = reader.GetString(0);//other than 2 stirng i am using getstirng so that i can specify the column
                                model.Last_Name = reader.GetString(1);
                                model.Address = reader.GetString(2);
                                model.City = reader.GetString(3);
                                model.State = reader.GetString(4);
                                model.Zip = reader.GetString(5);
                                model.Phone_Number = reader.GetString(6);
                                model.Email = reader.GetString(7);

                                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", model.First_Name, model.Last_Name, model.Address, model.City,
                                        model.State, model.Zip, model.Phone_Number, model.Email);
                                Console.WriteLine("\n");
                            }
                            if (reader.NextResult())
                            {
                                Console.WriteLine("Sorted Contact Using First_Name from Mysore");

                                while (reader.Read())
                                {
                                    model.First_Name = reader.GetString(0);
                                    model.Last_Name = reader.GetString(1);
                                    model.Address = reader.GetString(2);
                                    model.City = reader.GetString(3);
                                    model.State = reader.GetString(4);
                                    model.Zip = reader.GetString(5);
                                    model.Phone_Number = reader.GetString(6);
                                    model.Email = reader.GetString(7);

                                    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", model.First_Name, model.Last_Name, model.Address, model.City,
                                        model.State, model.Zip, model.Phone_Number, model.Email);
                                    Console.WriteLine("\n");
                                }
                            }
                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void identifyEachAddressbooktype()
        {
            try
            {
                AddressBookModel model = new AddressBookModel();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(
                        @"SELECT * FROM address_book WHERE addressbooktype = 'family'; 
                        SELECT * FROM address_book WHERE addressbooktype = 'friend';
                        SELECT * FROM address_book WHERE addressbooktype = 'Proffession';", connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("identify each addrass book by person name");

                            while (reader.Read())
                            {
                                model.First_Name = reader.GetString(0);
                                model.Last_Name = reader.GetString(1);
                                model.Address = reader.GetString(2);
                                model.City = reader.GetString(3);
                                model.State = reader.GetString(4);
                                model.Zip = reader.GetString(5);
                                model.Phone_Number = reader.GetString(6);
                                model.Email = reader.GetString(7);

                                Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", model.First_Name, model.Last_Name, model.Address, model.City,
                                        model.State, model.Zip, model.Phone_Number, model.Email);
                                Console.WriteLine("\n");
                            }
                            if (reader.NextResult())
                            {
                                //Console.WriteLine("identify each addrass book by person name");
                                //Console.WriteLine("===========================================");
                                while (reader.Read())
                                {
                                    model.First_Name = reader.GetString(0);
                                    model.Last_Name = reader.GetString(1);
                                    model.Address = reader.GetString(2);
                                    model.City = reader.GetString(3);
                                    model.State = reader.GetString(4);
                                    model.Zip = reader.GetString(5);
                                    model.Phone_Number = reader.GetString(6);
                                    model.Email = reader.GetString(7);

                                    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", model.First_Name, model.Last_Name, model.Address, model.City,
                                        model.State, model.Zip, model.Phone_Number, model.Email);
                                    Console.WriteLine("\n");
                                }
                            }
                            if (reader.NextResult())
                            {
                                Console.WriteLine("identify each addrass book by person name");
                                Console.WriteLine("===========================================");
                                while (reader.Read())
                                {
                                    model.First_Name = reader.GetString(0);
                                    model.Last_Name = reader.GetString(1);
                                    model.Address = reader.GetString(2);
                                    model.City = reader.GetString(3);
                                    model.State = reader.GetString(4);
                                    model.Zip = reader.GetString(5);
                                    model.Phone_Number = reader.GetString(6);
                                    model.Email = reader.GetString(7);

                                    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", model.First_Name, model.Last_Name, model.Address, model.City,
                                        model.State, model.Zip, model.Phone_Number, model.Email);
                                    Console.WriteLine("\n");
                                }
                            }
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    var count = reader.GetInt32(0);
                                    Console.WriteLine("Number of Contacts From Addressbook_Type_Office:{0} ", +count);
                                    Console.WriteLine("\n");
                                }
                            }

                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void GetNumberOfContactsCountByBookType()
        {
            try
            {
                using (this.connection)
                {
                    using (SqlCommand command = new SqlCommand(
                        @"SELECT COUNT(first_name) FROM address_book WHERE addressbooktype = 'family'; 
                        SELECT COUNT(first_name) FROM address_book WHERE addressbooktype = 'friend';
                        SELECT COUNT(first_name) FROM address_book WHERE addressbooktype = 'Profession';", connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())//it retruns the data in form of rows
                        {//it reads the rows from the sql server
                            while (reader.Read())//it advances to the next record
                            {
                                int count = reader.GetInt32(0);//gtet the values of specified column
                                Console.WriteLine("Number of Contacts From Addressbook_Type_Family:{0} ", +count);
                                Console.WriteLine("\n");
                            }
                            if (reader.NextResult())//nextresult=this method advances the data reader to nextresult
                                                    //and it will returns true or false as returntype
                            {
                                while (reader.Read())
                                {
                                    var count = reader.GetInt32(0);
                                    Console.WriteLine("Number of Contacts From Addressbook_Type_Friend:{0} ", +count);
                                    Console.WriteLine("\n");
                                }
                            }

                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}