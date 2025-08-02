
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PaymentContracts;
using PaymentContracts.DTOs;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Net;
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
        private List<Service?> _AllServices;
        private readonly PaymobService _paymobService;
        private readonly PaymobSettings _Settings;
        public HomeController(ILogger<HomeController> logger, IReview review,IService service, IPay payservice, PaymobService paymobService, IOptions<PaymobSettings> options)
        {
            _logger = logger;
            _ReviewService = review;
            _serviceService = service;
            _PayService = payservice;
            _paymobService = paymobService;
             _AllServices = _serviceService.GetServices().Result;
            _Settings= options.Value;
        }
        [Route("/Index")]
        public async Task<IActionResult> Index()
        {
          List<ReviewResponse> AllReviews = await _ReviewService.GetReviews();

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
        public IActionResult Privacy()
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
        public IActionResult Details(Guid id, string price)
        {
            string ServiceName = "";
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
             ViewBag.Price = price;
            return View(ServiceName);
        }
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
        [Route("~/SupportCircle/ChoosePayment")]

        public IActionResult ChoosePayment(string Price)
        {
            List<PaymentDTO> payments = new List<PaymentDTO>();
            if (Price.Contains("EGP"))
              payments= _PayService.GetLocalPayments().Result;
            else if (Price.Contains("$"))
              payments=  _PayService.GetInternationalPayments().Result;
            ViewBag.Price= Price;
                return View();
        }

 

        [Route("~/SupportCircle/PayPage")]
        [HttpPost]
        public async Task< IActionResult> PayPage(string PaymentName,string Price)
        {
            //string digitsOnly = new string(Price.Where(char.IsDigit).ToArray());
            //int  intPrice = int.Parse(digitsOnly);
            //intPrice *= 100;
            //string Auth = await _paymobService.AuthenticateAsync();        
            //int id = _paymobService.CreateOrderAsync(Auth, intPrice).Result;
            //string PaymentKey;
            //if (PaymentName=="Bank Transfer")
            //{
            //    string redirectUrl = "https://localhost:7118/SupportCircle/PaymentCallback";
            //    PaymentKey = await _paymobService.GeneratePaymentKeyAsync(Auth, id, intPrice, _Settings.CardIntegrationId, redirectUrl);
            //    PaymentKey = _paymobService.GeneratePaymentKeyAsync(Auth, id, intPrice, _Settings.CardIntegrationId,redirectUrl).Result;
            //    string iframeUrl = _paymobService.GetIframeUrl(PaymentKey, _Settings.IframeId);
            //    return Redirect(iframeUrl);
            //}
            //else
            //{
            //    ViewBag.Price= intPrice;
            //    return View("WalletInfo");
           return RedirectToAction("StartUnifiedPayment");
            }
        
        [HttpPost]
        public async Task <IActionResult> SubmitWalletPayment(WalletPaymentViewModel model,int Price)
        {
            if (!ModelState.IsValid)
            {
                return View("WalletInfo", model);
            }
            string Auth = await _paymobService.AuthenticateAsync();
            int id = _paymobService.CreateOrderAsync(Auth,Price).Result;
            string AFterPaymentredirectUrl = "https://localhost:7118/SupportCircle/PaymentCallback";

            string PaymentKey = _paymobService.GeneratePaymentKeyAsync(Auth, id, Price, _Settings.WalletIntegrationId,AFterPaymentredirectUrl).Result;
            string EncodedredirectUrl = _paymobService.InitiateWalletPaymentAsync(PaymentKey, model.WalletNumber).Result;
            string redirectUrl = WebUtility.UrlDecode(EncodedredirectUrl);
            return Redirect(redirectUrl);
        }
 
        public async Task<IActionResult> StartUnifiedPayment()
        {
            int priceInCents = 170000; // EGP 1700
            string integrationId = (364293935).ToString();

            string redirectUrl = await _paymobService.CreatePaymentIntentionAsync(priceInCents, integrationId);
            return Redirect(redirectUrl);
        }

        [HttpGet]
        [Route("~/SupportCircle/PaymentCallback")]
        public IActionResult PaymentCallback(string success, string pending, string amount_cents, string order)
        {
            if (success == "true")
            {
                // Optionally: confirm transaction with Paymob's Transaction API
                return RedirectToAction("index");
            }
            else
            {
                return RedirectToAction("ChoosePayment");
            }
        }
    }
}
