using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class BlogManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public BlogManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Blog Menu");
            Console.WriteLine(" 1) List Blogs");
            Console.WriteLine(" 2) Add Blog");
            Console.WriteLine(" 3) Remove Blog");
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
            List<Blog> blogs = _blogRepository.GetAll();
            Console.WriteLine();    
            Console.WriteLine("All Blogs");
                Console.WriteLine("------------");
            foreach (Blog b in blogs)
            {
                Console.WriteLine($"{b.Id} - {b.Title}");
                
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
        }

        private Author Choose(string prompt = null)
        {
            throw new NotImplementedException();
        }

        private void Add()
        {
            Console.WriteLine("Enter your blog title.");
            string blogTitle = Console.ReadLine();
            Console.WriteLine("Enter the URL.");
            string blogURL = Console.ReadLine();

            Blog newBlog = new Blog()
            {
                Title = blogTitle,
                Url = blogURL
            };

            _blogRepository.Insert(newBlog);

            Console.WriteLine("Blog saved!");
            Console.ReadKey();
        }

        private void Edit()
        {
            throw new NotImplementedException();
        }

        private void Remove()
        {
            {
                List<Blog> blogs = _blogRepository.GetAll();
                Console.WriteLine("Choose which blog you would like to delete:");
                Console.WriteLine("");

                foreach (Blog blog in blogs)
                {
                    Console.WriteLine($"{blog.Id}: {blog.Title}");
                }
                Console.WriteLine("");
                int blogToDelete = int.Parse(Console.ReadLine());
                Console.WriteLine("");

                _blogRepository.Delete(blogToDelete);
            }
        }
    }
}