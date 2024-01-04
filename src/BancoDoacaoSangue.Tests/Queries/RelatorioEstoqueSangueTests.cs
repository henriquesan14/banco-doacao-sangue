﻿using BancoDoacaoSangue.Application.Queries.RelatorioEstoqueSangue;
using BancoDoacaoSangue.Core.DTOs;
using BancoDoacaoSangue.Core.Repositories;
using Moq;
using Xunit;

namespace BancoDoacaoSangue.Tests.Queries
{
    public class RelatorioEstoqueSangueTests
    {
        private readonly Mock<IEstoqueSangueRepository> _mockEstoqueSangueRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public RelatorioEstoqueSangueTests()
        {
            _mockEstoqueSangueRepository = new Mock<IEstoqueSangueRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mockUnitOfWork.SetupGet(uow => uow.EstoqueSangue).Returns(_mockEstoqueSangueRepository.Object);
        }

        [Fact]
        public async Task GetRelatorioEstoqueSangue_Executed_ReturnListView()
        {
            var listEstoqueSangue = new List<RelatorioEstoqueSangueDto>() {
                new RelatorioEstoqueSangueDto
                {
                    Quantidade = 2,
                    TipoSanguineo = "A"
                },
                new RelatorioEstoqueSangueDto
                {
                    Quantidade = 3,
                    TipoSanguineo = "B"
                },
                new RelatorioEstoqueSangueDto
                {
                    Quantidade = 10,
                    TipoSanguineo = "AB"
                },
                new RelatorioEstoqueSangueDto
                {
                    Quantidade = 1,
                    TipoSanguineo = "O"
                },
            };
            _mockEstoqueSangueRepository.Setup(dr => dr.GetByQuantidadePorTipoSanguineo()).ReturnsAsync(listEstoqueSangue);

            var query = new RelatorioEstoqueSangueQuery();
            var queryHandler = new RelatorioEstoqueSangueQueryHandler(_mockUnitOfWork.Object);
            var result = await queryHandler.Handle(query, new CancellationToken());

            Assert.NotNull(result);
            Assert.Equal(result.Count, listEstoqueSangue.Count);
            _mockEstoqueSangueRepository.Verify(or => or.GetByQuantidadePorTipoSanguineo(), Times.Once);
        }
    }
}
