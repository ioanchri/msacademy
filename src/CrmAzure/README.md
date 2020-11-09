# CRM using EF Core persisted in Azure SQL DB

CRM using EF Core persisted in Azure SQL DB
In this lesson, we learn what an ORM is, why we should use Entity Framework and why it helps us in creating a database for a rapid prototyping of our project.

ORM (What is it?)
Converts data between incompatible type systems (Object Oriented structures to relational ones)
Idea: Hide dev from DB Construction.
Old years: logic in DB. Nowadays: logic in code (testable, with C# you express better your intents)
SQL one-to-one (Department - Manager). one-to-many (Department - Employees). many-to-many (Student - Teacher).
EF Core (Introduction)
Entity Framework (EF) Core is a lightweight, extensible, open source and cross-platform version of EF
Enables .NET developers to work with a database using .NET objects.
Eliminates the need for most of the data-access code that typically needs to be written.
We will see:

Create database from existing code
Data migrations to add/modify existing schema
How to install it in your project
Install packages:
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Install dotnet CLI tools for EF: dotnet tool install --global dotnet-ef
Create a class inheriting from DbContext
Insert code for connecting to the DB in OnConfiguring() (connection string)
Insert code for configuring relationships and customizing entities in OnModelCreating()
OnModelCreating()
Use FluentAPI for configuration (more flexible, doesn't affect classes. precedence over data annotations)
DbSet types are referred as Entities (DbSet, nav props + entities defined in OnModelCreating)
Entity properties map to table columns (public props with getter/setter by default)
modelBuilder.Entity<AuditEntry>() include entity in the model

modelBuilder.Ignore<BlogMetadata>(); ignore entity

modelBuilder.Entity<Blog>().ToTable("blogs"); table name

modelBuilder.Entity<Blog>().ToTable("blogs", schema: "blogging"); (table schema)

modelBuilder.HasDefaultSchema("blogging"); (default schema)

modelBuilder.Entity<Blog>().Ignore(b => b.LoadedFromDatabase); (ignore property)

modelBuilder.Entity<Blog>().Property(b => b.BlogId).HasColumnName("blog_id"); (column name)

eb.Property(b => b.Url).HasColumnType("varchar(200)"); (column data types #1)

eb.Property(b => b.Rating).HasColumnType("decimal(5, 2)"); (column data types #2)

modelBuilder.Entity<Blog>().Property(b => b.Url).HasMaxLength(500); (max length. applicable to string, byte[])

null vs not null props (required vs optional)

modelBuilder.Entity<Blog>().Property(b => b.Url).IsRequired(); (explicit set as required)

Keys (default primary key: a property named Id or <type name>Id )
modelBuilder.Entity<Car>().HasKey(c => c.LicensePlate); (explicit set)
modelBuilder.Entity<Car>().HasKey(c => new { c.State, c.LicensePlate }); (composite key)
modelBuilder.Entity<Blog>().HasKey(b => b.BlogId).HasName("PrimaryKey_BlogId"); (property key name. default: PK_<typename>)
Value generation methods

no value generation
value generated on add (tmp on client EF. actual when saved SaveChanges(). if you add value other than default type value, EF will try insert it)
modelBuilder.Entity<Blog>().Property(b => b.Inserted).ValueGeneratedOnAdd(); value generated on add
modelBuilder.Entity<Blog>().Property(b => b.Rating).HasDefaultValue(3); default value
modelBuilder.Entity<Blog>().Property(b => b.Created).HasDefaultValueSql("getdate()"); sql default value
modelBuilder.Entity<Blog>().Property(b => b.LastUpdated).ValueGeneratedOnAddOrUpdate(); value generated on add/update
modelBuilder.Entity<Person>().Property(p => p.DisplayName).HasComputedColumnSql("[LastName] + ', ' + [FirstName]"); computed columns
modelBuilder.Entity<Blog>().Property(b => b.BlogId).ValueGeneratedNever(); no value generation
Relationships
By default, a relationship will be created when there is a navigation property discovered on a type.
Relationships that are discovered by convention will always target the primary key of the principal entity.
No Foreign Key Property(no foreign key prop found, shadow prop created)
cascade delete will be set to Cascade for required relationships and ClientSetNull for optional relationships
Inverse nav property
To configure a relationship in the Fluent API, you start by identifying the navigation properties that make up the relationship. HasOne or HasMany identifies the navigation property on the entity type you are beginning the configuration on. You then chain a call to WithOne or WithMany to identify the inverse navigation. HasOne/WithOne are used for reference navigation properties and HasMany/WithMany are used for collection navigation properties.

modelBuilder.Entity<Post>().HasOne(p => p.Blog).WithMany(b => b.Posts);
Single nav property (include just one nav property)
If you only have one navigation property then there are parameterless overloads of WithOne and WithMany. This indicates that there is conceptually a reference or collection on the other end of the relationship, but there is no navigation property included in the entity class.

modelBuilder.Entity<Blog>().HasMany(b => b.Posts).WithOne();

modelBuilder.Entity<Blog>() .Navigation(b => b.Posts).UsePropertyAccessMode(PropertyAccessMode.Property);

modelBuilder.Entity<Post>().HasOne(p => p.Blog).WithMany(b => b.Posts).HasForeignKey(p => p.BlogForeignKey); (with manual foreign key)

Many-to-many
modelBuilder.Entity<Post>().HasMany(p => p.Tags).WithMany(p => p.Posts).UsingEntity(j => j.ToTable("PostTags"));
Data seeding
Model Seed Data (OnModelCreating)
modelBuilder.Entity<Post>().HasData(new Post { PostId = 1, Title = "First"});

modelBuilder.Entity<Tag>().HasData(new Tag { TagId = "ef" });

modelBuilder.Entity<Post>() .HasMany(p => p.Tags).WithMany(p => p.Posts).UsingEntity(j => j.HasData(new { PostsPostId = 1, TagsTagId = "ef" }));

Custom Init Logic
using (var context = new DataSeedingContext())
{
    context.Database.EnsureCreated();

    var testBlog = context.Blogs.FirstOrDefault(b => b.Url == "http://test.com");
    if (testBlog == null)
    {
        context.Blogs.Add(new Blog { Url = "http://test.com" });
    }
    context.SaveChanges();
}
Migrations and Database creation
Migrations if EF Core model is the source of truth. Reverse Engineering if DB is the source of truth.
Migrations and Create/Drop APIs diff
When prototyping:

dbContext.Database.EnsureCreated(); (create DB if not exists. no tables must be present)
var sql = dbContext.Database.GenerateCreateScript(); (get SQL generation script)
dbContext.Database.EnsureDeleted(); (drop DB if exists)
Dotnet CLI Tools
Install .NET Core CLI https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet

dotnet tool install --global dotnet-ef
Before you can use the tools on a specific project, you'll need to add the Microsoft.EntityFrameworkCore.Design package to it.

dotnet add package Microsoft.EntityFrameworkCore.Design
Update tools to latest version:

dotnet tool update --global dotnet-ef
Verify installation

dotnet ef
Add migration
dotnet ef migrations add InitialCreate
Apply migration (Create DB and Schema) - Update to latest migration
dotnet ef database update
dotnet ef database update AddNewTables (update to AddNewTables migration)
Remove last migration (e.g. to add more changes) (!!! DO NOT REMOVE when applied to prod. cannot revert and may break future migrations)
dotnet ef migrations remove
List Migrations
dotnet ef migrations list
Migration files
XXXXXXXXXXXXXX_AddCreatedTimestamp.cs--The main migrations file. Contains the operations necessary to apply the migration (in Up) and to revert it (in Down).
XXXXXXXXXXXXXX_AddCreatedTimestamp.Designer.cs--The migrations metadata file. Contains information used by EF.
MyContextModelSnapshot.cs--A snapshot of your current model. Used to determine what changed when adding the next migration.
Reset all migrations
Delete folder Migrations and drop the DB; at that point you can create a new initial migration, which will contain you entire current schema.
SQL script from blank DB to latest migration
dotnet ef migrations script --idempotent
dotnet ef migrations script AddNewTables (from AddNewTables to latest)
dotnet ef migrations script AddNewTables AddAuditTable (from AddNewTables to AddAuditTable)
Customize migration code
Generated code needs review. Column rename would imply a column drop and recreate. Instead modify it to:
migrationBuilder.RenameColumn(
    name: "Name",
    table: "Customers",
    newName: "FullName");
Unify two columns would also cause data loss. Use this:
migrationBuilder.AddColumn<string>(
    name: "FullName",
    table: "Customer",
    nullable: true);

migrationBuilder.Sql(
@"
    UPDATE Customer
    SET FullName = FirstName + ' ' + LastName;
");

migrationBuilder.DropColumn(
    name: "FirstName",
    table: "Customer");

migrationBuilder.DropColumn(
    name: "LastName",
    table: "Customer");
Create empty migration and add SQL
Reverse Engineering
dotnet ef dbcontext scaffold "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Chinook" Microsoft.EntityFrameworkCore.SqlServer

(PowerShell requires you to escape the $ character, but not \.)

The --schema option can be used to include every table within a schema, while --table can be used to include specific tables.

dotnet ef dbcontext scaffold ... --table Artist --table Album

Entity types are configured using the Fluent API by default.

The scaffolded DbContext class name will be the name of the database suffixed with Context by default. To specify a different one, use --context in the .NET Core CLI.

Change output dir of entity classes (output-dir) and context class (context-dir):

dotnet ef dbcontext scaffold ... --context-dir Data --output-dir Models

Querying data
Entity Framework Core uses Language Integrated Query (LINQ) to query data from the database.

Load all data:
using (var context = new BloggingContext())
{
    var blogs = context.Blogs.ToList();
}
var blog = context.Blogs.Single(b => b.BlogId == 1); (load single entity)
var blogs = context.Blogs.Where(b => b.Url.Contains("dotnet")).ToList(); (filtering)
Loading related data
Eager loading means that the related data is loaded from the database as part of the initial query.

var blogs = context.Blogs.Include(blog => blog.Posts).Include(blog => blog.Owner).ToList();

var blogs = context.Blogs.Include(blog => blog.Posts).ThenInclude(post => post.Author).ToList(); (multiple levels)

Filtered include (Supported operations are: Where, OrderBy, OrderByDescending, ThenBy, ThenByDescending, Skip, and Take.)

    var filteredBlogs = context.Blogs
        .Include(blog => blog.Posts
            .Where(post => post.BlogId == 1)
            .OrderByDescending(post => post.Title)
            .Take(5))
        	.ThenInclude(p => p.Comments) // also include comments from posts
        .ToList();
Explicit loading means that the related data is explicitly loaded from the database at a later time.

    var blog = context.Blogs
        .Single(b => b.BlogId == 1);

    context.Entry(blog)
        .Collection(b => b.Posts)
        .Load();

    context.Entry(blog)
        .Reference(b => b.Owner)
        .Load();

    var postCount = context.Entry(blog)
        .Collection(b => b.Posts)
        .Query()
        .Count();

 var goodPosts = context.Entry(blog)
        .Collection(b => b.Posts)
        .Query()
        .Where(p => p.Rating > 3)
        .ToList();
Lazy loading means that the related data is transparently loaded from the database when the navigation property is accessed.

The simplest way to use lazy-loading is by installing the Microsoft.EntityFrameworkCore.Proxies package and enabling it with a call to UseLazyLoadingProxies. For example:

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder
        .UseLazyLoadingProxies()
        .UseSqlServer(myConnectionString);
EF Core will then enable lazy loading for any navigation property that can be overridden--that is, it must be virtual and on a class that can be inherited from. For example, in the following entities, the Post.Blog and Blog.Posts navigation properties will be lazy-loaded.

public class Blog
{
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Post> Posts { get; set; }
}

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public virtual Blog Blog { get; set; }
}
No tracking Queries
var blogs = context.Blogs.AsNoTracking().ToList(); (quicker as no change tracking needs to be setup)

Raw SQL Queries
var blogs = context.Blogs.FromSqlRaw("SELECT * FROM dbo.Blogs").ToList(); (execute sql script)

var blogs = context.Blogs.FromSqlRaw("EXECUTE dbo.GetMostPopularBlogs").ToList(); (execute SP)

Saving Data
Each context instance has a ChangeTracker that is responsible for keeping track of changes that need to be written to the database. As you make changes to instances of your entity classes, these changes are recorded in the ChangeTracker and then written to the database when you call SaveChanges.

Add data
var blog = new Blog { Url = "http://example.com" };
context.Blogs.Add(blog);
await context.SaveChangesAsync();
Update data
var blog = await context.Blogs.FirstAsync();
blog.Url = "http://example.com/blog";
await context.SaveChangesAsync();
Delete data
var blog = await context.Blogs.FirstAsync();
context.Blogs.Remove(blog);
await context.SaveChangesAsync();
Add graph of entities
    var blog = new Blog
    {
        Url = "http://blogs.msdn.com/dotnet",
        Posts = new List<Post>
        {
            new Post { Title = "Intro to C#" },
            new Post { Title = "Intro to VB.NET" },
            new Post { Title = "Intro to F#" }
        }
    };

    context.Blogs.Add(blog);
    await context.SaveChangesAsync();
Add related entity
var blog = await context.Blogs.Include(b => b.Posts).FirstAsync();
var post = new Post { Title = "Intro to EF Core" };
blog.Posts.Add(post);
await context.SaveChangesAsync();
Changing relationship
var blog = new Blog { Url = "http://blogs.msdn.com/visualstudio" };
var post = await context.Posts.FirstAsync();

post.Blog = blog;
await context.SaveChangesAsync();
Remove relationship
var blog = await context.Blogs.Include(b => b.Posts).FirstAsync();
var post = await blog.Posts.AsQueryable().FirstAsync();

blog.Posts.Remove(post);
await context.SaveChangesAsync();
Cascade delete
Cascade delete is commonly used in database terminology to describe a characteristic that allows the deletion of a row to automatically trigger the deletion of related rows. A closely related concept also covered by EF Core delete behaviors is the automatic deletion of a child entity when it's relationship to a parent has been severed--this is commonly known as "deleting orphans".

There are three actions EF can take when a principal/parent entity is deleted or the relationship to the child is severed:

The child/dependent can be deleted
The child's foreign key values can be set to null
The child remains unchanged
Transactions
By default, if the database provider supports transactions, all changes in a single call to SaveChanges are applied in a transaction. If any of the changes fail, then the transaction is rolled back and none of the changes are applied to the database. This means that SaveChanges is guaranteed to either completely succeed, or leave the database unmodified if an error occurs.

using (var scope = new TransactionScope(
    TransactionScopeOption.Required,
    new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
{
    await using var connection = new SqlConnection(connectionString);
    await connection.OpenAsync();

    try
    {
        // Run raw ADO.NET command in the transaction
        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM dbo.Blogs";
        await command.ExecuteNonQueryAsync();

        // Run an EF Core command in the transaction
        var options = new DbContextOptionsBuilder<BloggingContext>()
            .UseSqlServer(connection)
            .Options;

        await using (var context = new BloggingContext(options))
        {
            context.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/dotnet" });
            await context.SaveChangesAsync();
        }

        // Commit transaction if all commands succeed, transaction will auto-rollback
        // when disposed if either commands fails
        scope.Complete();
    }
    catch (System.Exception)
    {
        // TODO: Handle failure
    }
}