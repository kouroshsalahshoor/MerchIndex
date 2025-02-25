using System.Net.Mail;
using System.Net;

namespace MerchIndex.Auto.Services
{
    public class EmailService
    {
        private readonly string _companyConfirmedUrl = "/EmailTemplates/CompanyConfirmed.html";
        private readonly string _companyConfirmedPath = string.Empty;
        private readonly string _companyConfirmedBody = string.Empty;
        private readonly string _companyConfirmedSubject = "Welcome to EshopsNet";
        private readonly IWebHostEnvironment _env;

        public EmailService(IWebHostEnvironment env)
        {
            _env = env;

            _companyConfirmedPath = _env.WebRootPath + _companyConfirmedUrl;
            _companyConfirmedBody = File.ReadAllText(_companyConfirmedPath);
        }

        public bool SendConfirmationGmail(string to)
        {
            return SendGmail(to, _companyConfirmedSubject, _companyConfirmedBody);
        }

        // https://www.youtube.com/watch?v=gdeCufsntGI
        // https://myaccount.google.com/apppasswords?pli=1&rapt=AEjHL4NTn_hqRnUnotZ4j8m4u8T2YbXmxQpXP82eO9KP45kBzaChQpPCFbwlCVyhGwNOmqzdT78xnr4Idz0E1cTLydiRSc1snVXCY4jQL43hpo_VveHNqig
        // private void sendGmail(string to, string subject, string body)
        public bool SendGmail(string to, string subject, string body)
        {
            var from = "kourosh234@gmail.com";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(from, "iemd meoo bhqg slxi"),
                // Credentials = new NetworkCredential("kourosh234@gmail.com", "Salam_234"),                
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(from),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(to);

            //var mailMessage = new MailMessage
            //{
            //    From = new MailAddress("kourosh234@gmail.com"),
            //    Subject = "subject",
            //    Body = "body",
            //    IsBodyHtml = true,
            //};
            //mailMessage.To.Add("kourosh23@hotmail.com");

            try
            {
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception)
            {
            }

            return false;
        }

        //public void SendHotmail()
        //{
        //    // var smtpClient = new SmtpClient("smtp.office365.com")
        //    var smtpClient = new SmtpClient("smtp-mail.outlook.com")
        //    {
        //        Port = 587,
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        UseDefaultCredentials = false,
        //        Credentials = new NetworkCredential("kourosh23@hotmail.com", "Salam234_Salam234"),
        //        EnableSsl = true,
        //    };

        //    var mailMessage = new MailMessage
        //    {
        //        From = new MailAddress("kourosh23@hotmail.com"),
        //        Subject = "subject",
        //        Body = "body",
        //        IsBodyHtml = true,
        //    };

        //    mailMessage.To.Add("kourosh23@hotmail.com");

        //    smtpClient.Send(mailMessage);
        //    // await smtpClient.SendMailAsync(mailMessage);

        //    // Console.WriteLine("Email sent successfully!");

        //}

        //     private void send()
        //     {
        //         var message = new MimeMessage();
        //         message.From.Add(new MailboxAddress("kourosh salahshoor", "kourosh23@hotmail.com"));
        //         message.To.Add(new MailboxAddress("to myself", "kourosh23@hotmail.com"));
        //         message.Subject = "How you doin'? :)";

        //         message.Body = new TextPart("plain")
        //             {
        //                 Text = @"Hey Kourosh,

        // I just wanted to say Hello :)

        // BR

        // -- Kourosh"
        //             };

        //         try
        //         {
        //             using (var client = new SmtpClient())
        //             {
        //                 client.Connect("smtp-mail.outlook.com", 587, false);

        //                 // Note: only needed if the SMTP server requires authentication
        //                 client.Authenticate("kourosh23@hotmail.com", "Salam234_Salam234");

        //                 client.Send(message);
        //                 client.Disconnect(true);
        //             }

        //             Console.WriteLine("Email sent successfully!");
        //         }
        //         catch (Exception e)
        //         {
        //             Console.WriteLine($">>> Error sending email: {e.Message}");
        //         }

        //     }

        // private async Task send()
        // {
        //     _loading = true;
        //     StateHasChanged();

        //     try
        //     {
        //         // Set up the email details
        //         var fromAddress = new MailAddress("kourosh23@hotmail.com");
        //         // var fromAddress = new MailAddress("kourosh234@gmail.com");
        //         const string fromPassword = "Salam234_Salam234"; //hotmail
        //         // const string fromPassword = "Salam_234"; //gmail

        //         var toAddress = new MailAddress("kourosh23@hotmail.com");

        //         // Configure the SMTP client
        //         var smtp = new SmtpClient
        //             {
        //                 Host = "smtp-mail.outlook.com",
        //                 // Host = "smtp.gmail.com", // e.g., smtp.gmail.com for Gmail
        //                 Port = 587, // or 465 for SSL
        //                 EnableSsl = true,
        //                 DeliveryMethod = SmtpDeliveryMethod.Network,
        //                 UseDefaultCredentials = false,
        //                 Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        //             };

        //         // Create the email message
        //         using (var message = new MailMessage(fromAddress, toAddress)
        //             {
        //                 Subject = "Subject of the email",
        //                 Body = "Body of the email"
        //             })
        //         {
        //             // Send the email
        //             smtp.Send(message);
        //         }

        //         await _js.ToastrError("Email sent successfully!");
        //         // Console.WriteLine("Email sent successfully!");
        //     }
        //     catch (Exception ex)
        //     {
        //         await _js.ToastrError($"An error occurred: {ex.Message}");
        //     }

        //     _loading = false;
        //     StateHasChanged();
        // }
    }
}
