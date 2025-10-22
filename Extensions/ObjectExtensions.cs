using Newtonsoft.Json.Linq;

namespace ASHelpers.Extensions
{
    /// <summary>
    /// Métodos de extensão para manipulação de objetos, como exclusão de propriedades e conversão para JSON.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Tenta converter qualquer objeto em string, utilizando um valor padrão caso seja nulo.
        /// </summary>
        /// <param name="obj">Objeto a ser convertido para string.</param>
        /// <param name="fallback">Valor padrão caso o objeto seja nulo. Padrão: string vazia.</param>
        /// <returns>Representação em string do objeto ou o valor padrão.</returns>
        public static string ToStringNullable(this object? obj, string fallback = "")
        => obj?.ToString() ?? fallback;

        /// <summary>
        /// Exclui propriedades especificadas de um objeto via reflexão.
        /// Funciona apenas em memória, não é traduzido para SQL pelo EF Core.
        /// Exemplo de uso:
        /// var result = users.Select(u => u.Exclude("Password", "Token")).ToList();
        /// </summary>
        /// <typeparam name="T">Tipo do objeto de origem.</typeparam>
        /// <param name="obj">Objeto de origem.</param>
        /// <param name="props">Nomes das propriedades a serem excluídas.</param>
        /// <returns>
        /// Um dicionário contendo as propriedades restantes do objeto, com seus respectivos valores.
        /// </returns>
        public static object Exclude<T>(this T obj, params string[] props)
        {
            var dict = typeof(T).GetProperties()
                .Where(p => !props.Contains(p.Name))
                .ToDictionary(p => p.Name, p => p.GetValue(obj));

            return dict;
        }

        /// <summary>
        /// Converte o objeto informado em um JSON formatado utilizando a biblioteca Newtonsoft.Json.
        /// </summary>
        /// <param name="obj">Objeto a ser convertido para JSON.</param>
        /// <returns>
        /// Um <see cref="JObject"/> representando o objeto em formato JSON.
        /// Se o objeto for nulo, retorna um <see cref="JObject"/> vazio.
        /// Se o objeto já for um <see cref="JObject"/>, retorna o próprio objeto.
        /// Se for uma string, tenta interpretar como JSON; caso não seja válido, retorna um <see cref="JObject"/> com o valor bruto.
        /// </returns>
        public static JObject ToJson(this object? obj)
        {
            if (obj is null)
                return new JObject();

            // Se já for um JObject, retorna direto
            if (obj is JObject jObj)
                return jObj;

            // Se for uma string, tenta interpretar como JSON
            if (obj is string jsonString)
            {
                try
                {
                    return JObject.Parse(jsonString);
                }
                catch
                {
                    // Caso a string não seja JSON válido, cria um JObject com valor bruto
                    return new JObject { ["value"] = jsonString };
                }
            }

            // Se for qualquer outro tipo de objeto .NET
            return JObject.FromObject(obj);
        }
    }
}
