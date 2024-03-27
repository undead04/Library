using FluentValidation.AspNetCore;
using Library.Data;
using Library.Model;
using Library.Services.AccountReponsitory;
using Library.Services.ClassReponsitory;
using Library.Services.DocumentRepository;
using Library.Services.LessonReponsitory;
using Library.Services.MailService;
using Library.Services.MajorReposntiory;
using Library.Services.NotificationSubjectRepository;
using Library.Services.QuestionRepository;
using Library.Services.ReissuePassword;
using Library.Services.ReplyQuestionRepository;
using Library.Services.ResourceReponsitory;
using Library.Services.RoleReponsitory;
using Library.Services.SubjectReponsitory;
using Library.Services.TopicReponsitory;
using Library.Services.UserReponsitory;
using Library.Services.UploadService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolLibrary.Validation;
using System.Text;
using Library.Services.ClassLessonRepository;
using Library.Services.MultipleChoiceRepository;
using Library.Services.ExamRepository;
using Library.Services.HelpRepository;
using Library.Services.MySubjectService;
using Library.Services.ApproveDocumetService;
using Library.Services.PrivateFileRepository;
using Library.Services.ApproveExamServices;
using Library.Services.ExcelService;
using Microsoft.AspNetCore.WebSockets;
using Library.Services.SystemNotificationRepository;
using Library.Services.NotificationRepository;
using Library.Services.MyNotificationRepository;
using Library.Services.HistoryLikeRepository;
using Library.Services.JWTService;
using Library.Services.ClaimsService;

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
builder.Services.AddScoped<IResourceReponsitory, ResourceReponsitory>();
builder.Services.AddScoped<IQuestionSubjectRepository, QuestionSubjectRepository>();
builder.Services.AddScoped<INotificationSubjectRepository,NotificationSubjectRepository>();
builder.Services.AddScoped<IReplyQuestionRepository, ReplyQuestionRepository>();
builder.Services.AddScoped<IUploadService, UploadService>();
builder.Services.AddScoped<IClassLessonRepository, ClassLessonRepository>();
builder.Services.AddScoped<IMultipleChoiceRepository, MultipleChoiceRepository>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();
builder.Services.AddScoped<IHelpRepository, HelpRepository>();
builder.Services.AddScoped<IMySubjectService, MySubjectService>();
builder.Services.AddScoped<IApproveDocumetService, ApproveDocumentService>();
builder.Services.AddScoped<IPrivateFileRepository, PrivateFileRepository>();
builder.Services.AddScoped<IApporveExamServices, ApproveExamService>();
builder.Services.AddScoped<IExcelService, ExcelService>();
builder.Services.AddScoped<ISystemNotificationRepository, SystemNotificationRepository>();
builder.Services.AddScoped<INotificationRepository,NotificationRepository>();
builder.Services.AddScoped<IMyNotificationRepository, MyNotificationRepository>();
builder.Services.AddScoped<IHistoryLikeRepository, HistoryLikeRepository>();
builder.Services.AddScoped<IJWTSevice, JWTService>();
builder.Services.AddScoped<IClaimService, IClaimService>();
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
// phân quyền
var policyConfigurations = new List<PolicyConfiguration>
        {
            new PolicyConfiguration { PolicyName = "SubjectView", ClaimType = "subject", ClaimValue = "view" },
            new PolicyConfiguration { PolicyName = "SubjectEdit", ClaimType = "subject", ClaimValue = "edit" },
            new PolicyConfiguration { PolicyName = "PrivateCreate", ClaimType = "private", ClaimValue = "create" },
            new PolicyConfiguration { PolicyName = "PrivateView", ClaimType = "private", ClaimValue = "view" },
            new PolicyConfiguration { PolicyName = "PrivateEdit", ClaimType = "private", ClaimValue = "edit" },
            new PolicyConfiguration { PolicyName = "PrivateDelete", ClaimType = "private", ClaimValue = "delete" },
            new PolicyConfiguration { PolicyName = "PrivateDownload", ClaimType = "private", ClaimValue = "download" },
            new PolicyConfiguration { PolicyName = "DocumentCreate", ClaimType = "document", ClaimValue = "create" },
            new PolicyConfiguration { PolicyName = "DocumentView", ClaimType = "document", ClaimValue = "view" },
            new PolicyConfiguration { PolicyName = "DocumentEdit", ClaimType = "document", ClaimValue = "edit" },
            new PolicyConfiguration { PolicyName = "DocumentDelete", ClaimType = "document", ClaimValue = "delete" },
            new PolicyConfiguration { PolicyName = "DocumentDownload", ClaimType = "document", ClaimValue = "download" },
            new PolicyConfiguration { PolicyName = "DocumentAdd", ClaimType = "document", ClaimValue = "add" },
            new PolicyConfiguration { PolicyName = "ExamCreate", ClaimType = "exam", ClaimValue = "create" },
            new PolicyConfiguration { PolicyName = "ExamView", ClaimType = "exam", ClaimValue = "view" },
            new PolicyConfiguration { PolicyName = "ExamEdit", ClaimType = "exam", ClaimValue = "edit" },
            new PolicyConfiguration { PolicyName = "ExamDelete", ClaimType = "exam", ClaimValue = "delete" },
            new PolicyConfiguration { PolicyName = "ExamDownload", ClaimType = "exam", ClaimValue = "download" },
            new PolicyConfiguration { PolicyName = "ExamApprove", ClaimType = "exam", ClaimValue = "approve" },
            new PolicyConfiguration { PolicyName = "NotificationView", ClaimType = "notification", ClaimValue = "view" },
            new PolicyConfiguration { PolicyName = "NotificationSystem", ClaimType = "notification", ClaimValue = "system" },
            new PolicyConfiguration { PolicyName = "NotificationEdit", ClaimType = "notification", ClaimValue = "edit" },
            new PolicyConfiguration { PolicyName = "NotificationDelete", ClaimType = "notification", ClaimValue = "delete" },
            new PolicyConfiguration { PolicyName = "RoleCreate", ClaimType = "role", ClaimValue = "create" },
            new PolicyConfiguration { PolicyName = "RoleView", ClaimType = "role", ClaimValue = "view" },
            new PolicyConfiguration { PolicyName = "RoleEdit", ClaimType = "role", ClaimValue = "edit" },
            new PolicyConfiguration { PolicyName = "RoleDelete", ClaimType = "role", ClaimValue = "delete" },
            new PolicyConfiguration { PolicyName = "AccountView", ClaimType = "Account", ClaimValue = "view" },
            new PolicyConfiguration { PolicyName = "AccountEdit", ClaimType = "Account", ClaimValue = "edit" },
            // Add more policy configurations as needed
        };

// Add authorization policies dynamically based on the configurations
foreach (var policyConfig in policyConfigurations)
{
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(policyConfig.PolicyName, policy =>
        {
            policy.RequireClaim(policyConfig.ClaimType, policyConfig.ClaimValue);
        });
    });
}
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors();
app.Run();
