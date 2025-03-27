using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Authorization;
using P7CreateRestApi.Repositories;
using P7CreateRestApi.Model;
using System.Collections;

namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurvePointsController : ControllerBase
    {
        private readonly ICurvePointsRepository _repository;

        public CurvePointsController(ICurvePointsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/CurvePoints
        [HttpGet]
        [Authorize(Roles = "Admin, Technician, Visitor")]
        public async Task<ActionResult<IEnumerable<CurvePoint>>> GetCurvePoints()
        {
            var curve = await _repository.GetAllCurvePointAsync();

            if (curve == null || !curve.Any())
            {
                return NotFound();
            }

            return Ok(curve);
        }

        // GET: api/CurvePoints/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Technician, Visitor")]
        public async Task<ActionResult<CurvePoint>> GetCurvePoint(int id)
        {
            var curvePoint = await _repository.GetCurvePointByIdAsync(id);

            if (curvePoint == null)
            {
                return NotFound();
            }

            return Ok(curvePoint);
        }

        // PUT: api/CurvePoints/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Technician")]
        public async Task<IActionResult> PutCurvePoint(int id, CurvePointDTO curvePointDTO)
        {
            var existingCurvePoint = await _repository.GetCurvePointByIdAsync(id);

            if (existingCurvePoint == null)
            {
                return NotFound();
            }

            existingCurvePoint.CurveId = curvePointDTO.CurveId;
            existingCurvePoint.AsOfDate = curvePointDTO.AsOfDate;
            existingCurvePoint.CurvePointValue = curvePointDTO.CurvePointValue;

            await _repository.UpdateCurvePointAsync(existingCurvePoint);

            return NoContent();
        }

        // POST: api/CurvePoints
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin, Technician")]
        public async Task<ActionResult<CurvePoint>> PostCurvePoint(CurvePointDTO curvePointDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var curvePoint = new CurvePoint
            {
                CurveId = curvePointDTO.CurveId,
                AsOfDate = curvePointDTO.AsOfDate,
                CurvePointValue = curvePointDTO.CurvePointValue
            };

            var createdCurvePoint = await _repository.CreateCurvePointAsync(curvePoint);
            return CreatedAtAction("GetCurvePoint", new { id = createdCurvePoint.Id }, createdCurvePoint);
        }

        // DELETE: api/CurvePoints/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCurvePoint(int id)
        {
            var curvePoint = await _repository.GetCurvePointByIdAsync(id);

            if (curvePoint == null)
            {
                return NotFound();
            }

            await _repository.DeleteCurvePointAsync(id);

            return NoContent();
        }
    }
}
