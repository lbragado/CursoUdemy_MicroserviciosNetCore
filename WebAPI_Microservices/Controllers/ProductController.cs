using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_Microservices.Controllers
{
    [ApiController]
    [Route("products")] //como las mejores prácticas nos dicen que deben ser en plural por eso le sobreescribimos como products
    public class ProductController: ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            //return Ok(Products);  //Como estamos implementando ApiController podemos dejar de usar el OK y devolverá el mismo resultado
            return Products;
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            return Products.Single(x => x.Id == id);
        }

        [HttpPost]
        public ActionResult Create(Product model)
        {
            model.Id = Products.Count() + 1;
            Products.Add(model);

            //CreatedAtAction permite crear una respuesta 201 - request was successful and a resources has been created
            //navegará hacia el método GET, es decir, una vez creado mostrará el objeto creado porque utilizaremos
            //el verbo GET
            return CreatedAtAction(
                "Get",
                new { id = model.Id },  //Objeto anónimo al que le pasaremos el ID del nuevo elemento
                model
                );
        }

        [HttpPut("{productId}")]
        public ActionResult Update(int productId, Product model)
        {
            var originalEntry = Products.Single(x => x.Id == productId);

            originalEntry.Name = model.Name;
            originalEntry.Price = model.Price;
            originalEntry.Description = model.Description;

            //Respuesta 204 - request has successed but that the client doesn't need to navigate away from its current page
            return NoContent();
        }


        [HttpDelete("{productId}")]
        public ActionResult Delete(int productId)
        {
            Products = Products.Where(x => x.Id != productId).ToList();

            //Respuesta 204 - request has successed but that the client doesn't need to navigate away from its current page
            return NoContent();
        }

        private static List<Product> Products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name ="Guitarra eléctrica",
                Price=1200,
                Description = "Ideal para tocar jazz, blues, rock clásico y afines."
            },
            new Product
            {
                Id = 2,
                Name = "Amplificador para guitarra eléctrica",
                Price= 1200,
                Description = "Excelente amplificador con un sonido cálido"
            }
        };
    }
}
