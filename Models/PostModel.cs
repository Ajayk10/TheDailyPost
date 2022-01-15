using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    
    public string Content { get; set; }
    public string Author { get; set; }
    public string AuthorImg { get; set; }
    public int Date { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public Int64 DDMMYY { get; set; }
    public string ImageName { get; set; }
    [NotMapped]
    public IFormFile ImageFile { get; set; }
    public int LikeCount { get; set; }
    public IEnumerable<CommentModel> Commentor { get; set; }
    [NotMapped]
    public List<LikeDetails> LikerList { get; set; }
    [NotMapped]
    public List<CommentModel> CommentModel { get; set; }

}