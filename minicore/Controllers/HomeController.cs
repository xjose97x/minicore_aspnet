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
        IEnumerable<PostDto> postsDto = posts.Select(p => new PostDto(p));
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

