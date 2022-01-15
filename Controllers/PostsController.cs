using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheDailyPost.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TheDailyPost.Areas.Identity.Data;

namespace TheDailyPost.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly PostContext _context;
        //private List<Post> sorted, sortmonth, sortyear;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PostsController(PostContext context, IWebHostEnvironment hostEnvironment, UserManager<AuthUser> userManager)
        {
            _context = context;
            this._userManager = userManager;
            this._hostEnvironment = hostEnvironment;
        }
        [Authorize]
        // GET: Posts
        public async Task<IActionResult> Index()
        {

            var comments = _context.CommentModel.ToList();
       
            ViewBag.Comments = comments;
            return View(await _context.Post.OrderByDescending(x => x.DDMMYY).ToListAsync());
        }
        [Authorize]
        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            var currentUser = _userManager.GetUserName(User);
            var liked_post_id = id;
            var liker_name_status = _context.LikeDetails.Any(ldetails=>ldetails.Liked_Post_id == id && ldetails.Likername==currentUser );
            if (liker_name_status)
            {
                ViewBag.UserStatus = true;
                ViewBag.Icon = "fas";
            }
            else
            {
                ViewBag.UserStatus =false;
                ViewBag.Icon = "far";
            }
            
            return View(post);
        }


        // GET: Details/5/<partial view>AddLike</partial view>
        public IActionResult AddLike()
        {

            return View();
        }
        // POST: Details/5/<partial view>AddLike</partial view
        [HttpPost]
        public IActionResult AddLike(int id, string likername, Post post)
        {
            LikeDetails like = new LikeDetails();
            _context.LikeDetails.Add(new LikeDetails()
            {
               Liked_Post_id = id,
                Likername = likername
            });
            var currentPost = _context.Post.Find(id);
            currentPost.LikeCount = post.LikeCount + 0;
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = id });
        }



        // GET: Details/5/<partial view>AddComment</partial view>
        public IActionResult AddComment()
        {

            return View();
        }
        // POST: Details/5/<partial view>AddComment</partial view
        [HttpPost]
        public IActionResult AddComment(int id, string commentorname,string comment, Post post)
        {
            _context.CommentModel.Add(new CommentModel()
            {
                Commented_Post_id = (int)id,
                Comment = comment,
                Commentorname = commentorname
            });
            var currentPost = _context.Post.Find(id);
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = id });
        }

        public IActionResult AllComments(int ? id)
        {
            _context.Post.ToList();
            return View(_context.CommentModel.OrderByDescending(x => x.Id).Where(cmnted=>cmnted.Commented_Post_id==id).ToList());
        }


        [Authorize]
        public async Task<IActionResult> MyActivity()
        {
            return View(await _context.Post.ToListAsync());
        }
        // GET: Posts/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }



        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Date,Month,Year,DDMMYY,Content,Author,AuthorImg,ImageFile,Likers")] Post post)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string filename = Path.GetFileNameWithoutExtension(post.ImageFile.FileName);
                string extension = Path.GetExtension(post.ImageFile.FileName);
                post.ImageName = filename = filename + DateTime.Now.ToString("yymmssff") + extension;
                string path = Path.Combine(wwwRootPath + "/Images/", filename);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await post.ImageFile.CopyToAsync(filestream);
                }
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }
        [Authorize]
        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LikeCount,Id,Title,Date,Month,Year,DDMMYY,Content,Author,ImageFile")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }
        [Authorize]
        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        public async Task<IActionResult> Search()
        {
            return View(await _context.Post.ToListAsync());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Search(string parameter)
        {
            var find = _context.Post.Where(a => a.Title.Contains(parameter));
            return View(await find.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> LikeCount(int id, [Bind("Id,LikeCount,Id,Title,Date,Month,Year,DDMMYY,Content,Author,ImageFile")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }
        //User list
        //public IActionResult Usernames()
        //{
        //    return View(_context.Users.ToListAsync());
        //}
        //About Me
        public IActionResult AboutMe()
        {

            return View();
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }
    }
}
