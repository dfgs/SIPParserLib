using LogLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using SIPParserLib.Parsers;
using System;
using System.Numerics;
using System.Reflection;

namespace SIPParserLib.UnitTest
{
	[TestClass]
	public class SDPBodyParserUnitTest
	{
		[TestMethod]
		public void ConstructorShouldThrowExceptionIfParameterIsNull()
		{
#pragma warning disable CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
			Assert.ThrowsException<ArgumentNullException>(() => new SDPBodyParser(null, Mock.Of<IClassStringParser<SDPField>>()) );
			Assert.ThrowsException<ArgumentNullException>(() => new SDPBodyParser(NullLogger.Instance, null));
#pragma warning restore CS8625 // Impossible de convertir un littéral ayant une valeur null en type référence non-nullable.
		}



		[DataTestMethod]
		[DataRow("SIPParserLib.UnitTest.SDPBodies.SDPBody1.txt")]
		public void ParseShouldParseRequest(string ResourceName)
		{
			SDP? sdp;

			SDPBodyParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new SDPBodyParser(logger);
			
			using (Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ResourceName))
			{
				if (stream == null) Assert.Fail("Resource not found");
				sdp = parser.Parse(stream);
				Assert.IsNotNull(sdp);
				Assert.AreEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
				Assert.AreNotEqual(0, sdp.Fields.Count());
			}

		}

		/*[DataTestMethod]
		[DataRow("SIPParserLib.UnitTest.SDPBodies.RequestIncompleteInvite1.txt")]
		public void ParseShouldNotParseIncompleteRequest(string ResourceName)
		{
			SDPBody? message;

			SDPBodyParser parser;
			DebugLogger logger;

			logger = new DebugLogger();
			parser = new SDPBodyParser(logger);

			using (Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ResourceName))
			{
				if (stream == null) Assert.Fail("Resource not found");
				message = parser.Parse(stream);
				Assert.IsNull(message);
				Assert.AreNotEqual(0, logger.ErrorCount + logger.WarningCount + logger.FatalCount);
			}

		}*/



	}
}
