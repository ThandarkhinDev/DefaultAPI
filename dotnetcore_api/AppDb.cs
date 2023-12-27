using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;

using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using OfficeOpenXml;

namespace MFI
{
    public class AppDb : DbContext
    {
        public string _connectionString = "";
        public AppDb(DbContextOptions<AppDb> options) : base(options)
        {

        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Adminlevel> Adminlevel { get; set; }
        public DbSet<Adminlevelmenu> Adminlevelmenu { get; set; }
        public DbSet<Adminmenu> Adminmenu { get; set; }
        public DbSet<AdminMenuDetails> AdminMenuDetails { get; set; }
        public DbSet<EventLog> EventLog { get; set; }
        public DbSet<Setting> Setting { get; set; }
        public DbSet<State> State { get; set; }
		public DbSet<AdminMenuUrl> AdminMenuUrl { get; set; }
        public DbSet<EmailTemplate> EmailTemplate { get; set; }
        public DbSet<Sample> Sample { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sample>()
                .HasKey(c => new { c.ID});

			modelBuilder.Entity<AdminMenuUrl>()
                .HasKey(c => new { c.AdminMenuID, c.ServiceUrl });
			
            modelBuilder.Entity<State>()
                .HasKey(c => new { c.stateid });

            modelBuilder.Entity<Adminlevelmenu>()
                .HasKey(c => new { c.AdminLevelID, c.AdminMenuID });

            modelBuilder.Entity<AdminMenuDetails>()
                .HasKey(c => new { c.MenuID, c.ControllerName });

            modelBuilder.Entity<EventLog>()
                .HasKey(c => new { c.ID });

        }
    }
}