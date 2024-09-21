using Squib.UserService.API;
using Squib.UserService.API.Profile;
using Squib.UserService.API.Repository;
using Squib.UserService.API.Service; // Ensure to include the Service namespace

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add Redis distributed cache configuration
services.AddDistributedRedisCache(options =>
{
    options.Configuration = "localhost:6379"; // Ensure this points to your Redis server
});

        // Add controllers for the API
        services.AddControllers();

        // Register AutoMapper
        services.AddAutoMapper(typeof(UserProfile).Assembly);

        // Register your repository (UserRepo) for dependency injection
        services.AddScoped<IUserRepo, UserRepo>();

        // Register your service (UserServi) for dependency injection
        services.AddScoped<IUSER_Service, UserServi>(); // Make sure to add this line
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        // Ensure the app uses HTTPS redirection and routing
        app.UseHttpsRedirection();
        app.UseRouting();

        // Enable authorization if needed
        app.UseAuthorization();

        // Configure the endpoints for the controllers
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
