using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TreinbeheersysteemMain.Models;

namespace TreinbeheersysteemMain.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TreinbeheersysteemMain.Models.PerronDetailViewModel> PerronDetailViewModel { get; set; }
        public DbSet<TreinbeheersysteemMain.Models.StationDetailViewModel> StationDetailViewModel { get; set; }
        public DbSet<TreinbeheersysteemMain.Models.TicketDetailViewModel> TicketDetailViewModel { get; set; }
        public DbSet<TreinbeheersysteemMain.Models.TreinDetailViewModel> TreinDetailViewModel { get; set; }
        public DbSet<TreinbeheersysteemMain.Models.WagonDetailViewModel> WagonDetailViewModel { get; set; }
        public DbSet<TreinbeheersysteemMain.Models.TrajectDetailViewModel> TrajectDetailViewModel { get; set; }
    }
}
