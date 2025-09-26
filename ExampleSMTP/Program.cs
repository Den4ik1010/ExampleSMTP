using ExampleSMTP;
using MailKit.Net.Smtp;
using MimeKit;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

Message msg = new Message
{
    Subject = "Hello rizz boy or girl",
    Body = "Сьогодні можна у двох посидіти в моєму гаражі",
    To = "onlybuzz52@gmail.com"
};

string pathFile = @"E:\reper-rocket_-kosmicheskiy-vzlet-na-moskovskoy-stsene.webp";

// Читаємо файл один раз у пам'ять
byte[] fileBytes = File.ReadAllBytes(pathFile);

using var client = new SmtpClient();
client.Connect(MailConf.SmtpServer, MailConf.SmtpPort, true);
client.Authenticate(MailConf.UserName, MailConf.Password);

for (int i = 0; i < 200; i++)
{
    try
    {
        var body = new TextPart("plain") { Text = msg.Body };

        var attachment = new MimePart("image", "jpg")
        {
            FileName = "IVAN.jpg",
            Content = new MimeContent(new MemoryStream(fileBytes)),
            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            ContentTransferEncoding = ContentEncoding.Base64
        };

        var multipart = new Multipart("mixed");
        multipart.Add(body);
        multipart.Add(attachment);

        var em = new MimeMessage();
        em.From.Add(new MailboxAddress(MailConf.From));
        em.To.Add(new MailboxAddress(msg.To));
        em.Subject = $"{msg.Subject} #{i + 1}";
        em.Body = multipart;

        client.Send(em);

        Console.WriteLine($"✅ Лист #{i + 1} успішно відправлено!");

        // 👇 після кожних 50 листів робимо перепідключення
        if ((i + 1) % 50 == 0)
        {
            Console.WriteLine("🔄 Перепідключення після 50 листів...");
            client.Disconnect(true);
            client.Connect(MailConf.SmtpServer, MailConf.SmtpPort, true);
            client.Authenticate(MailConf.UserName, MailConf.Password);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Помилка при надсиланні листа #{i + 1}: {ex.Message}");

        if (!client.IsConnected)
        {
            Console.WriteLine("🔄 Перепідключення до SMTP...");
            client.Connect(MailConf.SmtpServer, MailConf.SmtpPort, true);
            client.Authenticate(MailConf.UserName, MailConf.Password);
        }
    }
}

client.Disconnect(true);
