using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dtos.County;
using Api.Domain.Interfaces.Services.County;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountiesController : ControllerBase
    {
        public ICountyService _service;
        public CountiesController(ICountyService service)
        {
            _service = service;
        }

        #region GET
        [Authorize("Bearer")]
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var _getResult = await _service.Get(id);

                if (_getResult == null) return NotFound();

                return Ok(_getResult);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("id/{id}", Name = "GetCompleteById")]
        public async Task<ActionResult> GetCompleteById(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var _getCompleteResult = await _service.GetCompleteById(id);

                if (_getCompleteResult == null) return NotFound();

                return Ok(_getCompleteResult);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpGet]
        [Route("ibge/{IBGECode}", Name = "GetCompleteByIBGECode")]
        public async Task<ActionResult> GetCompleteByIBGE(int IBGECode)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var _getCompleteByIBGE = await _service.GetCompleteByIBGE(IBGECode);

                if (_getCompleteByIBGE == null) return NotFound();

                return Ok(_getCompleteByIBGE);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var _getAllResult = await _service.GetAll();

                if (_getAllResult == null) return NotFound();

                return Ok(_getAllResult);
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
        public async Task<ActionResult> Post([FromBody] CountyDtoCreate countyDtoCreate)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var result = await _service.Post(countyDtoCreate);
                if (result == null) return BadRequest();
                return Created(new Uri(Url.Link("GetWithId", new { id = result.Id })), result);
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
        public async Task<ActionResult> Put([FromBody] CountyDtoUpdate countyDtoUpdate)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var _updateResult = await _service.Put(countyDtoUpdate);
                if (_updateResult == null) return BadRequest();
                return Ok(_updateResult);
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
        [Route("{id}", Name = "DeleteById")]
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
