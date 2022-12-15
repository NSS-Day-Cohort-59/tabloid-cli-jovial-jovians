using System;
using System.Collections.Generic;
using System.Data;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _connectionString = connectionString;
            _postRepository= new PostRepository(connectionString);
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Post Details");
            Console.WriteLine(" 3) Add Post");
            Console.WriteLine(" 4) Edit Post");
            Console.WriteLine(" 5) Remove Post");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "4":
                    Edit();
                    return this;
                case "5":
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
            List<Post> posts = _postRepository.GetAll();
            foreach (Post p in posts)
            {
                Console.WriteLine($"{p.Id}) {p.Title}  URL: {p.Url}");
            }
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

        private void Edit()
        {
            Post postToEdit = Choose("Which post would you like to edit?");
            if (postToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("Edit title (blank to leave unchanged: ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                postToEdit.Title = title;
            }
            Console.Write("Edit author (blank to leave unchanged: ");
            string authorName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(authorName))
            {
                postToEdit.Author = new Author
                {
                    Id = ,
                }
            }
            Console.Write("Edit blog (blank to leave unchanged: ");
            string blogInfo = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(blogInfo))
            {
                postToEdit.Blog = blogInfo;
            }
            Console.Write("Edit URL (blank to leave unchanged: ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                postToEdit.Url = url;
            }
            Console.Write("Edit date of publication (blank to leave unchanged: ");
            string published = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(published))
            {
                postToEdit.PublishDateTime = DateTime.Parse(published);
            }

            Console.WriteLine("");
            Console.WriteLine("Post has been updated.");
            Console.WriteLine("");
            Console.Write("Press any key to continue...");
            Console.ReadKey();

            _postRepository.Update(postToEdit);
        }
        private Post Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Post:";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }
        private void Remove()
        {
            Post postToDelete = Choose("Which post would you like to remove?");
            if (postToDelete != null)
            {
                _postRepository.Delete(postToDelete.Id);
            }
        }
    }
}