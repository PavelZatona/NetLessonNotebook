using Microsoft.EntityFrameworkCore;
using System;

namespace Notebook.Services.Implementations.DAO
{
    public class MainDbContext : DbContext
    {
        private string _dbPath;

        public MainDbContext()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _dbPath = System.IO.Path.Join(path, "notes.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={_dbPath}");
        }

        /// <summary>
        /// Заметки
        /// </summary>
        public DbSet<NoteDbo> Notes { get; set; }
    }
}
