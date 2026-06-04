using ProjetoCrudPessoa.Domain;
using ProjetoCrudPessoa.DTOs;
using ProjetoCrudPessoa.Repositories;

namespace ProjetoCrudPessoa.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _repository;

        public PessoaService(IPessoaRepository repository)
        {
            _repository = repository;
        }

        public async Task AdicionarAsync(PessoaCreateDto dto)
        {
            if (await _repository.ExisteCpfAsync(dto.CPF))
            {
                throw new InvalidOperationException("CPF já cadastrado.");
            }

            var pessoa = new Pessoa
            {
                Nome = dto.Nome,
                CPF = dto.CPF,
                Idade = dto.Idade,
                DataNascimento = dto.DataNascimento,
                Status = 1
            };

            await _repository.AdicionarAsync(pessoa);
        }

        public async Task<bool> AtualizarAsync(int id, PessoaPatchDto dto)
        {
            var pessoa = await _repository.ObterPorIdAsync(id);

            if (pessoa == null)
                return false;

            if (dto.CPF != null)
            {
                if (await _repository.ExisteCpfEmOutroRegistroAsync(id, dto.CPF))
                {
                    throw new InvalidOperationException("CPF já cadastrado.");
                }

                pessoa.CPF = dto.CPF;
            }

            if (dto.Nome != null)
                pessoa.Nome = dto.Nome;

            if (dto.Idade.HasValue)
                pessoa.Idade = dto.Idade.Value;

            if (dto.DataNascimento.HasValue)
                pessoa.DataNascimento = dto.DataNascimento.Value;

            await _repository.AtualizarAsync(pessoa);

            return true;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var pessoa = await _repository.ObterPorIdAsync(id);

            if (pessoa == null)
                return false;

            await _repository.RemoverAsync(pessoa);

            return true;
        }

        public async Task<object> ListarAsync(
            int page,
            int pageSize,
            string? nome,
            string? cpf)
        {
            var totalRegistros = await _repository.ContarAsync(
                nome,
                cpf);

            var pessoas = await _repository.ListarAsync(
                page,
                pageSize,
                nome,
                cpf);

            var dados = pessoas.Select(p => new PessoaResponseDto
            {
                Id = p.Id,
                Nome = p.Nome,
                CPF = p.CPF,
                Idade = p.Idade,
                DataNascimento = p.DataNascimento,
                Status = p.Status
            });

            return new
            {
                page,
                pageSize,
                totalRegistros,
                totalPaginas = (int)Math.Ceiling((double)totalRegistros / pageSize),
                dados
            };
        }
    }
}