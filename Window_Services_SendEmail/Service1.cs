using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Window_Services_SendEmail.BirthdayModel;

namespace Window_Services_SendEmail
{
    public partial class Service1 : ServiceBase
    {
        private readonly string connectionString = "Server=PCA172\\SQL2017;Database=imagesdb;Trusted_Connection=True;MultipleActiveResultSets=true;User ID=sa;Password=Tatva@123;Integrated Security=False; TrustServerCertificate=True";

        Timer timer = new Timer();
        public Service1()
        {
            InitializeComponent();
        }

        // Services Start
        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is started at " + DateTime.Now);
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 120000; // 2 minute in milisecinds
           // timer.Interval = 86400000; // 24 hours in milliseconds
            timer.Enabled = true;
        }

        // Services Stop
        protected override void OnStop()
        {
            WriteToFile("Service is stopped at " + DateTime.Now);
        }

        // Services Elapsed
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile("Service is recall at " + DateTime.Now);

            // Retrieve the birthday user data
            List<Birthday_User_Data> birthdayUserData = AllBirthdayUser();
            // Write the user data to the file
            foreach (var userData in birthdayUserData)
            {
                string message = $"User: {userData.Name}, Birthdate: {userData.Date}, Email: {userData.Email}";
                SendEmail(userData.Email, "Today is Your Birthday", "your birthday is on: ");
                WriteToFile(message);
            }
        }

        // Write in file 
        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }

        // getting tha all user data that today birthdate
        public List<Birthday_User_Data> AllBirthdayUser()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<Birthday_User_Data> BirthdayUserData = connection.Query<Birthday_User_Data>("Birthday_UserData", commandType: CommandType.StoredProcedure).ToList();

                // Return the data
                return BirthdayUserData;
            }
        }
        // Send Email
        public void SendEmail(string alluseremail, string emailsubject, string emailbody)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient("yourSMTPServer"))
                {
                    //email sent
                    var fromEmail = new MailAddress("sohammodi124421@gmail.com");
                    var toEmail = new MailAddress(alluseremail);
                    var fromEmailPassword = "whwuvzwoegqezftj";
                    string subject = emailsubject;

                    string templatePath = @"D:\important\Window_Services_SendEmail\Template\Birthday_Wish.html";
                    StreamReader str = new StreamReader(templatePath);
                    string body = str.ReadToEnd();

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
                    };

                    MailMessage message = new MailMessage(fromEmail, toEmail);
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true;
                    message.IsBodyHtml = true;
                    smtp.Send(message);

                    WriteToFile("Email sent to " + alluseremail + " at " + DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                WriteToFile("Failed to send email: " + ex.Message);
            }
        }
    }
}
