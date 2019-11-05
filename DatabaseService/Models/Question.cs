using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseService
{
  public class Question
  {
    
    public int Id { get; set; }
    public DateTime? ClosedDate { get; set; }
    public string Title { get; set; }
    
    // public int CategoryId { get; set; }
    // public Category Category { get; set; }

  }
}