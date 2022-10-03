using API_Criptografia01.Model;
using Microsoft.AspNetCore.Mvc;
using TesteCriptografia;

namespace API_Criptografia01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Criptografia01Controller : ControllerBase
    {
        private readonly CriptografiaService _serviceCript;
        public Criptografia01Controller(CriptografiaService criptografiaHelper)
        {
            _serviceCript = new CriptografiaService();
        }

        [HttpPost("encriptar")]
        public ActionResult CriptografarChave([FromBody] EntradaCripto entradaCripto)
        {
            try
            {
                var chaveRetorno = _serviceCript.Encriptar(entradaCripto.Chave, entradaCripto.VetorInicializacao, entradaCripto.TextoChave);

                return Ok(chaveRetorno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("decriptar")]
        public ActionResult DesCriptografarChave([FromBody] EntradaCripto entradaCripto)
        {
            try
            {
            var chaveRetorno = _serviceCript.Decriptar(entradaCripto.Chave, entradaCripto.VetorInicializacao, entradaCripto.TextoChave);

            return Ok(chaveRetorno);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("encriptar64")]
        public ActionResult CriptografarChave64([FromBody] EntradaCripto entradaCripto)
        {
            try
            {
                var chaveRetorno = _serviceCript.Encriptar64(entradaCripto.Chave, entradaCripto.VetorInicializacao, entradaCripto.TextoChave);

                return Ok(chaveRetorno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("decriptar64")]
        public ActionResult DesCriptografarChave64([FromBody] EntradaCripto entradaCripto)
        {
            try
            {
                var chaveRetorno = _serviceCript.Decriptar64(entradaCripto.Chave, entradaCripto.VetorInicializacao, entradaCripto.TextoChave);

                return Ok(chaveRetorno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
