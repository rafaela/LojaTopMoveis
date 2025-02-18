﻿using Loja.Model;
using LojaTopMoveis.Interface;
using LojaTopMoveis.Model;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Topmoveis.Data;
using Topmoveis.Model;

namespace LojaTopMoveis.Service
{
    public class PhotosService : IPhoto
    {
        private readonly string _filePath;
        private readonly LojaContext _context;

        public PhotosService(LojaContext context)
        {
            _context = context;
            //_filePath = "C:\\Users\\ACER\\Pictures\\topmoveis";

            _filePath = "wwwroot/images";
        }
        
        //lidando com base64
        public string Save(string image)
        {
            var fileExt = image.Substring(image.IndexOf("/") + 1, image.IndexOf(";") - image.IndexOf("/") - 1); //png jpg

            var base64Code = image.Substring(image.IndexOf(",") + 1);

            var imgbytes = Convert.FromBase64String(base64Code);

            var fileName = Guid.NewGuid().ToString() + "." + fileExt;

            using(var imageFile = new FileStream(_filePath + "/" + fileName, FileMode.Create))
            {
                imageFile.Write(imgbytes, 0, imgbytes.Length);
                imageFile.Flush();

            }

            return _filePath + "/" + fileName;
        }

        public async Task<bool> Create(List<Photo> photos)
        {
            try
            {
                if (photos != null && photos.Count > 0)
                {
                    var lista = photos.ToList();
                    foreach(var photo in lista) {
                        var image = _context.Photos.Where(a => a.ID == photo.ID).FirstOrDefault();
                        var bytes = Encoding.UTF8.GetBytes(photo.urlImage);
                        photo.urlImage = null;
                        photo.Imagem = bytes;
                        if (image == null)
                        {
                            _context.Photos.Add(photo);
                        }
                        else
                        {
                            image = photo;
                        }
                        
                    }

                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return false;
            }
            return true;

        }

        

        public bool Update(List<Photo> photos)
        {
            
            return false;

        }

        public async Task<ServiceResponse<Photo>> Remove(Guid id)
        {
            ServiceResponse<Photo> serviceResponse = new ServiceResponse<Photo>();
            try
            {
                var image = _context.Photos.Where(a => a.ID == id).FirstOrDefault();
                if (image == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Imagem não encontrada";
                    serviceResponse.Sucess = false;
                }
                else
                {
                    _context.Photos.Remove(image);

                    await _context.SaveChangesAsync();
                    serviceResponse.Message = "Imagem removida";
                    serviceResponse.Sucess = true;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Sucess = false;
            }

            return serviceResponse;

        }
    }

}
