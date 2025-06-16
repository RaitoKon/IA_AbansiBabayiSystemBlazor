using MimeKit;
using MailKit.Net.Smtp;

namespace IA_AbansiBabayiSystemBlazor.Service
{
    public class EmailService
    {
        public async Task SendEmailAsync(string recipientEmail, string recipientName, string userTempPassword)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Girl Scout Admin", "tairodeoni.garcia-22@cpu.edu.ph"));
            message.To.Add(new MailboxAddress(recipientName, recipientEmail));
            message.Subject = "Hi there! — Welcome to the Iloilo Girl Scout Council Website";

            // HTML email body
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"
                <div style='font-family: Arial, sans-serif; padding: 20px; color: #333;'>
                    <h2 style='color: #4CAF50;'>Welcome, {recipientName}!</h2>
                    <p>Your account has been successfully <strong>approved</strong>.</p>
                    <p>Here are your login details:</p>
                    <ul style='line-height: 1.6;'>
                        <li><strong>Email:</strong> {recipientEmail}</li>
                        <li><strong>Temporary Password:</strong> {userTempPassword}</li>
                    </ul>
                    <p>👉 Please change your password immediately after logging in for the first time.</p>
                    <p>You can log in at: <a href='https://your-website-link.com/login'>https://your-website-link.com/login</a></p>
                    <br />
                    <p>Thank you,<br/>Girl Scout Admin</p>
                </div>";

                                // Optional plain-text fallback
                                bodyBuilder.TextBody = $@"
                    Welcome, {recipientName}!

                    Your account has been approved.

                    Email: {recipientEmail}
                    Temporary Password: {userTempPassword}

                    Please change your password after your first login.
                    Login here: https://your-website-link.com/login

                    - Girl Scout Admin";

            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("tairodeoni.garcia-22@cpu.edu.ph", "biul zmsz ilgz bttp"); // Use Gmail App Password
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
