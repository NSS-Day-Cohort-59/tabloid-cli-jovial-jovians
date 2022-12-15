using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class TagManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;

        private TagRepository _tagRepo;

        public TagManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _tagRepo = new TagRepository(connectionString);
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Tag Menu");
            Console.WriteLine(" 1) List Tags");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Edit Tag");
            Console.WriteLine(" 4) Remove Tag");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            {
                List<Tag> tags = _tagRepo.GetAll();
                Console.WriteLine();
                Console.WriteLine("All Tags");
                Console.WriteLine("------------");
                foreach (Tag t in tags)
                {
                    Console.WriteLine($"{t.Id} - {t.Name}");

                }
                Console.WriteLine();
                Console.WriteLine("Press any key to go back");
                Console.ReadKey();
            }
        }

        private void Add()
        {

            Tag newTag = new Tag();

            Console.WriteLine();
            Console.Write("Enter name of new tag > ");
            newTag.Name = Console.ReadLine();

            _tagRepo.Insert(newTag);

            Console.WriteLine($"New tag \"{newTag.Name}\" added!");
            Console.ReadKey();
            
        }

        private void Edit()
        {
            throw new NotImplementedException();
        }

        private void Remove()
        {
            throw new NotImplementedException();
        }
    }
}
