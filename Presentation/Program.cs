using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Presentation.Services;
var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
	services.AddDbContext<LibDbContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\EC\datalagring\Uppgift\LibraryApp\LibraryApp\Infrastructure\Data\library-database.mdf;Integrated Security=True;Connect Timeout=30"));
	services.AddScoped<AuthorRepository>();
	services.AddScoped<GenreRepository>();
	services.AddScoped<LanguageRepository>();
	services.AddScoped<PublisherRepository>();
	services.AddScoped<BookRepository>();
	services.AddScoped<CustomerRepository>();
	services.AddScoped<LoanRepository>();
	services.AddScoped<CustomerService>();
	services.AddScoped<BookService>();
	services.AddScoped<LoanService>();
	services.AddScoped<MenuService>();
}).Build();

var menuService = builder.Services.GetRequiredService<MenuService>();
menuService.AddCustomer();
