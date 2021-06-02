# TCC-FSG

## instalaçã sql server 

	https://docs.microsoft.com/pt-br/sql/linux/quickstart-install-connect-docker?view=sql-server-ver15&pivots=cs1-powershell
	CREATE DATABASE TCC
	
	
### consultar sql
	docker exec -it sql1 "bash"
	/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "<YourNewStrong@Passw0rd>"
	

## depdencia dee arquitetura
- unit of work e generalização repositorio
- migrations
