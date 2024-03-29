using FileSignatures;
using Schedule_Project.Service;

namespace Schedule_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<Models.PRN221ProjectContext>();
            builder.Services.AddSingleton<IFileFormatInspector>(new FileFormatInspector());
            builder.Services.AddSingleton(new SharingContent.HandleFileUpload());
            builder.Services.AddTransient<ScheduleServices>();
            builder.Services.AddTransient<TeacherServices>();
            builder.Services.AddTransient<SubjectServices>();
            builder.Services.AddTransient<UniversityClassesServices>();
            builder.Services.AddTransient<CourseSessionServices>();
            builder.Services.AddTransient<CommonService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}