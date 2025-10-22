namespace ASHelpers.Extensions
{
    /// <summary>
    /// Extensões para manipulação e formatação de datas e horários, incluindo formatos brasileiros, verificação de fim de semana,
    /// conversão para formato amigável em português e representação de durações.
    /// </summary>
    public static class DatetimeExtensions
    {
        /// <summary>
        /// Aplica formatação brasileira padrão dd/MM/yyyy HH:mm:ss.
        /// </summary>
        /// <param name="dateTime">A instância de <see cref="DateTime"/> a ser formatada.</param>
        /// <returns>Uma string representando a data e hora no formato brasileiro.</returns>
        public static string ToBrazilianFormat(this DateTime dateTime)
            => dateTime.ToString("dd/MM/yyyy HH:mm:ss");

        /// <summary>
        /// Verifica se a data especificada é um fim de semana (sábado ou domingo).
        /// </summary>
        /// <param name="date">A instância de <see cref="DateTime"/> a ser verificada.</param>
        /// <returns><c>true</c> se for sábado ou domingo; caso contrário, <c>false</c>.</returns>
        public static bool IsWeekend(this DateTime date)
            => date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;

        /// <summary>
        /// Retorna uma string formatada no padrão brasileiro para a data informada e, por meio do parâmetro de saída, uma descrição amigável em português do tempo decorrido desde essa data.
        /// </summary>
        /// <param name="date">A instância de <see cref="DateTime"/> que será utilizada para calcular o tempo decorrido.</param>
        /// <param name="time">
        /// Parâmetro de saída que recebe uma string amigável em português representando o tempo decorrido desde a data informada.
        /// Exemplos: "agora mesmo", "5 min atrás", "2 h atrás", "3 dias atrás".
        /// </param>
        /// <returns>
        /// Uma string representando a data e hora no formato brasileiro padrão "dd/MM/yyyy HH:mm:ss".
        /// </returns>
        public static string ToFriendlyFormat(this DateTime date, out string time)
        {
            var diff = DateTime.Now - date;

            if (diff.TotalSeconds < 60)
                time = "agora mesmo";
            else if (diff.TotalMinutes < 60)
                time = $"{(int)diff.TotalMinutes} min atrás";
            else if (diff.TotalHours < 24)
                time = $"{(int)diff.TotalHours} h atrás";
            else
                time = $"{(int)diff.TotalDays} dias atrás";

            return date.ToBrazilianFormat();
        }

        /// <summary>
        /// Retorna uma string amigável em português representando a duração do <see cref="TimeSpan"/> informado.
        /// </summary>
        /// <param name="time">A instância de <see cref="TimeSpan"/> a ser convertida.</param>
        /// <returns>
        /// Uma string amigável em português representando a duração.
        /// Exemplo: "30 segundos", "10 minutos", "2 horas", "1 dias".
        /// </returns>
        public static string ToDurationString(this TimeSpan time)
        {
            if (time.TotalSeconds < 60)
                return $"{time.Seconds} segundos";
            if (time.TotalMinutes < 60)
                return $"{(int)time.TotalMinutes} minutos";
            if (time.TotalHours < 24)
                return $"{(int)time.TotalHours} horas";
            return $"{(int)time.TotalDays} dias";
        }
    }
}
