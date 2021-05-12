using System;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Web.Models;


namespace _Api.Controllers
{
    [ApiController]
    public class CadastrarController : Controller
    {
        private readonly IServiceRepository _service;
        
        public CadastrarController(IServiceRepository service)
        {
            _service = service;
        }

        [Route("/Cadastrar")]
        public IActionResult Cadastrar()
        {
            return View();    
        }

        [Route("/Cadastrar")]
        [HttpPost]
        public IActionResult Cadastrar([FromBody]PessoaViewModelInput pessoaViewModelInput)
        {
            try 
            {
                _service.Adicionar(pessoaViewModelInput);
                return StatusCode(200,"Deu certo!");
            }
            catch (Exception ex)
            {
                return StatusCode(400,"Erro!");
                throw new Exception("Erro desconhecido!", ex);
            }
        }
    }
}