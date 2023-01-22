using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using minicore.Data;
using minicore.Entities;
using minicore.Interfaces;
using minicore.Models;
using minicore.ViewModels;

namespace minicore.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext context;
    private readonly ILanguageService languageService;
    private readonly UserManager<User> userManager;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, ILanguageService languageService, UserManager<User> userManager)
    {
        _logger = logger;
        this.context = context;
        this.languageService = languageService;
        this.userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var posts = await context.Posts.ToListAsync();
        var user = await GetUser();
        IEnumerable<PostDto> postsDto = posts.Select(p => new PostDto(p, user?.Id));
        HomeViewModel model = new HomeViewModel(postsDto);
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var categories = await context.Categories.ToListAsync();
        var model = new CreateViewModel(categories);
        return View(model);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CreateViewModel model)
    {
        string summary = model.Content.Length <= 200 ? model.Content : model.Content.Substring(0, 200);
        float fleschKinkaidScore = languageService.FleschKinkaidScore(model.Content);
        LanguageToolResult languageToolResult = await languageService.LanguageTool(model.Content);


        var user = await GetUser();
        var post = new Post(model.Title, model.Content, user.Id, int.Parse(model.CategoryId), fleschKinkaidScore, languageToolResult);
        await context.AddAsync(post);
        await context.SaveChangesAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Details(string id)
    {
        var user = await GetUser();
        var post = await context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        var postDto = new PostDto(post!, user.Id);
        return View(postDto);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> React(string id)
    {
        var user = await GetUser();
        var post = await context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        if (post != null)
        {
            if (post.LikedBy.Any(u => u.Id == user.Id))
            {
                post.LikedBy.Remove(user);
            }
            else
            {
                post.LikedBy.Add(user);
            }
            await context.SaveChangesAsync();
        }
        return RedirectToAction("Details", "Home", new { id = id });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    private async Task<User> GetUser()
    {
        return await userManager.GetUserAsync(HttpContext.User);
    }
}

