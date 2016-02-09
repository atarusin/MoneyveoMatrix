using System.Collections.Generic;
using System.Web;
using Moq;
using System.Collections.Specialized;
using System.Web.Routing;
using System.Web.Mvc;
using System.IO;

namespace AutofacMVC.Tests.Mock
{
	public class MVCContextMocks
	{
		public Mock<HttpContextBase> HttpContext { get; private set; }
		public Mock<HttpRequestBase> Request { get; private set; }
		public Mock<HttpResponseBase> Response { get; private set; }
		public RouteData RouteData { get; private set; }
		public FakeSessionState Session { get; private set; }
		public Mock<HttpFileCollectionBase> Files { get; private set; }
		public Mock<HttpPostedFileBase> File { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ContextMocks"/> class.
		/// </summary>
		/// <param name="controller">The controller to add mock context to.</param>
		public MVCContextMocks(Controller controller) : this(controller, null) { }
		public MVCContextMocks(Controller controller, FileStream stream)
		{
			// define all the common context objects, plus relationsips between them
			HttpContext = new Mock<HttpContextBase>();
			Request = new Mock<HttpRequestBase>();
			Response = new Mock<HttpResponseBase>();
			Files = new Mock<HttpFileCollectionBase>();
			File = new Mock<HttpPostedFileBase>();
			RouteData = new RouteData();
			Session = new FakeSessionState();

			HttpContext.Setup(m => m.Request).Returns(Request.Object);
			HttpContext.Setup(m => m.Response).Returns(Response.Object);
			HttpContext.Setup(m => m.Session).Returns(Session);

			Request.Setup(m => m.Cookies).Returns(new HttpCookieCollection());
			Request.Setup(m => m.QueryString).Returns(new NameValueCollection());
			Request.Setup(m => m.Form).Returns(new NameValueCollection());

			if (stream == null)
			{
				Files.Setup(x => x.Count).Returns(0);
			}
			else
			{
				// The required properties from my Controller side
				File.Setup(x => x.InputStream).Returns(stream);
				File.Setup(x => x.ContentLength).Returns((int)stream.Length);
				File.Setup(x => x.FileName).Returns(stream.Name);

				Files.Setup(x => x.Count).Returns(1);
				Files.Setup(x => x.Get(0)).Returns(File.Object);
				Files.Setup(x => x[0]).Returns(File.Object);

				Request.Setup(x => x.Files).Returns(Files.Object);
			}

			Response.Setup(m => m.Cookies).Returns(new HttpCookieCollection());

			// apply the mock context to the supplied controller instance
			RequestContext rc = new RequestContext(HttpContext.Object, new RouteData());
			controller.ControllerContext = new ControllerContext(rc, controller);
		}

		public void SetRequestPatam(string name, string value)
		{
			Request.Setup(x => x[name]).Returns(value);
		}

		/// <summary>
		/// Sets the ajax request header so that mocked HttpRequest shows as a ajax call.
		/// </summary>
		public void SetAjaxRequestHeader()
		{
			Request.Setup(f => f["X-Requested-With"])
				.Returns("XMLHttpRequest");
		}

		public class FakeSessionState : HttpSessionStateBase
		{
			Dictionary<string, object> items = new Dictionary<string, object>();

			public override object this[string name]
			{
				get
				{
					return items.ContainsKey(name) ? items[name] : null;
				}
				set
				{
					items[name] = value;
				}
			}
		}
	}
}