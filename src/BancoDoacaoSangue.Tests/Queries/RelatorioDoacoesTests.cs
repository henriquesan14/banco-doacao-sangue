using AutoMapper;
using BancoDoacaoSangue.Application.Mappers;
using BancoDoacaoSangue.Application.Queries.RelatorioDoacoes;
using BancoDoacaoSangue.Core.Entities;
using BancoDoacaoSangue.Core.Repositories;
using Moq;
using Xunit;

namespace BancoDoacaoSangue.Tests.Queries
{
    public class RelatorioDoacoesTests
    {
        private readonly Mock<IDoacaoRepository> _mockDoacaoRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;

        public RelatorioDoacoesTests()
        {
            _mockDoacaoRepository = new Mock<IDoacaoRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUnitOfWork.SetupGet(uow => uow.Doacoes).Returns(_mockDoacaoRepository.Object);
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
        public async Task GetRelatorioDoacoes_Executed_ReturnListView()
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
            var list = new List<Doacao>()
            {
                new Doacao
                {
                    DoadorId = 1,
                    CriadoEm = DateTime.Now,
                    QuantidadeMl = 430,
                    Doador = doador
                }
            };
            _mockDoacaoRepository.Setup(dr => dr.GetRelatorioDoacoes()).ReturnsAsync(list);

            var query = new RelatorioDoacoesQuery();
            var queryHandler = new RelatorioDoacoesQueryHandler(_mockUnitOfWork.Object, _mapper);
            var result = await queryHandler.Handle(query, new CancellationToken());

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(result[0].Doador!.NomeCompleto, doador.NomeCompleto);
            Assert.Equal(result[0].Doador!.TipoSanguineo, doador.TipoSanguineo);
            _mockDoacaoRepository.Verify(or => or.GetRelatorioDoacoes(), Times.Once);
        }
    }
}
