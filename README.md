# testIfood
Sistema referente ao teste da vaga developer full Stack

dados para acesso.
	Username “11234567890”,
	Password: null

Front
	Angular => dependencies => @angular/cdk": "^6.0.0",
	port default => 4200

	run =>
		npm i
		npm start

back
	c# => .Net core 5.0 + EntityFrameworkCore
	port default => 5000

	run =>
		dotnet run

serviço dependencia 
	endPoint => https://dev.sitemercado.com.br/api/login

DataBase
	SqlServer 2017+ => catalog: ifood user: ifood password: !f00d

	run => 
		exec scriptBanco.sql
