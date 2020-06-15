using FileConverter.Services.DataImport;
using FileConverter.Services.FileExport;
using Newtonsoft.Json;
using System;
using Xunit;

namespace FileConverter.Test
{
    public class FileImportFactoryShould
    {
        private readonly string testFile = @"Mocks\MockData.csv";

        [Fact]
        public void Convert_Csv_To_Json()
        {
            // Arrange
            var sut = FileImportFactory.FileImport(testFile);
            var model = sut.GetData();
            var expectedResult = JsonConvert.SerializeObject(model);

            // Act
            var fileExport = FileExportFactory.FileExport(Enums.Formats.JSON);
            var result = fileExport.Export(model);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Convert_Csv_To_Xml()
        {
            // Arrange
            //var sut = FileImportFactory.FileImport(testFile);
            //var model = sut.Process();
            //var expectedXml = JsonConvert.DeserializeXmlNode("{\"row\":" +
            //    JsonConvert.SerializeObject(model) + "}", "root");

            //// Act
            //var fileExport = FileExportFactory.FileExport(Enums.Formats.XML);
            //var result = fileExport.Export(model);

            //// Assert
            //Assert.Equal(expectedXml, result);
        }

        [Fact]
        public void Return_Instance_Of_CsvImport()
        {
            // Act
            var sut = FileImportFactory.FileImport(testFile);

            // Assert
            Assert.IsType<CsvImport>(sut);
        }

        [Theory]
        [InlineData(".abc")]
        [InlineData(".xls")]
        [InlineData(".xslt")]
        public void Throw_Error_If_Unknown_File_Format(string fileFormat)
        {
            // Act
            var exception = Assert.Throws<NotSupportedException>(
                () => FileImportFactory.FileImport($"Test{fileFormat}"));

            // Assert
            Assert.Equal($"Unsupported file format '{fileFormat}'", exception.Message);
        }
    }
}
