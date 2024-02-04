using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
	services.AddDbContext<LibDbContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\EC\datalagring\Uppgift\LibraryApp\LibraryApp\Infrastructure\Data\library-database.mdf;Integrated Security=True;Connect Timeout=30"))
).Build();
