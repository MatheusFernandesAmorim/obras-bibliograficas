using System.Globalization;
using System.Linq;

namespace ObrasBibliograficas.Servicos
{
    public static class Tratamentos
    {
        #region ' Variáveis '
        private readonly static string[] arrayPreposicoes = { "da", "de", "do", "das", "dos" };
        private readonly static string[] arraySobrenomes = new string[] { "FILHO", "FILHA", "NETO", "NETA", "SOBRINHO", "SOBRINHA", "JUNIOR" };
        #endregion

        #region ' TratarParaAdjuntosAdnominais '
        /// <summary>
        /// Método responsável por realizar o tratamento para adjuntos adnominais
        /// </summary>
        /// <param name="texto">Parâmetro que representa o texto a ser tratado</param>
        /// <remarks>Regra: da, de, do, das, dos</remarks>
        /// <returns>O texto tratado</returns>
        public static string TratarParaAdjuntosAdnominais(string texto)
        {
            // Verifica se o nome informado corresponde a um adjunto adnominal
            if (arrayPreposicoes.Contains(texto.ToLower()))
            {
                texto = texto.ToLower();
            }

            return texto;
        }
        #endregion

        #region ' TratarParaFormatoNominal '
        /// <summary>
        /// Método responsável por realizar o tratamento para o formato nominal
        /// </summary>
        /// <param name="texto">Parâmetro que representa o texto a ser formatado</param>
        /// <remarks>Regra: A primeira letra maiúscula e as demais letras minúsculas</remarks>
        /// <returns>O texto no formato nominal</returns>
        public static string TratarParaFormatoNominal(string texto)
        {
            TextInfo configuracaoTexto = new CultureInfo("en-US", false).TextInfo;

            return configuracaoTexto.ToTitleCase(texto.ToLower());
        }
        #endregion

        #region ' TratarParaSobrenomesEspeciais '
        /// <summary>
        /// Método responsável por realizar o tratamento de sobrenomes especiais 
        /// </summary>
        /// <param name="partesTexto">Parâmetro que representa as partes de um nome</param>
        /// <remarks>Regra: FILHO, FILHA, NETO, NETA, SOBRINHO, SOBRINHA, JUNIOR</remarks>
        /// <returns>As partes do nome considerando os sobrenomes especiais</returns>
        public static string[] TratarParaSobrenomesEspeciais(string[] partesTexto)
        {
            // Instancia um novo array
            string[] novoPartesNome = new string[partesTexto.Length];

            // Atribui na variável o antepenúltimo nome do array
            string antepenultimonome = partesTexto.Reverse().Skip(1).Take(1).FirstOrDefault();

            // Atribui na variável o último nome do array
            string sobrenome = partesTexto.LastOrDefault();

            // Verifica se o último nome corresponde a um sobrenome especial
            if (arraySobrenomes.Contains(sobrenome.ToUpper()) && partesTexto.Length > 2)
            {
                // Reconstrói o sobrenome
                sobrenome = $"{antepenultimonome} {sobrenome}";

                // Copia todo contéudo, exceto o antepenúltimo nome do array
                novoPartesNome = partesTexto.Where(p => p != antepenultimonome).ToArray();

                // Insere o novo sobrenome na última posição do array
                novoPartesNome.SetValue(sobrenome, novoPartesNome.Length - 1);
            }
            else
            {
                novoPartesNome = partesTexto;
            }

            return novoPartesNome;
        }
        #endregion
    }
}