using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Data.SqlClient;

namespace CarteleraCine
{
    public class Program
    {
        public static void Main()
        {
            FuncionRepositorio repo = new FuncionRepositorio();
            int opcion;
            do
            {
                Console.WriteLine();
                Console.WriteLine("1. Registrar función");
                Console.WriteLine("2. Listar funciones");
                Console.WriteLine("3. Buscar función por Id");
                Console.WriteLine("4. Actualizar precio");
                Console.WriteLine("5. Eliminar función");
                Console.WriteLine("6. Salir");
                Console.Write("Opción: ");
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    opcion = 0;
                }

                try
                {
                    switch (opcion)
                    {
                        case 1: Registrar(repo); break;
                        case 2: Listar(repo); break;
                        case 3: Buscar(repo); break;
                        case 4: ActualizarPrecio(repo); break;
                        case 5: Eliminar(repo); break;
                        case 6: break;
                        default: Console.WriteLine("Opción inválida."); break;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error de base de datos: " + ex.Message);
                }
            } while (opcion != 6);
        }

        private static bool LeerPrecio(out decimal precio)
        {
            string? texto = Console.ReadLine();
            return decimal.TryParse(texto, NumberStyles.Number, CultureInfo.InvariantCulture, out precio) && precio > 0;
        }

        private static bool LeerId(out int id)
        {
            Console.Write("Id: ");
            return int.TryParse(Console.ReadLine(), out id);
        }

        private static void Registrar(FuncionRepositorio repo)
        {
            Console.Write("Película: ");
            string pelicula = (Console.ReadLine() ?? "").Trim();
            Console.Write("Sala: ");
            string sala = (Console.ReadLine() ?? "").Trim();
            Console.Write("Precio: ");
            decimal precio;
            bool precioOk = LeerPrecio(out precio);

            if (pelicula == "" || sala == "" || !precioOk)
            {
                Console.WriteLine("Datos inválidos.");
                return;
            }
            repo.Insertar(new Funcion { Pelicula = pelicula, Sala = sala, Precio = precio });
            Console.WriteLine("Función registrada.");
        }

        private static void Listar(FuncionRepositorio repo)
        {
            List<Funcion> lista = repo.ListarTodos();
            if (lista.Count == 0)
            {
                Console.WriteLine("No hay funciones registradas.");
                return;
            }
            decimal total = 0;
            foreach (Funcion f in lista)
            {
                Console.WriteLine(f);
                total += f.Precio;
            }
            Console.WriteLine("TOTAL DE PRECIOS: Q " + total.ToString("F2", CultureInfo.InvariantCulture));
        }

        private static void Buscar(FuncionRepositorio repo)
        {
            int id;
            if (!LeerId(out id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }
            Funcion? f = repo.BuscarPorId(id);
            if (f == null)
            {
                Console.WriteLine("No existe ese Id.");
                return;
            }
            Console.WriteLine(f);
        }

        private static void ActualizarPrecio(FuncionRepositorio repo)
        {
            int id;
            if (!LeerId(out id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }
            Funcion? f = repo.BuscarPorId(id);
            if (f == null)
            {
                Console.WriteLine("No existe ese Id.");
                return;
            }
            Console.Write("Nuevo precio: ");
            decimal precio;
            if (!LeerPrecio(out precio))
            {
                Console.WriteLine("Precio inválido.");
                return;
            }
            f.Precio = precio;
            if (repo.Actualizar(f))
            {
                Console.WriteLine("Función actualizada.");
            }
            else
            {
                Console.WriteLine("No existe ese Id.");
            }
        }

        private static void Eliminar(FuncionRepositorio repo)
        {
            int id;
            if (!LeerId(out id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }
            if (repo.Eliminar(id))
            {
                Console.WriteLine("Función eliminada.");
            }
            else
            {
                Console.WriteLine("No existe ese Id.");
            }
        }
    }
}