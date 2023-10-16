using System;
using Microsoft.AspNetCore.Mvc;
using MVM.Estoque.Api.Controllers.Common;
using MVM.Estoque.Api.Dtos;
using MVM.Estoque.Api.Interfaces;
using MVM.Estoque.Api.Interfaces.Repositories;
using MVM.Estoque.Api.Interfaces.Services;

namespace MVM.Estoque.Api.Controllers;

[Route("api/forncedor")]
public class FornecedorController : MainController
{
    private readonly IFornecedorService _service;
    private readonly IFornecedorRepository _repository;

    public FornecedorController(IFornecedorService service,
                                IFornecedorRepository repository,
                                INotifyHandler notify) : base(notify)
    {
        _service = service;
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult> ObterTodosFonecedores()
    {
        var fornecedores = await _repository.ObterTodos();

        if (fornecedores == null || !fornecedores.Any())
        {
            return NotFound(new
            {
                HttpCode = 404,
                Sucess = true,
                Message = "Fornecedor não encontrado.",
                Data = fornecedores
            });
        }

        return await CustomResponse(fornecedores);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> ObterForncedorPorId([FromRoute] Guid id)
    {
        var fornecedor = await _repository.ObterPorId(id);

        if (fornecedor == null)
        {
            return NotFound(new
            {
                HttpCode = 404,
                Sucess = true,
                Message = "Fornecedor não encontrado.",
                Data = fornecedor
            });
        }

        return await CustomResponse(fornecedor);
    }

    [HttpPost]
    public async Task<ActionResult> CadastrarForncedor([FromBody] FornecedorDto model)
    {
        if (!ModelState.IsValid)
            return await CustomResponse(ModelState);

        var result = await _service.CadastrarFornecedor(model);

        return await CustomResponse(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DesativarFornecedor([FromRoute] Guid id)
    {
        if (id == Guid.Empty)
        {
            return BadRequest(new
            {
                HttpCode = 400,
                Sucess = false,
                Message = "Id informado inválido."
            });
        }

        await _service.DeletarFornecedor(id);

        return await CustomResponse();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> AtualizarFornecedor([FromRoute] Guid id, [FromBody] FornecedorDto model)
    {
        if (!ModelState.IsValid)
            return await CustomResponse(ModelState);

        if (id == Guid.Empty)
        {
            return BadRequest(new
            {
                HttpCode = 400,
                Sucess = false,
                Message = "Id informado inválido."
            });
        }

        await _service.AtualizarFornecedor(id, model);

        return await CustomResponse();
    }

    [HttpPost("{idFornecedor:guid}")]
    public async Task<ActionResult> AdicionarProduto([FromRoute] Guid idFornecedor, [FromBody] ProdutoDto model)
    {
        if (!ModelState.IsValid)
            return await CustomResponse(ModelState);

        var result = await _service.AdicionarProduto(idFornecedor, model);

        return await CustomResponse(result);
    }
}

