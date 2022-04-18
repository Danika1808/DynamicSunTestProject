using Application.Results;
using Microsoft.AspNetCore.Http;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ExcelFileReaderService
{
    public interface IExcelFileReader
    {
        Result<XSSFWorkbook> ReadFile(IFormFile formFile);
    }
}
