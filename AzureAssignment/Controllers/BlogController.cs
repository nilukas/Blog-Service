using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Threading.Tasks;
using AzureAssignment.Models;
using AzureAssignment.Services;

namespace AzureAssignment.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private AzureSearch azureSearch = new AzureSearch();

        [ActionName("Index")]
        public async Task<ActionResult> IndexAsync()
        {
            var items = await BlogPostRepository<BlogItemViewModel>.GetItemsAsync();
            return View(items);
        }

        [ActionName("Create")]
        public async Task<ActionResult> CreateAsync()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<ActionResult> CreateAsync([Bind(Include = "Id,Title,Author,Post,Tags")] BlogItemViewModel item)
        {
            if (ModelState.IsValid)
            {
                item.Post.CreatedDate = DateTime.Now.ToString();
                await BlogPostRepository<BlogItemViewModel>.CreateItemAsync(item);
                return RedirectToAction("Index");
            }

            return View(item);
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync([Bind(Include = "Id,Title,Author,Post,Tags")] BlogItemViewModel item)
        {
            if (ModelState.IsValid)
            {
                BlogItemViewModel blogItem = await BlogPostRepository<BlogItemViewModel>.GetItemAsync(item.Id);

                item.UserReviews = blogItem.UserReviews;
                item.Post.CreatedDate = blogItem.Post.CreatedDate;
                await BlogPostRepository<BlogItemViewModel>.UpdateItemAsync(item.Id, item);
                return RedirectToAction("Details", new { id = item.Id });
            }

            return View(item);
        }

        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BlogItemViewModel item = await BlogPostRepository<BlogItemViewModel>.GetItemAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
                     
            await BlogPostRepository<BlogItemViewModel>.DeleteItemAsync(id);
                       
            return RedirectToAction("Index");
        }

        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BlogItemViewModel item = await BlogPostRepository<BlogItemViewModel>.GetItemAsync(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            if (item.UserReviews == null)
            {
                item.UserReviews = new List<BlogReviewViewModel>();
            }

            return View(item);
        }

        [ActionName("CreateComment")]
        public async Task<ActionResult> CreateCommentAsync(string id)
        {
            return View();
        }

        [HttpPost]
        [ActionName("CreateComment")]
        public async Task<ActionResult> CreateCommentAsync(string id,[Bind(Include = "Author,Review")] BlogReviewViewModel item)
        {
            if (ModelState.IsValid)
            {
                item.CreatedDate = DateTime.Now.ToString();
                BlogItemViewModel blog = await BlogPostRepository<BlogItemViewModel>.GetItemAsync(id);
                if (blog.UserReviews == null)
                {
                    blog.UserReviews = new List<BlogReviewViewModel>();
                }
                blog.UserReviews.Add(item);
                await BlogPostRepository<BlogItemViewModel>.UpdateItemAsync(blog.Id, blog);
                return RedirectToAction("Details", new { id = blog.Id });
            }           

            return View(item);
        }

        [ActionName("Search")]
        public async Task<ActionResult> SearchAsync(string searchText = "")
        {

            if (string.IsNullOrWhiteSpace(searchText))
                searchText = "*";
            var results = azureSearch.Search(searchText);
            var items = new List<BlogItemViewModel>();
            if (results != null) {
                foreach (var p in results.Results)
                {
                    var fields = p.Document.ToArray();
                    var blogpost = new BlogPostViewModel()
                    {
                        Title = fields[1].Value.ToString(),
                        CreatedDate = fields[5].Value.ToString(),
                        Author = fields[2].Value.ToString(),
                        Post = fields[4].Value.ToString(),
                        Tags = fields[3].Value.ToString()
                    };
                    items.Add(new BlogItemViewModel() { Id = fields[0].Value.ToString(),
                        Post = blogpost
                    });
                }
            }

            return View(items); 
        }
    }
}