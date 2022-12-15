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
            Console.WriteLine("Jourtnal Menu");
            Console.WriteLine(" 1) Show All Jurnal Entries");
            Console.WriteLine(" 2) Add Jurnal Entry");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
               
                case"1":
                    Console.WriteLine("Work!!");
                    List();
                    return this;
                case "2":
                    Add();
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
      


    }

    }
