namespace DatabaseService
{
  public class Answer
  {

    public int AnswerId { get; set; }

    public int PostId { get; set; }
    public Post Post { get; set; }
  }
}