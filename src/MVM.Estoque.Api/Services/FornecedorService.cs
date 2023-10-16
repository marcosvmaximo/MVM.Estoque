using System;
using MVM.Estoque.Api.Data.Repositories;
using MVM.Estoque.Api.Dtos;
using MVM.Estoque.Api.Entities;
using MVM.Estoque.Api.Exceptions;
using MVM.Estoque.Api.Interfaces;
using MVM.Estoque.Api.Interfaces.Repositories;
using MVM.Estoque.Api.Interfaces.Services;
using MVM.Estoque.Api.Notifications;
using MVM.Estoque.Api.Services.Common;

namespace MVM.Estoque.Api.Services;

public class FornecedorService : MainService, IFornecedorService
{
    private readonly IFornecedorRepository _fornecedorRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly INotifyHandler _notify;

    public FornecedorService(IFornecedorRepository fornecedorRepository, IProdutoRepository produtoRepository, INotifyHandler notify)
    {
        _fornecedorRepository = fornecedorRepository;
        _produtoRepository = produtoRepository;
        _notify = notify;
    }

    public async Task<FornecedorViewModel?> CadastrarFornecedor(FornecedorDto model)
    {
        try
        {
            var existeFornecedor = await _fornecedorRepository.ObterPorCpf(model.Cpf) != null;

            if (existeFornecedor)
            {
                await _notify.PublicarNotificacao(new Notification(nameof(Fornecedor), "Fornecedor inserido já existe."));
                return null;
            }

            var endereco = new Endereco(model.Endereco.Cep,
                                        model.Endereco.Logradouro,
                                        model.Endereco.Complemento,
                                        model.Endereco.Numero,
                                        model.Endereco.Bairro,
                                        model.Endereco.Cidade,
                                        model.Endereco.UnidadeFederativa);

            var fornecedor = new Fornecedor(model.Cpf, model.Nome, model.DataNascimento, endereco);

            await _fornecedorRepository.Adicionar(fornecedor);

            int diferencaEmAnos = fornecedor.DataCriacao.Year - fornecedor.DataNascimento.Year;

            if (fornecedor.DataCriacao.Month < fornecedor.DataNascimento.Month || (fornecedor.DataCriacao.Month == fornecedor.DataNascimento.Month && fornecedor.DataCriacao.Day < fornecedor.DataNascimento.Day))
            {
                diferencaEmAnos--;
            }

            return new FornecedorViewModel()
            {
                Cpf = fornecedor.Cpf,
                Nome = fornecedor.Nome,
                Idade = diferencaEmAnos,
                Endereco = new EnderecoViewModel()
                {
                    Cep = endereco.Cep,
                    Complemento = endereco.Complemento,
                    Numero = endereco.Numero
                },
                Produtos = new List<ProdutoViewModel>()
            };
        }
        catch (DomainException ex)
        {
            await _notify.PublicarNotificacao(new Notification(ex.Key, ex.Message));
            return null;
        }
    }

    public async Task<FornecedorViewModel?> AtualizarFornecedor(Guid id, FornecedorDto model)
    {
        var fornecedor = await _fornecedorRepository.ObterPorId(id);

        if (fornecedor == null)
        {
            await _notify.PublicarNotificacao(new Notification(nameof(Fornecedor), "Fornecedor informado não existe ou não foi encontrado."));

            return null;
        }

        fornecedor.Cpf = model.Cpf;
        fornecedor.Nome = model.Nome;
        fornecedor.DataNascimento = model.DataNascimento;
        fornecedor.Endereco = new Endereco()
        {
            Cep = model.Endereco.Cep,
            Logradouro = model.Endereco.Logradouro,
            Complemento = model.Endereco.Complemento,
            Numero = model.Endereco.Numero,
            Bairro = model.Endereco.Bairro,
            Cidade = model.Endereco.Cidade,
            UnidadeFederativa = model.Endereco.UnidadeFederativa
        };

        await _fornecedorRepository.Atualizar(fornecedor);

        int diferencaEmAnos = fornecedor.DataCriacao.Year - fornecedor.DataNascimento.Year;

        if (fornecedor.DataCriacao.Month < fornecedor.DataNascimento.Month || (fornecedor.DataCriacao.Month == fornecedor.DataNascimento.Month && fornecedor.DataCriacao.Day < fornecedor.DataNascimento.Day))
        {
            diferencaEmAnos--;
        }

        return new FornecedorViewModel()
        {
            Cpf = fornecedor.Cpf,
            Nome = fornecedor.Nome,
            Idade = diferencaEmAnos,
            Endereco = new EnderecoViewModel()
            {
                Cep = fornecedor.Endereco.Cep,
                Complemento = fornecedor.Endereco.Complemento,
                Numero = fornecedor.Endereco.Numero
            },
            Produtos = new List<ProdutoViewModel>()
        };
    }

    public async Task DeletarFornecedor(Guid id)
    {
        var fornecedor = await _fornecedorRepository.ObterPorId(id);

        if (fornecedor == null)
        {
            await _notify.PublicarNotificacao(new Notification(nameof(Fornecedor), "Fornecedor informado não existe ou não foi encontrado."));

            return;
        }

        await _fornecedorRepository.Deletar(fornecedor);
    }

    public async Task<FornecedorViewModel> AdicionarProduto(Guid idFornecedor, ProdutoDto model)
    {
        try
        {
            Fornecedor? fornecedor = await _fornecedorRepository.ObterPorId(idFornecedor);

            if (fornecedor == null)
            {
                await _notify.PublicarNotificacao(new Notification(nameof(Fornecedor), "Fornecedor informado não existe ou não foi encontrado."));

                return null;
            }

            Produto produto = new Produto(idFornecedor, model.Nome, model.CategoriaProduto, model.Preco);
            await _produtoRepository.Adicionar(produto);

            //fornecedor.AdicionarProduto(produto);
            await _fornecedorRepository.Atualizar(fornecedor);

            int diferencaEmAnos = fornecedor.DataCriacao.Year - fornecedor.DataNascimento.Year;
            if (fornecedor.DataCriacao.Month < fornecedor.DataNascimento.Month || (fornecedor.DataCriacao.Month == fornecedor.DataNascimento.Month && fornecedor.DataCriacao.Day < fornecedor.DataNascimento.Day))
            {
                diferencaEmAnos--;
            }

            List<ProdutoViewModel> produtosModel = new List<ProdutoViewModel>();
            foreach (Produto? p in fornecedor.Produtos)
            {
                produtosModel.Add(new ProdutoViewModel()
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    CategoriaProduto = p.Categoria,
                    Preco = p.Preco,
                    Fornecedor = null
                });
            }

            return new FornecedorViewModel()
            {
                Cpf = fornecedor.Cpf,
                Nome = fornecedor.Nome,
                Idade = diferencaEmAnos,
                Endereco = new EnderecoViewModel()
                {
                    Cep = fornecedor.Endereco.Cep,
                    Complemento = fornecedor.Endereco.Complemento,
                    Numero = fornecedor.Endereco.Numero
                },
                Produtos = produtosModel
            };
        }
        catch (DomainException ex)
        {
            await _notify.PublicarNotificacao(new Notification(ex.Key, ex.Message));
            return null;
        }
    }
}

