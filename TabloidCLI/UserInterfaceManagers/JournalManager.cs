using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class JournalManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;
        private string _connectionString;


        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;
        }


        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine(" 1) List all journal entries");
            Console.WriteLine(" 2) Add journal entry");
            Console.WriteLine(" 3) Remove journal entry");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
               
                case"1":
                    List();
                    return this;
                case "2":
                    Add();
                    Console.WriteLine("");
                    Console.WriteLine("Great! Entry added to journal.");
                    Console.WriteLine("");
                    return this;
                case "3":
                    Remove();
                    Console.WriteLine("");
                    Console.WriteLine("Journal entry deleted.");
                    Console.WriteLine("");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void Add()
        {
            Console.WriteLine("New Journal Entry");
            Journal journal = new Journal();

            Console.Write("Title: ");
            journal.Title = Console.ReadLine();

            Console.Write("Content: ");
            journal.Content = Console.ReadLine();

       
            journal.CreateDateTime = DateTime.Now;

            _journalRepository.Insert(journal);
        }
        private void  List()
        {
            List<Journal> journals = _journalRepository.GetAll();
            foreach (Journal journal in journals)
            {
                Console.WriteLine(@$"{journal.Title}
                                        {journal.Content}
                                        Created on {journal.CreateDateTime}");
            }
            Console.WriteLine("Press Enter to go back to menu");
            Console.ReadLine();
        }

        private void Remove()
        {
            List<Journal> journals = _journalRepository.GetAll();
            Console.WriteLine("Choose which entry you would like to delete:");
            Console.WriteLine("");

            foreach (Journal j in journals)
            {
                Console.WriteLine($"{j.Id}: {j.Title}");
            }
            Console.WriteLine("");
            int journalToDelete = int.Parse(Console.ReadLine());
            Console.WriteLine("");
            
            _journalRepository.Delete(journalToDelete);
        }
    }
}
