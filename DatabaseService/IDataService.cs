using System;
using System.Collections.Generic;

namespace DatabaseService
{
  public interface IDataService
  {
    List<Question> GetQuestions();
    Question GetQuestion(int id);

    QuestionDto GetFullQuestion(int id);

    List<Answer> GetAnswers();
    Answer GetAnswer(int id);

    List<Post> GetPosts();
    Post GetPost(int id);
       

        /*
    List<Annotation> GetAnnotations();
    Annotation GetAnnotation(int id);
    */
        // Category CreateCategory(string name, string description);
        // // Category CreateCategory(Category category);
        // bool DeleteCategory(int id);
        // List<Category> GetCategories();
        // Order GetOrder(int id);
        // List<OrderDetails> GetOrderDetailsByOrderId(int id);
        // List<OrderDetails> GetOrderDetailsByProductId(int id);
        // List<Order> GetOrders();
        // Product GetProduct(int id);
        // List<Product> GetProductByCategory(int id);
        // List<Product> GetProductByName(string name);
        // bool UpdateCategory(int id, string name, string description);
        // bool PutCategory(int id, string name, string description);
    }
}