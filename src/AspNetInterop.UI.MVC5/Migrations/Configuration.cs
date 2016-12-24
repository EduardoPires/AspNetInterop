using System.Data.Entity.Migrations;
using AspNetInterop.UI.MVC5.Models;

namespace AspNetInterop.UI.MVC5.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AspNetInteropContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    }
}