﻿using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class BlogDetailManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private PostRepository _postRepository;
        private TagRepository _tagRepository;
        private int _blogId;

        public BlogDetailManager(IUserInterfaceManager parentUI, string connectionString, int blogId)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _postRepository = new PostRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
            _blogId = blogId;
        }

        public IUserInterfaceManager Execute()
        {
            Blog blog = _blogRepository.Get(_blogId);
            Console.WriteLine($"{blog.Title} Details");
            Console.WriteLine(" 1) View");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Remove Tag");
            Console.WriteLine(" 4) View Post");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    View();
                    return this;
                case "2":
                    AddTag();
                    return this;
                case "3":

                    return this;
                case "4":

                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void View()
        {
            Blog blog = _blogRepository.Get(_blogId);
            Console.WriteLine($"Titile: {blog.Title}");
            Console.WriteLine($"Url: {blog.Url}");
            Console.WriteLine("Tags:");
            foreach (Tag tag in blog.Tags)
            {
                Console.WriteLine(" " + tag);
            }
            Console.WriteLine();
            Console.ReadKey();
        }

        private void AddTag()
        {
            Blog blog = _blogRepository.Get(_blogId);

            Console.WriteLine($"Which tag would you like to add to {blog.Title}?");
            List<Tag> tags = _tagRepository.GetAll();

            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                Tag tag = tags[choice - 1];
                _blogRepository.InsertTag(blog, tag);
                Console.WriteLine("Tag has been added!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection. Won't add any tags.");
            }
        }




    }
}
