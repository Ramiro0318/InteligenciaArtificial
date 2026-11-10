
using System;
using System.Collections.Generic;
using chen0040.ExpertSystem;

namespace SistemaExpertoPlantas
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Sistema Experto de Clasificacion de Plantas";

            RuleInferenceEngine motor = new RuleInferenceEngine();

            // Cargar la base de conocimiento desde una clase independiente.
            BaseConocimiento.CargarReglas(motor);

            Console.WriteLine("==========================================");
            Console.WriteLine(" SISTEMA EXPERTO DE CLASIFICACION DE PLANTAS");
            Console.WriteLine("==========================================");
            Console.WriteLine();

            Console.WriteLine("1. Cactus");
            Console.WriteLine("2. Helecho");
            Console.WriteLine("3. Arbol");
            Console.WriteLine("4. Arbusto");
            Console.WriteLine("5. Evaluacion interactiva");
            Console.WriteLine();

            int opcion = LeerEntero("Opcion: ", 1, 5);

            switch (opcion)
            {
                case 1:
                    CargarCactus(motor);
                    break;

                case 2:
                    CargarHelecho(motor);
                    break;

                case 3:
                    CargarArbol(motor);
                    break;

                case 4:
                    CargarArbusto(motor);
                    break;

                case 5:
                    Console.WriteLine();
                    Console.WriteLine(
                        "El motor preguntara por las condiciones que necesite."
                    );
                    break;
            }

            // =====================================================
            // MEMORIA INICIAL
            // =====================================================

            Console.WriteLine();
            Console.WriteLine("=== MEMORIA INICIAL DE HECHOS ===");
            MostrarMemoria(motor);

            // =====================================================
            // ENCADENAMIENTO HACIA ADELANTE
            // =====================================================

            Console.WriteLine();
            Console.WriteLine("=== ENCADENAMIENTO HACIA ADELANTE ===");

            motor.Infer();

            Console.WriteLine();
            Console.WriteLine("Hechos despues de la inferencia:");
            MostrarMemoria(motor);

            // =====================================================
            // ENCADENAMIENTO HACIA ATRAS
            // =====================================================

            Console.WriteLine();
            Console.WriteLine("=== ENCADENAMIENTO HACIA ATRAS ===");
            Console.WriteLine("Objetivo: clasificacionFinal");

            Clause conclusion = null;

            while (true)
            {
                List<Clause> condicionesPendientes =
                    new List<Clause>();

                conclusion = motor.Infer(
                    "clasificacionFinal",
                    condicionesPendientes
                );

                if (conclusion != null)
                {
                    break;
                }

                if (condicionesPendientes.Count == 0)
                {
                    break;
                }

                Clause condicion = condicionesPendientes[0];

                string variable = condicion.Variable;

                Console.WriteLine();
                Console.WriteLine(
                    "Se necesita informacion adicional."
                );

                string valor = PreguntarValor(variable);

                motor.AddFact(
                    new IsClause(variable, valor)
                );

                Console.WriteLine(
                    $"Hecho agregado: {variable} = {valor}"
                );

                motor.Infer();
            }

            // =====================================================
            // RESULTADO
            // =====================================================

            Console.WriteLine();
            Console.WriteLine("=== RESULTADO ===");

            if (conclusion != null && conclusion.Variable == "clasificacionFinal")
            {
                Console.WriteLine(
                    $"Clasificacion obtenida: {conclusion.Value}"
                );
            }
            else
            {
                Console.WriteLine(
                    "No se obtuvo una clasificacion final."
                );
            }

            // =====================================================
            // MEMORIA FINAL
            // =====================================================

            Console.WriteLine();
            Console.WriteLine("=== MEMORIA FINAL DE HECHOS ===");

            MostrarMemoria(motor);

            Console.WriteLine();
            Console.WriteLine(
                "Presiona una tecla para salir."
            );
            Console.ReadKey();

            // --------------------------------------------------
            // ESCENARIOS PREDEFINIDOS
            // --------------------------------------------------

            static void CargarCactus(RuleInferenceEngine motor)
            {
                motor.AddFact(new IsClause("tieneEspinas", "si"));
                motor.AddFact(new IsClause("ambiente", "seco"));
                motor.AddFact(new IsClause("tieneFlores", "no"));
                motor.AddFact(new IsClause("tipoTallo", "lenoso"));
                motor.AddFact(new IsClause("tipoHoja", "escamosa"));
                motor.AddFact(new IsClause("altura", "baja"));
                motor.AddFact(new IsClause("tieneFrutos", "no"));
                motor.AddFact(new IsClause("conservaHojas", "todo_el_ano"));
                motor.AddFact(new IsClause("tipoRaiz", "pivotante"));
                motor.AddFact(new IsClause("reproduccionEsporas", "no"));
            }

            static void CargarHelecho(RuleInferenceEngine motor)
            {
                motor.AddFact(new IsClause("tieneEspinas", "no"));
                motor.AddFact(new IsClause("ambiente", "humedo"));
                motor.AddFact(new IsClause("tieneFlores", "no"));
                motor.AddFact(new IsClause("tipoTallo", "herbaceo"));
                motor.AddFact(new IsClause("tipoHoja", "frondosa"));
                motor.AddFact(new IsClause("altura", "baja"));
                motor.AddFact(new IsClause("tieneFrutos", "no"));
                motor.AddFact(new IsClause("conservaHojas", "pierde_en_cierta_epoca"));
                motor.AddFact(new IsClause("tipoRaiz", "fibrosa"));
                motor.AddFact(new IsClause("reproduccionEsporas", "si"));
            }

            static void CargarArbol(RuleInferenceEngine motor)
            {
                motor.AddFact(new IsClause("tieneEspinas", "no"));
                motor.AddFact(new IsClause("ambiente", "templado"));
                motor.AddFact(new IsClause("tieneFlores", "si"));
                motor.AddFact(new IsClause("tipoTallo", "lenoso"));
                motor.AddFact(new IsClause("tipoHoja", "ancha"));
                motor.AddFact(new IsClause("altura", "alta"));
                motor.AddFact(new IsClause("tieneFrutos", "si"));
                motor.AddFact(new IsClause("conservaHojas", "todo_el_ano"));
                motor.AddFact(new IsClause("tipoRaiz", "pivotante"));
                motor.AddFact(new IsClause("reproduccionEsporas", "no"));
            }

            static void CargarArbusto(RuleInferenceEngine motor)
            {
                motor.AddFact(new IsClause("tieneEspinas", "no"));
                motor.AddFact(new IsClause("ambiente", "templado"));
                motor.AddFact(new IsClause("tieneFlores", "si"));
                motor.AddFact(new IsClause("tipoTallo", "lenoso"));
                motor.AddFact(new IsClause("tipoHoja", "ancha"));
                motor.AddFact(new IsClause("altura", "media"));
                motor.AddFact(new IsClause("tieneFrutos", "si"));
                motor.AddFact(new IsClause("conservaHojas", "todo_el_ano"));
                motor.AddFact(new IsClause("tipoRaiz", "pivotante"));
                motor.AddFact(new IsClause("reproduccionEsporas", "no"));
            }

            // --------------------------------------------------
            // PREGUNTAS PARA OBTENER HECHOS FALTANTES
            // --------------------------------------------------

            static string PreguntarValor(string variable)
            {
                switch (variable)
                {
                    case "tieneEspinas":
                        return LeerSiNo(
                            "La planta tiene espinas?"
                        );

                    case "tieneFlores":
                        return LeerSiNo(
                            "La planta tiene flores?"
                        );

                    case "tieneFrutos":
                        return LeerSiNo(
                            "La planta tiene frutos?"
                        );

                    case "reproduccionEsporas":
                        return LeerSiNo(
                            "La planta se reproduce por esporas?"
                        );

                    case "tipoTallo":
                        return LeerOpcion(
                            "Tipo de tallo",
                            "lenoso",
                            "herbaceo"
                        );

                    case "tipoHoja":
                        return LeerOpcion(
                            "Tipo de hoja",
                            "ancha",
                            "aguja",
                            "frondosa",
                            "escamosa"
                        );

                    case "ambiente":
                        return LeerOpcion(
                            "Ambiente",
                            "seco",
                            "humedo",
                            "templado"
                        );

                    case "altura":
                        return LeerOpcion(
                            "Altura",
                            "baja",
                            "media",
                            "alta"
                        );

                    case "conservaHojas":
                        return LeerOpcion(
                            "Conservacion de hojas",
                            "todo_el_ano",
                            "pierde_en_cierta_epoca"
                        );

                    case "tipoRaiz":
                        return LeerOpcion(
                            "Tipo de raiz",
                            "fibrosa",
                            "pivotante",
                            "fasciculada"
                        );

                    default:

                        Console.WriteLine(
                            $"Variable desconocida: {variable}"
                        );

                        return "no";
                }
            }

            // --------------------------------------------------
            // UTILIDADES DE ENTRADA Y SALIDA
            // --------------------------------------------------

            static string LeerSiNo(string pregunta)
            {
                while (true)
                {
                    Console.Write(pregunta + " (si/no): ");

                    string respuesta = (Console.ReadLine() ?? "")
                        .Trim()
                        .ToLowerInvariant();

                    if (respuesta == "si" || respuesta == "sí")
                        return "si";

                    if (respuesta == "no")
                        return "no";

                    Console.WriteLine("Respuesta no valida.");
                }
            }

            static string LeerOpcion(
                string pregunta,
                params string[] opciones)
            {
                while (true)
                {
                    Console.WriteLine(pregunta + ":");

                    for (int i = 0; i < opciones.Length; i++)
                    {
                        Console.WriteLine(
                            $"{i + 1}. {opciones[i].Replace('_', ' ')}"
                        );
                    }

                    int seleccion = LeerEntero(
                        "Seleccion: ", 1, opciones.Length
                    );

                    return opciones[seleccion - 1];
                }
            }

            static int LeerEntero(
                string pregunta,
                int minimo,
                int maximo)
            {
                while (true)
                {
                    Console.Write(pregunta);

                    if (int.TryParse(
                        Console.ReadLine(),
                        out int numero) &&
                        numero >= minimo &&
                        numero <= maximo)
                    {
                        return numero;
                    }

                    Console.WriteLine(
                        $"Ingresa un entero entre {minimo} y {maximo}."
                    );
                }
            }

            static void MostrarMemoria(RuleInferenceEngine motor)
            {
                Console.WriteLine(motor.Facts);
            }
        }
    }
}
