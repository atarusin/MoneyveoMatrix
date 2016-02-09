using System;
using System.Text;
using System.Web.Mvc;
using AutofacMVC.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Models;
using AutofacMVC.Tests.Mock;
using Core.Interfaces;
using System.IO;
using System.Configuration;

namespace AutofacMVC.Tests.Controllers
{
	[TestClass]
	public class HomeControllerTest
	{
		[TestMethod]
		public void Index()
		{
			HomeController controller = new HomeController(new SquareMatrixModel());
			new MVCContextMocks(controller);

			// Act
			ViewResult result = controller.Index() as ViewResult;

			// Assert
			Assert.IsNotNull(result.Model);
		}

		[TestMethod]
		public void UploadFromFile()
		{
			HomeController controller = new HomeController(new SquareMatrixModel());
			ViewResult result;

			var FileName = string.Format(ConfigurationManager.AppSettings["SquareMatrixFile"],
						AppDomain.CurrentDomain.BaseDirectory);
			using (var stream = new FileStream(FileName, FileMode.Open))
			{
				var context = new MVCContextMocks(controller, stream);

				// Act
				result = controller.UploadFromFile() as ViewResult;
			}
			ISquareMatrixModel model = result.Model as ISquareMatrixModel;
			// Assert
			Assert.IsTrue(model.GetValue() == "11,12,13,14\r\n21,22,23,24\r\n31,32,33,34\r\n41,42,43,44");
		}

		[TestMethod]
		public void GetFile()
		{
			var matrix = "11,12,13\n21,22,23\n31,32,33";
			HomeController controller = new HomeController(new SquareMatrixModel());
			var context = new MVCContextMocks(controller);
			context.Session["Matrix"] = matrix;

			//// Act
			FileContentResult result = controller.GetFile() as FileContentResult;

			//// Assert
			Assert.IsTrue(result.ContentType == "text/plain");
			Assert.IsTrue(Encoding.UTF8.GetString(result.FileContents) == matrix);
		}

		[TestMethod]
		public void Rotation()
		{
			HomeController controller = new HomeController(new SquareMatrixModel());
			var context = new MVCContextMocks(controller);
			context.Session["Matrix"] = "11,12,13\n21,22,23\n31,32,33";

			//// Act
			ViewResult result = controller.Rotation() as ViewResult;
			ISquareMatrixModel model = result.Model as ISquareMatrixModel;

			//// Assert
			Assert.IsTrue(controller.Model.GetValue() == "31,21,11\n32,22,12\n33,23,13");
		}

		[TestMethod]
		public void Create()
		{
			HomeController controller = new HomeController(new SquareMatrixModel());
			var context = new MVCContextMocks(controller);
			context.SetRequestPatam("Length", "10");

			// Act
			ViewResult result = controller.Create() as ViewResult;
			ISquareMatrixModel model = result.Model as ISquareMatrixModel;

			// Assert
			Assert.IsTrue(model.GetLength() == 10);
		}

	}
}
