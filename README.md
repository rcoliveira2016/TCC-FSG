# TCC-FSG

## instalação sql server 

	https://docs.microsoft.com/pt-br/sql/linux/quickstart-install-connect-docker?view=sql-server-ver15&pivots=cs1-powershell
	CREATE DATABASE TCC
	
	
### consultar sql
	docker exec -it sql1 "bash"
	/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "<YourNewStrong@Passw0rd>"

## Serviço de busca: Sonic
	link do repositorio do projeto: https://github.com/valeriansaliou/sonic;
	link do video que pode ajudar: https://www.youtube.com/watch?v=rNCGwggC1RI

### Commando doker usados
		docker pull valeriansaliou/sonic:v1.3.0
		docker run -p 1491:1491 -v <arquivo de configuracao(D:\Ramon\Dev\configs\sonic\config.cfg)>:/etc/sonic.cfg -v <pasta de presistencia de dados(D:\Ramon\Dev\configs\sonic\store\)>:/var/lib/sonic/store/ valeriansaliou/sonic:v1.3.0
	obs: o arquivo de configuração possui coisa a mais olhar no video ou documentação



## depdencia dee arquitetura
- unit of work e generalização repositorio
