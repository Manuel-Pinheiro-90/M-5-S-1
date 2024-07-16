using M_5_S_1.Service;
using M_5_S_1.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Aggiungi i servizi necessari al contenitore di servizi.
builder.Services.AddControllersWithViews();

// Configura la stringa di connessione dal file di configurazione
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Configura la connessione al database SQL Server
builder.Services.AddScoped<DbConnection>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("CON");
    return new SqlConnection(connectionString);
});

// Aggiungi i servizi per le operazioni di Cliente, Spedizione e AggiornamentoSpedizione
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ISpedizioneService, SpedizioneService>();
builder.Services.AddScoped<IAggiornamentoSpedizioneService, AggiornamentoSpedizioneService>();

// Configura il servizio di accesso al database SQL Server
builder.Services.AddScoped<SqlServerServiceBase>();

var app = builder.Build();

// Configura la pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Il valore predefinito per HSTS è 30 giorni. Potresti voler cambiarlo per gli scenari di produzione.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
