using Bank.Data;
using Bank.Interfaces;
using Bank.Models;
using Bank.Repository;
using Bank.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option => {
	option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
	option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
		In = ParameterLocation.Header,
		Description = "Please enter a valid token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "Bearer"
	});
	option.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type=ReferenceType.SecurityScheme,
					Id="Bearer"
				}
			},
			new string[]{}
		}
	});
});

builder.Services.AddControllers().AddNewtonsoftJson(options => {
	options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<ApplicationDBContext>(options => {
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(options => {
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireUppercase = true;
	options.Password.RequireNonAlphanumeric = true;
	options.Password.RequiredLength = 12;
}).AddEntityFrameworkStores<ApplicationDBContext>();

builder.Services.AddAuthentication(options => {
	options.DefaultAuthenticateScheme =
	options.DefaultChallengeScheme =
	options.DefaultForbidScheme =
	options.DefaultScheme =
	options.DefaultSignOutScheme =
	JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
	options.TokenValidationParameters = new TokenValidationParameters {
		ValidateIssuer = true,
		ValidIssuer = builder.Configuration["JWT:Issuer"],
		ValidateAudience = true,
		ValidAudience = builder.Configuration["JWT:Audience"],
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(
			System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"] ?? "r78g6fd87f6g8df76b8cv6b87df687b6d878g6fd87f6g8df76b8cv6b87df687b6d8")
		),
	};
});

builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
