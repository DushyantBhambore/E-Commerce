using App.Core.Dtos;
using App.Core.Interface;
using Domain.ResponseModel;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.Product.Command
{
    public class AddProductCommad : IRequest<JSonModel>
    {
        public ProdcutDto  prodcutDto { get; set; }
     
    }
    public class AddProductCommandHandller : IRequestHandler<AddProductCommad, JSonModel>
    {
        private readonly IAppDbContext _appDbContext;


        private readonly IFileService _fileservice;
        public AddProductCommandHandller(IAppDbContext appDbContext, IWebHostEnvironment environment,  IFileService fileservice)
        {
            _appDbContext = appDbContext;
            _fileservice = fileservice;
        }

        public async Task<JSonModel> Handle(AddProductCommad request, CancellationToken cancellationToken)
        {
           ;

            var imageFile = request.prodcutDto.ProductImage;

            var allowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            var filePath = await _fileservice.SaveFileAsync(imageFile, allowedFileExtensions);
            var fileUrl = $"https://localhost:7295/Uploads/{Path.GetFileName(filePath)}";


            string prodcutcode = $"PC_{request.prodcutDto.ProductName.ToUpper()[2]}{request.prodcutDto.Id.ToString().PadLeft(3, '1')}";

            var product = new Domain.Product
            {
                ProductName = request.prodcutDto.ProductName,
                ProductCode = prodcutcode,
                ProductImage = fileUrl,
                Category = request.prodcutDto.Category,
                Brand = request.prodcutDto.Brand,
                SellingPrice = request.prodcutDto.SellingPrice,
                PurchasePrice = request.prodcutDto.PurchasePrice,
                PurchaseDate = request.prodcutDto.PurchaseDate,
                Stock = request.prodcutDto.Stock
            };
            await _appDbContext.Set<Domain.Product>().AddAsync(product);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return new JSonModel(200, "Product Added Successfully", product);

        }
    }
}
