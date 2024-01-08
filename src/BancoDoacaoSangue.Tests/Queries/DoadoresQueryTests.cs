using AutoMapper;
using BancoDoacaoSangue.Application.Commands.CadastrarDoacao;
using BancoDoacaoSangue.Application.Mappers;
using BancoDoacaoSangue.Application.Queries.BuscarDoadores;
using BancoDoacaoSangue.Application.Queries.BuscarDoadorPorId;
using BancoDoacaoSangue.Core.Entities;
using BancoDoacaoSangue.Core.Exceptions;
using BancoDoacaoSangue.Core.Repositories;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace BancoDoacaoSangue.Tests.Queries
{
    public class DoadoresQueryTests
    {
        private readonly Mock<IDoadorRepository> _mockDoadorRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;

        public DoadoresQueryTests()
        {
            _mockDoadorRepository = new Mock<IDoadorRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUnitOfWork.SetupGet(uow => uow.Doadores).Returns(_mockDoadorRepository.Object);
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new DoadorMapper());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public async Task BuscarDoadores_Executed_ReturnListView()
        {
            var doador = new Doador
            {
                NomeCompleto = "teste",
                Email = "teste@gmail.com",
                DataNascimento = new DateTime(1997, 6, 1),
                Genero = "M",
                Peso = 65,
                TipoSanguineo = "B",
                FatorRh = "positivo",
                Endereco = new Endereco
                {
                    Cep = "000000000",
                    Cidade = "cidade",
                    Estado = "estado",
                    Logradouro = "logradouro"
                }
            };
            var list = new List<Doador>()
            {
                doador
            };
            
            _mockDoadorRepository.Setup(dr => dr.GetAsync(null!,null!, "Endereco", true)).ReturnsAsync(list);

            var query = new BuscarDoadoresQuery();
            var queryHandler = new BuscarDoadoresQueryHandler(_mockUnitOfWork.Object, _mapper);
            var result = await queryHandler.Handle(query, new CancellationToken());

            Assert.NotNull(result);
            Assert.Single(result);
            _mockDoadorRepository.Verify(or => or.GetAsync(null!, null!, "Endereco", true), Times.Once);
        }

        [Fact]
        public async Task BuscarDoadorPorId_Executed_ReturnListView()
        {
            var doador = new Doador
            {
                NomeCompleto = "teste",
                Email = "teste@gmail.com",
                DataNascimento = new DateTime(1997, 6, 1),
                Genero = "M",
                Peso = 65,
                TipoSanguineo = "B",
                FatorRh = "positivo",
                Endereco = new Endereco
                {
                    Cep = "000000000",
                    Cidade = "cidade",
                    Estado = "estado",
                    Logradouro = "logradouro"
                }
            };
            
            
            _mockDoadorRepository.Setup(dr => dr.GetByIdIncludeEndereco(It.IsAny<int>())).ReturnsAsync(doador);

            var query = new BuscarDoadorPorIdQuery(1);
            var queryHandler = new BuscarDoadorPorIdQueryHandler(_mockUnitOfWork.Object, _mapper);
            var result = await queryHandler.Handle(query, new CancellationToken());

            Assert.NotNull(result);
            _mockDoadorRepository.Verify(or => or.GetByIdIncludeEndereco(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task BuscarDoadorPorId_Executed_ThrowNotFoundException()
        {

            var query = new BuscarDoadorPorIdQuery(1);
            var queryHandler = new BuscarDoadorPorIdQueryHandler(_mockUnitOfWork.Object, _mapper);

            await Assert.ThrowsAsync<NotFoundException>(async () => await queryHandler.Handle(query, new CancellationToken()));
        }
    }  
}
