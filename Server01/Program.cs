using Server01.Models;

namespace Server01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //string gg;
            //using (StreamReader sr = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "_templates/index.html")))
            //{
            //    gg = sr.ReadToEnd().HtmlToCshtml();
            //    using (StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + "/Views/Home/Index.cshtml"))
            //    {
            //        sw.Write(gg);
            //        sw.Close();
            //        //sw.Dispose();
            //    }
            //    sr.Close();
            //}

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}