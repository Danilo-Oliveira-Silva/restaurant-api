<h1 align="center">Restaurant API - C#</h1>

<table border=0>
<tr>
    <td><img src="https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white" /></td>
    <td><img src="https://img.shields.io/badge/MongoDB-%234ea94b.svg?style=for-the-badge&logo=mongodb&logoColor=white" /></td>
</tr>
</table>

API utilizando C# .NET com banco de dados MongoDB e testes de integração utilizando XUnit.

<h2>🛠 Tecnologias</h2>

- C#
- .NET Framework 6.0
- MongoDB
- XUnit
- Moq

<h2>Comandos de Execução</h2>

Rodar a aplicação no diretório `Restaurant.API`.

```shell
dotnet run
```

Rodar os testes no diretório `Restaurant.API.Test`.

```shell
dotnet test
```

<h2>Documentação da API</h2>


![POST](https://placehold.co/70x30/3dbf94/white/?text=POST&font=Montserrat) `/user`

Rota para cadastrar uma nova pessoa usuária.

Request:

<pre lang="json">
	{
		"Name": "Nome",
		"Email": "email@email.com",
		"Password": "suasenha"
	}
</pre>

Response:

<pre lang="json">
	{
		"token": "eyJhbGciOiJIUzU1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImRhbmlsby5zaWx2YUBiABRyeWJlLmNvbSIsIm5iZiI6MTY5NjYyNDA2OSwiZXhwIjoxNjk2NzEwNDY5LCJpYXQiOjE2OTY2MjQwNjl9.nLbBzFLMq87_OmGtWo9rs7lkbxj6J1osnSwEcMg"
	}
</pre>

![POST](https://placehold.co/70x30/3dbf94/white/?text=POST&font=Montserrat) `/user/login`

Rota para realizar o login de uma pessoa usuária.

Request:

<pre lang="json">
	{
		"Email": "email@email.com",
		"Password": "suasenha"
	}
</pre>

Response:

<pre lang="json">
	{
		"token": "eyJhbGciOiJIUzU1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImRhbmlsby5zaWx2YUBiABRyeWJlLmNvbSIsIm5iZiI6MTY5NjYyNDA2OSwiZXhwIjoxNjk2NzEwNDY5LCJpYXQiOjE2OTY2MjQwNjl9.nLbBzFLMq87_OmGtWo9rs7lkbxj6J1osnSwEcMg"
	}
</pre>

![POST](https://placehold.co/70x30/3dbf94/white/?text=POST&font=Montserrat) `/reservations`

Rota para inserir uma nova reserva. Necessário um token Bearer para identificar a pessoa usuária.

Request:

<pre lang="json">
{
	"Date": "2023-10-26",
	"GuestQuant": "3"
}
</pre>

Response:

<pre lang="json">
{
	"Date": "2023-10-26",
	"GuestQuant": "3",
    "User": "652449160e7de96d375e2854"
}
</pre>



![GET](https://placehold.co/70x30/3d76bf/white/?text=GET&font=Montserrat) `/reservations/{date}`

Rota para consultar as reservas de uma determinada data.

Response:

<pre lang="json">
[
    {
	    "Date": "2023-10-26",
	    "GuestQuant": "3",
        "User": "652449160e7de96d375e2854"
    }, /**--**/
]
</pre>

![DELETE](https://placehold.co/70x30/bf3d3d/white/?text=DELETE&font=Montserrat) `/reservations/{id}`

Rota para excluir uma reserva.

Response: 204 - No Content
