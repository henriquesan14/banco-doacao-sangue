using AutoMapper;
using BancoDoacaoSangue.Application.Commands.AtualizaDoador;
using BancoDoacaoSangue.Application.Commands.CadastrarDoador;
using BancoDoacaoSangue.Application.Mappers;
using BancoDoacaoSangue.Core.DTOs;
using BancoDoacaoSangue.Core.Entities;
using BancoDoacaoSangue.Core.Exceptions;
using BancoDoacaoSangue.Core.Repositories;
using BancoDoacaoSangue.Infra.Services;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace BancoDoacaoSangue.Tests.Commands
{
    public class DoadorCommandTests
    {
        private Mock<IDoadorRepository> _mockDoadorRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<ICepService> _mockCepService;
        private IMapper _mapper;

        public DoadorCommandTests()
        {
            _mockDoadorRepository = new Mock<IDoadorRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCepService = new Mock<ICepService>();

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
        public async Task CreateDoador_Executed_ReturnSuccess()
        {
            // Arrange

            var doador = new Doador
            {
                NomeCompleto = "teste",
                Email = "teste@gmail.com",
                DataNascimento = new DateTime(1997, 6, 1),
                Genero = "M",
                Peso = 65,
                TipoSanguineo = "B",
                FatorRh = "positivo"
            };
            IReadOnlyList<Doador> list = new List<Doador>() {
                doador
            };

            var responseCep = new ResponseCepDto
            {
                Logradouro = "teste",
                Localidade = "cidade",
                Uf = "PB"
            };

            var cadastrarDoadorCommand = new CadastrarDoadorCommand("teste","teste@gmail.com", new DateTime(1997,6,1), "M", 65, "B", "positivo", "58328000");

            
            _mockCepService.Setup(cs => cs.BuscaCep(It.IsAny<string>())).ReturnsAsync(responseCep);
            _mockDoadorRepository.Setup(pr => pr.AddAsync(It.IsAny<Doador>())).ReturnsAsync(doador);
            

            //Act

            var command = new CadastrarDoadorCommandHandler(_mockUnitOfWork.Object, _mapper, _mockCepService.Object);
            var result = await command.Handle(cadastrarDoadorCommand, new CancellationToken());

            // Assert
        
            _mockCepService.Verify(cs => cs.BuscaCep(It.IsAny<string>()), Times.Once());
            _mockDoadorRepository.Verify(or => or.AddAsync(It.IsAny<Doador>()), Times.Once);

        }

        [Fact]
        public async Task CreateDoador_Executed_ThrowDoadorJaExisteException()
        {
            // Arrange

            var doador = new Doador
            {
                NomeCompleto = "teste",
                Email = "teste@gmail.com",
                DataNascimento = new DateTime(1997, 6, 1),
                Genero = "M",
                Peso = 65,
                TipoSanguineo = "B",
                FatorRh = "positivo"
            };
            IReadOnlyList<Doador> list = new List<Doador>() {
                doador
            };

            var cadastrarDoadorCommand = new CadastrarDoadorCommand("teste", "teste@gmail.com", new DateTime(1997, 6, 1), "M", 65, "B", "positivo", "58328000");

            _mockDoadorRepository.Setup(pr => pr.GetByEmail(It.IsAny<string>())).ReturnsAsync(doador);


            //Act

            var command = new CadastrarDoadorCommandHandler(_mockUnitOfWork.Object, _mapper, _mockCepService.Object);

            // Assert
            await Assert.ThrowsAsync<DoadorJaExisteException>(async () => await command.Handle(cadastrarDoadorCommand, new CancellationToken()));
            _mockDoadorRepository.Verify(pr => pr.GetByEmail(It.IsAny<string>()), Times.Once);

        }


        [Fact]
        public async Task UpdateDoador_Executed_ReturnSuccess()
        {
            // Arrange

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

            var responseCep = new ResponseCepDto
            {
                Logradouro = "teste",
                Localidade = "cidade",
                Uf = "PB"
            };

            var atualizaDoadorCommand = new AtualizaDoadorCommand(0,"teste", "teste@gmail.com", new DateTime(1997, 6, 1), "M", 65, "B", "positivo", "58328000");

            _mockDoadorRepository.Setup(dr => dr.GetByIdIncludeEndereco(It.IsAny<int>())).ReturnsAsync(doador);
            _mockCepService.Setup(cs => cs.BuscaCep(It.IsAny<string>())).ReturnsAsync(responseCep);
            _mockDoadorRepository.Setup(pr => pr.AddAsync(It.IsAny<Doador>())).ReturnsAsync(doador);


            //Act

            var command = new AtualizaDoadorCommandHandler(_mockUnitOfWork.Object, _mockCepService.Object, _mapper);
            await command.Handle(atualizaDoadorCommand, new CancellationToken());

            // Assert

            _mockDoadorRepository.Verify(or => or.GetByIdIncludeEndereco(It.IsAny<int>()), Times.Once);
            _mockCepService.Verify(cs => cs.BuscaCep(It.IsAny<string>()), Times.Once());
            _mockDoadorRepository.Verify(or => or.UpdateAsync(It.IsAny<Doador>()), Times.Once);

        }

        [Fact]
        public async Task UpdateDoador_Executed_ThrowNotFoundException()
        {

            var atualizaDoadorCommand = new AtualizaDoadorCommand(0, "teste", "teste@gmail.com", new DateTime(1997, 6, 1), "M", 65, "B", "positivo", "58328000");

            //Act

            var command = new AtualizaDoadorCommandHandler(_mockUnitOfWork.Object, _mockCepService.Object, _mapper);

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await command.Handle(atualizaDoadorCommand, new CancellationToken()));

        }

    }
}
