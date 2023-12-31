using CatDogLoverManagement.Pages.Post;
using CatDogLoverManagement.Repository;
using CatDogLoverManagement.Repository.Models;
using CatDogLoverManagement.Repository.Models.ViewModels;
using CatDogLoverManagement.Repository.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CatDogLoverManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add services to the container.
            builder.Services.AddRazorPages();

            //API Image 
            builder.Services.AddControllers();

            //builder.Services.AddDbContext<CatDogLoveManagementContext>(options =>
            //options.UseSqlServer(
            //    builder.Configuration.GetConnectionString("CatDogLoverManagementDb")));

            //CRUD Repository
            builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IImageRepository, ImageRepositoryCloudinary>();
            builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
            builder.Services.AddScoped<ITimeFrameRepository, TimeFrameRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderServiceRepo, OrderServiceRepo>();

            //Login
            //builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            //    .AddEntityFrameworkStores<CatDogLoveManagementContext>();

            // Use session
            builder.Services.AddSession();
            builder.Services.AddMvc();
            builder.Services.AddHttpContextAccessor();
            builder.Services.Configure<StripeSetting>(builder.Configuration.GetSection("StripeSettings"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Session
            app.UseSession();
            //app.UseMvc();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();
            //API Images
            app.MapControllers();

            app.Run();
        }
    }
}