using System.Text;
using System.Web.Mvc;
using System.Web.SessionState;
using Core.Interfaces;

namespace AutofacMVC.Controllers
{
	[SessionState(SessionStateBehavior.Required)]
	public class HomeController : Controller
	{
		public ISquareMatrixModel Model { get; set; }

		public HomeController(ISquareMatrixModel model)
		{
			this.Model = model;
		}

		string Matrix
		{
			get { return Session["Matrix"] as string; }
			set { Session["Matrix"] = value; }
		}

		public ActionResult Index()
		{
			Model.LoadRandom(5);
			Matrix = Model.GetValue();
			return View(Model);
		}

		[HttpPost]
		public ActionResult UploadFromFile()
		{
			if (Request.Files.Count > 0)
			{
				var file = Request.Files.Get(0);
				Model.Load(file.InputStream);
				Matrix = Model.GetValue();
			}
			return View("Index", Model);
		}

		[HttpGet]
		public FileContentResult GetFile()
		{
			Model.Load(Matrix);
			var fileName = string.Format("Matrix{0}x{0}.txt", Model.GetLength());
			return File(Encoding.UTF8.GetBytes(Matrix), "text/plain", fileName);
		}

		[HttpGet]
		public ActionResult Rotation()
		{
			Model.Load(Matrix);
			Model.RightRotation();
			Matrix = Model.GetValue();
			return View("Index", Model);
		}

		[HttpPost]
		public ActionResult Create()
		{
			int length = 0;
			int.TryParse(Request["Length"], out length);
			Model.LoadRandom(length);
			Matrix = Model.GetValue();
			return View("Index", Model);
		}

	}
}