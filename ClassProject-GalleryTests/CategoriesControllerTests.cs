using ClassProject_Gallery.Controllers;
using ClassProject_Gallery.Data;
using ClassProject_Gallery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassProject_GalleryTests
{
    [TestClass]
    public class CategoriesControllerTests
    {
        private ApplicationDbContext _context;
        CategoriesController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            // In memory db
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            // Data to mock db
            _context.Categories.Add(new Category { CategoryId = 5, Name = "One Fish", Description = "xxxxxx" });
            _context.Categories.Add(new Category { CategoryId = 10, Name = "Two Fish", Description = "xxxxxx" });
            _context.Categories.Add(new Category { CategoryId = 15, Name = "Blue Fish", Description = "xxxxxx" });
            _context.Categories.Add(new Category { CategoryId = 20, Name = "Red Fish", Description = "xxxxxx" });
            _context.Categories.Add(new Category { CategoryId = 25, Name = "No Fish", Description = "xxxxxx" });
            _context.SaveChanges();

            controller = new CategoriesController(_context);
        }

        [TestMethod]
        public void IndexReturnsIndex()
        {
            var result = (ViewResult)controller.Index().Result;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexReturnsCategories()
        {
            var result = (ViewResult)controller.Index().Result;
            var dataModel = (List<Category>)result.Model;

            // assert.  
            CollectionAssert.AreEqual(_context.Categories.ToList(), dataModel);
        }

        [TestMethod]
        public void DetailsNoIdReturns404()
        {
            // act
            var result = (ViewResult)controller.Details(null).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DetailsInvalidIdReturns404()
        {
            // act
            var result = (ViewResult)controller.Details(-1).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DetailsValidIdReturnsView()
        {
            // act
            var result = (ViewResult)controller.Details(15).Result;

            // assert
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void CreateReturnsCreate()
        {
            var result = (ViewResult)controller.Create();

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void CreatePostValidModelRedirectsToIndex()
        {
            // arrange
            var newCategory = new Category { CategoryId = 2, Name = "Fish", Description = "xxx" };

            // act
            var result = controller.Create(newCategory).Result;

            // assert
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void CreatePostValidModelRedirectsToCorrectAction()
        {
            // arrange
            var newCategory = new Category { CategoryId = 2, Name = "Fish", Description = "xxx" };

            // act
            var result = controller.Create(newCategory).Result;
            var redirectResult = (RedirectToActionResult)result;

            // assert
            Assert.AreEqual("Index", redirectResult.ActionName);
        }


        [TestMethod]
        public void CreatePostInvalidModelReturnsViewResult()
        {
            // arrange
            var newCategory = new Category { CategoryId = 0, Name = "", Description = "" };
            controller.ModelState.AddModelError("Name, Description", "Name and Description both required");

            // act
            var result = controller.Create(newCategory).Result;

            // assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void CreatePostInvalidModelReturnsSameViewWithModel()
        {
            // arrange
            var newCategory = new Category { CategoryId = 0, Name = "", Description = "" };
            controller.ModelState.AddModelError("Name, Description", "Name and Description both required");

            // act
            var result = controller.Create(newCategory).Result;
            var viewResult = (ViewResult)result;

            // assert
            Assert.AreEqual(newCategory, viewResult.Model);
        }

        [TestMethod]
        public void CreatePostValidModelAddsCategoryToDatabase()
        {
            // arrange
            var newCategory = new Category { CategoryId = 3, Name = "New Fish", Description = "xx" };

            // act
            controller.Create(newCategory).Wait();

            // assert
            var categoryDb = _context.Categories.FirstOrDefault(c => c.CategoryId == newCategory.CategoryId);
            Assert.IsNotNull(categoryDb);
        }

        [TestMethod]
        public void CreatePostValidModelUpdatesCategoryData()
        {
            // arrange
            var newCategory = new Category { CategoryId = 3, Name = "New Fish", Description = "xx" };

            // act
            controller.Create(newCategory).Wait();

            // assert
            var categoryDb = _context.Categories.FirstOrDefault(c => c.CategoryId == newCategory.CategoryId);
            Assert.AreEqual("New Fish", categoryDb.Name);
        }

        [TestMethod]
        public void CreatePostInvalidModelDoesNotUpdateDatabase()
        {
            // arrange
            var newCategory = new Category { CategoryId = 0, Name = "", Description = "" };

            controller.ModelState.AddModelError("Name, Description", "Name and Description both required ");

            // act
            controller.Create(newCategory).Wait(); 

            // assert
            var categoryInDb = _context.Categories.FirstOrDefault(c => c.CategoryId == newCategory.CategoryId);
            Assert.IsNull(categoryInDb); 
        }
    }
}