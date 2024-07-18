using System;
using System.Collections.Generic;

public class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}

public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; } // length in seconds
    private List<Comment> Comments { get; set; }

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }

    public List<Comment> GetComments()
    {
        return Comments;
    }
}

class Program
{
    static void Main()
    {
        // Create video instances
        var video1 = new Video("Video 1", "Author A", 300);
        var video2 = new Video("Video 2", "Author B", 200);
        var video3 = new Video("Video 3", "Author C", 400);
        var video4 = new Video("Video 4", "Author D", 150);

        // Add comments to video1
        video1.AddComment(new Comment("User1", "Great video!"));
        video1.AddComment(new Comment("User2", "Very informative."));
        video1.AddComment(new Comment("User3", "I learned a lot."));

        // Add comments to video2
        video2.AddComment(new Comment("User4", "Awesome content!"));
        video2.AddComment(new Comment("User5", "Keep it up!"));
        video2.AddComment(new Comment("User6", "Nice explanation."));

        // Add comments to video3
        video3.AddComment(new Comment("User7", "Well done!"));
        video3.AddComment(new Comment("User8", "Very helpful."));
        video3.AddComment(new Comment("User9", "Excellent video."));

        // Add comments to video4
        video4.AddComment(new Comment("User10", "Good job!"));
        video4.AddComment(new Comment("User11", "Interesting."));
        video4.AddComment(new Comment("User12", "Thanks for sharing."));

        // Store videos in a list
        var videos = new List<Video> { video1, video2, video3, video4 };

        // Iterate through the list of videos and display information
        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.Length} seconds");
            Console.WriteLine($"Number of comments: {video.GetNumberOfComments()}");
            Console.WriteLine("Comments:");
            foreach (var comment in video.GetComments())
            {
                Console.WriteLine($" - {comment.Name}: {comment.Text}");
            }
            Console.WriteLine(); // blank line between videos
        }
    }
}
