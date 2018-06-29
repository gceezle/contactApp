using AutoMapper;
using contactApp.Contracts;
using contactApp.Controllers;
using contactApp.Data;
using contactApp.Models;
using contactApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace contactApp.Test.Controllers
{
    public class PersonsControllerTests
    {
        private PersonsController _personController;
        private Mock<IPersonRepository> _personRepositoryMock = new Mock<IPersonRepository>();

        public PersonsControllerTests()
        {
            _personController = new PersonsController(_personRepositoryMock.Object);
           
        }

        [Fact]
        public void Get_ShouldGetAllPerson()
        {
            var result = _personController.Get();
            Assert.NotNull(result);
            var objectFound = result as ObjectResult;
            Assert.NotNull(objectFound);
            Assert.Equal(200, objectFound.StatusCode);
        }

        [Theory]
        [InlineData("25")]
        public async Task Get_ShouldReturnNotFound(string Id)
        {
            var options = new DbContextOptionsBuilder<ContactAppDbContext>()
               .UseInMemoryDatabase(databaseName: "Get_ShouldReturnNotFound")
               .Options;
            using (var context = new ContactAppDbContext(options))
            {
                IPersonRepository repo = new PersonReposiroty(context);

                var controller = new PersonsController(repo);
                var result = await controller.Get(Convert.ToInt16(Id));

                Assert.NotNull(result);

                var notFoundResult = result as NotFoundResult;
                Assert.NotNull(notFoundResult);
                Assert.Equal(404, notFoundResult.StatusCode);
            }
           
        }

        [Theory]
        [InlineData("5")]
        public async Task Get_ShouldReturnPerson(string Id)
        {
            var options = new DbContextOptionsBuilder<ContactAppDbContext>()
              .UseInMemoryDatabase(databaseName: "Get_ShouldReturnPerson")
              .Options;
            using (var context = new ContactAppDbContext(options))
            {
                context.Persons.Add(new Person
                {
                    Id = 5,
                    first_name = "Test Name",
                    last_name = "Test Name",
                    phone = "+56376537357"
                });

                context.SaveChanges();

                IPersonRepository repo = new PersonReposiroty(context);

                var controller = new PersonsController(repo);
                var result = await controller.Get(Convert.ToInt16(Id));

                Assert.NotNull(result);

                var objectFound = result as ObjectResult;
                Assert.NotNull(objectFound);
                Assert.Equal(200, objectFound.StatusCode);
            }
        }

        [Fact]
        public async Task Post_ShouldCreatePerson()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<PersonDTO, Person>();
            });

            var person = new PersonDTO
            {
                first_name = "Test Name",
                last_name = "Test Name",
                phone = "+56376537357"
            };
            var result = await _personController.Post(person);

            Assert.NotNull(result);

            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);

        }

        [Fact]
        public async Task Post_ShouldReturnBadRequest()
        {
            var person = new PersonDTO
            {
                last_name = "Test Name",
                phone = "+56376537357"
            };
            _personController.ModelState.AddModelError("first_name", "Required");
            var result = await _personController.Post(person);

            
            Assert.NotNull(result);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Theory]
        [InlineData("25")]
        public async Task Put_ShouldReturnNotFound(string Id)
        {
            var options = new DbContextOptionsBuilder<ContactAppDbContext>()
              .UseInMemoryDatabase(databaseName: "Put_ShouldReturnNotFound")
              .Options;
            using (var context = new ContactAppDbContext(options))
            {
                var person = new PersonDTO
                {
                    first_name = "Test Name",
                    last_name = "Test Name",
                    phone = "+56376537357"
                };
                IPersonRepository repo = new PersonReposiroty(context);

                var controller = new PersonsController(repo);
                var result = await controller.Put(Convert.ToInt16(Id), person);

                Assert.NotNull(result);

                var notFoundResult = result as NotFoundResult;
                Assert.NotNull(notFoundResult);
                Assert.Equal(404, notFoundResult.StatusCode);
            }
        }

        [Theory]
        [InlineData("25")]
        public async Task Put_ShouldReturnBadRequest(string Id)
        {
            var options = new DbContextOptionsBuilder<ContactAppDbContext>()
              .UseInMemoryDatabase(databaseName: "Put_ShouldReturnBadRequest")
              .Options;
            using (var context = new ContactAppDbContext(options))
            {
                var person = new PersonDTO
                {
                    last_name = "Test Name",
                    phone = "+56376537357"
                };
                IPersonRepository repo = new PersonReposiroty(context);

                var controller = new PersonsController(repo);
                controller.ModelState.AddModelError("first_name", "Required");
                var result = await controller.Put(Convert.ToInt16(Id), person);

                Assert.NotNull(result);

                var badRequestResult = result as BadRequestObjectResult;
                Assert.NotNull(badRequestResult);
                Assert.Equal(400, badRequestResult.StatusCode);
            }
        }

        [Theory]
        [InlineData("5")]
        public async Task Put_ShouldUpdatePerson(string Id)
        {
            var options = new DbContextOptionsBuilder<ContactAppDbContext>()
              .UseInMemoryDatabase(databaseName: "Put_ShouldUpdatePerson")
              .Options;
            using (var context = new ContactAppDbContext(options))
            {
                context.Persons.Add(new Person
                {
                    Id = 5,
                    first_name = "Test Name",
                    last_name = "Test Name",
                    phone = "+56376537357"
                });

                context.SaveChanges();
                var person = new PersonDTO
                {
                    first_name = "Updated Test Name",
                    last_name = "Updated Test Name",
                    phone = "+56376537357"
                };
                IPersonRepository repo = new PersonReposiroty(context);

                var controller = new PersonsController(repo);
                var result = await controller.Put(Convert.ToInt16(Id), person);

                Assert.NotNull(result);

                var objectFound = result as ObjectResult;
                Assert.NotNull(objectFound);
                Assert.Equal(200, objectFound.StatusCode);
            }
        }

        [Theory]
        [InlineData("25")]
        public async Task Delete_ShouldReturnNotFound(string Id)
        {
            var options = new DbContextOptionsBuilder<ContactAppDbContext>()
              .UseInMemoryDatabase(databaseName: "Delete_ShouldReturnNotFound")
              .Options;
            using (var context = new ContactAppDbContext(options))
            {
               
                IPersonRepository repo = new PersonReposiroty(context);

                var controller = new PersonsController(repo);
                var result = await controller.Delete(Convert.ToInt16(Id));

                Assert.NotNull(result);

                var notFoundResult = result as NotFoundResult;
                Assert.NotNull(notFoundResult);
                Assert.Equal(404, notFoundResult.StatusCode);
            }
        }

        [Theory]
        [InlineData("5")]
        public async Task Delete_ShouldDeletePerson(string Id)
        {
            var options = new DbContextOptionsBuilder<ContactAppDbContext>()
              .UseInMemoryDatabase(databaseName: "Put_ShouldUpdatePerson")
              .Options;
            using (var context = new ContactAppDbContext(options))
            {
                context.Persons.Add(new Person
                {
                    Id = 5,
                    first_name = "Test Name",
                    last_name = "Test Name",
                    phone = "+56376537357"
                });

                context.SaveChanges();
                IPersonRepository repo = new PersonReposiroty(context);

                var controller = new PersonsController(repo);
                var result = await controller.Delete(Convert.ToInt16(Id));

                Assert.NotNull(result);

                var noContentResult = result as NoContentResult;
                Assert.NotNull(noContentResult);
                Assert.Equal(204, noContentResult.StatusCode);
            }
        }

        


    }
}
