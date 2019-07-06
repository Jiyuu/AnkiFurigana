using AnkiFurigana.Db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnkiFurigana.Db
{
    class AnkiContext: DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<Collection> Collection { get; set; }
        


        public AnkiContext(DbContextOptions<AnkiContext> options)
            : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new CollectionConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetAssembly(typeof(AnkiContext)));
        }
    }
}
