﻿using backENDCliente.Modelo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backENDCliente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public ClientesController(AplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("obtenerClientes")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listCliente = await _context.Cliente.ToListAsync();
                return Ok(listCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost("guardarClientes")]
        public async Task<IActionResult> Post(Cliente cliente)
        {
            try
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return CreatedAtAction("Get", new { id = cliente.idCliente }, cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("editarCliente/{id}")]
        public async Task<IActionResult> Put(int id, Cliente cliente)
        {
            try
            {
                if (id != cliente.idCliente)
                {
                    return BadRequest();
                }

                var clienteItem = await _context.Cliente.FindAsync(id);

                if (clienteItem == null)
                {
                    return NotFound();
                }

                clienteItem.Nombres = cliente.Nombres;
                clienteItem.Nombres = cliente.Nombres;
                clienteItem.Apellidos = cliente.Apellidos;
                clienteItem.Direccion = cliente.Direccion;
                clienteItem.FechaNacimiento = cliente.FechaNacimiento;
                clienteItem.DPI = cliente.DPI;
                clienteItem.NIT = cliente.NIT;
                clienteItem.Empresa = cliente.Empresa;


                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("eliminarCliente/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var cliente = await _context.Cliente.FindAsync(id);

                if (cliente == null)
                {
                    return NotFound();
                }

                _context.Cliente.Remove(cliente);
                await _context.SaveChangesAsync();

                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}