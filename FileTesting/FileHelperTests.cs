using Microsoft.AspNetCore.Http;
using Moq;
using Spedito.Helpers;
namespace FileTesting
{
    public class FileHelperTests
    {


        [Fact]
        public void MethodIsImage_ShouldReturnTrue_WhenTypeIsImage()
        {
            // A => Arrange
            var mockData = new Mock<IFormFile>();
            mockData.Setup(md => md.ContentType).Returns("image/");

            // A => Act
            bool res = FileHelper.IsImage(mockData.Object);

            // A => Assert
            Assert.True(res);
        }
        [Theory]
        [InlineData(1 * 1024 * 1024, 2, true)]
        [InlineData(3 * 1024 * 1024, 2, true)]
        public void MethodHasValidSize_ShouldReturnTrue_WhenFileSizeCorrect(int size, int limit, bool expectedRes)
        {
            var mockData = new Mock<IFormFile>();
            mockData.Setup(md => md.Length).Returns(size);

            bool res = FileHelper.HasValidSize(mockData.Object, limit);

            Assert.Equal(expectedRes, res);
        }
    }
}