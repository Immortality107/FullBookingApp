
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using PaymentContracts;
using PaymentContracts.DTOs;
using ServiceContracts.DTOs;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Runtime;
using System.Threading.Tasks;

namespace MyBookingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IReview _ReviewService;
        private readonly IService _serviceService;
        private readonly IPay _PayService;
        private readonly ILogin _LogService;
        private readonly IClient _ClientService;

        private List<Service?> _AllServices;
        private readonly PaymobService _paymobService;
        private readonly PaymobSettings _Settings;
        public HomeController(ILogger<HomeController> logger, IReview review,
            IService service, IPay payservice,ILogin _login, 
            PaymobService paymobService, IOptions<PaymobSettings> options,IClient clientService)
        {
            _logger = logger;
            _ReviewService = review;
            _serviceService = service;
            _PayService = payservice;
            _paymobService = paymobService;
            _LogService= _login;
             _AllServices =  _serviceService.GetServices().Result;
            _ClientService = clientService;
            _Settings= options.Value;
        }
        [Route("/Index")]
        [Route("/")]

        public async Task<IActionResult> Index()
        {
            List<ReviewResponse> AllReviews = await _ReviewService.GetReviews();
            ViewBag.ClientName= "";
            return View(AllReviews);
        }

        [Route("/About")]
        public IActionResult About()
        {
            return View();
        }

        [Route("/Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [Route("/Sevices")]
        public async Task<IActionResult> Services()
        {
            return View(_AllServices);
        }

        [Route("/Payments")]
        public IActionResult Payments()
        {
            return View();
        }

        [Route("Home/LocalChooseSession")]
        public IActionResult LocalChooseSession()
        {
            //ViewBag.PrivateSessionPrice="1700EGP";
            //ViewBag.OnlineMonthlyPackagePrice="5200EGP";
            //ViewBag.OfflineMonthlyPackagePrice="6200EGP";
            return View("EgyptianSessionTypeView", _AllServices);
        }

        [Route("Home/InternationalChooseSession")]
        public IActionResult InternationalChooseSession()
        {
            //ViewBag.PrivateSessionPrice="170$";
            //ViewBag.MonthlyPackagePrice="520$";
            return View("InternationalSessionTypeView");
        }

        [Route("Home/LocalSupportCircle")]
        public  IActionResult LocalSupportCircle()
        {
            return View("EgyptianCirclesTypeView", _AllServices);
        }

        [Route("Home/InternationalSupportCircle")]
        public  IActionResult InternationalSupportCircle()
        {
            return View("InternationalCirclesTypeView",_AllServices);
        }

        [Route("~/SupportCircle/Details/{id}")]
        public async Task<IActionResult> Details(Guid id, string price)
        {
            string ServiceName = "";
            string PriceWithoutCurrency = await _PayService.ExtractPriceAsync(price);
            Service service = _AllServices.FirstOrDefault(s => s.ServiceId == id);
            switch (service.ServiceName) {
            case  "دايرة الرفض":
                    ServiceName = "RejectionCircle";
                    break;
                case "دايرة الوفرة المالية":
                    ServiceName = "MoneyFlowCircle";
                    break;
                case "دايرة مساحة جسد":
                    ServiceName = "BodySpaceCircle";
                    break;
                case "دايرة سحر التمكين":
                    ServiceName = "EmpowermentMagicCircle";
                    break;
                case "دايرة الشكر":
                    ServiceName = "GratitudeCircle";
                    break;
                case "دايرة الأنس":
                    ServiceName = "GatheringCircle";
                    break;
                case "دايرة سنجل مامي":
                    ServiceName = "SingleMomCircle";
                    break;
                 default:
                    ServiceName = "EgyptianSessionTypeView";
                    break;
            }
             ViewBag.Price = PriceWithoutCurrency;
            ViewBag.ServiceName = ServiceName;
            return View(ServiceName);
        }
 

        [Route("~/SupportCircle/ChoosePayment")]
        public IActionResult ChoosePayment(string Price,string ServiceName)
        {
            //List<PaymentDTO> payments = new List<PaymentDTO>();
            ViewBag.Price=Price;
            ViewBag.ServiceName = ServiceName;
            return View();
        }

        [Route("~/SupportCircle/PayPage")]
        public async Task<IActionResult> PayPage(string Price,string ServiceName)
        {
            int intPrice = int.Parse(_PayService.ExtractPriceAsync(Price).Result);
            intPrice *= 100; //Paymob Gets Amount In Cents, So We Multiply By hundred To Fix This
            string Auth = await _paymobService.AuthenticateAsync();
            int id = _paymobService.CreateOrderAsync(Auth, intPrice).Result;
            PaymentKeyResponse paymentToken = _paymobService.GeneratePaymentTokenAsync(Auth, intPrice, id, int.Parse( _Settings.CardIntegrationId)).Result;
            string SecretKey = await _paymobService.CreatePaymentIntentionAsync(ServiceName, intPrice);
            string redirect = _paymobService.GetUnifiedUrl(SecretKey);
            return Redirect(redirect);
        }

        [HttpGet("~/AfterPaymentProcess")]
        public IActionResult AfterPaymentFirstProcess() {
            return View();
        }

        [HttpGet]
        public JsonResult GetEvents()
        {
            var events = new List<object>
            {
                new {
                    id = 1,
                    title = "Booked Slot",
                    start = DateTime.Today.AddHours(11).ToString("yyyy-MM-ddTHH:mm:ss"),
                    end = DateTime.Today.AddHours(12).ToString("yyyy-MM-ddTHH:mm:ss")
                },
                new {
                    id = 2,
                    title = "Another Booking",
                    start = DateTime.Today.AddDays(1).AddHours(14).ToString("yyyy-MM-ddTHH:mm:ss"),
                    end = DateTime.Today.AddDays(1).AddHours(15).ToString("yyyy-MM-ddTHH:mm:ss")
                }
            };
            return Json(events);
        }

        [HttpGet("~/Booked/BookAppointment")]
        public IActionResult BookAppointment()
        {        
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost("~/Home/Login")]
        public async Task<IActionResult> Register(string Email, string Password)
        {
            List<LoginDTO> AlllogingAccounts = await _LogService.GetAllRegisteredAccounts();
            foreach (LoginDTO L in AlllogingAccounts)
            {
                if (L.Email==Email)
                {
                    if (L.Password== LoginDTO.HashPassword(Password))
                    { 
                       
                    HttpContext.Session.SetString("UserName", L.Username);

                        ViewBag.ClientName= L.Username;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Password Is Invalid!");
                        var currentCulture = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
                        ViewBag.Countries = CountryHelper.GetAllCountries(currentCulture);
                        return View("Login");
                    }
                } 
            }
            ModelState.AddModelError("Email", "Email is not available, please sign up!");
            var CurrentCulture = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            ViewBag.Countries = CountryHelper.GetAllCountries(CurrentCulture);
            return View("Login"); 
            }

        public IActionResult LogOut()
        {
            HttpContext.Session.SetString("UserName", "");
            return RedirectToAction("Index");
        }
        public IActionResult SignUp()
        {
            var currentCulture = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            ViewBag.Countries = CountryHelper.GetAllCountries(currentCulture); return View();
        }
        public async Task<IActionResult> AddAccount(SignUpDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View("SignUp",model);
            }
            LoginDTO dTO= new LoginDTO { Email = model.Email, Password = model.Password, Username=model.UserName };
            Guid id = await _LogService.AddAccount(dTO);
            ClientDTO clientDTO = model.ToClientDTO(model);   
            _ClientService.AddClient(clientDTO);
            if ( id!=Guid.Empty && _LogService.AddAccount(dTO)!=null )
            {
                HttpContext.Session.SetString("UserName", model.UserName);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }

    }
    }

