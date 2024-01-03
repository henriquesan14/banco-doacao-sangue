using AutoMapper;
using BancoDoacaoSangue.Application.Commands.CadastrarDoador;
using BancoDoacaoSangue.Application.Mappers;
using BancoDoacaoSangue.Core.DTOs;
using BancoDoacaoSangue.Core.Entities;
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
            _mockDoadorRepository.Setup(pr => pr.GetAsync(It.IsAny<Expression<Func<Doador, bool>>>())).ReturnsAsync(new List<Doador>());
            _mockDoadorRepository.Setup(pr => pr.AddAsync(It.IsAny<Doador>())).ReturnsAsync(doador);
            

            //Act

            var command = new CadastrarDoadorCommandHandler(_mockUnitOfWork.Object, _mapper, _mockCepService.Object);
            var result = await command.Handle(cadastrarDoadorCommand, new CancellationToken());

            // Assert
        
            _mockCepService.Verify(cs => cs.BuscaCep(It.IsAny<string>()), Times.Once());
            _mockDoadorRepository.Verify(pr => pr.GetAsync(It.IsAny<Expression<Func<Doador, bool>>>()), Times.Once);
            _mockDoadorRepository.Verify(or => or.AddAsync(It.IsAny<Doador>()), Times.Once);

        }
    }
}
