using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MVM.Estoque.Api.Interfaces;
using MVM.Estoque.Api.Notifications;

namespace MVM.Estoque.Api.Controllers.Common;

[ApiController]
public abstract class MainController : ControllerBase
{
    private readonly INotifyHandler _notify;

    public MainController(INotifyHandler notify)
    {
        _notify = notify;
    }

    protected virtual async Task<ActionResult> CustomResponse(object result = null)
    {
        if (await _notify.PossuiNotificacao())
        {
            return BadRequest(new
            {
                HttpCode = 400,
                Sucess = false,
                Message = "Ocorreu uma falha durante a requisição.",
                Data = _notify.ObterNotificacoes().Result.ToList()
            }); ;
        }

        return Ok(new
        {
            HttpCode = 200,
            Sucess = true,
            Message = "Requisição enviada com sucesso.",
            Data = result
        });
    }

    protected virtual async Task<ActionResult> CustomResponse(ModelStateDictionary model)
    {
        if (!model.IsValid)
        {
            var erros = model.Values.SelectMany(e => e.Errors);

            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;

                await _notify.PublicarNotificacao(new Notification("", erroMsg));
            }
        }

        return await CustomResponse();
    }
}

