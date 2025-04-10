using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.IO;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;

namespace GadgetHub.Domain.Concrete
{
    public class EmailSettings
    {
        public string FileLocation = @"C:\gadgethub_emails";
        public bool WriteAsFile = true;
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using (var smtpClient = new SmtpClient())
            {
                if (emailSettings.WriteAsFile)
                {
                    Directory.CreateDirectory(emailSettings.FileLocation);
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("New order processed")
                    .AppendLine("---")
                    .AppendLine("Items:");

                foreach (var line in cart.Items)
                {
                    var subtotal = line.Price * line.Quantity;
                    body.AppendLine($"{line.Quantity} x {line.GadgetName} (subtotal: {subtotal:C})");
                }

                body.AppendLine("---")
                    .AppendLine($"Total order value: {cart.ComputeTotalValue():C}")
                    .AppendLine("---")
                    .AppendLine("Ship to:")
                    .AppendLine(shippingDetails.Name)
                    .AppendLine(shippingDetails.Address)
                    .AppendLine(shippingDetails.City)
                    .AppendLine(shippingDetails.State)
                    .AppendLine(shippingDetails.Zip)
                    .AppendLine(shippingDetails.Country);

                MailMessage mailMessage = new MailMessage(
                    "store@example.com", "admin@gadgethub.com", "New Order", body.ToString());

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}
