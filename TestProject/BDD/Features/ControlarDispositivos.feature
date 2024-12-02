Feature: ControlarDispositivos
	Para controlar os Dispositivos da lanchonete
	Eu preciso das seguindes funcionalidades
	Adicionar um Dispositivo
	Alterar um Dispositivo
	Consultar um Dispositivo
	Deletar um Dispositivo

Scenario: Controlar Dispositivos
	Given Recebendo um Dispositivo na lanchonete
	And Adicionar o Dispositivo
	And Encontrar o Dispositivo
	And Alterar o Dispositivo
	When Consultar o Dispositivo
	Then posso deletar o Dispositivo