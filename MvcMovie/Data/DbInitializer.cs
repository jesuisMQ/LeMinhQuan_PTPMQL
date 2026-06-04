using Bogus;
using MvcMovie.Models.Entities;
using MvcMovie.Models.Baithuchanh13;
namespace MvcMovie.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // var studentList= context.Students.ToList();
            // context.Students.RemoveRange(studentList);

            // var bookList= context.Books.ToList();
            // context.Books.RemoveRange(bookList);

            // var faculties= context.Faculties.ToList();
            // context.Faculties.RemoveRange(faculties);

            context.Students.RemoveRange(context.Students);
            context.Books.RemoveRange(context.Books);
            context.Faculties.RemoveRange(context.Faculties);



            context.SaveChanges();

            var categories = new[]
            {
                "Programming",
                "Database",
                "AI",
                "Networking",
                "DevOps",
                "Cyber Security",
                "Cloud Computing"
            };
            var faker_book = new Faker<Book>()
                .RuleFor(x => x.ISBN,
                    f => $"978-{f.Random.Number(1000, 9999)}")

                .RuleFor(x => x.Title,
                    f => f.Commerce.ProductName())

                .RuleFor(x => x.Author,
                    f => f.Name.FullName())

                .RuleFor(x => x.Publisher,
                    f => f.Company.CompanyName())

                .RuleFor(x => x.PublishYear,
                    f => f.Random.Int(2010, 2025))

                .RuleFor(x => x.Price,
                    f => f.Random.Decimal(100000, 1000000))

                .RuleFor(x => x.Quantity,
                    f => f.Random.Int(1, 100))

                .RuleFor(x => x.Category,
                    f => f.PickRandom(categories))

                .RuleFor(x => x.Description,
                    f => f.Lorem.Paragraph())

                .RuleFor(x => x.CreatedDate,
                    f => f.Date.Recent(365))

                .RuleFor(x => x.IsAvailable,
                    f => f.Random.Bool());

            // Sinh 500 bản ghi
            var books = faker_book.Generate(500);

            context.Books.AddRange(books);


            var faculties = new List<Faculty>
                {
                    new Faculty {FacultyID="CNTT001", FacultyName = "IT" },
                    new Faculty { FacultyID="TTNT001", FacultyName = "AI" },
                    new Faculty {FacultyID="KT001", FacultyName = "Business" },
                    new Faculty {FacultyID="DL001", FacultyName = "Tourism" },
                    new Faculty {FacultyID="Mineral001", FacultyName = "Mỏ" },
                };

                context.Faculties.AddRange(faculties);

                context.SaveChanges();

            var facultyid= context.Faculties.Select(x=>x.FacultyID).ToList();
            
            var faker_student =new Faker<Student>()
                .RuleFor(x=>x.StudentCode,
                    f=>$"2221050{f.UniqueIndex:D3}")
                .RuleFor(x=>x.FullName,
                    f=>f.Name.FullName())
                .RuleFor(x=>x.Email,
                    f=>f.Internet.Email())
                .RuleFor(x=>x.FacultyID,
                    f=>f.PickRandom(facultyid))
                .RuleFor(x=>x.Status,
                    f=>f.PickRandom<StudentStatus>());

            context.Students.AddRange(faker_student.Generate(200));

            context.SaveChanges();   
        }
    }
}