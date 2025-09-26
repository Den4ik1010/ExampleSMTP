namespace ExampleSMTP;

/// <summary>
/// Конфігурація пошти
/// </summary>
public static class MailConf
{
    /// <summary>
    /// Хто надсилає листа
    /// </summary>
    public static string From { get; set; } = "super.novakvova@ukr.net";

    /// <summary>
    /// Назва серверу на ukr.net 
    /// </summary>
    public static string SmtpServer { get; set; } = "smtp.ukr.net";

    /// <summary>
    /// Порт, через який ми будемо працювати на ukr.net
    /// </summary>
    public static int SmtpPort { get; set; } = 2525;

    /// <summary>
    /// Ім'я користувача для авторизації
    /// </summary>
    public static string UserName { get; set; } = "super.novakvova@ukr.net";

    /// <summary>
    /// Пароль для доступу до ukr.net
    /// </summary>
    public static string Password { get; set; } = "j22yJ7sD5PAb4ewk";
}
