using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Pdf.HtmlConverter;

namespace OutlookMessageConverter.InfraStructure
{
    public class PdfGenerator
    {
        public static bool GeneratePdf(List<EmailModel> messages, string filePath, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                PdfDocument pdf = new PdfDocument();
                PdfHtmlLayoutFormat htmlLayoutFormat = new PdfHtmlLayoutFormat();
                htmlLayoutFormat.IsWaiting = false;
                htmlLayoutFormat.FitToPage = Clip.Width;
                PdfPageSettings setting = new PdfPageSettings();
                setting.Size = PdfPageSize.A4;
                setting.SetMargins(3, 2);
                string htmlCode = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HtmlTemplate.html"));
                int repeatAreaStartIndex = htmlCode.IndexOf("@REPEATAREA@");
                int repeatAreaEndIndex = htmlCode.LastIndexOf("@REPEATAREA@") + "@REPEATAREA@".Length;
                int repeatAreaContentStartIndex = htmlCode.IndexOf("@REPEATAREA@") + "@REPEATAREA@".Length;
                string repeatArea =
                    htmlCode.Substring(
                        repeatAreaContentStartIndex,
                        htmlCode.LastIndexOf("@REPEATAREA@") - repeatAreaContentStartIndex).Trim();
                string messageHtml = "";
                foreach (var message in messages)
                {
                    messageHtml += repeatArea.Replace("@FROM@", message.From).Replace("@SUBJECT@", message.Subject).Replace("@BODY@", message.Body);
                }
                htmlCode = htmlCode.Remove(repeatAreaStartIndex, repeatAreaEndIndex - repeatAreaStartIndex);
                htmlCode = htmlCode.Insert(repeatAreaStartIndex, messageHtml);
                Thread thread = new Thread(() =>
                   {
                       pdf.LoadFromHTML(htmlCode, false, setting, htmlLayoutFormat);
                   });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
                pdf.SaveToFile(filePath);
                System.Diagnostics.Process.Start(filePath);
                return true;
            }
            catch (Exception exp)
            {
                errorMessage = exp.Message;
            }

            return false;
        }
    }
}
