using AutoMapper;
using BancoDoacaoSangue.Application.Commands.CadastrarDoacao;
using BancoDoacaoSangue.Application.Mappers;
using BancoDoacaoSangue.Core.Entities;
using BancoDoacaoSangue.Core.Exceptions;
using BancoDoacaoSangue.Core.Repositories;
using Moq;
using Xunit;

namespace BancoDoacaoSangue.Tests.Commands
{
    public class DoacaoCommandTests
    {
        private Mock<IDoadorRepository> _mockDoadorRepository;
        private Mock<IDoacaoRepository> _mockDoacaoRepository;
        private Mock<IEstoqueSangueRepository> _mockEstoqueSangueRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IMapper _mapper;

        public DoacaoCommandTests()
        {
            _mockDoadorRepository = new Mock<IDoadorRepository>();
            _mockDoacaoRepository = new Mock<IDoacaoRepository>();
            _mockEstoqueSangueRepository = new Mock<IEstoqueSangueRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            

            _mockUnitOfWork.SetupGet(uow => uow.Doadores).Returns(_mockDoadorRepository.Object);
            _mockUnitOfWork.SetupGet(uow => uow.Doacoes).Returns(_mockDoacaoRepository.Object);
            _mockUnitOfWork.SetupGet(uow => uow.EstoqueSangue).Returns(_mockEstoqueSangueRepository.Object);
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new DoacaoMapper());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public async Task CreateDoacao_Executed_ReturnSuccess()
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

            var doacao = new Doacao
            {
                DoadorId = doador.Id,
                QuantidadeMl = 400,
                CriadoEm = new DateTime(2023,6,1)
            };
            var doacoes = new List<Doacao>()
            {
                doacao
            };

            var estoque = new EstoqueSangue {
                QuantidadeMl = 400,
                FatorRh = "positivo",
                TipoSanguineo = "B"
            };

            var cadastrarDoacaoCommand = new CadastrarDoacaoCommand(doador.Id, 420);


            
            _mockDoadorRepository.Setup(pr => pr.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(doador);
            _mockDoacaoRepository.Setup(pr => pr.GetLastDoacaoByDoador(It.IsAny<int>())).ReturnsAsync(doacoes);
            _mockDoacaoRepository.Setup(pr => pr.AddAsync(It.IsAny<Doacao>())).ReturnsAsync(doacao);
            _mockEstoqueSangueRepository.Setup(esr => esr.GetByTipoSanguineoFatorRh(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(estoque);
            _mockEstoqueSangueRepository.Setup(esr => esr.UpdateAsync(It.IsAny<EstoqueSangue>()));



            //Act

            var commandHandler = new CadastrarDoacaoCommandHandler(_mockUnitOfWork.Object, _mapper);
            var result = await commandHandler.Handle(cadastrarDoacaoCommand, new CancellationToken());

            // Assert
            _mockDoadorRepository.Verify(pr => pr.GetByIdAsync(It.IsAny<int>()), Times.Once);
            _mockDoacaoRepository.Verify(or => or.GetLastDoacaoByDoador(It.IsAny<int>()), Times.Once);
            _mockDoacaoRepository.Verify(or => or.AddAsync(It.IsAny<Doacao>()), Times.Once);
            _mockEstoqueSangueRepository.Verify(or => or.GetByTipoSanguineoFatorRh(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _mockEstoqueSangueRepository.Verify(or => or.UpdateAsync(It.IsAny<EstoqueSangue>()), Times.Once);

        }

        [Fact]
        public async Task CreateDoacao_Executed_When_EstoqueNaoExiste_ReturnSuccess()
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

            var doacao = new Doacao
            {
                DoadorId = doador.Id,
                QuantidadeMl = 420,
                CriadoEm = new DateTime(2023, 6, 1)
            };
            var doacoes = new List<Doacao>()
            {
                doacao
            };

            var estoque = new EstoqueSangue
            {
                QuantidadeMl = 420,
                FatorRh = "positivo",
                TipoSanguineo = "B"
            };

            var cadastrarDoacaoCommand = new CadastrarDoacaoCommand(doador.Id, 420);



            _mockDoadorRepository.Setup(pr => pr.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(doador);
            _mockDoacaoRepository.Setup(pr => pr.GetLastDoacaoByDoador(It.IsAny<int>())).ReturnsAsync(doacoes);
            _mockDoacaoRepository.Setup(pr => pr.AddAsync(It.IsAny<Doacao>())).ReturnsAsync(doacao);
            _mockEstoqueSangueRepository.Setup(esr => esr.AddAsync(It.IsAny<EstoqueSangue>())).ReturnsAsync(estoque);



            //Act

            var commandHandler = new CadastrarDoacaoCommandHandler(_mockUnitOfWork.Object, _mapper);
            var result = await commandHandler.Handle(cadastrarDoacaoCommand, new CancellationToken());

            // Assert
            Assert.Equal(doacao.QuantidadeMl, estoque.QuantidadeMl);
            _mockDoadorRepository.Verify(pr => pr.GetByIdAsync(It.IsAny<int>()), Times.Once);
            _mockDoacaoRepository.Verify(or => or.GetLastDoacaoByDoador(It.IsAny<int>()), Times.Once);
            _mockDoacaoRepository.Verify(or => or.AddAsync(It.IsAny<Doacao>()), Times.Once);
            _mockEstoqueSangueRepository.Verify(or => or.AddAsync(It.IsAny<EstoqueSangue>()), Times.Once);

        }

        [Fact]
        public async Task CreateDoador_Executed_ThrowDoadorNotFoundException()
        {
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


            var cadastrarDoacaoCommand = new CadastrarDoacaoCommand(doador.Id, 420);

            var commandHandler = new CadastrarDoacaoCommandHandler(_mockUnitOfWork.Object, _mapper);

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await commandHandler.Handle(cadastrarDoacaoCommand, new CancellationToken()));
            _mockDoadorRepository.Verify(pr => pr.GetByIdAsync(It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public async Task CreateDoador_ThrowValidationException_When_PesoMenorQueCiquenta()
        {
            var doador = new Doador
            {
                NomeCompleto = "teste",
                Email = "teste@gmail.com",
                DataNascimento = new DateTime(1997, 6, 1),
                Genero = "M",
                Peso = 45,
                TipoSanguineo = "B",
                FatorRh = "positivo"
            };

            var doacao = new Doacao
            {
                DoadorId = doador.Id,
                QuantidadeMl = 420,
                CriadoEm = new DateTime(2023, 6, 1)
            };
            var doacoes = new List<Doacao>()
            {
                doacao
            };

            _mockDoadorRepository.Setup(pr => pr.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(doador);
            _mockDoacaoRepository.Setup(pr => pr.GetLastDoacaoByDoador(It.IsAny<int>())).ReturnsAsync(doacoes);

            var cadastrarDoacaoCommand = new CadastrarDoacaoCommand(doador.Id, 420);

            var commandHandler = new CadastrarDoacaoCommandHandler(_mockUnitOfWork.Object, _mapper);

            // Assert
            await Assert.ThrowsAsync<DoadorValidationException>(async () => await commandHandler.Handle(cadastrarDoacaoCommand, new CancellationToken()));
            _mockDoadorRepository.Verify(pr => pr.GetByIdAsync(It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public async Task CreateDoador_ThrowValidationException_When_MenorDeIdade()
        {
            var doador = new Doador
            {
                NomeCompleto = "teste",
                Email = "teste@gmail.com",
                DataNascimento = DateTime.Now.AddYears(-17),
                Genero = "M",
                Peso = 60,
                TipoSanguineo = "B",
                FatorRh = "positivo"
            };

            var doacao = new Doacao
            {
                DoadorId = doador.Id,
                QuantidadeMl = 420,
                CriadoEm = new DateTime(2023, 6, 1)
            };
            var doacoes = new List<Doacao>()
            {
                doacao
            };

            _mockDoadorRepository.Setup(pr => pr.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(doador);
            _mockDoacaoRepository.Setup(pr => pr.GetLastDoacaoByDoador(It.IsAny<int>())).ReturnsAsync(doacoes);

            var cadastrarDoacaoCommand = new CadastrarDoacaoCommand(doador.Id, 420);

            var commandHandler = new CadastrarDoacaoCommandHandler(_mockUnitOfWork.Object, _mapper);

            // Assert
            await Assert.ThrowsAsync<DoadorValidationException>(async () => await commandHandler.Handle(cadastrarDoacaoCommand, new CancellationToken()));
            _mockDoadorRepository.Verify(pr => pr.GetByIdAsync(It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public async Task CreateDoador_ThrowValidationException_When_HomemDoaComMenosDeSessentaDias()
        {
            var doador = new Doador
            {
                NomeCompleto = "teste",
                Email = "teste@gmail.com",
                DataNascimento = new DateTime(1997, 6, 1),
                Genero = "M",
                Peso = 60,
                TipoSanguineo = "B",
                FatorRh = "positivo"
            };

            var doacao = new Doacao
            {
                DoadorId = doador.Id,
                QuantidadeMl = 420,
                CriadoEm = DateTime.Now.AddDays(-59),
            };
            var doacoes = new List<Doacao>()
            {
                doacao
            };

            _mockDoadorRepository.Setup(pr => pr.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(doador);
            _mockDoacaoRepository.Setup(pr => pr.GetLastDoacaoByDoador(It.IsAny<int>())).ReturnsAsync(doacoes);

            var cadastrarDoacaoCommand = new CadastrarDoacaoCommand(doador.Id, 420);

            var commandHandler = new CadastrarDoacaoCommandHandler(_mockUnitOfWork.Object, _mapper);

            // Assert
            await Assert.ThrowsAsync<DoadorValidationException>(async () => await commandHandler.Handle(cadastrarDoacaoCommand, new CancellationToken()));
            _mockDoadorRepository.Verify(pr => pr.GetByIdAsync(It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public async Task CreateDoador_ThrowValidationException_When_MulherDoaComMenosDeNoventaDias()
        {
            var doador = new Doador
            {
                NomeCompleto = "teste",
                Email = "teste@gmail.com",
                DataNascimento = new DateTime(1997, 6, 1),
                Genero = "F",
                Peso = 60,
                TipoSanguineo = "B",
                FatorRh = "positivo"
            };

            var doacao = new Doacao
            {
                DoadorId = doador.Id,
                QuantidadeMl = 420,
                CriadoEm = DateTime.Now.AddDays(-89),
            };
            var doacoes = new List<Doacao>()
            {
                doacao
            };

            _mockDoadorRepository.Setup(pr => pr.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(doador);
            _mockDoacaoRepository.Setup(pr => pr.GetLastDoacaoByDoador(It.IsAny<int>())).ReturnsAsync(doacoes);

            var cadastrarDoacaoCommand = new CadastrarDoacaoCommand(doador.Id, 420);

            var commandHandler = new CadastrarDoacaoCommandHandler(_mockUnitOfWork.Object, _mapper);

            // Assert
            await Assert.ThrowsAsync<DoadorValidationException>(async () => await commandHandler.Handle(cadastrarDoacaoCommand, new CancellationToken()));
            _mockDoadorRepository.Verify(pr => pr.GetByIdAsync(It.IsAny<int>()), Times.Once);

        }
    
    }
}
