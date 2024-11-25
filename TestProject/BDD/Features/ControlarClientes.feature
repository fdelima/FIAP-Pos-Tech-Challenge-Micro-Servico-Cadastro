Feature: ControlarClientes
	Para controlar os Clientes da lanchonete
	Eu preciso das seguindes funcionalidades
	Adicionar um Cliente
	Alterar um Cliente
	Consultar um Cliente
	Deletar um Cliente

Scenario: Controlar Clientes
	Given Recebendo um Cliente na lanchonete
	And Adicionar o Cliente
	And Encontrar o Cliente
	And Alterar o Cliente
	When Consultar o Cliente
	Then posso deletar o Cliente