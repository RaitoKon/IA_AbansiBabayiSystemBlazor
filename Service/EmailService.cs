using MimeKit;
using MailKit.Net.Smtp;

namespace IA_AbansiBabayiSystemBlazor.Service
{
    public class EmailService
    {
        public async Task SendRegisteredConfirmationEmailAsync(string userNewEmail, string userCurrentEmail, string recipientFname, string userTempPassword)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Girl Scout Admin", "tairodeoni.garcia-22@cpu.edu.ph"));
            message.To.Add(new MailboxAddress(recipientFname, userCurrentEmail));
            message.Subject = "Hi there! — Welcome to the Iloilo Girl Scout Council Website";

            // HTML email body
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"
                <div style='font-family: Arial, sans-serif; padding: 20px; color: #333;'>
                    <h2 style='color: #4CAF50;'>Welcome, {recipientFname}!</h2>
                    <p>Your account has been successfully <strong>approved</strong>.</p>
                    <p>Here are your login details:</p>
                    <ul style='line-height: 1.6;'>
                        <li><strong>Email:</strong> {userNewEmail}</li>
                        <li><strong>Temporary Password:</strong> {userTempPassword}</li>
                    </ul>
                    <p>👉 Please change your password immediately after logging in for the first time.</p>
                    <p>You can log in at: <a href='https://your-website-link.com/login'>https://your-website-link.com/login</a></p>
                    <br />
                    <p>Thank you,<br/>Girl Scout Admin</p>
                </div>";

                                // Optional plain-text fallback
                                bodyBuilder.TextBody = $@"
                    Welcome, {recipientFname}!

                    Your account has been approved.

                    Email: {userCurrentEmail}
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

        public async Task SendRegistrationSubmittedEmailAsync(string recipientEmail, string recipientFname)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Girl Scout Admin", "tairodeoni.garcia-22@cpu.edu.ph"));
            message.To.Add(new MailboxAddress(recipientFname, recipientEmail));
            message.Subject = "Thank you for registering — Iloilo Girl Scout Council";

            // HTML email body
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"
                <div style='font-family: Arial, sans-serif; padding: 20px; color: #333;'>
                    <h2 style='color: #4CAF50;'>Hi {recipientFname}!</h2>
                    <p>We’ve received your registration and it’s currently being reviewed by our team.</p>
                    <p>Here’s what to expect next:</p>
                    <ul style='line-height: 1.6;'>
                        <li>You’ll receive another email once your registration has been approved.</li>
                        <li>If there are any issues or missing information, we’ll reach out to you.</li>
                    </ul>
                    <p>Thank you for registering!<br/>— Iloilo Girl Scout Council Team</p>
                </div>";

            // Optional plain-text fallback
            bodyBuilder.TextBody = $@"
                Hi {recipientFname}!

                We’ve received your registration and it’s now under review.

                What to expect:
                - We’ll notify you by email once your registration is approved.
                - We’ll contact you if we need more information.

                Thank you for registering!
                — Iloilo Girl Scout Council Team";

            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("tairodeoni.garcia-22@cpu.edu.ph", "biul zmsz ilgz bttp"); // Use Gmail App Password
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
