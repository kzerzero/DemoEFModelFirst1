using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoADatos;

namespace PruebaConsola
{
    class Program
    {
        //DEMO INTERACCION DE ENTITY FRAMEWORK CON ENFOQUE MODEL FIRST.
        static void Main(string[] args)
        {

            NorthWindDBModelContainer context = new NorthWindDBModelContainer();
            //Se crea una categoria bebida
            Categoria Bebidas = new Categoria
            {
                Nombre = "Bebidas embotelladas"
            };
            //Cuenta cuantas categorias tengo en el contexto
            Console.WriteLine("categorias antes de agregar al contexto: {0}", context.Categorias.Count());
            //almacena Categoria al contexto
            context.Categorias.Add(Bebidas);
            Console.WriteLine("Categorias antes de guardar a la base de datos:{0}", context.Categorias.Count());
            //Guarda categoría almacenada del contexto en la base de datos
            context.SaveChanges();
            Console.WriteLine("Categorias después de agregar a la base de datos: {0}", context.Categorias.Count());
            //Se captura Id de la categoria almacenada
            int IdBebidas = Bebidas.Id;

            Categoria lacteos = new Categoria();
            lacteos.Nombre="lacteos y derivados";
            context.Categorias.Add(lacteos);
            context.SaveChanges();

            Console.WriteLine("categorias despues de agregar a la base de datos: {0}", context.Categorias.Count());

            //Se Agrega nuevo producto con clave foreana a partir de valor almacenado anteriormente.
            Producto Naranjada = new Producto
            {
                Nombre = "Naranjada embotellada",
                Precio = 8,
                UnidadesEnExistencia = 200,
                CategoriaId = IdBebidas //Se instancia el IdBebida almacenada anteriormente.
            };

            context.Productos.Add(Naranjada);
          //  context.Productos.addObject(Naranjada);
            context.SaveChanges();

            //Se Agrega nuevo producto con clave foreana a patir de la entidad Categoria en contexto
            Producto Queso = new Producto
            {
                Nombre = "Queso de cabra",
                Precio = 10,
                UnidadesEnExistencia = 30,
                Categoria = lacteos
            };
            //context.Productos.Add(Queso);  Como ya existe el objeto lacteos en contexto, no es necesario atachar en contexto.
            context.Productos.Add(Queso);
            
            Producto LecheDescremada = new Producto
            {
                Nombre = "Leche descremada la Pastora",
                Precio = 12,
                UnidadesEnExistencia = 500,
                Categoria = lacteos
            };
            context.Productos.Add(LecheDescremada);
            

            Producto LecheEntera = new Producto
            {
                Nombre = "Leche Entera Soprole",
                Precio = 14,
                UnidadesEnExistencia = 300,
                Categoria = lacteos
            };
            context.Productos.Add(LecheEntera);
            context.SaveChanges();
            Producto QuesoBlanco = new Producto
            {
                Nombre = "Queso blanco Colun",
                Precio = 14,
                UnidadesEnExistencia = 300,
                Categoria = lacteos
            };
            context.Productos.Add(QuesoBlanco);
            context.SaveChanges();
            //Se modifica el Nombre del Producto, operación UPDATE 
            LecheEntera.Nombre = "Leche Entera LEOLAO";
            context.SaveChanges();
            //
  //          context.Productos.Detach(Queso);

           Queso.Nombre = "Nuevo queso de cabra";

  //          Producto modificado = new Producto
  //          {
  //              Id = Queso.Id
  //          };

     //       context.Productos.Attach(modificado);
      //      context.Productos.(Queso);
            context.SaveChanges();
            //Se busca por Id el nombre de la categoria
            Categoria ce = context.Categorias.FirstOrDefault(c => c.Id == IdBebidas);
            Console.WriteLine("categoria encontrada {0}", ce.Nombre);
            //Se buscar por Nombre una categoria
            ce = context.Categorias.FirstOrDefault(c => c.Nombre.Contains("Lacteos"));
            Console.WriteLine("Productos de {0}", ce.Nombre);
            //Busca todos los productos que son categoria Lacteos
            foreach (Producto p in ce.Productos)
            {
                Console.WriteLine("\t{0}\t{1}", p.Id,p.Nombre);
            }
            //Elimina el Producto QuesoBlanco.
            context.Productos.Remove(QuesoBlanco);
            context.SaveChanges();
            Console.WriteLine("Presione Enter para continuar");
            Console.ReadLine();
        }
    }
}
