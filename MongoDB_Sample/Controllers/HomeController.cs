using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB_Sample.Models;
using System.Diagnostics;

namespace MongoDB_Sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //Connection Stringimiz ile mongoDB Client a erişebiliriz

            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://ysnesra:Password1@intocluster.wjhuase.mongodb.net/?retryWrites=true&w=majority");
            var client = new MongoClient(settings);
            var database = client.GetDatabase("IntroSampleDB"); //clienttan da database erişebiliriz

            //Collectiona erişmek için; Test classımızla Test collectionımızı(tablomuzu) ilişkilendirdik
            var collection = database.GetCollection<Test>("Test");

            //test clasından nesne oluşturalım:
            var test = new Test()
            {
                _Id = ObjectId.GenerateNewId(),
                NameSurname="Mehmet Mutlu",
                Age=20,
                AddressList=new List<Address>()
                { 
                    new Address
                    {
                        Title="Ev Adresi",
                        Description="Tavlusun Mah./Kayseri"
                    },
                      new Address
                    {
                        Title="İş Adresi",
                        Description="Hulusi Akar Caddesi/Kayseri"
                    }
                }
            };

            collection.InsertOne(test);


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}