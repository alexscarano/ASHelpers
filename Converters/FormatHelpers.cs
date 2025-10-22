using System.Globalization;

namespace ASHelpers.Converters
{
    /// <summary>
    /// Métodos auxiliares para formatação de valores numéricos e objetos.
    /// </summary>
    public static class FormatHelpers
    {   
        /// <summary>
        /// Converte um número decimal para formato monetário conforme a cultura informada.
        /// </summary>
        /// <param name="value">Valor decimal a ser convertido.</param>
        /// <param name="culture">Código da cultura (ex: "pt-BR"). Padrão: "pt-BR".</param>
        /// <returns>O valor formatado como moeda de acordo com a cultura.</returns>
        public static string ToCurrency(this decimal value, string culture = "pt-BR")
        => value.ToString("C", CultureInfo.CreateSpecificCulture(culture));

        /// <summary>
        /// Converte um número para formato percentual.
        /// </summary>
        /// <param name="value">Valor numérico a ser convertido.</param>
        /// <param name="assumeWholeNumber">Se verdadeiro, assume que o valor é inteiro (ex: 50 = 50%). Padrão: true.</param>
        /// <param name="decimals">Quantidade de casas decimais. Padrão: 2.</param>
        /// <returns>O valor formatado como porcentagem.</returns>
        public static string ToPercentage(this double value, bool assumeWholeNumber = true, int decimals = 2)
        {
            double finalValue = assumeWholeNumber ? value / 100 : value;
            return finalValue.ToString($"P{decimals}", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Formata um número de telefone brasileiro, removendo caracteres não numéricos e aplicando a máscara adequada.
        /// </summary>
        /// <param name="phoneNumber">Número de telefone a ser formatado. Pode conter caracteres não numéricos.</param>
        /// <returns>
        /// O número de telefone formatado:
        /// - Se possuir 10 dígitos: formato (00) 0000-0000.
        /// - Se possuir 11 dígitos: formato (00) 0 0000-0000.
        /// - Caso contrário, retorna o valor original.
        /// </returns>
        public static string ToBrazilianPhoneFormat(this string phoneNumber)
        {
            // Remove caracteres não numéricos
            var digits = new string(phoneNumber.Where(char.IsDigit).ToArray());
            if (digits.Length == 10) // Formato sem código de país
            {
                return Convert.ToUInt64(digits).ToString(@"(00) 0000-0000");
            }
            else if (digits.Length == 11) // Formato com código de área
            {
                return Convert.ToUInt64(digits).ToString(@"(00) 0 0000-0000");
            }
            else
            {
                return phoneNumber; // Retorna o número original se o formato for inválido
            }
        }
        
        /// <summary>
        /// Retorna apenas os caracteres numéricos (dígitos) presentes na string de entrada.
        /// </summary>
        /// <param name="input">String de entrada que pode conter caracteres não numéricos. Não deve ser nula.</param>
        /// <returns>
        /// Uma nova string contendo somente os dígitos (0-9) encontrados em <paramref name="input"/>.
        /// Se não houver dígitos, retorna uma string vazia.
        /// </returns>
        public static string ToDigitsOnly(this string input)
        => new string(input.Where(char.IsDigit).ToArray());

        /// <summary>
        /// Converte uma string em um "slug" (formato amigável para URLs), substituindo espaços por hífens e convertendo para minúsculas.
        /// </summary>
        /// <param name="input">Texto de entrada a ser convertido em slug.</param>
        /// <returns>Uma string formatada como slug, com espaços substituídos por hífens e todos os caracteres em minúsculo.</returns>
        public static string ToSlug(this string input)
        {
            string inputFiltro = new string(input
                .Where(c => char.IsLetterOrDigit(c) || char.IsSeparator(c))
                .ToArray());

            // Substitui espaços por hífens, converte para minúsculo e
            // remove espaços extras
            return inputFiltro
                .Trim()
                .Replace(" ", "-")
                .ToLower();
        } 
    }
}
