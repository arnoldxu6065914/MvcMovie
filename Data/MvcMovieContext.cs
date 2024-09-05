using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        // Movie 数据表
        public DbSet<MvcMovie.Models.Movie> Movie { get; set; } = default!;
        // ApplicationUser 数据表
        public DbSet<ApplicationUser> Users { get; set; } = default!;  // 添加用户表
    
    }
}
