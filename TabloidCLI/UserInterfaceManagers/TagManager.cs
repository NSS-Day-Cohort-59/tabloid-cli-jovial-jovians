using System;
using System.Collections.Generic;
using System.Threading.Channels;
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

        private Tag Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Tag by the number:";
            }

            Console.WriteLine(prompt);

            List<Tag> tags = _tagRepo.GetAll();

            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine(@$" {i + 1}) {tag.Name}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return tags[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
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
           Tag tagToEdit = Choose("Which blog would you like to edit?");
            if (tagToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("Edit Name (blank to leave unchanged: ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                tagToEdit.Name = newName;
            }
            _tagRepo.Update(tagToEdit);

        }

        private void Remove()
        {
            throw new NotImplementedException();
        }
    }
}
