help: ## help dialog
	@IFS=$$'\n' ; \
	help_lines=(`fgrep -h "##" $(MAKEFILE_LIST) | fgrep -v fgrep | sed -e 's/\\$$//'`); \
	for help_line in $${help_lines[@]}; do \
		IFS=$$'#' ; \
		help_split=($$help_line) ; \
		help_command=`echo $${help_split[0]} | sed -e 's/^ *//' -e 's/ *$$//'` ; \
		help_info=`echo $${help_split[2]} | sed -e 's/^ *//' -e 's/ *$$//'` ; \
		printf "%-30s %s\n" $$help_command $$help_info ; \
	done

api: ## build docker container of api
	docker build -t emsisoft-test-api:1.0.0 -f ./EmsisoftTest.Api/Dockerfile .
	docker tag emsisoft-test-api:1.0.0 emsisoft-test-api:latest

processor: ## build docker container of processor
	docker build -t emsisoft-test-processor:1.0.0 -f ./EmsisoftTest.Processor/Dockerfile .
	docker tag emsisoft-test-processor:1.0.0 emsisoft-test-processor:latest

run-database: ## run database separately (for local debugging)
	docker run --name emsisoft-test-database -p 1433:1433 -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=ejwwSfDHQyufLtj2OVOJ6JAb6LEHq3N" -d mcr.microsoft.com/mssql/server:latest

run-queue: ## run queue separately (for local debugging)
	docker run --name emsisoft-test-queue -p 5672:5672 -p 15672:15672 -e "RABBITMQ_DEFAULT_USER=user" -e "RABBITMQ_DEFAULT_PASS=LLR7m4YW14FUZ6lrxME" -d rabbitmq:management

run: ## run services
	cd ./docker && docker-compose up -d

stop: ## stop services
	cd ./docker && docker-compose down