using fitnessApp1.Pages;

var builder = WebApplication.CreateBuilder(args);

// Servisleri ekle
builder.Services.AddRazorPages();
builder.Services.AddAuthentication("Cookies")
    .AddCookie(options =>
    {
        options.LoginPath = "/LoginPage"; // Giri� sayfas� yolu
        options.AccessDeniedPath = "/AccessDenied"; // Eri�im reddedildi sayfas� yolu
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Hata y�netimi
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Kimlik do�rulama middleware'i
app.UseAuthorization(); // Yetkilendirme middleware'i

// Ana sayfa iste�ini /Login sayfas�na y�nlendirme
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/LoginPage"); // Giri� sayfas�na y�nlendirme
    }
    else
    {
        await next();
    }
});

app.MapRazorPages(); // Razor sayfalar�n� haritaland�rma

app.Run();
