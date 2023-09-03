using CustomerService.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Services
{
    public class KnowledgeBaseService
    {
        private List<Article> articles;

        public KnowledgeBaseService()
        {
            articles = new List<Article>();
            // Populate the articles list with some sample data
            articles.Add(new Article { Id = 1, Title = "Article 1", Content = "Sample content for Article 1" });
            articles.Add(new Article { Id = 2, Title = "Article 2", Content = "Sample content for Article 2" });
            articles.Add(new Article { Id = 3, Title = "Article 3", Content = "Sample content for Article 3" });
        }

        public List<Article> SearchArticles(string searchTerm)
        {
            // Logic to search for articles based on the provided search term
            List<Article> searchResults = articles
                .Where(a => a.Title.ToLower().Contains(searchTerm.ToLower()) || a.Content.ToLower().Contains(searchTerm.ToLower()))
                .ToList();

            return searchResults;
        }
    }
}
