using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseService
{
  public class Question
  {
    
    public int QuestionId { get; set; }
    public DateTime? ClosedDate { get; set; }
    public string Title { get; set; }
    public int? AcceptedAnswerId { get; set; }
    public int Postid { get; set; }

    public Post Post { get; set; }
    }
}