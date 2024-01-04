using AutoMapper;
using BancoDoacaoSangue.Application.Mappers;
using BancoDoacaoSangue.Application.Queries.BuscarDoacoesPorDoador;
using BancoDoacaoSangue.Application.Queries.BuscarDoador;
using BancoDoacaoSangue.Core.Entities;
using BancoDoacaoSangue.Core.Repositories;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace BancoDoacaoSangue.Tests.Queries
{
    public class BuscarDoacoesPorDoadorTests
    {
        private readonly Mock<IDoacaoRepository> _mockDoacaoRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;

        public BuscarDoacoesPorDoadorTests()
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
        public async Task GetDoacoesPorDoador_Executed_ReturnListView()
        {
            var list = new List<Doacao>()
            {
                new Doacao
                {
                    DoadorId = 1,
                    CriadoEm = DateTime.Now,
                    QuantidadeMl = 430
                }
            };
            _mockDoacaoRepository.Setup(dr => dr.GetAsync(It.IsAny<Expression<Func<Doacao, bool>>>())).ReturnsAsync(list);

            var query = new BuscarDoacoesPorDoadorQuery(1);
            var queryHandler = new BuscarDoacoesPorDoadorQueryHandler(_mockUnitOfWork.Object, _mapper);
            var result = await queryHandler.Handle(query, new CancellationToken());

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(result[0].QuantidadeMl, 430);
            _mockDoacaoRepository.Verify(or => or.GetAsync(It.IsAny<Expression<Func<Doacao, bool>>>()), Times.Once);
        }
    }
}
