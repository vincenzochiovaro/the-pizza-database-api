using AutoFixture;
using Moq;
using ThePizzaDatabaseAPI.Core.Contracts;
using ThePizzaDatabaseAPI.Core.Interfaces;
using ThePizzaDatabaseAPI.Core.Services;

namespace ThePizzaDatabaseAPI.Tests;

public class PresetDoughBuilderServiceTests
{
    private readonly Mock<IPresetRepository> _repository = new();
    private readonly Fixture _fixture = new Fixture();

    [Fact]
    public async Task GivenValidPresetAndConfig_WhenGettingDoughIngredients_ThenDtoIsReturned()
    {
        // Given
        var preset = "classic";
        var lang = "en";
        var doughBallCount = 2;
        var doughBallWeight = 250;

        _repository
            .Setup(r => r.GetByPresetAsync(preset))
            .ReturnsAsync((PresetData?)null);

        // When
        var sut = new PresetDoughBuilderService(_repository.Object);

        var result = await sut.GetDoughIngredients(preset, lang, doughBallCount, doughBallWeight);

        // Then
        Assert.NotNull(result);
        Assert.IsType<PresetDoughIngredients>(result);
    }

    [Fact]
    public async Task GivenValidPresetAndConfig_WhenGettingDoughIngredients_ThenRepositoryIsCalledOnce()
    {
        // Given
        var preset = "classic";
        var lang = "en";
        var doughBallCount = 2;
        var doughBallWeight = 250;

        _repository
            .Setup(r => r.GetByPresetAsync(preset))
            .ReturnsAsync((PresetData?)null);

        // When
        var sut = new PresetDoughBuilderService(_repository.Object);

        await sut.GetDoughIngredients(preset, lang, doughBallCount, doughBallWeight);

        // Then
        _repository.Verify(r => r.GetByPresetAsync(preset), Times.Once);
    }

    [Fact]
    public async Task GivenValidPresetAndConfig_WhenGettingDoughIngredients_ThenMockedValuesAreReturned()
    {
        // Given
        var preset = "classic";
        var lang = "en";
        var doughBallCount = 2;
        var doughBallWeight = 250;

        _repository
            .Setup(r => r.GetByPresetAsync(preset))
            .ReturnsAsync((PresetData?)null);

        // When
        var sut = new PresetDoughBuilderService(_repository.Object);

        var result = await sut.GetDoughIngredients(preset, lang, doughBallCount, doughBallWeight);

        // Then
        Assert.Equal(360, result.Water);
        Assert.Equal(600, result.Flour);
        Assert.Equal(12, result.Salt);
        Assert.Equal(3, result.Yeast);
    }
}