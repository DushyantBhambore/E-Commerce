using App.Core.Dtos;
using App.Core.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Core.Apps.Product.Command
{
    public class UpdateProductCommand : IRequest<string>
    {
        public ProdcutDto  prodcutDto { get; set; }
    }

    public class UpdateProductCommandHandller : IRequestHandler<UpdateProductCommand,string>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IFileService _fileservice;

        public UpdateProductCommandHandller(IAppDbContext appDbContext, IFileService fileservice)
        {
            _appDbContext = appDbContext;
            _fileservice = fileservice;
        }

        public async Task<string> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var findid = await _appDbContext.Set<Domain.Product>().
                FirstOrDefaultAsync(a => a.ProdcutId == request.prodcutDto.ProdcutId);


            if (findid == null)
            {
                return JsonSerializer.Serialize(new { message = "Prodct is not Found" });
            }
            var imageFile = request.prodcutDto.ProductImage;
            var allowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            var filePath = await _fileservice.SaveFileAsync(imageFile, allowedFileExtensions);
            var fileUrl = $"https://localhost:7295/Uploads/{Path.GetFileName(filePath)}";
            findid.ProductName = request.prodcutDto.ProductName;
            findid.ProductImage = fileUrl;
            findid.PurchasePrice= request.prodcutDto.PurchasePrice;
            findid.SellingPrice=request.prodcutDto.SellingPrice;
            findid.Stock= request.prodcutDto.Stock;
            findid.Brand= request.prodcutDto.Brand;
            findid.Category= request.prodcutDto.Category;
            findid.UpdateBy = "Admin";
            findid.UpdatedOn= DateTime.Now;
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return JsonSerializer.Serialize(new { message = "Product Update SuccessFully" });
        }
    }
}
