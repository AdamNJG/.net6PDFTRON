using Microsoft.Extensions.Configuration;
using pdftron;
using pdftron.Common;
using pdftron.PDF;
using pdftron.SDF;
using System;

namespace DistillerAPI.Services
{
	public class PDFTronService : IPDFTronService
	{
		private IConfiguration _configuration;

		public PDFTronService(IConfiguration configuration)
		{
			_configuration = configuration;
			PDFNet.Initialize(_configuration["PDFTronLicense"]);
		}

		public string Stuff()
		{

			string input_path = "C:/TestFiles/";
			string output_path = "C:/TestFiles/Output/";

			try
			{
				using (PDFDoc doc = new PDFDoc(input_path + "1.pdf"))
				{
					doc.InitSecurityHandler();

					Flattener fl = new Flattener();
					// The following lines can increase the resolution of background
					// images.
					fl.SetDPI(300);
					fl.SetMaximumImagePixels(5000000);

					// This line can be used to output Flate compressed background
					// images rather than DCTDecode compressed images which is the default
					//fl.SetPreferJPG(false);

					// In order to adjust thresholds for when text is Flattened
					// the following function can be used.
					//fl.SetThreshold(Flattener.Threshold.e_keep_most);

					// We use e_fast option here since it is usually preferable
					// to avoid Flattening simple pages in terms of size and 
					// rendering speed. If the desire is to simplify the 
					// document for processing such that it contains only text and
					// a background image e_simple should be used instead.


					fl.Process(doc, Flattener.FlattenMode.e_fast);

					doc.Save(output_path + "TigerText_flatten.pdf", SDFDoc.SaveOptions.e_linearized);
					return "Done";
				}
			}

			catch (PDFNetException e)
			{
				return e.Message;
			}
		}
	}
    
}
