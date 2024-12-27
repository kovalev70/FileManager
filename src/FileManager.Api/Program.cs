using File = FileManager.Domain.Entities.File;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(IRepository<File>), typeof(FileRepository));
builder.Services.AddScoped(typeof(IRepository<User>), typeof(UserRepository));

builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();
builder.Services.AddScoped<IGetUserFilesUseCase, GetUserFilesUseCase>();
builder.Services.AddScoped<IUploadFileUseCase, UploadFileUseCase>();
builder.Services.AddScoped<IDownloadFileUseCase, DownloadFileUseCase>();
builder.Services.AddScoped<IDeleteFileUseCase, DeleteFileUseCase>();

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFileStorageService, FileStorageService>(provider =>
    new FileStorageService(builder.Configuration["StoragePath"]));
builder.Services.AddScoped<IHashCalculator, HashCalculator>();

builder.Services.AddAuthentication("Windows")
    .AddScheme<Microsoft.AspNetCore.Authentication.Negotiate.NegotiateOptions, Microsoft.AspNetCore.Authentication.Negotiate.NegotiateHandler>("Windows", null);

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

app.UseCors(policy =>
    policy.WithOrigins(allowedOrigins)
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();