namespace NewspaperManangment.Entities;

public class Comment
{
    public int Id { get; set; }
    public int TheNewId  { get; set; }
    public string comment {get;set;}
    public int? ReplyId { get; set; }

    public TheNew TheNew { get; set; }
    public HashSet<Comment> theNew_Comments { get; set; } = new ();
    public Comment Comment1 { get; set; }
}