using FileUploadWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileUploadWebApi.Data
{
    public class FileUploadDbContext  : DbContext
    {
        protected readonly  IConfiguration _configuration;
        public FileUploadDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<FileDetails> FileDetails { get; set; }
    }
}