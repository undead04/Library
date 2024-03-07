using FluentValidation.AspNetCore;
using Library.Data;
using Library.Model;
using Library.Server;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolLibrary.Model;
using SchoolLibrary.Server;
using SchoolLibrary.Validation;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ChanglePassWordValidation>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Book API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
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

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<MyDB>().AddDefaultTokenProviders();
builder.Services.AddDbContext<MyDB>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDb"));
});



// Life cycle DI: AddSingleton(), AddTransient(), AddScoped()

builder.Services.AddScoped<IAccountReponsitory, AccountReponsitory>();
//load thông tin cấu hình và lưu vào đối tượng MailSetting
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
//add dependency inject cho MailService
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddScoped<IReissuePassword, ReissuePasswordReponsitory>();
builder.Services.AddScoped<IUserReponsitory, UserReponsitory>();
builder.Services.AddScoped<IRoleReponsitory, RoleReponsitory>();
builder.Services.AddScoped<ISubjectReponsitory, SubjectReponsitory>();
builder.Services.AddScoped<ITopicReponsitory, TopicReponsitory>();
builder.Services.AddScoped<ILessonReponsitory, LessonReponsitory>();
builder.Services.AddScoped<IClassReponsitory,ClassReponsitory>();
builder.Services.AddScoped<IMajorReponsitory, MajorReponsitory>();
builder.Services.AddScoped<IDocumentReponsitory, DocumentReponsitory>();
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
await SeedRole.CreateRoleAsync(app);
app.MapControllers();
app.UseCors();
app.Run();
