using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheDailyPost.Areas.Identity.Data;

namespace TheDailyPost.Data
{
    public class PostContext : IdentityDbContext<AuthUser>
    {
        public PostContext (DbContextOptions<PostContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Post { get; set; }
        public DbSet<LikeDetails> LikeDetails { get; set; }
        public DbSet<CommentModel> CommentModel { get; set; }
    }
}
