using Microsoft.AspNetCore.Mvc;
using ObrasBibliograficas.Servicos.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ObrasBibliograficas.Aplicacao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ObrasBibliograficasController : ControllerBase
    {
        #region ' #GET '

        #endregion

        #region ' #POST '

        #region ' FormatarNomeAutorLivro '
        /// <summary>
        /// Método responsável por formatar um nome específico da lista, no formato de referência bibliográfica
        /// </summary>
        /// <param name="numeroNome">Parâmetro que representa a posição do nome a ser formatado</param>
        /// <param name="listaNomesAutores">Parâmetro que representa a lista de nome a serem formatados</param>
        /// <returns>A lista com os mesmo nomes, formatados como referências bibliográficas</returns>
        [HttpPost("FormatarNomeAutorLivro")]
        [ProducesResponseType(typeof(List<string>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult FormatarNomeAutorLivro([FromServices] ObrasBibliograficasService _servico, [FromQuery, Required, Range(1, int.MaxValue)] int numeroNome, [FromQuery, Required, MinLength(1)] List<string> listaNomesAutores)
        {
            try
            {
                List<string> listaNomesAutoresFormatados = _servico.FormatarNomeAutorLivro(numeroNome, listaNomesAutores);

                return Ok(listaNomesAutoresFormatados);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region ' FormatarNomesAutoresLivros '
        /// <summary>
        /// Método responsável por formatar os nomes fornecidos em uma lista, no formato de referência bibliográfica
        /// </summary>        
        /// <param name="listaNomesAutores">Parâmetro que representa a lista de nome a serem formatados</param>
        /// <returns>A lista com os mesmo nomes, formatados como referências bibliográficas</returns>
        [HttpPost("FormatarNomesAutoresLivros")]
        [ProducesResponseType(typeof(List<string>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult FormatarNomesAutoresLivros([FromServices] ObrasBibliograficasService _servico, [FromQuery, Required, MinLength(1)] List<string> listaNomesAutores)
        {
            try
            {
                List<string> listaNomesAutoresFormatados = _servico.FormatarNomesAutoresLivros(listaNomesAutores);

                return Ok(listaNomesAutoresFormatados);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #endregion
    }
}