using Application.Results;
using ICSharpCode.SharpZipLib;
using Microsoft.AspNetCore.Http;
using NPOI.XSSF.UserModel;

namespace Application.Services.ExcelFileReaderService
{
    public class ExcelFileReader : IExcelFileReader
    {
        public const string MicrosoftExcelMimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public ExcelFileReader()
        {
        }
        public static bool IsExcelFile(string? contentType)
        {
            return contentType == MicrosoftExcelMimeType;
        }

        public Result<XSSFWorkbook> ReadFile(IFormFile formFile)
        {
            var contentType = formFile.ContentType;

            var excelBook = new XSSFWorkbook();

            if (IsExcelFile(contentType))
            {
                using (var file = formFile.OpenReadStream())
                {
                    try
                    {
                        excelBook = new XSSFWorkbook(file);
                    }

                    catch (SharpZipBaseException)
                    {
                        return CreateFailure(Error.BadRequest, "Файл поврежден");
                    }
                    catch (Exception ex)
                    {
                        return CreateFailure(Error.InternalServerError, ex.Message);
                    }
                }
            }
            else
            {
                return CreateFailure(Error.BadRequest, $"Неправильный тип файла - {formFile.ContentType}");
            }

            return Result<XSSFWorkbook>.CreateSuccess(excelBook);
        }

        private static Result<XSSFWorkbook> CreateFailure(Error error, string message)
        {
            return Result<XSSFWorkbook>.CreateFailure(error, message);
        }
    }
}
