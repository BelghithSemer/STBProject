using Microsoft.EntityFrameworkCore;
using projet.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestionCredits.data;

public class GestionCreditContext:Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<CreditApplication> CreditApplications { get; set; }
    public DbSet<Login> Logins { get; set; }
    public DbSet<Credit> Credits { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Sold> Solds { get; set; }
    public DbSet<CreditCards> CreditCards{ get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Data Source=(localdb)\mssqllocaldb;
            Initial Catalog = GestionCredit;
            Integrated Security = true");
    }

}
