using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dtos.ZipCode;
using Api.Domain.Interfaces.Services.ZipCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZipCodesController : ControllerBase
    {
        public IZipCodeService _service;

        public ZipCodesController(IZipCodeService service)
        {
            _service = service;
        }

        #region GET
        [Authorize("Bearer")]
        [HttpGet("id/{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var _cepResult = await _service.Get(id);

                if (_cepResult == null) return NotFound();

                return Ok(await _service.Get(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("cep/{cep}")]
        public async Task<ActionResult> GetByCep(string cep)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var _cepResult = await _service.Get(cep);

                if (_cepResult == null) return NotFound();

                return Ok(_cepResult);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        #endregion

        #region POST
        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ZipCodeDtoCreate zipCodeCreate)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                return Ok(await _service.Post(zipCodeCreate));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        #endregion

        #region PUT
        [Authorize("Bearer")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ZipCodeDtoUpdate zipCodeUpdate)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                return Ok(await _service.Put(zipCodeUpdate));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        #endregion

        #region DELETE
        [Authorize("Bearer")]
        [HttpDelete]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                return Ok(await _service.Delete(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        #endregion
    }
}
