using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MELI.Decoder.Api.Models;
using MELI.Decoder.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MELI.Decoder.Api.Controllers
{
    /// <summary>
    /// Controller de decodificación de textos.
    /// </summary>
    [ApiController]
    [Route("translate")]
    public class DecoderController : ControllerBase
    {
        private IDecoderService decoderService;
        public DecoderController(IDecoderService decoderServiceInit)
        {
            this.decoderService = decoderServiceInit;
        }

        /// <summary>
        /// Convierte un texto escrito en Código Morse a idioma humano.
        /// </summary>
        /// <param name="parameter">Objeto con el texto a decodificar</param>
        /// <returns>Resultado HTTP con el texto obtenido.</returns>
        [HttpPost]
        [HttpPost("2text")]
        public async Task<IActionResult> ConvertMorseToText([FromBody] ParameterDto parameter)
        {
            try
            {
                if (parameter.text == "" || parameter.text == null)
                {
                    return BadRequest("Debe ingresar un texto con el código morse a traducir");
                }
                string text = decoderService.Translate2Human(parameter.text.ToLower());
                if (text == "")
                {
                    return BadRequest("Debe ingresar un texto válido en código morse para traducir");
                }
                return Ok(text);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Convierte un texto escrito en idioma humano a Código Morse.
        /// </summary>
        /// <param name="parameter">Objeto con el texto a decodificar</param>
        /// <returns>Resultado HTTP con el texto obtenido.</returns>
        [HttpPost]
        [HttpPost("2morse")]
        public async Task<IActionResult> ConvertTextToMorse([FromBody] ParameterDto parameter)
        {
            try
            {
                if (parameter.text == "" || parameter.text == null)
                {
                    return BadRequest("Debe ingresar un texto con el texto a traducir");
                }
                string morse = decoderService.Translate2Morse(parameter.text.ToLower());
                return Ok(morse);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
