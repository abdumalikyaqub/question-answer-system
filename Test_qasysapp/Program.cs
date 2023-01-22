using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using Test_qasysapp.Models.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
// получаем строку подключения
//string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//string connectionString2 = "Server=COMPUTER\\SQLEXPRESS;Database=testqa;Trusted_Connection=True;TrustServerCertificate=True";
/*SqlConnection connection = new SqlConnection(connectionString2);
try
{
    // Открываем подключение
    await connection.OpenAsync();
    Console.WriteLine("Подключение открыто");
}
catch (SqlException ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    // если подключение открыто
    if (connection.State == ConnectionState.Open)
    {
        // закрываем подключение
        await connection.CloseAsync();
        Console.WriteLine("Подключение закрыто...");
    }
}*/

var app = builder.Build();

//string sqlExpression = "INSERT INTO Users (FullName, Email, Password) VALUES ('Same', 'same@bk.ru', 'qwuder76')";

/*using (SqlConnection connection = new SqlConnection(connectionString2))
{
    await connection.OpenAsync();
    Console.WriteLine("Подключение открыто");*//*
    SqlCommand command = new SqlCommand(sqlExpression, connection);
    int number = await command.ExecuteNonQueryAsync();
    Console.WriteLine($"Добавлено объектов: {number}");*//*
}*/
/*QuestionRepos questionRepos = new QuestionRepos();
//var constr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
SqlConnection con = new SqlConnection(connectionString);
foreach (var item in questionRepos.GetQuestions(con))
{
    Console.WriteLine(item);
}*/



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // auth
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}");

app.Run();
