using BouquetStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BouquetStore.Domain.Entities;
using System.Net.Mail;
using System.Diagnostics;
using System.Net;

namespace BouquetStore.Domain.Concrete
{
  public class EmailSettings
  {
    public string MailToAddress = "voronin-anton1@yandex.ru";
    public string MailFromAddress = "hestheone30@gmail.com";
    public string MailFromName = "Tophail";
    public bool UseSSL = true;
    public string Username = "hestheone30@gmail.com";
    public string Password = "";
    public string ServerName = "smtp.gmail.com";
    public int ServerPort = 587;
    public bool WriteAsFile = true;
    public string FileLocation = @"D:\TestMails";
  }
  public class EmailOrderProcessor : IOrderProcessor
  {
    private EmailSettings settings;

    public EmailOrderProcessor(EmailSettings settings)
    {
      this.settings = settings;
    }

    public void ProcessOrder(Cart cart, OrderDetails orderDetails)
    {
      using (var smtpClient = new SmtpClient())
      {
        smtpClient.EnableSsl = settings.UseSSL;
        smtpClient.Host = settings.ServerName;
        smtpClient.Port = settings.ServerPort;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential(settings.Username, settings.Password);
        if (settings.WriteAsFile)
        {
          smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
          smtpClient.PickupDirectoryLocation = settings.FileLocation;
          smtpClient.EnableSsl = false;
        }

        StringBuilder body = new StringBuilder()
            .AppendLine("A new order was submitted")
            .AppendLine("---")
            .AppendLine("Items: ");

        foreach (var line in cart.Lines)
        {
          var subTotal = line.Product.Price * line.Quantity;
          body.AppendFormat("{0} X {1} (subtotal: {2:c})\n", line.Quantity, line.Product.Name, subTotal);
        }

        body.AppendFormat("Total order value: {0:c}\n", cart.ComputeTotalValue())
            .AppendLine("---")
            .AppendLine("Ship to:")
            .AppendLine(orderDetails.Name)
            .AppendLine(orderDetails.PhoneNumber)
            .AppendLine(orderDetails.OrderComment)
            .AppendLine("---");

        MailMessage message = new MailMessage(
            settings.MailFromAddress, //From
            settings.MailToAddress, //To
            "You have a new order!", //Subj
            body.ToString()
            );

        if (settings.WriteAsFile) message.BodyEncoding = Encoding.UTF8;


        try
        {
          smtpClient.Send(message);
          //Debug.WriteLine("TORPHIK! " + settings.MailToAddress + " successfully sent mail");
          Debug.WriteLine("TORPHIK! " + settings.FileLocation + " successfully sent mail");
        }
        catch (SmtpException e)
        { Debug.WriteLine("TORPHIK! " + e.Message); }
      }
    }
  }
}
