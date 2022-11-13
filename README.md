# ML-get-pair

## Idea general
Un programa de consola al cual se le suministra una lista de argumentos, los cuales busca en la tienda de [Mercado Libre](www.mercadolibre.com.ar). Y genera una lista de los elementos coincidientes de cada proveedor y los ordena.


## Implementación
- Proyecto hecho con .NET Framework 3.5
- Utiliza la libreria [Json.NET](https://www.newtonsoft.com/json)


## Modo de uso

```
ML Getpair.exe "Motorola G6" iphone
```
### Modificadores
/s - Genera una pausa al final del programa y espera un Enter.

/z - No muestra los proveedores con 0 productos compatibles.

## Logs

1. Busca los argumentos con WebClient 
2. Implementación de comandos para el control de la ejecución.
