using fitnessApp1.Pages;

var builder = WebApplication.CreateBuilder(args);

// Servisleri ekle
builder.Services.AddRazorPages();
builder.Services.AddAuthentication("Cookies")
    .AddCookie(options =>
    {
        options.LoginPath = "/LoginPage"; // Giriþ sayfasý yolu
        options.AccessDeniedPath = "/AccessDenied"; // Eriþim reddedildi sayfasý yolu
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Hata yönetimi
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Kimlik doðrulama middleware'i
app.UseAuthorization(); // Yetkilendirme middleware'i

// Ana sayfa isteðini /Login sayfasýna yönlendirme
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/LoginPage"); // Giriþ sayfasýna yönlendirme
    }
    else
    {
        await next();
    }
});

app.MapRazorPages(); // Razor sayfalarýný haritalandýrma

app.Run();
