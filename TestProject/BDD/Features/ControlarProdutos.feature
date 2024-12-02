Feature: ControlarProdutos
	Para controlar os Produtos da lanchonete
	Eu preciso das seguindes funcionalidades
	Adicionar um Produto
	Alterar um Produto
	Consultar um Produto
	Deletar um Produto

Scenario: Controlar Produtos
	Given Recebendo um Produto na lanchonete
	And Adicionar o Produto
	And Encontrar o Produto
	And Alterar o Produto
	When Consultar o Produto
	Then posso deletar o Produto