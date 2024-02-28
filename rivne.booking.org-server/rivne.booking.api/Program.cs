using rivne.booking.Infrastructure;
using Rivne.Booking.Application;
using rivne.booking.api;


var builder = WebApplication.CreateBuilder(args);

builder.Services
	.AddInfrastructure(builder.Configuration)
	.AddApplication()
	.AddPresentation(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCustomStaticFiles();

app.UseCors(options => 
	options.SetIsOriginAllowed(origin => true)
		.AllowAnyHeader()
		.AllowCredentials()
		.AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//ApiUsersInitializer.SeedData(app);

app.Run();
