﻿using HouseOrganizer.DTOs.Login;
using HouseOrganizer.Repositories.Interfaces;
using HouseOrganizer.Services;
using Microsoft.AspNetCore.Mvc;

namespace HouseOrganizer.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;

        public LoginController(IUsuarioRepository usuarioRepository, ITokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }
        /// <summary>
        /// Autentica usuário no sistema por nome usuário e senha
        /// </summary>
        /// <param name="usuarioDTO">DTO de usuário com nome usuário e senha</param>
        /// <returns>Retorna Usuário e Token.</returns>
        [HttpPost]
        public IActionResult Autenticar([FromBody] LoginDTO usuarioDTO)
        {
            var usuario = _usuarioRepository.ObterPorNomeUsuarioESenha(usuarioDTO.NomeUsuario, usuarioDTO.Senha);

            if (usuario == null)
            {
                return NotFound(new { mensagem = "Usuário ou senha inválidos!" });
            }
            var token = _tokenService.GerarToken(usuario);

            usuario.Senha = null;

            return Ok(new
            {
                Usuario = usuario,
                Token = token
            });

        }
    }
}
