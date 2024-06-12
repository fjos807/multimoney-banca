<div align="center">
  <h3 align="center">multimoney-banca</h3>
  <p align="center">
    Implementación de la prueba técnica de MiBancaEnLinea
    <br />
  </p>
</div>

## Sobre la implementación

Para solicionar la prueba técnica propuesta se creó un proyecto en .Net 8 y C# que funciona como un API. Este proyecto le brinda a los usuarios la posibilida de consultar la información de sus cuentas, depositar dinero a sus cuentas, realizar retiros y ejecutar transferencias entre cuentas de la misma entidad bancaria.

De forma general, el proyecto se compone de las siguientes carpetas:
* Controllers
* Interfaces
* Middleware
* Modelos
* Repositorio
* Servicios

Lo anterior además de la carpeta Base.Datos (contiene los scripts de SQL utilizados para generar las tablas, procedimientos almacenados y funciones de la base de datos utilizada) y el proyecto de pruebas funcionales del código.

## Utilización y accesos

<b>**** Los accesos mostrados a continuación fueron desabilitados temporalmente por temas de costos. Si se desea se pueden habilitar de nuevo ****</b>

Para utilizar el proyecto se tiene disponible el siguiente enlace (publicado en AWS):
 ```sh
  http://multimoney-banca-api.us-east-1.elasticbeanstalk.com/swagger/index.html
  ```
Además, se brinda el acceso al servidor de base de datos utilizando los siguientes parámetros:
* URL
  ```sh
  db-multimoney-banca.cfge2u8cuipw.us-east-1.rds.amazonaws.com
  ```
* Usuario
  ```sh
  admin
  ```
* Contraseña
  ```sh
  3vbv3At7eMJq
  ```

## Datos de prueba

Con el objetivo de realizar pruebas sobre las funcionalidades del sistema se pueden utilizar los siguientes datos de prueba:

| Tipo Identificación | Identificación | Nombre         | Número de cuenta | Saldo |
|---------------------|----------------|----------------|------------------|------------|
| 1                   | 100010001      | Lucas Lorenzo  | 1                | 72876.7477 |
| 1                   | 100010002      | Juan Perez     | 2                | 48012.8227 |


## Funcionalidades

En esta sección se presentan las funcionalidades creadas en el proyecto.

### Consulta de cuenta

Mediante el consumo del endpoint `/api/v1/cuentas` se logra obtener la información sobre la cuenta. Para enviar la solicitud se utilizan los siguientes parámetros:

`HTTP Post application/json`

```json
  {
      "tipoIdentificacion": 1,
      "identificacion": "100010001",
      "numeroCuenta": 1
  }
```
Como respuesta, se obtiene la siguiente información:
```json
  {
    "operacionExitosa": true,
    "error": null,
    "mensaje": "Cuenta consultada correctamente",
    "datos": {
        "cuenta": {
            "idCuenta": 1,
            "numeroIBAN": "CR05000000000000000001",
            "moneda": "CRC",
            "saldoDisponible": 75864.7699,
            "saldoBloqueado": 0.0000
        },
        "movimientos": [
            {
                "nombre": "Crédito",
                "descripcion": "Depósito de dinero",
                "fecha": "2024-06-06T21:50:08.760",
                "monto": 1000.0000
            }
        ],
        "intereses": [
            {
                "fecha": "2024-06-06T06:05:00.987",
                "porcentajeInteres": 0.0000000,
                "montoInteres": 14.7699,
                "saldoAnterior": 89850.0000,
                "saldoSiguiente": 89864.7699
            }
        ]
    }
}
```

Cabe mencionar que se realizan validaciones para asegurar que la cuenta exista y que pertenezca al cliente, por lo que también se podría mostrar un mensaje de error.

### Realizar depósitos

Para realizar un depósito a la cuenta se debe utilizar el endpoint `/api/v1/cuentas/depositos`, enviando los siguientes parámetros:

`HTTP Post application/json`

```json
  {
      "tipoIdentificacion": 1,
      "identificacion": "100010001",
      "numeroCuenta": 1,
      "monto": "1000"
  }
```

Al realizar el consumo se obtiene la siguiente respuesta:

```json
  {
      "operacionExitosa": true,
      "error": null,
      "mensaje": "Deposito agregado correctamente",
      "datos": null
  }
```

Cabe mencionar que se realizan validaciones para asegurar que la cuenta exista y que pertenezca al cliente, por lo que también se podría mostrar un mensaje de error.

### Realizar retiros

Si el usuario desea realizar un retiro, se debe utilizar el endpoint `/api/v1/cuentas/retiros` y enviar los siguientes datos en la solicitud:

`HTTP Post application/json`

```json
  {
      "tipoIdentificacion": 1,
      "identificacion": "100010001",
      "numeroCuenta": 1,
      "monto": "1000"
  }
```

Como respuesta se obtendrá lo siguiente:

```json
  {
      "operacionExitosa": true,
      "error": null,
      "mensaje": "Retiro agregado correctamente",
      "datos": null
  }
```

Cabe mencionar que se realizan validaciones para asegurar que la cuenta exista y que pertenezca al cliente, por lo que también se podría mostrar un mensaje de error. Además, se valida que la cuenta tenga saldo suficiente para el retiro.

### Transferencias entre cuentas

La última funcionalidad le permite a los usuarios realizar transferencias entre cuentas, ejecutando un consumo del endpoint `/api/v1/cuentas/transferencias` con la siguiente información:

`HTTP Post application/json`

```json
  {
      "tipoIdentificacion": 1,
      "identificacion": "100010001",
      "numeroCuentaOrigen": 1,
      "numeroCuentaDestino": 2,
      "monto": "500000"
  }
```

Como respuesta, se obtiene lo siguiente:

```json
  {
      "operacionExitosa": true,
      "error": null,
      "mensaje": "Transferencia realizada correctamente",
      "datos": null
  }
```

Cabe mencionar que se realizan validaciones para asegurar que la cuenta exista y que pertenezca al cliente, por lo que también se podría mostrar un mensaje de error. Además, se valida que la cuenta de origen tenga saldo suficiente para la transferencia.

## Diseño de la base de datos

Para el desarrollo de esta solución se creó el diseño de base de datos presente en la siguiente imagen.

![Multimoney-BD drawio](https://github.com/fjos807/multimoney-banca/assets/31530117/27216a51-34a7-480c-ada1-7e6a426e4627)

## Seguridad

Con respecto a los temas de seguridad, el uso de procedimientos almacenados permite mitigar el riesgo de SQL Injection.

Además, se creó el middleware presente en este archivo [AntiXssMiddleware.cs](https://github.com/fjos807/multimoney-banca/blob/dev/Multimoney.Banca.Api/Middleware/AntiXssMiddleware.cs) para limpiar el contenido ingresado en las solicitudes y prevenir el Cross Site Scripting.

## Bonus! Cálculo del interés diario

La funcionalidad del cálculo de interés diario con la creación de un job dentro de SQL Server. 

Este job realiza la ejecución del procedimiento [PROC_CALCULAR_INTERESES_DIARIOS](https://github.com/fjos807/multimoney-banca/blob/dev/Base.Datos/Procedimientos/crea_sp_calcular_intereses_diarios.sql), mismo que se encarga de recorrer todas las cuentas existentes y calcular el interés diario con el apoyo de la función [calcularMontoInteresDiario](https://github.com/fjos807/multimoney-banca/blob/dev/Base.Datos/Funciones/crea_funcion_calcular_interes_diario.sql). Como parte de su ejecución, se insertan los registros correspondientes en la tabla Historico_Calculo_Interes.

![image](https://github.com/fjos807/multimoney-banca/assets/31530117/2a6666dd-7677-4145-998e-bf518426c09c)

Además, este job está programado para que se ejecute de forma diaria.

![image](https://github.com/fjos807/multimoney-banca/assets/31530117/2aab083e-0999-4ece-9f8c-967293e9c779)

## Bonus! Pruebas funcionales

Como último apartado de esta documentaciónse menciona que se crearon las pruebas para cada una de las funcionalidades del proyecto. Para cada una de estas se tiene un escenario positivo y otro negativo.

Estas pruebas se crearon utilizando la librería `xUnit` (entre otras) y están presentes en el proyecto `Multimoney.Banca.Api.Tests`.

Además, se evidencia que el proyecto aprueba todas las pruebas de forma exitosa.

![image](https://github.com/fjos807/multimoney-banca/assets/31530117/ebb479fd-1a6d-4120-889a-767df2285ce4)

Muchas gracias.
