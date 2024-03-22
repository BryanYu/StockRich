using Microsoft.EntityFrameworkCore;
using StockRich.Infrastructure.Data;
using StockRich.Infrastructure.Models;

namespace StockRich.API.Tests;

public class DbContextHelper
{
    public static StockRichContext CreateInMemoryStockRichDbContext()
    {
        var options =
            new DbContextOptionsBuilder<StockRichContext>().UseInMemoryDatabase(
                databaseName: Guid.NewGuid().ToString()).Options;

        var dbContext = new StockRichContext(options);
        
        
        
        var mocks = new List<CompanyInfo>
        {
            new CompanyInfo
            {
                Id = Guid.NewGuid(),
                StockCode = "1101",
                Name = "公司名稱1",
                Alias = "公司簡稱1",
                IndustryType = "01",
                ChairMan = "董事長1",
                GeneralManager = "總經理1",
                SpokesMan = "發言人1",
                ContactTelephone = "0200000000",
                BeginDate = DateOnly.FromDateTime(DateTime.Now),
                PublicDate = DateOnly.FromDateTime(DateTime.Now),
                Capital = 10000.00m,
                ContactEmail = "test@test.com",
                Website = "https://example.com",
                CreateDatetime = DateTime.Now,
                UpdateDatetime = DateTime.Now
            },
            new CompanyInfo
            {
                Id = Guid.NewGuid(),
                StockCode = "1102",
                Name = "公司名稱2",
                Alias = "公司簡稱2",
                IndustryType = "02",
                ChairMan = "董事長2",
                GeneralManager = "總經理2",
                SpokesMan = "發言人2",
                ContactTelephone = "0200000001",
                BeginDate = DateOnly.FromDateTime(DateTime.Now),
                PublicDate = DateOnly.FromDateTime(DateTime.Now),
                Capital = 20000.00m,
                ContactEmail = "test2@test2.com",
                Website = "https://example2.com",
                CreateDatetime = DateTime.Now,
                UpdateDatetime = DateTime.Now
            },
        };
        dbContext.CompanyInfos.AddRange(mocks);
        dbContext.SaveChanges();
        return dbContext;
    }
}