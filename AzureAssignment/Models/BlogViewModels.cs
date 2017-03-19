using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AzureAssignment.Models
{
    public class BlogPostViewModel
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }
        
        [JsonProperty(PropertyName = "post")]
        public string Post { get; set; }

        [JsonProperty(PropertyName = "createdDate")]
        public string CreatedDate { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public string Tags { get; set; }
    }

    public class BlogReviewViewModel
    {
        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "review")]
        public string Review { get; set; }

        [JsonProperty(PropertyName = "createdDate")]
        public string CreatedDate { get; set; }

    }

    public class BlogItemViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "post")]
        public BlogPostViewModel Post { get; set; }

        [JsonProperty(PropertyName = "userReviews")]
        public List<BlogReviewViewModel> UserReviews { get; set; }
    }

}
