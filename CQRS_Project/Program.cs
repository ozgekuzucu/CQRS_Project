using CQRS_Project.Context;
using CQRS_Project.CQRS.Handlers.AboutHandlers;
using CQRS_Project.CQRS.Handlers.BrandHandlers;
using CQRS_Project.CQRS.Handlers.CarHandlers;
using CQRS_Project.CQRS.Handlers.CategoryHandlers;
using CQRS_Project.CQRS.Handlers.ContactHandlers;
using CQRS_Project.CQRS.Handlers.CustomerHandlers;
using CQRS_Project.CQRS.Handlers.EmployeeHandlers;
using CQRS_Project.CQRS.Handlers.LocationHandlers;
using CQRS_Project.CQRS.Handlers.ReservationHandlers;
using CQRS_Project.CQRS.Handlers.ReviewHandlers;
using CQRS_Project.CQRS.Handlers.SliderHandlers;
using CQRS_Project.CQRS.Results.ReviewResults;
using CQRS_Project.Services;
using CQRS_Project.Services.Abstract;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CqrsContext>();

builder.Services.AddScoped<CreateAboutCommandHandler>();
builder.Services.AddScoped<GetAboutByIdQueryHandler>();
builder.Services.AddScoped<GetAboutQueryHandler>();
builder.Services.AddScoped<RemoveAboutCommandHandler>();
builder.Services.AddScoped<UpdateAboutCommandHandler>();

builder.Services.AddScoped<CreateBrandCommandHandler>();
builder.Services.AddScoped<GetBrandByIdQueryHandler>();
builder.Services.AddScoped<GetBrandQueryHandler>();
builder.Services.AddScoped<RemoveBrandCommandHandler>();
builder.Services.AddScoped<UpdateBrandCommandHandler>();

builder.Services.AddScoped<CreateCarCommandHandler>();
builder.Services.AddScoped<GetCarByIdQueryHandler>();
builder.Services.AddScoped<GetCarQueryHandler>();
builder.Services.AddScoped<RemoveCarCommandHandler>();
builder.Services.AddScoped<UpdateCarCommandHandler>();
builder.Services.AddScoped<GetTotalCarsQueryHandler>();
builder.Services.AddScoped<GetAvailableCarsQueryHandler>();

builder.Services.AddScoped<CreateCategoryCommandHandler>();
builder.Services.AddScoped<GetCategoryByIdQueryHandler>();
builder.Services.AddScoped<GetCategoryQueryHandler>();
builder.Services.AddScoped<RemoveCategoryCommandHandler>();
builder.Services.AddScoped<UpdateCategoryCommandHandler>();

builder.Services.AddScoped<CreateContactCommandHandler>();
builder.Services.AddScoped<GetContactByIdQueryHandler>();
builder.Services.AddScoped<GetContactQueryHandler>();
builder.Services.AddScoped<RemoveContactCommandHandler>();
builder.Services.AddScoped<UpdateContactCommandHandler>();

builder.Services.AddScoped<CreateCustomerCommandHandler>();
builder.Services.AddScoped<GetCustomerByIdQueryHandler>();
builder.Services.AddScoped<GetCustomerQueryHandler>();
builder.Services.AddScoped<RemoveCustomerCommandHandler>();
builder.Services.AddScoped<UpdateCustomerCommandHandler>();
builder.Services.AddScoped<GetTotalCustomersCountQueryHandler>();

builder.Services.AddScoped<CreateEmployeeCommandHandler>();
builder.Services.AddScoped<GetEmployeeByIdQueryHandler>();
builder.Services.AddScoped<GetEmployeeQueryHandler>();
builder.Services.AddScoped<RemoveEmployeeCommandHandler>();
builder.Services.AddScoped<UpdateEmployeeCommandHandler>();

builder.Services.AddScoped<CreateLocationCommandHandler>();
builder.Services.AddScoped<GetLocationByIdQueryHandler>();
builder.Services.AddScoped<GetLocationQueryHandler>();
builder.Services.AddScoped<RemoveLocationCommandHandler>();
builder.Services.AddScoped<UpdateLocationCommandHandler>();
builder.Services.AddScoped<GetActiveLocationsCountQueryHandler>();

builder.Services.AddScoped<CreateReservationCommandHandler>();
builder.Services.AddScoped<GetReservationByIdQueryHandler>();
builder.Services.AddScoped<GetReservationQueryHandler>();
builder.Services.AddScoped<RemoveReservationCommandHandler>();
builder.Services.AddScoped<UpdateReservationCommandHandler>();
builder.Services.AddScoped<GetTotalReservationsQueryHandler>();

builder.Services.AddScoped<GetTotalReservationQueryHandler>();

builder.Services.AddScoped<CreateReviewCommandHandler>();
builder.Services.AddScoped<GetReviewByIdQueryHandler>();
builder.Services.AddScoped<GetReviewQueryHandler>();
builder.Services.AddScoped<RemoveReviewCommandHandler>();
builder.Services.AddScoped<UpdateReviewCommandHandler>();

builder.Services.AddScoped<CreateSliderCommandHandler>();
builder.Services.AddScoped<GetSliderByIdQueryHandler>();
builder.Services.AddScoped<GetSliderQueryHandler>();
builder.Services.AddScoped<RemoveSliderCommandHandler>();
builder.Services.AddScoped<UpdateSliderCommandHandler>();


builder.Services.AddHttpClient<LocationService>();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<IDistanceCalculationService, DistanceCalculationService>();
builder.Services.AddScoped<IFuelPriceService, FuelPriceService>();


// YENÝ Handler'lar
builder.Services.AddScoped<SearchLocationQueryHandler>();
builder.Services.AddScoped<SyncTurkishCitiesCommandHandler>();
builder.Services.AddScoped<AddLocationFromApiCommandHandler>();
builder.Services.AddScoped<ICarRecommendationService, CarRecommendationService>();
builder.Services.AddHttpClient<IContactService, ContactService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// Console logging için
builder.Services.AddLogging(logging =>
{
	logging.ClearProviders();
	logging.AddConsole();
});


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Default}/{action=Index}/{id?}")
	.WithStaticAssets();


// Test endpoint
app.MapGet("/test-ai", async (IContactService contactService) =>
{
	var reply = await contactService.GenerateAutoReplyAsync(
		"Merhaba, sipariþim ne zaman gelir?",
		"Sipariþ Hakkýnda"
	);
	return reply;
});

app.Run();
