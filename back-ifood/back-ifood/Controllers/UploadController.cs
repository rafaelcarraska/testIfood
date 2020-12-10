using back_ifood.Interface.IBusiness;
using ifood_back.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace back_ifood.Controllers
{
    [ApiController]
    public class UploadController : Controller
    {
        private readonly IProdutoBusiness _produtoBusiness;

        public UploadController(IProdutoBusiness produtoBusiness)
        {
            _produtoBusiness = produtoBusiness;
        }

        [HttpPost("/[controller]/[action]")]
        [DisableRequestSizeLimit]
        public ActionResult Anexo()
        {
            try
            {
                var file = Request.Form.Files[0];
                var fileName = string.Empty;
                var extensao = string.Empty;

                string folderName = "\\assets\\images\\produtos";
                string webRootPath = "..\\..\\front_ifood\\src";

                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                if (file.Length > 0)
                {
                    fileName = Path.GetFileNameWithoutExtension(file.FileName).Replace(" ", "") + DateTime.Now.ToString("yyyyMMddhhmmss") + Path.GetExtension(file.FileName);
                    extensao = Path.GetExtension(file.FileName);
                    string fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    _produtoBusiness.UpdateAnexo(file.Name, fileName);

                }

                Result result = new Result()
                {
                    erro = string.Empty,
                    content = file.Name
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                Result result = new Result()
                {
                    erro = "Falha ao realizar o upload." ,
                    content = string.Empty
                };
                return Json(result);
            }
        }
    }
}