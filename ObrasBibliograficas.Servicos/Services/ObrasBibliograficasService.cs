using System;
using System.Collections.Generic;
using System.Linq;

namespace ObrasBibliograficas.Servicos.Services
{
    public class ObrasBibliograficasService
    {
        #region ' FormatarNomeAutorLivro '
        /// <summary>
        /// Método responsável por formatar um nome específico da lista, no formato de referência bibliográfica
        /// </summary>
        /// <param name="numeroNome">Parâmetro que representa a posição do nome a ser formatado</param>
        /// <param name="listaNomesAutores">Parâmetro que representa a lista de nome a serem formatados</param>
        /// <returns>Um nome formatado como referência bibliográfica</returns>
        public List<string> FormatarNomeAutorLivro(int numeroNome, List<string> listaNomesAutores)
        {
            // Representa a posição que do autor que o usuário deseja ver formatado
            numeroNome = numeroNome - 1;

            if (listaNomesAutores.Count > numeroNome)
            {
                // Identifica qual é o autor dentro da lista
                string autor = listaNomesAutores.ElementAt(numeroNome);

                // Cria uma lista de um único item
                List<string> listaUnicoAutor = listaNomesAutores.Where(l => l.Equals(autor)).ToList();

                return FormatarNomesAutoresLivros(listaUnicoAutor);
            }
            else
            {
                throw new Exception("O número do nome informado é inválido ou não existe!");
            }            
        }
        #endregion

        #region ' FormatarNomesAutoresLivros '
        /// <summary>
        /// Método responsável por formatar os nomes fornecidos em uma lista, no formato de referência bibliográfica
        /// </summary>
        /// <param name="listaNomesAutores">Parâmetro que representa a lista de nome a serem formatados</param>
        /// <returns>A lista com os mesmo nomes, formatados como referências bibliográficas</returns>
        public List<string> FormatarNomesAutoresLivros(List<string> listaNomesAutores)
        {
            // Instancia uma nova lista de nomes
            List<string> listaNomesAutoresFormatados = new List<string>();

            // Percorre toda a lista de nomes
            foreach (string nomeAutor in listaNomesAutores)
            {
                // Realiza a separação das partes do nome completo
                string[] partesNome = nomeAutor.Split(' ');

                // Verifica se o nome completo possui apenas 1 parte
                if (partesNome.Length.Equals(1))
                {
                    listaNomesAutoresFormatados.Add(partesNome.Last().ToUpper());                   
                }
                // Verifica se o nome completo possui mais que 1 parte
                else if (partesNome.Length > 1)
                {
                    listaNomesAutoresFormatados.Add(TratarListaNomes(partesNome));
                }
            }

            return listaNomesAutoresFormatados;
        }
        #endregion

        #region ' TratarListaNomes '
        /// <summary>
        /// Método responsável por realizar o tratamento de uma lista de nomes
        /// </summary>
        /// <param name="partesNome">Parâmetro que representa as partes de um nome</param>
        /// <returns>A lista de nomes com os devidos tratamentos</returns>
        private string TratarListaNomes(string[] partesNome)
        {
            // Realiza o tratamento para os sobrenomes especiais
            partesNome = Tratamentos.TratarParaSobrenomesEspeciais(partesNome);

            // Atribui na variável o restante do nome completo, exceto o sobrenome
            string[] nome = partesNome.Take(partesNome.Length - 1).ToArray();

            // Atribui na variável a última parte do nome completo
            string sobrenome = partesNome.Last();

            // instancia um novo array de acordo com o número de registros da lista de nomes
            string[] nomeTratado = new string[partesNome.Length];

            // Percorre todas as partes do nome
            for (int i = 0; i < nome.Length; i++)
            {
                string texto = nome[i];

                texto = Tratamentos.TratarParaFormatoNominal(texto);

                texto = Tratamentos.TratarParaAdjuntosAdnominais(texto);

                nomeTratado.SetValue(texto, i);
            }

            // Reconstrói o nome, após todos os tratamentos
            string restoNome = string.Join(" ", nomeTratado);

            // Contrói o nome completo totalmente formatado
            string nomeCompletoFormatado = $"{sobrenome.ToUpper()}, {restoNome.Trim()}";

            return nomeCompletoFormatado;
        }
        #endregion
    }
}