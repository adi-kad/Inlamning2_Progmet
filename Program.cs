using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlamning_2_progmet
{
    /* CLASS: Person
     * PURPOSE: Object in the adress/contact list storing person data
     */
    class Person
    {
        public string name, adress, phoneNumber, email;

        /* METHOD: Person
         * PURPOSE: Constructor which requires four parameters containing values to be assigned to Person.
         * PARAMETERS: string name = name of Person, string adress = adress of Person, string phoneNumber = phone number of Person, string email = email of Person
         * RETURN VALUE: Assigns parameter values to Person.
         */
        public Person(string name, string adress, string phoneNumber, string email)
        {
            this.name = name;
            this.adress = adress;
            this.phoneNumber = phoneNumber;
            this.email = email;
        }

        /* METHOD: Person
         * PURPOSE: Constructor which will ask user for values (name,adress,phoneNumber,email) when called.
         * PARAMETERS: none.
         * RETURN VALUE: Asks user for values and assigns to Person.
         */
        public Person()
        {
            Console.WriteLine("Lägger till ny person");
            Console.Write("  1. ange namn:  ");
            string name = Console.ReadLine();
            Console.Write("  2. ange adress:  ");
            string adress = Console.ReadLine();
            Console.Write("  3. ange telefon: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("  4. ange email:  ");
            string email = Console.ReadLine();

            this.name = name;
            this.adress = adress;
            this.phoneNumber = phoneNumber;
            this.email = email;
        }

        /* METHOD: Person.Print 
         * PURPOSE: Prints out name and contact details for Person.
         * PARAMETERS: none.
         * RETURN VALUE: Method prints out Person attributes but returns no value.
         */
        public void Print()
        {
            Console.WriteLine($"{name}, {adress}, {phoneNumber}, {email}");
        }

        /* METHOD: Person.Edit 
         * PURPOSE: Edits Person attribute in contact list.
         * PARAMETERS: string attribute is the field in contact list that person wants to edit. string newValue is the new value to be stored.
         * RETURN VALUE: Method edits a Person attribute but returns no value.
         */
        public void Edit(string attribute, string newValue)
        {          
            switch (attribute)
            {
                case "namn":
                    name = newValue;
                    break;
                case "adress":
                    adress = newValue;
                    break;
                case "telefon":
                    phoneNumber = newValue;
                    break;
                case "email":
                    email = newValue;
                    break;
                default:
                    Console.WriteLine("Something went wrong!");
                    break;
            }
        }      
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Variable Declarations
            List<Person> contactList = new List<Person>();
            string filePath = @"..\..\address.lis";

            //Loading list from file
            Console.Write("Laddar adresslistan ... ");
            LoadListFromFile(contactList, filePath);
            Console.WriteLine("klart!");

            //Greeting message
            Console.WriteLine("Hej och välkommen till adresslistan");
            Console.WriteLine("Skriv 'sluta' för att sluta!");
            string command;

            //Command Prompt
            do
            {
                Console.Write("> ");
                command = Console.ReadLine();
                if (command == "sluta")
                {
                    Console.WriteLine("Hej då!");
                }
                else if (command == "ny")
                {
                    AddToList(contactList);                   
                }
                else if (command == "ta bort")
                {
                    RemoveFromList(contactList);
                }
                else if (command == "visa")
                {
                    ShowList(contactList);
                }
                else if (command == "ändra")
                {
                    EditList(contactList);
                }
                else
                {
                    Console.WriteLine("Okänt kommando: {0}", command);
                }
            } while (command != "sluta");
        }

        /* METHOD: ShowList (static) 
         * PURPOSE: Prints out every Person in list.
         * PARAMETERS: List<Person> contactList is the list to show.
         * RETURN VALUE: Prints out every Person in the list but returns no value.
         */
        private static void ShowList(List<Person> contactList)
        {
            foreach (Person person in contactList)
            {
                person.Print();
            }
        }

        /* METHOD: EditList (static) 
         * PURPOSE: Gives option to edit Person in contactList.
         * PARAMETERS: List<Person> contactList is the list to edit.
         * RETURN VALUE: Edits Person attribute but returns no value.
         */
        private static void EditList(List<Person> contactList)
        {
            Console.Write("Vem vill du ändra (ange namn): ");
            string contactToEdit = Console.ReadLine();

            for (int i = 0; i < contactList.Count; i++)
            {
                if (contactList[i].name == contactToEdit)
                {
                    Console.Write("Vad vill du ändra (namn, adress, telefon eller email): ");
                    string attribute = Console.ReadLine();
                    Console.Write($"Vad vill du ändra {attribute} till?: ");
                    string newValue = Console.ReadLine();

                    contactList[i].Edit(attribute, newValue);
                }
            }
        }

        /* METHOD: RemoveFromList (static) 
         * PURPOSE: Removes a Person from contactList.
         * PARAMETERS: List<Person> contactList is the list to remove a Person from.
         * RETURN VALUE: Removes Person from list but returns no value.
         */
        private static void RemoveFromList(List<Person> contactList)
        {
            Console.Write("Vem vill du ta bort (ange namn): ");
            string contactToRemove = Console.ReadLine();

            int found = -1;
            for (int i = 0; i < contactList.Count(); i++)
            {
                if (contactList[i].name == contactToRemove)
                {
                    found = i;
                    contactList.RemoveAt(found);
                }
            }
            if (found == -1)
            {
                Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", contactToRemove);
            }
        }

        /* METHOD: AddToList (static) 
         * PURPOSE: Adds a new Person to contactList.
         * PARAMETERS: List<Person> contactList is the list to add a new Person to.
         * RETURN VALUE: Adds Person to contactList but returns no value.
         */
        private static void AddToList(List<Person> contactList)
        {
            contactList.Add(new Person());
        }

        /* METHOD: LoadListFromFile (static) 
         * PURPOSE: Loads list stored in a file.
         * PARAMETERS: List<Person> contactList adds data from string filePath.
         * RETURN VALUE: Loads list stored in a file but returns no value.
         */
        private static void LoadListFromFile(List<Person> contactList, string filePath)
        {
            using (StreamReader fileStream = new StreamReader(filePath))
            {
                while (fileStream.Peek() >= 0)
                {
                    string line = fileStream.ReadLine();
                    string[] word = line.Split('#');
                    Person person = new Person(word[0], word[1], word[2], word[3]);
                    contactList.Add(person);
                }
            }
        }
    }
}
