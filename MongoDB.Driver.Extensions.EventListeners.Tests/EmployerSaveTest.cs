using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Autofac;
using MongoDB.Bson;
using MongoDB.Driver.Extensions.EventListeners.Tests.Entities;
using NUnit.Framework;

namespace MongoDB.Driver.Extensions.EventListeners.Tests
{
    [TestFixture]
    public class EmployerSaveTest
    {
        private MongoCollection<Employer> _employerCollection;

        private IContainer _container = null;

        [SetUp]
        public void Setup()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<EventDataLoader>().As<IEventDataLoader>();
            containerBuilder.RegisterType<DefaultPreSaveEventListener>().As<IPreSaveOrUpdateEventListener>();
            
            _container = containerBuilder.Build();

            var connectionStringBuilder = new MongoConnectionStringBuilder(ConfigurationManager.ConnectionStrings["MongoDBConnectionString"].ConnectionString);
            var mongoServer = MongoServer.Create(connectionStringBuilder);
            _employerCollection = mongoServer.GetDatabase(connectionStringBuilder.DatabaseName).GetCollection<Employer>(typeof (Employer).Name);

            DefaultSerializationProvider.InitializeProvider(typeof(IEntity), new DependencyResolver(_container));
        }

        [Test]
        public void Verify_when_an_employer_is_saved_and_createdby_and_createdtime_is_set()
        {
            var employer = new Employer()
                               {
                                   Name = "Kata Wahapan Limited.",
                                   Employees = new List<Employee>()
                                                   {
                                                       new Employee()
                                                           {
                                                               Name = "Gon Buruwa"
                                                           },
                                                       new Employee()
                                                           {
                                                               Name = "Walding Buriya"
                                                           }
                                                   }
                               };
            _employerCollection.Save(employer);

            var id = employer.Id;

            var loadedFromDatabase = _employerCollection.FindOneById(employer.Id);
            Assert.IsNotNull(loadedFromDatabase);
            Assert.AreEqual(id, loadedFromDatabase.Id);
            Assert.AreEqual("Kata Wahapan Limited.", loadedFromDatabase.Name);

            Assert.AreEqual("Test Runner", loadedFromDatabase.CreatedBy);
            Assert.IsNotNull(loadedFromDatabase.CreatedOn);

            CollectionAssert.IsNotEmpty(loadedFromDatabase.Employees);
            Assert.AreEqual(2, loadedFromDatabase.Employees.Count);

            Assert.IsTrue(loadedFromDatabase.Employees[0].Id != ObjectId.Empty);
            Assert.IsTrue(loadedFromDatabase.Employees.Any(p => p.Name == "Gon Buruwa"));
            
            Assert.IsTrue(loadedFromDatabase.Employees[1].Id != ObjectId.Empty);
            Assert.IsTrue(loadedFromDatabase.Employees.Any(p => p.Name == "Walding Buriya"));
        }
    }
}
