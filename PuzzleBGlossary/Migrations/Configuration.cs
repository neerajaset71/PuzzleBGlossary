namespace PuzzleBGlossary.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PuzzleBGlossary.Models.PuzzleBGlossaryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PuzzleBGlossary.Models.PuzzleBGlossaryContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Glossaries.AddOrUpdate(
                x => x.ID,
                new Models.Glossary() { Term = "One", Definition = "One is a letter for numeric 1" },
                new Models.Glossary() { Term = "Two", Definition = "Two is a letter for numeric 2" },
                new Models.Glossary() { Term = "Three", Definition = "Three is a letter for numeric 3" }
            );
        }
    }
}
