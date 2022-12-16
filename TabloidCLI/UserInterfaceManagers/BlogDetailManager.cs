using System;
using System.Collections.Generic;
using System.Linq;
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

                    return this;
                case "3":
                    RemoveTag();
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
            Console.WriteLine($"Title: {blog.Title}");
            Console.WriteLine($"Url: {blog.Url}");
            Console.Write($"Tags: ");
            string tagString = string.Join(", ", blog.Tags);
            Console.WriteLine(tagString);
            Console.WriteLine("Press enter to go back:");
            Console.ReadLine();
        }

        private void RemoveTag()
        {
            Blog blog = _blogRepository.Get(_blogId);
            for (int i = 1;  i <= blog.Tags.Count; i++)
            {
                Console.WriteLine($" {i}) { blog.Tags[i - 1]}");
            }
            Console.Write("Choose a tag to remove from this blog > ");
            try
            {
                int tagIndex = int.Parse(Console.ReadLine()) - 1;
                int tagId = blog.Tags[tagIndex].Id;
                _blogRepository.DeleteBlogTag(_blogId, tagId);
                Console.WriteLine("Tag removed");
                Console.ReadKey();
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid selection, please try again");
            }

        }




    }
}
