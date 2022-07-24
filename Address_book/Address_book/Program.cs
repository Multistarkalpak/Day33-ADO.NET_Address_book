using System;

namespace AddressBookUsing_ADO.NET
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("AddressBook_Using_Ado.Net ");
            AddressBookRepo abrepo = new AddressBookRepo();
            //abrepo.CheckConnection();
            AddressBookModel abmodel = new AddressBookModel();
            AddressBookModel abmodel1 = new AddressBookModel();
            AddressBookModel delmodel = new AddressBookModel();
            while (true)
            {
                Console.WriteLine("\nEnter Choice  \n1.AddContact \n2.EditContact \n3.DeleteContact \n4.RetriveStateorCity" +
                                  "\n5.SizeofBook\n6.SortPersonNameByCity\n7.identifyEachAddressbook\n8.CountByBookType\n9.Exit\n10.Display ");
                int choice = Convert.ToInt32(Console.ReadLine());
                try
                {
                    switch (choice)
                    {
                        case 1:
                            //AddressBookModel abmodel = new AddressBookModel();
                            abmodel.First_Name = "kalpak";
                            abmodel.Last_Name = "Chincholkar";
                            abmodel.Address = "kdkr";
                            abmodel.City = "Ongl";
                            abmodel.State = "maharashtra";
                            abmodel.Zip = "445304";
                            abmodel.Phone_Number = "9492407486";
                            abmodel.Email = "kalpakc28@gmail.com";
                            abmodel.BookName = "address002";
                            abmodel.AddressbookType = "family";
                            bool result = abrepo.AddContact(abmodel);

                            if (result)
                                Console.WriteLine("Record added successfully...");
                            else
                                Console.WriteLine("Some problem is there...");
                            Console.ReadKey();
                            break;
                        case 2:
                            // Update recods
                            //AddressBookModel abmodel1 = new AddressBookModel();
                            abmodel1.First_Name = "Gurpreet";
                            abmodel1.Last_Name = "Singh";
                            abmodel1.City = "england";
                            abmodel1.State = "UK";
                            abmodel1.Email = "dk@gmail.com";
                            abmodel1.BookName = "address001";
                            abmodel1.AddressbookType = "office";
                            abrepo.EditContactUsingFirstName(abmodel1);
                            Console.ReadKey();
                            break;
                        case 3:
                            delmodel.First_Name = "radha";
                            abrepo.DeleteContactUsingName(delmodel);
                            Console.ReadKey();
                            break;
                        case 4:
                            abrepo.RetrieveContactFromPerticularCityOrState();
                            Console.ReadKey();
                            break;
                        case 5:
                            abrepo.AddressBookSizeByCityANDState();
                            Console.ReadKey();
                            break;
                        case 6:
                            abrepo.SortPersonNameByCity();
                            Console.ReadKey();
                            break;
                        case 7:
                            abrepo.identifyEachAddressbooktype();
                            Console.ReadKey();
                            break;
                        case 8:
                            abrepo.GetNumberOfContactsCountByBookType();
                            Console.ReadKey();
                            break;

                        case 9://it provide the infor mation about the current environment
                               //to execute so here we are passing exite so it will exit from this process
                            Environment.Exit(0);
                            break;
                        case 10:
                            abrepo.RetrieveContact();
                            break;
                        default:
                            Console.WriteLine("Enter The Valid Choise");
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("please enter integer options only");
                }
            }
        }
    }
}