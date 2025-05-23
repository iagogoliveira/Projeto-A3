using PetControl.Services;
using PetControl.Repositories;
using PetControl.Data;

var builder = WebApplication.CreateBuilder(args);

// Adicionando cache distribu�do e sess�es  
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<AppDbContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IVacinaRepository, VacinaRepository>();



builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PetService>();
builder.Services.AddScoped<VacinaService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Configurando o uso de sess�o  
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
   name: "default",
   pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
